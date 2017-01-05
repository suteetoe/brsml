using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Npgsql;

namespace BCTOSML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void _sourceConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                _global._sourceServerName = this._sourceServerNameTextBox.Text;
                _global._sourceUserCode = this._sourceUserCodeTextBox.Text;
                _global._sourceUserPassword = this._sourceUserPasswordTextBox.Text;
                _global._sourceDatabase = this._sourceDatabaseNameTextBox.Text;
                SqlConnection __conn = _global._sourceConnect();
                __conn.Open();
                __conn.Close();
                this._sourceConnectStatusTextBox.Text = "Connect Success";
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _targetConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                _global._targetServerName = this._targetServerNameTextBox.Text;
                _global._targetUserCode = this._targetUserCodeTextBox.Text;
                _global._targetUserPassword = this._targetUserPasswordTextBox.Text;
                _global._targetDatabase = this._targetDatabaseNameTextBox.Text;
                NpgsqlConnection __conn = _global._targetConnect();
                __conn.Open();
                __conn.Close();
                this._targetConnectStatusTextBox.Text = "Connect Success";
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private string _balanceQuery()
        {
            return @"select case when BalanceQty=0 then 0 else balanceAmount/BalanceQty end as avg,* from (
                            select itemcode@yyy@,
                            (select name1 from bcitem where code=itemcode) as item_name,
                            sum(temp1.cost) as balanceAmount,
                            (select amount from bcitem where code=itemcode) as BCAmount,
                            (select defstkunitcode from bcitem where bcitem.code=itemcode) as DefStkUnitCode, 
                            (select stockqty from bcitem where code=itemcode) as BCQty,
                            sum(temp1.qty)/isnull((select top 1 case when rate=0 then 1 else rate end from bcstkpacking where bcstkpacking.itemcode=temp1.itemcode and bcstkpacking.unitcode=(select defstkunitcode from bcitem where bcitem.code=temp1.itemcode)),1) as BalanceQty from  

                            -- ยอดยกมา (เพิ่มสต๊อก)
                            (SELECT     ItemCode@yyy@,sum(isnull(sumofcost,0)-isnull(sumofcost2,0)) as cost,SUM(Qty * ((case when PackingRate1 is null or PackingRate1=0 then 1 else PackingRate1 end) / (case when PackingRate2 is null or PackingRate2=0 then 1 else PackingRate2 end))) AS Qty 
                            FROM         BCStkBalanceSub 
                            where @xxx@ iscancel=0 and itemcode<>'.' and docdate<=@date@ and (select stocktype from bcitem where code=itemcode) not in (1,7) 
                            GROUP BY ItemCode@yyy@

                            UNION ALL 
                            -- รับสำเร็จรูป (เพิ่มสต๊อก)
                            SELECT     ItemCode@yyy@,sum(isnull(sumofcost,0)-isnull(sumofcost2,0)) as cost,SUM(Qty * ((case when PackingRate1 is null or PackingRate1=0 then 1 else PackingRate1 end) / (case when PackingRate2 is null or PackingRate2=0 then 1 else PackingRate2 end))) AS Qty 
                            FROM         BCFinishGoodsSub 
                            where @xxx@ iscancel=0 and itemcode<>'.'  and docdate<=@date@ and (select stocktype from bcitem where code=itemcode) not in (1,7) 
                            GROUP BY ItemCode@yyy@

                            union all
                            -- ซื้อ (เพิ่มสต๊อก)
                            SELECT     ItemCode@yyy@,sum(isnull(sumofcost,0)-isnull(sumofcost2,0)) as cost,SUM(Qty * ((case when PackingRate1 is null or PackingRate1=0 then 1 else PackingRate1 end) / (case when PackingRate2 is null or PackingRate2=0 then 1 else PackingRate2 end))) AS Qty 
                            FROM         bcapinvoicesub 
                            where @xxx@ iscancel=0 and itemcode<>'.'  and docdate<=@date@ and (select stocktype from bcitem where code=itemcode) not in (1,7) 
                            GROUP BY ItemCode@yyy@

                            union all
                            -- เพิ่มหนี้ ลูกหนี้ (ลดสต๊อก)
                            SELECT     ItemCode@yyy@,-1*sum(isnull(sumofcost,0)-isnull(sumofcost2,0)) as cost,-1*SUM(DiscQty * ((case when PackingRate1 is null or PackingRate1=0 then 1 else PackingRate1 end) / (case when PackingRate2 is null or PackingRate2=0 then 1 else PackingRate2 end))) AS Qty 
                            FROM         bcdebitnotesub1
                            where @xxx@ iscancel=0 and itemcode<>'.'  and docdate<=@date@ and (select stocktype from bcitem where code=itemcode) not in (1,7) 
                            GROUP BY ItemCode@yyy@

                            union all
                            -- เพิ่มหนี้เจ้าหนี้ (เพิ่มสต๊อก)
                            SELECT     ItemCode@yyy@,sum(isnull(sumofcost,0)-isnull(sumofcost2,0)) as cost,SUM(DiscQty * ((case when PackingRate1 is null or PackingRate1=0 then 1 else PackingRate1 end) / (case when PackingRate2 is null or PackingRate2=0 then 1 else PackingRate2 end))) AS Qty 
                            FROM         bcdebitnotesub2
                            where @xxx@ iscancel=0 and itemcode<>'.'  and docdate<=@date@ and (select stocktype from bcitem where code=itemcode) not in (1,7) 
                            GROUP BY ItemCode@yyy@

                            union all
                            -- ขาย  (ลดสต๊อก)
                            SELECT     ItemCode@yyy@,-1*sum(isnull(sumofcost,0)-isnull(sumofcost2,0)) as cost,-1*SUM(Qty * ((case when PackingRate1 is null or PackingRate1=0 then 1 else PackingRate1 end) / (case when PackingRate2 is null or PackingRate2=0 then 1 else PackingRate2 end))) AS Qty 
                            FROM         bcarinvoicesub 
                            where @xxx@ iscancel=0 and itemcode<>'.' and docdate<=@date@ and (select stocktype from bcitem where code=itemcode) not in (1,7) 
                            GROUP BY ItemCode@yyy@

                            union all
                            -- รับคืน ลูกหนี้ (เพิ่มสต๊อก)
                            SELECT     ItemCode@yyy@,sum(isnull(sumofcost,0)-isnull(sumofcost2,0)) as cost,SUM(DiscQty * ((case when PackingRate1 is null or PackingRate1=0 then 1 else PackingRate1 end) / (case when PackingRate2 is null or PackingRate2=0 then 1 else PackingRate2 end))) AS Qty 
                            FROM         bccreditnotesub  
                            where @xxx@ iscancel=0 and itemcode<>'.' and docdate<=@date@ and (select stocktype from bcitem where code=itemcode) not in (1,7) 
                            GROUP BY ItemCode@yyy@

                            union all
                            -- โอนออก  (ลดสต๊อก)
                            SELECT     ItemCode@yyy@,-1*sum(isnull(sumofcost,0)-isnull(sumofcost2,0)) as cost,-1*SUM(Qty * ((case when PackingRate1 is null or PackingRate1=0 then 1 else PackingRate1 end) / (case when PackingRate2 is null or PackingRate2=0 then 1 else PackingRate2 end))) AS Qty 
                            FROM         bcstktransfsub3
                            where @xxx@ iscancel=0 and mytype=13  and itemcode<>'.'  and docdate<=@date@ and (select stocktype from bcitem where code=itemcode) not in (1,7) 
                            GROUP BY ItemCode@yyy@

                            union all
                            -- โอนเข้า (เพิ่มสต๊อก)
                            SELECT     ItemCode@yyy@,sum(isnull(sumofcost,0)-isnull(sumofcost2,0)) as cost,SUM(Qty * ((case when PackingRate1 is null or PackingRate1=0 then 1 else PackingRate1 end) / (case when PackingRate2 is null or PackingRate2=0 then 1 else PackingRate2 end))) AS Qty 
                            FROM         bcstktransfsub3
                            where @xxx@ iscancel=0 and mytype=12  and itemcode<>'.'  and docdate<=@date@ and (select stocktype from bcitem where code=itemcode) not in (1,7) 
                            GROUP BY ItemCode@yyy@

                            union all
                            -- ลดหนี้ เจ้าหนี้  (ลดสต๊อก)
                            SELECT     ItemCode@yyy@,-1*sum(isnull(sumofcost,0)-isnull(sumofcost2,0)) as cost,-1*SUM(DiscQty * ((case when PackingRate1 is null or PackingRate1=0 then 1 else PackingRate1 end) / (case when PackingRate2 is null or PackingRate2=0 then 1 else PackingRate2 end))) AS Qty 
                            FROM         bcstkrefundsub
                            where @xxx@ iscancel=0 and itemcode<>'.'  and docdate<=@date@ and (select stocktype from bcitem where code=itemcode) not in (1,7) 
                            GROUP BY ItemCode@yyy@

                            union all
                            -- รับคินจากการเบิก (เพิ่มสต๊อก)
                            SELECT     ItemCode@yyy@,sum(isnull(sumofcost,0)-isnull(sumofcost2,0)) as cost,SUM(Qty * ((case when PackingRate1 is null or PackingRate1=0 then 1 else PackingRate1 end) / (case when PackingRate2 is null or PackingRate2=0 then 1 else PackingRate2 end))) AS Qty 
                            FROM         bcstkissretsub 
                            where @xxx@ iscancel=0 and itemcode<>'.'  and docdate<=@date@ and (select stocktype from bcitem where code=itemcode) not in (1,7) 
                            GROUP BY ItemCode@yyy@

                            union all
                            -- ปรับปรุง บวก (เพิ่มสต๊อก)
                            SELECT     ItemCode@yyy@,sum(isnull(sumofcost,0)-isnull(sumofcost2,0)) as cost,SUM(Qty * ((case when PackingRate1 is null or PackingRate1=0 then 1 else PackingRate1 end) / (case when PackingRate2 is null or PackingRate2=0 then 1 else PackingRate2 end))) AS Qty 
                            FROM         bcstkadjustsub
                            where @xxx@ iscancel=0 and AdjMark=0  and itemcode<>'.'  and docdate<=@date@ and (select stocktype from bcitem where code=itemcode) not in (1,7) 
                            GROUP BY ItemCode@yyy@

                            union all
                            -- ปรับปรุง ลบ  (ลดสต๊อก)
                            SELECT     ItemCode@yyy@,-1*sum(isnull(sumofcost,0)-isnull(sumofcost2,0)) as cost,-1*SUM(Qty * ((case when PackingRate1 is null or PackingRate1=0 then 1 else PackingRate1 end) / (case when PackingRate2 is null or PackingRate2=0 then 1 else PackingRate2 end))) AS Qty 
                            FROM         bcstkadjustsub
                            where @xxx@ iscancel=0 and AdjMark=1  and itemcode<>'.'  and docdate<=@date@ and (select stocktype from bcitem where code=itemcode) not in (1,7) 
                            GROUP BY ItemCode@yyy@

                            UNION ALL 
                            -- เบิก  (ลดสต๊อก)
                            SELECT     ItemCode@yyy@,-1*sum(isnull(sumofcost,0)-isnull(sumofcost2,0)) as cost,-1*SUM(Qty * ((case when PackingRate1 is null or PackingRate1=0 then 1 else PackingRate1 end) / (case when PackingRate2 is null or PackingRate2=0 then 1 else PackingRate2 end))) AS Qty 
                            FROM         BCStkIssueSub
                            where @xxx@ iscancel=0 and itemcode<>'.' and docdate<=@date@ and (select stocktype from bcitem where code=itemcode) not in (1,7) 
                            GROUP BY ItemCode@yyy@
) as temp1  
                            GROUP BY ItemCode@yyy@
) as x";
        }

        private void _checkButton_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection __sourceConnect = _global._sourceConnect();
                __sourceConnect.Open();
                string __query = this._balanceQuery().Replace("@xxx@", "").Replace("@yyy@", "") + " where balanceAmount<>bcamount or BCQty<>balanceqty";
                DataSet __ds = new DataSet();
                SqlDataAdapter __cmd = new SqlDataAdapter(__query, __sourceConnect);
                __cmd.Fill(__ds);
                __sourceConnect.Close();
                this._dataGridView.DataSource = __ds.Tables[0];
                MessageBox.Show("Success");
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        StringBuilder _itemList = new StringBuilder();
        int __docNoRunning = 0;

        private void _saveBill(SqlConnection __sourceConnect)
        {
            string __lastQuery = "";
            try
            {
                NpgsqlConnection __targetConnect = _global._targetConnect();
                __targetConnect.Open();
                //
                __docNoRunning++;
                string __docNo = "BL-" + __docNoRunning.ToString("0#####");
                Application.DoEvents();
                string __query = this._balanceQuery().Replace("@date@", "\'" + this._endDateTextBox.Text + "\'").Replace("@yyy@", ",whcode,shelfcode").Replace("@xxx@", " itemcode in (" + this._itemList.ToString() + ") and ") + " where balanceqty<>0";
                DataSet __dsItem = new DataSet();
                SqlDataAdapter __cmdItem = new SqlDataAdapter(__query, __sourceConnect);
                __cmdItem.Fill(__dsItem);
                // Insert
                decimal __totalAmount = 0M;
                string __docDate = "2011-1-1";
                for (int __itemRow = 0; __itemRow < __dsItem.Tables[0].Rows.Count; __itemRow++)
                {
                    DataRow __dr = __dsItem.Tables[0].Rows[__itemRow];
                    string __itemCode = __dr["itemcode"].ToString();
                    string __itemName = __dr["item_name"].ToString().Replace("\'", "").Replace("\"", "");
                    __itemName = "";
                    string __unitCode = __dr["DefStkUnitCode"].ToString();
                    string __wh = __dr["whcode"].ToString();
                    string __shelf = __dr["shelfcode"].ToString();
                    decimal __average = Decimal.Parse(__dr["avg"].ToString());
                    decimal __amount = Decimal.Parse(__dr["balanceAmount"].ToString());
                    decimal __qty = Decimal.Parse(__dr["BalanceQty"].ToString());
                    __totalAmount += __amount;
                    string __queryDetail = "insert into ic_trans_detail (trans_type,trans_flag,doc_no,doc_date,doc_time,last_status,item_code,item_name,unit_code,inquiry_type,qty,price,sum_of_cost,status,line_number,branch_code,wh_code,shelf_code,calc_flag,item_type,vat_type,ref_row,doc_date_calc,doc_time_calc,discount_amount,price_exclude_vat,sum_amount) values (3,54,\'" + __docNo + "\',\'" + __docDate + "\',\'08:00\',0,\'" + __itemCode + "\',\'" + __itemName + "\',\'" + __unitCode + "\',0," + __qty.ToString() + "," + __average.ToString() + "," + __amount.ToString() + ",0," + __itemRow.ToString() + ",\'000\',\'" + __wh + "\',\'" + __shelf + "\',1,0,0,0,\'" + __docDate + "\',\'08:00\',0," + __average.ToString() + "," + __amount.ToString() + ")";
                    __lastQuery = __queryDetail;
                    NpgsqlCommand __cmdInsertDetail = new NpgsqlCommand(__queryDetail, __targetConnect);
                    __cmdInsertDetail.ExecuteNonQuery();
                }
                string __queryHead = "insert into ic_trans (trans_type,trans_flag,doc_no,doc_date,doc_time,last_status,status,used_status,doc_success,total_amount) values (3,54,\'" + __docNo + "\',\'" + __docDate + "\',\'08:00\',0,0,0,0," + __totalAmount.ToString() + ")";
                __lastQuery = __queryHead;
                NpgsqlCommand __cmdInsertHead = new NpgsqlCommand(__queryHead, __targetConnect);
                __cmdInsertHead.ExecuteNonQuery();
                __targetConnect.Close();
            }
            catch (Exception __ex)
            {
                _errorForm __form = new _errorForm(__ex.Message.ToString() + " : " + __lastQuery);
                __form.ShowDialog();
            }
        }

        private void _transferBalanceButton_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection __sourceConnect = _global._sourceConnect();
                __sourceConnect.Open();
                //
                string __queryItem = "select code from bcitem";
                DataSet __dsItem = new DataSet();
                SqlDataAdapter __cmdItem = new SqlDataAdapter(__queryItem, __sourceConnect);
                __cmdItem.Fill(__dsItem);
                //
                this._itemList = new StringBuilder();
                int __count = 0;
                for (int __itemRow = 0; __itemRow < __dsItem.Tables[0].Rows.Count; __itemRow++)
                {
                    if (this._itemList.Length > 0)
                    {
                        this._itemList.Append(",");
                    }
                    this._itemList.Append("\'" + __dsItem.Tables[0].Rows[__itemRow][0].ToString() + "\'");
                    __count++;
                    if (__count > 50)
                    {
                        this._saveBill(__sourceConnect);
                        __count = 0;
                        this._itemList = new StringBuilder();
                    }
                }
                if (__count > 0)
                {
                    this._saveBill(__sourceConnect);
                }
                __sourceConnect.Close();
                MessageBox.Show("Success");
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        public Decimal _decimalPhase(string value)
        {
            decimal __getValue = 0M;
            if (value != null && value.Length != 0)
            {
                decimal __value = 0M;
                if (Decimal.TryParse(value, out __value) == true)
                {
                    __getValue = __value;
                }
            }
            return __getValue;
        }

        private void _priceStartButton_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection __sourceConnect = _global._sourceConnect();
                __sourceConnect.Open();
                //
                string __queryItem = "select ItemCode,UnitCode,saletype,SalePrice1,SalePrice2,SalePrice3,SalePrice4,SalePrice5 from BCPriceList";
                DataSet __dsItem = new DataSet();
                SqlDataAdapter __cmdItem = new SqlDataAdapter(__queryItem, __sourceConnect);
                __cmdItem.Fill(__dsItem);
                //
                NpgsqlConnection __targetConnect = _global._targetConnect();
                __targetConnect.Open();
                for (int __itemRow = 0; __itemRow < __dsItem.Tables[0].Rows.Count; __itemRow++)
                {
                    try
                    {
                        StringBuilder __query = new StringBuilder();
                        __query.Append("insert into ic_inventory_price_formula (ic_code,unit_code,sale_type,price_1,price_2,price_3,price_4,price_5,price_0) values (");
                        __query.Append("\'" + __dsItem.Tables[0].Rows[__itemRow][0].ToString() + "\',");
                        __query.Append("\'" + __dsItem.Tables[0].Rows[__itemRow][1].ToString() + "\',");
                        __query.Append(__dsItem.Tables[0].Rows[__itemRow][2].ToString() + ",");
                        decimal __maxPirce = 0;
                        for (int __loop = 3; __loop <= 7; __loop++)
                        {
                            decimal __price = _decimalPhase(__dsItem.Tables[0].Rows[__itemRow][__loop].ToString());
                            if (__price > __maxPirce)
                            {
                                __maxPirce = __price;
                            }
                            __query.Append("\'" + ((__price == 0) ? "" : __price.ToString()) + "\'");
                            if (__loop == 7)
                            {
                                __query.Append(",");
                                __query.Append("\'" + __maxPirce.ToString() + "\'");
                            }
                            else
                            {
                                __query.Append(",");
                            }
                        }
                        __query.Append(")");
                        NpgsqlCommand __cmdInsertHead = new NpgsqlCommand(__query.ToString(), __targetConnect);
                        __cmdInsertHead.ExecuteNonQuery();
                    }
                    catch
                    {
                    }
                }
                __targetConnect.Close();
                __sourceConnect.Close();
                MessageBox.Show("Success");
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _exportBCItemtoolStrip_Click(object sender, EventArgs e)
        {
            // create column grid
            // add column name
            this._bcItemGridView.Columns.Clear();

            DataGridViewTextBoxColumn __colCode = new DataGridViewTextBoxColumn();
            __colCode.HeaderText = "Item Code";
            __colCode.SortMode = DataGridViewColumnSortMode.NotSortable;
            __colCode.Name = "_itemcode";

            this._bcItemGridView.Columns.Add(__colCode);

            DataGridViewTextBoxColumn __colName1 = new DataGridViewTextBoxColumn();
            __colName1.HeaderText = "Item Name 1";
            __colName1.SortMode = DataGridViewColumnSortMode.NotSortable;
            __colName1.Name = "_itemname1";

            this._bcItemGridView.Columns.Add(__colName1);

            DataGridViewTextBoxColumn __colName2 = new DataGridViewTextBoxColumn();
            __colName2.HeaderText = "Item Name 2";
            __colName2.SortMode = DataGridViewColumnSortMode.NotSortable;
            __colName2.Name = "_itemname2";

            this._bcItemGridView.Columns.Add(__colName2);

            DataGridViewTextBoxColumn __colUnit = new DataGridViewTextBoxColumn();
            __colUnit.HeaderText = "Unit";
            __colUnit.SortMode = DataGridViewColumnSortMode.NotSortable;
            __colUnit.Name = "_itemunit";

            this._bcItemGridView.Columns.Add(__colUnit);


            //// add column type
            //DataGridViewComboBoxColumn __colType = new DataGridViewComboBoxColumn();
            //__colType.Name = "_itemName";
            //__colType.SortMode = DataGridViewColumnSortMode.NotSortable;
            //__colType.HeaderText = "Name 1";
            //__colType.Items.Add("String");
            //__colType.Items.Add("Number");
            //__colType.Items.Add("DateTime");

            //this._bcItemGridView.Columns.Add(__colType);



            SqlConnection __sourceConnect = _global._sourceConnect();
            __sourceConnect.Open();
            //
            //string __queryItem = "select ItemCode,UnitCode,saletype,SalePrice1,SalePrice2,SalePrice3,SalePrice4,SalePrice5 from BCPriceList";
            //DataSet __dsItem = new DataSet();
            //SqlDataAdapter __cmdItem = new SqlDataAdapter(__queryItem, __sourceConnect);
            //__cmdItem.Fill(__dsItem);
            ////
            //NpgsqlConnection __targetConnect = _global._targetConnect();
            //__targetConnect.Open();
            //for (int __itemRow = 0; __itemRow < __dsItem.Tables[0].Rows.Count; __itemRow++)
            //{
            //}

            try
            {
                // create and drop view
                _excuteCommand("drop view pricelist", __sourceConnect);
                _excuteCommand("drop view item_detail", __sourceConnect);
                _excuteCommand("drop view BCItemwarehouse_view", __sourceConnect);
                _excuteCommand("drop view ITEMComponents", __sourceConnect);

                string __viewPriceList = @"create view  Pricelist as " +
                    "(select 1 as status,itemcode,unitcode,'' as arcode,fromqty,toqty,startdate,stopdate,saletype,transporttype,saleprice1,'' as FromArGroup,1 as pricetype from BCPricelist  where itemcode is not null and startdate is not null " +
                    "union all " +
                    "select 1 as status,itemcode,unitcode,arcode,1,9999,'2010-01-01','2020-12-31',0,0,priceamount1,'',3 as pricetype from bcarpricelist " +
                    " union all " +
                    " select 1 as status,itemcode,unitcode,'',fromqty,toqty,'2010-01-01','2020-12-31',0,0,SalePrice,FromArGroup ,2 as pricetype from BCPromoPrice) ";
                _excuteCommand(__viewPriceList, __sourceConnect);

                string __viewItemDetail = @"Create view item_detail as " +
                    "select code,OrderPoint,StockMin,StockMax,SalePrice1,SalePrice2,SalePrice3,SalePrice4," +
                    "defbuyshelf,defbuyunitcode,defsalewhcode,defsaleshelf,defbadwhcode,defbadshelf,defbuywhcode,defsaleunitcode,AccGroupCode " +
                    " from bcitem";
                _excuteCommand(__viewItemDetail, __sourceConnect);

                string __viewBCItemwarehouse = "Create view BCItemwarehouse_view as " +
                    "select distinct Itemcode,WHCode,ShelfCode from BCItemwarehouse where itemcode<>'.'";
                _excuteCommand(__viewBCItemwarehouse, __sourceConnect);

                string __viewITEMComponents = "create view ITEMComponents as " +
                    " select ParentCode,ItemCode,UnitCode,Price1,Qty,Amount1,roworder as line_number " +
                    " from BCITEMComponents";
                _excuteCommand(__viewITEMComponents, __sourceConnect);

                // select item from bc and update to grid

                string __queryItem = "select code,name1,ShortName,UnitType,DefStkUnitCode,ExceptTax,CostType,MyDescription from bcitem";
                DataSet __dsItem = new DataSet();
                SqlDataAdapter __cmdItem = new SqlDataAdapter(__queryItem, __sourceConnect);
                __cmdItem.Fill(__dsItem);
                for (int __itemRow = 0; __itemRow < __dsItem.Tables[0].Rows.Count; __itemRow++)
                {
                    int __addr = this._bcItemGridView.Rows.Add();
                    this._bcItemGridView.Rows[__addr].Cells["_itemcode"].Value = __dsItem.Tables[0].Rows[__itemRow]["code"].ToString();
                    this._bcItemGridView.Rows[__addr].Cells["_itemname1"].Value = __dsItem.Tables[0].Rows[__itemRow]["name1"].ToString();
                    this._bcItemGridView.Rows[__addr].Cells["_itemname2"].Value = __dsItem.Tables[0].Rows[__itemRow]["ShortName"].ToString();
                    this._bcItemGridView.Rows[__addr].Cells["_itemunit"].Value = __dsItem.Tables[0].Rows[__itemRow]["DefStkUnitCode"].ToString();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            __sourceConnect.Close();

        }

        bool _excuteCommand(string query, IDbConnection conn)
        {
            if (conn.GetType() == typeof(SqlConnection))
            {
                try
                {
                    SqlConnection __sqlConn = (SqlConnection)conn;
                    SqlCommand __sqlCommand = new SqlCommand(query, __sqlConn);
                    __sqlCommand.ExecuteNonQuery();

                }
                catch
                {
                    return false;
                }
            }
            else if (conn.GetType() == typeof(NpgsqlConnection))
            {
                try
                {
                    NpgsqlConnection __sqlConn = (NpgsqlConnection)conn;
                    NpgsqlCommand __sqlCommand = new NpgsqlCommand(query, __sqlConn);
                    __sqlCommand.ExecuteNonQuery();

                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        private void _importSMLICtoolStrip_Click(object sender, EventArgs e)
        {
            if (this._bcItemGridView.Rows.Count == 0)
            {
                MessageBox.Show("ไม่พบรายการสินค้า");
                return;
            }
            // 
            // check truncate

            // *BCItemGroup,ic_group
            // Code,code
            // Name,name_1

            // bcItemGroup
            _transferTable("BCItemGroup", "ic_group", new string[] { "Code,code",
                "Name,name_1" });

            // BCItemType
            _transferTable("BCItemType", "ic_design", new string[] { "Code,code",
                "Name,name_1" });

            // *BCItemCategory
            _transferTable("BCItemCategory", "ic_category", new string[] { "Code,code",
                "Name,name_1" });

            // *BCItemFormat
            _transferTable("BCItemFormat", "ic_pattern", new string[] { "Code,code",
                "Name,name_1" });

            // **BCItemBrand
            _transferTable("BCItemBrand", "ic_brand", new string[] { "Code,code",
                "Name,name_1" });

            // *bcitemsize
            _transferTable("bcitemsize", "ic_size", new string[] { "Code,code",
                "Name,name_1" });

            // *bcitemcolor
            _transferTable("bcitemcolor", "ic_color", new string[] { "Code,code",
                "Name,name_1" });

            // *BCItemClassfication
            _transferTable("BCItemClassfication", "ic_model", new string[] { "Code,code",
                "Name,name_1" });

            // BCShelf
            _transferTable("BCShelf", "IC_Shelf", new string[] { "Code,Code",
                "Name,name_1",
                "Whcode,Whcode" });

            // BCItemUnit
            _transferTable("BCItemUnit", "ic_unit", new string[] { "Code,code",
                "Name,name_1" });

            // BCWarehouse
            _transferTable("BCWarehouse", "IC_Warehouse", new string[] { "Code,code",
                "Name,name_1" });

            // bcitem
            _transferTable("bcitem", "ic_inventory", new string[] { "code,code",
                "name1,name_1",
                "name2,name_eng_1",
                "ShortName,short_name",
                "CategoryCode,item_category ",
                "GroupCode,group_main",
                "BrandCode,item_brand",
                "TypeCode,item_design",
                "FormatCode,item_pattern",
                "ColorCode,item_color",
                "MyClass,item_model",
                "MySize,item_size",
                "UnitType,unit_type ",
                "DefStkUnitCode,unit_cost",
                "StockType,item_type",
                "ExceptTax,tax_type",
                "CostType,cost_type",
                "ActiveStatus,status",
                "MyDescription,remark" });

            // item_detail
            _transferTable("item_detail", "ic_inventory_detail", new string[] { "code,ic_code",
"OrderPoint,purchase_point",
"StockMin,minimum_qty",
"StockMax,maximum_qty",
"SalePrice1,sale_price_1,1",
"SalePrice2,sale_price_2,1",
"SalePrice3,sale_price_3,1",
"SalePrice4,sale_price_4,1",
"defbuyshelf,start_purchase_shelf",
"defbuyunitcode,start_purchase_unit",
"defsalewhcode,start_sale_wh",
"defsaleshelf,start_sale_shelf",
"defbadwhcode,ic_out_wh",
"defbadshelf,ic_out_shelf",
"defbuywhcode,start_purchase_wh",
"defsaleunitcode,start_sale_unit",
"AccGroupCode,account_group" });

            // BCbarcodeMaster
            _transferTable("BCbarcodeMaster", "ic_inventory_barcode", new string[] { "itemcode,ic_code",
"barcode,barcode",
"unitcode,unit_code",
"whcode,wh_code",
"shelfcode,shelf_code" });

            // BCItemwarehouse_view
            _transferTable("BCItemwarehouse_view", "ic_wh_shelf", new string[] { "Itemcode,ic_code",
"WHCode,wh_code",
"ShelfCode,shelf_code" });

            // BCStkpacking
            _transferTable("BCStkpacking", "ic_unit_use", new string[] { "unitcode,code",
"Running,line_number",
"Runner,row_order",
"ItemCode,ic_code",
"Rate1,stand_value",
"Rate2,divide_value",
"Rate,ratio" });

            // Pricelist
            _transferTable("Pricelist", "ic_inventory_price", new string[] { "itemcode,ic_code",
"unitcode,unit_code",
"fromqty,from_qty",
"toqty,to_qty",
"startdate,from_date,2",
"stopdate,to_date,2",
"saletype,sale_type",
"transporttype,transport_type",
"saleprice1,sale_price1",
"status,status",
"pricetype,price_type",
"arcode,cust_code",
"FromArGroup,cust_group_1" });


            //_transferTable("BCItemGroup", "ic_group", new string[] { "Code,code", "Name,name_1" });





            //*end_target

            NpgsqlConnection __targetConnect = _global._targetConnect();
            __targetConnect.Open();

            _excuteCommand("update ic_inventory set unit_standard=unit_cost,update_price=1,update_detail=1", __targetConnect);
            _excuteCommand("update ic_unit_use set status=1", __targetConnect);
            _excuteCommand("update ic_inventory_price set status=1,price_mode=0", __targetConnect);
            _excuteCommand("delete from ic_unit_use where ic_code in (select code from ic_inventory where unit_type=0)", __targetConnect);
            _excuteCommand("insert into ic_unit_use (ic_code,code,line_number,stand_value,divide_value,ratio,status) (select code as ic_code,unit_cost as code,1 as line_number,1 as stand_value,1 as divide_value,1 as ratio,1 as status from ic_inventory where unit_type=0)", __targetConnect);
            _excuteCommand("ic_inventory set ic_serial_no=1 where item_type=2", __targetConnect);
            _excuteCommand("update ic_inventory set item_type=2 where item_type=3", __targetConnect);
            _excuteCommand("update ic_inventory set item_type=3 where item_type=5 or item_type=7", __targetConnect);

            __targetConnect.Close();
            //[update ic_inventory set unit_standard=unit_cost,update_price=1,update_detail=1]
            //[update ic_unit_use set status=1]
            //[update ic_inventory_price set status=1,price_mode=0]
            //[update ar_customer_detail set credit_money_max=credit_money,is_lock_record=0]
            //[delete from ic_unit_use where ic_code in (select code from ic_inventory where unit_type=0)]
            //[insert into ic_unit_use (ic_code,code,line_number,stand_value,divide_value,ratio,status) (select code as ic_code,unit_cost as code,1 as line_number,1 as stand_value,1 as divide_value,1 as ratio,1 as status from ic_inventory where unit_type=0)]
            //[update ic_inventory set ic_serial_no=1 where item_type=2]
            //[update ic_inventory set item_type=2 where item_type=3]
            //[update ic_inventory set item_type=3 where item_type=5 or item_type=7]
            //[update ar_customer set status=0 ]
            //[update ap_supplier set status=0]

            //*end_target

            SqlConnection __sourceConnect = _global._sourceConnect();
            __sourceConnect.Open();

            _excuteCommand("drop view pricelist", __sourceConnect);
            _excuteCommand("drop view item_detail", __sourceConnect);
            _excuteCommand("drop view BCItemwarehouse_view", __sourceConnect);
            _excuteCommand("drop view ITEMComponents", __sourceConnect);

            //*end
            //[drop view pricelist]
            //[drop view ArContact]
            //[drop view ApContact]
            //[drop view chart_account]
            //[drop view item_detail]
            //[drop view Ar_detail]
            //[drop view Ap_detail]
            //[drop view assets_detail]
            //[drop view BCItemwarehouse_view]
            //[drop view ITEMComponents]
            //*end

            MessageBox.Show(" import Success");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceTable"></param>
        /// <param name="targetTable"></param>
        /// <param name="__fieldMapList">0=string,1=number,2=date</param>
        /// <returns></returns>
        bool _transferTable(string sourceTable, string targetTable, string[] __fieldMapList)
        {


            // get Field list 
            List<string> __sourceField = new List<string>();
            List<string> __targetField = new List<string>();
            List<int> __targetFieldType = new List<int>();
            for (int __index = 0; __index < __fieldMapList.Length; __index++)
            {
                string[] __splitWord = __fieldMapList[__index].Split(',');

                __sourceField.Add(__splitWord[0]);
                __targetField.Add(__splitWord[1]);

                if (__splitWord.Length == 3)
                {
                    __targetFieldType.Add(int.Parse(__splitWord[2]));
                }
                else
                {
                    __targetFieldType.Add(0);
                }
            }

            SqlConnection __sourceConnect = _global._sourceConnect();
            //__sourceConnect.ConnectionTimeout = ((60 * 100) * 1000);
            __sourceConnect.Open();

            string __queryItem = "select " + string.Join(",", __sourceField.ToArray()) + " from " + sourceTable;
            DataSet __dsItem = new DataSet();
            SqlDataAdapter __cmdItem = new SqlDataAdapter(__queryItem, __sourceConnect);
            __cmdItem.Fill(__dsItem);

            NpgsqlConnection __targetConnect = _global._targetConnect();
            __targetConnect.Open();

            if (this._truncateCheckBox.Checked == true)
            {
                // truncate 
                NpgsqlCommand __targetCommandTruncate = new NpgsqlCommand(" truncate table " + targetTable, __targetConnect);
                __targetCommandTruncate.ExecuteNonQuery();
            }

            for (int __itemRow = 0; __itemRow < __dsItem.Tables[0].Rows.Count; __itemRow++)
            {
                // packquery
                StringBuilder __insQuery = new StringBuilder("insert into " + targetTable + " (" + string.Join(",", __targetField.ToArray()) + ") values (");

                // value insert

                List<string> __value = new List<string>();
                for (int __i = 0; __i < __sourceField.Count; __i++)
                {
                    string __valueStr = "";
                    if (__targetFieldType[__i] == 2)
                    {
                        if (__dsItem.Tables[0].Rows[__itemRow][__sourceField[__i]] == null)
                        {
                            __valueStr = "NULL";
                        }
                        else
                        {
                            __valueStr = ((DateTime)__dsItem.Tables[0].Rows[__itemRow][__sourceField[__i]]).ToString("yyyy-MM-dd HH:mm:ss", new System.Globalization.CultureInfo("en-US"));
                        }
                    }
                    else
                    {
                        __valueStr = __dsItem.Tables[0].Rows[__itemRow][__sourceField[__i]].ToString();
                    }


                    //MessageBox.Show(__dsItem.Tables[0].Rows[__itemRow][__sourceField[__i]].GetType().ToString());
                    if (__targetFieldType[__i] == 2 && __valueStr.Length == 0)
                    {
                        __valueStr = "NULL";
                    }
                    else
                    {
                        __valueStr = "'" + _convertStrToQuery(__valueStr) + "'";
                    }

                    if (__targetFieldType[__i] == 1)
                    {
                        __value.Add("0");
                    }
                    else
                        __value.Add(__valueStr);
                }
                __insQuery.Append(string.Join(",", __value.ToArray()));
                __insQuery.Append(")");

                NpgsqlCommand __pgcommand = new NpgsqlCommand(__insQuery.ToString(), __targetConnect);
                __pgcommand.ExecuteNonQuery();
            }

            return true;
        }

        public static string _convertStrToQuery(string str)
        {
            return str.Replace("\'", "\'\'");
        }

        private void _transferAR_Click(object sender, EventArgs e)
        {
            SqlConnection __sourceConnect = _global._sourceConnect();
            __sourceConnect.Open();

            string __queryItem = "select code,name1, billaddress, telephone,fax, debtlimit1,emailaddress from bcar ";
            DataSet __dsItem = new DataSet();
            SqlDataAdapter __cmdItem = new SqlDataAdapter(__queryItem, __sourceConnect);
            __cmdItem.Fill(__dsItem);

            NpgsqlConnection __targetConnect = _global._targetConnect();
            __targetConnect.Open();


            if (_arTruncateCheckbox.Checked == true)
            {
                NpgsqlCommand __targetCommandTruncate = new NpgsqlCommand(" truncate table ar_customer", __targetConnect);
                __targetCommandTruncate.ExecuteNonQuery();

                NpgsqlCommand __targetCommandTruncate2 = new NpgsqlCommand(" truncate table ar_customer_detail", __targetConnect);
                __targetCommandTruncate2.ExecuteNonQuery();
            }

            try
            {
                for (int __itemRow = 0; __itemRow < __dsItem.Tables[0].Rows.Count; __itemRow++)
                {
                    string __arCode = __dsItem.Tables[0].Rows[__itemRow]["code"].ToString();

                    string __value = String.Concat(
                        "'" + __arCode + "'", ",",
                        "'" + _convertStrToQuery(__dsItem.Tables[0].Rows[__itemRow]["name1"].ToString()) + "'", ",",
                        "'" + _convertStrToQuery(__dsItem.Tables[0].Rows[__itemRow]["billaddress"].ToString()) + "'", ",",
                        "'" + __dsItem.Tables[0].Rows[__itemRow]["telephone"].ToString() + "'", ",",
                        "'" + __dsItem.Tables[0].Rows[__itemRow]["fax"].ToString() + "'", ",",
                        "'" + __dsItem.Tables[0].Rows[__itemRow]["emailaddress"].ToString() + "'"
                        );
                    // packquery
                    StringBuilder __insQuery = new StringBuilder("insert into ar_customer (code, name_1, address, telephone,fax,email) values (" + __value + ")");

                    NpgsqlCommand __pgcommand = new NpgsqlCommand(__insQuery.ToString(), __targetConnect);
                    __pgcommand.ExecuteNonQuery();

                    string __credit_money = __dsItem.Tables[0].Rows[__itemRow]["debtlimit1"].ToString();


                    string __insArDetail = "insert into ar_customer_detail (ar_code,credit_money) values('" + __arCode + "','" + __credit_money + "')";
                    NpgsqlCommand __pgcommand2 = new NpgsqlCommand(__insArDetail.ToString(), __targetConnect);
                    __pgcommand2.ExecuteNonQuery();

                    // value insert



                }

                MessageBox.Show("Success");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }


        }

        private void _transferAP_Click(object sender, EventArgs e)
        {
            SqlConnection __sourceConnect = _global._sourceConnect();
            __sourceConnect.Open();

            string __queryItem = "select code,name1, address, telephone,fax,emailaddress from bcap";
            DataSet __dsItem = new DataSet();
            SqlDataAdapter __cmdItem = new SqlDataAdapter(__queryItem, __sourceConnect);
            __cmdItem.Fill(__dsItem);

            NpgsqlConnection __targetConnect = _global._targetConnect();
            __targetConnect.Open();


            if (_aptruncatecheckBox.Checked == true)
            {
                NpgsqlCommand __targetCommandTruncate = new NpgsqlCommand(" truncate table ap_supplier", __targetConnect);
                __targetCommandTruncate.ExecuteNonQuery();

                //NpgsqlCommand __targetCommandTruncate2 = new NpgsqlCommand(" truncate table ar_customer_detail", __targetConnect);
                //__targetCommandTruncate2.ExecuteNonQuery();
            }

            try
            {
                for (int __itemRow = 0; __itemRow < __dsItem.Tables[0].Rows.Count; __itemRow++)
                {
                    string __arCode = __dsItem.Tables[0].Rows[__itemRow]["code"].ToString();

                    string __value = String.Concat(
                        "'" + __arCode + "'", ",",
                        "'" + _convertStrToQuery(__dsItem.Tables[0].Rows[__itemRow]["name1"].ToString()) + "'", ",",
                        "'" + _convertStrToQuery(__dsItem.Tables[0].Rows[__itemRow]["address"].ToString()) + "'", ",",
                        "'" + __dsItem.Tables[0].Rows[__itemRow]["telephone"].ToString() + "'", ",",
                        "'" + __dsItem.Tables[0].Rows[__itemRow]["fax"].ToString() + "'", ",",
                        "'" + __dsItem.Tables[0].Rows[__itemRow]["emailaddress"].ToString() + "'"
                        );
                    // packquery
                    StringBuilder __insQuery = new StringBuilder("insert into ap_supplier (code, name_1, address, telephone,fax,email) values (" + __value + ")");

                    NpgsqlCommand __pgcommand = new NpgsqlCommand(__insQuery.ToString(), __targetConnect);
                    __pgcommand.ExecuteNonQuery();

                    //string __credit_money = __dsItem.Tables[0].Rows[__itemRow]["debtlimit1"].ToString();


                    //string __insArDetail = "insert into ar_customer_detail (ar_code,credit_money) values('" + __arCode + "','" + __credit_money + "')";
                    //NpgsqlCommand __pgcommand2 = new NpgsqlCommand(__insArDetail.ToString(), __targetConnect);
                    //__pgcommand2.ExecuteNonQuery();

                    // value insert


                }



                MessageBox.Show("Success");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }

        }

        ic_table_optionForm _optionForm = null;

        private void _selectTableButton_Click(object sender, EventArgs e)
        {
            _optionForm = new ic_table_optionForm();
            _optionForm._processButton.Click += _processButton_Click;
            _optionForm._closeButton.Click += _closeButton_Click;
            _optionForm._dropViewButton.Click += _dropViewButton_Click;
            _optionForm._updateOtherButton.Click += _updateOtherButton_Click;
            _optionForm.StartPosition = FormStartPosition.CenterScreen;

            _optionForm.ShowDialog();
        }

        void _updateOtherButton_Click(object sender, EventArgs e)
        {
            NpgsqlConnection __targetConnect = _global._targetConnect();
            __targetConnect.Open();

            _excuteCommand("update ic_inventory set unit_standard=unit_cost,update_price=1,update_detail=1", __targetConnect);
            _excuteCommand("update ic_unit_use set status=1", __targetConnect);
            _excuteCommand("update ic_inventory_price set status=1,price_mode=0", __targetConnect);
            _excuteCommand("delete from ic_unit_use where ic_code in (select code from ic_inventory where unit_type=0)", __targetConnect);
            _excuteCommand("insert into ic_unit_use (ic_code,code,line_number,stand_value,divide_value,ratio,status) (select code as ic_code,unit_cost as code,1 as line_number,1 as stand_value,1 as divide_value,1 as ratio,1 as status from ic_inventory where unit_type=0)", __targetConnect);
            _excuteCommand("ic_inventory set ic_serial_no=1 where item_type=2", __targetConnect);
            _excuteCommand("update ic_inventory set item_type=2 where item_type=3", __targetConnect);
            _excuteCommand("update ic_inventory set item_type=3 where item_type=5 or item_type=7", __targetConnect);

            __targetConnect.Close();

            MessageBox.Show("Import Success");

        }

        void _dropViewButton_Click(object sender, EventArgs e)
        {

        }

        void _closeButton_Click(object sender, EventArgs e)
        {
            _optionForm.Close();
            _optionForm.Dispose();
        }

        void _processButton_Click(object sender, EventArgs e)
        {


            if (_optionForm != null)
            {
                if (this._bcItemGridView.Rows.Count == 0)
                {
                    MessageBox.Show("ไม่พบรายการสินค้า");
                    return;
                }

                for (int __i = 0; __i < this._optionForm.Controls.Count; __i++)
                {
                    if (this._optionForm.Controls[__i].GetType() == typeof(System.Windows.Forms.CheckBox))
                    {
                        CheckBox __getCheckbox = (CheckBox)this._optionForm.Controls[__i];

                        if (__getCheckbox.Checked == true)
                        {
                            string __getTableName = __getCheckbox.Text.ToLower();

                            switch (__getTableName)
                            {
                                case "bcitemgroup":
                                    // bcItemGroup
                                    _transferTable("BCItemGroup", "ic_group", new string[] { "Code,code",
                                        "Name,name_1" });
                                    break;
                                case "bcitemtype":

                                    // BCItemType
                                    _transferTable("BCItemType", "ic_design", new string[] { "Code,code",
                                        "Name,name_1" });
                                    break;
                                case "bcitemcategory":

                                    // *BCItemCategory
                                    _transferTable("BCItemCategory", "ic_category", new string[] { "Code,code",
                                        "Name,name_1" });
                                    break;

                                case "bcitemformat":
                                    // *BCItemFormat
                                    _transferTable("BCItemFormat", "ic_pattern", new string[] { "Code,code",
                                        "Name,name_1" });
                                    break;

                                case "bcitembrand":

                                    // **BCItemBrand
                                    _transferTable("BCItemBrand", "ic_brand", new string[] { "Code,code",
                                        "Name,name_1" });
                                    break;

                                case "bcitemsize":
                                    // *bcitemsize
                                    _transferTable("bcitemsize", "ic_size", new string[] { "Code,code",
                                        "Name,name_1" });
                                    break;

                                case "bcitemcolor":
                                    // *bcitemcolor
                                    _transferTable("bcitemcolor", "ic_color", new string[] { "Code,code",
                                        "Name,name_1" });
                                    break;

                                case "bcitemclassfication":
                                    // *BCItemClassfication
                                    _transferTable("BCItemClassfication", "ic_model", new string[] { "Code,code",
                                        "Name,name_1" });
                                    break;

                                // BCShelf
                                case "bcshelf":
                                    _transferTable("BCShelf", "IC_Shelf", new string[] { "Code,Code",
                                        "Name,name_1",
                                        "Whcode,Whcode" });
                                    break;

                                case "BCItemUnit":
                                    // BCItemUnit
                                    _transferTable("BCItemUnit", "ic_unit", new string[] { "Code,code",
                                        "Name,name_1" });
                                    break;

                                case "BCWarehouse":
                                    // BCWarehouse
                                    _transferTable("BCWarehouse", "IC_Warehouse", new string[] { "Code,code",
                                        "Name,name_1" });
                                    break;
                                case "bcitem":

                                    // bcitem
                                    _transferTable("bcitem", "ic_inventory", new string[] { "code,code",
                                        "name1,name_1",
                                        "name2,name_eng_1",
                                        "ShortName,short_name",
                                        "CategoryCode,item_category ",
                                        "GroupCode,group_main",
                                        "BrandCode,item_brand",
                                        "TypeCode,item_design",
                                        "FormatCode,item_pattern",
                                        "ColorCode,item_color",
                                        "MyClass,item_model",
                                        "MySize,item_size",
                                        "UnitType,unit_type ",
                                        "DefStkUnitCode,unit_cost",
                                        "StockType,item_type",
                                        "ExceptTax,tax_type",
                                        "CostType,cost_type",
                                        "ActiveStatus,status",
                                        "MyDescription,remark" });
                                    break;
                                case "item_detail":
                                    // item_detail
                                    _transferTable("item_detail", "ic_inventory_detail", new string[] { "code,ic_code",
                                        "OrderPoint,purchase_point",
                                        "StockMin,minimum_qty",
                                        "StockMax,maximum_qty",
                                        "SalePrice1,sale_price_1,1",
                                        "SalePrice2,sale_price_2,1",
                                        "SalePrice3,sale_price_3,1",
                                        "SalePrice4,sale_price_4,1",
                                        "defbuyshelf,start_purchase_shelf",
                                        "defbuyunitcode,start_purchase_unit",
                                        "defsalewhcode,start_sale_wh",
                                        "defsaleshelf,start_sale_shelf",
                                        "defbadwhcode,ic_out_wh",
                                        "defbadshelf,ic_out_shelf",
                                        "defbuywhcode,start_purchase_wh",
                                        "defsaleunitcode,start_sale_unit",
                                        "AccGroupCode,account_group" });
                                    break;

                                case "bcbarcodemaster":

                                    // BCbarcodeMaster
                                    _transferTable("BCbarcodeMaster", "ic_inventory_barcode", new string[] { "itemcode,ic_code",
                                        "barcode,barcode",
                                        "unitcode,unit_code",
                                        "whcode,wh_code",
                                        "shelfcode,shelf_code" });
                                    break;

                                case "bcitemwarehouse_view":

                                    // BCItemwarehouse_view
                                    _transferTable("BCItemwarehouse_view", "ic_wh_shelf", new string[] { "Itemcode,ic_code",
                                        "WHCode,wh_code",
                                        "ShelfCode,shelf_code" });

                                    break;
                                case "bcstkpacking":
                                    // BCStkpacking
                                    _transferTable("BCStkpacking", "ic_unit_use", new string[] { "unitcode,code",
                                        "Running,line_number",
                                        "Runner,row_order",
                                        "ItemCode,ic_code",
                                        "Rate1,stand_value",
                                        "Rate2,divide_value",
                                        "Rate,ratio" });
                                    break;

                                case "pricelist":


                                    // Pricelist
                                    _transferTable("Pricelist", "ic_inventory_price", new string[] { "itemcode,ic_code",
                                        "unitcode,unit_code",
                                        "fromqty,from_qty",
                                        "toqty,to_qty",
                                        "startdate,from_date,2",
                                        "stopdate,to_date,2",
                                        "saletype,sale_type",
                                        "transporttype,transport_type",
                                        "saleprice1,sale_price1",
                                        "status,status",
                                        "pricetype,price_type",
                                        "arcode,cust_code",
                                        "FromArGroup,cust_group_1" });
                                    break;
                            }
                        }
                    }
                }

                MessageBox.Show("Import Success");

            }
        }

        private void _transferArContact_Click(object sender, EventArgs e)
        {
            SqlConnection __sourceConnect = _global._sourceConnect();
            __sourceConnect.Open();

            string __queryItem = "select parentcode, name, address, telephone, personposition from bccontactlist  where mytype = 0";
            DataSet __dsItem = new DataSet();
            SqlDataAdapter __cmdItem = new SqlDataAdapter(__queryItem, __sourceConnect);
            __cmdItem.Fill(__dsItem);

            NpgsqlConnection __targetConnect = _global._targetConnect();
            __targetConnect.Open();


            if (this._truncateArContact.Checked == true)
            {
                NpgsqlCommand __targetCommandTruncate = new NpgsqlCommand(" truncate table ar_contactor", __targetConnect);
                __targetCommandTruncate.ExecuteNonQuery();

                //NpgsqlCommand __targetCommandTruncate2 = new NpgsqlCommand(" truncate table ar_customer_detail", __targetConnect);
                //__targetCommandTruncate2.ExecuteNonQuery();
            }

            try
            {
                for (int __itemRow = 0; __itemRow < __dsItem.Tables[0].Rows.Count; __itemRow++)
                {
                    string __arCode = __dsItem.Tables[0].Rows[__itemRow]["parentcode"].ToString();

                    string __value = String.Concat(
                        "'" + __arCode + "'", ",",
                        "'" + __dsItem.Tables[0].Rows[__itemRow]["name"].ToString() + "'", ",",
                        "'" + __dsItem.Tables[0].Rows[__itemRow]["address"].ToString() + "'", ",",
                        "'" + __dsItem.Tables[0].Rows[__itemRow]["telephone"].ToString() + "'", ",",
                        //"'" + __dsItem.Tables[0].Rows[__itemRow]["personposition"].ToString() + "'", ",",
                        "'" + __dsItem.Tables[0].Rows[__itemRow]["personposition"].ToString() + "'"
                        );
                    // packquery
                    StringBuilder __insQuery = new StringBuilder("insert into ar_contactor (ar_code, name, address, telephone, work_department ) values (" + __value + ")");

                    NpgsqlCommand __pgcommand = new NpgsqlCommand(__insQuery.ToString(), __targetConnect);
                    __pgcommand.ExecuteNonQuery();

                    //string __credit_money = __dsItem.Tables[0].Rows[__itemRow]["debtlimit1"].ToString();


                    //string __insArDetail = "insert into ar_customer_detail (ar_code,credit_money) values('" + __arCode + "','" + __credit_money + "')";
                    //NpgsqlCommand __pgcommand2 = new NpgsqlCommand(__insArDetail.ToString(), __targetConnect);
                    //__pgcommand2.ExecuteNonQuery();

                    // value insert



                }

                MessageBox.Show("Success");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        private void _apContactTransfer_Click(object sender, EventArgs e)
        {
            SqlConnection __sourceConnect = _global._sourceConnect();
            __sourceConnect.Open();

            string __queryItem = "select parentcode, name, address, telephone, personposition from bccontactlist  where mytype = 1";
            DataSet __dsItem = new DataSet();
            SqlDataAdapter __cmdItem = new SqlDataAdapter(__queryItem, __sourceConnect);
            __cmdItem.Fill(__dsItem);

            NpgsqlConnection __targetConnect = _global._targetConnect();
            __targetConnect.Open();


            if (this._apContactTruncate.Checked == true)
            {
                NpgsqlCommand __targetCommandTruncate = new NpgsqlCommand(" truncate table ap_contactor", __targetConnect);
                __targetCommandTruncate.ExecuteNonQuery();

                //NpgsqlCommand __targetCommandTruncate2 = new NpgsqlCommand(" truncate table ar_customer_detail", __targetConnect);
                //__targetCommandTruncate2.ExecuteNonQuery();
            }

            try
            {
                for (int __itemRow = 0; __itemRow < __dsItem.Tables[0].Rows.Count; __itemRow++)
                {
                    string __arCode = __dsItem.Tables[0].Rows[__itemRow]["parentcode"].ToString();

                    string __value = String.Concat(
                        "'" + __arCode + "'", ",",
                        "'" + __dsItem.Tables[0].Rows[__itemRow]["name"].ToString() + "'", ",",
                        "'" + __dsItem.Tables[0].Rows[__itemRow]["address"].ToString() + "'", ",",
                        "'" + __dsItem.Tables[0].Rows[__itemRow]["telephone"].ToString() + "'", ",",
                        //"'" + __dsItem.Tables[0].Rows[__itemRow]["personposition"].ToString() + "'", ",",
                        "'" + __dsItem.Tables[0].Rows[__itemRow]["personposition"].ToString() + "'"
                        );
                    // packquery
                    StringBuilder __insQuery = new StringBuilder("insert into ap_contactor (ap_code, name, address, telephone, work_department ) values (" + __value + ")");

                    NpgsqlCommand __pgcommand = new NpgsqlCommand(__insQuery.ToString(), __targetConnect);
                    __pgcommand.ExecuteNonQuery();

                    //string __credit_money = __dsItem.Tables[0].Rows[__itemRow]["debtlimit1"].ToString();


                    //string __insArDetail = "insert into ar_customer_detail (ar_code,credit_money) values('" + __arCode + "','" + __credit_money + "')";
                    //NpgsqlCommand __pgcommand2 = new NpgsqlCommand(__insArDetail.ToString(), __targetConnect);
                    //__pgcommand2.ExecuteNonQuery();

                    // value insert



                }

                MessageBox.Show("Success");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }
    }
}

