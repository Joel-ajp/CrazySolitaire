namespace CrazySolitaire;

// this class represents the Talon, which is the stack of
// cards that have been drawn from the Stock already
public class Talon : IFindMoveableCards, IDragFrom {
    // this is the control in the form which visually holds the
    // cards
    public Panel Panel { get; private set; }
    // this is where the cards are actually stored
    public Stack<Card> Cards { get; private set; }

    // a simple constructor
    public Talon(Panel pan) {
        Panel = pan;
        Cards = new();
    }

    // this is a helper function to send the cards
    // in the talon back into the deck in order,
    // removing them from the talon in the process
    public void ReleaseIntoDeck(Deck deck) {
        foreach (var card in Cards) {
            deck.Release(card);
            Panel.RemCard(card);
        }
        Cards.Clear();
    }

    // this is a helper function to add a card
    // to the talon stack
    public void AddCard(Card c) {
        Cards.Push(c);
        Panel.AddCard(c);
    }

    // this is a helper function that finds the cards in the talon which can
    // be moved, which would be only the top card in the stack. It returns this card
    // as the only item in a list. In the event that the stack is empty, it returns
    // an empty list
    public List<Card> FindMoveableCards() => (Cards.Count > 0 ? [Cards.Peek()] : []);

    // this is a helper function to remove a given card from the stack, but only if
    // it is actually the top card
    public void RemCard(Card card) {
        if (Cards.Peek() == card) {
            Cards.Pop();
        }
    }
}

//testing testing