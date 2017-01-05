using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGL._chart
{
    public partial class _chartOfAccountProcessFlow : Form
    {
        public _chartOfAccountProcessFlow()
        {
            InitializeComponent();
        }

        private void _chartOfAccountProcessFlow_Load(object sender, EventArgs e)
        {
            this.Show();
            this.Refresh();
            __startProcess();
            this.Close();
        }

        public void __startProcess()
        {
            SMLProcess._glProcess._glUpdateAll();
            SMLProcess._smlProcess __smlFrameWork = new SMLProcess._smlProcess();
            __smlFrameWork._glReFormatChartOfAccount();
        }
    }
}