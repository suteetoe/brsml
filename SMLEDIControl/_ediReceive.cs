using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Globalization;
using System.Collections;
using System.Json;

namespace SMLEDIControl
{
    public partial class _ediReceive : UserControl
    {
        string _agentCode = "";
        string __url = "http://bs.brteasy.com:8080/SinghaEDI_TEST";
        int _transFlag = 36;
        int _transType = 2;

        public _ediReceive()
        {
            InitializeComponent();

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __result = __myFrameWork._queryShort("select * from " + _g.d.erp_company_profile._table);

            if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
            {
                this._agentCode = __result.Tables[0].Rows[0][_g.d.erp_company_profile._agent_code].ToString();
            }

            this._docGrid._table_name = _g.d.ic_trans._table;
            this._docGrid._addColumn("select", 11, 10, 10);
            this._docGrid._isEdit = false;
            this._docGrid._addColumn(_g.d.ic_trans._doc_no, 1, 90, 90);
            this._docGrid.WidthByPersent = true;
            this._docGrid._calcPersentWidthToScatter();
            this._docGrid._mouseClick += _docGrid__mouseClick;


            splitContainer1.Panel2Collapsed = true;
            splitContainer1.Panel2.Hide();

            this.Load += _singhaOnlineOrderImport_Load;
        }
        private void _singhaOnlineOrderImport_Load(object sender, EventArgs e)
        {
            this._getData();
        }

        private void _docGrid__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            // load data to detail
            if (e._row > -1)
            {
                string __docNo = this._docGrid._cellGet(e._row, _g.d.ic_trans._doc_no).ToString();
                //DataRow[] __getRow = this._dataDetail.Select(_g.d.ic_trans_detail._doc_no + "=\'" + __docNo + "\'");
                //this._detailGrid._loadFromDataTable(this._dataDetail, __getRow);

            }
        }


        void _getData()
        {
            try
            {
                this._docGrid._clear();
                // get data from restful server
                WebClient __n = new WebClient();

                //var __json = __n.DownloadString(__url + "/EDI/" + "?agentcode=" + this._agentCode);
                var __json = __n.DownloadString(__url + "/EDI/" + "?agentcode=1000806");
                JsonValue __jsonObject = JsonValue.Parse(__json);
                //JsonArray __jsonObject = new JsonArray(__json);
                // do other


                if (__jsonObject.Count > 0)
                {
                    for (int __row = 0; __row < __jsonObject.Count; __row++)
                    {
                        JsonValue __object = (JsonValue)__jsonObject[__row];
                        if (__object.ToString().Equals("\"success\"") == false && __object.ToString().Equals("\"reject\"") == false)
                        {
                            int __rowAdd = this._docGrid._addRow();
                            this._docGrid._cellUpdate(__rowAdd, _g.d.ic_trans._doc_no, __object.ToString().Replace("\"", string.Empty), true);
                            this._docGrid._cellUpdate(__rowAdd, 0, 1, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void _reloadButton_Click(object sender, EventArgs e)
        {
            this._getData();
        }

        private void _importButton_Click(object sender, EventArgs e)
        {
            this._process();
        }

        void _process()
        {
            try
            {
                if (MessageBox.Show("ต้องการนำเข้าข้อมูลที่ได้เลือกไว้หรือไม่", "ยืนยัน", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    for (int __row = 0; __row < this._docGrid._rowData.Count; __row++)
                    {
                        if (this._docGrid._cellGet(__row, 0).ToString().Equals("1"))
                        {
                            StringBuilder __rejectMessage = new StringBuilder();
                            List<string> __itemList = new List<string>();
                            List<string> __productUnit = new List<string>();
                            Boolean __savePass = false;
                            WebClient __n = new WebClient();

                            StringBuilder __myQuery = new StringBuilder();
                            string __docNo = "";
                            string __fileName = "";
                            try
                            {
                                __fileName = this._docGrid._cellGet(__row, _g.d.ic_trans._doc_no).ToString().Replace("\"", string.Empty);
                                var __jsonStr = __n.DownloadString(__url + "/EDI/order/" + __fileName + "?agentcode=1000806");
                                //JsonValue __json = JsonValue.Parse(__jsonStr);
                                
                                MessageBox.Show(__jsonStr.ToString(), "นำเข้าข้อมูลเรียบร้อยแล้ว พบข้อผิดพลาดที่รายการดังต่อไปนี้", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                string __doc = __jsonStr.ToString();
                                string[] line = __doc.Split('\n');
                                int _runline = 0;
                                foreach (string __text_in_line in line)
                                {
                                   
                                    string _gethead = line[_runline].Substring(0, 6);
                                    //เช็คหัวแถว
                                    if (_gethead.Equals("HDRINF"))
                                    {
                                        string Header = line[_runline].Substring(0, 6);
                                        string PO_Type = line[_runline].Substring(6, 1);
                                        string Order_Number = line[_runline].Substring(7, 15);
                                        string Order_Date = line[_runline].Substring(22, 8);
                                        string Delivery_Date = line[_runline].Substring(30, 12);
                                        string Customer_Number = line[_runline].Substring(42, 14);
                                        string Customer_Name = line[_runline].Substring(56, 50);
                                        string Customer_Address = line[_runline].Substring(106, 100);
                                        string Ship_to_Code = line[_runline].Substring(206, 14);
                                        string Ship_to_Name = line[_runline].Substring(220, 50);
                                        string Supplier_Code = line[_runline].Substring(270, 14);
                                        string Supplier_Name = line[_runline].Substring(284, 50);
                                        string Supplier_Address = line[_runline].Substring(334, 100);
                                        string Bill_To_Customer_Number = line[_runline].Substring(434, 14);
                                        string MAIL_PROMOTION_NUMBER = line[_runline].Substring(448, 10);
                                        string Payment_eriod= line[_runline].Substring(458, 3);
                                        string Discount_Percent1 = line[_runline].Substring(461, 5);
                                        string Discount_Amount1 = line[_runline].Substring(466, 15);
                                        string Discount_Percent2 = line[_runline].Substring(481, 5);
                                        string Discount_Amount2 = line[_runline].Substring(486, 15);
                                        string Discount_Percent3 = line[_runline].Substring(501, 5);
                                        string Discount_Amount3 = line[_runline].Substring(506, 15);
                                        string Gross_Amount = line[_runline].Substring(521, 17);
                                        string Net_Amount = line[_runline].Substring(538, 17);
                                        string Total_Amount = line[_runline].Substring(555, 17);
                                        string Discount_Amount = line[_runline].Substring(572, 17);
                                        string Free_Text = line[_runline].Substring(589, 100);
                                        Console.WriteLine("--------หัว-----------");
                                        Console.WriteLine(Header);
                                        Console.WriteLine(PO_Type);
                                        Console.WriteLine(Order_Number);
                                        Console.WriteLine(Order_Date);
                                        Console.WriteLine(Delivery_Date);
                                        Console.WriteLine(Customer_Number);
                                        Console.WriteLine(Customer_Name);
                                        Console.WriteLine(Customer_Address);
                                        Console.WriteLine(Ship_to_Code);
                                        Console.WriteLine(Ship_to_Name);
                                        Console.WriteLine(Supplier_Code);
                                        Console.WriteLine(Supplier_Name);
                                        Console.WriteLine(Supplier_Address);
                                        Console.WriteLine(Bill_To_Customer_Number);
                                        Console.WriteLine(MAIL_PROMOTION_NUMBER);
                                        Console.WriteLine(Payment_eriod);
                                        Console.WriteLine(Discount_Percent1);
                                        Console.WriteLine(Discount_Amount1);
                                        Console.WriteLine(Discount_Percent2);
                                        Console.WriteLine(Discount_Amount2);
                                        Console.WriteLine(Discount_Percent3);
                                        Console.WriteLine(Discount_Amount3);
                                        Console.WriteLine(Gross_Amount);
                                        Console.WriteLine(Net_Amount);
                                        Console.WriteLine(Total_Amount);
                                        Console.WriteLine(Discount_Amount);
                                        Console.WriteLine(Free_Text);
                                        Console.WriteLine("---------------------");
                                    }
                                    else {
                                        string Header = line[_runline].Substring(0, 6);
                                        string Line_Number = line[_runline].Substring(6, 6);
                                        string Product_Code = line[_runline].Substring(12, 15);
                                        string Customer_Product_Number = line[_runline].Substring(27, 15);
                                        string Product_Description_Of_Customer_In_Thai = line[_runline].Substring(42, 50);
                                        string Order_Quantity = line[_runline].Substring(92, 12);
                                        string Order_Unit_of_Measure_Value = line[_runline].Substring(104, 6);
                                        string Free_Quantity = line[_runline].Substring(110, 12);
                                        string Free_Unit_of_Measure_Value = line[_runline].Substring(122, 6);
                                        string Unit_Price = line[_runline].Substring(128, 15);
                                        string Full_Pallet_Quantity_Pallet = line[_runline].Substring(143, 10);
                                        string Order_Quantity_for_Small_Unit = line[_runline].Substring(153, 12);
                                        string Order_unit_of_Measure_For_Small_Unit = line[_runline].Substring(165, 6);
                                        string Free_Quantity_For_Small_Unit = line[_runline].Substring(171, 12);
                                        string Free_Unit_Of_Measure_For_Small_Unit = line[_runline].Substring(183, 6);
                                        string Discount_Percent1 = line[_runline].Substring(189, 5);
                                        string Discount_Amount1 = line[_runline].Substring(194, 15);
                                        string Discount_Percent2 = line[_runline].Substring(209, 5);
                                        string Discount_Amount2 = line[_runline].Substring(214, 15);
                                        string Discount_Percent3 = line[_runline].Substring(229, 5);
                                        string Discount_Amount3 = line[_runline].Substring(234, 15);
                                        string Discount_Percent4 = line[_runline].Substring(249, 5);
                                        string Discount_Amount4 = line[_runline].Substring(254, 15);
                                        Console.WriteLine("--ตัว--");
                                        Console.WriteLine("");
                                        Console.WriteLine("------");
                                    }

                                    _runline++;



                           
                                }
                                // __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), "นำเข้าข้อมูลเรียบร้อยแล้ว พบข้อผิดพลาดที่รายการดังต่อไปนี้", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                __rejectMessage.AppendLine("Error : " + ex.ToString());
                            }


                        }
                    } // end loop
                    MessageBox.Show("Success");
                    this._getData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void _selectAllButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._docGrid._rowData.Count; __row++)
            {
                this._docGrid._cellUpdate(__row, 0, 1, true);
            }
            this._docGrid.Invalidate();
        }

        private void _selectNoneButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._docGrid._rowData.Count; __row++)
            {
                this._docGrid._cellUpdate(__row, 0, 0, true);
            }
            this._docGrid.Invalidate();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}