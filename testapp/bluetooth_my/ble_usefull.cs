using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Security.Cryptography;
using Windows.Foundation;
namespace test_blue_dll
{

    public delegate void sendmessage(string a);
    public delegate void callback_chuandi(BluetoothLEDevice bl);


    class BleCore
    {
        private bool asyncLock = false;
        public sendmessage st;
        public bool is_get_ser { get; private set; }
        /// <summary>
        /// 当前连接的服务
        /// </summary>
        public GattDeviceService CurrentService { get; private set; } ///////lllll

        /// <summary>
        /// 当前连接的蓝牙设备
        /// </summary>
        public BluetoothLEDevice CurrentDevice { get; private set; }

        /// <summary>
        /// 写特征对象
        /// </summary>
        public GattCharacteristic CurrentWriteCharacteristic { get; private set; } //////////

        /// <summary>
        /// 通知特征对象
        /// </summary>
        public GattCharacteristic CurrentNotifyCharacteristic { get; private set; }



        /// <summary>
        /// 读特征对象
        /// </summary>
        public GattCharacteristic currentreadcharacteristic { get; private set; }

        /// <summary>
        /// 存储检测到的设备
        /// </summary>
        /// 

        public List<BluetoothLEDevice> DeviceList { get; private set; }
        public List<GattDeviceService> servicelist { get; private set; }
        /// <summary>
        /// 特性通知类型通知启用
        /// </summary>
        private const GattClientCharacteristicConfigurationDescriptorValue CHARACTERISTIC_NOTIFICATION_TYPE = GattClientCharacteristicConfigurationDescriptorValue.Notify;

        /// <summary>
        /// 定义搜索蓝牙设备委托
        /// </summary>
        public delegate void DeviceWatcherChangedEvent(BluetoothLEDevice bluetoothLEDevice,int rssi);
        
        /// <summary>
        /// 搜索蓝牙事件
        /// </summary>
        public event DeviceWatcherChangedEvent DeviceWatcherChanged;

        /// <summary>
        /// 获取服务委托
        /// </summary>
        public delegate void CharacteristicFinishEvent(int size);

        /// <summary>
        /// 获取服务事件
        /// </summary>
        public event CharacteristicFinishEvent CharacteristicFinish;

        /// <summary>
        /// 获取特征委托
        /// </summary>
        public delegate void CharacteristicAddedEvent(GattCharacteristic gattCharacteristic);

        /// <summary>
        /// 获取特征事件
        /// </summary>
        public event CharacteristicAddedEvent CharacteristicAdded;

        /// <summary>
        /// 接受数据委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        public delegate void RecDataEvent(GattCharacteristic sender, byte[] data);

        /// <summary>
        /// 接受数据事件
        /// </summary>
        public event RecDataEvent Recdate;

        /// <summary>
        /// 当前连接的蓝牙Mac
        /// </summary>
        private string CurrentDeviceMAC { get; set; }

        private BluetoothLEAdvertisementWatcher Watcher = null;

        public BleCore()
        {
            is_get_ser = false;
            DeviceList = new List<BluetoothLEDevice>();
            servicelist = new List<GattDeviceService>();
        }

        ~BleCore()
        {
           
            this.Dispose();
        }

        /// <summary>
        /// 搜索蓝牙设备
        /// </summary>
        public void StartBleDeviceWatcher()
        {
            Watcher = new BluetoothLEAdvertisementWatcher();

            Watcher.ScanningMode = BluetoothLEScanningMode.Active;

            // only activate the watcher when we're recieving values >= -80
            Watcher.SignalStrengthFilter.InRangeThresholdInDBm = -80;

            // stop watching if the value drops below -90 (user walked away)
            Watcher.SignalStrengthFilter.OutOfRangeThresholdInDBm = -90;

            // register callback for when we see an advertisements
            Watcher.Received += OnAdvertisementReceived;
            Watcher.Stopped += Watcher_Stopped;
            // wait 5 seconds to make sure the device is really out of range
            Watcher.SignalStrengthFilter.OutOfRangeTimeout = TimeSpan.FromMilliseconds(5000);
            Watcher.SignalStrengthFilter.SamplingInterval = TimeSpan.FromMilliseconds(2000);

            // starting watching for advertisements
            Watcher.Start();
        }

        private void Watcher_Stopped(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementWatcherStoppedEventArgs args)
        {
            
        }

        /// <summary>
        /// 停止搜索蓝牙
        /// </summary>
        public void StopBleDeviceWatcher()
        {
           if(Watcher != null /*&& Watcher.Status == BluetoothLEAdvertisementWatcherStatus.Started*/)
          
               this.Watcher.Stop();
            is_get_ser = false;
        }

        /// <summary>
        /// 主动断开连接
        /// </summary>
        /// <returns></returns>
        public void Dispose()
        {
            is_get_ser = false;
            if (Watcher !=null) Watcher.Stop();
            Watcher = null;

            CurrentDeviceMAC = null;
            currentreadcharacteristic?.Service?.Dispose();
            currentreadcharacteristic = null;
            foreach (var ser in servicelist)
            {
               

                ser?.Dispose();
            }


            foreach (var chr in DeviceList)
            {
               
                chr?.Dispose();
               
            }

            servicelist.Clear();
            DeviceList.Clear();
            CurrentService = null;
            CurrentService = null;
    

            GC.Collect();
            st?.Invoke("++主动断开连接" + "\r\n");
        }

        /// <summary>
        /// 匹配
        /// </summary>
        /// <param name="Device"></param>
        public void StartMatching(BluetoothLEDevice Device)
        {
            this.CurrentDevice = Device;
        }

        /// <summary>
        /// 发送数据接口
        /// </summary>
        /// <returns></returns>
        public void Write(byte[] data)
        {
            if (CurrentWriteCharacteristic != null)
            {
                CurrentWriteCharacteristic.WriteValueAsync(CryptographicBuffer.CreateFromByteArray(data), GattWriteOption.WriteWithResponse).Completed = (asyncInfo, asyncStatus) =>
                {
                    if (asyncStatus == AsyncStatus.Completed)
                    {
                        GattCommunicationStatus a = asyncInfo.GetResults();
                        st?.Invoke("发送数据：" + BitConverter.ToString(data) + " State : " + a + "\r\n");
                    }
                };
            }

        }

        public  string get_value(string format= "ASCII") {



            if (currentreadcharacteristic != null)
            {

                var m = currentreadcharacteristic.ReadValueAsync(BluetoothCacheMode.Uncached);
                while (m.Status != AsyncStatus.Completed) { System.Threading.Thread.Sleep(10); }


                var reader = m.GetResults();
                var p = reader.Value;

                string PPP = Utilities.FormatValue(p, format);
                st?.Invoke(PPP + "\n\r");



                return PPP;
            }

            return "null";
        
        }



        /// 获取蓝牙服务
        /// </summary>
        public void FindService()
        {
            this.CurrentDevice.GetGattServicesAsync().Completed = (asyncInfo, asyncStatus) =>
            {
                if (asyncStatus == AsyncStatus.Completed)
                {
                    var services = asyncInfo.GetResults().Services;
                    st?.Invoke("GattServices size=" + services.Count + "\r\n");
                    foreach (GattDeviceService ser in services)
                    {
                        servicelist.Add(ser);
                        FindCharacteristic(ser);
                    }
                    CharacteristicFinish?.Invoke(services.Count);
                    is_get_ser = true;
                }
            };

        }

        /// <summary>
        /// 按MAC地址直接组装设备ID查找设备
        /// </summary>
        public void SelectDeviceFromIdAsync(string MAC)
        {
            CurrentDeviceMAC = MAC;
            CurrentDevice = null;
            BluetoothAdapter.GetDefaultAsync().Completed = (asyncInfo, asyncStatus) =>
            {
                if (asyncStatus == AsyncStatus.Completed)
                {
                    BluetoothAdapter mBluetoothAdapter = asyncInfo.GetResults();
                    byte[] _Bytes1 = BitConverter.GetBytes(mBluetoothAdapter.BluetoothAddress);//ulong转换为byte数组
                    Array.Reverse(_Bytes1);
                    string macAddress = BitConverter.ToString(_Bytes1, 2, 6).Replace('-', ':').ToLower();
                    string Id = "BluetoothLE#BluetoothLE" + macAddress + "-" + MAC;
                    Matching(Id);
                }
            };
        }

        /// <summary>
        /// 获取操作
        /// </summary>
        /// <returns></returns>
        public void SetOpteron(GattCharacteristic gattCharacteristic)
        {
            byte[] _Bytes1 = BitConverter.GetBytes(this.CurrentDevice.BluetoothAddress);
            Array.Reverse(_Bytes1);
            this.CurrentDeviceMAC = BitConverter.ToString(_Bytes1, 2, 6).Replace('-', ':').ToLower();

            string msg = "正在连接设备<" + this.CurrentDeviceMAC + ">..";
            st?.Invoke(msg + "\r\n");

            if (gattCharacteristic.CharacteristicProperties == GattCharacteristicProperties.Write)
            {
                this.CurrentWriteCharacteristic = gattCharacteristic;
            }
            if (gattCharacteristic.CharacteristicProperties == GattCharacteristicProperties.Notify)
            {
                this.CurrentNotifyCharacteristic = gattCharacteristic;
            }
            if (gattCharacteristic.CharacteristicProperties == GattCharacteristicProperties.Read)
            {

                this.currentreadcharacteristic = gattCharacteristic;

            }

            if (gattCharacteristic.CharacteristicProperties == (GattCharacteristicProperties.Read|GattCharacteristicProperties.Write | GattCharacteristicProperties.Notify))
            {

                try
                {
                    this.CurrentNotifyCharacteristic.ValueChanged -= Characteristic_ValueChanged;
                    this.CurrentDevice.ConnectionStatusChanged -= this.CurrentDevice_ConnectionStatusChanged;
                }
                catch { }
                this.CurrentWriteCharacteristic = gattCharacteristic;
                this.CurrentNotifyCharacteristic = gattCharacteristic;
                this.CurrentNotifyCharacteristic.ProtectionLevel = GattProtectionLevel.Plain;
                this.CurrentNotifyCharacteristic.ValueChanged += Characteristic_ValueChanged;
                this.CurrentDevice.ConnectionStatusChanged += this.CurrentDevice_ConnectionStatusChanged;
                this.EnableNotifications(CurrentNotifyCharacteristic);
            }


          

        }

        private void OnAdvertisementReceived(BluetoothLEAdvertisementWatcher watcher, BluetoothLEAdvertisementReceivedEventArgs eventArgs)
        {
            Int16 rssi = eventArgs.RawSignalStrengthInDBm;
            BluetoothLEDevice.FromBluetoothAddressAsync(eventArgs.BluetoothAddress).Completed = (asyncInfo, asyncStatus) =>
            {
                if (asyncStatus == AsyncStatus.Completed)
                {
                    if (asyncInfo.GetResults() == null)
                    {
                        //Console.WriteLine("没有得到结果集");
                    }
                    else
                    {
                        BluetoothLEDevice currentDevice = asyncInfo.GetResults();

                        if (DeviceList.FindIndex((x) => { return x.Name.Equals(currentDevice.Name); }) < 0)
                        {
                            this.DeviceList.Add(currentDevice);
                            DeviceWatcherChanged?.Invoke(currentDevice, rssi);
                        }

                    }

                }
            };
        }

        /// <summary>
        /// 获取特性
        /// </summary>
        private void FindCharacteristic(GattDeviceService gattDeviceService)
        {
            this.CurrentService = gattDeviceService;
            this.CurrentService.GetCharacteristicsAsync().Completed = (asyncInfo, asyncStatus) =>
            {
                if (asyncStatus == AsyncStatus.Completed)
                {
                    var services = asyncInfo.GetResults().Characteristics;
                    foreach (var c in services)
                    {
                        this.CharacteristicAdded?.Invoke(c);
                    }

                }
            };
        }

        /// <summary>
        /// 搜索到的蓝牙设备
        /// </summary>
        /// <returns></returns>
        private void Matching(string Id)
        {
            try
            {
                BluetoothLEDevice.FromIdAsync(Id).Completed = (asyncInfo, asyncStatus) =>
                {
                    if (asyncStatus == AsyncStatus.Completed)
                    {
                        BluetoothLEDevice bleDevice = asyncInfo.GetResults();
                        this.DeviceList.Add(bleDevice);
                        st?.Invoke(bleDevice.ToString());
                    }

                    if (asyncStatus == AsyncStatus.Started)
                    {
                        st(asyncStatus.ToString());
                    }
                    if (asyncStatus == AsyncStatus.Canceled)
                    {
                        st?.Invoke(asyncStatus.ToString());
                    }
                    if (asyncStatus == AsyncStatus.Error)
                    {
                        st?.Invoke(asyncStatus.ToString());
                    }
                };
            }
            catch (Exception e)
            {
                string msg = "没有发现设备" + e.ToString();
                st?.Invoke(msg + "\r\n");
               // this.StartBleDeviceWatcher();
            }
        }


        private void CurrentDevice_ConnectionStatusChanged(BluetoothLEDevice sender, object args)
        {
            if (sender.ConnectionStatus == BluetoothConnectionStatus.Disconnected && CurrentDeviceMAC != null)
            {
                if (asyncLock)
                {
                    asyncLock = true;
                    Watcher.Stop();
                    Watcher = null;

                    CurrentDeviceMAC = null;
                    currentreadcharacteristic?.Service?.Dispose();
                    currentreadcharacteristic = null;
                    foreach (var ser in servicelist)
                    {


                        ser?.Dispose();
                    }


                    foreach (var chr in DeviceList)
                    {

                        chr?.Dispose();

                    }

                    servicelist.Clear();
                    DeviceList.Clear();
                    CurrentService = null;
                    CurrentService = null;
                    try
                    {
                        Watcher.Received -= OnAdvertisementReceived;

                    }
                    catch { }

                    GC.Collect();

                }
            }
            else
            {
             
                    asyncLock = false;
                st?.Invoke("设备已连接" + "\r\n");
                
            }
        }

        /// <summary>
        /// 设置特征对象为接收通知对象
        /// </summary>
        /// <param name="characteristic"></param>
        /// <returns></returns>
        private void EnableNotifications(GattCharacteristic characteristic)
        {
            st?.Invoke("收通知对象=" + CurrentDevice.Name + ":" + CurrentDevice.ConnectionStatus + "\r\n");
            characteristic.WriteClientCharacteristicConfigurationDescriptorAsync(CHARACTERISTIC_NOTIFICATION_TYPE).Completed = (asyncInfo, asyncStatus) =>
            {
                if (asyncStatus == AsyncStatus.Completed)
                {
                    GattCommunicationStatus status = asyncInfo.GetResults();
                    if (status == GattCommunicationStatus.Unreachable)
                    {
                        st?.Invoke("设备不可用" + "\r\n");
                        if (CurrentNotifyCharacteristic != null && !asyncLock)
                        {
                            this.EnableNotifications(CurrentNotifyCharacteristic);
                        }
                        return;
                    }
                    asyncLock = false;
                    st?.Invoke("设备连接状态" + status + "\r\n");
                }
            };
        }

        /// <summary>
        /// 接受到蓝牙数据
        /// </summary>
        private void Characteristic_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
            byte[] data;
            CryptographicBuffer.CopyToByteArray(args.CharacteristicValue, out data);
            Recdate?.Invoke(sender, data);
        }

    }



}
