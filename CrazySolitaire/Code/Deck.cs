namespace CrazySolitaire;

// this class represents the draw deck, also called the stock.
// it is a typical implementation of the object pool design pattern.
public class Deck {
    // this is what actually holds the cards
    private Queue<Card> cards;

    // technically the constructor. Functionally, it's just a
    // wrapper for RegeneratePool
    public Deck() {
        RegeneratePool();
    }

    // a helper function to populate the deck with exactly one of
    // each card and then shuffle it
    private void RegeneratePool() {
        cards = new();
        // create one of each card and enqueue it to the cards queue
        foreach (var cardType in Enum.GetValues<CardType>()) {
            foreach (var suit in Enum.GetValues<Suit>()) {
                cards.Enqueue(new(cardType, suit));
            }
        }
        // shuffle
        Random rng = new();
        cards = new(cards.OrderBy(_ => rng.Next()));
    }
    // true when the deck is out of cards
    public bool IsEmpty() => cards.Count == 0;
    // helper function to draw a card. Removes it from the queue and returns it.
    // returns null if the deck is empty
    public Card Acquire() => (cards.Count > 0 ? cards.Dequeue() : null);
    // helper function to return a card to the deck. Puts it at the bottom of the deck
    // which will help the order remain the same provided the cards are returned in the
    // same order they were drawn
    public void Release(Card c) => cards.Enqueue(c);
}

//testing testing