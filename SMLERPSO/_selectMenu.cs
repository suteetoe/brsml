using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPSO
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName)
        {
            switch (menuName.ToLower())
            {
                case "menu_inquiry_data": return (new SMLERPSO._soInquiry()); //Inquiry : ความต้องการสินค้าของลูกค้า
                //
                case "menu_so_quotation_order": return (new SMLERPSO._soQuotation(menuName)); //Quotation : ใบเสนอราคา
                case "menu_so_quotation_approve": return (new SMLERPSO._soQuotationApproval());// อนุมัติใบเสนอราคา
                case "menu_so_quotation_cancel": return (new SMLERPSO._soQuotationCancel());// ยกเลิกใบเสนอราคา
                //
                case "menu_so_sale_order": return (new SMLERPSO._soSaleorder(menuName)); //SaleOrder : ใบสั่งขาย
                case "menu_so_sale_order_approve": return (new SMLERPSO._soSaleorderApproval()); //บันทึกอนุมัติใบสั่งขายสินค้า
                case "menu_so_sale_order_cancel": return (new SMLERPSO._soSaleorderCancel()); //SaleOrder : ใบสั่งขาย
                //
                //
                case "menu_so_inquiry_order": return (new SMLERPSO._soReserved(menuName)); //Reserved : ใบสั่งจองสินค้า
                case "menu_so_inquiry_order_approve": return (new SMLERPSO._soReservedApproval(menuName));
                case "menu_so_inquiry_order_cancel": return (new SMLERPSO._soReservedCancel());
                //
                case "menu_so_invoice": return (new SMLERPSO._soInvoice(menuName, "" + _g.d.ic_trans._is_pos + "=0", false)); //Invoice : ขายสินค้า/บริการ (ไม่รวมของ pos)
                case "menu_so_invoice_cancel": return (new SMLERPSO._soInvoiceCancel(menuName, "coalesce(" + _g.d.ic_trans._is_pos + ", 0)=0", false)); //Invoice : ขายสินค้า/บริการ
                //
                case "menu_so_invoice_add": return (new SMLERPSO._soInvoiceAdd(menuName)); //Invoice : เพิ่มหนี้
                case "menu_so_invoice_add_cancel": return (new SMLERPSO._soInvoiceAddCancel()); //Invoice : เพิ่มหนี้
                //
                case "menu_so_credit_note":
                case "menu_so_credit_note_pos":
                    return (new SMLERPSO._creditNote(menuName)); // รับคืนสินค้า/ลดหนี้
                case "menu_so_credit_note_cancel":
                case "menu_so_credit_note_cancel_pos":
                    return (new SMLERPSO._creditNoteCancel()); // รับคืนสินค้า/ลดหนี้
                //
                case "menu_estimate_data": return (new SMLERPSO._soEstimate()); //Estimate : กำหนดราคาขายสินค้า
                //case "menu_inquiry_data": return (new SMLERPSO._testictrans());
                //
                case "menu_so_deposit_receive_1": return (new SMLERPSO._so_advance_money());//บันทึกเงินล่วงหน้า
                case "menu_so_deposit_receive_1_cancel": return (new SMLERPSO._so_advance_money_cancel());//บันทึกเงินล่วงหน้า
                //
                case "menu_so_deposit_return_1": return (new SMLERPSO._so_advance_money_return());//รับคืนเงินล่วงหน้า
                case "menu_so_deposit_return_1_cancel": return (new SMLERPSO._so_advance_money_return_cancel());//รับคืนเงินล่วงหน้า
                //------------------------------------------------------------------------------------------------------------
                //บันทึกเงินมัดจำ
                case "menu_so_deposit_receive_2": return (new SMLERPSO._so_deposit_money());
                case "menu_so_deposit_receive_2_cancel": return (new SMLERPSO._so_deposit_money_cancel());
                //รับคืนเงินมัดจำ
                case "menu_so_deposit_return_2": return (new SMLERPSO._so_deposit_money_return());
                case "menu_so_deposit_return_2_cancel": return (new SMLERPSO._so_deposit_money_return_cancel());

                // POS
                case "pos_invoice_list_pos": return (new SMLERPSO._soInvoice(menuName, _g.d.ic_trans._is_pos + "=1 and ( coalesce(" + _g.d.ic_trans._doc_ref + ", '') =\'\') ", true));
                case "pos_invoice_cancel_pos": return (new SMLERPSO._soInvoiceCancel(menuName, _g.d.ic_trans._is_pos + "=1", true));
                case "pos_full_invoide_list_pos": return (new SMLERPSO._soFullInvoice(menuName, _g.d.ic_trans._is_pos + "=1 and (coalesce(" + _g.d.ic_trans._doc_ref + ", '') <> \'\')", true));

                case "menu_so_doc_picture": return (new SMLERPSO._so_docPicture());

                    // imex
                case "menu_sale_information": return (new _saleInformation());
            }
            return null;
        }
    }
}
