using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _searchItemRefRow : Form
    {
        public int _selectLineNumber = -1;
        public _searchItemRefRow()
        {
            InitializeComponent();

            string __formatNumberQty = _g.g._getFormatNumberStr(1);
            string __formatNumberPrice = _g.g._getFormatNumberStr(2);
            string __formatNumberAmount = _g.g._getFormatNumberStr(3);

            this._gridDetail._table_name = _g.d.ic_trans_detail._table;
            this._gridDetail.WidthByPersent = true;
            this._gridDetail._isEdit = false;
            this._gridDetail._addColumn(_g.d.ic_trans_detail._item_code, 1, 10, 10);
            this._gridDetail._addColumn(_g.d.ic_trans_detail._item_name, 1, 30, 30);
            this._gridDetail._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, false);
            this._gridDetail._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, false);
            this._gridDetail._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
            this._gridDetail._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 10, true, false, true, false, __formatNumberQty);
            this._gridDetail._addColumn(_g.d.ic_trans_detail._price, 3, 1, 10, _g.g._companyProfile._column_price_enable, false, true, false, __formatNumberPrice);
            this._gridDetail._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 15, ((MyLib._myGlobal._programName.Equals("SML CM")) ? false : true), false, true, false);
            this._gridDetail._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 15, false, false, true, false, __formatNumberAmount);
            this._gridDetail._addColumn(_g.d.ic_trans_detail._line_number, 2, 1, 5, false, true, true);
            this._gridDetail._calcPersentWidthToScatter();

            this._gridDetail._mouseDoubleClick += _gridDetail__mouseDoubleClick;
        }

        private void _gridDetail__mouseDoubleClick(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._row > -1)
            {
                this._selectLineNumber = MyLib._myGlobal._intPhase(this._gridDetail._cellGet(e._row, _g.d.ic_trans_detail._line_number).ToString());
                this.Close();
            }
        }

        public void _load(string docNo, string itemCode)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __result = __myFrameWork._queryShort("select line_number, item_code, item_name, (unit_code || '~' || (select name_1 from ic_unit where ic_unit.code = ic_trans_detail.unit_code)) as unit_code, wh_code, shelf_code, qty, price,discount,sum_amount from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + docNo + "\' and " + _g.d.ic_trans_detail._item_code + "= \'" + itemCode + "\'").Tables[0];
            this._gridDetail._loadFromDataTable(__result);
        }
    }
}
