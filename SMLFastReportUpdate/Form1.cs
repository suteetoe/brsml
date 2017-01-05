using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace SMLFastReportUpdate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MyLib._getStream._getStreamEvent += _getStream__getStreamEvent;

            this._myGrid1._isEdit = false;
            this._myGrid1._addColumn("check", 11, 10, 10);
            this._myGrid1._addColumn("code", 1, 25, 25);
            this._myGrid1._addColumn("name_1", 1, 40, 40);
            this._myGrid1._addColumn("time", 1, 25, 25);
        }

        Stream _getStream__getStreamEvent(string xmlFileName)
        {
            Assembly __thisAssembly = Assembly.GetExecutingAssembly();
            return __thisAssembly.GetManifestResourceStream("SMLFastReportUpdate." + xmlFileName);
        }

        bool _testConnect()
        {
            try
            {
                Npgsql.NpgsqlConnection __postgresConn = new Npgsql.NpgsqlConnection(this._postgresSqlConnString());
                __postgresConn.Open();

                if (__postgresConn.State == System.Data.ConnectionState.Open)
                    __postgresConn.Close();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return false;
        }

        private void _connectButton_Click(object sender, EventArgs e)
        {
            if (_testConnect())
            {
                MessageBox.Show("Connected !!!");
                this._updateButton.Enabled = true;
            }
        }

        string _postgresSqlConnString()
        {
            return String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", this._hostTextBox.Text, this._portTextBox.Text, this._userTextBox.Text, this._passwordTextBox.Text, this._databaseNameTextBox.Text);
        }


        private void _updateButton_Click(object sender, EventArgs e)
        {
            this._updateButton.Enabled = false;

            if (_testConnect())
            {
                // read xml file 
                // compare and copy

                string _reportFileName = "smlfastreport.xml";

                Npgsql.NpgsqlConnection __postgresConn = new Npgsql.NpgsqlConnection(this._postgresSqlConnString());
                __postgresConn.Open();

                try
                {
                    string __query = "select count(" + _g.d.sml_fastreport._menuid + ") as countreport from " + _g.d.sml_fastreport._table;

                    DataTable __dTable = new DataTable();
                    Npgsql.NpgsqlDataAdapter __npAdapter = new Npgsql.NpgsqlDataAdapter();
                    __npAdapter.SelectCommand = new Npgsql.NpgsqlCommand(__query, __postgresConn);
                    __npAdapter.Fill(__dTable);
                    __npAdapter.Dispose();


                    //StreamReader __source = MyLib._getStream._getDataStream(_reportFileName);
                    FileStream __source = File.Open(Application.StartupPath + @"\" + _reportFileName, FileMode.Open, FileAccess.Read, FileShare.Read);

                    XmlDocument _reportXML = new XmlDocument();
                    _reportXML.Load(__source);

                    if (MyLib._myGlobal._decimalPhase(__dTable.Rows[0]["countreport"].ToString()) == 0)
                    {
                        XmlNodeList __xmlList = _reportXML.SelectNodes("/node/report");

                        foreach (XmlNode __node in __xmlList)
                        {

                            string __reportId = __node.Attributes["id"].Value.ToString();
                            string __reportName = __node.Attributes["name"].Value.ToString();
                            string __reportTimeUpdate = __node.Attributes["timeupdate"].Value.ToString();
                            string __reportContent = __node["content"].InnerText;
                            int __reportType = MyLib._myGlobal._intPhase(__node.Attributes["reporttype"].Value.ToString());

                            byte[] __reportByte = Convert.FromBase64String(__reportContent);
                            string __reportStr = MyLib._compress._deCompressString(__reportByte);

                            //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\posInitTract_fastreport.txt", DateTime.Now.ToString() + " : Get Report " + __reportName + " Value", false);

                            SMLFastReport._xmlClass __xmlClass = (SMLFastReport._xmlClass)MyLib._myGlobal.FromXml(__reportStr, typeof(SMLFastReport._xmlClass));

                            XmlSerializer __xs = new XmlSerializer(typeof(SMLFastReport._xmlClass));
                            MemoryStream __memoryStream = new MemoryStream();
                            __xs.Serialize(__memoryStream, __xmlClass);

                            string __queryIns = string.Format("insert into " + _g.d.sml_fastreport._table + "(" + _g.d.sml_fastreport._is_system_report + "," + _g.d.sml_fastreport._menuid + "," + _g.d.sml_fastreport._menuname + ", " + _g.d.sml_fastreport._timeupdate + "," + _g.d.sml_fastreport._report_type + "," + _g.d.sml_fastreport._reportdata + ") VALUES(1, '{0}','{1}', '{2}', '{3}',@DataReport)", __reportId, __reportName, __reportTimeUpdate, __reportType);

                            byte[] __memoryStreamCompress = MyLib._compress._compressString(MyLib._myGlobal._convertMemoryStreamToString(__memoryStream));

                            // insert to db

                            //string __result = __ws._queryByteData(MyLib._myGlobal._databaseName, _queryIns, new object[] { __memoryStreamCompress });
                            Npgsql.NpgsqlCommand __command = new Npgsql.NpgsqlCommand(__queryIns, __postgresConn);
                            __command.Parameters.AddWithValue("@DataReport", __memoryStreamCompress);

                            try
                            {
                                if (__postgresConn.State == ConnectionState.Closed)
                                    __postgresConn.Open();
                                int __result = __command.ExecuteNonQuery();
                                if (__result > 0)
                                {
                                    Console.WriteLine("Import XML Report : " + __reportId + " Success \\n");
                                }
                                else
                                {
                                    Console.WriteLine("Error XML Report : " + __reportId + " " + " \\n");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error XML Report : " + __reportId + " Message \\n " + ex.ToString() + " " + " \\n");
                            }
                            //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\posInitTract_fastreport.txt", DateTime.Now.ToString() + " : Insert Report " + __reportName + " Result : " + __result, false);
                        }
                    }
                    else
                    {
                        //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\posInitTract_fastreport.txt", DateTime.Now.ToString() + " : Compare Import Mode", false);

                        // get report list to compare
                        __query = "select " + _g.d.sml_fastreport._menuid + ", " + _g.d.sml_fastreport._timeupdate + " from " + _g.d.sml_fastreport._table;
                        DataTable __reportResult = new DataTable();

                        Npgsql.NpgsqlDataAdapter __npAdapter2 = new Npgsql.NpgsqlDataAdapter();
                        __npAdapter2.SelectCommand = new Npgsql.NpgsqlCommand(__query, __postgresConn);
                        __npAdapter2.Fill(__reportResult);
                        __npAdapter2.Dispose();

                        // compare mode
                        XmlNodeList __xmlList = _reportXML.SelectNodes("/node/report");
                        foreach (XmlNode __node in __xmlList)
                        {
                            string __reportId = __node.Attributes["id"].Value.ToString();
                            string __reportName = __node.Attributes["name"].Value.ToString();
                            string __reportTimeUpdate = __node.Attributes["timeupdate"].Value.ToString();
                            DateTime __xmlTimeUpdate = DateTime.Parse(__reportTimeUpdate, MyLib._myGlobal._cultureInfo());
                            int __reportType = MyLib._myGlobal._intPhase(__node.Attributes["reporttype"].Value.ToString());

                            //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\posInitTract_fastreport.txt", DateTime.Now.ToString() + " : Get Report " + __reportName + " Value", false);

                            //string _compare = "select " + _g.d.sml_fastreport._menuid + ", " + _g.d.sml_fastreport._timeupdate + " from " + _g.d.sml_fastreport._table + " where " + _g.d.sml_fastreport._menuid + "=\'" + __reportId + "\' ";
                            //DataTable __compareResult = __ws._queryShort(_compare).Tables[0];
                            DataRow[] __select = __reportResult.Select(_g.d.sml_fastreport._menuid + "=\'" + __reportId + "\'");
                            bool _importReport = false;

                            if (__select.Length == 0)
                            {
                                _importReport = true;
                            }
                            else
                            {
                                DateTime _reportTimeUpdate = DateTime.Parse(__select[0][_g.d.sml_fastreport._timeupdate].ToString(), MyLib._myGlobal._cultureInfo());

                                if (__xmlTimeUpdate > _reportTimeUpdate)
                                {
                                    _importReport = true;
                                }
                            }

                            if (_importReport)
                            {
                                try
                                {
                                    Npgsql.NpgsqlCommand __command = new Npgsql.NpgsqlCommand("delete from " + _g.d.sml_fastreport._table + " where " + MyLib._myGlobal._addUpper(_g.d.sml_fastreport._menuid) + "=\'" + __reportId + "\' ", __postgresConn);
                                    if (__postgresConn.State == ConnectionState.Closed)
                                        __postgresConn.Open();
                                    int __result = __command.ExecuteNonQuery();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error XML Report : " + __reportId + " " + ex.Message.ToString() + " \\n");

                                }


                                string __reportContent = __node["content"].InnerText;

                                byte[] __reportByte = Convert.FromBase64String(__reportContent);
                                string __reportStr = MyLib._compress._deCompressString(__reportByte);
                                SMLFastReport._xmlClass __xmlClass = (SMLFastReport._xmlClass)MyLib._myGlobal.FromXml(__reportStr, typeof(SMLFastReport._xmlClass));

                                XmlSerializer __xs = new XmlSerializer(typeof(SMLFastReport._xmlClass));
                                MemoryStream __memoryStream = new MemoryStream();
                                __xs.Serialize(__memoryStream, __xmlClass);

                                string __queryIns = string.Format("insert into " + _g.d.sml_fastreport._table + "(" + _g.d.sml_fastreport._is_system_report + "," + _g.d.sml_fastreport._menuid + "," + _g.d.sml_fastreport._menuname + ", " + _g.d.sml_fastreport._timeupdate + "," + _g.d.sml_fastreport._report_type + "," + _g.d.sml_fastreport._reportdata + ") VALUES(1, '{0}','{1}', '{2}', '{3}',@DataReport)", __reportId, __reportName, __reportTimeUpdate, __reportType);

                                byte[] __memoryStreamCompress = MyLib._compress._compressString(MyLib._myGlobal._convertMemoryStreamToString(__memoryStream));

                                Npgsql.NpgsqlCommand __command2 = new Npgsql.NpgsqlCommand(__queryIns, __postgresConn);
                                __command2.Parameters.AddWithValue("@DataReport", __memoryStreamCompress);

                                try
                                {
                                    if (__postgresConn.State == ConnectionState.Closed)
                                        __postgresConn.Open();
                                    int __result = __command2.ExecuteNonQuery();
                                    if (__result > 0)
                                    {
                                        Console.WriteLine("Import XML Report : " + __reportId + " Success \\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error XML Report : " + __reportId + " " + " \\n");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error XML Report : " + __reportId + " Message \\n " + ex.ToString() + " " + " \\n");
                                }

                                //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\posInitTract_fastreport.txt", DateTime.Now.ToString() + " : Insert Report " + __reportName + " Result : " + __result, false);
                                //if (__result.Length == 0)
                                //{
                                //    Console.WriteLine("Import XML Report : " + __reportId + " Success \\n");
                                //}
                                //else
                                //{
                                //    Console.WriteLine("Error XML Report : " + __reportId + " " + __result + " \\n");
                                //}

                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\posInitTract_fastreport.txt", DateTime.Now.ToString() + " : Exception In Import Fast Report" + ex.ToString(), false);
                }

                //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\posInitTract_fastreport.txt", DateTime.Now.ToString() + " : End Process Import Report ", false);


            }

            MessageBox.Show("success");
            this._updateButton.Enabled = true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string _reportFileName = "smlfastreport.xml";

                FileStream __stream = File.Open(Application.StartupPath + @"\" + _reportFileName, FileMode.Open, FileAccess.Read, FileShare.Read);

                XmlDocument _reportXML = new XmlDocument();
                _reportXML.Load(__stream);



                //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\posInitTract_fastreport.txt", DateTime.Now.ToString() + " : Compare Import Mode", false);

                // get report list to compare
                //__query = "select " + _g.d.sml_fastreport._menuid + ", " + _g.d.sml_fastreport._timeupdate + " from " + _g.d.sml_fastreport._table;
                //DataTable __reportResult = new DataTable();

                //Npgsql.NpgsqlDataAdapter __npAdapter2 = new Npgsql.NpgsqlDataAdapter();
                //__npAdapter2.SelectCommand = new Npgsql.NpgsqlCommand(__query, __postgresConn);
                //__npAdapter2.Fill(__reportResult);
                //__npAdapter2.Dispose();

                // compare mode
                XmlNodeList __xmlList = _reportXML.SelectNodes("/node/report");

                foreach (XmlNode __node in __xmlList)
                {

                    string __reportId = __node.Attributes["id"].Value.ToString();
                    string __reportName = __node.Attributes["name"].Value.ToString();
                    string __reportTimeUpdate = __node.Attributes["timeupdate"].Value.ToString();
                    string __reportContent = __node["content"].InnerText;
                    DateTime __xmlTimeUpdate = DateTime.Parse(__reportTimeUpdate, MyLib._myGlobal._cultureInfo());
                    int __reportType = MyLib._myGlobal._intPhase(__node.Attributes["reporttype"].Value.ToString());

                    byte[] __reportByte = Convert.FromBase64String(__reportContent);
                    string __reportStr = MyLib._compress._deCompressString(__reportByte);

                    //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\posInitTract_fastreport.txt", DateTime.Now.ToString() + " : Get Report " + __reportName + " Value", false);

                    SMLFastReport._xmlClass __xmlClass = (SMLFastReport._xmlClass)MyLib._myGlobal.FromXml(__reportStr, typeof(SMLFastReport._xmlClass));

                    //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\posInitTract_fastreport.txt", DateTime.Now.ToString() + " : Insert Report " + __reportName + " Result : " + __result, false);

                    // add to grid
                    int __addr = this._myGrid1._addRow();

                    this._myGrid1._cellUpdate(__addr, "code", __reportId, true);
                    this._myGrid1._cellUpdate(__addr, "name_1", __reportName, true);
                    this._myGrid1._cellUpdate(__addr, "time", __reportTimeUpdate, true);
                }

            }
            catch (Exception ex)
            {
            }
        }
    }
}
