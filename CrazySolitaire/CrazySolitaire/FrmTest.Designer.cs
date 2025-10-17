namespace CrazySolitaire {
    partial class FrmTest {
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
        private void InitializeComponent() {
            flpTest = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // flpTest
            // 
            flpTest.BackColor = Color.Black;
            flpTest.Dock = DockStyle.Fill;
            flpTest.Location = new Point(0, 0);
            flpTest.Name = "flpTest";
            flpTest.Size = new Size(1106, 663);
            flpTest.TabIndex = 0;
            // 
            // FrmTest
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1106, 663);
            Controls.Add(flpTest);
            Name = "FrmTest";
            Text = "FrmTest";
            Load += FrmTest_Load;
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flpTest;
    }
}