namespace CrazySolitaire;

// this class represents the tableu stacks, those stacks of cards in
// the bottom/middle of the screen in which the majority of the gameplay
// occurs
public class TableauStack : IFindMoveableCards, IDropTarget, IDragFrom {
    // this is the control in the form which visibly contains the stack on screen
    public Panel Panel { get; set; }
    // this is where the cards are actually stored
    public LinkedList<Card> Cards { get; private set; }

    // a simple constructor
    public TableauStack(Panel panel) {
        Panel = panel;
        Cards = new();
    }

    // a helper function to add a new card to the end of the list
    public void AddCard(Card c) {
        Cards.AddLast(c);
        Panel.AddCard(c);
        c.PicBox.BringToFront();
    }

    // a helper function that returns a list of cards that are movable
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

    // highlights the stack the correct color when a card is draged over it
    // depending on whether the card can be dropped there
    public void DragOver(Card c) {
        if (CanDrop(c)) {
            Panel.BackColor = Color.Green;
        }
        else {
            Panel.BackColor = Color.Red;
        }
    }

    // determine if a given card is able to be dropped in this stack
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

    // the logic to actually drop a given card into this stack
    public void Dropped(Card c) {
        // add the card to this stack
        Cards.AddLast(c);
        FrmGame.Instance.RemCard(c);
        Panel.AddCard(c);
        // adjust it's location accordingly
        c.AdjustLocation(0, (Cards.Count - 1) * 20);
        c.PicBox.BringToFront();
        Panel.Refresh();
        c.PicBox.BringToFront();
        // increment the count of how many moves the player has made
        if (!Game.SuppressMoveCounting) {
            Game.MoveCounter++;
            System.Diagnostics.Debug.WriteLine($"Moves: {Game.MoveCounter}");
        }
    }

    // a helper function to un-highlight the stack when the
    // card that was dragged over it is no longer being dragged
    public void DragEnded() {
        Panel.BackColor = Color.Transparent;
    }

    // a helper function to get the card at the bottom of the stack
    public Card GetBottomCard() {
        return Cards.Count > 0 ? Cards.Last.Value : null;
    }

    // a helper function to remove a card from the stack
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