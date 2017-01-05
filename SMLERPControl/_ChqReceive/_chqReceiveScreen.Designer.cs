namespace SMLERPControl
{
    partial class _chqReceiveScreen
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
            this._myScreen1 = new MyLib._myScreen();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._buttonSave = new MyLib._myButton();
            this._myPanel1 = new MyLib._myPanel();
            this._myFlowLayoutPanel1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myScreen1
            // 
            this._myScreen1.BackColor = System.Drawing.Color.Transparent;
            this._myScreen1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myScreen1.Location = new System.Drawing.Point(5, 5);
            this._myScreen1.Name = "_myScreen1";
            this._myScreen1.Size = new System.Drawing.Size(284, 129);
            this._myScreen1.TabIndex = 0;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.AutoSize = true;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._buttonSave);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(5, 133);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(284, 33);
            this._myFlowLayoutPanel1.TabIndex = 1;
            // 
            // _buttonSave
            // 
            this._buttonSave.AutoSize = true;
            this._buttonSave.myImage = global::SMLERPControl.Properties.Resources.disk_blue;
            this._buttonSave.Location = new System.Drawing.Point(219, 3);
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.Padding = new System.Windows.Forms.Padding(2);
            this._buttonSave.Size = new System.Drawing.Size(62, 27);
            this._buttonSave.TabIndex = 1;
            this._buttonSave.TabStop = false;
            this._buttonSave.Text = "Save";
            this._buttonSave.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonSave.Click += new System.EventHandler(this._buttonSave_Click);
            // 
            // _myPanel1
            // 
            this._myPanel1.AutoSize = true;
            this._myPanel1.Controls.Add(this._myScreen1);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel1);
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel1.Size = new System.Drawing.Size(294, 171);
            this._myPanel1.TabIndex = 2;
            // 
            // _chqReceiveScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(294, 171);
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_chqReceiveScreen";
            this.ShowInTaskbar = false;
            this.Text = "_chqReceiveScreen";
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myScreen _myScreen1;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib._myPanel _myPanel1;
        private MyLib._myButton _buttonSave;
    }
}