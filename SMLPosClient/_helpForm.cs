using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLPosClient
{
    public partial class _helpForm : Form
    {
        public _helpForm()
        {
            InitializeComponent();
            //

            string __findMemberStr =  MyLib._myGlobal._resource("ค้นหาสมาชิก") ;
            string __findItemStr = MyLib._myGlobal._resource("ค้นหาสินค้า");
            string __findSaleStr = MyLib._myGlobal._resource("ค้นหาพนักงานขาย");
            string __findItemSearchLevelStr = MyLib._myGlobal._resource("ค้นหาลำดับสินค้า");
            string __fineCustomerStr = MyLib._myGlobal._resource("ค้นหาลูกค้า");
            string __foodDiscountStr = MyLib._myGlobal._resource("ให้ส่วนลดเฉพาะอาหาร");
            string __serviceChargeStr = MyLib._myGlobal._resource("Service Charge");
            string __calcTotalStr = MyLib._myGlobal._resource("คิดเงิน (ปิดการขาย)");

            string __specialBarcodeInput = MyLib._myGlobal._resource("คำสั่งพิเศษ (ป้อนที่ช่อง Barcode)");
            string __deleleLastItemStr = MyLib._myGlobal._resource("ลบรายการสุดท้าย");
            string __changeItemPriceStr = MyLib._myGlobal._resource("แก้ราคา (xxx=ราคาใหม่)");
            string __changeItemPriceDescStr = MyLib._myGlobal._resource("วิธีป้อน <b>*1*10</b> คือกำหนดให้ราคาใหม่เท่ากับสิบบาท");
            string __changeItemDiscountStr = MyLib._myGlobal._resource("แก้ส่วนลด (xxx=ส่วนลด เช่น 10%)");
            string __changeItemDiscountDescStr1 = MyLib._myGlobal._resource("วิธีป้อน <b>*2*10%</b> คือกำหนดส่วนลดใหม่เท่ากับ 10%");
            string __changeItemDiscountDescStr2 = MyLib._myGlobal._resource("สามารถลดเป็นขั้นได้ เช่น 10%,5% หรือ 10,5%");
            string __changeItemDiscountDescStr3 = MyLib._myGlobal._resource("กรณีไม่มี % ตามหลังโปรแกรมจะถือว่าลดเป็นบาท");
            string __editDrugPayStr = MyLib._myGlobal._resource("แก้ไขรายละเอียดการจ่ายยา");
            string __changeUnitCodeStr = MyLib._myGlobal._resource("เปลี่ยนหน่วยนับ (xxx=บรรทัดที่ต้องการเปลี่ยน)");
            string __openCashDarwerStr = MyLib._myGlobal._resource("เปิดลิ้นชัก");
            string __resetPOSStr = MyLib._myGlobal._resource("เริ่มใหม่ (ล้างหน้าจอขาย)");
            string __fullInvoiceReprint = MyLib._myGlobal._resource("พิมพ์ใบกำกับภาษีเต็มรูปแบบใหม่");
            string __printPrePayFoodStr = MyLib._myGlobal._resource("พิมพ์ใบเรียกเก็บเงิน");
            string __cancelInvoiceStr = MyLib._myGlobal._resource("ยกเลิกใบเสร็จ");
            string __reprintInvoiceStr = MyLib._myGlobal._resource("พิมพ์ใบกำกับภาษีอย่างย่อใหม่");
            string __printFullInvoice = MyLib._myGlobal._resource("พิมพ์ใบกำกับภาษีเต็มรูปแบบ");
            string __closeScreenStr = MyLib._myGlobal._resource("ปิดหน้าจอ");
            string __calcTotal2 = MyLib._myGlobal._resource("คิดเงิน (ปิดการขาย)");
            string __calcFoodTotal = MyLib._myGlobal._resource("ดึงรายการอาหารจากโต๊ะ");
            string __printInvoicePeroidStr = MyLib._myGlobal._resource("พิมพ์ใบเสร็จตามช่วง");
            string __summaryDailyPrintStr = MyLib._myGlobal._resource("พิมพ์ใบสรุปการขายประจำวัน");
            string __customerDetailStr = MyLib._myGlobal._resource("รายละเอียดสมาชิก");
            string __holdBillStr = MyLib._myGlobal._resource("พักบิล");
            string __selectBillHoldStr = MyLib._myGlobal._resource("เลือกบิลพัก");
            string __clearHollBIllStr = MyLib._myGlobal._resource("ล้างบิลพัก");
            string __pointMovementStr = MyLib._myGlobal._resource("แสดงเคลื่อนไหวแต้มสะสม");
            string __saleSummary = MyLib._myGlobal._resource("สรุปการขาย");
            string __openShipStr = MyLib._myGlobal._resource("เปิดกะ");
            string __fillMoneyStr = MyLib._myGlobal._resource("รับเงิน (เงินทอน)");
            string __sendMoneyStr = MyLib._myGlobal._resource("ส่งเงิน");
            string __closeshiftStr = MyLib._myGlobal._resource("ปิดกะ (ให้ส่งเงินก่อนปิดกะ)");
            string __previewDrugLabelStr = MyLib._myGlobal._resource("แสดงฉลากยาก่อนพิมพ์");
            string __drugLablePrint = MyLib._myGlobal._resource("พิมพ์ฉลากยา");
            string __cancelBillListStr = MyLib._myGlobal._resource("แสดงบิลยกเลิก");
            string __invoiceBillListStr = MyLib._myGlobal._resource("แสดงบิลขาย");
            string __addItemQtyStr = MyLib._myGlobal._resource("เพิ่มจำนวนสินค้าล่าสุด (x=จำนวนที่เพิ่ม เช่น <b>+1</b>)");
            string __reduleItemQtyStr = MyLib._myGlobal._resource("ลดจำนวนสินค้าล่าสุด (x=จำนวนที่ลด เช่น <b>-1</b>)");

            string __foodOrderByCodeStr = MyLib._myGlobal._resource("สั่งอาหารด้วยรหัส");
            string __foodCloseTableStr = MyLib._myGlobal._resource("ปิดโต๊ะ (ร้านอาหาร)");

            string __html = @"
<head>
<style type='text/css'>
body,table,tr,td {
	font-family: Tahoma,Arial, Helvetica, sans-serif;
    font-size:12px;
}
</style>
</head>
<body>
<b>Function Key</b><br/>
<b>F1</b>=Help<br/>
<b>F2</b>=" + __findItemStr + @"<br/>
<b>F3</b>=" + __findMemberStr  + @"<br/>
<b>F4</b>=" + __findSaleStr + @"<br/>
<b>F5</b>=" + __findItemSearchLevelStr + @"<br/>
<b>F6</b>=" + __fineCustomerStr + @"<br/>
" + ((MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong
             || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro) ? "<b>F7</b>=" + __foodDiscountStr + @"<br/><b>F8</b>=" + __serviceChargeStr + @"<br/>" : "") + @"
<b>F11</b>=" + __calcTotalStr + @"<br/>
<br/>
<b>" + __specialBarcodeInput + @"</b><br/>
<b>***</b>=" + __deleleLastItemStr + @"<br/>
<b>*1*xxx</b>=" + __changeItemPriceStr + " " + __changeItemPriceDescStr + @"<br/>
<b>*2*xxx</b>=" + __changeItemDiscountStr + " " + __changeItemDiscountDescStr1 + " " + __changeItemDiscountDescStr2 + " " + __changeItemDiscountDescStr3 + @"<br/>
<b>*3*xxx</b>=" + __editDrugPayStr + @"<br/>
<b>*4*xxx</b>=" + __changeUnitCodeStr + @"<br/>
<br/>
<table width=""100%"">
<tr>
<td valign=""top"">
<b>*90</b>=" + __openCashDarwerStr + @"<br/>
<b>*91</b>=" + __resetPOSStr + @"<br/> 
<b>*92</b>=" + __fullInvoiceReprint + @"<br/>" + ((MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong
             || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro ) ? "<b>*93</b>=" + __printPrePayFoodStr + "<br/>" : "") + @"
<b>*94</b>=" + __cancelInvoiceStr + @"<br/>
<b>*96</b>=" + __reprintInvoiceStr + @"<br/>
<b>*97</b>=" + __printFullInvoice  + @"<br/>
<b>*98</b>=" + __closeScreenStr + @"<br/>
<b>*99</b>=" + __calcTotal2 + @"<br/>" + ((MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong
             || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro) ? "<b>*00</b>=" + __calcFoodTotal + "<br/><b>*01</b>=" + __foodOrderByCodeStr + "<br/><b>*02</b>=" + __foodCloseTableStr + "<br/>" : "") + @"
</td>
<td valign=""top"">
<b>*51</b>=" + __printInvoicePeroidStr  + @"<br/>
<b>*52</b>=" + __summaryDailyPrintStr + @"<br/>
<b>*60</b>=" + __customerDetailStr + @"<br/>
<b>*71</b>=" + __holdBillStr + @"<br/>
<b>*72</b>=" + __selectBillHoldStr + @"<br/>
<b>*73</b>=" + __clearHollBIllStr  + @"<br/>
<b>*75</b>=" + __pointMovementStr + @"<br/>
</td>
<td valign=""top"">
<b>*80</b>=" + __saleSummary + @"<br/>
<b>*81</b>=" + __openShipStr + @"<br/>
<b>*82</b>=" + __fillMoneyStr + @"<br/>
<b>*83</b>=" + __sendMoneyStr + @"<br/>
<b>*84</b>=" + __closeshiftStr + @"<br/>
<b>*86</b>=" + __previewDrugLabelStr  + @"<br/>
<b>*87</b>=" + __drugLablePrint + @"<br/>
<b>*88</b>=" + __cancelBillListStr + @"<br/>
<b>*89</b>=" + __invoiceBillListStr + @"<br/>
</td>
</tr>
<table>
<br/>
<b>+x</b>=" + __addItemQtyStr + @"<br/>
<b>-x</b>=" + __reduleItemQtyStr + @"<br/>
            </body>";
            this._webBrowser.DocumentText = __html;
        }

        public _helpForm(string _content)
        {
            InitializeComponent();
            this._webBrowser.DocumentText = _content;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
