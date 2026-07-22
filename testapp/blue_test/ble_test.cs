using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Windows.Devices.Enumeration;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Security.Cryptography;
using Windows.Foundation;
namespace test_blue_dll
{
    public class test_blue
    {
        private BleCore bleCore = null;
        private BluetoothLEDevice test_dev = null;
        private volatile int find_flog = 0;
        private List<GattCharacteristic> characteristics = new List<GattCharacteristic>();
        private volatile string uuid = "0000ec0e-0000-1000-8000-00805f9b34fb";
        private volatile bool finduuid = false;
        GattCharacteristic gattCharacteristic;
        string device_name = "FiPy45";
        string device_MAC = "10:52:1c:65:df:5a";
        callback_chuandi Chuandi = null;
        StringBuilder builder = new StringBuilder();
        string result="";
        sendmessage st = null;

        public test_blue(string device_name_or_mac, int ismac = 1, sendmessage st =null)
        {
            characteristics.Clear();
            if (ismac == 0)
            {
                device_name = device_name_or_mac;//"FiPy 45";
                device_MAC = "";
            }
            else {
                device_MAC = device_name_or_mac;
                device_name = "";
            }
            
            
            find_flog = 0;

            bleCore = new BleCore();
            Chuandi = (o) => {
                bleCore.StartMatching(o);
                bleCore.FindService();
            };
            this.st = st;
            bleCore.st = st;
            bleCore.DeviceWatcherChanged += DeviceWatcherChanged;
            bleCore.CharacteristicAdded += CharacteristicAdded;
            bleCore.CharacteristicFinish += CharacteristicFinish;
            bleCore.Recdate += Recdata;
            bleCore.StartBleDeviceWatcher();
        }
       ~test_blue()
        {
            
            if(bleCore!=null)disConnectDevice();
        }
   

        private void printmsg(string a)
        {
            try
            {
           //////
            }
            catch { }
        }

        private void CharacteristicFinish(int size)
        {
            if (size > 0)
            {
                st?.Invoke($"rev_char{size}");

                return;
            }
            else
            {
                st?.Invoke($"no_found_char");


            };
        }

        private void Recdata(GattCharacteristic sender, byte[] data)
        {
            string str = System.Text.ASCIIEncoding.ASCII.GetString(data);

            result = str;
            st?.Invoke(sender.Uuid + "  " + str + "\r\n");


        }

        private void CharacteristicAdded(GattCharacteristic gatt)
        {

            st?.Invoke($"handle:[0x{gatt.AttributeHandle.ToString("X4")}]  char properties:[{gatt.CharacteristicProperties.ToString()}]  UUID:[{gatt.Uuid}]" + "\r\n");

            if (gatt.Uuid == new Guid(uuid)) { finduuid = true; }
            else { finduuid = false; }
            characteristics.Add(gatt);


        }



        private GattCharacteristic get_gattchar(string uuid)
        {

            GattCharacteristic gattCharacteristic = characteristics.Find((x) => { return x.Uuid.Equals(new Guid(uuid)); });

            if (gattCharacteristic != null) return gattCharacteristic;

            return null;
        }

        private void DeviceWatcherChanged(BluetoothLEDevice currentDevice, int signed)
        {
            byte[] _Bytes1 = BitConverter.GetBytes(currentDevice.BluetoothAddress);
            Array.Reverse(_Bytes1);
            string address = BitConverter.ToString(_Bytes1, 2, 6).Replace('-', ':').ToLower();

            st?.Invoke("发现设备：<" + currentDevice.Name + ">  address:<" + address + ">" + signed + "\r\n");

            if (currentDevice.Name == device_name || address == device_MAC)
            {
                builder.Clear();
                //  bleCore.StopBleDeviceWatcher();
                if (Chuandi != null) Chuandi(currentDevice);

            }

            //指定一个对象，使用下面方法去连接设备
            //ConnectDevice(currentDevice);
        }

        private void disConnectDevice()
        {


            bleCore.Dispose();





        }

        public void dis_conn_device()
        {
            device_name = "FiPy45";
            device_MAC = "";

            disConnectDevice();


        }

        public  void get_rx_date(string uuid = "00002a00-0000-1000-8000-00805f9b34fb")
        {
      

                while (!(bleCore.is_get_ser)) { System.Threading.Thread.Sleep(100); }
                gattCharacteristic = get_gattchar(uuid);

                bleCore.SetOpteron(gattCharacteristic);
                var m = bleCore.get_value("UTF8");


      
           

        }

    }

}
