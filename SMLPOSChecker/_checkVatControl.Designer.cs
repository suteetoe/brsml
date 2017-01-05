namespace SMLPOSChecker
{
    partial class _checkVatControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_checkVatControl));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._checkButton = new MyLib.ToolStripMyButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this._updateVatButton = new System.Windows.Forms.Button();
            this._gridVatProblem = new MyLib._myGrid();
            this._screen_check_vat1 = new SMLPOSChecker._screen_check_vat();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._checkButton,
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 42);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1023, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _checkButton
            // 
            this._checkButton.Image = ((System.Drawing.Image)(resources.GetObject("_checkButton.Image")));
            this._checkButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._checkButton.Name = "_checkButton";
            this._checkButton.Padding = new System.Windows.Forms.Padding(1);
            this._checkButton.ResourceName = "";
            this._checkButton.Size = new System.Drawing.Size(73, 22);
            this._checkButton.Text = "ตรวจสอบ";
            this._checkButton.Click += new System.EventHandler(this._checkButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Image = ((System.Drawing.Image)(resources.GetObject("_closeButton.Image")));
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "";
            this._closeButton.Size = new System.Drawing.Size(55, 22);
            this._closeButton.Text = "ปิดจอ";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._updateVatButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 742);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1023, 40);
            this.panel1.TabIndex = 2;
            // 
            // _updateVatButton
            // 
            this._updateVatButton.Location = new System.Drawing.Point(3, 7);
            this._updateVatButton.Name = "_updateVatButton";
            this._updateVatButton.Size = new System.Drawing.Size(135, 23);
            this._updateVatButton.TabIndex = 0;
            this._updateVatButton.Text = "Update New Vat";
            this._updateVatButton.UseVisualStyleBackColor = true;
            this._updateVatButton.Click += new System.EventHandler(this._updateVatButton_Click);
            // 
            // _gridVatProblem
            // 
            this._gridVatProblem._extraWordShow = true;
            this._gridVatProblem._selectRow = -1;
            this._gridVatProblem.BackColor = System.Drawing.SystemColors.Window;
            this._gridVatProblem.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridVatProblem.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridVatProblem.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridVatProblem.Font = new System.Drawing.Font("Tahoma", 9F);
            this._gridVatProblem.Location = new System.Drawing.Point(0, 67);
            this._gridVatProblem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridVatProblem.Name = "_gridVatProblem";
            this._gridVatProblem.Size = new System.Drawing.Size(1023, 675);
            this._gridVatProblem.TabIndex = 3;
            this._gridVatProblem.TabStop = false;
            // 
            // _screen_check_vat1
            // 
            this._screen_check_vat1._isChange = false;
            this._screen_check_vat1.BackColor = System.Drawing.Color.Transparent;
            this._screen_check_vat1.Dock = System.Windows.Forms.DockStyle.Top;
            this._screen_check_vat1.Font = new System.Drawing.Font("Tahoma", 9F);
            this._screen_check_vat1.Location = new System.Drawing.Point(0, 0);
            this._screen_check_vat1.Name = "_screen_check_vat1";
            this._screen_check_vat1.Size = new System.Drawing.Size(1023, 42);
            this._screen_check_vat1.TabIndex = 0;
            // 
            // _checkVatControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._gridVatProblem);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this._screen_check_vat1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_checkVatControl";
            this.Size = new System.Drawing.Size(1023, 782);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private _screen_check_vat _screen_check_vat1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private MyLib.ToolStripMyButton _checkButton;
        private System.Windows.Forms.Panel panel1;
        private MyLib._myGrid _gridVatProblem;
        private System.Windows.Forms.Button _updateVatButton;
        private MyLib.ToolStripMyButton _closeButton;
    }
}
