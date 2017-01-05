namespace SMLPOSControl._food
{
    partial class _foodDetailForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._qtyTextBox = new System.Windows.Forms.TextBox();
            this._priceTextBox = new System.Windows.Forms.TextBox();
            this._remarkTextBox = new System.Windows.Forms.TextBox();
            this._deleteButton = new System.Windows.Forms.Button();
            this._saveButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._itemNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "จำนวน :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(50, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "ราคา :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "หมายเหตุ :";
            // 
            // _qtyTextBox
            // 
            this._qtyTextBox.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._qtyTextBox.Location = new System.Drawing.Point(122, 58);
            this._qtyTextBox.Name = "_qtyTextBox";
            this._qtyTextBox.Size = new System.Drawing.Size(236, 30);
            this._qtyTextBox.TabIndex = 3;
            this._qtyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _priceTextBox
            // 
            this._priceTextBox.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._priceTextBox.Location = new System.Drawing.Point(122, 94);
            this._priceTextBox.Name = "_priceTextBox";
            this._priceTextBox.Size = new System.Drawing.Size(236, 30);
            this._priceTextBox.TabIndex = 4;
            this._priceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _remarkTextBox
            // 
            this._remarkTextBox.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._remarkTextBox.Location = new System.Drawing.Point(122, 130);
            this._remarkTextBox.Multiline = true;
            this._remarkTextBox.Name = "_remarkTextBox";
            this._remarkTextBox.Size = new System.Drawing.Size(409, 85);
            this._remarkTextBox.TabIndex = 5;
            // 
            // _deleteButton
            // 
            this._deleteButton.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._deleteButton.Image = global::SMLPOSControl.Properties.Resources.del;
            this._deleteButton.Location = new System.Drawing.Point(8, 221);
            this._deleteButton.Name = "_deleteButton";
            this._deleteButton.Size = new System.Drawing.Size(121, 52);
            this._deleteButton.TabIndex = 6;
            this._deleteButton.Text = "ลบรายการ";
            this._deleteButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._deleteButton.UseVisualStyleBackColor = true;
            // 
            // _saveButton
            // 
            this._saveButton.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._saveButton.Image = global::SMLPOSControl.Properties.Resources.save;
            this._saveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._saveButton.Location = new System.Drawing.Point(273, 221);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(126, 52);
            this._saveButton.TabIndex = 7;
            this._saveButton.Text = "บันทึก";
            this._saveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._saveButton.UseVisualStyleBackColor = true;
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._cancelButton.Image = global::SMLPOSControl.Properties.Resources.undo;
            this._cancelButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._cancelButton.Location = new System.Drawing.Point(405, 221);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(126, 52);
            this._cancelButton.TabIndex = 8;
            this._cancelButton.Text = "ยกเลิก";
            this._cancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _itemNameLabel
            // 
            this._itemNameLabel.AutoSize = true;
            this._itemNameLabel.BackColor = System.Drawing.Color.Transparent;
            this._itemNameLabel.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._itemNameLabel.ForeColor = System.Drawing.Color.MediumBlue;
            this._itemNameLabel.Location = new System.Drawing.Point(8, 9);
            this._itemNameLabel.Name = "_itemNameLabel";
            this._itemNameLabel.Size = new System.Drawing.Size(148, 33);
            this._itemNameLabel.TabIndex = 9;
            this._itemNameLabel.Text = "Item Name";
            // 
            // _foodDetailForm
            // 
            this._colorBegin = System.Drawing.Color.White;
            this._colorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 282);
            this.Controls.Add(this._itemNameLabel);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._saveButton);
            this.Controls.Add(this._deleteButton);
            this.Controls.Add(this._remarkTextBox);
            this.Controls.Add(this._priceTextBox);
            this.Controls.Add(this._qtyTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_foodDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "  แก้ไขรายละเอียดการสั่งอาหาร ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button _cancelButton;
        public System.Windows.Forms.TextBox _qtyTextBox;
        public System.Windows.Forms.TextBox _priceTextBox;
        public System.Windows.Forms.TextBox _remarkTextBox;
        public System.Windows.Forms.Button _deleteButton;
        public System.Windows.Forms.Button _saveButton;
        public System.Windows.Forms.Label _itemNameLabel;
    }
}