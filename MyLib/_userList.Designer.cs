namespace MyLib
{
    partial class _userList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_userList));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this._myPanel1 = new MyLib._myPanel();
            this._userListGrid = new MyLib._myGrid();
            this._myFlowLayoutPanel2 = new MyLib._myFlowLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._myShadowLabel1 = new MyLib._myShadowLabel(this.components);
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._buttonExit = new MyLib._myButton();
            this._buttonRefresh = new MyLib._myButton();
            this._myPanel1.SuspendLayout();
            this._myFlowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._userListGrid);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel2);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel1.Size = new System.Drawing.Size(648, 465);
            this._myPanel1.TabIndex = 0;
            // 
            // _userListGrid
            // 
            this._userListGrid._extraWordShow = true;
            this._userListGrid._selectRow = -1;
            this._userListGrid.BackColor = System.Drawing.SystemColors.Window;
            this._userListGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._userListGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            this._userListGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._userListGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._userListGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._userListGrid.Location = new System.Drawing.Point(5, 59);
            this._userListGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._userListGrid.Name = "_userListGrid";
            this._userListGrid.Size = new System.Drawing.Size(638, 375);
            this._userListGrid.TabIndex = 0;
            this._userListGrid.TabStop = false;
            // 
            // _myFlowLayoutPanel2
            // 
            this._myFlowLayoutPanel2.AutoSize = true;
            this._myFlowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel2.Controls.Add(this.pictureBox1);
            this._myFlowLayoutPanel2.Controls.Add(this._myShadowLabel1);
            this._myFlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel2.Location = new System.Drawing.Point(5, 5);
            this._myFlowLayoutPanel2.Name = "_myFlowLayoutPanel2";
            this._myFlowLayoutPanel2.Size = new System.Drawing.Size(638, 54);
            this._myFlowLayoutPanel2.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // _myShadowLabel1
            // 
            this._myShadowLabel1.Angle = 0F;
            this._myShadowLabel1.AutoSize = true;
            this._myShadowLabel1.DrawGradient = false;
            this._myShadowLabel1.EndColor = System.Drawing.Color.LightSkyBlue;
            this._myShadowLabel1.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myShadowLabel1.ForeColor = System.Drawing.Color.LightSkyBlue;
            this._myShadowLabel1.Location = new System.Drawing.Point(57, 0);
            this._myShadowLabel1.Name = "_myShadowLabel1";
            this._myShadowLabel1.ShadowColor = System.Drawing.Color.Black;
            this._myShadowLabel1.Size = new System.Drawing.Size(171, 45);
            this._myShadowLabel1.StartColor = System.Drawing.Color.White;
            this._myShadowLabel1.TabIndex = 5;
            this._myShadowLabel1.Text = "User list";
            this._myShadowLabel1.XOffset = 1F;
            this._myShadowLabel1.YOffset = 1F;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.AutoSize = true;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._buttonExit);
            this._myFlowLayoutPanel1.Controls.Add(this._buttonRefresh);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(5, 434);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(638, 26);
            this._myFlowLayoutPanel1.TabIndex = 3;
            // 
            // _buttonExit
            // 
            this._buttonExit.AutoSize = true;
            this._buttonExit.BackColor = System.Drawing.Color.Transparent;
            this._buttonExit.ButtonText = null;
            this._buttonExit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._buttonExit.Location = new System.Drawing.Point(598, 0);
            this._buttonExit.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._buttonExit.myImage = global::MyLib.Resource16x16.error;
            this._buttonExit.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonExit.myUseVisualStyleBackColor = true;
            this._buttonExit.Name = "_buttonExit";
            this._buttonExit.Padding = new System.Windows.Forms.Padding(1);
            this._buttonExit.ResourceName = "screen_close";
            this._buttonExit.Size = new System.Drawing.Size(32, 24);
            this._buttonExit.TabIndex = 1;
            this._buttonExit.Text = "Exit";
            this._buttonExit.UseVisualStyleBackColor = false;
            this._buttonExit.Click += new System.EventHandler(this._buttonExit_Click);
            // 
            // _buttonRefresh
            // 
            this._buttonRefresh.AutoSize = true;
            this._buttonRefresh.BackColor = System.Drawing.Color.Transparent;
            this._buttonRefresh.ButtonText = "refresh";
            this._buttonRefresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._buttonRefresh.Location = new System.Drawing.Point(522, 0);
            this._buttonRefresh.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._buttonRefresh.myImage = global::MyLib.Resource16x16.replace2;
            this._buttonRefresh.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonRefresh.myUseVisualStyleBackColor = true;
            this._buttonRefresh.Name = "_buttonRefresh";
            this._buttonRefresh.Padding = new System.Windows.Forms.Padding(1);
            this._buttonRefresh.ResourceName = "refresh";
            this._buttonRefresh.Size = new System.Drawing.Size(74, 24);
            this._buttonRefresh.TabIndex = 2;
            this._buttonRefresh.Text = "Refresh";
            this._buttonRefresh.UseVisualStyleBackColor = false;
            this._buttonRefresh.Click += new System.EventHandler(this._buttonRefresh_Click);
            // 
            // _userList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myPanel1);
            this.Name = "_userList";
            this.Size = new System.Drawing.Size(648, 465);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myFlowLayoutPanel2.ResumeLayout(false);
            this._myFlowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private _myPanel _myPanel1;
        private _myGrid _userListGrid;
        private _myFlowLayoutPanel _myFlowLayoutPanel1;
        private _myButton _buttonExit;
        private _myButton _buttonRefresh;
        private System.Windows.Forms.PictureBox pictureBox1;
        private _myFlowLayoutPanel _myFlowLayoutPanel2;
        private _myShadowLabel _myShadowLabel1;
        private System.Windows.Forms.Timer timer1;
    }
}
