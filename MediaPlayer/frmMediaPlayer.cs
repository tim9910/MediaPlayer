using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer
{
    public partial class frmMediaPlayer : Form
    {
        ToolTip toolTip = null;
        public frmMediaPlayer()
        {
            InitializeComponent();
            initToolTip();

            btnBrowser.Text = "";
            btnBrowser.Image = ResizeImage(GetImage("browser"), new Size(44, 44));
            btnBrowser.MouseEnter += (sender, e) =>
            {
                btnBrowser.Image = ResizeImage(GetImage("browser"), new Size(48, 48));
            };

            btnBrowser.MouseLeave += (sender, e) =>
            {
                btnBrowser.Image = ResizeImage(GetImage("browser"), new Size(44, 44));
            };

            btnPlay.Text = "";
            btnPlay.Image = ResizeImage(GetImage("play"), new Size(44, 44));
            btnPlay.MouseEnter += (sender, e) =>
            {
                btnPlay.Image = ResizeImage(GetImage("play"), new Size(48, 48));
            };

            btnPlay.MouseLeave += (sender, e) =>
            {
                btnPlay.Image = ResizeImage(GetImage("play"), new Size(44, 44));
            };


            btnStop.Text = "";
            btnStop.Image = ResizeImage(GetImage("stop"), new Size(44, 44));
            btnStop.MouseEnter += (sender, e) =>
            {
                btnStop.Image = ResizeImage(GetImage("stop"), new Size(48, 48));
            };

            btnStop.MouseLeave += (sender, e) =>
            {
                btnStop.Image = ResizeImage(GetImage("stop"), new Size(44, 44));
            };

            btnPause.Text = "";
            btnPause.Image = ResizeImage(GetImage("pause"), new Size(44, 44));
            btnPause.MouseEnter += (sender, e) =>
            {
                btnPause.Image = ResizeImage(GetImage("pause"), new Size(48, 48));
            };

            btnPause.MouseLeave += (sender, e) =>
            {
                btnPause.Image = ResizeImage(GetImage("pause"), new Size(44, 44));
            };

            btnPlay.Enabled = false;
            btnPause.Enabled = false;
            btnStop.Enabled = false;

            //disabled 視窗的最大化和最小化按鈕，讓使用者只能關閉視窗
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            //disbled 視窗的調整大小功能，讓使用者無法改變視窗大小
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            wmpVideo.PlayStateChange += wmpVideo_PlayStateChange;

        }
        private Image GetImage(string name)
        {
            return Properties.Resources.ResourceManager.GetObject(name) as Image;
        }
        private Image ResizeImage(Image img, Size size)
        {
            return new Bitmap(img, size);
        }

        private void initToolTip()
        {
            toolTip = new ToolTip();
            toolTip.SetToolTip(btnBrowser, "瀏覽");
            toolTip.SetToolTip(btnPlay, "播放");
            toolTip.SetToolTip(btnStop, "停止播放");
            toolTip.SetToolTip(btnPause, "暫停播放");
        }

        private void wmpVideo_Enter(object sender, EventArgs e)
        {

        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "WMV files (*.wmv)|*.wmv|MP4 files(*.mp4)|*.mp4|AVI files(*.avi)|*.avi|Allfiles (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                wmpVideo.URL = ofd.FileName;
                wmpVideo.Ctlcontrols.stop(); // 停止

                this.btnPlay.Enabled = true;
                this.btnBrowser.Enabled = false;
                //this.btnPause.Enabled = true;
                //this.btnStop.Enabled = true;
            }
        }

        private void wmpVideo_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            // 8 代表播放結束
            if (e.newState == 8)
            {
                // 播放結束後的處理
                btnBrowser.Enabled = true;
                btnPlay.Enabled = false;
                btnPause.Enabled = false;
                btnStop.Enabled = false;
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            wmpVideo.Ctlcontrols.play(); // 播放
            btnBrowser.Enabled = false;
            btnPlay.Enabled = false;
            btnPause.Enabled = true;
            btnStop.Enabled = true;
        }
        private void btnPause_Click(object sender, EventArgs e)
        {
            wmpVideo.Ctlcontrols.pause(); // 暫停
            btnBrowser.Enabled = false;
            btnPlay.Enabled = true;
            btnPause.Enabled = false;
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            wmpVideo.Ctlcontrols.stop(); // 停止
            btnBrowser.Enabled = true;
            btnPlay.Enabled = false;
            btnPause.Enabled = false;
            btnStop.Enabled = false;
        }
    }
}
