namespace MyLib
{
    partial class _myMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_myMessageBox));
            this.labelBody = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonNotoAll = new System.Windows.Forms.Button();
            this.buttonNo = new System.Windows.Forms.Button();
            this.buttonYestoAll = new System.Windows.Forms.Button();
            this.buttonYes = new System.Windows.Forms.Button();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // labelBody
            // 
            this.labelBody.AutoSize = true;
            this.labelBody.Location = new System.Drawing.Point(118, 12);
            this.labelBody.Name = "labelBody";
            this.labelBody.Size = new System.Drawing.Size(272, 169);
            this.labelBody.TabIndex = 13;
            this.labelBody.Text = resources.GetString("labelBody.Text");
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(347, 194);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 12;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonNotoAll
            // 
            this.buttonNotoAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNotoAll.Location = new System.Drawing.Point(266, 194);
            this.buttonNotoAll.Name = "buttonNotoAll";
            this.buttonNotoAll.Size = new System.Drawing.Size(75, 23);
            this.buttonNotoAll.TabIndex = 11;
            this.buttonNotoAll.Text = "Not to All";
            this.buttonNotoAll.UseVisualStyleBackColor = true;
            this.buttonNotoAll.Click += new System.EventHandler(this.buttonNotoAll_Click);
            // 
            // buttonNo
            // 
            this.buttonNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNo.Location = new System.Drawing.Point(185, 194);
            this.buttonNo.Name = "buttonNo";
            this.buttonNo.Size = new System.Drawing.Size(75, 23);
            this.buttonNo.TabIndex = 10;
            this.buttonNo.Text = "No";
            this.buttonNo.UseVisualStyleBackColor = true;
            this.buttonNo.Click += new System.EventHandler(this.buttonNo_Click);
            // 
            // buttonYestoAll
            // 
            this.buttonYestoAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonYestoAll.Location = new System.Drawing.Point(104, 194);
            this.buttonYestoAll.Name = "buttonYestoAll";
            this.buttonYestoAll.Size = new System.Drawing.Size(75, 23);
            this.buttonYestoAll.TabIndex = 9;
            this.buttonYestoAll.Text = "Yes to All";
            this.buttonYestoAll.UseVisualStyleBackColor = true;
            this.buttonYestoAll.Click += new System.EventHandler(this.buttonYestoAll_Click);
            // 
            // buttonYes
            // 
            this.buttonYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonYes.Location = new System.Drawing.Point(23, 194);
            this.buttonYes.Name = "buttonYes";
            this.buttonYes.Size = new System.Drawing.Size(75, 23);
            this.buttonYes.TabIndex = 8;
            this.buttonYes.Text = "Yes";
            this.buttonYes.UseVisualStyleBackColor = true;
            this.buttonYes.Click += new System.EventHandler(this.buttonYes_Click);
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(100, 100);
            this.pictureBoxIcon.TabIndex = 7;
            this.pictureBoxIcon.TabStop = false;
            // 
            // _myMessageBox
            // 
            this.AcceptButton = this.buttonYes;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(434, 229);
            this.Controls.Add(this.labelBody);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonNotoAll);
            this.Controls.Add(this.buttonNo);
            this.Controls.Add(this.buttonYestoAll);
            this.Controls.Add(this.buttonYes);
            this.Controls.Add(this.pictureBoxIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_myMessageBox";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_myMessageBox";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBody;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Button buttonNotoAll;
        public System.Windows.Forms.Button buttonNo;
        public System.Windows.Forms.Button buttonYestoAll;
        public System.Windows.Forms.Button buttonYes;
    }
}