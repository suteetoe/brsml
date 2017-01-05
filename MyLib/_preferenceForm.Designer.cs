namespace MyLib
{
    partial class _preferenceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_preferenceForm));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._saveButton = new System.Windows.Forms.Button();
            this._fontTextbox = new System.Windows.Forms.TextBox();
            this._fontSelect = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Font : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._fontSelect);
            this.groupBox1.Controls.Add(this._fontTextbox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 99);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Font";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::MyLib.Resource16x16.disk_blue;
            this._saveButton.Location = new System.Drawing.Point(292, 130);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(80, 23);
            this._saveButton.TabIndex = 2;
            this._saveButton.Text = "Save";
            this._saveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._saveButton.UseVisualStyleBackColor = true;
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _fontTextbox
            // 
            this._fontTextbox.Location = new System.Drawing.Point(68, 17);
            this._fontTextbox.Name = "_fontTextbox";
            this._fontTextbox.Size = new System.Drawing.Size(176, 22);
            this._fontTextbox.TabIndex = 1;
            // 
            // _fontSelect
            // 
            this._fontSelect.Location = new System.Drawing.Point(244, 16);
            this._fontSelect.Name = "_fontSelect";
            this._fontSelect.Size = new System.Drawing.Size(30, 23);
            this._fontSelect.TabIndex = 2;
            this._fontSelect.Text = "...";
            this._fontSelect.UseVisualStyleBackColor = true;
            this._fontSelect.Click += new System.EventHandler(this.button1_Click);
            // 
            // _preferenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 161);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._saveButton);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_preferenceForm";
            this.Text = "Preference";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _saveButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button _fontSelect;
        private System.Windows.Forms.TextBox _fontTextbox;
    }
}