namespace SMLPPControl
{
    partial class _shipmentConfirmControl
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
            this._mainPanel = new MyLib._myPanel();
            this.SuspendLayout();
            // 
            // _mainPanel
            // 
            this._mainPanel._switchTabAuto = false;
            this._mainPanel.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._mainPanel.CornerPicture = null;
            this._mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainPanel.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._mainPanel.Location = new System.Drawing.Point(0, 0);
            this._mainPanel.Name = "_mainPanel";
            this._mainPanel.Size = new System.Drawing.Size(761, 764);
            this._mainPanel.TabIndex = 0;
            // 
            // _shipmentConfirmControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._mainPanel);
            this.Name = "_shipmentConfirmControl";
            this.Size = new System.Drawing.Size(761, 764);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myPanel _mainPanel;
    }
}
