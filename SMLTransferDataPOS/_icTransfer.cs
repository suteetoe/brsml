using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using SMLDataCenterSync;
using System.Collections;
using System.Threading;
using System.IO;

namespace SMLTransferDataPOS
{
    public partial class _icTransfer : UserControl
    {
        private delegate void sendMessageInvoke(string message);
        private delegate void processSuccessInvoke();

        sendMessageInvoke _sendMessageResult;
        processSuccessInvoke _onProcessSuccess;

        int __insertCount = 5000;

        Boolean _showFullResult = false;

        XmlDocument _xDoc = null;
        List<_tableClass> __tableList = new List<_tableClass>();

        public _icTransfer()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this.toolStrip2.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            // set control
            this._myGrid1._isEdit = false;
            this._myGrid1._addColumn("Check", 11, 0, 10);
            this._myGrid1._addColumn("Table Name", 1, 0, 40, false, false);
            this._myGrid1._addColumn("Table Desc", 1, 0, 50, false, false);
            _addTableList();

            for (int __i = 0; __i < this.__tableList.Count; __i++)
            {
                int __rowAdd = this._myGrid1._addRow();
                this._myGrid1._cellUpdate(__rowAdd, 0, 1, false);
                this._myGrid1._cellUpdate(__rowAdd, 1, __tableList[__i]._tableName, false);
                this._myGrid1._cellUpdate(__rowAdd, 2, __tableList[__i]._tableDesc, false);
            }

            _sendMessageResult = new sendMessageInvoke(_sendMessage);
            _onProcessSuccess = new processSuccessInvoke(_processSuccess);

        }

        void _addTableList()
        {
            __tableList.Add(new _tableClass() { _tableName = _g.d.ap_supplier._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ap_supplier_detail._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ar_customer._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ar_customer_detail._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ar_dealer._table, _tableDesc = "" });

            __tableList.Add(new _tableClass() { _tableName = _g.d.erp_user._table, _tableDesc = "" });

            __tableList.Add(new _tableClass() { _tableName = _g.d.images._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_brand._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_category._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_character._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_class._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_close_reason._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_color._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_color_use._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_design._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_dimension._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_dimension_name._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_grade._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_group._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_group_sub._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_import_duty._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_inventory._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_inventory_append._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_inventory_append_detail._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_inventory_barcode._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_inventory_barcode_price._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_inventory_detail._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_inventory_level._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_inventory_pictures._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_inventory_price._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_inventory_price_formula._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_inventory_purchase_price._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_inventory_replace._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_inventory_replace_detail._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_inventory_replacement._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_inventory_set._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_inventory_set_detail._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_inventory_wh._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_issue_type._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_model._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_name_billing._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_name_merket._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_name_pos._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_pattern._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_pattern_use._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_serial._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_shelf._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_size._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_size_use._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_type._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_unit._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_unit_type._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_unit_use._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_warehouse._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_wh_shelf._table, _tableDesc = "" });

            // promotion
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_promotion._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_promotion_detail._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_promotion_formula._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_promotion_formula_condition._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_promotion_formula_price_discount._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_promotion_formula_action._table, _tableDesc = "" });
            __tableList.Add(new _tableClass() { _tableName = _g.d.ic_promotion_formula_group_qty._table, _tableDesc = "" });

        }

        public class _tableClass
        {
            public string _tableName = "";
            public string _tableDesc = "";
        }

        void _processSuccess()
        {
            if (_errorCount.Equals(0))
            {
                MessageBox.Show("Process Success !!", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Process Success !!" + "\n Error : " + _errorCount.ToString() + " Record ", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void _sendMessage(string message)
        {
            //this._resultTextbox..AppendText(message);
            //this._resultTextbox.AppendText("\n");
            this._resultTextBox.AppendText(message);
            this._resultTextBox.AppendText("\n");
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            // create script
            _process();
        }

        void _process()
        {
            try
            {

                //_copyTable(_g.d.ap_supplier._table);
                //_copyTable(_g.d.ap_supplier_detail._table);
                //_copyTable(_g.d.ar_customer._table);
                //_copyTable(_g.d.ar_customer_detail._table);
                //_copyTable(_g.d.ar_dealer._table);


                //_copyTable(_g.d.images._table);
                //_copyTable(_g.d.ic_brand._table);
                //_copyTable(_g.d.ic_category._table);
                //_copyTable(_g.d.ic_character._table);
                //_copyTable(_g.d.ic_class._table);
                //_copyTable(_g.d.ic_close_reason._table);
                //_copyTable(_g.d.ic_color._table);
                //_copyTable(_g.d.ic_color_use._table);
                //_copyTable(_g.d.ic_design._table);
                //_copyTable(_g.d.ic_dimension._table);
                //_copyTable(_g.d.ic_dimension_name._table);
                //_copyTable(_g.d.ic_grade._table);
                //_copyTable(_g.d.ic_group._table);
                //_copyTable(_g.d.ic_group_sub._table);
                //_copyTable(_g.d.ic_import_duty._table);
                //_copyTable(_g.d.ic_inventory._table);
                //_copyTable(_g.d.ic_inventory_append._table);
                //_copyTable(_g.d.ic_inventory_append_detail._table);
                //_copyTable(_g.d.ic_inventory_barcode._table);
                //_copyTable(_g.d.ic_inventory_barcode_price._table);
                //_copyTable(_g.d.ic_inventory_detail._table);
                //_copyTable(_g.d.ic_inventory_level._table);
                //_copyTable(_g.d.ic_inventory_pictures._table);
                //_copyTable(_g.d.ic_inventory_price._table);
                //_copyTable(_g.d.ic_inventory_price_formula._table);
                //_copyTable(_g.d.ic_inventory_purchase_price._table);
                //_copyTable(_g.d.ic_inventory_replace._table);
                //_copyTable(_g.d.ic_inventory_replace_detail._table);
                //_copyTable(_g.d.ic_inventory_replacement._table);
                //_copyTable(_g.d.ic_inventory_set._table);
                //_copyTable(_g.d.ic_inventory_set_detail._table);
                //_copyTable(_g.d.ic_inventory_wh._table);
                //_copyTable(_g.d.ic_issue_type._table);
                //_copyTable(_g.d.ic_model._table);
                //_copyTable(_g.d.ic_name_billing._table);
                //_copyTable(_g.d.ic_name_merket._table);
                //_copyTable(_g.d.ic_name_pos._table);
                //_copyTable(_g.d.ic_pattern._table);
                //_copyTable(_g.d.ic_pattern_use._table);
                //_copyTable(_g.d.ic_serial._table);
                //_copyTable(_g.d.ic_shelf._table);
                //_copyTable(_g.d.ic_size._table);
                //_copyTable(_g.d.ic_size_use._table);
                //_copyTable(_g.d.ic_type._table);
                //_copyTable(_g.d.ic_unit._table);
                //_copyTable(_g.d.ic_unit_type._table);
                //_copyTable(_g.d.ic_unit_use._table);
                //_copyTable(_g.d.ic_warehouse._table);
                //_copyTable(_g.d.ic_wh_shelf._table);
                if (this._showFullResult == true)
                {
                    _appendLog("Start Transfer DATA", true);
                    this.Invoke(_sendMessageResult, new object[] { "Start Transfer DATA" });
                }
                for (int __i = 0; __i < this._myGrid1._rowData.Count; __i++)
                {
                    if (this._myGrid1._cellGet(__i, 0).ToString().Equals("1"))
                    {
                        string __tableName = this._myGrid1._cellGet(__i, 1).ToString();
                        _copyTable(__tableName);

                        _cleanTemp(__tableName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Invoke(_sendMessageResult, new object[] { ex.ToString() });
            }

            //_copyTable(_g.d.ic_inventory_barcode._table);
            this.Invoke(_onProcessSuccess);
        }

        void _cleanTemp(string tableName)
        {
            MyLib._myFrameWork __clientFrameWork = new MyLib._myFrameWork();
            string __tempTableName = tableName + "_temp";
            __clientFrameWork._query(MyLib._myGlobal._databaseName, "truncate table " + __tempTableName);
        }

        int _errorCount = 0;

        /// <summary>
        /// สร้างตาราง ฝั่ง client (temp)
        /// </summary>
        /// <param name="tableName"></param>
        void _copyTable(string tableName)
        {
            if (this._showFullResult == true)
            {

                _appendLog("Start Copy Table " + tableName);
                this.Invoke(_sendMessageResult, new object[] { "Start Copy Table " + tableName });
            }

            string __tempTableName = tableName + "_temp";
            MyLib._myFrameWork __clientFrameWork = new MyLib._myFrameWork();
            string __getXmlClient = __clientFrameWork._getSchemaTable(MyLib._myGlobal._databaseName, __tempTableName);

            if (__getXmlClient.IndexOf("column_name") == -1)
            {
                // ถ้าไม่มีโครงสร้าง ฝั่ง client สร้างทันที

                // ดึงโครงสร้าง ฝั่ง server 
                if (_xDoc == null)
                {
                    MyLib._myFrameWork __datacenterFrameWork = new MyLib._myFrameWork(_global._datacenter_server, _global._datacenter_configFileName, _global._datacenter_database_type);
                    DataSet __myDataSet = __datacenterFrameWork._dataStruct(MyLib._myGlobal._databaseStructFileName);

                    _xDoc = new XmlDocument();
                    _xDoc.LoadXml(__myDataSet.GetXml());
                    _xDoc.DocumentElement.Normalize();

                }

                // filter
                XmlNode __tableSelect = _getTableStruct(tableName);

                // gen script สร้างฐานข้อมูล
                string __createTableQuery = _createTableScript(__tableSelect, __tempTableName);
                string __result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __createTableQuery);
                if (__result.Length == 0)
                {
                    this.Invoke(_sendMessageResult, new object[] { "create table " + __tempTableName + " success " });

                }
                else
                {
                    this.Invoke(_sendMessageResult, new object[] { __result });
                    _errorCount++;
                }


                _checkIndex(__tableSelect, __tempTableName);
            }

            // กระหน่ำ
            _transFerTable(tableName, __tempTableName);

            // compare และโอน จาก temp ไป table จริง
            _compareTable(__tempTableName, tableName);
        }

        void _compareTable(string tableName, string targetTableName)
        {
            // ดึงโครงสร้าง ทั้ง 2 ตาราง มา compare
            MyLib._myFrameWork __clientFrameWork = new MyLib._myFrameWork();

            DataSet __ds = _getField(targetTableName);

            string __getXmlSource = __clientFrameWork._getSchemaTable(MyLib._myGlobal._databaseName, tableName);
            List<_syncFieldListStruct> __fieldListSource = SMLDataCenterSync._run._getFieldFromXml(tableName, __getXmlSource, __ds.Tables[0]);

            string __getXmlClient = __clientFrameWork._getSchemaTable(MyLib._myGlobal._databaseName, targetTableName);
            List<_syncFieldListStruct> __fieldListClient = SMLDataCenterSync._run._getFieldFromXml(targetTableName, __getXmlClient, __ds.Tables[0]);

            int __loop1 = 0;
            while (__loop1 < __fieldListSource.Count)
            {
                Boolean __found = false;
                for (int __loop2 = 0; __loop2 < __fieldListClient.Count && __found == false; __loop2++)
                {
                    if (__fieldListSource[__loop1]._fieldNameForInsertUpdate.Equals(__fieldListClient[__loop2]._fieldNameForInsertUpdate))
                    {
                        __found = true;
                        break;
                    }
                }
                if (__found == false)
                {
                    __fieldListSource.RemoveAt(__loop1);
                }
                else
                {
                    __loop1++;
                }
            }
            // หาใน field client ถ้าไม่มีใน field server ให้ลบ field client ออก
            __loop1 = 0;
            while (__loop1 < __fieldListClient.Count)
            {
                Boolean __found = false;
                for (int __loop2 = 0; __loop2 < __fieldListSource.Count && __found == false; __loop2++)
                {
                    if (__fieldListClient[__loop1]._fieldNameForInsertUpdate.Equals(__fieldListSource[__loop2]._fieldNameForInsertUpdate))
                    {
                        __found = true;
                        break;
                    }
                }
                if (__found == false)
                {
                    __fieldListClient.RemoveAt(__loop1);
                }
                else
                {
                    __loop1++;
                }
            }

            StringBuilder __compareQuery = new StringBuilder();

            // ประกอบ field
            StringBuilder __fieldForInsert = new StringBuilder();
            StringBuilder __fieldForSelect = new StringBuilder();
            for (int __loop = 0; __loop < __fieldListClient.Count; __loop++)
            {

                if (__fieldForSelect.Length > 0)
                {
                    __fieldForInsert.Append(",");
                    __fieldForSelect.Append(",");
                }

                __fieldForInsert.Append(__fieldListClient[__loop]._fieldNameForInsertUpdate.ToString());
                __fieldForSelect.Append(__fieldListClient[__loop]._fieldNameForSelect.ToString());

                if (__fieldListClient[__loop]._fieldNameForSelect.ToLower().Equals("is_lock_record") == false && __fieldListClient[__loop]._fieldNameForSelect.ToLower().Equals("roworder") == false && (__fieldListClient[__loop]._fieldNameForSelect.ToLower().Equals("last_movement_date") == false) && (__fieldListClient[__loop]._fieldNameForSelect.ToLower().Equals("guid_code") == false))
                {
                    if (__compareQuery.Length > 0)
                    {
                        __compareQuery.Append(" and ");
                    }

                    __compareQuery.Append(tableName + "." + __fieldListClient[__loop]._fieldNameForSelect + "=" + targetTableName + "." + __fieldListClient[__loop]._fieldNameForSelect);
                }
            }

            // pack query ดึงข้อมูลทำ script
            StringBuilder __primarkCompareField = new StringBuilder();

            string __getPrimaryKey = _getPrimaryKey(targetTableName);
            if (__getPrimaryKey == null)
                __getPrimaryKey = "roworder";
            string[] __splitPK = __getPrimaryKey.Split(',');
            for (int __i = 0; __i < __splitPK.Length; __i++)
            {
                if (__primarkCompareField.Length > 0)
                {
                    __primarkCompareField.Append(" and ");
                }

                __primarkCompareField.Append(tableName + "." + __splitPK[__i] + "=" + targetTableName + "." + __splitPK[__i]);
            }

            // check pk ก่อน


            #region ทำ Delete
            // ทำ delete

            string __packDelete = MyLib._myGlobal._xmlHeader + "<node>" + MyLib._myUtil._convertTextToXmlForQuery( "delete from " + targetTableName + " where not exists(select " + __fieldForSelect + " from " + tableName + " where " + __primarkCompareField.ToString() + " )" ) + "</node>";
            string __result = __clientFrameWork._queryList(MyLib._myGlobal._databaseName, __packDelete.ToString());
            if (__result.Length == 0)
            {
                this.Invoke(_sendMessageResult, new object[] { "Delete data " + targetTableName + " success " });
            }
            else
            {
                this.Invoke(_sendMessageResult, new object[] { __result });
                _errorCount++;
            }

            /*
            string __querySelectDelete = "select " + __fieldForSelect + " from " + targetTableName + " where not exists(select " + __fieldForSelect + " from " + tableName + " where " + __primarkCompareField.ToString() + " )";
            DataSet __selectDeleteResult = __clientFrameWork._queryShort(__querySelectDelete);

            if (__selectDeleteResult.Tables.Count > 0 && __selectDeleteResult.Tables[0].Rows.Count > 0)
            {
                DataTable __getDataFromCenter = __selectDeleteResult.Tables[0];
                // pack query delete 
                StringBuilder __packDelete = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                for (int __rowData = 0; __rowData < __getDataFromCenter.Rows.Count; __rowData++)
                {
                    StringBuilder __query = new StringBuilder();
                    StringBuilder __where = new StringBuilder();
                    {
                        // update
                        __query.Append("delete from " + targetTableName + " ");
                        for (int __loop = 0; __loop < __fieldListClient.Count; __loop++)
                        {
                            if (Array.IndexOf(__splitPK, __fieldListClient[__loop]._fieldNameForInsertUpdate) != -1)
                            {
                                if (__where.Length > 0)
                                {
                                    __where.Append(" and ");
                                }

                                __where.Append(__fieldListClient[__loop]._fieldNameForInsertUpdate + " = ");
                                switch (__fieldListClient[__loop]._fieldType.ToLower())
                                {
                                    case "bytea":
                                    case "image":
                                        {
                                            string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString();
                                            __where.Append("decode(\'" + __value + "\',\'base64\')");
                                        }
                                        break;
                                    case "uuid":
                                    case "varchar":
                                        {
                                            string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                            __where.Append("\'" + __value + "\'");
                                        }
                                        break;
                                    case "date":
                                        {
                                            string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                            __where.Append((__value.Length == 0) ? "null" : "\'" + __value + "\'");
                                        }
                                        break;
                                    default: // ตัวเลข
                                        {
                                            string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                            __where.Append((__value.Length == 0) ? "0" : __value.ToString());
                                        }
                                        break;
                                }
                            }

                        }
                        //__query.Append(" where " + __whereInTable.ToString());

                        __query.Append(" where " + __where.ToString() + " ");

                    }
                    __packDelete.Append(MyLib._myUtil._convertTextToXmlForQuery(__query.ToString()));
                }
                __packDelete.Append("</node>");

                string __result = __clientFrameWork._queryList(MyLib._myGlobal._databaseName, __packDelete.ToString());
                if (__result.Length == 0)
                {
                    this.Invoke(_sendMessageResult, new object[] { "Delete data " + targetTableName + " success " });
                }
                else
                {
                    this.Invoke(_sendMessageResult, new object[] { __result });
                    _errorCount++;
                }
            }*/

            #endregion

            #region ทำ insert

            string __packInsert =  MyLib._myGlobal._xmlHeader + "<node>" + MyLib._myUtil._convertTextToXmlForQuery( "insert into " + targetTableName + "(" + __fieldForSelect + ") select " + __fieldForSelect + " from " + tableName + " where not exists(select " + __fieldForSelect + " from " + targetTableName + " where " + __primarkCompareField.ToString() + " )") + "</node>";

            __result = __clientFrameWork._queryList(MyLib._myGlobal._databaseName, __packInsert.ToString());
            if (__result.Length == 0)
            {
                this.Invoke(_sendMessageResult, new object[] { "insert data " + targetTableName + " success " });
            }
            else
            {
                this.Invoke(_sendMessageResult, new object[] { __result });
                _errorCount++;
            }

            /*
            string __querySelectInsert = "select " + __fieldForSelect + " from " + tableName + " where not exists(select " + __fieldForSelect + " from " + targetTableName + " where " + __primarkCompareField.ToString() + " )";

            DataSet __selectInsertResult = __clientFrameWork._queryShort(__querySelectInsert);

            if (__selectInsertResult.Tables.Count > 0 && __selectInsertResult.Tables[0].Rows.Count > 0)
            {
                DataTable __getDataFromCenter = __selectInsertResult.Tables[0];
                // pack query insert 
                StringBuilder __packInsert = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                for (int __rowData = 0; __rowData < __getDataFromCenter.Rows.Count; __rowData++)
                {
                    StringBuilder __query = new StringBuilder();
                    {
                        // insert
                        __query.Append("insert into " + targetTableName + " (" + __fieldForInsert + ") values (");
                        for (int __loop = 0; __loop < __fieldListClient.Count; __loop++)
                        {

                            if (__loop != 0)
                            {
                                __query.Append(",");
                            }
                            switch (__fieldListClient[__loop]._fieldType.ToLower())
                            {
                                case "bytea":
                                case "image":
                                    {
                                        string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString();
                                        __query.Append("decode(\'" + __value + "\',\'base64\')");
                                    }
                                    break;
                                case "uuid":
                                case "varchar":
                                    {
                                        string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                        __query.Append("\'" + MyLib._myGlobal._convertStrToQuery(__value) + "\'");
                                    }
                                    break;
                                case "date":
                                    {
                                        string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                        __query.Append((__value.Length == 0) ? "null" : "\'" + __value + "\'");
                                    }
                                    break;
                                default: // ตัวเลข
                                    {
                                        string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                        __query.Append((__value.Length == 0) ? "0" : __value.ToString());
                                    }
                                    break;
                            }
                        }
                        __query.Append(")");

                    }
                    __packInsert.Append(MyLib._myUtil._convertTextToXmlForQuery(__query.ToString()));
                }
                __packInsert.Append("</node>");

                string __result = __clientFrameWork._queryList(MyLib._myGlobal._databaseName, __packInsert.ToString());
                if (__result.Length == 0)
                {
                    this.Invoke(_sendMessageResult, new object[] { "insert data " + targetTableName + " success " });
                }
                else
                {
                    this.Invoke(_sendMessageResult, new object[] { __result });
                    _errorCount++;
                }

            }
            */
            #endregion

            #region ทำ Update
            // ทำ update
            string __querySelectUpdate = "select " + __fieldForSelect + " from " + tableName + " where not exists(select " + __fieldForSelect + " from " + targetTableName + " where " + __compareQuery.ToString() + " )";

            DataSet __selectUpdateResult = __clientFrameWork._queryShort(__querySelectUpdate);

            if (__selectUpdateResult.Tables.Count > 0 && __selectUpdateResult.Tables[0].Rows.Count > 0)
            {
                DataTable __getDataFromCenter = __selectUpdateResult.Tables[0];
                // pack query insert 
                StringBuilder __packUpdate = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                for (int __rowData = 0; __rowData < __getDataFromCenter.Rows.Count; __rowData++)
                {
                    StringBuilder __query = new StringBuilder();
                    StringBuilder __where = new StringBuilder();
                    {
                        // update
                        __query.Append("update " + targetTableName + " set ");
                        for (int __loop = 0; __loop < __fieldListClient.Count; __loop++)
                        {
                            if (Array.IndexOf(__splitPK, __fieldListClient[__loop]._fieldNameForInsertUpdate) != -1)
                            {
                                if (__where.Length > 0)
                                {
                                    __where.Append(" and ");
                                }

                                __where.Append(__fieldListClient[__loop]._fieldNameForInsertUpdate + " = ");
                                switch (__fieldListClient[__loop]._fieldType.ToLower())
                                {
                                    case "bytea":
                                    case "image":
                                        {
                                            string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString();
                                            __where.Append("decode(\'" + __value + "\',\'base64\')");
                                        }
                                        break;
                                    case "uuid":
                                    case "varchar":
                                        {
                                            string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                            __where.Append("\'" + __value + "\'");
                                        }
                                        break;
                                    case "date":
                                    case "timestamp" : 
                                        {
                                            string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                            __where.Append((__value.Length == 0) ? "null" : "\'" + __value + "\'");
                                        }
                                        break;
                                    default: // ตัวเลข
                                        {
                                            string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                            __where.Append((__value.Length == 0) ? "0" : __value.ToString());
                                        }
                                        break;
                                }
                            }
                            else
                            {

                                if (__loop != 0)
                                {
                                    __query.Append(",");
                                }
                                __query.Append(__fieldListClient[__loop]._fieldNameForInsertUpdate + "=");
                                switch (__fieldListClient[__loop]._fieldType.ToLower())
                                {
                                    case "bytea":
                                    case "image":
                                        {
                                            string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString();
                                            __query.Append("decode(\'" + __value + "\',\'base64\')");
                                        }
                                        break;
                                    case "uuid":
                                    case "varchar":
                                        {
                                            string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                            __query.Append("\'" + __value + "\'");
                                        }
                                        break;
                                    case "date":
                                    case "timestamp" :
                                        {
                                            string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                            __query.Append((__value.Length == 0) ? "null" : "\'" + __value + "\'");
                                        }
                                        break;
                                    default: // ตัวเลข
                                        {
                                            string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                            __query.Append((__value.Length == 0) ? "0" : __value.ToString());
                                        }
                                        break;
                                }
                            }
                        }
                        //__query.Append(" where " + __whereInTable.ToString());

                        if (__where.Length > 0)
                        {
                            __query.Append(" where " + __where.ToString() + " ");
                        }

                    }
                    __packUpdate.Append(MyLib._myUtil._convertTextToXmlForQuery(__query.ToString()));
                }
                __packUpdate.Append("</node>");

                __result = __clientFrameWork._queryList(MyLib._myGlobal._databaseName, __packUpdate.ToString());
                if (__result.Length == 0)
                {
                    this.Invoke(_sendMessageResult, new object[] { "Update data " + targetTableName + " success " });
                }
                else
                {
                    this.Invoke(_sendMessageResult, new object[] { __result });
                    _errorCount++;
                }

            }
            #endregion

        }



        string _getPrimaryKey(string tableName)
        {
            if (_xDoc == null)
            {
                MyLib._myFrameWork __datacenterFrameWork = new MyLib._myFrameWork(_global._datacenter_server, _global._datacenter_configFileName, _global._datacenter_database_type);
                DataSet __myDataSet = __datacenterFrameWork._dataStruct(MyLib._myGlobal._databaseStructFileName);

                _xDoc = new XmlDocument();
                _xDoc.LoadXml(__myDataSet.GetXml());
                _xDoc.DocumentElement.Normalize();

            }

            XmlNode __tableSelect = _getTableStruct(tableName);
            XmlElement __xTable = (XmlElement)__tableSelect;

            XmlNodeList __xIndex = __xTable.GetElementsByTagName("index");
            for (int __index = 0; __index < __xIndex.Count; __index++)
            {
                XmlNode __xReadNode = __xIndex.Item(__index);
                if (__xReadNode != null)
                {
                    if (__xReadNode.NodeType == XmlNodeType.Element)
                    {
                        // create index
                        //string __script = _createIndexScript(__xReadNode, tableName);
                        //string __result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script);
                        XmlElement __xGetField = (XmlElement)__xReadNode;
                        string __getFieldName = __xGetField.GetAttribute("field");
                        string __custer = __xGetField.GetAttribute("custer");

                        Boolean __isCuster = (__custer.ToLower().CompareTo("true") == 0) ? true : false;

                        if (__isCuster == true)
                        {
                            return __getFieldName;
                        }

                    }
                }
            } // for


            return null;

        }


        void _transFerTable(string tableName, string targetTableName)
        {
            if (this._showFullResult == true)
            {

                _appendLog("Start TransFer Table " + tableName + " " + __insertCount + " record");
                this.Invoke(_sendMessageResult, new object[] { "Start TransFer Table " + tableName + " " + __insertCount + " record" });
            }

            DataSet __ds = _getField(targetTableName);

            if (__ds.Tables.Count > 0)
            {
                // ดึงโครงสร้างฝั่ง Server
                MyLib._myFrameWork __datacenterFrameWork = new MyLib._myFrameWork(_global._datacenter_server, _global._datacenter_configFileName, _global._datacenter_database_type);
                MyLib._myFrameWork __clientFrameWork = new MyLib._myFrameWork();

                string __getXmlServre = __datacenterFrameWork._getSchemaTable(_global._datacenter_database_name, tableName);
                List<_syncFieldListStruct> __fieldListServer = SMLDataCenterSync._run._getFieldFromXml(tableName, __getXmlServre, __ds.Tables[0]);

                // ดึงโครงสร้างฝั่ง Client
                string __getXmlClient = __clientFrameWork._getSchemaTable(MyLib._myGlobal._databaseName, targetTableName);
                List<_syncFieldListStruct> __fieldListClient = SMLDataCenterSync._run._getFieldFromXml(targetTableName, __getXmlClient, __ds.Tables[0]);

                // ลบ Field ที่ไม่ตรงกัน ระหว่าง server,client
                // หาจาก field server ถ้าไม่มีใน field client ลบ field server ออก
                int __loop1 = 0;
                while (__loop1 < __fieldListServer.Count)
                {
                    Boolean __found = false;
                    for (int __loop2 = 0; __loop2 < __fieldListClient.Count && __found == false; __loop2++)
                    {
                        if (__fieldListServer[__loop1]._fieldNameForInsertUpdate.Equals(__fieldListClient[__loop2]._fieldNameForInsertUpdate))
                        {
                            __found = true;
                            break;
                        }
                    }
                    if (__found == false)
                    {
                        __fieldListServer.RemoveAt(__loop1);
                    }
                    else
                    {
                        __loop1++;
                    }
                }
                // หาใน field client ถ้าไม่มีใน field server ให้ลบ field client ออก
                __loop1 = 0;
                while (__loop1 < __fieldListClient.Count)
                {
                    Boolean __found = false;
                    for (int __loop2 = 0; __loop2 < __fieldListServer.Count && __found == false; __loop2++)
                    {
                        if (__fieldListClient[__loop1]._fieldNameForInsertUpdate.Equals(__fieldListServer[__loop2]._fieldNameForInsertUpdate))
                        {
                            __found = true;
                            break;
                        }
                    }
                    if (__found == false)
                    {
                        __fieldListClient.RemoveAt(__loop1);
                    }
                    else
                    {
                        __loop1++;
                    }
                }

                // ประกอบ field
                StringBuilder __fieldForInsert = new StringBuilder();
                StringBuilder __fieldForSelect = new StringBuilder();
                for (int __loop = 0; __loop < __fieldListClient.Count; __loop++)
                {
                    if (__fieldForSelect.Length > 0)
                    {
                        __fieldForSelect.Append(",");
                        __fieldForInsert.Append(",");
                    }
                    __fieldForSelect.Append(__fieldListClient[__loop]._fieldNameForSelect.ToString());
                    __fieldForInsert.Append(__fieldListClient[__loop]._fieldNameForInsertUpdate.ToString());
                }


                // start transer

                string __selectCount = "select count(*) as rowcount from " + tableName + " ";
                decimal __recordCount = MyLib._myGlobal._decimalPhase(__datacenterFrameWork._query(_global._datacenter_database_name, __selectCount).Tables[0].Rows[0][0].ToString());

                __clientFrameWork._query(MyLib._myGlobal._databaseName, "truncate table " + targetTableName);
                if (this._showFullResult == true)
                {

                    _appendLog("Truncate DATA " + targetTableName);
                    this.Invoke(_sendMessageResult, new object[] { "Truncate DATA " + targetTableName });
                }


                decimal __maxPage = Math.Ceiling(__recordCount / __insertCount);
                if (this._showFullResult == true)
                {

                    _appendLog("Transfer DATA " + tableName + "[ MaxPage : " + __maxPage + ", Record Count : " + __recordCount + "]");
                    this.Invoke(_sendMessageResult, new object[] { "Transfer DATA " + tableName + "[ MaxPage : " + __maxPage + ", Record Count : " + __recordCount + "]" });
                }


                // pack insert โลด
                for (int __page = 0; __page < __maxPage; __page++)
                {

                    decimal __startRecord = __page * __insertCount;
                    // 
                    DataTable __getDataFromCenter = null;
                    try
                    {
                        string __querySelect = "select " + __fieldForSelect.ToString() + " from " + tableName + " order by roworder "; ;
                        MyLib._queryReturn __dataResult = __datacenterFrameWork._queryLimit(_global._datacenter_database_name, __selectCount, __querySelect, (int)__startRecord, (int)__insertCount, (int)__recordCount);

                        if (this._showFullResult == true)
                        {

                            _appendLog("Select Page " + __page + "\n count query : " + __selectCount + "\n select query " + __querySelect + "\n Resource : start[ " + __startRecord + "] Limit : [" + __insertCount + "] All Rec [" + __recordCount + "]");
                            this.Invoke(_sendMessageResult, new object[] { "Select Page " + __page + "\n Resource : start[ " + __startRecord + "] Limit : [" + __insertCount + "] All Rec [" + __recordCount + "]" });
                        }

                        __getDataFromCenter = __dataResult.detail.Tables[0];
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(_sendMessageResult, new object[] { ex.ToString() });
                        _appendLog("Select Limit Error Page  " + __page + "");

                    }

                    if (__getDataFromCenter != null)
                    {

                        // เปรียบเทียบข้อมูล ถ้าพบข้อมูลเก่าก็ update หรือ ถ้าไม่พบก็ insert
                        StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                        for (int __rowData = 0; __rowData < __getDataFromCenter.Rows.Count; __rowData++)
                        {
                            StringBuilder __query = new StringBuilder();

                            {
                                // insert
                                __query.Append("insert into " + targetTableName + " (" + __fieldForInsert + ") values (");
                                for (int __loop = 0; __loop < __fieldListClient.Count; __loop++)
                                {
                                    if (__loop != 0)
                                    {
                                        __query.Append(",");
                                    }
                                    switch (__fieldListClient[__loop]._fieldType.ToLower())
                                    {
                                        case "bytea":
                                        case "image":
                                            {
                                                string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString();
                                                __query.Append("decode(\'" + __value + "\',\'base64\')");
                                            }
                                            break;
                                        case "uuid":
                                        case "varchar":
                                            {
                                                string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                __query.Append("\'" + MyLib._myGlobal._convertStrToQuery(__value) + "\'");
                                            }
                                            break;
                                        case "date":
                                        case "timestamp" :
                                            {
                                                string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                __query.Append((__value.Length == 0) ? "null" : "\'" + __value + "\'");
                                            }
                                            break;
                                        default: // ตัวเลข
                                            {
                                                string __value = __getDataFromCenter.Rows[__rowData][__fieldListClient[__loop]._fieldNameForInsertUpdate].ToString().Replace("\'", "\'\'");
                                                __query.Append((__value.Length == 0) ? "0" : __value.ToString());
                                            }
                                            break;
                                    }
                                }
                                __query.Append(")");

                            }
                            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__query.ToString()));
                        }
                        __queryList.Append("</node>");

                        string __result = __clientFrameWork._queryList(MyLib._myGlobal._databaseName, __queryList.ToString());
                        if (__result.Length == 0)
                        {
                            // nextpage
                            if (__result.Length == 0)
                            {
                                this.Invoke(_sendMessageResult, new object[] { "Transfer Data From : " + tableName + " success " });
                            }
                            else
                            {
                                this.Invoke(_sendMessageResult, new object[] { __result });
                                _errorCount++;
                            }

                        }
                        else
                        {
                            // eror

                        }
                    }
                    __getDataFromCenter.Dispose();
                    __getDataFromCenter = null;
                }

                // debug ดู q
            }
        }

        #region table และโครงสร้าง

        /// <summary>
        /// ดึง Field ฝั่ง Client
        /// </summary>
        /// <param name="getTableName"></param>
        /// <returns></returns>
        DataSet _getField(string getTableName)
        {
            StringBuilder __packXML = new StringBuilder();
            DataSet __ds = null;
            MyLib._myFrameWork __clientFrameWork = new MyLib._myFrameWork();
            string __schema = __clientFrameWork._getSchemaTable(MyLib._myGlobal._databaseName, getTableName);

            XmlDocument _xSchema = new XmlDocument();
            _xSchema.LoadXml(__schema);
            XmlElement __xRoot = _xSchema.DocumentElement;
            XmlNodeList __xReader = __xRoot.GetElementsByTagName("detail");

            if (__xReader.Count > 0)
            {
                __packXML.Append(MyLib._myGlobal._xmlHeader);
                __packXML.Append("<ResultSet>");

                for (int __detail = 0; __detail < __xReader.Count; __detail++)
                {
                    XmlNode __xFirstNode = __xReader.Item(__detail);
                    XmlElement __xTable = (XmlElement)__xFirstNode;
                    string __fieldName = __xTable.GetAttribute("column_name").ToLower();

                    __packXML.Append("<Row><field_name>").Append(__fieldName).Append("</field_name><default_value></default_value></Row>");
                    
                }
                __packXML.Append("</ResultSet>");

                __ds = MyLib._myGlobal._convertStringToDataSet(__packXML.ToString());

            }

            return __ds;
        }

        XmlNode _getTableStruct(string getTableName)
        {
            XmlNode __node = null;

            XmlElement __xRoot = _xDoc.DocumentElement;
            XmlNodeList __xReader = __xRoot.GetElementsByTagName("table");
            for (int __table = 0; __table < __xReader.Count; __table++)
            {
                XmlNode __xFirstNode = __xReader.Item(__table);
                if (__xFirstNode.NodeType == XmlNodeType.Element)
                {
                    XmlElement __xTable = (XmlElement)__xFirstNode;
                    if (getTableName.CompareTo(__xTable.GetAttribute("name")) == 0)
                    {
                        __node = __xFirstNode;
                        break;
                    }
                }
            }

            return __node;
        }

        string _createTableScript(XmlNode xmlField, string tableName)
        {
            StringBuilder __createQuery = new StringBuilder();

            XmlElement __xTable = (XmlElement)xmlField;

            XmlNodeList __xField = __xTable.GetElementsByTagName("field");

            __createQuery.Append("create table ").Append(tableName).Append(" (");
            Boolean __create = false;
            for (int __field = -2; __field < __xField.Count; __field++)
            {
                XmlElement __fieldElement = (__field == -1 || __field == -2) ? null : (XmlElement)__xField.Item(__field);

                String __getFieldName = (__field == -1) ? "roworder" : ((__field == -2) ? "is_lock_record" : __fieldElement.GetAttribute("name").ToLower());
                String __getType = (__field == -1 || __field == -2) ? "int" : _fieldTypeName(__fieldElement.GetAttribute("type").ToLower());
                String __getLength = (__field == -1 || __field == -2) ? "0" : __fieldElement.GetAttribute("length").ToLower();
                String __getIndentity = (__field == -1) ? "yes" : ((__field == -2) ? "no" : __fieldElement.GetAttribute("indentity").ToLower());
                String __getAllowNulls = (__field == -1) ? "false" : ((__field == -2) ? "true" : __fieldElement.GetAttribute("allow_null").ToLower());
                Boolean __getResourceOnly = (__field == -1 || __field == -2) ? false : ((__fieldElement.GetAttribute("resource_only").ToLower().CompareTo("true") == 0) ? true : false);

                if (__getResourceOnly == false)
                {
                    __create = true;
                    if (__field != -2)
                    {
                        // กรณีเป็น loop แรก ไม่ต้องใส่คอมม่า
                        __createQuery.Append(",");
                    }
                    __createQuery.Append(" ").Append(__getFieldName).Append(" ");
                    String __oldQuery = __createQuery.ToString();
                    __createQuery.Append(__getType);
                    // if (__getType.compareTo("int") == 0 || __getType.compareTo("image") == 0 || __getType.compareTo("oid") == 0 || __getType.compareTo("blob") == 0) {
                    if (__getType.CompareTo("int") == 0 || __getType.CompareTo("image") == 0 || __getType.CompareTo("bytea") == 0 || __getType.CompareTo("longblob") == 0)
                    {
                        __getLength = "0";
                    }
                    if (__getLength != null)
                    {
                        if (__getLength.CompareTo("0") != 0)
                        {
                            __createQuery.Append("(").Append(__getLength).Append(")");
                        }
                    }
                    if (__getIndentity != null)
                    {
                        if (__getIndentity.ToLower().CompareTo("yes") == 0)
                        {
                            switch (_global._datacenter_database_type)
                            {
                                case MyLib._myGlobal._databaseType.PostgreSql:
                                    // PostgreSql
                                    __createQuery = new StringBuilder("");
                                    __createQuery.Append(__oldQuery);
                                    __createQuery.Append(" serial ");
                                    break;
                                case MyLib._myGlobal._databaseType.MySql:
                                    __createQuery.Append(" AUTO_INCREMENT PRIMARY key ");
                                    __getAllowNulls = "false";
                                    //  __createQuery.Append(" default '1'");//somruk
                                    break;
                                case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                                case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                                    // Microsoft SQL
                                    __createQuery.Append(" IDENTITY (1,1) ");
                                    break;
                            }
                        }
                    }
                    if (__getAllowNulls != null)
                    {
                        if (__getAllowNulls.ToLower().CompareTo("false") == 0)
                        {
                            __createQuery.Append(" NOT NULL ");
                        }
                        if (__getAllowNulls.ToLower().CompareTo("true") == 0)
                        {
                            __createQuery.Append(" NULL ");
                        }
                    }
                    else
                    {
                        __createQuery.Append(" NULL ");
                    }
                }
            }
            switch (_global._datacenter_database_type)
            {
                case MyLib._myGlobal._databaseType.PostgreSql:
                    // PostgreSQL
                    __createQuery.Append(") ");
                    break;
                case MyLib._myGlobal._databaseType.MySql:
                    // MySQL
                    __createQuery.Append(") ENGINE = InnoDB DEFAULT CHARACTER SET = tis620 COLLATE = tis620_thai_ci;");
                    break;
                case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                    // Microsoft SQL
                    __createQuery.Append(") ON [PRIMARY]");
                    break;
            }
            String __result = "";

            return __createQuery.ToString();
        }

        private String _fieldTypeName(String fieldTypeName)
        {
            String __getType = fieldTypeName.ToLower();
            if (fieldTypeName.Equals("float"))
            {
                switch (_global._datacenter_database_type)
                {
                    case MyLib._myGlobal._databaseType.PostgreSql:
                        // PostgreSQL
                        __getType = "numeric";
                        break;
                    case MyLib._myGlobal._databaseType.MySql:
                        // MySQL
                        __getType = "float";
                        break;
                }
            }
            else if (fieldTypeName.Equals("smalldatetime"))
            {
                switch (_global._datacenter_database_type)
                {
                    case MyLib._myGlobal._databaseType.PostgreSql:
                        // PostgreSQL
                        __getType = "date";
                        break;
                    case MyLib._myGlobal._databaseType.MySql:
                        // MySQL
                        __getType = "datetime";
                        break;
                }
            }
            else if (fieldTypeName.Equals("varchar"))
            {
                switch (_global._datacenter_database_type)
                {
                    case MyLib._myGlobal._databaseType.PostgreSql:
                        // PostgreSQL
                        __getType = "character varying";
                        break;
                }
            }
            else if (fieldTypeName.Equals("tinyint"))
            {
                switch (_global._datacenter_database_type)
                {
                    case MyLib._myGlobal._databaseType.PostgreSql:
                    case MyLib._myGlobal._databaseType.MySql:
                    case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                    case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                        // PostgreSQL
                        __getType = "smallint";
                        break;
                }
            }
            else if (fieldTypeName.Equals("datetime"))
            {
                switch (_global._datacenter_database_type)
                {
                    case MyLib._myGlobal._databaseType.PostgreSql:
                        // PostgreSQL
                        __getType = "timestamp without time zone";
                        break;
                }
            }
            else if (fieldTypeName.Equals("image"))
            {
                switch (_global._datacenter_database_type)
                {
                    case MyLib._myGlobal._databaseType.PostgreSql:
                        // PostgreSQL
                        // __getType = "oid";
                        __getType = "bytea";
                        break;
                    case MyLib._myGlobal._databaseType.MySql:
                        // MySQL
                        __getType = "longblob";
                        break;
                }
                //MOO
            }
            else if (fieldTypeName.Equals("text"))
            {
                switch (_global._datacenter_database_type)
                {
                    case MyLib._myGlobal._databaseType.PostgreSql:
                        // PostgreSQL
                        // __getType = "oid";
                        __getType = "text";
                        break;
                    case MyLib._myGlobal._databaseType.MySql:
                        // MySQL
                        __getType = "text";
                        break;
                }

            }
            else if (fieldTypeName.Equals("currency"))
            {
                if (_global._datacenter_database_type == MyLib._myGlobal._databaseType.MicrosoftSQL2000 || _global._datacenter_database_type == MyLib._myGlobal._databaseType.MicrosoftSQL2005)
                {
                    __getType = "money";
                }
            }
            return __getType;
        }

        void _checkIndex(XmlNode xmlField, string tableName)
        {
            // อย่าลืม check index

            StringBuilder __query = new StringBuilder();
            MyLib._myFrameWork __clientFrameWork = new MyLib._myFrameWork();

            XmlElement __xTable = (XmlElement)xmlField;

            XmlNodeList __xIndex = __xTable.GetElementsByTagName("index");
            for (int __index = 0; __index < __xIndex.Count; __index++)
            {
                XmlNode __xReadNode = __xIndex.Item(__index);
                if (__xReadNode != null)
                {
                    if (__xReadNode.NodeType == XmlNodeType.Element)
                    {
                        // create index
                        string __script = _createIndexScript(__xReadNode, tableName);
                        string __result = __clientFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __script);
                    }
                }
            } // for


            //return __query.ToString();
        }

        string _createIndexScript(XmlNode xmlField, string tableName)
        {
            StringBuilder __createQuery = new StringBuilder();

            XmlElement __xGetField = (XmlElement)xmlField;

            string __getIndexName = __xGetField.GetAttribute("index_name");
            string __getFieldName = __xGetField.GetAttribute("field");
            string __custer = __xGetField.GetAttribute("custer");
            string __getUnique = __xGetField.GetAttribute("unique");

            Boolean __isCuster = (__custer.ToLower().CompareTo("true") == 0) ? true : false;
            Boolean __isUnique = (__getUnique.ToLower().CompareTo("true") == 0) ? true : false;

            __getIndexName = __getIndexName + "_idx_temp";

            if (__isCuster == true)
            {
                switch (_global._datacenter_database_type)
                {
                    case MyLib._myGlobal._databaseType.PostgreSql:
                        // PostgreSQL
                        //__createQuery.Append("alter table " + tableName + " add CONSTRAINT " + __getIndexName + " primary key (" + __getFieldName + "); CREATE INDEX "+__myIndex+" ON "+tableName+" ("+__getFieldName+");");
                        __createQuery.Append("alter table ").Append(tableName).Append(" add CONSTRAINT ").Append(__getIndexName).Append(" primary key (").Append(__getFieldName).Append(");");
                        break;
                    case MyLib._myGlobal._databaseType.MySql:
                        // MySQL
                        __createQuery.Append("alter table ").Append(tableName).Append(" add ").Append(__getUnique).Append(" ").Append(__getIndexName).Append(" (").Append(__getFieldName).Append(")");
                        break;
                    case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                    case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                        // Microsoft SQL
                        __createQuery.Append("ALTER TABLE ").Append(tableName).Append(" WITH NOCHECK ADD CONSTRAINT ").Append(__getIndexName).Append(" PRIMARY KEY  CLUSTERED (").Append(__getFieldName).Append(")  ON [PRIMARY]");
                        break;
                }
            }
            if (__isCuster == false && __isUnique == true)
            {

                switch (_global._datacenter_database_type)
                {
                    case MyLib._myGlobal._databaseType.PostgreSql:
                        // PostgreSQL
                        //__createQuery.Append(" create " + __getUnique + " index " + __getIndexName + " on " + tableName + " (" + __getFieldName + "),ENGINE = InnoDB;");
                        __createQuery.Append(" create ").Append(__getUnique).Append(" index ").Append(__getIndexName).Append(" on ").Append(tableName).Append(" (").Append(__getFieldName).Append(");");
                        break;
                    case MyLib._myGlobal._databaseType.MySql:
                        // MySQL
                        __createQuery.Append("alter table ").Append(tableName).Append(" add ").Append(__getUnique).Append(" ").Append(__getIndexName).Append(" (").Append(__getFieldName).Append(")");
                        break;
                    case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                    case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                        // Microsoft SQL
                        __createQuery.Append(" create ").Append(__getUnique).Append(" CLUSTERED index ").Append(__getIndexName).Append(" ON ").Append(tableName).Append(" (").Append(__getFieldName).Append(") ON [PRIMARY]");
                        break;
                }
            }


            if (__isCuster == false && __isUnique == false)
            {
                switch (_global._datacenter_database_type)
                {
                    case MyLib._myGlobal._databaseType.PostgreSql:
                        // PostgreSQL
                        //__createQuery.Append(" create " + __getUnique + " index " + __getIndexName + " on " + tableName + " (" + __getFieldName + "),ENGINE = InnoDB;");
                        __createQuery.Append(" CREATE INDEX ").Append(__getIndexName).Append(" ON ").Append(tableName).Append(" (").Append(__getFieldName).Append(");");
                        break;
                    case MyLib._myGlobal._databaseType.MySql:
                        // MySQL
                        if (__createQuery.ToString().Length > 0)
                        {
                            __createQuery.Append(" , add  index ").Append(__getIndexName).Append(" (").Append(__getFieldName).Append(")");
                        }
                        else
                        {
                            __createQuery.Append(" alter table ").Append(tableName).Append(" add  index ").Append(__getIndexName).Append(" (").Append(__getFieldName).Append(")");
                        }

                        break;
                    case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                    case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                        // Microsoft SQL

                        // __createQuery.Append(" create " + __getUnique + " CLUSTERED index " + __getIndexName + " ON " + tableName + " (" + __getFieldName + ") ON [PRIMARY]");
                        break;
                }
            }

            return __createQuery.ToString();
        }

        #endregion

        Thread _dataSyncThread;
        private void _processToolStripButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ยืนยันการถ่ายโอนข้อมูล", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this._showFullResult = this._fullTransferResultCheckbox.Checked;

                if (_dataSyncThread == null || _dataSyncThread.IsAlive == false)
                {
                    _errorCount = 0;
                    _resultTextBox.Text = "";

                    _dataSyncThread = new Thread(new ThreadStart(_process));
                    _dataSyncThread.IsBackground = true;
                    _dataSyncThread.Start();
                }
                else
                {
                    MessageBox.Show("Process is Running...", "Warning", MessageBoxButtons.YesNo);
                }
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            try
            {
                //_dataSyncThread.Abort();
            }
            catch
            {

            }
            this.Dispose();
        }

        string _saveLogFileName = "C:\\smlsoft\\transferLog.txt";

        void _appendLog(string message)
        {
            _appendLog(message, false);
        }

        void _appendLog(string message, bool clearLog)
        {
            if (clearLog)
            {

                try
                {
                    // append log
                    FileStream aFile = new FileStream(this._saveLogFileName, FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(aFile);
                    sw.WriteLine(DateTime.Now.ToString() + " " + message);
                    sw.Close();
                    aFile.Close();

                }
                catch
                {
                }
            }
            else
            {

                try
                {
                    // append log
                    FileStream aFile = new FileStream(this._saveLogFileName, FileMode.Append, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(aFile);
                    sw.WriteLine("");
                    sw.WriteLine(DateTime.Now.ToString() + " " + message);
                    sw.Close();
                    aFile.Close();

                }
                catch
                {
                }
            }
        }

        private void _removeAllButton_Click(object sender, EventArgs e)
        {
            for (int __i = 0; __i < this.__tableList.Count; __i++)
            {
                this._myGrid1._cellUpdate(__i, 0, 0, false);
            }

        }

        private void _selectAllButton_Click(object sender, EventArgs e)
        {
            for (int __i = 0; __i < this.__tableList.Count; __i++)
            {
                this._myGrid1._cellUpdate(__i, 0, 1, false);
            }

        }
    }
}
