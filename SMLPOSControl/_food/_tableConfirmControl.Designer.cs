namespace SMLPOSControl._food
{
    partial class _tableConfirmControl
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._orderListPanel = new System.Windows.Forms.Panel();
            this._itemListPanel = new System.Windows.Forms.Panel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this._saveButton = new MyLib.VistaButton();
            this._selectNoneButton = new MyLib.VistaButton();
            this._selectAllButton = new MyLib.VistaButton();
            this._viewAllButton = new MyLib.VistaButton();
            this._viewByDocButton = new MyLib.VistaButton();
            this._deviceLabel = new System.Windows.Forms.Label();
            this._tableLabel = new System.Windows.Forms.Label();
            this._saleLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._myShadowLabel1 = new MyLib._myShadowLabel(this.components);
            this._myGroupBox1 = new MyLib._myGroupBox();
            this._myGroupBox2 = new MyLib._myGroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this._myGroupBox3 = new MyLib._myGroupBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this._myGroupBox1.SuspendLayout();
            this._myGroupBox2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this._myGroupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 139);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._orderListPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._itemListPanel);
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel3);
            this.splitContainer1.Size = new System.Drawing.Size(1040, 543);
            this.splitContainer1.SplitterDistance = 466;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // _orderListPanel
            // 
            this._orderListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._orderListPanel.Location = new System.Drawing.Point(0, 0);
            this._orderListPanel.Name = "_orderListPanel";
            this._orderListPanel.Size = new System.Drawing.Size(466, 543);
            this._orderListPanel.TabIndex = 0;
            // 
            // _itemListPanel
            // 
            this._itemListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemListPanel.Location = new System.Drawing.Point(0, 32);
            this._itemListPanel.Name = "_itemListPanel";
            this._itemListPanel.Size = new System.Drawing.Size(569, 511);
            this._itemListPanel.TabIndex = 0;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.BackgroundImage = global::SMLPOSControl.Properties.Resources.bt03;
            this.flowLayoutPanel3.Controls.Add(this._saveButton);
            this.flowLayoutPanel3.Controls.Add(this._selectNoneButton);
            this.flowLayoutPanel3.Controls.Add(this._selectAllButton);
            this.flowLayoutPanel3.Controls.Add(this._viewAllButton);
            this.flowLayoutPanel3.Controls.Add(this._viewByDocButton);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(569, 32);
            this.flowLayoutPanel3.TabIndex = 0;
            // 
            // _saveButton
            // 
            this._saveButton._drawNewMethod = false;
            this._saveButton.BackColor = System.Drawing.Color.Transparent;
            this._saveButton.ButtonText = "บันทึก (F12)";
            this._saveButton.Location = new System.Drawing.Point(466, 3);
            this._saveButton.myImage = global::SMLPOSControl.Properties.Resources.disk_blue;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(100, 26);
            this._saveButton.TabIndex = 0;
            this._saveButton.Text = "vistaButton1";
            this._saveButton.UseVisualStyleBackColor = false;
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _selectNoneButton
            // 
            this._selectNoneButton._drawNewMethod = false;
            this._selectNoneButton.BackColor = System.Drawing.Color.Transparent;
            this._selectNoneButton.ButtonText = "ยกเลิกทั้งหมด";
            this._selectNoneButton.Location = new System.Drawing.Point(360, 3);
            this._selectNoneButton.myImage = global::SMLPOSControl.Properties.Resources.delete2;
            this._selectNoneButton.Name = "_selectNoneButton";
            this._selectNoneButton.Size = new System.Drawing.Size(100, 26);
            this._selectNoneButton.TabIndex = 3;
            this._selectNoneButton.Text = "vistaButton4";
            this._selectNoneButton.UseVisualStyleBackColor = false;
            this._selectNoneButton.Click += new System.EventHandler(this._selectNoneButton_Click);
            // 
            // _selectAllButton
            // 
            this._selectAllButton._drawNewMethod = false;
            this._selectAllButton.BackColor = System.Drawing.Color.Transparent;
            this._selectAllButton.ButtonText = "เลือกทั้งหมด";
            this._selectAllButton.Location = new System.Drawing.Point(254, 3);
            this._selectAllButton.myImage = global::SMLPOSControl.Properties.Resources.check2;
            this._selectAllButton.Name = "_selectAllButton";
            this._selectAllButton.Size = new System.Drawing.Size(100, 26);
            this._selectAllButton.TabIndex = 2;
            this._selectAllButton.Text = "vistaButton3";
            this._selectAllButton.UseVisualStyleBackColor = false;
            this._selectAllButton.Click += new System.EventHandler(this._selectAllButton_Click);
            // 
            // _viewAllButton
            // 
            this._viewAllButton._drawNewMethod = false;
            this._viewAllButton.BackColor = System.Drawing.Color.Transparent;
            this._viewAllButton.ButtonText = "แสดงทั้งหมด";
            this._viewAllButton.Location = new System.Drawing.Point(148, 3);
            this._viewAllButton.myImage = global::SMLPOSControl.Properties.Resources.bringtofront;
            this._viewAllButton.Name = "_viewAllButton";
            this._viewAllButton.Size = new System.Drawing.Size(100, 26);
            this._viewAllButton.TabIndex = 1;
            this._viewAllButton.Text = "vistaButton2";
            this._viewAllButton.UseVisualStyleBackColor = false;
            this._viewAllButton.Click += new System.EventHandler(this._viewAllButton_Click);
            // 
            // _viewByDocButton
            // 
            this._viewByDocButton._drawNewMethod = false;
            this._viewByDocButton.BackColor = System.Drawing.Color.Transparent;
            this._viewByDocButton.ButtonText = "แสดงตามเอกสาร";
            this._viewByDocButton.Location = new System.Drawing.Point(25, 3);
            this._viewByDocButton.myImage = global::SMLPOSControl.Properties.Resources.bringtofront;
            this._viewByDocButton.Name = "_viewByDocButton";
            this._viewByDocButton.Size = new System.Drawing.Size(117, 26);
            this._viewByDocButton.TabIndex = 4;
            this._viewByDocButton.Text = "vistaButton5";
            this._viewByDocButton.UseVisualStyleBackColor = false;
            this._viewByDocButton.Click += new System.EventHandler(this._viewByDocButton_Click);
            // 
            // _deviceLabel
            // 
            this._deviceLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._deviceLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._deviceLabel.Location = new System.Drawing.Point(3, 29);
            this._deviceLabel.Name = "_deviceLabel";
            this._deviceLabel.Size = new System.Drawing.Size(244, 54);
            this._deviceLabel.TabIndex = 5;
            this._deviceLabel.Text = "...";
            this._deviceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _tableLabel
            // 
            this._tableLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tableLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._tableLabel.Location = new System.Drawing.Point(3, 29);
            this._tableLabel.Name = "_tableLabel";
            this._tableLabel.Size = new System.Drawing.Size(244, 54);
            this._tableLabel.TabIndex = 3;
            this._tableLabel.Text = "...";
            this._tableLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _saleLabel
            // 
            this._saleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._saleLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._saleLabel.Location = new System.Drawing.Point(3, 29);
            this._saleLabel.Name = "_saleLabel";
            this._saleLabel.Size = new System.Drawing.Size(244, 54);
            this._saleLabel.TabIndex = 4;
            this._saleLabel.Text = "...";
            this._saleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this._myShadowLabel1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1040, 44);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // _myShadowLabel1
            // 
            this._myShadowLabel1.Angle = 0F;
            this._myShadowLabel1.AutoSize = true;
            this._myShadowLabel1.EndColor = System.Drawing.Color.Transparent;
            this._myShadowLabel1.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._myShadowLabel1.ForeColor = System.Drawing.Color.Maroon;
            this._myShadowLabel1.Location = new System.Drawing.Point(3, 0);
            this._myShadowLabel1.Name = "_myShadowLabel1";
            this._myShadowLabel1.ShadowColor = System.Drawing.Color.DarkGray;
            this._myShadowLabel1.Size = new System.Drawing.Size(273, 33);
            this._myShadowLabel1.StartColor = System.Drawing.Color.Transparent;
            this._myShadowLabel1.TabIndex = 6;
            this._myShadowLabel1.Text = "ยืนยันการเสิร์ฟอาหาร";
            this._myShadowLabel1.XOffset = 1F;
            this._myShadowLabel1.YOffset = 1F;
            // 
            // _myGroupBox1
            // 
            this._myGroupBox1.Controls.Add(this._tableLabel);
            this._myGroupBox1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myGroupBox1.Location = new System.Drawing.Point(259, 3);
            this._myGroupBox1.Name = "_myGroupBox1";
            this._myGroupBox1.ResourceName = "";
            this._myGroupBox1.Size = new System.Drawing.Size(250, 86);
            this._myGroupBox1.TabIndex = 6;
            this._myGroupBox1.TabStop = false;
            this._myGroupBox1.Text = "หมายเลขโต๊ะ";
            // 
            // _myGroupBox2
            // 
            this._myGroupBox2.Controls.Add(this._saleLabel);
            this._myGroupBox2.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myGroupBox2.Location = new System.Drawing.Point(515, 3);
            this._myGroupBox2.Name = "_myGroupBox2";
            this._myGroupBox2.ResourceName = "";
            this._myGroupBox2.Size = new System.Drawing.Size(250, 86);
            this._myGroupBox2.TabIndex = 7;
            this._myGroupBox2.TabStop = false;
            this._myGroupBox2.Text = "พนักงาน";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this._myGroupBox3);
            this.flowLayoutPanel2.Controls.Add(this._myGroupBox1);
            this.flowLayoutPanel2.Controls.Add(this._myGroupBox2);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 44);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(1040, 95);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // _myGroupBox3
            // 
            this._myGroupBox3.Controls.Add(this._deviceLabel);
            this._myGroupBox3.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myGroupBox3.Location = new System.Drawing.Point(3, 3);
            this._myGroupBox3.Name = "_myGroupBox3";
            this._myGroupBox3.ResourceName = "";
            this._myGroupBox3.Size = new System.Drawing.Size(250, 86);
            this._myGroupBox3.TabIndex = 8;
            this._myGroupBox3.TabStop = false;
            this._myGroupBox3.Text = "Device";
            // 
            // _tableConfirmControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "_tableConfirmControl";
            this.Size = new System.Drawing.Size(1040, 682);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this._myGroupBox1.ResumeLayout(false);
            this._myGroupBox2.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this._myGroupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label _deviceLabel;
        private System.Windows.Forms.Label _tableLabel;
        private System.Windows.Forms.Label _saleLabel;
        private System.Windows.Forms.Panel _itemListPanel;
        private System.Windows.Forms.Panel _orderListPanel;
        private MyLib._myGroupBox _myGroupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MyLib._myShadowLabel _myShadowLabel1;
        private MyLib._myGroupBox _myGroupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private MyLib.VistaButton _saveButton;
        private MyLib.VistaButton _viewAllButton;
        private MyLib.VistaButton _selectAllButton;
        private MyLib.VistaButton _selectNoneButton;
        private MyLib.VistaButton _viewByDocButton;
        private MyLib._myGroupBox _myGroupBox3;
    }
}
