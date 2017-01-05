namespace MyLib
{
    partial class _filterForm
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._closeButton = new MyLib.VistaButton();
            this._filterButton = new MyLib.VistaButton();
            this._screen = new MyLib._myScreen();
            this._clearButton = new MyLib.VistaButton();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this._closeButton);
            this.flowLayoutPanel1.Controls.Add(this._filterButton);
            this.flowLayoutPanel1.Controls.Add(this._clearButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(6, 340);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(445, 30);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // _closeButton
            // 
            this._closeButton._drawNewMethod = false;
            this._closeButton.AutoSize = true;
            this._closeButton.BackColor = System.Drawing.Color.Transparent;
            this._closeButton.ButtonText = "Close";
            this._closeButton.Location = new System.Drawing.Point(378, 3);
            this._closeButton.myImage = global::MyLib.Properties.Resources.error1;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(64, 24);
            this._closeButton.TabIndex = 0;
            this._closeButton.Text = "Close";
            this._closeButton.UseVisualStyleBackColor = false;
            // 
            // _filterButton
            // 
            this._filterButton._drawNewMethod = false;
            this._filterButton.AutoSize = true;
            this._filterButton.BackColor = System.Drawing.Color.Transparent;
            this._filterButton.ButtonText = "Filter";
            this._filterButton.Location = new System.Drawing.Point(309, 3);
            this._filterButton.myImage = global::MyLib.Properties.Resources.flash;
            this._filterButton.Name = "_filterButton";
            this._filterButton.Size = new System.Drawing.Size(63, 24);
            this._filterButton.TabIndex = 1;
            this._filterButton.Text = "Filter";
            this._filterButton.UseVisualStyleBackColor = false;
            // 
            // _screen
            // 
            this._screen._isChange = false;
            this._screen.BackColor = System.Drawing.Color.Transparent;
            this._screen.Dock = System.Windows.Forms.DockStyle.Fill;
            this._screen.Location = new System.Drawing.Point(6, 5);
            this._screen.Name = "_screen";
            this._screen.Size = new System.Drawing.Size(445, 335);
            this._screen.TabIndex = 0;
            // 
            // _clearButton
            // 
            this._clearButton._drawNewMethod = false;
            this._clearButton.AutoSize = true;
            this._clearButton.BackColor = System.Drawing.Color.Transparent;
            this._clearButton.ButtonText = "Clear";
            this._clearButton.Location = new System.Drawing.Point(240, 3);
            this._clearButton.myImage = global::MyLib.Properties.Resources.refresh;
            this._clearButton.Name = "_clearButton";
            this._clearButton.Size = new System.Drawing.Size(63, 24);
            this._clearButton.TabIndex = 2;
            this._clearButton.Text = "Clear";
            this._clearButton.UseVisualStyleBackColor = false;
            // 
            // _filterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 375);
            this.ControlBox = false;
            this.Controls.Add(this._screen);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "_filterForm";
            this.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Text = "Filter";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public _myScreen _screen;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public VistaButton _closeButton;
        public VistaButton _filterButton;
        public VistaButton _clearButton;

    }
}