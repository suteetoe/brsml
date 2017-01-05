using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Collections.ObjectModel;
using System.Threading;
using System.Globalization;
using SMLICIAdmin.excelObject;


namespace SMLICIAdmin
{
    public partial class _importFcolorDataControlScreenfile : UserControl
    {

        private StringBuilder __message = new StringBuilder();
        _excelClass __excel;
        private StringBuilder __messagefile = new StringBuilder();
        //DataTable __searchdt = null;
        ArrayList _listTable = new ArrayList();
        bool _isReadxmlsucess = false;
        string __fliename = "";
        public String _processMessage = "";
        double __valuePercent = 0.0;
        Thread _loadDataThreadChecking = null;


        public _importFcolorDataControlScreenfile()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(_importFcolorDataControl_Disposed);
            _cancelThread();
            //

        }
        void _cancelThread()
        {
            if (_loadDataThreadChecking != null)
            {

                try
                {
                    Thread.Sleep(100);
                    _loadDataThreadChecking.Abort();
                }
                catch { };
            }

        }


        void _importFcolorDataControl_Disposed(object sender, EventArgs e)
        {

            _cancelThread();
        }

        private void _selectFileButton1_Click(object sender, EventArgs e)
        {
            try
            {
                _cancelThread();
                OpenFileDialog __file = new OpenFileDialog();
                __file.Title = "Select Text File";
                __file.Multiselect = false;
                __file.Filter = "xml files (*.xml)|*.xml";
                if (__file.ShowDialog() == DialogResult.OK)
                {
                    this._textFileTextBox.Text = __file.FileName.ToString();
                    this.__progressBar.Value = 0;
                    //this._processMessage = "Build Data to Grid";
                    __messagefile = new StringBuilder();
                    __message.Append("<<<<<----- Running Process Chicking file ----->>>>>").AppendLine(); ;
                    __message.Append("From Xml file name " + __file.FileName).AppendLine(); ;
                    __messagefile.Append("<<<<<----- Running Process Chicking file ----->>>>>").AppendLine();
                    __messagefile.Append("From Xml file name " + __file.FileName).AppendLine();
                    __fliename = __file.FileName;
                    _dataGridView.DataSource = new DataTable();
                    this._isReadxmlsucess = true;

                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        public delegate void LabelProgressDelegate(string value);
        public void SetLabelProgressText(string value)
        {
            if (lblProgress.InvokeRequired)
            {
                lblProgress.Invoke(new LabelProgressDelegate(SetLabelProgressText), new object[] { value });
            }
            else
            {
                Application.DoEvents();
                lblProgress.Text = value;
            }
        }
        public delegate void ProgressDelegate(int maxValue, int minValue, int realValue);
        public void SetProgressValue(int maxValue, int minValue, int realValue)
        {
            if (this.__progressBar.InvokeRequired)
            {
                __progressBar.Invoke(new ProgressDelegate(SetProgressValue), new object[] { maxValue, minValue, realValue });
            }
            else
            {
                Application.DoEvents();
                __progressBar.Maximum = maxValue;
                __progressBar.Minimum = minValue;
                __progressBar.Value = realValue;
            }
        }
        public delegate void gridDelegate(DataTable data);
        public void SetGridValue(DataTable data)
        {
            if (this._dataGridView.InvokeRequired)
            {
                _dataGridView.Invoke(new gridDelegate(SetGridValue), new object[] { data });
            }
            else
            {
                Application.DoEvents();
                _dataGridView.DataSource = data;
            }
        }

        /// <summary>
        /// หน่วยต้นทุน
        /// </summary>
        /// <param name="code"></param>
        /// <param name="dt"></param>
        /// <returns></returns>



        string _substrunitcode(string sub)
        {

            string __result = "0";

            if (sub.Length > 0)
            {
                __result = sub.Substring(0, sub.Length - 1);
            }
            return __result;
        }




        void __log(string message)
        {
            try
            {
                CultureInfo _ci_en = new CultureInfo("en-US");
                // CultureInfo _ci_th = new CultureInfo("th-TH");                
                IFormatProvider __cultureEN = new CultureInfo("en-US");
                DateTime __dt = DateTime.Now;
                string __strDate = __dt.ToString("d MMMM yyyy", __cultureEN);
                string __strTime = __dt.ToString("HH:mm:ss", __cultureEN);

                FileInfo __file = new FileInfo(__fliename);
                string __newfilename = __file.Name;
                __newfilename = __fliename.Replace(__newfilename, __file.Name.Replace(".xml", "CheckingFile.txt"));
                FileInfo __filecreate = new FileInfo(__newfilename);

                // if (!System.IO.File.Exists(__fliename))
                // {
                StreamWriter __sw = __filecreate.CreateText();
                __sw.WriteLine("Date " + __strDate + " Time " + __strTime);
                __sw.WriteLine(message);
                __sw.Close();
                //  }

            }
            catch (Exception __ex)
            {
                Console.WriteLine(__ex.Message);
            }
        }



        private int _getAttributeToInt(XmlNode node, XmlAttributeCollection attribute, string name)
        {
            if (attribute[name] != null)
            {
                return Int32.Parse(node.Attributes[name].Value);
            }
            return 0;
        }

        void _buttonProcessk()
        {
            DataTable __dataTable = new DataTable("Data");
            SetProgressValue(100, 0, 0);
            SetLabelProgressText("Please Wait");

            try
            {

                ArrayList __rowArrayList = new ArrayList();
                XmlDocument __xmlDocumnet = new XmlDocument();
                __xmlDocumnet.Load(_textFileTextBox.Text);
                XmlNode __root = __xmlDocumnet["Workbook"]["Worksheet"];
                XmlNode __table = null;
                for (int __xtable = 0; __xtable < __root.ChildNodes.Count; __xtable++)
                {
                    if (__root.ChildNodes[__xtable].Name.ToLower().Equals("table"))
                    {
                        __table = __root.ChildNodes[__xtable];
                        break;
                    }
                }

                int __maxRow = _getAttributeToInt(__table, __table.Attributes, "ss:ExpandedRowCount");
                int __maxColumn = _getAttributeToInt(__table, __table.Attributes, "ss:ExpandedColumnCount");
                //
                __root = __xmlDocumnet["Workbook"]["Worksheet"]["Table"];
                int __rowIndex = 0;
                for (int __row = 0; __row < __maxRow; __row++)
                {
                    ArrayList __dataList = new ArrayList();
                    for (int __column = 0; __column < __maxColumn; __column++)
                    {
                        __dataList.Add("");
                    }
                    __rowArrayList.Add(__dataList);
                }

                foreach (XmlNode __row in __root.ChildNodes)
                {
                    if (__row.Name.Equals("Row"))
                    {
                        int __rowIndexNew = _getAttributeToInt(__row, __row.Attributes, "ss:Index");
                        if (__rowIndexNew != 0)
                        {
                            __rowIndex = __rowIndexNew - 1;
                        }
                        int __columnIndex = 0;
                        foreach (XmlNode __cell in __row.ChildNodes)
                        {
                            int __columnIndexNew = _getAttributeToInt(__cell, __cell.Attributes, "ss:Index");
                            if (__columnIndexNew != 0)
                            {
                                __columnIndex = __columnIndexNew - 1;
                            }

                            object __value = "";
                            XmlNode __data = null;
                            try
                            {

                                __data = (__cell["Data"].FirstChild != null) ? __cell["Data"].FirstChild : null;

                                if (__data != null)
                                {

                                    __value = __data.Value;
                                }
                                else
                                {
                                    __value = "";
                                }
                            }
                            catch
                            {

                            }

                            ((ArrayList)__rowArrayList[__rowIndex])[__columnIndex] = __value;
                            __columnIndex++;
                        }
                        // __valuePercent += (100.0 * __rowIndex / __maxRow);

                        __rowIndex++;
                        __valuePercent += (100.0 * __rowIndex / __maxRow);
                        //  this.__progressBar.Value = (int)(100.0 * __rowIndex / __maxRow);
                        //this._processMessage = this.__progressBar.Value.ToString() + " %";
                        SetProgressValue(100, 0, (int)(100.0 * __rowIndex / __maxRow));
                        SetLabelProgressText("Load data form xml file : " + (int)(100.0 * __rowIndex / __maxRow) + "%");
                    }
                }
                //
                __valuePercent = 0;
                SetProgressValue(100, 0, 0);
                SetLabelProgressText("Load data xml to Grid");
                __dataTable = new DataTable("Data");
                for (int __column = 0; __column < __maxColumn; __column++)
                {
                    // __dataTable.Columns.Add(Convert.ToChar(__column + 65).ToString(), Type.GetType("System.String"));
                    __dataTable.Columns.Add(__column.ToString(), Type.GetType("System.String"));
                }
                for (int __row = 1; __row < __maxRow; __row++)
                {
                    DataRow __newRow = __dataTable.NewRow();
                    for (int __column = 0; __column < __maxColumn; __column++)
                    {
                        __newRow[__column] = ((ArrayList)__rowArrayList[__row])[__column].ToString().Trim();
                    }
                    __dataTable.Rows.Add(__newRow);
                }

                SetProgressValue(100, 0, 0);
                SetLabelProgressText("Please Wait Checking file");
                __excel = new _excelClass(__maxColumn, "Arial", 7);
                __excel._pageSetup._orientation = OrientationType.Landscape;
                __excel._pageSetup._pageMarginBottom = 0.35f;
                __excel._pageSetup._pageMarginLeft = 0.35f;
                __excel._pageSetup._pageMarginRight = 0.35f;
                __excel._pageSetup._pageMarginTop = 0.35f;
                __excel._pageSetup._headerMargin = 0.5f;
                __excel._pageSetup._sheetName = "ReImportmaster";
                //string __stylex = __excel._styleAdd(_horizontalType.Center, _verticalType.Center, true, 1, 1, 1, 1, __numberFormat, true, "MS Sans Serif", 10.0f);
                string __stylex = __excel._styleAdd(_horizontalType.Center, _verticalType.Center, true, 1, 1, 1, 1, _borderStyleType.Continuous, _borderStyleType.Continuous, _borderStyleType.Continuous, _borderStyleType.Continuous, "", true, "MS Sans Serif`", 10.0f, false);
                __excel._addRow();
                //__excel._cellValue(__excel._currentRow, 0, "", __stylex, 55, SMLReport._report._cellType.String);
                string[] __newRowx = new string[__maxColumn];
                for (int __row = 0; __row < 1; __row++)
                {

                    for (int __column = 0; __column < __maxColumn; __column++)
                    {
                        __excel._cellValue(__excel._currentRow, __excel._currentColumn, ((ArrayList)__rowArrayList[__row])[__column].ToString(), __stylex, 1, _cellType.String);
                    }

                }
                StringBuilder __queryIn = new StringBuilder();
                List<string> __listqueryin = new List<string>();
                List<string> __listquerydistinctIC_Code = new List<string>();
                int codein = 0;
                for (int __rowin = 0; __rowin < __dataTable.Rows.Count; __rowin++)
                {
                    string ic_code = __dataTable.Rows[__rowin][54].ToString().Trim();
                    string __BD = __dataTable.Rows[__rowin][55].ToString();
                    string __M = __dataTable.Rows[__rowin][12].ToString().Trim();
                    string __O = __dataTable.Rows[__rowin][14].ToString().Trim();
                    string __Q = __dataTable.Rows[__rowin][16].ToString().Trim();
                    string __S = __dataTable.Rows[__rowin][18].ToString().Trim();
                    if (__listqueryin.Count == 0)
                    {
                        __listqueryin.Add(ic_code);
                        __listqueryin.Add(__BD);
                        __listqueryin.Add("99");
                        if (__M.Length > 0) __listqueryin.Add(__M);
                        if (__O.Length > 0) __listqueryin.Add(__O);
                        if (__Q.Length > 0) __listqueryin.Add(__Q);
                        if (__S.Length > 0) __listqueryin.Add(__S);
                    }
                    else
                    {
                        if (__M.Length > 0 && !__listqueryin.Contains(__M)) __listqueryin.Add(__M);
                        if (__O.Length > 0 && !__listqueryin.Contains(__O)) __listqueryin.Add(__O);
                        if (__Q.Length > 0 && !__listqueryin.Contains(__Q)) __listqueryin.Add(__Q);
                        if (__S.Length > 0 && !__listqueryin.Contains(__S)) __listqueryin.Add(__S);
                        if (!__listqueryin.Contains(ic_code)) __listqueryin.Add(ic_code);
                        if (!__listqueryin.Contains(__BD)) __listqueryin.Add(__BD);
                    }
                }
                for (int __rowdistinct = 0; __rowdistinct < __listqueryin.Count; __rowdistinct++)
                {
                    codein++;
                    string __data = __listqueryin[__rowdistinct];
                    if (__queryIn.Length > 0) __queryIn.Append(",");
                    __queryIn.Append("'" + __data + "'");
                    if (codein == 100)
                    {
                        codein = 0;
                        __listquerydistinctIC_Code.Add(__queryIn.ToString());
                        __queryIn = new StringBuilder();

                    }
                }
                this._listTable = new ArrayList();
                __listquerydistinctIC_Code.Add(__queryIn.ToString());
                for (int __in = 0; __in < __listquerydistinctIC_Code.Count; __in++)
                {
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    StringBuilder __myquery = new StringBuilder();
                    __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory._code + "," + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + " in (" + __listquerydistinctIC_Code[__in] + ")"));
                    __myquery.Append("</node>");
                    ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());

                    if (((DataSet)__getData[0]).Tables.Count > 0)
                    {
                        _listTable.Add(((DataSet)__getData[0]).Tables[0]);
                    }
                }
                //    public DataTable _createTable()
                //{
                DataTable __Tabletemp = new DataTable("temp");
                __Tabletemp.Columns.Add(new DataColumn("ic_set_code", Type.GetType("System.String")));
                __Tabletemp.Columns.Add(new DataColumn("ic_code", Type.GetType("System.String")));
                __Tabletemp.Columns.Add(new DataColumn("unit_code", Type.GetType("System.String")));
                __Tabletemp.Columns.Add(new DataColumn("qty", Type.GetType("System.Double")));
                __Tabletemp.Columns.Add(new DataColumn("price", Type.GetType("System.Decimal")));
                __Tabletemp.Columns.Add(new DataColumn("sum_amount", Type.GetType("System.Decimal")));


                //            return MenuTable;
                //}
                _inventorymain __listmain = new _inventorymain();
                for (int __data = 0; __data < __dataTable.Rows.Count; __data++)
                {
                    // List<string> __listdata = new List<string>();                   
                    try
                    {
                        _inventory __inventory = new _inventory();
                        string __M = __dataTable.Rows[__data][12].ToString().Trim();
                        string __N = __dataTable.Rows[__data][13].ToString().Trim();
                        string __O = __dataTable.Rows[__data][14].ToString().Trim();
                        string __P = __dataTable.Rows[__data][15].ToString().Trim();
                        string __Q = __dataTable.Rows[__data][16].ToString().Trim();
                        string __R = __dataTable.Rows[__data][17].ToString().Trim();
                        string __S = __dataTable.Rows[__data][18].ToString().Trim();
                        string __T = __dataTable.Rows[__data][19].ToString().Trim();
                        string __ic_code = __dataTable.Rows[__data][54].ToString().Trim();
                        string __unitstandart = _search_Ic_code(__ic_code, this._listTable);// _search_unitcode_byrow(__ic_code);
                        string __ic_name_1 = __dataTable.Rows[__data][0].ToString().Replace("\'", "\'\'").Trim();
                        __ic_code = __ic_code.Replace("  ", "-");
                        __ic_code = __ic_code.Replace("_", "-");
                        //    string  [] _spitic  = __ic_code.Split('-');
                        string _unitcode = __ic_code.Split('-')[3].ToString().Trim();
                        string __BD = __dataTable.Rows[__data][55].ToString().Replace("\'", "\'\'");
                        __BD = __BD.Replace("  ", "-");
                        __BD = __BD.Replace(" ", "-");
                        __BD = __BD.Replace("_", "-");
                        string __99 = "99";
                        string __Qty = "1";
                        string __Price = "0";
                        string __Amount = "0";
                        _inventory_detail __detail = new _inventory_detail();
                        __inventory._ic_code = __ic_code;
                        __inventory._name_1 = __ic_name_1.Replace("\'", "\'\'").Trim(); ;
                        __inventory._name_2 = __ic_name_1.Replace("\'", "\'\'").Trim(); ;
                        __inventory._name_eng_1 = __ic_name_1.Replace("\'", "\'\'").Trim(); ;
                        __inventory._name_eng_2 = __ic_name_1.Replace("\'", "\'\'").Trim(); ;
                        __inventory._unit_type = 0;
                        __inventory._tax_type = 0;
                        __inventory._cost_type = 0;
                        __inventory._unit_cost = _unitcode;
                        __inventory._unit_standard = _unitcode;
                        __inventory._item_type = 5;
                        __inventory._income_type = "001";
                        __detail = new _inventory_detail();
                        __detail._ic_set = __BD.Trim();
                        __detail._Qty = "1";
                        __detail._Price = "-1";
                        __detail._Amount = "-1";
                        __detail._Unitcode = _search_unit_code(__BD, this._listTable);// _search_unitcode_byrow(__BD);                        
                        DataRow newRow__BD = __Tabletemp.NewRow();
                        newRow__BD["ic_set_code"] = __ic_code;
                        newRow__BD["ic_code"] = __detail._ic_set;
                        newRow__BD["unit_code"] = __detail._Unitcode;
                        newRow__BD["qty"] = __detail._Qty;
                        newRow__BD["price"] = __detail._Price;
                        newRow__BD["sum_amount"] = __detail._Amount;
                        __Tabletemp.Rows.Add(newRow__BD);
                        __inventory.__listdata.Add(__detail);
                        __detail = new _inventory_detail();
                        __detail._ic_set = __99;
                        __detail._Qty = "1";
                        __detail._Price = "-1";
                        __detail._Amount = "-1";
                        __detail._Unitcode = _search_unit_code(__99, this._listTable); //_search_unitcode_byrow(__99);//

                        DataRow newRow__99 = __Tabletemp.NewRow();
                        newRow__99["ic_set_code"] = __ic_code;
                        newRow__99["ic_code"] = __detail._ic_set;
                        newRow__99["unit_code"] = __detail._Unitcode;
                        newRow__99["qty"] = __detail._Qty;
                        newRow__99["price"] = __detail._Price;
                        newRow__99["sum_amount"] = __detail._Amount;
                        __Tabletemp.Rows.Add(newRow__99);
                        __inventory.__listdata.Add(__detail);
                        if (__M.Length > 0 && double.Parse(__N) != 0)
                        {
                            __detail = new _inventory_detail();
                            __detail._ic_set = __M;
                            __detail._Qty = (double.Parse(__N) * MyLib._myGlobal._intPhase(_substrunitcode(_unitcode))).ToString();
                            __detail._Price = __Price.Trim();
                            __detail._Amount = __Amount.Trim(); ;
                            __detail._Unitcode = _search_unit_code(__M, this._listTable);// _search_unitcode_byrow(__M);//

                            DataRow newRow__M = __Tabletemp.NewRow();
                            newRow__M["ic_set_code"] = __ic_code;
                            newRow__M["ic_code"] = __detail._ic_set;
                            newRow__M["unit_code"] = __detail._Unitcode;
                            newRow__M["qty"] = __detail._Qty;
                            newRow__M["price"] = __detail._Price;
                            newRow__M["sum_amount"] = __detail._Amount;
                            __Tabletemp.Rows.Add(newRow__M);
                            __inventory.__listdata.Add(__detail);
                        }

                        if (__O.Length > 0 && double.Parse(__P) != 0)
                        {
                            __detail = new _inventory_detail();
                            __detail._ic_set = __O;
                            __detail._Qty = (double.Parse(__P) * MyLib._myGlobal._intPhase(_substrunitcode(_unitcode))).ToString();
                            __detail._Price = __Price.Trim();
                            __detail._Amount = __Amount.Trim();
                            __detail._Unitcode = _search_unit_code(__O, this._listTable); //_search_unitcode_byrow(__O);// 

                            DataRow  newRow__O = __Tabletemp.NewRow();
                            newRow__O["ic_set_code"] = __ic_code;
                            newRow__O["ic_code"] = __detail._ic_set;
                            newRow__O["unit_code"] = __detail._Unitcode;
                            newRow__O["qty"] = __detail._Qty;
                            newRow__O["price"] = __detail._Price;
                            newRow__O["sum_amount"] = __detail._Amount;
                            __Tabletemp.Rows.Add(newRow__O);
                            __inventory.__listdata.Add(__detail);
                        }
                        if (__Q.Length > 0 && double.Parse(__R) != 0)
                        {
                            __detail = new _inventory_detail();
                            __detail._ic_set = __Q;
                            __detail._Qty = (double.Parse(__R) * MyLib._myGlobal._intPhase(_substrunitcode(_unitcode))).ToString();
                            __detail._Price = __Price.Trim();
                            __detail._Amount = __Amount.Trim();
                            __detail._Unitcode = _search_unit_code(__Q, this._listTable); //_search_unitcode_byrow(__Q);//

                            DataRow newRow__Q = __Tabletemp.NewRow();
                            newRow__Q["ic_set_code"] = __ic_code;
                            newRow__Q["ic_code"] = __detail._ic_set;
                            newRow__Q["unit_code"] = __detail._Unitcode;
                            newRow__Q["qty"] = __detail._Qty;
                            newRow__Q["price"] = __detail._Price;
                            newRow__Q["sum_amount"] = __detail._Amount;
                            __Tabletemp.Rows.Add(newRow__Q);
                            __inventory.__listdata.Add(__detail);
                        }
                        if (__S.Length > 0 && double.Parse(__T) != 0)
                        {
                            __detail = new _inventory_detail();
                            __detail._ic_set = __S;
                            __detail._Qty = (double.Parse(__T) * MyLib._myGlobal._intPhase(_substrunitcode(_unitcode))).ToString();
                            __detail._Price = __Price.Trim();
                            __detail._Amount = __Amount.Trim();
                            __detail._Unitcode = _search_unit_code(__S, this._listTable); //_search_unitcode_byrow(__S);//

                            DataRow newRow__S = __Tabletemp.NewRow();
                            newRow__S["ic_set_code"] = __ic_code;
                            newRow__S["ic_code"] = __detail._ic_set;
                            newRow__S["unit_code"] = __detail._Unitcode;
                            newRow__S["qty"] = __detail._Qty;
                            newRow__S["price"] = __detail._Price;
                            newRow__S["sum_amount"] = __detail._Amount;
                            __Tabletemp.Rows.Add(newRow__S);
                            __inventory.__listdata.Add(__detail);
                        }
                        __listmain.__list.Add(__inventory);

                    }
                    catch (Exception ex)
                    {

                        __dataTable.Dispose();
                    }

                }
                StringBuilder __temstr = new StringBuilder();
                for (int __rowin = 0; __rowin < __dataTable.Rows.Count; __rowin++)
                {
                    bool __notfind = false;
                    string data = __dataTable.Rows[__rowin][54].ToString().Trim();
                    if (_search_Ic_code(data, this._listTable).Length == 0)
                    {
                        __temstr.Append(data + "  Not In IC_Inventory Table ").AppendLine();
                        __notfind = true;
                    }
                    else
                    {
                        //string __query = "select  count(*) as xcoun from " + _g.d.ic_inventory_set_detail._table + " where " + _g.d.ic_inventory_set_detail._ic_set_code + " = \'" + data + "\'";
                        string __query = "select  [ic_set_code],[ic_code],[unit_code],[qty],[price],[sum_amount] from " + _g.d.ic_inventory_set_detail._table + " where " + _g.d.ic_inventory_set_detail._ic_set_code + " = \'" + data + "\'";
                        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
                        DataSet __dsData = new DataSet();
                        DataTable __tempunituse = new DataTable();
                        __dsData = _myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                        if (__dsData.Tables.Count > 0)
                        {
                            __tempunituse = _myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
                            if (__tempunituse.Rows.Count > 0)
                            {
                                // if (MyLib._myGlobal._intPhase(__tempunituse.Rows[0]["xcoun"].ToString()) == 0)
                                if (__tempunituse.Rows[0]["ic_set_code"].ToString().Length == 0)
                                {
                                    __temstr.Append(data + " Not In IC_Inventory_set_detail Table").AppendLine();
                                    __notfind = true;
                                }
                                else
                                {
                                    if (__Tabletemp.Rows.Count > 0)
                                    {
                                        //if (data.Equals("AN-9901-45YR83/076-3L"))
                                        //{
                                        //    string xxxx = "";
                                        //}
                                        DataRow[] __rowtempxx = __Tabletemp.Select(" ic_set_code =\'" + data + "\' ");
                                        DataRow[] __rowtempserver = __tempunituse.Select(" ic_set_code =\'" + data + "\'");
                                        //if (__rowtempxx.Length == __rowtempserver.Length)
                                        //{
                                            for (int __row = 0; __row < __rowtempxx.Length; __row++)
                                            {
                                                Boolean __updateCompareDiff = false;
                                                for (int __column = 0; __column < __tempunituse.Columns.Count; __column++)
                                                {
                                                    int __indexOfColumn = __tempunituse.Columns.IndexOf(__tempunituse.Columns[__column].ToString());
                                                    string __columnName = __tempunituse.Columns[__column].ToString();
                                                    if (__indexOfColumn != -1)
                                                    {
                                                        int __fieldOrdinal = __Tabletemp.Columns[__columnName].Ordinal;
                                                        object __valueServer = "";
                                                        object __valueClient = "";
                                                        if (__Tabletemp.Columns[__columnName].DataType == typeof(String))
                                                        {
                                                            try
                                                            {
                                                                __valueServer = __rowtempserver[__row].ItemArray[__fieldOrdinal].ToString().Replace("\'", "\'\'");
                                                                __valueClient = __rowtempxx[__row].ItemArray[__fieldOrdinal].ToString().Replace("\'", "\'\'");
                                                            }
                                                            catch
                                                            {
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (__Tabletemp.Columns[__columnName].DataType == typeof(Double))
                                                            {
                                                                try
                                                                {
                                                                    __valueServer = Double.Parse(__rowtempserver[__row].ItemArray[__fieldOrdinal].ToString());
                                                                    __valueClient = Double.Parse(__rowtempxx[__row].ItemArray[__fieldOrdinal].ToString());
                                                                    if (__valueServer.Equals(__valueClient) == false &&  __columnName.Equals("qty"))
                                                                    {
                                                                        if (__valueServer.ToString().IndexOf(".") != -1 && __columnName.Equals("qty"))
                                                                        {

                                                                            __valueServer = String.Format("{0:0.###}", Double.Parse(__rowtempserver[__row].ItemArray[__fieldOrdinal].ToString()));
                                                                            __valueClient = String.Format("{0:0.###}", Double.Parse(__rowtempxx[__row].ItemArray[__fieldOrdinal].ToString()));
                                                                        }
                                                                    }                                                                    
                                                                }
                                                                catch
                                                                {
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (__Tabletemp.Columns[__columnName].DataType == typeof(Decimal))
                                                                {
                                                                    try
                                                                    {
                                                                        __valueServer = Decimal.Parse(__rowtempserver[__row].ItemArray[__fieldOrdinal].ToString());
                                                                        __valueClient = Decimal.Parse(__rowtempxx[__row].ItemArray[__fieldOrdinal].ToString());

                                                                    }
                                                                    catch
                                                                    {
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        if (__valueServer.Equals(__valueClient) == false)
                                                        {
                                                            __updateCompareDiff = true;
                                                            break;
                                                        }
                                                    }

                                                }
                                                if (__updateCompareDiff == true)
                                                {
                                                    __temstr.Append(data + " Have Difference IC_Inventory_set_detail Table").AppendLine();
                                                    __notfind = true;
                                                    break;
                                                }

                                            }
                                        //}
                                      //  else
                                       // {
                                       //     __temstr.Append(data + " Have Difference IC_Inventory_set_detail Table").AppendLine();
                                       //     __notfind = true;
                                      //  }
                                    }
                                }
                            }
                        }
                    }
                    if (__notfind)
                    {

                        __excel._addRow();
                        for (int __column = 0; __column < __maxColumn; __column++)
                        {
                            __excel._cellValue(__excel._currentRow, __excel._currentColumn, __repleace(__dataTable.Rows[__rowin][__column].ToString()), __stylex, 1, _cellType.String);
                        }
                    }
                    SetProgressValue(100, 0, (int)(100.0 * (__rowin + 1) / __dataTable.Rows.Count));
                    SetLabelProgressText(" Checking file : " + (int)(100.0 * (__rowin + 1) / __dataTable.Rows.Count) + "%");
                }
                __Tabletemp.Dispose();
                __dataTable.Dispose();
                //for (int __rowin = 0; __rowin < __dataTable.Rows.Count; __rowin++)
                //{
                //    __excel._addRow();
                //    for (int __column = 0; __column < __maxColumn; __column++)
                //    {
                //        __excel._cellValue(__excel._currentRow, __excel._currentColumn, __dataTable.Rows[__rowin][__column].ToString(), __stylex, 1, _cellType.String);
                //    }

                //}

                if (__temstr.ToString().Length == 0)
                {
                    __temstr.Append(" No record").AppendLine();

                }
                else
                {
                    FileInfo __file = new FileInfo(__fliename);
                    string __newfilename = __file.Name;
                    __newfilename = __fliename.Replace(__file.Name, "_new" + __file.Name);
                    //__newfilename = __fliename.Replace(__newfilename, __file.Name.Replace(".xml", "_NewFile"));
                    string __filenameexcel = __excel._createExcelFile(__newfilename);
                    __messagefile.Append("please ReImport from " + __filenameexcel).AppendLine();
                    __message.Append("please ReImport from " + __filenameexcel).AppendLine();
                }
                __message.Append(__temstr.ToString()).AppendLine();
                __messagefile.Append(__temstr.ToString()).AppendLine();
                __log(__messagefile.ToString());
                MessageBox.Show("Check ok");
                SetGridValue(__dataTable);

            }
            catch (Exception __ex)
            {
                MessageBox.Show(" Error " + __ex.Message);

                SetLabelProgressText("Error Load data xml >> " + __ex.Message);
                __message.Append(" Error >> " + __ex.Message).AppendLine();
                //  __dataTable.Dispose();

            }

        }
        string __repleace(string torepleace)
        {
            String __result = torepleace.ToString().Replace("&", "&amp;");
            __result = __result.Replace("<", "&lt;");
            __result = __result.Replace(">", "&gt;");
            __result = __result.Replace("\"", "&quot;");
            return __result;
        }
        string _search_Ic_code(string code, ArrayList __arrtable)
        {
            string __result = "";
            DataTable __temp;
            //string __query = "select " + _g.d.ic_inventory._code + "," + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + " order by  " + _g.d.ic_inventory._code + " , roworder";
            for (int __rowtable = 0; __rowtable < __arrtable.Count; __rowtable++)
            {
                __temp = new DataTable();
                __temp = (DataTable)__arrtable[__rowtable];
                if (__temp.Rows.Count > 0)
                {
                    DataRow[] __rowfind = __temp.Select(_g.d.ic_inventory._code + " = \'" + code + "\'");
                    if (__rowfind.Length > 0)
                    {
                        __result = __rowfind[0].ItemArray[0].ToString().Trim();
                        break;
                    }
                }
            }
            return __result;
        }
        string _search_unit_code(string code, ArrayList __arrtable)
        {
            string __result = "";
            DataTable __temp;
            //string __query = "select " + _g.d.ic_inventory._code + "," + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + " order by  " + _g.d.ic_inventory._code + " , roworder";
            for (int __rowtable = 0; __rowtable < __arrtable.Count; __rowtable++)
            {
                __temp = new DataTable();
                __temp = (DataTable)__arrtable[__rowtable];
                if (__temp.Rows.Count > 0)
                {
                    DataRow[] __rowfind = __temp.Select(_g.d.ic_inventory._code + " = \'" + code + "\'");
                    if (__rowfind.Length > 0)
                    {
                        __result = __rowfind[0].ItemArray[1].ToString().Trim();
                        break;
                    }
                }
            }
            return __result;
        }
        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _bntPreview_Click(object sender, EventArgs e)
        {

            _buttonProcessk();

            //  _previewXml __xpreview = new _previewXml();
            //  __xpreview.dataGridView1.DataSource = this._buttonProcessk();
            //  __xpreview.ShowDialog();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (_isReadxmlsucess)
            {
                _isReadxmlsucess = false;
                this._loadDataThreadChecking = new Thread(new ThreadStart(_buttonProcessk));
                this._loadDataThreadChecking.Start();
            }
            //if (this._isloadDatasucess && _isSavedata)
            //{
            //    this._isSavedata = false;
            //    _isSavedata = false;
            //    this._isloadDatasucess = false;
            //    this._SaveDataThread = new Thread(new ThreadStart(_savedata));
            //    this._SaveDataThread.Start();
            //}
        }

        private void _bntViewProcess_Click(object sender, EventArgs e)
        {
            _frmViewProcess __log = new _frmViewProcess();
            __log._lbllog.Text = __message.ToString();
            __log.Show();
        }
    }


}
