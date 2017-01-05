using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLTransportLabel
{
    public partial class _importTransportLabel : Form
    {
        int _mode = 0;

        public _importTransportLabel(int mode)
        {
            InitializeComponent();
            this._mode = mode;
        }

        private void _importButton_Click(object sender, EventArgs e)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            string __query = "";

            if (this._mode == 0)
            {
                __query = "insert into ap_ar_transport_label(cust_code, name_1, address, telephone, fax, email, ar_ap_type) select code, name_1, address, telephone, fax,email, 0 from ap_supplier where not exists (select cust_code from ap_ar_transport_label where ap_ar_transport_label.ar_ap_type = 0 and ap_ar_transport_label.cust_code = ap_supplier.code)";
            }
            else
            {
                __query = "insert into ap_ar_transport_label(cust_code, name_1, address, telephone, fax, email, ar_ap_type) select code, name_1, address, telephone, fax,email, 1 from ar_customer where not exists (select cust_code from ap_ar_transport_label where ap_ar_transport_label.ar_ap_type = 1 and ap_ar_transport_label.cust_code = ar_customer.code)";
            }

            if (__query.Length > 0)
            {
                string __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __query);

                if (__result.Length == 0)
                {
                    MessageBox.Show("นำเข้าข้อมูลเรียบร้อย", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("ผิดพลาด " + __result, "ผิดพลาด", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                }
            }
        }
    }
}
