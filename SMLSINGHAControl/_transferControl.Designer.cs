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
            this._myPanel2 = new MyLib._myPanel();
            this._singhaGridGetdata1 = new SMLSINGHAControl._singhaGridGetdata();
            this._myPanel3 = new MyLib._myPanel();
            this.button_selectNone = new MyLib.VistaButton();
            this.button_selectAll = new MyLib.VistaButton();
            this.button_process = new MyLib.VistaButton();
            this._myPanel2.SuspendLayout();
            this._myPanel3.SuspendLayout();
            this.SuspendLayout();
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
            this._myPanel2.Location = new System.Drawing.Point(0, 0);
            this._myPanel2.Name = "_myPanel2";
            this._myPanel2.Size = new System.Drawing.Size(1124, 780);
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
            this._singhaGridGetdata1.Size = new System.Drawing.Size(1124, 716);
            this._singhaGridGetdata1.TabIndex = 11;
            this._singhaGridGetdata1.TabStop = false;
            // 
            // _myPanel3
            // 
            this._myPanel3._switchTabAuto = false;
            this._myPanel3.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel3.Controls.Add(this.button_selectNone);
            this._myPanel3.Controls.Add(this.button_selectAll);
            this._myPanel3.Controls.Add(this.button_process);
            this._myPanel3.CornerPicture = null;
            this._myPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myPanel3.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel3.Location = new System.Drawing.Point(0, 716);
            this._myPanel3.Name = "_myPanel3";
            this._myPanel3.Size = new System.Drawing.Size(1124, 64);
            this._myPanel3.TabIndex = 10;
            // 
            // button_selectNone
            // 
            this.button_selectNone._drawNewMethod = false;
            this.button_selectNone.BackColor = System.Drawing.Color.Transparent;
            this.button_selectNone.ButtonColor = System.Drawing.Color.MediumSpringGreen;
            this.button_selectNone.ButtonText = "Select None";
            this.button_selectNone.Location = new System.Drawing.Point(109, 18);
            this.button_selectNone.Name = "button_selectNone";
            this.button_selectNone.Size = new System.Drawing.Size(100, 32);
            this.button_selectNone.TabIndex = 5;
            this.button_selectNone.Text = "selectNone";
            this.button_selectNone.UseVisualStyleBackColor = true;
            // 
            // button_selectAll
            // 
            this.button_selectAll._drawNewMethod = false;
            this.button_selectAll.BackColor = System.Drawing.Color.Transparent;
            this.button_selectAll.ButtonColor = System.Drawing.Color.MediumSpringGreen;
            this.button_selectAll.ButtonText = "Select All";
            this.button_selectAll.Location = new System.Drawing.Point(3, 18);
            this.button_selectAll.Name = "button_selectAll";
            this.button_selectAll.Size = new System.Drawing.Size(100, 32);
            this.button_selectAll.TabIndex = 4;
            this.button_selectAll.Text = "select All";
            this.button_selectAll.UseVisualStyleBackColor = true;
            // 
            // button_process
            // 
            this.button_process._drawNewMethod = false;
            this.button_process.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_process.BackColor = System.Drawing.Color.Transparent;
            this.button_process.ButtonText = "Process";
            this.button_process.Location = new System.Drawing.Point(1021, 16);
            this.button_process.Name = "button_process";
            this.button_process.Size = new System.Drawing.Size(100, 32);
            this.button_process.TabIndex = 2;
            this.button_process.Text = "Process";
            this.button_process.UseVisualStyleBackColor = true;
            // 
            // _transferControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myPanel2);
            this.Name = "_transferControl";
            this.Size = new System.Drawing.Size(1124, 780);
            this._myPanel2.ResumeLayout(false);
            this._myPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MyLib._myPanel _myPanel2;
        private MyLib._myPanel _myPanel3;
        public _singhaGridGetdata _singhaGridGetdata1;
        public MyLib.VistaButton button_process;
        public MyLib.VistaButton button_selectNone;
        public MyLib.VistaButton button_selectAll;
    }
}
