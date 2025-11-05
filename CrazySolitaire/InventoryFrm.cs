using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrazySolitaire
{
    public partial class InventoryFrm : Form
    {
        public InventoryFrm()
        {
            System.Diagnostics.Trace.WriteLine($"Proof that game instance is still open: coins {Game.Coins} score: {Game.Score} cards in 1st tableau {Game.TableauStacks[0].CountCards}");
            InitializeComponent();
        }

        private void InventoryFrm_Load(object sender, EventArgs e)
        {
            //setting up desc panel sizing stuff
            InvItemDesc.MaximumSize = new Size(panel1.Width - 20, 0);
            InvItemDesc.AutoSize = true;
        }
        private void InvBackButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //i know its called InvItemSlot1 but its the hover for all item slots
        private void InvItemSlot1_MouseHover(object sender, EventArgs e)
        {
            //retrieve item being hovered over
            Control hoveredSlot = sender as Control;

            //yellow outline on hover
            ControlPaint.DrawBorder(hoveredSlot.CreateGraphics(), hoveredSlot.ClientRectangle, Color.Yellow, ButtonBorderStyle.Solid);

            //show desc
            InvDescHeader.Text = "Item 1"; //placeholder, will eventually pull from item data
            InvItemDesc.Text = "Item that does stuff."; //placeholder, same here

            InvItemDesc.Visible = true;
            InvDescHeader.Visible = true;

        }

        private void InvItemSlot_MouseLeave(object sender, EventArgs e)
        {
            //hide everything again, hide border
            InvItemDesc.Visible = false;
            InvDescHeader.Visible = false;
            this.Refresh();
        }
    }
}
