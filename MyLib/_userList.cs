using System;
using System.Data;
using System.Windows.Forms;

namespace MyLib
{
    public partial class _userList : UserControl
    {
        public _userList()
        {
            InitializeComponent();
            this._userListGrid._isEdit = false;
            this._userListGrid._width_by_persent = true;
            this._userListGrid._addColumn("user_code", 1, 100, 15);
            this._userListGrid._addColumn("user_name", 1, 100, 25);
            this._userListGrid._addColumn("computer_name", 1, 100, 20);
            this._userListGrid._addColumn("login_time", 1, 100, 20);
            this._userListGrid._addColumn("database_code", 1, 100, 10);

            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
            {

            }
            else
            {
                this._userListGrid._addColumn("capture", 1, 100, 10);
            }

            this._userListGrid._addColumn("guid_code", 1, 100, 10, false, true, true);
            this._userListGrid._mouseClick += new MouseClickHandler(_userListGrid__mouseClick);
            this._getUserList();
            this._userListGrid._calcPersentWidthToScatter();
            this.Invalidate();
        }

        void _userListGrid__mouseClick(object sender, GridCellEventArgs e)
        {
            if (e._columnName.Equals("capture"))
            {
                string __guid = this._userListGrid._cellGet(e._row, "guid_code").ToString();
                _screenCaptureForm __form = new _screenCaptureForm(__guid);
                __form.Show();
            }
        }

        private void _getUserList()
        {
            MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
            DataSet _userList = _myFrameWork._query(MyLib._myGlobal._mainDatabase, "select user_code,(select user_name from sml_user_list where " + _myGlobal._addUpper("sml_user_list.user_code") + "=" + _myGlobal._addUpper("sml_guid.user_code") + ") as user_name,computer_name,database_code,login_time,'Capture' as capture,guid_code  from sml_guid order by login_time");
            if (_userList.Tables.Count > 0)
            {
                this._userListGrid._clear();
                this._userListGrid._loadFromDataTable(_userList.Tables[0]);
            }
        }

        private void _buttonExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _buttonRefresh_Click(object sender, EventArgs e)
        {
            this._getUserList();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this._getUserList();
        }
    }
}
