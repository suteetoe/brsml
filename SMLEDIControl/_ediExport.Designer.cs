namespace SMLEDIControl
{
    partial class _ediExport
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this._resultTextbox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this._gridDocList = new MyLib._myGrid();
            this._buttonExportPreview = new MyLib.VistaButton();
            this._buttonCheckAll = new MyLib.VistaButton();
            this._buttonUncheckAll = new MyLib.VistaButton();
            this._buttonExport = new MyLib.VistaButton();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._buttonLoad = new MyLib.VistaButton();
            this._buttonClear = new MyLib.VistaButton();
            this._buttonClose = new MyLib.VistaButton();
            this._conditionScreen = new MyLib._myScreen();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 44);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._gridDocList);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._resultTextbox);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(893, 811);
            this.splitContainer1.SplitterDistance = 379;
            this.splitContainer1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._buttonExportPreview);
            this.panel1.Controls.Add(this._buttonCheckAll);
            this.panel1.Controls.Add(this._buttonUncheckAll);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 347);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(891, 30);
            this.panel1.TabIndex = 0;
            // 
            // _resultTextbox
            // 
            this._resultTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultTextbox.Location = new System.Drawing.Point(0, 0);
            this._resultTextbox.Multiline = true;
            this._resultTextbox.Name = "_resultTextbox";
            this._resultTextbox.Size = new System.Drawing.Size(891, 396);
            this._resultTextbox.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._buttonExport);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 396);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(891, 30);
            this.panel2.TabIndex = 1;
            // 
            // _gridDocList
            // 
            this._gridDocList._extraWordShow = true;
            this._gridDocList._selectRow = -1;
            this._gridDocList.BackColor = System.Drawing.SystemColors.Window;
            this._gridDocList.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridDocList.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridDocList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridDocList.Font = new System.Drawing.Font("Tahoma", 9F);
            this._gridDocList.Location = new System.Drawing.Point(0, 0);
            this._gridDocList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridDocList.Name = "_gridDocList";
            this._gridDocList.Size = new System.Drawing.Size(891, 347);
            this._gridDocList.TabIndex = 1;
            this._gridDocList.TabStop = false;
            // 
            // _buttonExportPreview
            // 
            this._buttonExportPreview._drawNewMethod = false;
            this._buttonExportPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonExportPreview.BackColor = System.Drawing.Color.Transparent;
            this._buttonExportPreview.ButtonText = "Preview";
            this._buttonExportPreview.Location = new System.Drawing.Point(788, 4);
            this._buttonExportPreview.myImage = global::SMLEDIControl.Properties.Resources.scroll_view;
            this._buttonExportPreview.Name = "_buttonExportPreview";
            this._buttonExportPreview.Size = new System.Drawing.Size(100, 24);
            this._buttonExportPreview.TabIndex = 4;
            this._buttonExportPreview.Text = "vistaButton5";
            this._buttonExportPreview.UseVisualStyleBackColor = false;
            this._buttonExportPreview.Click += new System.EventHandler(this._buttonExportPreview_Click);
            // 
            // _buttonCheckAll
            // 
            this._buttonCheckAll._drawNewMethod = false;
            this._buttonCheckAll.BackColor = System.Drawing.Color.Transparent;
            this._buttonCheckAll.ButtonText = "Check All";
            this._buttonCheckAll.Location = new System.Drawing.Point(3, 3);
            this._buttonCheckAll.myImage = global::SMLEDIControl.Properties.Resources.document_check;
            this._buttonCheckAll.Name = "_buttonCheckAll";
            this._buttonCheckAll.Size = new System.Drawing.Size(100, 24);
            this._buttonCheckAll.TabIndex = 3;
            this._buttonCheckAll.Text = "vistaButton4";
            this._buttonCheckAll.UseVisualStyleBackColor = false;
            this._buttonCheckAll.Click += new System.EventHandler(this._buttonCheckAll_Click);
            // 
            // _buttonUncheckAll
            // 
            this._buttonUncheckAll._drawNewMethod = false;
            this._buttonUncheckAll.BackColor = System.Drawing.Color.Transparent;
            this._buttonUncheckAll.ButtonText = "Uncheck All";
            this._buttonUncheckAll.Location = new System.Drawing.Point(109, 3);
            this._buttonUncheckAll.myImage = global::SMLEDIControl.Properties.Resources.document_delete;
            this._buttonUncheckAll.Name = "_buttonUncheckAll";
            this._buttonUncheckAll.Size = new System.Drawing.Size(100, 24);
            this._buttonUncheckAll.TabIndex = 2;
            this._buttonUncheckAll.Text = "vistaButton3";
            this._buttonUncheckAll.UseVisualStyleBackColor = false;
            this._buttonUncheckAll.Click += new System.EventHandler(this._buttonUncheckAll_Click);
            // 
            // _buttonExport
            // 
            this._buttonExport._drawNewMethod = false;
            this._buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonExport.BackColor = System.Drawing.Color.Transparent;
            this._buttonExport.ButtonText = "Export";
            this._buttonExport.Location = new System.Drawing.Point(788, 3);
            this._buttonExport.myImage = global::SMLEDIControl.Properties.Resources.scroll_run;
            this._buttonExport.Name = "_buttonExport";
            this._buttonExport.Size = new System.Drawing.Size(100, 24);
            this._buttonExport.TabIndex = 5;
            this._buttonExport.Text = "vistaButton6";
            this._buttonExport.UseVisualStyleBackColor = false;
            this._buttonExport.Click += new System.EventHandler(this._buttonExport_Click);
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._buttonLoad);
            this._myFlowLayoutPanel1.Controls.Add(this._buttonClear);
            this._myFlowLayoutPanel1.Controls.Add(this._buttonClose);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(3, 14);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(893, 30);
            this._myFlowLayoutPanel1.TabIndex = 1;
            // 
            // _buttonLoad
            // 
            this._buttonLoad._drawNewMethod = false;
            this._buttonLoad.BackColor = System.Drawing.Color.Transparent;
            this._buttonLoad.ButtonText = "Load";
            this._buttonLoad.Location = new System.Drawing.Point(3, 3);
            this._buttonLoad.myImage = global::SMLEDIControl.Properties.Resources.flash;
            this._buttonLoad.Name = "_buttonLoad";
            this._buttonLoad.Size = new System.Drawing.Size(80, 24);
            this._buttonLoad.TabIndex = 0;
            this._buttonLoad.Text = "vistaButton1";
            this._buttonLoad.UseVisualStyleBackColor = false;
            this._buttonLoad.Click += new System.EventHandler(this._buttonLoad_Click);
            // 
            // _buttonClear
            // 
            this._buttonClear._drawNewMethod = false;
            this._buttonClear.BackColor = System.Drawing.Color.Transparent;
            this._buttonClear.ButtonText = "Clear";
            this._buttonClear.Location = new System.Drawing.Point(89, 3);
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
            this._buttonClose.Location = new System.Drawing.Point(175, 3);
            this._buttonClose.myImage = global::SMLEDIControl.Properties.Resources.error;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Size = new System.Drawing.Size(80, 24);
            this._buttonClose.TabIndex = 6;
            this._buttonClose.Text = "vistaButton1";
            this._buttonClose.UseVisualStyleBackColor = false;
            this._buttonClose.Click += new System.EventHandler(this._buttonClose_Click);
            // 
            // _conditionScreen
            // 
            this._conditionScreen._isChange = false;
            this._conditionScreen.BackColor = System.Drawing.Color.Transparent;
            this._conditionScreen.Dock = System.Windows.Forms.DockStyle.Top;
            this._conditionScreen.Location = new System.Drawing.Point(3, 3);
            this._conditionScreen.Name = "_conditionScreen";
            this._conditionScreen.Size = new System.Drawing.Size(893, 11);
            this._conditionScreen.TabIndex = 0;
            // 
            // _ediExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this._myFlowLayoutPanel1);
            this.Controls.Add(this._conditionScreen);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_ediExport";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(899, 858);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myScreen _conditionScreen;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib.VistaButton _buttonLoad;
        private MyLib.VistaButton _buttonClear;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MyLib._myGrid _gridDocList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox _resultTextbox;
        private System.Windows.Forms.Panel panel2;
        private MyLib.VistaButton _buttonExportPreview;
        private MyLib.VistaButton _buttonCheckAll;
        private MyLib.VistaButton _buttonUncheckAll;
        private MyLib.VistaButton _buttonExport;
        private MyLib.VistaButton _buttonClose;
    }
}
