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
        // A card is movable if it belongs to the longest valid run (face-up, alternating colors, descending by 1)
        // ending at the bottom of this tableau stack. Any card within that run can be the start of a drag.
        if (Cards.Count == 0) return [];
        List<Card> movable = new();
        var node = Cards.Last;
        Card prevBelow = null; // card immediately below the current iterated card (towards bottom)
        while (node is not null && node.Value.FaceUp) {
            if (prevBelow is null) {
                // Bottom-most face-up card is always movable
                movable.Add(node.Value);
            }
            else {
                bool suitCheck = ((int)node.Value.Suit % 2 != (int)prevBelow.Suit % 2);
                bool typeCheck = node.Value.Type == prevBelow.Type + 1;
                if (!(suitCheck && typeCheck)) break;
                movable.Add(node.Value);
            }
            prevBelow = node.Value;
            node = node.Previous;
        }
        return movable; // order does not matter for membership checks
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
        if (!Game.SuppressMoveCounting) {
            Game.MoveCounter++;
            System.Diagnostics.Debug.WriteLine($"Moves: {Game.MoveCounter}");
        }
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

    // Helper: get the draggable run starting at a given card (inclusive) down to the bottom,
    // ensuring the run remains valid (face-up, alternating colors, descending by 1).
    public List<Card> GetDraggableRunStartingAt(Card start) {
        List<Card> run = new();
        var node = Cards.Find(start);
        if (node is null || !node.Value.FaceUp) return run;
        run.Add(node.Value);
        var next = node.Next;
        while (next is not null) {
            var above = node.Value;
            var below = next.Value;
            if (!below.FaceUp) break;
            bool suitCheck = ((int)above.Suit % 2 != (int)below.Suit % 2);
            bool typeCheck = above.Type == below.Type + 1;
            if (!(suitCheck && typeCheck)) break;
            run.Add(below);
            node = next;
            next = next.Next;
        }
        return run;
    }

    // Rebuilds the panel controls' z-order and positions to match the logical order in Cards
    public void RebuildLayout() {
        Panel.SuspendLayout();
        try {
            Panel.Controls.Clear();
            int i = 0;
            foreach (var card in Cards) {
                Panel.AddCard(card);
                card.AdjustLocation(0, i * 20);
                card.PicBox.BringToFront();
                i++;
            }
        }
        finally {
            Panel.ResumeLayout();
        }
    }
}

//testing testing