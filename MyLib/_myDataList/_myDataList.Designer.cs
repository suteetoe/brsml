namespace MyLib
{
	partial class _myDataList
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
            this._button = new System.Windows.Forms.ToolStrip();
            this._flowButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._buttonNew = new System.Windows.Forms.ToolStripButton();
            this._buttonNewFromTemp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._buttonSelectAll = new System.Windows.Forms.ToolStripButton();
            this._buttonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this._buttonLockDoc = new MyLib.ToolStripMyButton();
            this._buttonUnlockDoc = new MyLib.ToolStripMyButton();
            this._separatorLockDoc = new System.Windows.Forms.ToolStripSeparator();
            this._docPictureButton = new System.Windows.Forms.ToolStripButton();
            this._printRangeButton = new MyLib.ToolStripMyButton();
            this._clearLogPrintButton = new MyLib.ToolStripMyButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this._buttonPrev = new System.Windows.Forms.ToolStripButton();
            this._buttonNext = new System.Windows.Forms.ToolStripButton();
            this._buttonGotoPage = new System.Windows.Forms.ToolStripDropDownButton();
            this._gotoPage = new System.Windows.Forms.ToolStripTextBox();
            this._buttonPageBegin = new System.Windows.Forms.ToolStripButton();
            this._buttonPageEnd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this._buttonHelp = new System.Windows.Forms.ToolStripButton();
            this._buttonClose = new System.Windows.Forms.ToolStripButton();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this._searchAuto = new System.Windows.Forms.CheckBox();
            this._searchPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._searchTextWord = new MyLib._myLabel();
            this._searchText = new MyLib._myTextBox();
            this._infoLabel = new MyLib._myLabel();
            this._filterButton = new System.Windows.Forms.Button();
            this._timerLoadData = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._selectSuccessButton = new System.Windows.Forms.ToolStripButton();
            this._clearButton = new System.Windows.Forms.ToolStripButton();
            this._styleComboBox = new System.Windows.Forms.ToolStripComboBox();
            this._multiPanel = new System.Windows.Forms.Panel();
            this._selectedGrid = new MyLib._myGrid();
            this._gridData = new MyLib._myGrid();
            this._button.SuspendLayout();
            this._searchPanel.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this._multiPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _button
            // 
            this._button.AllowItemReorder = true;
            this._button.BackColor = System.Drawing.Color.WhiteSmoke;
            this._button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._button.Dock = System.Windows.Forms.DockStyle.Left;
            this._button.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._button.GripMargin = new System.Windows.Forms.Padding(0);
            this._button.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._button.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._flowButton,
            this.toolStripSeparator2,
            this._buttonNew,
            this._buttonNewFromTemp,
            this.toolStripSeparator3,
            this._buttonSelectAll,
            this._buttonDelete,
            this.toolStripSeparator6,
            this._buttonLockDoc,
            this._buttonUnlockDoc,
            this._separatorLockDoc,
            this._docPictureButton,
            this._printRangeButton,
            this._clearLogPrintButton,
            this.toolStripSeparator4,
            this._buttonPrev,
            this._buttonNext,
            this._buttonGotoPage,
            this._buttonPageBegin,
            this._buttonPageEnd,
            this.toolStripSeparator5,
            this._buttonHelp,
            this._buttonClose});
            this._button.Location = new System.Drawing.Point(0, 0);
            this._button.Name = "_button";
            this._button.Padding = new System.Windows.Forms.Padding(0);
            this._button.Size = new System.Drawing.Size(31, 437);
            this._button.TabIndex = 3;
            this._button.Text = "Tool Bar";
            this._button.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this._button_ItemClicked);
            // 
            // _flowButton
            // 
            this._flowButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._flowButton.Image = global::MyLib.Properties.Resources.branch_element;
            this._flowButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._flowButton.Name = "_flowButton";
            this._flowButton.Size = new System.Drawing.Size(30, 20);
            this._flowButton.Text = "Flow";
            this._flowButton.Click += new System.EventHandler(this._flowButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(30, 6);
            // 
            // _buttonNew
            // 
            this._buttonNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonNew.Image = global::MyLib.Resource16x16.document_plain_new;
            this._buttonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonNew.Name = "_buttonNew";
            this._buttonNew.Size = new System.Drawing.Size(30, 20);
            this._buttonNew.Text = "New Record";
            this._buttonNew.Click += new System.EventHandler(this._buttonNew_Click);
            // 
            // _buttonNewFromTemp
            // 
            this._buttonNewFromTemp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonNewFromTemp.Image = global::MyLib.Resource16x16.document_plain;
            this._buttonNewFromTemp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonNewFromTemp.Name = "_buttonNewFromTemp";
            this._buttonNewFromTemp.Size = new System.Drawing.Size(30, 20);
            this._buttonNewFromTemp.Text = "New Data from this";
            this._buttonNewFromTemp.Click += new System.EventHandler(this._buttonNewFromTemp_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(30, 6);
            // 
            // _buttonSelectAll
            // 
            this._buttonSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonSelectAll.Image = global::MyLib.Resource16x16.preferences;
            this._buttonSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSelectAll.Name = "_buttonSelectAll";
            this._buttonSelectAll.Size = new System.Drawing.Size(30, 20);
            this._buttonSelectAll.Text = "Select records";
            this._buttonSelectAll.Click += new System.EventHandler(this._buttonSelectAll_Click);
            // 
            // _buttonDelete
            // 
            this._buttonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonDelete.Image = global::MyLib.Resource16x16.document_delete;
            this._buttonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonDelete.Name = "_buttonDelete";
            this._buttonDelete.Size = new System.Drawing.Size(30, 20);
            this._buttonDelete.Text = "Delete selected records";
            this._buttonDelete.Click += new System.EventHandler(this._buttonDelete_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(30, 6);
            // 
            // _buttonLockDoc
            // 
            this._buttonLockDoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonLockDoc.Image = global::MyLib.Resource16x16.document_lock;
            this._buttonLockDoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonLockDoc.Name = "_buttonLockDoc";
            this._buttonLockDoc.Padding = new System.Windows.Forms.Padding(1);
            this._buttonLockDoc.ResourceName = "";
            this._buttonLockDoc.Size = new System.Drawing.Size(30, 22);
            this._buttonLockDoc.Text = "toolStripMyButton1";
            this._buttonLockDoc.ToolTipText = "Lock Record";
            this._buttonLockDoc.Visible = false;
            // 
            // _buttonUnlockDoc
            // 
            this._buttonUnlockDoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonUnlockDoc.Image = global::MyLib.Resource16x16.document_into1;
            this._buttonUnlockDoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonUnlockDoc.Name = "_buttonUnlockDoc";
            this._buttonUnlockDoc.Padding = new System.Windows.Forms.Padding(1);
            this._buttonUnlockDoc.ResourceName = "";
            this._buttonUnlockDoc.Size = new System.Drawing.Size(30, 22);
            this._buttonUnlockDoc.Text = "toolStripMyButton2";
            this._buttonUnlockDoc.ToolTipText = "Unlock Record";
            this._buttonUnlockDoc.Visible = false;
            // 
            // _separatorLockDoc
            // 
            this._separatorLockDoc.Name = "_separatorLockDoc";
            this._separatorLockDoc.Size = new System.Drawing.Size(30, 6);
            this._separatorLockDoc.Visible = false;
            // 
            // _docPictureButton
            // 
            this._docPictureButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._docPictureButton.Image = global::MyLib.Resource16x16.text_rich_colored;
            this._docPictureButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._docPictureButton.Name = "_docPictureButton";
            this._docPictureButton.Size = new System.Drawing.Size(30, 20);
            this._docPictureButton.Text = "Document Picture";
            this._docPictureButton.Click += new System.EventHandler(this._docPictureButton_Click);
            // 
            // _printRangeButton
            // 
            this._printRangeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._printRangeButton.Image = global::MyLib.Properties.Resources.printer;
            this._printRangeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._printRangeButton.Name = "_printRangeButton";
            this._printRangeButton.Padding = new System.Windows.Forms.Padding(1);
            this._printRangeButton.ResourceName = "พิมพ์เอกสารที่เลือก";
            this._printRangeButton.Size = new System.Drawing.Size(30, 22);
            this._printRangeButton.Text = "พิมพ์เอกสารที่เลือก";
            this._printRangeButton.Visible = false;
            this._printRangeButton.Click += new System.EventHandler(this._printRangeButton_Click);
            // 
            // _clearLogPrintButton
            // 
            this._clearLogPrintButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._clearLogPrintButton.Image = global::MyLib.Resource16x16.printer_error;
            this._clearLogPrintButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._clearLogPrintButton.Name = "_clearLogPrintButton";
            this._clearLogPrintButton.Padding = new System.Windows.Forms.Padding(1);
            this._clearLogPrintButton.ResourceName = "ล้างประวัติการพิมพ์";
            this._clearLogPrintButton.Size = new System.Drawing.Size(30, 22);
            this._clearLogPrintButton.Text = "ล้างประวัติการพิมพ์";
            this._clearLogPrintButton.Visible = false;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(30, 6);
            // 
            // _buttonPrev
            // 
            this._buttonPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonPrev.Image = global::MyLib.Resource16x16.nav_left_blue;
            this._buttonPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonPrev.Name = "_buttonPrev";
            this._buttonPrev.Size = new System.Drawing.Size(30, 20);
            this._buttonPrev.Text = "Prev";
            this._buttonPrev.Click += new System.EventHandler(this._buttonPrev_Click);
            // 
            // _buttonNext
            // 
            this._buttonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonNext.Image = global::MyLib.Resource16x16.nav_right_blue;
            this._buttonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonNext.Name = "_buttonNext";
            this._buttonNext.Size = new System.Drawing.Size(30, 20);
            this._buttonNext.Text = "Next";
            this._buttonNext.Click += new System.EventHandler(this._buttonNext_Click_1);
            // 
            // _buttonGotoPage
            // 
            this._buttonGotoPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonGotoPage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._gotoPage});
            this._buttonGotoPage.Image = global::MyLib.Resource16x16.navigate_right2;
            this._buttonGotoPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonGotoPage.Name = "_buttonGotoPage";
            this._buttonGotoPage.Size = new System.Drawing.Size(30, 20);
            this._buttonGotoPage.Text = "Goto page number";
            // 
            // _gotoPage
            // 
            this._gotoPage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gotoPage.Name = "_gotoPage";
            this._gotoPage.Size = new System.Drawing.Size(100, 22);
            this._gotoPage.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _buttonPageBegin
            // 
            this._buttonPageBegin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonPageBegin.Image = global::MyLib.Resource16x16.nav_up_blue1;
            this._buttonPageBegin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonPageBegin.Name = "_buttonPageBegin";
            this._buttonPageBegin.Size = new System.Drawing.Size(30, 20);
            this._buttonPageBegin.Text = "Top";
            this._buttonPageBegin.Click += new System.EventHandler(this._buttonPageBegin_Click);
            // 
            // _buttonPageEnd
            // 
            this._buttonPageEnd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonPageEnd.Image = global::MyLib.Resource16x16.nav_down_blue1;
            this._buttonPageEnd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonPageEnd.Name = "_buttonPageEnd";
            this._buttonPageEnd.Size = new System.Drawing.Size(30, 20);
            this._buttonPageEnd.Text = "Buttom";
            this._buttonPageEnd.Click += new System.EventHandler(this._buttonPageEnd_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(30, 6);
            // 
            // _buttonHelp
            // 
            this._buttonHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonHelp.Image = global::MyLib.Resource16x16.help;
            this._buttonHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonHelp.Name = "_buttonHelp";
            this._buttonHelp.Size = new System.Drawing.Size(30, 20);
            this._buttonHelp.Text = "Help";
            // 
            // _buttonClose
            // 
            this._buttonClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonClose.Image = global::MyLib.Resource16x16.error1;
            this._buttonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Size = new System.Drawing.Size(23, 20);
            this._buttonClose.Text = "Close this Screen";
            this._buttonClose.Click += new System.EventHandler(this._buttonClose_Click_1);
            // 
            // _timer
            // 
            this._timer.Interval = 500;
            this._timer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // _searchAuto
            // 
            this._searchAuto.AutoSize = true;
            this._searchAuto.BackColor = System.Drawing.Color.Transparent;
            this._searchAuto.Dock = System.Windows.Forms.DockStyle.Right;
            this._searchAuto.Location = new System.Drawing.Point(266, 2);
            this._searchAuto.Margin = new System.Windows.Forms.Padding(2);
            this._searchAuto.Name = "_searchAuto";
            this._searchAuto.Size = new System.Drawing.Size(53, 20);
            this._searchAuto.TabIndex = 2;
            this._searchAuto.TabStop = false;
            this._searchAuto.Text = "Auto";
            this._searchAuto.UseVisualStyleBackColor = false;
            // 
            // _searchPanel
            // 
            this._searchPanel.AutoSize = true;
            this._searchPanel.BackColor = System.Drawing.Color.MintCream;
            this._searchPanel.Controls.Add(this._searchTextWord);
            this._searchPanel.Controls.Add(this._searchText);
            this._searchPanel.Controls.Add(this._searchAuto);
            this._searchPanel.Controls.Add(this._infoLabel);
            this._searchPanel.Controls.Add(this._filterButton);
            this._searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._searchPanel.Location = new System.Drawing.Point(31, 0);
            this._searchPanel.Margin = new System.Windows.Forms.Padding(0);
            this._searchPanel.Name = "_searchPanel";
            this._searchPanel.Size = new System.Drawing.Size(898, 24);
            this._searchPanel.TabIndex = 4;
            // 
            // _searchTextWord
            // 
            this._searchTextWord.AutoSize = true;
            this._searchTextWord.BackColor = System.Drawing.Color.Transparent;
            this._searchTextWord.Dock = System.Windows.Forms.DockStyle.Left;
            this._searchTextWord.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._searchTextWord.Location = new System.Drawing.Point(2, 2);
            this._searchTextWord.Margin = new System.Windows.Forms.Padding(2);
            this._searchTextWord.Name = "_searchTextWord";
            this._searchTextWord.ResourceName = "text_for_search";
            this._searchTextWord.Size = new System.Drawing.Size(106, 20);
            this._searchTextWord.TabIndex = 0;
            this._searchTextWord.Text = "text_for_search";
            this._searchTextWord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _searchText
            // 
            this._searchText._column = 0;
            this._searchText._defaultBackGround = System.Drawing.Color.White;
            this._searchText._emtry = true;
            this._searchText._enterToTab = false;
            this._searchText._icon = false;
            this._searchText._iconNumber = 1;
            this._searchText._isChange = false;
            this._searchText._isGetData = false;
            this._searchText._isQuery = true;
            this._searchText._isSearch = false;
            this._searchText._isTime = false;
            this._searchText._labelName = "";
            this._searchText._maxColumn = 0;
            this._searchText._name = null;
            this._searchText._row = 0;
            this._searchText._rowCount = 0;
            this._searchText._textFirst = "";
            this._searchText._textLast = "";
            this._searchText._textSecond = "";
            this._searchText._upperCase = false;
            this._searchText.BackColor = System.Drawing.SystemColors.Window;
            this._searchText.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._searchText.ForeColor = System.Drawing.Color.Black;
            this._searchText.IsUpperCase = false;
            this._searchText.Location = new System.Drawing.Point(110, 0);
            this._searchText.Margin = new System.Windows.Forms.Padding(0);
            this._searchText.MaxLength = 0;
            this._searchText.Name = "_searchText";
            this._searchText.ShowIcon = false;
            this._searchText.Size = new System.Drawing.Size(154, 22);
            this._searchText.TabIndex = 1;
            this._searchText.TabStop = false;
            // 
            // _infoLabel
            // 
            this._infoLabel.AutoSize = true;
            this._infoLabel.BackColor = System.Drawing.Color.Transparent;
            this._infoLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this._infoLabel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this._infoLabel.Location = new System.Drawing.Point(323, 2);
            this._infoLabel.Margin = new System.Windows.Forms.Padding(2);
            this._infoLabel.Name = "_infoLabel";
            this._infoLabel.ResourceName = "";
            this._infoLabel.Size = new System.Drawing.Size(0, 20);
            this._infoLabel.TabIndex = 3;
            this._infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _filterButton
            // 
            this._filterButton.AutoSize = true;
            this._filterButton.Image = global::MyLib.Properties.Resources.zoom_in;
            this._filterButton.Location = new System.Drawing.Point(325, 0);
            this._filterButton.Margin = new System.Windows.Forms.Padding(0);
            this._filterButton.Name = "_filterButton";
            this._filterButton.Size = new System.Drawing.Size(25, 24);
            this._filterButton.TabIndex = 4;
            this._filterButton.UseVisualStyleBackColor = true;
            this._filterButton.Click += new System.EventHandler(this._filterButton_Click);
            // 
            // _timerLoadData
            // 
            this._timerLoadData.Interval = 1000;
            this._timerLoadData.Tick += new System.EventHandler(this._timerLoadData_Tick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._selectSuccessButton,
            this._clearButton,
            this._styleComboBox});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(276, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _selectSuccessButton
            // 
            this._selectSuccessButton.Image = global::MyLib.Properties.Resources.flash1;
            this._selectSuccessButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectSuccessButton.Name = "_selectSuccessButton";
            this._selectSuccessButton.Size = new System.Drawing.Size(51, 22);
            this._selectSuccessButton.Text = "Save";
            // 
            // _clearButton
            // 
            this._clearButton.Image = global::MyLib.Properties.Resources.error1;
            this._clearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._clearButton.Name = "_clearButton";
            this._clearButton.Size = new System.Drawing.Size(54, 22);
            this._clearButton.Text = "Clear";
            this._clearButton.Click += new System.EventHandler(this._clearButton_Click);
            // 
            // _styleComboBox
            // 
            this._styleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._styleComboBox.Items.AddRange(new object[] {
            "Move Down",
            "Move Left"});
            this._styleComboBox.Name = "_styleComboBox";
            this._styleComboBox.Size = new System.Drawing.Size(121, 25);
            // 
            // _multiPanel
            // 
            this._multiPanel.Controls.Add(this._selectedGrid);
            this._multiPanel.Controls.Add(this.toolStrip1);
            this._multiPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this._multiPanel.Location = new System.Drawing.Point(653, 24);
            this._multiPanel.Name = "_multiPanel";
            this._multiPanel.Size = new System.Drawing.Size(276, 413);
            this._multiPanel.TabIndex = 0;
            // 
            // _selectedGrid
            // 
            this._selectedGrid._extraWordShow = true;
            this._selectedGrid._selectRow = -1;
            this._selectedGrid.BackColor = System.Drawing.Color.WhiteSmoke;
            this._selectedGrid.ColumnBackground = System.Drawing.Color.White;
            this._selectedGrid.ColumnBackgroundAuto = false;
            this._selectedGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._selectedGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._selectedGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._selectedGrid.Location = new System.Drawing.Point(0, 25);
            this._selectedGrid.Margin = new System.Windows.Forms.Padding(0);
            this._selectedGrid.Name = "_selectedGrid";
            this._selectedGrid.RowOddBackground = System.Drawing.Color.Azure;
            this._selectedGrid.Size = new System.Drawing.Size(276, 388);
            this._selectedGrid.TabIndex = 3;
            this._selectedGrid.TabStop = false;
            // 
            // _gridData
            // 
            this._gridData._extraWordShow = true;
            this._gridData._selectRow = -1;
            this._gridData.BackColor = System.Drawing.Color.WhiteSmoke;
            this._gridData.ColumnBackground = System.Drawing.Color.White;
            this._gridData.ColumnBackgroundAuto = false;
            this._gridData.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._gridData.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridData.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridData.IsEdit = false;
            this._gridData.Location = new System.Drawing.Point(31, 24);
            this._gridData.Margin = new System.Windows.Forms.Padding(0);
            this._gridData.Name = "_gridData";
            this._gridData.RowOddBackground = System.Drawing.Color.Azure;
            this._gridData.Size = new System.Drawing.Size(622, 413);
            this._gridData.TabIndex = 2;
            this._gridData.TabStop = false;
            // 
            // _myDataList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this._gridData);
            this.Controls.Add(this._multiPanel);
            this.Controls.Add(this._searchPanel);
            this.Controls.Add(this._button);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "_myDataList";
            this.Size = new System.Drawing.Size(929, 437);
            this._button.ResumeLayout(false);
            this._button.PerformLayout();
            this._searchPanel.ResumeLayout(false);
            this._searchPanel.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this._multiPanel.ResumeLayout(false);
            this._multiPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.ToolStrip _button;
		private System.Windows.Forms.Timer _timer;
		private _myLabel _infoLabel;
		private _myLabel _searchTextWord;
		public _myGrid _gridData;
        private System.Windows.Forms.ToolStripTextBox _gotoPage;
		public System.Windows.Forms.ToolStripButton _buttonNew;
		public System.Windows.Forms.ToolStripButton _buttonNewFromTemp;
		public System.Windows.Forms.ToolStripButton _buttonSelectAll;
		public System.Windows.Forms.ToolStripButton _buttonDelete;
		public System.Windows.Forms.ToolStripButton _buttonPrev;
		public System.Windows.Forms.ToolStripButton _buttonNext;
		public System.Windows.Forms.ToolStripDropDownButton _buttonGotoPage;
		public System.Windows.Forms.ToolStripButton _buttonPageBegin;
		public System.Windows.Forms.ToolStripButton _buttonPageEnd;
		public System.Windows.Forms.ToolStripButton _buttonHelp;
		public System.Windows.Forms.ToolStripButton _buttonClose;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        public _myTextBox _searchText;
        private System.Windows.Forms.CheckBox _searchAuto;
        public System.Windows.Forms.FlowLayoutPanel _searchPanel;
        private System.Windows.Forms.Timer _timerLoadData;
        public System.Windows.Forms.ToolStripButton _flowButton;
        public _myGrid _selectedGrid;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox _styleComboBox;
        private System.Windows.Forms.ToolStripButton _clearButton;
        public System.Windows.Forms.ToolStripButton _selectSuccessButton;
        private System.Windows.Forms.Panel _multiPanel;
        private System.Windows.Forms.Button _filterButton;
        public System.Windows.Forms.ToolStripButton _docPictureButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        public ToolStripMyButton _buttonLockDoc;
        public ToolStripMyButton _buttonUnlockDoc;
        public System.Windows.Forms.ToolStripSeparator _separatorLockDoc;
        public ToolStripMyButton _printRangeButton;
        public ToolStripMyButton _clearLogPrintButton;
    }
}
