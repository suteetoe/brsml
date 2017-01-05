namespace SMLERPGL
{
    partial class _checkData
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._buttonStart = new MyLib.ToolStripMyButton();
            this._buttonClose = new MyLib.ToolStripMyButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._all = new MyLib.ToolStripCheckedBox();
            this.toolStripLabel1 = new MyLib.ToolStripMyLabel();
            this._endDateTextBox = new System.Windows.Forms.ToolStripTextBox();
            this._have_book_only = new MyLib.ToolStripCheckedBox();
            this._resultGrid = new MyLib._myGrid();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::SMLERPGL.Resource16x16.bt03;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonStart,
            this._buttonClose,
            this.toolStripSeparator1,
            this._all,
            this.toolStripLabel1,
            this._endDateTextBox,
            this._have_book_only});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(953, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _buttonStart
            // 
            this._buttonStart.Image = global::SMLERPGL.Resource16x16.check;
            this._buttonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonStart.Name = "_buttonStart";
            this._buttonStart.Padding = new System.Windows.Forms.Padding(1);
            this._buttonStart.ResourceName = "เริ่มตรวจสอบ";
            this._buttonStart.Size = new System.Drawing.Size(86, 22);
            this._buttonStart.Text = "เริ่มตรวจสอบ";
            this._buttonStart.Click += new System.EventHandler(this._buttonStart_Click);
            // 
            // _buttonClose
            // 
            this._buttonClose.Image = global::SMLERPGL.Resource16x16.error;
            this._buttonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Padding = new System.Windows.Forms.Padding(1);
            this._buttonClose.ResourceName = "ปิดหน้าจอ";
            this._buttonClose.Size = new System.Drawing.Size(74, 22);
            this._buttonClose.Text = "ปิดหน้าจอ";
            this._buttonClose.Click += new System.EventHandler(this._buttonClose_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _all
            // 
            this._all.BackColor = System.Drawing.Color.Transparent;
            // 
            // _all
            // 
            this._all.MyCheckBox.AccessibleName = "_all";
            this._all.MyCheckBox.BackColor = System.Drawing.Color.Transparent;
            this._all.MyCheckBox.Location = new System.Drawing.Point(166, 1);
            this._all.MyCheckBox.Name = "_all";
            this._all.MyCheckBox.Size = new System.Drawing.Size(59, 22);
            this._all.MyCheckBox.TabIndex = 1;
            this._all.MyCheckBox.Text = "ทั้งหมด";
            this._all.MyCheckBox.UseVisualStyleBackColor = false;
            this._all.Name = "_all";
            this._all.Size = new System.Drawing.Size(59, 22);
            this._all.Text = "ทั้งหมด";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripLabel1.ResourceName = "ตรวจสอบถึงวันที่ (yyyy-mm-dd)";
            this.toolStripLabel1.Size = new System.Drawing.Size(163, 22);
            this.toolStripLabel1.Text = "ตรวจสอบถึงวันที่ (yyyy-mm-dd)";
            // 
            // _endDateTextBox
            // 
            this._endDateTextBox.Name = "_endDateTextBox";
            this._endDateTextBox.Size = new System.Drawing.Size(200, 25);
            // 
            // _have_book_only
            // 
            this._have_book_only.BackColor = System.Drawing.Color.Transparent;
            // 
            // _have_book_only
            // 
            this._have_book_only.MyCheckBox.AccessibleName = "_have_book_only";
            this._have_book_only.MyCheckBox.BackColor = System.Drawing.Color.Transparent;
            this._have_book_only.MyCheckBox.Location = new System.Drawing.Point(590, 1);
            this._have_book_only.MyCheckBox.Name = "_have_book_only";
            this._have_book_only.MyCheckBox.Size = new System.Drawing.Size(230, 22);
            this._have_book_only.MyCheckBox.TabIndex = 0;
            this._have_book_only.MyCheckBox.Text = "เฉพาะประเภทเอกสารที่มีการกำหนดสมุดรายวัน";
            this._have_book_only.MyCheckBox.UseVisualStyleBackColor = false;
            this._have_book_only.Name = "_have_book_only";
            this._have_book_only.Size = new System.Drawing.Size(230, 22);
            this._have_book_only.Text = "เฉพาะประเภทเอกสารที่มีการกำหนดสมุดรายวัน";
            // 
            // _resultGrid
            // 
            this._resultGrid._extraWordShow = true;
            this._resultGrid._selectRow = -1;
            this._resultGrid.BackColor = System.Drawing.SystemColors.Window;
            this._resultGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._resultGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._resultGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._resultGrid.Location = new System.Drawing.Point(0, 25);
            this._resultGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._resultGrid.Name = "_resultGrid";
            this._resultGrid.Size = new System.Drawing.Size(953, 639);
            this._resultGrid.TabIndex = 1;
            this._resultGrid.TabStop = false;
            // 
            // _checkData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._resultGrid);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_checkData";
            this.Size = new System.Drawing.Size(953, 664);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private MyLib.ToolStripMyButton _buttonStart;
        private MyLib.ToolStripMyButton _buttonClose;
        private MyLib._myGrid _resultGrid;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private MyLib.ToolStripCheckedBox _all;
        private MyLib.ToolStripMyLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox _endDateTextBox;
        private MyLib.ToolStripCheckedBox _have_book_only;
    }
}
