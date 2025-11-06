using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrazySolitaire
{
    public partial class InventoryFrm : Form
    {
        public bool InvEmpty = (Game.TalonShuffles <= 0 && Game.RevealUses <= 0 && !Game.DoubleCoinsActive);
        
        //this is a very stupid design choice but idgaf
        public Dictionary<string, string> ItemDescriptions = new Dictionary<string, string>()
        {
            { "Talon Shuffle", "Shuffles the Talon Stack" },
            {"Reveal Card", "Reveals facedown cards for one turn" },
            {"Coin Multiplier", "Doubles Coinds earned for the remainder of the game" }
         };
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

            //of there are no items , show no items message
            if (InvEmpty)
            {
                InvDescHeader.Visible = true;
                InvItemDesc.Visible = true;
                InvDescHeader.Text = "No Items in Inventory";
                InvItemDesc.Text = "Visit the shop to fix that!";
            }
            else
            {
                InvItemSlot1.Visible = (Game.TalonShuffles > 0);
                InvItemSlot2.Visible = (Game.RevealUses > 0);
                InvItemSlot3.Visible = (Game.DoubleCoinsActive);
            }
            






        }
        private void InvBackButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //helper function to retrieve item desc based on hovered slot
        private void InvItemRetrieveDesc(Control hoveredSlot)
        {
            switch (hoveredSlot.Name)
            {
                case "InvItemSlot1":
                    InvDescHeader.Text = "Talon Shuffle";
                    InvItemDesc.Text = ItemDescriptions["Talon Shuffle"];
                    break;
                case "InvItemSlot2":
                    InvDescHeader.Text = "Reveal Card";
                    InvItemDesc.Text = ItemDescriptions["Reveal Card"];
                    break;
                case "InvItemSlot3":   
                    InvDescHeader.Text = "Coin Multiplier";
                    InvItemDesc.Text = ItemDescriptions["Coin Multiplier"];
                    break;
            }
        }

        private void InvItemSlot_Click(object sender, EventArgs e)
        {
            if (InvEmpty) { return; } //nothing happens if inventory is empty
            Control clickedSlot = sender as Control;
            switch (clickedSlot.Name)
            {
                case "InvItemSlot1":    // Talon Shuffle
                    Game.TalonShuffles--;
                    Game.Deck.Shuffle();
                    break;
                case "InvItemSlot2":    // Reveal Card
                    Game.RevealUses--;
                    Game.RevealAllTableauFaceDown();
                    break;
            }
            //refresh inventory
            InvEmpty = (Game.TalonShuffles <= 0 && Game.RevealUses <= 0 && !Game.DoubleCoinsActive);
            InventoryFrm_Load(sender, e);
        }

        //i know its called InvItemSlot1 but its the hover for all item slots
        private void InvItemSlot1_MouseHover(object sender, EventArgs e)
        {
            if (InvEmpty) {
                return; //nothing happens if inventory is empty
            }
            //retrieve item being hovered over
            Control hoveredSlot = sender as Control;

            //yellow outline on hover
            ControlPaint.DrawBorder(hoveredSlot.CreateGraphics(), hoveredSlot.ClientRectangle, Color.Yellow, ButtonBorderStyle.Solid);

            //show desc
            InvItemRetrieveDesc(hoveredSlot);
            InvItemDesc.Visible = true;
            InvDescHeader.Visible = true;

        }

        private void InvItemSlot_MouseLeave(object sender, EventArgs e)
        {
            if (InvEmpty) { return; }//nothing happens if inventory is empty
            
            //hide everything again, hide border
            InvItemDesc.Visible = false;
            InvDescHeader.Visible = false;
            this.Refresh();
        }
    }
}
