namespace SMLReport._report
{
    partial class _conditionScreen
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripMyButton1 = new MyLib.ToolStripMyButton();
            this._buttonPageSetup = new MyLib.ToolStripMyButton();
            this.toolStripMyButton3 = new MyLib.ToolStripMyButton();
            this.toolStripMyButton4 = new MyLib.ToolStripMyButton();
            this._buttonCondition = new MyLib.ToolStripMyButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonCondition,
            this.toolStripMyButton1,
            this._buttonPageSetup,
            this.toolStripMyButton3,
            this.toolStripMyButton4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(621, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripMyButton1
            // 
            this.toolStripMyButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMyButton1.Name = "toolStripMyButton1";
            this.toolStripMyButton1.ResourceName = "เริ่มพิมพ์ (F12)";
            this.toolStripMyButton1.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripMyButton1.Size = new System.Drawing.Size(95, 22);
            this.toolStripMyButton1.Text = "เริ่มพิมพ์ (F12)";
            // 
            // _buttonPageSetup
            // 
            this._buttonPageSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonPageSetup.Name = "_buttonPageSetup";
            this._buttonPageSetup.ResourceName = "กำหนดหน้ากระดาษ";
            this._buttonPageSetup.Padding = new System.Windows.Forms.Padding(1);
            this._buttonPageSetup.Size = new System.Drawing.Size(118, 22);
            this._buttonPageSetup.Text = "กำหนดหน้ากระดาษ";
            // 
            // toolStripMyButton3
            // 
            this.toolStripMyButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMyButton3.Name = "toolStripMyButton3";
            this.toolStripMyButton3.ResourceName = "ข้อเลือกพิเศษ";
            this.toolStripMyButton3.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripMyButton3.Size = new System.Drawing.Size(94, 22);
            this.toolStripMyButton3.Text = "ข้อเลือกพิเศษ";
            // 
            // toolStripMyButton4
            // 
            this.toolStripMyButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMyButton4.Name = "toolStripMyButton4";
            this.toolStripMyButton4.ResourceName = "ตัวอย่างรายงาน";
            this.toolStripMyButton4.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripMyButton4.Size = new System.Drawing.Size(97, 22);
            this.toolStripMyButton4.Text = "ตัวอย่างรายงาน";
            // 
            // _buttonCondition
            // 
            this._buttonCondition.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonCondition.Name = "_buttonCondition";
            this._buttonCondition.ResourceName = "เงื่อนไขการพิมพ์";
            this._buttonCondition.Padding = new System.Windows.Forms.Padding(1);
            this._buttonCondition.Size = new System.Drawing.Size(103, 22);
            this._buttonCondition.Text = "เงื่อนไขการพิมพ์";
            // 
            // _reportSelectScreenFrameWork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_reportSelectScreenFrameWork";
            this.Size = new System.Drawing.Size(621, 165);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private MyLib.ToolStripMyButton toolStripMyButton1;
        private MyLib.ToolStripMyButton _buttonPageSetup;
        private MyLib.ToolStripMyButton toolStripMyButton3;
        private MyLib.ToolStripMyButton toolStripMyButton4;
        private MyLib.ToolStripMyButton _buttonCondition;
    }
}
