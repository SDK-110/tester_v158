using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FftSharp;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using NAudio.Wave.Compression;
namespace testapp
{
    public delegate bool data_capture(string a, string b, bool c);
    public partial class mic_test : Form
    {
        public int esc = 0;
        const int cyl = 48000;
        //const int cyl = 96000;
        // private IWaveIn waveIn;
        //  private WasapiCapture waveIn;
        List<double> amp_freq_pag = new List<double>();
        volatile int dected_flog = 0;
         
       // private WasapiLoopbackCapture waveIn;
        //MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
        //MMDevice[] CaptureDevices = null;
        //MMDevice selectedDevice = null;
        public data_capture data_Capture = null;
        volatile int is_start = 0;
        static mic_test mic_test_instance;
        double _threshold = 500;


        private NAudio.Wave.WaveInEvent waveIn;
       
       
        private  mic_test()
        {
            
      

            InitializeComponent();
            SetCurrentMicVolume(100);

            if (NAudio.Wave.WaveIn.DeviceCount == 0) return;

            waveIn = new WaveInEvent();
            waveIn.DeviceNumber = 0;
            waveIn.WaveFormat= new WaveFormat(cyl, 16, 1);
         



            //CaptureDevices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToArray();
            //var defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
            //selectedDevice = CaptureDevices.FirstOrDefault(c => c.ID == defaultDevice.ID);
            // selectedDevice = CaptureDevices[1];
            // if (waveIn != null) { waveIn.StopRecording(); waveIn.Dispose(); }
          //  waveIn = new WasapiCapture();
            waveIn.DataAvailable += wav_rec_call;
            waveIn.RecordingStopped += WaveIn_RecordingStopped;
            // waveIn.WaveFormat = new WaveFormat(cyl, 1);
             waveIn.BufferMilliseconds = 20;


            //  MessageBox.Show("Test" + waveIn.WaveFormat.SampleRate);


        }

        public static  mic_test instance {

            get {


                if (mic_test_instance == null) mic_test_instance = new mic_test();

                return mic_test_instance;

            }

        }

        public void start_fft()
        {
            rsu_str = "";
          
            dected_flog = 0;
        
            if (waveIn != null && is_start == 0)
            {

                is_start = 1;
                try
                {
                    waveIn.StartRecording();

                }
                catch { }
                






            }
        }
        string rsu_str = "";
        private void wav_rec_call(object sender, WaveInEventArgs e)
        {

            //int bytesPerSample = waveIn.WaveFormat.BitsPerSample / 8;
            //int sampleRecorded = e.BytesRecorded / bytesPerSample;

            //double [] lastbuffer = new double[sampleRecorded];

            //for (int i = 0; i < sampleRecorded; i++) {


            //    lastbuffer[i] = BitConverter.ToInt16(e.Buffer, i * bytesPerSample);

            //}

               // var m = BitConverter.ToInt16(e.Buffer, 0);
                int t = 0;
               // double[] rs = new double[2048 * 2];
            double[] rs = new double[1024];

            // for (int i = 0; i < e.BytesRecorded; i += waveIn.WaveFormat.BlockAlign)
            for (int i = 0; i < e.BytesRecorded; i +=((WaveInEvent)sender).WaveFormat.BlockAlign)
            {
                var s = BitConverter.ToInt16(e.Buffer, i)/2.0000;
             //   var s = BitConverter.ToInt16(e.Buffer, i)/5000;
                // this.textBox2.AppendText("" + (s) + "\r\n");

                rs[t++] = s;
                    if (t == 1024) break;
                }



            //  double[] audio = FftSharp.SampleData.SampleAudio1();
         // int sampleRate = 96000;
          int sampleRate = 48000;


             double[] fftPower = FftSharp.FFT.Magnitude(FftSharp.FFT.Forward(rs));     //  FFTpower(audio);
            double[] freqs = FftSharp.FFT.FrequencyScale(fftPower.Length, sampleRate);

            for (int i = 0; i < freqs.Length; i++)
                {


                    if (fftPower[i] > _threshold)
                    {


                      foreach(var freq in amp_freq_pag) {

                        if (Math.Abs((freqs[i]) - freq) < 100) {
                            rsu_str = $"{fftPower[i]:f3}" + "|" + (freqs[i]);
                            dected_flog += 1;
                        }
                    }
                       mylib.utility_func.callbackdebuginfo(fftPower[i] + " ; " + (freqs[i]));
                        //if (data_Capture != null)
                        //    data_Capture(fftPower[i] + "", (freqs[i]) + "", true);
                    //  ppp = ppp + (fftPower[i] ) + "," + (freqs[i]*2) + "\r\n";
                        //this.Invoke(new Action(() => {

                        //    label1.Text = fftPower[i] + ";" + (freqs[i]) + "";


                        //}));
                    }


                }

           
        }

        public void stop_fft()
        {

            amp_freq_pag.Clear();
            dected_flog = 0;
        
            if (waveIn != null && is_start == 1)
            {
                is_start = 0;
                tflog = 0;
                waveIn.StopRecording();

                while (tflog == 1) {

                    System.Threading.Thread.Sleep(10);
                    Application.DoEvents();

                }

             //  waveIn.Dispose();


            }

        }

        volatile int tflog = 0;
        private void WaveIn_RecordingStopped(object sender, StoppedEventArgs e)
        {
            tflog = 1;
        }

        private int GetCurrentMicVolume()
        {
            int volume = 0;
            var enumerator = new MMDeviceEnumerator();

            //获取音频输入设备
            IEnumerable<MMDevice> captureDevices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToArray();
            if (captureDevices.Count() > 0)
            {
                MMDevice mMDevice = captureDevices.ToList()[0];
                volume = (int)(mMDevice.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
            }
            return volume;
        }

        private void SetCurrentMicVolume(int volume)
        {
            var enumerator = new MMDeviceEnumerator();
            IEnumerable<MMDevice> captureDevices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToArray();
            if (captureDevices.Count() > 0)
            {
                MMDevice mMDevice = captureDevices.ToList()[0];
                mMDevice.AudioEndpointVolume.MasterVolumeLevelScalar = volume / 100.0f;
                
            }
        }

        public float GetVoicePeakValue()
        {
            var enumerator = new MMDeviceEnumerator();
            var CaptureDevices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToArray();
            var defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
            var selectedDevice = CaptureDevices.FirstOrDefault(c => c.ID == defaultDevice.ID);







            return selectedDevice.AudioMeterInformation.MasterPeakValue;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void mic_test_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (waveIn != null) { is_start = 0; waveIn.StopRecording(); waveIn.Dispose(); }
        }

        public void dispose_vave() {

            if (waveIn != null) { is_start = 0; waveIn.StopRecording(); waveIn.Dispose(); }
        }

        public int   get_amp_freq(int delay,double threshold,  bool is_det_break,double [] freqs,int times, out string srsu) {

            this._threshold = threshold;
            foreach (var frq in freqs) {

                amp_freq_pag.Add(frq);

            }
            dected_flog = 0;
            start_fft();
            int rsu = 0;
            int dida = delay / 4;
            srsu = "not_found";
            while (dida-- > 0 ) {
                System.Threading.Thread.Sleep(3);
                Application.DoEvents();
                rsu = dected_flog;
                srsu = rsu_str;
                if (dected_flog >= times) {  break; }
                
            }
            stop_fft();

            return rsu;

        }
        
    }
}
