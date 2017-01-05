using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace TestPrinter
{
    public partial class Form1 : Form
    {
        string _printerIP = "192.168.2.168";
        public Form1()
        {
            InitializeComponent();
            this._textBox.Text = "TEST ทดสอบ ภาษาไทย กุ้ง ข้าวเหนียว สู้ชีวิต TEST";
        }

        private void _printButton_Click(object sender, EventArgs e)
        {
            Socket __clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            __clientSock.NoDelay = true;
            IPAddress __ip = IPAddress.Parse(_printerIP);
            IPEndPoint __remoteEP = new IPEndPoint(__ip, 9100);
            __clientSock.Connect(__remoteEP);
            Encoding enc = Encoding.UTF8;
            __clientSock.Send(new byte[] { 27, 64 });
            for (int __loop = 0; __loop < 1; __loop++)
            {
                // init
                __clientSock.Send(new byte[] { 27, (byte)'2' });
                __clientSock.Send(new byte[] { 27, (byte)'3', 10 });
                __clientSock.Send(System.Text.Encoding.Default.GetBytes(__loop.ToString() + ":" + this._textBox.Text.ToString()));
                __clientSock.Send(new byte[] { 27, (byte)'K', 200 });
                //__clientSock.Send(new byte[] { 10 });
                __clientSock.Send(new byte[] { 27, (byte)'!', 32 });
                __clientSock.Send(System.Text.Encoding.Default.GetBytes(this._textBox.Text.ToString()));
                __clientSock.Send(new byte[] { 10 });
                __clientSock.Send(new byte[] { 27, 50 });
                __clientSock.Send(new byte[] { 27, (byte)'!', (byte)(32 | 16) });
                __clientSock.Send(System.Text.Encoding.Default.GetBytes(this._textBox.Text.ToString()));
                __clientSock.Send(new byte[] { 10 });
                __clientSock.Send(new byte[] { 27, 33, 4 });
                __clientSock.Send(new byte[] { 27, (byte)'!', 0 });
                __clientSock.Send(new byte[] { 27, (byte)'M', 1 });
                __clientSock.Send(System.Text.Encoding.Default.GetBytes(this._textBox.Text.ToString()));
                __clientSock.Send(new byte[] { 10 });
                __clientSock.Send(new byte[] { 27, (byte)'M', 1 });
                __clientSock.Send(System.Text.Encoding.Default.GetBytes(this._textBox.Text.ToString()));
                __clientSock.Send(new byte[] { 10 });
                __clientSock.Send(new byte[] { 27, (byte)'M', 1 });
                __clientSock.Send(System.Text.Encoding.Default.GetBytes(this._textBox.Text.ToString()));
                __clientSock.Send(new byte[] { 10 });
                __clientSock.Send(new byte[] { 27, (byte)'M', 1 });
                __clientSock.Send(System.Text.Encoding.Default.GetBytes(this._textBox.Text.ToString()));
                __clientSock.Send(new byte[] { 10 });
            }
            // Sends an ESC/POS command to the printer to cut the paper
            string __output = Convert.ToChar(29) + "V" + Convert.ToChar(65) + Convert.ToChar(0);
            char[] __array = __output.ToCharArray();
            byte[] __byData = enc.GetBytes(__array);
            __clientSock.Send(__byData);
            __clientSock.Disconnect(false);
            __clientSock.Close();
            this.Dispose();
        }
    }
}
