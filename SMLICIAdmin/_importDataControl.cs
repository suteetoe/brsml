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

namespace SMLICIAdmin
{
    public partial class _importICIDataControl : UserControl
    {
        private ArrayList _fieldList = new ArrayList();
        private object[] _fieldType = new object[] { "text", "float,int", "date" };
        DataTable _textData = null;

        public _importICIDataControl()
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
                if ( __tableList[__row].ToString().ToLower().Equals("ic_inventory")
                    || __tableList[__row].ToString().ToLower().Equals("ic_unit") || __tableList[__row].ToString().ToLower().Equals("ic_category")
                    || __tableList[__row].ToString().ToLower().Equals("ic_brand")
                     || __tableList[__row].ToString().ToLower().Equals("ic_warehouse")
                    || __tableList[__row].ToString().ToLower().Equals("ic_color")
                     || __tableList[__row].ToString().ToLower().Equals("ic_group")
                     || __tableList[__row].ToString().ToLower().Equals("ic_dimension")
                    )
                {
                    this._tableComboBox.Items.Add(__tableList[__row].ToString());
                }
                
            }
        }

        private void _selectFileButton1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog __file = new OpenFileDialog();
                __file.Title = "Select Text File";
                __file.Multiselect = false;
                //__file.Filter = "All Supported File text cvs Excel XML SpreadSheet (*.txt,*.csv,*.xml) | *.txt;*.csv,*.xml";
               // __file.Filter =  "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                __file.Filter = "xml files (*.xml)|*.xml";
                if (__file.ShowDialog() == DialogResult.OK)
                {
                    
                    this._textFileTextBox.Text = __file.FileName.ToString();
                    FileDialog __exten = (FileDialog)__file;
                    String [] __xname =__exten.FileNames;
                //  this.dataGridView1.DataSource =  _buttonProcessk();
                    
                    //+		((System.Windows.Forms.FileDialog)(__file)).FilterExtensions	{string[1]}	string[]

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
            using (TextReader tr = new StreamReader(filename, Encoding.Default))
            {
                //  If column names were not passed, we'll read them from the file
                if (columnNames == null)
                {
                    //  Get the first line
                    line = tr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        throw new IOException("Could not read column names from file.");
                    columnNames = line.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
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
                while ((line = tr.ReadLine()) != null)
                {
                    columns = line.Split(new string[] { delimiter }, StringSplitOptions.None);
                    //  Ensure we have the same number of columns
                    if (columns[0].Trim().Length > 0)
                    {
                        if (columns.Length != columnNames.Length)
                        {
                            string message = "Data row has {0} columns and {1} are defined by column names.";
                            throw new DataException(string.Format(message, columns.Length, columnNames.Length));
                        }
                        data.Rows.Add(columns);
                    }
                }
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
                this._textData = _buttonProcessk();//_readFileToDataTable(this._textFileTextBox.Text, "\t");
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
                for (int __row = 1; __row < this._textData.Rows.Count; __row++)
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
                        if (__columnType != 1)
                        {
                            __dataList.Append("\'");
                        }
                        string __getData = this._textData.Rows[__row][__columnNumber].ToString().Split('\\')[0].ToString().Replace("\'", "\'\'");
                        __getData = __getData.Replace("  ", "-");
                        __getData = __getData.Replace("_", "-").Trim(); 
                       
                        if (__columnType != 1)
                        {
                            __dataList.Append(__getData);
                            __dataList.Append("\'");
                        }
                        else
                        {
                            __dataList.Append(__getData);
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
        private int _getAttributeToInt(XmlNode node, XmlAttributeCollection attribute, string name)
        {
            if (attribute[name] != null)
            {
                return Int32.Parse(node.Attributes[name].Value);
            }
            return 0;
        }

        private DataTable _buttonProcessk()
        {
            DataTable __dataTable = new DataTable("Data");
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
                        __rowIndex++;
                    }
                }
                //
                __dataTable = new DataTable("Data");
                for (int __column = 0; __column < __maxColumn; __column++)
                {
                    // __dataTable.Columns.Add(Convert.ToChar(__column + 65).ToString(), Type.GetType("System.String"));
                    __dataTable.Columns.Add(__column.ToString(), Type.GetType("System.String"));
                }
                for (int __row = 0; __row < __maxRow; __row++)
                {
                    DataRow __newRow = __dataTable.NewRow();
                    for (int __column = 0; __column < __maxColumn; __column++)
                    {
                        __newRow[__column] = ((ArrayList)__rowArrayList[__row])[__column].ToString();
                    }
                    __dataTable.Rows.Add(__newRow);
                }
                
              //  this._dataGridView.DataSource = __dataTable;
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.StackTrace.ToString());
            }
            return __dataTable;
        }
        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _bntPreview_Click(object sender, EventArgs e)
        {
            _previewXml __xpreview = new _previewXml();
            __xpreview.dataGridView1.DataSource = this._buttonProcessk();
            __xpreview.ShowDialog();
        }
    }
}
