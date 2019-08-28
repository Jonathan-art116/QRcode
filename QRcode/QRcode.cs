using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QRCoder;
using System.Drawing.Imaging;
using System.IO;
using cmd;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace QRcode
{
    public partial class QRcode : Form
    {
        private System.Timers.Timer myTimer;
        public QRcode()
        {
            InitializeComponent();
        }

        private string execCommand(string exeName, string argName)
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = exeName;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.Arguments = argName;
            p.Start();
            p.WaitForExit();
            return p.StandardOutput.ReadToEnd();
        }

        public void button1_Click(object sender, EventArgs e)
        {
  
        }

        private void QRcode_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;//不是很合理的做法
            textBox1.Text = "";
            pictureBox1.Image = null;
            this.myTimer = new System.Timers.Timer(1000);
            this.myTimer.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Tick);
            this.myTimer.AutoReset = true;
            this.myTimer.Enabled = true;
            this.myTimer.Start();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string check_devices = execCommand("adb", "devices");
            string[] str = check_devices.Split('\n');
            if (str[1].Length > 6)
            {
                string ssid = execCommand("adb", "shell cat /dev/atel_misc");
                if (ssid.Length > 3)
                {
                    textBox1.Text = ssid;
                    QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                    QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(ssid, QRCodeGenerator.ECCLevel.Q);
                    QRCode qRCode = new QRCode(qRCodeData);
                    Bitmap bitmap = qRCode.GetGraphic(9);
                    pictureBox1.Image = bitmap;
                }
            }
            else
            {
                textBox1.Text = "";
                pictureBox1.Image = null;
            }
        }
    } 
}
