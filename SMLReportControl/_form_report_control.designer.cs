namespace SMLInventoryControl
{
    partial class _form_report_control
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
            this._formPreviewDialog = new SMLInventoryControl.FormPrintPreviewDialogControl();
            this.SuspendLayout();
            // 
            // _formPreviewDialog
            // 
            this._formPreviewDialog.Dock = System.Windows.Forms.DockStyle.Fill;
            this._formPreviewDialog.Document = null;
            this._formPreviewDialog.Location = new System.Drawing.Point(0, 0);
            this._formPreviewDialog.Name = "_formPreviewDialog";
            this._formPreviewDialog.Size = new System.Drawing.Size(639, 442);
            this._formPreviewDialog.TabIndex = 0;
            // 
            // _form_report_control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 442);
            this.ControlBox = false;
            this.Controls.Add(this._formPreviewDialog);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_form_report_control";
            this.Text = "_form_report_control";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private SMLInventoryControl.FormPrintPreviewDialogControl _formPreviewDialog;
    }
}