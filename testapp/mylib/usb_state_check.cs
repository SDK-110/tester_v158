using NativeUsbLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NativeUsbLib;
namespace testapp.mylib
{
    internal  static class usb_state_check
    {
        static string  rsult = "";
       public static string  usb_check()
            {
            rsult = "";
            UsbBus usbBus = new UsbBus();

                usbBus.Refresh();

                foreach (var controller in usbBus.Controller)
                {
                    Console.WriteLine("Controller: " + controller.GetDescription());

                    foreach (var hub in controller.Hubs)
                    {
                       CheckHub(hub);
                    }
                   
            }

            return rsult;
        }

            static void CheckHub(UsbHub hub)
            {
            
                if (hub == null)
                    return;

                try
                {
                    // HUB 也是一种 Device
                    var desc = hub.DeviceDescriptor;

                    if (desc != null)
                    {
                        ushort vid = desc.idVendor;
                        ushort pid = desc.idProduct;

                    //if (vid == 0x05E3 && pid == 0x610)
                    if (vid == 0x0451 && pid == 0x8442)
                    {
                        utility_func.callbackdebuginfo("Found Target HUB");
                        utility_func.callbackdebuginfo($"VID:PID = {vid:X4}:{pid:X4}");

                        byte mask = hub.HubInformation
                                       .UsbHubDescriptor
                                       .RemoveAndPowerMask[0];

                        rsult = ($"DeviceRemovable = 0x{mask:X2}");
                        utility_func.callbackdebuginfo(rsult);
                        return;
                    }
                   
                    
                    
                   
                    }
                }
                catch
                {
                }

                // 继续递归子设备
                foreach (var device in hub.ChildDevices)
                {
                    if (device is UsbHub subHub)
                        CheckHub(subHub);
                }
            }
        }














    
}
