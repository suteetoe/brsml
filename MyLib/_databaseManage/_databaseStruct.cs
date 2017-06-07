using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;

namespace MyLib._databaseManage
{
    public partial class _databaseStruct : UserControl
    {
        XmlDocument _xDoc = new XmlDocument();
        Boolean _loadSuccess = false;
        string _tableNameSelected = "";

        public _databaseStruct()
        {
            InitializeComponent();
            //
            //
            _myGridTable._isEdit = false;
            _myGridTable._addColumn("table name", 1, 100, 40);
            _myGridTable._addColumn("name thai", 1, 100, 30);
            _myGridTable._addColumn("name english", 1, 100, 30);
            _myGridTable._mouseClick += new MouseClickHandler(_myGridTable__mouseClick);
            //
            _myGridField._isEdit = false;
            _myGridField._addColumn("field name", 1, 100, 20);
            _myGridField._addColumn("name thai", 1, 100, 20);
            _myGridField._addColumn("name english", 1, 100, 20);
            _myGridField._addColumn("type", 1, 100, 7);
            _myGridField._addColumn("length", 1, 100, 7);
            _myGridField._addColumn("indentity", 1, 100, 7);
            _myGridField._addColumn("allow_null", 1, 100, 10);
            _myGridField._addColumn("resource_only", 1, 100, 9);
            _myGridField._mouseDoubleClick += new MouseDoubleClickHandler(_myGridField__mouseDoubleClick);
            _myGridField._beforeDisplayRendering += new BeforeDisplayRenderRowEventHandler(_myGridField__beforeDisplayRendering);
            //
            _myGridIndex._isEdit = false;
            _myGridIndex._addColumn("index name", 1, 100, 30);
            _myGridIndex._addColumn("field", 1, 100, 30);
            _myGridIndex._addColumn("custer", 1, 100, 20);
            _myGridIndex._addColumn("unique", 1, 100, 20);
            //
            this.Invalidate();
            // getTable
        }

        BeforeDisplayRowReturn _myGridField__beforeDisplayRendering(_myGrid sender, int row, int columnNumber, string columnName, BeforeDisplayRowReturn senderRow, _myGrid._columnType columnType, System.Collections.ArrayList rowData, Graphics e)
        {
            MyLib.BeforeDisplayRowReturn __result = senderRow;
            try
            {
                if (sender._cellGet(row, sender._findColumnByName("resource_only")).ToString().ToLower().Equals("true"))
                {
                    __result.newColor = Color.Red;
                }
            }
            catch
            {
            }
            return __result;
        }

        void _myGridField__mouseDoubleClick(object sender, GridCellEventArgs e)
        {
            MyLib._myGrid __sender = (MyLib._myGrid)sender;
            if (this._fieldDoubleClick != null)
            {
                string __selectFieldName = __sender._cellGet(e._row, 0).ToString();
                this._fieldDoubleClick(this, _tableNameSelected + "." + __selectFieldName);
            }
        }

        public event DatabaseStructLoadSuccessHandler _databaseStructLoadSuccess;
        public event FieldDoubleClickHandler _fieldDoubleClick;

        private void _databaseStruct_Load(object sender, EventArgs e)
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                if (this._loadSuccess == false)
                {
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    DataSet __myDataSet = __myFrameWork._dataStruct(MyLib._myGlobal._databaseStructFileName);
                    _xDoc.LoadXml(__myDataSet.GetXml());
                    _xDoc.DocumentElement.Normalize();
                    XmlElement __xRoot = _xDoc.DocumentElement;
                    XmlNodeList __xReader = __xRoot.GetElementsByTagName("table");
                    DataTable __dt = new DataTable();
                    __dt.Columns.Add("table name", typeof(string));
                    __dt.Columns.Add("name thai", typeof(string));
                    __dt.Columns.Add("name english", typeof(string));
                    __dt.DefaultView.Sort = "table name";
                    for (int __table = 0; __table < __xReader.Count; __table++)
                    {
                        XmlNode xFirstNode = __xReader.Item(__table);
                        if (xFirstNode.NodeType == XmlNodeType.Element)
                        {
                            XmlElement xTable = (XmlElement)xFirstNode;
                            string tableName = xTable.GetAttribute("name");
                            string nameThai = xTable.GetAttribute("thai");
                            string nameEnglish = xTable.GetAttribute("eng");
                            __dt.Rows.Add(tableName, nameThai, nameEnglish);
                        }
                    } // for
                    _myGridTable._loadFromDataTable(__dt);
                    if (this._databaseStructLoadSuccess != null)
                    {
                        this._databaseStructLoadSuccess(this);
                    }
                    this._loadSuccess = true;
                }
            }
        }

        public void _displayDetail(string getTableName)
        {
            this._tableNameSelected = getTableName;
            XmlElement __xRoot = _xDoc.DocumentElement;
            XmlNodeList __xReader = __xRoot.GetElementsByTagName("table");
            for (int __table = 0; __table < __xReader.Count; __table++)
            {
                XmlNode __xFirstNode = __xReader.Item(__table);
                if (__xFirstNode.NodeType == XmlNodeType.Element)
                {
                    XmlElement __xTable = (XmlElement)__xFirstNode;
                    if (getTableName.CompareTo(__xTable.GetAttribute("name")) == 0)
                    {
                        // get field
                        DataTable __dt = new DataTable();
                        __dt.Columns.Add("field name", typeof(string));
                        __dt.Columns.Add("name thai", typeof(string));
                        __dt.Columns.Add("name english", typeof(string));
                        __dt.Columns.Add("type", typeof(string));
                        __dt.Columns.Add("length", typeof(string));
                        __dt.Columns.Add("indentity", typeof(string));
                        __dt.Columns.Add("allow_null", typeof(string));
                        __dt.Columns.Add("resource_only", typeof(string));
                        __dt.DefaultView.Sort = "field name";
                        XmlNodeList __xField = __xTable.GetElementsByTagName("field");
                        for (int __field = 0; __field < __xField.Count; __field++)
                        {
                            XmlNode __xReadNode = __xField.Item(__field);
                            if (__xReadNode != null)
                            {
                                if (__xReadNode.NodeType == XmlNodeType.Element)
                                {
                                    XmlElement __xGetField = (XmlElement)__xReadNode;
                                    __dt.Rows.Add(__xGetField.GetAttribute("name"), __xGetField.GetAttribute("thai"), __xGetField.GetAttribute("eng"), __xGetField.GetAttribute("type"), __xGetField.GetAttribute("length"), __xGetField.GetAttribute("indentity"), __xGetField.GetAttribute("allow_null"), __xGetField.GetAttribute("resource_only"));
                                }
                            }
                        } // for
                        _myGridField._loadFromDataTable(__dt);
                        //
                        XmlNodeList __xIndex = __xTable.GetElementsByTagName("index");
                        for (int __index = 0; __index < __xIndex.Count; __index++)
                        {
                            XmlNode __xReadNode = __xIndex.Item(__index);
                            if (__xReadNode != null)
                            {
                                if (__xReadNode.NodeType == XmlNodeType.Element)
                                {
                                    XmlElement __xGetField = (XmlElement)__xReadNode;
                                    _myGridIndex._addRow();
                                    int __addr = _myGridIndex._rowData.Count - 1;
                                    _myGridIndex._cellUpdate(__addr, 0, __xGetField.GetAttribute("index_name"), false);
                                    _myGridIndex._cellUpdate(__addr, 1, __xGetField.GetAttribute("field"), false);
                                    _myGridIndex._cellUpdate(__addr, 2, __xGetField.GetAttribute("custer"), false);
                                    _myGridIndex._cellUpdate(__addr, 3, __xGetField.GetAttribute("unique"), false);
                                }
                            }
                        } // for
                        break;
                    }
                }
            } // for
        }

        void _myGridTable__mouseClick(object sender, GridCellEventArgs e)
        {
            _myGridField._clear();
            _myGridIndex._clear();
            string __getTableName = _myGridTable._cellGet(e._row, 0).ToString();
            // get field
            _displayDetail(__getTableName);
        }

        private void _exitButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _exportAllButton_Click(object sender, EventArgs e)
        {
            Form __form = new Form();
            MyLib._myGrid __gridExport = new _myGrid();

            __gridExport._isEdit = false;
            __gridExport._addColumn("table name", 1, 100, 20);
            __gridExport._addColumn("table name thai", 1, 100, 30);
            __gridExport._addColumn("table name english", 1, 100, 30);
            __gridExport._addColumn("field name", 1, 100, 20);
            __gridExport._addColumn("name thai", 1, 100, 20);
            __gridExport._addColumn("name english", 1, 100, 20);
            __gridExport._addColumn("type", 1, 100, 7);
            __gridExport._addColumn("length", 1, 100, 7);
            __gridExport._addColumn("indentity", 1, 100, 7);
            __gridExport._addColumn("allow_null", 1, 100, 10);
            __gridExport._calcPersentWidthToScatter();

            // get field
            DataTable __dt = new DataTable();
            __dt.Columns.Add("table name", typeof(string));
            __dt.Columns.Add("table name thai", typeof(string));
            __dt.Columns.Add("table name english", typeof(string));
            __dt.Columns.Add("field name", typeof(string));
            __dt.Columns.Add("name thai", typeof(string));
            __dt.Columns.Add("name english", typeof(string));
            __dt.Columns.Add("type", typeof(string));
            __dt.Columns.Add("length", typeof(string));
            __dt.Columns.Add("indentity", typeof(string));
            __dt.Columns.Add("allow_null", typeof(string));
            __dt.Columns.Add("resource_only", typeof(string));
            __dt.DefaultView.Sort = "table name";

            XmlElement __xRoot = _xDoc.DocumentElement;
            XmlNodeList __xReader = __xRoot.GetElementsByTagName("table");
            for (int __table = 0; __table < __xReader.Count; __table++)
            {
                XmlNode __xFirstNode = __xReader.Item(__table);
                if (__xFirstNode.NodeType == XmlNodeType.Element)
                {
                    XmlElement __xTable = (XmlElement)__xFirstNode;

                    string tableName = __xTable.GetAttribute("name");
                    string tableNameThai = __xTable.GetAttribute("thai");
                    string tableNameEnglish = __xTable.GetAttribute("eng");

                    //if (getTableName.CompareTo(__xTable.GetAttribute("name")) == 0)
                    {

                        XmlNodeList __xField = __xTable.GetElementsByTagName("field");
                        for (int __field = 0; __field < __xField.Count; __field++)
                        {
                            XmlNode __xReadNode = __xField.Item(__field);
                            if (__xReadNode != null)
                            {
                                if (__xReadNode.NodeType == XmlNodeType.Element)
                                {
                                    XmlElement __xGetField = (XmlElement)__xReadNode;
                                    if (__xGetField.GetAttribute("resource_only") == "false")
                                        __dt.Rows.Add(tableName, tableNameThai, tableNameEnglish, __xGetField.GetAttribute("name"), __xGetField.GetAttribute("thai"), __xGetField.GetAttribute("eng"), __xGetField.GetAttribute("type"), __xGetField.GetAttribute("length"), __xGetField.GetAttribute("indentity"), __xGetField.GetAttribute("allow_null"), __xGetField.GetAttribute("resource_only"));
                                }
                            }
                        } // for

                        //
                        /*
                        XmlNodeList __xIndex = __xTable.GetElementsByTagName("index");
                        for (int __index = 0; __index < __xIndex.Count; __index++)
                        {
                            XmlNode __xReadNode = __xIndex.Item(__index);
                            if (__xReadNode != null)
                            {
                                if (__xReadNode.NodeType == XmlNodeType.Element)
                                {
                                    XmlElement __xGetField = (XmlElement)__xReadNode;
                                    int __addr = __gridExport._addRow();
                                    // __gridExport._rowData.Count - 1;
                                    __gridExport._cellUpdate(__addr, 0, __xGetField.GetAttribute("index_name"), false);
                                    __gridExport._cellUpdate(__addr, 1, __xGetField.GetAttribute("field"), false);
                                    __gridExport._cellUpdate(__addr, 2, __xGetField.GetAttribute("custer"), false);
                                    __gridExport._cellUpdate(__addr, 3, __xGetField.GetAttribute("unique"), false);
                                }
                            }
                        } // for
                        */
                        //break;
                    }
                }
            } // for

            __gridExport._loadFromDataTable(__dt);
            __form.Controls.Add(__gridExport);
            __gridExport.Dock = DockStyle.Fill;
            __form.WindowState = FormWindowState.Maximized;

            __form.ShowDialog(this);

        }
    }

    public delegate void DatabaseStructLoadSuccessHandler(object sender);
    public delegate void FieldDoubleClickHandler(object sender, string selectFieldName);

}
