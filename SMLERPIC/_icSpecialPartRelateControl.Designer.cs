namespace SMLERPIC
{
    partial class _icSpecialPartRelateControl
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this._gridStockBalance = new MyLib._myGrid();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this._getPicture = new SMLERPControl._getPicture();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this._applicablePartListBox = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this._partDetailNameLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 189);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1058, 629);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this._gridStockBalance);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1050, 603);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Stock Balance";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // _gridStockBalance
            // 
            this._gridStockBalance._extraWordShow = true;
            this._gridStockBalance._selectRow = -1;
            this._gridStockBalance.BackColor = System.Drawing.SystemColors.Window;
            this._gridStockBalance.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridStockBalance.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridStockBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridStockBalance.Font = new System.Drawing.Font("Tahoma", 9F);
            this._gridStockBalance.Location = new System.Drawing.Point(3, 3);
            this._gridStockBalance.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridStockBalance.Name = "_gridStockBalance";
            this._gridStockBalance.Size = new System.Drawing.Size(1044, 597);
            this._gridStockBalance.TabIndex = 0;
            this._gridStockBalance.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this._getPicture);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(428, 461);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Picture";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // _getPicture
            // 
            this._getPicture._DisplayPictureAmount = 5;
            this._getPicture._isScanner = false;
            this._getPicture._isWebcam = false;
            this._getPicture.AutoSize = true;
            this._getPicture.BackColor = System.Drawing.Color.Transparent;
            this._getPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this._getPicture.Location = new System.Drawing.Point(3, 3);
            this._getPicture.Name = "_getPicture";
            this._getPicture.Size = new System.Drawing.Size(422, 455);
            this._getPicture.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 818);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1058, 100);
            this.panel2.TabIndex = 7;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(10, 26);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(418, 55);
            this.textBox2.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(7, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 14);
            this.label2.TabIndex = 7;
            this.label2.Text = "Applicable With :";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._applicablePartListBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this._partDetailNameLabel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1058, 189);
            this.panel1.TabIndex = 6;
            // 
            // _applicablePartListBox
            // 
            this._applicablePartListBox.FormattingEnabled = true;
            this._applicablePartListBox.Location = new System.Drawing.Point(14, 84);
            this._applicablePartListBox.Name = "_applicablePartListBox";
            this._applicablePartListBox.Size = new System.Drawing.Size(414, 82);
            this._applicablePartListBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(7, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 14);
            this.label4.TabIndex = 5;
            this.label4.Text = "Applicable Part :";
            // 
            // _partDetailNameLabel
            // 
            this._partDetailNameLabel.AutoSize = true;
            this._partDetailNameLabel.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._partDetailNameLabel.Location = new System.Drawing.Point(8, 4);
            this._partDetailNameLabel.Name = "_partDetailNameLabel";
            this._partDetailNameLabel.Size = new System.Drawing.Size(46, 23);
            this._partDetailNameLabel.TabIndex = 2;
            this._partDetailNameLabel.Text = "xxxx";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Price :";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(55, 36);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(153, 20);
            this.textBox1.TabIndex = 4;
            // 
            // _icSpecialPartRelateControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "_icSpecialPartRelateControl";
            this.Size = new System.Drawing.Size(1058, 918);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TabControl tabControl1;
        public System.Windows.Forms.TabPage tabPage1;
        public MyLib._myGrid _gridStockBalance;
        public System.Windows.Forms.TabPage tabPage2;
        public SMLERPControl._getPicture _getPicture;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.TextBox textBox2;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ListBox _applicablePartListBox;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label _partDetailNameLabel;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBox1;
    }
}
