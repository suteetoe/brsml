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

namespace SMLICIAdmin
{
    public partial class _importFcolorDataControl : UserControl
    {
        _inventorymain __listmain = null;
        _inventorymain __listmaint_temp = null;
        private StringBuilder __message = new StringBuilder();
        private StringBuilder __messagefile = new StringBuilder();
        //DataTable __searchdt = null;
        ArrayList _listTable = new ArrayList();
        bool _isReadxmlsucess = false;
        bool _isloadDatasucess = false;
        bool _isSavedata = false;
        bool _isSavedatasucess = false;
        string __fliename = "";
        public String _processMessage = "";
        int __totol_inventory_set_detail = 0;
        int __totol_inventory = 0;
        double __valuePercent = 0.0;
        Thread _loadDataThread = null;
        Thread _SaveDataThread = null;
        public _importFcolorDataControl()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(_importFcolorDataControl_Disposed);
            _cancelThread();
            //

        }
        void _cancelThread()
        {
            if (_loadDataThread != null)
            {

                try
                {
                    Thread.Sleep(100);
                    _loadDataThread.Abort();
                }
                catch { };
            }
            if (_SaveDataThread != null)
            {
                try
                {
                    Thread.Sleep(100);
                    _SaveDataThread.Abort();
                }
                catch { };
            }
        }
        string _search_unitcode(string code, ArrayList __arrtable)
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
        ArrayList _search_unit_List()
        {
            ArrayList __result = new ArrayList();
            //int __countertrotalrectode = 0;
            //int __searchPageTotal = 0;
            int __searchRecordPerPage = 500;
            //int __searchTotalRecord = 0;
            int _searchPageNumber = 0;
            string __querCount = " select count(*) as rowcount from " + _g.d.ic_inventory._table;
            string __query = "select " + _g.d.ic_inventory._code + "," + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + " order by  " + _g.d.ic_inventory._code + " , roworder";
            MyLib._queryReturn __dsresult = null;
            bool __isrecord = false;
            do
            {
                try
                {
                    MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
                    __dsresult = _myFrameWork._queryLimit(MyLib._myGlobal._databaseName, __querCount, __query, _searchPageNumber * __searchRecordPerPage, __searchRecordPerPage, 1);
                    if (__dsresult.detail.Tables.Count > 0)
                    {
                        __isrecord = __dsresult.detail.Tables[0].Rows.Count > 0;
                        // __searchTotalRecord = __dsresult.totalRecord;
                        // _searchPageTotal = (__dsresult.totalRecord / _searchRecordPerPage) + 1;
                        // __searchTotalRecord = __dsresult.totalRecord;
                        // __countertrotalrectode += dsresult;
                        __result.Add(__dsresult.detail.Tables[0]);
                        _searchPageNumber++;
                    }
                }
                catch
                {
                    __isrecord = false;
                }
            } while (__isrecord == true);
            return __result;
        }
        string[] _search_ic_unit_use_row_order(string ic_coce, string unit_code)
        {

            string[] __result = new string[2];
            __result[0] = "";
            __result[1] = "1";
            string __row_order = "";
            try
            {
                string __query = "select " + _g.d.ic_unit_use._code + "," + _g.d.ic_unit_use._ic_code + "," + _g.d.ic_unit_use._row_order + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._code + " = \'" + unit_code + "\' and " + _g.d.ic_unit_use._ic_code + " = \'" + ic_coce + "\'" + " order by " + _g.d.ic_unit_use._ic_code + "," + _g.d.ic_unit_use._row_order;
                MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
                DataTable __tempunituse = new DataTable();
                DataSet __ds = new DataSet();
                __ds = _myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                if (__ds.Tables.Count > 0)
                {
                    __tempunituse = __ds.Tables[0];
                    if (__tempunituse.Rows.Count > 0)
                    {
                        __result[0] = __tempunituse.Rows[0][_g.d.ic_unit_use._ic_code].ToString();
                        __row_order = __tempunituse.Rows[0][_g.d.ic_unit_use._row_order].ToString();
                        if (__row_order.Length == 0)
                        {
                            __result[1] = "1";
                        }
                        else
                        {
                            __result[1] = __row_order;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            //if (listtableunituse.Count > 0)
            //{
            //    for (int __row = 0; __row < listtableunituse.Count; __row++)
            //    {
            //        __tempunituse = new DataTable();
            //        __tempunituse = (DataTable)listtableunituse[__row];
            //        if (__tempunituse.Rows.Count > 0)
            //        {
            //            DataRow[] __rowfind = __tempunituse.Select(_g.d.ic_unit_use._code + " = \'" + unit_code + "\' and " + _g.d.ic_unit_use._ic_code + " = \'" + ic_coce + "\'");
            //            if (__rowfind.Length > 0)
            //            {
            //                //__result =__rowfind.ar
            //                __result[0] = __rowfind[0].ItemArray[1].ToString().Trim();
            //                __row_order = __rowfind[0].ItemArray[2].ToString().Trim();
            //                if (__row_order.Length == 0)
            //                {
            //                    __result[1] = "1";
            //                }
            //                else
            //                {
            //                    __result[1] = __row_order;
            //                }
            //                break;
            //            }
            //        }
            //    }
            //}

            return __result;
        }
        ArrayList _search_unit_useList()
        {
            ArrayList __result = new ArrayList();
            //int __countertrotalrectode = 0;
            //int __searchPageTotal = 0;
            int __searchRecordPerPage = 500;
            int __searchTotalRecord = 0;
            int __searchPageNumber = 0;

            string __querCount = " select count(*) as rowcount from " + _g.d.ic_unit_use._table;
            string __query = "select " + _g.d.ic_unit_use._code + "," + _g.d.ic_unit_use._ic_code + "," + _g.d.ic_unit_use._row_order + " from " + _g.d.ic_unit_use._table + " order by " + _g.d.ic_unit_use._ic_code + "," + _g.d.ic_unit_use._row_order;

            MyLib._queryReturn __dsresult = null;
            bool __isrecord = false;
            do
            {
                try
                {
                    MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
                    __dsresult = _myFrameWork._queryLimit(MyLib._myGlobal._databaseName, __querCount, __query, __searchPageNumber * __searchRecordPerPage, __searchRecordPerPage, 1);
                    if (__dsresult.detail.Tables.Count > 0)
                    {
                        __isrecord = __dsresult.detail.Tables[0].Rows.Count > 0;
                        //__searchTotalRecord = __dsresult.totalRecord;
                        // __searchPageTotal = (__dsresult.totalRecord / __searchRecordPerPage) + 1;
                        __searchTotalRecord = __dsresult.totalRecord;
                        //  __countertrotalrectode += dsresult;
                        __result.Add(__dsresult.detail.Tables[0]);
                        __searchPageNumber++;
                    }
                }
                catch
                {
                    __isrecord = false;
                }
            } while (__isrecord == true);
            return __result;
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
                    __message.Append("<<<<<----- Running Process ----->>>>>").AppendLine(); ;
                    __message.Append("From Xml file name " + __file.FileName).AppendLine(); ;
                    __messagefile.Append("<<<<<----- Running Process ----->>>>>").AppendLine();
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
        DataTable _search_unit_use()
        {
            DataTable __dt = new DataTable();
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __ds = new DataSet();
            //   __ds = __myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.ic_inventory._code + "," + _g.d.ic_inventory._unit_standard + " from " + _g.d.ic_inventory._table + "");
            __ds = __myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.ic_inventory._code + "," + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + "");
            if (__ds.Tables.Count > 0)
            {
                __dt = __ds.Tables[0];
            }
            return __dt;
        }
        /// <summary>
        /// หน่วยต้นทุน
        /// </summary>
        /// <param name="code"></param>
        /// <param name="dt"></param>
        /// <returns></returns>

        DataTable _search_unitcode()
        {
            DataTable __dt = new DataTable();
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __ds = new DataSet();
            //   __ds = __myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.ic_inventory._code + "," + _g.d.ic_inventory._unit_standard + " from " + _g.d.ic_inventory._table + "");
            __ds = __myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.ic_inventory._code + "," + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + "");
            if (__ds.Tables.Count > 0)
            {
                __dt = __ds.Tables[0];
            }
            return __dt;
        }
        string[] _search_ic_unit_use_row_order(string ic_coce, string unit_code, DataTable dt)
        {
            string[] __result = new string[2];
            __result[0] = "";
            __result[1] = "1";
            string __row_order = "";
            if (dt.Rows.Count > 0)
            {
                DataRow[] __rowfind = dt.Select(_g.d.ic_unit_use._code + " = \'" + unit_code.Trim() + "\' and " + _g.d.ic_unit_use._ic_code + " = \'" + ic_coce.Trim() + "\'");
                if (__rowfind.Length > 0)
                {
                    //__result =__rowfind.ar
                    __result[0] = __rowfind[0].ItemArray[1].ToString().Trim();
                    __row_order = __rowfind[0].ItemArray[2].ToString().Trim();
                    if (__row_order.Length == 0)
                    {
                        __result[1] = "1";
                    }
                    else
                    {
                        __result[1] = __row_order;
                    }
                }
            }
            return __result;
        }
        string _search_ic_unit_use(string ic_coce, string unit_code, DataTable dt)
        {
            string __result = "";
            string __resultline = "";
            string __row_order = "";
            if (dt.Rows.Count > 0)
            {
                DataRow[] __rowfind = dt.Select(_g.d.ic_unit_use._code + " = \'" + unit_code + "\' and " + _g.d.ic_unit_use._ic_code + " = \'" + ic_coce + "\'");
                if (__rowfind.Length > 0)
                {
                    //__result =__rowfind.ar
                    __result = __rowfind[0].ItemArray[1].ToString().Trim();
                    __row_order = __rowfind[0].ItemArray[2].ToString().Trim();
                }
                else
                {
                    DataRow[] __rowfindcode = dt.Select(_g.d.ic_unit_use._ic_code + " = \'" + ic_coce + "\'");
                    if (__rowfindcode.Length > 0)
                    {
                        //__result =__rowfind.ar
                        __resultline = __rowfindcode[0].ItemArray[2].ToString().Trim();
                    }
                }
            }

            return __result;
        }
        string _substrunitcode(string sub)
        {

            string __result = "0";

            if (sub.Length > 0)
            {
                __result = sub.Substring(0, sub.Length - 1);
            }
            return __result;
        }


        string _search_unitcode_byrow(string code)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __ds = new DataSet();
            //   __ds = __myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.ic_inventory._code + "," + _g.d.ic_inventory._unit_standard + " from " + _g.d.ic_inventory._table + "");
            // string __query = "select " + _g.d.ic_inventory._code + "," + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + " order by  " + _g.d.ic_inventory._code + " , roworder";
            __ds = __myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.ic_inventory._code + "," + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + " = \'" + code.Trim() + "\'");
            string __result = "";
            DataTable __temp = new DataTable();
            if (__ds.Tables.Count > 0)
            {
                __temp = __ds.Tables[0];

                if (__temp.Rows.Count > 0)
                {
                    __result = __temp.Rows[0][_g.d.ic_inventory._unit_cost].ToString();

                }
            }
            return __result;
        }
        string _search_ic_code(string code)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __ds = new DataSet();
            //   __ds = __myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.ic_inventory._code + "," + _g.d.ic_inventory._unit_standard + " from " + _g.d.ic_inventory._table + "");
            // string __query = "select " + _g.d.ic_inventory._code + "," + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + " order by  " + _g.d.ic_inventory._code + " , roworder";
            __ds = __myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.ic_inventory._code + "," + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + " = \'" + code.Trim() + "\'");
            string __result = "";
            DataTable __temp = new DataTable();
            if (__ds.Tables.Count > 0)
            {
                __temp = __ds.Tables[0];

                if (__temp.Rows.Count > 0)
                {
                    __result = __temp.Rows[0][_g.d.ic_inventory._code].ToString();

                }
            }
            return __result;
        }
        string _search_ic_codebyrow(string code)
        {
            string __result = "";

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __ds = new DataSet();

            __ds = __myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.ic_inventory._code + "," + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + " = \'" + code + "\'");
            DataTable __temp = new DataTable();
            if (__ds.Tables.Count > 0)
            {
                __temp = __ds.Tables[0];
                if (__temp.Rows.Count > 0)
                {
                    DataRow[] __rowfind = __temp.Select(_g.d.ic_inventory._code + " = \'" + code + "\'");
                    if (__rowfind.Length > 0)
                    {
                        __result = __rowfind[0].ItemArray[0].ToString().Trim();

                    }
                }
            }
            return __result;
        }
        /*      string _search_ic_code(string code, ArrayList __arrtable)
              {
                  string __result = "";
                  DataTable __temp;
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
              }*/
        string[] _search_ic_unit_use_row_order(string ic_coce, string unit_code, ArrayList listtableunituse)
        {
            DataTable __tempunituse;

            string[] __result = new string[2];
            __result[0] = "";
            __result[1] = "1";
            string __row_order = "";
            if (listtableunituse.Count > 0)
            {
                for (int __row = 0; __row < listtableunituse.Count; __row++)
                {
                    __tempunituse = new DataTable();
                    __tempunituse = (DataTable)listtableunituse[__row];
                    if (__tempunituse.Rows.Count > 0)
                    {
                        DataRow[] __rowfind = __tempunituse.Select(_g.d.ic_unit_use._code + " = \'" + unit_code + "\' and " + _g.d.ic_unit_use._ic_code + " = \'" + ic_coce + "\'");
                        if (__rowfind.Length > 0)
                        {
                            //__result =__rowfind.ar
                            __result[0] = __rowfind[0].ItemArray[1].ToString().Trim();
                            __row_order = __rowfind[0].ItemArray[2].ToString().Trim();
                            if (__row_order.Length == 0)
                            {
                                __result[1] = "1";
                            }
                            else
                            {
                                __result[1] = __row_order;
                            }
                            break;
                        }
                    }
                }
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
                __newfilename = __fliename.Replace(__newfilename, __file.Name.Replace(".xml", ".txt"));
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
        _inventorymain _distinctIcinventory(_inventorymain __listmain)
        {
            __listmaint_temp = new _inventorymain();
            ArrayList __listtemp = new ArrayList();
            for (int __row = 0; __row < __listmain.__list.Count; __row++)
            {
                _inventory __inv = (_inventory)__listmain.__list[__row];
               
                string __ic_code = __inv._ic_code.Trim();
                if (__listtemp.Count == 0)
                {
                    __listmaint_temp.__list.Add(__inv);
                    __listtemp.Add(__ic_code);
                }
                else
                {
                    if (!__listtemp.Contains(__ic_code))
                    {
                        __listmaint_temp.__list.Add(__inv);
                        __listtemp.Add(__ic_code);
                    }
                }
            }
            return __listmaint_temp;
        }
        void _savedata()
        {
            try
            {

                if (__listmain != null)
                {
                    _distinctIcinventory(__listmain);
                    __valuePercent = 0;
                    //  this.__progressBar.Value = (int)(100.0 * __rowIndex / __maxRow);
                    //this._processMessage = this.__progressBar.Value.ToString() + " %";
                    SetProgressValue(100, 0, 0);
                    SetLabelProgressText("ImportData Please Wait... ");
                    StringBuilder __result = new StringBuilder();
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    DataSet __dssearchUnituse = new DataSet();// = __myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.ic_unit_use._code + "," + _g.d.ic_unit_use._ic_code + "," + _g.d.ic_unit_use._row_order + " from " + _g.d.ic_unit_use._table + " order by " + _g.d.ic_unit_use._ic_code + "," + _g.d.ic_unit_use._row_order);                   
                    // int __totalperfile = 0;                    
                    int checkupload = 0;
                    StringBuilder __query_inventory_set_detail = new StringBuilder();
                    StringBuilder __query_inventory = new StringBuilder();
                    StringBuilder __query_ic_unit_use = new StringBuilder();
                    __query_inventory_set_detail.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    __query_inventory.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    __query_ic_unit_use.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    for (int __row = 0; __row < __listmaint_temp.__list.Count; __row++)
                    {

                        _inventory __inv = (_inventory)__listmaint_temp.__list[__row];
                        string __ic_code = __inv._ic_code.Trim();

                        int __linenumber = 0;
                        //if (__ic_code.Equals("AN-9971-70BG70/113-3L"))
                        //{
                        //    string xxx = "";
                        //}
                        string __searcyic_code = _search_unitcode(__ic_code, this._listTable);// _search_ic_code(__ic_code);//                       
                        int __line_number_unituse = 0;
                        //__dssearchUnituse = __myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.ic_unit_use._code + "," + _g.d.ic_unit_use._ic_code + "," + _g.d.ic_unit_use._row_order + " from " + _g.d.ic_unit_use._table + "  where " + _g.d.ic_unit_use._code + " = \'" + __inv._unit_standard.Trim() + "\' and " + _g.d.ic_unit_use._ic_code + " = \'" + __inv._ic_code.Trim() + "\'" + "order by " + _g.d.ic_unit_use._ic_code + "," + _g.d.ic_unit_use._row_order);
                        //if (__dssearchUnituse.Tables.Count > 0)
                        //{
                        //    if (__dssearchUnituse.Tables[0].Rows.Count > 0)
                        //    {
                        //        string[] __unitcode_use = _search_ic_unit_use_row_order(__inv._ic_code, __inv._unit_standard, __dssearchUnituse.Tables[0]);//_search_ic_unit_use_row_order(__inv._ic_code, __inv._unit_standard, __list_unit_use);
                        //        string __searcyic_unit_use = __unitcode_use[0];//__unitcode_use_search_ic_unit_use(__inv._ic_code,__inv._unit_standard ,__dssearchUnituse.Tables[0]);
                        //        if (__searcyic_unit_use.Length > 0)
                        //        {
                        //            __line_number_unituse = MyLib._myGlobal._intPhase(__unitcode_use[1]);
                        //            __query_ic_unit_use.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._code + " = \'" + __inv._unit_standard + "\' and " + _g.d.ic_unit_use._ic_code + " = \'" + __inv._ic_code.Trim() + "\'"));
                        //        }
                        //    }
                        //}
                           __query_ic_unit_use.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._code + " = \'" + __inv._unit_standard + "\' and " + _g.d.ic_unit_use._ic_code + " = \'" + __inv._ic_code.Trim() + "\'"));
                        if (__searcyic_code.Length > 0)
                        {
                            __query_inventory.Append(MyLib._myUtil._convertTextToXmlForQuery(" update " + _g.d.ic_inventory._table + " set " + _g.d.ic_inventory._name_1 + " = \'" + __inv._name_1 + "\'," + _g.d.ic_inventory._name_2 + "=\'" + __inv._name_2 + "\' ," + _g.d.ic_inventory._name_eng_1 + " = \'" + __inv._name_eng_1 + "\'," + _g.d.ic_inventory._name_eng_2 + " = \'" + __inv._name_eng_2 + "\'," + _g.d.ic_inventory._unit_standard + " =\'" + __inv._unit_standard + "\'," + _g.d.ic_inventory._unit_cost + " =\'" + __inv._unit_cost + "\'," + _g.d.ic_inventory._item_type + " = " + __inv._item_type + "," + _g.d.ic_inventory._unit_type + " =" + __inv._unit_type + "," + _g.d.ic_inventory._cost_type + " =" + __inv._cost_type + "," + _g.d.ic_inventory._tax_type + " =" + __inv._tax_type + "," + _g.d.ic_inventory._income_type + "=\'" + __inv._income_type + "\' where " + _g.d.ic_inventory._code + " =\'" + __inv._ic_code + "\'"));
                            // __query_inventory.Append(MyLib._myUtil._convertTextToXmlForQuery(" delete  from " + _g.d.ic_inventory._table + "   where " + _g.d.ic_inventory._code + " =\'" + __inv._ic_code + "\'"));

                        }
                        else
                        {
                            __query_inventory.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_inventory._table + " (" + _g.d.ic_inventory._code + "," + _g.d.ic_inventory._name_1 + "," + _g.d.ic_inventory._name_2 + "," + _g.d.ic_inventory._name_eng_1 + "," + _g.d.ic_inventory._name_eng_2 + "," + _g.d.ic_inventory._unit_standard + "," + _g.d.ic_inventory._unit_cost + "," + _g.d.ic_inventory._item_type + "," + _g.d.ic_inventory._unit_type + "," + _g.d.ic_inventory._cost_type + "," + _g.d.ic_inventory._tax_type + "," + _g.d.ic_inventory._income_type + ") values (\'" + __inv._ic_code + "\',\'" + __inv._name_1 + "\',\'" + __inv._name_2 + "\',\'" + __inv._name_eng_1 + "\',\'" + __inv._name_eng_2 + "\',\'" + __inv._unit_standard + "\',\'" + __inv._unit_cost + "\'," + __inv._item_type + "," + __inv._unit_type + "," + __inv._cost_type + "," + __inv._tax_type + ",\'" + __inv._income_type + "\')"));
                        }
                        __query_ic_unit_use.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_unit_use._table + " (" + _g.d.ic_unit_use._code + "," + _g.d.ic_unit_use._ic_code + "," + _g.d.ic_unit_use._stand_value + "," + _g.d.ic_unit_use._divide_value + "," + _g.d.ic_unit_use._ratio + "," + _g.d.ic_unit_use._status + "," + _g.d.ic_unit_use._row_order + ") values (\'" + __inv._unit_standard + "\',\'" + __inv._ic_code + "\',1,1,1,1," + __line_number_unituse + ")"));
                        __query_inventory_set_detail.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_inventory_set_detail._table + " where " + _g.d.ic_inventory_set_detail._ic_set_code + " = \'" + __ic_code.Trim() + "\'"));
                        for (int __detail = 0; __detail < __inv.__listdata.Count; __detail++)
                        {
                            __linenumber++;
                            _inventory_detail __objdetail = (_inventory_detail)__inv.__listdata[__detail];
                            string __Amount = __objdetail._Amount.Trim();
                            string __Qty = __objdetail._Qty.Trim();
                            string __Price = __objdetail._Price.Trim();
                            string __unitcode = __objdetail._Unitcode.Trim();
                            string __ic_set = __objdetail._ic_set.Trim();
                            //if (__ic_code.Equals("AN-3654-80YR16/193-3L"))
                            //{
                            //    string __xxxxx = "";
                            //}
                            __query_inventory_set_detail.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_inventory_set_detail._table + " (" + _g.d.ic_inventory_set_detail._ic_set_code + ", " + _g.d.ic_inventory_set_detail._ic_code + ", " + _g.d.ic_inventory_set_detail._unit_code + ", " + _g.d.ic_inventory_set_detail._qty + ", " + _g.d.ic_inventory_set_detail._price + ", " + _g.d.ic_inventory_set_detail._sum_amount + ", " + _g.d.ic_inventory_set_detail._status + ", " + _g.d.ic_inventory_set_detail._line_number + ") values (\'" + __ic_code + "\',\'" + __ic_set + "\',\'" + __unitcode + "\'," + __Qty + "," + __Price + "," + __Amount + ",1," + __linenumber + ")"));
                           // checkupload++;
                        }
                        checkupload++;
                        if (checkupload == 200)
                        {
                            checkupload = 0;
                            __query_inventory_set_detail.Append("</node>");
                            __query_inventory.Append("</node>");
                            __query_ic_unit_use.Append("</node>");

                            __result.Append(__myFrameWork._queryList(MyLib._myGlobal._databaseName, __query_ic_unit_use.ToString()));
                            __result.Append(__myFrameWork._queryList(MyLib._myGlobal._databaseName, __query_inventory.ToString()));
                            __result.Append(__myFrameWork._queryList(MyLib._myGlobal._databaseName, __query_inventory_set_detail.ToString()));

                            __query_inventory_set_detail = new StringBuilder();
                            __query_inventory = new StringBuilder();
                            __query_ic_unit_use = new StringBuilder();
                            __query_inventory_set_detail = new StringBuilder();

                            __query_inventory_set_detail.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            __query_inventory.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            __query_ic_unit_use.Append(MyLib._myGlobal._xmlHeader + "<node>");                            
                        }

                        SetProgressValue(100, 0, (int)(100.0 * (__row + 1) / __listmaint_temp.__list.Count));
                        SetLabelProgressText("Packing Data : " + (int)(100.0 * (__row + 1) / __listmaint_temp.__list.Count) + "%");
                    }
                    //   __message.Append("Import record = " + __totalperfile + " " + __fliename).AppendLine(); ;
                    //   __message.Append("ImportTotal record = " + __totol_inventory).AppendLine(); ;
                    //   __messagefile.Append(" Import record = " + __totalperfile + " " + __fliename).AppendLine();
                    //   __messagefile.Append(" ImportTotal record = " + __totol_inventory).AppendLine();
                    //string __newMessage = __messagefile.ToString();
                    //  __log(__newMessage);
                    __query_inventory_set_detail.Append("</node>");
                    __query_inventory.Append("</node>");
                    __query_ic_unit_use.Append("</node>");

                    //string __result = "";
                    SetLabelProgressText("Pease Wait Insert Data ");
                    __result.Append(__myFrameWork._queryList(MyLib._myGlobal._databaseName, __query_ic_unit_use.ToString()));
                    __result.Append(__myFrameWork._queryList(MyLib._myGlobal._databaseName, __query_inventory.ToString()));
                    __result.Append(__myFrameWork._queryList(MyLib._myGlobal._databaseName, __query_inventory_set_detail.ToString()));
                    if (__result.ToString().Length != 0)
                    {
                        // MessageBox.Show(__result.ToString(), "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        __log(__result.ToString());
                        SetLabelProgressText("Import Data Faile >>");
                    }
                    else
                    {
                        SetLabelProgressText("Import  Data Complete >>");
                        MessageBox.Show("Success");
                        SetProgressValue(100, 0, 0);
                        SetLabelProgressText("");
                        SetGridValue(new DataTable());
                        //  _dataGridView.DataSource = new DataTable();
                        __listmaint_temp = null;
                        __listmain = null;
                        this._listTable = new ArrayList();
                        //  _cancelThread();
                        __log("Success");
                    }

                }
            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message, "Process Error>> ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetLabelProgressText(ex.StackTrace);

            }
        }
        private void vistaButton2_Click(object sender, EventArgs e)
        {
            _isSavedata = true;
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
                                string __xxxx = "";
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
                //for (int __rowin = 0; __rowin < __dataTable.Rows.Count; __rowin++)
                //{
                //    string data = __dataTable.Rows[__rowin][54].ToString().Trim();
                //    if (_search_ic_code(data).Length == 0) { 
                //    __messagefile.Append(data).AppendLine();
                //    }
                //}
                //__log(__messagefile.ToString());
                //MessageBox.Show("Check ok");
                StringBuilder __queryIn = new StringBuilder();
                List<string> __listqueryin = new List<string>();
                List<string> __listquerydistinctIC_Code = new List<string>();
                int codein = 0;
                for (int __rowin = 0; __rowin < __dataTable.Rows.Count; __rowin++)
                {
                    string ic_code = __dataTable.Rows[__rowin][54].ToString().Trim(); // MasterCode
                    string __BD = __dataTable.Rows[__rowin][55].ToString(); // ProductCodeOfBase
                    string __M = __dataTable.Rows[__rowin][12].ToString().Trim(); //BaseName
                    string __O = __dataTable.Rows[__rowin][14].ToString().Trim(); // Ingredient0Abbr
                    string __Q = __dataTable.Rows[__rowin][16].ToString().Trim(); // Ingredient1Abbr
                    string __S = __dataTable.Rows[__rowin][18].ToString().Trim(); // Ingredient2Abbr
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
                    string __data = __listqueryin[__rowdistinct];
                    codein++;
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
                        if (((DataSet)__getData[0]).Tables[0].Rows.Count > 0)
                        {
                            _listTable.Add(((DataSet)__getData[0]).Tables[0]);
                        }
                    }
                }
                __message.Append("record = " + __dataTable.Rows.Count.ToString() + " of " + __fliename).AppendLine();
                __messagefile.Append("record = " + __dataTable.Rows.Count.ToString() + " of  " + __fliename).AppendLine();
                __listmain = new _inventorymain();
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
                        string __unitstandart = _search_unitcode(__ic_code, this._listTable);// _search_unitcode_byrow(__ic_code);
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
                        __detail._Unitcode = _search_unitcode(__BD, this._listTable);// _search_unitcode_byrow(__BD);
                        __inventory.__listdata.Add(__detail);
                        __detail = new _inventory_detail();
                        __detail._ic_set = __99;
                        __detail._Qty = "1";
                        __detail._Price = "-1";
                        __detail._Amount = "-1";
                        __detail._Unitcode = _search_unitcode(__99, this._listTable); //_search_unitcode_byrow(__99);//
                        __inventory.__listdata.Add(__detail);
                        if (__M.Length > 0 && double.Parse(__N) != 0)
                        {
                            __detail = new _inventory_detail();
                            __detail._ic_set = __M;
                            __detail._Qty = (double.Parse(__N) * MyLib._myGlobal._intPhase(_substrunitcode(_unitcode))).ToString();
                            __detail._Price = __Price.Trim();
                            __detail._Amount = __Amount.Trim(); ;
                            __detail._Unitcode = _search_unitcode(__M, this._listTable);// _search_unitcode_byrow(__M);//
                            __inventory.__listdata.Add(__detail);
                        }

                        if (__O.Length > 0 && double.Parse(__P) != 0)
                        {
                            __detail = new _inventory_detail();
                            __detail._ic_set = __O;
                            __detail._Qty = (double.Parse(__P) * MyLib._myGlobal._intPhase(_substrunitcode(_unitcode))).ToString();
                            __detail._Price = __Price.Trim();
                            __detail._Amount = __Amount.Trim();
                            __detail._Unitcode = _search_unitcode(__O, this._listTable); //_search_unitcode_byrow(__O);// 
                            __inventory.__listdata.Add(__detail);
                        }
                        if (__Q.Length > 0 && double.Parse(__R) != 0)
                        {
                            __detail = new _inventory_detail();
                            __detail._ic_set = __Q;
                            __detail._Qty = (double.Parse(__R) * MyLib._myGlobal._intPhase(_substrunitcode(_unitcode))).ToString();
                            __detail._Price = __Price.Trim();
                            __detail._Amount = __Amount.Trim();
                            __detail._Unitcode = _search_unitcode(__Q, this._listTable); //_search_unitcode_byrow(__Q);//
                            __inventory.__listdata.Add(__detail);
                        }
                        if (__S.Length > 0 && double.Parse(__T) != 0)
                        {
                            __detail = new _inventory_detail();
                            __detail._ic_set = __S;
                            __detail._Qty = (double.Parse(__T) * MyLib._myGlobal._intPhase(_substrunitcode(_unitcode))).ToString();
                            __detail._Price = __Price.Trim();
                            __detail._Amount = __Amount.Trim();
                            __detail._Unitcode = _search_unitcode(__S, this._listTable); //_search_unitcode_byrow(__S);//
                            __inventory.__listdata.Add(__detail);
                        }
                        __listmain.__list.Add(__inventory);
                        SetProgressValue(100, 0, (int)(100.0 * (__data + 1) / __dataTable.Rows.Count));
                        SetLabelProgressText("Load data xml to Grid : " + (int)(100.0 * (__data + 1) / __dataTable.Rows.Count) + "%");
                        _isloadDatasucess = true;
                    }
                    catch (Exception ex)
                    {
                        _isloadDatasucess = false;
                        SetLabelProgressText("Error Load data xml to Grid >> " + ex.Message);
                        __dataTable.Dispose();
                    }

                }
                if (_isloadDatasucess)
                {
                    SetLabelProgressText("Load data xml to Grid : 100 % Next press Process");
                    SetGridValue(__dataTable);
                    __dataTable.Dispose();
                }
            }
            catch (Exception __ex)
            {
                _isloadDatasucess = false;
                SetLabelProgressText("Error Load data xml >> " + __ex.Message);
                __dataTable.Dispose();

            }

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
                this._loadDataThread = new Thread(new ThreadStart(_buttonProcessk));
                this._loadDataThread.Start();
            }
            if (this._isloadDatasucess && _isSavedata)
            {
                this._isSavedata = false;
                _isSavedata = false;
                this._isloadDatasucess = false;
                this._SaveDataThread = new Thread(new ThreadStart(_savedata));
                this._SaveDataThread.Start();
            }
        }

        private void _bntViewProcess_Click(object sender, EventArgs e)
        {
            _frmViewProcess __log = new _frmViewProcess();
            __log._lbllog.Text = __message.ToString();
            __log.Show();
        }
    }
    public class _inventorymain
    {

        public ArrayList __list = new ArrayList();
        //ArrayList<_inventory_detail> __listIc = new ArrayList<_inventory_detail

    }
    public class _inventory
    {
        public string _ic_code;
        public string _name_1;
        public string _name_2;
        public string _name_eng_1;
        public string _name_eng_2;
        public int _item_type;
        public int _unit_type;
        public int _cost_type;
        public int _tax_type;
        public string _unit_standard;
        public string _unit_cost;
        public string _income_type;
        public List<_inventory_detail> __listdata = new List<_inventory_detail>();
        //ArrayList<_inventory_detail> __listIc = new ArrayList<_inventory_detail

    }
    public class _inventory_detail
    {
        public string _ic_set;
        public string _Qty = "1";
        public string _Price = "1";
        public string _Amount = "1";
        public string _Unitcode = "1";


    }
}
