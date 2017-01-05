namespace SMLInventoryControl._utils
{
    partial class _selectSerialNumberForm
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
            this._myPanel1 = new MyLib._myPanel();
            this._allCheckBox = new System.Windows.Forms.CheckBox();
            this._dataList = new MyLib._myDataList();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.AutoSize = true;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._allCheckBox);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(452, 24);
            this._myPanel1.TabIndex = 0;
            // 
            // _allCheckBox
            // 
            this._allCheckBox.AutoSize = true;
            this._allCheckBox.BackColor = System.Drawing.Color.Transparent;
            this._allCheckBox.Location = new System.Drawing.Point(13, 3);
            this._allCheckBox.Name = "_allCheckBox";
            this._allCheckBox.Size = new System.Drawing.Size(38, 18);
            this._allCheckBox.TabIndex = 0;
            this._allCheckBox.Text = "All";
            this._allCheckBox.UseVisualStyleBackColor = false;
            // 
            // _dataList
            // 
            this._dataList.BackColor = System.Drawing.Color.WhiteSmoke;
            this._dataList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._dataList.Location = new System.Drawing.Point(0, 24);
            this._dataList.Margin = new System.Windows.Forms.Padding(0);
            this._dataList.Name = "_dataList";
            this._dataList.Size = new System.Drawing.Size(452, 340);
            this._dataList.TabIndex = 1;
            this._dataList.TabStop = false;
            // 
            // _selectSerialNumberForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 364);
            this.ControlBox = false;
            this.Controls.Add(this._dataList);
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_selectSerialNumberForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Serial Number";
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private System.Windows.Forms.CheckBox _allCheckBox;
        public MyLib._myDataList _dataList;
    }
}