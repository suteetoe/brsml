namespace SMLReport._formReport
{
    partial class _openFormServer
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this._manualLoadData = new System.Windows.Forms.Button();
            this._dataGrid = new MyLib._myGrid();
            this._myDataList1 = new MyLib._myDataList();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(97, 379);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(182, 20);
            this.textBox1.TabIndex = 1;
            // 
            // _manualLoadData
            // 
            this._manualLoadData.Location = new System.Drawing.Point(285, 379);
            this._manualLoadData.Name = "_manualLoadData";
            this._manualLoadData.Size = new System.Drawing.Size(75, 23);
            this._manualLoadData.TabIndex = 2;
            this._manualLoadData.Text = "load";
            this._manualLoadData.UseVisualStyleBackColor = true;
            this._manualLoadData.Click += new System.EventHandler(this._manualLoadData_Click);
            // 
            // _dataGrid
            // 
            this._dataGrid._extraWordShow = true;
            this._dataGrid._selectRow = -1;
            this._dataGrid.BackColor = System.Drawing.SystemColors.Window;
            this._dataGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._dataGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._dataGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._dataGrid.Location = new System.Drawing.Point(0, 0);
            this._dataGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._dataGrid.Name = "_dataGrid";
            this._dataGrid.Size = new System.Drawing.Size(540, 440);
            this._dataGrid.TabIndex = 3;
            this._dataGrid.TabStop = false;
            // 
            // _myDataList1
            // 
            this._myDataList1.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myDataList1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myDataList1.Location = new System.Drawing.Point(0, 0);
            this._myDataList1.Margin = new System.Windows.Forms.Padding(0);
            this._myDataList1.Name = "_myDataList1";
            this._myDataList1.Size = new System.Drawing.Size(540, 440);
            this._myDataList1.TabIndex = 4;
            this._myDataList1.TabStop = false;
            // 
            // _openFormServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 443);
            this.Controls.Add(this._myDataList1);
            this.Controls.Add(this._manualLoadData);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this._dataGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "_openFormServer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Open Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button _manualLoadData;
        private MyLib._myGrid _dataGrid;
        public MyLib._myDataList _myDataList1;
    }
}