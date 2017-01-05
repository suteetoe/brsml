namespace MyLib._databaseManage
{
    partial class _dataBackupForm
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
            this._buttonSelect = new System.Windows.Forms.Button();
            this._buttonStart = new System.Windows.Forms.Button();
            this._buttonStop = new System.Windows.Forms.Button();
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this._folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this._timerForProgress = new System.Windows.Forms.Timer(this.components);
            this._progressTextTable = new MyLib._myLabel();
            this._fileNameTextBox = new MyLib._myTextBox();
            this._myLabel2 = new MyLib._myLabel();
            this._folderTextBox = new MyLib._myTextBox();
            this._myLabel1 = new MyLib._myLabel();
            this._myToolStrip1 = new MyLib._myToolStrip();
            this._progressBarRecord = new System.Windows.Forms.ProgressBar();
            this._progressRecordText = new MyLib._myLabel();
            this._buttonClose = new System.Windows.Forms.ToolStripButton();
            this._optionButton = new System.Windows.Forms.ToolStripButton();
            this._myToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _buttonSelect
            // 
            this._buttonSelect.Location = new System.Drawing.Point(523, 39);
            this._buttonSelect.Name = "_buttonSelect";
            this._buttonSelect.Size = new System.Drawing.Size(38, 23);
            this._buttonSelect.TabIndex = 3;
            this._buttonSelect.Text = "...";
            this._buttonSelect.UseVisualStyleBackColor = true;
            this._buttonSelect.Click += new System.EventHandler(this._buttonSelect_Click);
            // 
            // _buttonStart
            // 
            this._buttonStart.Location = new System.Drawing.Point(523, 71);
            this._buttonStart.Name = "_buttonStart";
            this._buttonStart.Size = new System.Drawing.Size(71, 23);
            this._buttonStart.TabIndex = 4;
            this._buttonStart.Text = "Start";
            this._buttonStart.UseVisualStyleBackColor = true;
            this._buttonStart.Click += new System.EventHandler(this._buttonStart_Click);
            // 
            // _buttonStop
            // 
            this._buttonStop.Location = new System.Drawing.Point(523, 101);
            this._buttonStop.Name = "_buttonStop";
            this._buttonStop.Size = new System.Drawing.Size(71, 23);
            this._buttonStop.TabIndex = 5;
            this._buttonStop.Text = "Stop";
            this._buttonStop.UseVisualStyleBackColor = true;
            this._buttonStop.Click += new System.EventHandler(this._buttonStop_Click);
            // 
            // _progressBar
            // 
            this._progressBar.Location = new System.Drawing.Point(15, 101);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(505, 23);
            this._progressBar.TabIndex = 2;
            // 
            // _timerForProgress
            // 
            this._timerForProgress.Tick += new System.EventHandler(this._timerForProgress_Tick);
            // 
            // _progressTextTable
            // 
            this._progressTextTable.AutoSize = true;
            this._progressTextTable.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._progressTextTable.Location = new System.Drawing.Point(15, 127);
            this._progressTextTable.Name = "_progressTextTable";
            this._progressTextTable.ResourceName = "";
            this._progressTextTable.Size = new System.Drawing.Size(48, 19);
            this._progressTextTable.TabIndex = 8;
            this._progressTextTable.Text = "Table";
            // 
            // _fileNameTextBox
            // 
            this._fileNameTextBox._column = 0;
            this._fileNameTextBox._defaultBackGround = System.Drawing.Color.White;
            this._fileNameTextBox._emtry = true;
            this._fileNameTextBox._enterToTab = false;
            this._fileNameTextBox._icon = false;
            this._fileNameTextBox._iconNumber = 1;
            this._fileNameTextBox._isChange = false;
            this._fileNameTextBox._isGetData = false;
            this._fileNameTextBox._isQuery = true;
            this._fileNameTextBox._isSearch = false;
            this._fileNameTextBox._isTime = false;
            this._fileNameTextBox._labelName = "";
            this._fileNameTextBox._maxColumn = 0;
            this._fileNameTextBox._name = null;
            this._fileNameTextBox._row = 0;
            this._fileNameTextBox._rowCount = 0;
            this._fileNameTextBox._textFirst = "";
            this._fileNameTextBox._textLast = "";
            this._fileNameTextBox._textSecond = "";
            this._fileNameTextBox._upperCase = false;
            this._fileNameTextBox.BackColor = System.Drawing.SystemColors.Window;
            this._fileNameTextBox.Enabled = false;
            this._fileNameTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._fileNameTextBox.ForeColor = System.Drawing.Color.Black;
            this._fileNameTextBox.IsUpperCase = false;
            this._fileNameTextBox.Location = new System.Drawing.Point(162, 70);
            this._fileNameTextBox.Margin = new System.Windows.Forms.Padding(0);
            this._fileNameTextBox.MaxLength = 0;
            this._fileNameTextBox.Name = "_fileNameTextBox";
            this._fileNameTextBox.ShowIcon = false;
            this._fileNameTextBox.Size = new System.Drawing.Size(358, 22);
            this._fileNameTextBox.TabIndex = 7;
            // 
            // _myLabel2
            // 
            this._myLabel2.AutoSize = true;
            this._myLabel2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myLabel2.Location = new System.Drawing.Point(15, 71);
            this._myLabel2.Name = "_myLabel2";
            this._myLabel2.ResourceName = "";
            this._myLabel2.Size = new System.Drawing.Size(145, 19);
            this._myLabel2.TabIndex = 6;
            this._myLabel2.Text = "Backup File Name :";
            // 
            // _folderTextBox
            // 
            this._folderTextBox._column = 0;
            this._folderTextBox._defaultBackGround = System.Drawing.Color.White;
            this._folderTextBox._emtry = true;
            this._folderTextBox._enterToTab = false;
            this._folderTextBox._icon = false;
            this._folderTextBox._iconNumber = 1;
            this._folderTextBox._isChange = false;
            this._folderTextBox._isGetData = false;
            this._folderTextBox._isQuery = true;
            this._folderTextBox._isSearch = false;
            this._folderTextBox._isTime = false;
            this._folderTextBox._labelName = "";
            this._folderTextBox._maxColumn = 0;
            this._folderTextBox._name = null;
            this._folderTextBox._row = 0;
            this._folderTextBox._rowCount = 0;
            this._folderTextBox._textFirst = "";
            this._folderTextBox._textLast = "";
            this._folderTextBox._textSecond = "";
            this._folderTextBox._upperCase = false;
            this._folderTextBox.BackColor = System.Drawing.SystemColors.Window;
            this._folderTextBox.Enabled = false;
            this._folderTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._folderTextBox.ForeColor = System.Drawing.Color.Black;
            this._folderTextBox.IsUpperCase = false;
            this._folderTextBox.Location = new System.Drawing.Point(162, 39);
            this._folderTextBox.Margin = new System.Windows.Forms.Padding(0);
            this._folderTextBox.MaxLength = 0;
            this._folderTextBox.Name = "_folderTextBox";
            this._folderTextBox.ShowIcon = false;
            this._folderTextBox.Size = new System.Drawing.Size(358, 22);
            this._folderTextBox.TabIndex = 2;
            // 
            // _myLabel1
            // 
            this._myLabel1.AutoSize = true;
            this._myLabel1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myLabel1.Location = new System.Drawing.Point(12, 39);
            this._myLabel1.Name = "_myLabel1";
            this._myLabel1.ResourceName = "";
            this._myLabel1.Size = new System.Drawing.Size(148, 19);
            this._myLabel1.TabIndex = 1;
            this._myLabel1.Text = "Destination Folder :";
            // 
            // _myToolStrip1
            // 
            this._myToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonClose,
            this._optionButton});
            this._myToolStrip1.Location = new System.Drawing.Point(0, 0);
            this._myToolStrip1.Name = "_myToolStrip1";
            this._myToolStrip1.Size = new System.Drawing.Size(607, 25);
            this._myToolStrip1.TabIndex = 0;
            this._myToolStrip1.Text = "_myToolStrip1";
            // 
            // _progressBarRecord
            // 
            this._progressBarRecord.Location = new System.Drawing.Point(15, 149);
            this._progressBarRecord.Name = "_progressBarRecord";
            this._progressBarRecord.Size = new System.Drawing.Size(579, 23);
            this._progressBarRecord.TabIndex = 9;
            // 
            // _progressRecordText
            // 
            this._progressRecordText.AutoSize = true;
            this._progressRecordText.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._progressRecordText.Location = new System.Drawing.Point(15, 175);
            this._progressRecordText.Name = "_progressRecordText";
            this._progressRecordText.ResourceName = "";
            this._progressRecordText.Size = new System.Drawing.Size(65, 19);
            this._progressRecordText.TabIndex = 10;
            this._progressRecordText.Text = "Records";
            // 
            // _buttonClose
            // 
            this._buttonClose.Image = global::MyLib.Properties.Resources.error1;
            this._buttonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Size = new System.Drawing.Size(56, 22);
            this._buttonClose.Text = "Close";
            this._buttonClose.Click += new System.EventHandler(this._buttonClose_Click);
            // 
            // _optionButton
            // 
            this._optionButton.Image = global::MyLib.Resource16x16.preferences;
            this._optionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._optionButton.Name = "_optionButton";
            this._optionButton.Size = new System.Drawing.Size(64, 22);
            this._optionButton.Text = "Option";
            this._optionButton.Click += new System.EventHandler(this._optionButton_Click);
            // 
            // _dataBackupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 209);
            this.Controls.Add(this._progressRecordText);
            this.Controls.Add(this._progressBarRecord);
            this.Controls.Add(this._progressTextTable);
            this.Controls.Add(this._fileNameTextBox);
            this.Controls.Add(this._myLabel2);
            this.Controls.Add(this._buttonStop);
            this.Controls.Add(this._buttonStart);
            this.Controls.Add(this._buttonSelect);
            this.Controls.Add(this._folderTextBox);
            this.Controls.Add(this._myLabel1);
            this.Controls.Add(this._progressBar);
            this.Controls.Add(this._myToolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_dataBackupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backup";
            this._myToolStrip1.ResumeLayout(false);
            this._myToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private _myToolStrip _myToolStrip1;
        private System.Windows.Forms.ToolStripButton _buttonClose;
        private _myLabel _myLabel1;
        private _myTextBox _folderTextBox;
        private System.Windows.Forms.Button _buttonSelect;
        private System.Windows.Forms.Button _buttonStart;
        private System.Windows.Forms.Button _buttonStop;
        private System.Windows.Forms.ProgressBar _progressBar;
        private _myLabel _myLabel2;
        private _myTextBox _fileNameTextBox;
        private System.Windows.Forms.FolderBrowserDialog _folderBrowserDialog;
        private System.Windows.Forms.Timer _timerForProgress;
        private _myLabel _progressTextTable;
        private System.Windows.Forms.ProgressBar _progressBarRecord;
        private _myLabel _progressRecordText;
        private System.Windows.Forms.ToolStripButton _optionButton;
    }
}