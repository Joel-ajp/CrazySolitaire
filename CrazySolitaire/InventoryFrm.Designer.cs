
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventoryFrm));
            InvCardSlots = new TableLayoutPanel();
            InvItemSlot3 = new PictureBox();
            InvItemSlot2 = new PictureBox();
            InvItemSlot1 = new PictureBox();
            invPanel = new Panel();
            panel1 = new Panel();
            InvItemDesc = new Label();
            InvDescHeader = new Label();
            InvBackButton = new Button();
            InvTitle = new Label();
            InvCardSlots.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)InvItemSlot3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)InvItemSlot2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)InvItemSlot1).BeginInit();
            invPanel.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // InvCardSlots
            // 
            InvCardSlots.BackColor = Color.Black;
            InvCardSlots.ColumnCount = 3;
            InvCardSlots.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            InvCardSlots.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            InvCardSlots.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            InvCardSlots.Controls.Add(InvItemSlot3, 2, 0);
            InvCardSlots.Controls.Add(InvItemSlot2, 1, 0);
            InvCardSlots.Controls.Add(InvItemSlot1, 0, 0);
            InvCardSlots.Location = new Point(166, 61);
            InvCardSlots.Name = "InvCardSlots";
            InvCardSlots.RowCount = 1;
            InvCardSlots.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            InvCardSlots.Size = new Size(415, 183);
            InvCardSlots.TabIndex = 0;
            // 
            // InvItemSlot3
            // 
            InvItemSlot3.BackColor = Color.Lime;
            InvItemSlot3.Dock = DockStyle.Fill;
            InvItemSlot3.Image = (Image)resources.GetObject("InvItemSlot3.Image");
            InvItemSlot3.Location = new Point(281, 5);
            InvItemSlot3.Margin = new Padding(5);
            InvItemSlot3.Name = "InvItemSlot3";
            InvItemSlot3.Size = new Size(129, 173);
            InvItemSlot3.SizeMode = PictureBoxSizeMode.Zoom;
            InvItemSlot3.TabIndex = 2;
            InvItemSlot3.TabStop = false;
            InvItemSlot3.Visible = false;
            InvItemSlot3.MouseLeave += InvItemSlot_MouseLeave;
            InvItemSlot3.MouseHover += InvItemSlot1_MouseHover;
            // 
            // InvItemSlot2
            // 
            InvItemSlot2.BackColor = Color.Purple;
            InvItemSlot2.Dock = DockStyle.Fill;
            InvItemSlot2.Image = (Image)resources.GetObject("InvItemSlot2.Image");
            InvItemSlot2.Location = new Point(143, 5);
            InvItemSlot2.Margin = new Padding(5);
            InvItemSlot2.Name = "InvItemSlot2";
            InvItemSlot2.Size = new Size(128, 173);
            InvItemSlot2.SizeMode = PictureBoxSizeMode.Zoom;
            InvItemSlot2.TabIndex = 1;
            InvItemSlot2.TabStop = false;
            InvItemSlot2.Visible = false;
            InvItemSlot2.MouseLeave += InvItemSlot_MouseLeave;
            InvItemSlot2.MouseHover += InvItemSlot1_MouseHover;
            // 
            // InvItemSlot1
            // 
            InvItemSlot1.BackColor = Color.Navy;
            InvItemSlot1.BackgroundImageLayout = ImageLayout.Center;
            InvItemSlot1.Dock = DockStyle.Fill;
            InvItemSlot1.Image = (Image)resources.GetObject("InvItemSlot1.Image");
            InvItemSlot1.Location = new Point(5, 5);
            InvItemSlot1.Margin = new Padding(5);
            InvItemSlot1.Name = "InvItemSlot1";
            InvItemSlot1.Size = new Size(128, 173);
            InvItemSlot1.SizeMode = PictureBoxSizeMode.Zoom;
            InvItemSlot1.TabIndex = 0;
            InvItemSlot1.TabStop = false;
            InvItemSlot1.Visible = false;
            InvItemSlot1.MouseLeave += InvItemSlot_MouseLeave;
            InvItemSlot1.MouseHover += InvItemSlot1_MouseHover;
            // 
            // invPanel
            // 
            invPanel.BackColor = Color.Black;
            invPanel.Controls.Add(panel1);
            invPanel.Controls.Add(InvBackButton);
            invPanel.Controls.Add(InvTitle);
            invPanel.Controls.Add(InvCardSlots);
            invPanel.Location = new Point(12, 12);
            invPanel.Name = "invPanel";
            invPanel.Size = new Size(776, 461);
            invPanel.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(InvItemDesc);
            panel1.Controls.Add(InvDescHeader);
            panel1.Location = new Point(106, 259);
            panel1.Name = "panel1";
            panel1.Size = new Size(520, 141);
            panel1.TabIndex = 4;
            // 
            // InvItemDesc
            // 
            InvItemDesc.AutoSize = true;
            InvItemDesc.Font = new Font("Times New Roman", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            InvItemDesc.ForeColor = Color.Khaki;
            InvItemDesc.Location = new Point(12, 40);
            InvItemDesc.Name = "InvItemDesc";
            InvItemDesc.Size = new Size(57, 25);
            InvItemDesc.TabIndex = 4;
            InvItemDesc.Text = "Desc";
            InvItemDesc.Visible = false;
            // 
            // InvDescHeader
            // 
            InvDescHeader.AutoSize = true;
            InvDescHeader.Font = new Font("Times New Roman", 20F);
            InvDescHeader.ForeColor = Color.LightYellow;
            InvDescHeader.Location = new Point(-1, -1);
            InvDescHeader.Name = "InvDescHeader";
            InvDescHeader.Size = new Size(125, 31);
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
            InvBackButton.Font = new Font("Copperplate Gothic Light", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            InvBackButton.ForeColor = Color.White;
            InvBackButton.Location = new Point(634, 419);
            InvBackButton.Name = "InvBackButton";
            InvBackButton.Size = new Size(127, 30);
            InvBackButton.TabIndex = 2;
            InvBackButton.Text = "Close";
            InvBackButton.UseVisualStyleBackColor = false;
            InvBackButton.Click += InvBackButton_Click;
            // 
            // InvTitle
            // 
            InvTitle.Anchor = AnchorStyles.Top;
            InvTitle.AutoSize = true;
            InvTitle.Font = new Font("Felix Titling", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            InvTitle.ForeColor = Color.White;
            InvTitle.Location = new Point(283, 11);
            InvTitle.Name = "InvTitle";
            InvTitle.Size = new Size(204, 38);
            InvTitle.TabIndex = 1;
            InvTitle.Text = "Inventory";
            // 
            // InventoryFrm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Yellow;
            ClientSize = new Size(800, 485);
            Controls.Add(invPanel);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "InventoryFrm";
            Text = "InventoryFrm";
            Load += InventoryFrm_Load;
            InvCardSlots.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)InvItemSlot3).EndInit();
            ((System.ComponentModel.ISupportInitialize)InvItemSlot2).EndInit();
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
        private PictureBox InvItemSlot3;
        private PictureBox InvItemSlot2;
        private Label InvDescHeader;
        private Panel panel1;
        private Label InvItemDesc;
    }
}