using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;

namespace SMLReport._report
{
    public class _csvClass
    {
        StringBuilder _csvResult = new StringBuilder();
        List<string> _csvColumns = new List<string>();

        public _csvClass()
        {

        }

        public void _addCell(object __value)
        {
            _addCell(__value, _cellType.String);
        }

        public void _addCell(object __value, SMLReport._report._cellType dataType)
        {
            // check object type ปรับ format ให้รับ csv ได้
            string __cellData = __value.ToString();
            if (dataType == _cellType.Number)
            {
                __cellData = MyLib._myGlobal._decimalPhase(__value.ToString()).ToString("#######.00");
            }

            _csvColumns.Add(__cellData);
        }

        public void _addNewLine()
        {
            if (_csvColumns.Count > 0)
            {
                _csvResult.Append(string.Join(",", _csvColumns.ToArray()));
                _csvColumns = new List<string>();
                _csvResult.AppendLine();
            }

        }

        public string _export()
        {
            return _csvResult.ToString();
        }

        public void _exportCSVFile(string __fileName)
        {
            File.WriteAllText(__fileName, _export(), Encoding.Default);

            //File.WriteAllText(__fileName, _export(), Encoding.GetEncoding(1250));

        }

        public string _convertDateToStr(DateTime value)
        {
            string result = "";
            try
            {
                CultureInfo ci1 = new CultureInfo("en-US");
                result = value.ToString("yyyy-MM-dd", ci1);
            }
            catch
            {
            }
            return (result);
        }
    }
}
