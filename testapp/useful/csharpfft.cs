using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FftSharp;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using NAudio.Wave.Compression;
namespace testapp.useful
{
   public  delegate bool data_capture(string a, string b,bool c);
   public  class csharpfft
    {
        public int esc = 0;
        const int cyl = 48000;
        private IWaveIn waveIn;
        MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
        MMDevice[] CaptureDevices = null;
        MMDevice selectedDevice = null;
      public  data_capture data_Capture = null ;
        int is_start = 0;
        public csharpfft()
        {

            CaptureDevices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToArray();
            var defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Communications);
            selectedDevice = CaptureDevices.FirstOrDefault(c => c.ID == defaultDevice.ID);
            if (waveIn != null) { waveIn.StopRecording(); waveIn.Dispose(); }
            waveIn = new WaveIn();
           
            waveIn.WaveFormat = new WaveFormat(cyl, 1);
           


        }
        public void start_fft() {

            if (waveIn != null && is_start == 0) {

                is_start = 1;
                waveIn.StartRecording();
                waveIn.DataAvailable += wav_rec_call;
                





            }
        }

        private void wav_rec_call(object sender, WaveInEventArgs e)
        {
    


                var m = BitConverter.ToInt16(e.Buffer, 0);
                int t = 0;
                double[] rs = new double[2048 * 2];
                for (int i = 0; i < e.BytesRecorded; i += waveIn.WaveFormat.BlockAlign)
                {
                    var s = BitConverter.ToInt16(e.Buffer, i) / 32768.00000;
                    // this.textBox2.AppendText("" + (s) + "\r\n");

                    rs[t++] = s;
                    if (t == 2048 * 2) break;
                }



                //  double[] audio = FftSharp.SampleData.SampleAudio1();
                int sampleRate = 96000;

                double[] fftPower = FftSharp.FFT.Magnitude(FftSharp.FFT.Forward(rs));     //  FFTpower(audio);

                double[] freqs = FftSharp.FFT.FrequencyScale(fftPower.Length, sampleRate);

                for (int i = 0; i < freqs.Length; i++)
                {


                    if (fftPower[i] > 0.3)
                    {


                        if (data_Capture != null)
                            data_Capture(fftPower[i] + "", (freqs[i]) + "", true);
                        //  ppp = ppp + (fftPower[i] ) + "," + (freqs[i]*2) + "\r\n";

                    }


                }






         


        }

        public void stop_fft() {



            if (waveIn != null &&  is_start == 1) {
                is_start = 0;
                waveIn.StopRecording();
                
                waveIn.DataAvailable -= wav_rec_call;
            }
            
        }

        ~csharpfft() {


         if (waveIn != null) { waveIn.StopRecording(); waveIn.Dispose(); }

        }

    }
}
