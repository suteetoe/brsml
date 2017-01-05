using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPAPARControl
{
    public partial class _ap_ar_billForm : Form
    {
        _g.g._transTypeEnum _transType = _g.g._transTypeEnum.ว่าง;
        public _ap_ar_billForm(_g.g._transTypeEnum transType)
        {
            InitializeComponent();

            this._transType = transType;
            this.build();

        }

        void build()
        {
            string __formatNumberAmount = _g.g._getFormatNumberStr(3);

            switch (this._transType)
            {
                case _g.g._transTypeEnum.ลูกหนี้:
                    this._resultGrid._table_name = _g.d.ap_ar_resource._table;
                    this._resultGrid._isEdit = false;
                    this._resultGrid._addColumn(_g.d.ap_ar_resource._doc_date, 4, 10, 15);
                    this._resultGrid._addColumn(_g.d.ap_ar_resource._doc_no, 1, 10, 15);
                    this._resultGrid._addColumn(_g.d.ap_ar_resource._amount, 3, 10, 15, false, false, false, false, __formatNumberAmount);
                    this._resultGrid._addColumn(_g.d.ap_ar_resource._ref_doc_no, 1, 10, 15);
                    this._resultGrid._addColumn(_g.d.ap_ar_resource._due_date, 4, 10, 15);
                    this._resultGrid._addColumn(_g.d.ap_ar_resource._doc_type, 1, 10, 15, false);
                    this._resultGrid._addColumn(_g.d.ap_ar_resource._sale_name, 1, 10, 15);

                    this._resultGrid._total_show = true;
                    this._resultGrid._calcPersentWidthToScatter();

                    break;
                case _g.g._transTypeEnum.เจ้าหนี้:
                    break;
            }
        }

        /// <summary>
        /// Load Data 
        /// </summary>
        /// <param name="custCode">Customer Code</param>
        /// <param name="mode">Mode 0=All, 1=Ondue, 2=Overdue</param>
        public void _loadData(string custCode, int mode)
        {
            string __where = "";

            switch (mode)
            {
                case 1:
                    __where = " due_date >= now() ";
                    break;
                case 2:
                    __where = " due_date < now() ";
                    break;
            }
            StringBuilder __query = new StringBuilder();

            switch (this._transType)
            {
                case _g.g._transTypeEnum.ลูกหนี้:
                    __query.Append("select " + MyLib._myGlobal._fieldAndComma(_g.d.ap_ar_resource._doc_date,
                        _g.d.ap_ar_resource._doc_no,
                        _g.d.ap_ar_resource._amount,
                        _g.d.ap_ar_resource._ref_doc_no,
                        _g.d.ap_ar_resource._due_date,
                        "trans_flag(" + _g.d.ap_ar_resource._doc_type + ") as  " + _g.d.ap_ar_resource._doc_type,
                        "(select sale_code || \'~\' || (select name_1 from erp_user where erp_user.code = ic_trans.sale_code) from  ic_trans where ic_trans.doc_no = temp1.doc_no and ic_trans.trans_flag = temp1.doc_type ) as " + _g.d.ap_ar_resource._sale_name) +

                        " from sml_ar_balance_doc(null, null, \'" + custCode + "\') as temp1 " + ((__where.Length > 0) ? " where " + __where : ""));
                    break;
            }

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query.ToString());
            this._resultGrid._loadFromDataTable(__result.Tables[0]);

        }
    }
}
