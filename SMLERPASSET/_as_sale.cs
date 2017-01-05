﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPASSET
{
	public partial class _as_sale : UserControl
	{
		MyLib._searchDataFull _searchSideCode = new MyLib._searchDataFull();
		MyLib._searchDataFull _searchDepartmentCode = new MyLib._searchDataFull();
		MyLib._searchDataFull _searchDocGroup = new MyLib._searchDataFull();
		MyLib._searchDataFull _searchSaleType = new MyLib._searchDataFull();
		MyLib._searchDataFull _searchArCode = new MyLib._searchDataFull();
		string _searchName = "";
		TextBox _searchTextBox;
		MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
		// Old DocNo For Update
		int _getColumnDocNo = 0;
		string _oldDocNo = "";

		public _as_sale()
		{
			InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            // Clear And Auto Run
			this._autoRunningButton._iconNumber = 1;
			this._autoRunningButton.Image = imageList1.Images[this._autoRunningButton._iconNumber];
			this._clearDataAfterSaveButton._iconNumber = 0;
			this._clearDataAfterSaveButton.Image = imageList1.Images[this._clearDataAfterSaveButton._iconNumber];
			// Screen Top
			this._screenTop.AutoSize = true;
			this._screenTop._maxColumn = 2;
			this._screenTop._table_name = _g.d.as_asset_sale._table;
			this._screenTop._addDateBox(0, 0, 1, 0, _g.d.as_asset_sale._doc_date, 1, true, false);
			this._screenTop._addTextBox(0, 1, 1, 0, _g.d.as_asset_sale._doc_no, 1, 25, 0, true, false, false);
			this._screenTop._addTextBox(1, 0, 1, 0, _g.d.as_asset_sale._doc_group, 1, 10, 1, true, false, true);
			this._screenTop._addTextBox(1, 1, 1, 0, _g.d.as_asset_sale._doc_ref, 1, 25, 0, true, false, true);
			this._screenTop._addDateBox(2, 0, 1, 0, _g.d.as_asset_sale._bill_date, 1, true);
			this._screenTop._addTextBox(2, 1, 1, 0, _g.d.as_asset_sale._bill_no, 1, 25, 0, true, false, true);
			this._screenTop._addDateBox(3, 0, 1, 0, _g.d.as_asset_sale._sale_date, 1, true);
			this._screenTop._addTextBox(3, 1, 1, 0, _g.d.as_asset_sale._sale_type, 1, 10, 1, true, false, true);
			this._screenTop._addTextBox(4, 0, 0, 1, _g.d.as_asset_sale._ar_code, 1, 0, 1, true, false, true);
			this._screenTop._addTextBox(4, 1, 0, 0, "ar_name", 1, 0, 0, false, false, true, false);
			this._screenTop._addTextBox(5, 0, 1, 0, _g.d.as_asset_sale._side_code, 1, 10, 1, true, false, true);
			this._screenTop._addTextBox(5, 1, 1, 0, _g.d.as_asset_sale._department_code, 1, 10, 1, true, false, true);
			this._screenTop._addTextBox(6, 0, 2, _g.d.as_asset_sale._remark, 2, 255);
			this._screenTop._refresh();
			// Event Screen Top
			this._screenTop._checkKeyDown += new MyLib.CheckKeyDownHandler(_screenTop__checkKeyDown);
			this._screenTop._checkKeyDownReturn += new MyLib.CheckKeyDownReturnHandler(_screenTop__checkKeyDownReturn);
			this._screenTop._textBoxSearch += new MyLib.TextBoxSearchHandler(_screenTop__textBoxSearch);
			this._screenTop._textBoxChanged += new MyLib.TextBoxChangedHandler(_screenTop__textBoxChanged);
			this._screenTop._saveKeyDown += new MyLib.SaveKeyDownHandler(_screenTop__saveKeyDown);
			// Start Ar Code
			_searchArCode._name = _g.g._search_screen_ar;
			_searchArCode._dataList._loadViewFormat(_searchArCode._name, MyLib._myGlobal._userSearchScreenGroup, false);
			_searchArCode._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
			_searchArCode._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchArCode__searchEnterKeyPress);
			// Start Sale Type
			_searchSaleType._name = _g.g._search_master_as_asset_retire;
			_searchSaleType._dataList._loadViewFormat(_searchSaleType._name, MyLib._myGlobal._userSearchScreenGroup, false);
			_searchSaleType._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
			_searchSaleType._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchSaleType__searchEnterKeyPress);
			// Start Doc Group
			_searchDocGroup._name = _g.g._search_screen_erp_doc_group;
			_searchDocGroup._dataList._loadViewFormat(_searchDocGroup._name, MyLib._myGlobal._userSearchScreenGroup, false);
			_searchDocGroup._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
			_searchDocGroup._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchDocGroup__searchEnterKeyPress);
			// Start Sid Code
			_searchSideCode._name = _g.g._search_screen_erp_side_list;
			_searchSideCode._dataList._loadViewFormat(_searchSideCode._name, MyLib._myGlobal._userSearchScreenGroup, false);
			_searchSideCode._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
			_searchSideCode._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchSideCode__searchEnterKeyPress);
			// Start Section Code
			_searchDepartmentCode._name = _g.g._search_screen_erp_department_list;
			_searchDepartmentCode._dataList._loadViewFormat(_searchDepartmentCode._name, MyLib._myGlobal._userSearchScreenGroup, false);
			_searchDepartmentCode._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
			_searchDepartmentCode._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchDepartmentCode__searchEnterKeyPress);
			// ค้นหาแบบทันทีเมื่อเลื่อนไปช่องนั้น
			MyLib._myTextBox __getArCodeControl = (MyLib._myTextBox)_screenTop._getControl(_g.d.as_asset_sale._ar_code);
			__getArCodeControl.textBox.Enter += new EventHandler(textBox_Enter);
			__getArCodeControl.textBox.Leave += new EventHandler(textBox_Leave);
			__getArCodeControl.textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);
			// ค้นหาแบบทันทีเมื่อเลื่อนไปช่องนั้น
			MyLib._myTextBox __getSaleTypeControl = (MyLib._myTextBox)_screenTop._getControl(_g.d.as_asset_sale._sale_type);
			__getSaleTypeControl.textBox.Enter += new EventHandler(textBox_Enter);
			__getSaleTypeControl.textBox.Leave += new EventHandler(textBox_Leave);
			__getSaleTypeControl.textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);
			// ค้นหาแบบทันทีเมื่อเลื่อนไปช่องนั้น
			MyLib._myTextBox __getDocGroupControl = (MyLib._myTextBox)_screenTop._getControl(_g.d.as_asset_sale._doc_group);
			__getDocGroupControl.textBox.Enter += new EventHandler(textBox_Enter);
			__getDocGroupControl.textBox.Leave += new EventHandler(textBox_Leave);
			__getDocGroupControl.textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);
			// ค้นหาแบบทันทีเมื่อเลื่อนไปช่องนั้น
			MyLib._myTextBox __getSideCodeControl = (MyLib._myTextBox)_screenTop._getControl(_g.d.as_asset_sale._side_code);
			__getSideCodeControl.textBox.Enter += new EventHandler(textBox_Enter);
			__getSideCodeControl.textBox.Leave += new EventHandler(textBox_Leave);
			__getSideCodeControl.textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);
			// ค้นหาแบบทันทีเมื่อเลื่อนไปช่องนั้น
			MyLib._myTextBox __getSectionCodeControl = (MyLib._myTextBox)_screenTop._getControl(_g.d.as_asset_sale._department_code);
			__getSectionCodeControl.textBox.Enter += new EventHandler(textBox_Enter);
			__getSectionCodeControl.textBox.Leave += new EventHandler(textBox_Leave);
			__getSectionCodeControl.textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);
			// Event Grid
			this._saleGrid1._myGrid1._clear();
			this._saleGrid1._myGrid1._queryForUpdateWhere += new MyLib.QueryForUpdateWhereEventHandler(_myGrid1__queryForUpdateWhere);
			this._saleGrid1._myGrid1._queryForUpdateCheck += new MyLib.QueryForUpdateCheckEventHandler(_myGrid1__queryForUpdateCheck);
			this._saleGrid1._myGrid1._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_myGrid1__queryForInsertCheck);
			this._saleGrid1._myGrid1._queryForRowRemoveCheck += new MyLib.QueryForRowRemoveCheckEventHandler(_myGrid1__queryForRowRemoveCheck);
			this._saleGrid1._myGrid1._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_myGrid1__alterCellUpdate);
			// Manage Data
			_myManageData1._displayMode = 0;
			_myManageData1._selectDisplayMode(_myManageData1._displayMode);
			_myManageData1._dataList._lockRecord = true;
			_myManageData1._dataList._loadViewFormat("screen_as_sale", MyLib._myGlobal._userSearchScreenGroup, true);
			_myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
			_myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
			_myManageData1._manageButton = this._myToolBar;
			_myManageData1._manageBackgroundPanel = this._myPanel1;
			_myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
			_myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
			_myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
			_myManageData1._autoSize = true;
			_myManageData1._autoSizeHeight = 500;
			_myManageData1._dataList._referFieldAdd(_g.d.as_asset_sale._doc_no, 1);
			// Resize
			this.Resize += new EventHandler(_as_sale_Resize);
		}

		void _searchSaleType__searchEnterKeyPress(MyLib._myGrid sender, int row)
		{
			_searchByParent(sender, row);
		}

		bool _myManageData1__discardData()
		{
			bool result = true;
			if (this._screenTop._isChange || this._saleGrid1._myGrid1._isChange)
			{
				if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
				{
					result = false;
				}
				else
				{
					this._screenTop._isChange = false;
					this._saleGrid1._myGrid1._isChange = false;
				}
			}
			return (result);
		}

		void _searchArCode__searchEnterKeyPress(MyLib._myGrid sender, int row)
		{
			_searchByParent(sender, row);
		}

		void _myGrid1__alterCellUpdate(object sender, int row, int column)
		{
			if (_buttonSave.Enabled == false)
			{
				MyLib._myGlobal._displayWarning(4, null);
			}
		}

		bool _myGrid1__queryForRowRemoveCheck(MyLib._myGrid sender, int row)
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

		bool _myGrid1__queryForInsertCheck(MyLib._myGrid sender, int row)
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

		bool _myGrid1__queryForUpdateCheck(MyLib._myGrid sender, int row)
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

		string _myGrid1__queryForUpdateWhere(MyLib._myGrid sender, int row)
		{
			int __getInt = (int)sender._cellGet(row, sender._rowNumberName);
			return (sender._rowNumberName + "=" + __getInt.ToString());
		}

		void _screenTop__saveKeyDown(object sender)
		{
			save_data();
		}

		void _as_sale_Resize(object sender, EventArgs e)
		{
			if (_myManageData1._dataList._loadViewDataSuccess == false)
			{
				_myManageData1._dataListOpen = true;
				_myManageData1._calcArea();
				_myManageData1._dataList._loadViewData(0);
			}
		}

		void _searchDocGroup__searchEnterKeyPress(MyLib._myGrid sender, int row)
		{
			_searchByParent(sender, row);
		}

		bool _screenTop__checkKeyDownReturn(object sender, Keys keyData)
		{
			if (keyData == Keys.Enter || keyData == Keys.Tab)
			{
				if (sender != null)
				{
					if (sender.GetType() == typeof(MyLib._myTextBox))
					{
						MyLib._myTextBox __getTextBox = (MyLib._myTextBox)sender;
						if (__getTextBox._name.Equals(_g.d.as_asset_sale._remark))
						{
							_saleGridFocus();
							return false;
						}
					}
				}
			}
			return true;
		}

		Boolean _screenTop__checkKeyDown(object sender, Keys keyData)
		{
			if (_buttonSave.Enabled == false)
			{
				MyLib._myGlobal._displayWarning(4, null);
			}
			else
			{
				if (keyData == Keys.PageDown)
				{
					_saleGridFocus();
				}
			}
			return true;
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			Keys keyCode = (Keys)(int)keyData & Keys.KeyCode;
			if ((keyData & Keys.Control) == Keys.Control && (keyCode == Keys.Home))
			{
				this._screenTop._focusFirst();
				return true;
			}
			else
			{
				if ((keyData & Keys.Alt) == Keys.Alt && (keyCode == Keys.D1))
				{
					_saleGridFocus();
					return true;
				}
			}
			if (keyData == Keys.F12)
			{
				save_data();
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		void _saleGridFocus()
		{
			if (this._saleGrid1._myGrid1._selectRow == -1)
			{
				this._saleGrid1._myGrid1._selectRow = 0;
				this._saleGrid1._myGrid1._selectColumn = 0;
			}
			this._saleGrid1._myGrid1._inputCell(this._saleGrid1._myGrid1._selectRow, this._saleGrid1._myGrid1._selectColumn);
		}

		void _myManageData1__newDataClick()
		{
			Control codeControl = _screenTop._getControl(_g.d.as_asset_sale._doc_no);
			codeControl.Enabled = true;
			this._saleGrid1._myGrid1._clear();
			_screenTop._focusFirst();
		}

		void _get_column_number()
		{
			_getColumnDocNo = _myManageData1._dataList._gridData._findColumnByName(_g.d.as_asset_sale._table + "." + _g.d.as_asset_sale._doc_no);
		}

		bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
		{
			try
			{
				ArrayList rowDataArray = (ArrayList)rowData;
				_get_column_number();
				_oldDocNo = rowDataArray[_getColumnDocNo].ToString();
				StringBuilder __myquery = new StringBuilder();
				__myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
				__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _myManageData1._dataList._tableName + whereString));
				__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.as_asset_sale_detail._table + " where " + _g.d.as_asset_sale_detail._doc_no + "=\'" + _oldDocNo + "\'"));
				__myquery.Append("</node>");
				ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
				// Master
				this._screenTop._loadData(((DataSet)_getData[0]).Tables[0]);
				Control _codeControl = _screenTop._getControl(_g.d.as_asset_sale_detail._doc_no);
				_codeControl.Enabled = false;
				this._search(false);
				// Detail
				this._saleGrid1._myGrid1._clear();
				this._saleGrid1._myGrid1._loadFromDataTable(((DataSet)_getData[1]).Tables[0]);
				if (forEdit)
				{
					_screenTop._focusFirst();
				}
                this._saleGrid1._myGrid1._calcTotal(false);
				this._saleGrid1._myGrid1.Invalidate();
				return (true);
			}
			catch (Exception)
			{
			}
			return (false);
		}

		void _dataList__deleteData(ArrayList selectRowOrder)
		{
			StringBuilder __myQuery = new StringBuilder();
			__myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
			for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
			{
				MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];
				// Delete Master
				__myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _myManageData1._dataList._tableName, getData.whereString));
				string getDocNo = this._myManageData1._dataList._gridData._cellGet(getData.row, 1).ToString();
				// Delete Detail
				__myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} where " + _g.d.as_asset_sale_detail._doc_no + "=\'" + getDocNo + "\'"), _g.d.as_asset_sale_detail._table));
			} // for
			__myQuery.Append("</node>");
			string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
			if (result.Length == 0)
			{
                MyLib._myGlobal._displayWarning(1, MyLib._myGlobal._resource("สำเร็จ"));
				_myManageData1._dataList._refreshData();
			}
			else
			{
				MessageBox.Show(result, MyLib._myGlobal._resource("ล้มเหลว"), MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		void _screenTop__textBoxChanged(object sender, string name)
		{
			if (name.CompareTo(_g.d.as_asset_sale._sale_type) == 0 || name.CompareTo(_g.d.as_asset_sale._ar_code) == 0 || name.CompareTo(_g.d.as_asset_sale._doc_group) == 0 || name.CompareTo(_g.d.as_asset_sale._side_code) == 0 || name.CompareTo(_g.d.as_asset_sale._department_code) == 0)
			{
				_searchTextBox = (TextBox)sender;
				_searchName = name;
				_search(true);
			}
		}

		void _screenTop__textBoxSearch(object sender)
		{
			string name = ((MyLib._myTextBox)sender)._name;
			string label_name = ((MyLib._myTextBox)sender)._labelName;
			if (name.CompareTo(_g.d.as_asset_sale._sale_type) == 0)
			{
				MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
				_searchName = name;
				_searchTextBox = getControl.textBox;
				MyLib._myGlobal._startSearchBox(getControl, label_name, _searchSaleType, false);
			}
			if (name.CompareTo(_g.d.as_asset_sale._ar_code) == 0)
			{
				MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
				_searchName = name;
				_searchTextBox = getControl.textBox;
				MyLib._myGlobal._startSearchBox(getControl, label_name, _searchArCode, false);
			}
			if (name.CompareTo(_g.d.as_asset_sale._doc_group) == 0)
			{
				MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
				_searchName = name;
				_searchTextBox = getControl.textBox;
				MyLib._myGlobal._startSearchBox(getControl, label_name, _searchDocGroup, false);
			}
			if (name.CompareTo(_g.d.as_asset_sale._side_code) == 0)
			{
				MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
				_searchName = name;
				_searchTextBox = getControl.textBox;
				MyLib._myGlobal._startSearchBox(getControl, label_name, _searchSideCode, false);
			}
			if (name.CompareTo(_g.d.as_asset_sale._department_code) == 0)
			{
				MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
				_searchName = name;
				_searchTextBox = getControl.textBox;
				MyLib._myGlobal._startSearchBox(getControl, label_name, _searchDepartmentCode, false);
			}
		}

		void textBox_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F2)
			{
				if (_searchSaleType.Visible)
				{
					_searchSaleType.Focus();
					_searchSaleType._firstFocus();
				}
				if (_searchArCode.Visible)
				{
					_searchArCode.Focus();
					_searchArCode._firstFocus();
				}
				if (_searchDocGroup.Visible)
				{
					_searchDocGroup.Focus();
					_searchDocGroup._firstFocus();
				}
				if (_searchSideCode.Visible)
				{
					_searchSideCode.Focus();
					_searchSideCode._firstFocus();
				}
				if (_searchDepartmentCode.Visible)
				{
					_searchDepartmentCode.Focus();
					_searchDepartmentCode._firstFocus();
				}
			}
		}

		void textBox_Leave(object sender, EventArgs e)
		{
			_searchSaleType.Visible = false;
			_searchArCode.Visible = false;
			_searchDocGroup.Visible = false;
			_searchSideCode.Visible = false;
			_searchDepartmentCode.Visible = false;
		}

		void textBox_Enter(object sender, EventArgs e)
		{
			MyLib._myTextBox __getControl = (MyLib._myTextBox)((Control)sender).Parent;
			_screenTop__textBoxSearch(__getControl);
			__getControl.textBox.Focus();
		}

		void _searchDepartmentCode__searchEnterKeyPress(MyLib._myGrid sender, int row)
		{
			_searchByParent(sender, row);
		}

		void _searchSideCode__searchEnterKeyPress(MyLib._myGrid sender, int row)
		{
			_searchByParent(sender, row);
		}
		void _searchByParent(object sender, int row)
		{
			MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
			MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
			_searchAll(__getParent2._name, row);
			SendKeys.Send("{TAB}");
		}
		/// <summary>
		/// กด Mouse ตอนค้นหา หรือ Enter ตอนค้นหา
		/// </summary>
		/// <param name="name"></param>
		/// <param name="row"></param>
		void _searchAll(string name, int row)
		{
			if (name.CompareTo(_g.g._search_master_as_asset_retire) == 0)
			{
				string result = (string)_searchSaleType._dataList._gridData._cellGet(row, 0);
				if (result.Length > 0)
				{
					_searchSaleType.Close();
					_screenTop._setDataStr(_searchName, result, "", true);
					_search(true);
				}
			}
			if (name.CompareTo(_g.g._search_screen_ar) == 0)
			{
				string result = (string)_searchArCode._dataList._gridData._cellGet(row, 0);
				if (result.Length > 0)
				{
					_searchArCode.Close();
					_screenTop._setDataStr(_searchName, result, "", true);
					_search(true);
				}
			}
			if (name.CompareTo(_g.g._search_screen_erp_doc_group) == 0)
			{
				string result = (string)_searchDocGroup._dataList._gridData._cellGet(row, 0);
				if (result.Length > 0)
				{
					_searchDocGroup.Close();
					_screenTop._setDataStr(_searchName, result, "", true);
					_search(true);
				}
			}
			if (name.CompareTo(_g.g._search_screen_erp_side_list) == 0)
			{
				string result = (string)_searchSideCode._dataList._gridData._cellGet(row, 0);
				if (result.Length > 0)
				{
					_searchSideCode.Close();
					_screenTop._setDataStr(_searchName, result, "", true);
					_search(true);
				}
			}
			if (name.CompareTo(_g.g._search_screen_erp_department_list) == 0)
			{
				string result = (string)_searchDepartmentCode._dataList._gridData._cellGet(row, 0);
				if (result.Length > 0)
				{
					_searchDepartmentCode.Close();
					_screenTop._setDataStr(_searchName, result, "", true);
					_search(true);
				}
			}
		}

		void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
		{
			MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
			MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
			_searchAll(__getParent2._name, e._row);
		}

		public void _search(Boolean warning)
		{
			try
			{
				StringBuilder __myquery = new StringBuilder();
				__myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
				__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.as_asset_retire._name_1 + " from " + _g.d.as_asset_retire._table + " where " + _g.d.as_asset_retire._code + "=\'" + _screenTop._getDataStr(_g.d.as_asset_sale._sale_type) + "\'"));
				__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + _screenTop._getDataStr(_g.d.as_asset_sale._ar_code) + "\'"));
				__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_doc_group._name_1 + " from " + _g.d.erp_doc_group._table + " where " + _g.d.erp_doc_group._code + "=\'" + _screenTop._getDataStr(_g.d.as_asset_sale._doc_group) + "\'"));
				__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_side_list._name_1 + " from " + _g.d.erp_side_list._table + " where " + _g.d.erp_side_list._code + "=\'" + _screenTop._getDataStr(_g.d.as_asset_sale._side_code) + "\'"));
				__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._code + "=\'" + _screenTop._getDataStr(_g.d.as_asset_sale._department_code) + "\'"));
				__myquery.Append("</node>");
				ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
				_searchAndWarning(_g.d.as_asset_sale._sale_type, (DataSet)_getData[0], warning);
				_searchAndWarning(_g.d.as_asset_sale._ar_code, (DataSet)_getData[1], warning);
				_searchAndWarning(_g.d.as_asset_sale._doc_group, (DataSet)_getData[2], warning);
				_searchAndWarning(_g.d.as_asset_sale._side_code, (DataSet)_getData[3], warning);
				_searchAndWarning(_g.d.as_asset_sale._department_code, (DataSet)_getData[4], warning);
			}
			catch
			{
			}
		}

		bool _searchAndWarning(string fieldName, DataSet dataResult, Boolean warning)
		{
			bool __result = false;
			if (dataResult.Tables[0].Rows.Count > 0)
			{
				string getData = dataResult.Tables[0].Rows[0][0].ToString();
				string getDataStr = _screenTop._getDataStr(fieldName);
				if (fieldName.Equals(_g.d.as_asset_sale._ar_code) == true)
				{
					_screenTop._setDataStr(fieldName, getDataStr, "", true);
					_screenTop._setDataStr("ar_name", getData);
				}
				else
				{
					_screenTop._setDataStr(fieldName, getDataStr, getData, true);
				}
			}
			if (_searchTextBox != null)
			{
				if (_searchName.Equals(fieldName) && _screenTop._getDataStr(fieldName) != "")
				{
					if (dataResult.Tables[0].Rows.Count == 0 && warning)
					{
						((MyLib._myTextBox)_searchTextBox.Parent).textBox.Text = "";
						((MyLib._myTextBox)_searchTextBox.Parent)._textFirst = "";
						((MyLib._myTextBox)_searchTextBox.Parent)._textSecond = "";
						((MyLib._myTextBox)_searchTextBox.Parent)._textLast = "";
						_searchTextBox.Focus();
						//
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk); __result = true;
					}
				}
			}
			return __result;
		}

		void _myManageData1__closeScreen()
		{
			this.Dispose();
		}

		void save_data()
		{
			// กำหนด Focus ใหม่ ไม่งั้นข้อมูลจะไม่สมบูรณ์
			this._saleGrid1._myGrid1.Focus();
			if (_myManageData1._manageButton.Enabled)
			{
				string getEmtry = _screenTop._checkEmtryField();
				if (getEmtry.Length > 0)
				{
					MyLib._myGlobal._displayWarning(2, getEmtry);
				}
				else
				{
					// Sale Master
					ArrayList getDataTop = _screenTop._createQueryForDatabase();
					// Sale Detail
					string fieldList = _g.d.as_asset_sale_detail._doc_no + "," + _g.d.as_asset_sale_detail._doc_date + ",";
					string dataList = _screenTop._getDataStrQuery(_g.d.as_asset_sale._doc_no) + "," + _screenTop._getDataStrQuery(_g.d.as_asset_sale._doc_date) + ",";
					// Total
					int getColumnTotalValue = this._saleGrid1._myGrid1._findColumnByName(_g.d.as_asset_sale_detail._as_value);
					int getColumnTotalDepreciate = this._saleGrid1._myGrid1._findColumnByName(_g.d.as_asset_sale_detail._depreciate_value);
					int getColumnTotalAfter = this._saleGrid1._myGrid1._findColumnByName(_g.d.as_asset_sale_detail._after_depreciate);
					int getColumnTotalSalePrice = this._saleGrid1._myGrid1._findColumnByName(_g.d.as_asset_sale_detail._sale_price);
					int getColumnTotalVat = this._saleGrid1._myGrid1._findColumnByName(_g.d.as_asset_sale_detail._vat_value);
					int getColumnTotalNet = this._saleGrid1._myGrid1._findColumnByName(_g.d.as_asset_sale_detail._net_value);
					//
					StringBuilder __myQuery = new StringBuilder();
					__myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
					if (_myManageData1._mode == 1)
					{
						string extData = ((MyLib._myGrid._columnType)this._saleGrid1._myGrid1._columnList[getColumnTotalValue])._total.ToString() + "," + ((MyLib._myGrid._columnType)this._saleGrid1._myGrid1._columnList[getColumnTotalDepreciate])._total.ToString() + "," + ((MyLib._myGrid._columnType)this._saleGrid1._myGrid1._columnList[getColumnTotalAfter])._total.ToString() + "," + ((MyLib._myGrid._columnType)this._saleGrid1._myGrid1._columnList[getColumnTotalSalePrice])._total.ToString() + "," + ((MyLib._myGrid._columnType)this._saleGrid1._myGrid1._columnList[getColumnTotalVat])._total.ToString() + "," + ((MyLib._myGrid._columnType)this._saleGrid1._myGrid1._columnList[getColumnTotalNet])._total.ToString();
						string extField = _g.d.as_asset_sale._total_value + "," + _g.d.as_asset_sale._total_depreciate_value + "," + _g.d.as_asset_sale._after_depreciate_value + "," + _g.d.as_asset_sale._total_sale_price + "," + _g.d.as_asset_sale._total_vat_value + "," + _g.d.as_asset_sale._total_net_value;
						__myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _myManageData1._dataList._tableName + " (" + getDataTop[0].ToString() + "," + extField + ") values (" + getDataTop[1].ToString() + "," + extData + ")"));
						__myQuery.Append(this._saleGrid1._myGrid1._createQueryForInsert(_g.d.as_asset_sale_detail._table, fieldList, dataList));
					}
					else
					{
						string extStr = _g.d.as_asset_sale._total_value + "=" + ((MyLib._myGrid._columnType)this._saleGrid1._myGrid1._columnList[getColumnTotalValue])._total.ToString() + "," + _g.d.as_asset_sale._total_depreciate_value + "=" + ((MyLib._myGrid._columnType)this._saleGrid1._myGrid1._columnList[getColumnTotalDepreciate])._total.ToString() + "," + _g.d.as_asset_sale._after_depreciate_value + "=" + ((MyLib._myGrid._columnType)this._saleGrid1._myGrid1._columnList[getColumnTotalAfter])._total.ToString() + "," + _g.d.as_asset_sale._total_sale_price + "=" + ((MyLib._myGrid._columnType)this._saleGrid1._myGrid1._columnList[getColumnTotalSalePrice])._total.ToString() + "," + _g.d.as_asset_sale._total_vat_value + "=" + ((MyLib._myGrid._columnType)this._saleGrid1._myGrid1._columnList[getColumnTotalVat])._total.ToString() + "," + _g.d.as_asset_sale._total_net_value + "=" + ((MyLib._myGrid._columnType)this._saleGrid1._myGrid1._columnList[getColumnTotalNet])._total.ToString();
						__myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _myManageData1._dataList._tableName + " set " + extStr + "," + getDataTop[2].ToString() + _myManageData1._dataList._whereString));
						__myQuery.Append(this._saleGrid1._myGrid1._createQueryRowRemove(_g.d.as_asset_sale_detail._table));
						// อย่าลืม Event _queryForUpdateWhere ไม่งั้นมันไม่ทำงานนะ
						string __fieldUpdate = _g.d.as_asset_sale_detail._doc_no + "=\'" + _oldDocNo + "\'";
						__myQuery.Append(this._saleGrid1._myGrid1._createQueryForUpdate(_g.d.as_asset_sale_detail._table, __fieldUpdate));
						// ต่อท้ายด้วย Insert บรรทัดใหม่
						__myQuery.Append(this._saleGrid1._myGrid1._createQueryForInsert(_g.d.as_asset_sale_detail._table, fieldList, dataList, true));
					}
					//
					__myQuery.Append("</node>");
					string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
					// Show Status
					if (result.Length == 0)
					{
						MyLib._myGlobal._displayWarning(1, null);
						if (_myManageData1._mode == 1)
						{
							_myManageData1._afterInsertData();
							string __getOldDocNo = this._screenTop._getDataStr(_g.d.as_asset_sale._doc_no);
							string __getOldBillNo = this._screenTop._getDataStr(_g.d.as_asset_sale._bill_no);
							if (this._clearDataAfterSaveButton._iconNumber == 0)
							{
								this._screenTop._clear();
								this._saleGrid1._myGrid1._clear();
							}
							if (this._autoRunningButton._iconNumber == 0)
							{
								this._screenTop._setDataStr(_g.d.as_asset_sale._doc_no, MyLib._myGlobal._autoRunningNumberStyleRightToLeft(__getOldDocNo));
								this._screenTop._setDataStr(_g.d.as_asset_sale._bill_no, MyLib._myGlobal._autoRunningNumberStyleRightToLeft(__getOldBillNo));
							}
							this._screenTop._focusFirst();
						}
						else
						{
							_myManageData1._afterUpdateData();
						}
						this._screenTop._isChange = false;
						this._saleGrid1._myGrid1._isChange = false;
					}
					else
					{
						MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		private void _buttonSave_Click(object sender, EventArgs e)
		{
			save_data();
		}

		private void _as_sale_Load(object sender, EventArgs e)
		{
		}

		private void _clearDataAfterSaveButton_Click(object sender, EventArgs e)
		{
			this._clearDataAfterSaveButton._iconNumber = (this._clearDataAfterSaveButton._iconNumber == 0) ? 1 : 0;
			this._clearDataAfterSaveButton.Image = imageList1.Images[this._clearDataAfterSaveButton._iconNumber];
			this._clearDataAfterSaveButton.Invalidate();
		}

		private void _autoRunningButton_Click(object sender, EventArgs e)
		{
			this._autoRunningButton._iconNumber = (this._autoRunningButton._iconNumber == 0) ? 1 : 0;
			this._autoRunningButton.Image = imageList1.Images[this._autoRunningButton._iconNumber];
			this._autoRunningButton.Invalidate();
		}

	}
}
