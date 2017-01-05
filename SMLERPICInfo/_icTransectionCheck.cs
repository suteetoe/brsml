using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SMLERPICInfo
{
    public partial class _icTransectionCheck : UserControl
    {
        public static string _inoutFlag = "12,14,16,44,46,48,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,76,6,34,36,310";
        List<_queryCheck> _query = new List<_queryCheck>();
        public _icTransectionCheck()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            this._myGrid._addColumn("queryName", 1, 0, 85, false, false, false, false, "", "", "", "รายการ");
            this._myGrid._addColumn("checkresult", 1, 0, 5, false, false, false, false, "", "", "", "ตรวจสอบ");
            this._myGrid._addColumn("updateresult", 1, 0, 5, false, false, false, false, "", "", "", "ปรับปรุง");
            this._myGrid._addColumn("Select", 11, 0, 5);

            _addquery();

            this.Load += new EventHandler(_icTransectionCheck_Load);

        }

        void _icTransectionCheck_Load(object sender, EventArgs e)
        {
            // load
            for (int __i = 0; __i < _query.Count; __i++)
            {
                int __row = this._myGrid._addRow();
                string __name = MyLib._myResource._findResource(_query[__i].queryName)._str;
                if (__name.Length == 0)
                {
                    __name = _query[__i].queryName;
                }
                this._myGrid._cellUpdate(__row, 0, __name, false);
            }
        }

        public void _addquery()
        {
            _query = _getQuery();
        }

        public static List<_queryCheck> _getQuery()
        {
            List<_queryCheck> __query = new List<_queryCheck>();
            __query.Clear();
            __query.Add(new _queryCheck()
            {
                queryName = "ตรวจสอบรายการรายวันตัวตั้งหน่วยนับสินค้า",
                queryCheck = "select count(roworder) as xcount from ic_trans_detail where trans_flag in (" + _inoutFlag + ",6,30) and ( stand_value=0 or stand_value is null or divide_value = 0 or divide_value is null )",
                queryUpdate = "update ic_trans_detail set stand_value=1, divide_value = 1 where trans_flag in (" + _inoutFlag + ",6,30) and ( stand_value=0 or stand_value is null or divide_value = 0 or divide_value is null ) ",
                queryDetail = "select doc_no,doc_date,item_code,item_name,trans_flag from ic_trans_detail where trans_flag in (" + _inoutFlag + ",6,30) and ( stand_value=0 or stand_value is null or divide_value = 0 or divide_value is null ) "
            });

            __query.Add(new _queryCheck()
            {
                queryName = "ตรวจสอบคลังและที่เก็บที่ไม่มีรหัสสินค้า",
                queryCheck = "select count(wh_code) as xcount from ic_wh_shelf where ic_code='' ",
                queryUpdate = "delete from ic_wh_shelf where ic_code='' "
            });

            __query.Add(new _queryCheck()
            {
                queryName = "ic_trans.is_pos = null",
                queryCheck = "select count(*) as xcount from ic_trans where ic_trans.is_pos is null ",
                queryUpdate = " update ic_trans set is_pos = 0 where ic_trans.is_pos is null ",
                _isAutoProcess = true
            });

            __query.Add(new _queryCheck()
            {
                queryName = "ic_trans_detail.is_pos = null",
                queryCheck = "select count(*) as xcount from ic_trans_detail where ic_trans_detail.is_pos is null ",
                queryUpdate = " update ic_trans_detail set is_pos = 0 where ic_trans_detail.is_pos is null",
                _isAutoProcess = true
            });

            __query.Add(new _queryCheck()
            {
                // 12,14,16,44,46,48,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73

                queryName = "ตรวจสอบรายวันซื้อขายสินค้าที่ไม่มีคลังและที่เก็บ",
                //queryCheck = "select count(item_code) as xcount from ic_trans_detail where ( wh_code = '' or shelf_code = '' or wh_code is null or shelf_code is null ) and trans_flag in (12,14,16,44,46,48,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73)",
                queryCheck = "select count(item_code) as xcount from ic_trans_detail where ( ( coalesce(wh_code, '') = ''  or coalesce(shelf_code, '') = '' )  or ( length(coalesce((select name_1 from ic_warehouse where ic_warehouse.code = ic_trans_detail.wh_code) , ''))  = 0 or  length(coalesce((select name_1 from ic_shelf where ic_shelf.code = ic_trans_detail.shelf_code and ic_shelf.whcode = ic_trans_detail.wh_code) , ''))  = 0 )) and trans_flag in (12,14,16,44,46,48,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73)",
                queryUpdate = "",
                queryDetail = "select doc_no,doc_date,item_code,item_name,trans_flag from ic_trans_detail where ( ( coalesce(wh_code, '') = ''  or coalesce(shelf_code, '') = '' )  or ( length(coalesce((select name_1 from ic_warehouse where ic_warehouse.code = ic_trans_detail.wh_code) , ''))  = 0 or  length(coalesce((select name_1 from ic_shelf where ic_shelf.code = ic_trans_detail.shelf_code and ic_shelf.whcode = ic_trans_detail.wh_code) , ''))  = 0 )) and trans_flag in (12,14,16,44,46,48,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73)",
            });

            __query.Add(new _queryCheck()
            {
                queryName = "ตรวจสอบรายวันซื้อขายสินค้าที่ไม่มีสถานะ",
                queryCheck = "select count(doc_no) as xcount from ic_trans where last_status is null",
                queryUpdate = "update ic_trans set last_status = 0 where last_status is null",
                queryDetail = "select doc_no,doc_date,cust_code,trans_flag from ic_trans where last_status is null",
                _isAutoProcess = true
            });

            __query.Add(new _queryCheck()
            {
                queryName = "ตรวจสอบรายละเอียดรายวันซื้อขายสินค้าที่ไม่มีสถานะ",
                queryCheck = "select count(doc_no) as xcount from ic_trans_detail where last_status is null",
                queryUpdate = "update ic_trans_detail set last_status = 0 where last_status is null",
                queryDetail = "select doc_no,doc_date,cust_code,trans_flag from ic_trans_detail where last_status is null",
                _isAutoProcess = true
            });

            __query.Add(new _queryCheck()
            {
                queryName = "ตรวจสอบรายวันลูกหนี้-เจ้าหนี้ที่ไม่มีสถานะ",
                queryCheck = "select count(doc_no) as xcount from ap_ar_trans where last_status is null",
                queryUpdate = "update ap_ar_trans set last_status = 0 where last_status is null",
                queryDetail = "select doc_no,doc_date,cust_code,trans_flag from ap_ar_trans where last_status is null",
                _isAutoProcess = true
            });

            __query.Add(new _queryCheck()
            {
                queryName = "ตรวจสอบรายละเอียดรายวันลูกหนี้-เจ้าหนี้ที่ไม่มีสถานะ",
                queryCheck = "select count(doc_no) as xcount from ap_ar_trans_detail where last_status is null",
                queryUpdate = "update ap_ar_trans_detail set last_status = 0 where last_status is null",
                queryDetail = "select doc_no,doc_date,billing_no,trans_flag from ap_ar_trans_detail where last_status is null",
                _isAutoProcess = true
            });

            __query.Add(new _queryCheck()
            {
                queryName = "ตรวจสอบรายละเอียดการรับ-จ่ายชำระที่ไม่มีสถานะ",
                queryCheck = "select count(doc_no) as xcount from cb_trans_detail where last_status is null",
                queryUpdate = "update cb_trans_detail set last_status = 0 where last_status is null",
                queryDetail = "select doc_no,doc_date,trans_flag from cb_trans_detail where last_status is null",
                _isAutoProcess = true
            });

            __query.Add(new _queryCheck()
            {
                // 12,14,16,44,46,48,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73

                queryName = "ตรวจสอบรายวันซื้อขายไม่มีวันที่คำณวน",
                queryCheck = "select count(doc_no) as xcount from ic_trans_detail where doc_date_calc is null or doc_time_calc is null",
                queryUpdate = " update ic_trans_detail set doc_date_calc=doc_date,doc_time_calc=coalesce(doc_time, '08:00') where doc_date_calc is null or doc_time_calc is null",
                queryDetail = "select doc_no,doc_date,item_code,trans_flag from ic_trans_detail where doc_date_calc is null or doc_time_calc is null",
                _isAutoProcess = true
            });

            __query.Add(new _queryCheck()
            {
                // 12,14,16,44,46,48,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73

                queryName = "ตรวจสอบสินค้า ที่มีการเลือกคลังและที่เก็บ ที่ไม่มีอยู่จริง",
                queryCheck = "select count(*) as xcount from ic_wh_shelf where length(coalesce((select name_1 from ic_warehouse where ic_warehouse.code = ic_wh_shelf.wh_code) , ''))  = 0 or  length(coalesce((select name_1 from ic_shelf where ic_shelf.code = ic_wh_shelf.shelf_code and ic_shelf.whcode = ic_wh_shelf.wh_code) , ''))  = 0",
                queryUpdate = "delete from ic_wh_shelf where length(coalesce((select name_1 from ic_warehouse where ic_warehouse.code = ic_wh_shelf.wh_code) , ''))  = 0 or  length(coalesce((select name_1 from ic_shelf where ic_shelf.code = ic_wh_shelf.shelf_code and ic_shelf.whcode = ic_wh_shelf.wh_code) , ''))  = 0",
                queryDetail = "select ic_code,wh_code,shelf_code from ic_wh_shelf where length(coalesce((select name_1 from ic_warehouse where ic_warehouse.code = ic_wh_shelf.wh_code) , ''))  = 0 or  length(coalesce((select name_1 from ic_shelf where ic_shelf.code = ic_wh_shelf.shelf_code and ic_shelf.whcode = ic_wh_shelf.wh_code) , ''))  = 0"
            });

            __query.Add(new _queryCheck()
            {
                queryName = "ตรวจสอบสินค้า ที่ไม่พบประเภทสินค้า",
                queryCheck = "select count(*) as xcount from ic_trans_detail where item_type is null",
                queryUpdate = "update ic_trans_detail set item_type = (select item_type from ic_inventory where ic_inventory.code=ic_trans_detail.item_code) where item_type is null",
                queryDetail = "select doc_no,doc_date,item_code,item_name,trans_flag from ic_trans_detail where item_type is null",
                _isAutoProcess = true
            });

            __query.Add(new _queryCheck()
            {
                queryName = "ตรวจสอบลำดับการค้นหา ที่ไม่มีสินค้า",
                queryCheck = "select count(*) as xcount  from ic_inventory_level where not exists(select code from ic_inventory where ic_inventory.code = ic_inventory_level.ic_code)",
                queryUpdate = "delete from ic_inventory_level where not exists(select code from ic_inventory where ic_inventory.code = ic_inventory_level.ic_code)",
                queryDetail = ""
            });

            __query.Add(new _queryCheck()
            {
                queryName = "รายวันซื้อขาย ไม่ม่หน่วยนับสินค้า",
                queryCheck = "select count(*) as xcount from ic_trans_detail where ( unit_code is null or unit_code  = '' ) and (item_code <> '' or item_code is not null ) and  trans_flag in (" + _inoutFlag + ")",
                queryDetail = "select doc_no,doc_date,trans_flag, item_code,item_name from ic_trans_detail where ( unit_code is null or unit_code  = '' ) and (item_code <> '' or item_code is not null ) and  trans_flag in (" + _inoutFlag + ")",
                queryUpdate = ""
            });

            __query.Add(new _queryCheck()
            {
                queryName = "ข้อมูลสินค้า ที่มีตัวตั้งสินค้าเป็น 0",
                queryCheck = "select count(*) as xcount from ic_inventory where coalesce(" + _g.d.ic_inventory._unit_standard_stand_value + ", 0)=0",
                queryDetail = "select code,name_1 from ic_inventory where coalesce(" + _g.d.ic_inventory._unit_standard_stand_value + ", 0)=0",
                queryUpdate = "update ic_inventory set " + _g.d.ic_inventory._unit_standard_stand_value + "=1 where coalesce(" + _g.d.ic_inventory._unit_standard_stand_value + ", 0)=0"
            });

            // " + _g.d.ic_inventory._unit_standard_divide_value + "=0 or 
            __query.Add(new _queryCheck()
            {
                queryName = "ข้อมูลสินค้า ที่มีตัวหารสินค้าเป็น 0",
                queryCheck = "select count(*) as xcount from ic_inventory where coalesce(" + _g.d.ic_inventory._unit_standard_divide_value + ", 0)=0",
                queryDetail = "select code,name_1 from ic_inventory where coalesce(" + _g.d.ic_inventory._unit_standard_divide_value + ", 0)=0",
                queryUpdate = "update ic_inventory set " + _g.d.ic_inventory._unit_standard_divide_value + "=1 where coalesce(" + _g.d.ic_inventory._unit_standard_divide_value + ", 0)=0"
            });

            // update ic_inventory set use_expire=1 where (select count(*) from ic_trans_detail where item_code=ic_inventory.code and trans_flag=12 and date_expire is not null) > 0

            __query.Add(new _queryCheck()
            {
                queryName = "ข้อมูลสินค้า ที่มีการบันทึกวันหมดอายุ แต่ยังไม่ได้กำหนด ให้ใช้ระบบหมดอายุ",
                queryCheck = "select count(*) as xcount from ic_inventory where use_expire=0 and (select count(*) from ic_trans_detail where item_code=ic_inventory.code and trans_flag=12 and date_expire is not null) > 0",
                queryDetail = "select code,name_1 from ic_inventory where use_expire=0 and (select count(*) from ic_trans_detail where item_code=ic_inventory.code and trans_flag=12 and date_expire is not null) > 0",
                queryUpdate = "update ic_inventory set use_expire=1 where use_expire=0 and (select count(*) from ic_trans_detail where item_code=ic_inventory.code and trans_flag=12 and date_expire is not null) > 0"
            });

            __query.Add(new _queryCheck()
            {
                queryName = "ข้อมูล barcode สินค้า ที่ไม่พบรหัสสินค้า",
                queryCheck = "select count(*) as xcount from ic_inventory_barcode where not exists( select code from ic_inventory where ic_inventory_barcode.ic_code = ic_inventory.code)",
                queryDetail = "select ic_code, barcode, description, unit_code, price from ic_inventory_barcode where not exists( select code from ic_inventory where ic_inventory_barcode.ic_code = ic_inventory.code)",
                queryUpdate = "delete from ic_inventory_barcode where not exists( select code from ic_inventory where ic_inventory_barcode.ic_code = ic_inventory.code)"
            });

            __query.Add(new _queryCheck()
            {
                queryName = "ข้อมูลรหัสสมาชิกไม่พบลูกหนี้ สินค้า ที่ไม่พบรหัสสินค้า",
                queryCheck = "select count(*) as xcount from ar_dealer where not exists (select code from ar_customer where ar_customer.code = ar_dealer.ar_code)",
                queryDetail = "select code, ar_code from ar_dealer where not exists (select code from ar_customer where ar_customer.code = ar_dealer.ar_code) ",
                queryUpdate = "delete from ar_dealer where not exists (select code from ar_customer where ar_customer.code = ar_dealer.ar_code)"
            });

            __query.Add(new _queryCheck()
            {
                queryName = "รายการสินค้าไม่พบ Serial",
                queryCheck = "select count(*) as xcount from ic_trans_detail where is_serial_number <> 1 and (select ic_serial_no from ic_inventory where ic_inventory.code = ic_trans_detail.item_code ) = 1 and last_status = 0 and trans_flag in (" + _inoutFlag + ")",
                queryDetail = "select doc_no ,doc_date, trans_flag, item_code, qty from ic_trans_detail where is_serial_number <> 1 and (select ic_serial_no from ic_inventory where ic_inventory.code = ic_trans_detail.item_code ) = 1 and last_status = 0 and trans_flag in (" + _inoutFlag + ")",
                queryUpdate = ""
            });

            __query.Add(new _queryCheck()
            {
                queryName = "รายการเช็คจ่ายไม่พบเอกสารอ้างอิง",
                queryCheck = "select count(*) as xcount from ic_trans_detail where trans_flag in ( 451, 452,453,456 ) and coalesce(doc_ref , \'\') = \'\' and  (select count(doc_ref) from cb_chq_list where cb_chq_list.chq_number = ic_trans_detail.chq_number and cb_chq_list.chq_type = 2)  = 1 ",
                queryDetail = "select doc_no ,doc_date, trans_flag, item_code, chq_number, doc_ref, (select doc_ref from cb_chq_list where cb_chq_list.chq_number = ic_trans_detail.chq_number and chq_type = 2) as new_doc_ref from ic_trans_detail where trans_flag in ( 451, 452,453,456 ) and coalesce(doc_ref , \'\') = \'\' and  (select count(doc_ref) from cb_chq_list where cb_chq_list.chq_number = ic_trans_detail.chq_number and cb_chq_list.chq_type = 2)  = 1 ",
                queryUpdate = "update ic_trans_detail set doc_ref =  (select doc_ref from cb_chq_list where cb_chq_list.chq_number = ic_trans_detail.chq_number and chq_type = 2) where trans_flag in ( 451, 452,453,456 ) and coalesce(doc_ref , \'\') = \'\' and  (select count(doc_ref) from cb_chq_list where cb_chq_list.chq_number = ic_trans_detail.chq_number  and cb_chq_list.chq_type = 2)  = 1 and  (select doc_ref from cb_chq_list where cb_chq_list.chq_number = ic_trans_detail.chq_number and chq_type = 2 limit 1)   <> ''",
                _isAutoProcess = false

            });

            __query.Add(new _queryCheck()
            {
                queryName = "รายการเช็ครับไม่พบเอกสารอ้างอิง",
                queryCheck = "select count(*) as xcount from ic_trans_detail where trans_flag in ( 430, 431,432,433,434,436 ) and coalesce(doc_ref , \'\') = \'\' and  (select count(doc_ref) from cb_chq_list where cb_chq_list.chq_number = ic_trans_detail.chq_number and cb_chq_list.chq_type = 1)  = 1 ",
                queryDetail = "select doc_no ,doc_date, trans_flag, item_code, chq_number, doc_ref, (select doc_ref from cb_chq_list where cb_chq_list.chq_number = ic_trans_detail.chq_number and chq_type = 2) as new_doc_ref from ic_trans_detail where trans_flag in (  430, 431,432,433,434,436  ) and coalesce(doc_ref , \'\') = \'\' and  (select count(doc_ref) from cb_chq_list where cb_chq_list.chq_number = ic_trans_detail.chq_number and cb_chq_list.chq_type = 1)  = 1 ",
                queryUpdate = "update ic_trans_detail set doc_ref =  (select doc_ref from cb_chq_list where cb_chq_list.chq_number = ic_trans_detail.chq_number and chq_type = 1) where trans_flag in (  430, 431,432,433,434,436  ) and coalesce(doc_ref , \'\') = \'\' and  (select count(doc_ref) from cb_chq_list where cb_chq_list.chq_number = ic_trans_detail.chq_number  and cb_chq_list.chq_type = 1)  = 1 and  (select doc_ref from cb_chq_list where cb_chq_list.chq_number = ic_trans_detail.chq_number and chq_type = 1 limit 1)   <> ''",
                _isAutoProcess = false

            });

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro)
            {
                __query.Add(new _queryCheck()
                {
                    queryName = "รายการสั่งอาหารไม่พบรายละเอียดสินค้า",
                    queryCheck = "select count(*) as xcount from table_order where not exists( select code from ic_unit_use where ic_unit_use.code= table_order.unit_code and ic_unit_use.ic_code = table_order.item_code) ",
                    queryDetail = "select item_code, unit_code from table_order where not exists( select code from ic_unit_use where ic_unit_use.code= table_order.unit_code and ic_unit_use.ic_code = table_order.item_code) ",
                    queryUpdate = "",
                    _isAutoProcess = false

                });


            }

            return __query;
        }

        int _processIndex = -1;
        private void _processButton_Click(object sender, EventArgs e)
        {
            // clear grid value
            for (int __i = 0; __i < this._myGrid._rowData.Count; __i++)
            {
                this._myGrid._cellUpdate(__i, "checkresult", "", false);
            }

            _processIndex = 0;
            Thread __processThread = new Thread(_process);
            __processThread.Start();

        }

        public void _process()
        {
            MyLib._myFrameWork __fw = new MyLib._myFrameWork();

            for (_processIndex = 0; _processIndex < _query.Count; _processIndex++)
            {
                this._myGrid._cellUpdate(_processIndex, "checkresult", "in process", false);

                try
                {

                    DataTable __ds = __fw._queryShort(_query[_processIndex].queryCheck).Tables[0];
                    this._myGrid._cellUpdate(_processIndex, "checkresult", __ds.Rows[0]["xcount"].ToString(), false);
                }
                catch (Exception ex)
                {
                    this._myGrid._cellUpdate(_processIndex, "checkresult", ex.Message.ToString(), false);
                }
            }
        }

        Thread _updateThread = null;
        private void _updateButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(MyLib._myGlobal._resource("ยืนยันการปรับปรุงข้อมูล"), MyLib._myGlobal._resource("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                _processIndex = 0;
                _updateThread = new Thread(_update);
                _updateThread.Start();
            }
        }

        public void _update()
        {
            MyLib._myFrameWork __fw = new MyLib._myFrameWork();

            for (_processIndex = 0; _processIndex < _query.Count; _processIndex++)
            {
                if (((int)_myGrid._cellGet(_processIndex, "Select")) == 1)
                {
                    if (_query[_processIndex].queryUpdate.Length > 0)
                    {
                        string __result = __fw._queryInsertOrUpdate(MyLib._myGlobal._databaseName, _query[_processIndex].queryUpdate);

                        string __status = (__result.Length > 0) ? __result : "Success";
                        this._myGrid._cellUpdate(_processIndex, "updateresult", __status, false);
                    }
                    else
                    {
                        this._myGrid._cellUpdate(_processIndex, "updateresult", "No Update", false);
                    }
                }
            }

            MessageBox.Show("Success", "Complete !!!", MessageBoxButtons.OK);
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                _updateThread.Abort();
            }
            catch
            {
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _viewDetailButton_Click(object sender, EventArgs e)
        {
            int __row = this._myGrid._selectRow;

            //return;
            if (__row != -1 && __row < _query.Count)
            {
                if (_query[__row].queryDetail.Length > 0)
                {
                    try
                    {
                        Form _detailForm = new Form();
                        _detailForm.WindowState = FormWindowState.Maximized;
                        _detailForm.Text = _query[__row].queryName;

                        DataGridView __gridView = new DataGridView();
                        __gridView.Dock = DockStyle.Fill;
                        _detailForm.Controls.Add(__gridView);

                        MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                        DataSet result = myFrameWork._query(MyLib._myGlobal._databaseName, _query[__row].queryDetail);
                        __gridView.DataSource = result;
                        __gridView.DataMember = "Row";

                        _detailForm.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("warning20") + "\n" + ex.Message, MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        public static void _startProcess()
        {
            List<_queryCheck> __query = _getQuery();
            MyLib._myFrameWork __fw = new MyLib._myFrameWork();

            for (int __i = 0; __i < __query.Count; __i++)
            {
                if (__query[__i]._isAutoProcess == true)
                {

                    DataSet __result = __fw._queryShort(__query[__i].queryCheck);
                    if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
                    {
                        if (MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[0][0].ToString()) > 0)
                        {
                            // start update 
                            if (__query[__i].queryUpdate.Length > 0)
                            {
                                string __queryResult = __fw._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __query[__i].queryUpdate);
                            }
                        }
                    }
                }
            }

            //{
            //    // toe ตรวจสอบรายการใบกำกับภาษีอย่างเต็มออกแทน แต่ is_pos เป็น 0
            //    StringBuilder __script = new StringBuilder();
            //    __script.Append("update " + _g.d.ic_trans_detail._table + " set " + _g.d.ic_trans_detail._is_pos + "=1 ");
            //    __script.Append(" where " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ));
            //    __script.Append(" and (select coalesce(ic_trans.doc_ref, '') from ic_trans where ic_trans.trans_flag = ic_trans_detail.trans_flag and ic_trans.is_pos = 1 and ic_trans.doc_no = ic_trans_detail.doc_no) <> '' ");
            //    __script.Append(" and " + _g.d.ic_trans_detail._is_pos + "=0");

            //    string __result = __fw._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script.ToString());
            //}
        }
    }

    public class _queryCheck
    {
        public string queryName = "";
        public string queryCheck = "";
        public string queryUpdate = "";
        public string queryDetail = "";
        public bool _isAutoProcess = false;
        public bool _isConfirm = false;
    }
}
