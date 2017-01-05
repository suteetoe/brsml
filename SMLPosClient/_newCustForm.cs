using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLPosClient
{
    public partial class _newCustForm : Form
    {
        SMLERPControl._customer._screen_ar_main _cust = new SMLERPControl._customer._screen_ar_main();
        SMLERPControl._customer._screen_ar_main _custTax = new SMLERPControl._customer._screen_ar_main();
        public event insertlSuccessEventHandler _insertSuccess;

        /// <summary>0=เพิ่ม, 1=แก้ไข</summary>
        private int _mode = 0;
        string _custCode = "";

        public _newCustForm(int mode, string custcode)
        {
            InitializeComponent();

            this._mode = mode;
            this._custCode = custcode;
            this._cust._controlName = SMLERPControl._customer._controlTypeEnum.Ar;
            this._custTax._controlName = SMLERPControl._customer._controlTypeEnum.ArTaxOnly;

            if (this._mode == 1)
            {
                this.Text = "แก้ไขลูกค้า";

                // load ข้อมูลลูกค้า
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable _dataCust = __myFrameWork._queryShort("select * from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + " = \'" + this._custCode + "\'").Tables[0];
                this._cust._loadData(_dataCust);

                this._cust._getControl(_g.d.ar_customer._code).Enabled = false;
            }

            this._cust.Dock = DockStyle.Top;
            this._custTax.Dock = DockStyle.Top;

            this._panel.Controls.Add(this._custTax);
            this._panel.Controls.Add(this._cust);

            this._cust._textBoxSaved += _cust__textBoxSaved;

        }

        private void _cust__textBoxSaved(object sender, string name)
        {
            this._custTax._setDataStr(_g.d.ar_customer_detail._tax_id, this._cust._getDataStr(_g.d.ar_customer_detail._tax_id));
            this._custTax._setDataStr(_g.d.ar_customer_detail._card_id, this._cust._getDataStr(_g.d.ar_customer_detail._card_id));
            this._custTax._setComboBox(_g.d.ar_customer_detail._branch_type, MyLib._myGlobal._intPhase(this._cust._getDataNumber(_g.d.ar_customer_detail._branch_type).ToString()));
            this._custTax._setDataStr(_g.d.ar_customer_detail._branch_code, this._cust._getDataStr(_g.d.ar_customer_detail._branch_code));
        }

        private void _buttonCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            // ปรับข้อมูล

            this._custTax._setDataStr(_g.d.ar_customer_detail._tax_id, this._cust._getDataStr(_g.d.ar_customer_detail._tax_id));
            this._custTax._setDataStr(_g.d.ar_customer_detail._card_id, this._cust._getDataStr(_g.d.ar_customer_detail._card_id));
            this._custTax._setComboBox(_g.d.ar_customer_detail._tax_id, MyLib._myGlobal._intPhase(this._cust._getDataNumber(_g.d.ar_customer_detail._tax_id).ToString()));
            this._custTax._setDataStr(_g.d.ar_customer_detail._branch_code, this._cust._getDataStr(_g.d.ar_customer_detail._branch_code));

            ArrayList __getData = this._cust._createQueryForDatabase();
            ArrayList __getDetail = this._custTax._createQueryForDatabase();

            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");

            if (this._mode == 0)
            {
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ar_customer._table + " (" + __getData[0].ToString() + ") values (" + __getData[1].ToString() + ")"));
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ar_customer_detail._table + " (" + __getDetail[0].ToString() + "," + _g.d.ar_customer_detail._ar_code + ") values (" + __getDetail[1].ToString() + "," + this._cust._getDataStrQuery(_g.d.ar_customer._code) + ")"));

            }
            else
            {
                //__myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ar_customer._table + " (" + __getData[0].ToString() + ") values (" + __getData[1].ToString() + ")"));
                // update  cust
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ar_customer._table + " set " + __getData[2].ToString() + " where " + _g.d.ar_customer._code + "=" + this._cust._getDataStrQuery(_g.d.ar_customer._code)));
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ar_customer_detail._table + " set " + __getDetail[2].ToString() + " where " + _g.d.ar_customer_detail._ar_code + "=" + this._cust._getDataStrQuery(_g.d.ar_customer._code)));
            }

            __myQuery.Append("</node>");
            string result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (result.Length != 0)
            {
                MessageBox.Show(result);
            }
            else
            {
                this._insertSuccess(this._cust._getDataStr(_g.d.ar_customer._code));
            }
        }
        public delegate void insertlSuccessEventHandler(string code);
    }
}
