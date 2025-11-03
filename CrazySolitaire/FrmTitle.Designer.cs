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
        /// Required method for Designer support - do not modify the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            btnStartGame = new Button();
            pictureBox1 = new PictureBox();
            cboDifficulty = new ComboBox();
            lblDifficulty = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnStartGame
            // 
            btnStartGame.AutoSize = true;
            btnStartGame.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnStartGame.Location = new Point(370, 584);
            btnStartGame.Name = "btnStartGame";
            btnStartGame.Size = new Size(181, 50);
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
            cboDifficulty.Location = new Point(370, 545);
            cboDifficulty.Name = "cboDifficulty";
            cboDifficulty.Size = new Size(181, 23);
            cboDifficulty.TabIndex = 2;
            cboDifficulty.SelectedIndexChanged += cboDifficulty_SelectedIndexChanged;
            // default to Normal
            cboDifficulty.SelectedIndex = 1;
            // 
            // lblDifficulty
            // 
            lblDifficulty.AutoSize = true;
            lblDifficulty.BackColor = Color.Black;
            lblDifficulty.ForeColor = Color.White;
            lblDifficulty.Location = new Point(290, 548);
            lblDifficulty.Name = "lblDifficulty";
            lblDifficulty.Size = new Size(62, 15);
            lblDifficulty.TabIndex = 3;
            lblDifficulty.Text = "Difficulty:";
            // 
            // FrmTitle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(976, 674);
            Controls.Add(lblDifficulty);
            Controls.Add(cboDifficulty);
            Controls.Add(btnStartGame);
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
        private Label lblDifficulty;
    }
}