namespace SMLPOSControl._designer
{
    partial class _deletePOSDesign
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
            this._myDataList1.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myDataList1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myDataList1.Location = new System.Drawing.Point(0, 0);
            this._myDataList1.Margin = new System.Windows.Forms.Padding(0);
            this._myDataList1.Name = "_myDataList1";
            this._myDataList1.Size = new System.Drawing.Size(549, 465);
            this._myDataList1.TabIndex = 0;
            this._myDataList1.TabStop = false;
            // 
            // _deletePOSDesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 468);
            this.Controls.Add(this._myDataList1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_deletePOSDesign";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Delete Design";
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myDataList _myDataList1;
    }
}