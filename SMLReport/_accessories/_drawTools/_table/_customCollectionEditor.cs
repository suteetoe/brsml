using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Design;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Drawing;

namespace SMLReport._design
{
    /// <summary>
    /// Default Editor TableColumns
    /// </summary>
    internal class _customColumnCollectionEditor : UITypeEditor
    {
        private CollectionEditor _editor = new CollectionEditor(typeof(_tableColumnsCollection));
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (value != null)
            {
                var editorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
                var editorServiceType = editorService.GetType();
                var ownerGridField = editorServiceType.GetField("ownerGrid", BindingFlags.Instance | BindingFlags.NonPublic);
                var propertyGrid = ownerGridField.GetValue(editorService) as PropertyGrid;

                // set default value
                if (propertyGrid.SelectedObject.GetType() == typeof(_drawTable))
                {
                    _drawTable __activeTool = propertyGrid.SelectedObject as _drawTable;
                    string[] __standardValue = __activeTool._getFieldStandardValue(__activeTool);

                    if (value.GetType() == typeof(_tableColumnsCollection))
                    {
                        for (int __i = 0; __i < ((_tableColumnsCollection)value).Count; __i++)
                        {
                            ((_tableColumnsCollection)value)[__i].__defaultValueTmp = __standardValue;
                        }
                    }

                }

                value = _editor.EditValue(context, provider, value);
                _tableColumnsCollection __list = (_tableColumnsCollection)value;

                // refresh propertyGrid and drawPanel

                if (propertyGrid.SelectedObject.GetType() == typeof(_drawTable))
                {
                    _drawTable __activeTool = propertyGrid.SelectedObject as _drawTable;
                    if (__activeTool._activedrawPanel != null)
                    {
                        __activeTool._activedrawPanel.Invalidate();
                    }
                }

                propertyGrid.Refresh();

                return __list.Clone();
            }
            return base.EditValue(context, provider, value);
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return _editor.GetEditStyle(context);
        }
    }

    internal class _customFooterCollectionEditor : UITypeEditor
    {
        private CollectionEditor _editor = new CollectionEditor(typeof(_tableFootersCollection));
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (value != null)
            {
                var editorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
                var editorServiceType = editorService.GetType();
                var ownerGridField = editorServiceType.GetField("ownerGrid", BindingFlags.Instance | BindingFlags.NonPublic);
                var propertyGrid = ownerGridField.GetValue(editorService) as PropertyGrid;

                // set default value
                if (propertyGrid.SelectedObject.GetType() == typeof(_drawTable))
                {
                    _drawTable __activeTool = propertyGrid.SelectedObject as _drawTable;
                    string[] __standardValue = __activeTool._getFieldStandardValue(__activeTool);

                    if (value.GetType() == typeof(_tableFootersCollection))
                    {
                        for (int __i = 0; __i < ((_tableFootersCollection)value).Count; __i++)
                        {
                            ((_tableFootersCollection)value)[__i].__defaultValueTmp = __standardValue;
                        }
                    }

                }

                value = _editor.EditValue(context, provider, value);
                _tableFootersCollection __list = (_tableFootersCollection)value;

                // refresh propertyGrid and drawPanel

                if (propertyGrid.SelectedObject.GetType() == typeof(_drawTable))
                {
                    _drawTable __activeTool = propertyGrid.SelectedObject as _drawTable;
                    if (__activeTool._activedrawPanel != null)
                    {
                        __activeTool._activedrawPanel.Invalidate();
                    }
                }

                propertyGrid.Refresh();

                return __list.Clone();
            }
            return base.EditValue(context, provider, value);
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return _editor.GetEditStyle(context);
        }
    }

    /// <summary>
    /// Editor For Multifield TableColumn
    /// </summary>
    internal class _columnMultiFieldEditor : CollectionEditor
    {
        private ComboBox __drawFieldCombo = new ComboBox();

        public _columnMultiFieldEditor(Type type)
            : base(type)
        {

        }

        protected override object[] GetItems(object editValue)
        {
            if (editValue.GetType() == typeof(_columnMultiFieldCollection))
            {
                _tableColumns __tmpColumn = (_tableColumns)this.Context.Instance;
                _columnMultiFieldCollection __collection = (_columnMultiFieldCollection) editValue;

                for (int __i = 0; __i < __collection.Count; __i++)
                {
                    ((_drawObject)__collection[__i])._defaultField = __tmpColumn.__defaultValueTmp;
                    if (__collection[__i].GetType() == typeof(_drawImageField))
                    {
                        ((_drawImageField)__collection[__i])._nameImageList = __tmpColumn._nameImageListResultTmp;
                    }
                }
            }

            return base.GetItems(editValue);
        }

        protected override object SetItems(object editValue, object[] value)
        {

            if (this.Context.Instance.GetType() == typeof(_tableColumns))
            {
                _tableColumns __tmpColumn = (_tableColumns)this.Context.Instance;

                for (int __i = 0; __i < value.Length; __i++)
                {
                    ((_drawObject)value[__i])._defaultField = __tmpColumn.__defaultValueTmp;
                    if (value[__i].GetType() == typeof(_drawImageField))
                    {
                        ((_drawImageField)value[__i])._nameImageList = __tmpColumn._nameImageListResultTmp;
                    }
                }
            }
            return base.SetItems(editValue, value);
        }
        /// <summary>
        /// get Collection Form Can Add Custom Object Here!
        /// </summary>
        /// <returns></returns>
        protected override CollectionEditor.CollectionForm CreateCollectionForm()
        {
            __drawFieldCombo = new ComboBox();
            __drawFieldCombo.Name = "comboDrawType";
            __drawFieldCombo.Items.Add(_drawToolType.Label.ToString());
            __drawFieldCombo.Items.Add(_drawToolType.TextField.ToString());
            __drawFieldCombo.Items.Add(_drawToolType.ImageField.ToString());
            __drawFieldCombo.Items.Add(_drawToolType.Line.ToString());
            __drawFieldCombo.Items.Add(_drawToolType.Rectangle.ToString());
            __drawFieldCombo.SelectedIndex = 0;

            CollectionForm __collectionForm = base.CreateCollectionForm();

            Form frmCollectionEditorForm = (Form)__collectionForm;

            Control __tmpControl = frmCollectionEditorForm.Controls[0];
            if (__tmpControl.GetType() == typeof(TableLayoutPanel))
            {
                TableLayoutPanel __mainLayout = (TableLayoutPanel)frmCollectionEditorForm.Controls[0];


                __mainLayout.SetColumnSpan(__tmpControl.Controls[6], 1);
                __mainLayout.SetColumn(__tmpControl.Controls[6], 2);

                __drawFieldCombo.Location = new Point(0, __mainLayout.ClientRectangle.Bottom - 35);
                __drawFieldCombo.Anchor = AnchorStyles.Left;

                __mainLayout.Controls.Add(__drawFieldCombo);
                __mainLayout.SetRow(__drawFieldCombo, 4);
                __mainLayout.SetColumn(__drawFieldCombo, 0);

            }

            // set Caption Of Form
            frmCollectionEditorForm.Text = "MultiField Collection Editor";

            return (CollectionEditor.CollectionForm)frmCollectionEditorForm;
        }

        /// <summary>
        /// On Click Add Object
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        protected override object CreateInstance(Type itemType)
        {
            switch (__drawFieldCombo.SelectedIndex)
            {
                case 0:
                    return base.CreateInstance(typeof(_drawLabel));
                case 1:
                    return base.CreateInstance(typeof(_drawTextField));
                case 2:
                    return base.CreateInstance(typeof(_drawImageField));
                case 3:
                    return base.CreateInstance(typeof(_drawLine));
                case 4:
                    return base.CreateInstance(typeof(_drawRectangle));
            }

            return base.CreateInstance(itemType);
        }

    }
}
