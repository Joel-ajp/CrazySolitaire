using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrazySolitaire.Properties;

namespace CrazySolitaire.Code
{
    public partial class FrmShop : Form
    {
        // the screen to go back to when you hit the back button
        public FrmTitle titleScreen { get; set; }
        // all of the light bulbs around the sides
        private Panel[] bulbs;
        // a parallel array to bulbs to keep track of if the 
        // light is on or off
        private bool[] bulbIsOn;

        // Track whether we're navigating back (so we don't exit the app)
        private bool _navigatingBack = false;

        // items available in the shop
        public enum ShopItems
        {
            TalonShuffleCard,
            RevealCard,
            DoubleCoinsCard
        }

        // the costs of the shop items
        public Dictionary<ShopItems, int> costs = new();

        public FrmShop(FrmTitle cameFrom)
        {
            InitializeComponent();
            this.titleScreen = cameFrom;

            // Ensure closing the shop via X exits the app (closes hidden title)
            this.FormClosing += FrmShop_FormClosing;

            costs.Add(ShopItems.TalonShuffleCard, 25);
            costs.Add(ShopItems.RevealCard, 15);
            costs.Add(ShopItems.DoubleCoinsCard, 50);

            // set the visible cost labels for each purchase box
            lblReverseCost.Text = costs[ShopItems.TalonShuffleCard].ToString();
            RevealCost.Text = costs[ShopItems.RevealCard].ToString();
            label9.Text = costs[ShopItems.DoubleCoinsCard].ToString();

            // store all of the light bulbs in an array so you can
            // itterate over them later. They're listed in counterclockwise
            // order because the lihgt will be moving in clockwise order
            // and it will cause problems in the script if those are
            // the same direction
            #region this.bulbs = [...]
            this.bulbs = [
                bulb2a,
                bulb3a,
                bulb4a,
                bulb5a,
                bulb6a,
                bulb7a,
                bulb8a,
                bulb9a,
                bulb10a,
                bulb11a,
                bulb12a,
                bulb13a,
                bulb14a,
                bulb15a,
                bulb16a,
                bulb16b,
                bulb16c,
                bulb16d,
                bulb16e,
                bulb16f,
                bulb16g,
                bulb16h,
                bulb16i,
                bulb16j,
                bulb16k,
                bulb16l,
                bulb16m,
                bulb16n,
                bulb16o,
                bulb16p,
                bulb16q,
                bulb16r,
                bulb16s,
                bulb16t,
                bulb16u,
                bulb15u,
                bulb14u,
                bulb13u,
                bulb12u,
                bulb11u,
                bulb10u,
                bulb9u,
                bulb8u,
                bulb7u,
                bulb6u,
                bulb5u,
                bulb4u,
                bulb3u,
                bulb2u,
                bulb1u,
                bulb1t,
                bulb1s,
                bulb1r,
                bulb1q,
                bulb1p,
                bulb1o,
                bulb1n,
                bulb1m,
                bulb1l,
                bulb1k,
                bulb1j,
                bulb1i,
                bulb1h,
                bulb1g,
                bulb1f,
                bulb1e,
                bulb1d,
                bulb1c,
                bulb1b,
                bulb1a];
            #endregion
            #region this.bulbIsOn = [...]
            this.bulbIsOn = [
                true,
                true,
                true,
                true,
                false,
                false,
                false,
                false,
                false,
                false,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                false,
                false,
                false,
                false,
                false,
                true,
                true,
                true,
                true,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                true,
                true,
                true];
            #endregion
        }

        private void btnBackToStart_Click(object sender, EventArgs e)
        {
            titleScreen.Show();
            // Dispose this shop instead of hiding it so it doesn't linger
            _navigatingBack = true;
            Close();
        }

        private void FrmShop_Load(object sender, EventArgs e)
        {
            lblCoinCount.Text = $"Coins:{Game.Coins}";
            lblReverseCount.Text = $"{Game.UnoReverses}";

            // wire up reveal and double counts
            lblRevealCount.Text = $"{Game.RevealUses}";
            lblDoubleCount.Text = Game.DoubleCoinsActive ? "1" : "0";

            Random rand = new();
            var colors = Enum.GetValues(typeof(UnoColor));
            var randomColor = (UnoColor)colors.GetValue(rand.Next(4));
            PnlReverseCard.BackgroundImage = Resources.ResourceManager.GetObject($"uno_reverse_{randomColor.ToString().ToLower()}") as Bitmap;

            randomColor = (UnoColor)colors.GetValue(rand.Next(4));
            pnlReverseCountIcon.BackgroundImage = Resources.ResourceManager.GetObject($"uno_reverse_{randomColor.ToString().ToLower()}") as Bitmap;
        }

        // this fires every time bulbTimer fires
        private void changeLights(object sender, EventArgs e)
        {
            // loop through the light bulbs
            for (int i = 0; i < bulbs.Length; i++)
            {
                // if the lightbulb before it is on
                if ((i == bulbs.Length - 1 && bulbIsOn[0]) || (i != bulbs.Length - 1 && bulbIsOn[i + 1]))
                {
                    // check if the lightbulb is off
                    if (!bulbIsOn[i])
                    {
                        // turn on the bulb
                        bulbs[i].BackgroundImage = Resources.lightBulbOn;
                        bulbIsOn[i] = true;
                    }
                    // if the one before it is off
                }
                else
                {
                    // check if the lightbulb is on
                    if (bulbIsOn[i])
                    {
                        // turn off the bulb
                        bulbs[i].BackgroundImage = Resources.lightBulbOff;
                        bulbIsOn[i] = false;
                    }
                }
            }
        }

        private void ReversePurchase_Click(object sender, EventArgs e)
        {
            bool bought = PurchaseItem(ShopItems.TalonShuffleCard, PnlReversePurchaseBx, lblReversNoMoney);
            // give them the card if it went through
            if (bought)
            {
                Game.UnoReverses = Game.UnoReverses + 1;
                lblReverseCount.Text = $"{Game.UnoReverses}";
            }
        }

        private void RevealPurchase_Click(object sender, EventArgs e)
        {
            bool bought = PurchaseItem(ShopItems.RevealCard, panel1, RevealWarning);
            if (bought)
            {
                // grant a reveal use (game code can consume this)
                Game.RevealUses = Game.RevealUses + 1;
                // update UI count
                lblRevealCount.Text = $"{Game.RevealUses}";
                // briefly flash the header to indicate success
                RevealHeader.Text = "Reveal Purchased";
                var reset = Task.Run(async delegate
                {
                    await Task.Delay(600);
                    RevealHeader.Text = "Reveal Card";
                });
                reset.Wait();
                reset.Dispose();
            }
        }

        private void DoublePurchase_Click(object sender, EventArgs e)
        {
            bool bought = PurchaseItem(ShopItems.DoubleCoinsCard, panel3, label6);
            if (bought)
            {
                // enable double coins for the remainder of the session
                Game.DoubleCoinsActive = true;
                // reflect active state in header and disable further purchases
                label7.Text = "Double Coins (ACTIVE)";
                panel3.Enabled = false;
                // update UI count so player can see it's active
                lblDoubleCount.Text = Game.DoubleCoinsActive ? "1" : "0";
            }
        }

        private bool PurchaseItem(ShopItems item, Panel clickBox, Label noMoney)
        {
            // if the player has enough coins for the item
            if (Game.Coins >= costs[item])
            {
                // make the box flash gold
                clickBox.BackColor = Color.Gold;
                var resetColor = Task.Run(async delegate
                {
                    await Task.Delay(150);
                    clickBox.BackColor = Color.FromArgb(64, 0, 0);
                });
                resetColor.Wait();
                resetColor.Dispose();

                // subtract coins
                Game.Coins = Game.Coins - costs[item];

                // update shop screen coin tally
                lblCoinCount.Text = $"Coins:{Game.Coins}";

                // return that they got the thing
                return true;

                // if they don't have enough coins for the item
            }
            else
            {
                // flash the box flash red and make the not enough
                // coins message appear
                clickBox.BackColor = Color.Red;
                noMoney.Show();
                var resetColor = Task.Run(async delegate
                {
                    await Task.Delay(150);
                    clickBox.BackColor = Color.FromArgb(64, 0, 0);
                });
                resetColor.Wait();
                resetColor.Dispose();
                var hideNoMoneyDelay = Task.Run(async delegate
                {
                    await Task.Delay(1000);
                });
                hideNoMoneyDelay.Wait();
                noMoney.Hide();
                hideNoMoneyDelay.Dispose();

                // return that they didn't get the thing
                return false;
            }
        }

        private void FrmShop_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If user closes the shop via X/Alt+F4, close the hidden title to exit the app
            if (!_navigatingBack && titleScreen != null && !titleScreen.IsDisposed)
            {
                titleScreen.Close();
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void PnlReversePurchaseBx_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
