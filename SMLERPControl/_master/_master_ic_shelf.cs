using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPControl._master
{
	public partial class _master_ic_shelf : UserControl
	{
		public _master_ic_shelf()
		{
			this.SuspendLayout();
			InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._myManageData1._displayMode = 0;
			this._myManageData1._dataList._lockRecord = true;
			this._myManageData1._selectDisplayMode(this._myManageData1._displayMode);
            this._myManageData1._dataList._loadViewFormat(_g.g._search_master_ic_warehouse, MyLib._myGlobal._userSearchScreenGroup, true);
			this._myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
			this._myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
			this._myManageData1._manageButton = this._myToolbar;
			this._myManageData1._manageBackgroundPanel = this._myPanel1;
			this._myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
			this._myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
			this._myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
			this._myManageData1._dataList._referFieldAdd(_g.d.ic_inventory._code, 1);

			this._myManageData1._dataListOpen = true;
			this._myManageData1._calcArea();
			this._myManageData1._dataList._loadViewData(0);
			this._myManageData1._autoSize = true;
			this._myManageData1._autoSizeHeight = 450;
			this.Disposed += new EventHandler(_ic_inventory_price_Disposed);

			this._myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
			//

			this._ic_inventory_screen_top._saveKeyDown += new MyLib.SaveKeyDownHandler(_ic_inventory_screen_top__saveKeyDown);
			this._ic_inventory_screen_top._checkKeyDown += new MyLib.CheckKeyDownHandler(_ic_inventory_screen_top__checkKeyDown);

			this._ic_inventory_data_grid._clear();
			this._ic_inventory_data_grid._queryForUpdateWhere += new MyLib.QueryForUpdateWhereEventHandler(_ic_inventory_data_grid__queryForUpdateWhere);
			this._ic_inventory_data_grid._queryForUpdateCheck += new MyLib.QueryForUpdateCheckEventHandler(_ic_inventory_data_grid__queryForUpdateCheck);
			this._ic_inventory_data_grid._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_ic_inventory_data_grid__queryForInsertCheck);
			this._ic_inventory_data_grid._queryForRowRemoveCheck += new MyLib.QueryForRowRemoveCheckEventHandler(_ic_inventory_data_grid__queryForRowRemoveCheck);

			this.ResumeLayout();
		}


		bool _ic_inventory_data_grid__queryForRowRemoveCheck(MyLib._myGrid sender, int row)
		{
			if (sender._cellGet(row, 0) == null)
			{
				return true;
			}
			// เป็นค่าว่างหรือไม่ (ให้ลบออก)
			if (((string)sender._cellGet(row, 0)).ToString().Length == 0)
			{
				return true;
			}
			return false;
		}

		bool _ic_inventory_data_grid__queryForInsertCheck(MyLib._myGrid sender, int row)
		{
			if (sender._cellGet(row, 0) == null)
			{
				return false;
			}
			// เป็นค่าว่างหรือไม่ (ไม่บันทึก)
			if (((string)sender._cellGet(row, 0)).ToString().Length == 0)
			{
				return false;
			}
			return true;
		}

		bool _ic_inventory_data_grid__queryForUpdateCheck(MyLib._myGrid sender, int row)
		{
			if (sender._cellGet(row, 0) == null)
			{
				return false;
			}
			// เป็นค่าว่างหรือไม่ (ไม่บันทึก)
			if (((string)sender._cellGet(row, 0)).ToString().Length == 0)
			{
				return false;
			}
			// ดูว่าเป็น row_number เป็น 0 หรือไม่ ถ้าเป็น 0 คือเป็นรายการใหม่ (ห้ามใช้คำสั่ง Update เดี๋ยวมันจะไป Insert ให้ต่อไป)
			if (((int)sender._cellGet(row, sender._rowNumberName)) == 0)
			{
				return false;
			}
			return true;
		}

		string _ic_inventory_data_grid__queryForUpdateWhere(MyLib._myGrid sender, int row)
		{
			int __getInt = (int)sender._cellGet(row, sender._rowNumberName);
			return (sender._rowNumberName + "=" + __getInt.ToString());
		}

		void _ic_inventory_price_Disposed(object sender, EventArgs e)
		{
			this.Dispose();
		}

		Boolean _ic_inventory_screen_top__checkKeyDown(object sender, Keys keyData)
		{
			if (this._myManageData1._manageButton.Enabled == false)
			{
				MyLib._myGlobal._displayWarning(4, null);
			}
			return true;
		}

		void _ic_inventory_screen_top__saveKeyDown(object sender)
		{
			_save_data();
		}

		void _myManageData1__clearData()
		{
			this._ic_inventory_screen_top._clear();
			this._ic_inventory_data_grid._clear();
		}

		bool _myManageData1__discardData()
		{
			bool result = true;
			if (this._ic_inventory_screen_top._isChange)
			{
				if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
				{
					result = false;
				}
				else
				{
					this._ic_inventory_screen_top._isChange = false;
				}
			}
			return (result);
		}

		void _myManageData1__newDataClick()
		{
			Control __codeControl = this._ic_inventory_screen_top._getControl(_g.d.ic_warehouse._code);
			__codeControl.Enabled = true;
			this._ic_inventory_screen_top._focusFirst();
			this._ic_inventory_screen_top._clear();
			this._ic_inventory_data_grid._clear();
		}


		void _dataList__deleteData(ArrayList selectRowOrder)
		{
			_get_column_number();
			StringBuilder __myQuery = new StringBuilder();
			__myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
			int _getColumnCode = this._myManageData1._dataList._gridData._findColumnByName(_g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code);
			for (int loop = 0; loop < selectRowOrder.Count; loop++)
			{
				MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[loop];
				string __getWHCode = this._myManageData1._dataList._gridData._cellGet(getData.row, _getColumnCode).ToString();
				__myQuery.Append(string.Format("<query>delete from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._whcode + "=\'" + __getWHCode + "\'</query>"));
			} // for
			__myQuery.Append("</node>");
			MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
			string __result = myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
			if (__result.Length == 0)
			{
				MyLib._myGlobal._displayWarning(0, null);
				this._myManageData1._dataList._refreshData();
			}
			else
			{
				MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		int _get_column_number()
		{
			return this._myManageData1._dataList._gridData._findColumnByName(_g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code);
		}

		bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
		{
			try
			{
				ArrayList __rowDataArray = (ArrayList)rowData;
				int xx = _get_column_number();
				string _oldWHCode = __rowDataArray[_get_column_number()].ToString();
				// 
				MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
				StringBuilder __myquery = new StringBuilder();
				__myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
				__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_warehouse._table + whereString));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._whcode + "=\'" + _oldWHCode + "\' order by " + _g.d.ic_shelf._code));
				__myquery.Append("</node>");
				ArrayList _getData = myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
				this._ic_inventory_screen_top._loadData(((DataSet)_getData[0]).Tables[0]);
				this._ic_inventory_data_grid._loadFromDataTable(((DataSet)_getData[1]).Tables[0]);
				//this._search(false);
				if (forEdit)
				{
                    this._ic_inventory_data_grid._updateRowIsChangeAll(true);
                    this._ic_inventory_screen_top._focusFirst();
				}
				//       
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return (true);
		}

		void _myManageData1__closeScreen()
		{
			this.Dispose();
		}

		private void _save_data()
		{
			if (this._myManageData1._manageButton.Enabled)
			{
				string __getEmtry = this._ic_inventory_screen_top._checkEmtryField();
				if (__getEmtry.Length > 0)
				{
					MyLib._myGlobal._displayWarning(2, __getEmtry);
				}
				else
				{
					ArrayList __getDataScreenTop = this._ic_inventory_screen_top._createQueryForDatabase();
					StringBuilder __myQuery = new StringBuilder();
					__myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
					string __dataList = this._ic_inventory_screen_top._getDataStrQuery(_g.d.ic_warehouse._code);
					string __dataListUpdate = "  where " + _g.d.ic_shelf._whcode + " = " + this._ic_inventory_screen_top._getDataStrQuery(_g.d.ic_warehouse._code);
					//
					// ต่อท้ายด้วย Insert บรรทัดใหม่
					__myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_shelf._table + "  " + __dataListUpdate));
					__myQuery.Append(this._ic_inventory_data_grid._createQueryForInsert(_g.d.ic_shelf._table, _g.d.ic_shelf._whcode + ",", __dataList + ",", false));

					__myQuery.Append("</node>");
					MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
					string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
					if (__result.Length == 0)
					{
						MyLib._myGlobal._displayWarning(1, null);
						this._ic_inventory_screen_top._isChange = false;
						this._myManageData1._afterUpdateData();
						this._ic_inventory_screen_top._clear();
						this._ic_inventory_data_grid._clear();
						this._ic_inventory_screen_top._focusFirst();
					}
					else
					{
						MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}


				}
			}
		}
		//

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			Keys keyCode = (Keys)(int)keyData & Keys.KeyCode;
			if ((keyData & Keys.Control) == Keys.Control)
			{
				switch (keyCode)
				{
					case Keys.Home:
						this._ic_inventory_screen_top._focusFirst();
						return true;
				}
			}
			if (keyData == Keys.F12)
			{
				_save_data();
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void _buttonSave_Click(object sender, EventArgs e)
		{
			_save_data();
		}
	}
	public class _master_ic_shelf_screen : MyLib._myScreen
	{
		public _master_ic_shelf_screen()
		{
			this._maxColumn = 2;
			this._table_name = _g.d.ic_warehouse._table;
			this._addTextBox(0, 0, 1, 0, _g.d.ic_warehouse._code, 1, 1, 0, true, false, false);
			this._addTextBox(1, 0, 1, 0, _g.d.ic_warehouse._name_1, 2, 1, 0, true, false, false);
			this._addTextBox(2, 0, 1, 0, _g.d.ic_warehouse._address, 2, 1, 0, true, false, true);
			this._addTextBox(3, 0, 1, 0, _g.d.ic_warehouse._wh_manager, 2, 1, 0, true, false, true);
			this._getControl(_g.d.ic_warehouse._code).Enabled = false;
			this._getControl(_g.d.ic_warehouse._name_1).Enabled = false;
			this._getControl(_g.d.ic_warehouse._address).Enabled = false;
			this._getControl(_g.d.ic_warehouse._wh_manager).Enabled = false;
			this.Dock = DockStyle.Top;
		}
	}

	public partial class _master_ic_shelf_grid : MyLib._myGrid
	{
		public _master_ic_shelf_grid()
		{
			_clear();
			_rowNumberWork = true;
			_getResource = true;
			_table_name = _g.d.ic_shelf._table;
			_addColumn(_g.d.ic_shelf._code, 1, 10, 20, true, false, true, false);
			_addColumn(_g.d.ic_shelf._name_1, 1, 0, 27, true, false, true, false);
			_addColumn(_g.d.ic_shelf._width, 1, 0, 12, true, false, true, false);
			_addColumn(_g.d.ic_shelf._depth, 1, 0, 12, true, false, true, false);
			_addColumn(_g.d.ic_shelf._height, 1, 0, 12, true, false, true, false);
			_addColumn(_g.d.ic_shelf._weight, 1, 0, 12, true, false, true, false);
			_addColumn(_g.d.ic_shelf._status, 11, 0, 5, true, false, true, false);
			_addColumn(this._rowNumberName, 2, 0, 5, false, true, true);
			this.Dock = DockStyle.Fill;
			Refresh();
		}
	}

}
