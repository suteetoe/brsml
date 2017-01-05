namespace SMLPosClient
{
    partial class _itemSearchLevelForm
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
            this._itemSearchLevelControl1 = new SMLInventoryControl._itemSearchLevelControl();
            this.SuspendLayout();
            // 
            // _itemSearchLevelControl1
            // 
            this._itemSearchLevelControl1.@__where = null;
            this._itemSearchLevelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemSearchLevelControl1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._itemSearchLevelControl1.Location = new System.Drawing.Point(0, 0);
            this._itemSearchLevelControl1.Name = "_itemSearchLevelControl1";
            this._itemSearchLevelControl1.Size = new System.Drawing.Size(651, 415);
            this._itemSearchLevelControl1.TabIndex = 0;
            // 
            // _itemSearchLevelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 415);
            this.Controls.Add(this._itemSearchLevelControl1);
            this.Name = "_itemSearchLevelForm";
            this.Text = "_itemSearchLevelForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        public SMLInventoryControl._itemSearchLevelControl _itemSearchLevelControl1;

    }
}