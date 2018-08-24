using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Json;
using System.Text;
using System.Windows.Forms;

namespace SMLEDIControl
{
    public class transdatadetailsap
    {
        public string BILLINGDOCNO { get; set; }
        public string Agentcode { get; set; }
        public string MATERIALCODE { get; set; }
        public string line_number { get; set; }
        public string is_permium { get; set; }
        public string SALESUNIT { get; set; }
        public string wh_code { get; set; }
        public string shelf_code { get; set; }
        public string qty { get; set; }
        public string price { get; set; }
        public string price_exclude_vat { get; set; }
        public string discount_amount { get; set; }
        public string sum_amount { get; set; }
        public string vat_amount { get; set; }
        public string tax_type { get; set; }
        public string vat_type { get; set; }
        public string BAT_DATE { get; set; }
        public string BAT_NUMBER { get; set; }
        public string date_expire { get; set; }
        public string item_code { get; set; }
        public decimal total_vat_value { get; set; }
        public decimal sum_amount_exclude_vat { get; set; }

        //edi
        public decimal unit_code { get; set; }
        public decimal discount { get; set; }
        public decimal item_name { get; set; }






        public transdatadetailsap()
        {

        }

        public JsonValue _getJson()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            return json;
        }
        public static transdatadetailsap Parse(string json)
        {
            if (json != null)
            {
                transdatadetailsap _transdatadetail = JsonConvert.DeserializeObject<transdatadetailsap>(json);
                return _transdatadetail;
            }
            return null;
        }

        public static transdatadetailsap ParseEDIText(string __text_in_line)
        {
            string Header = __cutstring(__text_in_line, "Header", 1, 6);
            string Line_Number = __cutstring(__text_in_line, "Line_Number", 7, 6);
            string Product_Code = __cutstring(__text_in_line, "Product_Code", 13, 15);
            string Customer_Product_Number = __cutstring(__text_in_line, "Customer_Product_Number", 28, 15);
            string Product_Description_Of_Customer_In_Thai = __cutstring(__text_in_line, "Product_Description_Of_Customer_In_Thai", 43, 50);
            string Order_Quantity = __cutstring(__text_in_line, "Order_Quantity", 93, 12);
            string Order_Unit_of_Measure_Value = __cutstring(__text_in_line, "Order_Unit_of_Measure_Value", 105, 6);
            string Free_Quantity = __cutstring(__text_in_line, "Free_Quantity", 111, 12);
            string Free_Unit_of_Measure_Value = __cutstring(__text_in_line, "Free_Unit_of_Measure_Value", 123, 6);
            string Unit_Price = __cutstring(__text_in_line, "Unit_Price", 129, 15);
            string Full_Pallet_Quantity_Pallet = __cutstring(__text_in_line, "Full_Pallet_Quantity_Pallet", 144, 10);
            string Order_Quantity_for_Small_Unit = __cutstring(__text_in_line, "Order_Quantity_for_Small_Unit", 154, 12);
            string Order_unit_of_Measure_For_Small_Unit = __cutstring(__text_in_line, "Order_unit_of_Measure_For_Small_Unit", 166, 6);
            string Free_Quantity_For_Small_Unit = __cutstring(__text_in_line, "Free_Quantity_For_Small_Unit", 172, 12);
            string Free_Unit_Of_Measure_For_Small_Unit = __cutstring(__text_in_line, "Free_Unit_Of_Measure_For_Small_Unit", 184, 6);
            string Discount_Percent1 = __cutstring(__text_in_line, "Discount_Percent1", 190, 5);
            string Discount_Amount1 = __cutstring(__text_in_line, "Discount_Amount1", 195, 15);
            string Discount_Percent2 = __cutstring(__text_in_line, "Discount_Percent2", 210, 5);
            string Discount_Amount2 = __cutstring(__text_in_line, "Discount_Amount2", 215, 15);
            string Discount_Percent3 = __cutstring(__text_in_line, "Discount_Percent3", 230, 5);
            string Discount_Amount3 = __cutstring(__text_in_line, "Discount_Amount3", 235, 15);
            string Discount_Percent4 = __cutstring(__text_in_line, "Discount_Percent4", 250, 5);
            string Discount_Amount4 = __cutstring(__text_in_line, "Discount_Amount4", 255, 15);
            Console.WriteLine("--ตัว--");
            Console.WriteLine(Header);
            Console.WriteLine(Line_Number);
            Console.WriteLine(Product_Code);
            Console.WriteLine(Customer_Product_Number);
            Console.WriteLine(Product_Description_Of_Customer_In_Thai);
            Console.WriteLine(Order_Quantity);
            Console.WriteLine(Free_Unit_of_Measure_Value);
            Console.WriteLine(Unit_Price);
            Console.WriteLine(Full_Pallet_Quantity_Pallet);
            Console.WriteLine(Order_Quantity_for_Small_Unit);
            Console.WriteLine(Order_unit_of_Measure_For_Small_Unit);
            Console.WriteLine(Free_Quantity_For_Small_Unit);
            Console.WriteLine(Free_Unit_Of_Measure_For_Small_Unit);
            Console.WriteLine(Discount_Percent1);
            Console.WriteLine(Discount_Amount1);
            Console.WriteLine(Discount_Percent2);
            Console.WriteLine(Discount_Amount2);
            Console.WriteLine(Discount_Percent3);
            Console.WriteLine(Discount_Amount3);
            Console.WriteLine(Discount_Percent4);
            Console.WriteLine("------");

            transdatadetailsap _transdatadetailsap = new transdatadetailsap();
            _transdatadetailsap.item_code = Product_Code;
            _transdatadetailsap.line_number = Line_Number; 
            _transdatadetailsap.unit_code = MyLib._myGlobal._intPhase(Unit_Price); 
            _transdatadetailsap.qty = Order_Quantity; 
            _transdatadetailsap.wh_code = ""; 
            _transdatadetailsap.shelf_code = ""; 
            _transdatadetailsap.price = "";
            _transdatadetailsap.price_exclude_vat = ""; 
            _transdatadetailsap.sum_amount = ""; 
            _transdatadetailsap.discount_amount = ""; 
            _transdatadetailsap.discount = 0; 
            _transdatadetailsap.total_vat_value = 0;
            _transdatadetailsap.tax_type = ""; 
            _transdatadetailsap.vat_type = ""; 
            _transdatadetailsap.item_name = 0;
            return _transdatadetailsap;
        }

        public static string __cutstring(string data, string name, int start, int Length)
        {
            string value = "";
            start = start - 1;
            try
            {
                if (Length < data.Length)
                {
                    value = data.Substring(start, Length);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ผิดพลาดการนำเข้าห้อมูล " + name + "ที่เริ่มจาก" + start.ToString() + "ทั้งหมด " + Length.ToString() + "ตัวอักษร", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return value.Trim();
        }


        public string _queryInsertEdiDetail()
        {
            StringBuilder __queryInsert = new StringBuilder();

            //detail
            StringBuilder sqlDetail = new StringBuilder();
            sqlDetail.Append("INSERT INTO ic_trans_detail (" +
                "trans_flag, trans_type, item_code," +
                "line_number,unit_code, qty, wh_code, shelf_code, price," +
                "price_exclude_vat, sum_amount, discount_amount, discount, total_vat_value," +
                "tax_type, vat_type, item_name" +
                ")");
            sqlDetail.Append(
                   " VALUES (\'{0}\', {1}, {2},\'{3}\', \'{4}\',\'{5}\'" +
                   ",{6},{7},{8},{9},{10}" +
                   ",{11},{12},{13},{14},{15}" +
                   ")"
            //",{16},{17},{18})"
            );
            __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery(String.Format(sqlDetail.ToString()
                , 36, 2, this.item_code
                , this.line_number, this.unit_code, this.qty, this.wh_code, this.shelf_code, this.price
                , this.price_exclude_vat, this.sum_amount, this.discount_amount, this.discount, this.total_vat_value
                , this.tax_type, this.vat_type, this.item_name
                )));
            return __queryInsert.ToString();
        }


    }
}
