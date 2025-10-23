namespace CrazySolitaire;

public interface IDropTarget {
    public void DragOver(Card c);
    public bool CanDrop(Card c);
    public void DragEnded();
    public void Dropped(Card c);
}

//testing testing