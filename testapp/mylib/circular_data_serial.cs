using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace testapp.mylib
{
    internal class circular_data_serial
    {

       public static SerialPort _serialPort;
        private static readonly CircularStringBuffer _ringBuffer = new CircularStringBuffer(20);
        private static readonly AutoResetEvent _exitEvent = new AutoResetEvent(false);
        volatile public bool match = false;
        volatile private string _str_mat = "";

        public string str_mat
        {
            get { return _str_mat; }
            set
            {
                _str_mat = value;
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                    mylib.utility_func.callbackdebuginfo("串口不正常关闭，已经强制再次打开");

                }
            }
        }
        public circular_data_serial(string port)
        {
            string portName = port; // ← 修改为你的串口号
            int baudRate = 1152000;
           
            try
            {
                _serialPort = new SerialPort(portName, baudRate)
                {
                    Parity = Parity.None,
                    DataBits = 8,
                    StopBits = StopBits.One,
                    Handshake = Handshake.None,
                    ReadTimeout = 500,
                    WriteTimeout = 500
                };

                _serialPort.DataReceived += OnDataReceived;
                _serialPort.Open();
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"串口打开失败: {ex.Message}");
            }

        }

        private static StringBuilder DisplayBuffer()
        {
            StringBuilder sb = new StringBuilder();
                var all = _ringBuffer.GetAll();
                if (all.Count > 0)
                {
                   
                    for (int i = 0; i < all.Count; i++)
                    {
                        mylib.utility_func.callbackdebuginfo($"[{i + 1:D2}] {all[i]}");
                    sb.Append(all[i].ToString());   
                    }
                  
                }
           return sb;
        }
        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (_serialPort == null || !_serialPort.IsOpen) return;

            try
            {
                int ct = _serialPort.BytesToRead;
                if (ct == 0) return;

                byte[] rd = new byte[ct]; ;
                _serialPort.Read(rd, 0, ct);

                string data = Encoding.UTF8.GetString(rd);

                //string data = _serialPort.ReadLine(); // 读取一行（直到 \n 或 \r\n）
                //data = data.Trim(); // 去除首尾空白

                if (!string.IsNullOrEmpty(data))
                {
                    if(data.IndexOf(_str_mat)>=0) match = true;
                    _ringBuffer.Add(data);
                    mylib.utility_func.callbackdebuginfo($"rev: \"{data}\" (current buffer: {_ringBuffer.Count}/20)");
                }
            }
            catch (TimeoutException)
            {
                // ReadLine 超时，忽略
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"接收错误: {ex.Message}");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {


            //using (var audioFile = new AudioFileReader(@"D:\BaiduNetdisk\sounds\1.wav"))
            //using (var outputDevice = new WaveOutEvent())
            //{
            //    outputDevice.Init(audioFile);
            //    outputDevice.Play();
            //    while (outputDevice.PlaybackState == PlaybackState.Playing)
            //    {
            //        Thread.Sleep(100);
            //    }
            //}

            string[] audioPaths = {
            @"D:\BaiduNetdisk\sounds\1.wav",
             @"D:\BaiduNetdisk\sounds\2.wav"
        };
            await Task.Run(() =>
            {
                foreach (string path in audioPaths)
                {
                    if (System.IO.File.Exists(path))
                    {
                        mylib.utility_func.callbackdebuginfo($"playing: {path}");
                        PlayWavFile(path);
                    }
                    else
                    {
                        mylib.utility_func.callbackdebuginfo($"文件不存在: {path}");
                    }
                }
            });

        }
        static void PlayWavFile(string filePath)
        {
            try
            {
                using (var player = new SoundPlayer(filePath))
                {
                    player.PlaySync(); // 同步播放（会等待播放完成）
                }
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"播放失败 {filePath}: {ex.Message}");
            }


        }

       ~circular_data_serial()
        {
            _exitEvent.Set(); // 通知显示线程退出

            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Close();
                _serialPort.Dispose();
            }
        }
    }
    public class CircularStringBuffer
    {
        private readonly LinkedList<string> _buffer = new LinkedList<string>();
        private readonly int _maxCapacity;
        private readonly object _lock = new object();

        public CircularStringBuffer(int maxCapacity = 20)
        {
            _maxCapacity = maxCapacity;
        }

        public void Add(string item)
        {
            lock (_lock)
            {
                if(item.IndexOf(item)>=0)
                _buffer.AddLast(item);
                if (_buffer.Count > _maxCapacity)
                {
                    _buffer.RemoveFirst(); // 移除最旧的
                }
            }
        }
        public void ClearData()
        {
            lock (_lock)
            {
                _buffer.Clear();
            }
        }
        public List<string> GetAll()
        {
            lock (_lock)
            {
                return new List<string>(_buffer);
            }
        }

        public int Count
        {
            get
            {
                lock (_lock)
                {
                    return _buffer.Count;
                }
            }
        }
    }

}
