using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace SMLERPIC
{
    public class _utility
    {
        /// <summary>
        /// ตรวจสอบ Barcode ซ้ำกัน
        /// </summary>
        public void _barcodeDuplicate()
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __dt = __myFrameWork._queryShort("select * from ( select " + _g.d.ic_inventory_barcode._barcode + ",count(*) as xcount from " + _g.d.ic_inventory_barcode._table + " group by " + _g.d.ic_inventory_barcode._barcode + ") as temp1 where  xcount > 1").Tables[0];
            if (__dt.Rows.Count > 0)
            {
                StringBuilder __result = new StringBuilder();
                for (int __row = 0; __row < __dt.Rows.Count; __row++)
                {
                    if (__result.Length > 0)
                    {
                        __result.Append(",");
                    }
                    __result.Append(__dt.Rows[__row][_g.d.ic_inventory_barcode._barcode].ToString());
                }
                MessageBox.Show("Barcode ซ้ำ : " + __result.ToString());
            }
            __dt.Dispose();
            __dt = null;
        }
    }
}
