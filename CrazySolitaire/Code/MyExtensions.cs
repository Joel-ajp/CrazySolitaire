namespace CrazySolitaire;

public static class MyExtensions {
    public static void AddCard(this Control control, Card card) {
        if (card is not null) {
            control.Controls.Add(card.PicBox);
        }
    }
    public static void RemCard(this Control control, Card card) {
        if (card is not null) {
            control.Controls.Remove(card.PicBox);
        }
    }
}

//testing testing