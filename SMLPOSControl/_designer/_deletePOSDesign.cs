﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSControl._designer
{
    public partial class _deletePOSDesign : Form
    {
        public _deletePOSDesign()
        {
            InitializeComponent();
            this.Load += new EventHandler(_deletePOSDesign_Load);
        }

        void _deletePOSDesign_Load(object sender, EventArgs e)
        {
            this._myDataList1._buttonSelectAll.Enabled = false;
            this._myDataList1._buttonNewFromTemp.Enabled = false;
            this._myDataList1._buttonNew.Enabled = false;
            this._myDataList1._buttonClose.Click += new EventHandler(_buttonClose_Click);


            this._myDataList1._gridData._isEdit = false;
            //this._myDataList1._mainMenuId = MyLib._myGlobal._mainMenuIdPassTrue;
            //this._myDataList1._mainMenuCode = MyLib._myGlobal._mainMenuCodePassTrue;
            //this._myDataList1._editMode = true;
            this._myDataList1._lockRecord = true;
            this._myDataList1._loadViewFormat("screen_posdesign_loaddesign", MyLib._myGlobal._userSearchScreenGroup, true);
            this._myDataList1._referFieldAdd(_g.d.sml_posdesign._screen_code, 1);

            this._myDataList1._deleteData += new MyLib.DeleteDataEventHandler(_myDataList1__deleteData);

        }

        void _myDataList1__deleteData(System.Collections.ArrayList selectRowOrder)
        {
            MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();

            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
            {
                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _myDataList1._tableName, getData.whereString));
            } // for
            __myQuery.Append("</node>");
            string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(1, MyLib._myGlobal._resource("สำเร็จ"));
                _myDataList1._refreshData();
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
