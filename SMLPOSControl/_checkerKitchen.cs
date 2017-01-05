using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSControl
{
    public partial class _checkerKitchen : UserControl
    {
        private string _fieldBarcode = "barcode";
        private string _fieldTable = "โต๊ะ";
        private string _fieldItemName = "ชื่ออาหาร";
        private string _fieldItemUnit = "หน่วยนับ";
        private string _fieldItemQty = "จำนวนทำ";
        private string _fieldItemTotalQty = "จำนวนสั่ง";
        private string _fieldItemOrderTime = "เวลาสั่ง";
        private string _fieldItemCheckTime = "เวลาออก";
        private string _fieldItemCalcTime = "ใช้เวลา";
        private string _fieldDocNo = "เลขที่ใบสั่ง";
        private string _fieldKitchen = "ครัว";
        private string _formatNumber = "###,###";
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private string _orderByWait = "order by doc_date,doc_time";
        private string _orderByLast = "order by cofirm_date desc,confirm_time desc";
        private _g._checkerDeviceXMLConfig _config;
        private bool _actived = false;
        private bool _warningActived = false;

        public _checkerKitchen()
        {
            InitializeComponent();
            //
            this._lastGrid._fixFont = new System.Drawing.Font(this._lastGrid.Font.FontFamily, this._lastGrid.Font.Size);
            this._lastGrid._table_name = "";
            this._lastGrid._addColumn(this._fieldTable, 1, 5, 5);
            this._lastGrid._addColumn(this._fieldItemName, 1, 10, 20);
            this._lastGrid._addColumn(this._fieldItemUnit, 1, 5, 5);
            this._lastGrid._addColumn(this._fieldItemQty, 3, 10, 10, false, false, false, false, _formatNumber);
            this._lastGrid._addColumn(this._fieldItemTotalQty, 3, 10, 10, false, false, false, false, _formatNumber);
            this._lastGrid._addColumn(this._fieldItemOrderTime, 1, 5, 5);
            this._lastGrid._addColumn(this._fieldItemCheckTime, 1, 5, 5);
            this._lastGrid._addColumn(this._fieldItemCalcTime, 1, 5, 5);
            this._lastGrid._addColumn(this._fieldDocNo, 1, 10, 10);
            this._lastGrid._addColumn(this._fieldKitchen, 1, 5, 5);
            this._lastGrid._addColumn(this._fieldBarcode, 1, 1, 1);
            this._lastGrid._calcPersentWidthToScatter();
            //
            this._waitGrid._fixFont = new System.Drawing.Font(this._waitGrid.Font.FontFamily, this._waitGrid.Font.Size);
            this._waitGrid._table_name = "";
            this._waitGrid._addColumn(this._fieldTable, 1, 5, 5);
            this._waitGrid._addColumn(this._fieldItemName, 1, 10, 30);
            this._waitGrid._addColumn(this._fieldItemUnit, 1, 10, 10);
            this._waitGrid._addColumn(this._fieldItemQty, 3, 5, 5, false, false, false, false, _formatNumber);
            this._waitGrid._addColumn(this._fieldItemTotalQty, 3, 5, 5, false, false, false, false, _formatNumber);
            this._waitGrid._addColumn(this._fieldItemOrderTime, 1, 5, 5);
            this._waitGrid._addColumn(this._fieldItemCalcTime, 1, 5, 5);
            this._waitGrid._addColumn(this._fieldDocNo, 1, 10, 10);
            this._waitGrid._addColumn(this._fieldKitchen, 1, 5, 5);
            this._waitGrid._addColumn(this._fieldBarcode, 1, 1, 1);
            this._waitGrid._calcPersentWidthToScatter();
            //
            this._barcodeTextBox.KeyDown += _barcodeTextBox_KeyDown;
            this._loadDataToGrid(this._lastGrid, 1, this._orderByLast, false);
            this._loadDataToGrid(this._waitGrid, 0, this._orderByWait, true);
            this._timer.Tick += _timer_Tick;
        }

        void _loadConfig()
        {
            try
            {
                string __localpath = MyLib._myGlobal._smlConfigFile + _g.g._checkerXmlFileName;
                TextReader __readFile = new StreamReader(__localpath);
                XmlSerializer __xsLoad = new XmlSerializer(typeof(_g._checkerDeviceXMLConfig));
                this._config = (_g._checkerDeviceXMLConfig)__xsLoad.Deserialize(__readFile);
                __readFile.Close();
            }
            catch
            {
            }
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            if (this._actived)
            {
                this._loadDataToGrid(this._waitGrid, 0, this._orderByWait, true);
                // play sound
                try
                {
                    StringBuilder __kitchenList = new StringBuilder();
                    // toe fix bug 
                    if (this._config != null)
                    {
                        for (int __loop = 0; __loop < this._config._kitchenCode.Count; __loop++)
                        {
                            if (__kitchenList.Length > 0)
                            {
                                __kitchenList.Append(",");
                            }
                            __kitchenList.Append("\'" + this._config._kitchenCode[__loop] + "\'");
                        }
                    }
                    DateTime __date = DateTime.Now.AddDays(-2);
                    string __dateStr = __date.Year.ToString() + "-" + __date.Month.ToString() + "-" + __date.Day.ToString();
                    List<String> __fieldList = new List<string>();
                    __fieldList.Add("item_code");
                    __fieldList.Add("table_number");
                    __fieldList.Add("unit_code");
                    __fieldList.Add("remark");
                    __fieldList.Add("price");
                    __fieldList.Add("qty_balance");
                    __fieldList.Add("doc_time");
                    __fieldList.Add("last_status");
                    __fieldList.Add("isspeech");
                    __fieldList.Add("kitchen_code");
                    StringBuilder __query = new StringBuilder();
                    __query.Append("select roworderx,item_name||' '||remark as item_name,order_qty,unit_code from (");
                    __query.Append("select *,");
                    __query.Append("(select name_1 from ic_inventory where ic_inventory.code=item_code limit 1) as item_name ");
                    __query.Append(" from ");
                    __query.Append("(select *");
                    for (int __loop = 0; __loop < __fieldList.Count; __loop++)
                    {
                        __query.Append(",(select " + __fieldList[__loop] + " from table_order where table_order.guid_line=order_checker.guid_line) as " + __fieldList[__loop]);
                    }
                    __query.Append(",(select roworder from table_order where table_order.guid_line=order_checker.guid_line) as roworderx");
                    __query.Append(" from order_checker where doc_date > \'" + __dateStr + "\'");
                    __query.Append(" ) as t1");
                    // toe fix error ก่อน
                    __query.Append(" where last_status in (0,2) and (isspeech is null or isspeech=0) ");
                    if (__kitchenList.Length > 0)
                    {
                        __query.Append(" and kitchen_code in (" + __kitchenList.ToString() + ") ");
                    }
                    __query.Append(") as t2");
                    DataTable __data = this._myFrameWork._queryShort(__query.ToString()).Tables[0];
                    List<_speechListStuct> __speechList = new List<_speechListStuct>();
                    StringBuilder __rowOrder = new StringBuilder();
                    for (int __row = 0; __row < __data.Rows.Count; __row++)
                    {
                        if (__rowOrder.Length > 0)
                        {
                            __rowOrder.Append(",");
                        }
                        __rowOrder.Append(__data.Rows[__row]["roworderx"].ToString());
                        Boolean __found = false;
                        string __getItemName = __data.Rows[__row]["item_name"].ToString();
                        string __getUnitName = __data.Rows[__row]["unit_code"].ToString();
                        float __getQty = float.Parse(__data.Rows[__row]["order_qty"].ToString());
                        for (int __find = 0; __find < __speechList.Count; __find++)
                        {
                            if (__speechList[__find]._itemName.Equals(__getItemName) && __speechList[__find]._itemUnit.Equals(__getUnitName))
                            {
                                __speechList[__find]._itemQty = __speechList[__find]._itemQty + __getQty;
                                __found = true;
                                break;
                            }
                        }
                        if (__found == false)
                        {
                            _speechListStuct __newData = new _speechListStuct();
                            __newData._itemName = __getItemName;
                            __newData._itemUnit = __getUnitName;
                            __newData._itemQty = __getQty;
                            __speechList.Add(__newData);
                        }
                    }
                    for (int __loop = 0; __loop < __speechList.Count; __loop++)
                    {
                        if (__loop == 0)
                        {
                            MyLib._googleSound._play("ออเด้อมาใหม่");
                        }
                        int __count = __loop + 1;
                        MyLib._googleSound._play(__count.ToString());
                        MyLib._googleSound._play(__speechList[__loop]._itemName);
                        if (__speechList[__loop]._itemQty > 1)
                        {
                            MyLib._googleSound._play("จำนวน");
                            MyLib._googleSound._play(__speechList[__loop]._itemQty.ToString());
                            MyLib._googleSound._play(__speechList[__loop]._itemUnit);
                        }
                    }
                    if (__rowOrder.ToString().Trim().Length > 0)
                    {
                        // update
                        _myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + _g.d.table_order._table + " set " + _g.d.table_order._isspeech + "=1 where roworder in (" + __rowOrder.ToString() + ")");
                    }
                }
                catch
                {
                }
            }
        }

        private string _createQuery(string barcode, int status, string order)
        {
            StringBuilder __kitchenList = new StringBuilder();
            // toe fix bug 
            if (this._config != null)
            {
                for (int __loop = 0; __loop < this._config._kitchenCode.Count; __loop++)
                {
                    if (__kitchenList.Length > 0)
                    {
                        __kitchenList.Append(",");
                    }
                    __kitchenList.Append("\'" + this._config._kitchenCode[__loop] + "\'");
                }
            }
            DateTime __date = DateTime.Now.AddDays(-2);
            string __dateStr = __date.Year.ToString() + "-" + __date.Month.ToString() + "-" + __date.Day.ToString();
            List<String> __fieldList = new List<string>();
            __fieldList.Add("item_code");
            __fieldList.Add("table_number");
            __fieldList.Add("unit_code");
            __fieldList.Add("remark");
            __fieldList.Add("price");
            __fieldList.Add("qty_balance");
            __fieldList.Add("doc_time");
            __fieldList.Add("last_status");
            __fieldList.Add("kitchen_code");
            StringBuilder __query = new StringBuilder();
            __query.Append("select *,(select name_1 from ic_inventory where ic_inventory.code=item_code limit 1) as item_name,");
            __query.Append("coalesce((select production_period from ic_inventory where ic_inventory.code=item_code limit 1),0) as production_period ");
            __query.Append(" from ");
            __query.Append("(select *");
            for (int __loop = 0; __loop < __fieldList.Count; __loop++)
            {
                __query.Append(",(select " + __fieldList[__loop] + " from table_order where table_order.guid_line=order_checker.guid_line) as " + __fieldList[__loop]);
            }
            __query.Append(" from order_checker where doc_date > \'" + __dateStr + "\'");
            if (barcode.Length == 0)
            {
                __query.Append(" and coalesce(confirm_status, 0)=" + status.ToString() + " " + order + " ) as t1");
            }
            else
            {
                __query.Append(" and guid_confirm=\'" + barcode + "\' limit 1) as t1");
            }
            // toe fix error ก่อน
            if (__kitchenList.Length > 0)
            {
                __query.Append(" where kitchen_code in (" + __kitchenList.ToString() + ") and last_status in (0,2)");
            }

            return __query.ToString();
        }

        private void _loadDataToGrid(object grid, int status, string order, Boolean now)
        {
            this._loadConfig();
            MyLib._myGrid __grid = (MyLib._myGrid)grid;
            __grid._clear();
            DataTable __data = this._myFrameWork._queryShort(this._createQuery("", status, order)).Tables[0];
            for (int __rowLoop = 0; __rowLoop < __data.Rows.Count; __rowLoop++)
            {
                DataRow __row = __data.Rows[__rowLoop];
                string __barCode = __row["guid_confirm"].ToString();
                string __itemName = __row["item_name"].ToString();
                string __remark = __row["remark"].ToString();
                string __unit = __row["unit_code"].ToString();
                string __tableNumber = __row["table_number"].ToString();
                string __docDate = __row["doc_date"].ToString();
                string __docTime = __row["doc_time"].ToString();
                string __confirmDate = (now) ? DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() : __row["cofirm_date"].ToString();
                string __confirmTime = (now) ? String.Format("{0:00}", DateTime.Now.Hour) + ":" + String.Format("{0:00}", DateTime.Now.Minute) : __row["confirm_time"].ToString();
                string __docNo = __row["doc_no"].ToString();
                string __kitchenCode = __row["kitchen_code"].ToString();
                decimal __qty = MyLib._myGlobal._decimalPhase(__row["order_qty"].ToString());
                decimal __qtyBalance = MyLib._myGlobal._decimalPhase(__row["qty_balance"].ToString());
                decimal __price = MyLib._myGlobal._decimalPhase(__row["price"].ToString());
                int __addr = __grid._addRow();
                __grid._cellUpdate(__addr, this._fieldTable, __tableNumber, false);
                __grid._cellUpdate(__addr, this._fieldItemName, __itemName + __remark, false);
                __grid._cellUpdate(__addr, this._fieldItemUnit, __unit, false);
                __grid._cellUpdate(__addr, this._fieldItemQty, __qty, false);
                __grid._cellUpdate(__addr, this._fieldItemTotalQty, __qtyBalance, false);
                __grid._cellUpdate(__addr, this._fieldItemOrderTime, __docTime, false);
                __grid._cellUpdate(__addr, this._fieldItemCheckTime, __confirmTime, false);
                __grid._cellUpdate(__addr, this._fieldBarcode, __barCode, false);
                //
                string __orderDateStr = __docDate.Split(' ')[0].ToString() + " " + __docTime;
                string __confirmDateStr = __confirmDate.Split(' ')[0].ToString() + " " + __confirmTime;
                DateTime __dateStart = DateTime.Parse(__orderDateStr);
                DateTime __dateStop = DateTime.Parse(__confirmDateStr);
                TimeSpan __diff = __dateStop.Subtract(__dateStart);
                __grid._cellUpdate(__addr, this._fieldItemCalcTime, String.Format("{0:00}", __diff.Hours) + ":" + String.Format("{0:00}", __diff.Minutes), false);
                //
                __grid._cellUpdate(__addr, this._fieldDocNo, __docNo, false);
                __grid._cellUpdate(__addr, this._fieldKitchen, __kitchenCode, false);
            }
            __grid.Invalidate();
        }

        private void _barcodeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this._webBrowser.DocumentText = "";
                string __barcode = this._barcodeTextBox.Text.Trim().Replace(" ", "");
                if (__barcode.Length > 0)
                {
                    try
                    {
                        if (__barcode.Length > 0 && __barcode.Length < 5)
                        {
                            int __lineNumber = -1;
                            try
                            {
                                __lineNumber = int.Parse(__barcode) - 1;
                            }
                            catch
                            {
                            }
                            if (__lineNumber >= 0 && __lineNumber < this._waitGrid._rowData.Count)
                            {
                                __barcode = (string)this._waitGrid._cellGet(__lineNumber, this._fieldBarcode);
                            }
                        }
                    }
                    catch
                    {
                    }
                    if (__barcode.Length > 5)
                    {
                        DataTable __data = this._myFrameWork._queryShort(this._createQuery(__barcode, 0, "")).Tables[0];
                        if (__data.Rows.Count > 0)
                        {
                            StringBuilder __webData = new StringBuilder();
                            DataRow __row = __data.Rows[0];
                            string __barCode = __row["guid_confirm"].ToString();
                            string __itemName = __row["item_name"].ToString();
                            string __remark = __row["remark"].ToString();
                            string __unit = __row["unit_code"].ToString();
                            string __tableNumber = __row["table_number"].ToString();
                            decimal __qtyBalance = MyLib._myGlobal._decimalPhase(__row["qty_balance"].ToString());
                            decimal __price = MyLib._myGlobal._decimalPhase(__row["price"].ToString());
                            __webData.Append("ชื่อ : " + __itemName + " " + __remark + " หน่วยนับ : " + __unit + "<br/>");
                            __webData.Append("โต๊ะ : " + __tableNumber + " ");
                            __webData.Append("จำนวน : " + __qtyBalance + " ");
                            __webData.Append("ราคา : " + __price + "<br/>");
                            this._webBrowser.DocumentText = __webData.ToString();
                            //
                            string __dateConfirm = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                            string __timeConfirm = String.Format("{0:00}", DateTime.Now.Hour) + ":" + String.Format("{0:00}", DateTime.Now.Minute) + ":" + String.Format("{0:00}", DateTime.Now.Second);
                            this._myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update order_checker set confirm_status=1,cofirm_date=\'" + __dateConfirm + "\',confirm_time=\'" + __timeConfirm + "\' where guid_confirm=\'" + __barCode + "\'");
                            //
                            this._loadDataToGrid(this._lastGrid, 1, this._orderByLast, false);
                            this._loadDataToGrid(this._waitGrid, 0, this._orderByWait, true);
                        }
                    }
                }
                this._barcodeTextBox.Text = "";
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _lastGrid_Load(object sender, EventArgs e)
        {

        }

        private void _timer_focus_Tick(object sender, EventArgs e)
        {
            this._actived = (_g.g._tabControl.SelectedTab.Name.IndexOf("menu_food_checker") != -1);
            if (this._actived)
            {
                this.ActiveControl = this._barcodeTextBox;
            }
        }

        class _speechListStuct
        {
            public string _itemName = "";
            public string _itemUnit = "";
            public float _itemQty = 0f;
        }

        private void _timer_warning_Tick(object sender, EventArgs e)
        {
            // เตือนอาหารช้า
            if (this._warningActived == false)
            {
                this._warningActived = true;
                this._timer_warning.Stop();
                if (MyLib._googleSound._soundList.Count == 0)
                {
                    DataTable __data = this._myFrameWork._queryShort(this._createQuery("", 0, this._orderByWait)).Tables[0];
                    if (__data.Rows.Count > 0)
                    {
                        int __count = 0;
                        for (int __loop = 0; __loop < __data.Rows.Count && __count < 4; __loop++)
                        {
                            DataRow __row = __data.Rows[__loop];
                            string __getItemName = __row["item_name"].ToString();
                            string __docDate = __row["doc_date"].ToString();
                            string __docTime = __row["doc_time"].ToString();
                            string __confirmDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                            string __confirmTime = String.Format("{0:00}", DateTime.Now.Hour) + ":" + String.Format("{0:00}", DateTime.Now.Minute);
                            string __orderDateStr = __docDate.Split(' ')[0].ToString() + " " + __docTime;
                            string __confirmDateStr = __confirmDate.Split(' ')[0].ToString() + " " + __confirmTime;
                            DateTime __dateStart = DateTime.Parse(__orderDateStr);
                            DateTime __dateStop = DateTime.Parse(__confirmDateStr);
                            TimeSpan __diff = __dateStop.Subtract(__dateStart);
                            int __itemMinute = Int32.Parse(__row["production_period"].ToString());
                            if (__diff.TotalMinutes > __itemMinute)
                            {
                                __count++;
                                MyLib._googleSound._play("เตือน");
                                MyLib._googleSound._play("อาหารช้า");
                                MyLib._googleSound._play(__getItemName);
                                MyLib._googleSound._play(__diff.TotalMinutes.ToString());
                                MyLib._googleSound._play("นาที");
                            }
                        }
                    }
                }
                this._timer_warning.Start();
                this._warningActived = false;
            }
        }
    }
}
