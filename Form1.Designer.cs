namespace Oraycn.DesktopMixedAudioRecordDemo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_startRecord = new System.Windows.Forms.Button();
            this.button_stopRecord = new System.Windows.Forms.Button();
            this.label_recording = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_frameCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_startRecord
            // 
            this.button_startRecord.Location = new System.Drawing.Point(12, 12);
            this.button_startRecord.Name = "button_startRecord";
            this.button_startRecord.Size = new System.Drawing.Size(75, 23);
            this.button_startRecord.TabIndex = 0;
            this.button_startRecord.Text = "开始录制";
            this.button_startRecord.UseVisualStyleBackColor = true;
            this.button_startRecord.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_stopRecord
            // 
            this.button_stopRecord.Enabled = false;
            this.button_stopRecord.Location = new System.Drawing.Point(93, 12);
            this.button_stopRecord.Name = "button_stopRecord";
            this.button_stopRecord.Size = new System.Drawing.Size(75, 23);
            this.button_stopRecord.TabIndex = 0;
            this.button_stopRecord.Text = "停止录制";
            this.button_stopRecord.UseVisualStyleBackColor = true;
            this.button_stopRecord.Click += new System.EventHandler(this.button2_Click);
            // 
            // label_recording
            // 
            this.label_recording.AutoSize = true;
            this.label_recording.ForeColor = System.Drawing.Color.Red;
            this.label_recording.Location = new System.Drawing.Point(12, 62);
            this.label_recording.Name = "label_recording";
            this.label_recording.Size = new System.Drawing.Size(143, 12);
            this.label_recording.TabIndex = 7;
            this.label_recording.Text = "正在采集、混音、录制...";
            this.label_recording.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "队列中待处理视频帧个数：";
            // 
            // label_frameCount
            // 
            this.label_frameCount.AutoSize = true;
            this.label_frameCount.Location = new System.Drawing.Point(159, 92);
            this.label_frameCount.Name = "label_frameCount";
            this.label_frameCount.Size = new System.Drawing.Size(11, 12);
            this.label_frameCount.TabIndex = 9;
            this.label_frameCount.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 113);
            this.Controls.Add(this.label_frameCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_recording);
            this.Controls.Add(this.button_stopRecord);
            this.Controls.Add(this.button_startRecord);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "桌面、麦克风和声卡混音，录制 Demo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_startRecord;
        private System.Windows.Forms.Button button_stopRecord;
        private System.Windows.Forms.Label label_recording;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_frameCount;
    }
}

