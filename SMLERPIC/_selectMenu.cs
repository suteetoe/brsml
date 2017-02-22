using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPIC
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName)
        {
            switch (menuName.ToLower())
            {
                case "menu_ic_item_detail": return (new SMLERPIC._icMain(0));
                case "ic_warehouse_and_location_group": return (new SMLERPIC._whLocationGroup());
                case "menu_ic_item_detail_barcode": return (new SMLERPIC._icMain(1));
                case "menu_ic_item_add_barcode_fast": return (new SMLERPIC._icAddBarcodeFastControl());
                case "menu_ic_item_barcode_checker": return (new SMLERPIC._barcodeCheckerControl());
                case "menu_ic_item_level": return (new SMLERPIC._icSearchLevel());
                case "menu_ic_item_detail_2": return (new SMLERPIC._icMainDetail(0));
                case "menu_ic_item_detail_2_barcode": return (new SMLERPIC._icMainDetail(1));
                case "menu_ic_item_serial": return (new SMLERPIC._icSerialNumber());//กำหนดทะเบียนผลิตภัณฑ์
                case "menu_ic_item_barcode": return (new SMLERPIC._icBarcode()); //กำหนดบาร์โค้ดสินค้า
                case "menu_ic_item_barcode_discount": return (new SMLERPIC._icBarcodeDiscount()); //กำหนดส่วนลดตามบาร์โค้ดสินค้า 
                case "menu_ic_item_point_update": return (new SMLERPIC._icPointUpdateControl()); //ปรับปรุงสถานสะสินค้า (แต้ม)
                case "menu_ic_item_barcode_checker_ean13": return new SMLBarcodeManage._itemBarcodeCheckerEan13();
                case "menu_ic_item_barcode_laser_print": return new SMLBarcodeManage._itemBarcodePrint();
                case "menu_ic_unit_checker": return new SMLERPIC._icUnitCheckerControl();

                case "menu_wh_balance": return new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.คลัง_ยอดคงเหลือสินค้ายกมา, menuName);
                case "menu_wh_in_purchase": return new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.คลัง_รับสินค้าเข้า_จากการซื้อ, menuName);
                case "menu_wh_out": return new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.คลัง_จ่ายสินค้าออก_จากการขาย, menuName);
                case "menu_wh_stk_count": return new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.คลัง_ตรวจนับสินค้า, menuName);
                case "menu_wh_adj": return new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.คลัง_ปรับปรุงเพิ่มลดสินค้า, menuName);

                case "menu_ic_stk_count": return new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า, menuName);
                case "menu_ic_stk_balance": return new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา, menuName);
                case "menu_ic_finish_receive": return new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป, menuName);
                case "menu_ic_issue": return new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ, menuName);

                // toe
                case "menu_ic_request_issue": return new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ, menuName);

                case "menu_ic_return_receive": return new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก, menuName);
                case "menu_ic_transfer_wh_out": return new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.สินค้า_โอนออก, menuName);
                case "menu_ic_stock_result": return new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.StockCheckResult, menuName);
                case "menu_ic_stk_adjust_auto": return new SMLInventoryControl._stockAutoAdjustControl(); // ประมวลผลจากการตรวจนับ
                case "menu_ic_stk_adjust": return new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก, menuName);

                case "menu_ic_stk_adjust_subtract": return new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด, menuName); // toe

                case "menu_ic_item_formula_template": return (new SMLInventoryControl._icPrice._icPriceFormulaTemplate()); // กำหนดราคาสินค้า (สูตร)
                case "menu_ic_item_saleprice_formula": return (new SMLInventoryControl._icPrice._icPriceFormula()); // กำหนดราคาสินค้า (สูตร)
                case "menu_ic_item_saleprice": return (new SMLInventoryControl._icPrice._icPriceList(0)); // กำหนดราคาสินค้า (มาตรฐาน)
                case "menu_ic_item_saleprice_2": return (new SMLInventoryControl._icPrice._icPriceList(1)); // กำหนดราคาสินค้า (ทั่วไป)
                case "menu_ic_item_picture": return (new SMLERPIC._icMainDetailPicture());//กำหนดรูปภาพสินค้า
                case "menu_ic_item_set": return (new SMLERPIC._icMainProductSet());// สินค้าชุด
                case "menu_ic_item_color_set": return (new SMLERPIC._icMainColorSet());// สูตรผสมสี
                case "menu_ic_recalc": return (new SMLERPIC._icRecalc());// คำนวณใหม่
                case "menu_ic_stk_color_adj": return (new SMLERPIC._icStockColorAdj());// ประมวลผลแม่สี (สร้างใบเบิก,ใบรับอัตโนมัติ)
                case "menu_ic_stk_color_recalc": return (new SMLERPIC._icStockColorRecalc());// ประมวลผลแม่สี (สร้างใบเบิก,ใบรับอัตโนมัติ)
                case "menu_ic_purchase_price": return (new SMLInventoryControl._icPurchasePrice._icPriceList());// ราคาซื้อ 
                case "menu_ic_purchase_premium": return (new SMLERPIC._icPurchasePermium());// ของแถมซื้อ 
                case "menu_ic_build_price_list": return (new SMLERPIC._icBuildPriceList());// สร้างตารางราคาสี
                case "menu_ic_info_color": return (new SMLInventoryControl._colorInfo()); // แสดงสูตรสี
                case "pos_promotion_formula": return (new SMLERPIC._icPromotion._formula()); // สูตร Promotion
                //
                case "menu_ic_finish_receive_cancel": return (new _cancelControl(_g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก));
                case "menu_ic_issue_cancel": return (new _cancelControl(_g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก));

                case "menu_ic_request_issue_cancel": return (new _cancelControl(_g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ_ยกเลิก)); // toe
                case "menu_ic_return_receive_cancel": return (new _cancelControl(_g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก));
                case "menu_ic_transfer_wh_out_cancel": return (new _cancelControl(_g.g._transControlTypeEnum.สินค้า_โอนออก_ยกเลิก));
                case "menu_ic_stk_adjust_over_cancel": return (new _cancelControl(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก));
                case "menu_ic_stk_adjust_lost_cancel": return (new _cancelControl(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก));

                //toe
                case "menu_import_ic_picture": return (new _icImportPicture()); // นำเข้ารูปภาพ
                case "menu_ic_item_multibarcode_discount": return new _icMultiBarcodeDiscount();
                case "menu_ic_multi_item_level": return new _icSearchLevelMultiItem();// กำหนด ลำดับการค้นหาแบบเร็ว
                case "menu_ic_serialnumber_checker": return new _icSerialStockCheck();
                case "menu_ic_item_barcode_picture": return (new SMLERPIC._icBarcodePicture());//กำหนดรูปภาพสินค้า 
                case "menu_ic_description": return (new SMLERPIC._icDescription()); // toe รายละเอียดสินค้า

                case "menu_ic_document_picture": return (new SMLERPIC._icDocPicture());
                case "menu_ic_price_adjust": return (new SMLInventoryControl._icPriceManage._icPriceManageList());

                case "menu_ic_discount": return (new SMLInventoryControl._icPrice._icDiscount());

                // toe imex project
                case "menu_ic_quality_control": return (new _icQualityControl());
                case "menu_print_tag_label": return (new SMLInventoryControl._icTransLabelPrintControl());

                case "menu_ic_shipping": return new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.สินค้า_บันทึกการจัดส่ง, menuName);
                case "menu_ic_description_fast": return new _icEditDetailFast();

                case "menu_ic_lot_manage": return new _icLotManage();

                case "menu_ic_all_price": return (new SMLInventoryControl._icPrice._icAllPrice());

                // singha
                case "menu_wh_deposits": return new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก, menuName);
                case "menu_wh_issue": return new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก, menuName);
                case "menu_wh_issue_return": return new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก, menuName);

                case "menu_ic_request_transfer": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.สินค้า_ขอโอน, menuName));

                // imes
                case "menu_ic_specific_search": return (new _icSpecificSearch());

                case "menu_ic_finish_receive_ap": return new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป, menuName);
                case "menu_ic_finish_receive_ar": return new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป, menuName);
                case "menu_ic_issue_ap": return new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ, menuName);
                case "menu_ic_issue_ar": return new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ, menuName);
            }
            return null;
        }
    }
}
