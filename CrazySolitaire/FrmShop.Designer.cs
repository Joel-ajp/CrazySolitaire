namespace CrazySolitaire.Code
{
    partial class FrmShop
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnStartGame = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnStartGame
            // 
            btnStartGame.AutoSize = true;
            btnStartGame.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnStartGame.Location = new Point(46, 40);
            btnStartGame.Margin = new Padding(3, 4, 3, 4);
            btnStartGame.Name = "btnStartGame";
            btnStartGame.Size = new Size(84, 67);
            btnStartGame.TabIndex = 1;
            btnStartGame.Text = "🔙";
            btnStartGame.UseVisualStyleBackColor = true;
            btnStartGame.Click += btnBackToStart_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Broadway", 36F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(333, 39);
            label1.Name = "label1";
            label1.Size = new Size(721, 68);
            label1.TabIndex = 2;
            label1.Text = "Welcome to the Shop!";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FrmShop
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1115, 899);
            Controls.Add(label1);
            Controls.Add(btnStartGame);
            Name = "FrmShop";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmShop";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStartGame;
        private Label label1;
    }
}