namespace SMLTransportLabel
{
    partial class _transport_label
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
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._printButton = new MyLib.ToolStripMyButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._transport_screen_top1 = new SMLTransportLabel._transport_screen_top();
            this._detail_screen = new SMLTransportLabel._transport_screen_detail();
            this._myManageDetail = new MyLib._myManageData();
            this._myPanel = new MyLib._myPanel();
            this._myTab = new MyLib._myTabControl();
            this.tab_main = new MyLib._myTabPage();
            this.tab_detail = new MyLib._myTabPage();
            this._myToolBar.SuspendLayout();
            this._myPanel.SuspendLayout();
            this._myTab.SuspendLayout();
            this.tab_main.SuspendLayout();
            this.tab_detail.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myToolBar
            // 
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._printButton,
            this._closeButton});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(784, 25);
            this._myToolBar.TabIndex = 0;
            this._myToolBar.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLTransportLabel.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "บันทึก";
            this._saveButton.Size = new System.Drawing.Size(57, 22);
            this._saveButton.Text = "บันทึก";
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _printButton
            // 
            this._printButton.Image = global::SMLTransportLabel.Properties.Resources.printer;
            this._printButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._printButton.Name = "_printButton";
            this._printButton.Padding = new System.Windows.Forms.Padding(1);
            this._printButton.ResourceName = "พิมพ์";
            this._printButton.Size = new System.Drawing.Size(52, 22);
            this._printButton.Text = "พิมพ์";
            this._printButton.Click += new System.EventHandler(this._printButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLTransportLabel.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "ปิดจอ";
            this._closeButton.Size = new System.Drawing.Size(55, 22);
            this._closeButton.Text = "ปิดจอ";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _transport_screen_top1
            // 
            this._transport_screen_top1._isChange = false;
            //this._transport_screen_top1._mode = 0;
            this._transport_screen_top1.BackColor = System.Drawing.Color.Transparent;
            this._transport_screen_top1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._transport_screen_top1.Location = new System.Drawing.Point(3, 3);
            this._transport_screen_top1.Name = "_transport_screen_top1";
            this._transport_screen_top1.Size = new System.Drawing.Size(770, 562);
            this._transport_screen_top1.TabIndex = 1;
            // 
            // _detail_screen
            // 
            this._detail_screen._isChange = false;
            this._detail_screen.BackColor = System.Drawing.Color.Transparent;
            this._detail_screen.Dock = System.Windows.Forms.DockStyle.Fill;
            this._detail_screen.Location = new System.Drawing.Point(3, 3);
            this._detail_screen.Name = "_detail_screen";
            this._detail_screen.Size = new System.Drawing.Size(770, 537);
            this._detail_screen.TabIndex = 1;
            // 
            // _myManageData1
            // 
            this._myManageDetail._mainMenuCode = "";
            this._myManageDetail._mainMenuId = "";
            this._myManageDetail.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageDetail.Location = new System.Drawing.Point(0, 25);
            this._myManageDetail.Name = "_myManageData1";
            // 
            // _myManageDetail.Panel1
            // 
            this._myManageDetail._form1.BackColor = System.Drawing.Color.WhiteSmoke;
            // 
            // _myManageDetail.Panel2
            // 
            this._myManageDetail._form2.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageDetail._form2.Controls.Add(this._myPanel);
            this._myManageDetail._form2.Controls.Add(this._myToolBar);
            this._myManageDetail.Size = new System.Drawing.Size(869, 616);
            this._myManageDetail.TabIndex = 2;
            this._myManageDetail.TabStop = false;
            // 
            // _myPanel
            // 
            this._myPanel._switchTabAuto = false;
            this._myPanel.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel.Controls.Add(this._myTab);
            this._myPanel.CornerPicture = null;
            this._myPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel.Location = new System.Drawing.Point(0, 25);
            this._myPanel.Name = "_myPanel";
            this._myPanel.Size = new System.Drawing.Size(784, 595);
            this._myPanel.TabIndex = 0;
            // 
            // _myTab
            // 
            this._myTab.Controls.Add(this.tab_main);
            this._myTab.Controls.Add(this.tab_detail);
            this._myTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myTab.Font = new System.Drawing.Font("Tahoma", 9F);
            this._myTab.Location = new System.Drawing.Point(0, 0);
            this._myTab.Multiline = true;
            this._myTab.Name = "_myTab";
            this._myTab.SelectedIndex = 0;
            this._myTab.Size = new System.Drawing.Size(784, 595);
            this._myTab.TabIndex = 0;
            this._myTab.TableName = "ap_ar_transport_label";
            // 
            // tab_main
            // 
            this.tab_main.Controls.Add(this._transport_screen_top1);
            this.tab_main.Location = new System.Drawing.Point(4, 23);
            this.tab_main.Name = "tab_main";
            this.tab_main.Padding = new System.Windows.Forms.Padding(3);
            this.tab_main.Size = new System.Drawing.Size(776, 568);
            this.tab_main.TabIndex = 0;
            this.tab_main.Text = "1.tab_main";
            this.tab_main.UseVisualStyleBackColor = true;
            // 
            // tab_detail
            // 
            this.tab_detail.Controls.Add(this._detail_screen);
            this.tab_detail.Location = new System.Drawing.Point(4, 23);
            this.tab_detail.Name = "tab_detail";
            this.tab_detail.Padding = new System.Windows.Forms.Padding(3);
            this.tab_detail.Size = new System.Drawing.Size(776, 543);
            this.tab_detail.TabIndex = 0;
            this.tab_detail.Text = "2.tab_detail";
            this.tab_detail.UseVisualStyleBackColor = true;
            // 
            // _transport_label
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageDetail);
            this.Name = "_transport_label";
            this.Size = new System.Drawing.Size(869, 641);
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this._myPanel.ResumeLayout(false);
            this._myTab.ResumeLayout(false);
            this.tab_main.ResumeLayout(false);
            this.tab_detail.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib.ToolStripMyButton _saveButton;
        private MyLib.ToolStripMyButton _closeButton;
        private _transport_screen_top _transport_screen_top1;
        private _transport_screen_detail _detail_screen;
        private MyLib.ToolStripMyButton _printButton;
        private MyLib._myManageData _myManageDetail;
        private MyLib._myPanel _myPanel;
        private MyLib._myTabControl _myTab;
        private MyLib._myTabPage tab_main;
        private MyLib._myTabPage tab_detail;

    }
}
