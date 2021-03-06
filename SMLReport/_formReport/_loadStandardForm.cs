﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLReport._formReport
{
    public partial class _loadStandardForm : Form
    {
        public _loadStandardForm()
        {
            InitializeComponent();

            this.Load += new EventHandler(_loadStandardForm_Load);
        }

        void _loadStandardForm_Load(object sender, EventArgs e)
        {
            this._dataGrid._addColumn("รหัสฟอร์ม", 1, 50, 20, false);
            this._dataGrid._addColumn("ชื่อฟอร์ม", 1, 50, 80, false);
            this._dataGrid.IsEdit = false;

            try
            {
                MyLib._myFrameWork __fw = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);

                string __query = "select " + _g.d.formdesign._formguid_code + ", " + _g.d.formdesign._formcode + ", " + _g.d.formdesign._formname + ", " + _g.d.formdesign._timeupdate + " from " + _g.d.formdesign._table + " order by " + _g.d.formdesign._formcode;

                DataSet __result = __fw._query(MyLib._myGlobal._masterDatabaseName, __query);

                DataTable __table = __result.Tables[0];


                for (int __i = 0; __i < __table.Rows.Count; __i++)
                {
                    int __row = this._dataGrid._addRow();
                    this._dataGrid._cellUpdate(__i, 0, __table.Rows[__i][_g.d.formdesign._formcode], false);
                    this._dataGrid._cellUpdate(__i, 1, __table.Rows[__i][_g.d.formdesign._formname], false);
                    //this._dataGrid._cellUpdate(__i, 2, __table.Rows[__i][_g.d.formdesign._timeupdate], false);
                }


                this._dataGrid._mouseDoubleClick += (s1, e1) =>
                {
                    if (e1._row != -1)
                    {
                        if (_selectFormStandard != null)
                        {
                            _selectFormStandard(this, __table.Rows[e1._row][_g.d.formdesign._formcode].ToString());
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

        public event AfterSelectFormStandard _selectFormStandard;
    }

    public delegate void AfterSelectFormStandard(object sender, string formDesignGuid);

}
