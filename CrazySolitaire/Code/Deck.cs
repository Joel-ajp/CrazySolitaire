namespace CrazySolitaire;

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

//testing testing