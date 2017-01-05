using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLFastReport
{
    public partial class _deleteForm : MyLib._myForm
    {
        public _deleteForm()
        {
            InitializeComponent();

            this._myDataList._buttonSelectAll.Enabled = false;
            this._myDataList._buttonNewFromTemp.Enabled = false;
            this._myDataList._buttonNew.Enabled = false;
            this._myDataList._buttonClose.Click += new EventHandler(_buttonClose_Click);


            this._myDataList._gridData._isEdit = false;

            this._myDataList._lockRecord = true;
            this._myDataList._loadViewFormat("screen_fastreport_loadreport", MyLib._myGlobal._userSearchScreenGroup, true);
            this._myDataList._referFieldAdd(_g.d.sml_fastreport._menuid, 1);

            this._myDataList._deleteData += new MyLib.DeleteDataEventHandler(_myDataList__deleteData);

        }

        void _myDataList__deleteData(System.Collections.ArrayList selectRowOrder)
        {
            MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();

            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
            {
                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _myDataList._tableName, getData.whereString));
            } // for
            __myQuery.Append("</node>");
            string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(1, MyLib._myGlobal._resource("สำเร็จ"));
                _myDataList._refreshData();
            }
            else
            {
                MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    this.Close();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


    }
}
