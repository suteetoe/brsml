using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPASSET
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName)
        {
            switch (menuName.ToLower())
            {
                // ข้อมูลสินทรัพย์
                case "menu_asset_lists": return(new SMLERPASSET._as_list()); 
                // บันทึกการซ่อมบำรุงสินทรัพย์
                case "menu_asset_maintenance": return(new SMLERPASSET._as_maintain()); 
                // บันทึกการขายสินทรัพย์
                case "menu_asset_sale": return(new SMLERPASSET._as_sale()); 
                // แสดงค่าเสื่อมราคาสินทรัพย์รายวัน
                case "menu_show_asset_byday": return(new SMLERPASSET._display._depreciateByDay()); 
                // แสดงค่าเสื่อมราคาสินทรัพย์รายเดือน
                case "menu_show_asset_bymonth": return(new SMLERPASSET._display._depreciateByMonth()); 
                // แสดงค่าเสื่อมราคาสินทรัพย์รายปี
                case "menu_show_asset_byyear": return(new SMLERPASSET._display._depreciateByYear()); 
                // โอนข้อมูลเข้าระบบบัญชี
                //case "menu_asset_transfers": return(new SMLERPAS._as_transfer()); 
                // รายงานรายละเอียดสินทรัพย์
                case "menu_asset_report_list": return(new SMLERPASSET._report._asset._report()); 
                // รายงานค่าเสื่อมราคาสินทรัพย์
                case "menu_asset_report_depreciate": return(new SMLERPASSET._report._depreciate._report()); 
                // รายงานการซ่อมบำรุงสินทรัพย์
                case "menu_asset_report_maintain": return(new SMLERPASSET._report._maintain._report()); 
                // รายงานสินทรัพย์ที่ถูกขายพร้อมกำไรขั้นต้น
                case "menu_asset_report_sale": return(new SMLERPASSET._report._sale._report()); 
                // กำหนดรูปภาพ
                case "menu_asset_picture": return(new SMLERPASSET._as_list_pic()); //somruk
            }
            return null;
        }
    }
}
