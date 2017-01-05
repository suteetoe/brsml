namespace SMLReport._formReport
{
    partial class _pageSort
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_pageSort));
            this._listBoxPage = new System.Windows.Forms.ListBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this._buttonUp = new System.Windows.Forms.Button();
            this._buttonDown = new System.Windows.Forms.Button();
            this._buttonCalcel = new System.Windows.Forms.Button();
            this._buttonOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _listBoxPage
            // 
            this._listBoxPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listBoxPage.FormattingEnabled = true;
            this._listBoxPage.Location = new System.Drawing.Point(2, 2);
            this._listBoxPage.Name = "_listBoxPage";
            this._listBoxPage.Size = new System.Drawing.Size(174, 212);
            this._listBoxPage.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this._buttonUp);
            this.flowLayoutPanel2.Controls.Add(this._buttonDown);
            this.flowLayoutPanel2.Controls.Add(this._buttonCalcel);
            this.flowLayoutPanel2.Controls.Add(this._buttonOk);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(178, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(72, 223);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // _buttonUp
            // 
            this._buttonUp.AutoSize = true;
            this._buttonUp.Image = global::SMLReport.Properties.Resources.nav_up_blue;
            this._buttonUp.Location = new System.Drawing.Point(3, 3);
            this._buttonUp.Name = "_buttonUp";
            this._buttonUp.Size = new System.Drawing.Size(29, 22);
            this._buttonUp.TabIndex = 0;
            this._buttonUp.UseVisualStyleBackColor = true;
            this._buttonUp.Click += new System.EventHandler(this._buttonUp_Click);
            // 
            // _buttonDown
            // 
            this._buttonDown.AutoSize = true;
            this._buttonDown.Image = global::SMLReport.Properties.Resources.nav_down_blue;
            this._buttonDown.Location = new System.Drawing.Point(38, 3);
            this._buttonDown.Name = "_buttonDown";
            this._buttonDown.Size = new System.Drawing.Size(29, 22);
            this._buttonDown.TabIndex = 1;
            this._buttonDown.UseVisualStyleBackColor = true;
            this._buttonDown.Click += new System.EventHandler(this._buttonDown_Click);
            // 
            // _buttonCalcel
            // 
            this._buttonCalcel.AutoSize = true;
            this._buttonCalcel.Image = global::SMLReport.Properties.Resources.error;
            this._buttonCalcel.Location = new System.Drawing.Point(3, 31);
            this._buttonCalcel.Name = "_buttonCalcel";
            this._buttonCalcel.Size = new System.Drawing.Size(66, 23);
            this._buttonCalcel.TabIndex = 0;
            this._buttonCalcel.Text = "Cancel";
            this._buttonCalcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonCalcel.UseVisualStyleBackColor = true;
            this._buttonCalcel.Click += new System.EventHandler(this._buttonCalcel_Click);
            // 
            // _buttonOk
            // 
            this._buttonOk.AutoSize = true;
            this._buttonOk.Image = global::SMLReport.Properties.Resources.check2;
            this._buttonOk.Location = new System.Drawing.Point(3, 60);
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Size = new System.Drawing.Size(64, 23);
            this._buttonOk.TabIndex = 1;
            this._buttonOk.Text = "OK";
            this._buttonOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonOk.UseVisualStyleBackColor = true;
            this._buttonOk.Click += new System.EventHandler(this._buttonOk_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._listBoxPage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(2);
            this.panel1.Size = new System.Drawing.Size(178, 223);
            this.panel1.TabIndex = 3;
            // 
            // _pageSort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MintCream;
            this.ClientSize = new System.Drawing.Size(250, 223);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "_pageSort";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Page";
            this.Load += new System.EventHandler(this._pageSort_Load);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _buttonOk;
        private System.Windows.Forms.Button _buttonCalcel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button _buttonUp;
        private System.Windows.Forms.Button _buttonDown;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ListBox _listBoxPage;
    }
}