namespace MyLib._databaseManage
{
    partial class _menupermissions_group
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
            this._splitContainer = new System.Windows.Forms.SplitContainer();
            this._myPanel1 = new MyLib._myPanel();
            this._myFlowLayoutPanel3 = new MyLib._myFlowLayoutPanel();
            this.ButtonExit = new MyLib._myButton();
            this._myGrid_group_list = new MyLib._myGrid();
            this._myPanel2 = new MyLib._myPanel();
            this._myGridpermissions = new _permissionGrid();
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._selectread = new MyLib.ToolStripMyButton();
            this._selectAdd = new MyLib.ToolStripMyButton();
            this._selectdelete = new MyLib.ToolStripMyButton();
            this._selectEdit = new MyLib.ToolStripMyButton();
            this._selectPrint = new System.Windows.Forms.ToolStripButton();
            this._myScreen1 = new MyLib._myScreen();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this.ButtonSave = new MyLib._myButton();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._myFlowLayoutPanel3.SuspendLayout();
            this._myPanel2.SuspendLayout();
            this._toolStrip.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _splitContainer
            // 
            this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer.Location = new System.Drawing.Point(0, 0);
            this._splitContainer.Name = "_splitContainer";
            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.Controls.Add(this._myPanel1);
            this._splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            // 
            // _splitContainer.Panel2
            // 
            this._splitContainer.Panel2.Controls.Add(this._myPanel2);
            this._splitContainer.Size = new System.Drawing.Size(789, 495);
            this._splitContainer.SplitterDistance = 303;
            this._splitContainer.SplitterWidth = 5;
            this._splitContainer.TabIndex = 1;
            this._splitContainer.Text = "splitContainer1";
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel3);
            this._myPanel1.Controls.Add(this._myGrid_group_list);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(6, 5);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(291, 485);
            this._myPanel1.TabIndex = 1;
            // 
            // _myFlowLayoutPanel3
            // 
            this._myFlowLayoutPanel3.AutoSize = true;
            this._myFlowLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel3.Controls.Add(this.ButtonExit);
            this._myFlowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel3.Location = new System.Drawing.Point(0, 457);
            this._myFlowLayoutPanel3.Name = "_myFlowLayoutPanel3";
            this._myFlowLayoutPanel3.Size = new System.Drawing.Size(291, 28);
            this._myFlowLayoutPanel3.TabIndex = 1;
            // 
            // ButtonExit
            // 
            this.ButtonExit.AutoSize = true;
            this.ButtonExit.BackColor = System.Drawing.Color.Transparent;
            this.ButtonExit.ButtonText = "ปิดหน้าจอ";
            this.ButtonExit.Location = new System.Drawing.Point(203, 0);
            this.ButtonExit.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.ButtonExit.myImage = global::MyLib.Resource16x16.error;
            this.ButtonExit.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ButtonExit.myUseVisualStyleBackColor = false;
            this.ButtonExit.Name = "ButtonExit";
            this.ButtonExit.Padding = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.ButtonExit.ResourceName = "screen_close";
            this.ButtonExit.Size = new System.Drawing.Size(87, 24);
            this.ButtonExit.TabIndex = 0;
            this.ButtonExit.Text = "ปิด";
            this.ButtonExit.UseVisualStyleBackColor = false;
            this.ButtonExit.Click += new System.EventHandler(this.ButtonExit_Click);
            // 
            // _myGrid_group_list
            // 
            this._myGrid_group_list._extraWordShow = true;
            this._myGrid_group_list._selectRow = -1;
            this._myGrid_group_list.BackColor = System.Drawing.SystemColors.Window;
            this._myGrid_group_list.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._myGrid_group_list.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._myGrid_group_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myGrid_group_list.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myGrid_group_list.IsEdit = false;
            this._myGrid_group_list.Location = new System.Drawing.Point(0, 0);
            this._myGrid_group_list.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._myGrid_group_list.Name = "_myGrid_group_list";
            this._myGrid_group_list.Size = new System.Drawing.Size(291, 485);
            this._myGrid_group_list.TabIndex = 0;
            this._myGrid_group_list.TabStop = false;
            // 
            // _myPanel2
            // 
            this._myPanel2._switchTabAuto = false;
            this._myPanel2.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Controls.Add(this._myGridpermissions);
            this._myPanel2.Controls.Add(this._toolStrip);
            this._myPanel2.Controls.Add(this._myScreen1);
            this._myPanel2.Controls.Add(this._myFlowLayoutPanel1);
            this._myPanel2.CornerPicture = null;
            this._myPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel2.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Location = new System.Drawing.Point(0, 0);
            this._myPanel2.Name = "_myPanel2";
            this._myPanel2.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this._myPanel2.Size = new System.Drawing.Size(481, 495);
            this._myPanel2.TabIndex = 2;
            // 
            // _myGridpermissions
            // 
            this._myGridpermissions._extraWordShow = true;
            this._myGridpermissions._selectRow = -1;
            this._myGridpermissions.BackColor = System.Drawing.SystemColors.Window;
            this._myGridpermissions.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._myGridpermissions.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._myGridpermissions.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myGridpermissions.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myGridpermissions.Location = new System.Drawing.Point(6, 30);
            this._myGridpermissions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._myGridpermissions.Name = "_myGridpermissions";
            this._myGridpermissions.Size = new System.Drawing.Size(469, 432);
            this._myGridpermissions.TabIndex = 3;
            this._myGridpermissions.TabStop = false;
            // 
            // _toolStrip
            // 
            this._toolStrip.BackColor = System.Drawing.Color.WhiteSmoke;
            this._toolStrip.BackgroundImage = global::MyLib.Properties.Resources.bt03;
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._selectread,
            this._selectAdd,
            this._selectdelete,
            this._selectEdit,
            this._selectPrint});
            this._toolStrip.Location = new System.Drawing.Point(6, 5);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(469, 25);
            this._toolStrip.TabIndex = 2;
            this._toolStrip.Text = "toolStrip1";
            // 
            // _selectread
            // 
            this._selectread.Image = global::MyLib.Properties.Resources.preferences;
            this._selectread.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectread.Name = "_selectread";
            this._selectread.Padding = new System.Windows.Forms.Padding(1);
            this._selectread.ResourceName = "select_read";
            this._selectread.Size = new System.Drawing.Size(103, 22);
            this._selectread.Text = "ให้เข้าใช้งานได้";
            this._selectread.Click += new System.EventHandler(this._selectread_Click);
            // 
            // _selectAdd
            // 
            this._selectAdd.Image = global::MyLib.Resource16x16.preferences;
            this._selectAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectAdd.Name = "_selectAdd";
            this._selectAdd.Padding = new System.Windows.Forms.Padding(1);
            this._selectAdd.ResourceName = "select_add";
            this._selectAdd.Size = new System.Drawing.Size(101, 22);
            this._selectAdd.Tag = "";
            this._selectAdd.Text = "ให้เพิ่มข้อมูลได้";
            this._selectAdd.Click += new System.EventHandler(this._selectAdd_Click);
            // 
            // _selectdelete
            // 
            this._selectdelete.Image = global::MyLib.Properties.Resources.preferences;
            this._selectdelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectdelete.Name = "_selectdelete";
            this._selectdelete.Padding = new System.Windows.Forms.Padding(1);
            this._selectdelete.ResourceName = "select_delete";
            this._selectdelete.Size = new System.Drawing.Size(94, 22);
            this._selectdelete.Text = "ให้ลบข้อมูลได้";
            this._selectdelete.Click += new System.EventHandler(this._selectdelete_Click);
            // 
            // _selectEdit
            // 
            this._selectEdit.Image = global::MyLib.Properties.Resources.preferences;
            this._selectEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectEdit.Name = "_selectEdit";
            this._selectEdit.Padding = new System.Windows.Forms.Padding(1);
            this._selectEdit.ResourceName = "select_change";
            this._selectEdit.Size = new System.Drawing.Size(109, 22);
            this._selectEdit.Text = "ให้แก้ไขข้อมูลได้";
            this._selectEdit.Click += new System.EventHandler(this._selectEdit_Click);
            // 
            // _selectPrint
            // 
            this._selectPrint.Image = global::MyLib.Properties.Resources.printer;
            this._selectPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectPrint.Name = "_selectPrint";
            this._selectPrint.Size = new System.Drawing.Size(50, 22);
            this._selectPrint.Text = "พิมพ์";
            this._selectPrint.Click += new System.EventHandler(this._selectPrint_Click);
            // 
            // _myScreen1
            // 
            this._myScreen1._isChange = false;
            this._myScreen1.AutoSize = true;
            this._myScreen1.BackColor = System.Drawing.Color.Transparent;
            this._myScreen1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myScreen1.Location = new System.Drawing.Point(6, 5);
            this._myScreen1.Name = "_myScreen1";
            this._myScreen1.Size = new System.Drawing.Size(469, 0);
            this._myScreen1.TabIndex = 1;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.AutoSize = true;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this.ButtonSave);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(6, 462);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(469, 28);
            this._myFlowLayoutPanel1.TabIndex = 0;
            // 
            // ButtonSave
            // 
            this.ButtonSave.AutoSize = true;
            this.ButtonSave.BackColor = System.Drawing.Color.Transparent;
            this.ButtonSave.ButtonText = "บันทึก";
            this.ButtonSave.Location = new System.Drawing.Point(401, 0);
            this.ButtonSave.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.ButtonSave.myImage = global::MyLib.Resource16x16.disk_blue;
            this.ButtonSave.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ButtonSave.myUseVisualStyleBackColor = false;
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Padding = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.ButtonSave.ResourceName = "save";
            this.ButtonSave.Size = new System.Drawing.Size(67, 24);
            this.ButtonSave.TabIndex = 1;
            this.ButtonSave.Text = "บันทึก";
            this.ButtonSave.UseVisualStyleBackColor = false;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // _menupermissions_group
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._splitContainer);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_menupermissions_group";
            this.Size = new System.Drawing.Size(789, 495);
            this._splitContainer.Panel1.ResumeLayout(false);
            this._splitContainer.Panel2.ResumeLayout(false);
            this._splitContainer.ResumeLayout(false);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myFlowLayoutPanel3.ResumeLayout(false);
            this._myFlowLayoutPanel3.PerformLayout();
            this._myPanel2.ResumeLayout(false);
            this._myPanel2.PerformLayout();
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private _myScreen _myScreen1;
        private _myGrid _myGrid_group_list;
        private System.Windows.Forms.SplitContainer _splitContainer;
        private _myFlowLayoutPanel _myFlowLayoutPanel3;
        private _myPanel _myPanel2;
        private _myFlowLayoutPanel _myFlowLayoutPanel1;
        private _myButton ButtonSave;
        private System.Windows.Forms.ToolStrip _toolStrip;
        private ToolStripMyButton _selectread;
        private ToolStripMyButton _selectAdd;
        private ToolStripMyButton _selectEdit;
        private ToolStripMyButton _selectdelete;
        private _myPanel _myPanel1;
        private _permissionGrid _myGridpermissions;
        public _myButton ButtonExit;
        private System.Windows.Forms.ToolStripButton _selectPrint;

    }
}
