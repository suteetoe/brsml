using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLFastReport
{
    public partial class _searchResourceForm : Form
    {
        public _searchResourceForm()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            this._loadResource();
            this._searchTextBox.TextChanged += (s1, e1) =>
            {
                this._loadResource();
            };
        }

        void _loadResource()
        {
            try
            {
                this._grid.Rows.Clear();
                string[] __split = this._searchTextBox.Text.Trim().Split(' ');
                StringBuilder __queryName = new StringBuilder();
                for (int __loop = 0; __loop < __split.Length; __loop++)
                {
                    if (__split[__loop].Trim().Length > 0)
                    {
                        if (__queryName.Length > 0) __queryName.Append(" and ");
                        __queryName.Append("code+name_1 like \'%" + __split[__loop].Trim() + "%\'");
                    }
                }
                DataRow[] __rows = (this._searchTextBox.Text.Trim().Length == 0) ? MyLib._myResource._resource.Select() : MyLib._myResource._resource.Select(__queryName.ToString());
                for (int __loop = 0; __loop < __rows.Length; __loop++)
                {
                    this._grid.Rows.Add(__rows[__loop]["code"].ToString(), __rows[__loop]["name_1"].ToString(), __rows[__loop]["name_2"].ToString());
                }
            }
            catch
            {
            }
        }
    }
}
