using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace MyLib
{
    public class _printRaw
    {
        private Bitmap _bmp;
        private float _heightTemp = 0;
        private float _widthTemp = 0;
        private Font _lastFont = null;
        public Graphics _g;

        public _printRaw(float width)
        {
            if (width != -1)
            {
                this._width = (width == 0) ? 575 : width;

                //try
                //{
                this._bmp = new Bitmap(this._getImageWidth, (int)200000);
                //}     
           
                this._bmp.SetResolution(200, 200);
                this._g = Graphics.FromImage(this._bmp);
                //this._g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
                this._g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                this._g.FillRegion(new SolidBrush(Color.White), new Region(new Rectangle(0, 0, (int)this._width, 200000)));
            }
        }

        public _printRaw(float width, float height)
        {
            if (width != -1)
            {
                this._width = (width == 0) ? 575 : width;
                
                //try
                //{
                this._bmp = new Bitmap((int)this._width, (int)height);
                //}     

                this._bmp.SetResolution(200, 200);
                this._g = Graphics.FromImage(this._bmp);
                //this._g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
                this._g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                this._g.FillRegion(new SolidBrush(Color.White), new Region(new Rectangle(0, 0, (int)this._width, 200000)));
            }
        }

        public void _drawString(string str, float x, float y)
        {
            if (this._lastFont == null)
            {
                this._lastFont = new Font("Tahoma", 18);
            }
            this._drawString(str, this._lastFont, x, y);
        }

        public void _drawString(string str, Font font, float x, float y)
        {
            this._lastFont = font;
            Font __font = new Font(this._lastFont.FontFamily, this._lastFont.Size * 2, this._lastFont.Style);
            this._g.DrawString(str, __font, new SolidBrush(Color.Black), x, y);
            if (y + __font.Height > this._height)
            {
                this._height = y + __font.Height;
            }
        }

        public int _getImageWidth
        {
            get
            {
                return MyLib._myGlobal._intPhase(this._width.ToString());
            }
        }

        public float _width
        {
            set
            {
                this._widthTemp = value;
            }
            get
            {
                return this._widthTemp;
            }
        }

        public float _height
        {
            set
            {
                this._heightTemp = value;
            }
            get
            {
                return this._heightTemp;
            }
        }

        private class _bitmapData
        {
            public BitArray _dots
            {
                get;
                set;
            }

            public int _height
            {
                get;
                set;
            }

            public int _width
            {
                get;
                set;
            }
        }

        private _bitmapData _getBitmapData()
        {
            using (var __bitmap = (Bitmap)this._bitmap)
            {
                //var __threshold = 254;
                var __threshold = 127;
                var __index = 0;
                var __dimensions = __bitmap.Width * __bitmap.Height;
                var __dots = new BitArray(__dimensions);

                for (var __y = 0; __y < this._height; __y++)
                {
                    for (var __x = 0; __x < __bitmap.Width; __x++)
                    {
                        var __color = __bitmap.GetPixel(__x, __y);
                        var __luminance = (int)(__color.R * 0.3 + __color.G * 0.59 + __color.B * 0.11);
                        __dots[__index] = (__luminance < __threshold);
                        __index++;
                    }
                }

                return new _bitmapData()
                {
                    _dots = __dots,
                    _height = __bitmap.Height,
                    _width = __bitmap.Width
                };
            }
        }

        public byte[] _getDataBytes()
        {
            using (var __ms = new MemoryStream())
            using (var __bw = new BinaryWriter(__ms))
            {
                _render(__bw);
                __bw.Flush();
                return __ms.ToArray();
            }
        }

        private void _render(BinaryWriter bw)
        {
            var __data = _getBitmapData();
            var __dots = __data._dots;
            var __width = BitConverter.GetBytes(__data._width);

            bw.Write((byte)27);
            bw.Write('3');
            bw.Write((byte)24);
            int __offset = 0;

            while (__offset < this._height)
            {
                bw.Write((byte)27);
                bw.Write('*');         // bit-image mode
                bw.Write((byte)33);    // 24-dot double-density
                bw.Write(__width[0]);  // width low byte
                bw.Write(__width[1]);  // width high byte

                for (int __x = 0; __x < __data._width; ++__x)
                {
                    for (int _k = 0; _k < 3; ++_k)
                    {
                        byte __slice = 0;

                        for (int __b = 0; __b < 8; ++__b)
                        {
                            int __y = (((__offset / 8) + _k) * 8) + __b;

                            // Calculate the location of the pixel we want in the bit array.
                            // It'll be at (y * width) + x.
                            int __i = (__y * __data._width) + __x;

                            // If the image is shorter than 24 dots, pad with zero.
                            bool __v = false;
                            if (__i < __dots.Length)
                            {
                                __v = __dots[__i];
                            }

                            __slice |= (byte)((__v ? 1 : 0) << (7 - __b));
                        }

                        bw.Write(__slice);
                    }
                }

                __offset += 24;
                bw.Write((byte)10);
            }

            // Restore the line spacing to the default of 30 dots.
            bw.Write((byte)27);
            bw.Write('3');
            bw.Write((byte)30);
        }

        public void _print(string printerName)
        {
            this._print(printerName, this._getDataBytes());
        }

        public void _print(string printerName, byte[] document)
        {
            _nativeMethods.DOC_INFO_1 __documentInfo;
            IntPtr __printerHandle;

            __documentInfo = new _nativeMethods.DOC_INFO_1();
            __documentInfo.pDataType = "RAW";
            __documentInfo.pDocName = "Bit Image Test";

            __printerHandle = new IntPtr(0);

            if (_nativeMethods.OpenPrinter(printerName.Normalize(), out __printerHandle, IntPtr.Zero))
            {
                if (_nativeMethods.StartDocPrinter(__printerHandle, 1, __documentInfo))
                {
                    int __bytesWritten;
                    byte[] __managedData;
                    IntPtr __unmanagedData;

                    __managedData = document;
                    __unmanagedData = Marshal.AllocCoTaskMem(__managedData.Length);
                    Marshal.Copy(__managedData, 0, __unmanagedData, __managedData.Length);

                    if (_nativeMethods.StartPagePrinter(__printerHandle))
                    {
                        _nativeMethods.WritePrinter(
                            __printerHandle,
                            __unmanagedData,
                            __managedData.Length,
                            out __bytesWritten);
                        _nativeMethods.EndPagePrinter(__printerHandle);
                    }
                    else
                    {
                        throw new Win32Exception();
                    }

                    Marshal.FreeCoTaskMem(__unmanagedData);
                    _nativeMethods.EndDocPrinter(__printerHandle);
                }
                else
                {
                    throw new Win32Exception();
                }

                _nativeMethods.ClosePrinter(__printerHandle);
            }
            else
            {
                throw new Win32Exception();
            }
        }

        public void _dispose()
        {
            this._bmp.Dispose();

        }

        public Bitmap _bitmap
        {
            get
            {
                return this._bmp;
            }
        }
    }
}
