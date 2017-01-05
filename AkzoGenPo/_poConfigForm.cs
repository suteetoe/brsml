using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace AkzoGenPo
{
    public partial class _poConfigForm : Form
    {

        public _poConfigForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(_poConfigForm_Load);
        }

        void _poConfigForm_Load(object sender, EventArgs e)
        {
            try
            {
                XmlSerializer s = new XmlSerializer(typeof(AkzoGlobal._global._poConfig));
                AkzoGlobal._global._poConfig __config;
                TextReader r = new StreamReader(AkzoGlobal._global._poConfigFileName);
                __config = (AkzoGlobal._global._poConfig)s.Deserialize(r);
                r.Close();
                this._serverTextbox.Text = __config._serverAddress;
                this._databaseTextbox.Text = __config._database_name;
                this._usercodeTextbox.Text = __config._user_code;
                this._passwordTextbox.Text = __config._user_password;
                this._sendmailCheckbox.Checked = __config._sendOrder;
                this._emailTextbox.Text = __config._emailOrderTarget;
                this._passwordSenderTextbox.Text = __config._emailSenderPassword;
            }
            catch
            {
                this._serverTextbox.Text = MyLib._myGlobal._webServiceServer;
                this._databaseTextbox.Text = MyLib._myGlobal._databaseName;
                this._usercodeTextbox.Text = MyLib._myGlobal._userName;
            }
        }

        void _saveButton_Click(object sender, System.EventArgs e)
        {
            // test connection 
            if (AkzoGlobal._global._sqlTestConnection(this._serverTextbox.Text, this._databaseTextbox.Text, this._usercodeTextbox.Text, this._passwordTextbox.Text) == false)
            {
                MessageBox.Show("Connect Fail", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // save config
            try
            {
                AkzoGlobal._global._poConfig __config = new AkzoGlobal._global._poConfig();
                __config._serverAddress = this._serverTextbox.Text;
                __config._database_name = this._databaseTextbox.Text;
                __config._user_code = this._usercodeTextbox.Text;
                __config._user_password = this._passwordTextbox.Text;
                __config._sendOrder = this._sendmailCheckbox.Checked;
                __config._emailOrderTarget = this._emailTextbox.Text;
                __config._emailSenderPassword = this._passwordSenderTextbox.Text;

                XmlSerializer s = new XmlSerializer(typeof(AkzoGlobal._global._poConfig));
                TextWriter w = new StreamWriter(AkzoGlobal._global._poConfigFileName);
                s.Serialize(w, __config);
                //
                w.Close();

                MessageBox.Show("Save PO Config Success. \nthis Screen will be close.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString(), "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
