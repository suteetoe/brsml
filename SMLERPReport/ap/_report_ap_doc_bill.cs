using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport.ap
{
    public partial class _report_ap_doc_bill : UserControl
    {
        public _report_ap_doc_bill()
        {
            InitializeComponent(); 
            
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (Keys.Escape == keyData)
            {
                this.Dispose();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);

        }


    }
}
