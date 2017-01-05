namespace SMLERPGL._design
{
    partial class _conditionForm
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
            this._myTab = new MyLib._myTabControl();
            this._tabCondition = new System.Windows.Forms.TabPage();
            this._tabDesign = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.vistaButton1 = new MyLib.VistaButton();
            this.vistaButton2 = new MyLib.VistaButton();
            this._myPanel1 = new MyLib._myPanel();
            this._myTab.SuspendLayout();
            this._tabCondition.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myTab
            // 
            this._myTab.Controls.Add(this._tabCondition);
            this._myTab.Controls.Add(this._tabDesign);
            this._myTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myTab.Font = new System.Drawing.Font("Tahoma", 9F);
            this._myTab.Location = new System.Drawing.Point(0, 0);
            this._myTab.Multiline = true;
            this._myTab.Name = "_myTab";
            this._myTab.SelectedIndex = 0;
            this._myTab.Size = new System.Drawing.Size(842, 711);
            this._myTab.TabIndex = 0;
            this._myTab.TableName = "";
            // 
            // _tabCondition
            // 
            this._tabCondition.Controls.Add(this._myPanel1);
            this._tabCondition.Location = new System.Drawing.Point(4, 23);
            this._tabCondition.Name = "_tabCondition";
            this._tabCondition.Padding = new System.Windows.Forms.Padding(3);
            this._tabCondition.Size = new System.Drawing.Size(834, 684);
            this._tabCondition.TabIndex = 0;
            this._tabCondition.Text = "Condition";
            this._tabCondition.UseVisualStyleBackColor = true;
            // 
            // _tabDesign
            // 
            this._tabDesign.Location = new System.Drawing.Point(4, 23);
            this._tabDesign.Name = "_tabDesign";
            this._tabDesign.Padding = new System.Windows.Forms.Padding(3);
            this._tabDesign.Size = new System.Drawing.Size(834, 684);
            this._tabDesign.TabIndex = 1;
            this._tabDesign.Text = "Design";
            this._tabDesign.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.vistaButton1);
            this.flowLayoutPanel1.Controls.Add(this.vistaButton2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 648);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel1.Size = new System.Drawing.Size(828, 30);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // vistaButton1
            // 
            this.vistaButton1._drawNewMethod = false;
            this.vistaButton1.BackColor = System.Drawing.Color.Transparent;
            this.vistaButton1.ButtonText = "Process";
            this.vistaButton1.Location = new System.Drawing.Point(749, 4);
            this.vistaButton1.Name = "vistaButton1";
            this.vistaButton1.Size = new System.Drawing.Size(74, 24);
            this.vistaButton1.TabIndex = 0;
            this.vistaButton1.Text = "vistaButton1";
            this.vistaButton1.UseVisualStyleBackColor = false;
            // 
            // vistaButton2
            // 
            this.vistaButton2._drawNewMethod = false;
            this.vistaButton2.BackColor = System.Drawing.Color.Transparent;
            this.vistaButton2.ButtonText = "Close";
            this.vistaButton2.Location = new System.Drawing.Point(669, 4);
            this.vistaButton2.Name = "vistaButton2";
            this.vistaButton2.Size = new System.Drawing.Size(74, 24);
            this.vistaButton2.TabIndex = 1;
            this.vistaButton2.Text = "vistaButton2";
            this.vistaButton2.UseVisualStyleBackColor = false;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this.flowLayoutPanel1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(3, 3);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(828, 678);
            this._myPanel1.TabIndex = 2;
            // 
            // _conditionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 711);
            this.Controls.Add(this._myTab);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_conditionForm";
            this.Text = "_conditionForm";
            this._myTab.ResumeLayout(false);
            this._tabCondition.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this._myPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myTabControl _myTab;
        private System.Windows.Forms.TabPage _tabCondition;
        private System.Windows.Forms.TabPage _tabDesign;
        private MyLib._myPanel _myPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MyLib.VistaButton vistaButton1;
        private MyLib.VistaButton vistaButton2;
    }
}