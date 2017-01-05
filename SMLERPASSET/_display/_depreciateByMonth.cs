using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.IO;
using System.Globalization;

namespace SMLERPASSET._display
{
    public partial class _depreciateByMonth : UserControl
    {
        public _depreciateByMonth()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //**************************
            string formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            this._myGrid1._isEdit = false;
            //this._myGrid1._width_by_persent = true;
            this._myGrid1._addColumn("as_resource.as_code", 1, 25, 20, true, false);
            this._myGrid1._addColumn("as_resource.as_name", 1, 100, 70, true, false);
            this._myGrid1._addColumn("as_resource.as_result", 3, 0, 10, true, false, true, false, formatNumber);
            //**************************
            this._myGrid1._setColumnBackground("as_resource.as_result", Color.AliceBlue);
            //this._myGrid1._calcPersentWidthToScatter();
            // Event Screen
            this._selectAsset1._saveKeyDown += new MyLib.SaveKeyDownHandler(_selectAsset1__saveKeyDown);
        }

        void _selectAsset1__saveKeyDown(object sender)
        {
            _process();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F12)
            {
                _process();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            _process();
        }

        public ArrayList _getAssetForProcess(DateTime dateBegin, DateTime dateEnd, int processBy, int processMode)
        {
            ArrayList __result = new ArrayList();
            DataSet __ds = new DataSet();
            string getString = "";
            try
            {
                __result.Clear();
                SMLProcess._asProcess __process = new SMLProcess._asProcess();
                getString = __process._asViewDepreciateBalance(dateBegin, dateEnd, processBy, processMode);
                XmlDocument __readXmlDoc = new XmlDocument();
                __readXmlDoc.LoadXml(getString);
                XmlNodeList __tableNodes = __readXmlDoc.SelectNodes("//ResultSet");
                foreach (XmlNode __getTableNode in __tableNodes)
                {
                    DataSet __getData = new DataSet();
                    XmlTextReader __readTableXml = new XmlTextReader(new StringReader("<ResultSet>" + __getTableNode.InnerXml + "</ResultSet>"));
                    try
                    {
                        __getData.ReadXml(__readTableXml, XmlReadMode.InferSchema);
                        if (__getData.Tables.Count == 0)
                        {
                            __getData.Tables.Add(new DataTable());
                        }
                    }
                    catch
                    {
                        __getData.Tables.Add(new DataTable());
                    }
                    __result.Add(__getData);
                }
            }
            catch
            {
            }
            return (__result);
        }

        public void _process()
        {
            this._myGrid1.Focus();
            string __getScreenEmtry = this._selectAsset1._checkEmtryField();
            if (__getScreenEmtry.Length != 0)
            {
                MessageBox.Show((MyLib._myGlobal._language == 0) ? "กรุณาบันทึกรายละเอียดให้ครบก่อน" : "Please enter ref field");
            }
            else
            {
                DateTime __getDateBegin = MyLib._myGlobal._convertDate(this._selectAsset1._getDataStr("as_resource.date_begin"));
                DateTime __getDateEnd = MyLib._myGlobal._convertDate(this._selectAsset1._getDataStr("as_resource.date_end"));
                int __dateCompare = DateTime.Compare(__getDateBegin, __getDateEnd);
                if (__dateCompare > 0)
                {
                    MessageBox.Show((MyLib._myGlobal._language == 0) ? "วันที่สิ้นสุดต้องมากกว่าหรือเท่ากับวันที่เริ่มต้นเท่านั้น" : "Enddate much more than or equal to Begindate");
                }
                else
                {
                    // processBy 1=Day,2=Month,3=Year
                    int __processBy = 2;
                    // processMode 0=ตามหลักกรมสรรพากร, 1=ตามหลักการบัญชี, 2=ตามจำนวนวันจริง
                    Control __getControl = this._selectAsset1._getControl("as_resource.process_mode");
                    MyLib._myComboBox getData = (MyLib._myComboBox)__getControl;
                    int __processMode = getData.SelectedIndex;
                    this.Cursor = Cursors.WaitCursor;
                    ArrayList __result = _getAssetForProcess(__getDateBegin, __getDateEnd, __processBy, __processMode);
                    DataSet __getAssets = null;
                    // Have Data
                    if (__result.Count > 0)
                    {
                        __getAssets = (DataSet)__result[0];
                        // Create Column
                        DateTime __monthColumnBegin = __getDateBegin;
                        DateTime __monthColumnEnd = __getDateEnd;
                        int __diffColumn = 12 * (__monthColumnEnd.Year - __monthColumnBegin.Year) + __monthColumnEnd.Month - __monthColumnBegin.Month;
                        this._myGrid1._clearGridColumn();
                        this._myGrid1._clear();
                        string formatNumber = MyLib._myGlobal._getFormatNumber("m02");
                        this._myGrid1._isEdit = false;
                        this._myGrid1._width_by_persent = false;
                        this._myGrid1._addColumn("as_resource.as_code", 1, 25, 100, true, false);
                        this._myGrid1._addColumn("as_resource.as_name", 1, 100, 250, true, false);
                        this._myGrid1._addColumn("as_resource.buy_price", 3, 0, 90, true, false, true, false, formatNumber);
                        this._myGrid1._addColumn("as_resource.as_come", 3, 0, 90, true, false, true, false, formatNumber);
                        Calendar cal = CultureInfo.CurrentCulture.DateTimeFormat.Calendar;
                        for (int __col = 0; __col <= __diffColumn; __col++)
                        {
                            string __columnName = MyLib._myGlobal._convertDateToString(Convert.ToDateTime(__monthColumnBegin), false);
                            __columnName = __columnName.Substring(2, __columnName.Length - 2);
                            this._myGrid1._addColumn(__columnName, 3, 0, 90, true, false, true, false, formatNumber);
                            __monthColumnBegin = cal.AddMonths(__monthColumnBegin, 1);
                        }
                        this._myGrid1._addColumn("as_resource.as_result", 3, 0, 90, true, false, true, false, formatNumber);
                        this._myGrid1._addColumn("as_resource.value_go", 3, 0, 90, true, false, true, false, formatNumber);
                        //**************************
                        this._myGrid1._setColumnBackground("as_resource.buy_price", Color.AliceBlue);
                        this._myGrid1._setColumnBackground("as_resource.as_come", Color.AliceBlue);
                        this._myGrid1._setColumnBackground("as_resource.as_result", Color.AliceBlue);
                        this._myGrid1._setColumnBackground("as_resource.value_go", Color.AliceBlue);
                        this._myGrid1._total_show = true;
                        //this._myGrid1._calcPersentWidthToScatter();
                        //**************
                        int __resultCount = __getAssets.Tables[0].Rows.Count;
                        int __columnAssetCode = __getAssets.Tables[0].Columns.IndexOf("as_code");
                        int __columnAssetName = __getAssets.Tables[0].Columns.IndexOf("as_name");
                        int __columnAssetResult = __getAssets.Tables[0].Columns.IndexOf("as_result");
                        int __columnAssetBuyPrice = __getAssets.Tables[0].Columns.IndexOf("as_buyprice");
                        int __columnAssetCome = __getAssets.Tables[0].Columns.IndexOf("as_come");
                        int __columnAssetValueGo = __getAssets.Tables[0].Columns.IndexOf("as_valuego");
                        for (int __row = 0; __row < __resultCount; __row++)
                        {
                            decimal _dResult = (decimal)Double.Parse(__getAssets.Tables[0].Rows[__row].ItemArray[__columnAssetResult].ToString());
                            decimal _dBuyPrice = (decimal)Double.Parse(__getAssets.Tables[0].Rows[__row].ItemArray[__columnAssetBuyPrice].ToString());
                            decimal _dCome = (decimal)Double.Parse(__getAssets.Tables[0].Rows[__row].ItemArray[__columnAssetCome].ToString());
                            decimal _dValueGo = (decimal)Double.Parse(__getAssets.Tables[0].Rows[__row].ItemArray[__columnAssetValueGo].ToString());
                            //***********************
                            int __newRow = this._myGrid1._addRow();
                            this._myGrid1._cellUpdate(__newRow, "as_resource.as_code", __getAssets.Tables[0].Rows[__row].ItemArray[__columnAssetCode].ToString(), false);
                            this._myGrid1._cellUpdate(__newRow, "as_resource.as_name", __getAssets.Tables[0].Rows[__row].ItemArray[__columnAssetName].ToString(), false);
                            this._myGrid1._cellUpdate(__newRow, "as_resource.buy_price", _dBuyPrice, false);
                            this._myGrid1._cellUpdate(__newRow, "as_resource.as_come", _dCome, false);
                            for (int __coladd = 0; __coladd <= __diffColumn; __coladd++)
                            {
                                decimal _dDay = (decimal)Double.Parse(__getAssets.Tables[0].Rows[__row].ItemArray[__coladd + 4].ToString());
                                this._myGrid1._cellUpdate(__newRow, __coladd + 4, _dDay, false);
                            }
                            this._myGrid1._cellUpdate(__newRow, "as_resource.as_result", _dResult, false);
                            this._myGrid1._cellUpdate(__newRow, "as_resource.value_go", _dValueGo, false);
                        }
                        this.Cursor = Cursors.Default;
                    }
                    else
                    {
                        MessageBox.Show((MyLib._myGlobal._language == 0) ? "ไม่พบข้อมูลที่ต้องการ" : "Not Found Data");
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }
    }
}
