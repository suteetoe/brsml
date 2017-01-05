namespace SMLERPControl._customer
{
    partial class _arStatusForm
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
            this.components = new System.ComponentModel.Container();
            this._tab = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this._arStatusGrid = new MyLib._myGrid();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this._arChqGrid = new MyLib._myGrid();
            this._webBrowser = new System.Windows.Forms.WebBrowser();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this._srssGrid = new MyLib._myGrid();
            this._tab.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _tab
            // 
            this._tab.Controls.Add(this.tabPage2);
            this._tab.Controls.Add(this.tabPage3);
            this._tab.Controls.Add(this.tabPage1);
            this._tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tab.Location = new System.Drawing.Point(0, 138);
            this._tab.Name = "_tab";
            this._tab.SelectedIndex = 0;
            this._tab.Size = new System.Drawing.Size(744, 538);
            this._tab.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this._arStatusGrid);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(736, 512);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Status";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // _arStatusGrid
            // 
            this._arStatusGrid._extraWordShow = true;
            this._arStatusGrid._selectRow = -1;
            this._arStatusGrid.BackColor = System.Drawing.SystemColors.Window;
            this._arStatusGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._arStatusGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._arStatusGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._arStatusGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._arStatusGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._arStatusGrid.Location = new System.Drawing.Point(3, 3);
            this._arStatusGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._arStatusGrid.Name = "_arStatusGrid";
            this._arStatusGrid.Size = new System.Drawing.Size(730, 506);
            this._arStatusGrid.TabIndex = 4;
            this._arStatusGrid.TabStop = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this._arChqGrid);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(736, 512);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Cheque";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // _arChqGrid
            // 
            this._arChqGrid._extraWordShow = true;
            this._arChqGrid._selectRow = -1;
            this._arChqGrid.BackColor = System.Drawing.SystemColors.Window;
            this._arChqGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._arChqGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._arChqGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._arChqGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._arChqGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._arChqGrid.Location = new System.Drawing.Point(3, 3);
            this._arChqGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._arChqGrid.Name = "_arChqGrid";
            this._arChqGrid.Size = new System.Drawing.Size(730, 506);
            this._arChqGrid.TabIndex = 3;
            this._arChqGrid.TabStop = false;
            // 
            // _webBrowser
            // 
            this._webBrowser.Dock = System.Windows.Forms.DockStyle.Top;
            this._webBrowser.Location = new System.Drawing.Point(0, 0);
            this._webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this._webBrowser.Name = "_webBrowser";
            this._webBrowser.Size = new System.Drawing.Size(744, 138);
            this._webBrowser.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this._srssGrid);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(736, 512);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "SR, SS";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // _srssGrid
            // 
            this._srssGrid._extraWordShow = true;
            this._srssGrid._selectRow = -1;
            this._srssGrid.BackColor = System.Drawing.SystemColors.Window;
            this._srssGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._srssGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._srssGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._srssGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._srssGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._srssGrid.Location = new System.Drawing.Point(0, 0);
            this._srssGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._srssGrid.Name = "_srssGrid";
            this._srssGrid.Size = new System.Drawing.Size(736, 512);
            this._srssGrid.TabIndex = 4;
            this._srssGrid.TabStop = false;
            // 
            // _arStatusFormcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 676);
            this.Controls.Add(this._tab);
            this.Controls.Add(this._webBrowser);
            this.Name = "_arStatusFormcs";
            this.Text = "_arStatusFormcs";
            this._tab.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl _tab;
        private System.Windows.Forms.TabPage tabPage2;
        private MyLib._myGrid _arStatusGrid;
        private System.Windows.Forms.TabPage tabPage3;
        private MyLib._myGrid _arChqGrid;
        private System.Windows.Forms.WebBrowser _webBrowser;
        private System.Windows.Forms.TabPage tabPage1;
        private MyLib._myGrid _srssGrid;
    }
}