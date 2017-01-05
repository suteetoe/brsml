namespace SMLERPGL._chart
{
    partial class _chatOfAccountFlow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_chatOfAccountFlow));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._chartOfAccountTreeView = new MyLib._myTreeViewDragDrop();
            this._toolBar = new System.Windows.Forms.ToolStrip();
            this._buttonClose = new MyLib.ToolStripMyButton();
            this._toolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "paste.png");
            // 
            // _chartOfAccountTreeView
            // 
            this._chartOfAccountTreeView.AllowDrop = true;
            this._chartOfAccountTreeView.BackColor = System.Drawing.Color.White;
            this._chartOfAccountTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._chartOfAccountTreeView.DragCursor = null;
            this._chartOfAccountTreeView.DragCursorType = MyLib.DragCursorType.DragIcon;
            this._chartOfAccountTreeView.DragImageIndex = 0;
            this._chartOfAccountTreeView.DragImageList = this.imageList1;
            this._chartOfAccountTreeView.DragMode = System.Windows.Forms.DragDropEffects.None;
            this._chartOfAccountTreeView.DragNodeFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._chartOfAccountTreeView.DragNodeOpacity = 0.8;
            this._chartOfAccountTreeView.DragOverNodeBackColor = System.Drawing.Color.Gainsboro;
            this._chartOfAccountTreeView.DragOverNodeForeColor = System.Drawing.Color.DimGray;
            this._chartOfAccountTreeView.ForeColor = System.Drawing.Color.Black;
            this._chartOfAccountTreeView.Location = new System.Drawing.Point(0, 25);
            this._chartOfAccountTreeView.Name = "_chartOfAccountTreeView";
            this._chartOfAccountTreeView.Size = new System.Drawing.Size(475, 418);
            this._chartOfAccountTreeView.TabIndex = 1;
            // 
            // _toolBar
            // 
            this._toolBar.BackgroundImage = global::SMLERPGL.Resource16x16.bt03;
            this._toolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonClose});
            this._toolBar.Location = new System.Drawing.Point(0, 0);
            this._toolBar.Name = "_toolBar";
            this._toolBar.Size = new System.Drawing.Size(475, 25);
            this._toolBar.TabIndex = 2;
            this._toolBar.Text = "toolStrip1";
            // 
            // _buttonClose
            // 
            this._buttonClose.Image = global::SMLERPGL.Resource16x16.error;
            this._buttonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Padding = new System.Windows.Forms.Padding(1);
            this._buttonClose.Size = new System.Drawing.Size(103, 22);
            this._buttonClose.Text = "ปิดหน้าจอ (Esc)";
            this._buttonClose.Click += new System.EventHandler(this._buttonClose_Click);
            // 
            // _chatOfAccountFlow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this._chartOfAccountTreeView);
            this.Controls.Add(this._toolBar);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_chatOfAccountFlow";
            this.Size = new System.Drawing.Size(475, 443);
            this._toolBar.ResumeLayout(false);
            this._toolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private MyLib._myTreeViewDragDrop _chartOfAccountTreeView;
        private System.Windows.Forms.ToolStrip _toolBar;
        private MyLib.ToolStripMyButton _buttonClose;
    }
}
