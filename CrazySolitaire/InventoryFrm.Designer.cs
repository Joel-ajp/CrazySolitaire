
namespace CrazySolitaire
{
    partial class InventoryFrm
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
            InvCardSlots = new TableLayoutPanel();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            InvItemSlot1 = new PictureBox();
            invPanel = new Panel();
            panel1 = new Panel();
            InvItemDesc = new Label();
            InvDescHeader = new Label();
            InvBackButton = new Button();
            InvTitle = new Label();
            InvCardSlots.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)InvItemSlot1).BeginInit();
            invPanel.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // InvCardSlots
            // 
            InvCardSlots.BackColor = Color.IndianRed;
            InvCardSlots.ColumnCount = 3;
            InvCardSlots.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            InvCardSlots.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            InvCardSlots.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            InvCardSlots.Controls.Add(pictureBox3, 2, 0);
            InvCardSlots.Controls.Add(pictureBox2, 1, 0);
            InvCardSlots.Controls.Add(InvItemSlot1, 0, 0);
            InvCardSlots.Location = new Point(53, 53);
            InvCardSlots.Name = "InvCardSlots";
            InvCardSlots.RowCount = 1;
            InvCardSlots.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            InvCardSlots.Size = new Size(639, 159);
            InvCardSlots.TabIndex = 0;
            // 
            // pictureBox3
            // 
            pictureBox3.Dock = DockStyle.Fill;
            pictureBox3.Image = Properties.Resources._10_of_hearts;
            pictureBox3.Location = new Point(436, 10);
            pictureBox3.Margin = new Padding(10);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(193, 139);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 2;
            pictureBox3.TabStop = false;
            pictureBox3.MouseLeave += InvItemSlot_MouseLeave;
            pictureBox3.MouseHover += InvItemSlot1_MouseHover;
            // 
            // pictureBox2
            // 
            pictureBox2.Dock = DockStyle.Fill;
            pictureBox2.Image = Properties.Resources._10_of_hearts;
            pictureBox2.Location = new Point(223, 10);
            pictureBox2.Margin = new Padding(10);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(193, 139);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            pictureBox2.MouseLeave += InvItemSlot_MouseLeave;
            pictureBox2.MouseHover += InvItemSlot1_MouseHover;
            // 
            // InvItemSlot1
            // 
            InvItemSlot1.Dock = DockStyle.Fill;
            InvItemSlot1.Image = Properties.Resources._10_of_hearts;
            InvItemSlot1.Location = new Point(10, 10);
            InvItemSlot1.Margin = new Padding(10);
            InvItemSlot1.Name = "InvItemSlot1";
            InvItemSlot1.Size = new Size(193, 139);
            InvItemSlot1.SizeMode = PictureBoxSizeMode.Zoom;
            InvItemSlot1.TabIndex = 0;
            InvItemSlot1.TabStop = false;
            InvItemSlot1.MouseLeave += InvItemSlot_MouseLeave;
            InvItemSlot1.MouseHover += InvItemSlot1_MouseHover;
            // 
            // invPanel
            // 
            invPanel.BackColor = Color.Maroon;
            invPanel.Controls.Add(panel1);
            invPanel.Controls.Add(InvBackButton);
            invPanel.Controls.Add(InvTitle);
            invPanel.Controls.Add(InvCardSlots);
            invPanel.Location = new Point(31, 12);
            invPanel.Name = "invPanel";
            invPanel.Size = new Size(743, 461);
            invPanel.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(InvItemDesc);
            panel1.Controls.Add(InvDescHeader);
            panel1.Location = new Point(106, 243);
            panel1.Name = "panel1";
            panel1.Size = new Size(520, 141);
            panel1.TabIndex = 4;
            // 
            // InvItemDesc
            // 
            InvItemDesc.AutoSize = true;
            InvItemDesc.Font = new Font("Segoe UI", 12F);
            InvItemDesc.ForeColor = Color.Khaki;
            InvItemDesc.Location = new Point(38, 40);
            InvItemDesc.Name = "InvItemDesc";
            InvItemDesc.Size = new Size(43, 21);
            InvItemDesc.TabIndex = 4;
            InvItemDesc.Text = "Desc";
            InvItemDesc.Visible = false;
            // 
            // InvDescHeader
            // 
            InvDescHeader.AutoSize = true;
            InvDescHeader.Font = new Font("Rockwell Extra Bold", 18F);
            InvDescHeader.ForeColor = Color.LightYellow;
            InvDescHeader.Location = new Point(187, 0);
            InvDescHeader.Name = "InvDescHeader";
            InvDescHeader.Size = new Size(165, 28);
            InvDescHeader.TabIndex = 3;
            InvDescHeader.Text = "ItemName";
            InvDescHeader.Visible = false;
            // 
            // InvBackButton
            // 
            InvBackButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            InvBackButton.BackColor = Color.DarkGreen;
            InvBackButton.FlatAppearance.BorderSize = 0;
            InvBackButton.FlatStyle = FlatStyle.Popup;
            InvBackButton.Font = new Font("Papyrus", 15F);
            InvBackButton.ForeColor = Color.White;
            InvBackButton.Location = new Point(568, 406);
            InvBackButton.Name = "InvBackButton";
            InvBackButton.Size = new Size(160, 43);
            InvBackButton.TabIndex = 2;
            InvBackButton.Text = "Close";
            InvBackButton.UseVisualStyleBackColor = false;
            InvBackButton.Click += InvBackButton_Click;
            // 
            // InvTitle
            // 
            InvTitle.AutoSize = true;
            InvTitle.Dock = DockStyle.Top;
            InvTitle.Font = new Font("Rockwell Extra Bold", 24F);
            InvTitle.ForeColor = Color.White;
            InvTitle.Location = new Point(0, 0);
            InvTitle.Name = "InvTitle";
            InvTitle.Size = new Size(205, 37);
            InvTitle.TabIndex = 1;
            InvTitle.Text = "Inventory";
            // 
            // InventoryFrm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Firebrick;
            ClientSize = new Size(800, 485);
            Controls.Add(invPanel);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "InventoryFrm";
            Text = "InventoryFrm";
            Load += InventoryFrm_Load;
            InvCardSlots.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)InvItemSlot1).EndInit();
            invPanel.ResumeLayout(false);
            invPanel.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TableLayoutPanel InvCardSlots;
        private Panel invPanel;
        private Label InvTitle;
        private Button InvBackButton;
        private PictureBox InvItemSlot1;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private Label InvDescHeader;
        private Panel panel1;
        private Label InvItemDesc;
    }
}