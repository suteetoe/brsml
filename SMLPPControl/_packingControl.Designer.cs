namespace SMLPPControl
{
    partial class _packingControl
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
            this._myTabControl1 = new MyLib._myTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this._myTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myTabControl1
            // 
            this._myTabControl1.Controls.Add(this.tabPage1);
            this._myTabControl1.Controls.Add(this.tabPage2);
            this._myTabControl1.Font = new System.Drawing.Font("Tahoma", 9F);
            this._myTabControl1.Location = new System.Drawing.Point(57, 107);
            this._myTabControl1.Multiline = true;
            this._myTabControl1.Name = "_myTabControl1";
            this._myTabControl1.SelectedIndex = 0;
            this._myTabControl1.Size = new System.Drawing.Size(200, 100);
            this._myTabControl1.TabIndex = 0;
            this._myTabControl1.TableName = "";
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(192, 73);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 73);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // _packingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myTabControl1);
            this.Name = "_packingControl";
            this.Size = new System.Drawing.Size(959, 795);
            this._myTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myTabControl _myTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}
