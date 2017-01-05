namespace SMLPOSControl._food
{
    partial class _tableOrderEditControl
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
            this._tableInfoPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._deviceLabel = new System.Windows.Forms.Label();
            this._tableLabel = new System.Windows.Forms.Label();
            this._saleLabel = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._orderDocListPanel = new System.Windows.Forms.Panel();
            this._orderDetailPanel = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this._saveButton = new MyLib.VistaButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this._tableInfoPanel.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._tableInfoPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(950, 710);
            this.splitContainer1.SplitterDistance = 106;
            this.splitContainer1.TabIndex = 0;
            // 
            // _tableInfoPanel
            // 
            this._tableInfoPanel.Controls.Add(this._deviceLabel);
            this._tableInfoPanel.Controls.Add(this._tableLabel);
            this._tableInfoPanel.Controls.Add(this._saleLabel);
            this._tableInfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tableInfoPanel.Location = new System.Drawing.Point(0, 0);
            this._tableInfoPanel.Name = "_tableInfoPanel";
            this._tableInfoPanel.Size = new System.Drawing.Size(950, 106);
            this._tableInfoPanel.TabIndex = 0;
            // 
            // _deviceLabel
            // 
            this._deviceLabel.AutoSize = true;
            this._deviceLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._deviceLabel.Location = new System.Drawing.Point(3, 0);
            this._deviceLabel.Name = "_deviceLabel";
            this._deviceLabel.Size = new System.Drawing.Size(24, 19);
            this._deviceLabel.TabIndex = 5;
            this._deviceLabel.Text = "...";
            // 
            // _tableLabel
            // 
            this._tableLabel.AutoSize = true;
            this._tableLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._tableLabel.Location = new System.Drawing.Point(33, 0);
            this._tableLabel.Name = "_tableLabel";
            this._tableLabel.Size = new System.Drawing.Size(24, 19);
            this._tableLabel.TabIndex = 3;
            this._tableLabel.Text = "...";
            // 
            // _saleLabel
            // 
            this._saleLabel.AutoSize = true;
            this._saleLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._saleLabel.Location = new System.Drawing.Point(63, 0);
            this._saleLabel.Name = "_saleLabel";
            this._saleLabel.Size = new System.Drawing.Size(24, 19);
            this._saleLabel.TabIndex = 4;
            this._saleLabel.Text = "...";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._orderDocListPanel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel1);
            this.splitContainer2.Size = new System.Drawing.Size(950, 600);
            this.splitContainer2.SplitterDistance = 473;
            this.splitContainer2.TabIndex = 0;
            // 
            // _orderDocListPanel
            // 
            this._orderDocListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._orderDocListPanel.Location = new System.Drawing.Point(0, 0);
            this._orderDocListPanel.Name = "_orderDocListPanel";
            this._orderDocListPanel.Size = new System.Drawing.Size(473, 600);
            this._orderDocListPanel.TabIndex = 0;
            // 
            // _orderDetailPanel
            // 
            this._orderDetailPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._orderDetailPanel.Location = new System.Drawing.Point(0, 30);
            this._orderDetailPanel.Name = "_orderDetailPanel";
            this._orderDetailPanel.Size = new System.Drawing.Size(471, 568);
            this._orderDetailPanel.TabIndex = 2;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BackgroundImage = global::SMLPOSControl.Properties.Resources.bt03;
            this.flowLayoutPanel2.Controls.Add(this._saveButton);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(471, 30);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // _saveButton
            // 
            this._saveButton._drawNewMethod = false;
            this._saveButton.AutoSize = true;
            this._saveButton.BackColor = System.Drawing.Color.Transparent;
            this._saveButton.ButtonText = "บันทึก (F12)";
            this._saveButton.Location = new System.Drawing.Point(376, 3);
            this._saveButton.myImage = global::SMLPOSControl.Properties.Resources.disk_blue;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(92, 24);
            this._saveButton.TabIndex = 10;
            this._saveButton.Text = "บันทึก (F12)";
            this._saveButton.UseVisualStyleBackColor = false;
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this._orderDetailPanel);
            this.panel1.Controls.Add(this.flowLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(473, 600);
            this.panel1.TabIndex = 0;
            // 
            // _tableOrderEditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "_tableOrderEditControl";
            this.Size = new System.Drawing.Size(950, 710);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this._tableInfoPanel.ResumeLayout(false);
            this._tableInfoPanel.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel _tableInfoPanel;
        private System.Windows.Forms.Label _deviceLabel;
        private System.Windows.Forms.Label _tableLabel;
        private System.Windows.Forms.Label _saleLabel;
        private MyLib.VistaButton _saveButton;
        private System.Windows.Forms.Panel _orderDocListPanel;
        private System.Windows.Forms.Panel _orderDetailPanel;
        private System.Windows.Forms.Panel panel1;
    }
}
