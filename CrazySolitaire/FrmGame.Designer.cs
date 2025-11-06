namespace CrazySolitaire
{
    partial class FrmGame
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pbStock = new PictureBox();
            panTalon = new Panel();
            panFoundationStack_Clubs = new Panel();
            panFoundationStack_Hearts = new Panel();
            panFoundationStack_Spades = new Panel();
            panFoundationStack_Diamonds = new Panel();
            panTableauStack_0 = new Panel();
            panTableauStack_1 = new Panel();
            panTableauStack_2 = new Panel();
            panTableauStack_3 = new Panel();
            panTableauStack_4 = new Panel();
            panTableauStack_5 = new Panel();
            panTableauStack_6 = new Panel();
            picBox_coin = new PictureBox();
            lblCoinCount = new Label();
            InvButton = new Button();
            ((System.ComponentModel.ISupportInitialize)pbStock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBox_coin).BeginInit();
            SuspendLayout();
            // 
            // pbStock
            // 
            pbStock.BackgroundImage = Properties.Resources.back_green;
            pbStock.BackgroundImageLayout = ImageLayout.Stretch;
            pbStock.BorderStyle = BorderStyle.Fixed3D;
            pbStock.Location = new Point(40, 27);
            pbStock.Name = "pbStock";
            pbStock.Size = new Size(75, 109);
            pbStock.TabIndex = 0;
            pbStock.TabStop = false;
            pbStock.Click += pbStock_Click;
            // 
            // panTalon
            // 
            panTalon.Location = new Point(160, 27);
            panTalon.Name = "panTalon";
            panTalon.Size = new Size(151, 109);
            panTalon.TabIndex = 1;
            // 
            // panFoundationStack_Clubs
            // 
            panFoundationStack_Clubs.AllowDrop = true;
            panFoundationStack_Clubs.BackgroundImage = Properties.Resources.FoundationStackBg_Clubs;
            panFoundationStack_Clubs.BackgroundImageLayout = ImageLayout.Stretch;
            panFoundationStack_Clubs.BorderStyle = BorderStyle.FixedSingle;
            panFoundationStack_Clubs.Location = new Point(427, 26);
            panFoundationStack_Clubs.Name = "panFoundationStack_Clubs";
            panFoundationStack_Clubs.Size = new Size(85, 109);
            panFoundationStack_Clubs.TabIndex = 2;
            // 
            // panFoundationStack_Hearts
            // 
            panFoundationStack_Hearts.BackgroundImage = Properties.Resources.FoundationStackBg_Hearts;
            panFoundationStack_Hearts.BackgroundImageLayout = ImageLayout.Stretch;
            panFoundationStack_Hearts.BorderStyle = BorderStyle.FixedSingle;
            panFoundationStack_Hearts.Location = new Point(556, 27);
            panFoundationStack_Hearts.Name = "panFoundationStack_Hearts";
            panFoundationStack_Hearts.Size = new Size(85, 109);
            panFoundationStack_Hearts.TabIndex = 3;
            // 
            // panFoundationStack_Spades
            // 
            panFoundationStack_Spades.BackgroundImage = Properties.Resources.FoundationStackBg_Spades;
            panFoundationStack_Spades.BackgroundImageLayout = ImageLayout.Stretch;
            panFoundationStack_Spades.BorderStyle = BorderStyle.FixedSingle;
            panFoundationStack_Spades.Location = new Point(685, 27);
            panFoundationStack_Spades.Name = "panFoundationStack_Spades";
            panFoundationStack_Spades.Size = new Size(85, 109);
            panFoundationStack_Spades.TabIndex = 3;
            // 
            // panFoundationStack_Diamonds
            // 
            panFoundationStack_Diamonds.BackgroundImage = Properties.Resources.FoundationStackBg_Diamonds;
            panFoundationStack_Diamonds.BackgroundImageLayout = ImageLayout.Stretch;
            panFoundationStack_Diamonds.BorderStyle = BorderStyle.FixedSingle;
            panFoundationStack_Diamonds.Location = new Point(814, 27);
            panFoundationStack_Diamonds.Name = "panFoundationStack_Diamonds";
            panFoundationStack_Diamonds.Size = new Size(85, 109);
            panFoundationStack_Diamonds.TabIndex = 3;
            // 
            // panTableauStack_0
            // 
            panTableauStack_0.AllowDrop = true;
            panTableauStack_0.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panTableauStack_0.AutoSize = true;
            panTableauStack_0.Location = new Point(39, 172);
            panTableauStack_0.Name = "panTableauStack_0";
            panTableauStack_0.Size = new Size(86, 286);
            panTableauStack_0.TabIndex = 4;
            // 
            // panTableauStack_1
            // 
            panTableauStack_1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panTableauStack_1.AutoSize = true;
            panTableauStack_1.Location = new Point(168, 172);
            panTableauStack_1.Name = "panTableauStack_1";
            panTableauStack_1.Size = new Size(86, 286);
            panTableauStack_1.TabIndex = 5;
            // 
            // panTableauStack_2
            // 
            panTableauStack_2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panTableauStack_2.AutoSize = true;
            panTableauStack_2.Location = new Point(297, 172);
            panTableauStack_2.Name = "panTableauStack_2";
            panTableauStack_2.Size = new Size(86, 286);
            panTableauStack_2.TabIndex = 5;
            // 
            // panTableauStack_3
            // 
            panTableauStack_3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panTableauStack_3.AutoSize = true;
            panTableauStack_3.Location = new Point(426, 172);
            panTableauStack_3.Name = "panTableauStack_3";
            panTableauStack_3.Size = new Size(86, 286);
            panTableauStack_3.TabIndex = 5;
            // 
            // panTableauStack_4
            // 
            panTableauStack_4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panTableauStack_4.AutoSize = true;
            panTableauStack_4.Location = new Point(555, 172);
            panTableauStack_4.Name = "panTableauStack_4";
            panTableauStack_4.Size = new Size(86, 286);
            panTableauStack_4.TabIndex = 5;
            // 
            // panTableauStack_5
            // 
            panTableauStack_5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panTableauStack_5.AutoSize = true;
            panTableauStack_5.Location = new Point(684, 172);
            panTableauStack_5.Name = "panTableauStack_5";
            panTableauStack_5.Size = new Size(86, 286);
            panTableauStack_5.TabIndex = 5;
            // 
            // panTableauStack_6
            // 
            panTableauStack_6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panTableauStack_6.AutoSize = true;
            panTableauStack_6.Location = new Point(813, 172);
            panTableauStack_6.Name = "panTableauStack_6";
            panTableauStack_6.Size = new Size(86, 286);
            panTableauStack_6.TabIndex = 5;
            // 
            // picBox_coin
            // 
            picBox_coin.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            picBox_coin.BackgroundImage = Properties.Resources.coin;
            picBox_coin.BackgroundImageLayout = ImageLayout.Stretch;
            picBox_coin.Location = new Point(768, 588);
            picBox_coin.Name = "picBox_coin";
            picBox_coin.Size = new Size(40, 40);
            picBox_coin.TabIndex = 6;
            picBox_coin.TabStop = false;
            // 
            // lblCoinCount
            // 
            lblCoinCount.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblCoinCount.AutoSize = true;
            lblCoinCount.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold);
            lblCoinCount.Location = new Point(814, 588);
            lblCoinCount.Name = "lblCoinCount";
            lblCoinCount.Size = new Size(107, 40);
            lblCoinCount.TabIndex = 7;
            lblCoinCount.Text = "Label1";
            // 
            // InvButton
            // 
            InvButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            InvButton.BackColor = Color.DeepPink;
            InvButton.Font = new Font("Script MT Bold", 18F);
            InvButton.ForeColor = Color.Black;
            InvButton.Location = new Point(30, 587);
            InvButton.Margin = new Padding(2);
            InvButton.Name = "InvButton";
            InvButton.Size = new Size(189, 39);
            InvButton.TabIndex = 8;
            InvButton.Text = "Inventory";
            InvButton.UseVisualStyleBackColor = false;
            InvButton.Click += InvButton_Click;
            InvButton.MouseLeave += InvButton_MouseLeave;
            InvButton.MouseHover += InvButton_MouseHover;
            // 
            // FrmGame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 64, 0);
            ClientSize = new Size(976, 637);
            Controls.Add(InvButton);
            Controls.Add(lblCoinCount);
            Controls.Add(picBox_coin);
            Controls.Add(panTableauStack_6);
            Controls.Add(panTableauStack_5);
            Controls.Add(panTableauStack_4);
            Controls.Add(panTableauStack_3);
            Controls.Add(panTableauStack_2);
            Controls.Add(panTableauStack_1);
            Controls.Add(panTableauStack_0);
            Controls.Add(panFoundationStack_Diamonds);
            Controls.Add(panFoundationStack_Spades);
            Controls.Add(panFoundationStack_Hearts);
            Controls.Add(panFoundationStack_Clubs);
            Controls.Add(panTalon);
            Controls.Add(pbStock);
            ForeColor = SystemColors.ButtonHighlight;
            Name = "FrmGame";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Crazy Solitaire";
            FormClosing += FrmGame_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pbStock).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBox_coin).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbStock;
        private Panel panTalon;
        private Panel panFoundationStack_Clubs;
        private Panel panFoundationStack_Hearts;
        private Panel panFoundationStack_Spades;
        private Panel panFoundationStack_Diamonds;
        private Panel panTableauStack_0;
        private Panel panTableauStack_1;
        private Panel panTableauStack_2;
        private Panel panTableauStack_3;
        private Panel panTableauStack_4;
        private Panel panTableauStack_5;
        private Panel panTableauStack_6;
        private PictureBox picBox_coin;
        private Label lblCoinCount;
        private Button InvButton;
    }
}
