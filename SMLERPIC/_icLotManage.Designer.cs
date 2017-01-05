namespace SMLERPIC
{
    partial class _icLotManage
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
            this.panel1 = new MyLib._myPanel();
            this._myToolStrip1 = new MyLib._myToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._lotGrid = new MyLib._myGrid();
            this._myManageData = new MyLib._myManageData();
            this.panel1.SuspendLayout();
            this._myToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._lotGrid);
            this.panel1.Controls.Add(this._myToolStrip1);
            this.panel1.Location = new System.Drawing.Point(289, 121);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(461, 560);
            this.panel1.TabIndex = 0;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // _myToolStrip1
            // 
            this._myToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._closeButton});
            this._myToolStrip1.Location = new System.Drawing.Point(0, 0);
            this._myToolStrip1.Name = "_myToolStrip1";
            this._myToolStrip1.Size = new System.Drawing.Size(461, 25);
            this._myToolStrip1.TabIndex = 0;
            this._myToolStrip1.Text = "_myToolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLERPIC.Properties.Resources.flash;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "บันทึก (F12)";
            this._saveButton.Size = new System.Drawing.Size(53, 22);
            this._saveButton.Text = "Save";
            this._saveButton.ToolTipText = "บันทึก (F12)";
            this._saveButton.Click += _saveButton_Click;
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
            this._closeButton.Click += _closeButton_Click;
            // 
            // _lotGrid
            // 
            this._lotGrid._extraWordShow = true;
            this._lotGrid._selectRow = -1;
            this._lotGrid.BackColor = System.Drawing.SystemColors.Window;
            this._lotGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._lotGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._lotGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lotGrid.Font = new System.Drawing.Font("Tahoma", 9F);
            this._lotGrid.Location = new System.Drawing.Point(0, 25);
            this._lotGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._lotGrid.Name = "_lotGrid";
            this._lotGrid.Size = new System.Drawing.Size(461, 535);
            this._lotGrid.TabIndex = 1;
            this._lotGrid.TabStop = false;
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
            this._myManageData.Size = new System.Drawing.Size(908, 779);
            this._myManageData.TabIndex = 1;
            this._myManageData.TabStop = false;

            this._myManageData._form2.Controls.Add(this.panel1);
            // 
            // _icLotManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageData);
            //this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_icLotManage";
            this.Size = new System.Drawing.Size(908, 779);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this._myToolStrip1.ResumeLayout(false);
            this._myToolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myPanel panel1;
        private MyLib._myToolStrip _myToolStrip1;
        private MyLib.ToolStripMyButton _saveButton;
        private MyLib.ToolStripMyButton _closeButton;
        private MyLib._myGrid _lotGrid;
        private MyLib._myManageData _myManageData;
    }
}
