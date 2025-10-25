namespace CrazySolitaire;

public class FoundationStack : IFindMoveableCards, IDropTarget, IDragFrom {
    public Panel Panel { get; private set; }
    public Stack<Card> Cards { get; private set; }
    public Suit Suit { get; private init; }
    private CardType highestCard = CardType.ACE - 1;

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

        if (c.Type > highestCard) {
            Game.Coins++;
            highestCard = c.Type;
            System.Diagnostics.Debug.WriteLine($"Coins: {Game.Coins}");
        }

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

//testing testing