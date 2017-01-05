using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLPosClient
{
    public partial class _posExtendForm : Form
    {
        private string _posXmlFormName = "";
        public _posClientForm _posClientForm;
        public _posExtendForm()
        {
            InitializeComponent();

        }

        public _posExtendForm(string userCode,string userPassword,string __xmlForm)
        {
            InitializeComponent();

            this._posClientForm = new SMLPosClient._posClientForm(userCode, userPassword,__xmlForm);
            this._posClientForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this._posClientForm.Location = new System.Drawing.Point(0, 0);
            this._posClientForm.Name = "_posClientForm";
            this._posClientForm.Size = MyLib._myGlobal._mainSize;
            this._posClientForm.TabIndex = 0;
            this.Controls.Add(this._posClientForm);

        }

        public void _sendMessage(string __object, string __message)
        {
            if (this._posClientForm != null)
            {
                this._posClientForm._sendMessage(__object, __message,"");
            }
        }
    }
}
