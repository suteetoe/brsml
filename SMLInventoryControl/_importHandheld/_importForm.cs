using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl._importHandheld
{
    public partial class _importHandheldForm : Form
    {
        public _importHandheldForm()
        {
            InitializeComponent();
            //
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._dataList._lockRecord = false;
            this._dataList._loadViewFormat("screen_import_handheld", MyLib._myGlobal._userSearchScreenGroup, false);
            this._dataList._gridData._afterSelectRow += new MyLib.AfterSelectRowEventHandler(_gridData__afterSelectRow);
            //
            string __formatNumberQty = _g.g._getFormatNumberStr(1);
            this._itemGrid._isEdit = false;
            this._itemGrid._table_name = _g.d.ic_trans_detail._table;
            this._itemGrid._addColumn(_g.d.ic_trans_detail._barcode, 1, 20, 20);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._item_code, 1, 20, 20);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._item_name, 1, 40, 40);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._unit_code, 1, 10, 10);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._qty, 3, 10, 10, true, false, true, false, __formatNumberQty);

            // toe for imex
            if (MyLib._myGlobal._OEMVersion.Equals("ims") || MyLib._myGlobal._OEMVersion.Equals("imex"))
            {
                this._itemGrid._addColumn(_g.d.barcode_import_list_detail._lot_number, 1, 10, 10);
                this._itemGrid._addColumn(_g.d.barcode_import_list_detail._expire_date, 1, 10, 10);
                this._itemGrid._addColumn(_g.d.barcode_import_list_detail._mfd_date, 1, 10, 10);
                this._itemGrid._addColumn(_g.d.barcode_import_list_detail._mfn_name, 1, 10, 10);
            }
        }


        void _gridData__afterSelectRow(object sender, int row)
        {
            string __docNo = this._dataList._gridData._cellGet(row, _g.d.barcode_import_list._table + "." + _g.d.barcode_import_list._doc_no).ToString();
            this._itemGrid._clear();
            if (__docNo.Length > 0)
            {
                try
                {
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    this._itemGrid._loadFromDataTable(__myFrameWork._queryShort("select *, (select name_1 from ic_inventory where ic_inventory.code=" + _g.d.barcode_import_list_detail._item_code + ") as " + _g.d.ic_trans_detail._item_name + " from " + _g.d.barcode_import_list_detail._table + " where " + _g.d.barcode_import_list_detail._doc_no + "=\'" + __docNo + "\' order by roworder").Tables[0]);
                    this._sendButton.Text = "ส่งเอกสารเลขที่" + " : " + __docNo + " " + "เข้าสู่รายการ";
                    this._sendButton.Invalidate();
                }
                catch
                {
                }
            }
        }
    }
}
