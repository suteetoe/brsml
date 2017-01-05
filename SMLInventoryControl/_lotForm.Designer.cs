namespace SMLInventoryControl
{
    partial class _lotForm
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
            this._myPanel1 = new MyLib._myPanel();
            this._resultGrid = new MyLib._myGrid();
            this._myPanel2 = new MyLib._myPanel();
            this._mfdDate = new MyLib._myDateBox();
            this._mfnTextbox = new MyLib._myTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._expireDate = new MyLib._myDateBox();
            this.label2 = new System.Windows.Forms.Label();
            this._lotNumber = new MyLib._myTextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._text = new System.Windows.Forms.Label();
            this._myPanel1.SuspendLayout();
            this._myPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._resultGrid);
            this._myPanel1.Controls.Add(this._myPanel2);
            this._myPanel1.Controls.Add(this.flowLayoutPanel1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(432, 371);
            this._myPanel1.TabIndex = 6;
            // 
            // _resultGrid
            // 
            this._resultGrid._extraWordShow = true;
            this._resultGrid._selectRow = -1;
            this._resultGrid.BackColor = System.Drawing.SystemColors.Window;
            this._resultGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._resultGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._resultGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._resultGrid.IsEdit = false;
            this._resultGrid.Location = new System.Drawing.Point(0, 142);
            this._resultGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._resultGrid.Name = "_resultGrid";
            this._resultGrid.Size = new System.Drawing.Size(432, 229);
            this._resultGrid.TabIndex = 7;
            this._resultGrid.TabStop = false;
            // 
            // _myPanel2
            // 
            this._myPanel2._switchTabAuto = false;
            this._myPanel2.AutoSize = true;
            this._myPanel2.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Controls.Add(this._mfdDate);
            this._myPanel2.Controls.Add(this._mfnTextbox);
            this._myPanel2.Controls.Add(this.label4);
            this._myPanel2.Controls.Add(this.label3);
            this._myPanel2.Controls.Add(this.label1);
            this._myPanel2.Controls.Add(this._expireDate);
            this._myPanel2.Controls.Add(this.label2);
            this._myPanel2.Controls.Add(this._lotNumber);
            this._myPanel2.CornerPicture = null;
            this._myPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this._myPanel2.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Location = new System.Drawing.Point(0, 29);
            this._myPanel2.Name = "_myPanel2";
            this._myPanel2.Padding = new System.Windows.Forms.Padding(10);
            this._myPanel2.Size = new System.Drawing.Size(432, 113);
            this._myPanel2.TabIndex = 8;
            // 
            // _mfdDate
            // 
            this._mfdDate._column = 0;
            this._mfdDate._dateTime = new System.DateTime(1000, 1, 1, 0, 0, 0, 0);
            this._mfdDate._dateTimeOld = new System.DateTime(1000, 1, 1, 0, 0, 0, 0);
            this._mfdDate._defaultBackGround = System.Drawing.Color.White;
            this._mfdDate._emtry = true;
            this._mfdDate._enterToTab = false;
            this._mfdDate._icon = true;
            this._mfdDate._iconNumber = 1;
            this._mfdDate._isChange = false;
            this._mfdDate._isGetData = false;
            this._mfdDate._isQuery = true;
            this._mfdDate._isSearch = false;
            this._mfdDate._isTime = false;
            this._mfdDate._labelName = "";
            this._mfdDate._lostFocust = true;
            this._mfdDate._maxColumn = 0;
            this._mfdDate._name = null;
            this._mfdDate._row = 0;
            this._mfdDate._rowCount = 0;
            this._mfdDate._textFirst = "";
            this._mfdDate._textLast = "";
            this._mfdDate._textSecond = "";
            this._mfdDate._upperCase = false;
            this._mfdDate._warning = true;
            this._mfdDate.BackColor = System.Drawing.Color.Transparent;
            this._mfdDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._mfdDate.ForeColor = System.Drawing.Color.Black;
            this._mfdDate.IsUpperCase = false;
            this._mfdDate.Location = new System.Drawing.Point(112, 56);
            this._mfdDate.Margin = new System.Windows.Forms.Padding(0);
            this._mfdDate.MaxLength = 0;
            this._mfdDate.Name = "_mfdDate";
            this._mfdDate.ShowIcon = true;
            this._mfdDate.Size = new System.Drawing.Size(143, 22);
            this._mfdDate.TabIndex = 7;
            // 
            // _mfnTextbox
            // 
            this._mfnTextbox._column = 0;
            this._mfnTextbox._defaultBackGround = System.Drawing.Color.White;
            this._mfnTextbox._emtry = true;
            this._mfnTextbox._enterToTab = false;
            this._mfnTextbox._icon = false;
            this._mfnTextbox._iconNumber = 1;
            this._mfnTextbox._isChange = false;
            this._mfnTextbox._isGetData = false;
            this._mfnTextbox._isQuery = true;
            this._mfnTextbox._isSearch = false;
            this._mfnTextbox._isTime = false;
            this._mfnTextbox._labelName = "";
            this._mfnTextbox._maxColumn = 0;
            this._mfnTextbox._name = null;
            this._mfnTextbox._row = 0;
            this._mfnTextbox._rowCount = 0;
            this._mfnTextbox._textFirst = "";
            this._mfnTextbox._textLast = "";
            this._mfnTextbox._textSecond = "";
            this._mfnTextbox._upperCase = false;
            this._mfnTextbox.BackColor = System.Drawing.SystemColors.Window;
            this._mfnTextbox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._mfnTextbox.ForeColor = System.Drawing.Color.Black;
            this._mfnTextbox.IsUpperCase = false;
            this._mfnTextbox.Location = new System.Drawing.Point(112, 81);
            this._mfnTextbox.Margin = new System.Windows.Forms.Padding(0);
            this._mfnTextbox.MaxLength = 0;
            this._mfnTextbox.Name = "_mfnTextbox";
            this._mfnTextbox.ShowIcon = false;
            this._mfnTextbox.Size = new System.Drawing.Size(309, 22);
            this._mfnTextbox.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(7, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Manufacturing :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(7, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Mfd. Date :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(7, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lot Number  :";
            // 
            // _expireDate
            // 
            this._expireDate._column = 0;
            this._expireDate._dateTime = new System.DateTime(1000, 1, 1, 0, 0, 0, 0);
            this._expireDate._dateTimeOld = new System.DateTime(1000, 1, 1, 0, 0, 0, 0);
            this._expireDate._defaultBackGround = System.Drawing.Color.White;
            this._expireDate._emtry = true;
            this._expireDate._enterToTab = false;
            this._expireDate._icon = true;
            this._expireDate._iconNumber = 1;
            this._expireDate._isChange = false;
            this._expireDate._isGetData = false;
            this._expireDate._isQuery = true;
            this._expireDate._isSearch = false;
            this._expireDate._isTime = false;
            this._expireDate._labelName = "";
            this._expireDate._lostFocust = true;
            this._expireDate._maxColumn = 0;
            this._expireDate._name = null;
            this._expireDate._row = 0;
            this._expireDate._rowCount = 0;
            this._expireDate._textFirst = "";
            this._expireDate._textLast = "";
            this._expireDate._textSecond = "";
            this._expireDate._upperCase = false;
            this._expireDate._warning = true;
            this._expireDate.BackColor = System.Drawing.Color.Transparent;
            this._expireDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._expireDate.ForeColor = System.Drawing.Color.Black;
            this._expireDate.IsUpperCase = false;
            this._expireDate.Location = new System.Drawing.Point(112, 31);
            this._expireDate.Margin = new System.Windows.Forms.Padding(0);
            this._expireDate.MaxLength = 0;
            this._expireDate.Name = "_expireDate";
            this._expireDate.ShowIcon = true;
            this._expireDate.Size = new System.Drawing.Size(143, 22);
            this._expireDate.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(7, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Expire Date :";
            // 
            // _lotNumber
            // 
            this._lotNumber._column = 0;
            this._lotNumber._defaultBackGround = System.Drawing.Color.White;
            this._lotNumber._emtry = true;
            this._lotNumber._enterToTab = false;
            this._lotNumber._icon = false;
            this._lotNumber._iconNumber = 1;
            this._lotNumber._isChange = false;
            this._lotNumber._isGetData = false;
            this._lotNumber._isQuery = true;
            this._lotNumber._isSearch = false;
            this._lotNumber._isTime = false;
            this._lotNumber._labelName = "";
            this._lotNumber._maxColumn = 0;
            this._lotNumber._name = null;
            this._lotNumber._row = 0;
            this._lotNumber._rowCount = 0;
            this._lotNumber._textFirst = "";
            this._lotNumber._textLast = "";
            this._lotNumber._textSecond = "";
            this._lotNumber._upperCase = false;
            this._lotNumber.BackColor = System.Drawing.SystemColors.Window;
            this._lotNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._lotNumber.ForeColor = System.Drawing.Color.Black;
            this._lotNumber.IsUpperCase = false;
            this._lotNumber.Location = new System.Drawing.Point(112, 6);
            this._lotNumber.Margin = new System.Windows.Forms.Padding(0);
            this._lotNumber.MaxLength = 0;
            this._lotNumber.Name = "_lotNumber";
            this._lotNumber.ShowIcon = false;
            this._lotNumber.Size = new System.Drawing.Size(309, 22);
            this._lotNumber.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this._text);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(432, 29);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // _text
            // 
            this._text.AutoSize = true;
            this._text.BackColor = System.Drawing.Color.Transparent;
            this._text.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._text.Location = new System.Drawing.Point(8, 5);
            this._text.Name = "_text";
            this._text.Size = new System.Drawing.Size(59, 19);
            this._text.TabIndex = 5;
            this._text.Text = "label3";
            // 
            // _lotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 371);
            this.ControlBox = false;
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_lotForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lot";
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myPanel2.ResumeLayout(false);
            this._myPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public MyLib._myTextBox _lotNumber;
        public MyLib._myDateBox _expireDate;
        private System.Windows.Forms.Label _text;
        private MyLib._myPanel _myPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MyLib._myGrid _resultGrid;
        private MyLib._myPanel _myPanel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public MyLib._myDateBox _mfdDate;
        public MyLib._myTextBox _mfnTextbox;
    }
}