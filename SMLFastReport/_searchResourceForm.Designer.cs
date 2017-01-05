namespace SMLFastReport
{
    partial class _searchResourceForm
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this._searchTextBox = new System.Windows.Forms.ToolStripTextBox();
            this._grid = new System.Windows.Forms.DataGridView();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this._searchTextBox});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(813, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(48, 22);
            this.toolStripLabel1.Text = "Search :";
            // 
            // _searchTextBox
            // 
            this._searchTextBox.Name = "_searchTextBox";
            this._searchTextBox.Size = new System.Drawing.Size(300, 25);
            // 
            // _grid
            // 
            this._grid.AllowUserToAddRows = false;
            this._grid.AllowUserToDeleteRows = false;
            this._grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Code,
            this.name_1,
            this.name_2});
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.Location = new System.Drawing.Point(0, 25);
            this._grid.Name = "_grid";
            this._grid.ReadOnly = true;
            this._grid.Size = new System.Drawing.Size(813, 606);
            this._grid.TabIndex = 1;
            // 
            // Code
            // 
            this.Code.HeaderText = "Code";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            // 
            // name_1
            // 
            this.name_1.HeaderText = "Name";
            this.name_1.Name = "name_1";
            this.name_1.ReadOnly = true;
            // 
            // name_2
            // 
            this.name_2.HeaderText = "Name2";
            this.name_2.Name = "name_2";
            this.name_2.ReadOnly = true;
            // 
            // _searchResourceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 631);
            this.Controls.Add(this._grid);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_searchResourceForm";
            this.Text = "Resource";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox _searchTextBox;
        private System.Windows.Forms.DataGridView _grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn name_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn name_2;
    }
}