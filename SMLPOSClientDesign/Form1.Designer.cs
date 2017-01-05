namespace SMLPOSClientDesign
{
    partial class Form1
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this._genXMLButton = new System.Windows.Forms.Button();
            this._myDataList = new MyLib._myDataList();
            this.panel1 = new System.Windows.Forms.Panel();
            this._textXML = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._saveButton = new System.Windows.Forms.Button();
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this._resultLabel = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel2);
            this.splitContainer1.Panel1.Controls.Add(this._myDataList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(876, 471);
            this.splitContainer1.SplitterDistance = 347;
            this.splitContainer1.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this._genXMLButton);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 440);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(347, 31);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // _genXMLButton
            // 
            this._genXMLButton.Location = new System.Drawing.Point(269, 3);
            this._genXMLButton.Name = "_genXMLButton";
            this._genXMLButton.Size = new System.Drawing.Size(75, 23);
            this._genXMLButton.TabIndex = 0;
            this._genXMLButton.Text = "Gen XML";
            this._genXMLButton.UseVisualStyleBackColor = true;
            this._genXMLButton.Click += new System.EventHandler(this._genXMLButton_Click);
            // 
            // _myDataList
            // 
            this._myDataList._extraWhere = "";
            this._myDataList._multiSelect = false;
            this._myDataList.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myDataList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myDataList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myDataList.Location = new System.Drawing.Point(0, 0);
            this._myDataList.Margin = new System.Windows.Forms.Padding(0);
            this._myDataList.Name = "_myDataList";
            this._myDataList.Size = new System.Drawing.Size(347, 471);
            this._myDataList.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._textXML);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(525, 440);
            this.panel1.TabIndex = 2;
            // 
            // _textXML
            // 
            this._textXML.Dock = System.Windows.Forms.DockStyle.Fill;
            this._textXML.Location = new System.Drawing.Point(0, 0);
            this._textXML.Multiline = true;
            this._textXML.Name = "_textXML";
            this._textXML.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._textXML.Size = new System.Drawing.Size(525, 440);
            this._textXML.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this._saveButton);
            this.flowLayoutPanel1.Controls.Add(this._progressBar);
            this.flowLayoutPanel1.Controls.Add(this._resultLabel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 440);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(525, 31);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // _saveButton
            // 
            this._saveButton.Enabled = false;
            this._saveButton.Location = new System.Drawing.Point(447, 3);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(75, 23);
            this._saveButton.TabIndex = 0;
            this._saveButton.Text = "Save";
            this._saveButton.UseVisualStyleBackColor = true;
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _progressBar
            // 
            this._progressBar.Location = new System.Drawing.Point(215, 3);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(226, 23);
            this._progressBar.TabIndex = 1;
            // 
            // _resultLabel
            // 
            this._resultLabel.AutoSize = true;
            this._resultLabel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._resultLabel.Location = new System.Drawing.Point(209, 0);
            this._resultLabel.Name = "_resultLabel";
            this._resultLabel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this._resultLabel.Size = new System.Drawing.Size(0, 23);
            this._resultLabel.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 471);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private MyLib._myDataList _myDataList;
        private System.Windows.Forms.TextBox _textXML;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button _saveButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button _genXMLButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ProgressBar _progressBar;
        private System.Windows.Forms.Label _resultLabel;
    }
}

