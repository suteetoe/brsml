using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SMLERPICInfo
{
    public partial class _ic_inventory_information_2 : UserControl
    {
        string __formatNumberQty = _g.g._getFormatNumberStr(1);
        string __formatNumberPrice = _g.g._getFormatNumberStr(2);
        private string _itemCode;
        int _mode = 0;

        public _ic_inventory_information_2()
        {

            InitializeComponent();
            _build();
            _load();
        }

        void _build()
        {
            Font _pageFont = new Font("Tahoma", 15.95F);
            Font _pageFont2 = new Font("Tahoma", 15.95F);

            this._icGrid._fixFont = new System.Drawing.Font(_pageFont.Name, _pageFont.Size, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._icGrid.Font = new System.Drawing.Font(_pageFont.Name, _pageFont.Size, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._icGrid._calcHeightCalc();

            this._icGrid._isEdit = false;
            this._icGrid._table_name = _g.d.ic_inventory._table;
            this._icGrid._width_by_persent = true;
            this._icGrid._addColumn(_g.d.ic_inventory._code, 1, 30, 30);
            this._icGrid._addColumn(_g.d.ic_inventory._name_1, 1, 70, 70);
            this._icGrid._calcPersentWidthToScatter();
            this._icGrid._mouseClick += _icGrid__mouseClick;
            this._icGrid._enterKey += _icGrid__enterKey;

            this._gridEngineSearch._fixFont = new System.Drawing.Font(_pageFont.Name, _pageFont.Size, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridEngineSearch.Font = new System.Drawing.Font(_pageFont.Name, _pageFont.Size, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridEngineSearch._calcHeightCalc();

            this._icPriceFormulaGrid._fixFont = new System.Drawing.Font(_pageFont2.Name, _pageFont2.Size, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._icPriceFormulaGrid.Font = new System.Drawing.Font(_pageFont2.Name, _pageFont2.Size, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._icPriceFormulaGrid._calcHeightCalc();
            this._icPriceFormulaGrid.GetUnitType += _icPriceFormulaGrid_GetUnitType;
            this._icPriceFormulaGrid.GetUnitCode += _icPriceFormulaGrid_GetUnitCode;
            this._icPriceFormulaGrid.GetItemCode += _icPriceFormulaGrid_GetItemCode;
            this._icPriceFormulaGrid.GetItemDesc += _icPriceFormulaGrid_GetItemDesc;
            this._icPriceFormulaGrid._inputTextBox.textBox.Font = new System.Drawing.Font(_pageFont.Name, _pageFont.Size, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));


            this._icWarehouseBalanceGrid._getResource = true;
            this._icWarehouseBalanceGrid._table_name = _g.d.ic_resource._table;
            this._icWarehouseBalanceGrid._addColumn(_g.d.ic_resource._warehouse, 1, 20, 20);
            this._icWarehouseBalanceGrid._addColumn(_g.d.ic_resource._qty, 3, 20, 20, true, false, true, false, __formatNumberQty);
            this._icWarehouseBalanceGrid._addColumn(_g.d.ic_resource._balance_qty, 1, 20, 60);
            this._icWarehouseBalanceGrid._calcPersentWidthToScatter();
            //this._icWarehouseBalanceGrid._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_whBalanceGrid__beforeDisplayRow);
            this._icWarehouseBalanceGrid._isEdit = false;

            this._icWarehouseBalanceGrid._fixFont = new System.Drawing.Font(_pageFont2.Name, _pageFont2.Size, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._icWarehouseBalanceGrid.Font = new System.Drawing.Font(_pageFont2.Name, _pageFont2.Size, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._icWarehouseBalanceGrid._calcHeightCalc();

            this._screenTop._textBoxSearch += _screen_ic_inventory_information_21__textBoxSearch;
            this._screenTop._textBoxChanged += _screenTop__textBoxChanged;

            this._gridCostDate._fixFont = new System.Drawing.Font(_pageFont2.Name, _pageFont2.Size, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridCostDate.Font = new System.Drawing.Font(_pageFont2.Name, _pageFont2.Size, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridCostDate._calcHeightCalc();
            this._gridCostDate._table_name = _g.d.ic_cost_date._table;
            this._gridCostDate._addColumn(_g.d.ic_cost_date._doc_date, 4, 25, 25);
            this._gridCostDate._addColumn(_g.d.ic_cost_date._price, 3, 25, 25, true, false, true, false, __formatNumberPrice);

            this._gridCostDate._addColumn(_g.d.ic_cost_date._remark, 1, 50, 50);
            this._gridCostDate._queryForInsertCheck += _gridCostDate__queryForInsertCheck;
            this._gridCostDate._calcPersentWidthToScatter();

            //this._itemCodeLabel.Text = "";
            //this._itemNameLabel.Text = "";
            //this._itemUnitLabel.Text = "";

            // special search
            this._gridEngineSearch._table_name = _g.d.ic_specific_search_word._table;
            this._gridEngineSearch._isEdit = false;
            this._gridEngineSearch._addColumn(_g.d.ic_specific_search_word._keyword, 1, 20, 20);
            this._gridEngineSearch._addColumn(_g.d.ic_specific_search_word._description, 1, 80, 80, false, false, true, false, "", "", "", _g.d.ic_specific_search_word._market_names);
            this._gridEngineSearch._mouseClick += _gridEngineSearch__mouseClick;
            this._gridEngineSearch._calcPersentWidthToScatter();

            //
            this._gridSpecialSearch._table_name = _g.d.ic_specific_search._table;
            this._gridSpecialSearch._isEdit = false;
            this._gridSpecialSearch._addColumn(_g.d.ic_specific_search._ic_code, 1, 20, 20);
            this._gridSpecialSearch._addColumn(_g.d.ic_specific_search._ic_name, 1, 80, 80);
            this._gridSpecialSearch._addColumn(_g.d.ic_specific_search._group_type, 1, 0, 0, false, true);
            this._gridSpecialSearch._calcPersentWidthToScatter();
            this._gridSpecialSearch._mouseClick += _gridSpecialSearch__mouseClick;


            this._myTabControl1.SelectedIndexChanged += _myTabControl1_SelectedIndexChanged;
            this.tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;

            this._partTreeView.NodeMouseClick += _partTreeView_NodeMouseClick;
        }

        private void _partTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                this._load(e.Node.Tag.ToString());
            }
        }

        private void _gridSpecialSearch__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._row != -1)
            {
                string __iccode = _gridSpecialSearch._cellGet(e._row, _g.d.ic_specific_search._ic_code).ToString();
                if (__iccode.Length > 0)
                {
                    this._load(__iccode);

                }
            }
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                _searchFromEngineCode();
                //this.toolStrip1.Enabled = false;
            }
            else
            {
                this.toolStrip1.Enabled = true;

            }
        }

        private void _gridEngineSearch__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            // get engine
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            String __keyword = this._gridEngineSearch._cellGet(e._row, _g.d.ic_specific_search._keyword).ToString();


            string __getPartQuery = "select " + _g.d.ic_specific_search._ic_code + "," + _g.d.ic_specific_search._sort_order + ", (select name_1 from ic_inventory where ic_inventory.code = ic_specific_search.ic_code) as " + _g.d.ic_specific_search._ic_name + ", (select name_1 from ic_group where ic_group.code = (select " + _g.d.ic_inventory._group_main + " from ic_inventory where ic_inventory.code = ic_specific_search.ic_code)) as " + _g.d.ic_specific_search._group_code + "  from " + _g.d.ic_specific_search._table + " where " + _g.d.ic_specific_search._keyword + " = \'" + __keyword + "\' order by line_number";

            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

            // engine part

            StringBuilder __queryGetPart = new StringBuilder("select group_code, coalesce((select name_1 from ic_group where ic_group.code = group_code), 'อื่น ๆ') as group_name, ic_code as item_code, line_number");

            __queryGetPart.Append(",(select name_1 from ic_inventory where ic_inventory.code = ic_code) as item_name ");
            __queryGetPart.Append(", array_to_string(array(select '#' || keyword from ic_specific_search where  ic_specific_search.ic_code = temp2.ic_code and keyword != \'" + __keyword + "\' ), ',') as relate_engine ");
            __queryGetPart.Append(", (select count(image_id) from images where image_id =  temp2.ic_code) as img_count ");
            __queryGetPart.Append(" from (");
            __queryGetPart.Append("select ic_code, (select case when coalesce(group_main, '') = '' then 'NOGROUP' else group_main end  from ic_inventory where ic_inventory.code = ic_code) as group_code, coalesce(line_number, 0) as line_number, sort_order ");
            __queryGetPart.Append("from ic_specific_search ");
            __queryGetPart.Append("where keyword = \'" + __keyword + "\' ");
            __queryGetPart.Append(") as temp2 ");
            __queryGetPart.Append("order by (select coalesce(sort_order, 0) from ic_group where ic_group.code = group_code),group_code, sort_order desc, line_number");

            // suggest

            StringBuilder __queryGetSuggest = new StringBuilder("select suggest_code, item_code, group_code, coalesce((select name_1 from ic_group where ic_group.code = group_code), 'อื่น ๆ') as group_name, (select name_1 from ic_inventory where ic_inventory.code = item_code) as item_name " +
                ", array_to_string(array(select '#' || keyword from ic_specific_search where ic_specific_search.ic_code = temp2.item_code and keyword != \'" + __keyword + "\'), ',') as relate_engine " +
                ",  (select count(image_id) from images where image_id =  temp2.item_code) as img_count " +
                " from (");
            __queryGetSuggest.Append("select ic_code as suggest_code, ic_suggest_code as item_code, (select case when coalesce(group_main, '') = '' then 'NOGROUP' else group_main end  from ic_inventory where ic_inventory.code = ic_inventory_suggest.ic_code) as group_code from ic_inventory_suggest where ic_code in (select ic_code from ic_specific_search where keyword = \'" + __keyword + "\' ) ");
            __queryGetSuggest.Append(" union all ");
            __queryGetSuggest.Append("select ic_suggest_code as suggest_code, ic_code as item_code, (select case when coalesce(group_main, '') = '' then 'NOGROUP' else group_main end  from ic_inventory where ic_inventory.code = ic_inventory_suggest.ic_suggest_code) as group_code from ic_inventory_suggest where ic_suggest_code in (select ic_code from ic_specific_search where keyword = \'" + __keyword + "\' )");
            __queryGetSuggest.Append(") as temp2");

            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryGetPart.ToString()));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryGetSuggest.ToString()));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_specific_search_word._table + " where keyword = \'" + __keyword + "\' "));

            StringBuilder __queryGetProductReplacement = new StringBuilder();
            __queryGetProductReplacement.Append(" select ic_code, (select name_1 from ic_inventory where ic_inventory.code = ic_inventory_replacement.ic_code) as ic_name, ic_replace_code from ic_inventory_replacement where ic_replace_code in ( ");
            __queryGetProductReplacement.Append("select ic_code as item_code from ic_specific_search  where keyword = '" + __keyword + "'  ");
            __queryGetProductReplacement.Append(" union all ");
            __queryGetProductReplacement.Append(" select item_code from (select  ic_suggest_code as item_code from ic_inventory_suggest where ic_code in (select ic_code from ic_specific_search where keyword = '" + __keyword + "' ) union all select  ic_code as item_code from ic_inventory_suggest where ic_suggest_code in (select ic_code from ic_specific_search where keyword = '" + __keyword + "' )) as temp33 ");
            __queryGetProductReplacement.Append(") ");

            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryGetProductReplacement.ToString()));

            __query.Append("</node>");

            //DataTable __result = __myFrameWork._queryShort(__getPartQuery).Tables[0];
            //this._gridSpecialSearch._loadFromDataTable(__result);

            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
            if (__result.Count > 0)
            {
                this._gridSpecialSearch._clear();
                this._partTreeView.Nodes.Clear();

                DataTable __partTable = ((DataSet)__result[0]).Tables[0];
                DataTable __partSuggest = ((DataSet)__result[1]).Tables[0];
                DataTable __keywordDetail = ((DataSet)__result[2]).Tables[0];

                DataTable __productReplacement = ((DataSet)__result[3]).Tables[0];

                DataTable __mainGroupDistinct = MyLib._dataTableExtension._selectDistinct(__partTable, "group_code,group_name");

                for (int __row = 0; __row < __mainGroupDistinct.Rows.Count; __row++)
                {
                    /*
                    string __groupCode = __mainGroupDistinct.Rows[__row]["group_code"].ToString();
                    string __groupName = __mainGroupDistinct.Rows[__row]["group_name"].ToString();

                    int __addr = this._gridSpecialSearch._addRow();
                    this._gridSpecialSearch._cellUpdate(__addr, _g.d.ic_specific_search._ic_code, __groupCode, true);
                    this._gridSpecialSearch._cellUpdate(__addr, _g.d.ic_specific_search._ic_name, __groupName, true);
                    this._gridSpecialSearch._cellUpdate(__addr, _g.d.ic_specific_search._group_type, "0", true);

                    DataRow[] __partRow = __partTable.Select("group_code=\'" + __groupCode + "\'");

                    for (int __row2 = 0; __row2 < __partRow.Length; __row2++)
                    {
                        int __addr2 = this._gridSpecialSearch._addRow();
                        string __partcode = __partRow[__row2]["item_code"].ToString();
                        string __partName = __partRow[__row2]["item_name"].ToString();

                        this._gridSpecialSearch._cellUpdate(__addr2, _g.d.ic_specific_search._ic_code, __partcode, true);
                        this._gridSpecialSearch._cellUpdate(__addr2, _g.d.ic_specific_search._ic_name, __partName, true);
                        this._gridSpecialSearch._cellUpdate(__addr2, _g.d.ic_specific_search._group_type, "1", true);

                        // get  Rerate item
                    }
                    */

                    string __groupCode = __mainGroupDistinct.Rows[__row]["group_code"].ToString();
                    string __groupName = __mainGroupDistinct.Rows[__row]["group_name"].ToString();

                    TreeNode __groupNode = new TreeNode();
                    __groupNode.Text = __groupName;
                    //__groupNode.Tag = "0";

                    DataRow[] __partRow = __partTable.Select("group_code=\'" + __groupCode + "\'");

                    for (int __row2 = 0; __row2 < __partRow.Length; __row2++)
                    {
                        int __addr2 = this._gridSpecialSearch._addRow();
                        string __partcode = __partRow[__row2]["item_code"].ToString();
                        string __partName = __partRow[__row2]["item_name"].ToString();
                        string __relateEngine = __partRow[__row2]["relate_engine"].ToString();
                        int __imgCount = MyLib._myGlobal._intPhase(__partRow[__row2]["img_count"].ToString());

                        TreeNode __partNode = new TreeNode();
                        __partNode.Text = __partName + ((__relateEngine.Length > 0) ? " " + __relateEngine : ""); // __partcode + " : " +
                        __partNode.Tag = __partcode;
                        if (__imgCount > 0)
                        {
                            __partNode.ImageIndex = 1;
                        }

                        // get  Rerate item
                        if (__partSuggest.Rows.Count > 0)
                        {
                            DataRow[] __getGroupRelate = __partSuggest.Select("suggest_code = \'" + __partcode + "\' "); // suggest_code = \'" + __partcode + "\' or 

                            DataTable __mergeTable = __partSuggest.Clone();
                            foreach (DataRow dr in __getGroupRelate)
                            {
                                __mergeTable.ImportRow(dr);
                            }
                            DataTable __suggestGroup = MyLib._dataTableExtension._selectDistinct(__mergeTable, "group_code,group_name");

                            if (__suggestGroup.Rows.Count > 0)
                            {
                                for (int __row3 = 0; __row3 < __suggestGroup.Rows.Count; __row3++)
                                {
                                    string __groupSuggestCode = __suggestGroup.Rows[__row3]["group_code"].ToString();
                                    string __groupSuggestName = __suggestGroup.Rows[__row3]["group_name"].ToString();

                                    TreeNode __groupSuggestNode = new TreeNode();
                                    __groupSuggestNode.Text = __groupSuggestName;
                                    //__groupSuggestNode.Tag = "2";

                                    // part in suggest

                                    DataRow[] __partSuggestRow = __mergeTable.Select("group_code=\'" + __groupSuggestCode + "\'");

                                    for (int __row4 = 0; __row4 < __partSuggestRow.Length; __row4++)
                                    {
                                        string __partSuggestCode = __partSuggestRow[__row4]["item_code"].ToString();
                                        // string __partSuggestCode = __partSuggestRow[__row4]["item_code"].ToString();
                                        string __partSuggestName = __partSuggestRow[__row4]["item_name"].ToString();
                                        string __partSuggestRelateEnging = __partSuggestRow[__row4]["relate_engine"].ToString();
                                        int __imgSuggestCount = MyLib._myGlobal._intPhase(__partSuggestRow[__row4]["img_count"].ToString());

                                        TreeNode __partSuggestNode = new TreeNode();
                                        __partSuggestNode.Text = __partSuggestName + ((__partSuggestRelateEnging.Length > 0) ? " " + __partSuggestRelateEnging : ""); //  __partSuggestCode + " : " + 
                                        __partSuggestNode.Tag = __partSuggestCode;
                                        if (__imgSuggestCount > 0)
                                        {
                                            __partSuggestNode.ImageIndex = 1;
                                        }

                                        if (__productReplacement.Rows.Count > 0)
                                        {
                                            DataRow[] __replacerow = __productReplacement.Select(" ic_replace_code=\'" + __partSuggestCode + "\' ");
                                            if (__replacerow.Length > 0)
                                            {
                                                for (int __rowReplace = 0; __rowReplace < __replacerow.Length; __rowReplace++)
                                                {
                                                    TreeNode __replaceNode = new TreeNode();
                                                    __replaceNode.Text = "***"+  __replacerow[__rowReplace]["ic_name"].ToString();
                                                    __replaceNode.Tag = __replacerow[__rowReplace]["ic_code"].ToString();
                                                    __partSuggestNode.Nodes.Add(__replaceNode);
                                                }
                                            }
                                        }

                                        __groupSuggestNode.Nodes.Add(__partSuggestNode);
                                    }

                                    __partNode.Nodes.Add(__groupSuggestNode);

                                    // add replacement
                                    if (__productReplacement.Rows.Count > 0)
                                    {
                                        DataRow[] __replacerow = __productReplacement.Select(" ic_replace_code=\'" + __partcode + "\' ");
                                        if (__replacerow.Length > 0)
                                        {
                                            TreeNode __replaceNodeGroup = new TreeNode();
                                            __replaceNodeGroup.Text = "สินค้าทดแทน";

                                            for (int __rowReplace = 0; __rowReplace < __replacerow.Length; __rowReplace++)
                                            {
                                                TreeNode __replaceNode = new TreeNode();
                                                __replaceNode.Text = "***" + __replacerow[__rowReplace]["ic_name"].ToString();
                                                __replaceNode.Tag = __replacerow[__rowReplace]["ic_code"].ToString();
                                                __replaceNodeGroup.Nodes.Add(__replaceNode);
                                            }
                                            __partNode.Nodes.Add(__replaceNodeGroup);
                                        }
                                    }

                                }
                            }
                            else
                            {
                                if (__productReplacement.Rows.Count > 0)
                                {
                                    DataRow[] __replacerow = __productReplacement.Select(" ic_replace_code=\'" + __partcode + "\' ");
                                    for (int __rowReplace = 0; __rowReplace < __replacerow.Length; __rowReplace++)
                                    {
                                        TreeNode __replaceNode = new TreeNode();
                                        __replaceNode.Text = "***" + __replacerow[__rowReplace]["ic_name"].ToString();
                                        __replaceNode.Tag = __replacerow[__rowReplace]["ic_code"].ToString();
                                        __partNode.Nodes.Add(__replaceNode);
                                    }
                                }
                            }
                        }
                        else
                        {
                            // replacement node
                            if (__productReplacement.Rows.Count > 0)
                            {

                                DataRow[] __replacerow = __productReplacement.Select(" ic_replace_code=\'" + __partcode + "\' ");
                                for (int __rowReplace = 0; __rowReplace < __replacerow.Length; __rowReplace++)
                                {
                                    TreeNode __replaceNode = new TreeNode();
                                    __replaceNode.Text = "***" + __replacerow[__rowReplace]["ic_name"].ToString();
                                    __replaceNode.Tag = __replacerow[__rowReplace]["ic_code"].ToString();
                                    __partNode.Nodes.Add(__replaceNode);
                                }
                            }
                        }

                        // add to tree
                        __groupNode.Nodes.Add(__partNode);
                    }

                    //__groupNode.Expand();
                    this._partTreeView.Nodes.Add(__groupNode);

                }

                // expand
                foreach (TreeNode getNode in this._partTreeView.Nodes)
                {
                    // _expandAll(getNode);
                    _changeColorTree(getNode);
                }

                // keyword detail
                StringBuilder __engineDetail = new StringBuilder();

                string __numbervalve = __keywordDetail.Rows[0][_g.d.ic_specific_search_word._engine_valve].ToString();
                string __displacement = __keywordDetail.Rows[0][_g.d.ic_specific_search_word._displacement].ToString();
                string __marketNames = __keywordDetail.Rows[0][_g.d.ic_specific_search_word._market_names].ToString();
                string __note = __keywordDetail.Rows[0][_g.d.ic_specific_search_word._remark].ToString();

                __engineDetail.Append("จำนวนของวาล์ว : " + __numbervalve + "\r\n");
                __engineDetail.Append("ปริมาณ : " + __displacement + "\r\n");
                __engineDetail.Append("ชื่อตลาด : " + __marketNames + "\r\n");
                __engineDetail.Append("โน๊ต : " + __note);

                this._textEngineDetail.Text = __engineDetail.ToString();
            }
        }

        void _collapseAll(TreeNode node)
        {
            node.Collapse(true);
        }

        void _expandAll(TreeNode node)
        {
            node.Expand();
            foreach (TreeNode getNode in node.Nodes)
            {
                _expandAll(getNode);
            }
        }

        void _changeColorTree(TreeNode getNode)
        {
            getNode.ForeColor = Color.Black;
            //if (getNode.Tag != null)
            {
                switch (getNode.Level.ToString())
                {
                    case "0": getNode.ForeColor = Color.Blue; break;
                    case "1": getNode.ForeColor = Color.Brown; break;
                    case "2": getNode.ForeColor = Color.BlueViolet; break;
                    case "3": getNode.ForeColor = Color.Chocolate; break;
                    case "4": getNode.ForeColor = Color.DarkBlue; break;
                    default: getNode.ForeColor = Color.DarkGreen; break;
                }
            }
            for (int loop = 0; loop < getNode.Nodes.Count; loop++)
            {
                _changeColorTree(getNode.Nodes[loop]);
            }
        }


        void _searchFromEngineCode()
        {
            string __searchText = this._searchEngineTextbox.textBox.Text.Trim();

            StringBuilder __query = new StringBuilder();
            __query.Append("select keyword, (replace(market_names, E'\n', ',')) as description from ic_specific_search_word ");

            // do search full text
            if (__searchText.Length > 0)
            {
                string[] __searchTextSplit = __searchText.Split(' ');
                StringBuilder __where = new StringBuilder();
                if (__searchTextSplit.Length > 1)
                {
                    bool __first2 = false;
                    for (int __searchIndex = 0; __searchIndex < __searchTextSplit.Length; __searchIndex++)
                    {
                        if (__searchTextSplit[__searchIndex].Length > 0)
                        {
                            if (__first2)
                            {
                                __where.Append(" and ");
                            }

                            __first2 = true;
                            string __getValue = __searchTextSplit[__searchIndex].ToUpper();
                            // string __newDateValue = __getValue;
                            __where.Append(" ( upper(" + _g.d.ic_specific_search_word._keyword + ") like \'%" + __getValue.ToUpper() + "%\' or upper(" + _g.d.ic_specific_search_word._market_names + ") like \'%" + __getValue.ToUpper() + "%\' ) ");


                        }
                    }
                }
                else
                {
                    __where.Append(" ( upper(" + _g.d.ic_specific_search_word._keyword + ") like \'%" + __searchText.ToUpper() + "%\' or upper(" + _g.d.ic_specific_search_word._market_names + ") like \'%" + __searchText.ToUpper() + "%\' ) ");
                }

                if (__where.Length > 0)
                {
                    __query.Append(" where (" + __where.ToString() + ")");
                }

            }
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __result = __myFrameWork._queryShort(__query.ToString()).Tables[0];

            this._gridEngineSearch._loadFromDataTable(__result);
            this._gridEngineSearch.Invalidate();
        }

        void _myTabControl1_SelectedIndexChanged(object sender, EventArgs e)

        {
            if (this._myTabControl1.SelectedTab == this.tab_picture)
            {
                this._getPicture._clearpic();
                string _codepic = this._screenTop._getDataStr(_g.d.ic_inventory._code);
                string _codepic_ = _codepic.Trim(); // .Replace("/", "")

                this._getPicture._loadImage(_codepic_);
                this._getPicture._setEnable(true);
            }
        }


        bool _gridCostDate__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            DateTime getValue = (DateTime)sender._cellGet(row, _g.d.ic_cost_date._doc_date);
            if (getValue.Year > 1900)
                return true;
            return false;
        }

        void _icGrid__enterKey(MyLib._myGrid sender, int row)

        {
            string __getItemCode = this._icGrid._cellGet(row, _g.d.ic_inventory._code).ToString();
            this._load(__getItemCode);
        }

        void _screenTop__textBoxChanged(object sender, string name)
        {
            MyLib._myTextBox __textBox = (MyLib._myTextBox)((Control)sender).Parent;
            if (name.Equals(_g.d.ic_inventory._code))
            {
                string __newCode = MyLib._myGlobal._codeRemove(__textBox._textFirst);
                if (__newCode.Equals(__textBox._textFirst) == false)
                {
                    __textBox._textFirst = __newCode;
                    __textBox.textBox.Invalidate();
                }
                // autorun
                string __icCode = __textBox._textFirst;
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + " from " + _g.d.erp_doc_format._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_doc_format._code) + "=\'" + __icCode.ToUpper() + "\'").Tables[0];
                if (__getFormat.Rows.Count > 0)
                {
                    string __format = __getFormat.Rows[0][0].ToString();
                    string __docFormatCode = __getFormat.Rows[0][1].ToString();
                    string __newICCode = _g.g._getAutoRun(_g.g._autoRunType.สินค้า, __icCode, MyLib._myGlobal._convertDateToString(MyLib._myGlobal._workingDate, true), __format, _g.g._transControlTypeEnum.ว่าง, _g.g._transControlTypeEnum.ว่าง);
                    this._screenTop._setDataStr(_g.d.ic_inventory._code, __newICCode, "", true);
                }
                else
                {
                    if (__icCode.Length > 0)
                    {
                        try
                        {
                            string __newICCode = __icCode;
                            DataTable __dt = __myFrameWork._queryShort("select " + _g.d.ic_inventory._code + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "<\'" + __icCode + "z\' order by " + _g.d.ic_inventory._code + " desc limit 1").Tables[0];
                            if (__dt.Rows.Count > 0)
                            {
                                string __getItemCode = __dt.Rows[0][_g.d.ic_inventory._code].ToString();
                                if (__getItemCode.Length > __icCode.Length)
                                {
                                    string __s1 = __getItemCode.Substring(0, __icCode.Length);
                                    if (__s1.Equals(__icCode))
                                    {
                                        string __s2 = __getItemCode.Remove(0, __icCode.Length);
                                        int __runningNumber = (int)MyLib._myGlobal._decimalPhase(__s2);
                                        if (__runningNumber > 0)
                                        {
                                            StringBuilder __format = new StringBuilder();
                                            for (int __loop = 0; __loop < __s2.Length; __loop++)
                                            {
                                                __format.Append("0");
                                            }
                                            __newICCode = __s1 + String.Format("{0:" + __format.ToString() + "}", ((decimal)(__runningNumber + 1)));
                                            this._screenTop._setDataStr(_g.d.ic_inventory._code, __newICCode, "", true);
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }

        }

        string _search_screen_name(string _name)
        {
            if (_name.Equals(_g.d.ic_inventory._code)) return _g.g._search_screen_erp_doc_format;
            if (_name.Equals(_g.d.ic_inventory._unit_cost)) return _g.g._search_master_ic_unit;
            if (_name.Equals(_g.d.ic_inventory._unit_standard)) return _g.g._search_master_ic_unit;
            if (_name.Equals(_g.d.ic_inventory._income_type)) return _g.g._search_screen_income_list;
            if (_name.Equals(_g.d.ic_inventory._item_pattern)) return _g.g._search_master_ic_pattern;
            if (_name.Equals(_g.d.ic_inventory._supplier_code)) return _g.g._search_screen_ap;
            return "";
        }

        string _search_screen_name_extra_where(string _name)
        {
            if (_name.Equals(_g.d.ic_inventory._code)) return MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code) + "=\'" + this._screenCode + "\'";
            return "";
        }


        private string _searchName = "";
        private MyLib._searchDataFull _search_data_full_pointer;
        private string _old_filed_name = "";
        private ArrayList _search_data_full_buffer = new ArrayList();
        private ArrayList _search_data_full_buffer_name = new ArrayList();
        private int _search_data_full_buffer_addr = -1;
        private string _screenCode = "IC";

        void _screen_ic_inventory_information_21__textBoxSearch(object sender)
        {
            this._screenTop._saveLastControl();

            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchName = __getControl._name.ToLower();
            string label_name = __getControl._labelName;
            string _search_text_new = _search_screen_name(this._searchName);
            string __where_query = _search_screen_name_extra_where(this._searchName);

            if (!this._old_filed_name.Equals(__getControl._name.ToLower()))
            {
                Boolean __found = false;
                this._old_filed_name = __getControl._name.ToLower();
                // jead
                int __addr = 0;
                for (int __loop = 0; __loop < this._search_data_full_buffer_name.Count; __loop++)
                {
                    if (__getControl._name.Equals(((string)this._search_data_full_buffer_name[__loop]).ToString()))
                    {
                        __addr = __loop;
                        __found = true;
                        break;
                    }
                }
                if (__found == false)
                {
                    __addr = this._search_data_full_buffer_name.Add(__getControl._name);
                    MyLib._searchDataFull __searchObject = new MyLib._searchDataFull();
                    __searchObject.Font = new System.Drawing.Font("Tahoma", 15.95F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                    __searchObject._dataList._gridData._fixFont = new System.Drawing.Font("Tahoma", 15.95F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                    __searchObject._dataList._gridData.Font = new System.Drawing.Font("Tahoma", 15.95F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                    __searchObject._dataList._gridData._calcHeightCalc();
                    //__searchObject._dataList.Font = new System.Drawing.Font("Tahoma", 15.95F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                    this._search_data_full_buffer.Add(__searchObject);
                }
                if (this._search_data_full_pointer != null && this._search_data_full_pointer.Visible == true)
                {
                    this._search_data_full_pointer.Visible = false;
                }
                this._search_data_full_pointer = (MyLib._searchDataFull)this._search_data_full_buffer[__addr];
                this._search_data_full_buffer_addr = __addr;
                //
            }

            if (!this._search_data_full_pointer._name.Equals(_search_text_new.ToLower()))
            {
                if (this._search_data_full_pointer._name.Length == 0)
                {
                    this._search_data_full_pointer._showMode = 0;
                    this._search_data_full_pointer._name = _search_text_new;
                    this._search_data_full_pointer._dataList._loadViewFormat(this._search_data_full_pointer._name, MyLib._myGlobal._userSearchScreenGroup, false);

                    this._search_data_full_pointer._dataList._gridData._mouseClick -= _gridData__mouseClick;
                    this._search_data_full_pointer._dataList._gridData._mouseClick += _gridData__mouseClick;
                    this._search_data_full_pointer._searchEnterKeyPress -= _search_data_full_pointer__searchEnterKeyPress;
                    this._search_data_full_pointer._searchEnterKeyPress += _search_data_full_pointer__searchEnterKeyPress;
                }
            }
            if (this._search_data_full_pointer._show == false)
            {
                MyLib._myGlobal._startSearchBox(this, __getControl, label_name, this._search_data_full_pointer, false, true, __where_query);
            }
            else
            {
                MyLib._myGlobal._startSearchBox(this, __getControl, label_name, this._search_data_full_pointer, false, false, __where_query);
            }
            if (__getControl._iconNumber == 1)
            {
                __getControl.Focus();
                __getControl.textBox.Focus();
            }
            else
            {
                this._search_data_full_pointer.Focus();
                this._search_data_full_pointer._firstFocus();
            }
        }

        void _search_data_full_pointer__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);

        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);

        }

        void _searchAll(string name, int row)
        {
            string _search_text_new = name;
            string __result = "";
            if (name.Length > 0)
            {
                __result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, 0);
                if (__result.Length != 0)
                {
                    this._search_data_full_pointer.Visible = false;
                    this._screenTop._setDataStr(_searchName, __result);
                }
            }
            SendKeys.Send("{TAB}");
        }


        string _icPriceFormulaGrid_GetItemDesc(object sender)
        {
            return this._screenTop._getDataStr(_g.d.ic_inventory._name_1);
        }

        string _icPriceFormulaGrid_GetItemCode(object sender)
        {
            return this._screenTop._getDataStr(_g.d.ic_inventory._code);
        }

        string _icPriceFormulaGrid_GetUnitCode(object sender)
        {
            return this._screenTop._getDataStr(_g.d.ic_inventory._unit_cost);
        }

        int _icPriceFormulaGrid_GetUnitType(object sender)
        {
            if (this._mode == 1)
                return 0;

            // search from database
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __result = __myFrameWork._queryShort("select " + _g.d.ic_inventory._unit_type + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + " =\'" + this._itemCode + "\'").Tables[0];

            int __type = MyLib._myGlobal._intPhase(__result.Rows[0][0].ToString());

            return __type;
        }

        void _icGrid__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            string __getItemCode = this._icGrid._cellGet(e._row, _g.d.ic_inventory._code).ToString();
            this._load(__getItemCode);

        }

        public void _load(string itemCode)
        {
            this._itemCode = itemCode;
            this._mode = 0;

            //
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myquery = new StringBuilder();
            // ตามคลัง
            SMLProcess._icProcess __process = new SMLProcess._icProcess();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            // ดึงสินค้า
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + this._itemCode + "\'"));
            // ตามคลัง
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__process._queryItemBalance(this._itemCode, _g.d.ic_trans_detail._wh_code + " as " + _g.d.ic_resource._warehouse, _g.d.ic_resource._qty, _g.d.ic_trans_detail._wh_code, "")));
            // หน่วยนับ
            //string __calcPack = "/(select stand_value/divide_value from ic_unit_use where ic_unit_use.ic_code=ic_inventory.code and ic_unit_use.code=ic_inventory.unit_standard)";
            string __queryUnit = "(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + "{0})";
            string __queryPacking = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_unit_use._code, String.Format(__queryUnit, _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code) + " as " + _g.d.ic_unit_use._name_1, "coalesce(" + _g.d.ic_unit_use._row_order + ",1) as " + _g.d.ic_unit_use._row_order, _g.d.ic_unit_use._stand_value, _g.d.ic_unit_use._divide_value) + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._status + "=1 and " + _g.d.ic_unit_use._ic_code + "=\'" + this._itemCode + "\' order by " + _g.d.ic_unit_use._ratio;
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryPacking));
            // ราคาตามสูตร
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory_price_formula._table + " where " + _g.d.ic_inventory_price_formula._ic_code + "=\'" + itemCode + "\' order by " + _g.d.ic_inventory_price_formula._unit_code));
            // ic_cost_date
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_cost_date._table + " where " + _g.d.ic_cost_date._ic_code + "=\'" + itemCode + "\' order by " + _g.d.ic_cost_date._line_number));

            __myquery.Append("</node>");
            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());

            DataTable __dataIC = ((DataSet)__getData[0]).Tables[0];
            string __icCode = __dataIC.Rows[0][_g.d.ic_inventory._code].ToString();
            string __itemName = __dataIC.Rows[0][_g.d.ic_inventory._name_1].ToString();
            string __standardUnit = __dataIC.Rows[0][_g.d.ic_inventory._unit_standard].ToString();

            //this._itemCodeLabel.Text = __icCode;
            //this._itemNameLabel.Text = __itemName;
            //this._itemUnitLabel.Text = __standardUnit;

            //this._icmainScreenTop._loadData(((DataSet)__getData[0]).Tables[0]);
            //this._icmainScreenTop._search(true);
            this._screenTop._loadData(((DataSet)__getData[0]).Tables[0]);

            //
            this._icWarehouseBalanceGrid._loadFromDataTable(((DataSet)__getData[1]).Tables[0]);
            this._icWarehouseBalanceGrid.Invalidate();
            //
            //
            this._icPriceFormulaGrid._loadFromDataTable(((DataSet)__getData[3]).Tables[0]);
            //this._icPriceFormulaGrid._calc();
            this._gridCostDate._loadFromDataTable(((DataSet)__getData[4]).Tables[0]);
            //
            this._screenTop._enabedControl(_g.d.ic_inventory._code, false);

            this._getPicture._clearpic();
            string _codepic = this._screenTop._getDataStr(_g.d.ic_inventory._code);
            string _codepic_ = _codepic.Replace("/", "").Trim();

            if (this._myTabControl1.SelectedTab == this.tab_picture)
            {
                this._getPicture._loadImage(_codepic_);
            }
            this._getPicture._setEnable(true);


        }


        void _load()
        {
            // load 
            string __searchTextTrim = this._searchTextbox.textBox.Text.Trim();
            string[] __searchTextSplit = __searchTextTrim.Split(' ');

            // ประกอบ where
            StringBuilder __where = new StringBuilder();
            if (__searchTextSplit.Length > 1)
            {
                // กรณี ป้อนการค้นหาหลายคำ (เว้น spacebar)
                for (int __loop = 0; __loop < this._icGrid._columnList.Count; __loop++)
                {
                    bool __whereFirst = false;
                    MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType)this._icGrid._columnList[__loop];
                    if (__getColumnType._originalName.Length > 0 && __getColumnType._isQuery == true && __getColumnType._isColumnFilter == true)
                    {
                        if (__getColumnType._isHide == false)
                        {
                            // กรณี ป้อนการค้นหาหลายคำ (เว้น spacebar)
                            bool __first2 = false;
                            for (int __searchIndex = 0; __searchIndex < __searchTextSplit.Length; __searchIndex++)
                            {
                                if (__searchTextSplit[__searchIndex].Length > 0)
                                {
                                    string __getValue = __searchTextSplit[__searchIndex].ToUpper();
                                    string __newDateValue = __getValue;
                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                    {
                                        __newDateValue = __getValue.ToString().Remove(0, 1);
                                    }
                                    switch (__getColumnType._type)
                                    {
                                        case 2: // Number
                                        case 3:
                                        case 5:
                                            //
                                            decimal __newValue = 0M;
                                            try
                                            {
                                                if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                {
                                                    __newValue = MyLib._myGlobal._decimalPhase(__newDateValue);
                                                    //
                                                    if (__newValue != 0)
                                                    {
                                                        if (__whereFirst == false)
                                                        {
                                                            if (__where.Length > 0)
                                                            {
                                                                __where.Append(" or ");
                                                            }
                                                            __where.Append("(");
                                                            __whereFirst = true;
                                                        }
                                                        if (__first2)
                                                        {
                                                            __where.Append(" and ");
                                                        }
                                                        __first2 = true;
                                                        //
                                                        //if (this._addQuotWhere)
                                                        //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                        //else
                                                        __where.Append(string.Concat(__getColumnType._query));
                                                        //
                                                        if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                            __getValue = __getValue[0].ToString() + __newValue.ToString();
                                                        else
                                                            __getValue = String.Concat("=", __newValue.ToString());
                                                        __where.Append(__getValue);
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                            }
                                            break;
                                        case 4: // Date
                                            try
                                            {
                                                if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                {
                                                    DateTime __test = DateTime.Parse(__newDateValue, MyLib._myGlobal._cultureInfo());
                                                    //
                                                    if (__whereFirst == false)
                                                    {
                                                        if (__where.Length > 0)
                                                        {
                                                            __where.Append(" or ");
                                                        }
                                                        __where.Append("(");
                                                        __whereFirst = true;
                                                    }
                                                    if (__first2)
                                                    {
                                                        __where.Append(" and ");
                                                    }
                                                    __first2 = true;
                                                    //
                                                    //if (this._addQuotWhere)
                                                    //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                    //else
                                                    __where.Append(string.Concat(__getColumnType._query));
                                                    DateTime __newDate = MyLib._myGlobal._convertDate(__getValue);
                                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                    {
                                                        __newDate = MyLib._myGlobal._convertDate(__newDateValue);
                                                        __where.Append(string.Concat(__getValue[0].ToString(), "\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                    }
                                                    else
                                                    {
                                                        __where.Append(string.Concat("=\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                            }
                                            break;
                                        case 11:
                                            {

                                            }
                                            break;
                                        default:// String
                                            if (__whereFirst == false)
                                            {
                                                if (__where.Length > 0)
                                                {
                                                    __where.Append(" or ");
                                                }
                                                __where.Append("(");
                                                __whereFirst = true;
                                            }
                                            if (__first2)
                                            {
                                                __where.Append(" and ");
                                            }
                                            __first2 = true;
                                            //
                                            //if (this._addQuotWhere)
                                            //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                            //else
                                            __where.Append(string.Concat("upper(" + __getColumnType._query + ")"));
                                            if (this._searchTextbox.textBox.Text[0] == '+')
                                            {
                                                __where.Append(string.Concat(" like \'", __newDateValue.Remove(0, 1).ToUpper(), "%\'"));
                                            }
                                            else
                                            {
                                                __where.Append(string.Concat(" like \'%", __newDateValue.ToUpper(), "%\'"));
                                            }
                                            break;
                                    }
                                }
                            }
                            if (__whereFirst)
                            {
                                __where.Append(")");
                            }
                        }
                    }
                } // for
            }
            else
            {
                bool __whereFirst = false;
                for (int __loop = 0; __loop < this._icGrid._columnList.Count; __loop++)
                {
                    MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType)this._icGrid._columnList[__loop];
                    if (__getColumnType._originalName.Length > 0 && __getColumnType._isQuery == true && __getColumnType._isColumnFilter == true)
                    {
                        if (__getColumnType._isHide == false)
                        {
                            // กรณีการค้นหาตัวเดียว
                            if (this._searchTextbox.textBox.Text.Length > 0)
                            {
                                try
                                {
                                    string __getValue = this._searchTextbox.textBox.Text;
                                    string __newDateValue = __getValue;
                                    Boolean __valueExtra = false;
                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                    {
                                        __newDateValue = __getValue.ToString().Remove(0, 1);
                                        __valueExtra = true;
                                    }
                                    switch (__getColumnType._type)
                                    {
                                        case 2: // Number
                                        case 3:
                                        case 5:
                                            double __newValue = 0;
                                            try
                                            {
                                                if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                {
                                                    __newValue = Double.Parse(__newDateValue);
                                                    //
                                                    if (__newValue != 0)
                                                    {
                                                        if (__whereFirst == true)
                                                        {
                                                            __where.Append(" or ");
                                                        }
                                                        __whereFirst = true;
                                                        //
                                                        //if (this._addQuotWhere)
                                                        //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                        //else
                                                        __where.Append(string.Concat(__getColumnType._query));
                                                        //
                                                        if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                            __getValue = __getValue[0].ToString() + __newValue.ToString();
                                                        else
                                                            __getValue = String.Concat("=", __newValue.ToString());
                                                        __where.Append(__getValue);
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                            }
                                            break;
                                        case 4: // Date
                                            try
                                            {
                                                if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                {
                                                    DateTime __test = DateTime.Parse(__newDateValue, MyLib._myGlobal._cultureInfo());
                                                    //
                                                    if (__whereFirst == true)
                                                    {
                                                        __where.Append(" or ");
                                                    }
                                                    __whereFirst = true;
                                                    //
                                                    //if (this._addQuotWhere)
                                                    //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                    //else
                                                    __where.Append(string.Concat(__getColumnType._query));
                                                    DateTime __newDate = MyLib._myGlobal._convertDate(__getValue);
                                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                    {
                                                        __newDate = MyLib._myGlobal._convertDate(__newDateValue);
                                                        __where.Append(string.Concat(__getValue[0].ToString(), "\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                    }
                                                    else
                                                    {
                                                        __where.Append(string.Concat("=\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                            }
                                            break;
                                        case 11:
                                            {

                                            }
                                            break;
                                        default:// String
                                            //
                                            if (__valueExtra == false)
                                            {
                                                if (__whereFirst == true)
                                                {
                                                    __where.Append(" or ");
                                                }
                                                __whereFirst = true;
                                                //
                                                //if (this._addQuotWhere)
                                                //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                //else
                                                __where.Append(string.Concat("upper(" + __getColumnType._query + ")"));
                                                if (this._searchTextbox.textBox.Text[0] == '+')
                                                {
                                                    __where.Append(string.Concat(" like \'", __newDateValue.Remove(0, 1).ToUpper(), "%\'"));
                                                }
                                                else
                                                {
                                                    __where.Append(string.Concat(" like \'%", __newDateValue.ToUpper(), "%\'"));
                                                }
                                            }
                                            break;
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                } // for
            }
            if (__where.Length > 0)
            {
                __where = new StringBuilder("(" + __where.ToString() + ")");
            }

            StringBuilder __query = new StringBuilder();
            __query.Append("select code, name_1 from ic_inventory ");

            if (__where.Length > 0)
            {
                __query.Append(" where " + __where.ToString());
            }
            __query.Append(" order by code ");

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __result = __myFrameWork._queryShort(__query.ToString()).Tables[0];
            this._icGrid._loadFromDataTable(__result);
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            this._saveData();
        }

        void _saveData()
        {

            bool __pass = true; ;

            string __check = this._screenTop._checkEmtryField();
            if (__check.Length > 0)
            {
                MessageBox.Show(__check, "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                __pass = false;
            }

            this._gridCostDate._updateRowIsChangeAll(true);


            if (__pass)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                ArrayList __getData = this._screenTop._createQueryForDatabase();

                StringBuilder __query = new StringBuilder();


                if (_mode == 0)
                {
                    __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_inventory_price_formula._table + " where " + _g.d.ic_inventory_price_formula._ic_code + "=\'" + this._itemCode + "\'"));
                    // update
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_inventory._table + " set " + __getData[2].ToString() + " where " + _g.d.ic_inventory._code + "=\'" + this._itemCode + "\' "));

                    this._icPriceFormulaGrid._updateRowIsChangeAll(true);
                    __query.Append(this._icPriceFormulaGrid._createQueryForInsert(_g.d.ic_inventory_price_formula._table, _g.d.ic_inventory_price_formula._ic_code + ",", "\'" + this._itemCode + "\',"));

                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_cost_date._table + " where " + _g.d.ic_cost_date._ic_code + "=\'" + this._itemCode + "\'"));
                    __query.Append(this._gridCostDate._createQueryForInsert(_g.d.ic_cost_date._table, _g.d.ic_cost_date._ic_code + ",", "\'" + this._itemCode + "\',", false, true));

                    __query.Append("</node>");
                }
                else
                {
                    // insert
                    string __getItemCode = this._screenTop._getDataStr(_g.d.ic_inventory._code);
                    string __getUnitCode = this._screenTop._getDataStr(_g.d.ic_inventory._unit_cost).ToString();
                    __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_inventory_price_formula._table + " where " + _g.d.ic_inventory_price_formula._ic_code + "=\'" + __getItemCode + "\'"));

                    string __fieldMaster = MyLib._myGlobal._fieldAndComma(
                        _g.d.ic_inventory._item_type,
                        _g.d.ic_inventory._unit_standard,
                        _g.d.ic_inventory._cost_type,
                        _g.d.ic_inventory._unit_type,
                        _g.d.ic_inventory._update_detail,
                        _g.d.ic_inventory._update_price);
                    string __valueMaster = MyLib._myGlobal._fieldAndComma(
                        "0",
                        "\'" + __getUnitCode + "\'",
                        "0",
                        "0",
                        "1",
                        "1");

                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_inventory._table + "(" + __getData[0].ToString() + "," + __fieldMaster + ") values ( " + __getData[1].ToString() + "," + __valueMaster + ") "));

                    this._icPriceFormulaGrid._updateRowIsChangeAll(true);
                    __query.Append(this._icPriceFormulaGrid._createQueryForInsert(_g.d.ic_inventory_price_formula._table, _g.d.ic_inventory_price_formula._ic_code + ",", "\'" + __getItemCode + "\',"));

                    // unit
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_unit_use._table + " (ic_code, code, stand_value, divide_value, ratio, line_number, status) values (\'" + __getItemCode + "\', \'" + __getUnitCode + "\', 1, 1, 1, 0, 1) "));

                    // wh shelf
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_wh_shelf._table + " (ic_code, wh_code, shelf_code) select '" + __getItemCode + "', whcode, code from ic_shelf order by whcode, code "));

                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_cost_date._table + " where " + _g.d.ic_cost_date._ic_code + "=\'" + __getItemCode + "\'"));
                    __query.Append(this._gridCostDate._createQueryForInsert(_g.d.ic_cost_date._table, _g.d.ic_cost_date._ic_code + ",", "\'" + __getItemCode + "\',", false, true));

                    __query.Append("</node>");

                }

                if (__query.Length > 0)
                {
                    string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                    if (__result.Length > 0)
                    {
                        MessageBox.Show(__result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        string _codepic = this._screenTop._getDataStr(_g.d.ic_inventory._code);
                        string _codepic_ = _codepic.Replace("/", "").Trim();
                        string __result2 = "";// this._getPicture._updateImage(_codepic);
                        if (__result2.Length > 0)
                        {
                            MessageBox.Show("Image Error : " + __result2);
                        }
                        MessageBox.Show("บันทึกสำเร็จ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        _g._utils __utils = new _g._utils();
                        __utils._updateInventoryMaster("");
                        Thread __thread = new Thread(new ThreadStart(__utils._updateInventoryMasterFunction));
                        __thread.Start();

                        this._clear();
                        this._load();
                    }
                }
            }
        }

        void _clear()
        {
            this._mode = 1;
            this._screenTop._clear();
            this._icPriceFormulaGrid._clear();
            this._icWarehouseBalanceGrid._clear();

        }

        void _newData()
        {
            this._clear();
            this._screenTop._enabedControl(_g.d.ic_inventory._code, true);
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _newButton_Click(object sender, EventArgs e)
        {
            this._newData();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                this._timer.Stop();
                this._timer2.Stop();

                this._timer.Start();
                this._timer2.Start();

            }

            switch (keyData)
            {
                case Keys.F12:
                    {
                        this._icGrid._removeLastControl();
                        this._saveData();
                        return true;
                    }
                case Keys.F8:
                    {
                        this._newData();
                        return true;
                    }
                case Keys.F2:
                    {
                        this._searchTextbox.Focus();
                        return true;
                    }

            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        string _oldText = "";

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                if (_searchAuto.Checked)
                {
                    if (_oldText.CompareTo(this._searchTextbox.textBox.Text) != 0)
                    {
                        _oldText = this._searchTextbox.textBox.Text;
                        this._load();
                    }
                }
            }
        }

        string _searchEngineOldText = "";
        private void _timer2_Tick(object sender, EventArgs e)
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                if (_searchEngineAuto.Checked)
                {
                    if (_searchEngineOldText.CompareTo(this._searchEngineTextbox.textBox.Text) != 0)
                    {
                        _searchEngineOldText = this._searchEngineTextbox.textBox.Text;
                        this._searchFromEngineCode();
                    }
                }
            }
        }

        private void _expendAllButton_Click(object sender, EventArgs e)
        {
            foreach (TreeNode getNode in this._partTreeView.Nodes)
            {
                _expandAll(getNode);
            }
        }

        private void __collapseAllButton_Click(object sender, EventArgs e)
        {
            foreach (TreeNode getNode in this._partTreeView.Nodes)
            {
                _collapseAll(getNode);
            }
        }
    }

    public class _screen_ic_inventory_information_2 : MyLib._myScreen
    {
        public _screen_ic_inventory_information_2()
        {
            this.Font = new System.Drawing.Font("Tahoma", 15.95f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222))); ;
            this._table_name = _g.d.ic_inventory._table;
            this._maxColumn = 2;
            this._lineSpace = 4;
            int __row = 0;
            this._addTextBox(__row, 0, 1, 0, _g.d.ic_inventory._code, 2, 1, 1, true, false, false);
            __row++;

            this._addTextBox(__row++, 0, 0, 0, _g.d.ic_inventory._name_1, 4, 0, 0, true, false, false);
            this._addTextBox(__row++, 0, 0, 0, _g.d.ic_inventory._name_2, 4, 0, 0, true, false, true);
            this._addTextBox(__row++, 0, 0, 0, _g.d.ic_inventory._name_eng_1, 4, 0, 0, true, false, true);
            this._addTextBox(__row++, 0, 0, 0, _g.d.ic_inventory._name_eng_2, 4, 0, 0, true, false, true);

            //this._addTextBox(__row, 0, 0, 0, _g.d.ic_inventory._tax_type, 1, 0, 1, true, false, false);
            this._addComboBox(__row, 0, _g.d.ic_inventory._tax_type, true, new string[] { "normal_vat", "exc_vat" }, true);
            this._addTextBox(__row, 1, 0, 0, _g.d.ic_inventory._unit_cost, 1, 0, 1, true, false, false);
            this._refresh();
        }
    }

    public class _icGridInformation : MyLib._myGrid
    {
        public event _icGridInformtionEnterKey _enterKey;

        public _icGridInformation()
        {

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Space)
            {
                if (this._enterKey != null)
                    this._enterKey(this, this._selectRow);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
    public delegate void _icGridInformtionEnterKey(MyLib._myGrid sender, int row);

}
