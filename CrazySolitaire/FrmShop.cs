using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrazySolitaire.Code
{
    public partial class FrmShop : Form
    {
        public FrmTitle titleScreen { get; set; }
        public FrmShop(FrmTitle cameFrom)
        {
            InitializeComponent();
            this.titleScreen = cameFrom;
        }

        private void btnBackToStart_Click(object sender, EventArgs e)
        {
            titleScreen.Show();
            Hide();
        }
    }
}
