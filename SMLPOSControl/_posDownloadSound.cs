using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using ICSharpCode.SharpZipLib.Zip;

namespace SMLPOSControl
{
    public partial class _posDownloadSound : Form
    {
        bool _downloadProcess = false;
        
        _posSoundDownload __dw = null;

        public _posDownloadSound()
        {
            InitializeComponent();

            _enableButton(true, false);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_downloadProcess)
            {
                if (MessageBox.Show("กำลังดาวน์โหลด เสียงจาก Server คุณต้องการที่ะยกเลิกแล้วจบการทำงานหรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        __dw._cancelDownload();
                    }
                    catch
                    {
                    }
                    e.Cancel = true;
                }
                e.Cancel = false;
            }
        }

        private void _startButton_Click(object sender, EventArgs e)
        {
            _downloadProcess = true;
            try
            {
                //string __path = Path.GetTempPath() + "\\" + _soundFileName.ToLower();
                //WebClient __client = new WebClient();
                //__client.DownloadFileCompleted += new AsyncCompletedEventHandler(__client_DownloadFileCompleted);
                //__client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(__client_DownloadProgressChanged);
                //__client.DownloadFileAsync(new Uri("http://www.smlsoft.com/download/pos_sound.zip"), __path);

                __dw = new _posSoundDownload();
                __dw.onDownloadComplete += new _onDownloadSoundComplete(__client_DownloadFileCompleted);
                __dw.onDownloadProcess += new _onDownloadSoundProcess(__client_DownloadProgressChanged);
                __dw._downloadSound();
                _enableButton(false, true);
            }
            catch
            {
                _downloadProcess = false;
                _enableButton(true, false);
            }
        }

        void __client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            _resultLabel.Text = string.Format("{0} of {1} Byte", e.BytesReceived, e.TotalBytesToReceive);
            _downloadProgressbar.Value = e.ProgressPercentage;
        }

        void __client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {

            _downloadProcess = false;
            _enableButton(true, false);

            if (e.Cancelled == false)
            {
                _resultLabel.Text = "Install POS Sound Success !!";
                MessageBox.Show("Install POS Sound Success !!");
            }
        }

        private void _enableButton(bool __buttonStart, bool __buttonStop)
        {
            _startButton.Enabled = __buttonStart;
            _stopButton.Enabled = __buttonStop;
        }

        private void _stopButton_Click(object sender, EventArgs e)
        {
            __dw._cancelDownload();
            _downloadProcess = false;
            _downloadProgressbar.Value = 100;
            _resultLabel.Text = "Cancel Download";
            _enableButton(true, false);
        }
    }

    public class _posSoundDownload
    {
        public string _soundFileName = "pos_sound.zip";
        public event _onDownloadSoundProcess onDownloadProcess;
        public event _onDownloadSoundComplete onDownloadComplete;
        WebClient __client = null;

        public void _downloadSound()
        {
            string __path = Path.GetTempPath() + "\\" + _soundFileName.ToLower();
            __client = new WebClient();
            __client.DownloadFileCompleted += new AsyncCompletedEventHandler(__client_DownloadFileCompleted);
            __client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(__client_DownloadProgressChanged);
            __client.DownloadFileAsync(new Uri("http://www.smlsoft.com/download/pos_sound.zip"), __path);
        }

        void __client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (onDownloadProcess != null)
            {
                onDownloadProcess(this, e);
            }            
        }

        public void _cancelDownload()
        {
            try
            {
                __client.CancelAsync();
            }
            catch
            {

            }
        }

        void __client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            string __smlSoftPath = @"C:\smlsoft\";
            bool __isDirCreate = MyLib._myUtil._dirExists(__smlSoftPath);

            if (__isDirCreate == false)
            {
                System.IO.Directory.CreateDirectory(__smlSoftPath); // create folders
            }

            string __path = Path.GetTempPath() + "\\" + _soundFileName.ToLower();

            // unzip it
            try
            {
                ZipInputStream zipIn = new ZipInputStream(File.OpenRead(__path));
                ZipEntry entry;

                while ((entry = zipIn.GetNextEntry()) != null)
                {
                    FileStream streamWriter = File.Create(__smlSoftPath + entry.Name);
                    
                    long size = entry.Size;
                    byte[] data = new byte[size];
                    while (true)
                    {
                        size = zipIn.Read(data, 0, data.Length);
                        if (size > 0) streamWriter.Write(data, 0, (int)size);
                        else break;
                    }
                    streamWriter.Close();
                }
            }
            catch (Exception ex)
            {
            }
            
            if (onDownloadComplete != null)
                onDownloadComplete(this, e);
        }

    }

    public delegate void _onDownloadSoundProcess(object sender, DownloadProgressChangedEventArgs e);
    public delegate void _onDownloadSoundComplete(object sender, AsyncCompletedEventArgs e);

}
