using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Xml;
using System.Windows.Forms;

namespace SMLOffTakeSalesAdmin
{
    public static class _myGlobal
    {
        private static int _getAttributeToInt(XmlNode node, XmlAttributeCollection attribute, string name)
        {
            if (attribute[name] != null)
            {
                return Int32.Parse(node.Attributes[name].Value);
            }
            return 0;
        }
        public static DataTable getXmlSpeedsheet(string filename, int row)
        {

            DataTable __temp = new DataTable();

            try
            {

                ArrayList __rowArrayList = new ArrayList();
                XmlDocument __xmlDocumnet = new XmlDocument();
                __xmlDocumnet.Load(filename);
                XmlNode __root = __xmlDocumnet["Workbook"]["Worksheet"];
                XmlNode __table = null;
                for (int __xtable = 0; __xtable < __root.ChildNodes.Count; __xtable++)
                {
                    if (__root.ChildNodes[__xtable].Name.ToLower().Equals("table"))
                    {
                        __table = __root.ChildNodes[__xtable];
                        break;
                    }
                }

                int __maxRow = _getAttributeToInt(__table, __table.Attributes, "ss:ExpandedRowCount");
                int __maxColumn = _getAttributeToInt(__table, __table.Attributes, "ss:ExpandedColumnCount");
                //
                __root = __xmlDocumnet["Workbook"]["Worksheet"]["Table"];
                int __rowIndex = 0;
                for (int __row = 0; __row < __maxRow; __row++)
                {
                    ArrayList __dataList = new ArrayList();
                    for (int __column = 0; __column < __maxColumn; __column++)
                    {
                        __dataList.Add("");
                    }
                    __rowArrayList.Add(__dataList);
                }

                foreach (XmlNode __row in __root.ChildNodes)
                {
                    if (__row.Name.Equals("Row"))
                    {
                        int __rowIndexNew = _getAttributeToInt(__row, __row.Attributes, "ss:Index");
                        if (__rowIndexNew != 0)
                        {
                            __rowIndex = __rowIndexNew - 1;
                        }
                        int __columnIndex = 0;
                        foreach (XmlNode __cell in __row.ChildNodes)
                        {
                            int __columnIndexNew = _getAttributeToInt(__cell, __cell.Attributes, "ss:Index");
                            if (__columnIndexNew != 0)
                            {
                                __columnIndex = __columnIndexNew - 1;
                            }

                            object __value = "";
                            XmlNode __data = null;
                            try
                            {

                                __data = (__cell["Data"].FirstChild != null) ? __cell["Data"].FirstChild : null;

                                if (__data != null)
                                {

                                    __value = __data.Value;
                                }
                                else
                                {
                                    __value = "";
                                }
                            }
                            catch
                            {
                                string __xxxx = "";
                            }

                            ((ArrayList)__rowArrayList[__rowIndex])[__columnIndex] = __value;
                            __columnIndex++;
                        }
                        // __valuePercent += (100.0 * __rowIndex / __maxRow);

                        __rowIndex++;
                    }
                }
                DataTable __dataTable = new DataTable("Data");
                for (int __column = 0; __column < __maxColumn; __column++)
                {
                    // __dataTable.Columns.Add(Convert.ToChar(__column + 65).ToString(), Type.GetType("System.String"));
                    __dataTable.Columns.Add(__column.ToString(), Type.GetType("System.String"));
                }
                for (int __row = row; __row < __maxRow; __row++)
                {
                    DataRow __newRow = __dataTable.NewRow();
                    for (int __column = 0; __column < __maxColumn; __column++)
                    {
                        __newRow[__column] = ((ArrayList)__rowArrayList[__row])[__column].ToString().Trim();
                    }
                    __dataTable.Rows.Add(__newRow);
                }
                //  _dataGridView.DataSource = __dataTable;
                __temp = __dataTable;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 1 " + ex.Message);
            }
            return __temp;
        }
        public static DataTable __dupplicateKey(DataTable data, int feildname)
        {
            ArrayList __listtemp = new ArrayList();
            DataTable __temp = new DataTable();
            __temp = data.Clone();
            for (int __rowtemp = 0; __rowtemp < data.Rows.Count; __rowtemp++)
            {
                string __key = data.Rows[__rowtemp][feildname].ToString().Trim();
                if (__listtemp.Count == 0)
                {
                    if (__key.Length > 0)
                    {
                        __temp.ImportRow(data.Rows[__rowtemp]);
                        __listtemp.Add(__key);
                    }
                }
                else
                {
                    if (__key.Length > 0)
                    {
                        if (!__listtemp.Contains(__key))
                        {
                            __temp.ImportRow(data.Rows[__rowtemp]);
                            __listtemp.Add(__key);
                        }
                       
                    }
                }
            }
            return __temp;
        }
    }
}
