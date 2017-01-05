using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLReport._formReport
{
    public partial class _queryParameterForm : MyLib._myForm
    {
        public _queryParameterForm(List<string> __conditionList, List<_formPrint._conditionClass> __defineCondition)
        {
            InitializeComponent();

            this._conditionScreen._maxColumn = 1;
            for (int __i = 0; __i < __conditionList.Count; __i++)
            {
                this._conditionScreen._addTextBox(__i, 0, 1, 0, __conditionList[__i], 1, 2, 0, true, false, false, false, true, __conditionList[__i]);
            }

            for (int __i = 0; __i < __defineCondition.Count; __i++)
            {
                this._conditionScreen._setDataStr(__defineCondition[__i]._fieldName, __defineCondition[__i]._value);
            }
        }

        private void _bt_process_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
