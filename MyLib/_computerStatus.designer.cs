namespace MyLib
{
    partial class _computerStatus
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_computerStatus));
            this._imageList = new System.Windows.Forms.ImageList(this.components);
            this._listView = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _imageList
            // 
            this._imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList.ImageStream")));
            this._imageList.TransparentColor = System.Drawing.Color.Transparent;
            this._imageList.Images.SetKeyName(0, "idisk.ico");
            this._imageList.Images.SetKeyName(1, "Removable HD.ico");
            // 
            // _listView
            // 
            this._listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._listView.LabelWrap = false;
            this._listView.LargeImageList = this._imageList;
            this._listView.Location = new System.Drawing.Point(5, 5);
            this._listView.Margin = new System.Windows.Forms.Padding(0);
            this._listView.Name = "_listView";
            this._listView.Size = new System.Drawing.Size(381, 256);
            this._listView.TabIndex = 0;
            this._listView.TileSize = new System.Drawing.Size(250, 34);
            this._listView.UseCompatibleStateImageBehavior = false;
            this._listView.View = System.Windows.Forms.View.Tile;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this._listView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(391, 266);
            this.panel1.TabIndex = 2;
            // 
            // _computerStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(391, 266);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_computerStatus";
            this.ShowInTaskbar = false;
            this.Text = "Computer Status";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList _imageList;
        private System.Windows.Forms.ListView _listView;
        private System.Windows.Forms.Panel panel1;

    }
}