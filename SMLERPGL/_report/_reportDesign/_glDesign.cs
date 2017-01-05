using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using SMLERPGLControl;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;

namespace SMLERPGL._report._reportDesign
{
    public partial class _glDesign : UserControl
    {
        class _valueStruct
        {
            public string _valueName = "";
            public decimal _value = 0.0M;

            public _valueStruct(string name)
            {
                this._valueName = name;
            }
        }

        private DataTable _accountList;
        private DataTable _amountList;
        private string _reportCode = "";
        private string _reportName = "";
        private List<_valueStruct> _value = new List<_valueStruct>();
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private int _valueRunning = 0;

        public _glDesign()
        {
            InitializeComponent();
            this._saveAsButton.Enabled = false;
            this._info.Text = "";
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolStripFile.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this._toolStripControl.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this._valuesGrid._isEdit = false;
                this._valuesGrid._addColumn("Name", 1, 50, 50);
                this._valuesGrid._addColumn("Value", 3, 50, 50, true, false, true, false, _g.g._getFormatNumberStr(3));
                //
                // Create Grid Column
                this._accountGrid._table_name = _g.d.gl_chart_of_account._table;
                this._accountGrid._width_by_persent = true;
                this._accountGrid._isEdit = false;
                this._accountGrid.AllowDrop = true;
                this._accountGrid._addColumn(_g.d.gl_chart_of_account._code, 1, 100, 20, false, false, false, false);
                this._accountGrid._addColumn(_g.d.gl_chart_of_account._name_1, 1, 100, 40, false, false, false, false);
                this._accountGrid._addColumn(_g.d.gl_chart_of_account._name_2, 1, 100, 35, false, false, false, false);
                this._accountGrid._addColumn(_g.d.gl_chart_of_account._account_level, 2, 100, 35, false, true, false, false);
                this._accountGrid._addColumn(_g.d.gl_chart_of_account._status, 2, 100, 35, false, true, false, false);
                this._accountGrid._beforeDisplayRow += _accountGrid__beforeDisplayRow;
                this._accountGrid.Refresh();
                // load Data
                string __query = "select  " + _g.d.gl_chart_of_account._code + "," + _g.d.gl_chart_of_account._name_1 + "," + _g.d.gl_chart_of_account._name_2 + "," + _g.d.gl_chart_of_account._account_level + "," + _g.d.gl_chart_of_account._status + " from " + _g.d.gl_chart_of_account._table + " order by " + _g.d.gl_chart_of_account._code;
                DataSet __getData = this._myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                this._accountGrid._loadFromDataTable(__getData.Tables[0]);
                this._accountGrid.Invalidate();

                // toe add html editor

            }
            //
            this._textBox.Text =
                            @"
<CENTER>
<H1>{=LTD-NAME}</H1>
<H2>งบดุล</H2>ณ.วันที่ {=TODAY-THAI}<BR>
</CENTER>
#COLUMN,1,#width,20%
#COLUMN,2,#width,40%
#COLUMN,3,#width,40%
#TABLE,BEGIN
#
#column,1-3,<b>สินทรัพย์</b>
#
#column,1-3,<tab><b>สินทรัพย์หมุนเวียน<b/>
#
#column,1-3,<tab><tab><b>เงินสดและเงินฝากสถาบันการเงิน</b>
#
#column,2,เงินสดในมือ
#column,3,{SET,A11,ACC,1111-10}
#
#column,2,เงินสดย่อย
#column,3,{SET,A12,ACC,1111-20}
#
#column,2,เงินฝากกระแสรายวัน-กสิกร
#column,3,{SET,A13,ACC,1112-10}
#
#column,2,<B>รวมเงินสดและเงินฝากสถาบันการเงิน</B>
#column,3,{SET,A19,CALC,@A11@+@A12@+@A13@}
#
#column,1-3,<tab><tab><b>ลูกหนี้การค้าและตั๋วเงินรับ</b>
#
#loop,1,account,begin,1130
#loop,1,account,end,1130-9
#loop,1,start
#column,2,ลูกหนี้การค้า-ในประเทศ
#column,3,{SET,A20,ACC,1130-00,1140-99}
#
#loop,1,stop
#TABLE,END
";
            this._processNow();
        }

        MyLib.BeforeDisplayRowReturn _accountGrid__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            return (_g.g._chartOfAccountBeforeDisplay(sender, columnNumber, columnName, senderRow, columnType, rowData));
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                this._processNow();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private List<string> _getCommand(string str)
        {
            List<string> __result = new List<string>();
            int __index = str.IndexOf('{');
            while (__index != -1)
            {
                StringBuilder __word = new StringBuilder();
                for (int __loop = __index + 1; __loop < str.Length; __loop++)
                {
                    if (str[__loop] == '}')
                    {
                        break;
                    }
                    __word.Append(str[__loop]);
                }
                __result.Add(__word.ToString());
                __index = str.IndexOf('{', __index + 1);
            }
            return __result;
        }

        private decimal _getValue(string value)
        {
            decimal __getValueResult = 0.0M;
            decimal __getValueNumber;
            string[] __getValueStrSplit = value.Split(',');
            if (__getValueStrSplit.Length > 0)
            {
                try
                {
                    switch (__getValueStrSplit[0])
                    {
                        case "CALC":
                            {
                                __getValueResult = 0;
                                try
                                {
                                    string __valueCalc = __getValueStrSplit[1].ToUpper().Replace(" ", "").Replace(" ", "").Replace(" ", "").Replace(" ", "").Replace(" ", "");
                                    for (int __getValueLoop = 0; __getValueLoop < this._value.Count; __getValueLoop++)
                                    {
                                        __valueCalc = __valueCalc.Replace("@" + this._value[__getValueLoop]._valueName + "@", "(" + this._value[__getValueLoop]._value.ToString() + ")");
                                    }
                                    if (__valueCalc.IndexOf("@") == -1)
                                    {
                                        try
                                        {
                                            __getValueResult = (decimal)new Evaluant.Calculator.Expression(__valueCalc).Evaluate();
                                        }
                                        catch
                                        {
                                            MessageBox.Show(__valueCalc.ToString());
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error : " + __valueCalc.ToString());
                                    }
                                }
                                catch
                                {
                                }
                            }
                            break;
                        case "ACC":
                            {
                                string __accountBegin = __getValueStrSplit[1];
                                string __accountEnd = (__getValueStrSplit.Length > 2) ? __getValueStrSplit[2] : __accountBegin;
                                for (int __loop = 0; __loop < this._amountList.Rows.Count; __loop++)
                                {
                                    string __accountCode = this._amountList.Rows[__loop][1].ToString();
                                    if (__accountCode.CompareTo(__accountBegin) >= 0 && __accountCode.CompareTo(__accountEnd) <= 0)
                                    {
                                        __getValueResult += MyLib._myGlobal._decimalPhase(this._amountList.Rows[__loop][0].ToString());
                                    }
                                }
                            }
                            break;
                        default:
                            if (Decimal.TryParse(value, out __getValueNumber))
                            {
                                __getValueResult = __getValueNumber;
                            }
                            break;
                    }
                }
                catch
                {
                }
            }
            return __getValueResult;
        }

        private int _findValueAddr(string name)
        {
            for (int __loop = 0; __loop < this._value.Count; __loop++)
            {
                if (this._value[__loop]._valueName.Equals(name))
                {
                    return (__loop);
                }
            }
            return -1;
        }

        private void _processNow()
        {
            string __dateBegin = MyLib._myGlobal._convertDateToQuery("2500-1-1");
            string __dateEnd = MyLib._myGlobal._convertDateToQuery("2558-12-31");
            string __fontName = "Arial";
            string __fontSize = "12px";
            string __fontColor = "black";
            string __formatNumber = "###,###,###.00";

            this._valueRunning = 0;
            this._value.Clear();
            this._preview.Navigate("about:blank");
            StringBuilder __result = new StringBuilder();
            string[] __line = this._textBox.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder __queryListAccountValue = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            // ดึงผังบัญชี
            __queryListAccountValue.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.gl_chart_of_account._code + "," + _g.d.gl_chart_of_account._name_1 + " from " + _g.d.gl_chart_of_account._table + " order by " + _g.d.gl_chart_of_account._code));
            // ดึงยอดสุดท้าย
            string __querySum = "select sum(" + _g.d.gl_journal_detail._debit + ") - sum(" + _g.d.gl_journal_detail._credit + ") as " + _g.d.gl_journal_detail._amount + "," + _g.d.gl_journal_detail._account_code + " from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._doc_date + " between \'" + __dateBegin + "\' and \'" + __dateEnd + "\' group by " + _g.d.gl_journal_detail._account_code + " order by " + _g.d.gl_journal_detail._account_code;
            __queryListAccountValue.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySum));
            __queryListAccountValue.Append("</node>");
            ArrayList __queryResult = this._myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryListAccountValue.ToString());
            this._accountList = ((DataSet)__queryResult[0]).Tables[0];
            this._amountList = ((DataSet)__queryResult[1]).Tables[0];
            int __columnMax = 0;
            int __columnBegin = 1;
            int __columnEnd = 1;
            bool __trFirst = false;
            List<string> __columnWidth = new List<string>();
            for (int __loop = 0; __loop < 100; __loop++)
            {
                __columnWidth.Add("");
            }
            for (int __row = 0; __row < __line.Length; __row++)
            {
                string __str = __line[__row].Trim();
                if (__str.Length > 0)
                {
                    if (__str.Length == 1 && __str[0] == '#')
                    {
                        if (__trFirst)
                        {
                            __result.Append("</tr>\n");
                        }
                        __trFirst = true;
                        __result.Append("<tr>");
                        __columnBegin = 1;
                        __columnEnd = 1;
                    }
                    else
                    {
                        if (__str[0] == '#')
                        {
                            try
                            {
                                string[] __arrayStr = __str.Split(',');
                                string __valueStr = "";
                                if (__arrayStr.Length > 2)
                                {
                                    __valueStr = __str.Remove(0, __arrayStr[0].Length + __arrayStr[1].Length + 2);
                                }
                                switch (__arrayStr[0].ToUpper())
                                {
                                    case "#TABLE":
                                        {
                                            string __command = __arrayStr[1].ToUpper();
                                            if (__command.Equals("BEGIN"))
                                            {
                                                __result.Append("<table width=100%' border=1>");
                                            }
                                            else
                                            {
                                                if (__command.Equals("END"))
                                                {
                                                    __result.Append("</table>");
                                                }
                                            }
                                        }
                                        break;
                                    case "#COLUMN":
                                        {
                                            Boolean __isColumnSpan = false;
                                            string __columnNumberStr = __arrayStr[1].ToUpper();
                                            while (__columnNumberStr.IndexOf(" ") != -1)
                                            {
                                                __columnNumberStr = __columnNumberStr.Replace("  ", "");
                                            }
                                            if (__columnNumberStr.IndexOf('-') != -1)
                                            {
                                                string[] __split = __columnNumberStr.Split('-');
                                                __columnBegin = MyLib._myGlobal._intPhase(__split[0]);
                                                __columnEnd = MyLib._myGlobal._intPhase(__split[1]);
                                                __isColumnSpan = true;
                                            }
                                            else
                                            {
                                                __columnEnd = MyLib._myGlobal._intPhase(__columnNumberStr);
                                            }
                                            if (__columnMax < __columnEnd)
                                            {
                                                __columnMax = __columnEnd;
                                            }
                                            string __command = __arrayStr[2].ToUpper();
                                            switch (__command)
                                            {
                                                case "#WIDTH":
                                                    {
                                                        __columnWidth[__columnEnd] = __arrayStr[3];
                                                    }
                                                    break;
                                                default:
                                                    {
                                                        string __align = "";
                                                        string __strProcess = __valueStr;
                                                        if (__strProcess.Length > 0 && __strProcess[0] == '{')
                                                        {
                                                            __strProcess = __strProcess.Replace("{", "").Replace("}", "");
                                                            decimal __value = 0;
                                                            //
                                                            string[] __strSplit = __strProcess.Split(',');
                                                            if (__strSplit[0].ToUpper().Equals("SET"))
                                                            {
                                                                try
                                                                {
                                                                    string __valueName = __strSplit[1].ToUpper();
                                                                    // ค้นหาตัวแปร
                                                                    int __rowValue = this._findValueAddr(__valueName);
                                                                    if (__rowValue == -1)
                                                                    {
                                                                        // เพิ่มตัวแปรใหม่
                                                                        this._value.Add(new _valueStruct(__valueName));
                                                                        __rowValue = this._value.Count - 1;
                                                                    }
                                                                    string __command2 = __valueStr.Replace("{", "").Replace("}", "").Remove(0, __strSplit[0].Length + __strSplit[1].Length + 2);
                                                                    __value = this._getValue(__command2);
                                                                    this._value[__rowValue]._value += __value;
                                                                }
                                                                catch
                                                                {
                                                                }
                                                            }
                                                            //
                                                            if (__value == 0.0M)
                                                            {
                                                                __strProcess = "&nbsp;";
                                                            }
                                                            else
                                                            {
                                                                __align = " align='right' ";
                                                                if (__value >= 0.0M)
                                                                {
                                                                    if (this._formulaCheckBox.MyCheckBox.Checked)
                                                                    {
                                                                    }
                                                                    else
                                                                    {
                                                                        __strProcess = __strProcess = "&nbsp;" + __value.ToString(__formatNumber) + "&nbsp;";
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (this._formulaCheckBox.MyCheckBox.Checked)
                                                                    {
                                                                    }
                                                                    else
                                                                    {
                                                                        __value = __value * -1;
                                                                        __strProcess = "(" + __value.ToString(__formatNumber) + ")";
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        if (__columnBegin != __columnEnd)
                                                        {
                                                            if (__isColumnSpan)
                                                            {
                                                                __result.Append("<td " + __align + "colspan =" + ((__columnEnd - __columnBegin) + 1).ToString() + ">");
                                                            }
                                                            else
                                                            {
                                                                for (int __loop = __columnBegin; __loop < __columnEnd; __loop++)
                                                                {
                                                                    __result.Append("<td " + __align + "width='" + __columnWidth[__loop] + "'>&nbsp;</td>\n");
                                                                }
                                                                __result.Append("<td " + __align + "width='" + __columnWidth[__columnEnd] + "'>");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            __result.Append("<td " + __align + "width='" + __columnWidth[__columnEnd] + "'>");
                                                        }
                                                        __columnBegin = __columnEnd + 1;
                                                        __result.Append(__strProcess.Replace("  ", "&nbsp;"));
                                                        __result.Append("</td>\n");
                                                    }
                                                    break;
                                            }
                                        }
                                        break;
                                }
                            }
                            catch
                            {

                            }
                        }
                        else
                        {
                            string __str2 = __str;
                            List<string> __command = _getCommand(__str);
                            for (int __loop = 0; __loop < __command.Count; __loop++)
                            {
                                string __cmd = __command[__loop].ToUpper();
                                if (__cmd[0] == '=' || __cmd[0] == '*')
                                {
                                    __cmd = __cmd.Remove(0, 1);
                                    string[] __strSplit = __cmd.Split(',');
                                    if (__strSplit.Length > 1)
                                    {
                                        string __valueName = "";
                                        if (__strSplit[0].ToUpper().Equals("RUN"))
                                        {

                                        }
                                        else
                                        {
                                            if (__strSplit[0].ToUpper().Equals("SET"))
                                            {
                                                try
                                                {
                                                    __valueName = __strSplit[1].ToUpper();
                                                    if (__valueName.IndexOf('*') != -1)
                                                    {
                                                        this._valueRunning++;
                                                        __valueName = __valueName.Replace("*", this._valueRunning.ToString());
                                                    }
                                                    switch (__valueName)
                                                    {
                                                        case "RUNNING":
                                                            {
                                                                string __command2 = __cmd.Remove(0, __strSplit[0].Length + __strSplit[1].Length + 2);
                                                                this._valueRunning = (int)this._getValue(__command2);
                                                            }
                                                            break;
                                                        default:
                                                            {
                                                                // ค้นหาตัวแปร
                                                                int __rowValue = this._findValueAddr(__valueName);
                                                                if (__rowValue == -1)
                                                                {
                                                                    // เพิ่มตัวแปรใหม่
                                                                    this._value.Add(new _valueStruct(__valueName));
                                                                    __rowValue = this._value.Count - 1;
                                                                }
                                                                string __command2 = __cmd.Remove(0, __strSplit[0].Length + __strSplit[1].Length + 2);
                                                                this._value[__rowValue]._value += this._getValue(__command2);
                                                            }
                                                            break;
                                                    }
                                                }
                                                catch
                                                {
                                                }
                                            }
                                        }
                                        if (this._formulaCheckBox.MyCheckBox.Checked)
                                        {
                                            __str = __str.Replace("{" + __command[__loop] + "}", "{" + __command[__loop] + "}{<font color='blue'>" + __valueName + "</font>}");
                                        }
                                        else
                                        {
                                            __str = __str.Replace("{" + __command[__loop] + "}", "&nbsp;");
                                        }
                                    }
                                    switch (__cmd)
                                    {
                                        case "LTD-NAME":
                                            {
                                                if (this._formulaCheckBox.MyCheckBox.Checked == false)
                                                {
                                                    __str = __str.Replace("{" + __command[__loop] + "}", MyLib._myGlobal._ltdName);
                                                }
                                            }
                                            break;
                                        case "TODAY-THAI":
                                            {
                                                if (this._formulaCheckBox.MyCheckBox.Checked == false)
                                                {
                                                    string __value = "1 สิงหาคม 2558";
                                                    __str = __str.Replace("{" + __command[__loop] + "}", __value);
                                                }
                                            }
                                            break;
                                        default:
                                            {
                                                decimal __value = 0.0M;
                                                string[] __cmd2 = __cmd.Split('@');
                                                string __valueName = __cmd2[0].ToString();
                                                if (__valueName.IndexOf('*') != -1)
                                                {
                                                    __valueName = __valueName.Replace("*", this._valueRunning.ToString());
                                                }

                                                int __rowValue = this._findValueAddr(__valueName);
                                                if (__rowValue != -1)
                                                {
                                                    if (__cmd.IndexOf('@') != -1)
                                                    {
                                                        // ตรวจสอบว่ามีการขอเป็น Debit หรือ เครดิต
                                                        try
                                                        {
                                                            if (__cmd2[1][0] == 'X')
                                                            {
                                                                __value = this._value[__rowValue]._value * -1;
                                                            }
                                                            else
                                                            {
                                                                if (__cmd2[1][0] == 'D' && this._value[__rowValue]._value >= 0)
                                                                {
                                                                    __value = this._value[__rowValue]._value;
                                                                }
                                                                if (__cmd2[1][0] == 'C' && this._value[__rowValue]._value < 0)
                                                                {
                                                                    __value = this._value[__rowValue]._value * -1;
                                                                }
                                                            }
                                                        }
                                                        catch
                                                        {
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // ไม่มีการขอ ให้ Return ค่าจริงไปเลย
                                                        __value = this._value[__rowValue]._value;
                                                    }
                                                    if (__value == 0.0M)
                                                    {
                                                        __str = __str.Replace("{" + __command[__loop] + "}", "&nbsp;");
                                                    }
                                                    else
                                                    {
                                                        if (__value >= 0.0M)
                                                        {
                                                            if (this._formulaCheckBox.MyCheckBox.Checked)
                                                            {
                                                                __str = __str.Replace("{" + __command[__loop] + "}", "{" + __command[__loop] + "}<font color='blue'>{" + __valueName + "</font>}");
                                                            }
                                                            else
                                                            {
                                                                __str = __str.Replace("{" + __command[__loop] + "}", "&nbsp;" + __value.ToString(__formatNumber) + "&nbsp;");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (this._formulaCheckBox.MyCheckBox.Checked)
                                                            {
                                                                __str = __str.Replace("{" + __command[__loop] + "}", "{" + __command[__loop] + "}<font color='blue'>{" + __valueName + "</font>}");
                                                            }
                                                            else
                                                            {
                                                                __value = __value * -1;
                                                                __str = __str.Replace("{" + __command[__loop] + "}", "(" + __value.ToString(__formatNumber + ")"));
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                            __result.Append(__str + "\n");
                        }
                    }
                }
            }
            this._preview.Document.OpenNew(false);
            StringBuilder __html = new StringBuilder();
            __html.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            __html.Append("<head>");
            __html.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
            __html.Append("<style type=\"text/css\">");
            __html.Append("body  { ");
            __html.Append("font-family: " + __fontName + ";");
            __html.Append("font-size: " + __fontSize + ";");
            __html.Append("}");
            __html.Append("</style> ");
            __html.Append("</head>");
            __html.Append("<body>");
            string __resultStr = __result.ToString();
            __resultStr = __resultStr.Replace("<tab>", "<span style='display: inline - block; width: 20px; '></span>");
            __html.Append(__resultStr);
            __html.Append("</body>");
            __html.Append("</html>");
            this._preview.Document.Write(__html.ToString());
            this._preview.Refresh();
            //
            this._valuesGrid._clear();
            for (int __loop = 0; __loop < this._value.Count; __loop++)
            {
                int __addr = this._valuesGrid._addRow();
                this._valuesGrid._cellUpdate(__addr, 0, this._value[__loop]._valueName, false);
                this._valuesGrid._cellUpdate(__addr, 1, this._value[__loop]._value, false);
            }
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            this._processNow();
        }

        void _saveAs(string code)
        {
            _saveAsControl __saveAs = new _saveAsControl(code, this._textBox.Text);
            __saveAs.ShowDialog();
            if (__saveAs._codeReturn.Length > 0)
            {
                this._reportCode = __saveAs._codeReturn;
                this._reportName = __saveAs._nameReturn;
                this._info.Text = this._reportCode + " (" + this._reportName + ")";
            }
        }
        private void _saveButton_Click(object sender, EventArgs e)
        {
            if (this._reportCode == "")
            {
                this._saveAs(this._reportCode);
            }
            else
            {
                string __query = "update " + _g.d.gl_design_report._table + " set " + _g.d.gl_design_report._data + " = '" + Convert.ToBase64String(Encoding.Unicode.GetBytes(this._textBox.Text)) + "' where " + _g.d.gl_design_report._code + "='" + this._reportCode + "'";
                string __result = _myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __query);
                if (__result.Length != 0)
                {
                    MessageBox.Show(__result);
                }
            }
        }

        private void _saveAsButton_Click(object sender, EventArgs e)
        {
            this._saveAs("");
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _loadButton_Click(object sender, EventArgs e)
        {
            _loadForm __load = new _loadForm();
            __load._dataGrid._mouseClick += (s1, e1) =>
            {
                string __code = ((MyLib._myGrid)s1)._cellGet(e1._row, _g.d.gl_design_report._code).ToString();
                __load.Dispose();
                DataTable __data = this._myFrameWork._queryShort("select " + _g.d.gl_design_report._data + "," + _g.d.gl_design_report._name1 + " from " + _g.d.gl_design_report._table + " where " + _g.d.gl_design_report._code + "='" + __code + "'").Tables[0];
                //string __dataStr = __data.Rows[0][0].ToString();
                this._textBox.Text = Encoding.Unicode.GetString(Convert.FromBase64String(__data.Rows[0][0].ToString()));
                this._processNow();
                this._reportCode = __code;
                this._reportName = __data.Rows[0][1].ToString();
                this._info.Text = this._reportCode + " (" + this._reportName + ")";
                this._saveAsButton.Enabled = true;
            };
            __load.ShowDialog();
        }

        private void _newButton_Click(object sender, EventArgs e)
        {
            Boolean __confirm = true;
            if (this._textBox.Text.Length > 0)
            {
                if (MessageBox.Show("ต้องการสร้างงบใหม่", "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    __confirm = false;
                }
            }
            if (__confirm)
            {
                this._reportCode = "";
                this._reportName = "";
                this._info.Text = "";
                this._saveAsButton.Enabled = false;
                this._textBox.Text = "";
                this._processNow();
            }
        }
    }
}
