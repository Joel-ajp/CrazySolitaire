namespace CrazySolitaire;

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

//testing testing