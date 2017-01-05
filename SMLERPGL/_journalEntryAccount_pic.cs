using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGL
{
	public partial class _journalEntryAccount_pic : UserControl
	{
		string _oldDocNo = "";
		string _oldBookCode = "";
		DateTime _oldDate = new DateTime(1000, 1, 1);
		int _getColumnDocDate = 0;
		int _getColumnBookCode = 0;
		int _getColumnDocNo = 0;

		public _journalEntryAccount_pic()
		{
			InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            _myManageData1._displayMode = 0;
			_myManageData1._dataList._fullMode = false;
			_myManageData1._dataList._lockRecord = true;
			_myManageData1._selectDisplayMode(this._myManageData1._displayMode);
			_myManageData1._dataList._loadViewFormat("screen_gl_journal", MyLib._myGlobal._userSearchScreenGroup, true);
			_myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);

			_myManageData1._manageButton = this._myToolBar;
			_myManageData1._manageBackgroundPanel = this._myPanel1;
			_myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
			_myManageData1._dataList._referFieldAdd(_g.d.gl_journal._doc_date, 2);
			_myManageData1._dataList._referFieldAdd(_g.d.gl_journal._doc_no, 1);
			_myManageData1._dataList._referFieldAdd(_g.d.gl_journal._book_code, 1);
			_myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
			this.Resize += new EventHandler(_journalEntryAccount_pic_Resize);
			_getPicture1._setEnable(false);
			//
			this._screenTop._saveKeyDown += new MyLib.SaveKeyDownHandler(_screenTop__saveKeyDown);
			this._screenTop._checkKeyDown += new MyLib.CheckKeyDownHandler(_screenTop__checkKeyDown);
		}

		void _journalEntryAccount_pic_Resize(object sender, EventArgs e)
		{
			if (_myManageData1._dataList._loadViewDataSuccess == false)
			{
				_myManageData1._dataListOpen = true;
				_myManageData1._calcArea();
				_myManageData1._dataList._loadViewData(0);
			}
		}

		void _myManageData1__closeScreen()
		{
			this.Dispose();
		}



		void _screenTop__saveKeyDown(object sender)
		{
			_save_data();
		}

		void _save_data()
		{
			// กำหนด Focus ใหม่ ไม่งั้นข้อมูลจะไม่สมบูรณ์
			string __result = "";
			if (_myManageData1._manageButton.Enabled)
			{
				string __getEmtry = this._screenTop._checkEmtryField();
				if (__getEmtry.Length > 0)
				{
					MyLib._myGlobal._displayWarning(2, __getEmtry);
				}
				else
				{
					string _codepic = this._screenTop._getDataStr(_g.d.gl_journal._doc_date) + this._screenTop._getDataStr(_g.d.gl_journal._doc_no) + this._screenTop._getDataStr(_g.d.gl_journal._book_code);
					string _codepic_ = _codepic.Replace("/", "").Trim();
					if (_myManageData1._mode == 1)
					{

					}
					else
					{
						__result = this._getPicture1._updateImage(_codepic_);
					}

					if (__result.Length == 0)
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
						MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			Keys keyCode = (Keys)(int)keyData & Keys.KeyCode;

			if (keyData == Keys.F12)
			{
				_save_data();
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		Boolean _screenTop__checkKeyDown(object sender, Keys keyData)
		{
			if (_myManageData1._manageButton.Enabled == false)
			{
				MyLib._myGlobal._displayWarning(4, null);
			}
			return true;
		}
		void _myManageData1__clearData()
		{
			this._screenTop._clear();

		}
		void _get_column_number()
		{
			_getColumnDocDate = _myManageData1._dataList._gridData._findColumnByName(_g.d.gl_journal._table + "." + _g.d.gl_journal._doc_date);
			_getColumnBookCode = _myManageData1._dataList._gridData._findColumnByName(_g.d.gl_journal._table + "." + _g.d.gl_journal._book_code);
			_getColumnDocNo = _myManageData1._dataList._gridData._findColumnByName(_g.d.gl_journal._table + "." + _g.d.gl_journal._doc_no);
		}

		bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
		{
			try
			{
				ArrayList __rowDataArray = (ArrayList)rowData;
				_get_column_number();
				// get old
				_oldBookCode = __rowDataArray[_getColumnBookCode].ToString();
				_oldDate = (DateTime)__rowDataArray[_getColumnDocDate];
				_oldDocNo = __rowDataArray[_getColumnDocNo].ToString();
				//
				MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
				StringBuilder __myquery = new StringBuilder();
				__myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
				__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.gl_journal._table + whereString));
				// __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal._doc_date + "=\'" + MyLib._myGlobal._convertDateToQuery(_oldDate) + "\' and " + _g.d.gl_journal._doc_no + "=\'" + _oldDocNo + "\' and " + _g.d.gl_journal._book_code + "=\'" + _oldBookCode + "\'"));
				__myquery.Append("</node>");
				ArrayList _getData = myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
				this._screenTop._loadData(((DataSet)_getData[0]).Tables[0]);
				// this._glDetail1._glDetailGrid._loadFromDataTable(((DataSet)_getData[1]).Tables[0]);
				this._screenTop._search(false);
				this._getPicture1._clearpic();
				string _codepic = this._screenTop._getDataStr(_g.d.gl_journal._doc_date) + this._screenTop._getDataStr(_g.d.gl_journal._doc_no) + this._screenTop._getDataStr(_g.d.gl_journal._book_code);
				string _codepic_ = _codepic.Replace("/", "").Trim();
				this._getPicture1._loadImage(_codepic_);

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

			}
			catch (Exception)
			{
			}
			return (true);
		}

		private void _buttonSave_Click(object sender, EventArgs e)
		{
			_save_data();
		}

		private void toolStripMyButton1_Click(object sender, EventArgs e)
		{
			this.Dispose();
		}


	}
}
