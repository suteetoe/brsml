using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;

namespace MyLib
{
    public sealed class _compress
    {
        #region Compressing.
        // ------------------------------------------------------------------
        /// <summary>
        /// Compress a string with the ZIP algorithm.
        /// Use the DecompressString() routine to decompress the compressed bytes.
        /// </summary>
        /// <param name="input">The string to compress.</param>
        /// <returns>Returns the compressed string.</returns>

        public static byte[] _compressString(string input)
        {
            byte[] inputByteArray = Encoding.UTF8.GetBytes(input);
            MemoryStream ms = new MemoryStream();

            ZipOutputStream zipOut = new ZipOutputStream(ms);
            ZipEntry ZipEntry = new ZipEntry("0");
            zipOut.PutNextEntry(ZipEntry);
            zipOut.SetLevel(9);
            zipOut.Write(inputByteArray, 0, inputByteArray.Length);
            zipOut.Finish();
            zipOut.Close();
            return ms.ToArray();
        }

        // ------------------------------------------------------------------
        #endregion

        #region Decompressing.
        // ------------------------------------------------------------------

        /// <summary>
        /// Decompress a byte stream that was formerly compressed
        /// with the CompressBytes() routine with the ZIP algorithm.
        /// </summary>
        /// <param name="input">The buffer that contains the compressed
        /// stream with the bytes.</param>
        /// <returns>Returns the decompressed bytes.</returns>
        public static byte[] _decompressBytes(byte[] input)
        {
            using (MemoryStream mem = new MemoryStream(input))
            using (ZipInputStream stm = new ZipInputStream(mem))
            using (MemoryStream mem2 = new MemoryStream())
            {
                //เอ้ เพิ่ม try ป้องกัน Err  แล้ว ไม่ทำงานต่อ
                try
                {
                    ZipEntry entry = stm.GetNextEntry();
                    if (entry != null)
                    {
                        byte[] data = new byte[4096];

                        while (true)
                        {
                            int size = stm.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                mem2.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                catch (Exception __ex)
                {
                    String g = __ex.Message.ToString();
                }

                using (BinaryReader r = new BinaryReader(mem2))
                {
                    byte[] c = new byte[mem2.Length];
                    mem2.Seek(0, SeekOrigin.Begin);
                    r.Read(c, 0, (int)mem2.Length);

                    return c;
                }
            }
        }


        /// <summary>
        /// Decompress a byte stream of a string that was formerly 
        /// compressed with the CompressString() routine with the ZIP algorithm.
        /// </summary>
        /// <param name="input">The buffer that contains the compressed
        /// stream with the string.</param>
        /// <returns>Returns the decompressed string.</returns>
        public static string _deCompressString(byte[] input)
        {
            return Encoding.UTF8.GetString(_decompressBytes(input));
        }

        // ------------------------------------------------------------------
        #endregion

    }
}
