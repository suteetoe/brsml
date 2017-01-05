namespace SMLInventoryControl._icPriceManage
{
    partial class _getICForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_getICForm));
            this._queryTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _queryTextbox
            // 
            this._queryTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._queryTextbox.Location = new System.Drawing.Point(0, 0);
            this._queryTextbox.Multiline = true;
            this._queryTextbox.Name = "_queryTextbox";
            this._queryTextbox.Size = new System.Drawing.Size(784, 562);
            this._queryTextbox.TabIndex = 2;
            // 
            // _getICForm
            // 
            this._colorBackground = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this._queryTextbox);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "_getICForm";
            this.ResourceName = "ขอบเขตข้อมูลสินค้า";
            this.Text = "ขอบเขตข้อมูลสินค้า";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox _queryTextbox;


    }
}