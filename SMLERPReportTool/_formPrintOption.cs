using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

namespace SMLERPReportTool
{
    public partial class _formPrintOption : Form
    {
        public System.Drawing.Printing.PrintRange _printRange
        {
            get
            {
                return (_printAllPageRadio.Checked == true) ? System.Drawing.Printing.PrintRange.AllPages : System.Drawing.Printing.PrintRange.SomePages;
            }
        }

        public System.Drawing.Printing.PrintRange _printSeriesRangeOption
        {
            get
            {
                return (_allSeriesRadio.Checked == true) ? System.Drawing.Printing.PrintRange.AllPages : System.Drawing.Printing.PrintRange.SomePages;
            }
        }

        public int[] _printSeriesRange
        {
            get
            {
                return _getPageRange(_printDocSeriesNoTextBox);
            }
        }

        public int[] _printPageRange
        {
            get
            {
                return _getPageRange(_printRangePageTextbox);
            }
        }

        public int[] _getPageRange(TextBox __targetTextBox)
        {

            if (!__targetTextBox.Text.Equals(""))
            {
                List<int> __pageRange = new List<int>();
                // process range page
                string[] __pageList = __targetTextBox.Text.Split(',');
                foreach (string __pageSplit in __pageList)
                {
                    if (__pageSplit.IndexOf("-") == -1)
                    {
                        // single page
                        // checcl pagename ex. first,last,หน้าแรก,n
                        switch (__pageSplit.Trim().ToLower())
                        {
                            case "b":
                            case "begin":
                            case "first":
                                // หน้าแรก 
                                __pageRange.Add(1);
                                break;
                            case "n":
                            case "l":
                            case "last":
                                // หน้าสุดท้าย 
                                __pageRange.Add(999999999);

                                break;
                            default:
                                // check page number
                                if (MyLib._myGlobal._decimalPhase(__pageSplit) != 0)
                                {
                                    int __pageNumber = (int)MyLib._myGlobal._decimalPhase(__pageSplit);
                                    if (!__pageRange.Contains(__pageNumber))
                                    {
                                        __pageRange.Add(__pageNumber);
                                    }
                                }
                                break;
                        }
                    }
                    else
                    {
                        // range page ex 2-4
                        string[] _pageRangeStr = __pageSplit.Split('-');
                        if (_pageRangeStr.Length > 1)
                        {
                            //  -                             int __startPage = (__pageRange[0] > __pageRange[__pageRange.Count]) ? __pageRange[__pageRange.Count] : __pageRange[0];
                            //  -                             int __endPage = (__pageRange[__pageRange.Count] < __pageRange[0]) ? __pageRange[0] : __pageRange[__pageRange.Count];


                            int __startPage = MyLib._myGlobal._intPhase(_pageRangeStr[0]);
                            int __endPage = MyLib._myGlobal._intPhase(_pageRangeStr[_pageRangeStr.Length - 1]);

                            if (__startPage > __endPage)
                            {
                                int __temp = __startPage;
                                __startPage = __endPage;
                                __endPage = __temp;
                            }

                            for (int __i = __startPage; __i <= __endPage; __i++)
                            {
                                __pageRange.Add(__i);
                            }
                        }
                    }
                }

                // sort ASC
                __pageRange.Sort();

                return __pageRange.ToArray();
            }

            return null;

        }

        public List<string> __formCodeList = new List<string>();
        public string _screenCode = "";
        public _formPrintOption()
        {
            InitializeComponent();
            _getPrinterDevice();
        }

        public _formPrintOption(string _mode, string formCode)
        {
            InitializeComponent();

            //this._myScreen1._maxColumn = 1;
            _screenCode = _mode;
            _getPrinterDevice();
            _getFormName(formCode);

            this.Load += new EventHandler(_formPrintOption_Load);
        }

        void _formPrintOption_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.pictureBox2, "ป้อนช่วงหน้า หรือ หมายเลขหน้า \nโดยใช้ \", \" สำหรับคั่นแต่ละช่วง \nตัวอย่าง 1,2,4-5,n");
        }

        private void _getPrinterDevice()
        {
            //ManagementObjectSearcher __printerList = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");

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
                _printerCombo.Items.Add(__printerName);
                __count++;
            }

            if (_printerCombo.Items.Count > 0)
                _printerCombo.SelectedIndex = __default;
        }

        private void _getFormName(string formCode)
        {
            string[] __formCodeList = formCode.Split(',');

            MyLib._myFrameWork __fw = new MyLib._myFrameWork();
            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

            List<string> __listFormCode = new List<string>();
            foreach (string __str in __formCodeList)
            {
                //__listFormCode.Add("'" + __str.Trim().ToUpper() + "'");
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.formdesign._formguid_code + ", " + _g.d.formdesign._formcode + ", " + _g.d.formdesign._formname + ", " + _g.d.formdesign._timeupdate + " from " + _g.d.formdesign._table + " where upper(" + _g.d.formdesign._formcode + ") = '" + __str.Trim().ToUpper() + "'"));
            }

            //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.formdesign._formguid_code + ", " + _g.d.formdesign._formcode + ", " + _g.d.formdesign._formname + ", " + _g.d.formdesign._timeupdate + " from " + _g.d.formdesign._table + " where upper(" + _g.d.formdesign._formcode + ") IN (" + string.Join(",", __listFormCode.ToArray()) + ")"));
            __query.Append("</node>");

            ArrayList __result = __fw._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
            if (__result.Count > 0)
            {
                int __screenHeight = Screen.FromControl(this).Bounds.Height - 330;
                int __rowHeight = (int)((this._myScreen1.Font.GetHeight() + 8) + (this._myScreen1.Padding.Top + 1));

                int __maxRow = (__screenHeight / __rowHeight);

                //DataTable __table = ((DataSet)__result[0]).Tables[0];
                //DataSet __ds = __result as DataSet;
                int rows = 0;
                float __divide = (float)(__result.Count / (float)__maxRow);
                int __maxColumn = (int)Math.Ceiling(__divide);
                if (__maxColumn > 1)
                {
                    this._myScreen1.Width += ((__maxColumn - 1) * this._myScreen1.Width);
                    this._myScreen1._maxColumn += (__maxColumn - 1);
                    this.Invalidate();
                }
                int __currentColumn = 0;
                for (int __i = 0; __i < __result.Count; __i++)
                {
                    DataSet __ds = __result[__i] as DataSet;
                    if (__ds.Tables.Count != 0 && __ds.Tables[0].Rows.Count != 0)
                    {

                        this.__formCodeList.Add(__ds.Tables[0].Rows[0][_g.d.formdesign._formcode].ToString());
                        this._myScreen1._addCheckBox(rows, __currentColumn++, __ds.Tables[0].Rows[0][_g.d.formdesign._formcode].ToString(), false, true, false, __ds.Tables[0].Rows[0][_g.d.formdesign._formname].ToString());

                        if (__maxColumn == __currentColumn)
                        {
                            __currentColumn = 0;
                            rows++;
                        }
                    }

                }

                //this._myScreen1.Invalidate();
                //_formCombo.DisplayMember = _g.d.formdesign._formname;
                //_formCombo.ValueMember = _g.d.formdesign._formcode;
                //_formCombo.DataSource = __table;
            }

        }

        public void _setCheckedPrintFormName(string __name)
        {
            Control __getControl = this._myScreen1._getControl(__name);
            if (__getControl != null)
            {
                if (__getControl.GetType() == typeof(MyLib._myCheckBox))
                {
                    ((MyLib._myCheckBox)__getControl).Checked = true;
                }
            }
        }

        public void _setPrintNameSelectIndex(string printName)
        {
            int __index = 0;
            for (__index = 0; __index < _printerCombo.Items.Count; __index++)
            {
                if (((string)_printerCombo.Items[__index]).Equals(printName))
                {
                    break;
                }
            }

            if (__index == _printerCombo.Items.Count)
            {
                __index = 0;
            }

            _printerCombo.SelectedIndex = __index;
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            // toe fix per webservcie per database 
            string __getTempFileByServer = MyLib._myGlobal._getFirstWebServiceServer.Replace(".", "_").Replace(":", "__") + "-" + MyLib._myGlobal._databaseName + "-";

            string __currentConfigFileName = __getTempFileByServer + "configPrinterScreen" + _screenCode + ".xml";
            string __path = Path.GetTempPath() + "\\" + __currentConfigFileName.ToLower();

            _PrintConfig __config = new _PrintConfig();

            // print multi form
            /*
            if (_formCombo.SelectedValue != null)
            {
            */
            //__config.FormCode = _formCombo.SelectedValue.ToString();

            for (int __i = 0; __i < this.__formCodeList.Count; __i++)
            {

                if (this._myScreen1._getDataStr(this.__formCodeList[__i]).Equals("1"))
                {
                    __config.FormCode.Add(this.__formCodeList[__i]);
                }

            }

            __config.PrinterName = _printerCombo.Text.ToString();
            __config.isPreview = _previewPrintCheck.Checked;
            __config.isPrint = _printCheck.Checked;
            __config.ShowAgain = _showagainCheck.Checked;
            __config.ScreenCode = _screenCode;
            //__config.printAttactForm = _printVatCheck.Checked;

            XmlSerializer __colXs = new XmlSerializer(typeof(_PrintConfig));
            TextWriter __memoryStream = new StreamWriter(__path);
            __colXs.Serialize(__memoryStream, __config);
            __memoryStream.Close();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Hide();

            // print multi form
            /*
            }
            else
            {
                //MessageBox.Show("กรุณาเลือกฟอร์มที่ต้องการพิมพ์");
            }
             * */
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Hide();
        }

        private void _printSomePageRadio_CheckedChanged(object sender, EventArgs e)
        {
            _printRangePageTextbox.Enabled = (_printSomePageRadio.Checked) ? true : false;
            _panelIncludeDoc.Enabled = (_printSomePageRadio.Checked) ? true : false;
        }

        private void _includeDocSeriesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            _panelDocSeries.Enabled = (_includeDocSeriesCheckbox.Checked) ? true : false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            _printDocSeriesNoTextBox.Enabled = (_someSeriesRadio.Checked) ? true : false;
        }
    }

    public class _selectFormScreen : MyLib._myScreen
    {
        public _selectFormScreen()
        {
            this._maxColumn = 1;
            //this._maxLabelWidth = new int[] {5};
            //this._addCheckBox(1, 1, "test", true, true, false, "test");
            //this._addTextBox(2, 1
        }
    }

    [Serializable]
    public class _PrintConfig
    {
        public string ScreenCode;
        public string PrinterName;

        //public string FormCode;
        [XmlArrayItem("Form", typeof(string))]
        public ArrayList FormCode = new ArrayList();

        public Boolean isPrint;
        public Boolean isPreview;
        public Boolean printAttactForm;
        public Boolean ShowAgain;
    }

    public class _formObject
    {
        public string Name;
        public string Value;
    }
}
