namespace SMLERPGL._reportDesign
{
    partial class _properties
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
            this._screen = new SMLERPGL._reportDesign._propertiesScreen();
            this.panel1 = new System.Windows.Forms.Panel();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._myButton1 = new MyLib._myButton();
            this.panel1.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _screen
            // 
            this._screen.AutoSize = true;
            this._screen.BackColor = System.Drawing.Color.Transparent;
            this._screen.Dock = System.Windows.Forms.DockStyle.Top;
            this._screen.Location = new System.Drawing.Point(4, 4);
            this._screen.Name = "_screen";
            this._screen.Size = new System.Drawing.Size(318, 68);
            this._screen.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._myFlowLayoutPanel1);
            this.panel1.Controls.Add(this._screen);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(4);
            this.panel1.Size = new System.Drawing.Size(326, 101);
            this.panel1.TabIndex = 2;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.AutoSize = true;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._myButton1);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(4, 74);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(318, 23);
            this._myFlowLayoutPanel1.TabIndex = 2;
            // 
            // _myButton1
            // 
            this._myButton1.AutoSize = true;
            this._myButton1.myImage = global::SMLERPGL.Resource16x16.disk_blue;
            this._myButton1.Location = new System.Drawing.Point(242, 0);
            this._myButton1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._myButton1.Name = "_myButton1";
            this._myButton1.Size = new System.Drawing.Size(75, 23);
            this._myButton1.TabIndex = 0;
            this._myButton1.Text = "บันทึก";
            this._myButton1.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._myButton1.myUseVisualStyleBackColor = true;
            // 
            // _properties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(326, 101);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "_properties";
            this.Text = "Properties";
            this.Load += new System.EventHandler(this._properties_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib._myButton _myButton1;
        public _propertiesScreen _screen;
    }
}