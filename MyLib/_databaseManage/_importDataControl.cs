using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

namespace MyLib._databaseManage
{
    public partial class _importDataControl : UserControl
    {
        private ArrayList _fieldList = new ArrayList();
        private object[] _fieldType = new object[] { "text", "float,int", "date" };
        DataTable _textData = null;

        public _importDataControl()
        {
            InitializeComponent();
            //
            this.Load += new EventHandler(_importDataControl_Load);
            this._mapGrid._table_name = "import";
            this._mapGrid._addColumn("Select", 11, 10, 10);
            this._mapGrid._addColumn("Field (Text File)", 1, 100, 40, false, false, true);
            this._mapGrid._addColumn("Field (Table)", 10, 100, 40, true, false, true);
            this._mapGrid._addColumn("Field Type (Table)", 10, 100, 10, true, false, true);
            this._mapGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_mapGrid__cellComboBoxItem);
            this._mapGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_mapGrid__cellComboBoxGet);
        }

        string _mapGrid__cellComboBoxGet(object sender, int row, int column, string columnName, int select)
        {
            if (column == 2)
                return (select >= this._fieldList.Count) ? "" : this._fieldList[select].ToString();
            else
                return (select >= this._fieldType.Length) ? "" : this._fieldType[select].ToString();
        }

        object[] _mapGrid__cellComboBoxItem(object sender, int row, int column)
        {
            if (column == 2)
                return this._fieldList.ToArray();
            else
                return this._fieldType;
        }

        void _importDataControl_Load(object sender, EventArgs e)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            ArrayList __tableList = __myFrameWork._getTableFromDatabase(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseName);
            this._tableComboBox.Items.Clear();
            for (int __row = 0; __row < __tableList.Count; __row++)
            {
                this._tableComboBox.Items.Add(__tableList[__row].ToString());
            }
        }

        private void _selectFileButton1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog __file = new OpenFileDialog();
                __file.Title = "Select Text File";
                __file.Multiselect = false;
                __file.Filter = "All Supported File (*.txt,*.csv) | *.txt;*.csv";
                if (__file.ShowDialog() == DialogResult.OK)
                {
                    this._textFileTextBox.Text = __file.FileName.ToString();
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        public static DataTable _readFileToDataTable(string filename, string delimiter)
        {
            return _readFileToDataTable(filename, delimiter, null);
        }
        /// <summary>
        /// Read the file and return a DataTable
        /// </summary>
        /// <param name="filename">File to read</param>
        /// <param name="delimiter">Delimiting string</param>
        /// <param name="columnNames">Array of column names</param>
        /// <returns>Populated DataTable</returns>
        public static DataTable _readFileToDataTable(string filename, string delimiter, string[] columnNames)
        {
            //  Create the new table
            DataTable data = new DataTable();
            data.Locale = System.Globalization.CultureInfo.CurrentCulture;

            //  Check file
            if (!File.Exists(filename))
                throw new FileNotFoundException("File not found", filename);

            //  Process the file line by line
            string line;
            string[] lineData = new string[0];
            using (TextReader tr = new StreamReader(filename, Encoding.Default))
            {
                //  If column names were not passed, we'll read them from the file
                if (columnNames == null)
                {
                    //  Get the first line
                    //line = tr.ReadLine();
                    //if (string.IsNullOrEmpty(line))
                    //    throw new IOException("Could not read column names from file.");
                    //columnNames = line.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
                    line = tr.ReadLine().Replace("\n", "");
                    if (string.IsNullOrEmpty(line))
                        throw new IOException("Could not read column names from file.");
                    columnNames = line.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
                    lineData = tr.ReadToEnd().Replace("\n", "").Split('\r');
                }

                //  Add the columns to the data table
                foreach (string colName in columnNames)
                {
                    try
                    {
                        string colName2 = colName.Trim().Replace(" ", "");
                        if (colName2.Length > 0)
                        {
                            data.Columns.Add(colName2);
                        }
                    }
                    catch
                    {
                    }
                }

                //  Read the file
                string[] columns;
                for (int __iLine = 0; __iLine < lineData.Length; __iLine++)
                {
                    columns = lineData[__iLine].Split(new string[] { delimiter }, StringSplitOptions.None);
                    if (columns[0].Trim().Length > 0)
                    {
                        if (columns.Length != columnNames.Length)
                        {
                            string message = "Data row has {0} columns and {1} are defined by column names. {2} ";
                            throw new DataException(string.Format(message, columns.Length, columnNames.Length, lineData[__iLine]));
                        }
                        data.Rows.Add(columns);
                    }
                }
                //while ((line = tr.ReadLine()) != null)
                //{
                //    columns = line.Split(new string[] { delimiter }, StringSplitOptions.None);
                //    //  Ensure we have the same number of columns
                //    if (columns[0].Trim().Length > 0)
                //    {
                //        if (columns.Length != columnNames.Length)
                //        {
                //            string message = "Data row has {0} columns and {1} are defined by column names. {2} ";
                //            throw new DataException(string.Format(message, columns.Length, columnNames.Length, line));
                //        }
                //        data.Rows.Add(columns);
                //    }
                //}
            }
            return data;
        }

        private void _mapFieldButton_Click(object sender, EventArgs e)
        {
            try
            {
                //
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                this._fieldList = __myFrameWork._getFieldFromDatabase(MyLib._myGlobal._databaseConfig, MyLib._myGlobal._databaseName, this._tableComboBox.SelectedItem.ToString());
                this._fieldList.Insert(0, "<none>");
                //
                this._mapGrid._clear();
                this._textData = _readFileToDataTable(this._textFileTextBox.Text, "\t");
                for (int __column = 0; __column < _textData.Columns.Count; __column++)
                {
                    int __addr = this._mapGrid._addRow();
                    string __columnName = _textData.Columns[__column].ToString();
                    this._mapGrid._cellUpdate(__addr, 1, __columnName, false);
                    for (int __find = 1; __find < this._fieldList.Count; __find++)
                    {
                        string[] __fieldName = this._fieldList[__find].ToString().Split(',');
                        if (__columnName.ToLower().Equals(__fieldName[0].ToLower()))
                        {
                            this._mapGrid._cellUpdate(__addr, 0, 1, false);
                            this._mapGrid._cellUpdate(__addr, 2, __find, false);
                            if (__fieldName[1].ToLower().IndexOf("varchar") != -1)
                            {
                                this._mapGrid._cellUpdate(__addr, 3, 0, false);
                            }
                            else
                                if (__fieldName[1].ToLower().IndexOf("int") != -1 || __fieldName[1].ToLower().IndexOf("float") != -1)
                                {
                                    this._mapGrid._cellUpdate(__addr, 3, 1, false);
                                }
                                else
                                    if (__fieldName[1].ToLower().IndexOf("date") != -1)
                                    {
                                        this._mapGrid._cellUpdate(__addr, 3, 2, false);
                                    }
                            break;
                        }
                    }
                }
                this._mapGrid.Invalidate();
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void vistaButton2_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder __query = new StringBuilder();
                ArrayList __textColumnNumberSelect = new ArrayList();
                ArrayList __gridRowNumberSelect = new ArrayList();
                StringBuilder __fieldList = new StringBuilder();
                for (int __row = 0; __row < this._mapGrid._rowData.Count; __row++)
                {
                    int __select = (int)this._mapGrid._cellGet(__row, 0);
                    if (__select == 1)
                    {
                        int __selectField = (int)this._mapGrid._cellGet(__row, 2);
                        if (__selectField > 0)
                        {
                            __textColumnNumberSelect.Add(__row);
                            __gridRowNumberSelect.Add(__row);
                            if (__fieldList.Length > 0)
                            {
                                __fieldList.Append(",");
                            }
                            __fieldList.Append(this._fieldList[__selectField].ToString().Split(',')[0].ToString());
                        }
                    }
                }
                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                for (int __row = 0; __row < this._textData.Rows.Count; __row++)
                {
                    StringBuilder __dataList = new StringBuilder();
                    for (int __column = 0; __column < __textColumnNumberSelect.Count; __column++)
                    {
                        int __columnNumber = (int)__textColumnNumberSelect[__column];
                        int __columnType = (int)this._mapGrid._cellGet((int)__gridRowNumberSelect[__column], 3);
                        if (__column != 0)
                        {
                            __dataList.Append(",");
                        }
                        if (__columnType != 1 && __columnType != 2) // toe fix date null
                        {
                            __dataList.Append("\'");
                        }
                        string __data = this._textData.Rows[__row][__columnNumber].ToString().Split('\\')[0].ToString().Replace("\'", "\'\'");
                        StringBuilder __process = new StringBuilder();
                        for (int __loop = 0; __loop < __data.Length; __loop++)
                        {
                            if (__data[__loop] >= ' ')
                            {
                                __process.Append(__data[__loop]);
                            }
                        }
                        string __getData = __process.ToString();
                        if (__columnType == 2 && __getData.IndexOf('/') != -1)
                        {
                            // แปลงวันที่ dd/MM/yyyy
                            try
                            {
                                DateTime __resultDate = DateTime.Parse(__getData, new CultureInfo("th-TH"));
                                __getData = MyLib._myGlobal._convertDateToQuery(__resultDate);
                            }
                            catch
                            {
                            }
                        }

                        if (__columnType == 2 && (__getData.Length == 0 || __getData.ToLower() == "null")) // toe fix date null
                        {
                            __getData = "null";
                        }

                        if (__columnType == 1 && __getData.IndexOf('(') != -1)
                        {
                            // กรณีตัวเลขติดลบ แบบใส่วงเล็บ
                            __getData = "-" + __getData.Replace("(", "").Replace(")", "");
                        }

                        if (__columnType != 1 && __columnType != 2)
                        {
                            __dataList.Append(__getData.Trim());

                            __dataList.Append("\'");
                        }
                        else
                        {
                            // กรณีเป็น ตัวอักษร หรือวันที่                           
                            if (__columnType == 2)
                            {
                                if (__getData != "null") // toe fix date null
                                {
                                    __dataList.Append("\'");
                                }

                                __dataList.Append(__getData.Trim());

                                if (__getData != "null") // toe fix date null
                                {
                                    __dataList.Append("\'");
                                }
                            }
                            else
                            {
                                __dataList.Append(__getData.Trim());
                            }
                        }
                    }
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + this._tableComboBox.SelectedItem.ToString() + " (" + __fieldList.ToString() + ") values (" + __dataList + ")"));
                }
                __query.Append("</node>");
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                if (__result.Length != 0)
                {
                    MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Success");
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
