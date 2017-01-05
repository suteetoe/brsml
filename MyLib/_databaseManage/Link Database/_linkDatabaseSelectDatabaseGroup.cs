using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyLib._databaseManage._linkDatabase
{
	public partial class _linkDatabaseSelectDatabaseGroup : MyLib._myForm
    {
        public _linkDatabaseSelectDatabaseGroup()
		{
            InitializeComponent();
            listViewDatabaseGroupResize();
            this.Resize += new EventHandler(_linkDatabaseSelectDatabaseGroup_Resize);
            _listViewDatabaseGroup.MouseDoubleClick += new MouseEventHandler(_listViewDatabaseGroup_MouseDoubleClick);
            _listViewDatabaseGroup.KeyDown += new KeyEventHandler(_listViewDatabaseGroup_KeyDown);
			_listViewDatabaseGroup._setExStyles();
        }

        void sendData()
        {
            string data = "";
            ListView.SelectedListViewItemCollection breakfast = this._listViewDatabaseGroup.SelectedItems;
            foreach (ListViewItem item in breakfast)
            {
                data = item.Tag.ToString();
            }
            if (data.Length > 0)
            {
                _doubleClickWork(data);
            }
        }

        void _listViewDatabaseGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
				Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                sendData();
            }
        }

        void _listViewDatabaseGroup_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            sendData();
        }

        public event searchDatabaseGroupDoubleClickEventHandler _doubleClick;

        protected virtual void _doubleClickWork(string e)
        {
            if (_doubleClick != null) _doubleClick(this, e);
			Close();
        }

        void _linkDatabaseSelectDatabaseGroup_Resize(object sender, EventArgs e)
        {
            listViewDatabaseGroupResize();
        }

        private void _linkDatabaseSelectDatabaseGroup_Load(object sender, EventArgs e)
        {

        }

        void listViewDatabaseGroupResize()
        {
            _listViewDatabaseGroup.Location = new Point(5, 5);
            _listViewDatabaseGroup.Width = this.DisplayRectangle.Width - 10;
            _listViewDatabaseGroup.Height = this.DisplayRectangle.Height - 10;
        }
    }

    public delegate void searchDatabaseGroupDoubleClickEventHandler(object sender, string e);
}