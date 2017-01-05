namespace SMLReport._formReport
{
    partial class _selectBackgroundForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_selectBackgroundForm));
            this._showBackgroundCheckBox = new System.Windows.Forms.CheckBox();
            this._loadButton = new System.Windows.Forms.Button();
            this._imageList = new System.Windows.Forms.ImageList(this.components);
            this._removeButton = new System.Windows.Forms.Button();
            this._pictureBox = new System.Windows.Forms.PictureBox();
            this._saveButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._leftMarginText = new System.Windows.Forms.TextBox();
            this._topMarginText = new System.Windows.Forms.TextBox();
            this._imageWidthText = new System.Windows.Forms.TextBox();
            this._imageHeightText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._fitPageBackgroundCheckbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _showBackgroundCheckBox
            // 
            this._showBackgroundCheckBox.AutoSize = true;
            this._showBackgroundCheckBox.Location = new System.Drawing.Point(12, 12);
            this._showBackgroundCheckBox.Name = "_showBackgroundCheckBox";
            this._showBackgroundCheckBox.Size = new System.Drawing.Size(125, 18);
            this._showBackgroundCheckBox.TabIndex = 0;
            this._showBackgroundCheckBox.Text = "Show Background";
            this._showBackgroundCheckBox.UseVisualStyleBackColor = true;
            // 
            // _loadButton
            // 
            this._loadButton.ImageKey = "folder_into.png";
            this._loadButton.ImageList = this._imageList;
            this._loadButton.Location = new System.Drawing.Point(168, 7);
            this._loadButton.Name = "_loadButton";
            this._loadButton.Size = new System.Drawing.Size(150, 23);
            this._loadButton.TabIndex = 1;
            this._loadButton.Text = "Load Background";
            this._loadButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._loadButton.UseVisualStyleBackColor = true;
            this._loadButton.Click += new System.EventHandler(this._loadButton_Click);
            // 
            // _imageList
            // 
            this._imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList.ImageStream")));
            this._imageList.TransparentColor = System.Drawing.Color.Transparent;
            this._imageList.Images.SetKeyName(0, "folder_into.png");
            this._imageList.Images.SetKeyName(1, "delete2.png");
            this._imageList.Images.SetKeyName(2, "disk_blue.png");
            this._imageList.Images.SetKeyName(3, "undo.png");
            // 
            // _removeButton
            // 
            this._removeButton.ImageKey = "delete2.png";
            this._removeButton.ImageList = this._imageList;
            this._removeButton.Location = new System.Drawing.Point(345, 7);
            this._removeButton.Name = "_removeButton";
            this._removeButton.Size = new System.Drawing.Size(144, 23);
            this._removeButton.TabIndex = 2;
            this._removeButton.Text = "Remove Background";
            this._removeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._removeButton.UseVisualStyleBackColor = true;
            this._removeButton.Click += new System.EventHandler(this._removeButton_Click);
            // 
            // _pictureBox
            // 
            this._pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pictureBox.Location = new System.Drawing.Point(13, 66);
            this._pictureBox.Name = "_pictureBox";
            this._pictureBox.Size = new System.Drawing.Size(476, 366);
            this._pictureBox.TabIndex = 3;
            this._pictureBox.TabStop = false;
            // 
            // _saveButton
            // 
            this._saveButton.ImageKey = "disk_blue.png";
            this._saveButton.ImageList = this._imageList;
            this._saveButton.Location = new System.Drawing.Point(333, 438);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(75, 23);
            this._saveButton.TabIndex = 4;
            this._saveButton.Text = "Save";
            this._saveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._saveButton.UseVisualStyleBackColor = true;
            // 
            // _cancelButton
            // 
            this._cancelButton.ImageKey = "undo.png";
            this._cancelButton.ImageList = this._imageList;
            this._cancelButton.Location = new System.Drawing.Point(414, 438);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 5;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(42, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "LeftMargin : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(284, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 14);
            this.label2.TabIndex = 7;
            this.label2.Text = "TopMargin : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _leftMarginText
            // 
            this._leftMarginText.Location = new System.Drawing.Point(119, 38);
            this._leftMarginText.Name = "_leftMarginText";
            this._leftMarginText.Size = new System.Drawing.Size(109, 22);
            this._leftMarginText.TabIndex = 8;
            // 
            // _topMarginText
            // 
            this._topMarginText.Location = new System.Drawing.Point(361, 38);
            this._topMarginText.Name = "_topMarginText";
            this._topMarginText.Size = new System.Drawing.Size(109, 22);
            this._topMarginText.TabIndex = 9;
            // 
            // _imageWidthText
            // 
            this._imageWidthText.Location = new System.Drawing.Point(119, 66);
            this._imageWidthText.Name = "_imageWidthText";
            this._imageWidthText.Size = new System.Drawing.Size(109, 22);
            this._imageWidthText.TabIndex = 13;
            // 
            // _imageHeightText
            // 
            this._imageHeightText.Location = new System.Drawing.Point(361, 69);
            this._imageHeightText.Name = "_imageHeightText";
            this._imageHeightText.Size = new System.Drawing.Size(109, 22);
            this._imageHeightText.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(284, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 14);
            this.label3.TabIndex = 11;
            this.label3.Text = "Height : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(42, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 14);
            this.label4.TabIndex = 10;
            this.label4.Text = "Width : ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _fitPageBackgroundCheckbox
            // 
            this._fitPageBackgroundCheckbox.AutoSize = true;
            this._fitPageBackgroundCheckbox.Location = new System.Drawing.Point(168, 12);
            this._fitPageBackgroundCheckbox.Name = "_fitPageBackgroundCheckbox";
            this._fitPageBackgroundCheckbox.Size = new System.Drawing.Size(86, 18);
            this._fitPageBackgroundCheckbox.TabIndex = 14;
            this._fitPageBackgroundCheckbox.Text = "Fit to Page";
            this._fitPageBackgroundCheckbox.UseVisualStyleBackColor = true;
            // 
            // _selectBackgroundForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 471);
            this.ControlBox = false;
            this.Controls.Add(this._topMarginText);
            this.Controls.Add(this._leftMarginText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._saveButton);
            this.Controls.Add(this._pictureBox);
            this.Controls.Add(this._removeButton);
            this.Controls.Add(this._loadButton);
            this.Controls.Add(this._showBackgroundCheckBox);
            this.Controls.Add(this._imageWidthText);
            this.Controls.Add(this._imageHeightText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._fitPageBackgroundCheckbox);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_selectBackgroundForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_selectBackgroundForm";
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _loadButton;
        private System.Windows.Forms.Button _removeButton;
        private System.Windows.Forms.ImageList _imageList;
        public System.Windows.Forms.Button _saveButton;
        protected System.Windows.Forms.Button _cancelButton;
        public System.Windows.Forms.PictureBox _pictureBox;
        public System.Windows.Forms.CheckBox _showBackgroundCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox _leftMarginText;
        public System.Windows.Forms.TextBox _topMarginText;
        public System.Windows.Forms.TextBox _imageWidthText;
        public System.Windows.Forms.TextBox _imageHeightText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.CheckBox _fitPageBackgroundCheckbox;
    }
}