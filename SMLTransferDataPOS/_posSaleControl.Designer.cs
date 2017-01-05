namespace SMLTransferDataPOS
{
    partial class _posSaleControl
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
            this._sale_pos_screen_top = new MyLib._myScreen();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._loadButton = new MyLib.VistaButton();
            this.vistaButton3 = new MyLib.VistaButton();
            this._closeButton = new MyLib.VistaButton();
            this._myFlowLayoutPanel2 = new MyLib._myFlowLayoutPanel();
            this._saveButton = new MyLib.VistaButton();
            this._transGrid = new MyLib._myGrid();
            this._ic_trans_screen_top = new MyLib._myScreen();
            this._ic_trans_screen_bottom = new MyLib._myScreen();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this._screen_pay = new MyLib._myScreen();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this._payCreditCardGridControl1 = new SMLERPAPARControl._payCreditCardGridControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this._payCouponGridControl1 = new SMLERPAPARControl._payCouponGridControl();
            this._myFlowLayoutPanel1.SuspendLayout();
            this._myFlowLayoutPanel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // _sale_pos_screen_top
            // 
            this._sale_pos_screen_top._isChange = false;
            this._sale_pos_screen_top.BackColor = System.Drawing.Color.Transparent;
            this._sale_pos_screen_top.Dock = System.Windows.Forms.DockStyle.Top;
            this._sale_pos_screen_top.Location = new System.Drawing.Point(0, 0);
            this._sale_pos_screen_top.Name = "_sale_pos_screen_top";
            this._sale_pos_screen_top.Size = new System.Drawing.Size(1548, 11);
            this._sale_pos_screen_top.TabIndex = 1;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._loadButton);
            this._myFlowLayoutPanel1.Controls.Add(this.vistaButton3);
            this._myFlowLayoutPanel1.Controls.Add(this._closeButton);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 11);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(1548, 30);
            this._myFlowLayoutPanel1.TabIndex = 2;
            // 
            // _loadButton
            // 
            this._loadButton._drawNewMethod = false;
            this._loadButton.BackColor = System.Drawing.Color.Transparent;
            this._loadButton.ButtonText = "Load";
            this._loadButton.Location = new System.Drawing.Point(3, 3);
            this._loadButton.Name = "_loadButton";
            this._loadButton.Size = new System.Drawing.Size(93, 24);
            this._loadButton.TabIndex = 0;
            this._loadButton.Text = "vistaButton1";
            this._loadButton.UseVisualStyleBackColor = false;
            this._loadButton.Click += new System.EventHandler(this._loadButton_Click);
            // 
            // vistaButton3
            // 
            this.vistaButton3._drawNewMethod = false;
            this.vistaButton3.BackColor = System.Drawing.Color.Transparent;
            this.vistaButton3.ButtonText = "เลือกรูปแบบเอกสาร";
            this.vistaButton3.Location = new System.Drawing.Point(102, 3);
            this.vistaButton3.Name = "vistaButton3";
            this.vistaButton3.Size = new System.Drawing.Size(116, 24);
            this.vistaButton3.TabIndex = 1;
            this.vistaButton3.Text = "vistaButton3";
            this.vistaButton3.UseVisualStyleBackColor = false;
            this.vistaButton3.Visible = false;
            // 
            // _closeButton
            // 
            this._closeButton._drawNewMethod = false;
            this._closeButton.BackColor = System.Drawing.Color.Transparent;
            this._closeButton.ButtonText = "Close";
            this._closeButton.Location = new System.Drawing.Point(224, 3);
            this._closeButton.myImage = global::SMLTransferDataPOS.Properties.Resources.error;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(88, 24);
            this._closeButton.TabIndex = 2;
            this._closeButton.Text = "vistaButton4";
            this._closeButton.UseVisualStyleBackColor = false;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _myFlowLayoutPanel2
            // 
            this._myFlowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel2.Controls.Add(this._saveButton);
            this._myFlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel2.Location = new System.Drawing.Point(0, 721);
            this._myFlowLayoutPanel2.Name = "_myFlowLayoutPanel2";
            this._myFlowLayoutPanel2.Size = new System.Drawing.Size(1548, 32);
            this._myFlowLayoutPanel2.TabIndex = 3;
            // 
            // _saveButton
            // 
            this._saveButton._drawNewMethod = false;
            this._saveButton.BackColor = System.Drawing.Color.Transparent;
            this._saveButton.ButtonText = "บันทึก";
            this._saveButton.Location = new System.Drawing.Point(1465, 3);
            this._saveButton.myImage = global::SMLTransferDataPOS.Properties.Resources.disk_blue;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(80, 24);
            this._saveButton.TabIndex = 0;
            this._saveButton.Text = "vistaButton2";
            this._saveButton.UseVisualStyleBackColor = false;
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _transGrid
            // 
            this._transGrid._extraWordShow = true;
            this._transGrid._selectRow = -1;
            this._transGrid.BackColor = System.Drawing.SystemColors.Window;
            this._transGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._transGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._transGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._transGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._transGrid.Location = new System.Drawing.Point(3, 3);
            this._transGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._transGrid.Name = "_transGrid";
            this._transGrid.Size = new System.Drawing.Size(1534, 627);
            this._transGrid.TabIndex = 4;
            this._transGrid.TabStop = false;
            // 
            // _ic_trans_screen_top
            // 
            this._ic_trans_screen_top._isChange = false;
            this._ic_trans_screen_top.BackColor = System.Drawing.Color.Transparent;
            this._ic_trans_screen_top.Dock = System.Windows.Forms.DockStyle.Top;
            this._ic_trans_screen_top.Location = new System.Drawing.Point(0, 41);
            this._ic_trans_screen_top.Name = "_ic_trans_screen_top";
            this._ic_trans_screen_top.Size = new System.Drawing.Size(1548, 10);
            this._ic_trans_screen_top.TabIndex = 5;
            // 
            // _ic_trans_screen_bottom
            // 
            this._ic_trans_screen_bottom._isChange = false;
            this._ic_trans_screen_bottom.BackColor = System.Drawing.Color.Transparent;
            this._ic_trans_screen_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._ic_trans_screen_bottom.Location = new System.Drawing.Point(3, 630);
            this._ic_trans_screen_bottom.Name = "_ic_trans_screen_bottom";
            this._ic_trans_screen_bottom.Size = new System.Drawing.Size(1534, 10);
            this._ic_trans_screen_bottom.TabIndex = 6;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 51);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1548, 670);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this._transGrid);
            this.tabPage1.Controls.Add(this._ic_trans_screen_bottom);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1540, 643);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "รายละเอียด";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tabControl2);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1540, 643);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "รายละเอียดการรับเงิน";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1534, 637);
            this.tabControl2.TabIndex = 8;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this._screen_pay);
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1526, 610);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "รายละเอียด";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // _screen_pay
            // 
            this._screen_pay._isChange = false;
            this._screen_pay.BackColor = System.Drawing.Color.Transparent;
            this._screen_pay.Dock = System.Windows.Forms.DockStyle.Fill;
            this._screen_pay.Location = new System.Drawing.Point(3, 3);
            this._screen_pay.Name = "_screen_pay";
            this._screen_pay.Size = new System.Drawing.Size(1520, 604);
            this._screen_pay.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this._payCreditCardGridControl1);
            this.tabPage4.Location = new System.Drawing.Point(4, 23);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1526, 610);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "บัตรเครดิต";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // _payCreditCardGridControl1
            // 
            this._payCreditCardGridControl1._extraWordShow = true;
            this._payCreditCardGridControl1._selectRow = -1;
            this._payCreditCardGridControl1.AllowDrop = true;
            this._payCreditCardGridControl1.AutoSize = true;
            this._payCreditCardGridControl1.BackColor = System.Drawing.SystemColors.Window;
            this._payCreditCardGridControl1.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._payCreditCardGridControl1.ColumnBackgroundAuto = false;
            this._payCreditCardGridControl1.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._payCreditCardGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._payCreditCardGridControl1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._payCreditCardGridControl1.Location = new System.Drawing.Point(3, 3);
            this._payCreditCardGridControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._payCreditCardGridControl1.Name = "_payCreditCardGridControl1";
            this._payCreditCardGridControl1.RowOddBackground = System.Drawing.Color.AliceBlue;
            this._payCreditCardGridControl1.Size = new System.Drawing.Size(1520, 604);
            this._payCreditCardGridControl1.TabIndex = 0;
            this._payCreditCardGridControl1.TabStop = false;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this._payCouponGridControl1);
            this.tabPage5.Location = new System.Drawing.Point(4, 23);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1526, 610);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "คูปอง";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // _payCouponGridControl1
            // 
            this._payCouponGridControl1._extraWordShow = true;
            this._payCouponGridControl1._selectRow = -1;
            this._payCouponGridControl1.AllowDrop = true;
            this._payCouponGridControl1.AutoSize = true;
            this._payCouponGridControl1.BackColor = System.Drawing.SystemColors.Window;
            this._payCouponGridControl1.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._payCouponGridControl1.ColumnBackgroundAuto = false;
            this._payCouponGridControl1.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._payCouponGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._payCouponGridControl1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._payCouponGridControl1.Location = new System.Drawing.Point(3, 3);
            this._payCouponGridControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._payCouponGridControl1.Name = "_payCouponGridControl1";
            this._payCouponGridControl1.RowOddBackground = System.Drawing.Color.AliceBlue;
            this._payCouponGridControl1.Size = new System.Drawing.Size(1520, 604);
            this._payCouponGridControl1.TabIndex = 0;
            this._payCouponGridControl1.TabStop = false;
            // 
            // _posSaleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this._ic_trans_screen_top);
            this.Controls.Add(this._myFlowLayoutPanel2);
            this.Controls.Add(this._myFlowLayoutPanel1);
            this.Controls.Add(this._sale_pos_screen_top);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "_posSaleControl";
            this.Size = new System.Drawing.Size(1548, 753);
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myScreen _sale_pos_screen_top;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib.VistaButton _loadButton;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel2;
        private MyLib.VistaButton _saveButton;
        private MyLib.VistaButton vistaButton3;
        private MyLib.VistaButton _closeButton;
        private MyLib._myGrid _transGrid;
        private MyLib._myScreen _ic_trans_screen_top;
        private MyLib._myScreen _ic_trans_screen_bottom;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private MyLib._myScreen _screen_pay;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private SMLERPAPARControl._payCreditCardGridControl _payCreditCardGridControl1;
        private SMLERPAPARControl._payCouponGridControl _payCouponGridControl1;
    }
}
