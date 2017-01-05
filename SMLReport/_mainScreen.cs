using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLReport._design
{
    public partial class _mainScreen : UserControl
    {
        public _mainScreen()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._myTabControl.SelectedIndexChanged += new EventHandler(_myTabControl_SelectedIndexChanged);
        }

        void _myTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_myTabControl.SelectedIndex == 1)
            {
/*                ArrayList __getField = _designView._linkList._getAllFieldFormPanel();
                _designCondition._fieldGrid._clear();
                for (int __loop = 0; __loop < __getField.Count; __loop++)
                {
                    _designCondition._fieldGrid._addRow();
                    _designCondition._fieldGrid._cellUpdate(__loop, _designCondition._resourceConditionList, __getField[__loop].ToString(), false);
                }*/
            }
        }
    }
}
