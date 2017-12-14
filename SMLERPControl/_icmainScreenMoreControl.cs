using System;
using System.Collections.Generic;
using System.Text;

namespace SMLERPControl
{
    public class _icmainScreenMoreControl : MyLib._myScreen
    {
        public _icmainScreenMoreControl()
        {
            int __row = 0;
            this._maxColumn = 2;
            this._table_name = _g.d.ic_inventory._table;

            this._addNumberBox(__row++, 0, 1, 1, _g.d.ic_inventory._production_period, 1, 0, true);
            this._addCheckBox(__row, 0, _g.d.ic_inventory._barcode_checker_print, false, true);
            this._addCheckBox(__row++, 1, _g.d.ic_inventory._is_product_boonrawd, false, true);
            this._addCheckBox(__row, 0, _g.d.ic_inventory._print_order_per_unit, false, true);
            this._addCheckBox(__row++, 1, _g.d.ic_inventory._is_premium, false, true);

            //this._maxLabelWidth = new int[] { 40, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            this._addCheckBox(__row++, 0, _g.d.ic_inventory._is_eordershow, false, true);
            this._addCheckBox(__row++, 0, _g.d.ic_inventory._is_speech, false, true);

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.IMSPOS ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOS ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSLite ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSMED || 
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
            {
                this._addCheckBox(__row++, 0, _g.d.ic_inventory._pos_no_sum, false, true);
            }

            if (MyLib._myGlobal._OEMVersion.ToLower().Equals("imex"))
            {
                this._addTextBox(__row++, 0, 1, 0, _g.d.ic_inventory._medicine_register_number, 2, 0, 0, true, false, true);
                this._addTextBox(__row++, 0, 1, 0, _g.d.ic_inventory._medicine_standard_code, 2, 0, 0, true, false, true);

            }

            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
            {
                this._addTextBox(__row++, 0, 1, 0, _g.d.ic_inventory._quantity, 2, 0, 0, true, false, true);
                this._addTextBox(__row++, 0, 1, 0, _g.d.ic_inventory._degree, 2, 0, 0, true, false, true);
                this._addTextBox(__row++, 0, 6, 0, _g.d.ic_inventory._remark, 2, 0, 0, true, false, true);

            }
        }
    }
}
