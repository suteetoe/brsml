namespace SMLERPConfig
{
    partial class _optionSaleShiftScreen
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
            this._saleShiftScreen = new MyLib._myScreen();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._createShiftButton = new MyLib.VistaButton();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _saleShiftScreen
            // 
            this._saleShiftScreen._isChange = false;
            this._saleShiftScreen.BackColor = System.Drawing.Color.Transparent;
            this._saleShiftScreen.Dock = System.Windows.Forms.DockStyle.Top;
            this._saleShiftScreen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._saleShiftScreen.Location = new System.Drawing.Point(0, 0);
            this._saleShiftScreen.Name = "_saleShiftScreen";
            this._saleShiftScreen.Size = new System.Drawing.Size(650, 10);
            this._saleShiftScreen.TabIndex = 0;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._createShiftButton);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 10);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5, 3, 5, 0);
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(650, 44);
            this._myFlowLayoutPanel1.TabIndex = 1;
            // 
            // _createShiftButton
            // 
            this._createShiftButton._drawNewMethod = false;
            this._createShiftButton.BackColor = System.Drawing.Color.Transparent;
            this._createShiftButton.ButtonText = "สร้างรอบการขาย";
            this._createShiftButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._createShiftButton.Location = new System.Drawing.Point(516, 6);
            this._createShiftButton.myImage = global::SMLERPConfig.Properties.Resources.flash2;
            this._createShiftButton.Name = "_createShiftButton";
            this._createShiftButton.Size = new System.Drawing.Size(121, 23);
            this._createShiftButton.TabIndex = 0;
            this._createShiftButton.Text = "vistaButton1";
            this._createShiftButton.UseVisualStyleBackColor = false;
            this._createShiftButton.Click += new System.EventHandler(this._createShiftButton_Click);
            // 
            // _optionSaleShiftScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myFlowLayoutPanel1);
            this.Controls.Add(this._saleShiftScreen);
            this.Name = "_optionSaleShiftScreen";
            this.Size = new System.Drawing.Size(650, 412);
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public MyLib._myScreen _saleShiftScreen;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib.VistaButton _createShiftButton;
    }
}
