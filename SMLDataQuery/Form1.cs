using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMLDataQuery
{
    public partial class Form1 : Form
    {
        MyLib._myFrameWork _myFrameWork;
        public Form1()
        {
            InitializeComponent();
            this._myFrameWork = new MyLib._myFrameWork();
            _getCompanyName();
        }

        void _getCompanyName()
        {
            DataTable __result = _myFrameWork._queryShort("select * from erp_company_profile").Tables[0];
            if (__result.Rows.Count > 0)
            {
                this.Text = __result.Rows[0]["company_name_1"].ToString();
            }

        }

        private void _processButton_Click(object sender, EventArgs e)
        {

            string query = _queryTextbox.Text.ToUpper();
            if (query.Length > 7)
            {
                string __computerName = SystemInformation.ComputerName.ToLower();
                if ((query.IndexOf("INSERT") != -1 || query.IndexOf("UPDATE") != -1 || query.IndexOf("DELETE") != -1 || query.IndexOf("DROP") != -1 || query.IndexOf("TRUNCATE") != -1))
                {
                    MessageBox.Show(MyLib._myGlobal._resource("warning20"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //for execute

                }
                else
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                        DataSet result = myFrameWork._query(MyLib._myGlobal._databaseName, _queryTextbox.Text);
                        _resultGrid.DataSource = result;
                        _resultGrid.DataMember = "Row";
                        this.Cursor = Cursors.Default;
                    }
                    catch (Exception ex)
                    {
                        // Debugger.Break();
                        this.Cursor = Cursors.Default;
                        MessageBox.Show(MyLib._myGlobal._resource("warning20") + "\n" + ex.Message, MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void _executeButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure ??", "Sure", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                string query = _queryTextbox.Text.ToUpper();

                if ((query.IndexOf("INSERT") != -1 || query.IndexOf("UPDATE") != -1 || query.IndexOf("DELETE") != -1 || query.IndexOf("DROP") != -1 || query.IndexOf("TRUNCATE") != -1))
                {
                    //for execute
                    string result = _myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, _queryTextbox.Text);
                    this._resultLabel.Text = (result.Length > 0) ? result : "success";
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    MessageBox.Show("Query Selec Only");
                }
            }
        }
    }
}
