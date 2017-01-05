using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPAPARControl
{
    public partial class _advanceControl : UserControl
    {
        public _g.g._transControlTypeEnum _icTransControlType = _g.g._transControlTypeEnum.ว่าง;

        public _advanceControl()
        {
            InitializeComponent();

        }

        public string _queryLoad(string docNo)
        {
            StringBuilder __query = new StringBuilder();
            string __transFlag = _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString();
            string __transType = _g.g._transTypeGlobal._transType(this._icTransControlType).ToString();
            string __queryDetail = "select *{0} from " + _g.d.cb_trans_detail._table + " where " + _g.d.cb_trans_detail._doc_no + "=\'" + docNo + "\' and " + _g.d.cb_trans_detail._trans_flag + "=" + __transFlag.ToString() + " and " + _g.d.cb_trans_detail._trans_type + "=" + __transType + " and " + _g.d.cb_trans_detail._doc_type + "={1}";
            // เงินมัดจำ
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(string.Format(__queryDetail, "", "6")));
            return __query.ToString();
        }

        public string _queryInsert(string custCode, string docNo, string docDate, string docTime)
        {
            StringBuilder __query = new StringBuilder();
            string __transFlag = _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString();
            string __transType = _g.g._transTypeGlobal._transType(this._icTransControlType).ToString();
            string __payType = _g.g._transTypeGlobal._payType(this._icTransControlType).ToString();
            // รายละเอียด
            string __fieldList = _g.d.cb_trans_detail._doc_no + "," + _g.d.cb_trans_detail._doc_date + "," + _g.d.cb_trans_detail._doc_time + "," + _g.d.cb_trans_detail._trans_type + "," + _g.d.cb_trans_detail._trans_flag + ",";
            string __dataList = "\'" + docNo + "\'," + docDate + ",\'" + docTime + "\'," + __transType + "," + __transFlag + ",";
            int __ap_ar_type = 0;
            switch (_g.g._transType(this._icTransControlType))
            {
                case _g.g._transTypeEnum.ขาย_ลูกหนี้:
                case _g.g._transTypeEnum.ลูกหนี้:
                    __ap_ar_type = 1;
                    break;
                default:
                    __ap_ar_type = 2;
                    break;
            }
            this._dataGrid._updateRowIsChangeAll(true);
            __query.Append(this._dataGrid._createQueryForInsert(_g.d.cb_trans_detail._table,
                __fieldList + _g.d.cb_trans_detail._doc_type + "," + _g.d.cb_trans_detail._ap_ar_type + "," + _g.d.cb_trans_detail._ap_ar_code + ",",
                __dataList + "6," +  __ap_ar_type + ",\'" + custCode + "\',"));
            // ลบค่าว่าง
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.cb_trans_detail._table + " where " + _g.d.cb_trans_detail._trans_number + " is null or " + _g.d.cb_trans_detail._trans_number + "=\'\'"));
            //
            return __query.ToString();
        }

        public string _queryDelete(string docNo)
        {
            if (this._icTransControlType == _g.g._transControlTypeEnum.ว่าง)
            {
                MessageBox.Show("Error : _icTransControlType");
            }
            StringBuilder __query = new StringBuilder();
            string __transFlag = _g.g._transFlagGlobal._transFlag(this._icTransControlType).ToString();
            string __transType = _g.g._transTypeGlobal._transType(this._icTransControlType).ToString();
            // เงินมัดจำ
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.cb_trans_detail._table + " where " + _g.d.cb_trans_detail._doc_no + "=\'" + docNo + "\' and " + _g.d.cb_trans_detail._trans_flag + "=" + __transFlag.ToString() + " and " + _g.d.cb_trans_detail._trans_type + "=" + __transType + " and " + _g.d.cb_trans_detail._doc_type + "=6"));
            return __query.ToString();
        }
    }
}
