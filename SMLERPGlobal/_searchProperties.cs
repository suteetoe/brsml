using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SMLERPGlobal
{
    public class _searchProperties
    {
        // _cName = Column Name 
        // _masterUse = Master Use Select กรณีที่ ชื่อฟิวด์เหมือนกัน เพิ่ม 0 หรือ 1
        // _tName = Table Name
        public ArrayList _setColumnSearch(string _cName, int _masterUse, string _tName)
        {
            return _setColumnSearch(_cName, _masterUse, _tName, "");
        }

        public ArrayList _setColumnSearch(string _cName, int _masterUse, string _tName, string screenCode)
        {
            return _setColumnSearch(_cName, _masterUse, _tName, screenCode, "");
        }

        public ArrayList _setColumnSearch(string _cName, int _masterUse, string _tName, string screenCode, string where)
        {
            ArrayList __list = new ArrayList();
            if (_cName.Equals(_g.d.ic_trans._doc_quotation_no))
            {
                __list.Add(_g.g._search_screen_ขาย_ใบเสนอราคา);
                __list.Add(_g.d.ic_trans._table);
            }
            else
                if (_cName.Equals(_g.d.ic_trans._doc_po_request))
            {
                __list.Add(_g.g._search_screen_ซื้อ_เสนอซื้อ);
                __list.Add(_g.d.ic_trans._table);
            }
            else
                    if ((_cName.Equals(_g.d.ic_trans_detail._unit_code) || _cName.Equals(_g.d.ic_trans_detail._approval_unit) || _cName.Equals(_g.d.ic_inventory_detail._start_purchase_unit) || _cName.Equals(_g.d.ic_inventory_detail._start_unit_code)
                         || _cName.Equals(_g.d.ic_inventory_detail._start_sale_unit) || _cName.Equals(_g.d.ic_promotion_detail._unit_code)
                         || _cName.Equals(_g.d.ic_promotion_detail._free_ic_unit)) && _masterUse == 0)
            {
                __list.Add(_g.g._search_master_ic_unit_use);
                __list.Add(_g.d.ic_unit_use._table);
            }
            else if (_cName.Equals(_g.d.ic_inventory._item_model))
            {
                __list.Add(_g.g._search_master_ic_model);
                __list.Add(_g.d.ic_model._table);
            }
            else if (_cName.Equals(_g.d.ic_trans_detail._ref_doc_no))
            {
                __list.Add(_g.g._search_screen_ซื้อ_เสนอซื้อ);
                __list.Add(_g.d.ic_trans_detail._table);
            }
            else if (_cName.Equals(_g.d.ic_trans._user_request))
            {
                __list.Add(_g.g._search_screen_erp_user);
                __list.Add(_g.d.erp_user._table);
            }
            else if (_cName.Equals(_g.d.ic_trans._user_request_transfer))
            {
                __list.Add(_g.g._search_screen_erp_user);
                __list.Add(_g.d.erp_user._table);
            }
            else if (_cName.Equals(_g.d.ic_trans._approve_code))
            {
                __list.Add(_g.g._search_screen_erp_user_group);
                __list.Add(_g.d.erp_user_group._table);
                if (where.Length > 0)
                {
                    __list.Add(where);
                }
            }
            else if (_cName.Equals(_g.d.ic_trans._sale_group))
            {
                __list.Add(_g.g._search_screen_erp_user_group);
                __list.Add(_g.d.erp_user_group._table);
                __list.Add(_g.d.erp_user_group._is_sale_group + "=1");
            }
            else if (_cName.Equals(_g.d.ic_trans._transport_code))
            {
                __list.Add(_g.g._search_screen_erp_transport);
                __list.Add(_g.d.transport_type._table);
            }
            //somruk
            else if (_cName.Equals(_g.d.ic_trans._doc_no) || _cName.Equals(_g.d.ic_trans._doc_format_code))
            {
                // toe ใส่ where เอกสารตามสาขา
                string __branch_filter = "";
                if (_g.g._companyProfile._branchStatus == 1 || _g.g._companyProfile._change_branch_code == false)
                {
                    __branch_filter = " AND ((coalesce(" + _g.d.erp_doc_format._use_branch_select + ", 0) = 0 ) or ( '**' || replace(" + _g.d.erp_doc_format._branch_list + ", ',', '**,**') || '**' like '%**" + MyLib._myGlobal._branchCode + "**%'))";
                }

                __list.Add(_g.g._search_screen_erp_doc_format);
                __list.Add(_g.d.erp_doc_format._table);
                __list.Add(MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code, false) + "=\'" + screenCode.ToUpper() + "\'" + __branch_filter);
            }
            else if (_cName.Equals(_g.d.ic_trans_detail._branch_code) || _cName.Equals(_g.d.ic_trans._branch_code_to))
            {
                __list.Add(_g.g._search_master_erp_branch_list);
            }
            else if (_cName.Equals(_g.d.ic_trans_detail._wh_code) ||
                _cName.Equals(_g.d.ic_inventory_detail._start_purchase_wh) || _cName.Equals(_g.d.ic_inventory_detail._start_sale_wh)
                || _cName.Equals(_g.d.ic_inventory_detail._ic_out_wh) || _cName.Equals(_g.d.ic_wh_shelf._wh_code))
            {
                __list.Add(_g.g._search_master_ic_warehouse);
                __list.Add(_g.d.ic_warehouse._table);
            }
            else if (_cName.Equals(_g.d.ic_trans_detail._shelf_code) ||
                _cName.Equals(_g.d.ic_inventory_detail._start_purchase_shelf) || _cName.Equals(_g.d.ic_inventory_detail._start_sale_shelf)
                || _cName.Equals(_g.d.ic_inventory_detail._ic_out_shelf) || _cName.Equals(_g.d.ic_wh_shelf._shelf_code))
            {
                __list.Add(_g.g._search_master_ic_shelf);
                __list.Add(_g.d.ic_shelf._table);
            }
            else if ((_cName.Equals(_g.d.ic_trans._doc_group)) || (_cName.Equals(_g.d.ic_purchase_price._doc_group)))
            {
                __list.Add(_g.g._search_screen_erp_doc_group);
                __list.Add(_g.d.erp_doc_group._table);
            }
            else if (_cName.Equals(_g.d.ic_trans._doc_ref))
            {
                __list.Add(_g.g._search_screen_erp_doc_group); // ยังไม่มี Template
            }
            else if (_cName.Equals(_g.d.ic_trans._sale_code))
            {
                __list.Add(_g.g._search_screen_erp_user);
            }
            else if (_cName.Equals(_g.d.ic_trans._sender_code))
            {
                __list.Add(_g.g._search_screen_erp_user);
            }
            else if (_cName.Equals(_g.d.ic_trans._adjust_reason))
            {
                __list.Add(_g.g._search_master_ic_adjust_reason);
                __list.Add(_g.d.ic_stk_adjust_reason._table);
            }
            else if (_cName.Equals(_g.d.ic_trans._side_code) || _cName.Equals(_g.d.ic_trans._side_code_to))
            {
                __list.Add(_g.g._search_screen_erp_side_list);
            }
            else if (_cName.Equals(_g.d.ic_trans._department_code) || _cName.Equals(_g.d.ic_trans._department_code_to))
            {
                __list.Add(_g.g._search_screen_erp_department_list);
            }
            else if (_cName.Equals(_g.d.ic_trans._allocate_code) || _cName.Equals(_g.d.ic_trans._allocate_code_to))
            {
                __list.Add(_g.g._search_master_erp_allocate_list);
            }
            else if (_cName.Equals(_g.d.ic_trans._project_code) || _cName.Equals(_g.d.ic_trans._project_code_to))
            {
                __list.Add(_g.g._search_master_erp_project_list);
            }
            else if (_cName.Equals(_g.d.ic_trans._job_code) || _cName.Equals(_g.d.ic_trans._job_code_to))
            {
                __list.Add(_g.g._search_master_erp_job_list);
            }
            else if (_cName.Equals(_g.d.ic_trans._ap_code))
            {
                __list.Add(_g.g._search_screen_ap);
                __list.Add(_g.d.ap_supplier._table);
            }
            else if (_cName.Equals(_g.d.ic_trans._ar_code))
            {
                __list.Add(_g.g._search_screen_ar);
                __list.Add(_g.d.ar_customer._table);
            }
            else if (_cName.Equals(_g.d.ic_promotion._ar_type))
            {
                __list.Add(_g.g._search_master_ar_type);
            }
            else if (_cName.Equals(_g.d.ic_promotion._ar_group))
            {
                __list.Add(_g.g._search_master_ar_group);
            }
            else if (_cName.Equals(_g.d.ic_inventory_detail._ic_code) || (_cName.Equals(_g.d.ic_promotion_detail._free_ic_code)))
            {
                __list.Add(_g.g._search_screen_ic_inventory);
                __list.Add(_g.d.ic_inventory._table);
            }
            else if ((_cName.Equals(_g.d.ic_inventory._unit_cost) || _cName.Equals(_g.d.ic_inventory._unit_standard) || _cName.Equals(_g.d.ic_unit_use._code)
                || _cName.Equals(_g.d.ic_opposite_unit._unit_code)) && _masterUse == 1)
            {
                __list.Add(_g.g._search_master_ic_unit);
                __list.Add(_g.d.ic_unit._table);
            }
            else if (_cName.Equals(_g.d.ic_inventory._income_type))
            {
                __list.Add(_g.g._search_screen_income_list);
            }
            else if (_cName.Equals(_g.d.ic_inventory_detail._tax_import))
            {
                __list.Add(_g.g._search_master_ic_import_duty);
            }
            else if (_cName.Equals(_g.d.ic_inventory._item_brand))
            {
                __list.Add(_g.g._search_master_ic_brand);
            }
            else if (_cName.Equals(_g.d.ic_inventory._item_pattern))
            {
                __list.Add(_g.g._search_master_ic_pattern);
            }
            else if (_cName.Equals(_g.d.ic_inventory._item_design))
            {
                __list.Add(_g.g._search_master_ic_design);
            }
            else if (_cName.Equals(_g.d.ic_inventory._item_grade))
            {
                __list.Add(_g.g._search_master_ic_grade);
            }
            else if (_cName.Equals(_g.d.ic_inventory._item_category))
            {
                __list.Add(_g.g._search_master_ic_category);
            }
            else if (_cName.Equals(_g.d.ic_inventory._group_main))
            {
                __list.Add(_g.g._search_master_ic_group);
            }
            else if (_cName.Equals(_g.d.ic_inventory._group_sub))
            {
                __list.Add(_g.g._search_master_ic_group_sub);
            }
            else if (_cName.Equals(_g.d.ic_inventory._item_class))
            {
                __list.Add(_g.g._search_master_ic_class);
            }
            else if (_cName.Equals(_g.d.ic_inventory_detail._dimension_1) || _cName.Equals(_g.d.ic_inventory_detail._dimension_2) ||
                _cName.Equals(_g.d.ic_inventory_detail._dimension_3) || _cName.Equals(_g.d.ic_inventory_detail._dimension_4) ||
                _cName.Equals(_g.d.ic_inventory_detail._dimension_5) || _cName.Equals(_g.d.ic_inventory_detail._dimension_6) ||
                _cName.Equals(_g.d.ic_inventory_detail._dimension_7) || _cName.Equals(_g.d.ic_inventory_detail._dimension_8) ||
                _cName.Equals(_g.d.ic_inventory_detail._dimension_9) || _cName.Equals(_g.d.ic_inventory_detail._dimension_10) ||
                _cName.Equals(_g.d.ic_inventory_detail._dimension_11) || _cName.Equals(_g.d.ic_inventory_detail._dimension_12) ||
                _cName.Equals(_g.d.ic_inventory_detail._dimension_13) || _cName.Equals(_g.d.ic_inventory_detail._dimension_14) ||
                _cName.Equals(_g.d.ic_inventory_detail._dimension_15) || _cName.Equals(_g.d.ic_inventory_detail._dimension_16) ||
                _cName.Equals(_g.d.ic_inventory_detail._dimension_17) || _cName.Equals(_g.d.ic_inventory_detail._dimension_18) ||
                _cName.Equals(_g.d.ic_inventory_detail._dimension_19) || _cName.Equals(_g.d.ic_inventory_detail._dimension_20))
            {
                __list.Add(_g.g._search_master_ic_dimension);
            }
            else if (_cName.Equals(_g.d.ic_inventory_detail._user_group_for_purchase) || _cName.Equals(_g.d.ic_inventory_detail._user_group_for_sale)
                || _cName.Equals(_g.d.ic_inventory_detail._user_group_for_manage) || _cName.Equals(_g.d.ic_inventory_detail._user_group_for_warehouse))
            {
                __list.Add(_g.g._search_screen_erp_user_group);
            }
            else if (_cName.Equals(_g.d.ic_color_use._code) && _tName.Equals(_g.d.ic_color_use._table))
            {
                __list.Add(_g.g._search_master_ic_color);
                __list.Add(_g.d.ic_color._table);
            }
            else if (_cName.Equals(_g.d.ic_size_use._code) && _tName.Equals(_g.d.ic_size_use._table))
            {
                __list.Add(_g.g._search_master_ic_size);
                __list.Add(_g.d.ic_size._table);
            }
            else if (_cName.Equals(_g.d.ic_purchase_price._ap_code))
            {
                __list.Add(_g.g._search_master_ap_group);
                __list.Add(_g.d.ap_group._table);
            }
            else if (_cName.Equals(_g.d.ic_trans_detail._item_code))
            {
                __list.Add(_g.g._search_screen_ic_inventory);
                __list.Add(_g.d.ic_inventory._table);
            }
            else if (_cName.Equals(_g.d.ic_trans._currency_code))
            {
                __list.Add(_g.g._search_screen_erp_currency);
                __list.Add(_g.d.erp_currency._table);
            }
            return __list;
        }
    }
}
