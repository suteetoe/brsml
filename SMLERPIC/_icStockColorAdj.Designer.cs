namespace SMLERPIC
{
    partial class _icStockColorAdj
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._dateBeginLabel = new MyLib._myLabel();
            this._dateBegin = new MyLib._myDateBox();
            this._myLabel1 = new MyLib._myLabel();
            this._dateEnd = new MyLib._myDateBox();
            this._autoSaveCheckBox = new System.Windows.Forms.CheckBox();
            this._processStartButton = new MyLib.VistaButton();
            this._processStopButton = new MyLib.VistaButton();
            this._closeButton = new MyLib.VistaButton();
            this._processLabel = new System.Windows.Forms.Label();
            this._ictransItemGridOut = new SMLInventoryControl._icTransItemGridControl();
            this._grouper1 = new MyLib._grouper();
            this._grouper2 = new MyLib._grouper();
            this._ictransItemGridIn = new SMLInventoryControl._icTransItemGridControl();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this.flowLayoutPanel1.SuspendLayout();
            this._grouper1.SuspendLayout();
            this._grouper2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this._dateBeginLabel);
            this.flowLayoutPanel1.Controls.Add(this._dateBegin);
            this.flowLayoutPanel1.Controls.Add(this._myLabel1);
            this.flowLayoutPanel1.Controls.Add(this._dateEnd);
            this.flowLayoutPanel1.Controls.Add(this._autoSaveCheckBox);
            this.flowLayoutPanel1.Controls.Add(this._processStartButton);
            this.flowLayoutPanel1.Controls.Add(this._processStopButton);
            this.flowLayoutPanel1.Controls.Add(this._closeButton);
            this.flowLayoutPanel1.Controls.Add(this._processLabel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(884, 30);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // _dateBeginLabel
            // 
            this._dateBeginLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._dateBeginLabel.AutoSize = true;
            this._dateBeginLabel.Location = new System.Drawing.Point(3, 8);
            this._dateBeginLabel.Name = "_dateBeginLabel";
            this._dateBeginLabel.ResourceName = "";
            this._dateBeginLabel.Size = new System.Drawing.Size(57, 13);
            this._dateBeginLabel.TabIndex = 4;
            this._dateBeginLabel.Text = "From Date";
            // 
            // _dateBegin
            // 
            this._dateBegin._column = 0;
            this._dateBegin._dateTime = new System.DateTime(1000, 1, 1, 0, 0, 0, 0);
            this._dateBegin._dateTimeOld = new System.DateTime(1000, 1, 1, 0, 0, 0, 0);
            this._dateBegin._defaultBackGround = System.Drawing.Color.White;
            this._dateBegin._emtry = true;
            this._dateBegin._enterToTab = false;
            this._dateBegin._icon = true;
            this._dateBegin._iconNumber = 1;
            this._dateBegin._isChange = false;
            this._dateBegin._isQuery = true;
            this._dateBegin._isSearch = false;
            this._dateBegin._isTime = false;
            this._dateBegin._labelName = "";
            this._dateBegin._lostFocust = true;
            this._dateBegin._maxColumn = 0;
            this._dateBegin._name = null;
            this._dateBegin._row = 0;
            this._dateBegin._rowCount = 0;
            this._dateBegin._textFirst = "";
            this._dateBegin._textLast = "";
            this._dateBegin._textSecond = "";
            this._dateBegin._upperCase = false;
            this._dateBegin._warning = true;
            this._dateBegin.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._dateBegin.BackColor = System.Drawing.Color.Transparent;
            this._dateBegin.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._dateBegin.ForeColor = System.Drawing.Color.Black;
            this._dateBegin.IsUpperCase = false;
            this._dateBegin.Location = new System.Drawing.Point(63, 4);
            this._dateBegin.Margin = new System.Windows.Forms.Padding(0);
            this._dateBegin.MaxLength = 0;
            this._dateBegin.Name = "_dateBegin";
            this._dateBegin.ShowIcon = true;
            this._dateBegin.Size = new System.Drawing.Size(151, 22);
            this._dateBegin.TabIndex = 1;
            // 
            // _myLabel1
            // 
            this._myLabel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._myLabel1.AutoSize = true;
            this._myLabel1.Location = new System.Drawing.Point(217, 8);
            this._myLabel1.Name = "_myLabel1";
            this._myLabel1.ResourceName = "";
            this._myLabel1.Size = new System.Drawing.Size(45, 13);
            this._myLabel1.TabIndex = 8;
            this._myLabel1.Text = "To Date";
            // 
            // _dateEnd
            // 
            this._dateEnd._column = 0;
            this._dateEnd._dateTime = new System.DateTime(1000, 1, 1, 0, 0, 0, 0);
            this._dateEnd._dateTimeOld = new System.DateTime(1000, 1, 1, 0, 0, 0, 0);
            this._dateEnd._defaultBackGround = System.Drawing.Color.White;
            this._dateEnd._emtry = true;
            this._dateEnd._enterToTab = false;
            this._dateEnd._icon = true;
            this._dateEnd._iconNumber = 1;
            this._dateEnd._isChange = false;
            this._dateEnd._isQuery = true;
            this._dateEnd._isSearch = false;
            this._dateEnd._isTime = false;
            this._dateEnd._labelName = "";
            this._dateEnd._lostFocust = true;
            this._dateEnd._maxColumn = 0;
            this._dateEnd._name = null;
            this._dateEnd._row = 0;
            this._dateEnd._rowCount = 0;
            this._dateEnd._textFirst = "";
            this._dateEnd._textLast = "";
            this._dateEnd._textSecond = "";
            this._dateEnd._upperCase = false;
            this._dateEnd._warning = true;
            this._dateEnd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._dateEnd.BackColor = System.Drawing.Color.Transparent;
            this._dateEnd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._dateEnd.ForeColor = System.Drawing.Color.Black;
            this._dateEnd.IsUpperCase = false;
            this._dateEnd.Location = new System.Drawing.Point(265, 4);
            this._dateEnd.Margin = new System.Windows.Forms.Padding(0);
            this._dateEnd.MaxLength = 0;
            this._dateEnd.Name = "_dateEnd";
            this._dateEnd.ShowIcon = true;
            this._dateEnd.Size = new System.Drawing.Size(151, 22);
            this._dateEnd.TabIndex = 2;
            // 
            // _autoSaveCheckBox
            // 
            this._autoSaveCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._autoSaveCheckBox.AutoSize = true;
            this._autoSaveCheckBox.Location = new System.Drawing.Point(419, 6);
            this._autoSaveCheckBox.Name = "_autoSaveCheckBox";
            this._autoSaveCheckBox.Size = new System.Drawing.Size(76, 17);
            this._autoSaveCheckBox.TabIndex = 3;
            this._autoSaveCheckBox.Text = "Auto Save";
            this._autoSaveCheckBox.UseVisualStyleBackColor = true;
            // 
            // _processStartButton
            // 
            this._processStartButton.AutoSize = true;
            this._processStartButton.BackColor = System.Drawing.Color.Transparent;
            this._processStartButton.ButtonText = "Start Process";
            this._processStartButton.Location = new System.Drawing.Point(501, 3);
            this._processStartButton.Name = "_processStartButton";
            this._processStartButton.Size = new System.Drawing.Size(84, 24);
            this._processStartButton.TabIndex = 4;
            this._processStartButton.Text = "Process";
            this._processStartButton.UseVisualStyleBackColor = true;
            this._processStartButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _processStopButton
            // 
            this._processStopButton.AutoSize = true;
            this._processStopButton.BackColor = System.Drawing.Color.Transparent;
            this._processStopButton.ButtonText = "Stop Process";
            this._processStopButton.Location = new System.Drawing.Point(591, 3);
            this._processStopButton.Name = "_processStopButton";
            this._processStopButton.Size = new System.Drawing.Size(83, 24);
            this._processStopButton.TabIndex = 5;
            this._processStopButton.Text = "Stop Process";
            this._processStopButton.UseVisualStyleBackColor = true;
            // 
            // _closeButton
            // 
            this._closeButton.AutoSize = true;
            this._closeButton.BackColor = System.Drawing.Color.Transparent;
            this._closeButton.ButtonText = "Close";
            this._closeButton.Location = new System.Drawing.Point(680, 3);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(46, 24);
            this._closeButton.TabIndex = 6;
            this._closeButton.Text = "Close";
            this._closeButton.UseVisualStyleBackColor = true;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _processLabel
            // 
            this._processLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._processLabel.AutoSize = true;
            this._processLabel.Location = new System.Drawing.Point(732, 8);
            this._processLabel.Name = "_processLabel";
            this._processLabel.Size = new System.Drawing.Size(78, 13);
            this._processLabel.TabIndex = 10;
            this._processLabel.Text = "Process Status";
            // 
            // _ictransItemGridOut
            // 
            this._ictransItemGridOut._custCode = "";
            this._ictransItemGridOut._extraWordShow = true;
            this._ictransItemGridOut._icTransControlType = _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ;
            this._ictransItemGridOut._icTransRef = null;
            this._ictransItemGridOut._selectRow = -1;
            this._ictransItemGridOut.BackColor = System.Drawing.SystemColors.Window;
            this._ictransItemGridOut.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._ictransItemGridOut.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._ictransItemGridOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ictransItemGridOut.Enabled = false;
            this._ictransItemGridOut.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._ictransItemGridOut.Location = new System.Drawing.Point(5, 25);
            this._ictransItemGridOut.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._ictransItemGridOut.Name = "_ictransItemGridOut";
            this._ictransItemGridOut.Size = new System.Drawing.Size(874, 356);
            this._ictransItemGridOut.TabIndex = 1;
            this._ictransItemGridOut.TabStop = false;
            // 
            // _grouper1
            // 
            this._grouper1.BackgroundColor = System.Drawing.Color.White;
            this._grouper1.BackgroundGradientColor = System.Drawing.Color.White;
            this._grouper1.BackgroundGradientMode = MyLib._grouper.GroupBoxGradientMode.None;
            this._grouper1.BorderColor = System.Drawing.Color.Black;
            this._grouper1.BorderThickness = 1F;
            this._grouper1.Controls.Add(this._ictransItemGridOut);
            this._grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this._grouper1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grouper1.GroupImage = null;
            this._grouper1.GroupTitle = "Out";
            this._grouper1.Location = new System.Drawing.Point(0, 30);
            this._grouper1.Name = "_grouper1";
            this._grouper1.Padding = new System.Windows.Forms.Padding(5, 25, 5, 5);
            this._grouper1.PaintGroupBox = false;
            this._grouper1.RoundCorners = 10;
            this._grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this._grouper1.ShadowControl = false;
            this._grouper1.ShadowThickness = 3;
            this._grouper1.Size = new System.Drawing.Size(884, 386);
            this._grouper1.TabIndex = 3;
            // 
            // _grouper2
            // 
            this._grouper2.BackgroundColor = System.Drawing.Color.White;
            this._grouper2.BackgroundGradientColor = System.Drawing.Color.White;
            this._grouper2.BackgroundGradientMode = MyLib._grouper.GroupBoxGradientMode.None;
            this._grouper2.BorderColor = System.Drawing.Color.Black;
            this._grouper2.BorderThickness = 1F;
            this._grouper2.Controls.Add(this._ictransItemGridIn);
            this._grouper2.CustomGroupBoxColor = System.Drawing.Color.White;
            this._grouper2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._grouper2.GroupImage = null;
            this._grouper2.GroupTitle = "In";
            this._grouper2.Location = new System.Drawing.Point(0, 416);
            this._grouper2.Name = "_grouper2";
            this._grouper2.Padding = new System.Windows.Forms.Padding(5, 25, 5, 5);
            this._grouper2.PaintGroupBox = false;
            this._grouper2.RoundCorners = 10;
            this._grouper2.ShadowColor = System.Drawing.Color.DarkGray;
            this._grouper2.ShadowControl = false;
            this._grouper2.ShadowThickness = 3;
            this._grouper2.Size = new System.Drawing.Size(884, 223);
            this._grouper2.TabIndex = 4;
            // 
            // _ictransItemGridIn
            // 
            this._ictransItemGridIn._custCode = "";
            this._ictransItemGridIn._extraWordShow = true;
            this._ictransItemGridIn._icTransControlType = _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป;
            this._ictransItemGridIn._icTransRef = null;
            this._ictransItemGridIn._selectRow = -1;
            this._ictransItemGridIn.BackColor = System.Drawing.SystemColors.Window;
            this._ictransItemGridIn.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._ictransItemGridIn.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._ictransItemGridIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ictransItemGridIn.Enabled = false;
            this._ictransItemGridIn.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._ictransItemGridIn.Location = new System.Drawing.Point(5, 25);
            this._ictransItemGridIn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._ictransItemGridIn.Name = "_ictransItemGridIn";
            this._ictransItemGridIn.Size = new System.Drawing.Size(874, 193);
            this._ictransItemGridIn.TabIndex = 2;
            this._ictransItemGridIn.TabStop = false;
            // 
            // _icStockColorAdj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._grouper1);
            this.Controls.Add(this._grouper2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_icStockColorAdj";
            this.Size = new System.Drawing.Size(884, 639);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this._grouper1.ResumeLayout(false);
            this._grouper2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private SMLInventoryControl._icTransItemGridControl _ictransItemGridOut;
        private MyLib._grouper _grouper1;
        private MyLib._grouper _grouper2;
        private MyLib.VistaButton _processStartButton;
        private SMLInventoryControl._icTransItemGridControl _ictransItemGridIn;
        private MyLib._myLabel _dateBeginLabel;
        private MyLib._myDateBox _dateBegin;
        private MyLib.VistaButton _closeButton;
        private MyLib._myLabel _myLabel1;
        private MyLib._myDateBox _dateEnd;
        private System.Windows.Forms.CheckBox _autoSaveCheckBox;
        private System.Windows.Forms.Label _processLabel;
        private MyLib.VistaButton _processStopButton;
        private System.Windows.Forms.Timer _timer;
    }
}
