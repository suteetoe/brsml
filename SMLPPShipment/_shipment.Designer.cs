namespace SMLPPShipment
{
    partial class _shipment
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
            this._shipmentControl1 = new SMLPPControl._shipmentControl();
            this.SuspendLayout();
            // 
            // _shipmentControl1
            // 
            this._shipmentControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._shipmentControl1.Location = new System.Drawing.Point(0, 0);
            this._shipmentControl1.Name = "_shipmentControl1";
            this._shipmentControl1.Size = new System.Drawing.Size(672, 679);
            this._shipmentControl1.TabIndex = 0;
            this._shipmentControl1.transControlType = SMLPPGlobal.g._ppControlTypeEnum.Shipment;
            // 
            // _shipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._shipmentControl1);
            this.Name = "_shipment";
            this.Size = new System.Drawing.Size(672, 679);
            this.ResumeLayout(false);

        }

        #endregion

        private SMLPPControl._shipmentControl _shipmentControl1;
    }
}
