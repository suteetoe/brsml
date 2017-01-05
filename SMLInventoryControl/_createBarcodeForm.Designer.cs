namespace SMLInventoryControl
{
    partial class _createBarcodeForm
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
            this.label1 = new System.Windows.Forms.Label();
            this._firstDigitTextBox = new System.Windows.Forms.TextBox();
            this._barCodelabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._closeButton = new MyLib.VistaButton();
            this._saveButton = new MyLib.VistaButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "First Digit :";
            // 
            // _firstDigitTextBox
            // 
            this._firstDigitTextBox.Location = new System.Drawing.Point(159, 11);
            this._firstDigitTextBox.Name = "_firstDigitTextBox";
            this._firstDigitTextBox.Size = new System.Drawing.Size(242, 22);
            this._firstDigitTextBox.TabIndex = 1;
            // 
            // _barCodelabel
            // 
            this._barCodelabel.AutoSize = true;
            this._barCodelabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._barCodelabel.Location = new System.Drawing.Point(156, 48);
            this._barCodelabel.Name = "_barCodelabel";
            this._barCodelabel.Size = new System.Drawing.Size(19, 14);
            this._barCodelabel.TabIndex = 2;
            this._barCodelabel.Text = "...";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this._saveButton);
            this.flowLayoutPanel1.Controls.Add(this._closeButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 89);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(410, 30);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // _closeButton
            // 
            this._closeButton.AutoSize = true;
            this._closeButton.BackColor = System.Drawing.Color.Transparent;
            this._closeButton.ButtonText = "Close";
            this._closeButton.Location = new System.Drawing.Point(275, 3);
            this._closeButton.myImage = global::SMLInventoryControl.Properties.Resources.error1;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(64, 24);
            this._closeButton.TabIndex = 1;
            this._closeButton.Text = "Close";
            this._closeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._closeButton.UseVisualStyleBackColor = false;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _saveButton
            // 
            this._saveButton.AutoSize = true;
            this._saveButton.BackColor = System.Drawing.Color.Transparent;
            this._saveButton.ButtonText = "Save";
            this._saveButton.Location = new System.Drawing.Point(345, 3);
            this._saveButton.myImage = global::SMLInventoryControl.Properties.Resources.disk_blue;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(62, 24);
            this._saveButton.TabIndex = 2;
            this._saveButton.Text = "Save";
            this._saveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._saveButton.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::SMLInventoryControl.Properties.Resources.barcode;
            this.pictureBox1.Location = new System.Drawing.Point(12, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(70, 31);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // _createBarcodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 119);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this._barCodelabel);
            this.Controls.Add(this._firstDigitTextBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "_createBarcodeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Barcode";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _firstDigitTextBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MyLib.VistaButton _closeButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Label _barCodelabel;
        public MyLib.VistaButton _saveButton;

    }
}