namespace SMLFastReport
{
    partial class _deleteForm
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
            this._myDataList = new MyLib._myDataList();
            this.SuspendLayout();
            // 
            // _myDataList
            // 
            this._myDataList._extraWhere = "";
            this._myDataList._multiSelect = false;
            this._myDataList.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myDataList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myDataList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myDataList.Location = new System.Drawing.Point(0, 0);
            this._myDataList.Margin = new System.Windows.Forms.Padding(0);
            this._myDataList.Name = "_myDataList";
            this._myDataList.Size = new System.Drawing.Size(391, 365);
            this._myDataList.TabIndex = 0;
            // 
            // _deleteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 365);
            this.Controls.Add(this._myDataList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_deleteForm";
            this.Text = "ลบรายงาย";
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myDataList _myDataList;
    }
}