using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace _g
{
    public class _selectMenuPayroll
    {
        public static Control _getObject(string menuName, string screenName, string programName)
        {
            //MyLib._manageMasterCode __screen;
            MyLib._manageMasterCodeFull __screenFull;
            switch (menuName.ToLower())
            {
                    //กำหนดรูปแบบเอกสาร
                case "menu_setup_doc_format":
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.erp_doc_format._table;
                    __screenFull._addColumn(_g.d.erp_doc_format._code, 10, 10);
                    __screenFull._inputScreen._setUpper(_g.d.erp_doc_format._code);
                    __screenFull._addColumn(_g.d.erp_doc_format._screen_code, 10, 10);
                    __screenFull._addColumn(_g.d.erp_doc_format._name_1, 100, 20);
                    __screenFull._addColumn(_g.d.erp_doc_format._name_2, 100, 20);
                    __screenFull._addColumn(_g.d.erp_doc_format._format, 100, 30);
                    __screenFull._addColumn(_g.d.erp_doc_format._form_code, 100, 30);
                    __screenFull._finish();
                    __screenFull._webBrowser.Visible = true;
                    __screenFull._webBrowser.DocumentText =
@"
<html>
<head>
<style type='text/css'>
body,td,th {
	font-family: Tahoma, Geneva, sans-serif;
	font-size: 12px;
}
h1,h2,h3,h4,h5,h6 {
	font-family: Tahoma, Geneva, sans-serif;
}
</style>
<meta http-equiv='Content-Type' content='text/html; charset=utf-8'></head>
<body leftmargin='5' topmargin='5' marginwidth='5' marginheight='5'>
<table width='100%' border='1'>
  <tr>
    <th scope='col'>ข้อมูลหลัก</th>
    <th scope='col'>รายวัน</th>    
  </tr>
  <tr>
    <td valign='top' align='left'>
    <b>EM</b>=บันทึกรายละเอียดพนักงาน/ลูกจ้าง<br>
    <b>SW</b>=บันทึกเงินหักการขาดงาน<br>
    <b>LA</b>=บันทึกเงินหักการลางาน<br>
    <b>AL</b>=บันทึกเงินหักการมาสาย<br>
    <b>OT</b>=บันทึกค่าล่วงเวลา<br>
    <b>IT</b>=บันทึกอัตราภาษีเงินได้<br>
    </td>
    <td valign='top'>
    <b>TL</b>=บันทึกรายละเอียดการทำงาน<br>
    <b>CW</b>=บันทีกการคำนวนเงินเดือน/ค่าจ้าง<br>
    <b>TA</b>=บันทึกการจ่ายภาษีรายเดือน<br>
    <b>CT</b>=บันทึกการคำนวนภาษี<br>    
    </td valign='top'>   
  </tr>
</table>
<p> Format :   <b>@</b>=รหัสเอกสาร,<b>DD</b>=วันที่,<b>MM</b>=เดือน,<b>YY</b>=ปี,<b>YYYY</b>=ปี,<b>####</b>=จำนวนตัวเลข ตัวอย่าง :   <b>@-DD-MM-YYYY-######</b> หรือ <b>@DDMMYY-######</b> </p>
</body>
</html>";
                    return __screenFull;
                case "menu_side_list": //กำหนดฝ่าย
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g._dataPayroll.payroll_side_list._table;
                    __screenFull._addColumn(_g._dataPayroll.payroll_side_list._code, 10, 20);
                    __screenFull._addColumn(_g._dataPayroll.payroll_side_list._name, 100, 40);
                    __screenFull._addColumn(_g._dataPayroll.payroll_side_list._name_en, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_section_list": //กำหนดแผนก
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g._dataPayroll.payroll_section_list._table;
                    __screenFull._addColumn(_g._dataPayroll.payroll_section_list._code, 10, 20);
                    __screenFull._addColumn(_g._dataPayroll.payroll_section_list._name, 100, 40);
                    __screenFull._addColumn(_g._dataPayroll.payroll_section_list._name_en, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_work_title": //กำหนดตำแหน่งงาน
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g._dataPayroll.payroll_work_title._table;
                    __screenFull._addColumn(_g._dataPayroll.payroll_work_title._code, 10, 20);
                    __screenFull._addColumn(_g._dataPayroll.payroll_work_title._name, 100, 40);
                    __screenFull._addColumn(_g._dataPayroll.payroll_work_title._name_en, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_bank_list": //กำหนดธนาคาร
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g._dataPayroll.payroll_bank_list._table;
                    __screenFull._addColumn(_g._dataPayroll.payroll_bank_list._code, 10, 20);
                    __screenFull._addColumn(_g._dataPayroll.payroll_bank_list._name, 100, 40);
                    __screenFull._addColumn(_g._dataPayroll.payroll_bank_list._name_en, 100, 40);
                    __screenFull._finish();
                    return __screenFull;
                //case "menu_branch_bank": //กำหนดสาขาธนาคาร
                //    __screenFull = new MyLib._manageMasterCodeFull();
                //    __screenFull._labelTitle.Text = screenName;
                //    __screenFull._dataTableName = _g.dPR.payroll_branch_bank._table;
                //    __screenFull._addColumn(_g.dPR.payroll_branch_bank._code, 10, 20);
                //    __screenFull._addColumn(_g.dPR.payroll_branch_bank._name1, 100, 40);
                //    __screenFull._addColumn(_g.dPR.payroll_branch_bank._name2, 100, 40);
                //    __screenFull._finish();
                //    return __screenFull;


                // ระบบข้อมูล menu_view_manager
                case "menu_view_manager": return (new MyLib._databaseManage._viewManage(true));
                case "menu_database_struct": return (new MyLib._databaseManage._databaseStruct());
                case "menu_query": return (new MyLib._databaseManage._queryDataView());
                case "menu_verify_database": MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, screenName, new MyLib._databaseManage._verifyDatabase()); break;
                case "menu_shink_database": MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, screenName, new MyLib._databaseManage._shrinkDatabase()); break;
                //case "menu_change_password": MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, screenName, new MyLib._databaseManage._changePassword()); break;
            }
            return null;
        }
    }
}
