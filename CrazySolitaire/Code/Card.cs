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
                                if (possiblyACard is Card) { possiblyACard.FlipOver(); }
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

                        if (fromBefore is TableauStack fromStack2) {
                            if (fromStack2.Cards.Count > 0){
                                var possiblyACard = fromStack2.Cards.Last();
                                if (possiblyACard is Card) { possiblyACard.FlipOver(); }
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