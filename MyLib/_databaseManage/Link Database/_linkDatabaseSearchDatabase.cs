using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyLib._databaseManage._linkDatabase
{
	public partial class _linkDatabaseSearchDatabase : MyLib._myForm
    {
        public _linkDatabaseSearchDatabase()
		{
            InitializeComponent();
            listViewDatabaseResize();
            this.Resize += new EventHandler(_linkDatabaseSearch_Resize);
            _listViewDatabase.MouseDoubleClick += new MouseEventHandler(_listViewDatabase_MouseDoubleClick);
            _listViewDatabase.KeyDown += new KeyEventHandler(_linkDatabaseSearch_KeyDown);
			_listViewDatabase._setExStyles();
        }

        void sendData()
        {
            string data = "";
            ListView.SelectedListViewItemCollection breakfast = this._listViewDatabase.SelectedItems;
            foreach (ListViewItem item in breakfast)
            {
                data = item.Name.ToString();
            }
            _doubleClickWork(data);
        }

        void _linkDatabaseSearch_KeyDown(object sender, KeyEventArgs e)
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

        void _listViewDatabase_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            sendData();
        }

        public event searchDatabaseDoubleClickEventHandler _doubleClick;

        protected virtual void _doubleClickWork(string e)
        {
            if (e.Length > 0)
            {
                if (_doubleClick != null) _doubleClick(this, e);
				Close();
            }
        }

        void _linkDatabaseSearch_Resize(object sender, EventArgs e)
        {
            listViewDatabaseResize();
        }

        void listViewDatabaseResize()
        {
            _listViewDatabase.Location = new Point(5, 5);
            _listViewDatabase.Width = this.DisplayRectangle.Width - 10;
            _listViewDatabase.Height = this.DisplayRectangle.Height - 10;
        }

        private void _listViewDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void _linkDatabaseSearch_Load(object sender, EventArgs e)
        {

        }
    }

    public delegate void searchDatabaseDoubleClickEventHandler(object sender, string e);

}