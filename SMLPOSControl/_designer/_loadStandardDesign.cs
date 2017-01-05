using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSControl._designer
{
    public partial class _loadStandardDesign : Form
    {
        public event _afterGridSelect _afterSelect;

        public _loadStandardDesign()
        {
            InitializeComponent();
            this.Load += new EventHandler(_loadStandardDesign_Load);
        }

        void _loadStandardDesign_Load(object sender, EventArgs e)
        {
            this._dataGrid._addColumn("รหัสจอขาย", 1, 50, 20, false);
            this._dataGrid._addColumn("ชื่อจอขาย", 1, 50, 80, false);
            this._dataGrid.IsEdit = false;

            try
            {
                MyLib._myFrameWork __fw = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);

                string __query = "select " + _g.d.sml_posdesign._screen_code + ", " + _g.d.sml_posdesign._screen_name + " from " + _g.d.sml_posdesign._table + " order by " + _g.d.sml_posdesign._screen_code;

                DataSet __result = __fw._query(MyLib._myGlobal._masterDatabaseName, __query);

                DataTable __table = __result.Tables[0];


                for (int __i = 0; __i < __table.Rows.Count; __i++)
                {
                    int __row = this._dataGrid._addRow();
                    this._dataGrid._cellUpdate(__i, 0, __table.Rows[__i][_g.d.sml_posdesign._screen_code], false);
                    this._dataGrid._cellUpdate(__i, 1, __table.Rows[__i][_g.d.sml_posdesign._screen_name], false);
                    //this._dataGrid._cellUpdate(__i, 2, __table.Rows[__i][_g.d.formdesign._timeupdate], false);
                }


                this._dataGrid._mouseDoubleClick += (s1, e1) =>
                {
                    if (e1._row != -1)
                    {
                        if (_afterSelect != null)
                        {
                            _afterSelect(this, __table.Rows[e1._row][_g.d.sml_posdesign._screen_code].ToString());
                            this.Close();
                        }
                    }
                };

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "warning");
            }

        }
    }

    public delegate void _afterGridSelect(Object sender, string _formCode);
}
