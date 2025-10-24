namespace CrazySolitaire
{
    partial class FrmHighScore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHighScore));
            label1 = new Label();
            pictureBox1 = new PictureBox();
            button1 = new Button();
            ScoreTextBox = new TextBox();
            label2 = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Comic Sans MS", 72F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(169, 283);
            label1.Name = "label1";
            label1.Size = new Size(568, 201);
            label1.TabIndex = 0;
            label1.Text = "you win";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(734, 159);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(552, 500);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom;
            button1.Font = new Font("Comic Sans MS", 20F);
            button1.Location = new Point(572, 756);
            button1.Name = "button1";
            button1.Size = new Size(356, 98);
            button1.TabIndex = 2;
            button1.Text = "main menu";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // ScoreTextBox
            // 
            ScoreTextBox.BackColor = Color.Indigo;
            ScoreTextBox.BorderStyle = BorderStyle.None;
            ScoreTextBox.Enabled = false;
            ScoreTextBox.Font = new Font("Segoe UI", 20F);
            ScoreTextBox.ForeColor = Color.GreenYellow;
            ScoreTextBox.Location = new Point(3, 3);
            ScoreTextBox.Name = "ScoreTextBox";
            ScoreTextBox.Size = new Size(207, 54);
            ScoreTextBox.TabIndex = 3;
            ScoreTextBox.Text = "000";
            ScoreTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 24F);
            label2.ForeColor = Color.Cyan;
            label2.Location = new Point(304, 497);
            label2.Name = "label2";
            label2.Size = new Size(318, 65);
            label2.TabIndex = 4;
            label2.Text = "Personal Best:";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.None;
            flowLayoutPanel1.Controls.Add(ScoreTextBox);
            flowLayoutPanel1.ForeColor = Color.PaleGreen;
            flowLayoutPanel1.Location = new Point(348, 565);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(210, 72);
            flowLayoutPanel1.TabIndex = 5;
            // 
            // FrmHighScore
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Indigo;
            ClientSize = new Size(1394, 1060);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(label2);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Name = "FrmHighScore";
            Text = "HighScore";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private PictureBox pictureBox1;
        private Button button1;
        private TextBox ScoreTextBox;
        private Label label2;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}