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
        public FrmTitle titleScreen { get; set; }
        private Panel[] bulbs;

        public FrmShop(FrmTitle cameFrom)
        {
            InitializeComponent();
            this.titleScreen = cameFrom;

            // store all of the light bulbs in an array so you can
            // itterate over them later. They're listed in counterclockwise
            // order because the lihgt will be moving in clockwise order
            // and it will cause problems in the script if those are
            // the same direction
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
        }

        private void btnBackToStart_Click(object sender, EventArgs e)
        {
            titleScreen.Show();
            Hide();
        }

        private void FrmShop_Load(object sender, EventArgs e)
        {
            lblCoinCount.Text = $"Coins:{Game.Coins}";
        }

        // this fires every time bulbTimer fires
        private void changeLights(object sender, EventArgs e)
        {
            // loop through the light bulbs
            for (int i = 0; i < bulbs.Length; i++)
            {
                // if the lightbulb before it is on
                if ((i == bulbs.Length - 1 && bulbs[0].BackgroundImage == Resources.lightBulbOn) || (i != bulbs.Length - 1 && bulbs[i + 1].BackgroundImage == Resources.lightBulbOn))
                {
                    // check if the lightbulb is off
                    if (bulbs[i].BackgroundImage == Resources.lightBulbOff)
                    {
                        // turn on the bulb
                        bulbs[i].BackgroundImage = Resources.lightBulbOn;
                    }
                    // if the one before it is off
                }
                else
                {
                    // check if the lightbulb is on
                    if (bulbs[i].BackgroundImage == Resources.lightBulbOn)
                    {
                        // turn off the bulb
                        bulbs[i].BackgroundImage = Resources.lightBulbOff;
                    }
                }
            }
        }
    }
}
