namespace SMLInventoryControl
{
    partial class _priceInfoForm
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
            this._splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._priceByCust = new SMLInventoryControl._icPrice._icPriceDetail();
            this._myLabel1 = new MyLib._myLabel();
            this._splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._priceByCustGroup = new SMLInventoryControl._icPrice._icPriceDetail();
            this._myLabel2 = new MyLib._myLabel();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this._priceNormal = new SMLInventoryControl._icPrice._icPriceDetail();
            this._myLabel3 = new MyLib._myLabel();
            this._priceStandard = new SMLInventoryControl._icPrice._icPriceDetail();
            this._myLabel4 = new MyLib._myLabel();
            this._splitContainer1.Panel1.SuspendLayout();
            this._splitContainer1.Panel2.SuspendLayout();
            this._splitContainer1.SuspendLayout();
            this._splitContainer2.Panel1.SuspendLayout();
            this._splitContainer2.Panel2.SuspendLayout();
            this._splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // _splitContainer1
            // 
            this._splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer1.Location = new System.Drawing.Point(0, 0);
            this._splitContainer1.Name = "_splitContainer1";
            this._splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _splitContainer1.Panel1
            // 
            this._splitContainer1.Panel1.BackColor = System.Drawing.Color.LightCyan;
            this._splitContainer1.Panel1.Controls.Add(this._priceByCust);
            this._splitContainer1.Panel1.Controls.Add(this._myLabel1);
            this._splitContainer1.Panel1MinSize = 0;
            // 
            // _splitContainer1.Panel2
            // 
            this._splitContainer1.Panel2.Controls.Add(this._splitContainer2);
            this._splitContainer1.Panel2MinSize = 0;
            this._splitContainer1.Size = new System.Drawing.Size(762, 598);
            this._splitContainer1.SplitterDistance = 155;
            this._splitContainer1.TabIndex = 2;
            // 
            // _priceByCust
            // 
            this._priceByCust._priceListType = _g.g._priceListType.ขาย_ราคาขายตามลูกค้า;
            this._priceByCust._priceType = _g.g._priceGridType.ราคาตามลูกค้า;
            this._priceByCust.Dock = System.Windows.Forms.DockStyle.Fill;
            this._priceByCust.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._priceByCust.Location = new System.Drawing.Point(0, 19);
            this._priceByCust.Name = "_priceByCust";
            this._priceByCust.Size = new System.Drawing.Size(762, 136);
            this._priceByCust.TabIndex = 1;
            // 
            // _myLabel1
            // 
            this._myLabel1.AutoSize = true;
            this._myLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myLabel1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myLabel1.Location = new System.Drawing.Point(0, 0);
            this._myLabel1.Name = "_myLabel1";
            this._myLabel1.ResourceName = "ราคาขาย (ตามลูกค้า)";
            this._myLabel1.Size = new System.Drawing.Size(168, 19);
            this._myLabel1.TabIndex = 2;
            this._myLabel1.Text = "ราคาขาย (ตามลูกค้า)";
            // 
            // _splitContainer2
            // 
            this._splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer2.Location = new System.Drawing.Point(0, 0);
            this._splitContainer2.Name = "_splitContainer2";
            this._splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _splitContainer2.Panel1
            // 
            this._splitContainer2.Panel1.BackColor = System.Drawing.Color.LightCyan;
            this._splitContainer2.Panel1.Controls.Add(this._priceByCustGroup);
            this._splitContainer2.Panel1.Controls.Add(this._myLabel2);
            this._splitContainer2.Panel1MinSize = 0;
            // 
            // _splitContainer2.Panel2
            // 
            this._splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this._splitContainer2.Panel2MinSize = 0;
            this._splitContainer2.Size = new System.Drawing.Size(762, 439);
            this._splitContainer2.SplitterDistance = 138;
            this._splitContainer2.TabIndex = 0;
            // 
            // _priceByCustGroup
            // 
            this._priceByCustGroup._priceListType = _g.g._priceListType.ขาย_ราคาขายตามกลุ่มลูกค้า;
            this._priceByCustGroup._priceType = _g.g._priceGridType.ราคาตามกลุ่มลูกค้า;
            this._priceByCustGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this._priceByCustGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._priceByCustGroup.Location = new System.Drawing.Point(0, 19);
            this._priceByCustGroup.Name = "_priceByCustGroup";
            this._priceByCustGroup.Size = new System.Drawing.Size(762, 119);
            this._priceByCustGroup.TabIndex = 3;
            // 
            // _myLabel2
            // 
            this._myLabel2.AutoSize = true;
            this._myLabel2.Dock = System.Windows.Forms.DockStyle.Top;
            this._myLabel2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myLabel2.Location = new System.Drawing.Point(0, 0);
            this._myLabel2.Name = "_myLabel2";
            this._myLabel2.ResourceName = "ราคาขาย (ตามกลุ่มลูกค้า)";
            this._myLabel2.Size = new System.Drawing.Size(200, 19);
            this._myLabel2.TabIndex = 4;
            this._myLabel2.Text = "ราคาขาย (ตามกลุ่มลูกค้า)";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.BackColor = System.Drawing.Color.LightCyan;
            this.splitContainer3.Panel1.Controls.Add(this._priceNormal);
            this.splitContainer3.Panel1.Controls.Add(this._myLabel3);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.BackColor = System.Drawing.Color.LightCyan;
            this.splitContainer3.Panel2.Controls.Add(this._priceStandard);
            this.splitContainer3.Panel2.Controls.Add(this._myLabel4);
            this.splitContainer3.Size = new System.Drawing.Size(762, 297);
            this.splitContainer3.SplitterDistance = 151;
            this.splitContainer3.TabIndex = 0;
            // 
            // _priceNormal
            // 
            this._priceNormal._priceListType = _g.g._priceListType.ขาย_ราคาขายทั่วไป;
            this._priceNormal._priceType = _g.g._priceGridType.ราคาปรกติ;
            this._priceNormal.Dock = System.Windows.Forms.DockStyle.Fill;
            this._priceNormal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._priceNormal.Location = new System.Drawing.Point(0, 19);
            this._priceNormal.Name = "_priceNormal";
            this._priceNormal.Size = new System.Drawing.Size(762, 132);
            this._priceNormal.TabIndex = 3;
            // 
            // _myLabel3
            // 
            this._myLabel3.AutoSize = true;
            this._myLabel3.Dock = System.Windows.Forms.DockStyle.Top;
            this._myLabel3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myLabel3.Location = new System.Drawing.Point(0, 0);
            this._myLabel3.Name = "_myLabel3";
            this._myLabel3.ResourceName = "ราคาขายทั่วไป";
            this._myLabel3.Size = new System.Drawing.Size(117, 19);
            this._myLabel3.TabIndex = 4;
            this._myLabel3.Text = "ราคาขายทั่วไป";
            // 
            // _priceStandard
            // 
            this._priceStandard._priceListType = _g.g._priceListType.ขาย_ราคาขายมาตรฐาน;
            this._priceStandard._priceType = _g.g._priceGridType.ราคาปรกติ;
            this._priceStandard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._priceStandard.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._priceStandard.Location = new System.Drawing.Point(0, 19);
            this._priceStandard.Name = "_priceStandard";
            this._priceStandard.Size = new System.Drawing.Size(762, 123);
            this._priceStandard.TabIndex = 3;
            // 
            // _myLabel4
            // 
            this._myLabel4.AutoSize = true;
            this._myLabel4.Dock = System.Windows.Forms.DockStyle.Top;
            this._myLabel4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myLabel4.Location = new System.Drawing.Point(0, 0);
            this._myLabel4.Name = "_myLabel4";
            this._myLabel4.ResourceName = "ราคาขาย (มาตรฐาน)";
            this._myLabel4.Size = new System.Drawing.Size(165, 19);
            this._myLabel4.TabIndex = 4;
            this._myLabel4.Text = "ราคาขาย (มาตรฐาน)";
            // 
            // _priceInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 598);
            this.Controls.Add(this._splitContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "_priceInfoForm";
            this.Text = "_priceInfoForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this._splitContainer1.Panel1.ResumeLayout(false);
            this._splitContainer1.Panel1.PerformLayout();
            this._splitContainer1.Panel2.ResumeLayout(false);
            this._splitContainer1.ResumeLayout(false);
            this._splitContainer2.Panel1.ResumeLayout(false);
            this._splitContainer2.Panel1.PerformLayout();
            this._splitContainer2.Panel2.ResumeLayout(false);
            this._splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private _icPrice._icPriceDetail _priceByCust;
        private System.Windows.Forms.SplitContainer _splitContainer1;
        private System.Windows.Forms.SplitContainer _splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private MyLib._myLabel _myLabel1;
        private _icPrice._icPriceDetail _priceByCustGroup;
        private MyLib._myLabel _myLabel2;
        private _icPrice._icPriceDetail _priceNormal;
        private MyLib._myLabel _myLabel3;
        private _icPrice._icPriceDetail _priceStandard;
        private MyLib._myLabel _myLabel4;
    }
}