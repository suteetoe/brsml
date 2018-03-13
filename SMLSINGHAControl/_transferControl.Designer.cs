namespace SMLSINGHAControl
{
    partial class _transferControl
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
            this.button_prepare = new System.Windows.Forms.Button();
            this.button_process = new System.Windows.Forms.Button();
            this._myPanel1 = new MyLib._myPanel();
            this._myPanel2 = new MyLib._myPanel();
            this._singhaGridGetdata1 = new SMLSINGHAControl._singhaGridGetdata();
            this._myPanel3 = new MyLib._myPanel();
            this._myPanel1.SuspendLayout();
            this._myPanel2.SuspendLayout();
            this._myPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_prepare
            // 
            this.button_prepare.BackColor = System.Drawing.SystemColors.Info;
            this.button_prepare.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_prepare.Location = new System.Drawing.Point(3, 7);
            this.button_prepare.Name = "button_prepare";
            this.button_prepare.Size = new System.Drawing.Size(110, 30);
            this.button_prepare.TabIndex = 0;
            this.button_prepare.Text = "PrePare Data";
            this.button_prepare.UseVisualStyleBackColor = false;
            this.button_prepare.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_process
            // 
            this.button_process.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_process.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button_process.Location = new System.Drawing.Point(1046, 15);
            this.button_process.Name = "button_process";
            this.button_process.Size = new System.Drawing.Size(75, 36);
            this.button_process.TabIndex = 7;
            this.button_process.Text = "Process";
            this.button_process.UseVisualStyleBackColor = false;
            this.button_process.Click += new System.EventHandler(this.button_process_Click);
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this.button_prepare);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(1124, 44);
            this._myPanel1.TabIndex = 8;
            // 
            // _myPanel2
            // 
            this._myPanel2._switchTabAuto = false;
            this._myPanel2.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Controls.Add(this._singhaGridGetdata1);
            this._myPanel2.Controls.Add(this._myPanel3);
            this._myPanel2.CornerPicture = null;
            this._myPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel2.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Location = new System.Drawing.Point(0, 44);
            this._myPanel2.Name = "_myPanel2";
            this._myPanel2.Size = new System.Drawing.Size(1124, 736);
            this._myPanel2.TabIndex = 9;
            // 
            // _singhaGridGetdata1
            // 
            this._singhaGridGetdata1._extraWordShow = true;
            this._singhaGridGetdata1._selectRow = -1;
            this._singhaGridGetdata1.BackColor = System.Drawing.SystemColors.Window;
            this._singhaGridGetdata1.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._singhaGridGetdata1.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._singhaGridGetdata1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._singhaGridGetdata1.Font = new System.Drawing.Font("Tahoma", 9F);
            this._singhaGridGetdata1.Location = new System.Drawing.Point(0, 0);
            this._singhaGridGetdata1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._singhaGridGetdata1.Name = "_singhaGridGetdata1";
            this._singhaGridGetdata1.Size = new System.Drawing.Size(1124, 672);
            this._singhaGridGetdata1.TabIndex = 11;
            this._singhaGridGetdata1.TabStop = false;
            // 
            // _myPanel3
            // 
            this._myPanel3._switchTabAuto = false;
            this._myPanel3.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel3.Controls.Add(this.button_process);
            this._myPanel3.CornerPicture = null;
            this._myPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myPanel3.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel3.Location = new System.Drawing.Point(0, 672);
            this._myPanel3.Name = "_myPanel3";
            this._myPanel3.Size = new System.Drawing.Size(1124, 64);
            this._myPanel3.TabIndex = 10;
            // 
            // _transferControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myPanel2);
            this.Controls.Add(this._myPanel1);
            this.Name = "_transferControl";
            this.Size = new System.Drawing.Size(1124, 780);
            this._myPanel1.ResumeLayout(false);
            this._myPanel2.ResumeLayout(false);
            this._myPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_prepare;
        private System.Windows.Forms.Button button_process;
        private MyLib._myPanel _myPanel1;
        private MyLib._myPanel _myPanel2;
        private MyLib._myPanel _myPanel3;
        protected _singhaGridGetdata _singhaGridGetdata1;
    }
}
