using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPickAndPack
{
    public partial class _setupMachineUserControl : UserControl
    {
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();

        public _setupMachineUserControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            this._grid._table_name = _g.d.pp_config._table;
            this._grid._addColumn(_g.d.pp_config._serial_id, 1, 0, 20);
            this._grid._addColumn(_g.d.pp_config._wh_code, 1, 0, 20);
            this._grid._addColumn(_g.d.pp_config._location_list, 1, 0, 50);
            this._grid._addColumn(_g.d.pp_config._auto_mode, 11, 0, 10);
            //            
            this._grid._loadFromDataTable(this._myFrameWork._queryShort("select * from " + _g.d.pp_config._table + " order by " + _g.d.pp_config._wh_code).Tables[0]);
            this._grid.Invalidate();
            this._grid._queryForInsertCheck += (MyLib._myGrid sender, int row) =>
            {
                return (sender._cellGet(row,_g.d.pp_config._serial_id).ToString().Trim().Length > 0);
            };
        }

        private void _buttonExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            this._grid.Invalidate();
            this._grid._updateRowIsChangeAll(true);
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.pp_config._table));
            __myQuery.Append(this._grid._createQueryForInsert(_g.d.pp_config._table, "", ""));
            __myQuery.Append("</node>");
            string __result = this._myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (__result.Length == 0)
            {
                MessageBox.Show("Save Success");
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Error : " + __result.ToString());
            }
        }
    }
}
