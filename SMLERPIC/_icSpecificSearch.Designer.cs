namespace SMLERPIC
{
    partial class _icSpecificSearch
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
            this._myPanel1 = new MyLib._myPanel();
            //this._grid = new SMLERPIC._gridICSpecialSearchWord();
            this._screenTop = new _icSpecialSearchWordControl();
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._myPanel1.SuspendLayout();
            this._myToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myManageMain
            // 
            this._myManageMain._mainMenuCode = "";
            this._myManageMain._mainMenuId = "";
            this._myManageMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageMain.Location = new System.Drawing.Point(0, 0);
            this._myManageMain.Name = "_myManageMain";
            this._myManageMain.Size = new System.Drawing.Size(953, 762);
            this._myManageMain.TabIndex = 2;
            this._myManageMain.TabStop = false;
            this._myManageMain._form2.Controls.Add(this._myPanel1);
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            //this._myPanel1.Controls.Add(this._grid);
            this._myPanel1.Controls.Add(this._screenTop);
            this._myPanel1.Controls.Add(this._myToolBar);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(868, 741);
            this._myPanel1.TabIndex = 3;
            // 
            // _grid
            // 
            //this._grid._extraWordShow = true;
            //this._grid._selectRow = -1;
            //this._grid.BackColor = System.Drawing.SystemColors.Window;
            //this._grid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            //this._grid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            //this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            //this._grid.Font = new System.Drawing.Font("Tahoma", 9F);
            //this._grid.Location = new System.Drawing.Point(0, 52);
            //this._grid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            //this._grid.Name = "_grid";
            //this._grid.Size = new System.Drawing.Size(868, 689);
            //this._grid.TabIndex = 6;
            //this._grid.TabStop = false;
            // 
            // _screenTop
            // 
            this._screenTop.BackColor = System.Drawing.Color.Transparent;
            this._screenTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this._screenTop.Location = new System.Drawing.Point(0, 25);
            this._screenTop.Name = "_screenTop";
            this._screenTop.Size = new System.Drawing.Size(868, 27);
            this._screenTop.TabIndex = 5;
            // 
            // _myToolBar
            // 
            this._myToolBar.BackgroundImage = global::SMLERPIC.Properties.Resources.bt03;
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(868, 25);
            this._myToolBar.TabIndex = 4;
            this._myToolBar.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLERPIC.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "บันทึกข้อมูล (F12)";
            this._saveButton.Size = new System.Drawing.Size(112, 22);
            this._saveButton.Text = "บันทึกข้อมูล (F12)";
            // 
            // _icSpecificSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageMain);
            this.Name = "_icSpecificSearch";
            this.Size = new System.Drawing.Size(953, 762);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myManageData _myManageMain;
        private MyLib._myPanel _myPanel1;
        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib.ToolStripMyButton _saveButton;
        //private _gridICSpecialSearchWord _grid;
        private _icSpecialSearchWordControl _screenTop;
    }
}
