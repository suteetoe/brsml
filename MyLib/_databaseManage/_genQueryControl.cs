using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MyLib._databaseManage
{
    public partial class _genQueryControl : UserControl
    {
        private string _columnName = "name";
        private string _columnType = "type";

        public _genQueryControl()
        {
            InitializeComponent();
            this._columnGrid._addColumn(this._columnName, 1, 100, 80);
            this._columnGrid._addColumn(this._columnType, 1, 100, 20);
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _clearButton_Click(object sender, EventArgs e)
        {
            this._sourceTextBox.Clear();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder __columnName = new StringBuilder();
                List<int> __columnType = new List<int>();
                int __columnCount = 0;
                for (int __row = 0; __row < this._columnGrid._rowData.Count; __row++)
                {
                    string __fieldName = this._columnGrid._cellGet(__row, this._columnName).ToString().Trim();
                    if (__fieldName.Length > 0)
                    {
                        if (__columnCount != 0)
                        {
                            __columnName.Append(",");
                        }
                        int __fieldType = (int)MyLib._myGlobal._decimalPhase(this._columnGrid._cellGet(__row, this._columnType).ToString());
                        __columnType.Add(__fieldType);
                        __columnName.Append(__fieldName);
                        __columnCount++;
                    }
                }
                //
                StringBuilder __query1 = new StringBuilder();
                string[] __dataLine = this._sourceTextBox.Text.Split('\n');
                for (int __line = 0; __line < __dataLine.Length; __line++)
                {
                    string __data = __dataLine[__line].ToString().Trim().Replace("\'", "\''");
                    if (__data.Length > 0)
                    {
                        StringBuilder __query2 = new StringBuilder();
                        try
                        {
                            string[] __dataSplit = __data.Split('\t');
                            for (int __column = 0; __column < __columnCount; __column++)
                            {
                                if (__column != 0)
                                {
                                    __query2.Append(",");
                                }
                                if (__columnType[__column] == 0)
                                {
                                    __query2.AppendFormat("\'{0}\'", __dataSplit[__column].ToString());
                                }
                                else
                                {
                                    __query2.Append(__dataSplit[__column].ToString());
                                }
                            }
                        }
                        catch
                        {
                            MessageBox.Show((__line + 1).ToString() + ":" + __data);
                        }
                        __query1.AppendFormat("insert into {0} ({1}) values ({2});\r\n", this._tableNameTextBox.Text.Trim(), __columnName, __query2.ToString());
                    }
                }
                this._resultTextBox.Text = __query1.ToString();
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }
    }
}
