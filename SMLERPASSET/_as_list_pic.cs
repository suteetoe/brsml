using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPASSET
{
	public partial class _as_list_pic : UserControl
	{
		MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
		// Old Code For Update
		//int _getColumnAsCode = 0;
		//string _oldAsCode = "";
		public _as_list_pic()
		{
			InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //**********************
			this._screenTop.AutoSize = true;
			this._screenTop._maxColumn = 2;
			this._screenTop._table_name = _g.d.as_asset._table;
			this._screenTop._addTextBox(0, 0, 1, 0, _g.d.as_asset._code, 1, 1, 0, true, false, false);
			this._screenTop._addTextBox(1, 0, 1, 0, _g.d.as_asset._name_1, 2, 100, 0, true, false, false);
			this._screenTop._addTextBox(2, 0, 1, 0, _g.d.as_asset._name_2, 2, 100, 0, true, false, true);
			this._screenTop._addTextBox(3, 0, 1, 0, _g.d.as_asset._name_eng_1, 2, 100, 0, true, false, false);
			this._screenTop._addTextBox(4, 0, 1, 0, _g.d.as_asset._name_eng_2, 2, 100, 0, true, false, true);
			this._screenTop._checkKeyDown += new MyLib.CheckKeyDownHandler(_screenTop__checkKeyDown);
			this._screenTop._saveKeyDown += new MyLib.SaveKeyDownHandler(_screenTop__saveKeyDown);
			this._screenTop._refresh();
			this._screenTop.Enabled = false;
			_myManageData1._displayMode = 0;
			_myManageData1._dataList._fullMode = false;
			_myManageData1._selectDisplayMode(_myManageData1._displayMode);
			_myManageData1._dataList._lockRecord = true;
			_myManageData1._dataList._loadViewFormat("screen_as_asset", MyLib._myGlobal._userSearchScreenGroup, true);
			_myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
			_myManageData1._manageButton = this._myToolBar;
			_myManageData1._manageBackgroundPanel = this._myPanel1;
			_myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
			_myManageData1._autoSize = true;
			_myManageData1._autoSizeHeight = 500;
			_myManageData1._dataList._referFieldAdd(_g.d.as_asset._code, 1);
			_getPicture1._setEnable(false);
			// Resize
			this.Resize += new EventHandler(_as_list_pic_Resize);
		}

		void _as_list_pic_Resize(object sender, EventArgs e)
		{
			if (_myManageData1._dataList._loadViewDataSuccess == false)
			{
				_myManageData1._dataListOpen = true;
				_myManageData1._calcArea();
				_myManageData1._dataList._loadViewData(0);
			}
		}

		void _screenTop__saveKeyDown(object sender)
		{
			this.save_data();
		}

		Boolean _screenTop__checkKeyDown(object sender, Keys keyData)
		{
			if (_myManageData1._manageButton.Enabled == false)
			{
				MyLib._myGlobal._displayWarning(4, null);
			}
			return true;
		}
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			Keys keyCode = (Keys)(int)keyData & Keys.KeyCode;
			if ((keyData & Keys.Control) == Keys.Control)
			{
				switch (keyCode)
				{
					case Keys.Home:
						this._screenTop._focusFirst();
						return true;
					case Keys.End:
						return true;
				}
			}
			if (keyData == Keys.F12)
			{
				this.save_data();
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void save_data()
		{
			string result = "";
			string getEmtry = this._screenTop._checkEmtryField();
			if (getEmtry.Length > 0)
			{
				MyLib._myGlobal._displayWarning(2, getEmtry);
			}
			else
			{
				ArrayList __getData = _screenTop._createQueryForDatabase();
				StringBuilder __myQuery = new StringBuilder();
				__myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
				if (_myManageData1._mode == 1)
				{
					__myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _myManageData1._dataList._tableName + " (" + __getData[0].ToString() + ") values (" + __getData[1].ToString() + ")"));
				}
				else
				{
					result = this._getPicture1._updateImage(_screenTop._getDataStr(_g.d.as_asset._code));
				}
				__myQuery.Append("</node>");
				if (result.Length == 0)
				{
					MyLib._myGlobal._displayWarning(1, null);
					_screenTop._isChange = false;
					if (_myManageData1._mode == 1)
					{
						_myManageData1._afterInsertData();
					}
					else
					{
						_myManageData1._afterUpdateData();
					}
					_screenTop._clear();
					_screenTop._focusFirst();
					_getPicture1._clearpic();
					_getPicture1._setEnable(false);
				}
				else
				{
					MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
		{
			try
			{
				MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
				DataSet getData = _myFrameWork._query(MyLib._myGlobal._databaseName, "select * from " + _myManageData1._dataList._tableName + whereString);
				this._screenTop._loadData(getData.Tables[0]);
				this._getPicture1._clearpic();
				this._getPicture1._loadImage(this._screenTop._getDataStr(_g.d.as_asset._code));
				if (_myToolBar.Enabled == false)
				{
					this._getPicture1._setEnable(_myToolBar.Enabled);
				}
				else
				{
					this._getPicture1._setEnable(_myToolBar.Enabled);
				}
				if (forEdit)
				{
					this._screenTop._focusFirst();
				}
				return (true);
			}
			catch
			{
			}
			return (false);
		}

		void _myManageData1__closeScreen()
		{
			this.Dispose();
		}

		private void _buttonSave_Click(object sender, EventArgs e)
		{
			this.save_data();
		}

		private void toolStripMyButton1_Click(object sender, EventArgs e)
		{
			this.Dispose();
		}
	}
}
