using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Threading;

namespace SMLFastReportDesign
{
    public partial class _formCreateScrip : Form
    {
        DataTable __dt = null;

        public _formCreateScrip()
        {
            InitializeComponent();
            this._textScrip.KeyPress += new KeyPressEventHandler(_textScrip_KeyPress);
            this.Load += new EventHandler(_formCreateScrip_Load);
        }

        void _formCreateScrip_Load(object sender, EventArgs e)
        {
            // load by thread 
            string _query = "select " + _g.d.sml_fastreport._menuid + ", " + _g.d.sml_fastreport._menuname + ", " + _g.d.sml_fastreport._timeupdate + "," + _g.d.sml_fastreport._report_type +  " from " + _g.d.sml_fastreport._table ;
            MyLib._myFrameWork __fw = new MyLib._myFrameWork();
            DataSet __ds = __fw._query(MyLib._myGlobal._databaseName, _query);
            if (__ds.Tables[0].Rows.Count > 0)
            {
                __dt = __ds.Tables[0];

                Thread __thread = new Thread(_process);
                __thread.Start();

                _timer.Start();
            }

        }

        void _textScrip_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        String _processText = "";
        List<SMLFastReport._xmlClass> _reportList = null;

        void _process()
        {
            _reportList = new List<SMLFastReport._xmlClass>();
            MyLib._myFrameWork __fw = new MyLib._myFrameWork();
            if (__dt != null)
            {
                for (int __i = 0; __i < __dt.Rows.Count; __i++)
                {
                    SMLFastReport._xmlClass __xml = new SMLFastReport._xmlClass();

                    try
                    {
                        // query max from database 
                        string __reportNameGet = __dt.Rows[__i][_g.d.sml_fastreport._menuid].ToString();
                        string _query = "select " + _g.d.sml_fastreport._reportdata + " from " + _g.d.sml_fastreport._table + " where " + _g.d.sml_fastreport._menuid + " = \'" + __reportNameGet + "\'";
                        Byte[] __result = __fw._queryByte(MyLib._myGlobal._databaseName, _query);

                        MemoryStream __ms = new MemoryStream(MyLib._compress._decompressBytes((byte[])__result));
                        XmlSerializer __xs = new XmlSerializer(typeof(SMLFastReport._xmlClass));
                        __xml = (SMLFastReport._xmlClass)__xs.Deserialize(__ms);
                    }
                    catch(Exception ex)
                    {
                    }

                    if (__xml != null)
                    {
                        _reportList.Add(__xml);
                    }
                    // after success
                    _processText = string.Format("{0} of {1}", __i, __dt.Rows.Count);
                }
            }

            _processText = "success";
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (_processText.Equals("success"))
            {
                // gen xml to textbox
                _textScrip.Text = _genXML();
                _buttonSave.Enabled = true;

                _timer.Stop();
            }
            label1.Text = _processText;
        }

        string _genXML()
        {
            StringBuilder __reportStrList = new StringBuilder();
            __reportStrList.AppendLine(MyLib._myGlobal._xmlHeader);
            if (_reportList != null)
            {
                __reportStrList.AppendLine("<node>");
                for (int __i = 0; __i < _reportList.Count; __i++)
                {
                    __reportStrList.AppendLine(string.Format("<report id=\"{0}\" name=\"{1}\" timeupdate=\"{2}\" reporttype=\"{3}\" >", __dt.Rows[__i][_g.d.sml_fastreport._menuid].ToString(), __dt.Rows[__i][_g.d.sml_fastreport._menuname].ToString(), __dt.Rows[__i][_g.d.sml_fastreport._timeupdate].ToString(), __dt.Rows[__i][_g.d.sml_fastreport._report_type].ToString()));
                    __reportStrList.AppendLine("<content>");
                   
                    string __reportXML = ToXml(_reportList[__i], typeof(SMLFastReport._xmlClass));
                    
                    byte[] __reportCompress = MyLib._compress._compressString(__reportXML);
                    string __reportData = Convert.ToBase64String(__reportCompress); //MyLib._myGlobal._convertMemoryStreamToString(new MemoryStream(__reportCompress));
                    
                    __reportStrList.AppendLine(__reportData);
                    __reportStrList.AppendLine("</content>");
                    __reportStrList.AppendLine("</report>");
                }
                __reportStrList.AppendLine("</node>");

            }

            return __reportStrList.ToString();
        }

        public static object FromXml(string Xml, System.Type ObjType)
        {
            XmlSerializer ser = new XmlSerializer(ObjType);
            StringReader stringReader;
            stringReader = new StringReader(Xml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            object obj = ser.Deserialize(xmlReader);
            xmlReader.Close();
            stringReader.Close();
            return obj;
        }

        public static string ToXml(object Obj, System.Type ObjType)
        {
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Indent = true;

            XmlSerializer ser = new XmlSerializer(ObjType);
            MemoryStream memStream = new MemoryStream();
            StringBuilder __str = new StringBuilder();
            XmlWriter xmlWriter = XmlWriter.Create(__str, setting);

            ser.Serialize(xmlWriter, Obj);
            xmlWriter.Close();
            memStream.Close();
            return __str.ToString();
        }

        string _saveFileName = "";
        private void _buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog __save = new SaveFileDialog() { DefaultExt = "xml", Filter = "XML Files (*.xml)|*.xml|All files (*.*)|*.*", FilterIndex = 0 };
            if (__save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                    _saveFileName = __save.FileName;
                    try
                    {
                        // Write the string to a file.
                        System.IO.StreamWriter file = new System.IO.StreamWriter(_saveFileName, false, Encoding.GetEncoding("utf-8"));
                        file.Write(_genXML());
                        file.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Can't save " + _saveFileName + " (" + ex.Message.ToString() + ")");
                    }
            }
        }

    }
}
