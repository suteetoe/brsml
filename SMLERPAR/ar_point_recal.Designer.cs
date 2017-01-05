namespace SMLERPAR
{
    partial class ar_point_recal
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
            this._processButton = new MyLib.VistaButton();
            this._closeButton = new MyLib.VistaButton();
            this._resultTextbox = new System.Windows.Forms.TextBox();
            this._fromDocDate = new MyLib._myDateBox();
            this._toDocDate = new MyLib._myDateBox();
            this._myLabel1 = new MyLib._myLabel();
            this._myLabel2 = new MyLib._myLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this._stopButton = new MyLib.VistaButton();
            this.SuspendLayout();
            // 
            // _processButton
            // 
            this._processButton._drawNewMethod = false;
            this._processButton.BackColor = System.Drawing.Color.Transparent;
            this._processButton.ButtonText = "Process";
            this._processButton.Location = new System.Drawing.Point(15, 79);
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(117, 34);
            this._processButton.TabIndex = 0;
            this._processButton.UseVisualStyleBackColor = false;
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton._drawNewMethod = false;
            this._closeButton.BackColor = System.Drawing.Color.Transparent;
            this._closeButton.ButtonText = "Close";
            this._closeButton.Location = new System.Drawing.Point(261, 79);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(117, 34);
            this._closeButton.TabIndex = 1;
            this._closeButton.Text = "vistaButton2";
            this._closeButton.UseVisualStyleBackColor = false;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _resultTextbox
            // 
            this._resultTextbox.Location = new System.Drawing.Point(15, 51);
            this._resultTextbox.Name = "_resultTextbox";
            this._resultTextbox.Size = new System.Drawing.Size(410, 22);
            this._resultTextbox.TabIndex = 2;
            // 
            // _fromDocDate
            // 
            this._fromDocDate._column = 0;
            this._fromDocDate._dateTime = new System.DateTime(1000, 1, 1, 0, 0, 0, 0);
            this._fromDocDate._dateTimeOld = new System.DateTime(1000, 1, 1, 0, 0, 0, 0);
            this._fromDocDate._defaultBackGround = System.Drawing.Color.White;
            this._fromDocDate._emtry = true;
            this._fromDocDate._enterToTab = false;
            this._fromDocDate._icon = true;
            this._fromDocDate._iconNumber = 1;
            this._fromDocDate._isChange = false;
            this._fromDocDate._isGetData = false;
            this._fromDocDate._isQuery = true;
            this._fromDocDate._isSearch = false;
            this._fromDocDate._isTime = false;
            this._fromDocDate._labelName = "";
            this._fromDocDate._lostFocust = true;
            this._fromDocDate._maxColumn = 0;
            this._fromDocDate._name = null;
            this._fromDocDate._row = 0;
            this._fromDocDate._rowCount = 0;
            this._fromDocDate._textFirst = "";
            this._fromDocDate._textLast = "";
            this._fromDocDate._textSecond = "";
            this._fromDocDate._upperCase = false;
            this._fromDocDate._warning = true;
            this._fromDocDate.BackColor = System.Drawing.Color.Transparent;
            this._fromDocDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._fromDocDate.ForeColor = System.Drawing.Color.Black;
            this._fromDocDate.IsUpperCase = false;
            this._fromDocDate.Location = new System.Drawing.Point(112, 14);
            this._fromDocDate.Margin = new System.Windows.Forms.Padding(0);
            this._fromDocDate.MaxLength = 0;
            this._fromDocDate.Name = "_fromDocDate";
            this._fromDocDate.ShowIcon = true;
            this._fromDocDate.Size = new System.Drawing.Size(218, 22);
            this._fromDocDate.TabIndex = 3;
            // 
            // _toDocDate
            // 
            this._toDocDate._column = 0;
            this._toDocDate._dateTime = new System.DateTime(1000, 1, 1, 0, 0, 0, 0);
            this._toDocDate._dateTimeOld = new System.DateTime(1000, 1, 1, 0, 0, 0, 0);
            this._toDocDate._defaultBackGround = System.Drawing.Color.White;
            this._toDocDate._emtry = true;
            this._toDocDate._enterToTab = false;
            this._toDocDate._icon = true;
            this._toDocDate._iconNumber = 1;
            this._toDocDate._isChange = false;
            this._toDocDate._isGetData = false;
            this._toDocDate._isQuery = true;
            this._toDocDate._isSearch = false;
            this._toDocDate._isTime = false;
            this._toDocDate._labelName = "";
            this._toDocDate._lostFocust = true;
            this._toDocDate._maxColumn = 0;
            this._toDocDate._name = null;
            this._toDocDate._row = 0;
            this._toDocDate._rowCount = 0;
            this._toDocDate._textFirst = "";
            this._toDocDate._textLast = "";
            this._toDocDate._textSecond = "";
            this._toDocDate._upperCase = false;
            this._toDocDate._warning = true;
            this._toDocDate.BackColor = System.Drawing.Color.Transparent;
            this._toDocDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._toDocDate.ForeColor = System.Drawing.Color.Black;
            this._toDocDate.IsUpperCase = false;
            this._toDocDate.Location = new System.Drawing.Point(465, 14);
            this._toDocDate.Margin = new System.Windows.Forms.Padding(0);
            this._toDocDate.MaxLength = 0;
            this._toDocDate.Name = "_toDocDate";
            this._toDocDate.ShowIcon = true;
            this._toDocDate.Size = new System.Drawing.Size(219, 22);
            this._toDocDate.TabIndex = 4;
            // 
            // _myLabel1
            // 
            this._myLabel1.AutoSize = true;
            this._myLabel1.Location = new System.Drawing.Point(12, 16);
            this._myLabel1.Name = "_myLabel1";
            this._myLabel1.ResourceName = "";
            this._myLabel1.Size = new System.Drawing.Size(97, 14);
            this._myLabel1.TabIndex = 5;
            this._myLabel1.Text = "จากเอกสารวันที่ : ";
            // 
            // _myLabel2
            // 
            this._myLabel2.AutoSize = true;
            this._myLabel2.Location = new System.Drawing.Point(372, 18);
            this._myLabel2.Name = "_myLabel2";
            this._myLabel2.ResourceName = "";
            this._myLabel2.Size = new System.Drawing.Size(90, 14);
            this._myLabel2.TabIndex = 6;
            this._myLabel2.Text = "ถึงเอกสารวันที่ : ";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // _stopButton
            // 
            this._stopButton._drawNewMethod = false;
            this._stopButton.BackColor = System.Drawing.Color.Transparent;
            this._stopButton.ButtonText = "Stop";
            this._stopButton.Location = new System.Drawing.Point(138, 79);
            this._stopButton.Name = "_stopButton";
            this._stopButton.Size = new System.Drawing.Size(117, 34);
            this._stopButton.TabIndex = 7;
            this._stopButton.Text = "Stop";
            this._stopButton.UseVisualStyleBackColor = false;
            this._stopButton.Click += new System.EventHandler(this._stopButton_Click);
            // 
            // ar_point_recal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._stopButton);
            this.Controls.Add(this._myLabel2);
            this.Controls.Add(this._myLabel1);
            this.Controls.Add(this._toDocDate);
            this.Controls.Add(this._fromDocDate);
            this.Controls.Add(this._resultTextbox);
            this.Controls.Add(this._closeButton);
            this.Controls.Add(this._processButton);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "ar_point_recal";
            this.Size = new System.Drawing.Size(758, 229);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib.VistaButton _processButton;
        private MyLib.VistaButton _closeButton;
        private System.Windows.Forms.TextBox _resultTextbox;
        private MyLib._myDateBox _fromDocDate;
        private MyLib._myDateBox _toDocDate;
        private MyLib._myLabel _myLabel1;
        private MyLib._myLabel _myLabel2;
        private System.Windows.Forms.Timer timer1;
        private MyLib.VistaButton _stopButton;
    }
}