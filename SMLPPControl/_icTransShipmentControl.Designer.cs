namespace SMLPPControl
{
    partial class _icTransShipmentControl
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._shipmentScreen = new MyLib._myScreen();
            this.panel1 = new System.Windows.Forms.Panel();
            this._shipmentSearchButton = new MyLib.ToolStripMyButton();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._shipmentSearchButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(723, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _shipmentScreen
            // 
            this._shipmentScreen._isChange = false;
            this._shipmentScreen.BackColor = System.Drawing.Color.Transparent;
            this._shipmentScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this._shipmentScreen.Location = new System.Drawing.Point(0, 0);
            this._shipmentScreen.Name = "_shipmentScreen";
            this._shipmentScreen.Size = new System.Drawing.Size(723, 702);
            this._shipmentScreen.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._shipmentScreen);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(723, 702);
            this.panel1.TabIndex = 4;
            // 
            // _shipmentSearchButton
            // 
            this._shipmentSearchButton.Image = global::SMLPPControl.Properties.Resources.folder_add;
            this._shipmentSearchButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._shipmentSearchButton.Name = "_shipmentSearchButton";
            this._shipmentSearchButton.Padding = new System.Windows.Forms.Padding(1);
            this._shipmentSearchButton.ResourceName = "ค้นหาที่อยู่";
            this._shipmentSearchButton.Size = new System.Drawing.Size(74, 22);
            this._shipmentSearchButton.Text = "ค้นหาที่อยู่";
            this._shipmentSearchButton.Click += new System.EventHandler(this._shipmentSearchButton_Click);
            // 
            // _icTransShipmentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_icTransShipmentControl";
            this.Size = new System.Drawing.Size(723, 727);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private MyLib.ToolStripMyButton _shipmentSearchButton;
        public MyLib._myScreen _shipmentScreen;
        private System.Windows.Forms.Panel panel1;
    }
}
