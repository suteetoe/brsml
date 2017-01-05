using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace MyLib._databaseManage
{
    public partial class _queryDataView : UserControl
    {
        public _queryDataView()
        {
            InitializeComponent();
            _textBoxQuery.Focus();
            _textBoxQuery.Select(_textBoxQuery.Text.Length, 1);

        }

        private void _buttonProcess_Click(object sender, EventArgs e)
        {
            string query = _textBoxQuery.Text.ToUpper();
            if (query.Length > 7)
            {
                string __computerName = SystemInformation.ComputerName.ToLower();
                if ((query.IndexOf("INSERT") != -1 || query.IndexOf("UPDATE") != -1 || query.IndexOf("DELETE") != -1 || query.IndexOf("DROP") != -1 || query.IndexOf("TRUNCATE") != -1) && (__computerName.IndexOf("toe-pc") == -1) )
                {
                    MessageBox.Show(MyLib._myGlobal._resource("warning20"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                        DataSet result = myFrameWork._query(MyLib._myGlobal._databaseName, _textBoxQuery.Text);
                        dataGridView1.DataSource = result;
                        dataGridView1.DataMember = "Row";
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void _buttonExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
