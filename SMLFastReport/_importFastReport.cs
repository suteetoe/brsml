using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;

namespace SMLFastReport
{
    public partial class _importFastReport : UserControl
    {
        int _mode = 0;
        string _reportDatabaseName = MyLib._myGlobal._masterDatabaseName;


        /// <summary>
        /// 0 
        /// </summary>
        public int mode
        {
            get
            {
                return this._mode;
            }
            set
            {
                this._mode = value;
                if(this._mode== 1)
                {
                    _reportDatabaseName = "smlreportbranch";
                }
                else
                {
                    _reportDatabaseName = MyLib._myGlobal._masterDatabaseName;
                }
            }
        }

        public _importFastReport()
        {
            InitializeComponent();
            this.Load += new EventHandler(_importFastReport_Load);
            this._dataGrid._beforeDisplayRendering += new MyLib.BeforeDisplayRenderRowEventHandler(_dataGrid__beforeDisplayRendering);
        }

        MyLib.BeforeDisplayRowReturn _dataGrid__beforeDisplayRendering(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, System.Collections.ArrayList rowData, Graphics e)
        {
            //throw new NotImplementedException();
            MyLib.BeforeDisplayRowReturn __result = senderRow;
            if (row < sender._rowData.Count && columnNumber == 2)
            {
                if (sender._cellGet(row, 2) == null)
                {
                    ((ArrayList)senderRow.newData)[columnNumber] = MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._inventory_report)._str;
                }
                else
                {
                    int __mode = (int)MyLib._myGlobal._decimalPhase(sender._cellGet(row, 2).ToString());
                    //int __select = (int)MyLib._myGlobal._decimalPhase(sender._cellGet(row, sender._findColumnByName(this._gridSelect)).ToString());
                    ((ArrayList)senderRow.newData)[columnNumber] = "";
                    //if (__select == 1)
                    //{
                    switch (__mode)
                    {
                        case 1:
                            ((ArrayList)senderRow.newData)[columnNumber] = MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._inventory_report)._str;
                            break;
                        case 2:
                            ((ArrayList)senderRow.newData)[columnNumber] = MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._purchase_report)._str;
                            break;
                        case 3:
                            ((ArrayList)senderRow.newData)[columnNumber] = MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._sale_report)._str;
                            break;
                        case 4:
                            ((ArrayList)senderRow.newData)[columnNumber] = MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._supplier_report)._str;
                            break;
                        case 5:
                            ((ArrayList)senderRow.newData)[columnNumber] = MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._customer_report)._str;
                            break;
                        case 6:
                            ((ArrayList)senderRow.newData)[columnNumber] = MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._cashbank_report)._str;
                            break;
                        case 7:
                            ((ArrayList)senderRow.newData)[columnNumber] = MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._account_report)._str;
                            break;
                        default:
                            ((ArrayList)senderRow.newData)[columnNumber] = MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._general_report)._str;
                            break;
                    }
                }
                //}
            }
            return __result;
        }

        void _importFastReport_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            this._dataGrid._addColumn("รหัสรายงาน", 1, 255, 30, false);
            this._dataGrid._addColumn("ชื่อรายงาน", 1, 255, 40, false);
            this._dataGrid._addColumn("ประเภทรายงาน", 1, 255, 30, false);
            this._dataGrid.IsEdit = false;

            MyLib._myFrameWork __fw = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
            string __getWhere = " where coalesce(" + _g.d.sml_fastreport._owner_code + ", '' ) =\'\' or ( " + _g.d.sml_fastreport._owner_code + "=\'" + MyLib._myGlobal._productCode + "\') ";
            string __query = "select " + _g.d.sml_fastreport._menuid + ", " + _g.d.sml_fastreport._menuname + ", " + _g.d.sml_fastreport._report_type + " from " + _g.d.sml_fastreport._table + __getWhere + " order by " + _g.d.sml_fastreport._menuid;

            DataSet __result = __fw._query(_reportDatabaseName, __query);

            if (__result.Tables.Count > 0)
            {
                DataTable __table = __result.Tables[0];

                for (int __i = 0; __i < __table.Rows.Count; __i++)
                {
                    int __row = this._dataGrid._addRow();
                    //this._dataGrid._cellUpdate(__i, 0, __table.Rows[__i][_g.d.formdesign._formcode], false);
                    this._dataGrid._cellUpdate(__row, 0, __table.Rows[__i][_g.d.sml_fastreport._menuid], false);
                    this._dataGrid._cellUpdate(__row, 1, __table.Rows[__i][_g.d.sml_fastreport._menuname], false);
                    this._dataGrid._cellUpdate(__row, 2, __table.Rows[__i][_g.d.sml_fastreport._report_type], false);
                }

                this._dataGrid.Invalidate();
            }

        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        string _progressMessage = "";
        int _progressMax = 0;
        int _progressCount = 0;
        int _successCount = 0;

        private void _buttonProcess_Click(object sender, EventArgs e)
        {
            this._progressCount = 0;
            this._progressMax = 0;
            this._successCount = 0;

            // check password
            _passwordProtectForm __passwordBox = new _passwordProtectForm();
            __passwordBox.Text = "Password Confirm";
            __passwordBox._beforeClose += (s1, e1) =>
            {
                // password ?
                string __pass = __passwordBox._dialogScreen._getDataStr("_password_importfastreport");
                if (__passwordBox.DialogResult == DialogResult.Yes && __pass != null && __pass.Equals("smladmin"))
                {
                    for (int __row = 0; __row < this._dataGrid._rowData.Count; __row++)
                    {
                        string __menuId = (string)this._dataGrid._cellGet(__row, 0);
                        if (__menuId != null)
                        {
                            this._progressMax++;
                        }
                    }
                    this._progressbar.Visible = true;
                    this._progressbar.Maximum = this._progressMax;
                    this.Enabled = false;
                    Thread __new = new Thread(_process);
                    __new.Start();
                    this._timer.Start();
                }
                else
                {
                    MessageBox.Show("รหัสผ่านไม่ถูกต้อง", "Warning");
                }
            };

            __passwordBox.ShowDialog();
        }

        private void _process()
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                //MyLib._myFrameWork __smlWebService = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                for (int __row = 0; __row < this._dataGrid._rowData.Count; __row++)
                {
                    string __reportId = (String)this._dataGrid._cellGet(__row, 0);
                    string __reportName = (String)this._dataGrid._cellGet(__row, 1);
                    int __reportType = (int)MyLib._myGlobal._decimalPhase(this._dataGrid._cellGet(__row, 2).ToString());

                    _progressMessage = __reportId + "," + __reportName;

                    // remove old
                    string _removeSql = string.Format("delete from " + _g.d.sml_fastreport._table + " where UPPER(" + _g.d.sml_fastreport._menuid + ") = '{0}'", __reportId.ToString().ToUpper());
                    __myFrameWork._query(MyLib._myGlobal._databaseName, _removeSql);

                    // import 

                    // load report from SMLMASTER
                    MyLib._myFrameWork __fw = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                    string __query = string.Format("select " + _g.d.sml_fastreport._reportdata + " from " + _g.d.sml_fastreport._table + " where UPPER(" + _g.d.sml_fastreport._menuid + ") = '{0}'", __reportId.ToUpper());
                    Byte[] __result = __fw._queryByte(_reportDatabaseName, __query);

                    //DataTable __table = __result.Tables[0];

                    string __insQuery = string.Format("insert into " + _g.d.sml_fastreport._table + "(" + _g.d.sml_fastreport._menuid + "," + _g.d.sml_fastreport._menuname + ", " + _g.d.sml_fastreport._report_type + "," + _g.d.sml_fastreport._reportdata + ") VALUES('{0}','{1}', {2} ,?)", __reportId.ToUpper(), __reportName, __reportType);
                    string __insResult = __myFrameWork._queryByteData(MyLib._myGlobal._databaseName, __insQuery, new object[] { __result });

                    if (__insResult.Equals(""))
                    {
                        this._successCount++;
                    }

                    this._progressCount++;
                }

                this._progressMessage = "Success";
            }
            catch (Exception __ex)
            {
                this._progressMessage = __ex.Message.ToString() + " : " + __ex.StackTrace.ToString();
            }
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            this._progressbar.Value = this._progressCount;
            this._progressbar.Invalidate();
            this._statusLabel.Text = this._progressMessage;
            this._statusLabel.Invalidate();
            if (this._progressMessage.Equals("Success"))
            {
                this._timer.Stop();
                MessageBox.Show(string.Format("นำเข้ารายงานทั้งหมด {0} รายงาน \n นำเข้าสำเร็จ {0} รายงาน", this._dataGrid._rowData.Count, this._successCount));
                this._progressMessage = "";
                this._progressbar.Visible = false;
                this.Enabled = true;
            }
        }
    }

    class _passwordProtectForm : MyLib._myDialogForm
    {
        public _passwordProtectForm()
        {
            this._dialogScreen._maxColumn = 1;
            this._buttonOk.ButtonText = " OK   ";
            this._dialogScreen._addTextBox(0, 0, 1, 0, "_password_importfastreport", 1, 1, 0, true, false, false, false, true, "รหัสผ่าน");
            Control __textBox = this._dialogScreen._getControl("_password_importfastreport");
            if (__textBox != null && __textBox.GetType() == typeof(MyLib._myTextBox))
            {
                MyLib._myTextBox __tb = (MyLib._myTextBox)__textBox;
                __tb.textBox.PasswordChar = '*';
            }

            this._dialogScreen.Invalidate();
        }
    }

}
