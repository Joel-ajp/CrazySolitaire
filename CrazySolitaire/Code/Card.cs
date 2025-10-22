using CrazySolitaire.Properties;

namespace CrazySolitaire;

public class Card {
    public CardType Type { get; private set; }
    public Suit Suit { get; private set; }
    public bool FaceUp { get; private set; }
    public PictureBox PicBox { get; private set; }
    public Bitmap PicImg {
        get => FaceUp ? Resources.ResourceManager.GetObject($"{Type.ToString().Replace("_", "").ToLower()}_of_{Suit.ToString().ToLower()}") as Bitmap
                      : Resources.back_green;
    }
    private Point dragOffset;
    private Point relLocBeforeDrag;
    private Control conBeforeDrag;
    private IDropTarget lastDropTarget;

    public Card(CardType type, Suit suit) {
        Type = type;
        Suit = suit;
        FaceUp = true;
        SetupPicBox();
    }

    private void SetupPicBox() {
        PicBox = new() {
            Width = 90,
            Height = 126,
            BackgroundImageLayout = ImageLayout.Stretch,
            BorderStyle = BorderStyle.FixedSingle,
            BackgroundImage = PicImg
        };
        PicBox.Click += (sender, e) => {
            if (!FaceUp && Game.CanFlipOver(this)) {
                FlipOver();
            }
        };
        PicBox.MouseDown += (sender, e) => {
            if (e.Button == MouseButtons.Left && Game.IsCardMovable(this)) {
                FrmGame.DragCard(this);
                dragOffset = e.Location;
                conBeforeDrag = PicBox.Parent;
                relLocBeforeDrag = PicBox.Location;
                conBeforeDrag.RemCard(this);
                FrmGame.Instance.AddCard(this);
                PicBox.Location = e.Location;
                PicBox.BringToFront();
            }
        };
        PicBox.MouseUp += (sender, e) => {
            if (FrmGame.IsDraggingCard(this)) {
                FrmGame.StopDragCard(this);
                Game.CallDragEndedOnAll();

                if (lastDropTarget is not null && lastDropTarget.CanDrop(this)) {
                    FrmGame.CardDraggedFrom.RemCard(this);
                    lastDropTarget.Dropped(this);
                    PicBox.BringToFront();

                    if (FrmGame.CardDraggedFrom is TableauStack) {
                        TableauStack fromStack = FrmGame.CardDraggedFrom as TableauStack;
                        if (fromStack.Cards.Count > 0){
                            var possiblyACard = fromStack.Cards.Last();
                            if (possiblyACard is Card) { possiblyACard.FlipOver(); }
                        }
                    }

                }
                else {
                    FrmGame.Instance.RemCard(this);
                    conBeforeDrag?.AddCard(this);
                    PicBox.Location = relLocBeforeDrag;
                    PicBox.BringToFront();
                }
            }
        };
        PicBox.MouseMove += (sender, e) => {
            if (FrmGame.CurDragCard == this) {

                var dragged = (Control)sender;
                Point screenPos = dragged.PointToScreen(e.Location);
                Point parentPos = dragged.Parent.PointToClient(screenPos);
                dragged.Left = screenPos.X - dragOffset.X;
                dragged.Top = screenPos.Y - dragOffset.Y;

                // Find the control currently under the mouse
                Control target = FrmGame.Instance.GetChildAtPoint(dragged.Parent.PointToClient(screenPos));

                // Avoid detecting the dragged control itself
                if (target is not null && target != dragged) {
                    var dropTarget = Game.FindDropTarget(target);
                    if (dropTarget is null) {
                        Game.CallDragEndedOnAll();
                    }
                    else if (dropTarget != lastDropTarget) {
                        lastDropTarget?.DragEnded();
                    }
                    if (dropTarget != FrmGame.CardDraggedFrom as IDropTarget) {
                        dropTarget?.DragOver(this);
                        lastDropTarget = dropTarget;
                    }
                }

                dragged.Location = new Point(
                    parentPos.X - dragOffset.X,
                    parentPos.Y - dragOffset.Y
                );
            }
        };
    }

    public void FlipOver() {
        FaceUp = !FaceUp;
        PicBox.BackgroundImage = PicImg;
    }

    public void AdjustLocation(int left, int top) {
        PicBox.Left = left;
        PicBox.Top = top;
    }
}

//testing testing