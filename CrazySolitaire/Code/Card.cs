using CrazySolitaire.Properties;

namespace CrazySolitaire;

// This class represents a single card
public class Card {
    // The Value of the card, K-A
    public CardType Type { get; private set; }
    // The suit of the card. With the numerical representation
    // of the enum, even numbers are red, Odd numbers are black
    public Suit Suit { get; private set; }
    // true means that the face of the card is showing, false means
    // the back of the card is showing
    public bool FaceUp { get; private set; }
    // the control on the form that is the card
    public PictureBox PicBox { get; private set; }
    // The image to be displayed on the card, automatically figures out
    // which file to use based on other fields, FaceUp, Type, and Suit
    public Bitmap PicImg {
        get => FaceUp ? Resources.ResourceManager.GetObject($"{Type.ToString().Replace("_", "").ToLower()}_of_{Suit.ToString().ToLower()}") as Bitmap
                      : Resources.back_green;
    }
    // the XY coordinate of where the card should be on be on screen at
    // any given frame while being dragged
    private Point dragOffset;
    // the XY coordinate of where the card was on screen prior to being
    // draged relative to the container it was in
    private Point relLocBeforeDrag;
    // the container the card was in prior to being dragged
    private Control conBeforeDrag;
    // the container that was last hovered over while dragging the card
    // which the player would be trying to drop the card on if they
    // release the mouse button during this frame
    private IDropTarget lastDropTarget;

    // simple constructor
    public Card(CardType type, Suit suit) {
        Type = type;
        Suit = suit;
        FaceUp = true;
        SetupPicBox();
    }

    // This method is called by the constructor. It sets
    // up the visuals of the card and also contains
    // event handlers for when the card is clicked and
    // dragged around
    private void SetupPicBox() {
        // sets up fhe card's visual settings
        PicBox = new() {
            Width = (int)(FrmGame.Instance.Width * 0.075),
            Height = (int)(FrmGame.Instance.Height*0.156),
            BackgroundImageLayout = ImageLayout.Stretch,
            BorderStyle = BorderStyle.FixedSingle,
            BackgroundImage = PicImg
        };
        // flips over the card when clicked
        PicBox.Click += (sender, e) => {
            if (!FaceUp && Game.CanFlipOver(this)) {
                FlipOver();
            }
        };
        // handles the card being dragged around
        PicBox.MouseDown += (sender, e) => {
            if (e.Button == MouseButtons.Left && Game.IsCardMovable(this)) {
                // Determine if this is part of a tableau run drag
                var dragFrom = Game.FindDragFrom(this);
                if (dragFrom is TableauStack ts) {
                    var run = ts.GetDraggableRunStartingAt(this);
                    if (run.Count > 1) {
                        FrmGame.StartRunDrag(this, run);
                    }
                    else {
                        FrmGame.DragCard(this);
                        conBeforeDrag = PicBox.Parent;
                        relLocBeforeDrag = PicBox.Location;
                        conBeforeDrag.RemCard(this);
                        FrmGame.Instance.AddCard(this);
                        // Preserve absolute position when reparenting
                        var abs = new Point(conBeforeDrag.Left + relLocBeforeDrag.X, conBeforeDrag.Top + relLocBeforeDrag.Y);
                        PicBox.Location = abs;
                        PicBox.BringToFront();
                    }
                }
                else {
                    // Non-tableau sources remain single-card
                    FrmGame.DragCard(this);
                    conBeforeDrag = PicBox.Parent;
                    relLocBeforeDrag = PicBox.Location;
                    conBeforeDrag.RemCard(this);
                    FrmGame.Instance.AddCard(this);
                    var abs = new Point(conBeforeDrag.Left + relLocBeforeDrag.X, conBeforeDrag.Top + relLocBeforeDrag.Y);
                    PicBox.Location = abs;
                    PicBox.BringToFront();
                }
                dragOffset = e.Location;
            }
        };
        // handles dropping a card that has been dragged
        PicBox.MouseUp += (sender, e) => {
            if (FrmGame.IsDraggingCard(this) || (FrmGame.CurDragRun.Count > 0 && FrmGame.CurDragRun[0] == this)) {
                FrmGame.StopDragCard(this);
                Game.CallDragEndedOnAll();

                if (FrmGame.CurDragRun.Count > 0) {
                    // Multi-card run drag
                    if (lastDropTarget is TableauStack tStack && tStack.CanDrop(this)) {
                        // Remove from source and drop the whole run
                        foreach (var card in FrmGame.CurDragRun) {
                            FrmGame.CardDraggedFrom.RemCard(card);
                        }
                        FrmGame.CompleteRunDrop(tStack);

                        // Flip next card in from-stack if any
                        if (FrmGame.CardDraggedFrom is TableauStack fromStack) {
                            if (fromStack.Cards.Count > 0) {
                                var possiblyACard = fromStack.Cards.Last();
                                if (possiblyACard is Card && possiblyACard.FaceUp == false) { possiblyACard.FlipOver(); }
                            }
                            // Ensure ordering is consistent
                            fromStack.RebuildLayout();
                        }
                    }
                    else {
                        FrmGame.CancelRunDrag();
                    }
                }
                else {
                    // Single card drag behavior
                    if (lastDropTarget is not null && lastDropTarget.CanDrop(this)) {
                        var fromBefore = FrmGame.CardDraggedFrom;
                        FrmGame.CardDraggedFrom.RemCard(this);
                        lastDropTarget.Dropped(this);
                        PicBox.BringToFront();

                        // Flip next card in from-stack if any
                        if (fromBefore is TableauStack fromStack2) {
                            if (fromStack2.Cards.Count > 0){
                                var possiblyACard = fromStack2.Cards.Last();
                                if (possiblyACard is Card && possiblyACard.FaceUp == false) { possiblyACard.FlipOver(); }
                            }
                            fromStack2.RebuildLayout();
                        }

                    }
                    else {
                        FrmGame.Instance.RemCard(this);
                        conBeforeDrag?.AddCard(this);
                        PicBox.Location = relLocBeforeDrag;
                        PicBox.BringToFront();
                        if (conBeforeDrag is Panel p) {
                            // If returning to a tableau panel, rebuild layout for safety
                            var ts = Game.TableauStacks.FirstOrDefault(t => t.Panel == p);
                            ts?.RebuildLayout();
                        }
                    }
                }
            }
        };
        // handle making the card or run of cards follow the mouse around during
        // dragging, and also keep lastDropTarget up to date
        PicBox.MouseMove += (sender, e) => {
            if (FrmGame.CurDragRun.Count > 0 && FrmGame.CurDragRun[0] == this) {
                var dragged = (Control)sender;
                Point screenPos = dragged.PointToScreen(e.Location);
                Point parentPos = dragged.Parent.PointToClient(screenPos);

                // Determine potential drop panel by bounds to avoid hitting dragged cards
                Point formPt = FrmGame.Instance.PointToClient(screenPos);
                Control targetPanel = null;
                foreach (var ts in Game.TableauStacks) {
                    if (ts.Panel.Bounds.Contains(formPt)) targetPanel = ts.Panel;
                }
                foreach (var fs in Game.FoundationStacks) {
                    if (fs.Value.Panel.Bounds.Contains(formPt)) targetPanel = fs.Value.Panel;
                }
                var dropTarget = Game.FindDropTarget(targetPanel);
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

                // Move the entire run by the delta of the top card
                Point newLoc = new(parentPos.X - dragOffset.X, parentPos.Y - dragOffset.Y);
                Point delta = new(newLoc.X - dragged.Left, newLoc.Y - dragged.Top);
                FrmGame.MoveRunBy(delta);
            }
            else if (FrmGame.CurDragRun.Count == 0 && FrmGame.CurDragCard == this) {
                var dragged = (Control)sender;
                Point screenPos = dragged.PointToScreen(e.Location);
                Point parentPos = dragged.Parent.PointToClient(screenPos);

                // Determine potential drop panel by bounds to avoid hitting dragged card
                Point formPt = FrmGame.Instance.PointToClient(screenPos);
                Control targetPanel = null;
                foreach (var ts in Game.TableauStacks) {
                    if (ts.Panel.Bounds.Contains(formPt)) targetPanel = ts.Panel;
                }
                foreach (var fs in Game.FoundationStacks) {
                    if (fs.Value.Panel.Bounds.Contains(formPt)) targetPanel = fs.Value.Panel;
                }
                var dropTarget = Game.FindDropTarget(targetPanel);
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

                // Move the single card
                dragged.Location = new Point(
                    parentPos.X - dragOffset.X,
                    parentPos.Y - dragOffset.Y
                );
            }
        };
    }

    // a helper function to flip over the card by
    // toggling FaceUp and updating the background
    // image to match the new PicImg
    public void FlipOver() {
        FaceUp = !FaceUp;
        PicBox.BackgroundImage = PicImg;
    }

    // a helper function to adjust the location of a
    // card by a given amount
    public void AdjustLocation(int left, int top) {
        PicBox.Left = left;
        PicBox.Top = top;
    }
}

//testing testing