using System;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;

namespace MyLib
{
    public partial class _myScreen : UserControl
    {
        public int _maxColumn = 2;
        public string _table_name = "";
        public Boolean _isSelecctLabel = false;
        public int[] _maxLabelWidth = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public Boolean _getResource = true;
        public Boolean _isChangeResult = false;
        public Control _lastControl = null;
        public TextBox _lastTextBox = null;
        public Boolean _autoUpperString = true;
        public Boolean _f12 = true;
        public String _recentXmlFileName = "";

        /// <summary>
        /// ช่องไฟ
        /// </summary>
        public int _lineSpace = 0;

        public _myScreen()
        {
            this.DoubleBuffered = true;
            /*this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UseTextForAccessibility, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.CacheText, true);*/
            //
            //this.ControlAdded += new ControlEventHandler(_myScreen_ControlAdded);
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this.Width = 100;
            this.Height = 10;
            this.BackColor = Color.Transparent;
            this.SizeChanged += new EventHandler(_myScreen_SizeChanged);
            this.Invalidated += new InvalidateEventHandler(_myScreen_Invalidated);
        }

        public void _enabedControl(string name, Boolean value)
        {
            Control __getControl = this._getControl(name);
            if (__getControl != null)
            {
                __getControl.Enabled = value;
            }
        }

        public void _setRecent(string name, Boolean value)
        {
            Control __getControl = this._getControl(name);
            if (__getControl != null)
            {
                if (__getControl.GetType() == typeof(_myTextBox))
                {
                    ((_myTextBox)__getControl)._useRecentValue = value;
                }
                else if (__getControl.GetType() == typeof(_myComboBox))
                {
                    ((_myComboBox)__getControl)._useRecentValue = value;
                }
            }
        }

        public System.Json.JsonObject _getJson()
        {
            System.Json.JsonObject __json = new System.Json.JsonObject();

            // 
            foreach (Control __getControl in this.Controls)
            {
                if (__getControl.GetType() == typeof(_myTextBox))
                {
                    _myTextBox __getText = (_myTextBox)__getControl;
                    if (__getText._isQuery)
                    {
                        string __value = "";

                        if (__getText._textFirst.Length == 0)
                        {
                            __value = "null";
                        }
                        else
                        {
                            if (__getText.textBox.Multiline)
                            {
                                __getText._textFirst = __getText._textFirst.Replace("\r\n", "\n");
                            }
                            __getText._textFirst = __getText._textFirst.Replace("\'", "\'\'");
                            __value = __getText._textFirst;
                        }

                        __json.Add(__getText._name, __value);
                    }
                }
                else if (__getControl.GetType() == typeof(_myDateBox))
                {
                    _myDateBox __getText = (_myDateBox)__getControl;
                    if (__getText._isQuery)
                    {
                        __json.Add(__getText._name, __getText._textQuery());
                    }
                }
                else if (__getControl.GetType() == typeof(_myNumberBox))
                {
                    _myNumberBox __getText = (_myNumberBox)__getControl;
                    if (__getText._isQuery)
                    {
                        __json.Add(__getText._name, __getText._double);
                    }
                }
                else if (__getControl.GetType() == typeof(_myCheckBox))
                {
                    _myCheckBox __getData = (_myCheckBox)__getControl;
                    if (__getData._isQuery)
                    {
                        __json.Add(__getData._name, (__getData.Checked) ? "1" : "0");
                    }
                }
                else if (__getControl.GetType() == typeof(_myComboBox))
                {
                    _myComboBox __getData = (_myComboBox)__getControl;
                    if (__getData._isQuery)
                    {
                        __json.Add(__getData._name, __getData.SelectedIndex);
                    }
                }
                else if (__getControl.GetType() == typeof(_myGroupBox))
                {
                    _myGroupBox __getData = (_myGroupBox)__getControl;
                    if (__getData.__query == true)
                    {
                        object __getValue = null;
                        foreach (Control __getControlInGroupBox in __getData.Controls)
                        {
                            if (__getControlInGroupBox.GetType() == typeof(_myRadioButton))
                            {
                                _myRadioButton __getRadioButton = (_myRadioButton)__getControlInGroupBox;
                                if (__getRadioButton.Checked)
                                {
                                    __getValue = __getRadioButton.__value;
                                }
                            }
                        }
                        if (__getValue != null)
                        {
                            __json.Add(__getData.__name, __getValue.ToString());
                        }
                    }
                }
            }

            return __json;
        }

        public string _getJsonData()
        {
            StringBuilder __result = new StringBuilder();
            System.Json.JsonObject __json = this._getJson();
            __result.Append(__json.ToString());
            return __result.ToString();
        }

        private void _myScreen_Invalidated(object sender, InvalidateEventArgs e)
        {
            this._refresh();
        }

        private void _myScreen_SizeChanged(object sender, EventArgs e)
        {
            this._refresh();
        }

        /*void _myScreen_ControlAdded(object sender, ControlEventArgs e)
        {
            this._beforeChangeResource();
        }*/

        public void _reset()
        {
            this._maxColumn = 2;
            this._table_name = "";
            for (int __loop = 0; __loop < 10; __loop++)
            {
                this._maxLabelWidth[__loop] = 0;
            }
            this._getResource = true;
            this._isChangeResult = false;
            this._lastControl = null;
            this._lastTextBox = null;
            this.Controls.Clear();
        }

        public Boolean _isChange
        {
            get
            {
                this._saveLastControl();
                return this._isChangeResult;
            }
            set
            {
                //if (value == true) Debug.Print(Environment.StackTrace.ToString());
                this._isChangeResult = value;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;
            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                Keys __keyCode = (Keys)(int)keyData & Keys.KeyCode;
                if ((keyData & Keys.Control) == Keys.Control && (__keyCode == Keys.Home))
                {
                    this._focusFirst();
                }
                else
                    if ((keyData & Keys.Shift) == Keys.Shift && (__keyCode == Keys.Enter))
                {
                    SendKeys.Send("+{TAB}");
                    return true;
                }
                else
                        if (keyData == Keys.Enter)
                {
                    bool __tabKey = true;
                    if (this._checkKeyDownReturn != null)
                    {
                        __tabKey = this._checkKeyDownReturn(_lastControl, keyData);
                    }
                    if (__tabKey)
                    {
                        SendKeys.Send("{TAB}");
                    }
                    if (__tabKey == true)
                    {
                        return true;
                    }
                }
                else
                            if (keyData == Keys.Tab)
                {
                    /*if (_checkKeyDown != null)
                    {
                        Boolean __getCheck = _checkKeyDown(_lastControl, keyData);
                        if (__getCheck == false)
                        {
                            return true;
                        }
                    }*/
                    this._saveLastControl();
                    bool __tabKey = true;
                    if (_checkKeyDownReturn != null)
                    {
                        __tabKey = this._checkKeyDownReturn(_lastControl, keyData);
                    }
                    if (__tabKey == false)
                    {
                        return true;
                    }
                }
                else
                {
                    if (keyData == Keys.F12 && this._f12)
                    {
                        this._saveLastControl();
                        this._saveKeyDownWork(_lastControl);
                        return true;
                    }
                    else
                    {
                        if (keyData == Keys.Escape)
                        {
                            this._exitKeyDownWork(_lastControl);
                            return true;
                        }
                        else
                        {
                            if (_checkKeyDown != null)
                            {
                                if ((keyData & Keys.Control) == Keys.Control ||
                                        keyData == Keys.Home ||
                                        keyData == Keys.End ||
                                        keyData == Keys.Left ||
                                        keyData == Keys.Right ||
                                        keyData == Keys.Up ||
                                        keyData == Keys.Down ||
                                        keyData == Keys.PageDown ||
                                        keyData == Keys.PageUp ||
                                        keyData == Keys.Tab)
                                {
                                    return base.ProcessCmdKey(ref msg, keyData);
                                }
                                Boolean __getCheck = this._checkKeyDown(_lastControl, keyData);
                                if (__getCheck == false)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// บันทึกข้อมูลทั้งหมดก่อนที่จะทำการ Save
        /// </summary>
        public void _saveLastControl()
        {
            this._textBoxSave(_lastTextBox);
        }

        public void _refresh()
        {
            this.SizeChanged -= new EventHandler(_myScreen_SizeChanged);
            this.Invalidated -= new InvalidateEventHandler(_myScreen_Invalidated);
            bool __haveGroupBox = false;
            foreach (Control __getControl in this.Controls)
            {
                if (__getControl.GetType() == typeof(_myLabel))
                {
                    _myLabel __getLabel = (_myLabel)__getControl;
                    __getLabel.Width = this._maxLabelWidth[__getLabel._column];
                    __getLabel.Location = new Point(this._calcColumnPosition(__getLabel._column), this._calcRowPosition(__getLabel._row) - 2);
                }
                else if (__getControl.GetType() == typeof(_myTextBox))
                {
                    _myTextBox __getText = (_myTextBox)__getControl;
                    int __calcWidth = (this._calcColumnWidth() - (((__getText._displayLabel) ? this._maxLabelWidth[__getText._column] : 0))) + (this._calcColumnWidth() * (__getText._maxColumn - 1));
                    if (__getText.textBox.Multiline)
                    {
                        __getText.Width = __calcWidth;
                    }
                    else
                    {
                        int __calcWidth2 = 8 * __getText.MaxLength;
                        __getText.Width = (__calcWidth2 > __calcWidth || __getText._iconNumber == 1 || __getText._iconNumber == 4) ? __calcWidth : __calcWidth2;//(this._calcColumnWidth() - (((getText._displayLabel) ? this._maxLabelWidth[getText._column] : 0) + (_margin * 2))) + (this._calcColumnWidth() * (getText._maxColumn - 1));
                    }
                    __getText.Location = new Point(this._calcColumnPosition(__getText._column) + (((__getText._displayLabel) ? this._maxLabelWidth[__getText._column] : 0)), this._calcRowPosition(__getText._row));
                    if (__getControl.Enabled == false)
                    {
                        __getText.textBox.BackColor = Color.MintCream;
                    }
                    else
                    {
                        __getText.textBox.BackColor = (__getText._emtry) ? __getText._defaultBackGround : Color.OldLace;
                    }
                }
                else if (__getControl.GetType() == typeof(_myNumberBox))
                {
                    _myNumberBox __getNumberBox = (_myNumberBox)__getControl;
                    int __calcWidth = (this._calcColumnWidth() - (((__getNumberBox._displayLabel) ? this._maxLabelWidth[__getNumberBox._column] : 0))) + (this._calcColumnWidth() * (__getNumberBox._maxColumn - 1));
                    __getNumberBox.Width = (__calcWidth > 250) ? 250 : __calcWidth;
                    __getNumberBox.Location = new Point(this._calcColumnPosition(__getNumberBox._column) + (((__getNumberBox._displayLabel) ? this._maxLabelWidth[__getNumberBox._column] : 0)), _calcRowPosition(__getNumberBox._row));
                    if (__getControl.Enabled == false)
                    {
                        __getNumberBox.textBox.BackColor = Color.MintCream;
                    }
                    else
                    {
                        __getNumberBox.textBox.BackColor = (__getNumberBox._emtry) ? __getNumberBox._defaultBackGround : Color.OldLace;
                    }
                }
                else if (__getControl.GetType() == typeof(_myDateBox))
                {
                    _myDateBox __getDateBox = (_myDateBox)__getControl;
                    int __calcWidth = (this._calcColumnWidth() - (((__getDateBox._displayLabel) ? this._maxLabelWidth[__getDateBox._column] : 0))) + (this._calcColumnWidth() * (__getDateBox._maxColumn - 1));
                    __getDateBox.Width = (__calcWidth > 250) ? 250 : __calcWidth;
                    __getDateBox.Location = new Point(this._calcColumnPosition(__getDateBox._column) + (((__getDateBox._displayLabel) ? this._maxLabelWidth[__getDateBox._column] : 0)), _calcRowPosition(__getDateBox._row));
                    if (__getControl.Enabled == false)
                    {
                        __getDateBox.textBox.BackColor = Color.MintCream;
                    }
                    else
                    {
                        __getDateBox.textBox.BackColor = (__getDateBox._emtry) ? __getDateBox._defaultBackGround : Color.OldLace;
                    }
                }
                else
 if (__getControl.GetType() == typeof(_myComboBox))
                {
                    _myComboBox __getComboBox = (_myComboBox)__getControl;
                    int __calcWidth = (this._calcColumnWidth() - (this._maxLabelWidth[__getComboBox._column])) + (this._calcColumnWidth() * (__getComboBox._maxColumn - 1));
                    __getComboBox.Width = __calcWidth;// this._calcColumnWidth() - (_maxLabelWidth[__getComboBox._column]);
                    __getComboBox.Location = new Point(this._calcColumnPosition(__getComboBox._column) + (_maxLabelWidth[__getComboBox._column]), _calcRowPosition(__getComboBox._row));
                }
                else if (__getControl.GetType() == typeof(_myCheckBox))
                {
                    _myCheckBox __getCheckBox = (_myCheckBox)__getControl;
                    __getCheckBox.Width = this._calcColumnWidth() - this._maxLabelWidth[__getCheckBox._column];
                    __getCheckBox.Location = new Point(this._calcColumnPosition(__getCheckBox._column) + (_maxLabelWidth[__getCheckBox._column]), _calcRowPosition(__getCheckBox._row) + 4);
                }
                else if (__getControl.GetType() == typeof(_myGroupBox))
                {
                    _myGroupBox __getGroupBox = (_myGroupBox)__getControl;
                    __getGroupBox.Width = (this._calcColumnWidth() - (_maxLabelWidth[__getGroupBox.__column])) + (this._calcColumnWidth() * (__getGroupBox.__maxColumnPanel - 1));
                    __getGroupBox.Height = _calcRowPosition(__getGroupBox.__rowCount) + 20;
                    __getGroupBox.Location = new Point(this._calcColumnPosition(__getGroupBox.__column) + +(_maxLabelWidth[__getGroupBox.__column]), _calcRowPosition(__getGroupBox.__row));
                    __haveGroupBox = true;
                    foreach (Control __getControlInGroupBox in __getGroupBox.Controls)
                    {
                        if (__getControlInGroupBox.GetType() == typeof(_myRadioButton))
                        {
                            _myRadioButton __getRadioButton = (_myRadioButton)__getControlInGroupBox;
                            int __calcWidth = (__getGroupBox.Width - 20) / __getGroupBox.__maxColumn;
                            __getRadioButton.Width = __calcWidth;
                            __getRadioButton.Location = new Point(10 + (__getRadioButton.__column * __calcWidth), 15 + _calcRowPosition(__getRadioButton.__row));
                        }
                    }
                }
                else if (__getControl.GetType() == typeof(_myButton))
                {
                    _myButton __getButton = (_myButton)__getControl;
                    __getButton.Width = (this._calcColumnWidth() - this._maxLabelWidth[__getButton._column]) + (this._calcColumnWidth() * (__getButton._maxColumn - 1));
                    __getButton.Location = new Point(this._calcColumnPosition(__getButton._column) + (_maxLabelWidth[__getButton._column]), _calcRowPosition(__getButton._row) + 2);
                }
                else if (__getControl.GetType() == typeof(_myImage))
                {
                    _myImage __getImage = (_myImage)__getControl;
                    __getImage.Width = (this._calcColumnWidth() - this._maxLabelWidth[__getImage._column]) + (this._calcColumnWidth() * (__getImage._columnCount - 1));
                    __getImage.Location = new Point(this._calcColumnPosition(__getImage._column) + (_maxLabelWidth[__getImage._column]), _calcRowPosition(__getImage._row) + 2);
                }
            }
            // ส่ง Group Box ไปอยู่ด้านหลัง (ต้องทำหลังจากปรับความกว้าง ไม่เช่นนั้นจะเรียงลำดับผิด และเพี้ยน)
            if (__haveGroupBox)
            {
                foreach (Control __getControl in this.Controls)
                {
                    if (__getControl.GetType() == typeof(_myGroupBox))
                    {
                        __getControl.SendToBack();
                    }
                }
            }
            this.SizeChanged += new EventHandler(_myScreen_SizeChanged);
            this.Invalidated += new InvalidateEventHandler(_myScreen_Invalidated);
            if (_afterRefresh != null)
            {
                _afterRefresh(this);
            }
        }

        public void _clear()
        {
            // Debug.Print(Environment.StackTrace.ToString());
            DataSet __recent = null;
            if (this._recentXmlFileName.Length > 0)
            {
                try
                {
                    string __xPathName = Path.GetTempPath();
                    string __xFileName = Path.GetTempPath() + "\\" + this._recentXmlFileName + ".xml";
                    if (File.Exists(__xFileName))
                    {
                        StreamReader __sr = File.OpenText(__xFileName);
                        __recent = MyLib._myGlobal._convertStringToDataSet(__sr.ReadToEnd());
                    }
                }
                catch
                {
                }
            }
            foreach (Control __getControl in this.Controls)
            {
                if (__getControl.GetType() == typeof(_myTextBox))
                {
                    string __value = "";
                    _myTextBox __getText = (_myTextBox)__getControl;
                    if (__recent != null && __recent.Tables.Count > 0)
                    {
                        DataRow[] __select = __recent.Tables[0].Select("name=\'" + __getText._name + "\'");
                        if (__select.Length > 0)
                        {
                            __value = __select[0]["value"].ToString();
                        }
                    }
                    __getText.textBox.Text =
                    __getText._textFirst =
                    __getText._textSecond =
                    __getText._textLast = __value;
                }
                else if (__getControl.GetType() == typeof(_myDateBox))
                {
                    _myDateBox __getText = (_myDateBox)__getControl;
                    __getText.textBox.Text =
                    __getText._textFirst =
                    __getText._textSecond =
                    __getText._textLast = "";
                    __getText._dateTime = new DateTime(1000, 1, 1);
                    __getText._dateTimeOld = __getText._dateTime;
                }
                else if (__getControl.GetType() == typeof(_myNumberBox))
                {
                    _myNumberBox __getText = (_myNumberBox)__getControl;
                    __getText.textBox.Text =
                    __getText._textFirst =
                    __getText._textSecond =
                    __getText._textLast = "";
                    __getText._double = 0;
                    __getText.__oldResult = 0;
                }
                else if (__getControl.GetType() == typeof(_myCheckBox))
                {
                    _myCheckBox __getCheckBox = (_myCheckBox)__getControl;
                    __getCheckBox.Checked = __getCheckBox._defaultValue;
                }
                else if (__getControl.GetType() == typeof(_myComboBox))
                {
                    _myComboBox __getComboBox = (_myComboBox)__getControl;

                    int __value = -1;
                    if (__recent != null && __recent.Tables.Count > 0)
                    {
                        DataRow[] __select = __recent.Tables[0].Select("name=\'" + __getComboBox._name + "\'");
                        if (__select.Length > 0)
                        {
                            __value = (int)MyLib._myGlobal._decimalPhase(__select[0]["value"].ToString());
                        }
                    }

                    if (__value != -1)
                        __getComboBox.SelectedIndex = __value;

                    if (__value == -1 && __getComboBox.Items.Count > 0)
                        __getComboBox.SelectedIndex = 0;

                }
                else if (__getControl.GetType() == typeof(_myImage))
                {
                    _myImage __getImage = (_myImage)__getControl;
                    __getImage._pictureBox.Image = null;
                }
            } // foreach
            if (this._afterClear != null)
            {
                this._afterClear(this);
            }
            this._isChange = false;
        }

        public bool _loadData(DataTable getTable)
        {
            try
            {
                this._isChange = false;
                if (getTable.Rows.Count > 0)
                {
                    DataRow __getRow = getTable.Rows[0];
                    for (int __column = 0; __column < getTable.Columns.Count; __column++)
                    {
                        string __fieldName = getTable.Columns[__column].ColumnName;
                        string __dataColumn = __getRow.ItemArray[__column].ToString();
                        foreach (Control __getControl in this.Controls)
                        {
                            if (__getControl.GetType() == typeof(_myTextBox))
                            {
                                _myTextBox __getText = (_myTextBox)__getControl;
                                if ((__getText._isQuery || __getText._isGetData) && __getText._name.ToLower().CompareTo(__fieldName.ToLower()) == 0) // toe เพิ่ม check is get data สำหรับ isquery = false
                                {
                                    if (__getText.textBox.Multiline)
                                    {
                                        __dataColumn = __dataColumn.Replace("\n", "\r\n");
                                    }
                                    __getText.textBox.Text = __getText._textFirst = __getText._textLast = __dataColumn;
                                    __getText._textSecond = "";
                                    break;
                                }
                            }
                            else if (__getControl.GetType() == typeof(_myDateBox))
                            {
                                _myDateBox __getText = (_myDateBox)__getControl;
                                if (__getText._isQuery && __getText._name.ToLower().CompareTo(__fieldName.ToLower()) == 0)
                                {
                                    try
                                    {
                                        if (__dataColumn.Length > 0)
                                        {
                                            IFormatProvider __cultureEng = new CultureInfo("en-US");
                                            DateTime __getDate = Convert.ToDateTime(__dataColumn, __cultureEng);
                                            IFormatProvider __cultureThai = MyLib._myGlobal._cultureInfo();
                                            __getText.textBox.Text = __getText._textFirst = __getText._textLast = __getDate.ToString("d/M/yyyy", __cultureThai);
                                            __getText._textSecond = "";
                                            __getText._checkDate(true, true);
                                        }
                                        else
                                        {
                                            __getText.textBox.Text = __getText._textFirst = __getText._textLast = "";
                                            __getText._textSecond = "";
                                        }
                                    }
                                    catch
                                    {
                                        // Debugger.Break();
                                        __getText.textBox.Text = __getText._textFirst = __getText._textLast = "";
                                        __getText._textSecond = "";
                                    }
                                    __getText._refresh();
                                    __getText._dateTimeOld = __getText._dateTime;
                                    break;
                                }
                            }
                            else if (__getControl.GetType() == typeof(_myNumberBox))
                            {
                                _myNumberBox __getText = (_myNumberBox)__getControl;
                                if (__getText._isQuery && __getText._name.ToLower().CompareTo(__fieldName.ToLower()) == 0)
                                {
                                    __getText.textBox.Text = __getText._textFirst = __getText._textLast = __dataColumn;
                                    __getText._checkNumber();
                                    __getText._refresh();
                                    __getText.__oldResult = (__getText.textBox.Text.Length == 0) ? 0 : Convert.ToDecimal(__getText.textBox.Text);
                                    break;
                                }
                            }
                            else if (__getControl.GetType() == typeof(_myCheckBox))
                            {
                                _myCheckBox __getData = (_myCheckBox)__getControl;
                                if (__getData._name.ToLower().CompareTo(__fieldName.ToLower()) == 0)
                                {
                                    try
                                    {
                                        __getData.Checked = (__dataColumn.CompareTo("1") == 0) ? true : false;
                                    }
                                    catch
                                    {
                                        // Debugger.Break();
                                        __getData.Checked = false;
                                    }
                                    break;
                                }
                            }
                            else if (__getControl.GetType() == typeof(_myComboBox))
                            {
                                _myComboBox __getData = (_myComboBox)__getControl;
                                if (__getData._name.ToLower().CompareTo(__fieldName.ToLower()) == 0)
                                {
                                    int __selectIndex = 0;
                                    try
                                    {
                                        __selectIndex = (__dataColumn.Length == 0) ? 0 : Convert.ToInt32(__dataColumn);
                                    }
                                    catch
                                    {
                                    }
                                    __getData._oldValue = __selectIndex;
                                    // เอาไว้หลังกัน event ทำงานก่อน _oldValue
                                    __getData.SelectedIndex = __selectIndex;
                                    break;
                                }
                            }
                            else if (__getControl.GetType() == typeof(_myGroupBox))
                            {
                                _myGroupBox __getData = (_myGroupBox)__getControl;
                                if (__getData.__name.ToLower().CompareTo(__fieldName.ToLower()) == 0)
                                {
                                    if (__getData.__query == true)
                                    {
                                        foreach (Control __getControlInGroupBox in __getData.Controls)
                                        {
                                            if (__getControlInGroupBox.GetType() == typeof(_myRadioButton))
                                            {
                                                _myRadioButton __getRadioButton = (_myRadioButton)__getControlInGroupBox;
                                                int __getInt = (__dataColumn.Length == 0) ? 0 : Convert.ToInt32(__dataColumn);
                                                if (__getInt == (int)__getRadioButton.__value)
                                                {
                                                    __getRadioButton.Checked = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else if (__getControl.GetType() == typeof(_myImage))
                            {
                                try
                                {
                                    _myImage __getImage = (_myImage)__getControl;
                                    if (__getImage._fieldName.ToLower().CompareTo(__fieldName.ToLower()) == 0)
                                    {
                                        if (__getImage._isQuery || __getImage._isGetData)
                                        {
                                            __getImage._base64ToImage(__dataColumn);
                                        }
                                    }
                                }
                                catch
                                {

                                }
                            }
                        } // foreach
                    } // for
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show("Load fail. : " + __ex.Message.ToString());
            }
            return (true);
        }

        /// <summary>
        /// สร้าง Log
        /// </summary>
        /// <returns></returns>
        public String _logCreate(string rootName)
        {
            StringBuilder __result = new StringBuilder(MyLib._myGlobal._xmlHeader);
            __result.Append("<" + rootName + ">");
            foreach (Control __getControl in this.Controls)
            {
                if (__getControl.GetType() == typeof(_myTextBox))
                {
                    _myTextBox __getData = (_myTextBox)__getControl;
                    __result.Append("<d t=1 f=" + __getData._name + ">" + MyLib._myGlobal._convertStrToQuery(__getData.textBox.Text) + "</d>");
                }
                else if (__getControl.GetType() == typeof(_myDateBox))
                {
                    _myDateBox __getData = (_myDateBox)__getControl;
                    __result.Append("<d t=2 f=" + __getData._name + ">" + __getData.textBox.Text + "</d>");
                }
                else if (__getControl.GetType() == typeof(_myNumberBox))
                {
                    _myNumberBox __getData = (_myNumberBox)__getControl;
                    __result.Append("<d t=3 f=" + __getData._name + ">" + __getData.textBox.Text + "</d>");
                }
                else if (__getControl.GetType() == typeof(_myCheckBox))
                {
                    _myCheckBox __getData = (_myCheckBox)__getControl;
                    __result.Append("<d t=4 f=" + __getData._name + ">" + __getData.Checked.ToString() + "</d>");
                }
                else if (__getControl.GetType() == typeof(_myComboBox))
                {
                    _myComboBox __getData = (_myComboBox)__getControl;
                    __result.Append("<d t=5 f=" + __getData._name + ">" + __getData.SelectedIndex.ToString() + "</d>");
                }
                else if (__getControl.GetType() == typeof(_myGroupBox))
                {
                    _myGroupBox __getData = (_myGroupBox)__getControl;
                    object __getValue = null;
                    foreach (Control __getControlInGroupBox in __getData.Controls)
                    {
                        if (__getControlInGroupBox.GetType() == typeof(_myRadioButton))
                        {
                            _myRadioButton __getRadioButton = (_myRadioButton)__getControlInGroupBox;
                            if (__getRadioButton.Checked)
                            {
                                __getValue = __getRadioButton.__value;
                            }
                        }
                    }
                    if (__getValue != null)
                    {
                        __result.Append("<d t=6 f=" + __getData.__name + ">" + __getValue.ToString() + "</d>");
                    }
                }
            }
            __result.Append("</" + rootName + ">");
            return __result.ToString();
        }

        /// <summary>
        /// สร้าง query สำหรับ Update โดยแก้ไขเฉพาะที่มีค่า
        /// </summary>
        /// <returns></returns>
        public string _createQueryForUpdateValueOnly()
        {
            this._saveLastControl();

            StringBuilder __resultUpdate = new StringBuilder();
            int __count = 0;

            foreach (Control __getControl in this.Controls)
            {
                if (__getControl.GetType() == typeof(_myTextBox))
                {
                    _myTextBox __getText = (_myTextBox)__getControl;
                    if (__getText._isQuery)
                    {
                        if (__getText._textFirst.Length != 0)
                        {
                            if (__count != 0)
                            {
                                __resultUpdate.Append(",");
                            }
                            __count++;
                            if (__getText.textBox.Multiline)
                            {
                                __getText._textFirst = __getText._textFirst.Replace("\r\n", "\n");
                            }
                            __getText._textFirst = __getText._textFirst.Replace("\'", "\'\'");
                            __resultUpdate.Append(string.Concat(__getText._name, "=\'", __getText._textFirst, "\'"));
                        }
                    }
                }
                else if (__getControl.GetType() == typeof(_myDateBox))
                {
                    _myDateBox __getText = (_myDateBox)__getControl;
                    if (__getText._isQuery)
                    {
                        DateTime __getDateTemp = __getText._dateTime;
                        string __getDate = __getText._textQuery();
                        if (__getDate.Length > 0 && __getDate != "null")
                        {
                            if (__count != 0)
                            {
                                __resultUpdate.Append(",");
                            }
                            __count++;
                            __resultUpdate.Append(string.Concat(__getText._name, "=", __getDate));
                        }
                    }
                }
                else if (__getControl.GetType() == typeof(_myNumberBox))
                {
                    _myNumberBox __getText = (_myNumberBox)__getControl;
                    if (__getText._isQuery)
                    {
                        if (__getText.textBox.Text.Length > 0)
                        {
                            if (__count != 0)
                            {
                                __resultUpdate.Append(",");
                            }
                            __count++;
                            __resultUpdate.Append(string.Concat(__getText._name, "=", __getText._double));
                        }
                    }
                }
                else if (__getControl.GetType() == typeof(_myCheckBox))
                {
                    _myCheckBox __getData = (_myCheckBox)__getControl;
                    if (__getData._isQuery)
                    {
                        if (__count != 0)
                        {
                            __resultUpdate.Append(",");
                        }
                        __count++;
                        __resultUpdate.Append(string.Concat(__getData._name, "=", ((__getData.Checked) ? "1" : "0")));
                    }
                }
                else if (__getControl.GetType() == typeof(_myComboBox))
                {
                    _myComboBox __getData = (_myComboBox)__getControl;
                    if (__count != 0)
                    {
                        __resultUpdate.Append(",");
                    }
                    __count++;
                    __resultUpdate.Append(string.Concat(__getData._name, "=", __getData.SelectedIndex));

                }
                else if (__getControl.GetType() == typeof(_myGroupBox))
                {
                    _myGroupBox __getData = (_myGroupBox)__getControl;
                    if (__getData.__query == true)
                    {
                        object __getValue = null;
                        foreach (Control __getControlInGroupBox in __getData.Controls)
                        {
                            if (__getControlInGroupBox.GetType() == typeof(_myRadioButton))
                            {
                                _myRadioButton __getRadioButton = (_myRadioButton)__getControlInGroupBox;
                                if (__getRadioButton.Checked)
                                {
                                    __getValue = __getRadioButton.__value;
                                }
                            }
                        }
                        if (__getValue != null)
                        {
                            if (__count != 0)
                            {
                                __resultUpdate.Append(",");
                            }
                            __count++;
                            __resultUpdate.Append(string.Concat(__getData.__name, "=", __getValue.ToString()));
                        }
                    }
                }
            }

            return __resultUpdate.ToString();
        }

        /// <summary>
        /// Query สำหรับ Insert และ Update
        /// </summary>
        /// <returns></returns>
        public ArrayList _createQueryForDatabase()
        {
            this._saveLastControl();
            ArrayList __result = new ArrayList();
            StringBuilder __resultField = new StringBuilder();
            StringBuilder __resultData = new StringBuilder();
            StringBuilder __resultUpdate = new StringBuilder();
            int __count = 0;
            StringBuilder __xmlRecent = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            Boolean __haveRecent = false;
            foreach (Control __getControl in this.Controls)
            {
                if (__getControl.GetType() == typeof(_myTextBox))
                {
                    _myTextBox __getText = (_myTextBox)__getControl;
                    if (__getText._isQuery)
                    {
                        if (__count != 0)
                        {
                            __resultField.Append(",");
                            __resultData.Append(",");
                            __resultUpdate.Append(",");
                        }
                        __count++;
                        __resultField.Append(__getText._name);
                        if (__getText._textFirst.Length == 0)
                        {
                            __resultData.Append("null");
                            __resultUpdate.Append(string.Concat(__getText._name, "=null"));
                        }
                        else
                        {
                            if (__getText.textBox.Multiline)
                            {
                                __getText._textFirst = __getText._textFirst.Replace("\r\n", "\n");
                            }
                            __getText._textFirst = __getText._textFirst.Replace("\'", "\'\'");
                            __resultData.Append(string.Concat("\'", __getText._textFirst, "\'"));
                            __resultUpdate.Append(string.Concat(__getText._name, "=\'", __getText._textFirst, "\'"));
                        }
                    }
                    if (__getText._useRecentValue)
                    {
                        __xmlRecent.Append("<row>");
                        __xmlRecent.Append("<name>" + __getText._name + "</name>");
                        __xmlRecent.Append("<value>" + __getText._textFirst + "</value>");
                        __xmlRecent.Append("</row>");
                        __haveRecent = true;
                    }
                }
                else if (__getControl.GetType() == typeof(_myDateBox))
                {
                    _myDateBox __getText = (_myDateBox)__getControl;
                    if (__getText._isQuery)
                    {
                        if (__count != 0)
                        {
                            __resultField.Append(",");
                            __resultData.Append(",");
                            __resultUpdate.Append(",");
                        }
                        __count++;
                        __resultField.Append(__getText._name);
                        DateTime __getDateTemp = __getText._dateTime;
                        string __getDate = __getText._textQuery();
                        __resultData.Append(__getDate);
                        __resultUpdate.Append(string.Concat(__getText._name, "=", __getDate));
                    }
                }
                else if (__getControl.GetType() == typeof(_myNumberBox))
                {
                    _myNumberBox __getText = (_myNumberBox)__getControl;
                    if (__getText._isQuery)
                    {
                        if (__count != 0)
                        {
                            __resultField.Append(",");
                            __resultData.Append(",");
                            __resultUpdate.Append(",");
                        }
                        __count++;
                        __resultField.Append(__getText._name);
                        __resultData.Append(__getText._double);
                        __resultUpdate.Append(string.Concat(__getText._name, "=", __getText._double));
                    }
                }
                else if (__getControl.GetType() == typeof(_myCheckBox))
                {
                    _myCheckBox __getData = (_myCheckBox)__getControl;
                    if (__getData._isQuery)
                    {
                        if (__count != 0)
                        {
                            __resultField.Append(",");
                            __resultData.Append(",");
                            __resultUpdate.Append(",");
                        }
                        __count++;
                        __resultField.Append(__getData._name);
                        __resultData.Append((__getData.Checked) ? "1" : "0");
                        __resultUpdate.Append(string.Concat(__getData._name, "=", ((__getData.Checked) ? "1" : "0")));
                    }
                }
                else if (__getControl.GetType() == typeof(_myComboBox))
                {
                    _myComboBox __getData = (_myComboBox)__getControl;
                    if (__getData._isQuery)
                    {

                        if (__count != 0)
                        {
                            __resultField.Append(",");
                            __resultData.Append(",");
                            __resultUpdate.Append(",");
                        }
                        __count++;
                        __resultField.Append(__getData._name);
                        __resultData.Append(__getData.SelectedIndex);
                        __resultUpdate.Append(string.Concat(__getData._name, "=", __getData.SelectedIndex));

                        if (__getData._useRecentValue)
                        {
                            __xmlRecent.Append("<row>");
                            __xmlRecent.Append("<name>" + __getData._name + "</name>");
                            __xmlRecent.Append("<value>" + __getData.SelectedIndex + "</value>");
                            __xmlRecent.Append("</row>");
                            __haveRecent = true;
                        }
                    }
                }
                else if (__getControl.GetType() == typeof(_myGroupBox))
                {
                    _myGroupBox __getData = (_myGroupBox)__getControl;
                    if (__getData.__query == true)
                    {
                        object __getValue = null;
                        foreach (Control __getControlInGroupBox in __getData.Controls)
                        {
                            if (__getControlInGroupBox.GetType() == typeof(_myRadioButton))
                            {
                                _myRadioButton __getRadioButton = (_myRadioButton)__getControlInGroupBox;
                                if (__getRadioButton.Checked)
                                {
                                    __getValue = __getRadioButton.__value;
                                }
                            }
                        }
                        if (__getValue != null)
                        {
                            if (__count != 0)
                            {
                                __resultField.Append(",");
                                __resultData.Append(",");
                                __resultUpdate.Append(",");
                            }
                            __count++;
                            __resultField.Append(__getData.__name);
                            __resultData.Append(__getValue.ToString());
                            __resultUpdate.Append(string.Concat(__getData.__name, "=", __getValue.ToString()));
                        }
                    }
                }
                else if (__getControl.GetType() == typeof(_myImage))
                {
                    _myImage __getImage = (_myImage)__getControl;
                    if (__getImage._isQuery)
                    {
                        if (__count != 0)
                        {
                            __resultField.Append(",");
                            __resultData.Append(",");
                            __resultUpdate.Append(",");
                        }
                        __count++;
                        __resultField.Append(__getImage._fieldName);
                        __resultData.Append(string.Concat("\'", __getImage._imageToBase64(), "\'"));
                        __resultUpdate.Append(string.Concat(__getImage._fieldName, "=\'", __getImage._imageToBase64(), "\'"));
                    }
                }
            }
            __result.Add(__resultField);
            __result.Add(__resultData);
            __result.Add(__resultUpdate);
            __xmlRecent.Append("</node>");
            if (this._recentXmlFileName.Length > 0 && __haveRecent)
            {
                try
                {
                    string __xPathName = Path.GetTempPath();
                    string __xFileName = Path.GetTempPath() + "\\" + this._recentXmlFileName + ".xml";
                    StreamWriter __sr = File.CreateText(__xFileName);
                    __sr.WriteLine(__xmlRecent.ToString());
                    __sr.Close();
                }
                catch
                {
                }
            }
            return (__result);
        }

        public void _focusFirst()
        {
            foreach (Control __getControl in this.Controls)
            {
                if (__getControl.Enabled)
                {
                    if (__getControl.GetType() == typeof(_myTextBox))
                    {
                        _myTextBox __getText = (_myTextBox)__getControl;
                        __getText.textBox.Focus();
                        __getText.textBox.SelectAll();
                        break;
                    }
                    else if (__getControl.GetType() == typeof(_myDateBox))
                    {
                        _myDateBox __getText = (_myDateBox)__getControl;
                        __getText.textBox.Focus();
                        __getText.textBox.SelectAll();
                        break;
                    }
                    else if (__getControl.GetType() == typeof(_myNumberBox))
                    {
                        _myNumberBox __getText = (_myNumberBox)__getControl;
                        __getText.textBox.Focus();
                        __getText.textBox.SelectAll();
                        break;
                    }
                }
            }
        }

        public string _getDataStrQuery(string name)
        {
            return (_getDataStr(name, true));
        }

        public string _getDataStrQuery(string name, string extraWord)
        {
            return (_getDataStr(name, true, extraWord));
        }

        public string _getDataStr(string name)
        {
            return (_getDataStr(name, false));
        }

        public void _setUpper(string name)
        {
            try
            {
                Control __getControl = (Control)_getControl(name);
                if (__getControl.GetType() == typeof(_myTextBox))
                {
                    MyLib._myTextBox __getControl2 = (MyLib._myTextBox)_getControl(name);
                    __getControl2._upperCase = true;
                }
            }
            catch
            {
            }
        }

        public DateTime _getDataDate(string name)
        {
            DateTime __result = new DateTime();
            Control __getControl = (Control)_getControl(name);
            if (__getControl.GetType() == typeof(_myDateBox))
            {
                __result = ((MyLib._myDateBox)__getControl)._dateTime;
            }
            return __result;
        }

        public decimal _getDataNumber(string name)
        {
            // MOO
            // ใส่ try เกิด Err บ่อยครั้ง กรณีที่ ไม่ทมีการคีย์ลงในช่องฟิวล์ แล้วดึงค่าในชองฟิวล
            decimal __result = 0;
            try
            {
                Control __getControl = (Control)_getControl(name);
                if (__getControl != null && __getControl.GetType() == typeof(_myNumberBox))
                {
                    __result = ((_myNumberBox)__getControl)._double;
                }
                else if (__getControl != null && __getControl.GetType() == typeof(_myComboBox))
                {
                    __result = ((_myComboBox)__getControl).SelectedIndex;
                }
            }
            catch
            {
            }
            return __result;
        }

        /// <summary>
        /// ค้นหา Control โดยใช้ชื่อ Field
        /// </summary>
        /// <param name="controlName">ชื่อ Field</param>
        /// <returns></returns>
        public Control _getControl(string controlName)
        {
            Control __result = null;
            foreach (Control __getControl in this.Controls)
            {
                if (__getControl.GetType() == typeof(_myTextBox))
                {
                    _myTextBox __getText = (_myTextBox)__getControl;
                    if (__getText._name.CompareTo(controlName) == 0)
                    {
                        return (__getControl);
                    }
                }
                else if (__getControl.GetType() == typeof(_myDateBox))
                {
                    _myDateBox __getText = (_myDateBox)__getControl;
                    if (__getText._name.CompareTo(controlName) == 0)
                    {
                        return (__getControl);
                    }
                }
                else if (__getControl.GetType() == typeof(_myNumberBox))
                {
                    _myNumberBox __getText = (_myNumberBox)__getControl;
                    if (__getText._name.CompareTo(controlName) == 0)
                    {
                        return (__getControl);
                    }
                }
                else if (__getControl.GetType() == typeof(_myCheckBox))
                {
                    _myCheckBox __getData = (_myCheckBox)__getControl;
                    if (__getData._name.CompareTo(controlName) == 0)
                    {
                        return (__getControl);
                    }
                }
                else if (__getControl.GetType() == typeof(_myComboBox))
                {
                    _myComboBox __getData = (_myComboBox)__getControl;
                    if (__getData._name.CompareTo(controlName) == 0)
                    {
                        return (__getControl);
                    }
                }
                else if (__getControl.GetType() == typeof(_myGroupBox))
                {
                    _myGroupBox __getData = (_myGroupBox)__getControl;
                    if (__getData.__name.CompareTo(controlName) == 0)
                    {
                        return (__getControl);
                    }
                }
                else if (__getControl.GetType() == typeof(_myImage))
                {
                    _myImage __getData = (_myImage)__getControl;
                    if (__getData._fieldName.CompareTo(controlName) == 0)
                    {
                        return (__getControl);
                    }
                }
            }
            return (__result);
        }

        public string _getDataStr(string name, Boolean query)
        {
            return _getDataStr(name, query, "");
        }

        public string _getDataStr(string name, Boolean query, string extraWord)
        {
            foreach (Control __getControl in this.Controls)
            {
                if (__getControl.GetType() == typeof(_myTextBox))
                {
                    _myTextBox __getText = (_myTextBox)__getControl;
                    if (__getText._name.CompareTo(name) == 0)
                    {
                        if (query)
                        {
                            string result = (__getText._textFirst.Length == 0) ? "null" : string.Concat("\'", __getText._textFirst.Replace("\'", "\'\'"), "\'");
                            return (MyLib._myUtil._convertTextToXml(result));
                        }
                        else
                        {
                            return (__getText._textFirst);
                        }
                    }
                }
                else if (__getControl.GetType() == typeof(_myDateBox))
                {
                    _myDateBox __getText = (_myDateBox)__getControl;
                    if (__getText._name.CompareTo(name) == 0)
                    {
                        if (query)
                        {
                            return (__getText._textQuery(extraWord));
                        }
                        else
                        {
                            if (__getText._dateTime.Year < 1900)
                            {
                                return "";
                            }
                            IFormatProvider __culture = MyLib._myGlobal._cultureInfo();
                            return (__getText._dateTime.ToString("d/MM/yyyy", __culture));
                        }
                    }
                }
                else if (__getControl.GetType() == typeof(_myNumberBox))
                {
                    _myNumberBox __getText = (_myNumberBox)__getControl;
                    if (__getText._name.CompareTo(name) == 0)
                    {
                        if (query)
                        {
                            return ((__getText._double == 0) ? "0" : __getText._textFirst);
                        }
                        else
                        {
                            return (__getText._textFirst);
                        }
                    }
                }
                else if (__getControl.GetType() == typeof(_myCheckBox))
                {
                    _myCheckBox __getData = (_myCheckBox)__getControl;
                    if (__getData._name.CompareTo(name) == 0)
                    {
                        if (query)
                        {
                            return ((__getData.Checked) ? "1" : "0");
                        }
                        else
                        {
                            return ((__getData.Checked) ? "1" : "");
                        }
                    }
                }
                else if (__getControl.GetType() == typeof(_myGroupBox))
                {
                    _myGroupBox __getGroupBox = (_myGroupBox)__getControl;
                    if (__getGroupBox.__name.CompareTo(name) == 0)
                    {
                        foreach (Control __getControlInGroupBox in __getGroupBox.Controls)
                        {
                            if (__getControlInGroupBox.GetType() == typeof(_myRadioButton))
                            {
                                _myRadioButton __getRadioButton = (_myRadioButton)__getControlInGroupBox;
                                if (__getRadioButton.Checked)
                                {
                                    return __getRadioButton.__value.ToString();
                                }
                            }
                        }
                    }
                }
                else if (__getControl.GetType() == typeof(_myComboBox))
                {
                    _myComboBox __getData = (_myComboBox)__getControl;
                    if (__getData._name.CompareTo(name) == 0)
                    {
                        if (__getData.InvokeRequired == true)
                        {
                            string __getSelectedIndex = "";
                            __getData.Invoke(new MethodInvoker(delegate () { __getSelectedIndex = __getData.SelectedIndex.ToString(); }));
                            return __getSelectedIndex;
                        }
                        else
                            return __getData.SelectedIndex.ToString();
                    }
                }
            }

            //if (query)
            //    return ("''");
            return ("");
        }

        public string _getDataComboStr(string name, Boolean query)
        {
            foreach (Control __getControl in this.Controls)
            {
                if (__getControl.GetType() == typeof(_myComboBox))
                {
                    _myComboBox __getData = (_myComboBox)__getControl;
                    if (__getData._name.CompareTo(name) == 0)
                    {
                        if (__getData.SelectedItem == null) return "";
                        return __getData.SelectedItem.ToString();
                    }
                }
            }

            return ("");
        }

        /// <summary>
        /// SetDataStr
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        /// <param name="value2"></param>
        /// <param name="updateOldText">ถ้าเป็น True Event จะไม่ทำงาน</param>
        public void _setDataStr(string fieldName, string value, string value2, Boolean updateOldText)
        {
            //Debug.Print(String.Format("_setDataStr(string fieldName={0}, string value={1}, string value2={2}, Boolean updateOldText={3})",fieldName.ToString(),value.ToString(),value2.ToString(),updateOldText.ToString()));
            if (value == null) value = "";
            if (value2 == null) value2 = "";
            foreach (Control __getControl in this.Controls)
            {
                if (__getControl.GetType() == typeof(_myTextBox))
                {
                    _myTextBox __getText = (_myTextBox)__getControl;
                    string __fieldNameCompare = (__getText._label == null || __getText._label._field_name.Length == 0) ? __getText._name : __getText._label._field_name;
                    if (__fieldNameCompare.Equals(fieldName) || __getText._name.Equals(fieldName))
                    {
                        if (updateOldText && __getText._isQuery == true && __getText._textFirst.Equals(value) == false) // toe fix หากไม่ใช่ field สำหรับ query ไม่ต้องเช็ค
                        {
                            this._isChange = true;
                        }
                        __getText._textFirst = value;
                        __getText._textSecond = value2;
                        if (updateOldText)
                        {
                            __getText._textLast = value;
                        }
                        string __textLast = __getText._textLast;
                        __getText.textBox.Text = __getText._textFirst;
                        /*Debug.Print("[" + value + "]," + __getText._labelName);
                        Debug.Print(Environment.StackTrace.ToString());*/
                        if (__getText._textSecond.Length > 0)
                        {
                            //getText.textBox.Text = string.Concat(getText._textFirst, "/", getText._textSecond);
                            __getText.textBox.Text = (__getText._textFirst.Trim().Length == 0) ? "" : string.Concat(__getText._textFirst.Trim(), "~", __getText._textSecond);//Moo Ae
                        }
                        if (__textLast.Equals(__getText._textFirst) == false && _textBoxChanged != null)
                        {
                            _textBoxChanged((TextBox)__getText.textBox, fieldName);
                        }
                        if (_textBoxSaved != null)
                        {
                            _textBoxSaved((TextBox)__getText.textBox, fieldName);
                        }
                        break;
                    }
                }
            }
        }

        public void _setDataStr(string fieldName, string value)
        {
            _setDataStr(fieldName, value, "", false);
        }

        public void _setDataNumber(string fieldName, decimal value)
        {
            foreach (Control __getControl in this.Controls)
            {
                if (__getControl.GetType() == typeof(_myNumberBox))
                {
                    _myNumberBox __getNumber = (_myNumberBox)__getControl;
                    if (__getNumber._name.CompareTo(fieldName) == 0)
                    {
                        __getNumber._double = value;
                        __getNumber.__oldResult = value;
                        __getNumber._textFirst = value.ToString();
                        __getNumber._refresh();
                        break;
                    }
                }
            }
        }

        public void _setDataDate(string fieldName, object value)
        {
            foreach (Control __getControl in this.Controls)
            {
                if (__getControl.GetType() == typeof(_myDateBox))
                {
                    _myDateBox __getText = (_myDateBox)__getControl;
                    if (__getText._name.CompareTo(fieldName) == 0)
                    {
                        if (value == null || value.GetType() != typeof(DateTime))
                        {
                            __getText._dateTime =
                            __getText._dateTimeOld = new DateTime(1000, 1, 1);
                        }
                        else
                        {
                            __getText._dateTime =
                            __getText._dateTimeOld = (DateTime)value;
                            __getText._refresh();
                        }
                        break;
                    }
                }
            }
        }

        public void _setCheckBox(string fieldName, Boolean value)
        {
            _setCheckBox(fieldName, (value) ? "true" : "false");
        }

        public void _setCheckBox(string fieldName, string value)
        {
            foreach (Control __getControl in this.Controls)
            {
                if (__getControl.GetType() == typeof(_myCheckBox))
                {
                    _myCheckBox __getCheckBox = (_myCheckBox)__getControl;
                    if (__getCheckBox._name.CompareTo(fieldName) == 0)
                    {
                        __getCheckBox.Checked = (value.CompareTo("1") == 0 || value.ToLower().CompareTo("true") == 0) ? true : false;
                        break;
                    }
                }
            }
        }

        public void _clearComcoBox(string fieldName)
        {
            _clearComcoBox(fieldName, true);
        }

        public void _clearComcoBox(string fieldName, Boolean _selectFirst)
        {
            foreach (Control __getControl in this.Controls)
            {
                if (__getControl.GetType() == typeof(_myComboBox))
                {
                    _myComboBox __getComboBox = (_myComboBox)__getControl;
                    if (__getComboBox._name.CompareTo(fieldName) == 0)
                    {
                        //__getCheckBox.SelectedIndex = value;
                        if (__getComboBox.Items.Count == 0 || _selectFirst == false)
                        {
                            __getComboBox.Text = "";
                            __getComboBox.SelectedItem = null;
                        }
                        else
                        {
                            __getComboBox.SelectedIndex = 0;
                        }
                        break;
                    }
                }
            }
        }

        public void _setComboBox(string fieldName, int value)
        {
            foreach (Control __getControl in this.Controls)
            {
                if (__getControl.GetType() == typeof(_myComboBox))
                {
                    _myComboBox __getCheckBox = (_myComboBox)__getControl;
                    if (__getCheckBox._name.CompareTo(fieldName) == 0)
                    {
                        __getCheckBox.SelectedIndex = value;
                        break;
                    }
                }
            }
        }

        public void _setComboBox(string fieldName, string value)
        {
            foreach (Control __getControl in this.Controls)
            {
                if (__getControl.GetType() == typeof(_myComboBox))
                {
                    _myComboBox __getCheckBox = (_myComboBox)__getControl;
                    if (__getCheckBox._name.CompareTo(fieldName) == 0)
                    {
                        __getCheckBox.SelectedItem = value;
                        //__getCheckBox.SelectedItem = value;

                        break;
                    }
                }
            }
        }

        public void _changeResource(string fieldName, string newText)
        {
            // ค้นหาและแก้ไข Label ตามชื่อ Field ในกรณีเปลี่ยนภาษา (หรือต้องการเปลี่ยนข้อความ)
            foreach (Control __getControl in this.Controls)
            {
                if (__getControl.GetType() == typeof(_myLabel))
                {
                    _myLabel __getText = (_myLabel)__getControl;
                    if (__getText._field_name.CompareTo(fieldName) == 0)
                    {
                        __getText.Text = string.Concat(newText, ":");
                        break;
                    }
                }
            }
        }

        public void _beforeChangeResource()
        {
            for (int __loop = 0; __loop < this._maxColumn; __loop++)
            {
                this._maxLabelWidth[__loop] = 0;
            }
            // วนรอบเดียวหา Label เพื่อคำนวณว่าควรจะกว้างเท่าใด
            foreach (Control __getControl in this.Controls)
            {
                if (__getControl.GetType() == typeof(_myLabel))
                {
                    _myLabel __getText = (_myLabel)__getControl;
                    SizeF __stringSize = __getControl.CreateGraphics().MeasureString(__getText.Text, this.Font); // SizeF __stringSize = __getControl.CreateGraphics().MeasureString(__getText.Text, _myGlobal._myFont);
                    int __width = (int)Math.Floor(__stringSize.Width + 0.5f);
                    if (_maxLabelWidth[__getText._column] < (int)__width + 5)
                    {
                        this._maxLabelWidth[__getText._column] = (int)__width + 5;
                    }
                }
            }
        }

        private void _setHeight(int row, int rowCount)
        {
            // toe เพิ่ม linespace
            int __space = 0;
            if (_lineSpace > 0 && row > 0)
            {
                __space = row * _lineSpace;
            }
            //int __calc = ((int)((row + rowCount) * this.Font.Height * 1.5));
            int __calc = ((int)((row + rowCount) * (this.Font.GetHeight() + 8)) + __space);
            if (__calc > this.Height)
            {
                this.Height = __calc + 4;
            }
        }

        /// <summary>
        /// คำนวณความกว้างของ Column
        /// </summary>
        int _calcColumnWidth()
        {
            return ((this.Width - (this.Padding.Left + this.Padding.Right)) / _maxColumn);
        }

        /// <summary>
        /// คำนวณตำแหน่งของ Column
        /// </summary>
        int _calcColumnPosition(int column)
        {
            return (this._calcColumnWidth() * column) + this.Padding.Left;
        }

        int _calcRowPosition(int row)
        {
            //return (row * 23) + 1 + this.Padding.Top;
            //return (int)((row * (this.Font.Height * 1.5)) + (this.Padding.Top + 1));
            int __space = 0;
            if (_lineSpace > 0 && row > 0)
            {
                __space = row * _lineSpace;
            }

            return (int)((row * (this.Font.GetHeight() + 8)) + (this.Padding.Top + 1) + __space);
        }

        /// <summary>
        /// เพิ่ม Label Control
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="fieldName"></param>
        /// <returns>Str  + ความยาว</returns>
        public _addLabelReturn _addLabel(int row, int column, string buttomStr, string fieldName, string fieldNameResource)
        {
            _addLabelReturn __resultLabel = new _addLabelReturn();
            __resultLabel._label_name = "";
            __resultLabel._length = 0;
            this._setHeight(row, 1);
            __resultLabel._label = new _myLabel();
            __resultLabel._label._row = row;
            __resultLabel._label._column = column;
            __resultLabel._label.Font = new Font(this.Font.FontFamily, this.Font.Size); // toe new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            __resultLabel._label.TextAlign = ContentAlignment.MiddleRight;
            __resultLabel._label._field_name = fieldName;
            __resultLabel._label._buttomStr = buttomStr;
            //__result._label._resource_name = (this._table_name.Length > 0) ? (string.Concat(this._table_name, ".", fieldNameResource)) : fieldNameResource;
            __resultLabel._label._getResource = this._getResource;
            string __resourceName = (this._table_name.Length > 0) ? (string.Concat(this._table_name, ".", fieldNameResource)) : fieldNameResource;
            if (this._getResource && MyLib._myGlobal._isDesignMode == false)
            {
                _myResourceType __getResource = _myResource._findResource(__resourceName, __resourceName);
                if (__getResource._length > 0)
                {
                    __resultLabel._length = __getResource._length;
                }
                if (__getResource._str.Length == 0)
                {
                    __getResource._str = fieldName;
                }
                __resultLabel._label_name = __getResource._str;
                __resultLabel._label.Text = __getResource._str + buttomStr;
            }
            else
            {
                __resultLabel._label_name = __resultLabel._label.Text = fieldName;
            }
            if (((Control.ModifierKeys & Keys.Control) == Keys.Control) && ((Control.ModifierKeys & Keys.Shift) == Keys.Shift))
            {
                __resultLabel._label.Text = __resourceName; //fieldName;
                this.Controls.Add(__resultLabel._label);
            }
            else
            {
                this.Controls.Add(__resultLabel._label);
            }
            this._beforeChangeResource();
            return (__resultLabel);
        }

        public string _getLabelName(string fieldName)
        {
            foreach (Control __getControl in this.Controls)
            {
                if (__getControl.GetType() == typeof(_myLabel))
                {
                    _myLabel __getLabel = (_myLabel)__getControl;
                    if (__getLabel._field_name.CompareTo(fieldName) == 0)
                    {
                        return (__getLabel.Text);
                    }
                }
            }
            return ("");
        }

        /// <summary>
        /// ตรวจสอบว่ามีการบันทึกครบตามกำหนดหรือเปล่า
        /// </summary>
        /// <returns></returns>
        public string _checkEmtryField()
        {
            StringBuilder __result = new StringBuilder();
            foreach (Control __getControl in this.Controls)
            {
                if (__getControl.GetType() == typeof(_myTextBox))
                {
                    _myTextBox __getTextBox = (_myTextBox)__getControl;
                    if (__getTextBox._emtry == false)
                    {
                        if (__getTextBox.textBox.Text.Length == 0)
                        {
                            __result.Append(string.Concat("\n", __getTextBox._labelName));
                        }
                    }
                }
                if (__getControl.GetType() == typeof(_myDateBox))
                {
                    _myDateBox __getTextBox = (_myDateBox)__getControl;
                    if (__getTextBox._emtry == false)
                    {
                        if (__getTextBox.textBox.Text.Length == 0)
                        {
                            __result.Append(string.Concat("\n", __getTextBox._labelName));
                        }
                    }
                }
            }
            return (__result.ToString());
        }

        public void _addImage(int row, int column, int rowCount, int maxColumn, Boolean displayLabel, string fieldName)
        {
            _myImage __picture = new _myImage(row, column, rowCount, maxColumn, fieldName);
            if (displayLabel)
            {
                _addLabelReturn __getLabel = _addLabel(row, column, ":", fieldName, fieldName);
                __picture._label = __getLabel._label;
            }
            this.Controls.Add(__picture);
        }

        /// <summary>
        /// เพิ่ม TextBox Control
        /// </summary>
        public void _addTextBox(int row, int column, string fieldName, int maxLength)
        {
            _addTextBox(row, column, 1, 0, fieldName, 1, maxLength, 0, true, false);
        }

        /// <summary>
        /// เพิ่ม TextBox Control
        /// </summary>
        public void _addTextBox(int row, int column, int rowCount, string fieldName, int maxColumn, int maxLength)
        {
            _addTextBox(row, column, rowCount, 0, fieldName, maxColumn, maxLength, 0, true, false);
        }

        /// <summary>
        /// เพิ่ม TextBox Control
        /// </summary>
        public void _addTextBox(int row, int column, int rowCount, int subColumn, string fieldName, int maxColumn, int maxLength)
        {
            _addTextBox(row, column, rowCount, subColumn, fieldName, maxColumn, maxLength, 0, true, false);
            _setUpper("");
        }

        /// <summary>
        /// เพิ่ม TextBox Control
        /// </summary>
        public void _addTextBox(int row, int column, int rowCount, int subColumn, string fieldName, int maxColumn, int maxLength, int iconNumber, Boolean displayLabel, Boolean isPassword, Boolean isEmtry)
        /// <summary>
        /// เพิ่ม TextBox Control
        /// </summary>
        {
            _addTextBox(row, column, rowCount, subColumn, fieldName, maxColumn, maxLength, iconNumber, displayLabel, isPassword, isEmtry, true, true);
        }
        public void _addTextBox(int row, int column, int rowCount, int subColumn, string fieldName, int maxColumn, int maxLength, int iconNumber, Boolean displayLabel, Boolean isPassword, Boolean isEmtry, Boolean isQuery)
        {
            _addTextBox(row, column, rowCount, subColumn, fieldName, maxColumn, maxLength, iconNumber, displayLabel, isPassword, isEmtry, isQuery, true);
        }

        public void _addTextBox(int row, int column, int rowCount, int subColumn, string fieldName, int maxColumn, int maxLength, int iconNumber, Boolean displayLabel, Boolean isPassword, Boolean isEmtry, Boolean isQuery, bool tabStop)
        {
            _addTextBox(row, column, rowCount, subColumn, fieldName, maxColumn, maxLength, iconNumber, displayLabel, isPassword, isEmtry, isQuery, tabStop, fieldName);
        }

        public void _addTextBox(int row, int column, int rowCount, int subColumn, string fieldName, int maxColumn, int maxLength, int iconNumber, Boolean displayLabel, Boolean isPassword, Boolean isEmtry, Boolean isQuery, bool tabStop, string resourceFieldName)
        {
            _addTextBox(row, column, rowCount, subColumn, fieldName, maxColumn, maxLength, iconNumber, displayLabel, isPassword, isEmtry, isQuery, tabStop, resourceFieldName, "");
        }


        public void _addTextBox(int row, int column, int rowCount, int subColumn, string fieldName, int maxColumn, int maxLength, int iconNumber, Boolean displayLabel, Boolean isPassword, Boolean isEmtry, Boolean isQuery, bool tabStop, string resourceFieldName, string searchFormat)
        {
            _addTextBox(row, column, rowCount, subColumn, fieldName, maxColumn, maxLength, iconNumber, displayLabel, isPassword, isEmtry, isQuery, false, tabStop, resourceFieldName, searchFormat);
        }
        /// <summary>
        /// เพิ่มช่องแบบ Text
        /// </summary>
        /// <param name="row">row เริ่มจาก 0</param>
        /// <param name="column">col  เริ่มจาก 0</param>
        /// <param name="rowCount">สูงกี่บรรทัด</param>
        /// <param name="subColumn"></param>
        /// <param name="fieldName">ชื่อ field</param>
        /// <param name="maxColumn">column span กี่แถว</param>
        /// <param name="maxLength">ความยาวสูงสุดของ Text</param>
        /// <param name="iconNumber">1=ค้นหาแบบจอเล็ก,2=ปุ่มขึ้นลง,3=Autorun,4=ค้นหาแบบเต็มใหญ่</param>
        /// <param name="displayLabel">แสดงข้อความด้านหน้า TextBox หรือไม่</param>
        /// <param name="isPassword">เป็นรหัสผ่านหรือไม่</param>
        /// <param name="isEmtry">อนุญาติให้ว่างหรือไม่</param>
        /// <param name="isQuery">ประกอบใน Query หรือไม่</param>
        /// <param name="tabStop">ให้กด Tab แล้วเข้ามาได้หรือไม่</param>
        /// <param name="resourceFieldName">ชื่อ Resource ในกรณีไม่ตรงกับชื่อ Field</param>
        public void _addTextBox(int row, int column, int rowCount, int subColumn, string fieldName, int maxColumn, int maxLength, int iconNumber, Boolean displayLabel, Boolean isPassword, Boolean isEmtry, Boolean isQuery, Boolean isGetData, bool tabStop, string resourceFieldName, string searchFormat)
        {
            string __label_name = "";
            //setHeight(row, rowCount);
            int __getLength = maxLength;
            _myTextBox __newTextBox = new _myTextBox();
            if (displayLabel)
            {
                _addLabelReturn __getLabel = this._addLabel(row, column, ":", resourceFieldName, resourceFieldName);
                __newTextBox._label = __getLabel._label;
                __getLength = __getLabel._length;
                __label_name = __getLabel._label_name;
                __newTextBox._displayLabel = true;
            }
            else
            {
                __newTextBox._displayLabel = false;
            }
            __newTextBox._row = row;
            __newTextBox._column = column;
            __newTextBox._name = fieldName;
            __newTextBox._maxColumn = maxColumn;
            __newTextBox._rowCount = rowCount;
            __newTextBox.Font = new Font(this.Font.FontFamily, this.Font.Size); // toe new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            __newTextBox.MaxLength = (__getLength == 0) ? 255 : __getLength;
            __newTextBox.textBox.MaxLength = __newTextBox.MaxLength;
            __newTextBox._icon = (iconNumber == 0) ? false : true;
            __newTextBox._emtry = isEmtry;
            __newTextBox._isQuery = isQuery;
            __newTextBox._enterToTab = true;
            __newTextBox._isSearch = (iconNumber == 0) ? false : true;
            __newTextBox._iconNumber = iconNumber;
            __newTextBox._labelName = __label_name;
            __newTextBox.textBox.Leave += new EventHandler(textBox_Leave);
            __newTextBox.textBox.Enter += new EventHandler(textBox_Enter);
            __newTextBox._cellSearch += new CellSearchHandler(newTextBox__cellSearch);
            __newTextBox._cellMoveUp += new CellMoveUpHandler(__newTextBox__cellMoveUp);
            __newTextBox._cellMoveDown += new CellMoveDownHandler(__newTextBox__cellMoveDown);
            __newTextBox.TabStop = tabStop;
            __newTextBox._isGetData = isGetData;
            __newTextBox._searchFormat = searchFormat;
          
            if (isPassword)
            {
                __newTextBox.textBox.PasswordChar = '*';
            }
            if (rowCount > 1)
            {
                __newTextBox.textBox.Multiline = true;
            }
            this.Controls.Add(__newTextBox);
            _setHeight(row, rowCount);
        }

        void _cellMoveUp(int column, int row)
        {
            for (int __rowStep = row - 1; __rowStep >= 0; __rowStep--)
            {
                foreach (Control __getControl in this.Controls)
                {
                    if (__getControl.GetType() == typeof(_myTextBox))
                    {
                        _myTextBox __getText = (_myTextBox)__getControl;
                        if (__getText._column == column && __getText._row == __rowStep)
                        {
                            __getText.textBox.Focus();
                            __getText.textBox.SelectAll();
                            return;
                        }
                    }
                    else if (__getControl.GetType() == typeof(_myDateBox))
                    {
                        _myDateBox __getText = (_myDateBox)__getControl;
                        if (__getText._column == column && __getText._row == __rowStep)
                        {
                            __getText.textBox.Focus();
                            __getText.textBox.SelectAll();
                            return;
                        }
                    }
                    else if (__getControl.GetType() == typeof(_myNumberBox))
                    {
                        _myNumberBox __getText = (_myNumberBox)__getControl;
                        if (__getText._column == column && __getText._row == __rowStep)
                        {
                            __getText.textBox.Focus();
                            __getText.textBox.SelectAll();
                            return;
                        }
                    }
                    else if (__getControl.GetType() == typeof(_myCheckBox))
                    {
                        _myCheckBox __getData = (_myCheckBox)__getControl;
                        if (__getData._column == column && __getData._row == __rowStep)
                        {
                            __getData.Focus();
                            return;
                        }
                    }
                    else if (__getControl.GetType() == typeof(_myComboBox))
                    {
                        _myComboBox __getData = (_myComboBox)__getControl;
                        if (__getData._column == column && __getData._row == __rowStep)
                        {
                            __getData.Focus();
                            return;
                        }
                    }
                }
            }
            SendKeys.Send("+{TAB}");
        }

        void _cellMoveDown(int column, int row)
        {
            foreach (Control __getControl in this.Controls)
            {
                if (__getControl.GetType() == typeof(_myTextBox))
                {
                    _myTextBox __getText = (_myTextBox)__getControl;
                    if (__getText._column == column && __getText._row > row)
                    {
                        __getText.textBox.Focus();
                        __getText.textBox.SelectAll();
                        return;
                    }
                }
                else if (__getControl.GetType() == typeof(_myDateBox))
                {
                    _myDateBox __getText = (_myDateBox)__getControl;
                    if (__getText._column == column && __getText._row > row)
                    {
                        __getText.textBox.Focus();
                        __getText.textBox.SelectAll();
                        return;
                    }
                }
                else if (__getControl.GetType() == typeof(_myNumberBox))
                {
                    _myNumberBox __getText = (_myNumberBox)__getControl;
                    if (__getText._column == column && __getText._row > row)
                    {
                        __getText.textBox.Focus();
                        __getText.textBox.SelectAll();
                        return;
                    }
                }
                else if (__getControl.GetType() == typeof(_myCheckBox))
                {
                    _myCheckBox __getData = (_myCheckBox)__getControl;
                    if (__getData._column == column && __getData._row > row)
                    {
                        __getData.Focus();
                        return;
                    }
                }
                else if (__getControl.GetType() == typeof(_myComboBox))
                {
                    _myComboBox __getData = (_myComboBox)__getControl;
                    if (__getData._column == column && __getData._row > row)
                    {
                        __getData.Focus();
                        return;
                    }
                }
            }
            SendKeys.Send("{TAB}");
        }

        void __newTextBox__cellMoveDown(object sender)
        {
            // ค้นหาตำแหน่งที่ใกล้ที่สุด			
            _myTextBox __sender = (_myTextBox)sender;
            _cellMoveDown(__sender._column, __sender._row);
        }

        void __newTextBox__cellMoveUp(object sender)
        {
            // ค้นหาตำแหน่งที่ใกล้ที่สุด			
            _myTextBox __sender = (_myTextBox)sender;
            _cellMoveUp(__sender._column, __sender._row);
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            if (this._autoUpperString)
            {
                if (sender != null)
                {
                    if (((TextBox)sender).Parent.GetType() == typeof(_myTextBox))
                    {
                        _myTextBox __result = (_myTextBox)((TextBox)sender).Parent;
                        if (__result._iconNumber == 1 || __result._iconNumber == 3 || __result._iconNumber == 4)
                        {
                            __result.textBox.Text = __result.textBox.Text.ToUpper();
                        }
                    }
                }
                _textBoxSave(sender);
            }
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            _enterField(sender);
        }

        void newTextBox__cellSearch(object sender, string e)
        {
            if (_textBoxSearch != null)
            {
                _textBoxSearch(sender);
            }
        }

        /// <summary>
        /// เพิ่ม TextBox Control
        /// </summary>
        public void _addTextBox(int row, int column, int rowCount, int subColumn, string fieldName, int maxColumn, int maxLength, int iconNumber, Boolean displayLabel, Boolean isPassword)
        {
            _addTextBox(row, column, rowCount, subColumn, fieldName, maxColumn, maxLength, iconNumber, displayLabel, isPassword, true);
        }

        void _textBoxSave(object sender)
        {
            if (sender == null)
            {
                return;
            }
            if (sender.GetType() == typeof(TextBox))
            {
                if (((TextBox)sender).Parent.GetType() == typeof(_myTextBox))
                {
                    _myTextBox __result = (_myTextBox)((TextBox)sender).Parent;
                    if (__result._displayLabel && __result._label != null)
                    {
                        __result._label.Font = new Font(__result._label.Font, FontStyle.Regular);
                        __result._label.Invalidate();
                    }
                    string __text = (__result.IsUpperCase && this._autoUpperString) ? __result.textBox.Text.ToUpper() : __result.textBox.Text;
                    if (__result._isSearch)
                    {
                        __text = __text.Split('~')[0].ToString();
                    }
                    if (__result._textFirst.CompareTo(__text) != 0)
                    {
                        this._isChange = true;
                    }
                    __result._textFirst = __text;
                    if (__result._textFirst.CompareTo(__result._textLast) != 0)
                    {
                        __result._textLast = __result._textFirst;
                        if (_textBoxChanged != null)
                        {
                            _textBoxChanged((TextBox)sender, __result._name);
                        }
                    }
                    if (_textBoxSaved != null)
                    {
                        _textBoxSaved((TextBox)sender, __result._name);
                    }
                    if (__result._textSecond.Length > 0)
                    {
                        __result.textBox.Text = (__result._textFirst.Trim().Length == 0) ? "" : string.Concat(__result._textFirst, "~", __result._textSecond);
                    }
                }
                // Date
                if (((TextBox)sender).Parent.GetType() == typeof(_myDateBox))
                {
                    _myDateBox __result = (_myDateBox)((TextBox)sender).Parent;
                    __result._checkDate(true, false);
                    __result._label.Font = new Font(__result._label.Font, FontStyle.Regular);
                    String __get1 = MyLib._myGlobal._convertDateToString(__result._dateTime, false);
                    if (__get1.CompareTo(MyLib._myGlobal._convertDateToString(__result._dateTimeOld, false)) != 0)
                    {
                        __result._dateTimeOld = __result._dateTime;
                        if (_textBoxChanged != null)
                        {
                            _textBoxChanged((TextBox)sender, __result._name);
                        }
                    }
                    if (_textBoxSaved != null)
                    {
                        _textBoxSaved((TextBox)sender, __result._name);
                    }
                }
                // Number
                if (((TextBox)sender).Parent.GetType() == typeof(_myNumberBox))
                {
                    _myNumberBox __result = (_myNumberBox)((TextBox)sender).Parent;
                    __result._label.Font = new Font(__result._label.Font, FontStyle.Regular);
                    if (__result._double != __result.__oldResult)
                    {
                        this._isChange = true;
                        __result.__oldResult = __result._double;
                        if (_textBoxChanged != null)
                        {
                            _textBoxChanged((TextBox)sender, __result._name);
                        }
                    }
                    if (_textBoxSaved != null)
                    {
                        _textBoxSaved((TextBox)sender, __result._name);
                    }
                }
            }
        }

        public void _enterField(object sender)
        {
            if (sender.GetType() == typeof(TextBox))
            {
                _lastTextBox = (TextBox)sender;
                if (((TextBox)sender).Parent.GetType() == typeof(_myTextBox))
                {
                    _myTextBox __result = (_myTextBox)((TextBox)sender).Parent;
                    if (__result._displayLabel && __result._label != null)
                    {
                        __result._label.Font = new Font(__result._label.Font, FontStyle.Underline);
                    }
                    _lastControl = (Control)__result;
                    if (__result.IsUpperCase && this._autoUpperString)
                    {
                        __result.textBox.Text = __result._textFirst.ToUpper();
                    }
                    else
                    {
                        __result.textBox.Text = __result._textFirst;
                    }
                }
                else
                if (((TextBox)sender).Parent.GetType() == typeof(_myDateBox))
                {
                    _myDateBox __result = (_myDateBox)((TextBox)sender).Parent;
                    if (__result._displayLabel && __result._label != null)
                    {
                        __result._label.Font = new Font(__result._label.Font, FontStyle.Underline);
                    }
                    _lastControl = (Control)__result;
                }
                else
                if (((TextBox)sender).Parent.GetType() == typeof(_myNumberBox))
                {
                    _myNumberBox __result = (_myNumberBox)((TextBox)sender).Parent;
                    if (__result._displayLabel && __result._label != null)
                    {
                        __result._label.Font = new Font(__result._label.Font, FontStyle.Underline);
                    }
                    _lastControl = (Control)__result;
                }
            }
        }

        /// <summary>
        /// เมื่อกดปุ่มค้นหาใน TextBox
        /// </summary>
        public event TextBoxSearchHandler _textBoxSearch;
        public event TextBoxChangedHandler _textBoxChanged;
        public event TextBoxSavedHandler _textBoxSaved;
        public event CheckBoxChangedHandler _checkBoxChanged;
        public event CheckKeyDownHandler _checkKeyDown;
        public event CheckKeyDownReturnHandler _checkKeyDownReturn;
        public event AfterRefreshHandler _afterRefresh;
        public event AfterClearHandler _afterClear;

        public void _addNumberBox(int row, int column, int rowCount, int subColumn, string fieldName, int maxColumn, int point, Boolean displayLabel)
        {
            _addNumberBox(row, column, rowCount, subColumn, fieldName, maxColumn, point, displayLabel, MyLib._myGlobal._getFormatNumber("m02"), true, fieldName);
        }

        /// <summary>
        /// เพิ่ม Input Number Control
        /// </summary>
        public void _addNumberBox(int row, int column, int rowCount, int subColumn, string fieldName, int maxColumn, int point, Boolean displayLabel, string format)
        {
            _addNumberBox(row, column, rowCount, subColumn, fieldName, maxColumn, point, displayLabel, format, true, fieldName);
        }
        public void _addNumberBox(int row, int column, int rowCount, int subColumn, string fieldName, int maxColumn, int point, Boolean displayLabel, string format, Boolean isQuery)
        {
            _addNumberBox(row, column, rowCount, subColumn, fieldName, maxColumn, point, displayLabel, format, isQuery, fieldName);
        }

        public void _addNumberBox(int row, int column, int rowCount, int subColumn, string fieldName, int maxColumn, int point, Boolean displayLabel, string format, Boolean isQuery, string resourceFieldName)
        {
            _addNumberBox(row, column, rowCount, subColumn, fieldName, maxColumn, point, displayLabel, format, isQuery, resourceFieldName, "");
        }
        /// <summary>
        /// เพิ่ม Input Number Control
        /// </summary>
        public void _addNumberBox(int row, int column, int rowCount, int subColumn, string fieldName, int maxColumn, int point, Boolean displayLabel, string format, Boolean isQuery, string resourceFieldName, string searchFormat)
        {
            _setHeight(row, rowCount);
            _myNumberBox __newTextBox = new _myNumberBox();
            if (displayLabel)
            {
                _addLabelReturn __getLabel = _addLabel(row, column, ":", resourceFieldName, resourceFieldName);
                __newTextBox._label = __getLabel._label;
            }
            __newTextBox._searchFormat = searchFormat;
            __newTextBox._row = row;
            __newTextBox._column = column;
            __newTextBox._name = fieldName;
            __newTextBox._maxColumn = maxColumn;
            __newTextBox._rowCount = rowCount;
            __newTextBox._point = point;
            __newTextBox._format = format;
            __newTextBox._enterToTab = true;
            __newTextBox._isQuery = isQuery;
            __newTextBox.Font = new Font(this.Font.FontFamily, this.Font.Size); // toe  new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            __newTextBox.textBox.Leave += new EventHandler(textBox_Leave);
            __newTextBox.textBox.Enter += new EventHandler(textBox_Enter);
            __newTextBox._cellMoveDown += new CellMoveDownHandler(__newTextBox__cellMoveDown);
            __newTextBox._cellMoveUp += new CellMoveUpHandler(__newTextBox__cellMoveUp);
            //newTextBox.textBox.KeyDown += new KeyEventHandler(textBox_KeyDown);
            this.Controls.Add(__newTextBox);
        }

        /// <summary>
        /// เพิ่ม Input Date Control
        /// </summary>
        public void _addDateBox(int row, int column, int rowCount, int subColumn, string fieldName, int maxColumn, Boolean displayLabel, Boolean isEmtry)
        {
            _addDateBox(row, column, rowCount, subColumn, fieldName, maxColumn, displayLabel, isEmtry, true, fieldName);
        }
        public void _addDateBox(int row, int column, int rowCount, int subColumn, string fieldName, int maxColumn, Boolean displayLabel, Boolean isEmtry, Boolean isQuery)
        {
            _addDateBox(row, column, rowCount, subColumn, fieldName, maxColumn, displayLabel, isEmtry, isQuery, fieldName);
        }
        public void _addDateBox(int row, int column, int rowCount, int subColumn, string fieldName, int maxColumn, Boolean displayLabel, Boolean isEmtry, Boolean isQuery, string resourceFieldName)
        {
            string __label_name = "";
            _setHeight(row, rowCount);
            _myDateBox __newTextBox = new _myDateBox();
            __newTextBox._lostFocust = false;
            if (displayLabel)
            {
                _addLabelReturn getLabel = _addLabel(row, column, __label_name + ":", resourceFieldName, resourceFieldName);
                __newTextBox._label = getLabel._label;
                __label_name = getLabel._label_name;
            }
            __newTextBox._row = row;
            __newTextBox._column = column;
            __newTextBox._name = fieldName;
            __newTextBox._maxColumn = maxColumn;
            __newTextBox._rowCount = rowCount;
            __newTextBox._enterToTab = true;
            __newTextBox._emtry = isEmtry;
            __newTextBox._isQuery = isQuery;
            __newTextBox._labelName = __label_name;
            __newTextBox.Font = new Font(this.Font.FontFamily, this.Font.Size); // toe   new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            __newTextBox.textBox.Enter += new EventHandler(textBox_Enter);
            __newTextBox.textBox.Leave += new EventHandler(textBox_Leave);
            __newTextBox._cellMoveDown += new CellMoveDownHandler(__newTextBox__cellMoveDown);
            __newTextBox._cellMoveUp += new CellMoveUpHandler(__newTextBox__cellMoveUp);
            this.Controls.Add(__newTextBox);
        }
        /// <summary>
        /// เพิ่ม Input Date Control
        /// </summary>
        public void _addDateBox(int row, int column, int rowCount, int subColumn, string fieldName, int maxColumn, Boolean displayLabel)
        {
            _addDateBox(row, column, rowCount, subColumn, fieldName, maxColumn, displayLabel, true, true, fieldName);
        }

        /// <summary>
        /// เมื่อกดปุ่ม Save
        /// </summary>
        public event SaveKeyDownHandler _saveKeyDown;
        /// <summary>
        /// เมื่อกดปุ่มปิด (Exit)
        /// </summary>
        public event ExitKeyDownHandler _exitKeyDown;
        /// <summary>
        /// เมื่อกดปุ่ม
        /// </summary>
        public event ButtonClickHandler _buttonClick;

        protected virtual void _saveKeyDownWork(object sender)
        {
            if (_saveKeyDown != null) _saveKeyDown(sender);
        }

        protected virtual void _exitKeyDownWork(object sender)
        {
            if (_exitKeyDown != null) _exitKeyDown(sender);
        }

        public event ComboBoxSelectIndexChangedHandler _comboBoxSelectIndexChanged;
        /// <summary>
        /// เพิ่ม ComboBox Control
        /// </summary>
        public void _addComboBox(int row, int column, string fieldName, Boolean displayLabel, string[] listName, Boolean addCounter)
        {
            _addComboBox(row, column, fieldName, displayLabel, listName, addCounter, fieldName);
        }

        public void _addComboBox(int row, int column, string fieldName, Boolean displayLabel, string[] listName, Boolean addCounter, string resourceFieldName)
        {
            _addComboBox(row, column, fieldName, displayLabel, listName, addCounter, resourceFieldName, true);
        }

        public void _addComboBox(int row, int column, string fieldName, Boolean displayLabel, string[] listName, Boolean addCounter, string resourceFieldName, bool getResourceList)
        {
            _addComboBox(row, column, fieldName, 1, displayLabel, listName, addCounter, resourceFieldName, getResourceList);
        }

        public void _addComboBox(int row, int column, string fieldName, int maxColumn, Boolean displayLabel, string[] listName, Boolean addCounter, string resourceFieldName, bool getResourceList)
        {
            _addComboBox(row, column, fieldName, maxColumn, displayLabel, listName, addCounter, resourceFieldName, getResourceList, true);

        }

        // toe override เพื่อ ไม่ให้ get resource ของ list ใน combobox
        public void _addComboBox(int row, int column, string fieldName, int maxColumn, Boolean displayLabel, string[] listName, Boolean addCounter, string resourceFieldName, bool getResourceList, bool isQuery)
        {
            _setHeight(row, 1);
            _myComboBox __newComboBox = new _myComboBox();
            if (displayLabel)
            {
                _addLabelReturn __getLabel = _addLabel(row, column, ":", resourceFieldName, resourceFieldName);
                __newComboBox._label = __getLabel._label;
            }
            __newComboBox._isQuery = isQuery;
            __newComboBox._row = row;
            __newComboBox._column = column;
            __newComboBox._maxColumn = maxColumn;
            __newComboBox._name = fieldName;
            __newComboBox._listName = listName;
            __newComboBox._addCounter = addCounter;
            __newComboBox._tableName = this._table_name;
            __newComboBox.Font = new Font(this.Font.FontFamily, this.Font.Size); // toe   new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            __newComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            __newComboBox._cellMoveLeft += new CellComboBoxMoveLeftHandler(__newComboBox__cellMoveLeft);
            __newComboBox._cellMoveRight += new CellComboBoxMoveRightHandler(newComboBox__cellMoveRight);
            __newComboBox._cellMoveDown += new CellComboBoxMoveDownHandler(__newComboBox__cellMoveDown);
            __newComboBox._cellMoveUp += new CellComboBoxMoveUpHandler(__newComboBox__cellMoveUp);
            //newComboBox.KeyDown += new KeyEventHandler(newComboBox_KeyDown);
            for (int __loop = 0; __loop < listName.Length; __loop++)
            {
                string __fieldname_2 = (this._table_name.Length > 0) ? (string.Concat(this._table_name, ".", listName[__loop])) : listName[__loop];
                int __loopStr = __loop + 1;
                string __getStr = ((addCounter) ? (__loopStr + ".") : "") + ((getResourceList) ? ((this._getResource && MyLib._myGlobal._isDesignMode == false) ? _myResource._findResource(__fieldname_2, __fieldname_2)._str : __fieldname_2) : listName[__loop]);
                __newComboBox.Items.Add(__getStr);
                __newComboBox.SelectedIndex = 0;
                Graphics __myGraphics = this.CreateGraphics();
                __myGraphics.SmoothingMode = SmoothingMode.HighQuality;
                SizeF __stringSize = __myGraphics.MeasureString(__getStr, _myGlobal._myFont);
                if (__stringSize.Width + 5 > __newComboBox.DropDownWidth)
                {
                    __newComboBox.DropDownWidth = (int)__stringSize.Width + 5;
                }
            }
            __newComboBox.MaxDropDownItems = (listName.Length == 0) ? 1 : listName.Length;
            __newComboBox.Enter += new EventHandler(newComboBox_Enter);
            __newComboBox.Leave += new EventHandler(__newComboBox_Leave);
            __newComboBox.SelectedIndexChanged += new EventHandler(__newComboBox_SelectedIndexChanged);
            this.Controls.Add(__newComboBox);
        }
        void __newComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((_myComboBox)sender)._oldValue != ((_myComboBox)sender).SelectedIndex)
            {
                this._isChange = true;
                ((_myComboBox)sender)._oldValue = ((_myComboBox)sender).SelectedIndex;
            }
            if (_comboBoxSelectIndexChanged != null)
            {
                _comboBoxSelectIndexChanged(sender, ((_myComboBox)sender)._name);
            }
        }

        void __newComboBox__cellMoveLeft(object sender)
        {
            SendKeys.Send("+{TAB}");
        }

        void __newComboBox__cellMoveUp(object sender)
        {
            // ค้นหาตำแหน่งที่ใกล้ที่สุด			
            _myComboBox __sender = (_myComboBox)sender;
            _cellMoveUp(__sender._column, __sender._row);
        }

        void __newComboBox__cellMoveDown(object sender)
        {
            // ค้นหาตำแหน่งที่ใกล้ที่สุด			
            _myComboBox __sender = (_myComboBox)sender;
            _cellMoveDown(__sender._column, __sender._row);
        }

        void __newComboBox_Leave(object sender, EventArgs e)
        {
            _myComboBox __result = (_myComboBox)sender;
            if (__result._label != null)
            {
                __result._label.Font = new Font(__result._label.Font, FontStyle.Regular);
            }
        }

        void newComboBox_Enter(object sender, EventArgs e)
        {
            _lastControl = (Control)sender;
            _myComboBox __result = (_myComboBox)sender;
            if (__result._label != null)
            {
                __result._label.Font = new Font(__result._label.Font, FontStyle.Underline);
            }
        }

        void newComboBox__cellMoveRight(object sender)
        {
            SendKeys.Send("{TAB}");
        }

        /// <summary>
        /// เพิ่ม CheckBox Control
        /// </summary>
        public void _addCheckBox(int row, int column, string fieldName, Boolean displayLabel, Boolean checkBoxName)
        {
            _addCheckBox(row, column, fieldName, displayLabel, checkBoxName, false);
        }
        public void _addCheckBox(int row, int column, string fieldName, Boolean displayLabel, Boolean checkBoxName, Boolean isCheck)
        {
            _addCheckBox(row, column, fieldName, displayLabel, checkBoxName, isCheck, fieldName);
        }
        public void _addCheckBox(int row, int column, string fieldName, Boolean displayLabel, Boolean checkBoxName, Boolean isCheck, string resourceFieldName)
        {
            _addCheckBox(row, column, fieldName, displayLabel, checkBoxName, isCheck, true, resourceFieldName);
        }

        public void _addCheckBox(int row, int column, string fieldName, Boolean displayLabel, Boolean checkBoxName, Boolean isCheck, Boolean _isQuery, string resourceFieldName)
        {
            _addCheckBox(row, column, fieldName, displayLabel, checkBoxName, isCheck, _isQuery, resourceFieldName, false);
        }

        //public void _addCheckBox(int row, int column, string fieldName, Boolean displayLabel, Boolean checkBoxName, Boolean isCheck, Boolean defaultValue, Boolean _isQuery, string resourceFieldName)
        public void _addCheckBox(int row, int column, string fieldName, Boolean displayLabel, Boolean checkBoxName, Boolean isCheck, Boolean _isQuery, string resourceFieldName, Boolean defaultValue)
        {
            _setHeight(row, 1);
            _myCheckBox __newCheckBox = new _myCheckBox();
            if (displayLabel)
            {
                _addLabelReturn __label = _addLabel(row, column, ":", resourceFieldName, resourceFieldName);
                __newCheckBox._label = __label._label;
            }
            __newCheckBox._row = row;
            __newCheckBox._defaultValue = defaultValue;
            __newCheckBox._column = column;
            __newCheckBox._name = fieldName;
            __newCheckBox.Checked = isCheck;
            __newCheckBox.Font = new Font(this.Font.FontFamily, this.Font.Size); // toe   new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            __newCheckBox._isQuery = _isQuery;
            __newCheckBox.ResourceName = "";
            __newCheckBox._cellMoveRight += new CellCheckBoxMoveRightHandler(newCheckBox__cellMoveRight);
            //newCheckBox.KeyDown += new KeyEventHandler(newCheckBox_KeyDown);
            if (checkBoxName)
            {
                string __fieldname_2 = (this._table_name.Length > 0) ? (string.Concat(this._table_name, ".", resourceFieldName)) : resourceFieldName;
                __newCheckBox._resource_name = __fieldname_2;
                if (MyLib._myGlobal._isDesignMode == false)
                {
                    __newCheckBox.Text = _myResource._findResource(__fieldname_2, __fieldname_2)._str;
                    if (__newCheckBox.Text.Length == 0)
                    {
                        __newCheckBox.Text = resourceFieldName;
                    }
                }
                else
                {
                    __newCheckBox.Text = resourceFieldName;
                }
            }
            __newCheckBox.CheckedChanged += new EventHandler(newCheckBox_CheckedChanged);
            __newCheckBox.Enter += new EventHandler(newCheckBox_Enter);
            __newCheckBox.Leave += new EventHandler(__newCheckBox_Leave);
            this.Controls.Add(__newCheckBox);
        }

        void __newCheckBox_Leave(object sender, EventArgs e)
        {
            _myCheckBox __result = (_myCheckBox)sender;
            if (__result._label != null)
            {
                __result._label.Font = new Font(__result._label.Font, FontStyle.Regular);
            }
        }

        void newCheckBox_Enter(object sender, EventArgs e)
        {
            _lastControl = (Control)sender;
            _myCheckBox __result = (_myCheckBox)sender;
            if (__result._label != null)
            {
                __result._label.Font = new Font(__result._label.Font, FontStyle.Underline);
            }
        }

        void newCheckBox__cellMoveRight(object sender)
        {
            SendKeys.Send("{TAB}");
        }

        void newCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_checkBoxChanged != null)
            {
                _checkBoxChanged(sender, ((_myCheckBox)sender)._name);
            }
        }

        /// <summary>
        /// เพิ่ม Group Box ใน Screen
        /// </summary>
        /// <param name="row">บรรทัด</param>
        /// <param name="column">ตำแหน่งของ Column เริ่มต้น</param>
        /// <param name="rowCount">ความสูง</param>
        /// <param name="maxColumnForGroupBox">จำนวน Column (ใน GroupBox)</param>
        /// <param name="maxColumn">จำนวน Column (กว้าง)</param>
        /// <param name="fieldName">ชื่อ Field ของ GroupBox</param>
        /// <returns></returns>
        public _myGroupBox _addGroupBox(int row, int column, int rowCount, int maxColumnForGroupBox, int maxColumn, string fieldName, bool query)
        {
            _setHeight(row, rowCount + 1);
            _myGroupBox __newGroupBox = new _myGroupBox();
            __newGroupBox.__row = row;
            __newGroupBox.__column = column;
            __newGroupBox.__rowCount = rowCount;
            __newGroupBox.Name = fieldName;
            __newGroupBox.__name = fieldName;
            __newGroupBox.__tableName = this._table_name;
            __newGroupBox.__maxColumn = maxColumnForGroupBox;
            __newGroupBox.__maxColumnPanel = maxColumn;
            __newGroupBox.Font = new Font(this.Font.FontFamily, this.Font.Size); // toe   new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            __newGroupBox.__resource_name = (this._table_name.Length > 0) ? (string.Concat(this._table_name, ".", fieldName)) : fieldName;
            __newGroupBox.__query = query;
            if (MyLib._myGlobal._isDesignMode == false)
            {
                _myResourceType __getResource = _myResource._findResource(__newGroupBox.__resource_name, __newGroupBox.__resource_name);
                if (__getResource._str.Length == 0)
                {
                    __getResource._str = fieldName;
                }
                __newGroupBox.Text = __getResource._str;
            }
            else
            {
                __newGroupBox.Text = fieldName;
            }
            this.Controls.Add(__newGroupBox);
            return (__newGroupBox);
        }

        public _myGroupBox _addGroupBox(int row, int column, int rowCount, int maxColumnForGroupBox, int maxColumn, string fieldName, bool query, string resourceFieldName)
        {
            _setHeight(row, rowCount + 1);
            _myGroupBox __newGroupBox = new _myGroupBox();
            __newGroupBox.__row = row;
            __newGroupBox.__column = column;
            __newGroupBox.__rowCount = rowCount;
            __newGroupBox.Name = fieldName;
            __newGroupBox.__name = fieldName;
            __newGroupBox.__tableName = this._table_name;
            __newGroupBox.__maxColumn = maxColumnForGroupBox;
            __newGroupBox.__maxColumnPanel = maxColumn;
            __newGroupBox.Font = new Font(this.Font.FontFamily, this.Font.Size); // toe   new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            __newGroupBox.__resource_name = (this._table_name.Length > 0) ? (string.Concat(this._table_name, ".", resourceFieldName)) : resourceFieldName;
            __newGroupBox.__query = query;
            if (MyLib._myGlobal._isDesignMode == false)
            {
                _myResourceType __getResource = _myResource._findResource(__newGroupBox.__resource_name, __newGroupBox.__resource_name);
                if (__getResource._str.Length == 0)
                {
                    __getResource._str = resourceFieldName;
                }
                __newGroupBox.Text = __getResource._str;
            }
            else
            {
                __newGroupBox.Text = fieldName;
            }
            this.Controls.Add(__newGroupBox);
            return (__newGroupBox);
        }

        /// <summary>
        /// เพิ่มปุ่ม RatiobBotton เข้าใน GroupBox
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="groupBox"></param>
        /// <param name="fieldName"></param>
        public _myRadioButton _addRadioButtonOnGroupBox(int row, int column, _myGroupBox groupBox, string fieldName, object value, bool isChecked)
        {
            return _addRadioButtonOnGroupBox(row, column, groupBox, fieldName, value, isChecked, fieldName);
        }

        public _myRadioButton _addRadioButtonOnGroupBox(int row, int column, _myGroupBox groupBox, string fieldName, object value, bool isChecked, string resourceFieldName)
        {
            _myRadioButton __radioButton = new _myRadioButton();
            __radioButton.Name = fieldName;
            __radioButton.__resource_name = (this._table_name.Length > 0) ? (string.Concat(this._table_name, ".", resourceFieldName)) : resourceFieldName;
            __radioButton.__row = row;
            __radioButton.__column = column;
            __radioButton.Font = new Font(this.Font.FontFamily, this.Font.Size); // toe   new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            __radioButton.__value = value;
            __radioButton.Checked = isChecked;
            if (MyLib._myGlobal._isDesignMode == false)
            {
                _myResourceType __getResource = _myResource._findResource(__radioButton.__resource_name, __radioButton.__resource_name);
                if (__getResource._str.Length == 0)
                {
                    __getResource._str = resourceFieldName;
                }
                __radioButton.Text = __getResource._str;
            }
            else
            {
                __radioButton.Text = resourceFieldName;
            }
            groupBox.Controls.Add(__radioButton);
            return __radioButton;
        }

        public _myButton _addButton(int row, int column, int maxColumn, string fieldName)
        {
            _myButton __button = new _myButton();
            __button.Name = fieldName;
            __button._resource_name = (this._table_name.Length > 0) ? (string.Concat(this._table_name, ".", fieldName)) : fieldName;
            __button._row = row;
            __button.Font = new Font(this.Font.FontFamily, this.Font.Size); // toe   new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            __button._column = column;
            __button._maxColumn = maxColumn;
            if (MyLib._myGlobal._isDesignMode == false)
            {
                _myResourceType __getResource = _myResource._findResource(__button._resource_name, __button._resource_name);
                if (__getResource._str.Length == 0)
                {
                    __getResource._str = fieldName;
                }
                __button.Text = __getResource._str;
            }
            else
            {
                __button.Text = fieldName;
            }
            __button.ButtonText = __button.Text;
            __button.Click += (s1, e1) =>
            {
                if (this._buttonClick != null)
                {
                    this._buttonClick(this, fieldName);
                }
            };
            this.Controls.Add(__button);
            return __button;
        }
    }

    public delegate void ButtonClickHandler(object sender, string name);
    public delegate void SaveKeyDownHandler(object sender);
    public delegate void ExitKeyDownHandler(object sender);
    public delegate void TextBoxSearchHandler(object sender);
    public delegate void TextBoxChangedHandler(object sender, string name);
    public delegate void TextBoxSavedHandler(object sender, string name);
    public delegate void CheckBoxChangedHandler(object sender, string name);
    public delegate void ComboBoxSelectIndexChangedHandler(object sender, string name);
    public delegate Boolean CheckKeyDownHandler(object sender, Keys keyData);
    public delegate Boolean CheckKeyDownReturnHandler(object sender, Keys keyData);
    public delegate void AfterRefreshHandler(object sender);
    public delegate void AfterClearHandler(object sender);
    public delegate void AfterSelectCalendarHandler(DateTime e);
    public delegate void AfterSelectCalculatorHandler(decimal e);

}
