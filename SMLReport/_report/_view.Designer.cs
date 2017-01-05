namespace SMLReport._report
{
    partial class _view
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_view));
            this._pageSetupDialog = new System.Windows.Forms.PageSetupDialog();
            this._printDocument = new System.Drawing.Printing.PrintDocument();
            this._printDialog = new System.Windows.Forms.PrintDialog();
            this._printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this._toolStripPreview = new System.Windows.Forms.ToolStrip();
            this._buttonCondition = new MyLib.ToolStripMyButton();
            this._buttonBuildReport = new MyLib.ToolStripMyButton();
            this._optionButton = new MyLib.ToolStripMyButton();
            this._buttonOpenExcel = new MyLib.ToolStripMyButton();
            this._saveToExcelButton = new MyLib.ToolStripMyButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._buttonPageSetup = new MyLib.ToolStripMyButton();
            this._buttonPrintPreview = new MyLib.ToolStripMyButton();
            this._buttonPrint = new MyLib.ToolStripMyButton();
            this._buttonFormPrint = new MyLib.ToolStripMyButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._buttonPageFirst = new System.Windows.Forms.ToolStripButton();
            this._buttonPagePrev = new System.Windows.Forms.ToolStripButton();
            this._textBoxPageNumber = new System.Windows.Forms.ToolStripTextBox();
            this._buttonPageNext = new System.Windows.Forms.ToolStripButton();
            this._buttonPageLast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._scaleComboBox = new System.Windows.Forms.ToolStripComboBox();
            this._buttonExample = new MyLib.ToolStripMyButton();
            this._buttonClose = new MyLib.ToolStripMyButton();
            this._csvExportButton = new System.Windows.Forms.ToolStripButton();
            this._reportDesignButton = new MyLib.ToolStripMyButton();
            this._panel = new MyLib._myPanel();
            this._hScrollBar = new System.Windows.Forms.HScrollBar();
            this._vScrollBar = new System.Windows.Forms.VScrollBar();
            this._reportStatusStrip = new System.Windows.Forms.StatusStrip();
            this._reportProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this._reportStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this._panelView = new System.Windows.Forms.Panel();
            this._rulerTopControl = new SMLReport._design._rulerControl();
            this._rulerLeftControl = new SMLReport._design._rulerControl();
            this._paper = new SMLReport._report._reportDrawPaper();
            this._timer1 = new System.Windows.Forms.Timer(this.components);
            this._toolStripPreview.SuspendLayout();
            this._reportStatusStrip.SuspendLayout();
            this._panelView.SuspendLayout();
            this.SuspendLayout();
            // 
            // _pageSetupDialog
            // 
            this._pageSetupDialog.Document = this._printDocument;
            this._pageSetupDialog.EnableMetric = true;
            // 
            // _printDialog
            // 
            this._printDialog.AllowSelection = true;
            this._printDialog.AllowSomePages = true;
            this._printDialog.Document = this._printDocument;
            this._printDialog.PrintToFile = true;
            this._printDialog.UseEXDialog = true;
            // 
            // _printPreviewDialog
            // 
            this._printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this._printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this._printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this._printPreviewDialog.Document = this._printDocument;
            this._printPreviewDialog.Enabled = true;
            this._printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("_printPreviewDialog.Icon")));
            this._printPreviewDialog.Name = "_printPreviewDialog";
            this._printPreviewDialog.Visible = false;
            // 
            // _toolStripPreview
            // 
            this._toolStripPreview.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolStripPreview.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonCondition,
            this._buttonBuildReport,
            this._optionButton,
            this._buttonOpenExcel,
            this._saveToExcelButton,
            this.toolStripSeparator1,
            this._buttonPageSetup,
            this._buttonPrintPreview,
            this._buttonPrint,
            this._buttonFormPrint,
            this.toolStripSeparator3,
            this._buttonPageFirst,
            this._buttonPagePrev,
            this._textBoxPageNumber,
            this._buttonPageNext,
            this._buttonPageLast,
            this.toolStripSeparator2,
            this._scaleComboBox,
            this._buttonExample,
            this._buttonClose,
            this._csvExportButton,
            this._reportDesignButton});
            this._toolStripPreview.Location = new System.Drawing.Point(0, 0);
            this._toolStripPreview.Name = "_toolStripPreview";
            this._toolStripPreview.Padding = new System.Windows.Forms.Padding(0);
            this._toolStripPreview.Size = new System.Drawing.Size(1014, 25);
            this._toolStripPreview.TabIndex = 3;
            this._toolStripPreview.Text = "toolStrip3";
            // 
            // _buttonCondition
            // 
            this._buttonCondition.Image = global::SMLReport.Resource16x16.import1;
            this._buttonCondition.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonCondition.Name = "_buttonCondition";
            this._buttonCondition.Padding = new System.Windows.Forms.Padding(1);
            this._buttonCondition.ResourceName = "เงื่อนไข";
            this._buttonCondition.Size = new System.Drawing.Size(60, 22);
            this._buttonCondition.Text = "เงื่อนไข";
            // 
            // _buttonBuildReport
            // 
            this._buttonBuildReport.Image = global::SMLReport.Resource16x16.refresh;
            this._buttonBuildReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonBuildReport.Name = "_buttonBuildReport";
            this._buttonBuildReport.Padding = new System.Windows.Forms.Padding(1);
            this._buttonBuildReport.ResourceName = "ประมวลผล";
            this._buttonBuildReport.Size = new System.Drawing.Size(76, 22);
            this._buttonBuildReport.Text = "ประมวลผล";
            this._buttonBuildReport.Click += new System.EventHandler(this._buttonBuildReport_Click);
            // 
            // _optionButton
            // 
            this._optionButton.Image = global::SMLReport.Resource16x16.preferences;
            this._optionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._optionButton.Name = "_optionButton";
            this._optionButton.Padding = new System.Windows.Forms.Padding(1);
            this._optionButton.ResourceName = "ข้อเลือกพิเศษ";
            this._optionButton.Size = new System.Drawing.Size(89, 22);
            this._optionButton.Text = "ข้อเลือกพิเศษ";
            // 
            // _buttonOpenExcel
            // 
            this._buttonOpenExcel.Image = global::SMLReport.Resource16x16.excel;
            this._buttonOpenExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonOpenExcel.Name = "_buttonOpenExcel";
            this._buttonOpenExcel.Padding = new System.Windows.Forms.Padding(1);
            this._buttonOpenExcel.ResourceName = "เปิดโดย Excel";
            this._buttonOpenExcel.Size = new System.Drawing.Size(91, 22);
            this._buttonOpenExcel.Text = "เปิดโดย Excel";
            this._buttonOpenExcel.Click += new System.EventHandler(this._buttonOpenExcel_Click);
            // 
            // _saveToExcelButton
            // 
            this._saveToExcelButton.Image = global::SMLReport.Resource16x16.disk_blue;
            this._saveToExcelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveToExcelButton.Name = "_saveToExcelButton";
            this._saveToExcelButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveToExcelButton.ResourceName = "Save To Excel";
            this._saveToExcelButton.Size = new System.Drawing.Size(98, 22);
            this._saveToExcelButton.Text = "Save To Excel";
            this._saveToExcelButton.Click += new System.EventHandler(this._saveToExcelButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _buttonPageSetup
            // 
            this._buttonPageSetup.Image = global::SMLReport.Resource16x16.pagesetup;
            this._buttonPageSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonPageSetup.Name = "_buttonPageSetup";
            this._buttonPageSetup.Padding = new System.Windows.Forms.Padding(1);
            this._buttonPageSetup.ResourceName = "ขนาดกระดาษ";
            this._buttonPageSetup.Size = new System.Drawing.Size(90, 22);
            this._buttonPageSetup.Text = "ขนาดกระดาษ";
            this._buttonPageSetup.Click += new System.EventHandler(this._buttonPrintSetup_Click);
            // 
            // _buttonPrintPreview
            // 
            this._buttonPrintPreview.Image = global::SMLReport.Resource16x16.document;
            this._buttonPrintPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonPrintPreview.Name = "_buttonPrintPreview";
            this._buttonPrintPreview.Padding = new System.Windows.Forms.Padding(1);
            this._buttonPrintPreview.ResourceName = "แสดงก่อนพิมพ์";
            this._buttonPrintPreview.Size = new System.Drawing.Size(96, 22);
            this._buttonPrintPreview.Text = "แสดงก่อนพิมพ์";
            this._buttonPrintPreview.Click += new System.EventHandler(this._buttonPrintPreview_Click);
            // 
            // _buttonPrint
            // 
            this._buttonPrint.Image = global::SMLReport.Resource16x16.printer;
            this._buttonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonPrint.Name = "_buttonPrint";
            this._buttonPrint.Padding = new System.Windows.Forms.Padding(1);
            this._buttonPrint.ResourceName = "พิมพ์";
            this._buttonPrint.Size = new System.Drawing.Size(52, 22);
            this._buttonPrint.Text = "พิมพ์";
            this._buttonPrint.Click += new System.EventHandler(this._buttonPrint_Click);
            // 
            // _buttonFormPrint
            // 
            this._buttonFormPrint.Image = global::SMLReport.Resource16x16.printer;
            this._buttonFormPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonFormPrint.Name = "_buttonFormPrint";
            this._buttonFormPrint.Padding = new System.Windows.Forms.Padding(1);
            this._buttonFormPrint.ResourceName = "พิมพ์แบบฟอร์ม";
            this._buttonFormPrint.Size = new System.Drawing.Size(98, 22);
            this._buttonFormPrint.Text = "พิมพ์แบบฟอร์ม";
            this._buttonFormPrint.Visible = false;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // _buttonPageFirst
            // 
            this._buttonPageFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonPageFirst.Image = global::SMLReport.Resource16x16.navigate_left2;
            this._buttonPageFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonPageFirst.Name = "_buttonPageFirst";
            this._buttonPageFirst.Size = new System.Drawing.Size(23, 22);
            this._buttonPageFirst.Text = "toolStripButton1";
            this._buttonPageFirst.Click += new System.EventHandler(this._buttonPageFirst_Click);
            // 
            // _buttonPagePrev
            // 
            this._buttonPagePrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonPagePrev.Image = global::SMLReport.Resource16x16.navigate_left;
            this._buttonPagePrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonPagePrev.Name = "_buttonPagePrev";
            this._buttonPagePrev.Size = new System.Drawing.Size(23, 22);
            this._buttonPagePrev.Text = "toolStripButton2";
            this._buttonPagePrev.Click += new System.EventHandler(this._buttonPagePrev_Click);
            // 
            // _textBoxPageNumber
            // 
            this._textBoxPageNumber.Name = "_textBoxPageNumber";
            this._textBoxPageNumber.Size = new System.Drawing.Size(40, 25);
            // 
            // _buttonPageNext
            // 
            this._buttonPageNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonPageNext.Image = global::SMLReport.Resource16x16.navigate_right;
            this._buttonPageNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonPageNext.Name = "_buttonPageNext";
            this._buttonPageNext.Size = new System.Drawing.Size(23, 22);
            this._buttonPageNext.Text = "toolStripButton3";
            this._buttonPageNext.Click += new System.EventHandler(this._buttonPageNext_Click);
            // 
            // _buttonPageLast
            // 
            this._buttonPageLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonPageLast.Image = global::SMLReport.Resource16x16.navigate_right2;
            this._buttonPageLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonPageLast.Name = "_buttonPageLast";
            this._buttonPageLast.Size = new System.Drawing.Size(23, 22);
            this._buttonPageLast.Text = "toolStripButton4";
            this._buttonPageLast.Click += new System.EventHandler(this._buttonPageLast_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _scaleComboBox
            // 
            this._scaleComboBox.AutoSize = false;
            this._scaleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._scaleComboBox.Items.AddRange(new object[] {
            "500%",
            "200%",
            "150%",
            "100%",
            "75%",
            "50%",
            "25%",
            "Page Width",
            "Whole Page"});
            this._scaleComboBox.MaxDropDownItems = 20;
            this._scaleComboBox.Name = "_scaleComboBox";
            this._scaleComboBox.Size = new System.Drawing.Size(80, 23);
            // 
            // _buttonExample
            // 
            this._buttonExample.Image = global::SMLReport.Resource16x16.document;
            this._buttonExample.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonExample.Name = "_buttonExample";
            this._buttonExample.Padding = new System.Windows.Forms.Padding(1);
            this._buttonExample.ResourceName = "ตัวอย่าง";
            this._buttonExample.Size = new System.Drawing.Size(63, 22);
            this._buttonExample.Text = "ตัวอย่าง";
            // 
            // _buttonClose
            // 
            this._buttonClose.Image = global::SMLReport.Resource16x16.error;
            this._buttonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Padding = new System.Windows.Forms.Padding(1);
            this._buttonClose.ResourceName = "ปิดหน้าจอ";
            this._buttonClose.Size = new System.Drawing.Size(74, 22);
            this._buttonClose.Text = "ปิดหน้าจอ";
            // 
            // _csvExportButton
            // 
            this._csvExportButton.Image = global::SMLReport.Resource16x16.csv_icon;
            this._csvExportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._csvExportButton.Name = "_csvExportButton";
            this._csvExportButton.Size = new System.Drawing.Size(84, 20);
            this._csvExportButton.Text = "CSV Export";
            this._csvExportButton.Click += new System.EventHandler(this._csvExportButton_Click);
            // 
            // _reportDesignButton
            // 
            this._reportDesignButton.Image = global::SMLReport.Resource16x16.window_edit;
            this._reportDesignButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._reportDesignButton.Name = "_reportDesignButton";
            this._reportDesignButton.Padding = new System.Windows.Forms.Padding(1);
            this._reportDesignButton.ResourceName = "";
            this._reportDesignButton.Size = new System.Drawing.Size(103, 22);
            this._reportDesignButton.Text = "Report Design";
            this._reportDesignButton.Visible = false;
            // 
            // _panel
            // 
            this._panel._switchTabAuto = false;
            this._panel.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._panel.CornerPicture = null;
            this._panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panel.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._panel.Location = new System.Drawing.Point(0, 0);
            this._panel.Name = "_panel";
            this._panel.ShowLineBackground = true;
            this._panel.Size = new System.Drawing.Size(1014, 534);
            this._panel.TabIndex = 0;
            // 
            // _hScrollBar
            // 
            this._hScrollBar.Location = new System.Drawing.Point(0, 0);
            this._hScrollBar.Name = "_hScrollBar";
            this._hScrollBar.Size = new System.Drawing.Size(198, 17);
            this._hScrollBar.TabIndex = 10;
            // 
            // _vScrollBar
            // 
            this._vScrollBar.Location = new System.Drawing.Point(0, 0);
            this._vScrollBar.Name = "_vScrollBar";
            this._vScrollBar.Size = new System.Drawing.Size(17, 102);
            this._vScrollBar.TabIndex = 9;
            // 
            // _reportStatusStrip
            // 
            this._reportStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._reportProgressBar,
            this._reportStatus});
            this._reportStatusStrip.Location = new System.Drawing.Point(0, 534);
            this._reportStatusStrip.Name = "_reportStatusStrip";
            this._reportStatusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this._reportStatusStrip.Size = new System.Drawing.Size(1014, 22);
            this._reportStatusStrip.TabIndex = 12;
            this._reportStatusStrip.Text = "statusStrip1";
            // 
            // _reportProgressBar
            // 
            this._reportProgressBar.Name = "_reportProgressBar";
            this._reportProgressBar.Size = new System.Drawing.Size(300, 16);
            // 
            // _reportStatus
            // 
            this._reportStatus.Name = "_reportStatus";
            this._reportStatus.Size = new System.Drawing.Size(10, 17);
            this._reportStatus.Text = ".";
            // 
            // _panelView
            // 
            this._panelView.Controls.Add(this._rulerTopControl);
            this._panelView.Controls.Add(this._rulerLeftControl);
            this._panelView.Controls.Add(this._paper);
            this._panelView.Controls.Add(this._vScrollBar);
            this._panelView.Controls.Add(this._hScrollBar);
            this._panelView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelView.Location = new System.Drawing.Point(0, 25);
            this._panelView.Name = "_panelView";
            this._panelView.Size = new System.Drawing.Size(1014, 509);
            this._panelView.TabIndex = 12;
            // 
            // _rulerTopControl
            // 
            this._rulerTopControl._beginValue = 0F;
            this._rulerTopControl._ruleScale = 1F;
            this._rulerTopControl.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this._rulerTopControl.Location = new System.Drawing.Point(0, 0);
            this._rulerTopControl.Name = "_rulerTopControl";
            this._rulerTopControl.Size = new System.Drawing.Size(411, 18);
            this._rulerTopControl.TabIndex = 7;
            // 
            // _rulerLeftControl
            // 
            this._rulerLeftControl._beginValue = 0F;
            this._rulerLeftControl._ruleScale = 1F;
            this._rulerLeftControl._vertical = true;
            this._rulerLeftControl.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this._rulerLeftControl.Location = new System.Drawing.Point(0, 0);
            this._rulerLeftControl.Name = "_rulerLeftControl";
            this._rulerLeftControl.Size = new System.Drawing.Size(18, 411);
            this._rulerLeftControl.TabIndex = 8;
            // 
            // _paper
            // 
            this._paper._drawScale = 1F;
            this._paper._topLeftPaper = new System.Drawing.Point(0, 0);
            this._paper.BackColor = System.Drawing.Color.Transparent;
            this._paper.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._paper.Location = new System.Drawing.Point(0, 0);
            this._paper.Name = "_paper";
            this._paper.PageCurrent = 1;
            this._paper.Size = new System.Drawing.Size(343, 324);
            this._paper.TabIndex = 11;
            // 
            // _timer1
            // 
            this._timer1.Enabled = true;
            this._timer1.Tick += new System.EventHandler(this._timer1_Tick);
            // 
            // _view
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.Controls.Add(this._panelView);
            this.Controls.Add(this._toolStripPreview);
            this.Controls.Add(this._panel);
            this.Controls.Add(this._reportStatusStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_view";
            this.Size = new System.Drawing.Size(1014, 556);
            this.Load += new System.EventHandler(this._myReportView_Load);
            this._toolStripPreview.ResumeLayout(false);
            this._toolStripPreview.PerformLayout();
            this._reportStatusStrip.ResumeLayout(false);
            this._reportStatusStrip.PerformLayout();
            this._panelView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Drawing.Printing.PrintDocument _printDocument;
        private System.Windows.Forms.PrintDialog _printDialog;
        private System.Windows.Forms.PrintPreviewDialog _printPreviewDialog;
        private SMLReport._design._rulerControl _rulerTopControl;
        private SMLReport._design._rulerControl _rulerLeftControl;
        private System.Windows.Forms.VScrollBar _vScrollBar;
        private System.Windows.Forms.HScrollBar _hScrollBar;
        public _reportDrawPaper _paper;
        public System.Windows.Forms.PageSetupDialog _pageSetupDialog;
        private System.Windows.Forms.ToolStrip _toolStripPreview;
        public MyLib.ToolStripMyButton _buttonBuildReport;
        private MyLib.ToolStripMyButton _buttonOpenExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private MyLib.ToolStripMyButton _buttonPageSetup;
        private MyLib.ToolStripMyButton _buttonPrintPreview;
        private MyLib.ToolStripMyButton _buttonPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton _buttonPageFirst;
        private System.Windows.Forms.ToolStripButton _buttonPagePrev;
        private System.Windows.Forms.ToolStripTextBox _textBoxPageNumber;
        private System.Windows.Forms.ToolStripButton _buttonPageNext;
        private System.Windows.Forms.ToolStripButton _buttonPageLast;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ToolStripComboBox _scaleComboBox;
        public MyLib._myPanel _panel;
        public MyLib.ToolStripMyButton _buttonClose;
        public MyLib.ToolStripMyButton _buttonCondition;
        public MyLib.ToolStripMyButton _buttonExample;
        public System.Windows.Forms.ToolStripProgressBar _reportProgressBar;
        public System.Windows.Forms.ToolStripStatusLabel _reportStatus;
        public System.Windows.Forms.StatusStrip _reportStatusStrip;
        private System.Windows.Forms.Panel _panelView;
        private System.Windows.Forms.Timer _timer1;
        public MyLib.ToolStripMyButton _optionButton;
        public MyLib.ToolStripMyButton _buttonFormPrint;
        private MyLib.ToolStripMyButton _saveToExcelButton;
        private System.Windows.Forms.ToolStripButton _csvExportButton;
        public MyLib.ToolStripMyButton _reportDesignButton;
    }
}
