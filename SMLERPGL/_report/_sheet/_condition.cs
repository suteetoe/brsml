using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGL._report._sheet
{
    public partial class _condition : Form
    {
        public _condition()
        {
            InitializeComponent();

            this._selectBranchControl.Visible = false;
            this.splitContainer1.SplitterDistance = this.Width - 1;

        }

        private void _buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _condition_Load(object sender, EventArgs e)
        {

        }

        private void _selectBranchButton_Click(object sender, EventArgs e)
        {
            if (this._selectBranchControl.Visible == false)
            {
                this._selectBranchControl.Visible = true;
                this.Width = (int)(this.Width * 1.5);
            }
            this.splitContainer1.SplitterDistance = this.Width / 2;
        }

        private void _branchShow_CheckedChanged(object sender, EventArgs e)
        {
            if (this._selectBranchControl.Visible == false)
            {
                this._selectBranchControl.Visible = true;
                this.Width = (int)(this.Width * 1.5);
            }
            this.splitContainer1.SplitterDistance = this.Width / 2;
        }

        private void _selectAllButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._selectBranchControl._gridBranch._rowData.Count; __row++)
            {
                this._selectBranchControl._gridBranch._cellUpdate(__row, 0, 1, true);
            }
            this._selectBranchControl._gridBranch.Invalidate();

        }

        private void _selectNoneButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._selectBranchControl._gridBranch._rowData.Count; __row++)
            {
                this._selectBranchControl._gridBranch._cellUpdate(__row, 0, 0, true);
            }
            this._selectBranchControl._gridBranch.Invalidate();

        }
    }
}