using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Un4seen.Bass;

namespace BassDemoForm
{
    public partial class Form1 : Form
    {
        string _fileName;

        int stream;

        public Form1()
        {
            InitializeComponent();

            if (!Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_CPSPEAKERS, this.Handle))
                MessageBox.Show("Bass 初始化失败 " + Bass.BASS_ErrorGetCode().ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                _fileName = openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stream = Bass.BASS_StreamCreateFile(_fileName, 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);
            Bass.BASS_ChannelPlay(stream, true);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Bass.BASS_ChannelStop(stream);
            Bass.BASS_StreamFree(stream);
            Bass.BASS_Stop();
            Bass.BASS_Free();
        }
    }
}
