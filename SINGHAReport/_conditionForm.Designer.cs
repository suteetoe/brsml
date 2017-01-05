namespace SINGHAReport
{
    partial class _conditionForm
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
            this._myPanel1 = new MyLib._myPanel();
            this._grouper1 = new MyLib._grouper();
            this._myTabControl1 = new MyLib._myTabControl();
            this.branch = new System.Windows.Forms.TabPage();
            this._selectBranchPanel = new System.Windows.Forms.Panel();
            this._gridCondition = new MyLib._myGrid();
            this._conditionToolbar = new System.Windows.Forms.ToolStrip();
            this._selectAllToolstrip = new MyLib.ToolStripMyButton();
            this._selectNoneToolstrip = new MyLib.ToolStripMyButton();
            this._useBranchPanel = new System.Windows.Forms.Panel();
            this._useBranchCheckbox = new System.Windows.Forms.CheckBox();
            this._extendPanel = new MyLib._myFlowLayoutPanel();
            this._selectWarehouseButton = new MyLib.VistaButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this._screen = new SINGHAReport._conditionScreenTop();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this.vistaButton1 = new MyLib.VistaButton();
            this.vistaButton2 = new MyLib.VistaButton();
            this._myPanel2 = new MyLib._myPanel();
            this._label = new MyLib._myShadowLabel(this.components);
            this._myPanel1.SuspendLayout();
            this._grouper1.SuspendLayout();
            this._myTabControl1.SuspendLayout();
            this.branch.SuspendLayout();
            this._selectBranchPanel.SuspendLayout();
            this._conditionToolbar.SuspendLayout();
            this._useBranchPanel.SuspendLayout();
            this._extendPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this._myPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._grouper1);
            this._myPanel1.Controls.Add(this._extendPanel);
            this._myPanel1.Controls.Add(this.panel1);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel1);
            this._myPanel1.Controls.Add(this._myPanel2);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(723, 567);
            this._myPanel1.TabIndex = 1;
            // 
            // _grouper1
            // 
            this._grouper1.AutoSize = true;
            this._grouper1.BackgroundColor = System.Drawing.Color.White;
            this._grouper1.BackgroundGradientColor = System.Drawing.Color.PaleTurquoise;
            this._grouper1.BackgroundGradientMode = MyLib._grouper.GroupBoxGradientMode.ForwardDiagonal;
            this._grouper1.BorderColor = System.Drawing.Color.Black;
            this._grouper1.BorderThickness = 1F;
            this._grouper1.Controls.Add(this._myTabControl1);
            this._grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this._grouper1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grouper1.GroupImage = null;
            this._grouper1.GroupTitle = "";
            this._grouper1.Location = new System.Drawing.Point(0, 83);
            this._grouper1.Name = "_grouper1";
            this._grouper1.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this._grouper1.PaintGroupBox = false;
            this._grouper1.RoundCorners = 3;
            this._grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this._grouper1.ShadowControl = false;
            this._grouper1.ShadowThickness = 3;
            this._grouper1.Size = new System.Drawing.Size(723, 452);
            this._grouper1.TabIndex = 15;
            // 
            // _myTabControl1
            // 
            this._myTabControl1.Controls.Add(this.branch);
            this._myTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myTabControl1.Font = new System.Drawing.Font("Tahoma", 9F);
            this._myTabControl1.Location = new System.Drawing.Point(6, 5);
            this._myTabControl1.Multiline = true;
            this._myTabControl1.Name = "_myTabControl1";
            this._myTabControl1.SelectedIndex = 0;
            this._myTabControl1.Size = new System.Drawing.Size(711, 442);
            this._myTabControl1.TabIndex = 0;
            this._myTabControl1.TableName = "resource_report";
            // 
            // branch
            // 
            this.branch.Controls.Add(this._selectBranchPanel);
            this.branch.Controls.Add(this._useBranchPanel);
            this.branch.Location = new System.Drawing.Point(4, 23);
            this.branch.Name = "branch";
            this.branch.Padding = new System.Windows.Forms.Padding(3);
            this.branch.Size = new System.Drawing.Size(703, 415);
            this.branch.TabIndex = 0;
            this.branch.Text = "สาขา";
            this.branch.UseVisualStyleBackColor = true;
            // 
            // _selectBranchPanel
            // 
            this._selectBranchPanel.Controls.Add(this._gridCondition);
            this._selectBranchPanel.Controls.Add(this._conditionToolbar);
            this._selectBranchPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._selectBranchPanel.Location = new System.Drawing.Point(3, 29);
            this._selectBranchPanel.Name = "_selectBranchPanel";
            this._selectBranchPanel.Size = new System.Drawing.Size(697, 383);
            this._selectBranchPanel.TabIndex = 3;
            // 
            // _gridCondition
            // 
            this._gridCondition._extraWordShow = true;
            this._gridCondition._selectRow = -1;
            this._gridCondition.BackColor = System.Drawing.SystemColors.Window;
            this._gridCondition.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridCondition.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridCondition.Font = new System.Drawing.Font("Tahoma", 9F);
            this._gridCondition.Location = new System.Drawing.Point(0, 25);
            this._gridCondition.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridCondition.Name = "_gridCondition";
            this._gridCondition.Size = new System.Drawing.Size(697, 358);
            this._gridCondition.TabIndex = 0;
            this._gridCondition.TabStop = false;
            // 
            // _conditionToolbar
            // 
            this._conditionToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._selectAllToolstrip,
            this._selectNoneToolstrip});
            this._conditionToolbar.Location = new System.Drawing.Point(0, 0);
            this._conditionToolbar.Name = "_conditionToolbar";
            this._conditionToolbar.Size = new System.Drawing.Size(697, 25);
            this._conditionToolbar.TabIndex = 1;
            this._conditionToolbar.Text = "toolStrip1";
            // 
            // _selectAllToolstrip
            // 
            this._selectAllToolstrip.Image = global::SINGHAReport.Properties.Resources.check2;
            this._selectAllToolstrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectAllToolstrip.Name = "_selectAllToolstrip";
            this._selectAllToolstrip.Padding = new System.Windows.Forms.Padding(1);
            this._selectAllToolstrip.ResourceName = "เลือกทั้งหมด";
            this._selectAllToolstrip.Size = new System.Drawing.Size(84, 22);
            this._selectAllToolstrip.Text = "เลือกทั้งหมด";
            // 
            // _selectNoneToolstrip
            // 
            this._selectNoneToolstrip.Image = global::SINGHAReport.Properties.Resources.delete2;
            this._selectNoneToolstrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectNoneToolstrip.Name = "_selectNoneToolstrip";
            this._selectNoneToolstrip.Padding = new System.Windows.Forms.Padding(1);
            this._selectNoneToolstrip.ResourceName = "ไม่เลือกทั้งหมด";
            this._selectNoneToolstrip.Size = new System.Drawing.Size(96, 22);
            this._selectNoneToolstrip.Text = "ไม่เลือกทั้งหมด";
            // 
            // _useBranchPanel
            // 
            this._useBranchPanel.Controls.Add(this._useBranchCheckbox);
            this._useBranchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._useBranchPanel.Location = new System.Drawing.Point(3, 3);
            this._useBranchPanel.Name = "_useBranchPanel";
            this._useBranchPanel.Size = new System.Drawing.Size(697, 26);
            this._useBranchPanel.TabIndex = 2;
            // 
            // _useBranchCheckbox
            // 
            this._useBranchCheckbox.AutoSize = true;
            this._useBranchCheckbox.Location = new System.Drawing.Point(45, 5);
            this._useBranchCheckbox.Name = "_useBranchCheckbox";
            this._useBranchCheckbox.Size = new System.Drawing.Size(93, 18);
            this._useBranchCheckbox.TabIndex = 0;
            this._useBranchCheckbox.Text = "แบ่งตามสาขา";
            this._useBranchCheckbox.UseVisualStyleBackColor = true;
            // 
            // _extendPanel
            // 
            this._extendPanel.BackColor = System.Drawing.Color.Transparent;
            this._extendPanel.Controls.Add(this._selectWarehouseButton);
            this._extendPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._extendPanel.Location = new System.Drawing.Point(0, 51);
            this._extendPanel.Name = "_extendPanel";
            this._extendPanel.Size = new System.Drawing.Size(723, 32);
            this._extendPanel.TabIndex = 16;
            this._extendPanel.Visible = false;
            // 
            // _selectWarehouseButton
            // 
            this._selectWarehouseButton._drawNewMethod = false;
            this._selectWarehouseButton.BackColor = System.Drawing.Color.Transparent;
            this._selectWarehouseButton.ButtonText = "เลือกคลัง/ที่เก็บ";
            this._selectWarehouseButton.Location = new System.Drawing.Point(3, 3);
            this._selectWarehouseButton.Name = "_selectWarehouseButton";
            this._selectWarehouseButton.Size = new System.Drawing.Size(113, 26);
            this._selectWarehouseButton.TabIndex = 1;
            this._selectWarehouseButton.Text = "vistaButton4";
            this._selectWarehouseButton.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this._screen);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(723, 10);
            this.panel1.TabIndex = 14;
            // 
            // _screen
            // 
            this._screen._isChange = false;
            this._screen.AutoSize = true;
            this._screen.BackColor = System.Drawing.Color.Transparent;
            this._screen.Dock = System.Windows.Forms.DockStyle.Fill;
            this._screen.Location = new System.Drawing.Point(0, 0);
            this._screen.Name = "_screen";
            this._screen.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this._screen.Size = new System.Drawing.Size(723, 10);
            this._screen.TabIndex = 15;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this.vistaButton1);
            this._myFlowLayoutPanel1.Controls.Add(this.vistaButton2);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 535);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(723, 32);
            this._myFlowLayoutPanel1.TabIndex = 0;
            // 
            // vistaButton1
            // 
            this.vistaButton1._drawNewMethod = false;
            this.vistaButton1.BackColor = System.Drawing.Color.Transparent;
            this.vistaButton1.ButtonText = "Exit";
            this.vistaButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.vistaButton1.Location = new System.Drawing.Point(607, 3);
            this.vistaButton1.myImage = global::SINGHAReport.Properties.Resources.error;
            this.vistaButton1.Name = "vistaButton1";
            this.vistaButton1.Size = new System.Drawing.Size(113, 26);
            this.vistaButton1.TabIndex = 0;
            this.vistaButton1.Text = "vistaButton1";
            this.vistaButton1.UseVisualStyleBackColor = false;
            // 
            // vistaButton2
            // 
            this.vistaButton2._drawNewMethod = false;
            this.vistaButton2.BackColor = System.Drawing.Color.Transparent;
            this.vistaButton2.ButtonText = "Process";
            this.vistaButton2.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.vistaButton2.Location = new System.Drawing.Point(488, 3);
            this.vistaButton2.myImage = global::SINGHAReport.Properties.Resources.flash;
            this.vistaButton2.Name = "vistaButton2";
            this.vistaButton2.Size = new System.Drawing.Size(113, 26);
            this.vistaButton2.TabIndex = 1;
            this.vistaButton2.Text = "vistaButton2";
            this.vistaButton2.UseVisualStyleBackColor = false;
            // 
            // _myPanel2
            // 
            this._myPanel2._switchTabAuto = false;
            this._myPanel2.AutoSize = true;
            this._myPanel2.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myPanel2.Controls.Add(this._label);
            this._myPanel2.CornerPicture = null;
            this._myPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this._myPanel2.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Location = new System.Drawing.Point(0, 0);
            this._myPanel2.Name = "_myPanel2";
            this._myPanel2.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this._myPanel2.Size = new System.Drawing.Size(723, 41);
            this._myPanel2.TabIndex = 13;
            // 
            // _label
            // 
            this._label.Angle = 0F;
            this._label.AutoSize = true;
            this._label.BackColor = System.Drawing.Color.Transparent;
            this._label.DrawGradient = false;
            this._label.EndColor = System.Drawing.Color.LightSkyBlue;
            this._label.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this._label.Location = new System.Drawing.Point(6, 5);
            this._label.Name = "_label";
            this._label.ShadowColor = System.Drawing.Color.Gray;
            this._label.Size = new System.Drawing.Size(77, 29);
            this._label.StartColor = System.Drawing.Color.White;
            this._label.TabIndex = 1;
            this._label.Text = "Label";
            this._label.XOffset = 1F;
            this._label.YOffset = 1F;
            // 
            // _conditionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 567);
            this.ControlBox = false;
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_conditionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "เงื่อนไขรายงาน";
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._grouper1.ResumeLayout(false);
            this._myTabControl1.ResumeLayout(false);
            this.branch.ResumeLayout(false);
            this._selectBranchPanel.ResumeLayout(false);
            this._selectBranchPanel.PerformLayout();
            this._conditionToolbar.ResumeLayout(false);
            this._conditionToolbar.PerformLayout();
            this._useBranchPanel.ResumeLayout(false);
            this._useBranchPanel.PerformLayout();
            this._extendPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myPanel2.ResumeLayout(false);
            this._myPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib.VistaButton vistaButton1;
        private MyLib.VistaButton vistaButton2;
        private MyLib._myPanel _myPanel1;
        private MyLib._myPanel _myPanel2;
        public MyLib._myShadowLabel _label;
        private System.Windows.Forms.Panel panel1;
        private MyLib._grouper _grouper1;
        private MyLib._myTabControl _myTabControl1;
        private System.Windows.Forms.TabPage branch;
        private MyLib.ToolStripMyButton _selectAllToolstrip;
        private MyLib.ToolStripMyButton _selectNoneToolstrip;
        private System.Windows.Forms.Panel _selectBranchPanel;
        public _conditionScreenTop _screen;
        public System.Windows.Forms.CheckBox _useBranchCheckbox;
        public MyLib._myGrid _gridCondition;
        public System.Windows.Forms.Panel _useBranchPanel;
        public System.Windows.Forms.ToolStrip _conditionToolbar;
        private MyLib._myFlowLayoutPanel _extendPanel;
        private MyLib.VistaButton _selectWarehouseButton;
    }
}