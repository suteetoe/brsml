using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyLib._databaseManage
{
	public partial class _shrinkDatabase : MyLib._myForm
    {
        public _shrinkDatabase()
		{
            InitializeComponent();
            _listViewDatabase.View = View.List;
            _listViewDatabase.CheckBoxes = true;
            // ดึงฐานข้อมูลทั้งหมด
            _myFrameWork myFrameWork = new _myFrameWork();
            string query = "select " + MyLib._d.sml_database_list._data_group + "," + MyLib._d.sml_database_list._data_code + "," + MyLib._d.sml_database_list._data_name + "," + MyLib._d.sml_database_list._data_database_name + " from " + MyLib._d.sml_database_list._table;
            DataSet result = myFrameWork._query(MyLib._myGlobal._mainDatabase, query);
            DataRow[] getRows = result.Tables[0].Select();
            for (int row = 0; row < getRows.Length; row++)
            {
                ListViewItem item = new ListViewItem();
                item.Name = getRows[row].ItemArray[3].ToString();
                item.Text = getRows[row].ItemArray[3].ToString() + " (Group:[" + getRows[row].ItemArray[0].ToString() + "],Code:[" + getRows[row].ItemArray[1].ToString() + "],Name:[" + getRows[row].ItemArray[2].ToString() + "]";
                item.ImageIndex = 0;
                _listViewDatabase.Items.Add(item);
            } // for
            _listViewDatabase._setExStyles();
        }

        private void _shrinkDatabase_Load(object sender, EventArgs e)
        {
        }

        private void _viewByIcon_CheckedChanged(object sender, EventArgs e)
        {
            _listViewDatabase.View = View.LargeIcon;
        }

        private void _viewByList_CheckedChanged(object sender, EventArgs e)
        {
            _listViewDatabase.View = View.List;
        }

        private void _buttonSelectAll_Click(object sender, EventArgs e)
        {
            for (int __loop = 0; __loop < _listViewDatabase.Items.Count; __loop++)
            {
                _listViewDatabase.Items[__loop].Checked = true;
            } // for
        }

        private void _buttonStart_Click(object sender, EventArgs e)
        {
            int __totalDatabase = 0;
            for (int __loop = 0; __loop < _listViewDatabase.Items.Count; __loop++)
            {
                if (_listViewDatabase.Items[__loop].Checked)
                {
                    __totalDatabase++;
                }
            } // for

            if (__totalDatabase == 0)
            {
                // ยังไม่ได้เลือกข้อมูลที่ต้องการ Shrink กรุณาเลือกก่อน
                MessageBox.Show(MyLib._myGlobal._resource("warning25"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                //   --  
                string __message = String.Format(MyLib._myGlobal._resource("warning27"),__totalDatabase.ToString());
                DialogResult __result = MessageBox.Show(__message, MyLib._myGlobal._resource("warning"), MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (__result == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    _progressBarDatabase.Value = 0;
                    _resultTextBox.Text = "";
                    _myFrameWork __myFrameWork = new _myFrameWork();
                    // start
                    int __databaseCount = 0;
                    int __errorCount = 0;
                    for (int __databaseLoop = 0; __databaseLoop < _listViewDatabase.Items.Count; __databaseLoop++)
                    {
                        if (_listViewDatabase.Items[__databaseLoop].Checked)
                        {
                            string __databaseName = _listViewDatabase.Items[__databaseLoop].Name;
                            __databaseCount++;
                            this._progressTextDatabase.Text = "Shrink Database (" + __databaseCount + "/" + __totalDatabase + ") : [" + __databaseName + "]";
                            _progressTextDatabase.Invalidate();
                            this._progressBarDatabase.Value = ((__databaseCount * 100) / (__totalDatabase));
                            this._progressBarDatabase.Invalidate();
                            this._resultTextBox.Text += "Database [" + __databaseName + "] start\n";
                            this._resultTextBox.Invalidate();
                            //
                            string __myQuery = MyLib._myGlobal._xmlHeader + "<node>";
                            __myQuery += "<query>backup log " + __databaseName + " with no_log</query>";
                            __myQuery += "<query>backup log " + __databaseName + " with truncate_only</query>";
                            __myQuery += "<query>dbcc shrinkdatabase (" + __databaseName + ",1)</query>";
                            __myQuery += "</node>";
                            string __queryResult = __myFrameWork._queryList("", __myQuery); // database name ว่าง คือ connect แบบไม่ต้องระบุ database
                            if (__queryResult.Length > 0)
                            {
                                //  --  
                                __message = String.Format(MyLib._myGlobal._resource("warning27"),__queryResult);
                                MessageBox.Show(__message, MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                __errorCount++;
                            }
                        }
                    }
                    this.Cursor = Cursors.Default;
                    // --  
                    __message = String.Format(MyLib._myGlobal._resource("warning28"),__totalDatabase,__errorCount);
                    MessageBox.Show(__message, MyLib._myGlobal._resource("success"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void _buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}