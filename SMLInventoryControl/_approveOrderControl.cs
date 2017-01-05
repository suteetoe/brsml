using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _approveOrderControl : UserControl
    {
        public _approveOrderControl()
        {
            InitializeComponent();

            this._dataList._buttonNew.Visible =
                this._dataList._buttonNewFromTemp.Visible =
                this._dataList._buttonDelete.Visible =
                this._dataList._buttonLockDoc.Visible = false;

            this._dataList._lockRecord = false;
            this._dataList._loadViewFormat("screen_so_approve", MyLib._myGlobal._userSearchScreenGroup, false);
            this._dataList._gridData._afterSelectRow += new MyLib.AfterSelectRowEventHandler(_gridData__afterSelectRow);

            string __formatNumberQty = _g.g._getFormatNumberStr(1);
            this._itemGrid._isEdit = false;
            this._itemGrid._total_show = true;
            this._itemGrid.WidthByPersent = true;
            this._itemGrid._table_name = _g.d.ic_trans_detail_draft._table;
            this._itemGrid._addColumn(_g.d.ic_trans_detail_draft._item_code, 1, 20, 20);
            this._itemGrid._addColumn(_g.d.ic_trans_detail_draft._item_name, 1, 40, 40);
            this._itemGrid._addColumn(_g.d.ic_trans_detail_draft._unit_code, 1, 10, 10);
            this._itemGrid._addColumn(_g.d.ic_trans_detail_draft._qty, 3, 10, 10, true, false, true, false, __formatNumberQty);
            this._itemGrid._addColumn(_g.d.ic_trans_detail_draft._price, 3, 10, 10, true, false, true, false, __formatNumberQty);
            this._itemGrid._addColumn(_g.d.ic_trans_detail_draft._discount_amount, 3, 10, 10, true, false, true, false, __formatNumberQty);
            this._itemGrid._addColumn(_g.d.ic_trans_detail_draft._sum_amount, 3, 10, 10, true, false, true, false, __formatNumberQty);

            this._itemGrid._calcPersentWidthToScatter();

        }

        void _gridData__afterSelectRow(object sender, int row)
        {
            string __docNo = this._dataList._gridData._cellGet(row, _g.d.ic_trans_draft._table + "." + _g.d.ic_trans_draft._doc_no).ToString();
            this._processButton.Text = "เรียกรายการขออนุมัติเอกสาร" + " : " + __docNo + " " + "";
            this._processButton.Enabled = true;
            this._processButton.Invalidate();
            this._itemGrid._clear();
            if (__docNo.Length > 0)
            {
                try
                {
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    this._itemGrid._loadFromDataTable(__myFrameWork._queryShort("select * " +
                        //", (select name_1 from ic_inventory where ic_inventory.code=" + _g.d.barcode_import_list_detail._item_code + ") as " + _g.d.ic_trans_detail._item_name +
                        " from " + _g.d.ic_trans_detail_draft._table + " where " + _g.d.ic_trans_detail_draft._doc_no + "=\'" + __docNo + "\' order by roworder").Tables[0]);
                    // this._sendButton.Text = "ส่งเอกสารเลขที่" + " : " + __docNo + " " + "เข้าสู่รายการ";
                    //this._sendButton.Invalidate();
                }
                catch
                {
                }
            }
        }
    }
}
