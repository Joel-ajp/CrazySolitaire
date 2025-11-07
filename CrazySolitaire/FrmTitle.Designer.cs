namespace CrazySolitaire {
    partial class FrmTitle {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTitle));
            btnStartGame = new Button();
            TitleBox = new PictureBox();
            cboDifficulty = new ComboBox();
            button1 = new Button();
            DifficultyLbl = new Label();
            TextPic = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)TitleBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TextPic).BeginInit();
            SuspendLayout();
            // 
            // btnStartGame
            // 
            btnStartGame.AutoSize = true;
            btnStartGame.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnStartGame.Location = new Point(681, 154);
            btnStartGame.Name = "btnStartGame";
            btnStartGame.Size = new Size(197, 50);
            btnStartGame.TabIndex = 0;
            btnStartGame.Text = "Start Game";
            btnStartGame.UseVisualStyleBackColor = true;
            btnStartGame.Click += btnStartGame_Click;
            // 
            // TitleBox
            // 
            TitleBox.BackgroundImageLayout = ImageLayout.Stretch;
            TitleBox.Image = (Image)resources.GetObject("TitleBox.Image");
            TitleBox.Location = new Point(140, 35);
            TitleBox.Name = "TitleBox";
            TitleBox.Size = new Size(374, 285);
            TitleBox.TabIndex = 1;
            TitleBox.TabStop = false;
            // 
            // cboDifficulty
            // 
            cboDifficulty.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDifficulty.FormattingEnabled = true;
            cboDifficulty.Items.AddRange(new object[] { "Easy", "Normal", "Hard", "Unlimited" });
            cboDifficulty.Location = new Point(681, 356);
            cboDifficulty.Margin = new Padding(3, 2, 3, 2);
            cboDifficulty.Name = "cboDifficulty";
            cboDifficulty.Size = new Size(197, 23);
            cboDifficulty.TabIndex = 2;
            cboDifficulty.SelectedIndexChanged += cboDifficulty_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.AutoSize = true;
            button1.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(681, 237);
            button1.Name = "button1";
            button1.Size = new Size(197, 50);
            button1.TabIndex = 4;
            button1.Text = "Shop";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnOpenShop_Click;
            // 
            // DifficultyLbl
            // 
            DifficultyLbl.AutoSize = true;
            DifficultyLbl.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DifficultyLbl.ForeColor = Color.White;
            DifficultyLbl.Location = new Point(697, 312);
            DifficultyLbl.Name = "DifficultyLbl";
            DifficultyLbl.Size = new Size(168, 25);
            DifficultyLbl.TabIndex = 5;
            DifficultyLbl.Text = "Choose Difficulty:";
            // 
            // TextPic
            // 
            TextPic.Image = (Image)resources.GetObject("TextPic.Image");
            TextPic.Location = new Point(56, 312);
            TextPic.Name = "TextPic";
            TextPic.Size = new Size(548, 130);
            TextPic.SizeMode = PictureBoxSizeMode.StretchImage;
            TextPic.TabIndex = 6;
            TextPic.TabStop = false;
            // 
            // FrmTitle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(976, 510);
            Controls.Add(TextPic);
            Controls.Add(DifficultyLbl);
            Controls.Add(btnStartGame);
            Controls.Add(button1);
            Controls.Add(cboDifficulty);
            Controls.Add(TitleBox);
            Name = "FrmTitle";
            StartPosition = FormStartPosition.CenterScreen;
            FormClosing += FrmTitle_FormClosing;
            Load += FrmTitle_Load;
            ((System.ComponentModel.ISupportInitialize)TitleBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)TextPic).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStartGame;
        private PictureBox TitleBox;
        private ComboBox cboDifficulty;
        private Button button1;
        private Label DifficultyLbl;
        private PictureBox TextPic;
    }
}
