namespace SMLERPControl._contactDetail
{
    partial class _contactDescription
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
            this._myGridContact = new MyLib._myGrid();
            this.SuspendLayout();
            // 
            // _myGridContact
            // 
            this._myGridContact.BackColor = System.Drawing.SystemColors.Window;
            this._myGridContact.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            this._myGridContact.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._myGridContact.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myGridContact.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myGridContact.Location = new System.Drawing.Point(0, 0);
            this._myGridContact.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._myGridContact.Name = "_myGridContact";
            this._myGridContact.Size = new System.Drawing.Size(549, 460);
            this._myGridContact.TabIndex = 0;
            this._myGridContact.TabStop = false;
            // 
            // _contractDescription
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myGridContact);
            this.Name = "_contractDescription";
            this.Size = new System.Drawing.Size(549, 460);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myGrid _myGridContact;
    }
}
