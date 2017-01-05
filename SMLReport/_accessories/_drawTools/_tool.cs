using System;
using System.Windows.Forms;
using System.Drawing;

namespace SMLReport._design
{
    /// <summary>
    /// Base class for all drawing tools
    /// </summary>
    public enum _drawToolType
    {
        Pointer,
        Rectangle,
        Ellipse,
        Line,
        Label,
        TextField,
        ImageField,
        Picture,
        RoundedRectangle,
        Table,
        SpecialLabel,
        SpecialImageField,
        NumberOfDrawTools
    };

    
    public enum _FieldType
    {
        String,
        Number,
        DateTime,
        Image,
        Barcode,
        Auto
    }

    public enum _barcodeType
    {
        BarCode_EAN13,
        BarCode_EAN8,
        BarCode_2Of5,
        BarCode_Code39,
        BarCode_Code128,
        BarCode_UPCA,
        BarCode_QRCode
    }

    public enum _barcodeLabelPosition
    {
        TopLeft,
        TopCenter, 
        TopRight, 
        BottomLeft, 
        BottomCenter, 
        BottomRight
    }

    public enum _fieldOperation
    {
        None,
        Count,
        Average,
        Sum,
        Max,
        Min,
        CurrentRow,
        Group
    }

    public enum _columnDisplayTypeEnum
    {
        DetailColumn,
        GroupColumn,
        SumColumn,
        GroupDetailColumn
    }

    public enum overFlowType
    {
        NewLine,
        Hidden        
    }

    // see @ http://www.csharp-examples.net/string-format-datetime/
    //public enum FormatNumberValue
    //{

    //}

    public abstract class _tool 
    {

        /// <summary>
        /// Left nous button is pressed
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public virtual void OnMouseDown(_drawPanel drawArea, MouseEventArgs e)
        {
        }


        /// <summary>
        /// Mouse is moved, left mouse button is pressed or none button is pressed
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public virtual void OnMouseMove(_drawPanel drawArea, MouseEventArgs e)
        {
        }


        /// <summary>
        /// Left mouse button is released
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public virtual void OnMouseUp(_drawPanel drawArea, MouseEventArgs e)
        {
        }
    }
}
