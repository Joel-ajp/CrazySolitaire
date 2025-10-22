namespace CrazySolitaire;

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

//testing testing