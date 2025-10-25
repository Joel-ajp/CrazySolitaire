using Timer = System.Windows.Forms.Timer;

namespace CrazySolitaire;

public static class Game {
    public static Form TitleForm { get; set; }
    public static Deck Deck { get; private set; }
    public static Dictionary<Suit, FoundationStack> FoundationStacks { get; set; }
    public static TableauStack[] TableauStacks;
    public static Talon Talon { get; set; }
    public static int StockReloadCount { get; set; }
    public static int MoveCounter { get; set; }
    // Raised whenever Coins changes
    public static event Action<int> CoinsChanged;

    private static int _coins;
    public static int Coins
    {
        get => _coins;
        set
        {
            if (_coins == value) return;
            _coins = value;
            CoinsChanged?.Invoke(_coins);
        }
    }

    static Game() {
        MoveCounter = 0;
        StockReloadCount = 0;
    }

    public static void Init(Panel panTalon, Panel[] panTableauStacks, Dictionary<Suit, Panel> panFoundationStacks) {
        Deck = new();

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

    public static void CallDragEndedOnAll() {
        foreach (var foundationStack in FoundationStacks) {
            foundationStack.Value.DragEnded();
        }
        foreach (var tableauStack in TableauStacks) {
            tableauStack.DragEnded();
        }
    }

    public static bool CanFlipOver(Card c) {
        foreach (var tableauStack in TableauStacks) {
            if (tableauStack.GetBottomCard() == c) {
                return true;
            }
        }
        return false;
    }

    public static void Explode() {
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
        const int SPEED = 6;
        const int MORE_SPEED = 10;
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
        Point[] explodeVectors = new Point[allCardsInPlay.Count];
        Random rand = new();
        for (int i = 0; i < explodeVectors.Length; i++) {
            explodeVectors[i] = possibleExplodeVectors[rand.Next(possibleExplodeVectors.Length)];
        }
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

//testing testing testing