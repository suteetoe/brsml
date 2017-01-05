using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MyLib;

namespace SMLFastReport
{
    public partial class _queryControl : UserControl
    {
        public Font _defaultFont = new Font("Angsana New", 12, FontStyle.Regular);
        public Color _defaultFontColor = Color.Black;

        public _queryControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this.toolStrip2.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._setFontTextBox();
            this._fieldGridView.CellClick += new DataGridViewCellEventHandler(_fieldGridView_CellClick);
            this._fieldGridView.CellFormatting += new DataGridViewCellFormattingEventHandler(_fieldGridView_CellFormatting);
            this._fieldGridView.CurrentCellDirtyStateChanged += new EventHandler(_fieldGridView_CurrentCellDirtyStateChanged);
            this._fieldGridView.CellValueChanged += new DataGridViewCellEventHandler(_fieldGridView_CellValueChanged);
        }

        void _fieldGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                this._fieldGridView.EndEdit();
                string __text = MyLib._myResource._findResource(this._fieldGridView.Rows[e.RowIndex].Cells[4].Value.ToString(), false)._str;
                this._fieldGridView.Rows[e.RowIndex].Cells[5].Value = __text;
                this._fieldGridView.EndEdit();
            }
        }

        void _fieldGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this._fieldGridView.CurrentCell.ColumnIndex == 6)
            {
                this._fieldGridView.EndEdit();
                switch (this._fieldGridView.Rows[this._fieldGridView.CurrentCell.RowIndex].Cells[6].Value.ToString().ToLower())
                {
                    case "text":
                    case "date":
                        this._fieldGridView.Rows[this._fieldGridView.CurrentCell.RowIndex].Cells[12].Value = "Left";
                        break;
                    case "number":
                        this._fieldGridView.Rows[this._fieldGridView.CurrentCell.RowIndex].Cells[12].Value = "Right";
                        break;
                    //case "autonumber" :
                        
                    //    break;
                }
                this._fieldGridView.EndEdit();
            }
        }

        void _fieldGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 10)
            {
                if (this._fieldGridView.Rows[e.RowIndex].Cells[10].Value != null && this._fieldGridView.Rows[e.RowIndex].Cells[10].Value.ToString().Length > 0)
                {
                    TypeConverter __tc1 = TypeDescriptor.GetConverter(typeof(Font));
                    e.CellStyle.Font = (Font)__tc1.ConvertFromString(this._fieldGridView.Rows[e.RowIndex].Cells[10].Value.ToString());
                    if (this._fieldGridView.Rows[e.RowIndex].Cells[11].Value != null && this._fieldGridView.Rows[e.RowIndex].Cells[11].Value.ToString().Length > 0)
                    {
                        TypeConverter __tc2 = TypeDescriptor.GetConverter(typeof(Color));
                        e.CellStyle.BackColor = (Color)__tc2.ConvertFromString(this._fieldGridView.Rows[e.RowIndex].Cells[11].Value.ToString());
                        e.CellStyle.ForeColor = e.CellStyle.BackColor;
                    }
                }
            }
            if (e.ColumnIndex == 11)
            {
                if (this._fieldGridView.Rows[e.RowIndex].Cells[11].Value != null && this._fieldGridView.Rows[e.RowIndex].Cells[11].Value.ToString().Length > 0)
                {
                    TypeConverter __tc1 = TypeDescriptor.GetConverter(typeof(Color));
                    e.CellStyle.BackColor = (Color)__tc1.ConvertFromString(this._fieldGridView.Rows[e.RowIndex].Cells[11].Value.ToString());
                    e.CellStyle.ForeColor = e.CellStyle.BackColor;
                }
            }
        }

        void _fieldGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 10)
            {
                try
                {
                    FontDialog __fd = new FontDialog();
                    TypeConverter __tc1 = TypeDescriptor.GetConverter(typeof(Font));
                    TypeConverter __tc2 = TypeDescriptor.GetConverter(typeof(Color));
                    __fd.Font = (this._fieldGridView.Rows[e.RowIndex].Cells[10].Value == null || this._fieldGridView.Rows[e.RowIndex].Cells[10].Value.ToString().Length == 0) ? this._defaultFont : (Font)__tc1.ConvertFromString(this._fieldGridView.Rows[e.RowIndex].Cells[10].Value.ToString());
                    __fd.Color = (this._fieldGridView.Rows[e.RowIndex].Cells[11].Value == null || this._fieldGridView.Rows[e.RowIndex].Cells[11].Value.ToString().Length == 0) ? this._defaultFontColor : (Color)__tc2.ConvertFromString(this._fieldGridView.Rows[e.RowIndex].Cells[11].Value.ToString());
                    __fd.ShowColor = true;
                    __fd.ShowEffects = true;
                    __fd.ShowHelp = true;
                    __fd.MinSize = 9;
                    if (__fd.ShowDialog() == DialogResult.OK)
                    {
                        this._fieldGridView.Rows[e.RowIndex].Cells[10].Value = __tc1.ConvertToString(__fd.Font);
                        this._fieldGridView.Rows[e.RowIndex].Cells[11].Value = __tc2.ConvertToString(__fd.Color);
                    }
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                }
            }
        }

        void _queryControl_Click(object sender, EventArgs e)
        {
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<DataGridViewRow> __object = new List<DataGridViewRow>();
                List<string> __fieldName = new List<string>();
                List<string> __fontList = new List<string>();
                List<string> __fontColorList = new List<string>();
                List<string> __typeList = new List<string>();
                for (int __row = 0; __row < this._fieldGridView.Rows.Count; __row++)
                {
                    __fieldName.Add(this._fieldGridView.Rows[__row].Cells[3].Value.ToString());
                    __object.Add(this._fieldGridView.Rows[__row]);
                    __typeList.Add(this._fieldGridView.Rows[__row].Cells[5].Value.ToString());
                    __fontList.Add(this._fieldGridView.Rows[__row].Cells[10].Value.ToString());
                    __fontColorList.Add(this._fieldGridView.Rows[__row].Cells[11].Value.ToString());
                }
                _myFrameWork __myFrameWork = new _myFrameWork();
                string[] __columnName = __myFrameWork._queryColumnName(MyLib._myGlobal._databaseName, this._queryTextBox.Text).Split(',');
                if (__columnName.Length > 0)
                {
                    this._fieldGridView.Rows.Clear();
                    for (int __loop = 0; __loop < __columnName.Length; __loop++)
                    {
                        string __getColumnName = __columnName[__loop].ToString();
                        string __resourceName = (__getColumnName.IndexOf('.') == -1) ? this._tableNameTextBox.Text + "." + __getColumnName : __getColumnName;
                        _myResourceType __getResource = _myResource._findResource(__resourceName, false);
                        string __resourceFindName = __getResource._str;
                        if (__getResource._length > 50)
                        {
                            __getResource._length = 50;
                        }
                        Boolean __found = false;
                        for (int __row = 0; __row < __fieldName.Count; __row++)
                        {
                            if (__getColumnName.Equals(__fieldName[__row]))
                            {
                                this._fieldGridView.Rows.Add(__object[__row]);
                                __found = true;
                                break;
                            }
                        }
                        if (__found == false)
                        {
                            this._fieldGridView.Rows.Add(0, 0, (__getResource._length == 0) ? 10 : __getResource._length, __getColumnName, __resourceName, __resourceFindName, "Text", "", false, false, "", "", "Left", false);
                        }
                    }
                    this._fieldGridView.Invalidate();
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        public string _toString(object source)
        {
            if (source == null) return "";
            return source.ToString();
        }

        public _queryClass _xml()
        {
            List<float> __totalWidth = new List<float>();
            List<float> __margin = new List<float>();
            _queryClass __xml = new _queryClass();
            __xml._queryForGetField = this._queryTextBox.Text;
            __xml._query = this._queryProcessTextBox.Text;
            __xml._resourceTableName = this._tableNameTextBox.Text;
            __xml._relation = this._relationTextBox.Text;
            __xml._leftMargin = (float)MyLib._myGlobal._decimalPhase(this._leftMarginTextBox.Text.ToString());
            //
            TypeConverter __tc1 = TypeDescriptor.GetConverter(typeof(Font));
            __xml._defaultFont = __tc1.ConvertToString(this._defaultFont);
            TypeConverter __tc2 = TypeDescriptor.GetConverter(typeof(Color));
            __xml._defaultFontColor = __tc2.ConvertToString(this._defaultFontColor);
            //
            for (int __line = 0; __line < 10; __line++)
            {
                __totalWidth.Add(0);
                __margin.Add(0);
            }
            this._fieldGridView.EndEdit();
            for (int __row = 0; __row < this._fieldGridView.Rows.Count; __row++)
            {
                _fieldClass __field = new _fieldClass();
                DataGridViewRow __dataRow = this._fieldGridView.Rows[__row];
                int __column = 0;
                __field._line = (int)MyLib._myGlobal._decimalPhase(__dataRow.Cells[__column++].Value.ToString());
                __field._margin = (float)MyLib._myGlobal._decimalPhase(__dataRow.Cells[__column++].Value.ToString());
                __field._width = (float)MyLib._myGlobal._decimalPhase(__dataRow.Cells[__column++].Value.ToString());
                __field._fieldName = this._toString(__dataRow.Cells[__column++].Value);
                __field._resourceCode = this._toString(__dataRow.Cells[__column++].Value);
                __field._resourceName = this._toString(__dataRow.Cells[__column++].Value);
                __field._type = this._toString(__dataRow.Cells[__column++].Value);
                __field._format = this._toString(__dataRow.Cells[__column++].Value);
                __field._printTotal = (bool)__dataRow.Cells[__column++].Value;
                __field._hide = (bool)__dataRow.Cells[__column++].Value;
                __field._fontName = this._toString(__dataRow.Cells[__column++].Value);
                __field._fontColor = this._toString(__dataRow.Cells[__column++].Value);
                __field._align = this._toString(__dataRow.Cells[__column++].Value);
                __field._breakLine = (bool)__dataRow.Cells[__column++].Value;
                if (__field._hide)
                {
                    __field._width = 0;
                }
                __field._widthPersent = 0;
                __totalWidth[__field._line] += __field._width;
                if (__field._margin > __margin[__field._line])
                {
                    __margin[__field._line] = __field._margin;
                }
                __xml._field.Add(__field);
            }
            for (int __line = 0; __line < 10; __line++)
            {
                // คำนวณความกว้างเป็น %
                for (int __column = 0; __column < __xml._field.Count; __column++)
                {
                    if (__xml._field[__column]._line == __line && __xml._field[__column]._width > 0)
                    {
                        __xml._field[__column]._widthPersent = (100 * __xml._field[__column]._width) / (__totalWidth[__line] + __margin[__line]);
                    }
                }
            }
            return __xml;
        }

        public void _setXML(_queryClass __xml)
        {
            this._fieldGridView.Rows.Clear();
            this._queryTextBox.Text = __xml._queryForGetField;
            this._relationTextBox.Text = __xml._relation;
            this._leftMarginTextBox.Text = __xml._leftMargin.ToString();
            this._queryProcessTextBox.Text = __xml._query;
            this._tableNameTextBox.Text = __xml._resourceTableName;
            try
            {
                TypeConverter __tc1 = TypeDescriptor.GetConverter(typeof(Font));
                this._defaultFont = (Font)__tc1.ConvertFromString(__xml._defaultFont);
            }
            catch
            {
            }
            try
            {
                TypeConverter __tc2 = TypeDescriptor.GetConverter(typeof(Color));
                this._defaultFontColor = (Color)__tc2.ConvertFromString(__xml._defaultFontColor);
            }
            catch
            {
            }
            this._setFontTextBox();
            if (__xml._field.Count > 0)
            {
                foreach (_fieldClass __field in __xml._field)
                {
                    this._fieldGridView.Rows.Add(__field._line, __field._margin, __field._width, __field._fieldName, __field._resourceCode, __field._resourceName, __field._type, __field._format, __field._printTotal, __field._hide, __field._fontName, __field._fontColor, __field._align, __field._breakLine);
                }
                _fieldGridView.Invalidate();
            }
        }

        private void _upButton_Click(object sender, EventArgs e)
        {
            if (this._fieldGridView.SelectedCells.Count > 0)
            {
                int __rowSelected = this._fieldGridView.SelectedCells[0].RowIndex;
                int __cellSelected = this._fieldGridView.SelectedCells[0].ColumnIndex;
                if (__rowSelected > 0)
                {
                    DataGridViewRow __dr1 = (DataGridViewRow)this._fieldGridView.Rows[__rowSelected].Clone();
                    int __i = 0;
                    foreach (DataGridViewCell __cell in this._fieldGridView.Rows[__rowSelected].Cells)
                    {
                        __dr1.Cells[__i].Value = __cell.Value;
                        __i++;
                    }
                    this._fieldGridView.Rows.RemoveAt(__rowSelected);
                    this._fieldGridView.Rows.Insert(__rowSelected - 1, __dr1);
                    this._fieldGridView.ClearSelection();
                    this._fieldGridView.Rows[__rowSelected - 1].Selected = true;
                }
            }
        }

        private void _downButton_Click(object sender, EventArgs e)
        {
            if (this._fieldGridView.SelectedCells.Count > 0)
            {
                int __rowSelected = this._fieldGridView.SelectedCells[0].RowIndex;
                int __cellSelected = this._fieldGridView.SelectedCells[0].ColumnIndex;
                if (__rowSelected < this._fieldGridView.Rows.Count - 1)
                {
                    DataGridViewRow __dr1 = (DataGridViewRow)this._fieldGridView.Rows[__rowSelected].Clone();
                    int __i = 0;
                    foreach (DataGridViewCell __cell in this._fieldGridView.Rows[__rowSelected].Cells)
                    {
                        __dr1.Cells[__i].Value = __cell.Value;
                        __i++;
                    }
                    this._fieldGridView.Rows.RemoveAt(__rowSelected);
                    this._fieldGridView.Rows.Insert(__rowSelected + 1, __dr1);
                    this._fieldGridView.ClearSelection();
                    this._fieldGridView.Rows[__rowSelected + 1].Selected = true;
                }
            }
        }

        private void _selectFontButton_Click(object sender, EventArgs e)
        {
            try
            {
                FontDialog __fd = new FontDialog();
                __fd.Font = this._defaultFont;
                __fd.Color = this._defaultFontColor;
                __fd.ShowColor = true;
                __fd.ShowEffects = true;
                __fd.ShowHelp = true;
                __fd.MinSize = 9;
                if (__fd.ShowDialog() == DialogResult.OK)
                {
                    this._defaultFont = __fd.Font;
                    this._defaultFontColor = __fd.Color;
                    this._setFontTextBox();
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _setFontTextBox()
        {
            try
            {
                TypeConverter __tc = TypeDescriptor.GetConverter(typeof(Font));
                this._fontTextBox.Font = this._defaultFont;
                this._fontTextBox.ForeColor = this._defaultFontColor;
                this._fontTextBox.Text = __tc.ConvertToString(this._defaultFont);//;this._defaultFont.Name + "," + this._defaultFont.Size;
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _removeFontButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._fieldGridView.Rows.Count; __row++)
            {
                this._fieldGridView.Rows[__row].Cells[10].Value = "";
                this._fieldGridView.Rows[__row].Cells[11].Value = "";
            }
            this._fieldGridView.Invalidate();
        }

        private void _clearButton_Click(object sender, EventArgs e)
        {
            this._fieldGridView.EndEdit();
            this._fieldGridView.Rows.Clear();
        }
    }
}
