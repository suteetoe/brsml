using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace AkzoGenPo
{
    public partial class _loginForm : Form
    {
        string _fileNameLastAccess = @"c:\\smlconfig\\lastaccess2.xml";

        public _loginForm()
        {
            InitializeComponent();
            // เช็ค folders ก่อนเลย
            string __configDir = AkzoGlobal._global._smlConfigFile;
        }

        private void _loginButton_Click(object sender, EventArgs e)
        {
            if (AkzoGlobal._global._sqlTestConnection(this._serverNameTextBox.Text, this._databaseNameTextBox.Text, this._userCodeTextBox.Text, this._userPasswordTextBox.Text))
            {
                AkzoGlobal._global._loginPass = true;

                AkzoGlobal._global._webServiceServer = this._serverNameTextBox.Text;
                AkzoGlobal._global._databaseName = this._databaseNameTextBox.Text;
                AkzoGlobal._global._userName = this._userCodeTextBox.Text;
                AkzoGlobal._global._password = this._userPasswordTextBox.Text;
                // Serialization
                lastAccess myList = new lastAccess();
                myList._databaseName = AkzoGlobal._global._databaseName;
                myList._databaseUser = AkzoGlobal._global._userName;
                myList._serverName = AkzoGlobal._global._webServiceServer;
                XmlSerializer s = new XmlSerializer(typeof(lastAccess));
                TextWriter w = new StreamWriter(_fileNameLastAccess);
                s.Serialize(w, myList);
                //
                w.Close();
                this.Dispose();
            }
            else
            {
                MessageBox.Show("ไม่สามารถติดต่อกับฐานข้อมูลได้ กรุณาตรวจสอบใหม่");
            }
        }

        private void _loginForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Deserialization
                XmlSerializer s = new XmlSerializer(typeof(lastAccess));
                lastAccess newList;
                TextReader r = new StreamReader(_fileNameLastAccess);
                newList = (lastAccess)s.Deserialize(r);
                r.Close();
                this._serverNameTextBox.Text = newList._serverName;
                this._databaseNameTextBox.Text = newList._databaseName;
                this._userCodeTextBox.Text = newList._databaseUser;
                this._userPasswordTextBox.Text = "";
            }
            catch
            {
            }
        }
    }

    [XmlRoot("lastAccess")]
    public class lastAccess
    {
        [XmlAttribute("_serverName")]
        public string _serverName;
        [XmlAttribute("_databaseName")]
        public string _databaseName;
        [XmlAttribute("_databaseUser")]
        public string _databaseUser;
    }
}