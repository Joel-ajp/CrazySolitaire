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
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            FrmGame frmGame = new();
            frmGame.Show();
            Hide();
        }

        private void btnOpenShop_Click(object sender, EventArgs e)
        {
            FrmShop frmShop = new(this);
            frmShop.Show();
            Hide();
        }
    }
}
