namespace SMLFastReport
{
    partial class _loadForm
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
            this._myDataList1 = new MyLib._myDataList();
            this.SuspendLayout();
            // 
            // _myDataList1
            // 
            this._myDataList1._extraWhere = "";
            this._myDataList1._multiSelect = false;
            this._myDataList1.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myDataList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myDataList1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myDataList1.Location = new System.Drawing.Point(0, 0);
            this._myDataList1.Margin = new System.Windows.Forms.Padding(0);
            this._myDataList1.Name = "_myDataList1";
            this._myDataList1.Size = new System.Drawing.Size(871, 441);
            this._myDataList1.TabIndex = 0;
            this._myDataList1.TabStop = false;
            // 
            // _loadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 441);
            this.Controls.Add(this._myDataList1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_loadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Load Report";
            this.ResumeLayout(false);

        }

        #endregion

        public MyLib._myDataList _myDataList1;
    }
}