namespace CrazySolitaire {
    public partial class FrmYouLose : Form {
        public FrmYouLose() {
            InitializeComponent();
        }

        private void FrmYouLose_Load(object sender, EventArgs e) {
            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.Bounds.Height;
        }

        private void FrmYouLose_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode is Keys.Escape or Keys.Enter) {
                Close();
                Game.TitleForm.Close();
            }
        }
    }
}
