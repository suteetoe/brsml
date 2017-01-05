using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml.Serialization;	 //For serialization of an object to an XML Document file.

namespace SMLERPGLControl
{
    public partial class _recurring : UserControl
    {
        _recurringInputScreen _recurringInput;
        public _journalScreen _journalInputScreen;
        public SMLERPGLControl._glDetail _journalInputDetail;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();

        public _recurring()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            if (MyLib._myGlobal._isDesignMode == false)
            {
                _reloadData();
            }
            this._recurringComboBox.SelectedIndexChanged += new EventHandler(_recurringComboBox_SelectedIndexChanged);
        }

        void _recurringComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string __getCode = this._recurringComboBox.SelectedItem.ToString().Split('/')[0].ToString();
            DataSet __result = _myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.gl_recurring._xmlstring + " from " + _g.d.gl_recurring._table + " where " + _g.d.gl_recurring._code + "=\'" + __getCode + "\'");
            _recurringFormatType __data = new _recurringFormatType();
            XmlSerializer __serializer = new XmlSerializer(typeof(_recurringFormatType));
            string __getData = MyLib._myUtil._reConvertTextToXml(__result.Tables[0].Rows[0].ItemArray[0].ToString());
            XmlTextReader __textReader = new XmlTextReader(new StringReader(__getData));
            __data = (_recurringFormatType)__serializer.Deserialize(__textReader);
            //
            this._journalInputScreen._setDataStr(_g.d.gl_journal._book_code, __data._bookCode);
            this._journalInputScreen._setDataStr(_g.d.gl_journal._description, __data._description);
            this._journalInputDetail._glDetailGrid._clear();
            for (int __row = 0; __row < __data._detail.Count; __row++)
            {
                int __newRow = this._journalInputDetail._glDetailGrid._addRow();
                _recurringDetailFormatType __getDataRow = (_recurringDetailFormatType)__data._detail[__row];
                this._journalInputDetail._glDetailGrid._cellUpdate(__row, _g.d.gl_journal_detail._account_code, __getDataRow._accountCode, false);
                this._journalInputDetail._glDetailGrid._cellUpdate(__row, _g.d.gl_journal_detail._account_name, __getDataRow._accountName, false);
                this._journalInputDetail._glDetailGrid._cellUpdate(__row, _g.d.gl_journal_detail._description, __getDataRow._desc, false);
            }
        }

        public void _reloadData()
        {
            DataSet __result = _myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.gl_recurring._code + "," + _g.d.gl_recurring._gl_desc + " from " + _g.d.gl_recurring._table + " order by " + _g.d.gl_recurring._code);
            this._recurringComboBox.Items.Clear();
            for (int __loop = 0; __loop < __result.Tables[0].Rows.Count; __loop++)
            {
                string __getCode = __result.Tables[0].Rows[__loop].ItemArray[0].ToString();
                string __genText = __getCode + "/" + __result.Tables[0].Rows[__loop].ItemArray[1].ToString();
                this._recurringComboBox.Items.Add(__genText);
            }
        }

        public void _recurringActive()
        {
            _recurringInput = new _recurringInputScreen();
            _recurringInput._saveButton.Click += new EventHandler(_saveButton_Click);
            _recurringInput._cancelButton.Click += new EventHandler(_cancelButton_Click);
            _recurringInput.ShowDialog(this);
        }

        void _cancelButton_Click(object sender, EventArgs e)
        {
            _recurringInput.Close();
        }

        void _saveButton_Click(object sender, EventArgs e)
        {
            string __code = _recurringInput._codeTextBox.textBox.Text;
            _recurringInput.Close();
            if (_journalInputScreen != null && _journalInputDetail != null)
            {
                if (__code.Length > 0)
                {
                    _recurringFormatType _data = new _recurringFormatType();
                    _data._bookCode = _journalInputScreen._getDataStr(_g.d.gl_journal._book_code).ToString();
                    _data._description = _journalInputScreen._getDataStr(_g.d.gl_journal._description).ToString();
                    for (int __row = 0; __row < this._journalInputDetail._glDetailGrid._rowData.Count; __row++)
                    {
                        _recurringDetailFormatType __newDetail = new _recurringDetailFormatType();
                        __newDetail._accountCode = this._journalInputDetail._glDetailGrid._cellGet(__row, _g.d.gl_journal_detail._account_code).ToString();
                        __newDetail._accountName = this._journalInputDetail._glDetailGrid._cellGet(__row, _g.d.gl_journal_detail._account_name).ToString();
                        __newDetail._desc = this._journalInputDetail._glDetailGrid._cellGet(__row, _g.d.gl_journal_detail._description).ToString();
                        _data._detail.Add(__newDetail);
                    }
                    //
                    MemoryStream memoryStream = new MemoryStream();
                    XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                    XmlSerializer xs = new XmlSerializer(typeof(_recurringFormatType));
                    xs.Serialize(xmlTextWriter, _data);
                    String XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
                    Debug.WriteLine(XmlizedString);
                    XmlizedString = MyLib._myUtil._convertTextToXml(XmlizedString);
                    XmlizedString = XmlizedString.Remove(0, 1);
                    Debug.WriteLine(XmlizedString);
                    string __query = "insert into " + _g.d.gl_recurring._table + " (" + _g.d.gl_recurring._code + "," + _g.d.gl_recurring._gl_desc + "," + _g.d.gl_recurring._xmlstring + ") values (\'" + __code + "\',\'" + _data._description + "\',\'" + XmlizedString + "\')";
                    Debug.WriteLine(__query);
                    string __result = _myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __query);
                    if (__result.Length > 0)
                    {
                        MessageBox.Show(__result);
                    }
                    else
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("สร้างเรียบร้อย"));
                        this._reloadData();
                    }
                }
            }
        }

        private String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        private void _recurringSaveButton_Click(object sender, EventArgs e)
        {
            _recurringActive();
        }

        private void _recurringRefreshButton_Click(object sender, EventArgs e)
        {
            this._reloadData();
        }

        public void _deleteData()
        {
            if (this._recurringComboBox.SelectedIndex == -1)
            {
                MessageBox.Show(MyLib._myGlobal._resource("ยังไม่ได้เลือกรายการ"));
            }
            else
            {
                DialogResult __getResult = MessageBox.Show(String.Format(MyLib._myGlobal._resource("ต้องการลบ [{0}] จริงหรือไม่"), this._recurringComboBox.SelectedItem.ToString()), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (__getResult == DialogResult.Yes)
                {
                    _myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from " + _g.d.gl_recurring._table + " where " + _g.d.gl_recurring._code + "=\'" + this._recurringComboBox.SelectedItem.ToString().Split('/')[0].ToString() + "\'");
                    this._reloadData();
                }
            }
        }

        private void _recurringDeleteButton_Click(object sender, EventArgs e)
        {
            _deleteData();
        }
    }

    [XmlRootAttribute(ElementName = "_data", IsNullable = false)]
    public class _recurringFormatType
    {
        [XmlAttribute]
        public string _bookCode;
        [XmlAttribute]
        public string _description;
        [XmlArray("_detail"), XmlArrayItem("_detailList", typeof(_recurringDetailFormatType))]
        public ArrayList _detail = new ArrayList();
    }

    [Serializable]
    public class _recurringDetailFormatType
    {
        [XmlAttribute]
        public string _accountCode;
        [XmlAttribute]
        public string _accountName;
        [XmlAttribute]
        public string _desc;
    }
}
