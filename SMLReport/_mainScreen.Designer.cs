namespace SMLReport._design
{
    partial class _mainScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_mainScreen));
            this._tabimageList = new System.Windows.Forms.ImageList(this.components);
            this._toolBar = new System.Windows.Forms.ToolStrip();
            this._buttonNew = new System.Windows.Forms.ToolStripButton();
            this._buttonSave = new System.Windows.Forms.ToolStripButton();
            this._buttonOpen = new System.Windows.Forms.ToolStripButton();
            this._buttonImport = new System.Windows.Forms.ToolStripButton();
            this._myTabControl = new MyLib._myTabControl();
            this._queryDesignerTab = new System.Windows.Forms.TabPage();
            this._parameterDesignerTab = new System.Windows.Forms.TabPage();
            this._queryPreviewTab = new System.Windows.Forms.TabPage();
            this._reportDesignTab = new System.Windows.Forms.TabPage();
            this._reportPreviewTab = new System.Windows.Forms.TabPage();
            this._designView = new SMLReport._design._queryDesigner();
            this._designCondition = new SMLReport._design._designCondition();
            this._dataPreview1 = new SMLReport._queryPreview();
            this._standardReportDesigner1 = new SMLReport._standardReport._standardReportDesigner();
            this._formDesigner1 = new SMLReport._formReport._formDesigner();
            this._toolBar.SuspendLayout();
            this._myTabControl.SuspendLayout();
            this._queryDesignerTab.SuspendLayout();
            this._parameterDesignerTab.SuspendLayout();
            this._queryPreviewTab.SuspendLayout();
            this._reportDesignTab.SuspendLayout();
            this._reportPreviewTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // _tabimageList
            // 
            this._tabimageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_tabimageList.ImageStream")));
            this._tabimageList.TransparentColor = System.Drawing.Color.Transparent;
            this._tabimageList.Images.SetKeyName(0, "tables.png");
            this._tabimageList.Images.SetKeyName(1, "presentation.png");
            this._tabimageList.Images.SetKeyName(2, "document_refresh.png");
            this._tabimageList.Images.SetKeyName(3, "funnel.png");
            this._tabimageList.Images.SetKeyName(4, "table.png");
            // 
            // _toolBar
            // 
            this._toolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonNew,
            this._buttonSave,
            this._buttonOpen,
            this._buttonImport});
            this._toolBar.Location = new System.Drawing.Point(0, 0);
            this._toolBar.Name = "_toolBar";
            this._toolBar.Size = new System.Drawing.Size(680, 25);
            this._toolBar.TabIndex = 0;
            // 
            // _buttonNew
            // 
            this._buttonNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonNew.Image = global::SMLReport.Resource16x16.document_new;
            this._buttonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonNew.Name = "_buttonNew";
            this._buttonNew.Size = new System.Drawing.Size(23, 22);
            this._buttonNew.Text = "New";
            // 
            // _buttonSave
            // 
            this._buttonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonSave.Image = global::SMLReport.Resource16x16.disk_blue;
            this._buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.Size = new System.Drawing.Size(23, 22);
            this._buttonSave.Text = "Save";
            // 
            // _buttonOpen
            // 
            this._buttonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonOpen.Image = global::SMLReport.Resource16x16.folder_view;
            this._buttonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonOpen.Name = "_buttonOpen";
            this._buttonOpen.Size = new System.Drawing.Size(23, 22);
            this._buttonOpen.Text = "Open";
            // 
            // _buttonImport
            // 
            this._buttonImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonImport.Image = global::SMLReport.Resource16x16.import1;
            this._buttonImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonImport.Name = "_buttonImport";
            this._buttonImport.Size = new System.Drawing.Size(23, 22);
            this._buttonImport.Text = "Import";
            // 
            // _myTabControl
            // 
            this._myTabControl.Controls.Add(this._queryPreviewTab);
            this._myTabControl.Controls.Add(this._reportDesignTab);
            this._myTabControl.Controls.Add(this._reportPreviewTab);
            this._myTabControl.Controls.Add(this._parameterDesignerTab);
            this._myTabControl.Controls.Add(this._queryDesignerTab);
            this._myTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myTabControl.ImageList = this._tabimageList;
            this._myTabControl.Location = new System.Drawing.Point(0, 25);
            this._myTabControl.Margin = new System.Windows.Forms.Padding(0);
            this._myTabControl.Name = "_myTabControl";
            this._myTabControl.SelectedIndex = 0;
            this._myTabControl.Size = new System.Drawing.Size(680, 493);
            this._myTabControl.TabIndex = 4;
            // 
            // _queryDesignerTab
            // 
            this._queryDesignerTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._queryDesignerTab.Controls.Add(this._designView);
            this._queryDesignerTab.ImageKey = "tables.png";
            this._queryDesignerTab.Location = new System.Drawing.Point(4, 23);
            this._queryDesignerTab.Name = "_queryDesignerTab";
            this._queryDesignerTab.Size = new System.Drawing.Size(672, 466);
            this._queryDesignerTab.TabIndex = 3;
            this._queryDesignerTab.Text = "Query Designer";
            this._queryDesignerTab.UseVisualStyleBackColor = true;
            // 
            // _parameterDesignerTab
            // 
            this._parameterDesignerTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._parameterDesignerTab.Controls.Add(this._designCondition);
            this._parameterDesignerTab.ImageKey = "funnel.png";
            this._parameterDesignerTab.Location = new System.Drawing.Point(4, 23);
            this._parameterDesignerTab.Name = "_parameterDesignerTab";
            this._parameterDesignerTab.Size = new System.Drawing.Size(672, 466);
            this._parameterDesignerTab.TabIndex = 4;
            this._parameterDesignerTab.Text = "Parameter  Designer";
            this._parameterDesignerTab.UseVisualStyleBackColor = true;
            // 
            // _queryPreviewTab
            // 
            this._queryPreviewTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._queryPreviewTab.Controls.Add(this._dataPreview1);
            this._queryPreviewTab.ImageKey = "table.png";
            this._queryPreviewTab.Location = new System.Drawing.Point(4, 23);
            this._queryPreviewTab.Name = "_queryPreviewTab";
            this._queryPreviewTab.Size = new System.Drawing.Size(672, 466);
            this._queryPreviewTab.TabIndex = 5;
            this._queryPreviewTab.Text = "Query Preview";
            this._queryPreviewTab.UseVisualStyleBackColor = true;
            // 
            // _reportDesignTab
            // 
            this._reportDesignTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._reportDesignTab.Controls.Add(this._standardReportDesigner1);
            this._reportDesignTab.ImageKey = "presentation.png";
            this._reportDesignTab.Location = new System.Drawing.Point(4, 23);
            this._reportDesignTab.Margin = new System.Windows.Forms.Padding(0);
            this._reportDesignTab.Name = "_reportDesignTab";
            this._reportDesignTab.Size = new System.Drawing.Size(672, 466);
            this._reportDesignTab.TabIndex = 1;
            this._reportDesignTab.Text = "Report  Designer";
            this._reportDesignTab.UseVisualStyleBackColor = true;
            // 
            // _reportPreviewTab
            // 
            this._reportPreviewTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._reportPreviewTab.Controls.Add(this._formDesigner1);
            this._reportPreviewTab.ImageKey = "document_refresh.png";
            this._reportPreviewTab.Location = new System.Drawing.Point(4, 23);
            this._reportPreviewTab.Name = "_reportPreviewTab";
            this._reportPreviewTab.Size = new System.Drawing.Size(672, 466);
            this._reportPreviewTab.TabIndex = 2;
            this._reportPreviewTab.Text = "Report Preview";
            this._reportPreviewTab.UseVisualStyleBackColor = true;
            // 
            // _designView
            // 
            this._designView.BackColor = System.Drawing.Color.White;
            this._designView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._designView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._designView.Location = new System.Drawing.Point(0, 0);
            this._designView.Margin = new System.Windows.Forms.Padding(0);
            this._designView.Name = "_designView";
            this._designView.Size = new System.Drawing.Size(670, 464);
            this._designView.TabIndex = 0;
            // 
            // _designCondition
            // 
            this._designCondition.BackColor = System.Drawing.Color.White;
            this._designCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this._designCondition.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._designCondition.Location = new System.Drawing.Point(0, 0);
            this._designCondition.Name = "_designCondition";
            this._designCondition.Size = new System.Drawing.Size(670, 464);
            this._designCondition.TabIndex = 0;
            // 
            // _dataPreview1
            // 
            this._dataPreview1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataPreview1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._dataPreview1.Location = new System.Drawing.Point(0, 0);
            this._dataPreview1.Name = "_dataPreview1";
            this._dataPreview1.Size = new System.Drawing.Size(670, 464);
            this._dataPreview1.TabIndex = 0;
            // 
            // _standardReportDesigner1
            // 
            this._standardReportDesigner1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._standardReportDesigner1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._standardReportDesigner1.Location = new System.Drawing.Point(0, 0);
            this._standardReportDesigner1.Name = "_standardReportDesigner1";
            this._standardReportDesigner1.Size = new System.Drawing.Size(670, 464);
            this._standardReportDesigner1.TabIndex = 0;
            // 
            // _formDesigner1
            // 
            this._formDesigner1.BackColor = System.Drawing.SystemColors.Window;
            this._formDesigner1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._formDesigner1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._formDesigner1.Location = new System.Drawing.Point(0, 0);
            this._formDesigner1.Margin = new System.Windows.Forms.Padding(0);
            this._formDesigner1.Name = "_formDesigner1";
            this._formDesigner1.Size = new System.Drawing.Size(670, 464);
            this._formDesigner1.TabIndex = 0;
            // 
            // _mainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myTabControl);
            this.Controls.Add(this._toolBar);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "_mainScreen";
            this.Size = new System.Drawing.Size(680, 518);
            this._toolBar.ResumeLayout(false);
            this._toolBar.PerformLayout();
            this._myTabControl.ResumeLayout(false);
            this._queryDesignerTab.ResumeLayout(false);
            this._parameterDesignerTab.ResumeLayout(false);
            this._queryPreviewTab.ResumeLayout(false);
            this._reportDesignTab.ResumeLayout(false);
            this._reportPreviewTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myTabControl _myTabControl;
        private System.Windows.Forms.TabPage _reportDesignTab;
        private System.Windows.Forms.ImageList _tabimageList;
        private System.Windows.Forms.TabPage _reportPreviewTab;
        private System.Windows.Forms.TabPage _queryDesignerTab;
        private _queryDesigner _designView;
        private System.Windows.Forms.TabPage _parameterDesignerTab;
        private _designCondition _designCondition;
        private System.Windows.Forms.ToolStrip _toolBar;
        private System.Windows.Forms.ToolStripButton _buttonSave;
        private System.Windows.Forms.ToolStripButton _buttonOpen;
        private System.Windows.Forms.ToolStripButton _buttonNew;
        private System.Windows.Forms.ToolStripButton _buttonImport;
        private SMLReport._standardReport._standardReportDesigner _standardReportDesigner1;
        private SMLReport._formReport._formDesigner _formDesigner1;
        private System.Windows.Forms.TabPage _queryPreviewTab;
        private _queryPreview _dataPreview1;
    }
}
