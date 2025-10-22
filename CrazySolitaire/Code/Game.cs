using CrazySolitaire.Properties;
using Timer = System.Windows.Forms.Timer;

namespace CrazySolitaire;

public enum CardType {
    ACE,
    _2,
    _3,
    _4,
    _5,
    _6,
    _7,
    _8,
    _9,
    _10,
    JACK,
    QUEEN,
    KING
}

public enum Suit {
    DIAMONDS,
    SPADES,
    HEARTS,
    CLUBS
}

public interface IDragFrom {
    public void RemCard(Card card);
    public void AddCard(Card card);
}

public interface IFindMoveableCards {
    public List<Card> FindMoveableCards();
}

public interface IDropTarget {
    public void DragOver(Card c);
    public bool CanDrop(Card c);
    public void DragEnded();
    public void Dropped(Card c);
}

public static class MyExtensions {
    public static void AddCard(this Control control, Card card) {
        if (card is not null) {
            control.Controls.Add(card.PicBox);
        }
    }
    public static void RemCard(this Control control, Card card) {
        if (card is not null) {
            control.Controls.Remove(card.PicBox);
        }
    }
}

public class Deck {
    private Queue<Card> cards;

    public Deck() {
        RegeneratePool();
    }

    private void RegeneratePool() {
        cards = new();
        foreach (var cardType in Enum.GetValues<CardType>()) {
            foreach (var suit in Enum.GetValues<Suit>()) {
                cards.Enqueue(new(cardType, suit));
            }
        }
        // shuffle
        Random rng = new();
        cards = new(cards.OrderBy(_ => rng.Next()));
    }

    public bool IsEmpty() => cards.Count == 0;
    public Card Acquire() => (cards.Count > 0 ? cards.Dequeue() : null);
    public void Release(Card c) => cards.Enqueue(c);
}

public class Card {
    public CardType Type { get; private set; }
    public Suit Suit { get; private set; }
    public bool FaceUp { get; private set; }
    public PictureBox PicBox { get; private set; }
    public Bitmap PicImg {
        get => FaceUp ? Resources.ResourceManager.GetObject($"{Type.ToString().Replace("_", "").ToLower()}_of_{Suit.ToString().ToLower()}") as Bitmap
                      : Resources.back_green;
    }
    private Point dragOffset;
    private Point relLocBeforeDrag;
    private Control conBeforeDrag;
    private IDropTarget lastDropTarget;

    public Card(CardType type, Suit suit) {
        Type = type;
        Suit = suit;
        FaceUp = true;
        SetupPicBox();
    }

    private void SetupPicBox() {
        PicBox = new() {
            Width = 90,
            Height = 126,
            BackgroundImageLayout = ImageLayout.Stretch,
            BorderStyle = BorderStyle.FixedSingle,
            BackgroundImage = PicImg
        };
        PicBox.Click += (sender, e) => {
            if (!FaceUp && Game.CanFlipOver(this)) {
                FlipOver();
            }
        };
        PicBox.MouseDown += (sender, e) => {
            if (e.Button == MouseButtons.Left && Game.IsCardMovable(this)) {
                FrmGame.DragCard(this);
                dragOffset = e.Location;
                conBeforeDrag = PicBox.Parent;
                relLocBeforeDrag = PicBox.Location;
                conBeforeDrag.RemCard(this);
                FrmGame.Instance.AddCard(this);
                PicBox.Location = e.Location;
                PicBox.BringToFront();
            }
        };
        PicBox.MouseUp += (sender, e) => {
            if (FrmGame.IsDraggingCard(this)) {
                FrmGame.StopDragCard(this);
                Game.CallDragEndedOnAll();

                if (lastDropTarget is not null && lastDropTarget.CanDrop(this)) {
                    FrmGame.CardDraggedFrom.RemCard(this);
                    lastDropTarget.Dropped(this);
                    PicBox.BringToFront();

                    if (FrmGame.CardDraggedFrom is TableauStack) {
                        TableauStack fromStack = FrmGame.CardDraggedFrom as TableauStack;
                        if (fromStack.Cards.Count > 0){
                            var possiblyACard = fromStack.Cards.Last();
                            if (possiblyACard is Card) { possiblyACard.FlipOver(); }
                        }
                    }

                }
                else {
                    FrmGame.Instance.RemCard(this);
                    conBeforeDrag?.AddCard(this);
                    PicBox.Location = relLocBeforeDrag;
                    PicBox.BringToFront();
                }
            }
        };
        PicBox.MouseMove += (sender, e) => {
            if (FrmGame.CurDragCard == this) {

                var dragged = (Control)sender;
                Point screenPos = dragged.PointToScreen(e.Location);
                Point parentPos = dragged.Parent.PointToClient(screenPos);
                dragged.Left = screenPos.X - dragOffset.X;
                dragged.Top = screenPos.Y - dragOffset.Y;

                // Find the control currently under the mouse
                Control target = FrmGame.Instance.GetChildAtPoint(dragged.Parent.PointToClient(screenPos));

                // Avoid detecting the dragged control itself
                if (target is not null && target != dragged) {
                    var dropTarget = Game.FindDropTarget(target);
                    if (dropTarget is null) {
                        Game.CallDragEndedOnAll();
                    }
                    else if (dropTarget != lastDropTarget) {
                        lastDropTarget?.DragEnded();
                    }
                    if (dropTarget != FrmGame.CardDraggedFrom as IDropTarget) {
                        dropTarget?.DragOver(this);
                        lastDropTarget = dropTarget;
                    }
                }

                dragged.Location = new Point(
                    parentPos.X - dragOffset.X,
                    parentPos.Y - dragOffset.Y
                );
            }
        };
    }

    public void FlipOver() {
        FaceUp = !FaceUp;
        PicBox.BackgroundImage = PicImg;
    }

    public void AdjustLocation(int left, int top) {
        PicBox.Left = left;
        PicBox.Top = top;
    }
}

public class TableauStack : IFindMoveableCards, IDropTarget, IDragFrom {
    public Panel Panel { get; set; }
    public LinkedList<Card> Cards { get; private set; }

    public TableauStack(Panel panel) {
        Panel = panel;
        Cards = new();
    }

    public void AddCard(Card c) {
        Cards.AddLast(c);
        Panel.AddCard(c);
        c.PicBox.BringToFront();
    }

    public List<Card> FindMoveableCards() {
        return Cards.Count > 0 ? [Cards.Last.Value] : [];
    }

    public void DragOver(Card c) {
        if (CanDrop(c)) {
            Panel.BackColor = Color.Green;
        }
        else {
            Panel.BackColor = Color.Red;
        }
    }

    public bool CanDrop(Card c) {
        if (Cards.Count == 0) {
            return c.Type == CardType.KING;
        }
        else {
            Card lastCard = Cards.Last.Value;
            bool suitCheck = ((int)lastCard.Suit % 2 != (int)c.Suit % 2);
            bool typeCheck = lastCard.Type == c.Type + 1;
            return (suitCheck && typeCheck);
        }
    }

    public void Dropped(Card c) {
        Cards.AddLast(c);
        FrmGame.Instance.RemCard(c);
        Panel.AddCard(c);
        c.AdjustLocation(0, (Cards.Count - 1) * 20);
        c.PicBox.BringToFront();
        Panel.Refresh();
        c.PicBox.BringToFront();
        Game.MoveCounter++;
        System.Diagnostics.Debug.WriteLine($"Moves: {Game.MoveCounter}");
    }

    public void DragEnded() {
        Panel.BackColor = Color.Transparent;
    }

    public Card GetBottomCard() {
        return Cards.Count > 0 ? Cards.Last.Value : null;
    }

    public void RemCard(Card card) {
        Cards.Remove(card);
    }
}

public class Talon : IFindMoveableCards, IDragFrom {
    public Panel Panel { get; private set; }
    public Stack<Card> Cards { get; private set; }

    public Talon(Panel pan) {
        Panel = pan;
        Cards = new();
    }

    public void ReleaseIntoDeck(Deck deck) {
        foreach (var card in Cards) {
            deck.Release(card);
            Panel.RemCard(card);
        }
        Cards.Clear();
    }

    public void AddCard(Card c) {
        Cards.Push(c);
        Panel.AddCard(c);
    }

    public List<Card> FindMoveableCards() => (Cards.Count > 0 ? [Cards.Peek()] : []);

    public void RemCard(Card card) {
        if (Cards.Peek() == card) {
            Cards.Pop();
        }
    }
}

public class FoundationStack : IFindMoveableCards, IDropTarget, IDragFrom {
    public Panel Panel { get; private set; }
    public Stack<Card> Cards { get; private set; }
    public Suit Suit { get; private init; }

    public FoundationStack(Panel panel, Suit suit) {
        Panel = panel;
        Cards = new();
        Suit = suit;
    }

    public List<Card> FindMoveableCards() => (Cards.Count > 0 ? [Cards.Peek()] : []);

    public void DragOver(Card c) {
        if (CanDrop(c)) {
            Panel.BackColor = Color.Green;
        }
        else {
            Panel.BackColor = Color.Red;
        }
    }

    public bool CanDrop(Card c) {
        Card topCard = Cards.Count > 0 ? Cards.Peek() : null;
        bool suitCheck;
        bool typeCheck;

        suitCheck = Suit == c.Suit;
        if (topCard is null) {
            typeCheck = c.Type == CardType.ACE;
        }
        else {
            typeCheck = topCard.Type == c.Type - 1;
        }
        if (typeCheck && suitCheck) {
            Panel.BackColor = Color.Green;
        }
        else {
            Panel.BackColor = Color.Red;
        }
        return suitCheck && typeCheck;
    }

    public void Dropped(Card c)
    {
        Cards.Push(c);
        FrmGame.Instance.RemCard(c);
        Panel.AddCard(c);
        c.AdjustLocation(0, 0);
        c.PicBox.BringToFront();
        Game.MoveCounter++;
        System.Diagnostics.Debug.WriteLine($"Moves: {Game.MoveCounter}");
    }

    public void DragEnded() {
        Panel.BackColor = Color.Transparent;
    }

    public void RemCard(Card card) {
        List<Card> cards = Cards.ToList<Card>();
        cards.Remove(card);
        Cards = new Stack<Card>(cards);
    }

    public void AddCard(Card card) {
        Dropped(card);
    }
}

public static class Game {
    public static Form TitleForm { get; set; }
    public static Deck Deck { get; private set; }
    public static Dictionary<Suit, FoundationStack> FoundationStacks { get; set; }
    public static TableauStack[] TableauStacks;
    public static Talon Talon { get; set; }
    public static int StockReloadCount { get; set; }
    public static int MoveCounter { get; set; }

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

//testing testing