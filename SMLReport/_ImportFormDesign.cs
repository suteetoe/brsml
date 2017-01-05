using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SMLReport
{
    public partial class _ImportFormDesign : UserControl
    {
        public _ImportFormDesign()
        {
            InitializeComponent();
            this.Load += new EventHandler(_ImportFormDesign_Load);
        }

        void _ImportFormDesign_Load(object sender, EventArgs e)
        {

            this._dataGrid._addColumn("check", 11, 0, 10, false, false, false, false);
            this._dataGrid._addColumn("รหัสฟอร์ม", 1, 50, 20, false);
            this._dataGrid._addColumn("ชื่อฟอร์ม", 1, 50, 70, false);
            this._dataGrid.IsEdit = false;

            MyLib._myFrameWork __fw = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);

            string __query = "select " + _g.d.formdesign._formguid_code + ", " + _g.d.formdesign._formcode + ", " + _g.d.formdesign._formname + ", " + _g.d.formdesign._timeupdate + " from " + _g.d.formdesign._table + " order by " + _g.d.formdesign._formcode;

            DataSet __result = __fw._query(MyLib._myGlobal._masterDatabaseName, __query);

            if (__result.Tables.Count > 0)
            {
                DataTable __table = __result.Tables[0];

                for (int __i = 0; __i < __table.Rows.Count; __i++)
                {
                    int __row = this._dataGrid._addRow();
                    //this._dataGrid._cellUpdate(__i, 0, __table.Rows[__i][_g.d.formdesign._formcode], false);
                    this._dataGrid._cellUpdate(__row, 1, __table.Rows[__i][_g.d.formdesign._formcode], false);
                    this._dataGrid._cellUpdate(__row, 2, __table.Rows[__i][_g.d.formdesign._formname], false);
                }

                this._dataGrid.Invalidate();
            }
        }

        private void _selectAllButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._dataGrid._rowData.Count; __row++)
            {
                this._dataGrid._cellUpdate(__row, 0, 1, false);
            }
            this._dataGrid.Invalidate();

        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        string _progressMessage = "";
        int _progressMax = 0;
        int _progressCount = 0;

        private void _buttonProcess_Click(object sender, EventArgs e)
        {
            this._progressCount = 0;
            this._progressMax = 0;
            for (int __row = 0; __row < this._dataGrid._rowData.Count; __row++)
            {
                int __checked = (int)this._dataGrid._cellGet(__row, 0);
                if (__checked == 1)
                {
                    this._progressMax++;
                }
            }
            this._progressbar.Visible = true;
            this._progressbar.Maximum = this._progressMax;
            this.Enabled = false;
            Thread __new = new Thread(__process);
            __new.Start();
            this._timer.Start();

        }

        void __process()
        {
            // check save replace confirm
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                MyLib._myFrameWork __smlWebService = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                for (int __row = 0; __row < this._dataGrid._rowData.Count; __row++)
                {
                    int __checked = (int)this._dataGrid._cellGet(__row, 0);
                    if (__checked == 1)
                    {
                        string __formCode = (String)this._dataGrid._cellGet(__row, 1);
                        string __formName = (String)this._dataGrid._cellGet(__row, 2);

                        _progressMessage = __formCode + "," + __formName;

                        // check replace 
                        String __checkForm = "select count(" + _g.d.formdesign._formcode + ") as codecount from " + _g.d.formdesign._table + "  where upper(" + _g.d.formdesign._formcode + ") = upper('" + __formCode + "')";
                        DataSet __checkResult = __myFrameWork._query(MyLib._myGlobal._databaseName, __checkForm);

                        if (__checkResult.Tables.Count > 0)
                        {
                            if (MyLib._myGlobal._decimalPhase(__checkResult.Tables[0].Rows[0]["codecount"].ToString()) > 0)
                            {
                                //MessageBox.Show("แบบฟอร์ม " + __formCode + "," + __formName + " \nได้มีการนำเข้าสู่ระบบไปแล้ว\nคุณต้องการที่จะเขียนทับเลยหรือไม่", "เตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (MessageBox.Show("แบบฟอร์ม : " + __formCode + "," + __formName + " \nได้มีการนำเข้าสู่ระบบไปแล้ว\nคุณต้องการที่จะเขียนทับเลยหรือไม่", "เตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    continue;
                                }
                            }
                        }


                        // import 

                        MyLib._myFrameWork __fw = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                        String __query = "select " + _g.d.formdesign._formcode + ", " + _g.d.formdesign._formguid_code + ", " + _g.d.formdesign._formname + ", encode(" + _g.d.formdesign._formdesigntext + ", 'base64') as " + _g.d.formdesign._formdesigntext + ", encode(" + _g.d.formdesign._formbackground + ", 'base64') as " + _g.d.formdesign._formbackground + " from " + _g.d.formdesign._table + " where " + _g.d.formdesign._formcode + " = '" + __formCode.ToString().ToUpper() + "' ";
                        DataSet __result = __fw._query(MyLib._myGlobal._masterDatabaseName, __query);

                        DataTable __table = __result.Tables[0];
                        if (__table.Rows.Count > 0)
                        {
                            string __saveFormResult = __myFrameWork._saveFormDesign(MyLib._myGlobal._databaseName, "0", __table.Rows[0][_g.d.formdesign._formcode].ToString().ToUpper(), Guid.NewGuid().ToString().ToUpper(), __table.Rows[0][_g.d.formdesign._formname].ToString().ToUpper(), Convert.FromBase64String(__table.Rows[0][_g.d.formdesign._formdesigntext].ToString()), Convert.FromBase64String(__table.Rows[0][_g.d.formdesign._formbackground].ToString()));
                        }

                        this._progressCount++;
                    }
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
                MessageBox.Show(this._progressMessage);
                this._progressMessage = "";
                this._progressbar.Visible = false;
                this.Enabled = true;
            }

        }
    }
}
