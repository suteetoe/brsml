namespace SMLERPIC
{
    partial class _icWeightCost
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_icWeightCost));
            this._myManageData1 = new MyLib._myManageData();
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._icmainScreenTop = new SMLInventoryControl._icmainScreenWeightCost();
            this._icmainGridWeightCost = new SMLInventoryControl._icmainGridWeightCostControl();
            this._myManageData1._form2.SuspendLayout();
            this._myManageData1.SuspendLayout();
            this._myToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myManageData1
            // 
            this._myManageData1.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageData1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageData1.Location = new System.Drawing.Point(0, 0);
            this._myManageData1.Name = "_myManageData1";
            // 
            // _myManageData1.Panel1
            // 
            this._myManageData1._form1.BackColor = System.Drawing.Color.WhiteSmoke;
            // 
            // _myManageData1.Panel2
            // 
            this._myManageData1._form2.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageData1._form2.Controls.Add(this._icmainGridWeightCost);
            this._myManageData1._form2.Controls.Add(this._icmainScreenTop);
            this._myManageData1._form2.Controls.Add(this._myToolBar);
            this._myManageData1.Size = new System.Drawing.Size(759, 569);
            this._myManageData1.TabIndex = 0;
            this._myManageData1.TabStop = false;
            // 
            // _myToolBar
            // 
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(583, 25);
            this._myToolBar.TabIndex = 0;
            this._myToolBar.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._saveButton.Image = ((System.Drawing.Image)(resources.GetObject("_saveButton.Image")));
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.ResourceName = "บันทึกข้อมูล (F12)";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.Size = new System.Drawing.Size(23, 22);
            this._saveButton.Text = "บันทึก";
            // 
            // _icmainScreenTop
            // 
            this._icmainScreenTop._isChange = false;
            this._icmainScreenTop.AutoSize = true;
            this._icmainScreenTop.BackColor = System.Drawing.Color.Transparent;
            this._icmainScreenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._icmainScreenTop.Location = new System.Drawing.Point(0, 25);
            this._icmainScreenTop.Name = "_icmainScreenTop";
            this._icmainScreenTop.Size = new System.Drawing.Size(583, 159);
            this._icmainScreenTop.TabIndex = 1;
            // 
            // _icmainGridWeightCost
            // 
            this._icmainGridWeightCost.AllowDrop = true;
            this._icmainGridWeightCost.BackColor = System.Drawing.SystemColors.Window;
            this._icmainGridWeightCost.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._icmainGridWeightCost.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._icmainGridWeightCost.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icmainGridWeightCost.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._icmainGridWeightCost.Location = new System.Drawing.Point(0, 184);
            this._icmainGridWeightCost.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._icmainGridWeightCost.Name = "_icmainGridWeightCost";
            this._icmainGridWeightCost.Size = new System.Drawing.Size(583, 383);
            this._icmainGridWeightCost.TabIndex = 2;
            this._icmainGridWeightCost.TabStop = false;
            // 
            // _icWeightCost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageData1);
            this.Name = "_icWeightCost";
            this.Size = new System.Drawing.Size(759, 569);
            this._myManageData1._form2.ResumeLayout(false);
            this._myManageData1._form2.PerformLayout();
            this._myManageData1.ResumeLayout(false);
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myManageData _myManageData1;
        private SMLInventoryControl._icmainScreenWeightCost _icmainScreenTop;
        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib.ToolStripMyButton _saveButton;
        private SMLInventoryControl._icmainGridWeightCostControl _icmainGridWeightCost;
    }
}
