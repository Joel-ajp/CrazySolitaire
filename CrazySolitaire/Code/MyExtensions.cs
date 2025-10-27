namespace CrazySolitaire;

public static class MyExtensions {
    // a helper function too add a card to a control
    public static void AddCard(this Control control, Card card) {
        if (card is not null) {
            control.Controls.Add(card.PicBox);
        }
    }
    // a helper function to remove a card from a control
    public static void RemCard(this Control control, Card card) {
        if (card is not null) {
            control.Controls.Remove(card.PicBox);
        }
    }
}

//testing testing