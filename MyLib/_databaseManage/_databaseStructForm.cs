using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyLib._databaseManage
{
    public partial class _databaseStructForm : Form
    {
        private string _tableName = "";

        public _databaseStructForm(string tableName)
        {
            this._tableName = tableName.ToLower();
            InitializeComponent();
            this._database._myFlowLayoutPanel1.Visible = false;
            this._database._databaseStructLoadSuccess += new DatabaseStructLoadSuccessHandler(_database__databaseStructLoadSuccess);
            this.Load += new EventHandler(_databaseStructForm_Load);
            this._database.splitContainer2.Panel1.Visible = false;
            this._database.splitContainer2.SplitterDistance = 0;
            this._database._toolStrip.Visible = false;
        }

        void _database__databaseStructLoadSuccess(object sender)
        {
            this._database._displayDetail(this._tableName);
        }

        void _databaseStructForm_Load(object sender, EventArgs e)
        {
            
        }

        private void _database_Load(object sender, EventArgs e)
        {

        }
    }
}
