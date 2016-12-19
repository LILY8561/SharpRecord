using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESBasic;
using Oraycn.MCapture;
using Oraycn.MFile;

namespace Oraycn.DesktopMixedAudioRecordDemo
{
    /*
    * 本demo采用的是 语音视频采集组件MCapture 和 语音视频录制组件MFile 的免费版本。若想获取MCapture、MFile其它版本，请联系 www.oraycn.com 。
    * 
    * 试用版：只能连续工作5分钟！
    */
    public partial class Form1 : Form
    {       
        private ISoundcardCapturer soundcardCapturer;
        private IMicrophoneCapturer microphoneCapturer;
        private IDesktopCapturer desktopCapturer;
        private IAudioMixter audioMixter;
        private VideoFileMaker videoFileMaker;
        private Timer timer;
        private bool sizeRevised = false;// 是否需要将图像帧的长宽裁剪为4的整数倍

        public Form1()
        {
            InitializeComponent();           

            Oraycn.MCapture.GlobalUtil.SetAuthorizedUser("FreeUser", "");
            Oraycn.MFile.GlobalUtil.SetAuthorizedUser("FreeUser", "");
            this.timer = new Timer();
            this.timer.Interval = 1000;
            this.timer.Tick += new EventHandler(timer_Tick);
            this.timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (this.videoFileMaker != null)
            {
                this.label_frameCount.Text = this.videoFileMaker.ToBeProcessedVideoFrameCount.ToString();
            }
        }       

        
        private volatile bool isRecording = false;
        //开始桌面、麦克风、声卡 采集、录制        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.audioCount = 0;

                
                int frameRate = 15;
                this.desktopCapturer = CapturerFactory.CreateDesktopCapturer(frameRate, false);
                this.desktopCapturer.ImageCaptured += new CbGeneric<Bitmap>(desktopCapturer_ImageCaptured);

                //声卡采集器 【目前声卡采集仅支持vista以及以上系统】
                this.soundcardCapturer = CapturerFactory.CreateSoundcardCapturer();

                //麦克风采集器
                this.microphoneCapturer = CapturerFactory.CreateMicrophoneCapturer(0);

                //混音器
                this.audioMixter = CapturerFactory.CreateAudioMixter(this.microphoneCapturer, this.soundcardCapturer, SoundcardMode4Mix.DoubleChannel,true);

                this.audioMixter.AudioMixed += new CbGeneric<byte[]>(audioMixter_AudioMixed);
                this.microphoneCapturer.CaptureError += new CbGeneric<Exception>(capturer_CaptureError);
                this.soundcardCapturer.CaptureError += new CbGeneric<Exception>(capturer_CaptureError);

                //开始采集
                this.microphoneCapturer.Start();
                this.soundcardCapturer.Start();
                this.desktopCapturer.Start();                  

                //录制组件
                this.videoFileMaker = new VideoFileMaker();
                Size videoSize = Screen.PrimaryScreen.Bounds.Size;
                this.sizeRevised = (videoSize.Width % 4 != 0) || (videoSize.Height % 4 != 0);
                if (this.sizeRevised)
                {
                    videoSize = new Size(videoSize.Width / 4 * 4, videoSize.Height / 4 * 4);
                }
                this.videoFileMaker.Initialize("test.mp4", VideoCodecType.H264, videoSize.Width, videoSize.Height, frameRate, VideoQuality.High, AudioCodecType.AAC,
                    this.audioMixter.SampleRate, this.audioMixter.ChannelCount, true);
                this.isRecording = true;
                
                this.button_startRecord.Enabled = false;
                this.button_stopRecord.Enabled = true;               
                this.label_recording.Visible = true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        void desktopCapturer_ImageCaptured(Bitmap img)
        {
            if (this.isRecording)
            {
                Bitmap imgRecorded = img;
                if (this.sizeRevised) // 对图像进行裁剪，  MFile要求录制的视频帧的长和宽必须是4的整数倍。
                {
                    imgRecorded = ESBasic.Helpers.ImageHelper.RoundSizeByNumber(img, 4);
                    img.Dispose();
                }
                this.videoFileMaker.AddVideoFrame(imgRecorded);
            }
        }

        void capturer_CaptureError(Exception obj)
        {

        }

        private int audioCount = 0;
        void audioMixter_AudioMixed(byte[] audioData)
        {
            if (this.isRecording)
            {
                this.videoFileMaker.AddAudioFrame(audioData);
                ++this.audioCount;
            }
        }

        //停止声卡采集、停止录制
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CbGeneric cb = new CbGeneric(this.StopRecordAsyn);
                cb.BeginInvoke(null, null);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }  
        }

        private void StopRecordAsyn()
        {
            this.isRecording = false;            
            this.desktopCapturer.Stop();
            this.desktopCapturer.Dispose();
            this.microphoneCapturer.Stop();
            this.microphoneCapturer.Dispose();
            this.soundcardCapturer.Stop();
            this.soundcardCapturer.Dispose(); //必须要释放声卡采集器！！！！！！！！
            this.audioMixter.Dispose();

            this.videoFileMaker.Close(true);
            this.videoFileMaker.Dispose();
            this.videoFileMaker = null;
            this.AfterStopRecord();
            
        }

        private void AfterStopRecord()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CbGeneric(this.AfterStopRecord));
            }
            else
            {
                this.button_startRecord.Enabled = true;
                this.button_stopRecord.Enabled = false;               
                this.label_recording.Visible = false;
                this.Cursor = Cursors.Default;
                MessageBox.Show("录制完成！" + (this.audioCount * 0.02).ToString() + "秒。");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.isRecording)
            {
                MessageBox.Show("正在录制视频，请先停止！");
                e.Cancel = true;
                return;
            }           
        }        
    }    
}
