namespace MyLib._databaseManage
{
    partial class _databaseStructForm
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
            this._database = new MyLib._databaseManage._databaseStruct();
            this.SuspendLayout();
            // 
            // _database
            // 
            this._database.BackColor = System.Drawing.Color.OldLace;
            this._database.Dock = System.Windows.Forms.DockStyle.Fill;
            this._database.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._database.Location = new System.Drawing.Point(0, 0);
            this._database.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._database.Name = "_database";
            this._database.Padding = new System.Windows.Forms.Padding(5);
            this._database.Size = new System.Drawing.Size(688, 526);
            this._database.TabIndex = 0;
            this._database.Load += new System.EventHandler(this._database_Load);
            // 
            // _databaseStructForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 526);
            this.Controls.Add(this._database);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "_databaseStructForm";
            this.Text = "_databaseStructForm";
            this.ResumeLayout(false);

        }

        #endregion

        public _databaseStruct _database;

    }
}