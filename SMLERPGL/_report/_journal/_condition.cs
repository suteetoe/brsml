using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGL._report._journal
{
    public partial class _condition : Form
    {
        public _condition()
        {
            InitializeComponent();
            this._selectBook.Visible = false;
            this.splitContainer1.SplitterDistance = this.Width - 1;
            this._buttonOk.Click += new EventHandler(_buttonOk_Click);
            try
            {
                int __period = _g.g._accountPeriodFind(MyLib._myGlobal._workingDate) - 1;
                this._screenJournalCondition1._setDataDate(_g.d.gl_resource._date_begin, _g.g._accountPeriodDateBegin[__period]);
                this._screenJournalCondition1._setDataDate(_g.d.gl_resource._date_end, _g.g._accountPeriodDateEnd[__period]);
            }
            catch
            {
            }
        }

        void _buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _condition_Load(object sender, EventArgs e)
        {
        }

        private void _buttonOk_Click_1(object sender, EventArgs e)
        {
        }

        private void _bookButton_Click(object sender, EventArgs e)
        {
            if (this._selectBook.Visible == false)
            {
                this._selectBook.Visible = true;
                this.Width = (int)(this.Width * 1.5);
            }
            this.splitContainer1.SplitterDistance = this.Width / 2;
        }

        private void _selectBranchButton_Click(object sender, EventArgs e)
        {

        }

        private void _selectAllButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._selectBook._bookGrid._rowData.Count; __row++)
            {
                this._selectBook._bookGrid._cellUpdate(__row, 0, 1, true);
            }
            this._selectBook._bookGrid.Invalidate();
        }

        private void _selectNoneButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._selectBook._bookGrid._rowData.Count; __row++)
            {
                this._selectBook._bookGrid._cellUpdate(__row, 0, 0, true);
            }
            this._selectBook._bookGrid.Invalidate();

        }
    }
}