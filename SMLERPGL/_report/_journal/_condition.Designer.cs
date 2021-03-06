﻿namespace SMLERPGL._report._journal
{
    partial class _condition
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._buttonOk = new MyLib._myButton();
            this._bookButton = new MyLib._myButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._screenJournalCondition1 = new SMLERPGL._report._screenReportCondition();
            this._selectBook = new SMLERPGL._report._selectJournalBook();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this._selectAllButton = new MyLib._myButton();
            this._selectNoneButton = new MyLib._myButton();
            this._myPanel1 = new MyLib._myPanel();
            this._myShadowLabel1 = new MyLib._myShadowLabel(this.components);
            this.flowLayoutPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this._buttonOk);
            this.flowLayoutPanel1.Controls.Add(this._bookButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 425);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel1.Size = new System.Drawing.Size(644, 26);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // _buttonOk
            // 
            this._buttonOk._drawNewMethod = false;
            this._buttonOk.AutoSize = true;
            this._buttonOk.BackColor = System.Drawing.Color.Transparent;
            this._buttonOk.ButtonText = "ตกลง";
            this._buttonOk.Location = new System.Drawing.Point(579, 0);
            this._buttonOk.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._buttonOk.myImage = global::SMLERPGL.Resource16x16.check;
            this._buttonOk.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonOk.myUseVisualStyleBackColor = true;
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Padding = new System.Windows.Forms.Padding(1);
            this._buttonOk.ResourceName = "ตกลง";
            this._buttonOk.Size = new System.Drawing.Size(64, 24);
            this._buttonOk.TabIndex = 0;
            this._buttonOk.TabStop = false;
            this._buttonOk.Text = "ตกลง";
            this._buttonOk.UseVisualStyleBackColor = false;
            this._buttonOk.Click += new System.EventHandler(this._buttonOk_Click_1);
            // 
            // _bookButton
            // 
            this._bookButton._drawNewMethod = false;
            this._bookButton.AutoSize = true;
            this._bookButton.BackColor = System.Drawing.Color.Transparent;
            this._bookButton.ButtonText = "เลือกสมุดรายวัน";
            this._bookButton.Location = new System.Drawing.Point(461, 0);
            this._bookButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._bookButton.myImage = global::SMLERPGL.Resource16x16.book_blue;
            this._bookButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._bookButton.myUseVisualStyleBackColor = true;
            this._bookButton.Name = "_bookButton";
            this._bookButton.Padding = new System.Windows.Forms.Padding(1);
            this._bookButton.ResourceName = "เลือกสมุดรายวัน";
            this._bookButton.Size = new System.Drawing.Size(116, 24);
            this._bookButton.TabIndex = 1;
            this._bookButton.TabStop = false;
            this._bookButton.Text = "เลือกสมุดรายวัน";
            this._bookButton.UseVisualStyleBackColor = false;
            this._bookButton.Click += new System.EventHandler(this._bookButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 39);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._screenJournalCondition1);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(3);
            this.splitContainer1.Panel1MinSize = 1;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._selectBook);
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel2);
            this.splitContainer1.Panel2MinSize = 1;
            this.splitContainer1.Size = new System.Drawing.Size(644, 386);
            this.splitContainer1.SplitterDistance = 330;
            this.splitContainer1.TabIndex = 2;
            // 
            // _screenJournalCondition1
            // 
            this._screenJournalCondition1._isChange = false;
            this._screenJournalCondition1._screenType = SMLERPGL._report._screenJournalConditionType.Journal;
            this._screenJournalCondition1.AutoSize = true;
            this._screenJournalCondition1.BackColor = System.Drawing.Color.Transparent;
            this._screenJournalCondition1.Dock = System.Windows.Forms.DockStyle.Top;
            this._screenJournalCondition1.Location = new System.Drawing.Point(3, 3);
            this._screenJournalCondition1.Name = "_screenJournalCondition1";
            this._screenJournalCondition1.Size = new System.Drawing.Size(324, 181);
            this._screenJournalCondition1.TabIndex = 0;
            // 
            // _selectBook
            // 
            this._selectBook.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._selectBook.Dock = System.Windows.Forms.DockStyle.Fill;
            this._selectBook.Location = new System.Drawing.Point(0, 0);
            this._selectBook.Name = "_selectBook";
            this._selectBook.Size = new System.Drawing.Size(310, 360);
            this._selectBook.TabIndex = 5;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel2.Controls.Add(this._selectAllButton);
            this.flowLayoutPanel2.Controls.Add(this._selectNoneButton);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 360);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel2.Size = new System.Drawing.Size(310, 26);
            this.flowLayoutPanel2.TabIndex = 6;
            // 
            // _selectAllButton
            // 
            this._selectAllButton._drawNewMethod = false;
            this._selectAllButton.AutoSize = true;
            this._selectAllButton.BackColor = System.Drawing.Color.Transparent;
            this._selectAllButton.ButtonText = "เลือกทั้งหมด";
            this._selectAllButton.Location = new System.Drawing.Point(1, 0);
            this._selectAllButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._selectAllButton.myImage = global::SMLERPGL.Resource16x16.checks;
            this._selectAllButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._selectAllButton.myUseVisualStyleBackColor = true;
            this._selectAllButton.Name = "_selectAllButton";
            this._selectAllButton.Padding = new System.Windows.Forms.Padding(1);
            this._selectAllButton.ResourceName = "เลือกทั้งหมด";
            this._selectAllButton.Size = new System.Drawing.Size(100, 24);
            this._selectAllButton.TabIndex = 1;
            this._selectAllButton.TabStop = false;
            this._selectAllButton.Text = "เลือกทั้งหมด";
            this._selectAllButton.UseVisualStyleBackColor = false;
            this._selectAllButton.Click += new System.EventHandler(this._selectAllButton_Click);
            // 
            // _selectNoneButton
            // 
            this._selectNoneButton._drawNewMethod = false;
            this._selectNoneButton.AutoSize = true;
            this._selectNoneButton.BackColor = System.Drawing.Color.Transparent;
            this._selectNoneButton.ButtonText = "ไม่เลือกทั้งหมด";
            this._selectNoneButton.Location = new System.Drawing.Point(103, 0);
            this._selectNoneButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._selectNoneButton.myImage = global::SMLERPGL.Resource16x16.delete2;
            this._selectNoneButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._selectNoneButton.myUseVisualStyleBackColor = true;
            this._selectNoneButton.Name = "_selectNoneButton";
            this._selectNoneButton.Padding = new System.Windows.Forms.Padding(1);
            this._selectNoneButton.ResourceName = "ไม่เลือกทั้งหมด";
            this._selectNoneButton.Size = new System.Drawing.Size(114, 24);
            this._selectNoneButton.TabIndex = 2;
            this._selectNoneButton.TabStop = false;
            this._selectNoneButton.Text = "ไม่เลือกทั้งหมด";
            this._selectNoneButton.UseVisualStyleBackColor = false;
            this._selectNoneButton.Click += new System.EventHandler(this._selectNoneButton_Click);
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.AutoSize = true;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myPanel1.Controls.Add(this._myShadowLabel1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(4);
            this._myPanel1.Size = new System.Drawing.Size(644, 39);
            this._myPanel1.TabIndex = 3;
            // 
            // _myShadowLabel1
            // 
            this._myShadowLabel1.Angle = 0F;
            this._myShadowLabel1.AutoSize = true;
            this._myShadowLabel1.BackColor = System.Drawing.Color.Transparent;
            this._myShadowLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myShadowLabel1.DrawGradient = false;
            this._myShadowLabel1.EndColor = System.Drawing.Color.LightSkyBlue;
            this._myShadowLabel1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myShadowLabel1.ForeColor = System.Drawing.Color.LightSkyBlue;
            this._myShadowLabel1.Location = new System.Drawing.Point(4, 4);
            this._myShadowLabel1.Name = "_myShadowLabel1";
            this._myShadowLabel1.ResourceName = "รายงานข้อมูลรายวัน";
            this._myShadowLabel1.ShadowColor = System.Drawing.Color.Black;
            this._myShadowLabel1.Size = new System.Drawing.Size(209, 29);
            this._myShadowLabel1.StartColor = System.Drawing.Color.White;
            this._myShadowLabel1.TabIndex = 2;
            this._myShadowLabel1.Text = "รายงานข้อมูลรายวัน";
            this._myShadowLabel1.XOffset = 1F;
            this._myShadowLabel1.YOffset = 1F;
            // 
            // _condition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(644, 451);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_condition";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_condition";
            this.Load += new System.EventHandler(this._condition_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MyLib._myShadowLabel _myShadowLabel1;
        private MyLib._myPanel _myPanel1;
        public _screenReportCondition _screenJournalCondition1;
        public MyLib._myButton _buttonOk;
        public MyLib._myButton _bookButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public _selectJournalBook _selectBook;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        public MyLib._myButton _selectAllButton;
        public MyLib._myButton _selectNoneButton;
    }
}