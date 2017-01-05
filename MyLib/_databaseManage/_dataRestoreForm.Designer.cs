namespace MyLib._databaseManage
{
    partial class _dataRestoreForm
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._buttonStop = new System.Windows.Forms.Button();
            this._buttonStart = new System.Windows.Forms.Button();
            this._buttonSelect = new System.Windows.Forms.Button();
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this._fileNameTextBox = new MyLib._myTextBox();
            this._myLabel2 = new MyLib._myLabel();
            this._timerForProcess = new System.Windows.Forms.Timer(this.components);
            this._progressTextTable = new MyLib._myLabel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(607, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::MyLib.Properties.Resources.error1;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "";
            this._closeButton.Size = new System.Drawing.Size(58, 22);
            this._closeButton.Text = "Close";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _buttonStop
            // 
            this._buttonStop.Location = new System.Drawing.Point(523, 84);
            this._buttonStop.Name = "_buttonStop";
            this._buttonStop.Size = new System.Drawing.Size(71, 23);
            this._buttonStop.TabIndex = 16;
            this._buttonStop.Text = "Stop";
            this._buttonStop.UseVisualStyleBackColor = true;
            // 
            // _buttonStart
            // 
            this._buttonStart.Location = new System.Drawing.Point(523, 54);
            this._buttonStart.Name = "_buttonStart";
            this._buttonStart.Size = new System.Drawing.Size(71, 23);
            this._buttonStart.TabIndex = 15;
            this._buttonStart.Text = "Start";
            this._buttonStart.UseVisualStyleBackColor = true;
            this._buttonStart.Click += new System.EventHandler(this._buttonStart_Click);
            // 
            // _buttonSelect
            // 
            this._buttonSelect.Location = new System.Drawing.Point(523, 25);
            this._buttonSelect.Name = "_buttonSelect";
            this._buttonSelect.Size = new System.Drawing.Size(38, 23);
            this._buttonSelect.TabIndex = 14;
            this._buttonSelect.Text = "...";
            this._buttonSelect.UseVisualStyleBackColor = true;
            this._buttonSelect.Click += new System.EventHandler(this._buttonSelect_Click);
            // 
            // _progressBar
            // 
            this._progressBar.Location = new System.Drawing.Point(15, 54);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(505, 23);
            this._progressBar.TabIndex = 13;
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
            this._fileNameTextBox.Location = new System.Drawing.Point(162, 26);
            this._fileNameTextBox.Margin = new System.Windows.Forms.Padding(0);
            this._fileNameTextBox.MaxLength = 0;
            this._fileNameTextBox.Name = "_fileNameTextBox";
            this._fileNameTextBox.ShowIcon = false;
            this._fileNameTextBox.Size = new System.Drawing.Size(358, 22);
            this._fileNameTextBox.TabIndex = 18;
            // 
            // _myLabel2
            // 
            this._myLabel2.AutoSize = true;
            this._myLabel2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myLabel2.Location = new System.Drawing.Point(15, 27);
            this._myLabel2.Name = "_myLabel2";
            this._myLabel2.ResourceName = "";
            this._myLabel2.Size = new System.Drawing.Size(145, 19);
            this._myLabel2.TabIndex = 17;
            this._myLabel2.Text = "Backup File Name :";
            // 
            // _timerForProcess
            // 
            this._timerForProcess.Tick += new System.EventHandler(this._timerForProcess_Tick);
            // 
            // _progressTextTable
            // 
            this._progressTextTable.AutoSize = true;
            this._progressTextTable.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._progressTextTable.Location = new System.Drawing.Point(15, 80);
            this._progressTextTable.Name = "_progressTextTable";
            this._progressTextTable.ResourceName = "";
            this._progressTextTable.Size = new System.Drawing.Size(0, 19);
            this._progressTextTable.TabIndex = 19;
            // 
            // _dataRestoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 122);
            this.Controls.Add(this._progressTextTable);
            this.Controls.Add(this._fileNameTextBox);
            this.Controls.Add(this._myLabel2);
            this.Controls.Add(this._buttonStop);
            this.Controls.Add(this._buttonStart);
            this.Controls.Add(this._buttonSelect);
            this.Controls.Add(this._progressBar);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_dataRestoreForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Restore";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private ToolStripMyButton _closeButton;
        private _myTextBox _fileNameTextBox;
        private _myLabel _myLabel2;
        private System.Windows.Forms.Button _buttonStop;
        private System.Windows.Forms.Button _buttonStart;
        private System.Windows.Forms.Button _buttonSelect;
        private System.Windows.Forms.ProgressBar _progressBar;
        private System.Windows.Forms.Timer _timerForProcess;
        private _myLabel _progressTextTable;
    }
}