using CrazySolitaire.Code;
using Timer = System.Windows.Forms.Timer;

namespace CrazySolitaire;

public static class Game {
    // Difficulty modes for controlling stock reload limits
    public enum DifficultyMode { Easy, Normal, Hard, Unlimited }

    // the title screen
    public static Form TitleForm { get; set; }
    // an instance of the deck of cards
    public static Deck Deck { get; private set; }
    //one foundation stack for each suit, referenced by the suit it's for
    public static Dictionary<Suit, FoundationStack> FoundationStacks { get; set; }
    // a list of instances of tableu stacks
    public static TableauStack[] TableauStacks;
    // an instance of the Talon
    public static Talon Talon { get; set; }
    // an integer keeping track of how many times the stock has been reloaded,
    // for explosion purposes
    public static int StockReloadCount { get; set; }
    // maximum allowed stock reloads for the current game (-1 means unlimited)
    public static int MaxStockReloads { get; private set; } = 3;
    // currently selected difficulty
    public static DifficultyMode Difficulty { get; private set; } = DifficultyMode.Normal;
    // coin multiplier tied to difficulty (Normal default => 3)
    public static int CoinMultiplier { get; private set; } = 3;
    // an integer keeping track of the number of moves the player has made, for
    // scorekeeping purposes

    //keep count the numebr of moves in game
    public static int MoveCounter { get; set; }

    // the stack with the old moves for undo purposes
    public static MovesStack MovesStack { get; set; }

    // a boolean property that returns true if the game is won, tracks how many cards are in each foundation stack
    public static bool IsGameWon => FoundationStacks.Values.All(fs => fs.Cards.Count == 13);
    //the players current score
    public static int Score { get; set; }
    // Raised whenever Coins changes
    public static event Action<int> CoinsChanged;

    // the getter and setter for the coins, which invokes
    // the CoinsChanged flag upon set
    public static int Coins
    {
        get => Properties.Settings.Default.Coins;
        set
        {
            if (Properties.Settings.Default.Coins == value) return;
            Properties.Settings.Default.Coins = value;
            Properties.Settings.Default.Save();
            CoinsChanged?.Invoke(value);
        }
    }

    // number of purchased reveal uses (not persisted)
    public static int RevealUses { 
        get => Properties.Settings.Default.RevealCards;
        set
        {
            if (Properties.Settings.Default.RevealCards == value) return;
            Properties.Settings.Default.RevealCards = value;
            Properties.Settings.Default.Save();
        } 
    }

    // whether double-coins was purchased for this session
    public static bool DoubleCoinsActive { get; set; } = false;

    // the getter and setter for the owned uno reverse cards
    public static int TalonShuffles
    {
        get => Properties.Settings.Default.TalonShuffles;
        set
        {
            if (Properties.Settings.Default.TalonShuffles == value) return;
            Properties.Settings.Default.TalonShuffles = value;
            Properties.Settings.Default.Save();
        }
    }


    // a boolean keeping track of whether or not the program should
    // be temporarily supressing move counting
    public static bool SuppressMoveCounting { get; set; }

    // Tracks temporary reveal state (cards flipped face-up due to reveal)
    private static readonly List<Card> _tempRevealed = new();
    public static bool TempRevealActive { get; private set; } = false;

    
    // Helper: award coins applying the current difficulty multiplier.
    // Positive amounts are multiplied; negative amounts (deductions) are not.
    public static void AddCoins(int amount)
    {
        if (amount == 0) return;
        // If DoubleCoins is active in the session, award twice as many coins
        int multiplier = CoinMultiplier * (DoubleCoinsActive ? 2 : 1);
        Coins += amount > 0 ? amount * multiplier : amount;
    }



    // a simple constructor, setting both MoveCounter and StockReloadCount
    // to zero, and setting SuppressMoveCounting to false
    static Game() {
        MoveCounter = 0;
        StockReloadCount = 0;
        SuppressMoveCounting = false;
    }

    // Configure difficulty and corresponding stock reload limits
    public static void ApplyDifficulty(DifficultyMode mode) {
        Difficulty = mode;
        MaxStockReloads = mode switch {
            DifficultyMode.Easy => 5,
            DifficultyMode.Normal => 3,
            DifficultyMode.Hard => 1,
            DifficultyMode.Unlimited => -1, // -1 indicates unlimited
            _ => 3
        };
        // Set coin multiplier per difficulty
        CoinMultiplier = mode switch {
            DifficultyMode.Unlimited => 1,
            DifficultyMode.Easy => 2,
            DifficultyMode.Normal => 3,
            DifficultyMode.Hard => 5,
            _ => 3
        };
    }

    // a helper method to initialize and populate all of the different objects
    // and collections of objects that make up the game
    public static void Init(Panel panTalon, Panel[] panTableauStacks, Dictionary<Suit, Panel> panFoundationStacks) {
        Deck = new();

        // reset counters for a fresh game
        MoveCounter = 0;
        StockReloadCount = 0;
        TempRevealActive = false;
        _tempRevealed.Clear();

        //Properties.Settings.Default.Reset();

        // create talon
        Talon = new(panTalon);

        // create tableau stacks
        TableauStacks = new TableauStack[7];
        for (int i = 0; i < TableauStacks.Length; i++) {
            TableauStacks[i] = new(panTableauStacks[i]);
        }

        // create foundation stacks
        FoundationStacks = new();
        foreach (var suit in Enum.GetValues<Suit>()) {
            FoundationStacks.Add(suit, new(panFoundationStacks[suit], suit));
        }

        // load tableau stacks
        const int VERT_OFFSET = 20;
        for (int i = 0; i < TableauStacks.Length; i++) {
            Card c;
            for (int j = 0; j < i; j++) {
                c = Deck.Acquire();
                c.FlipOver();
                c.AdjustLocation(0, j * VERT_OFFSET);
                TableauStacks[i].AddCard(c);
            }
            c = Deck.Acquire();
            c.AdjustLocation(0, i * VERT_OFFSET);
            TableauStacks[i].AddCard(c);
        }
    }
    
    // a helper function to check if a given card can be moved
    public static bool IsCardMovable(Card c) {
        bool isMovable = false;
        isMovable |= Talon.FindMoveableCards().Contains(c);
        foreach (var foundationStack in FoundationStacks) {
            isMovable |= foundationStack.Value.FindMoveableCards().Contains(c);
        }
        foreach (var tableauStack in TableauStacks) {
            isMovable |= tableauStack.FindMoveableCards().Contains(c);
        }
        return isMovable;
    }

    // a helper function to find the container a given card the player is
    // currently dragging was in prior to being dragged
    public static IDragFrom FindDragFrom(Card c) {
        if (Talon.Cards.Contains(c)) {
            return Talon;
        }
        foreach (var foundationStack in FoundationStacks) {
            if (foundationStack.Value.Cards.Contains(c)) {
                return foundationStack.Value;
            }
        }
        foreach (var tableauStack in TableauStacks) {
            if (tableauStack.Cards.Contains(c)) {
                return tableauStack;
            }
        }
        return null;
    }

    // a helper function to find which foundation stack or
    // tableu stack a given drop target is. This is important
    // because the drop target will be the control associated
    // with the object, and we need to get the object itself.
    public static IDropTarget FindDropTarget(Control c) {
        foreach (var foundationStack in FoundationStacks) {
            if (foundationStack.Value.Panel == c) {
                return foundationStack.Value;
            }
        }
        foreach (var tableauStack in TableauStacks) {
            if (tableauStack.Panel == c) {
                return tableauStack;
            }
        }
        return null;
    }

    // Centralized move registration: increments move counter and restores any temporary reveals
    public static void RegisterMove(Card c, IDragFrom f, IDropTarget t) {
        MoveCounter++;
        System.Diagnostics.Debug.WriteLine($"Moves: {MoveCounter}");
        // If a temporary reveal is active, restore it now
        RestoreTempReveal();

        // log the move with the MoveStack
        Move move;
        if (f is Deck d) { move = new Move(d); }
        else if (f is Talon ta) { move = new Move(ta); }
        else { move = new Move(c, f, t); }
        MovesStack.Log(move);
    }

    // Flip all currently face-down cards in the tableau stacks and mark them for restoration
    public static void RevealAllTableauFaceDown() {
        if (TempRevealActive) return;
        _tempRevealed.Clear();
        foreach (var tableau in TableauStacks) {
            foreach (var card in tableau.Cards) {
                if (!card.FaceUp) {
                    _tempRevealed.Add(card);
                    card.FlipOver();
                }
            }
        }
        TempRevealActive = _tempRevealed.Count > 0;
    }

    // Restore any cards flipped by a temporary reveal
    public static void RestoreTempReveal() {
        if (!TempRevealActive) return;
        foreach (var card in _tempRevealed) {
            if (card is not null && card.FaceUp) {
                card.FlipOver();
            }
        }
        _tempRevealed.Clear();
        TempRevealActive = false;
    }

    // a helper function to tell all potential drop locations
    // that a drag has ended. This will cause them to reset
    // any highlighting that they currently have.
    public static void CallDragEndedOnAll() {
        foreach (var foundationStack in FoundationStacks) {
            foundationStack.Value.DragEnded();
        }
        foreach (var tableauStack in TableauStacks) {
            tableauStack.DragEnded();
        }
    }

    // a helper function to determine if a given card is at the bottom
    // of a tableu stack for the purposes of verifying if it can be flipped
    // over. This is only used with manually flipping the cards over by
    // clicking them, which is functionality that, while still implemented
    // in the code, will never be used if something goes wrong with the
    // auto-flipping script. This function is thus, mostly obselete
    public static bool CanFlipOver(Card c) {
        foreach (var tableauStack in TableauStacks) {
            if (tableauStack.GetBottomCard() == c) {
                return true;
            }
        }
        return false;
    }

    // this is the logic for causing all the cards to explode
    public static void Explode() {
        // first, collect all the cards in play into one list,
        // no matter where on the screen they are
        List<Card> allCardsInPlay = new();
        foreach (var foundationStack in FoundationStacks) {
            allCardsInPlay.AddRange(foundationStack.Value.Cards);
        }
        foreach (var tableauStack in TableauStacks) {
            allCardsInPlay.AddRange(tableauStack.Cards);
        }
        allCardsInPlay.AddRange(Talon.Cards);
        foreach (Card c in allCardsInPlay) {
            Point origPos = c.PicBox.Location;
            origPos.X += c.PicBox.Parent.Location.X;
            origPos.Y += c.PicBox.Parent.Location.Y;
            c.PicBox.Parent.RemCard(c);
            FrmGame.Instance.AddCard(c);
            c.AdjustLocation(origPos.X, origPos.Y);
            c.PicBox.BringToFront();
        }
        // set the speed constants to move the cards at
        const int SPEED = 6;
        const int MORE_SPEED = 10;
        // create an array of possible directions and
        // speeds for the cards to get sent in
        Point[] possibleExplodeVectors = [
            new(0, SPEED),
            new(0, -SPEED),

            new(SPEED, 0),
            new(-SPEED, 0),
            
            new(SPEED, SPEED),
            new(-SPEED, SPEED),

            new(SPEED, -SPEED),
            new(-SPEED, -SPEED),

            new(SPEED, MORE_SPEED),
            new(-SPEED, MORE_SPEED),
            new(SPEED, -MORE_SPEED),
            new(-SPEED, -MORE_SPEED),

            new(MORE_SPEED, SPEED),
            new(-MORE_SPEED, SPEED),
            new(MORE_SPEED, -SPEED),
            new(-MORE_SPEED, -SPEED),
        ];
        // instantiate and allocate an array of the directions and
        // speeds to be used for each card, with one element per card
        Point[] explodeVectors = new Point[allCardsInPlay.Count];
        // populate explodeVectors with random values from possibleExplodeVectors
        Random rand = new();
        for (int i = 0; i < explodeVectors.Length; i++) {
            explodeVectors[i] = possibleExplodeVectors[rand.Next(possibleExplodeVectors.Length)];
        }
        // send each card flying in it's assigned direction at staggered times
        Timer tmr = new() { Interval = 25 };
        tmr.Tick += (sender, e) => {
            for (int i = 0; i < allCardsInPlay.Count; i++) {
                Card c = allCardsInPlay[i];
                c.AdjustLocation(c.PicBox.Location.X + explodeVectors[i].X, c.PicBox.Location.Y + explodeVectors[i].Y);
            }
        };
        tmr.Start();
    }
}