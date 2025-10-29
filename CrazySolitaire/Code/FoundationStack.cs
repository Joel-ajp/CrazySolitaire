namespace CrazySolitaire;

// this class represents the stacks at the top of the screen
// that the player collects cards into
public class FoundationStack : IFindMoveableCards, IDropTarget, IDragFrom {
    // the control on the screen that visually holds the cards
    public Panel Panel { get; private set; }
    // the stack of cards being held
    public Stack<Card> Cards { get; private set; }
    // the suit of the stack, because each stack is for a specific suit
    public Suit Suit { get; private init; }
    // keeps track of what the highest card is that has been added to the
    // stack so far, for coin awarding purposes. It begins at a value just below
    // the lowest card
    private CardType highestCard = CardType.ACE - 1;

    // a simple constructor
    public FoundationStack(Panel panel, Suit suit) {
        Panel = panel;
        Cards = new();
        Suit = suit;
    }
    // a helper method that tells you what card is at the top of the stack, and thus
    // which card can be moved. It returns this card, without removing it from the stack,
    // as the only item in a list. In the case that the stack is empty, it returns an
    // empty list
    public List<Card> FindMoveableCards() => (Cards.Count > 0 ? [Cards.Peek()] : []);

    // highlights the stack area the appropriate color for if it can drop there or not
    // when dragged over
    public void DragOver(Card c) {
        if (CanDrop(c)) {
            Panel.BackColor = Color.Green;
        }
        else {
            Panel.BackColor = Color.Red;
        }
    }

    // a helper function to determine if a card can be dropped on this stack
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
        return suitCheck && typeCheck;
    }

    // the logic for actually adding a card to the stack when dropped here
    public void Dropped(Card c)
    {
        // put the card here and position it properly
        Cards.Push(c);
        FrmGame.Instance.RemCard(c);
        Panel.AddCard(c);
        c.AdjustLocation(0, 0);
        c.PicBox.BringToFront();

        // award coins to the player if this is the highest card
        // that has been added to this stack thus far
        if (c.Type > highestCard) {
            Game.Coins++;
            highestCard = c.Type;
            System.Diagnostics.Trace.WriteLine($"Coins: {Game.Coins}");
        }

        // increment how many moves the player has made
        Game.MoveCounter++;
        System.Diagnostics.Debug.WriteLine($"Moves: {Game.MoveCounter}");
        FrmGame.GameState();
    }

    // helper function to un-highlight the stack when the card that was dragged
    // over it has been releasesd
    public void DragEnded() {
        Panel.BackColor = Color.Transparent;
    }

    // remove a card from the stack
    public void RemCard(Card card) {

        if (Cards.Count == 0) return;

    var temp = new Stack<Card>();
        while (Cards.Count > 0) {
            var top = Cards.Pop();
            if (top == card) { break; }
            temp.Push(top);
        }

        while (temp.Count > 0) {
            Cards.Push(temp.Pop());
        }
    }

    // add a card to the stack. This exists to properly implement the interface that
    // this class implements. It's just a wrapper for Dropped()
    public void AddCard(Card card) {
        Dropped(card);
    }
}

//testing testing