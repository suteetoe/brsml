using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPARAPInfo
{
    public partial class _stat : UserControl
    {
        private _apArConditionEnum _mode;
        private _conditionScreen _conditionForm;

        public _stat(_apArConditionEnum mode)
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._mode = mode;
            this._conditionForm = new _conditionScreen(mode);
            this.Controls.Add(this._conditionForm);
            //
            string __formatNumberAmount = _g.g._getFormatNumberStr(3);
            this._resultGrid._table_name = _g.d.ap_ar_resource._table;
            this._resultGrid._addColumn(_g.d.ap_ar_resource._ar_code, 1, 10, 10);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._ar_name, 1, 10, 20);
            //
            this._resultGrid._addColumn(_g.d.ap_ar_resource._balance_first, 3, 10, 8, false, false, false, false, __formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._debit_1, 3, 10, 8, false, false, false, false, __formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._debit_2, 3, 10, 8, false, false, false, false, __formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._credit_1, 3, 10, 8, false, false, false, false, __formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._credit_2, 3, 10, 8, false, false, false, false, __formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._balance_end, 3, 10, 8, false, false, false, false, __formatNumberAmount);
            //
            this._resultGrid._setColumnBackground(_g.d.ap_ar_resource._balance_first, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ap_ar_resource._balance_end, Color.AliceBlue);
            //
            this._resultGrid._total_show = true;
            this._resultGrid._calcPersentWidthToScatter();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            string __custCodeBegin = "";
            string __custCodeEnd = "";
            switch (this._mode)
            {
                case _apArConditionEnum.ลูกหนี้_เคลื่อนไหว:
                case _apArConditionEnum.ลูกหนี้_สถานะลูกหนี้:
                case _apArConditionEnum.ลูกหนี้_อายุลูกหนี้:
                case _apArConditionEnum.ลูกหนี้_อายุลูกหนี้_แยก_เอกสาร:
                case _apArConditionEnum.ลูกหนี้_อายุลูกหนี้_แยกลูกหนี้_แยกเอกสาร:
                    __custCodeBegin = this._conditionForm._getDataStr(_g.d.ap_ar_resource._ar_code_begin);
                    __custCodeEnd= this._conditionForm._getDataStr(_g.d.ap_ar_resource._ar_code_end);
                    break;
                default:
                    __custCodeBegin = this._conditionForm._getDataStr(_g.d.ap_ar_resource._ap_code_begin);
                    __custCodeEnd= this._conditionForm._getDataStr(_g.d.ap_ar_resource._ap_code_end);
                    break;
            }
            DateTime __dateBegin = MyLib._myGlobal._convertDate(this._conditionForm._getDataStr(_g.d.ap_ar_resource._date_begin));
            DateTime __dateEnd = MyLib._myGlobal._convertDate(this._conditionForm._getDataStr(_g.d.ap_ar_resource._date_end));

            _process __process = new _process();
            this._resultGrid._loadFromDataTable(__process._arStat(this._mode, null,__custCodeBegin, __custCodeEnd, __dateBegin, __dateEnd));

        }
    }
}
