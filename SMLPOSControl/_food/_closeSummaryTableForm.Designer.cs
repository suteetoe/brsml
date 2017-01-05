namespace SMLPOSControl._food
{
    partial class _closeSummaryTableForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this._posPanel = new System.Windows.Forms.Panel();
            this.vistaButton1 = new MyLib.VistaButton();
            this.vistaButton2 = new MyLib.VistaButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.vistaButton2);
            this.panel1.Controls.Add(this.vistaButton1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(774, 54);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "สมาชิก";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textBox1.Location = new System.Drawing.Point(74, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(408, 27);
            this.textBox1.TabIndex = 1;
            // 
            // _posPanel
            // 
            this._posPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._posPanel.Location = new System.Drawing.Point(0, 54);
            this._posPanel.Name = "_posPanel";
            this._posPanel.Size = new System.Drawing.Size(774, 402);
            this._posPanel.TabIndex = 1;
            // 
            // vistaButton1
            // 
            this.vistaButton1.BackColor = System.Drawing.Color.Transparent;
            this.vistaButton1.ButtonText = "คำณวนใหม่";
            this.vistaButton1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.vistaButton1.Location = new System.Drawing.Point(620, 13);
            this.vistaButton1.Name = "vistaButton1";
            this.vistaButton1.Size = new System.Drawing.Size(138, 30);
            this.vistaButton1.TabIndex = 2;
            this.vistaButton1.Text = "vistaButton1";
            this.vistaButton1.UseVisualStyleBackColor = false;
            // 
            // vistaButton2
            // 
            this.vistaButton2.BackColor = System.Drawing.Color.Transparent;
            this.vistaButton2.ButtonText = "ค้นหาลูกค้า";
            this.vistaButton2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.vistaButton2.Location = new System.Drawing.Point(488, 13);
            this.vistaButton2.Name = "vistaButton2";
            this.vistaButton2.Size = new System.Drawing.Size(126, 30);
            this.vistaButton2.TabIndex = 3;
            this.vistaButton2.Text = "vistaButton2";
            this.vistaButton2.UseVisualStyleBackColor = false;
            // 
            // _closeSummaryTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 456);
            this.Controls.Add(this._posPanel);
            this.Controls.Add(this.panel1);
            this.Name = "_closeSummaryTableForm";
            this.Text = "ยอดเงิน";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel _posPanel;
        private MyLib.VistaButton vistaButton1;
        private MyLib.VistaButton vistaButton2;
    }
}