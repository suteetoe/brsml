namespace SMLERPReportTool
{
    partial class _conditionScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

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
            this._grid = new SMLERPReportTool._condition_ic_grid();
            this.panel1 = new System.Windows.Forms.Panel();
            this._screen = new SMLERPReportTool._condition_ic_screen();
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._grouper2 = new MyLib._grouper();
            this._extra = new SMLReport._whereUserControl();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._exitButton = new MyLib.VistaButton();
            this._processButton = new MyLib.VistaButton();
            this._myPanel2 = new MyLib._myPanel();
            this._label = new MyLib._myShadowLabel(this.components);
            this._myPanel1.SuspendLayout();
            this._grouper1.SuspendLayout();
            this.panel1.SuspendLayout();
            this._grouper2.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this._myPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._grouper1);
            this._myPanel1.Controls.Add(this._grouper2);
            this._myPanel1.Controls.Add(this.panel1);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel1);
            this._myPanel1.Controls.Add(this._myPanel2);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(895, 678);
            this._myPanel1.TabIndex = 0;
            // 
            // _grouper1
            // 
            this._grouper1.AutoSize = true;
            this._grouper1.BackgroundColor = System.Drawing.Color.White;
            this._grouper1.BackgroundGradientColor = System.Drawing.Color.PaleTurquoise;
            this._grouper1.BackgroundGradientMode = MyLib._grouper.GroupBoxGradientMode.ForwardDiagonal;
            this._grouper1.BorderColor = System.Drawing.Color.Black;
            this._grouper1.BorderThickness = 1F;
            this._grouper1.Controls.Add(this._grid);
            this._grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this._grouper1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grouper1.GroupImage = null;
            this._grouper1.GroupTitle = "";
            this._grouper1.Location = new System.Drawing.Point(0, 76);
            this._grouper1.Name = "_grouper1";
            this._grouper1.Padding = new System.Windows.Forms.Padding(5);
            this._grouper1.PaintGroupBox = false;
            this._grouper1.RoundCorners = 3;
            this._grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this._grouper1.ShadowControl = false;
            this._grouper1.ShadowThickness = 3;
            this._grouper1.Size = new System.Drawing.Size(895, 363);
            this._grouper1.TabIndex = 11;
            // 
            // _grid
            // 
            this._grid._extraWordShow = true;
            this._grid._selectRow = -1;
            this._grid.BackColor = System.Drawing.SystemColors.Window;
            this._grid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._grid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._grid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._grid.Location = new System.Drawing.Point(5, 5);
            this._grid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._grid.Name = "_grid";
            this._grid.Size = new System.Drawing.Size(885, 353);
            this._grid.TabIndex = 0;
            this._grid.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this._screen);
            this.panel1.Controls.Add(this._toolStrip);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(895, 35);
            this.panel1.TabIndex = 13;
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
            this._screen.Size = new System.Drawing.Size(895, 10);
            this._screen.TabIndex = 10;
            // 
            // _toolStrip
            // 
            this._toolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._toolStrip.Location = new System.Drawing.Point(0, 10);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(895, 25);
            this._toolStrip.TabIndex = 11;
            this._toolStrip.Text = "toolStrip1";
            // 
            // _grouper2
            // 
            this._grouper2.BackgroundColor = System.Drawing.Color.Transparent;
            this._grouper2.BackgroundGradientColor = System.Drawing.SystemColors.ControlLight;
            this._grouper2.BackgroundGradientMode = MyLib._grouper.GroupBoxGradientMode.None;
            this._grouper2.BorderColor = System.Drawing.Color.Black;
            this._grouper2.BorderThickness = 1F;
            this._grouper2.Controls.Add(this._extra);
            this._grouper2.CustomGroupBoxColor = System.Drawing.Color.White;
            this._grouper2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._grouper2.GroupImage = null;
            this._grouper2.GroupTitle = "";
            this._grouper2.Location = new System.Drawing.Point(0, 439);
            this._grouper2.Name = "_grouper2";
            this._grouper2.Padding = new System.Windows.Forms.Padding(5);
            this._grouper2.PaintGroupBox = false;
            this._grouper2.RoundCorners = 3;
            this._grouper2.ShadowColor = System.Drawing.Color.DarkGray;
            this._grouper2.ShadowControl = false;
            this._grouper2.ShadowThickness = 3;
            this._grouper2.Size = new System.Drawing.Size(895, 209);
            this._grouper2.TabIndex = 1;
            // 
            // _extra
            // 
            this._extra.AutoSize = true;
            this._extra.BackColor = System.Drawing.Color.Transparent;
            this._extra.Dock = System.Windows.Forms.DockStyle.Fill;
            this._extra.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._extra.Location = new System.Drawing.Point(5, 5);
            this._extra.Name = "_extra";
            this._extra.Size = new System.Drawing.Size(885, 199);
            this._extra.TabIndex = 13;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.AutoSize = true;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._exitButton);
            this._myFlowLayoutPanel1.Controls.Add(this._processButton);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 648);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(895, 30);
            this._myFlowLayoutPanel1.TabIndex = 9;
            // 
            // _exitButton
            // 
            this._exitButton._drawNewMethod = false;
            this._exitButton.AutoSize = true;
            this._exitButton.BackColor = System.Drawing.Color.Transparent;
            this._exitButton.ButtonText = "ESC = Exit";
            this._exitButton.Location = new System.Drawing.Point(799, 3);
            this._exitButton.myImage = global::SMLERPReportTool.Properties.Resources.error;
            this._exitButton.myTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._exitButton.Name = "_exitButton";
            this._exitButton.Size = new System.Drawing.Size(93, 24);
            this._exitButton.TabIndex = 6;
            this._exitButton.Text = "ESC = Exit";
            this._exitButton.UseVisualStyleBackColor = true;
            // 
            // _processButton
            // 
            this._processButton._drawNewMethod = false;
            this._processButton.AutoSize = true;
            this._processButton.BackColor = System.Drawing.Color.Transparent;
            this._processButton.ButtonText = "F11 = Process";
            this._processButton.Location = new System.Drawing.Point(680, 3);
            this._processButton.myImage = global::SMLERPReportTool.Properties.Resources.flash;
            this._processButton.myTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(113, 24);
            this._processButton.TabIndex = 5;
            this._processButton.Text = "F11 = Process";
            this._processButton.UseVisualStyleBackColor = true;
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
            this._myPanel2.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel2.Size = new System.Drawing.Size(895, 41);
            this._myPanel2.TabIndex = 12;
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
            this._label.Location = new System.Drawing.Point(5, 5);
            this._label.Name = "_label";
            this._label.ShadowColor = System.Drawing.Color.Gray;
            this._label.Size = new System.Drawing.Size(77, 29);
            this._label.StartColor = System.Drawing.Color.White;
            this._label.TabIndex = 1;
            this._label.Text = "Label";
            this._label.XOffset = 1F;
            this._label.YOffset = 1F;
            // 
            // _conditionScreen
            // 
            this.ClientSize = new System.Drawing.Size(895, 678);
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "_conditionScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._grouper1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this._grouper2.ResumeLayout(false);
            this._grouper2.PerformLayout();
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this._myPanel2.ResumeLayout(false);
            this._myPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib.VistaButton _exitButton;
        private MyLib._grouper _grouper1;
        private MyLib._grouper _grouper2;
        public SMLReport._whereUserControl _extra;
        public _condition_ic_grid _grid;
        public _condition_ic_screen _screen;
        public MyLib.VistaButton _processButton;
        public MyLib._myShadowLabel _label;
        private MyLib._myPanel _myPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip _toolStrip;

    }
}