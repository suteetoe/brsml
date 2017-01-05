namespace SMLPOSControl._food
{
    partial class _orderCancelControl
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
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this._saveButton = new MyLib.VistaButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._deviceLabel = new System.Windows.Forms.Label();
            this._tableLabel = new System.Windows.Forms.Label();
            this._saleLabel = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._orderPanel = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._cancelAllOrder = new MyLib.ToolStripMyButton();
            this._cancelPanel = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this._orderPanel.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.BackgroundImage = global::SMLPOSControl.Properties.Resources.bt03;
            this.flowLayoutPanel2.Controls.Add(this._saveButton);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(1026, 30);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // _saveButton
            // 
            this._saveButton._drawNewMethod = false;
            this._saveButton.AutoSize = true;
            this._saveButton.BackColor = System.Drawing.Color.Transparent;
            this._saveButton.ButtonText = "บันทึก (F12)";
            this._saveButton.Location = new System.Drawing.Point(923, 3);
            this._saveButton.myImage = global::SMLPOSControl.Properties.Resources.disk_blue;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(100, 24);
            this._saveButton.TabIndex = 9;
            this._saveButton.Text = "บันทึก (F12)";
            this._saveButton.UseVisualStyleBackColor = false;
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this._deviceLabel);
            this.flowLayoutPanel1.Controls.Add(this._tableLabel);
            this.flowLayoutPanel1.Controls.Add(this._saleLabel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1024, 91);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // _deviceLabel
            // 
            this._deviceLabel.AutoSize = true;
            this._deviceLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._deviceLabel.Location = new System.Drawing.Point(3, 0);
            this._deviceLabel.Name = "_deviceLabel";
            this._deviceLabel.Size = new System.Drawing.Size(24, 19);
            this._deviceLabel.TabIndex = 2;
            this._deviceLabel.Text = "...";
            // 
            // _tableLabel
            // 
            this._tableLabel.AutoSize = true;
            this._tableLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._tableLabel.Location = new System.Drawing.Point(33, 0);
            this._tableLabel.Name = "_tableLabel";
            this._tableLabel.Size = new System.Drawing.Size(24, 19);
            this._tableLabel.TabIndex = 0;
            this._tableLabel.Text = "...";
            // 
            // _saleLabel
            // 
            this._saleLabel.AutoSize = true;
            this._saleLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._saleLabel.Location = new System.Drawing.Point(63, 0);
            this._saleLabel.Name = "_saleLabel";
            this._saleLabel.Size = new System.Drawing.Size(24, 19);
            this._saleLabel.TabIndex = 1;
            this._saleLabel.Text = "...";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 30);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1026, 568);
            this.splitContainer1.SplitterDistance = 93;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._orderPanel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._cancelPanel);
            this.splitContainer2.Size = new System.Drawing.Size(1024, 469);
            this.splitContainer2.SplitterDistance = 496;
            this.splitContainer2.TabIndex = 0;
            // 
            // _orderPanel
            // 
            this._orderPanel.Controls.Add(this.toolStrip1);
            this._orderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._orderPanel.Location = new System.Drawing.Point(0, 0);
            this._orderPanel.Name = "_orderPanel";
            this._orderPanel.Size = new System.Drawing.Size(496, 469);
            this._orderPanel.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._cancelAllOrder});
            this.toolStrip1.Location = new System.Drawing.Point(0, 444);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(496, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _cancelAllOrder
            // 
            this._cancelAllOrder.Image = global::SMLPOSControl.Properties.Resources.error1;
            this._cancelAllOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._cancelAllOrder.Name = "_cancelAllOrder";
            this._cancelAllOrder.Padding = new System.Windows.Forms.Padding(1);
            this._cancelAllOrder.ResourceName = "";
            this._cancelAllOrder.Size = new System.Drawing.Size(124, 22);
            this._cancelAllOrder.Text = "ยกเลิกรายการทั้งหมด";
            this._cancelAllOrder.Click += new System.EventHandler(this._cancelAllOrder_Click);
            // 
            // _cancelPanel
            // 
            this._cancelPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cancelPanel.Location = new System.Drawing.Point(0, 0);
            this._cancelPanel.Name = "_cancelPanel";
            this._cancelPanel.Size = new System.Drawing.Size(524, 469);
            this._cancelPanel.TabIndex = 1;
            // 
            // _orderCancelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_orderCancelControl";
            this.Size = new System.Drawing.Size(1026, 598);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this._orderPanel.ResumeLayout(false);
            this._orderPanel.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private MyLib.VistaButton _saveButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label _deviceLabel;
        private System.Windows.Forms.Label _tableLabel;
        private System.Windows.Forms.Label _saleLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel _orderPanel;
        private System.Windows.Forms.Panel _cancelPanel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private MyLib.ToolStripMyButton _cancelAllOrder;
    }
}
