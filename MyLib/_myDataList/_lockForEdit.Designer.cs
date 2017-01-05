namespace MyLib.My_DataList
{
    partial class _lockForEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_lockForEdit));
            this._myPanel1 = new MyLib._myPanel();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._buttonNo = new MyLib._myButton();
            this._buttonYes = new MyLib._myButton();
            this._myShadowLabel1 = new MyLib._myShadowLabel(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._message = new System.Windows.Forms.TextBox();
            this._showAgain = new MyLib._myCheckBox();
            this._myPanel1.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myPanel1.BeginColor = System.Drawing.Color.White;
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel1);
            this._myPanel1.Controls.Add(this._myShadowLabel1);
            this._myPanel1.Controls.Add(this.pictureBox1);
            this._myPanel1.Controls.Add(this._message);
            this._myPanel1.Controls.Add(this._showAgain);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.Cornsilk;
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel1.Size = new System.Drawing.Size(287, 181);
            this._myPanel1.TabIndex = 0;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.AutoSize = true;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._buttonNo);
            this._myFlowLayoutPanel1.Controls.Add(this._buttonYes);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(5, 150);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(277, 26);
            this._myFlowLayoutPanel1.TabIndex = 5;
            // 
            // _buttonNo
            // 
            this._buttonNo.AutoSize = true;
            this._buttonNo.BackColor = System.Drawing.Color.Transparent;
            this._buttonNo.ButtonText = "ยกเลิก";
            this._buttonNo.Location = new System.Drawing.Point(207, 0);
            this._buttonNo.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._buttonNo.myImage = global::MyLib.Resource16x16.delete2;
            this._buttonNo.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonNo.myUseVisualStyleBackColor = true;
            this._buttonNo.Name = "_buttonNo";
            this._buttonNo.Padding = new System.Windows.Forms.Padding(1);
            this._buttonNo.ResourceName = "cancel";
            this._buttonNo.Size = new System.Drawing.Size(69, 24);
            this._buttonNo.TabIndex = 1;
            this._buttonNo.Text = "No";
            this._buttonNo.UseVisualStyleBackColor = false;
            this._buttonNo.Click += new System.EventHandler(this._buttonNo_Click);
            // 
            // _buttonYes
            // 
            this._buttonYes.AutoSize = true;
            this._buttonYes.BackColor = System.Drawing.Color.Transparent;
            this._buttonYes.ButtonText = "ตกลง";
            this._buttonYes.Location = new System.Drawing.Point(141, 0);
            this._buttonYes.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._buttonYes.myImage = global::MyLib.Resource16x16.check2;
            this._buttonYes.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonYes.myUseVisualStyleBackColor = true;
            this._buttonYes.Name = "_buttonYes";
            this._buttonYes.Padding = new System.Windows.Forms.Padding(1);
            this._buttonYes.ResourceName = "yes";
            this._buttonYes.Size = new System.Drawing.Size(64, 24);
            this._buttonYes.TabIndex = 0;
            this._buttonYes.Text = "Yes";
            this._buttonYes.UseVisualStyleBackColor = false;
            this._buttonYes.Click += new System.EventHandler(this._buttonYes_Click);
            // 
            // _myShadowLabel1
            // 
            this._myShadowLabel1.Angle = 0F;
            this._myShadowLabel1.AutoSize = true;
            this._myShadowLabel1.BackColor = System.Drawing.Color.Transparent;
            this._myShadowLabel1.DrawGradient = false;
            this._myShadowLabel1.EndColor = System.Drawing.Color.LightSkyBlue;
            this._myShadowLabel1.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myShadowLabel1.ForeColor = System.Drawing.Color.LightSkyBlue;
            this._myShadowLabel1.Location = new System.Drawing.Point(66, 14);
            this._myShadowLabel1.Name = "_myShadowLabel1";
            this._myShadowLabel1.ShadowColor = System.Drawing.Color.Black;
            this._myShadowLabel1.Size = new System.Drawing.Size(213, 39);
            this._myShadowLabel1.StartColor = System.Drawing.Color.White;
            this._myShadowLabel1.TabIndex = 4;
            this._myShadowLabel1.Text = "Lock Record";
            this._myShadowLabel1.XOffset = 1F;
            this._myShadowLabel1.YOffset = 1F;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // _message
            // 
            this._message.BackColor = System.Drawing.Color.White;
            this._message.Location = new System.Drawing.Point(8, 85);
            this._message.Multiline = true;
            this._message.Name = "_message";
            this._message.ReadOnly = true;
            this._message.Size = new System.Drawing.Size(271, 57);
            this._message.TabIndex = 3;
            // 
            // _showAgain
            // 
            this._showAgain.AutoSize = true;
            this._showAgain.BackColor = System.Drawing.Color.Transparent;
            this._showAgain.Checked = true;
            this._showAgain.CheckState = System.Windows.Forms.CheckState.Checked;
            this._showAgain.Location = new System.Drawing.Point(8, 62);
            this._showAgain.Name = "_showAgain";
            this._showAgain.ResourceName = "show_me_again";
            this._showAgain.Size = new System.Drawing.Size(125, 18);
            this._showAgain.TabIndex = 2;
            this._showAgain.Text = "แสดงข้อความนี้เสมอ";
            this._showAgain.UseVisualStyleBackColor = false;
            // 
            // _lockForEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(287, 181);
            this.ControlBox = false;
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "_lockForEdit";
            this.ShowInTaskbar = false;
            this.Text = "Warning...";
            this.Load += new System.EventHandler(this._lockForEdit_Load);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private _myPanel _myPanel1;
        private _myButton _buttonNo;
        private _myButton _buttonYes;
        private System.Windows.Forms.TextBox _message;
        public _myCheckBox _showAgain;
        private System.Windows.Forms.PictureBox pictureBox1;
        private _myShadowLabel _myShadowLabel1;
        private _myFlowLayoutPanel _myFlowLayoutPanel1;
    }
}