namespace SMLInventoryControl
{
    partial class _icTransSerialNumberForm
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
            this._grid = new MyLib._myGrid();
            this._myPanel1 = new MyLib._myPanel();
            this._autoRunCheckBox = new System.Windows.Forms.CheckBox();
            this._serialNumberTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._genPanel = new MyLib._myPanel();
            this._myLabel1 = new MyLib._myLabel();
            this._serialStartTextbox = new System.Windows.Forms.TextBox();
            this._myLabel2 = new MyLib._myLabel();
            this._serialQtyTextbox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this._genSerialButton = new MyLib.VistaButton();
            this._serialVoidDate = new MyLib._myDateBox();
            this._myLabel3 = new MyLib._myLabel();
            this._serialDetailTextbox = new System.Windows.Forms.TextBox();
            this._myLabel4 = new MyLib._myLabel();
            this._myPanel1.SuspendLayout();
            this._genPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _grid
            // 
            this._grid._extraWordShow = true;
            this._grid._selectRow = -1;
            this._grid.BackColor = System.Drawing.SystemColors.Window;
            this._grid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._grid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._grid.Location = new System.Drawing.Point(0, 0);
            this._grid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._grid.Name = "_grid";
            this._grid.Size = new System.Drawing.Size(878, 251);
            this._grid.TabIndex = 0;
            this._grid.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.AutoSize = true;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._genPanel);
            this._myPanel1.Controls.Add(this.panel1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 251);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(878, 109);
            this._myPanel1.TabIndex = 3;
            // 
            // _autoRunCheckBox
            // 
            this._autoRunCheckBox.AutoSize = true;
            this._autoRunCheckBox.BackColor = System.Drawing.Color.Transparent;
            this._autoRunCheckBox.Location = new System.Drawing.Point(367, 7);
            this._autoRunCheckBox.Name = "_autoRunCheckBox";
            this._autoRunCheckBox.Size = new System.Drawing.Size(78, 18);
            this._autoRunCheckBox.TabIndex = 2;
            this._autoRunCheckBox.Text = "Auto Run";
            this._autoRunCheckBox.UseVisualStyleBackColor = false;
            // 
            // _serialNumberTextBox
            // 
            this._serialNumberTextBox.Location = new System.Drawing.Point(104, 5);
            this._serialNumberTextBox.Name = "_serialNumberTextBox";
            this._serialNumberTextBox.Size = new System.Drawing.Size(257, 22);
            this._serialNumberTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Serial Number :";
            // 
            // _genPanel
            // 
            this._genPanel._switchTabAuto = false;
            this._genPanel.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._genPanel.Controls.Add(this._serialDetailTextbox);
            this._genPanel.Controls.Add(this._myLabel4);
            this._genPanel.Controls.Add(this._myLabel3);
            this._genPanel.Controls.Add(this._serialVoidDate);
            this._genPanel.Controls.Add(this._genSerialButton);
            this._genPanel.Controls.Add(this._serialQtyTextbox);
            this._genPanel.Controls.Add(this._myLabel2);
            this._genPanel.Controls.Add(this._serialStartTextbox);
            this._genPanel.Controls.Add(this._myLabel1);
            this._genPanel.CornerPicture = null;
            this._genPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._genPanel.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._genPanel.Location = new System.Drawing.Point(0, 29);
            this._genPanel.Name = "_genPanel";
            this._genPanel.Size = new System.Drawing.Size(878, 80);
            this._genPanel.TabIndex = 5;
            // 
            // _myLabel1
            // 
            this._myLabel1.AutoSize = true;
            this._myLabel1.BackColor = System.Drawing.Color.Transparent;
            this._myLabel1.Location = new System.Drawing.Point(8, 7);
            this._myLabel1.Name = "_myLabel1";
            this._myLabel1.ResourceName = "Serial Number เริ่มต้น :";
            this._myLabel1.Size = new System.Drawing.Size(124, 14);
            this._myLabel1.TabIndex = 2;
            this._myLabel1.Text = "Serial Number เริ่มต้น :";
            // 
            // _serialStartTextbox
            // 
            this._serialStartTextbox.Location = new System.Drawing.Point(138, 4);
            this._serialStartTextbox.Name = "_serialStartTextbox";
            this._serialStartTextbox.Size = new System.Drawing.Size(223, 22);
            this._serialStartTextbox.TabIndex = 3;
            // 
            // _myLabel2
            // 
            this._myLabel2.AutoSize = true;
            this._myLabel2.BackColor = System.Drawing.Color.Transparent;
            this._myLabel2.Location = new System.Drawing.Point(84, 32);
            this._myLabel2.Name = "_myLabel2";
            this._myLabel2.ResourceName = "จำนวน :";
            this._myLabel2.Size = new System.Drawing.Size(48, 14);
            this._myLabel2.TabIndex = 4;
            this._myLabel2.Text = "จำนวน :";
            // 
            // _serialQtyTextbox
            // 
            this._serialQtyTextbox.Location = new System.Drawing.Point(138, 29);
            this._serialQtyTextbox.Name = "_serialQtyTextbox";
            this._serialQtyTextbox.Size = new System.Drawing.Size(223, 22);
            this._serialQtyTextbox.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this._autoRunCheckBox);
            this.panel1.Controls.Add(this._serialNumberTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(878, 29);
            this.panel1.TabIndex = 3;
            // 
            // _genSerialButton
            // 
            this._genSerialButton._drawNewMethod = false;
            this._genSerialButton.BackColor = System.Drawing.Color.Transparent;
            this._genSerialButton.ButtonText = "Generate";
            this._genSerialButton.Location = new System.Drawing.Point(138, 53);
            this._genSerialButton.myImage = global::SMLInventoryControl.Properties.Resources.magic_wand2;
            this._genSerialButton.Name = "_genSerialButton";
            this._genSerialButton.Size = new System.Drawing.Size(92, 24);
            this._genSerialButton.TabIndex = 6;
            this._genSerialButton.Text = "vistaButton1";
            this._genSerialButton.UseVisualStyleBackColor = false;
            this._genSerialButton.Click += new System.EventHandler(this._genSerialButton_Click);
            // 
            // _serialVoidDate
            // 
            this._serialVoidDate._column = 0;
            this._serialVoidDate._dateTime = new System.DateTime(1000, 1, 1, 0, 0, 0, 0);
            this._serialVoidDate._dateTimeOld = new System.DateTime(1000, 1, 1, 0, 0, 0, 0);
            this._serialVoidDate._defaultBackGround = System.Drawing.Color.White;
            this._serialVoidDate._emtry = true;
            this._serialVoidDate._enterToTab = false;
            this._serialVoidDate._icon = true;
            this._serialVoidDate._iconNumber = 1;
            this._serialVoidDate._isChange = false;
            this._serialVoidDate._isQuery = true;
            this._serialVoidDate._isSearch = false;
            this._serialVoidDate._isTime = false;
            this._serialVoidDate._labelName = "";
            this._serialVoidDate._lostFocust = true;
            this._serialVoidDate._maxColumn = 0;
            this._serialVoidDate._name = null;
            this._serialVoidDate._row = 0;
            this._serialVoidDate._rowCount = 0;
            this._serialVoidDate._textFirst = "";
            this._serialVoidDate._textLast = "";
            this._serialVoidDate._textSecond = "";
            this._serialVoidDate._upperCase = false;
            this._serialVoidDate._warning = true;
            this._serialVoidDate.BackColor = System.Drawing.Color.Transparent;
            this._serialVoidDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._serialVoidDate.ForeColor = System.Drawing.Color.Black;
            this._serialVoidDate.IsUpperCase = false;
            this._serialVoidDate.Location = new System.Drawing.Point(467, 4);
            this._serialVoidDate.Margin = new System.Windows.Forms.Padding(0);
            this._serialVoidDate.MaxLength = 0;
            this._serialVoidDate.Name = "_serialVoidDate";
            this._serialVoidDate.ShowIcon = true;
            this._serialVoidDate.Size = new System.Drawing.Size(163, 22);
            this._serialVoidDate.TabIndex = 7;
            // 
            // _myLabel3
            // 
            this._myLabel3.AutoSize = true;
            this._myLabel3.BackColor = System.Drawing.Color.Transparent;
            this._myLabel3.Location = new System.Drawing.Point(370, 7);
            this._myLabel3.Name = "_myLabel3";
            this._myLabel3.ResourceName = "วันที่หมดประกัน :";
            this._myLabel3.Size = new System.Drawing.Size(91, 14);
            this._myLabel3.TabIndex = 8;
            this._myLabel3.Text = "วันที่หมดประกัน :";
            // 
            // _serialDetailTextbox
            // 
            this._serialDetailTextbox.Location = new System.Drawing.Point(467, 29);
            this._serialDetailTextbox.Name = "_serialDetailTextbox";
            this._serialDetailTextbox.Size = new System.Drawing.Size(223, 22);
            this._serialDetailTextbox.TabIndex = 10;
            // 
            // _myLabel4
            // 
            this._myLabel4.AutoSize = true;
            this._myLabel4.BackColor = System.Drawing.Color.Transparent;
            this._myLabel4.Location = new System.Drawing.Point(391, 32);
            this._myLabel4.Name = "_myLabel4";
            this._myLabel4.ResourceName = "รายละเอียด :";
            this._myLabel4.Size = new System.Drawing.Size(70, 14);
            this._myLabel4.TabIndex = 9;
            this._myLabel4.Text = "รายละเอียด :";
            // 
            // _icTransSerialNumberForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 360);
            this.ControlBox = false;
            this.Controls.Add(this._grid);
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_icTransSerialNumberForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_icTransSerialNumberForm";
            this._myPanel1.ResumeLayout(false);
            this._genPanel.ResumeLayout(false);
            this._genPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public MyLib._myGrid _grid;
        private MyLib._myPanel _myPanel1;
        private System.Windows.Forms.TextBox _serialNumberTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox _autoRunCheckBox;
        private MyLib._myPanel _genPanel;
        private MyLib.VistaButton _genSerialButton;
        private System.Windows.Forms.TextBox _serialQtyTextbox;
        private MyLib._myLabel _myLabel2;
        private System.Windows.Forms.TextBox _serialStartTextbox;
        private MyLib._myLabel _myLabel1;
        private System.Windows.Forms.Panel panel1;
        private MyLib._myDateBox _serialVoidDate;
        private MyLib._myLabel _myLabel3;
        private System.Windows.Forms.TextBox _serialDetailTextbox;
        private MyLib._myLabel _myLabel4;
    }
}