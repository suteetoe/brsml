namespace SMLInventoryControl
{
    partial class _icTransRefControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_icTransRefControl));
            this._toolBarButtom = new System.Windows.Forms.ToolStrip();
            this._processToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._refCheck = new System.Windows.Forms.ToolStripButton();
            this._imageList = new System.Windows.Forms.ImageList(this.components);
            this._mapRefLineNumberButton = new MyLib.ToolStripMyButton();
            this._conditionScreen = new MyLib._myScreen();
            this._toolBarButtom.SuspendLayout();
            this.SuspendLayout();
            // 
            // _toolBarButtom
            // 
            this._toolBarButtom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._toolBarButtom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._toolBarButtom.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolBarButtom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._processToolStripButton,
            this._refCheck,
            this._mapRefLineNumberButton});
            this._toolBarButtom.Location = new System.Drawing.Point(0, 458);
            this._toolBarButtom.Name = "_toolBarButtom";
            this._toolBarButtom.Size = new System.Drawing.Size(932, 25);
            this._toolBarButtom.TabIndex = 2;
            this._toolBarButtom.Text = "_toolStripButtom";
            // 
            // _processToolStripButton
            // 
            this._processToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._processToolStripButton.Name = "_processToolStripButton";
            this._processToolStripButton.Size = new System.Drawing.Size(52, 22);
            this._processToolStripButton.Text = "Process";
            this._processToolStripButton.Click += new System.EventHandler(this._processToolStripButton_Click);
            // 
            // _refCheck
            // 
            this._refCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._refCheck.Name = "_refCheck";
            this._refCheck.Size = new System.Drawing.Size(79, 22);
            this._refCheck.Text = "เอกสารอ้างอิง";
            // 
            // _imageList
            // 
            this._imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList.ImageStream")));
            this._imageList.TransparentColor = System.Drawing.Color.Transparent;
            this._imageList.Images.SetKeyName(0, "delete2.png");
            this._imageList.Images.SetKeyName(1, "check2.png");
            // 
            // _mapRefLineNumberButton
            // 
            this._mapRefLineNumberButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._mapRefLineNumberButton.Image = ((System.Drawing.Image)(resources.GetObject("_mapRefLineNumberButton.Image")));
            this._mapRefLineNumberButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._mapRefLineNumberButton.Name = "_mapRefLineNumberButton";
            this._mapRefLineNumberButton.Padding = new System.Windows.Forms.Padding(1);
            this._mapRefLineNumberButton.ResourceName = "Map Ref Line Number";
            this._mapRefLineNumberButton.Size = new System.Drawing.Size(130, 22);
            this._mapRefLineNumberButton.Text = "Map Ref Line Number";
            this._mapRefLineNumberButton.Visible = false;
            // 
            // _conditionScreen
            // 
            this._conditionScreen._isChange = false;
            this._conditionScreen.AutoSize = true;
            this._conditionScreen.BackColor = System.Drawing.Color.Transparent;
            this._conditionScreen.Dock = System.Windows.Forms.DockStyle.Top;
            this._conditionScreen.Location = new System.Drawing.Point(0, 0);
            this._conditionScreen.Name = "_conditionScreen";
            this._conditionScreen.Size = new System.Drawing.Size(932, 0);
            this._conditionScreen.TabIndex = 1;
            // 
            // _icTransRefControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._toolBarButtom);
            this.Controls.Add(this._conditionScreen);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_icTransRefControl";
            this.Size = new System.Drawing.Size(932, 483);
            this._toolBarButtom.ResumeLayout(false);
            this._toolBarButtom.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myScreen _conditionScreen;
        private System.Windows.Forms.ToolStripButton _processToolStripButton;
        private System.Windows.Forms.ImageList _imageList;
        public System.Windows.Forms.ToolStripButton _refCheck;
        public System.Windows.Forms.ToolStrip _toolBarButtom;
        public MyLib.ToolStripMyButton _mapRefLineNumberButton;
    }
}
