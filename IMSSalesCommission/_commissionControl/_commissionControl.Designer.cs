namespace IMSSalesCommission
{
    partial class _commissionControl
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
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._closeButtom = new MyLib.ToolStripMyButton();
            this._myPanel1 = new MyLib._myPanel();
            this._myTab = new MyLib._myTabControl();
            this.tab_detail = new System.Windows.Forms.TabPage();
            this._itemGridControl = new IMSSalesCommission._itemGridControl();
            this._screenTop = new IMSSalesCommission._screenTopControl();
            this._screenBottom = new IMSSalesCommission._screenBottomControl();
            this._myManageData = new MyLib._myManageData();
            this._myToolBar.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._myTab.SuspendLayout();
            this.tab_detail.SuspendLayout();
            this.SuspendLayout();
            // 
            // _toolStrip
            // 
            this._myToolBar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._closeButtom});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_toolStrip";
            this._myToolBar.Size = new System.Drawing.Size(618, 25);
            this._myToolBar.TabIndex = 1;
            this._myToolBar.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::IMSSalesCommission.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "บันทึก (F12)";
            this._saveButton.Size = new System.Drawing.Size(94, 22);
            this._saveButton.Text = "บันทึก (F12)";
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _closeButtom
            // 
            this._closeButtom.Image = global::IMSSalesCommission.Properties.Resources.error;
            this._closeButtom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButtom.Name = "_closeButtom";
            this._closeButtom.Padding = new System.Windows.Forms.Padding(1);
            this._closeButtom.ResourceName = "ปิดจอ";
            this._closeButtom.Size = new System.Drawing.Size(58, 22);
            this._closeButtom.Text = "ปิดจอ";
            this._closeButtom.Click += new System.EventHandler(this._closeButtom_Click);
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._myTab);
            this._myPanel1.Controls.Add(this._screenTop);
            this._myPanel1.Controls.Add(this._screenBottom);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(618, 590);
            this._myPanel1.TabIndex = 0;
            // 
            // _myTab
            // 
            this._myTab.Controls.Add(this.tab_detail);
            this._myTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myTab.Font = new System.Drawing.Font("Tahoma", 9F);
            this._myTab.Location = new System.Drawing.Point(0, 92);
            this._myTab.Multiline = true;
            this._myTab.Name = "_myTab";
            this._myTab.SelectedIndex = 0;
            this._myTab.Size = new System.Drawing.Size(618, 384);
            this._myTab.TabIndex = 0;
            this._myTab.TableName = "";
            // 
            // tab_detail
            // 
            this.tab_detail.Controls.Add(this._itemGridControl);
            this.tab_detail.Location = new System.Drawing.Point(4, 23);
            this.tab_detail.Name = "tab_detail";
            this.tab_detail.Size = new System.Drawing.Size(610, 357);
            this.tab_detail.TabIndex = 0;
            this.tab_detail.Text = "tab_detail";
            this.tab_detail.UseVisualStyleBackColor = true;
            // 
            // _itemGridControl
            // 
            this._itemGridControl._extraWordShow = true;
            this._itemGridControl._selectRow = -1;
            this._itemGridControl.BackColor = System.Drawing.SystemColors.Window;
            this._itemGridControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._itemGridControl.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._itemGridControl.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._itemGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemGridControl.Font = new System.Drawing.Font("Tahoma", 9F);
            this._itemGridControl.Location = new System.Drawing.Point(3, 3);
            this._itemGridControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._itemGridControl.Name = "_itemGridControl";
            this._itemGridControl.Size = new System.Drawing.Size(604, 351);
            this._itemGridControl.TabIndex = 0;
            this._itemGridControl.TabStop = false;
            this._itemGridControl.ShowTotal = true;
            // 
            // _screenTop
            // 
            this._screenTop._isChange = false;
            this._screenTop.BackColor = System.Drawing.Color.Transparent;
            this._screenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._screenTop.Location = new System.Drawing.Point(0, 0);
            this._screenTop.Name = "_screenTop";
            this._screenTop.Size = new System.Drawing.Size(618, 92);
            this._screenTop.TabIndex = 3;
            // 
            // _screenBottom
            // 
            this._screenBottom._isChange = false;
            this._screenBottom.BackColor = System.Drawing.Color.Transparent;
            this._screenBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._screenBottom.Location = new System.Drawing.Point(0, 476);
            this._screenBottom.Name = "_screenBottom";
            this._screenBottom.Size = new System.Drawing.Size(618, 114);
            this._screenBottom.TabIndex = 2;
            // 
            // _myManageData
            // 
            this._myManageData._mainMenuCode = "";
            this._myManageData._mainMenuId = "";
            this._myManageData.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageData.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageData.Location = new System.Drawing.Point(0, 25);
            this._myManageData.Name = "_myManageData";
            this._myManageData.Size = new System.Drawing.Size(618, 590);
            this._myManageData.TabIndex = 1;
            this._myManageData.TabStop = false;

            this._myManageData._form2.Controls.Add(this._myPanel1);
            this._myManageData._form2.Controls.Add(this._myToolBar);

            // 
            // _commissionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageData);
            //this.Controls.Add(this._myPanel1);
            //this.Controls.Add(this._myToolBar);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_commissionControl";
            this.Size = new System.Drawing.Size(618, 615);
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myTab.ResumeLayout(false);
            this.tab_detail.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private MyLib._myTabControl _myTab;
        private System.Windows.Forms.TabPage tab_detail;
        private System.Windows.Forms.ToolStrip _myToolBar;
        private _screenTopControl _screenTop;
        private _screenBottomControl _screenBottom;
        private MyLib.ToolStripMyButton _saveButton;
        private MyLib.ToolStripMyButton _closeButtom;
        private _itemGridControl _itemGridControl;
        private MyLib._myManageData _myManageData;

    }
}
