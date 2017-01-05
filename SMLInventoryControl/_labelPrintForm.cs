using SMLTransportLabel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _labelPrintForm : Form
    {
        int _mode = 0;
        string _oldDocNo = "";

        public _labelPrintForm(int mode)
        {
            InitializeComponent();
            _build();
        }

        public void _load(string docNo, int transFlag)
        {
            /*
                         this._addColumn(_g.d.ap_ar_transport_label._cust_code, 1, 15, 15, false, false, true);
            this._addColumn(_g.d.ap_ar_transport_label._name_1, 1, 0, 20, true, true);
            this._addColumn(_g.d.ap_ar_transport_label._receivename, 1, 0, 20, false, false, true);
            this._addColumn(_g.d.ap_ar_transport_label._address, 1, 255, 30, false, false, true);
            this._addColumn(_g.d.ap_ar_transport_label._telephone, 1, 15, 20, false, false, true);
            this._addColumn(_g.d.ap_ar_transport_label._label_count, 2, 0, 15);


            this._addColumn(_g.d.ap_ar_transport_label._fax, 1, 15, 0, false, true, true);

             */

            string __query = "select doc_no" +
                ", item_code as invoice_no " +
                ", item_name as " + _g.d.ap_ar_transport_label._cust_code +
                ", ( select name_1 from ar_customer where ar_customer.code = ic_trans_detail.item_name) as " + _g.d.ap_ar_transport_label._name_1 +
                ", ( select name_1 from ar_customer where ar_customer.code = ic_trans_detail.item_name) as " + _g.d.ap_ar_transport_label._receivename +
                ", ( select address from ar_customer where ar_customer.code = ic_trans_detail.item_name) as " + _g.d.ap_ar_transport_label._address +
                ", ( select telephone from ar_customer where ar_customer.code = ic_trans_detail.item_name) as " + _g.d.ap_ar_transport_label._telephone +
                ", 1 as label_count " +
                " from ic_trans_detail where doc_no = '" + docNo + "' and trans_flag = " + transFlag + " order by line_number";
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __result = __myFrameWork._queryShort(__query).Tables[0];
            this._transport_labe_grid._loadFromDataTable(__result);



        }

        void _build()
        {
            this._transport_labe_grid._addColumn("invoice_no", 1, 15, 0, false, true, true);

            // set printer 
            int __default = 0;
            int __count = 0;
            //foreach (ManagementObject __getPrinter in __printerList.Get())
            foreach (MyLib._printerListClass __getPrinter in MyLib._myGlobal._printerList)
            {
                string __printerName = __getPrinter._printerName;
                if (__getPrinter._isDefault)
                {
                    __default = __count;
                }

                _printerCombobox.Items.Add(__printerName);
                __count++;
            }

            _printerCombobox.SelectedIndex = __default;

            // set form
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.formdesign._formname + ", " + _g.d.formdesign._formcode + " from " + _g.d.formdesign._table + " where coalesce(" + _g.d.formdesign._form_type + ", 0) in (0,3) "));

            //string __custName = (this._mode.Equals(0)) ? " (select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + " = " + _g.d.ap_ar_transport_label._table + "." + _g.d.ap_ar_transport_label._cust_code + ") as " + _g.d.ap_ar_transport_label._receivename :
            //    " (select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ap_ar_transport_label._table + "." + _g.d.ap_ar_transport_label._cust_code + ") as " + _g.d.ap_ar_transport_label._receivename;

            //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("select *, " + __custName + " from " + _g.d.ap_ar_transport_label._table + " where roworder = " + this._addrId + ""));
            // group list
            __query.Append("</node>");

            ArrayList __queryResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

            if (__queryResult.Count > 0)
            {

                DataSet __result = (DataSet)__queryResult[0]; //__myFrameWork._queryShort("select " + _g.d.formdesign._formname + ", " + _g.d.formdesign._formcode +" from " + _g.d.formdesign._table + "");
                //if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
                //{
                //    //for (int __i = 0; __i < __result.Tables[0].Rows.Count; __i++)
                //    //{
                //    //    _formObject __obj = new _formObject();
                //    //    __obj.Name = __result.Tables[0].Rows[__i][_g.d.formdesign._formcode].ToString();
                //    //    __obj.Value = __result.Tables[0].Rows[__i][_g.d.formdesign._formname].ToString();

                //    //    this._formComboBox.Items.Add(__obj);
                //    //}

                //}

                this._formCombobox.DataSource = __result.Tables[0];
                this._formCombobox.DisplayMember = _g.d.formdesign._formname;
                this._formCombobox.ValueMember = _g.d.formdesign._formcode;
                // 

            }
        }

        void _groupList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void _previewButton_Click(object sender, EventArgs e)
        {
            // pack and print
            _print(true);

        }

        private void _printButton_Click(object sender, EventArgs e)
        {
            // pack and print
            _print(false);
        }

        void _print(bool isPreview)
        {
            string __formCode = (this._formCombobox.SelectedValue != null) ? this._formCombobox.SelectedValue.ToString() : "";


            if (__formCode.Length > 0)
            {
                string __printerName = this._printerCombobox.SelectedItem.ToString();

                List<_transportLabelObj> __objList = new List<_transportLabelObj>();
                for (int __i = 0; __i < this._transport_labe_grid._rowData.Count; __i++)
                {
                    int __countLabel = (int)this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._label_count);
                    for (int __count = 0; __count < __countLabel; __count++)
                    {                        
                        _transportLabelObj __label = new _transportLabelObj();
                        __label._custCode = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._cust_code).ToString();
                        __label._custName = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._receivename).ToString();
                        __label._receiveName = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._name_1).ToString();
                        __label._address = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._address).ToString();
                        __label._telephone = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._telephone).ToString();
                        __label._fax = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._fax).ToString();
                        __label._email = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._email).ToString();
                        __label._invoiceNo = this._transport_labe_grid._cellGet(__i, "invoice_no").ToString();
                        __label._total = __countLabel;
                        __label._page = __count + 1;
                        __objList.Add(__label);
                    }
                }

                if (__objList.Count > 0)
                {
                    if (this._printModeCheckbox.Checked == true)
                    {
                        _labelPrintClass __printClass = new _labelPrintClass();
                        __printClass._printLabel(isPreview, __formCode, __printerName, MyLib._myGlobal._intPhase(this._startRowTextbox.Text), MyLib._myGlobal._intPhase(this._startColumnTextbox.Text), __objList);
                    }
                    else
                    {
                        _labelPrintClass __printClass = new _labelPrintClass();
                        __printClass._print(isPreview, __formCode, __printerName, __objList);
                    }
                }

            }
        }

    }
}
