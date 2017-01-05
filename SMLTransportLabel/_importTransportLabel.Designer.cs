namespace SMLTransportLabel
{
    partial class _importTransportLabel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_importTransportLabel));
            this._importButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _importButton
            // 
            this._importButton.Image = global::SMLTransportLabel.Properties.Resources.flash;
            this._importButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._importButton.Location = new System.Drawing.Point(31, 27);
            this._importButton.Name = "_importButton";
            this._importButton.Size = new System.Drawing.Size(157, 23);
            this._importButton.TabIndex = 0;
            this._importButton.Text = " Import Now !!!";
            this._importButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._importButton.UseVisualStyleBackColor = true;
            this._importButton.Click += new System.EventHandler(this._importButton_Click);
            // 
            // _importTransportLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(217, 78);
            this.Controls.Add(this._importButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_importTransportLabel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "นำเข้าที่อยู่จัดส่ง";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _importButton;
    }
}