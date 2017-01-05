using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SMLLanguage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this._dataGrid.Columns.Add("Thai", "Thai");
            this._dataGrid.Columns.Add("Eng", "Eng");
            this._dataGrid.Columns.Add("Chinese", "Chinese");
            this._dataGrid.Columns.Add("Malay", "Malay");
            this._dataGrid.Columns.Add("India", "India");
            this._dataGrid.Columns.Add("Lao", "Lao");

            this._dataGrid.RowPostPaint += _dataGrid_RowPostPaint;
        }

        void _dataGrid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(this._dataGrid.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        string _enCodeSlash(string value)
        {
            return value.Replace(@"\", @"\\");
        }

        string _decodeSlash(string value)
        {
            return value.Replace(@"\\", @"\");
        }

        String _convertTextToXml(String source)
        {
            return source.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;");
        }

        private void _buttonLoadFromTextFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "XML|*.xml";
            DialogResult __result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (__result == DialogResult.OK) // Test result.
            {
                string __fileName = openFileDialog1.FileName;
                try
                {
                    this._dataGrid.Rows.Clear();
                    DataSet __ds = new DataSet();
                    __ds.ReadXml(__fileName);
                    DataTable __dt = __ds.Tables[1];
                    string __thaiLang = "";
                    string __englishLang = "";
                    string __chineseLang = "";
                    string __malayLang = "";
                    string __indiaLang = "";
                    string __laoLang = "";
                    foreach (DataRow __row in __dt.Rows)
                    {
                        string __fieldName = __row.ItemArray[0].ToString();
                        string __value = __row.ItemArray[1].ToString();
                        if (__fieldName.Equals("thai_lang")) __thaiLang = (__value == null) ? "" : _enCodeSlash(__value);
                        else
                            if (__fieldName.Equals("english_lang")) __englishLang = (__value == null) ? "" : _enCodeSlash(__value);
                        else
                                if (__fieldName.Equals("chinese_lang")) __chineseLang = (__value == null) ? "" : _enCodeSlash(__value);
                        else
                                    if (__fieldName.Equals("malay_lang")) __malayLang = (__value == null) ? "" : _enCodeSlash(__value);
                        else
                                        if (__fieldName.Equals("india_lang")) __indiaLang = (__value == null) ? "" : _enCodeSlash(__value);
                        else
                                            if (__fieldName.Equals("lao_lang"))
                        {
                            __laoLang = (__value == null) ? "" : _enCodeSlash(__value);
                            // จบ Row ให้ add 
                            this._dataGrid.Rows.Add(__thaiLang, __englishLang, __malayLang, __chineseLang, __indiaLang, __laoLang);
                        }
                    }

                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                }
            }
        }

        void _compare(string language)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Txt|*.txt";
            DialogResult __result = openFileDialog1.ShowDialog(); // Show the dialog.

            if (__result == DialogResult.OK) // Test result.
            {
                string __lastLine = "";
                string __fileName = openFileDialog1.FileName;
                try
                {
                    using (StreamReader sr = File.OpenText(__fileName))
                    {
                        String __input;
                        while ((__input = sr.ReadLine()) != null)
                        {
                            __lastLine = __input;
                            string[] __split = __input.Split('\t');
                            for (int __row = 0; __row < this._dataGrid.Rows.Count; __row++)
                            {
                                string __str1 = _enCodeSlash(__split[0]);

                                if (this._dataGrid.Rows[__row].Cells["Thai"].Value != null && this._dataGrid.Rows[__row].Cells["Thai"].Value.ToString().Equals(__str1))
                                {
                                    this._dataGrid.Rows[__row].Cells[language].Value = _enCodeSlash(__split[1]);
                                    break;
                                }
                            }
                        }
                    }
                    MessageBox.Show("Success");
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString() + " : " + __lastLine);
                }
            }
        }

        private void _buttonLoadCompare_Click(object sender, EventArgs e)
        {
            _compare("Lao");
        }

        private void _buttonIncludeThai_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Txt|*.txt";
            DialogResult __result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (__result == DialogResult.OK) // Test result.
            {
                string __fileName = openFileDialog1.FileName;
                try
                {
                    using (StreamReader sr = File.OpenText(__fileName))
                    {
                        String __input;
                        while ((__input = sr.ReadLine()) != null)
                        {
                            Boolean __found = false;

                            if (__input.Length > 1 && __input.StartsWith("\"") && __input.EndsWith("\""))
                            {
                                __input = __input.Substring(1, __input.Length - 2);
                            }
                            for (int __row = 0; __row < this._dataGrid.Rows.Count; __row++)
                            {
                                if (__input.Length > 1 && this._dataGrid.Rows[__row].Cells["Thai"].Value != null && this._dataGrid.Rows[__row].Cells["Thai"].Value.ToString().Equals(__input))
                                {
                                    __found = true;
                                    break;
                                }
                            }
                            if (__found == false)
                            {
                                this._dataGrid.Rows.Add(__input, "", "", "", "", "");
                            }
                        }
                    }
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                }
            }
        }

        private void _buttonGenXml_Click(object sender, EventArgs e)
        {
            _genForm __result = new _genForm();
            StringBuilder __str = new StringBuilder();
            __str.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            __str.Append("<ROOT>");
            for (int __row = 0; __row < this._dataGrid.Rows.Count; __row++)
            {
                if (this._dataGrid.Rows[__row].Cells["Thai"].Value != null && this._dataGrid.Rows[__row].Cells["Thai"].Value.ToString().Trim().Length > 1)
                {
                    __str.Append("<row>");
                    __str.Append("<field name=\"roworder\">" + __row.ToString() + "</field>");
                    __str.Append("<field name=\"thai_lang\">" + _convertTextToXml(_decodeSlash(this._dataGrid.Rows[__row].Cells["Thai"].Value.ToString())) + "</field>");
                    __str.Append("<field name=\"english_lang\">" + _convertTextToXml(_decodeSlash((this._dataGrid.Rows[__row].Cells["Eng"].Value != null) ? this._dataGrid.Rows[__row].Cells["Eng"].Value.ToString() : "")) + "</field>");
                    __str.Append("<field name=\"chinese_lang\">" + _convertTextToXml(_decodeSlash((this._dataGrid.Rows[__row].Cells["Chinese"].Value != null) ? this._dataGrid.Rows[__row].Cells["Chinese"].Value.ToString() : "")) + "</field>");
                    __str.Append("<field name=\"malay_lang\">" + _convertTextToXml(_decodeSlash((this._dataGrid.Rows[__row].Cells["Malay"].Value != null) ? this._dataGrid.Rows[__row].Cells["Malay"].Value.ToString() : "")) + "</field>");
                    __str.Append("<field name=\"india_lang\">" + _convertTextToXml(_decodeSlash((this._dataGrid.Rows[__row].Cells["India"].Value != null) ? this._dataGrid.Rows[__row].Cells["India"].Value.ToString() : "")) + "</field>");
                    __str.Append("<field name=\"lao_lang\">" + _convertTextToXml(_decodeSlash((this._dataGrid.Rows[__row].Cells["Lao"].Value != null) ? this._dataGrid.Rows[__row].Cells["Lao"].Value.ToString() : "")) + "</field>");
                    __str.Append("</row>");
                }
            }
            __str.Append("</ROOT>");
            __result._textBox.Text = __str.ToString();
            __result.ShowDialog();
        }

        private void _buttonWriteXML_Click(object sender, EventArgs e)
        {
            SaveFileDialog __saveFile = new SaveFileDialog();
            __saveFile.Filter = "XML|*.xml";
            DialogResult __result = __saveFile.ShowDialog();
            if (__result == System.Windows.Forms.DialogResult.OK)
            {
                StringBuilder __str = new StringBuilder();
                __str.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                __str.AppendLine("<ROOT>");
                for (int __row = 0; __row < this._dataGrid.Rows.Count; __row++)
                {
                    if (this._dataGrid.Rows[__row].Cells["Thai"].Value != null && this._dataGrid.Rows[__row].Cells["Thai"].Value.ToString().Trim().Length > 1)
                    {
                        __str.AppendLine("<row>");
                        __str.AppendLine("<field name=\"roworder\">" + __row.ToString() + "</field>");
                        __str.AppendLine("<field name=\"thai_lang\">" + _convertTextToXml(_decodeSlash(this._dataGrid.Rows[__row].Cells["Thai"].Value.ToString())) + "</field>");
                        __str.AppendLine("<field name=\"english_lang\">" + _convertTextToXml(_decodeSlash((this._dataGrid.Rows[__row].Cells["Eng"].Value != null) ? this._dataGrid.Rows[__row].Cells["Eng"].Value.ToString() : "")) + "</field>");
                        __str.AppendLine("<field name=\"chinese_lang\">" + _convertTextToXml(_decodeSlash((this._dataGrid.Rows[__row].Cells["Chinese"].Value != null) ? this._dataGrid.Rows[__row].Cells["Chinese"].Value.ToString() : "")) + "</field>");
                        __str.AppendLine("<field name=\"malay_lang\">" + _convertTextToXml(_decodeSlash((this._dataGrid.Rows[__row].Cells["Malay"].Value != null) ? this._dataGrid.Rows[__row].Cells["Malay"].Value.ToString() : "")) + "</field>");
                        __str.AppendLine("<field name=\"india_lang\">" + _convertTextToXml(_decodeSlash((this._dataGrid.Rows[__row].Cells["India"].Value != null) ? this._dataGrid.Rows[__row].Cells["India"].Value.ToString() : "")) + "</field>");
                        __str.AppendLine("<field name=\"lao_lang\">" + _convertTextToXml(_decodeSlash((this._dataGrid.Rows[__row].Cells["Lao"].Value != null) ? this._dataGrid.Rows[__row].Cells["Lao"].Value.ToString() : "")) + "</field>");
                        __str.AppendLine("</row>");
                    }
                }
                __str.AppendLine("</ROOT>");

                try
                {
                    // Write the string to a file.
                    System.IO.StreamWriter file = new System.IO.StreamWriter(__saveFile.FileName, false, Encoding.GetEncoding("utf-8"));
                    file.Write(__str.ToString());
                    file.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Can't save " + __saveFile.FileName + " (" + ex.Message.ToString() + ")");
                }
            }

        }

        private void _enCompareButton_Click(object sender, EventArgs e)
        {
            _compare("Eng");
        }

        private void _duplicateCheckButton_Click(object sender, EventArgs e)
        {
            this._checkDuplicate();
        }

        void _checkDuplicate()
        {
            int __dupCount = 0;
            _dataGrid.Sort(_dataGrid.Columns[0], ListSortDirection.Ascending);
            //use the currentRow to compare against
            for (int currentRow = 0; currentRow < _dataGrid.Rows.Count - 1; currentRow++)
            {
                DataGridViewRow rowToCompare = _dataGrid.Rows[currentRow];
                //specify otherRow as currentRow + 1
                for (int otherRow = currentRow + 1; otherRow < _dataGrid.Rows.Count; otherRow++)
                {
                    DataGridViewRow row = _dataGrid.Rows[otherRow];

                    bool duplicateRow = true;
                    //compare cell ENVA_APP_ID between the two rows
                    if (!rowToCompare.Cells[0].Value.Equals(row.Cells[0].Value))
                    {
                        duplicateRow = false;
                        break;
                    }
                    //highlight both the currentRow and otherRow if ENVA_APP_ID matches 
                    if (duplicateRow)
                    {
                        rowToCompare.DefaultCellStyle.BackColor = Color.Red;
                        rowToCompare.DefaultCellStyle.ForeColor = Color.Black;
                        row.DefaultCellStyle.BackColor = Color.Red;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        __dupCount++;
                    }
                    else
                    {
                        rowToCompare.DefaultCellStyle.BackColor = Color.White;
                        rowToCompare.DefaultCellStyle.ForeColor = Color.Black;
                        row.DefaultCellStyle.BackColor = Color.White;
                        row.DefaultCellStyle.ForeColor = Color.Black;

                    }
                }
            }

            if (__dupCount > 0)
            {
                MessageBox.Show("Found Duplicate");
            }
            else
            {
                MessageBox.Show("Success");
            }
        }

        private void _nextDuplicateButton_Click(object sender, EventArgs e)
        {
            int __dupCount = 0;
            _dataGrid.Sort(_dataGrid.Columns[0], ListSortDirection.Ascending);
            //use the currentRow to compare against
            for (int currentRow = 0; currentRow < _dataGrid.Rows.Count - 1; currentRow++)
            {
                DataGridViewRow rowToCompare = _dataGrid.Rows[currentRow];
                //specify otherRow as currentRow + 1
                for (int otherRow = currentRow + 1; otherRow < _dataGrid.Rows.Count; otherRow++)
                {
                    DataGridViewRow row = _dataGrid.Rows[otherRow];

                    bool duplicateRow = true;
                    //compare cell ENVA_APP_ID between the two rows
                    if (!rowToCompare.Cells[0].Value.Equals(row.Cells[0].Value))
                    {
                        duplicateRow = false;
                        break;
                    }
                    //highlight both the currentRow and otherRow if ENVA_APP_ID matches 
                    if (duplicateRow)
                    {
                        rowToCompare.DefaultCellStyle.BackColor = Color.Red;
                        rowToCompare.DefaultCellStyle.ForeColor = Color.Black;
                        row.DefaultCellStyle.BackColor = Color.Red;
                        row.DefaultCellStyle.ForeColor = Color.Black;

                        this._dataGrid.ClearSelection();

                        this._dataGrid.Rows[currentRow].Selected = true;
                        this._dataGrid.Rows[currentRow].Cells[0].Selected = true;

                        _dataGrid.CurrentCell = this._dataGrid.Rows[currentRow].Cells[0];

                        return;
                    }

                }
            }

        }

        private void _databaseDesignCompare_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "XML|*.xml";
            DialogResult __result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (__result == DialogResult.OK) // Test result.
            {
                string __fileName = openFileDialog1.FileName;
                try
                {
                    string tableName = "";
                    // for table
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(__fileName);

                    XmlNodeList __tableNode = xmldoc.SelectNodes("/node/table");

                    // for field
                    for (int __table = 0; __table < __tableNode.Count; __table++)
                    {
                        XmlNodeList __fieldNode = __tableNode[__table].SelectNodes("field");

                        string __thaiword = __tableNode[__table].Attributes["thai"].Value.ToString();
                        string __engWord = __tableNode[__table].Attributes["eng"].Value.ToString();

                        _addThaiWord(__thaiword, __engWord, "Eng");

                        for (int __field = 0; __field < __fieldNode.Count; __field++)
                        {
                            __thaiword = __fieldNode[__field].Attributes["thai"].Value.ToString();
                            __engWord = __fieldNode[__field].Attributes["eng"].Value.ToString();

                            _addThaiWord(__thaiword, __engWord, "Eng");

                        }
                    }
                }
                catch
                {

                }
            }
        }

        void _addThaiWord(string thaiWord, string word2, string language)
        {
            Boolean __found = false;
            if (thaiWord == "")
                return;

            for (int __row = 0; __row < this._dataGrid.Rows.Count; __row++)
            {
                if (thaiWord.Length > 1 && this._dataGrid.Rows[__row].Cells["Thai"].Value != null && this._dataGrid.Rows[__row].Cells["Thai"].Value.ToString().Equals(_enCodeSlash(thaiWord)))
                {
                    __found = true;
                    if (this._dataGrid.Rows[__row].Cells[language].Value.Equals(""))
                    {
                        this._dataGrid.Rows[__row].Cells[language].Value = _enCodeSlash(word2);
                    }
                    break;
                }
            }

            if (__found == false)
            {
                this._dataGrid.Rows.Add(thaiWord, "", "", "", "", "");
                _addThaiWord(thaiWord, word2, language);
            }

        }
    }
}
