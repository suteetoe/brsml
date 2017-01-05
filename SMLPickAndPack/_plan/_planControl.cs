using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPickAndPack._plan
{
    public partial class _planControl : UserControl
    {
        public _planControl()
        {
            InitializeComponent();
            this._manageData._dataList._lockRecord = true;
            this._manageData._autoSize = true;
            this._manageData._displayMode = 0;
            this._manageData._selectDisplayMode(this._manageData._displayMode);
            this._manageData._dataList._loadViewFormat("screen_pp_plan", MyLib._myGlobal._userSearchScreenGroup, true);
            this._manageData._autoSizeHeight = 450;
            this._manageData._dataListOpen = true;
        }
    }
}
