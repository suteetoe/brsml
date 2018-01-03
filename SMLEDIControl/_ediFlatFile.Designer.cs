namespace SMLEDIControl
{
    partial class _ediFlatFile
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
            this._buttonExportPreview = new MyLib.VistaButton();
            this._buttonClear = new MyLib.VistaButton();
            this._buttonClose = new MyLib.VistaButton();
            this._buttonExport = new MyLib.VistaButton();
            this._myPanel1 = new MyLib._myPanel();
            this._myPanel2 = new MyLib._myPanel();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._myFlowLayoutPanel2 = new MyLib._myFlowLayoutPanel();
            this._conditionScreen = new MyLib._myScreen();
            this.tab_template_pl = new System.Windows.Forms.TabPage();
            this._resultTextboxpl = new System.Windows.Forms.TextBox();
            this.tab_template_bs = new System.Windows.Forms.TabPage();
            this._resultTextboxbs = new System.Windows.Forms.TextBox();
            this.tab_output = new System.Windows.Forms.TabControl();
            this._myPanel1.SuspendLayout();
            this._myPanel2.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this._myFlowLayoutPanel2.SuspendLayout();
            this.tab_template_pl.SuspendLayout();
            this.tab_template_bs.SuspendLayout();
            this.tab_output.SuspendLayout();
            this.SuspendLayout();
            // 
            // _buttonExportPreview
            // 
            this._buttonExportPreview._drawNewMethod = false;
            this._buttonExportPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonExportPreview.BackColor = System.Drawing.Color.Transparent;
            this._buttonExportPreview.ButtonText = "Preview";
            this._buttonExportPreview.Location = new System.Drawing.Point(3, 3);
            this._buttonExportPreview.myImage = global::SMLEDIControl.Properties.Resources.scroll_view;
            this._buttonExportPreview.Name = "_buttonExportPreview";
            this._buttonExportPreview.Size = new System.Drawing.Size(100, 24);
            this._buttonExportPreview.TabIndex = 4;
            this._buttonExportPreview.Text = "vistaButton5";
            this._buttonExportPreview.UseVisualStyleBackColor = false;
            this._buttonExportPreview.Click += new System.EventHandler(this._buttonExportPreview_Click);
            // 
            // _buttonClear
            // 
            this._buttonClear._drawNewMethod = false;
            this._buttonClear.BackColor = System.Drawing.Color.Transparent;
            this._buttonClear.ButtonText = "Clear";
            this._buttonClear.Location = new System.Drawing.Point(109, 3);
            this._buttonClear.myImage = global::SMLEDIControl.Properties.Resources.scroll_replace;
            this._buttonClear.Name = "_buttonClear";
            this._buttonClear.Size = new System.Drawing.Size(80, 24);
            this._buttonClear.TabIndex = 1;
            this._buttonClear.Text = "vistaButton2";
            this._buttonClear.UseVisualStyleBackColor = false;
            this._buttonClear.Click += new System.EventHandler(this._buttonClear_Click);
            // 
            // _buttonClose
            // 
            this._buttonClose._drawNewMethod = false;
            this._buttonClose.BackColor = System.Drawing.Color.Transparent;
            this._buttonClose.ButtonText = "ปิดจอ";
            this._buttonClose.Location = new System.Drawing.Point(195, 3);
            this._buttonClose.myImage = global::SMLEDIControl.Properties.Resources.error;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Size = new System.Drawing.Size(80, 24);
            this._buttonClose.TabIndex = 6;
            this._buttonClose.Text = "vistaButton1";
            this._buttonClose.UseVisualStyleBackColor = false;
            this._buttonClose.Click += new System.EventHandler(this._buttonClose_Click);
            // 
            // _buttonExport
            // 
            this._buttonExport._drawNewMethod = false;
            this._buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonExport.BackColor = System.Drawing.Color.Transparent;
            this._buttonExport.ButtonText = "Export";
            this._buttonExport.Location = new System.Drawing.Point(934, 3);
            this._buttonExport.myImage = global::SMLEDIControl.Properties.Resources.scroll_run;
            this._buttonExport.Name = "_buttonExport";
            this._buttonExport.Size = new System.Drawing.Size(100, 24);
            this._buttonExport.TabIndex = 5;
            this._buttonExport.Text = "vistaButton6";
            this._buttonExport.UseVisualStyleBackColor = false;
            this._buttonExport.Click += new System.EventHandler(this._buttonExport_Click);
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this.tab_output);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 40);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(1037, 780);
            this._myPanel1.TabIndex = 7;
            // 
            // _myPanel2
            // 
            this._myPanel2._switchTabAuto = false;
            this._myPanel2.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Controls.Add(this._myPanel1);
            this._myPanel2.Controls.Add(this._myFlowLayoutPanel1);
            this._myPanel2.Controls.Add(this._myFlowLayoutPanel2);
            this._myPanel2.Controls.Add(this._conditionScreen);
            this._myPanel2.CornerPicture = null;
            this._myPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel2.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Location = new System.Drawing.Point(3, 3);
            this._myPanel2.Name = "_myPanel2";
            this._myPanel2.Size = new System.Drawing.Size(1037, 851);
            this._myPanel2.TabIndex = 8;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._buttonExport);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 820);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(1037, 31);
            this._myFlowLayoutPanel1.TabIndex = 9;
            // 
            // _myFlowLayoutPanel2
            // 
            this._myFlowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel2.Controls.Add(this._buttonExportPreview);
            this._myFlowLayoutPanel2.Controls.Add(this._buttonClear);
            this._myFlowLayoutPanel2.Controls.Add(this._buttonClose);
            this._myFlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel2.Location = new System.Drawing.Point(0, 11);
            this._myFlowLayoutPanel2.Name = "_myFlowLayoutPanel2";
            this._myFlowLayoutPanel2.Size = new System.Drawing.Size(1037, 29);
            this._myFlowLayoutPanel2.TabIndex = 9;
            // 
            // _conditionScreen
            // 
            this._conditionScreen._isChange = false;
            this._conditionScreen.BackColor = System.Drawing.Color.Transparent;
            this._conditionScreen.Dock = System.Windows.Forms.DockStyle.Top;
            this._conditionScreen.Location = new System.Drawing.Point(0, 0);
            this._conditionScreen.Name = "_conditionScreen";
            this._conditionScreen.Size = new System.Drawing.Size(1037, 11);
            this._conditionScreen.TabIndex = 9;
            // 
            // tab_template_pl
            // 
            this.tab_template_pl.Controls.Add(this._resultTextboxpl);
            this.tab_template_pl.Location = new System.Drawing.Point(4, 23);
            this.tab_template_pl.Name = "tab_template_pl";
            this.tab_template_pl.Padding = new System.Windows.Forms.Padding(3);
            this.tab_template_pl.Size = new System.Drawing.Size(1029, 753);
            this.tab_template_pl.TabIndex = 1;
            this.tab_template_pl.Text = "Template - PL";
            this.tab_template_pl.UseVisualStyleBackColor = true;
            // 
            // _resultTextboxpl
            // 
            this._resultTextboxpl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultTextboxpl.Location = new System.Drawing.Point(3, 3);
            this._resultTextboxpl.Multiline = true;
            this._resultTextboxpl.Name = "_resultTextboxpl";
            this._resultTextboxpl.Size = new System.Drawing.Size(1023, 747);
            this._resultTextboxpl.TabIndex = 3;
            // 
            // tab_template_bs
            // 
            this.tab_template_bs.Controls.Add(this._resultTextboxbs);
            this.tab_template_bs.Location = new System.Drawing.Point(4, 23);
            this.tab_template_bs.Name = "tab_template_bs";
            this.tab_template_bs.Padding = new System.Windows.Forms.Padding(3);
            this.tab_template_bs.Size = new System.Drawing.Size(1029, 753);
            this.tab_template_bs.TabIndex = 0;
            this.tab_template_bs.Text = "Template - BS";
            this.tab_template_bs.UseVisualStyleBackColor = true;
            // 
            // _resultTextboxbs
            // 
            this._resultTextboxbs.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultTextboxbs.Location = new System.Drawing.Point(3, 3);
            this._resultTextboxbs.Multiline = true;
            this._resultTextboxbs.Name = "_resultTextboxbs";
            this._resultTextboxbs.Size = new System.Drawing.Size(1023, 747);
            this._resultTextboxbs.TabIndex = 2;
            // 
            // tab_output
            // 
            this.tab_output.Controls.Add(this.tab_template_bs);
            this.tab_output.Controls.Add(this.tab_template_pl);
            this.tab_output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_output.Location = new System.Drawing.Point(0, 0);
            this.tab_output.Name = "tab_output";
            this.tab_output.SelectedIndex = 0;
            this.tab_output.Size = new System.Drawing.Size(1037, 780);
            this.tab_output.TabIndex = 8;
            // 
            // _ediFlatFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myPanel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_ediFlatFile";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(1043, 857);
            this._myPanel1.ResumeLayout(false);
            this._myPanel2.ResumeLayout(false);
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel2.ResumeLayout(false);
            this.tab_template_pl.ResumeLayout(false);
            this.tab_template_pl.PerformLayout();
            this.tab_template_bs.ResumeLayout(false);
            this.tab_template_bs.PerformLayout();
            this.tab_output.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MyLib.VistaButton _buttonClear;
        private MyLib.VistaButton _buttonExportPreview;
        private MyLib.VistaButton _buttonClose;
        private MyLib.VistaButton _buttonExport;
        private MyLib._myPanel _myPanel1;
        private MyLib._myPanel _myPanel2;
        private MyLib._myScreen _conditionScreen;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel2;
        private System.Windows.Forms.TabControl tab_output;
        private System.Windows.Forms.TabPage tab_template_bs;
        private System.Windows.Forms.TextBox _resultTextboxbs;
        private System.Windows.Forms.TabPage tab_template_pl;
        private System.Windows.Forms.TextBox _resultTextboxpl;
    }
}
