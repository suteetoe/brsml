using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.Data;

namespace AkzoGenPo
{
    public class _podesign : SMLReport._formReport._formDesigner
    {
        string _poFormCode = "PO";
        string _poFormName = "Purchase Order Form";
        string __localPathName = MyLib._myGlobal._smlConfigFile + AkzoGlobal._global._poFormFileName;

        public _podesign()
        {
            this._buttoSaveAs.Visible = false;
            this._formManagerSeparator1.Visible = false;
            this._formManagerSeparator2.Visible = false;
            this._buttonDeleteForm.Visible = false;
            this._buttonLoadLocalDrive.Visible = false;
            this._buttonSaveLocalDrive.Visible = false;
            this._loadStandradForm.Visible = false;
            this._queryButton.Visible = false;

        }

        protected override void _onClickSaveFromDesign()
        {
            //MessageBox.Show("save form");
            try
            {
                SMLReport._formReport.SMLFormDesignXml __source = _writeXMLSource();
                XmlSerializer __colXs = new XmlSerializer(typeof(SMLReport._formReport.SMLFormDesignXml));
                TextWriter __memoryStream = new StreamWriter(__localPathName, false, Encoding.UTF8);
                __colXs.Serialize(__memoryStream, __source);
                __memoryStream.Close();

                MessageBox.Show("Success ", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void _onClickOpenFormDesign()
        {
            //MessageBox.Show("open form");
            SMLReport._formReport.SMLFormDesignXml __source = null;

            // load จากเครื่อง ดูก่อน 
            // ถ้าไม่มีไปเอาจาก resource และ save ลงที่ local
            //if (__source == null)
            //{
            //    __source = (SMLReport._formReport.SMLFormDesignXml)MyLib._myGlobal._objectFromXml(global::AkzoGenPo.Properties.Resources.po_form, typeof(SMLReport._formReport.SMLFormDesignXml));
            //}                       

            if (System.IO.File.Exists(__localPathName) == false)
            {
                // write to local drive foredit
                __source = (SMLReport._formReport.SMLFormDesignXml)MyLib._myGlobal._objectFromXml(global::AkzoGenPo.Properties.Resources.po_form, typeof(SMLReport._formReport.SMLFormDesignXml));

                XmlSerializer __colXs = new XmlSerializer(typeof(SMLReport._formReport.SMLFormDesignXml));
                TextWriter __memoryStream = new StreamWriter(__localPathName, false, Encoding.UTF8);
                __colXs.Serialize(__memoryStream, __source);
                __memoryStream.Close();
            }

            if (__source == null)
            {
                try
                {
                    TextReader readFile = new StreamReader(__localPathName);
                    XmlSerializer __xsLoad = new XmlSerializer(typeof(SMLReport._formReport.SMLFormDesignXml));
                    __source = (SMLReport._formReport.SMLFormDesignXml)__xsLoad.Deserialize(readFile);
                    readFile.Close();
                }
                catch
                {

                }
            }


            if (__source != null)
            {
                this._loadForm(__source, null, SMLReport._formReport._openFormMethod.OpenFormLocal);
            }

        }

        public void _buildForm()
        {
            this._onClickOpenFormDesign();
        }

        protected override void _onClickQueryButton()
        {
            MessageBox.Show("Query Button");
        }

        protected override void _onClickPreviewButton()
        {
            //MessageBox.Show("Preview Button");
            // start searh printer
            _previewDocDialog __dialog = new _previewDocDialog();
            __dialog.Text = "ค้นหาเครื่องพิมพ์";
            __dialog._beforeClose += (s1, e1) =>
            {
                _previewDocDialog _search = (_previewDocDialog)s1;
                if (_search.DialogResult == DialogResult.Yes)
                {
                    //MyLib._myComboBox __printerSelectCombo = (MyLib._myComboBox)_search._dialogScreen._getControl(doc_no);
                    string __docNo = _search._dialogScreen._getDataStr("doc_no");
                    this._previewPrint(__docNo);
                    //this._setDataStr(_g.d.order_device_id._printer_name, __printerSelect);
                }
            };
            __dialog.ShowDialog();
        }

        void _previewPrint(string __docNo)
        {
            SMLReport._formReport._formPrint __print = new SMLReport._formReport._formPrint();
            __print.formDesign = this;
            __print._printRangeType = System.Drawing.Printing.PrintRange.AllPages;
            __print._includeDocSeries = true;
            //__print._query();

            // ส่ง datatable แทน
            DataSet __ds = new DataSet();

            SqlConnection __conn = AkzoGlobal._global._sqlConnection;
            __conn.Open();
            string __myQueryTop = @"select doc_no, doc_date_time,amount_before_discount,discount , amount ,memo,agent_code, ap_code,vat_rate, vat_amount, total_amount,
                ap_name 
,address_1
,(( case when tambon = '' then '' else 'ตำบล ' + tambon + ' ' end) + ( case when amper = '' then '' else 'อำเภอ ' + amper + ' ' end) + ( case when province = '' then '' else 'จังหวัด ' + province + ' ' end)) as address_2
,(( case when tambon = '' then '' else 'ตำบล/แขวง ' + tambon + ' ' end) + ( case when amper = '' then '' else 'อำเภอ/เขต ' + amper + ' ' end) + ( case when province = '' then '' else 'จังหวัด ' + province + ' ' end)) as address_2_1
,(( case when tambon = '' then '' else 'แขวง ' + tambon + ' ' end) + ( case when amper = '' then '' else 'เขต ' + amper + ' ' end) + ( case when province = '' then '' else 'จังหวัด ' + province + ' ' end)) as address_2_2
,(( case when tambon = '' then '' else 'ต.' + tambon + ' ' end) + ( case when amper = '' then '' else 'อ.' + amper + ' ' end) + ( case when province = '' then '' else 'จ.' + province + ' ' end)) as address_short_2
,(( case when tambon = '' then '' else 'ตำบล ' + tambon + ' ' end) + ( case when amper = '' then '' else 'อำเภอ ' + amper + ' ' end) + ( case when province = '' then '' else 'จังหวัด ' + province + ' ' end) + zip_code) as address_zipcode_2
,(( case when tambon = '' then '' else 'ตำบล/แขวง ' + tambon + ' ' end) + ( case when amper = '' then '' else 'อำเภอ/เขต ' + amper + ' ' end) + ( case when province = '' then '' else 'จังหวัด ' + province + ' ' end) + zip_code) as address_zipcode_2_1
,(( case when tambon = '' then '' else 'แขวง ' + tambon + ' ' end) + ( case when amper = '' then '' else 'เขต ' + amper + ' ' end) + ( case when province = '' then '' else 'จังหวัด ' + province + ' ' end) + zip_code) as address_zipcode_2_2
,(( case when tambon = '' then '' else 'ต.' + tambon + ' ' end) + ( case when amper = '' then '' else 'อ.' + amper + ' ' end) + ( case when province = '' then '' else 'จ.' + province + ' ' end) + zip_code) as address_zipcode_short_2
, (( case when telephone = '' then '' else 'โทร.' + telephone + ' ' end) + ( case when fax = '' then '' else 'แฟกซ์.' + fax + ' ' end)) as tel_fax_short_1
, (( case when telephone = '' then '' else 'tel.' + telephone + ' ' end) + ( case when fax = '' then '' else 'fax.' + fax + ' ' end)) as tel_fax_short_2
,tambon
,amper
,province
,zip_code
,telephone
,fax
,email 
from ("
                + @"select doc_no ,doc_date_time, amount_before_discount,discount , amount ,memo,agent_code,
vat_rate, 
vat_amount, 
total_amount ,
(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code) as ap_code,
(select name_1 from ap_supplier where ap_supplier.code=(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code) and ap_supplier.agencode = agent_code) as ap_name,
(select address from ap_supplier where ap_supplier.code=(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code)  and ap_supplier.agencode = agent_code) as address_1
,(select name_1  from erp_tambon where erp_tambon.code=(select tambon from ap_supplier where ap_supplier.code=(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code)  and ap_supplier.agencode = agent_code) and erp_tambon.amper=(select amper from ap_supplier where ap_supplier.code=(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code)  and ap_supplier.agencode = agent_code) and erp_tambon.province=(select province from ap_supplier where ap_supplier.code=(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code)  and ap_supplier.agencode = agent_code)  )  as tambon
,(select name_1 from erp_amper where erp_amper.code = (select amper from ap_supplier where ap_supplier.code=(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code)  and ap_supplier.agencode = agent_code)  and erp_amper.province = (select province from ap_supplier where ap_supplier.code=(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code)  and ap_supplier.agencode = agent_code)) as amper
,(select name_1 from erp_province where erp_province.code = (select province from ap_supplier where ap_supplier.code=(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code)  and ap_supplier.agencode = agent_code)) as province
,(select zip_code from ap_supplier where ap_supplier.code=(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code)  and ap_supplier.agencode = agent_code) as zip_code
,(select telephone from ap_supplier where ap_supplier.code=(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code)  and ap_supplier.agencode = agent_code) as telephone
,(select fax from ap_supplier where ap_supplier.code=(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code)  and ap_supplier.agencode = agent_code) as fax
,(select email from ap_supplier where ap_supplier.code=(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code)  and ap_supplier.agencode = agent_code) as email 
" +

                 " from eorder_order  where doc_no =\'" + __docNo + "\' and  last_status = 2 ) as temp1 ";

            SqlDataAdapter __daWait = new SqlDataAdapter(__myQueryTop, __conn);
            DataTable __dtWait = new DataTable();
            __daWait.Fill(__dtWait);

            __ds.Tables.Add(__dtWait);

            string __myQueryDetail = "select ic_code,(select name_1 from ic_inventory where ic_inventory.code=eorder_order_detail.ic_code) as ic_name ,ic_unit, (select name_1 from ic_unit where ic_unit.code =  eorder_order_detail.ic_unit) as ic_unit_name ,qty,price , amount ,discount from eorder_order_detail where doc_no =\'" + __docNo + "\'";
            SqlDataAdapter __daWaitDetail = new SqlDataAdapter(__myQueryDetail, __conn);
            DataTable __dtWaitDetailII = new DataTable();
            __daWaitDetail.Fill(__dtWaitDetailII);

            __ds.Tables.Add(__dtWaitDetailII);


            __print._query(__ds, true);

        }

        protected override void _onGetFieldValue(SMLReport._design._drawObject sender)
        {
            //sender._defaultField = new string[] { "[doc_no], เลขที่เอกสาร", "[doc_date], วันที่เอกสาร" };

            int __queryIndex = -1;

            if (sender.GetType() == typeof(SMLReport._design._drawTextField))
            {
                SMLReport._design._drawTextField __objectTextField = (SMLReport._design._drawTextField)sender;
                __queryIndex = (int)__objectTextField.query;


            }
            else if (sender.GetType() == typeof(SMLReport._design._drawImageField))
            {
                SMLReport._design._drawImageField __objectTextField = (SMLReport._design._drawImageField)sender;
                __queryIndex = (int)__objectTextField.query;
            }
            else if (sender.GetType() == typeof(SMLReport._design._drawTable))
            {
                SMLReport._design._drawTable __objectTable = (SMLReport._design._drawTable)sender;
                __queryIndex = (int)__objectTable.DataQuery;
            }

            if (__queryIndex != -1)
            {
                sender._defaultField = _getField(__queryIndex);
            }
        }

        string[] _getField(int queryIndex)
        {
            string[] __field = null;
            switch (queryIndex)
            {
                case 0:
                    // select doc_no ,doc_date_time, amount_before_discount,discount , amount ,memo,agent_code,(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code) as agent_name,vat_rate, vat_amount, total_amount  from eorder_order  
                    // select code,name_1,address,(select name_1  from erp_tambon where erp_tambon.code=ap_supplier.tambon and erp_tambon.amper=ap_supplier.amper and erp_tambon.province=ap_supplier.province) as tambon,(select name_1 from erp_amper where erp_amper.code=ap_supplier.amper and erp_amper.province=ap_supplier.province) as amper,(select name_1 from erp_province where erp_province.code=ap_supplier.province) as province,zip_code,telephone,fax,email from ap_supplier
                    __field = new string[] { 
                        "[doc_no], เลขที่เอกสาร", 
                        "[doc_date_time], วันเวลาเอกสาร", 
                        "[amount_before_discount], ยอดก่อนลด", 
                        "[discount], ส่วนลด", 
                        "[amount], ราคาสินค้า/บริการ", 
                        "[memo], หมายเหตุ", 
                        "[agent_code], รหัสตัวแทน", 
                        "[agent_name], ชื่อตัวแทน", 
                        "[ap_name], ชื่อเจ้าหนี้", 
                        "[address_1], ที่อยู่", 
                        "[address_2], ตำบล อำเภอ จังหวัด", 
                        "[address_2_1], ตำบล/เขต อำเภอ/แขวง จังหวัด", 
                        "[address_2_2], เขต แขวง จังหวัด", 
                        "[address_short_2], ต. อ. จ.", 
                        "[address_zipcode_2], ตำบล อำเภอ จังหวัด รหัสไปรษณีย์", 
                        "[address_zipcode_2_1], ตำบล/เขต อำเภอ/แขวง จังหวัด รหัสไปรษณีย์", 
                        "[address_zipcode_2_2], เขต แขวง จังหวัด รหัสไปรษณีย์", 
                        "[address_zipcode_short_2], ต. อ. จ. รหัสไปรษณีย์", 
                        "[tel_fax_short_1], โทร แฟกซ์", 
                        "[tel_fax_short_2], Tel Fax", 
                        "[tambon], ตำบล", 
                        "[amper], อำเภอ", 
                        "[province], จังหวัด", 
                        "[zip_code], รหัสไปรษณีย์", 
                        "[telephone], โทรศพท์", 
                        "[fax], แฟกซ์", 
                        "[email], อีเมลล์", 
                        "[vat_rate], อัตราภาษี", 
                        "[vat_amount], มูลค่าภาษี", 
                        "[total_amount], รวมทั้งสิ้น" };
                    break;
                case 1:
                    // select ic_code,(select name_1 from ic_inventory where ic_inventory.code=eorder_order_detail.ic_code) as ic_name ,ic_unit, (select name_1 from ic_unit where ic_unit.code =  eorder_order_detail.ic_unit) as ic_unit_name ,qty,price , amount ,discount from eorder_order_detail 
                    __field = new string[] { "[ic_code], รหัสสินค้า", "[ic_name], ชื่อสินค้า", "[ic_unit], หน่วยนับ", "[ic_unit_name], ชื่อหน่วยนับ", "[qty], จำนวน", "[price], ราคา", "[amount], จำนวนเงิน", "[discount], ส่วนลด" };
                    break;
                case 2:
                    // ตัวแทนขาย (หัวบริษัท)
                    break;
            }
            return __field;
        }
    }

    public class _previewDocDialog : MyLib._myDialogForm
    {
        public _previewDocDialog()
        {
            //ManagementObjectSearcher __printerList = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");

            int __default = 0;
            int __count = 0;
            List<string> _printers = new List<string>();
            //foreach (ManagementObject __getPrinter in __printerList.Get())
            foreach (MyLib._printerListClass __getPrinter in MyLib._myGlobal._printerList)
            {
                string __printerName = __getPrinter._printerName;
                if (__getPrinter._isDefault)
                {
                    __default = __count;
                }
                _printers.Add(__printerName);
                __count++;
            }

            this._dialogScreen._maxColumn = 1;
            this._dialogScreen._addTextBox(0, 0, 1, 0, "doc_no", 1, 0, 0, true, false, false, false, true, "เลขทีเอกสาร");
            this._buttonOk.ButtonText = "Preview";
            this._dialogScreen.Invalidate();
        }
    }

}
