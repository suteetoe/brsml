using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLReport._formReport
{
    public partial class _queryExecuteForm : Form
    {
        public string __query = "";

        public _queryExecuteForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(_queryExecuteForm_Load);
        }

        void _queryExecuteForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                DataSet result = myFrameWork._query(MyLib._myGlobal._databaseName, this.__query);
                this._dataGridView.DataSource = result;
                this._dataGridView.DataMember = "Row";
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                // Debugger.Break();
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
