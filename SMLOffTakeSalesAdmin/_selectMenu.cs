using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace SMLOffTakeSalesAdmin
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName)
        {

            switch (menuName.ToLower())
            {
                case "menu_import_productmaster": return (new _importDataProductMasterControl());
                case "menu_import_rdm": return (new _importDataRDM_Master());
                case "menu_setup_chanel_master": return (new _importDataChannelMasterControl());
                //case "menu_import_rdm": return (new _import_masterdata());
                //case "menu_setup_chanel_master": return (new _icPrice._icPriceList(0));//กำหนดราคาสินค้า (มาตรฐาน)
                //case "menu_ic_item_saleprice_2": return (new _icPrice._icPriceList(1));//กำหนดราคาสินค้า (ทั่วไป)                
                //case "menu_ic_purchase_price": return (new _icPurchasePrice._icPriceList());// ราคาซื้อ 
                //case "menu_import_data_fomulcolor": return (new _importFcolorDataControl()); //กำหนดสมุดเงินฝากธนาคาร                    
                //case "menu_import_data_fomulcolorscreenfile": return (new _importFcolorDataControlScreenfile()); //กำหนดสมุดเงินฝากธนาคาร
                //case "_check_purchase_price": return (new _mainCondition()); //ตรวจสอบราคาซื้อ
                //case "_check_price": return (new _mainCondition_price()); //ตรวจสอบราคาขาย
                    
              //  case "menu_option": MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, screenName, new SMLERPConfig._option()); break;  // Option
            }
            return null;
        }
        //public static Control _getObject(string menuName, string screenName, string programName)
        //{
        //    MyLib._manageMasterCodeFull __screenFull;
        //    switch (menuName.ToLower())
        //    {
        //        case "menu_setup_agent":
        //            __screenFull = new MyLib._manageMasterCodeFull();
        //            __screenFull._labelTitle.Text = screenName;
        //            __screenFull._dataTableName = _g.d.sml_agent._table;
        //            __screenFull._addColumn(_g.d.sml_agent._code, 10, 20);
        //            __screenFull._inputScreen._setUpper(_g.d.sml_agent._code);
        //            __screenFull._addColumn(_g.d.sml_agent._name, 100, 40);                    
        //            __screenFull._addColumn(_g.d.sml_agent._email, 100, 40);                    
        //            __screenFull._finish();
        //            return __screenFull;
        //        case "menu_setup_ic_unit":
        //            __screenFull = new MyLib._manageMasterCodeFull();
        //            __screenFull._getTemplate = true;                   
        //            __screenFull._labelTitle.Text = screenName;
        //            __screenFull._dataTableName = _g.d.ic_unit._table;
        //            __screenFull._addColumn(_g.d.ic_unit._code, 10, 20);
        //            __screenFull._inputScreen._setUpper(_g.d.ic_unit._code);
        //            __screenFull._addColumn(_g.d.ic_unit._name_1, 100, 40);
        //            __screenFull._addColumn(_g.d.ic_unit._name_2, 100, 40);
                   
        //            __screenFull._finish();
        //            return __screenFull;
        //        case "menu_option": MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, screenName, new SMLERPConfig._option()); break;  // Option
        //        case "menu_setup_ic_class":
        //            __screenFull = new MyLib._manageMasterCodeFull();
        //            __screenFull._labelTitle.Text = screenName;
        //            __screenFull._dataTableName = _g.d.ic_class._table;
        //            __screenFull._addColumn(_g.d.ic_class._code, 10, 20);
        //            __screenFull._inputScreen._setUpper(_g.d.ic_class._code);
        //            __screenFull._addColumn(_g.d.ic_class._name_1, 100, 40);
        //            __screenFull._addColumn(_g.d.ic_class._name_2, 100, 40);
        //            __screenFull._finish();
        //            return __screenFull;
        //    }
        //    return null;
        //}
    }
}
