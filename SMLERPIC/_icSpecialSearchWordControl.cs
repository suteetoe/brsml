using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPIC
{
    public partial class _icSpecialSearchWordControl : UserControl
    {
        public _icSpecialSearchWordControl()
        {
            InitializeComponent();

            //this._gridPiston._afterSelectRow += _gridPart__afterSelectRow;
            //this._gridPistonRim._afterSelectRow += _gridPart__afterSelectRow;
            //this._gridOtherPart._afterSelectRow += _gridPart__afterSelectRow;
        }

        private void _gridPart__afterSelectRow(object sender, int row)
        {
            MyLib._myGrid __grid = (MyLib._myGrid)sender;

            string __getPartProductCode = __grid._cellGet(row, _g.d.ic_specific_search._ic_code).ToString();

            if (__getPartProductCode.Length > 0)
            {
                //_loadPartInfo(__getPartProductCode);
            }
        }


    }
}
