using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace MyLib
{
    public partial class _myGridImportFromTextFileForm : Form
    {
        private DataTable _dataTable = null;
        ArrayList _columnList = null;
        //public Boolean _showHideColumn = false;

        public _myGridImportFromTextFileForm(ArrayList columnList)
        {
            InitializeComponent();
            this._columnList = columnList;
            this._splitComboBox.SelectedIndex = 0;
            this._encodingComboBox.SelectedIndex = 0;
        }

        void _getData(StreamReader reader)
        {
            string __input = null;
            Boolean __first = false;
            while ((__input = reader.ReadLine()) != null)
            {
                string[] __split = __input.Split(this._splitComboBox.SelectedIndex == 1 ? ',' : '\t');
                if (__first == false)
                {
                    __first = true;
                    this._dataTable = new DataTable();
                    for (int __loop = 0; __loop < __split.Length; __loop++)
                    {
                        string __name = "C" + __loop.ToString();
                        DataColumn __column = new DataColumn(__name);
                        this._dataTable.Columns.Add(__column);
                    }
                }
                //
                try
                {
                    this._dataTable.Rows.Add(__split);
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                    break;
                }
            }
            this._dataGridView.DataSource = this._dataTable;
            //
            DataGridViewTextBoxColumn __column1 = new DataGridViewTextBoxColumn();
            __column1.HeaderText = "Target Field";
            __column1.ValueType = typeof(String);
            __column1.ReadOnly = true;
            //
            DataGridViewTextBoxColumn __column2 = new DataGridViewTextBoxColumn();
            __column2.HeaderText = "Source Field";
            __column2.ValueType = typeof(String);
            //
            this._mapFieldView.Columns.Clear();
            this._mapFieldView.Columns.Add(__column1);
            this._mapFieldView.Columns.Add(__column2);
            //
            this._mapFieldView.Rows.Clear();
            for (int __column = 0; __column < this._columnList.Count; __column++)
            {
                MyLib._myGrid._columnType __myColumn = (MyLib._myGrid._columnType)_columnList[__column];
                if (__myColumn._isHide == false)
                {
                    this._mapFieldView.Rows.Add(__myColumn._name, "C"+__column.ToString());
                }
            }
        }

        private void _openTextFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                this._dataTable = null;
                OpenFileDialog __file = new OpenFileDialog();
                __file.Title = "Select Text File";
                __file.Multiselect = false;
                __file.Filter = "All Supported File (*.txt,*.csv) | *.txt;*.csv";
                if (__file.ShowDialog() == DialogResult.OK)
                {
                    System.Text.Encoding __encoder = System.Text.Encoding.GetEncoding(874);
                    if (this._encodingComboBox.SelectedIndex == 1)
                    {
                        __encoder = System.Text.Encoding.UTF8;
                    }
                    StreamReader __re = new StreamReader(__file.FileName.ToString(), __encoder);
                    this._getData(__re);
                    __re.Close();
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _fromClipboardButton_Click(object sender, EventArgs e)
        {
            try
            {
                IDataObject __data = Clipboard.GetDataObject();
                string __text = __data.GetData(DataFormats.Text).ToString();
                //
                byte[] _byteArray = Encoding.UTF8.GetBytes(__text);
                MemoryStream __stream = new MemoryStream(_byteArray);
                StreamReader __reader = new StreamReader(__stream);
                this._getData(__reader);
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }
    }
}
