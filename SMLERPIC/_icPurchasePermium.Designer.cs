namespace SMLERPIC
{
    partial class _icPurchasePermium
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
            this._myManageMain = new MyLib._myManageData();
            this._condition = new SMLInventoryControl._icTransItemGridControl();
            this._myShadowLabel1 = new MyLib._myShadowLabel(this.components);
            this._icPurchasePermiumScreen = new SMLERPIC._icPurchasePermiumScreenControl();
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._myShadowLabel2 = new MyLib._myShadowLabel(this.components);
            this._permiumList = new SMLInventoryControl._icTransItemGridControl();
            this._myManageMain._form2.SuspendLayout();
            this._myManageMain.SuspendLayout();
            this._myToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myManageMain
            // 
            this._myManageMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageMain.Location = new System.Drawing.Point(0, 0);
            this._myManageMain.Name = "_myManageMain";
            // 
            // _myManageMain.Panel1
            // 
            this._myManageMain._form1.BackColor = System.Drawing.Color.WhiteSmoke;
            // 
            // _myManageMain.Panel2
            // 
            this._myManageMain._form2.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageMain._form2.Controls.Add(this._condition);
            this._myManageMain._form2.Controls.Add(this._myShadowLabel1);
            this._myManageMain._form2.Controls.Add(this._icPurchasePermiumScreen);
            this._myManageMain._form2.Controls.Add(this._myToolBar);
            this._myManageMain._form2.Controls.Add(this._myShadowLabel2);
            this._myManageMain._form2.Controls.Add(this._permiumList);
            this._myManageMain.Size = new System.Drawing.Size(874, 619);
            this._myManageMain.TabIndex = 0;
            this._myManageMain.TabStop = false;
            // 
            // _condition
            // 
            this._condition._extraWordShow = true;
            this._condition._icTransControlType = _g.g._transControlTypeEnum.สินค้า_เงื่อนไขแถมตอนซื้อ;
            this._condition._icTransRef = null;
            this._condition.BackColor = System.Drawing.SystemColors.Window;
            this._condition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._condition.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._condition.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._condition.Dock = System.Windows.Forms.DockStyle.Fill;
            this._condition.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._condition.Location = new System.Drawing.Point(0, 136);
            this._condition.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._condition.Name = "_condition";
            this._condition.Size = new System.Drawing.Size(672, 217);
            this._condition.TabIndex = 3;
            this._condition.TabStop = false;
            // 
            // _myShadowLabel1
            // 
            this._myShadowLabel1.Angle = 0F;
            this._myShadowLabel1.AutoSize = true;
            this._myShadowLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myShadowLabel1.DrawGradient = false;
            this._myShadowLabel1.EndColor = System.Drawing.Color.LightSkyBlue;
            this._myShadowLabel1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myShadowLabel1.ForeColor = System.Drawing.Color.LightSkyBlue;
            this._myShadowLabel1.Location = new System.Drawing.Point(0, 117);
            this._myShadowLabel1.Name = "_myShadowLabel1";
            this._myShadowLabel1.ShadowColor = System.Drawing.Color.Black;
            this._myShadowLabel1.Size = new System.Drawing.Size(87, 19);
            this._myShadowLabel1.StartColor = System.Drawing.Color.White;
            this._myShadowLabel1.TabIndex = 6;
            this._myShadowLabel1.Text = "Condition";
            this._myShadowLabel1.XOffset = 1F;
            this._myShadowLabel1.YOffset = 1F;
            // 
            // _icPurchasePermiumScreen
            // 
            this._icPurchasePermiumScreen._isChange = false;
            this._icPurchasePermiumScreen.AutoSize = true;
            this._icPurchasePermiumScreen.BackColor = System.Drawing.Color.Transparent;
            this._icPurchasePermiumScreen.Dock = System.Windows.Forms.DockStyle.Top;
            this._icPurchasePermiumScreen.Location = new System.Drawing.Point(0, 25);
            this._icPurchasePermiumScreen.Name = "_icPurchasePermiumScreen";
            this._icPurchasePermiumScreen.Size = new System.Drawing.Size(672, 92);
            this._icPurchasePermiumScreen.TabIndex = 2;
            // 
            // _myToolBar
            // 
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this.toolStripSeparator1,
            this._closeButton});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(672, 25);
            this._myToolBar.TabIndex = 1;
            this._myToolBar.Text = "_toolBar";
            // 
            // _saveButton
            // 
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.ResourceName = "บันทึกข้อมูล (F12)";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.Size = new System.Drawing.Size(77, 22);
            this._saveButton.Text = "บันทึก ( F12 )";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _closeButton
            // 
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.ResourceName = "ปิดหน้าจอ";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.Size = new System.Drawing.Size(59, 22);
            this._closeButton.Text = "ปิดหน้าจอ";
            // 
            // _myShadowLabel2
            // 
            this._myShadowLabel2.Angle = 0F;
            this._myShadowLabel2.AutoSize = true;
            this._myShadowLabel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myShadowLabel2.DrawGradient = false;
            this._myShadowLabel2.EndColor = System.Drawing.Color.LightSkyBlue;
            this._myShadowLabel2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myShadowLabel2.ForeColor = System.Drawing.Color.LightSkyBlue;
            this._myShadowLabel2.Location = new System.Drawing.Point(0, 353);
            this._myShadowLabel2.Name = "_myShadowLabel2";
            this._myShadowLabel2.ShadowColor = System.Drawing.Color.Black;
            this._myShadowLabel2.Size = new System.Drawing.Size(82, 19);
            this._myShadowLabel2.StartColor = System.Drawing.Color.White;
            this._myShadowLabel2.TabIndex = 7;
            this._myShadowLabel2.Text = "Premium";
            this._myShadowLabel2.XOffset = 1F;
            this._myShadowLabel2.YOffset = 1F;
            // 
            // _permiumList
            // 
            this._permiumList._extraWordShow = true;
            this._permiumList._icTransControlType = _g.g._transControlTypeEnum.สินค้า_สินค้าแถมตอนซื้อ;
            this._permiumList._icTransRef = null;
            this._permiumList.BackColor = System.Drawing.SystemColors.Window;
            this._permiumList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._permiumList.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._permiumList.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._permiumList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._permiumList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._permiumList.Location = new System.Drawing.Point(0, 372);
            this._permiumList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._permiumList.Name = "_permiumList";
            this._permiumList.Size = new System.Drawing.Size(672, 245);
            this._permiumList.TabIndex = 4;
            this._permiumList.TabStop = false;
            // 
            // _icPurchasePermium
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_icPurchasePermium";
            this.Size = new System.Drawing.Size(874, 619);
            this._myManageMain._form2.ResumeLayout(false);
            this._myManageMain._form2.PerformLayout();
            this._myManageMain.ResumeLayout(false);
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myManageData _myManageMain;
        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib.ToolStripMyButton _saveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private MyLib.ToolStripMyButton _closeButton;
        private _icPurchasePermiumScreenControl _icPurchasePermiumScreen;
        private SMLInventoryControl._icTransItemGridControl _condition;
        private SMLInventoryControl._icTransItemGridControl _permiumList;
        private MyLib._myShadowLabel _myShadowLabel1;
        private MyLib._myShadowLabel _myShadowLabel2;
    }
}
