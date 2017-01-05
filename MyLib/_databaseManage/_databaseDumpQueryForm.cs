using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace MyLib._databaseManage
{
    public partial class _databaseDumpQueryForm : Form
    {
        XmlDocument _xDoc = new XmlDocument();
        Boolean _loadSuccess = false;
        string _tableNameSelected = "";

        public _databaseDumpQueryForm()
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
            _myGridField._addColumn("select", 11, 10, 10);
            _myGridField._addColumn("field name", 1, 100, 20);
            _myGridField._addColumn("name thai", 1, 100, 20);
            _myGridField._addColumn("name english", 1, 100, 20);
            _myGridField._addColumn("type", 1, 100, 7);
            _myGridField._addColumn("length", 1, 100, 7);
            _myGridField._addColumn("indentity", 1, 100, 7);
            _myGridField._addColumn("allow_null", 1, 100, 10);
            _myGridField._addColumn("resource_only", 1, 100, 9);
            //
            this.Invalidate();

            this.Load += _databaseDumpQueryForm_Load;
        }

        private void _databaseDumpQueryForm_Load(object sender, EventArgs e)
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
                    /*if (this._databaseStructLoadSuccess != null)
                    {
                        this._databaseStructLoadSuccess(this);
                    }*/
                    this._loadSuccess = true;
                }
            }
        }

        void _myGridTable__mouseClick(object sender, GridCellEventArgs e)
        {
            _myGridField._clear();
            string __getTableName = _myGridTable._cellGet(e._row, 0).ToString();
            // get field
            _displayDetail(__getTableName);
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
                                    string __getResource = __xGetField.GetAttribute("resource_only").ToLower();
                                    if (__getResource.Equals("false"))
                                    {
                                        __dt.Rows.Add(__xGetField.GetAttribute("name"), __xGetField.GetAttribute("thai"), __xGetField.GetAttribute("eng"), __xGetField.GetAttribute("type"), __xGetField.GetAttribute("length"), __xGetField.GetAttribute("indentity"), __xGetField.GetAttribute("allow_null"), __xGetField.GetAttribute("resource_only"));
                                    }
                                }
                            }
                        } // for
                        _myGridField._loadFromDataTable(__dt);
                        //
                        break;
                    }
                }
            } // for
        }

        private void _genButton_Click(object sender, EventArgs e)
        {
            _gen(false);
        }

        void _gen(bool savetoFile)
        {
            string lastQuery = "";
            try
            {


                if (_myGridTable._selectRow > 0)
                {
                    string __tableName = _myGridTable._cellGet(_myGridTable._selectRow, 0).ToString();
                    MyLib._myFrameWork __myFrameWork = new _myFrameWork();

                    // this._progressTextStr = "Backup Table " + (__loop + 1) + "/" + (__getTableList.Count) + " : [" + MyLib._myGlobal._databaseName + "." + __tableName + "]";
                    // this._progressBarTableValue = (((__loop + 1) * 100) / (__getTableList.Count));
                    // this._progressRecordTextStr = "";
                    //this._progressBarRecordValue = 0;

                    bool __foundBytea = false;

                    if (__tableName.Equals("formdesign") || 2 == 2)
                    {
                        DataTable __countDataTable = __myFrameWork._queryShort("select count(*) as xcount from " + __tableName).Tables[0];
                        int __totalRecord = (int)MyLib._myGlobal._decimalPhase(__countDataTable.Rows[0][0].ToString());
                        int __offset = 0;
                        int __count = 0;
                        int __limit = 1000;
                        //DataRow[] __fieldList = __dt.Select("tablename=\'" + __tableName + "\'");
                        StringBuilder __querySelectField = new StringBuilder();

                        // for field
                        for (int __field = 0; __field < _myGridField._rowData.Count; __field++)
                        {
                            if (_myGridField._cellGet(__field, 0).ToString().Equals("1"))
                            {
                                if (__querySelectField.Length > 0)
                                {
                                    __querySelectField.Append(",");
                                }
                                string __fieldName = _myGridField._cellGet(__field, "field name").ToString(); //__fieldList[__field]["fieldname"].ToString();
                                string __fieldType = _myGridField._cellGet(__field, "type").ToString(); //__fieldList[__field]["type"].ToString();
                                if (__fieldType.Equals("image"))
                                {
                                    __querySelectField.Append("encode(" + __fieldName + ",'base64') as " + __fieldName);
                                    __foundBytea = true;
                                }
                                else
                                {
                                    __querySelectField.Append(__fieldName);
                                }
                            }
                        }

                        if (__foundBytea == true)
                        {
                            __limit = 5;
                        }

                        while (__count < __totalRecord)
                        {
                            lastQuery = "select " + __querySelectField.ToString() + " from " + __tableName + "  order by roworder offset " + __offset.ToString() + " limit " + __limit.ToString();

                            DataTable __data = __myFrameWork._queryShort("select " + __querySelectField.ToString() + " from " + __tableName + "  order by roworder offset " + __offset.ToString() + " limit " + __limit.ToString()).Tables[0];
                            __offset += __limit;
                            __count += __limit;
                            StringBuilder __query = new StringBuilder();
                            for (int __record = 0; __record < __data.Rows.Count; __record++)
                            {
                                StringBuilder __queryField = new StringBuilder();
                                StringBuilder __queryData = new StringBuilder();
                                for (int __field = 0; __field < _myGridField._rowData.Count; __field++)
                                {
                                    if (_myGridField._cellGet(__field, 0).ToString().Equals("1"))
                                    {
                                        if (__queryField.Length > 0)
                                        {
                                            __queryField.Append(",");
                                            __queryData.Append(",");
                                        }
                                        string __fieldName = _myGridField._cellGet(__field, "field name").ToString();// __fieldList[__field]["fieldname"].ToString();
                                        __queryField.Append(__fieldName);
                                        string __fieldType = _myGridField._cellGet(__field, "type").ToString();  //__fieldList[__field]["type"].ToString();
                                        if (__fieldType.Equals("image"))
                                        {
                                            __queryData.Append("decode(\'" + __data.Rows[__record][__fieldName].ToString() + "\','base64')");
                                        }
                                        else if (__fieldType.Equals("smalldatetime") || __fieldType.Equals("datetime"))
                                        {
                                            string __getDateValue = __data.Rows[__record][__fieldName].ToString();
                                            __queryData.Append((__getDateValue.Length > 0) ? "\'" + __getDateValue + "\'" : "null");
                                        }
                                        else
                                        {
                                            if (__fieldType.Equals("int") || __fieldType.Equals("smallint") || __fieldType.Equals("tinyint") || __fieldType.Equals("float"))
                                            {
                                                string __getNumberValue = __data.Rows[__record][__fieldName].ToString();
                                                __queryData.Append((__getNumberValue.Length > 0) ? __getNumberValue : "0");
                                            }
                                            else
                                            {
                                                __queryData.Append("\'" + __data.Rows[__record][__fieldName].ToString().Replace("'", "''") + "\'");
                                            }
                                        }
                                    }
                                }
                                __query.Append("insert into " + __tableName + " (" + __queryField.ToString() + ") values (" + __queryData.ToString() + ");\r\n");
                            }


                            this._queryResult.Text = __query.ToString();
                            /*
                            byte[] __array = Encoding.UTF8.GetBytes(__query.ToString());
                            __gz.Write(__array, 0, __array.Length);
                            int __countProgress = __count;
                            if (__countProgress > __totalRecord)
                            {
                                __countProgress = __totalRecord;
                            }
                            this._progressRecordTextStr = __countProgress.ToString() + "/" + __totalRecord.ToString();
                            this._progressBarRecordValue = (((__countProgress) * 100) / (__totalRecord));*/


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n" + lastQuery);
            }
        }

        private void _selectAll_Click(object sender, EventArgs e)
        {
            for (int __field = 0; __field < _myGridField._rowData.Count; __field++)
            {
                _myGridField._cellUpdate(__field, 0, 1, true);
            }
            _myGridField.Invalidate();

        }

        private void _selectNone_Click(object sender, EventArgs e)
        {
            for (int __field = 0; __field < _myGridField._rowData.Count; __field++)
            {
                _myGridField._cellUpdate(__field, 0, 0, true);
            }
            _myGridField.Invalidate();
        }

        private void _saveGzButton_Click(object sender, EventArgs e)
        {
            _genAndSave();
        }

        void _genAndSave()
        {
            string lastQuery = "";
            try
            {
                string __tableName = _myGridTable._cellGet(_myGridTable._selectRow, 0).ToString();

                FileStream __f2 = new FileStream(@"C:\\smlsoft" + "/" + __tableName + "-" + String.Format("{0:yy-MM-dd-HH-mm}" + ".gz", DateTime.Now), FileMode.Create);
                GZipStream __gz = new GZipStream(__f2, CompressionMode.Compress, false);

                if (_myGridTable._selectRow > 0)
                {
                    //string __tableName = _myGridTable._cellGet(_myGridTable._selectRow, 0).ToString();
                    MyLib._myFrameWork __myFrameWork = new _myFrameWork();

                    // this._progressTextStr = "Backup Table " + (__loop + 1) + "/" + (__getTableList.Count) + " : [" + MyLib._myGlobal._databaseName + "." + __tableName + "]";
                    // this._progressBarTableValue = (((__loop + 1) * 100) / (__getTableList.Count));
                    // this._progressRecordTextStr = "";
                    //this._progressBarRecordValue = 0;

                    bool __foundBytea = false;

                    if (__tableName.Equals("formdesign") || 2 == 2)
                    {
                        DataTable __countDataTable = __myFrameWork._queryShort("select count(*) as xcount from " + __tableName).Tables[0];
                        int __totalRecord = (int)MyLib._myGlobal._decimalPhase(__countDataTable.Rows[0][0].ToString());
                        int __offset = 0;
                        int __count = 0;
                        int __limit = 1000;
                        //DataRow[] __fieldList = __dt.Select("tablename=\'" + __tableName + "\'");
                        StringBuilder __querySelectField = new StringBuilder();

                        // for field
                        for (int __field = 0; __field < _myGridField._rowData.Count; __field++)
                        {
                            if (_myGridField._cellGet(__field, 0).ToString().Equals("1"))
                            {
                                if (__querySelectField.Length > 0)
                                {
                                    __querySelectField.Append(",");
                                }
                                string __fieldName = _myGridField._cellGet(__field, "field name").ToString(); //__fieldList[__field]["fieldname"].ToString();
                                string __fieldType = _myGridField._cellGet(__field, "type").ToString(); //__fieldList[__field]["type"].ToString();
                                if (__fieldType.Equals("image"))
                                {
                                    __querySelectField.Append("encode(" + __fieldName + ",'base64') as " + __fieldName);
                                    __foundBytea = true;
                                }
                                else
                                {
                                    __querySelectField.Append(__fieldName);
                                }
                            }
                        }

                        if (__foundBytea == true)
                        {
                            __limit = 5;
                        }

                        while (__count < __totalRecord)
                        {
                            lastQuery = "select " + __querySelectField.ToString() + " from " + __tableName + "  order by roworder offset " + __offset.ToString() + " limit " + __limit.ToString();

                            DataTable __data = __myFrameWork._queryShort("select " + __querySelectField.ToString() + " from " + __tableName + "  order by roworder offset " + __offset.ToString() + " limit " + __limit.ToString()).Tables[0];
                            __offset += __limit;
                            __count += __limit;
                            StringBuilder __query = new StringBuilder();
                            for (int __record = 0; __record < __data.Rows.Count; __record++)
                            {
                                StringBuilder __queryField = new StringBuilder();
                                StringBuilder __queryData = new StringBuilder();
                                for (int __field = 0; __field < _myGridField._rowData.Count; __field++)
                                {
                                    if (_myGridField._cellGet(__field, 0).ToString().Equals("1"))
                                    {
                                        if (__queryField.Length > 0)
                                        {
                                            __queryField.Append(",");
                                            __queryData.Append(",");
                                        }
                                        string __fieldName = _myGridField._cellGet(__field, "field name").ToString();// __fieldList[__field]["fieldname"].ToString();
                                        __queryField.Append(__fieldName);
                                        string __fieldType = _myGridField._cellGet(__field, "type").ToString();  //__fieldList[__field]["type"].ToString();
                                        if (__fieldType.Equals("image"))
                                        {
                                            __queryData.Append("decode(\'" + __data.Rows[__record][__fieldName].ToString() + "\','base64')");
                                        }
                                        else if (__fieldType.Equals("smalldatetime") || __fieldType.Equals("datetime"))
                                        {
                                            string __getDateValue = __data.Rows[__record][__fieldName].ToString();
                                            __queryData.Append((__getDateValue.Length > 0) ? "\'" + __getDateValue + "\'" : "null");
                                        }
                                        else
                                        {
                                            if (__fieldType.Equals("int") || __fieldType.Equals("smallint") || __fieldType.Equals("tinyint") || __fieldType.Equals("float"))
                                            {
                                                string __getNumberValue = __data.Rows[__record][__fieldName].ToString();
                                                __queryData.Append((__getNumberValue.Length > 0) ? __getNumberValue : "0");
                                            }
                                            else
                                            {
                                                __queryData.Append("\'" + __data.Rows[__record][__fieldName].ToString().Replace("'", "''") + "\'");
                                            }
                                        }
                                    }
                                }
                                __query.Append("insert into " + __tableName + " (" + __queryField.ToString() + ") values (" + __queryData.ToString() + ");\r\n");
                            }


                            // this._queryResult.Text = __query.ToString();

                            byte[] __array = Encoding.UTF8.GetBytes(__query.ToString());
                            __gz.Write(__array, 0, __array.Length);
                            int __countProgress = __count;
                            if (__countProgress > __totalRecord)
                            {
                                __countProgress = __totalRecord;
                            }
                            //this._progressRecordTextStr = __countProgress.ToString() + "/" + __totalRecord.ToString();
                            //this._progressBarRecordValue = (((__countProgress) * 100) / (__totalRecord));


                        }
                    }
                }

                __gz.Close();
                __f2.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n" + lastQuery);
            }

            MessageBox.Show("Success");

        }
    }
}
