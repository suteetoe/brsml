﻿namespace SMLERPTemplate
{
    partial class _selectMenuShortcutForm
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this._menuPanel = new System.Windows.Forms.Panel();
            this._menuTree = new MyLib._myTreeView();
            this.flowLayoutPanel1.SuspendLayout();
            this._menuPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Controls.Add(this.button2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 535);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(605, 29);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(527, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(446, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // _menuPanel
            // 
            this._menuPanel.BackColor = System.Drawing.Color.White;
            this._menuPanel.Controls.Add(this._menuTree);
            this._menuPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._menuPanel.Location = new System.Drawing.Point(0, 0);
            this._menuPanel.Name = "_menuPanel";
            this._menuPanel.Size = new System.Drawing.Size(605, 535);
            this._menuPanel.TabIndex = 1;
            // 
            // _menuTree
            // 
            this._menuTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this._menuTree.Location = new System.Drawing.Point(0, 0);
            this._menuTree.Name = "_menuTree";
            this._menuTree.Size = new System.Drawing.Size(605, 535);
            this._menuTree.TabIndex = 0;
            // 
            // _selectMenuShortcutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 564);
            this.Controls.Add(this._menuPanel);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "_selectMenuShortcutForm";
            this.Text = "Select Menu";
            this.flowLayoutPanel1.ResumeLayout(false);
            this._menuPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.Panel _menuPanel;
        public MyLib._myTreeView _menuTree;
    }
}