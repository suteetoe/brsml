using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLTransferDataPOS
{
    public partial class _mainServerSetup : Form
    {
        

        public _mainServerSetup()
        {
            InitializeComponent();

            this._screen_server_config._maxColumn = 1;
            this._screen_server_config._table_name = _g.d.erp_data_center._table;
            this._screen_server_config._addTextBox(0, 0, _g.d.erp_data_center._datacenter_server, 0);
            this._screen_server_config._addTextBox(1, 0, _g.d.erp_data_center._datacenter_provider_name, 0);
            this._screen_server_config._addComboBox(2, 0, _g.d.erp_data_center._datacenter_database_type, true, new string[] { _g.d.erp_data_center._postgresql, _g.d.erp_data_center._mysql, _g.d.erp_data_center._sql_server_2000, _g.d.erp_data_center._sql_server_2005, _g.d.erp_data_center._oracle, _g.d.erp_data_center._firebird  }, true);
            this._screen_server_config._addTextBox(3, 0, _g.d.erp_data_center._datacenter_database_name, 0);

            this.Load += new EventHandler(_mainServerSetup_Load);
        }

        void _mainServerSetup_Load(object sender, EventArgs e)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __result = __myFrameWork._queryShort("select * from " + _g.d.erp_data_center._table);

            this._screen_server_config._loadData(__result.Tables[0]);
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            _save_data();
        }

        void _save_data()
        {
            ArrayList __getData = this._screen_server_config._createQueryForDatabase();

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.erp_data_center._table));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + this._screen_server_config._table_name + "(" + __getData[0]  + ") values (" + __getData[1] + ")"));
            __query.Append("</node>");

            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());

            if (__result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(1, "บันทึกข้อมูลสำเร็จ ระบบจะปิดจอนี้ให้โดยอัตโนมัติ");
                this.Close();
            }
            else
            {
                MessageBox.Show(__result, "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
