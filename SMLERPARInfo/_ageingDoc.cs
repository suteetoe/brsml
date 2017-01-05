using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPARAPInfo
{
    public partial class _ageingDoc : UserControl
    {
        private string _formatNumberAmount = _g.g._getFormatNumberStr(3);
        private _apArConditionEnum _mode;
        private _conditionScreen _conditionForm;

        public _ageingDoc(_apArConditionEnum mode)
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._mode = mode;
            this._conditionForm = new _conditionScreen(mode);
            this.Controls.Add(this._conditionForm);
            //
            this._resultGrid._table_name = _g.d.ap_ar_resource._table;
            this._resultGrid._addColumn(_g.d.ap_ar_resource._doc_no, 1, 10, 8);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._doc_date, 1, 10, 10);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._doc_type, 1, 10, 8);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._due_date, 1, 10, 10);
            //
            this._resultGrid._addColumn(_g.d.ap_ar_resource._amount, 3, 10, 8, false, false, false, false, _formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._ar_balance, 3, 10, 8, false, false, false, false, _formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._out_due, 3, 10, 8, false, false, false, false, _formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._term_0, 3, 10, 8, false, false, false, false, _formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._term_1, 3, 10, 8, false, false, false, false, _formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._term_2, 3, 10, 8, false, false, false, false, _formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._term_3, 3, 10, 8, false, false, false, false, _formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._term_4, 3, 10, 8, false, false, false, false, _formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._term_5, 3, 10, 8, false, false, false, false, _formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._due_day, 3, 10, 8, false, false, false, false, _g.g._getFormatNumberStr(0, 0));
            //
            this._resultGrid._setColumnBackground(_g.d.ap_ar_resource._amount, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ap_ar_resource._ar_balance, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ap_ar_resource._out_due, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ap_ar_resource._term_0, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ap_ar_resource._term_1, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ap_ar_resource._term_2, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ap_ar_resource._term_3, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ap_ar_resource._term_4, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ap_ar_resource._term_5, Color.AliceBlue);
            //
            this._resultGrid._total_show = true;
            this._resultGrid._calcPersentWidthToScatter();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private string _getColumnName(string fieldName)
        {
            string __resourceFieldName = _g.d.ap_ar_resource._table + "." + fieldName;
            MyLib._myResourceType __getResource = MyLib._myResource._findResource(__resourceFieldName, __resourceFieldName);
            return __getResource._str;
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            //string __arDocNo = this._conditionScreen._getDataStr(_g.d.ap_ar_resource._doc_no);
            //string __arDocDate = this._conditionScreen._getDataStr(_g.d.ap_ar_resource._doc_date);

            string __custCodeBegin = "";
            switch (this._mode)
            {
                case _apArConditionEnum.ลูกหนี้_เคลื่อนไหว:
                case _apArConditionEnum.ลูกหนี้_สถานะลูกหนี้:
                case _apArConditionEnum.ลูกหนี้_อายุลูกหนี้:
                case _apArConditionEnum.ลูกหนี้_อายุลูกหนี้_แยก_เอกสาร:
                case _apArConditionEnum.ลูกหนี้_อายุลูกหนี้_แยกลูกหนี้_แยกเอกสาร:
                    __custCodeBegin = this._conditionForm._getDataStr(_g.d.ap_ar_resource._ar_code_begin);
                    break;
                default:
                    __custCodeBegin = this._conditionForm._getDataStr(_g.d.ap_ar_resource._ap_code_begin);
                    break;
            }

            DateTime __dateEnd = MyLib._myGlobal._convertDate(this._conditionForm._getDataStr(_g.d.ap_ar_resource._date_end));
            int __term_1_begin = (int)this._conditionForm._getDataNumber(_g.d.ap_ar_resource._term_1_begin);
            int __term_1_end = (int)this._conditionForm._getDataNumber(_g.d.ap_ar_resource._term_1_end);
            int __term_2_begin = (int)this._conditionForm._getDataNumber(_g.d.ap_ar_resource._term_2_begin);
            int __term_2_end = (int)this._conditionForm._getDataNumber(_g.d.ap_ar_resource._term_2_end);
            int __term_3_begin = (int)this._conditionForm._getDataNumber(_g.d.ap_ar_resource._term_3_begin);
            int __term_3_end = (int)this._conditionForm._getDataNumber(_g.d.ap_ar_resource._term_3_end);
            int __term_4_begin = (int)this._conditionForm._getDataNumber(_g.d.ap_ar_resource._term_4_begin);
            int __term_4_end = (int)this._conditionForm._getDataNumber(_g.d.ap_ar_resource._term_4_end);
            //
            string __word2 = _getColumnName(_g.d.ap_ar_resource._day_due);
            string __format = "{0}-{1} {2}";
            this._resultGrid._columnNameChange(_g.d.ap_ar_resource._term_1, string.Format(__format, __term_1_begin, __term_1_end, __word2));
            this._resultGrid._columnNameChange(_g.d.ap_ar_resource._term_2, string.Format(__format, __term_2_begin, __term_2_end, __word2));
            this._resultGrid._columnNameChange(_g.d.ap_ar_resource._term_3, string.Format(__format, __term_3_begin, __term_3_end, __word2));
            this._resultGrid._columnNameChange(_g.d.ap_ar_resource._term_4, string.Format(__format, __term_4_begin, __term_4_end, __word2));
            this._resultGrid._columnNameChange(_g.d.ap_ar_resource._term_5, string.Format("{0} {1}", _getColumnName(_g.d.ap_ar_resource._term_6), __term_4_end));
            this._resultGrid.Invalidate();
            //
            _process __process = new _process();
            int __dueDateSelect = MyLib._myGlobal._intPhase(this._conditionForm._getDataStr(_g.d.ap_ar_resource._due_date_select));
            this._resultGrid._loadFromDataTable(__process._arAgeingDoc(this._mode, __dueDateSelect, __custCodeBegin, "", __term_1_begin, __term_1_end, __term_2_begin, __term_2_end, __term_3_begin, __term_3_end, __term_4_begin, __term_4_end, __dateEnd, ""));
        }
    }
}
