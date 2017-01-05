namespace SMLERPIC
{
    partial class _icBuildPriceList
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
            this._processButton = new System.Windows.Forms.Button();
            this._folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this._folder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._closeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _processButton
            // 
            this._processButton.Location = new System.Drawing.Point(61, 51);
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(75, 23);
            this._processButton.TabIndex = 0;
            this._processButton.Text = "Process";
            this._processButton.UseVisualStyleBackColor = true;
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _folder
            // 
            this._folder.Location = new System.Drawing.Point(61, 12);
            this._folder.Name = "_folder";
            this._folder.Size = new System.Drawing.Size(377, 22);
            this._folder.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Folder";
            // 
            // _closeButton
            // 
            this._closeButton.Location = new System.Drawing.Point(151, 51);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(75, 23);
            this._closeButton.TabIndex = 3;
            this._closeButton.Text = "Close";
            this._closeButton.UseVisualStyleBackColor = true;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _icBuildPriceList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._closeButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._folder);
            this.Controls.Add(this._processButton);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_icBuildPriceList";
            this.Size = new System.Drawing.Size(537, 403);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _processButton;
        private System.Windows.Forms.FolderBrowserDialog _folderBrowserDialog;
        private System.Windows.Forms.TextBox _folder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _closeButton;
    }
}
