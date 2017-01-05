using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using System.Windows.Forms;

namespace SMLERPReport.condition
{
    public class _screen_condition_tabControl : MyLib._myTabControl
    {
        private XmlDocument xDoc = new XmlDocument();

        public _screen_condition_tabControl()
        {
        }

        public void _addTabs(string[] __table_names)
        {
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            DataSet myDataSet = myFrameWork._dataStruct(MyLib._myGlobal._databaseStructFileName);
            xDoc.LoadXml(myDataSet.GetXml());
            xDoc.DocumentElement.Normalize();

            this.TabPages.Clear();
            foreach (string __get_table_name in __table_names)
            {
                this._addTabPage(__get_table_name);
            }
        }

        private void _addTabPage(string __get_table_name)
        {
            MyLib._myGrid __mygrid = new MyLib._myGrid();
            __mygrid._addColumn("field name", 1, 100, 25);
            __mygrid._addColumn("name thai", 1, 100, 30);
            __mygrid._addColumn("name english", 1, 100, 30);
            __mygrid._addColumn("type", 1, 100, 15);
            __mygrid.Dock = System.Windows.Forms.DockStyle.Fill;

            string getTableName = __get_table_name;
            XmlElement xRoot = xDoc.DocumentElement;
            XmlNodeList xReader = xRoot.GetElementsByTagName("table");
            for (int table = 0; table < xReader.Count; table++)
            {
                XmlNode xFirstNode = xReader.Item(table);
                if (xFirstNode.NodeType == XmlNodeType.Element)
                {
                    XmlElement xTable = (XmlElement)xFirstNode;
                    if (getTableName.CompareTo(xTable.GetAttribute("name")) == 0)
                    {
                        // get field
                        XmlNodeList xField = xTable.GetElementsByTagName("field");
                        for (int field = 0; field < xField.Count; field++)
                        {
                            XmlNode xReadNode = xField.Item(field);
                            if (xReadNode != null)
                            {
                                if (xReadNode.NodeType == XmlNodeType.Element)
                                {
                                    XmlElement xGetField = (XmlElement)xReadNode;
                                    __mygrid._addRow();
                                    int addr = __mygrid._rowData.Count - 1;
                                    __mygrid._cellUpdate(addr, 0, xGetField.GetAttribute("name"), false);
                                    __mygrid._cellUpdate(addr, 1, xGetField.GetAttribute("thai"), false);
                                    __mygrid._cellUpdate(addr, 2, xGetField.GetAttribute("eng"), false);
                                    __mygrid._cellUpdate(addr, 3, xGetField.GetAttribute("type"), false);
                                }
                            }
                        } // for
                        break;
                    }
                }
            } // for

            this.TabPages.Add(__get_table_name, __get_table_name);
            this.TabPages[this.TabPages.IndexOfKey(__get_table_name)].Controls.Add(__mygrid);
        }
    }
}