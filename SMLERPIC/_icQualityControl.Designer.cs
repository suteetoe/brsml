namespace SMLERPIC
{
    partial class _icQualityControl
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
            this._myToolStrip1 = new MyLib._myToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._myScreen = new MyLib._myScreen();
            this._myGrid = new MyLib._myGrid();
            this._myPanel = new MyLib._myPanel();
            this._myManageData = new MyLib._myManageData();
            this._printButton = new MyLib.ToolStripMyButton();
            this._myToolStrip1.SuspendLayout();
            this._myPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myToolStrip1
            // 
            this._myToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._printButton,
            this._closeButton});
            this._myToolStrip1.Location = new System.Drawing.Point(0, 0);
            this._myToolStrip1.Name = "_myToolStrip1";
            this._myToolStrip1.Size = new System.Drawing.Size(678, 25);
            this._myToolStrip1.TabIndex = 0;
            this._myToolStrip1.Text = "_myToolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLERPIC.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "บันทึก (F12)";
            this._saveButton.Size = new System.Drawing.Size(87, 22);
            this._saveButton.Text = "บันทึก (F12)";
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLERPIC.Properties.Resources.error1;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "ปิดจอ";
            this._closeButton.Size = new System.Drawing.Size(55, 22);
            this._closeButton.Text = "ปิดจอ";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _printButton
            // 
            this._printButton.Image = global::SMLERPIC.Properties.Resources.printer1;
            this._printButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._printButton.Name = "_printButton";
            this._printButton.Padding = new System.Windows.Forms.Padding(1);
            this._printButton.ResourceName = "พิมพ์";
            this._printButton.Size = new System.Drawing.Size(87, 22);
            this._printButton.Text = "พิมพ์";
            this._printButton.Click += new System.EventHandler(this._printButton_Click);
            // 
            // _myScreen
            // 
            this._myScreen._isChange = false;
            this._myScreen.BackColor = System.Drawing.Color.Transparent;
            this._myScreen.Dock = System.Windows.Forms.DockStyle.Top;
            this._myScreen.Location = new System.Drawing.Point(0, 25);
            this._myScreen.Name = "_myScreen";
            this._myScreen.Size = new System.Drawing.Size(678, 50);
            this._myScreen.TabIndex = 1;
            // 
            // _myGrid
            // 
            this._myGrid._extraWordShow = true;
            this._myGrid._selectRow = -1;
            this._myGrid.BackColor = System.Drawing.SystemColors.Window;
            this._myGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._myGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._myGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myGrid.Font = new System.Drawing.Font("Tahoma", 9F);
            this._myGrid.Location = new System.Drawing.Point(0, 75);
            this._myGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._myGrid.Name = "_myGrid";
            this._myGrid.Size = new System.Drawing.Size(678, 466);
            this._myGrid.TabIndex = 2;
            this._myGrid.TabStop = false;
            // 
            // _myPanel
            // 
            this._myPanel._switchTabAuto = false;
            this._myPanel.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel.Controls.Add(this._myGrid);
            this._myPanel.Controls.Add(this._myScreen);
            this._myPanel.Controls.Add(this._myToolStrip1);
            this._myPanel.CornerPicture = null;
            this._myPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel.Location = new System.Drawing.Point(0, 0);
            this._myPanel.Name = "_myPanel";
            this._myPanel.Size = new System.Drawing.Size(678, 541);
            this._myPanel.TabIndex = 3;
            // 
            // _myManageData
            // 
            this._myManageData._mainMenuCode = "";
            this._myManageData._mainMenuId = "";
            this._myManageData.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageData.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageData.Location = new System.Drawing.Point(0, 0);
            this._myManageData.Name = "_myManageData";
            this._myManageData.Size = new System.Drawing.Size(678, 541);
            this._myManageData.TabIndex = 3;
            this._myManageData.TabStop = false;
            this._myManageData._form2.Controls.Add(this._myPanel);
            // 
            // _icQualityControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageData);
            //this.Controls.Add(this._myPanel);
            this.Name = "_icQualityControl";
            this.Size = new System.Drawing.Size(678, 541);
            this._myToolStrip1.ResumeLayout(false);
            this._myToolStrip1.PerformLayout();
            this._myPanel.ResumeLayout(false);
            this._myPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myToolStrip _myToolStrip1;
        private MyLib.ToolStripMyButton _closeButton;
        private MyLib._myScreen _myScreen;
        private MyLib._myGrid _myGrid;
        private MyLib.ToolStripMyButton _saveButton;
        private MyLib._myPanel _myPanel;
        private MyLib._myManageData _myManageData;
        private MyLib.ToolStripMyButton _printButton;
    }
}
