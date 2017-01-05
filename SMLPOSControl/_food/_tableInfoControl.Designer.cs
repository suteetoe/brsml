namespace SMLPOSControl._food
{
    partial class _tableInfoControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._tableLevelPanel = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._orderListPanel = new System.Windows.Forms.Panel();
            this._itemListPanel = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this._allItemButton = new MyLib.VistaButton();
            this._byOrderButton = new MyLib.VistaButton();
            this._printBillButton = new MyLib.VistaButton();
            this._reprintOrderButton = new MyLib.VistaButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._tableLevelPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1123, 622);
            this.splitContainer1.SplitterDistance = 246;
            this.splitContainer1.TabIndex = 0;
            // 
            // _tableLevelPanel
            // 
            this._tableLevelPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tableLevelPanel.Location = new System.Drawing.Point(0, 0);
            this._tableLevelPanel.Name = "_tableLevelPanel";
            this._tableLevelPanel.Size = new System.Drawing.Size(1121, 244);
            this._tableLevelPanel.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._orderListPanel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._itemListPanel);
            this.splitContainer2.Panel2.Controls.Add(this.flowLayoutPanel2);
            this.splitContainer2.Size = new System.Drawing.Size(1123, 372);
            this.splitContainer2.SplitterDistance = 533;
            this.splitContainer2.TabIndex = 0;
            // 
            // _orderListPanel
            // 
            this._orderListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._orderListPanel.Location = new System.Drawing.Point(0, 0);
            this._orderListPanel.Name = "_orderListPanel";
            this._orderListPanel.Size = new System.Drawing.Size(531, 370);
            this._orderListPanel.TabIndex = 4;
            // 
            // _itemListPanel
            // 
            this._itemListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemListPanel.Location = new System.Drawing.Point(0, 60);
            this._itemListPanel.Name = "_itemListPanel";
            this._itemListPanel.Size = new System.Drawing.Size(584, 310);
            this._itemListPanel.TabIndex = 3;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.BackgroundImage = global::SMLPOSControl.Properties.Resources.bt03;
            this.flowLayoutPanel2.Controls.Add(this._allItemButton);
            this.flowLayoutPanel2.Controls.Add(this._byOrderButton);
            this.flowLayoutPanel2.Controls.Add(this._printBillButton);
            this.flowLayoutPanel2.Controls.Add(this._reprintOrderButton);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(584, 60);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // _allItemButton
            // 
            this._allItemButton._drawNewMethod = false;
            this._allItemButton.AutoSize = true;
            this._allItemButton.BackColor = System.Drawing.Color.Transparent;
            this._allItemButton.ButtonText = "แสดงรายการอาหารทั้งหมด";
            this._allItemButton.Location = new System.Drawing.Point(409, 3);
            this._allItemButton.myImage = global::SMLPOSControl.Properties.Resources.bringtofront;
            this._allItemButton.Name = "_allItemButton";
            this._allItemButton.Size = new System.Drawing.Size(172, 24);
            this._allItemButton.TabIndex = 9;
            this._allItemButton.Text = "แสดงรายการอาหารทั้งหมด";
            this._allItemButton.UseVisualStyleBackColor = false;
            this._allItemButton.Click += new System.EventHandler(this._allItemButton_Click);
            // 
            // _byOrderButton
            // 
            this._byOrderButton._drawNewMethod = false;
            this._byOrderButton.AutoSize = true;
            this._byOrderButton.BackColor = System.Drawing.Color.Transparent;
            this._byOrderButton.ButtonText = "แสดงรายการอาหารตามใบสั่ง";
            this._byOrderButton.Location = new System.Drawing.Point(221, 3);
            this._byOrderButton.myImage = global::SMLPOSControl.Properties.Resources.bringtofront;
            this._byOrderButton.Name = "_byOrderButton";
            this._byOrderButton.Size = new System.Drawing.Size(182, 24);
            this._byOrderButton.TabIndex = 10;
            this._byOrderButton.Text = "แสดงรายการอาหารตามใบสั่ง";
            this._byOrderButton.UseVisualStyleBackColor = false;
            this._byOrderButton.Click += new System.EventHandler(this._byOrderButton_Click);
            // 
            // _printBillButton
            // 
            this._printBillButton._drawNewMethod = false;
            this._printBillButton.AutoSize = true;
            this._printBillButton.BackColor = System.Drawing.Color.Transparent;
            this._printBillButton.ButtonText = "พิมพ์ใบสรุป";
            this._printBillButton.Location = new System.Drawing.Point(121, 3);
            this._printBillButton.myImage = global::SMLPOSControl.Properties.Resources.scroll_preferences;
            this._printBillButton.Name = "_printBillButton";
            this._printBillButton.Size = new System.Drawing.Size(94, 24);
            this._printBillButton.TabIndex = 11;
            this._printBillButton.Text = "พิมพ์ใบสรุป";
            this._printBillButton.UseVisualStyleBackColor = false;
            // 
            // _reprintOrderButton
            // 
            this._reprintOrderButton._drawNewMethod = false;
            this._reprintOrderButton.AutoSize = true;
            this._reprintOrderButton.BackColor = System.Drawing.Color.Transparent;
            this._reprintOrderButton.ButtonText = "ส่งพิมพ์ใบสั่งอาหารใหม่";
            this._reprintOrderButton.Enabled = false;
            this._reprintOrderButton.Location = new System.Drawing.Point(426, 33);
            this._reprintOrderButton.myImage = global::SMLPOSControl.Properties.Resources.scroll_preferences;
            this._reprintOrderButton.Name = "_reprintOrderButton";
            this._reprintOrderButton.Size = new System.Drawing.Size(155, 24);
            this._reprintOrderButton.TabIndex = 12;
            this._reprintOrderButton.Text = "ส่งพิมพ์ใบสั่งอาหารใหม่";
            this._reprintOrderButton.UseVisualStyleBackColor = false;
            this._reprintOrderButton.Click += new System.EventHandler(this._reprintOrderButton_Click);
            // 
            // _tableInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_tableInfoControl";
            this.Size = new System.Drawing.Size(1123, 622);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel _tableLevelPanel;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel _orderListPanel;
        private System.Windows.Forms.Panel _itemListPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private MyLib.VistaButton _allItemButton;
        private MyLib.VistaButton _byOrderButton;
        public MyLib.VistaButton _printBillButton;
        public MyLib.VistaButton _reprintOrderButton;
    }
}
