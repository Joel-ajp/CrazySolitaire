using CrazySolitaire.Code;

namespace CrazySolitaire {
    public partial class FrmTitle : Form
    {
        public FrmTitle()
        {
            InitializeComponent();
        }

        private void FrmTitle_Load(object sender, EventArgs e)
        {
            Game.TitleForm = this;
            // Ensure difficulty is applied based on current combo selection
            // Default index is set in designer to Normal (index 1).
            ApplyDifficultyFromCombo();
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            FrmGame frmGame = new();
            frmGame.Show();
            Hide();
        }

        // Added on difficulty-modes branch
        private void cboDifficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyDifficultyFromCombo();
        }

        private void ApplyDifficultyFromCombo()
        {
            if (cboDifficulty == null) return;

            var sel = cboDifficulty.SelectedItem?.ToString();
            switch (sel)
            {
                case "Easy":
                    Game.ApplyDifficulty(Game.DifficultyMode.Easy);
                    break;
                case "Normal":
                    Game.ApplyDifficulty(Game.DifficultyMode.Normal);
                    break;
                case "Hard":
                    Game.ApplyDifficulty(Game.DifficultyMode.Hard);
                    break;
                case "Unlimited":
                    Game.ApplyDifficulty(Game.DifficultyMode.Unlimited);
                    break;
                default:
                    Game.ApplyDifficulty(Game.DifficultyMode.Normal);
                    break;
            }
        }

        // Added on main branch
        private void btnOpenShop_Click(object sender, EventArgs e)
        {
            FrmShop frmShop = new(this);
            frmShop.Show();
            Hide();
        }

        private void FrmTitle_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
