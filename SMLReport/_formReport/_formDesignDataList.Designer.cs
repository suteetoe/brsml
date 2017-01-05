namespace SMLReport._formReport
{
    partial class _formDesignDataList
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
            this._gridData = new MyLib._myGrid();
            this._buttonHelp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this._buttonPageEnd = new System.Windows.Forms.ToolStripButton();
            this._buttonPageBegin = new System.Windows.Forms.ToolStripButton();
            this._gotoPage = new System.Windows.Forms.ToolStripTextBox();
            this._buttonClose = new System.Windows.Forms.ToolStripButton();
            this._searchTextWord = new MyLib._myLabel();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this._searchPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._searchText = new MyLib._myTextBox();
            this._searchAuto = new System.Windows.Forms.CheckBox();
            this._infoLabel = new MyLib._myLabel();
            this._buttonGotoPage = new System.Windows.Forms.ToolStripDropDownButton();
            this._buttonNext = new System.Windows.Forms.ToolStripButton();
            this._buttonPrev = new System.Windows.Forms.ToolStripButton();
            this._button = new System.Windows.Forms.ToolStrip();
            this._buttonSelectAll = new System.Windows.Forms.ToolStripButton();
            this._buttonDelete = new System.Windows.Forms.ToolStripButton();
            this._timerLoadData = new System.Windows.Forms.Timer(this.components);
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this._searchPanel.SuspendLayout();
            this._button.SuspendLayout();
            this.SuspendLayout();
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
            this._gridData.Location = new System.Drawing.Point(29, 22);
            this._gridData.Margin = new System.Windows.Forms.Padding(0);
            this._gridData.Name = "_gridData";
            this._gridData.RowOddBackground = System.Drawing.Color.Azure;
            this._gridData.Size = new System.Drawing.Size(536, 508);
            this._gridData.TabIndex = 5;
            this._gridData.TabStop = false;
            // 
            // _buttonHelp
            // 
            this._buttonHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonHelp.Image = global::SMLReport.Resource16x16.help2;
            this._buttonHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonHelp.Name = "_buttonHelp";
            this._buttonHelp.Size = new System.Drawing.Size(30, 20);
            this._buttonHelp.Text = "Help";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(30, 6);
            // 
            // _buttonPageEnd
            // 
            this._buttonPageEnd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonPageEnd.Image = global::SMLReport.Properties.Resources.nav_down_blue;
            this._buttonPageEnd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonPageEnd.Name = "_buttonPageEnd";
            this._buttonPageEnd.Size = new System.Drawing.Size(30, 20);
            this._buttonPageEnd.Text = "Buttom";
            // 
            // _buttonPageBegin
            // 
            this._buttonPageBegin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonPageBegin.Image = global::SMLReport.Properties.Resources.nav_up_blue;
            this._buttonPageBegin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonPageBegin.Name = "_buttonPageBegin";
            this._buttonPageBegin.Size = new System.Drawing.Size(30, 20);
            this._buttonPageBegin.Text = "Top";
            // 
            // _gotoPage
            // 
            this._gotoPage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gotoPage.Name = "_gotoPage";
            this._gotoPage.Size = new System.Drawing.Size(100, 22);
            this._gotoPage.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _buttonClose
            // 
            this._buttonClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonClose.Image = global::SMLReport.Resource16x16.error;
            this._buttonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Size = new System.Drawing.Size(30, 20);
            this._buttonClose.Text = "Close this Screen";
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
            this._searchTextWord.Size = new System.Drawing.Size(82, 18);
            this._searchTextWord.TabIndex = 0;
            this._searchTextWord.Text = "ข้อความค้นหา";
            this._searchTextWord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _timer
            // 
            this._timer.Interval = 1000;
            // 
            // _searchPanel
            // 
            this._searchPanel.AutoSize = true;
            this._searchPanel.BackColor = System.Drawing.Color.MintCream;
            this._searchPanel.Controls.Add(this._searchTextWord);
            this._searchPanel.Controls.Add(this._searchText);
            this._searchPanel.Controls.Add(this._searchAuto);
            this._searchPanel.Controls.Add(this._infoLabel);
            this._searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._searchPanel.Location = new System.Drawing.Point(29, 0);
            this._searchPanel.Margin = new System.Windows.Forms.Padding(0);
            this._searchPanel.Name = "_searchPanel";
            this._searchPanel.Size = new System.Drawing.Size(536, 22);
            this._searchPanel.TabIndex = 7;
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
            this._searchText.Location = new System.Drawing.Point(86, 0);
            this._searchText.Margin = new System.Windows.Forms.Padding(0);
            this._searchText.MaxLength = 0;
            this._searchText.Name = "_searchText";
            this._searchText.ShowIcon = false;
            this._searchText.Size = new System.Drawing.Size(154, 22);
            this._searchText.TabIndex = 1;
            this._searchText.TabStop = false;
            // 
            // _searchAuto
            // 
            this._searchAuto.AutoSize = true;
            this._searchAuto.BackColor = System.Drawing.Color.Transparent;
            this._searchAuto.Dock = System.Windows.Forms.DockStyle.Right;
            this._searchAuto.Location = new System.Drawing.Point(242, 2);
            this._searchAuto.Margin = new System.Windows.Forms.Padding(2);
            this._searchAuto.Name = "_searchAuto";
            this._searchAuto.Size = new System.Drawing.Size(48, 18);
            this._searchAuto.TabIndex = 2;
            this._searchAuto.TabStop = false;
            this._searchAuto.Text = "Auto";
            this._searchAuto.UseVisualStyleBackColor = false;
            // 
            // _infoLabel
            // 
            this._infoLabel.AutoSize = true;
            this._infoLabel.BackColor = System.Drawing.Color.Transparent;
            this._infoLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this._infoLabel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this._infoLabel.Location = new System.Drawing.Point(294, 2);
            this._infoLabel.Margin = new System.Windows.Forms.Padding(2);
            this._infoLabel.Name = "_infoLabel";
            this._infoLabel.ResourceName = "";
            this._infoLabel.Size = new System.Drawing.Size(0, 18);
            this._infoLabel.TabIndex = 3;
            this._infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _buttonGotoPage
            // 
            this._buttonGotoPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonGotoPage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._gotoPage});
            this._buttonGotoPage.Image = global::SMLReport.Resource16x16.navigate_right2;
            this._buttonGotoPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonGotoPage.Name = "_buttonGotoPage";
            this._buttonGotoPage.Size = new System.Drawing.Size(28, 20);
            this._buttonGotoPage.Text = "Goto page number";
            // 
            // _buttonNext
            // 
            this._buttonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonNext.Image = global::SMLReport.Properties.Resources.nav_right_blue;
            this._buttonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonNext.Name = "_buttonNext";
            this._buttonNext.Size = new System.Drawing.Size(30, 20);
            this._buttonNext.Text = "Next";
            // 
            // _buttonPrev
            // 
            this._buttonPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonPrev.Image = global::SMLReport.Properties.Resources.nav_left_blue;
            this._buttonPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonPrev.Name = "_buttonPrev";
            this._buttonPrev.Size = new System.Drawing.Size(30, 20);
            this._buttonPrev.Text = "Prev";
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
            this._buttonSelectAll,
            this._buttonDelete,
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
            this._button.Size = new System.Drawing.Size(29, 530);
            this._button.TabIndex = 6;
            this._button.Text = "Tool Bar";
            // 
            // _buttonSelectAll
            // 
            this._buttonSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonSelectAll.Image = global::SMLReport.Resource16x16.preferences1;
            this._buttonSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSelectAll.Name = "_buttonSelectAll";
            this._buttonSelectAll.Size = new System.Drawing.Size(30, 20);
            this._buttonSelectAll.Text = "Select records";
            // 
            // _buttonDelete
            // 
            this._buttonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._buttonDelete.Image = global::SMLReport.Properties.Resources.document_delete;
            this._buttonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonDelete.Name = "_buttonDelete";
            this._buttonDelete.Size = new System.Drawing.Size(30, 20);
            this._buttonDelete.Text = "Delete selected records";
            // 
            // _timerLoadData
            // 
            this._timerLoadData.Interval = 1000;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(30, 6);
            // 
            // _formDesignDataList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._gridData);
            this.Controls.Add(this._searchPanel);
            this.Controls.Add(this._button);
            this.Name = "_formDesignDataList";
            this.Size = new System.Drawing.Size(565, 530);
            this._searchPanel.ResumeLayout(false);
            this._searchPanel.PerformLayout();
            this._button.ResumeLayout(false);
            this._button.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public MyLib._myGrid _gridData;
        public System.Windows.Forms.ToolStripButton _buttonHelp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        public System.Windows.Forms.ToolStripButton _buttonPageEnd;
        public System.Windows.Forms.ToolStripButton _buttonPageBegin;
        private System.Windows.Forms.ToolStripTextBox _gotoPage;
        public System.Windows.Forms.ToolStripButton _buttonClose;
        private MyLib._myLabel _searchTextWord;
        private System.Windows.Forms.Timer _timer;
        public System.Windows.Forms.FlowLayoutPanel _searchPanel;
        public MyLib._myTextBox _searchText;
        private System.Windows.Forms.CheckBox _searchAuto;
        private MyLib._myLabel _infoLabel;
        public System.Windows.Forms.ToolStripDropDownButton _buttonGotoPage;
        public System.Windows.Forms.ToolStripButton _buttonNext;
        public System.Windows.Forms.ToolStripButton _buttonPrev;
        public System.Windows.Forms.ToolStrip _button;
        public System.Windows.Forms.ToolStripButton _buttonSelectAll;
        public System.Windows.Forms.ToolStripButton _buttonDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Timer _timerLoadData;


    }
}
