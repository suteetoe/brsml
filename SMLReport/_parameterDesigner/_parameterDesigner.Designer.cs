namespace SMLReport._design
{
    partial class _designCondition
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("TextBox", "textbox.png");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("DateBox", "inputdate.png");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("NumberBox", "numberbox.png");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Label", "label.png");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("LinkLabel", "linklabel.png");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("ShadowLabel", "shadowlabel.png");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("RadioButton", "radiobutton.png");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("CheckBox", "checkbox.png");
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("Button", "button.png");
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("GroupBox", "groupbox.png");
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("Panel", "panel.png");
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("PictureBox", "picturebox.png");
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem("Grouper", "grouper.png");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_designCondition));
            this._controlListView = new System.Windows.Forms.ListView();
            this._imageList = new System.Windows.Forms.ImageList(this.components);
            this._myPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._screenPreview = new MyLib._myPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _controlListView
            // 
            this._controlListView.AllowDrop = true;
            this._controlListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._controlListView.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.StateImageIndex = 0;
            listViewItem4.StateImageIndex = 0;
            listViewItem5.StateImageIndex = 0;
            listViewItem7.StateImageIndex = 0;
            listViewItem8.StateImageIndex = 0;
            listViewItem9.StateImageIndex = 0;
            listViewItem10.StateImageIndex = 0;
            this._controlListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12,
            listViewItem13});
            this._controlListView.Location = new System.Drawing.Point(5, 19);
            this._controlListView.MultiSelect = false;
            this._controlListView.Name = "_controlListView";
            this._controlListView.ShowGroups = false;
            this._controlListView.Size = new System.Drawing.Size(205, 128);
            this._controlListView.SmallImageList = this._imageList;
            this._controlListView.TabIndex = 1;
            this._controlListView.UseCompatibleStateImageBehavior = false;
            this._controlListView.View = System.Windows.Forms.View.SmallIcon;
            // 
            // _imageList
            // 
            this._imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList.ImageStream")));
            this._imageList.TransparentColor = System.Drawing.Color.Transparent;
            this._imageList.Images.SetKeyName(0, "textbox.png");
            this._imageList.Images.SetKeyName(1, "numberbox.png");
            this._imageList.Images.SetKeyName(2, "inputdate.png");
            this._imageList.Images.SetKeyName(3, "label.png");
            this._imageList.Images.SetKeyName(4, "linklabel.png");
            this._imageList.Images.SetKeyName(5, "shadowlabel.png");
            this._imageList.Images.SetKeyName(6, "radiobutton.png");
            this._imageList.Images.SetKeyName(7, "checkbox.png");
            this._imageList.Images.SetKeyName(8, "button.png");
            this._imageList.Images.SetKeyName(9, "groupbox.png");
            this._imageList.Images.SetKeyName(10, "grouper.png");
            this._imageList.Images.SetKeyName(11, "panel.png");
            this._imageList.Images.SetKeyName(12, "picturebox.png");
            // 
            // _myPropertyGrid
            // 
            this._myPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPropertyGrid.HelpBackColor = System.Drawing.Color.AliceBlue;
            this._myPropertyGrid.Location = new System.Drawing.Point(3, 3);
            this._myPropertyGrid.Name = "_myPropertyGrid";
            this._myPropertyGrid.Size = new System.Drawing.Size(215, 431);
            this._myPropertyGrid.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._controlListView);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(215, 152);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Toolbox";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.splitContainer1.Panel1.Controls.Add(this._screenPreview);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(849, 463);
            this.splitContainer1.SplitterDistance = 616;
            this.splitContainer1.TabIndex = 6;
            // 
            // _screenPreview
            // 
            this._screenPreview.AllowDrop = true;
            this._screenPreview.BackColor = System.Drawing.Color.White;
            this._screenPreview.BeginColor = System.Drawing.Color.White;
            this._screenPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this._screenPreview.EndColor = System.Drawing.Color.LightCyan;
            this._screenPreview.Location = new System.Drawing.Point(0, 0);
            this._screenPreview.Name = "_screenPreview";
            this._screenPreview.ShowLineBackground = false;
            this._screenPreview.Size = new System.Drawing.Size(616, 463);
            this._screenPreview.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(229, 463);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(221, 437);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Fields Explorer";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this._myPropertyGrid);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(221, 437);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Properties";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // _designCondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_designCondition";
            this.Size = new System.Drawing.Size(849, 463);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView _controlListView;
        private System.Windows.Forms.ImageList _imageList;
        private System.Windows.Forms.PropertyGrid _myPropertyGrid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MyLib._myPanel _screenPreview;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;

    }
}
