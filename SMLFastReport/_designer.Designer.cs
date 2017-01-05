namespace SMLFastReport
{
    partial class _designer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_designer));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._previewButton = new System.Windows.Forms.ToolStripButton();
            this._showQueryCheckedBox = new MyLib.ToolStripCheckedBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMyLabel1 = new MyLib.ToolStripMyLabel();
            this._reportTypeCombo = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this._buttonLoadReport = new System.Windows.Forms.ToolStripButton();
            this._buttonSaveReport = new System.Windows.Forms.ToolStripButton();
            this._buttonSaveAs = new System.Windows.Forms.ToolStripButton();
            this._deleteButton = new MyLib.ToolStripMyButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._saveXMLButton = new System.Windows.Forms.ToolStripButton();
            this._loadXMLButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._viewSearchNameButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this._buttonClose = new System.Windows.Forms.ToolStripButton();
            this._reportServerSaveButton = new MyLib.ToolStripMyButton();
            this._dock = new Crom.Controls.Docking.DockContainer();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripMyButton1 = new MyLib.ToolStripMyButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._previewButton,
            this._showQueryCheckedBox,
            this.toolStripSeparator1,
            this.toolStripMyLabel1,
            this._reportTypeCombo,
            this.toolStripSeparator5,
            this._buttonLoadReport,
            this._buttonSaveReport,
            this._buttonSaveAs,
            this._deleteButton,
            this.toolStripSeparator2,
            this._saveXMLButton,
            this._loadXMLButton,
            this.toolStripSeparator3,
            this._viewSearchNameButton,
            this.toolStripSeparator4,
            this._buttonClose,
            this._reportServerSaveButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1284, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _previewButton
            // 
            this._previewButton.Image = global::SMLFastReport.Properties.Resources.table_sql_view;
            this._previewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._previewButton.Name = "_previewButton";
            this._previewButton.Size = new System.Drawing.Size(68, 22);
            this._previewButton.Text = "Preview";
            this._previewButton.Click += new System.EventHandler(this._previewButton_Click);
            // 
            // _showQueryCheckedBox
            // 
            this._showQueryCheckedBox.BackColor = System.Drawing.Color.Transparent;
            // 
            // _showQueryCheckedBox
            // 
            this._showQueryCheckedBox.MyCheckBox.AccessibleName = "_showQueryCheckedBox";
            this._showQueryCheckedBox.MyCheckBox.BackColor = System.Drawing.Color.Transparent;
            this._showQueryCheckedBox.MyCheckBox.Location = new System.Drawing.Point(77, 1);
            this._showQueryCheckedBox.MyCheckBox.Name = "_showQueryCheckedBox";
            this._showQueryCheckedBox.MyCheckBox.Size = new System.Drawing.Size(58, 22);
            this._showQueryCheckedBox.MyCheckBox.TabIndex = 1;
            this._showQueryCheckedBox.MyCheckBox.Text = "Query";
            this._showQueryCheckedBox.MyCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._showQueryCheckedBox.MyCheckBox.UseVisualStyleBackColor = false;
            this._showQueryCheckedBox.Name = "_showQueryCheckedBox";
            this._showQueryCheckedBox.Size = new System.Drawing.Size(58, 22);
            this._showQueryCheckedBox.Text = "Query";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripMyLabel1
            // 
            this.toolStripMyLabel1.Name = "toolStripMyLabel1";
            this.toolStripMyLabel1.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripMyLabel1.ResourceName = "ประเภท :";
            this.toolStripMyLabel1.Size = new System.Drawing.Size(51, 22);
            this.toolStripMyLabel1.Text = "ประเภท : ";
            // 
            // _reportTypeCombo
            // 
            this._reportTypeCombo.Name = "_reportTypeCombo";
            this._reportTypeCombo.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // _buttonLoadReport
            // 
            this._buttonLoadReport.Image = global::SMLFastReport.Properties.Resources.folder_view;
            this._buttonLoadReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonLoadReport.Name = "_buttonLoadReport";
            this._buttonLoadReport.Size = new System.Drawing.Size(53, 22);
            this._buttonLoadReport.Text = "Load";
            this._buttonLoadReport.Click += new System.EventHandler(this._buttonLoadReport_Click);
            // 
            // _buttonSaveReport
            // 
            this._buttonSaveReport.Image = global::SMLFastReport.Properties.Resources.disk_blue;
            this._buttonSaveReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSaveReport.Name = "_buttonSaveReport";
            this._buttonSaveReport.Size = new System.Drawing.Size(51, 22);
            this._buttonSaveReport.Text = "Save";
            this._buttonSaveReport.Click += new System.EventHandler(this._buttonSaveReport_Click);
            // 
            // _buttonSaveAs
            // 
            this._buttonSaveAs.Image = global::SMLFastReport.Properties.Resources.disks;
            this._buttonSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSaveAs.Name = "_buttonSaveAs";
            this._buttonSaveAs.Size = new System.Drawing.Size(67, 22);
            this._buttonSaveAs.Text = "Save As";
            this._buttonSaveAs.Click += new System.EventHandler(this._buttonSaveAs_Click);
            // 
            // _deleteButton
            // 
            this._deleteButton.Image = global::SMLFastReport.Properties.Resources.delete2;
            this._deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._deleteButton.Name = "_deleteButton";
            this._deleteButton.Padding = new System.Windows.Forms.Padding(1);
            this._deleteButton.ResourceName = "";
            this._deleteButton.Size = new System.Drawing.Size(62, 22);
            this._deleteButton.Text = "Delete";
            this._deleteButton.Click += new System.EventHandler(this._deleteButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _saveXMLButton
            // 
            this._saveXMLButton.Image = global::SMLFastReport.Properties.Resources.export2;
            this._saveXMLButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveXMLButton.Name = "_saveXMLButton";
            this._saveXMLButton.Size = new System.Drawing.Size(78, 22);
            this._saveXMLButton.Text = "Save XML";
            this._saveXMLButton.Click += new System.EventHandler(this._saveXMLButton_Click);
            // 
            // _loadXMLButton
            // 
            this._loadXMLButton.Image = global::SMLFastReport.Properties.Resources.import1;
            this._loadXMLButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._loadXMLButton.Name = "_loadXMLButton";
            this._loadXMLButton.Size = new System.Drawing.Size(80, 22);
            this._loadXMLButton.Text = "Load XML";
            this._loadXMLButton.Click += new System.EventHandler(this._loadXMLButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // _viewSearchNameButton
            // 
            this._viewSearchNameButton.Image = global::SMLFastReport.Properties.Resources.scroll_view;
            this._viewSearchNameButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._viewSearchNameButton.Name = "_viewSearchNameButton";
            this._viewSearchNameButton.Size = new System.Drawing.Size(97, 22);
            this._viewSearchNameButton.Text = "Search Name";
            this._viewSearchNameButton.Click += new System.EventHandler(this._viewSearchNameButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // _buttonClose
            // 
            this._buttonClose.Image = global::SMLFastReport.Properties.Resources.error;
            this._buttonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Size = new System.Drawing.Size(56, 22);
            this._buttonClose.Text = "Close";
            this._buttonClose.Click += new System.EventHandler(this._buttonClose_Click);
            // 
            // _reportServerSaveButton
            // 
            this._reportServerSaveButton.Image = ((System.Drawing.Image)(resources.GetObject("_reportServerSaveButton.Image")));
            this._reportServerSaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._reportServerSaveButton.Name = "_reportServerSaveButton";
            this._reportServerSaveButton.Padding = new System.Windows.Forms.Padding(1);
            this._reportServerSaveButton.ResourceName = "";
            this._reportServerSaveButton.Size = new System.Drawing.Size(144, 22);
            this._reportServerSaveButton.Text = "Send Report To Server";
            this._reportServerSaveButton.Visible = false;
            this._reportServerSaveButton.Click += new System.EventHandler(this._reportServerSaveButton_Click);
            // 
            // _dock
            // 
            this._dock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(118)))), ((int)(((byte)(118)))));
            this._dock.CanMoveByMouseFilledForms = true;
            this._dock.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dock.Location = new System.Drawing.Point(0, 25);
            this._dock.Name = "_dock";
            this._dock.Size = new System.Drawing.Size(1284, 661);
            this._dock.TabIndex = 0;
            this._dock.TitleBarGradientColor1 = System.Drawing.SystemColors.Control;
            this._dock.TitleBarGradientColor2 = System.Drawing.Color.White;
            this._dock.TitleBarGradientSelectedColor1 = System.Drawing.Color.DarkGray;
            this._dock.TitleBarGradientSelectedColor2 = System.Drawing.Color.White;
            this._dock.TitleBarTextColor = System.Drawing.Color.Black;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 23);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripMyButton1
            // 
            this.toolStripMyButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripMyButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMyButton1.Image")));
            this.toolStripMyButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMyButton1.Name = "toolStripMyButton1";
            this.toolStripMyButton1.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripMyButton1.ResourceName = "";
            this.toolStripMyButton1.Size = new System.Drawing.Size(23, 23);
            this.toolStripMyButton1.Text = "toolStripMyButton1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(23, 23);
            this.toolStripLabel1.Text = "toolStripLabel1";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(23, 23);
            this.toolStripLabel2.Text = "toolStripLabel2";
            // 
            // _designer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._dock);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_designer";
            this.Size = new System.Drawing.Size(1284, 686);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Crom.Controls.Docking.DockContainer _dock;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _previewButton;
        private System.Windows.Forms.ToolStripButton _buttonSaveReport;
        private System.Windows.Forms.ToolStripButton _buttonLoadReport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _buttonSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton _saveXMLButton;
        private System.Windows.Forms.ToolStripButton _loadXMLButton;
        private MyLib.ToolStripCheckedBox _showQueryCheckedBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton _viewSearchNameButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        public System.Windows.Forms.ToolStripButton _buttonClose;
        private MyLib.ToolStripMyButton _deleteButton;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private MyLib.ToolStripMyButton _reportServerSaveButton;
        private MyLib.ToolStripMyButton toolStripMyButton1;
        private MyLib.ToolStripMyLabel toolStripMyLabel1;
        private System.Windows.Forms.ToolStripComboBox _reportTypeCombo;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    }
}
