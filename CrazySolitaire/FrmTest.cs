namespace CrazySolitaire;

public partial class FrmTest : Form {
    public FrmTest() {
        InitializeComponent();
    }

    private void FrmTest_Load(object sender, EventArgs e) {
        Card c;
        while ((c = Game.Deck.Acquire()) != null) {
            flpTest.AddCard(c);
        }
    }
}
