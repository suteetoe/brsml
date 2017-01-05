using System;
using System.Collections.Generic;
using System.Text;

namespace SMLPOSControl._designer
{
    class _posSaveScreen : MyLib._myDialogForm
    {
        public _posSaveScreen()
        {
            this._dialogScreen._maxColumn = 1;
            this._buttonOk.ButtonText = "Save";
            this._dialogScreen._addTextBox(0, 0, 1, 0, _g.d.sml_posdesign._screen_code, 1, 1, 0, true, false, false, false, true, "รหัสจอขาย");
            this._dialogScreen._addTextBox(1, 0, 1, 0, _g.d.sml_posdesign._screen_name, 1, 1, 0, true, false, false, false, true, "ชื่อจอขาย");
            this._dialogScreen.Invalidate();
        }
    }
}
