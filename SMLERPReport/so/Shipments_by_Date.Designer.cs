﻿namespace SMLERPReport.so
{
    partial class Shipments_by_Date
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
            this._report_so1 = new SMLERPReport.so._report_so();
            this.SuspendLayout();
            // 
            // _report_so1
            // 
            this._report_so1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._report_so1.Location = new System.Drawing.Point(0, 0);
            this._report_so1.Name = "_report_so1";
            this._report_so1.Size = new System.Drawing.Size(731, 365);
            this._report_so1.SoType = SMLERPReport.so._report_so._soEnum.Shipments_by_Date;
            this._report_so1.TabIndex = 0;
            // 
            // Shipments_by_Date
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._report_so1);
            this.Name = "Shipments_by_Date";
            this.Size = new System.Drawing.Size(731, 365);
            this.ResumeLayout(false);

        }

        #endregion

        private _report_so _report_so1;
    }
}
