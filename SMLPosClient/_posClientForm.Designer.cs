namespace SMLPosClient
{
    partial class _posClientForm
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
            this._container = new Crom.Controls.Docking.DockContainer();
            this._TimerDate = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // _container
            // 
            this._container.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(118)))), ((int)(((byte)(118)))));
            this._container.CanMoveByMouseFilledForms = true;
            this._container.Dock = System.Windows.Forms.DockStyle.Fill;
            this._container.Location = new System.Drawing.Point(0, 0);
            this._container.Name = "_container";
            this._container.Size = new System.Drawing.Size(1162, 748);
            this._container.TabIndex = 0;
            this._container.TitleBarGradientColor1 = System.Drawing.SystemColors.Control;
            this._container.TitleBarGradientColor2 = System.Drawing.Color.White;
            this._container.TitleBarGradientSelectedColor1 = System.Drawing.Color.DarkGray;
            this._container.TitleBarGradientSelectedColor2 = System.Drawing.Color.White;
            this._container.TitleBarTextColor = System.Drawing.Color.Black;
            // 
            // _TimerDate
            // 
            this._TimerDate.Tick += new System.EventHandler(this._TimerDate_Tick);
            // 
            // _posClientForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this._container);
            this.Name = "_posClientForm";
            this.Size = new System.Drawing.Size(1162, 748);
            this.ResumeLayout(false);

        }

        #endregion

        private Crom.Controls.Docking.DockContainer _container;
        private System.Windows.Forms.Timer _TimerDate;
    }
}