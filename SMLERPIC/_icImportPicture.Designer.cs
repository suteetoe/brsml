namespace SMLERPIC
{
    partial class _icImportPicture
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_icImportPicture));
            this.label1 = new System.Windows.Forms.Label();
            this._foldersLocationTextbox = new System.Windows.Forms.TextBox();
            this._buttonProcess = new System.Windows.Forms.Button();
            this._buttonBrowseFolders = new System.Windows.Forms.Button();
            this._viewFileDirectory = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this._labelResult = new System.Windows.Forms.Label();
            this._progressbar = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this._close = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "เลือก Folders ปลายทาง : ";
            // 
            // _foldersLocationTextbox
            // 
            this._foldersLocationTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._foldersLocationTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._foldersLocationTextbox.Location = new System.Drawing.Point(148, 10);
            this._foldersLocationTextbox.Name = "_foldersLocationTextbox";
            this._foldersLocationTextbox.Size = new System.Drawing.Size(531, 22);
            this._foldersLocationTextbox.TabIndex = 1;
            // 
            // _buttonProcess
            // 
            this._buttonProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonProcess.Location = new System.Drawing.Point(637, 10);
            this._buttonProcess.Name = "_buttonProcess";
            this._buttonProcess.Size = new System.Drawing.Size(73, 26);
            this._buttonProcess.TabIndex = 2;
            this._buttonProcess.Text = "Process";
            this._buttonProcess.UseVisualStyleBackColor = true;
            this._buttonProcess.Click += new System.EventHandler(this._buttonProcess_Click);
            // 
            // _buttonBrowseFolders
            // 
            this._buttonBrowseFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonBrowseFolders.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._buttonBrowseFolders.Location = new System.Drawing.Point(689, 8);
            this._buttonBrowseFolders.Name = "_buttonBrowseFolders";
            this._buttonBrowseFolders.Size = new System.Drawing.Size(101, 26);
            this._buttonBrowseFolders.TabIndex = 3;
            this._buttonBrowseFolders.Text = "Browse";
            this._buttonBrowseFolders.UseVisualStyleBackColor = true;
            this._buttonBrowseFolders.Click += new System.EventHandler(this._buttonBrowseFolders_Click);
            // 
            // _viewFileDirectory
            // 
            this._viewFileDirectory.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this._viewFileDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this._viewFileDirectory.Location = new System.Drawing.Point(0, 66);
            this._viewFileDirectory.Name = "_viewFileDirectory";
            this._viewFileDirectory.Size = new System.Drawing.Size(804, 403);
            this._viewFileDirectory.StateImageList = this.imageList1;
            this._viewFileDirectory.TabIndex = 4;
            this._viewFileDirectory.UseCompatibleStateImageBehavior = false;
            this._viewFileDirectory.View = System.Windows.Forms.View.List;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "check.png");
            this.imageList1.Images.SetKeyName(1, "delete-16x16.png");
            this.imageList1.Images.SetKeyName(2, "resultset_next.png");
            this.imageList1.Images.SetKeyName(3, "document_out.png");
            this.imageList1.Images.SetKeyName(4, "first_aid (2).png");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._buttonBrowseFolders);
            this.panel1.Controls.Add(this._foldersLocationTextbox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(804, 66);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._close);
            this.panel2.Controls.Add(this._labelResult);
            this.panel2.Controls.Add(this._progressbar);
            this.panel2.Controls.Add(this._buttonProcess);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 469);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(804, 45);
            this.panel2.TabIndex = 6;
            // 
            // _labelResult
            // 
            this._labelResult.AutoSize = true;
            this._labelResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._labelResult.Location = new System.Drawing.Point(346, 15);
            this._labelResult.Name = "_labelResult";
            this._labelResult.Size = new System.Drawing.Size(0, 16);
            this._labelResult.TabIndex = 4;
            // 
            // _progressbar
            // 
            this._progressbar.Location = new System.Drawing.Point(16, 10);
            this._progressbar.Name = "_progressbar";
            this._progressbar.Size = new System.Drawing.Size(324, 23);
            this._progressbar.TabIndex = 3;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // _close
            // 
            this._close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._close.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._close.Image = global::SMLERPIC.Properties.Resources.error1;
            this._close.Location = new System.Drawing.Point(717, 10);
            this._close.Name = "_close";
            this._close.Size = new System.Drawing.Size(73, 26);
            this._close.TabIndex = 5;
            this._close.Text = "Close";
            this._close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._close.UseVisualStyleBackColor = true;
            this._close.Click += new System.EventHandler(this._close_Click);
            // 
            // _icImportPicture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._viewFileDirectory);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "_icImportPicture";
            this.Size = new System.Drawing.Size(804, 514);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _foldersLocationTextbox;
        private System.Windows.Forms.Button _buttonProcess;
        private System.Windows.Forms.Button _buttonBrowseFolders;
        private System.Windows.Forms.ListView _viewFileDirectory;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ProgressBar _progressbar;
        private System.Windows.Forms.Label _labelResult;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button _close;
    }
}
