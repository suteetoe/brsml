using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Xml.Serialization;

namespace SMLFormDesignGenerate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this._myDataList._buttonDelete.Visible = false;

            this._myDataList._gridData._isEdit = false;
            this._myDataList._lockRecord = true;
            this._myDataList._loadViewFormat("screen_formdesign_loadform", MyLib._myGlobal._userSearchScreenGroup, true);
            this._myDataList._referFieldAdd(_g.d.sml_posdesign._screen_code, 1);
            
            _textXML.KeyPress += new KeyPressEventHandler(_textXML_KeyPress);
        }

        void _textXML_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        int _posDesignMax = 0;
        int _posDesignIndex = 0;
        string _processText = "";

        List<_designSelect> __selectDesign = null;
        private void _genXMLButton_Click(object sender, EventArgs e)
        {
            _saveButton.Enabled = false;
            _textXML.Text = "";

            __selectDesign = new List<_designSelect>();
            // check select design
            for (int __i = 0; __i < this._myDataList._gridData._rowData.Count; __i++)
            {
                if ((int)this._myDataList._gridData._cellGet(__i, "check") == 1)
                {
                    string __screenCode = (string)this._myDataList._gridData._cellGet(__i, 1);
                    string __screenName = (string)this._myDataList._gridData._cellGet(__i, 2);
                    __selectDesign.Add(new _designSelect() { _code = __screenCode, _name = __screenName });
                }
            }

            _posDesignMax = __selectDesign.Count;
            _posDesignIndex = 0;
            _processText = "";
            _progressBar.Maximum = _posDesignMax;
            // gen by thread
            Thread __processThread = new Thread(_process);
            __processThread.Start();
            timer1.Start();
        }

        List<SMLReport._formReport.SMLFormDesignXml> __designXML = null;
        void _process()
        {
            __designXML = new List<SMLReport._formReport.SMLFormDesignXml>();
            MyLib._myFrameWork __fw = new MyLib._myFrameWork();

            for (_posDesignIndex = 0; _posDesignIndex < __selectDesign.Count; _posDesignIndex++)
            {
                // get design มาก่อนที่จะ gen
                SMLReport._formReport.SMLFormDesignXml __xml = new SMLReport._formReport.SMLFormDesignXml();

                try
                {
                    // query max from database 
                    string __reportNameGet = __selectDesign[_posDesignIndex]._code;

                    string _query = "select " + _g.d.formdesign._formdesigntext + " from " + _g.d.formdesign._table + " where " + MyLib._myGlobal._addUpper(_g.d.formdesign._formcode) + " = \'" + __reportNameGet.ToUpper() + "\'";
                    Byte[] __result = __fw._queryByte(MyLib._myGlobal._databaseName, _query);

                    MemoryStream __ms = new MemoryStream(MyLib._compress._decompressBytes((byte[])__result));
                    XmlSerializer __xs = new XmlSerializer(typeof(SMLReport._formReport.SMLFormDesignXml));
                    __xml = (SMLReport._formReport.SMLFormDesignXml)__xs.Deserialize(__ms);
                }
                catch (Exception ex)
                {
                }

                if (__xml != null)
                {
                    __designXML.Add(__xml);
                }
                // after success
                _processText = string.Format("{0} of {1}", _posDesignIndex, _posDesignMax);

            }

            _processText = "success";

        }

        string _genDesignXML()
        {
            string __XMLresult = "";

            StringBuilder __designStrList = new StringBuilder();
            __designStrList.AppendLine(MyLib._myGlobal._xmlHeader);

            if (__designXML != null)
            {
                __designStrList.AppendLine("<node>");
                for (int __i = 0; __i < __designXML.Count; __i++)
                {
                    __designStrList.AppendLine(string.Format("<formdesign formcode=\"{0}\" formname=\"{1}\" >", __selectDesign[__i]._code, __selectDesign[__i]._name));
                    __designStrList.AppendLine("<content>");

                    string __reportXML = MyLib._myGlobal._objectToXml(__designXML[__i], typeof(SMLReport._formReport.SMLFormDesignXml));

                    byte[] __reportCompress = MyLib._compress._compressString(__reportXML);
                    string __reportData = Convert.ToBase64String(__reportCompress); //MyLib._myGlobal._convertMemoryStreamToString(new MemoryStream(__reportCompress));

                    __designStrList.AppendLine(__reportData);
                    __designStrList.AppendLine("</content>");
                    __designStrList.AppendLine("</formdesign>");
                }
                __designStrList.AppendLine("</node>");

            }


            __XMLresult = __designStrList.ToString();
            return __XMLresult;
        }


        string _saveFileName = "";
        private void _saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog __save = new SaveFileDialog() { DefaultExt = "xml", Filter = "XML Files (*.xml)|*.xml|All files (*.*)|*.*", FilterIndex = 0 };
            if (__save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _saveFileName = __save.FileName;
                try
                {
                    // Write the string to a file.
                    System.IO.StreamWriter file = new System.IO.StreamWriter(_saveFileName, false, Encoding.GetEncoding("utf-8"));
                    file.Write(_genDesignXML());
                    file.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Can't save " + _saveFileName + " (" + ex.Message.ToString() + ")");
                }
                MessageBox.Show("Save Success !!");
            }
        }

        public class _designSelect
        {
            public string _code = "";
            public string _name = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _resultLabel.Text = _processText;
            _progressBar.Value = _posDesignIndex;

            if (_processText.Equals("success"))
            {
                _textXML.Text = _genDesignXML();
                _saveButton.Enabled = true;
                timer1.Stop();
            }
        }

    }
}
