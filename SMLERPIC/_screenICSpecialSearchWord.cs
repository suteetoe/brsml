using System;
using System.Collections.Generic;
using System.Text;

namespace SMLERPIC
{
    public class _screenICSpecialSearchWord : MyLib._myScreen
    {
        public _screenICSpecialSearchWord()
        {
            this._maxColumn = 1;
            this._table_name = _g.d.ic_specific_search_word._table;
            this._addTextBox(0, 0, 1, 1, _g.d.ic_specific_search_word._keyword, 1, 255, 0, true, false, false);
            this._addTextBox(1, 0, 1, 1, _g.d.ic_specific_search_word._engine_valve, 1, 255, 0, true, false, false);
            this._addTextBox(2, 0, 1, 1, _g.d.ic_specific_search_word._displacement, 1, 25, 0, true, false, false);

            this._addTextBox(3, 0, 3, 1, _g.d.ic_specific_search_word._market_names, 1, 255, 0, true, false, false);

            this._addTextBox(6, 0, 3, 1, _g.d.ic_specific_search_word._remark, 1, 255, 0, true, false, false);

            this._checkKeyDownReturn += _screenICSpecialSearchWord__checkKeyDownReturn;

            MyLib._myTextBox __text = (MyLib._myTextBox)this._getControl(_g.d.ic_specific_search_word._market_names);
            __text._enterToTab = false;
        }

        private bool _screenICSpecialSearchWord__checkKeyDownReturn(object sender, System.Windows.Forms.Keys keyData)
        {
            if (sender.GetType() == typeof(MyLib._myTextBox))
            {
                MyLib._myTextBox __text = (MyLib._myTextBox)sender;

                if (__text._name.Equals(_g.d.ic_specific_search_word._market_names))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
