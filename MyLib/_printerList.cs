using System;
using System.Collections;
using System.Management;

namespace MyLib
{
    public class _printerListType
    {
        public string _printerName;
        public bool _printerOnline;
    }

    public class _printerList
    {
        ArrayList _printer = new ArrayList();

        public void _getPrinter()
        {
            // Set management scope
            ManagementScope scope = new ManagementScope(@"\root\cimv2");
            scope.Connect();

            // Select Printers from WMI Object Collections
            ManagementObjectSearcher searcher = new
             ManagementObjectSearcher("SELECT * FROM Win32_Printer");
            _printer.Clear();
            foreach (ManagementObject printer in searcher.Get())
            {
                _printerListType __newPrinter = new _printerListType();
                __newPrinter._printerName=printer["Name"].ToString();;
                __newPrinter._printerOnline=(printer["WorkOffline"].ToString().ToLower().Equals("true")) ? false : true;
                _printer.Add(__newPrinter);
            }
        }

        public ArrayList PrinterList
        {
            get
            {
                return _printer;
            }
            set
            {
                _printer = value;
            }
        }
    }
}
