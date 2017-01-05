using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace AKZOPODownloadConfig
{
    public partial class Form1 : Form
    {
        string _configFileName = @"C:\smlconfig\akzopodownloadconfig.xml";
        string _termXMLFileName = @"C:\smlconfig\akzoterm.xml";
        string _unitcodeXMLFileName = @"C:\smlconfig\akzounit.xml";

        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
        }

        void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(_configFileName) == true)
            {
                _loadConfig();
            }

            string __computerName = SystemInformation.ComputerName.ToLower();
            if (__computerName.IndexOf("toe-pc") != -1)
            {
                this.button1.Visible = true;
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            this._saveData();
        }

        void _loadConfig()
        {
            XmlDocument xDoc = new XmlDocument();
            try
            {
                xDoc.Load(_configFileName);
            }
            catch
            {

            }

            xDoc.DocumentElement.Normalize();
            XmlElement __xRoot = xDoc.DocumentElement;

            string __center_address = __xRoot.GetElementsByTagName("center_address").Item(0).InnerText;
            string __partn = __xRoot.GetElementsByTagName("partn").Item(0).InnerText;
            string __host = __xRoot.GetElementsByTagName("host").Item(0).InnerText;
            string __port = __xRoot.GetElementsByTagName("port").Item(0).InnerText;
            string __db_user = __xRoot.GetElementsByTagName("db_user").Item(0).InnerText;
            string __db_pass = __xRoot.GetElementsByTagName("db_pass").Item(0).InnerText;
            string __db_name = __xRoot.GetElementsByTagName("db_name").Item(0).InnerText;
            string __ap_code = __xRoot.GetElementsByTagName("ap_code").Item(0).InnerText;

            this._serverIpTextbox.Text = __center_address;
            this._partnTextbox.Text = __partn;
            this._hostTextbox.Text = __host;
            this._portTextbox.Text = __port;
            this._userCodeTextbox.Text = __db_user;
            //this._userpwTextbox.Text = __db_pass;
            this._dbNameTextbox.Text = __db_name;
            this._apCodeTextbox.Text = __ap_code;
        }

        void _saveData()
        {
            if (_testConnect() == true)
            {

                // save xml other

                try
                {
                    _writeTermXMLFile();
                    _writePEINHXMLFile();

                    StringBuilder __xml = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                    __xml.Append("<center_address>" + this._serverIpTextbox.Text + "</center_address>");
                    __xml.Append("<partn>" + this._partnTextbox.Text + "</partn>");
                    __xml.Append("<host>" + this._hostTextbox.Text + "</host>");
                    __xml.Append("<port>" + this._portTextbox.Text + "</port>");
                    __xml.Append("<db_user>" + this._userCodeTextbox.Text + "</db_user>");
                    __xml.Append("<db_pass>" + this._userpwTextbox.Text + "</db_pass>");
                    __xml.Append("<db_name>" + this._dbNameTextbox.Text + "</db_name>");
                    __xml.Append("<ap_code>" + this._apCodeTextbox.Text + "</ap_code>");
                    __xml.Append("</node>");

                    StreamWriter __sr = File.CreateText(_configFileName);
                    __sr.WriteLine(__xml.ToString());
                    __sr.Close();

                    MessageBox.Show("Save Config Success !!!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        Boolean _testConnect()
        {
            string __connectStr = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", this._hostTextbox.Text, this._portTextbox.Text, this._userCodeTextbox.Text, this._userpwTextbox.Text, this._dbNameTextbox.Text);
            try
            {
                Npgsql.NpgsqlConnection __conn = new Npgsql.NpgsqlConnection(__connectStr);
                __conn.Open();
                __conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return false;
        }

        private void _testConnectButton_Click(object sender, EventArgs e)
        {
            if (_testConnect() == true)
            {
                MessageBox.Show("Connected Success !!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _process __process = new _process();
            __process._run();

            //_process._run();
        }

        void _writeTermXMLFile()
        {
            if (System.IO.File.Exists(_termXMLFileName) == false)
            {
                StringBuilder __xmlTerm = new StringBuilder();
                __xmlTerm.Append(MyLib._myGlobal._xmlHeader + "<node>");

                __xmlTerm.Append("<ZTERM key=\"TH4A\" value=\"30\" />");
                __xmlTerm.Append("<ZTERM key=\"TH4B\" value=\"30\" />");
                __xmlTerm.Append("<ZTERM key=\"TH4C\" value=\"90\" />");
                __xmlTerm.Append("<ZTERM key=\"TH4D\" value=\"120\" />");

                __xmlTerm.Append("<ZTERM key=\"TH6A\" value=\"60\" />");
                __xmlTerm.Append("<ZTERM key=\"TH6B\" value=\"90\" />");
                __xmlTerm.Append("<ZTERM key=\"TH6C\" value=\"120\" />");

                __xmlTerm.Append("<ZTERM key=\"TH8A\" value=\"30\" />");
                __xmlTerm.Append("<ZTERM key=\"TH8B\" value=\"120\" />");
                __xmlTerm.Append("<ZTERM key=\"TH8C\" value=\"90\" />");

                __xmlTerm.Append("</node>");

                // save xml
                StreamWriter __sr = File.CreateText(_termXMLFileName);
                __sr.WriteLine(__xmlTerm.ToString());
                __sr.Close();
            }


        }

        void _writePEINHXMLFile()
        {
            if (System.IO.File.Exists(_unitcodeXMLFileName) == false)
            {
                StringBuilder __xmlTerm = new StringBuilder();
                __xmlTerm.Append(MyLib._myGlobal._xmlHeader + "<node>");

                __xmlTerm.Append("<PEINH key=\"1\" value=\"1L\" />");
                __xmlTerm.Append("<PEINH key=\"3\" value=\"3L\" />");
                __xmlTerm.Append("<PEINH key=\"5\" value=\"5L\" />");
                __xmlTerm.Append("<PEINH key=\"9\" value=\"9L\" />");
                __xmlTerm.Append("<PEINH key=\"15\" value=\"15L\" />");
                __xmlTerm.Append("<PEINH key=\"0.9\" value=\"1/4G\" />");
                __xmlTerm.Append("<PEINH key=\"3.8\" value=\"1G\" />");
                __xmlTerm.Append("<PEINH key=\"18.9\" value=\"5G\" />");
                __xmlTerm.Append("<PEINH key=\"14.6\" value=\"25K\" />");
                __xmlTerm.Append("<PEINH key=\"2.9\" value=\"5K\" />");
                __xmlTerm.Append("</node>");

                // save xml
                StreamWriter __sr = File.CreateText(_unitcodeXMLFileName);
                __sr.WriteLine(__xmlTerm.ToString());
                __sr.Close();
            }
        }
    }
}
