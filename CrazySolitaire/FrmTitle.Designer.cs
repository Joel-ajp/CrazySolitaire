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
            btnStartGame = new Button();
            pictureBox1 = new PictureBox();
            cboDifficulty = new ComboBox();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnStartGame
            // 
            btnStartGame.AutoSize = true;
            btnStartGame.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnStartGame.Location = new Point(250, 533);
            btnStartGame.Name = "btnStartGame";
            btnStartGame.Size = new Size(198, 50);
            btnStartGame.TabIndex = 0;
            btnStartGame.Text = "Start Game";
            btnStartGame.UseVisualStyleBackColor = true;
            btnStartGame.Click += btnStartGame_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.title;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(976, 674);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // cboDifficulty
            // 
            cboDifficulty.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDifficulty.FormattingEnabled = true;
            cboDifficulty.Items.AddRange(new object[] { "Easy", "Normal", "Hard", "Unlimited" });
            cboDifficulty.Location = new Point(270, 588);
            cboDifficulty.Margin = new Padding(3, 2, 3, 2);
            cboDifficulty.Name = "cboDifficulty";
            cboDifficulty.Size = new Size(159, 23);
            cboDifficulty.TabIndex = 2;
            cboDifficulty.SelectedIndexChanged += cboDifficulty_SelectedIndexChanged;
            cboDifficulty.SelectedIndex = 0;
            // 
            // button1
            // 
            button1.AutoSize = true;
            button1.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(465, 533);
            button1.Name = "button1";
            button1.Size = new Size(198, 50);
            button1.TabIndex = 4;
            button1.Text = "Shop";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnOpenShop_Click;
            // 
            // FrmTitle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(976, 674);
            Controls.Add(btnStartGame);
            Controls.Add(button1);
            Controls.Add(cboDifficulty);
            Controls.Add(pictureBox1);
            Name = "FrmTitle";
            StartPosition = FormStartPosition.CenterScreen;
            Load += FrmTitle_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStartGame;
        private PictureBox pictureBox1;
        private ComboBox cboDifficulty;
        private Button button1;
    }
}
