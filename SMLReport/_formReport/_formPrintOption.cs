using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace SMLReport._formReport
{
    public partial class _formPrintOption : Form
    {
        public _formPrintOption()
        {
            InitializeComponent();

            this.Load += new EventHandler(_formPrintOption_Load);
        }

        public _formPrintOption(_g.g._transControlTypeEnum _screenCode)
        {

        }        

        void _formPrintOption_Load(object sender, EventArgs e)
        {
            ManagementObjectSearcher __printerList = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");

            foreach (ManagementObject __getPrinter in __printerList.Get())
            {
                string __printerName = __getPrinter["Name"].ToString();
                _printerCombo.Items.Add(__printerName);
            }

        }
    }

}
