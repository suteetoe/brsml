namespace SMLFastReportUpdate
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._hostTextBox = new System.Windows.Forms.TextBox();
            this._userTextBox = new System.Windows.Forms.TextBox();
            this._passwordTextBox = new System.Windows.Forms.TextBox();
            this._connectButton = new System.Windows.Forms.Button();
            this._updateButton = new System.Windows.Forms.Button();
            this._portTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._databaseNameTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this._myGrid1 = new MyLib._myGrid();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Host : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(66, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "User : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Password : ";
            // 
            // _hostTextBox
            // 
            this._hostTextBox.Location = new System.Drawing.Point(108, 17);
            this._hostTextBox.Name = "_hostTextBox";
            this._hostTextBox.Size = new System.Drawing.Size(272, 20);
            this._hostTextBox.TabIndex = 4;
            // 
            // _userTextBox
            // 
            this._userTextBox.Location = new System.Drawing.Point(108, 61);
            this._userTextBox.Name = "_userTextBox";
            this._userTextBox.Size = new System.Drawing.Size(272, 20);
            this._userTextBox.TabIndex = 6;
            // 
            // _passwordTextBox
            // 
            this._passwordTextBox.Location = new System.Drawing.Point(108, 83);
            this._passwordTextBox.Name = "_passwordTextBox";
            this._passwordTextBox.Size = new System.Drawing.Size(272, 20);
            this._passwordTextBox.TabIndex = 7;
            // 
            // _connectButton
            // 
            this._connectButton.Location = new System.Drawing.Point(108, 126);
            this._connectButton.Name = "_connectButton";
            this._connectButton.Size = new System.Drawing.Size(75, 23);
            this._connectButton.TabIndex = 9;
            this._connectButton.Text = "Connect";
            this._connectButton.UseVisualStyleBackColor = true;
            this._connectButton.Click += new System.EventHandler(this._connectButton_Click);
            // 
            // _updateButton
            // 
            this._updateButton.Enabled = false;
            this._updateButton.Location = new System.Drawing.Point(108, 151);
            this._updateButton.Name = "_updateButton";
            this._updateButton.Size = new System.Drawing.Size(139, 23);
            this._updateButton.TabIndex = 10;
            this._updateButton.Text = "Update Report";
            this._updateButton.UseVisualStyleBackColor = true;
            this._updateButton.Click += new System.EventHandler(this._updateButton_Click);
            // 
            // _portTextBox
            // 
            this._portTextBox.Location = new System.Drawing.Point(108, 39);
            this._portTextBox.Name = "_portTextBox";
            this._portTextBox.Size = new System.Drawing.Size(272, 20);
            this._portTextBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Port : ";
            // 
            // _databaseNameTextBox
            // 
            this._databaseNameTextBox.Location = new System.Drawing.Point(108, 105);
            this._databaseNameTextBox.Name = "_databaseNameTextBox";
            this._databaseNameTextBox.Size = new System.Drawing.Size(272, 20);
            this._databaseNameTextBox.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "DatabaseName : ";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 669);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this._hostTextBox);
            this.tabPage1.Controls.Add(this._databaseNameTextBox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this._portTextBox);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this._userTextBox);
            this.tabPage1.Controls.Add(this._updateButton);
            this.tabPage1.Controls.Add(this._passwordTextBox);
            this.tabPage1.Controls.Add(this._connectButton);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 643);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Connect";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this._myGrid1);
            this.tabPage2.Controls.Add(this.toolStrip1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 643);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Detail";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(786, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(60, 22);
            this.toolStripButton1.Text = "Check";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(67, 22);
            this.toolStripButton2.Text = "Process";
            // 
            // _myGrid1
            // 
            this._myGrid1._extraWordShow = true;
            this._myGrid1._selectRow = -1;
            this._myGrid1.BackColor = System.Drawing.SystemColors.Window;
            this._myGrid1.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._myGrid1.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._myGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myGrid1.Font = new System.Drawing.Font("Tahoma", 9F);
            this._myGrid1.Location = new System.Drawing.Point(3, 28);
            this._myGrid1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._myGrid1.Name = "_myGrid1";
            this._myGrid1.Size = new System.Drawing.Size(786, 612);
            this._myGrid1.TabIndex = 1;
            this._myGrid1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 669);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "SML FastReport Update";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _hostTextBox;
        private System.Windows.Forms.TextBox _userTextBox;
        private System.Windows.Forms.TextBox _passwordTextBox;
        private System.Windows.Forms.Button _connectButton;
        private System.Windows.Forms.Button _updateButton;
        private System.Windows.Forms.TextBox _portTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _databaseNameTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private MyLib._myGrid _myGrid1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;

    }
}

