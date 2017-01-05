using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLReport._formReport
{
    public partial class _pageSort : Form
    {
        public bool _selectOK = false;

        public _pageSort()
        {
            InitializeComponent();
        }

        private void _pageSort_Load(object sender, EventArgs e)
        {

        }

        private void _buttonOk_Click(object sender, EventArgs e)
        {
            this._selectOK = true;
            this.Close();
        }

        private void _buttonCalcel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _buttonUp_Click(object sender, EventArgs e)
        {
            if (this._listBoxPage.SelectedIndex > 0)
            {
                Object __getObject = (Object)this._listBoxPage.Items[this._listBoxPage.SelectedIndex];
                int __selectIndex = this._listBoxPage.SelectedIndex;
                this._listBoxPage.Items.RemoveAt(this._listBoxPage.SelectedIndex);
                this._listBoxPage.Items.Insert(__selectIndex - 1, __getObject);
                this._listBoxPage.SelectedIndex = __selectIndex - 1;
            }
        }

        private void _buttonDown_Click(object sender, EventArgs e)
        {
            if (this._listBoxPage.SelectedIndex < this._listBoxPage.Items.Count - 1)
            {
                Object __getObject = (Object)this._listBoxPage.Items[this._listBoxPage.SelectedIndex];
                int __selectIndex = this._listBoxPage.SelectedIndex;
                this._listBoxPage.Items.RemoveAt(this._listBoxPage.SelectedIndex);
                this._listBoxPage.Items.Insert(__selectIndex + 1, __getObject);
                this._listBoxPage.SelectedIndex = __selectIndex + 1;
            }
        }
    }
}