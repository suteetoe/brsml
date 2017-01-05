namespace SMLERPGLControl
{
    partial class _glDetailExtra
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_glDetailExtra));
            this._myTabControl1 = new MyLib._myTabControl();
            this.side = new System.Windows.Forms.TabPage();
            this.department = new System.Windows.Forms.TabPage();
            this.project = new System.Windows.Forms.TabPage();
            this.allocate = new System.Windows.Forms.TabPage();
            this.job = new System.Windows.Forms.TabPage();
            this._myPanel1 = new MyLib._myPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._buttonConfirm = new MyLib.ToolStripMyButton();
            this._buttonClose = new MyLib.ToolStripMyButton();
            this._glDetailExtraSideGridData = new SMLERPGLControl._glDetailExtraSideGrid();
            this._glDetailExtraDepartmentGridData = new SMLERPGLControl._glDetailExtraDepartmentGrid();
            this._glDetailExtraProjectGridData = new SMLERPGLControl._glDetailExtraProjectGrid();
            this._glDetailExtraAllocateGridData = new SMLERPGLControl._glDetailExtraAllocateGrid();
            this._glDetailExtraJobGridData = new SMLERPGLControl._glDetailExtraJobGrid();
            this._glDetailExtraTopScreen = new SMLERPGLControl._glDetailExtraTopScreen();
            this._myTabControl1.SuspendLayout();
            this.side.SuspendLayout();
            this.department.SuspendLayout();
            this.project.SuspendLayout();
            this.allocate.SuspendLayout();
            this.job.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myTabControl1
            // 
            this._myTabControl1.Controls.Add(this.side);
            this._myTabControl1.Controls.Add(this.department);
            this._myTabControl1.Controls.Add(this.project);
            this._myTabControl1.Controls.Add(this.allocate);
            this._myTabControl1.Controls.Add(this.job);
            this._myTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myTabControl1.Font = new System.Drawing.Font("Tahoma", 9F);
            this._myTabControl1.Location = new System.Drawing.Point(0, 99);
            this._myTabControl1.Multiline = true;
            this._myTabControl1.Name = "_myTabControl1";
            this._myTabControl1.SelectedIndex = 0;
            this._myTabControl1.ShowTabNumber = true;
            this._myTabControl1.Size = new System.Drawing.Size(748, 319);
            this._myTabControl1.TabIndex = 0;
            this._myTabControl1.TableName = "gl_resource";
            // 
            // side
            // 
            this.side.Controls.Add(this._glDetailExtraSideGridData);
            this.side.Location = new System.Drawing.Point(4, 23);
            this.side.Name = "side";
            this.side.Padding = new System.Windows.Forms.Padding(3);
            this.side.Size = new System.Drawing.Size(740, 290);
            this.side.TabIndex = 0;
            this.side.Text = "1.gl_resource.side";
            this.side.UseVisualStyleBackColor = true;
            // 
            // department
            // 
            this.department.Controls.Add(this._glDetailExtraDepartmentGridData);
            this.department.Location = new System.Drawing.Point(4, 23);
            this.department.Name = "department";
            this.department.Padding = new System.Windows.Forms.Padding(3);
            this.department.Size = new System.Drawing.Size(740, 290);
            this.department.TabIndex = 1;
            this.department.Text = "2.gl_resource.department";
            this.department.UseVisualStyleBackColor = true;
            // 
            // project
            // 
            this.project.Controls.Add(this._glDetailExtraProjectGridData);
            this.project.Location = new System.Drawing.Point(4, 23);
            this.project.Name = "project";
            this.project.Padding = new System.Windows.Forms.Padding(3);
            this.project.Size = new System.Drawing.Size(740, 290);
            this.project.TabIndex = 2;
            this.project.Text = "3.gl_resource.project";
            this.project.UseVisualStyleBackColor = true;
            // 
            // allocate
            // 
            this.allocate.Controls.Add(this._glDetailExtraAllocateGridData);
            this.allocate.Location = new System.Drawing.Point(4, 23);
            this.allocate.Name = "allocate";
            this.allocate.Padding = new System.Windows.Forms.Padding(3);
            this.allocate.Size = new System.Drawing.Size(740, 290);
            this.allocate.TabIndex = 3;
            this.allocate.Text = "4.gl_resource.allocate";
            this.allocate.UseVisualStyleBackColor = true;
            // 
            // job
            // 
            this.job.Controls.Add(this._glDetailExtraJobGridData);
            this.job.Location = new System.Drawing.Point(4, 23);
            this.job.Name = "job";
            this.job.Padding = new System.Windows.Forms.Padding(3);
            this.job.Size = new System.Drawing.Size(740, 292);
            this.job.TabIndex = 4;
            this.job.Text = "5.gl_resource.job";
            this.job.UseVisualStyleBackColor = true;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.AutoSize = true;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._glDetailExtraTopScreen);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this._myPanel1.ShowBackground = false;
            this._myPanel1.Size = new System.Drawing.Size(748, 74);
            this._myPanel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonConfirm,
            this._buttonClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(748, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _buttonConfirm
            // 
            this._buttonConfirm.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonConfirm.Image = global::SMLERPGLControl.Properties.Resources.flash;
            this._buttonConfirm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonConfirm.Name = "_buttonConfirm";
            this._buttonConfirm.Padding = new System.Windows.Forms.Padding(1);
            this._buttonConfirm.ResourceName = "";
            this._buttonConfirm.Size = new System.Drawing.Size(126, 22);
            this._buttonConfirm.Text = "ยืนยันและปิดหน้าจอ";
            // 
            // _buttonClose
            // 
            this._buttonClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonClose.Image = global::SMLERPGLControl.Properties.Resources.error;
            this._buttonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Padding = new System.Windows.Forms.Padding(1);
            this._buttonClose.ResourceName = "";
            this._buttonClose.Size = new System.Drawing.Size(130, 22);
            this._buttonClose.Text = "ยกเลิกและปิดหน้าจอ";
            this._buttonClose.Click += new System.EventHandler(this._buttonClose_Click);
            // 
            // _glDetailExtraSideGridData
            // 
            this._glDetailExtraSideGridData._extraWordShow = true;
            this._glDetailExtraSideGridData._selectRow = -1;
            this._glDetailExtraSideGridData.BackColor = System.Drawing.SystemColors.Window;
            this._glDetailExtraSideGridData.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._glDetailExtraSideGridData.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._glDetailExtraSideGridData.Dock = System.Windows.Forms.DockStyle.Fill;
            this._glDetailExtraSideGridData.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._glDetailExtraSideGridData.Location = new System.Drawing.Point(3, 3);
            this._glDetailExtraSideGridData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._glDetailExtraSideGridData.Name = "_glDetailExtraSideGridData";
            this._glDetailExtraSideGridData.ShowTotal = true;
            this._glDetailExtraSideGridData.Size = new System.Drawing.Size(734, 284);
            this._glDetailExtraSideGridData.TabIndex = 0;
            this._glDetailExtraSideGridData.TabStop = false;
            // 
            // _glDetailExtraDepartmentGridData
            // 
            this._glDetailExtraDepartmentGridData._extraWordShow = true;
            this._glDetailExtraDepartmentGridData._selectRow = -1;
            this._glDetailExtraDepartmentGridData.BackColor = System.Drawing.SystemColors.Window;
            this._glDetailExtraDepartmentGridData.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._glDetailExtraDepartmentGridData.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._glDetailExtraDepartmentGridData.Dock = System.Windows.Forms.DockStyle.Fill;
            this._glDetailExtraDepartmentGridData.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._glDetailExtraDepartmentGridData.Location = new System.Drawing.Point(3, 3);
            this._glDetailExtraDepartmentGridData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._glDetailExtraDepartmentGridData.Name = "_glDetailExtraDepartmentGridData";
            this._glDetailExtraDepartmentGridData.ShowTotal = true;
            this._glDetailExtraDepartmentGridData.Size = new System.Drawing.Size(734, 284);
            this._glDetailExtraDepartmentGridData.TabIndex = 1;
            this._glDetailExtraDepartmentGridData.TabStop = false;
            // 
            // _glDetailExtraProjectGridData
            // 
            this._glDetailExtraProjectGridData._extraWordShow = true;
            this._glDetailExtraProjectGridData._selectRow = -1;
            this._glDetailExtraProjectGridData.BackColor = System.Drawing.SystemColors.Window;
            this._glDetailExtraProjectGridData.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._glDetailExtraProjectGridData.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._glDetailExtraProjectGridData.Dock = System.Windows.Forms.DockStyle.Fill;
            this._glDetailExtraProjectGridData.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._glDetailExtraProjectGridData.Location = new System.Drawing.Point(3, 3);
            this._glDetailExtraProjectGridData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._glDetailExtraProjectGridData.Name = "_glDetailExtraProjectGridData";
            this._glDetailExtraProjectGridData.ShowTotal = true;
            this._glDetailExtraProjectGridData.Size = new System.Drawing.Size(734, 284);
            this._glDetailExtraProjectGridData.TabIndex = 0;
            this._glDetailExtraProjectGridData.TabStop = false;
            // 
            // _glDetailExtraAllocateGridData
            // 
            this._glDetailExtraAllocateGridData._extraWordShow = true;
            this._glDetailExtraAllocateGridData._selectRow = -1;
            this._glDetailExtraAllocateGridData.BackColor = System.Drawing.SystemColors.Window;
            this._glDetailExtraAllocateGridData.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._glDetailExtraAllocateGridData.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._glDetailExtraAllocateGridData.Dock = System.Windows.Forms.DockStyle.Fill;
            this._glDetailExtraAllocateGridData.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._glDetailExtraAllocateGridData.Location = new System.Drawing.Point(3, 3);
            this._glDetailExtraAllocateGridData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._glDetailExtraAllocateGridData.Name = "_glDetailExtraAllocateGridData";
            this._glDetailExtraAllocateGridData.ShowTotal = true;
            this._glDetailExtraAllocateGridData.Size = new System.Drawing.Size(734, 284);
            this._glDetailExtraAllocateGridData.TabIndex = 0;
            this._glDetailExtraAllocateGridData.TabStop = false;
            // 
            // _glDetailExtraJobGridData
            // 
            this._glDetailExtraJobGridData._extraWordShow = true;
            this._glDetailExtraJobGridData._selectRow = -1;
            this._glDetailExtraJobGridData.BackColor = System.Drawing.SystemColors.Window;
            this._glDetailExtraJobGridData.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._glDetailExtraJobGridData.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._glDetailExtraJobGridData.Dock = System.Windows.Forms.DockStyle.Fill;
            this._glDetailExtraJobGridData.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._glDetailExtraJobGridData.Location = new System.Drawing.Point(3, 3);
            this._glDetailExtraJobGridData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._glDetailExtraJobGridData.Name = "_glDetailExtraJobGridData";
            this._glDetailExtraJobGridData.ShowTotal = true;
            this._glDetailExtraJobGridData.Size = new System.Drawing.Size(734, 286);
            this._glDetailExtraJobGridData.TabIndex = 0;
            this._glDetailExtraJobGridData.TabStop = false;
            // 
            // _glDetailExtraTopScreen
            // 
            this._glDetailExtraTopScreen._isChange = false;
            this._glDetailExtraTopScreen.AutoSize = true;
            this._glDetailExtraTopScreen.BackColor = System.Drawing.Color.Transparent;
            this._glDetailExtraTopScreen.Dock = System.Windows.Forms.DockStyle.Top;
            this._glDetailExtraTopScreen.Enabled = false;
            this._glDetailExtraTopScreen.Font = new System.Drawing.Font("Tahoma", 9F);
            this._glDetailExtraTopScreen.Location = new System.Drawing.Point(5, 4);
            this._glDetailExtraTopScreen.Name = "_glDetailExtraTopScreen";
            this._glDetailExtraTopScreen.Size = new System.Drawing.Size(738, 66);
            this._glDetailExtraTopScreen.TabIndex = 1;
            // 
            // _glDetailExtra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(748, 418);
            this.Controls.Add(this._myTabControl1);
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "_glDetailExtra";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Extra";
            this.Load += new System.EventHandler(this._glDetailExtra_Load);
            this._myTabControl1.ResumeLayout(false);
            this.side.ResumeLayout(false);
            this.department.ResumeLayout(false);
            this.project.ResumeLayout(false);
            this.allocate.ResumeLayout(false);
            this.job.ResumeLayout(false);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myTabControl _myTabControl1;
        private System.Windows.Forms.TabPage side;
        private System.Windows.Forms.TabPage department;
        private System.Windows.Forms.TabPage project;
        private System.Windows.Forms.TabPage allocate;
        private System.Windows.Forms.TabPage job;
        private MyLib._myPanel _myPanel1;
        public _glDetailExtraSideGrid _glDetailExtraSideGridData;
        public _glDetailExtraDepartmentGrid _glDetailExtraDepartmentGridData;
        public _glDetailExtraJobGrid _glDetailExtraJobGridData;
        public _glDetailExtraProjectGrid _glDetailExtraProjectGridData;
        public _glDetailExtraAllocateGrid _glDetailExtraAllocateGridData;
        public _glDetailExtraTopScreen _glDetailExtraTopScreen;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private MyLib.ToolStripMyButton _buttonClose;
        public MyLib.ToolStripMyButton _buttonConfirm;
    }
}