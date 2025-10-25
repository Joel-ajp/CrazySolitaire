using CrazySolitaire.Properties;

namespace CrazySolitaire {
    public partial class FrmGame : Form {
        public static Card CurDragCard { get; private set; }
        public static IDragFrom CardDraggedFrom { get; private set; }
        public static FrmGame Instance { get; private set; }

        // Track multi-card drag state
        public static List<Card> CurDragRun { get; private set; } = new();
        private static Dictionary<Card, (Control parent, Point relLoc)> _preDragInfo = new();

        protected override CreateParams CreateParams {
            get {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        public FrmGame() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            Instance = this;
            Panel[] panTableauStacks = new Panel[7];
            for (int i = 0; i < 7; i++) {
                panTableauStacks[i] = (Panel)Controls.Find($"panTableauStack_{i}", false)[0];
            }
            Dictionary<Suit, Panel> panFoundationStacks = new() {
                [Suit.DIAMONDS] = panFoundationStack_Diamonds,
                [Suit.SPADES] = panFoundationStack_Spades,
                [Suit.HEARTS] = panFoundationStack_Hearts,
                [Suit.CLUBS] = panFoundationStack_Clubs,
            };
            Game.Init(panTalon, panTableauStacks, panFoundationStacks);
        }

        private void pbStock_Click(object sender, EventArgs e) {
            if (pbStock.BackgroundImage is null) {
                Game.StockReloadCount++;
                if (Game.StockReloadCount > 3) {
                    Game.Explode();
                    MessageBox.Show("You computer has been infected with ransomware", "You have been infected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    FrmYouLose frmYouLose = new();
                    frmYouLose.Show();
                    Hide();
                }
                else {
                    Game.Talon.ReleaseIntoDeck(Game.Deck);
                    pbStock.BackgroundImage = Game.StockReloadCount switch {
                        1 => Resources.back_green,
                        2 => Resources.back_orange,
                        3 => Resources.back_red,
                        _ => pbStock.BackgroundImage
                    };
                }
            }
            else {
                for (int i = 0; i < 3; i++) {
                    Card c = Game.Deck.Acquire();
                    if (c != null) {
                        Game.Talon.AddCard(c);
                        c.AdjustLocation(i * 20, 0);
                        c.PicBox.BringToFront();
                    }
                }
                if (Game.Deck.IsEmpty()) {
                    pbStock.BackgroundImage = null;
                }
            }
        }

        public static void DragCard(Card c) {
            CurDragCard = c;
            CardDraggedFrom = Game.FindDragFrom(c);
        }
        public static void StopDragCard(Card c) {
            if (CurDragCard == c)
                CurDragCard = null;
        }
        public static bool IsDraggingCard(Card c) => CurDragCard == c;

        // Multi-card drag API
        public static void StartRunDrag(Card startCard, List<Card> run) {
            CurDragRun = run;
            CurDragCard = startCard;
            CardDraggedFrom = Game.FindDragFrom(startCard);
            _preDragInfo.Clear();

            // 1) Reparent to the form while preserving absolute positions
            foreach (var card in run) {
                var parent = card.PicBox.Parent;
                var loc = card.PicBox.Location;
                _preDragInfo[card] = (parent, loc);
                parent.RemCard(card);
                var abs = new Point(parent.Left + loc.X, parent.Top + loc.Y);
                Instance.AddCard(card);
                card.AdjustLocation(abs.X, abs.Y);
            }
            // 2) Fix z-order: ensure lower (greater Y) cards are on top during drag
            // The run list is ordered from start (higher up) to bottom (lower down),
            // so bringing to front in this order yields bottom-most last (front-most).
            foreach (var card in run) {
                card.PicBox.BringToFront();
            }
        }
        public static void MoveRunBy(Point delta) {
            foreach (var card in CurDragRun) {
                card.AdjustLocation(card.PicBox.Location.X + delta.X, card.PicBox.Location.Y + delta.Y);
            }
        }
        public static void CancelRunDrag() {
            // Return all cards to their original parent and relative location
            foreach (var card in CurDragRun) {
                if (_preDragInfo.TryGetValue(card, out var info)) {
                    Instance.RemCard(card);
                    info.parent.AddCard(card);
                    card.AdjustLocation(info.relLoc.X, info.relLoc.Y);
                }
            }
            // Rebuild original stack layout if tableau
            if (CardDraggedFrom is TableauStack srcTs) {
                srcTs.RebuildLayout();
            }
            CurDragRun.Clear();
            CurDragCard = null;
        }
        public static void CompleteRunDrop(TableauStack target) {
            // Drop each card in order onto the target tableau (maintains visual order)
            bool counted = false;
            foreach (var card in CurDragRun) {
                Game.SuppressMoveCounting = true;
                target.Dropped(card);
                Game.SuppressMoveCounting = false;
                if (!counted) {
                    Game.MoveCounter++;
                    System.Diagnostics.Debug.WriteLine($"Moves: {Game.MoveCounter}");
                    counted = true;
                }
            }
            // Ensure target layout/z-order is perfect
            target.RebuildLayout();
            CurDragRun.Clear();
            CurDragCard = null;
        }

        private void FrmGame_FormClosing(object sender, FormClosingEventArgs e) {
            Game.TitleForm.Close();
        }
    }
}
