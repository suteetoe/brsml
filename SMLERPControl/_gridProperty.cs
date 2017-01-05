using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SMLERPControl
{    
   public class _gridProperty
   {
       public Object resultData = new Object();
       public ArrayList _setColumnFieldWidth(int _columnWidth,string _tabelName)
       {
           ArrayList __columnField = new ArrayList();
           int[] __Column4 = { 15, 45, 20, 20 };
           int[] __Column5 = { 15, 40, 15, 15, 15 };
           int[] __Column6 = { 10, 30, 15, 15, 15, 15 };
           int[] __Column7 = { 10, 30, 12, 12, 12, 12, 12 };
           int[] __Column8 = { 10, 30, 10, 10, 10, 10, 10, 10 };
           int[] __Column9 = { 10, 20, 10, 10, 10, 10, 10, 10, 10 };
           int[] __Column10 = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
           int[] __Column11 = { 8, 12, 8, 8, 8, 8, 8, 8, 8, 12, 12 };
           int[] __Column15 = { 6, 10, 8, 7, 6, 6, 6, 8, 7, 6, 6, 6, 6, 6, 6 };
           switch (_columnWidth)
           {
               case 4:
                   for (int i = 0; i < __Column4.Length; i++)
                   {
                       __columnField.Add((int)__Column4[i]);
                   }
                   break;
               case 5:
                   for (int i = 0; i < __Column5.Length; i++)
                   {
                       __columnField.Add((int)__Column5[i]);
                   }
                   break;
               case 6:
                   for (int i = 0; i < __Column6.Length; i++)
                   {
                       __columnField.Add((int)__Column6[i]);
                   }
                   break;
               case 7:
                   for (int i = 0; i < __Column7.Length; i++)
                   {
                       __columnField.Add((int)__Column7[i]);
                   }
                   break;
               case 8:
                   for (int i = 0; i < __Column8.Length; i++)
                   {
                       __columnField.Add((int)__Column8[i]);
                   }
                   break;
               case 9:
                   for (int i = 0; i < __Column9.Length; i++)
                   {
                       __columnField.Add((int)__Column9[i]);
                   }
                   break;
               case 10:
                   for (int i = 0; i < __Column10.Length; i++)
                   {
                       __columnField.Add((int)__Column10[i]);
                   }
                   break;
               case 11:
                   for (int i = 0; i < __Column11.Length; i++)
                   {
                       __columnField.Add((int)__Column11[i]);
                   }
                   break;                   
               case 15:
                   for (int i = 0; i < __Column15.Length; i++)
                   {
                       __columnField.Add((int)__Column15[i]);
                   }
                   break; 
           }
           return __columnField;
       }
       public ArrayList _setFieldColumnSearch(string _columnName,bool _itemUse)
       {
           ArrayList __fieldSearch = new ArrayList();           
           switch (_columnName)
           {
               case "ic_code":
                   __fieldSearch.Add("ic_code");
                   __fieldSearch.Add(_g.g._search_screen_ic_inventory);
                   __fieldSearch.Add(_g.d.ic_inventory._table);
                   __fieldSearch.Add(_g.d.ic_inventory._code);
                   __fieldSearch.Add(_g.d.ic_inventory._name_1);                   
                   break;
               case "erp_department_list":
                   __fieldSearch.Add("erp_department_list");
                   __fieldSearch.Add(_g.g._search_screen_erp_department_list);
                   __fieldSearch.Add(_g.d.erp_department_list._table);
                   __fieldSearch.Add(_g.d.erp_department_list._code);
                   __fieldSearch.Add(_g.d.erp_department_list._name_1);                   
                   break;
               case "shelf_code":
                   __fieldSearch.Add("shelf_code");
                   __fieldSearch.Add(_g.g._search_master_ic_shelf);
                   __fieldSearch.Add(_g.d.ic_shelf._table);
                   __fieldSearch.Add(_g.d.ic_shelf._code);
                   __fieldSearch.Add(_g.d.ic_shelf._name_1);                    
                   break;
               case "unit_code":
                   __fieldSearch.Add("unit_code");
                   if (_itemUse == true)
                   {
                       __fieldSearch.Add(_g.g._search_master_ic_unit_use);
                       __fieldSearch.Add(_g.d.ic_unit_use._table);
                       __fieldSearch.Add(_g.d.ic_unit_use._code);
                       __fieldSearch.Add(_g.d.ic_unit_use._name_1);
                   }
                   else
                   {
                       __fieldSearch.Add(_g.g._search_master_ic_unit);
                       __fieldSearch.Add(_g.d.ic_unit._table);
                       __fieldSearch.Add(_g.d.ic_unit._code);
                       __fieldSearch.Add(_g.d.ic_unit._name_1);
                   }
                   break;
               case "wh_code":
                   __fieldSearch.Add("wh_code");
                   __fieldSearch.Add(_g.g._search_master_ic_warehouse);
                   __fieldSearch.Add(_g.d.ic_warehouse._table);
                   __fieldSearch.Add(_g.d.ic_warehouse._code);
                   __fieldSearch.Add(_g.d.ic_warehouse._name_1);                    
                   break;
               case "ic_color":
                   __fieldSearch.Add("ic_color");
                   __fieldSearch.Add(_g.g._search_master_ic_color);
                   __fieldSearch.Add(_g.d.ic_color._table);
                   __fieldSearch.Add(_g.d.ic_color._code);
                   __fieldSearch.Add(_g.d.ic_color._name_1);
                   break;
               case "ic_size":
                   __fieldSearch.Add("ic_size");
                   __fieldSearch.Add(_g.g._search_master_ic_size);
                   __fieldSearch.Add(_g.d.ic_size._table);
                   __fieldSearch.Add(_g.d.ic_size._code);
                   __fieldSearch.Add(_g.d.ic_size._name_1);
                   break;             
           }           
           return __fieldSearch;
       }    
    }
    public class _searchField
    {
        // ขีดเดียวใช้แทนตัวแปรที่ใช้ได้ทั้ง Control 
        // สองขีดใช้แทนตัวแปรที่ใช้ในเฉพาะ Function
        public string _screenName = "";         // ตัวแปรที่เก็บ Screen ที่เก็ย Table
        public string _screenSearch;            // ตัวแปรที่เก็บ View ที่อยู่ใน SMLERPGlobal
        public string _setFieldSearch = "";     // ฟิวด์ที่เราค้นหา
        public string _setFieldSearchName = "";
        public string _setTableSearch = "";     // Table ที่เราเข้าไปดึงข้อมูล
        public string _setTableName = "";
        public string _selectFieldCode = "";    // ฟิวด์ที่เอาข้อมูลมาใส่
        public string _selectFieldName = "";
        public bool _setFieldFormat = false;    // กำหนดให้ฟิวด์ที่ค้นหาเป็นรูปแบบ  Code/Name
        public bool _setSearchFull = false;     // กำหนดขนาดของ Grid ค้นหา  True = Maximize , False =  Normal      
    }
    public class _setColumnFieldName
    {
        public string _tableName;
        public string _DocNo;
        public string _DocDate;
        public string _itemCheck;               // Check
        public int _itemColumn;                 // ตำแหน่ง Column
        public string _itemCode;                // รหัสสินค้า    
        public string _itemCodeRep;             // รหัสสินค้าทดแทน
        public string _itemName;                // ชื่อสินค้า
        public string _itemType;                // ประเภทสินค้า
        public string _itemGroup;               // กลุ่มสินค้า
        public string _itemCategory;            // หมวดสินค้า
        public string _itemBrand;               // ยี่ห้อสินค้า
        public string _itemPattarn;             // รูปแบบสินค้า
        public string _itemDesign;              // รูปทรงสินค้า
        public string _itemSize;                // ขนาดสินค้า
        public string _itemGrade;               // เกรดสินค้า
        public string _itemClass;               // ระดับสินค้า
        public string _itemColor;               // สีสินค้า
        public string _itemCost;                // ต้นทุน
        public string _itemShelf;               // ที่เก็บสินค้า
        public string _itemCharacter;           // ลักษณะสินค้า
        public string _itemUnit;                // หน่วยนับสินค้า
        public string _itemUnitType;            // ประเภทหน่วยนับ
        public string _itemQty;                 // จำนวนสินค้า    
        public string _itemWarehouse;           // คลังสินค้า
        public string _itemAmount;              // ราคาสินค้า
        public string _itemBarCode;             // บาร์โค้ด
        public string _itemStatus;              // สถานะ
        public string _itemDescrip;             // รายละเอียด
        public string _itemQtyMin;              // จำนวนต่ำสุด
        public string _itemQtyMax;              // จำนวนสูงสุด
        public string _itemPrice;               // ราคา
        public string _itemDiscount;            // ส่วนลด
        public string _itemBillNo;              // เลขที่บิล
        public string _itemBillDate;            // วันที่บิล
        public string _itemSalePrice;           // ราคาขาย
        public string _itemReturnPrice;         // ราคาคืน
        public string _itemSumprice;
        public string _itemSumValue;
        public string _itemSide;
        public string _itemSection;
        public string _itemProject;
        public string _itemAllocate;
        public string _itemAp;
        public string _itemApname;
        public string _itemRemark;
        public string _itemValue;
        public string _itemDueDate;
        public string _itemRequestDate;
        public bool _itemPopup;
        public object _itemCodition;
        public string _itemTransection;
        // โอนสินค้า เฉพาะ 1 กับ 2 จะเป็นการโอน
        public string _itemBranch1;
        public string _itemSide1;
        public string _itemSection1;
        public string _itemWarehouse1;
        public string _itemShelf1;
        public string _itemBranch2;
        public string _itemSide2;
        public string _itemSection2;
        public string _itemWarehouse2;
        public string _itemShelf2;
        public string _itemCredit;
        public string _itemSending;
        public string _itemFromQty;
        public string _itemToQty;
    }
    
    //เลือกหน้าจอที่ต้องการสร้าง Grid ของ Purchase Order and Sale Order
    public enum TableDisplayPOSOName
    {      
        /// <summary>
        /// รายละเอียดการสืบราคา = POInvestigate
        /// รายละเอียดใบเสนอราคา = POQuotation
        /// รายละเอียดเสนอซื้อสินค้า = PORequest
        /// รายละเอียดสั่งซื้อ/สั่งจอง = POInquiry        
        /// บันทึกซื้อสินค้า = POBilling
        /// รายละเอียดคืน/เพิ่มสินค้า ลดหนี้/เพิ่มหนี้ = POSOCreditnote
        /// รายละเอียดรับสินค้าซื้อ/ตั้งหนี้ = POGoodreceive
        /// รายละเอียดราคาซื้อ = POPrice
        /// </summary>
        POBilling,
        POInvestigate,
        POQuotation,
        POSOCreditnote,
        POGoodreceive,
        PORequest,
        POPrice,
        POInquiry
        
        
    }   
    //เลือกหน้าจอที่ต้องการสร้าง Grid ของ Inventory
    public enum TableDisplayICName
    {
        /// <summary>
        /// รายละเอียดสินค้ายกมา = StockBalance
        /// รายละเอียดการเบิก = StockRequest
        /// รายละเอียดรับคืนสินค้าเบิก = StockReturn
        /// รายละเอียดตรวจนับสินค้า = StockChecking
        /// รายละเอียดปรับปรุงสต็อก =  StockAdjust        
        /// รายละเอียดโอนสินค้า = StockTransfer
        /// รายละเอียดสินค้าประกอบ = ItemBuild
        /// บาร์โค้ดสินค้า = InventoryBarcode
        /// สินค้าทดแทน = InventoryReplace
        /// สินค้าพ่วง = InventoryAppend
        /// สินค้าชุด = InventorySet
        /// สินค้าตามคลัง = InventoryWarehouse
        /// สินค้าสำเร็จรูป = InventoryFinishGood
        /// สินค้าตามลูกค้า = InventoryCustomer
        /// สินค้าตามผู้จำหน่าย = InventorySupplier
        /// </summary>
        StockBalance,
        StockRequest,
        StockReturn,
        StockChecking,
        StockAdjust,       
        StockTransfer,
        ItemBuild,
        InventoryBarcode,
        InventoryReplace,
        InventoryAppend,
        InventorySet,
        InventoryWarehouse,
        InventoryFinishGood,
        InventoryCustomer,
        InventorySupplier
    }   
    // รูปแบบการค้นหา สินค้า
    public enum SearchItemFormat
    {
        Normal,
        Advance
    }
    public enum SearchMasFormat
    {
        Style1,
        Style2
    }  
}
