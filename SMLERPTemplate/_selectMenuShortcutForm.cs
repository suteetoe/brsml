using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPTemplate
{
    public partial class _selectMenuShortcutForm : Form
    {
        public string _selectMenuCode = "";
        public string _selectMenuName = "";
        public string _selectMenuTag = "";

        public _selectMenuShortcutForm()
        {
            InitializeComponent();

            this._menuTree.NodeMouseDoubleClick += _menuTree_NodeMouseDoubleClick;
        }

        void _menuTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Nodes.Count == 0 && MessageBox.Show("คุณต้องการ เพิ่ม เมนู " + e.Node.Text + " เข้าไป หรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                this._selectMenuCode = e.Node.Name;
                this._selectMenuName = e.Node.Text;
                if (e.Node.Tag != null)
                {
                    this._selectMenuTag = e.Node.Tag.ToString();
                }
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            }
        }
    }
}
