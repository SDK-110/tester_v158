using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Sys
{
    public partial class USB2185RSV
    {
        // 函数FILE_Create()的参数nOptMode所用的文件操作方式(支持"或"指令实现多种方式并行操作)
        public const Int32 USB2185_FILE_OPTMODE_CREATE_NEW = 1;                 // 创建文件,如果文件存在则会出错
        public const Int32 USB2185_FILE_OPTMODE_CREATE_ALWAYS = 2;          // 不管文件是否存在，总是要被创建(即可能改写前一个文件)
        public const Int32 USB2185_FILE_OPTMODE_OPEN_EXISTING = 3;            // 打开必须已经存在的文件
        public const Int32 USB2185_FILE_OPTMODE_OPEN_ALWAYS = 4;              // 打开文件，若该文件不在，则创建它

        // 函数FILE_SetOffset()的参数nBaseMode所用的文件指针移动参考基点
        public const Int32 USB2185_FILE_BASEMODE_BEGIN = 0;         // 以文件起点作为参考点往右偏移
        public const Int32 USB2185_FILE_BASEMODE_CURRENT = 1;   // 以文件的当前位置作为参考点往左或往右偏移(nOffsetBytes<0时往左偏移，>0时往右偏移)
        public const Int32 USB2185_FILE_BASEMODE_END = 2;             // 以文件的尾部作为参考点往左偏移

        // 函数AUX_GetCPUTime的参数nUnitType所用的返回值单位类型
        public const Int32 USB2185_UNIT_TYPE_NS = 0;             // 返回纳秒时间
        public const Int32 USB2185_UNIT_TYPE_US = 1;             // 返回微秒时间
        public const Int32 USB2185_UNIT_TYPE_MS = 2;             // 返回毫秒时间
        public const Int32 USB2185_UNIT_TYPE_S = 3;                // 返回秒时间
        public const Int32 USB2185_UNIT_TYPE_M = 4;               // 返回分时间
        public const Int32 USB2185_UNIT_TYPE_H = 5;               // 返回小时时间
        public const Int32 USB2185_UNIT_TYPE_D = 6;               // 返回天时间
        public const Int32 USB2185_UNIT_TYPE_AUTO = 7;       // 自动单位(如果大于等于1天，则以天为单位，依此类推)

        // ################################ 保留的设备驱动接口申明 ################################
        [DllImport("USB2185.DLL")]
        public static extern Boolean USB2185_DEV_ConfigDialog(					// 设备配置对话框，就是通过对话框的窗口操作方式进行设备的若干配置
                                                                                    UInt32 nDeviceIdx);

        [DllImport("USB2185.DLL")]
        public static extern Boolean USB2185_DEV_Find(                                           // 查找系统中存在的设备，并建立相应的列表项
                                                                                Boolean[] bLgcIdxList,        // 存在于系统中的设备逻辑号, =NULL:表示不接收查找结果信息
                                                                                Boolean[] bPhysIdxList,      // 存在于系统中的设备物理号, =NULL:表示不接收查找结果信息
                                                                                Boolean[] bUsedList,          // 存在于系统中的设备是否被其他的应用程序使用, =NULL:表示不接收查找结果信息
                                                                                UInt32 nDemandCnt,          // 请求查找的设备数量，取值范围[1， 128]。以逻辑号0为起点，往后查找设备的数量, 比如=5，则仅查找逻辑号为0,1,2,3,4的设备
                                                                                ref UInt32 pFoundCnt);       // 返回实际已找到的设备数量，它也决定了列表List中的有效单元数量

        // ################################ 校准函数 ################################
        [DllImport("USB2185.DLL")]
        public static extern Boolean USB2185_AI_IsCaled(					                 // AI是否已被自我校准过, 如果已校准过，则返回TRUE,否则返回FALSE
                                                                                 IntPtr hDevice,             // 设备对象句柄,它由DEV_Create()函数创建
                                                                                 ref Boolean pCaled);    // 是否已经被校准，=TRUE:表示已经被校准过, =FALSE:表示未被校准过

        [DllImport("USB2185.DLL")]
        public static extern Boolean USB2185_AI_SelfCal(					// AI自我校准, 成功时返回TRUE,否则返回FALSE,可调用GetLastError()分析错误原因
                                                                                IntPtr hDevice);

        [DllImport("USB2185.DLL")]
        public static extern Boolean USB2185_AO_IsCaled(					                 // AO是否已被自我校准过, 如果已校准过，则返回TRUE,否则返回FALSE
                                                                                 IntPtr hDevice,             // 设备对象句柄,它由DEV_Create()函数创建
                                                                                 ref Boolean pCaled);    // 是否已经被校准，=TRUE:表示已经被校准过, =FALSE:表示未被校准过

        [DllImport("USB2185.DLL")]
        public static extern Boolean USB2185_AO_SelfCal(					// AO自我校准, 成功时返回TRUE,否则返回FALSE,可调用GetLastError()分析错误原因
                                                                                IntPtr hDevice);

        // ################################ 设备信息函数 ################################
        [DllImport("USB2185.DLL")]
        public static extern Boolean USB2185_DEV_GetPhysIdx(					               // 获得物理序号, 成功时返回TRUE,否则返回FALSE,可调用GetLastError()分析错误原因
                                                                                          IntPtr hDevice,           // 设备对象句柄,它由DEV_Create()函数创建
                                                                                          ref UInt32 pPhysIdx); // 返回的物理序号

        [DllImport("USB2185.DLL")]
        public static extern Boolean USB2185_DEV_SetPhysIdx(					               // 设置物理序号, 成功时返回TRUE,否则返回FALSE,可调用GetLastError()分析错误原因
                                                                                          IntPtr hDevice,           // 设备对象句柄,它由DEV_Create()函数创建
                                                                                          UInt32 nPhysIdx); // 返回的物理序号

        [DllImport("USB2185.DLL")]
        public static extern Boolean USB2185_DEV_GetVersion(					                       //  获得设备版本信息, 成功时返回TRUE,否则返回FALSE,可调用GetLastError()分析错误原因
                                                                                          IntPtr hDevice,                   // 设备对象句柄,它由DEV_Create()函数创建
                                                                                          ref UInt32 pDllVer,             // 返回的动态库(.dll)版本号
                                                                                          ref UInt32 pDriverVer,        // 返回的驱动(.sys)版本号
                                                                                          ref UInt32 pFirmwareVer);  // 返回的固件版本号

        [DllImport("USB2185.DLL")]
        public static extern Boolean USB2185_DEV_GetSerialNum(					                  // 获得序列号, 成功时返回TRUE,否则返回FALSE,可调用GetLastError()分析错误原因
                                                                                              IntPtr hDevice,              // 设备对象句柄,它由DEV_Create()函数创建
                                                                                              ref UInt32 pSerialNum); // 返回的序列号

        [DllImport("USB2185.DLL")]
        public static extern Boolean USB2185_DEV_GetUserPID(					                  // 获得用户产品ID号, 成功时返回TRUE,否则返回FALSE,可调用GetLastError()分析错误原因
                                                                                            IntPtr hDevice,                // 设备对象句柄,它由DEV_Create()函数创建
                                                                                            ref UInt32 pUserPID);     // 返回的用户产品ID

        [DllImport("USB2185.DLL")]
        public static extern Boolean USB2185_DEV_SetUserPID(					                      // 设置用户产品ID号, 成功时返回TRUE,否则返回FALSE,可调用GetLastError()分析错误原因
                                                                                            IntPtr hDevice,                // 设备对象句柄,它由DEV_Create()函数创建
                                                                                            UInt32 pUserPID);          // 用户产品ID

        [DllImport("USB2185.DLL")]
        public static extern Boolean USB2185_DEV_GetPowerMode(					                        // 取得设备的电源模式(Get Power Mode), 成功时返回TRUE,否则返回FALSE,可调用GetLastError()分析错误原因
                                                                                                IntPtr hDevice,                      // 设备对象句柄,它由DEV_Create()函数创建
                                                                                                ref UInt32 pPowerMode);     // 电源模式, =0:表示设备由USB总线供电, =FALSE:表示设备由外部电源供电(5)

        [DllImport("USB2185.DLL")]
        public static extern Boolean USB2185_DEV_SetPower5VState(					                        // 取得设备的电源模式(Get Power Mode), 成功时返回TRUE,否则返回FALSE,可调用GetLastError()分析错误原因
                                                                                                IntPtr hDevice,                      // 设备对象句柄,它由DEV_Create()函数创建
                                                                                                Boolean bEnable);                 // 允许输出, =TRUE:表示允许输出, =FALSE:表示禁止输出

        [DllImport("USB2185.DLL")]
        public static extern Boolean USB2185_DEV_GetPower5VState(					                     // 允许+5V电源输出(Enable +5V Power Output), 成功时返回TRUE,否则返回FALSE,可调用GetLastError()分析错误原因
                                                                                                IntPtr hDevice,                      // 设备对象句柄,它由DEV_Create()函数创建
                                                                                                ref Boolean bEnable,              // 是否已允许输出, =TRUE:表示已允许输出, =FALSE:表示已禁止输出
                                                                                                ref Boolean pOverload);         // 是否过载, =TRUE:表示过载, =FALSE:表示未过载（正常）

        // ############################# 文件函数(支持大于4GB文件读写) ##############################
        [DllImport("USB2185.DLL")]
        public static extern IntPtr USB2185_FILE_Create(					                                  // 根据指定文件名创建文件句柄(hFile), 成功时返回TRUE,否则返回FALSE,可调用GetLastError()分析错误原因
                                                                                    String strFileName,                      // 路径及文件名,如"C:\\ART\\SampleData.dat" 
                                                                                    Int32 nOptMode);                        // 文件操作模式，见上面相关常量定义

        [DllImport("USB2185.DLL")]
        public static extern UInt32 USB2185_FILE_Read(					                                  // 从指定文件中读取数据,返回实际读取的字节数, 成功时返回值大于0,否则返回值等于0,可调用GetLastError()分析错误原因
                                                                                 IntPtr hFile,                                   // 文件句柄,由FILE_Create()函数创建
                                                                                 IntPtr pDataBuffer,                       // 数据缓冲区，存放从文件读取的数据
                                                                                 UInt32 nSizeBytes);                       // 请求写入数据的字节数

        [DllImport("USB2185.DLL")]
        public static extern UInt32 USB2185_FILE_Write(					                                  // 向指定文件写入数据,返回实际写入的字节数, 成功时返回值大于0,否则返回值等于0,可调用GetLastError()分析错误原因
                                                                                 IntPtr hFile,                                   // 文件句柄,由FILE_Create()函数创建
                                                                                 IntPtr pDataBuffer,                       // 数据缓冲区，存放从文件读取的数据
                                                                                 UInt32 nSizeBytes);                       // 请求读取数据的字节数

        [DllImport("USB2185.DLL")]
        public static extern UInt64 USB2185_FILE_GetLength(					// 获取指定文件的长度(字节数), 成功时返回值大于0,否则返回值等于0,可调用GetLastError()分析错误原因
                                                                                           IntPtr hDevice);

        [DllImport("USB2185.DLL")]
        public static extern Boolean USB2185_FILE_SetOffset(					                                  // 向指定文件写入数据,返回实际写入的字节数, 成功时返回值大于0,否则返回值等于0,可调用GetLastError()分析错误原因
                                                                                         IntPtr hFile,                                   // 文件句柄,由FILE_Create()函数创建
                                                                                         Int64 nOffsetBytes,                       // 偏移位置(字节)
                                                                                         Int32 nBaseMode);                       // 参考基点模式，具体请参考上面的相关常量定义

        [DllImport("USB2185.DLL")]
        public static extern UInt64 USB2185_FILE_GetDiskFreeBytes(					               // 获取指定磁盘的剩余空间（字节数）,成功时返回值大于0,否则返回值等于0,可调用GetLastError()分析错误原因
                                                                                                      string strDiskName);  // 磁盘名称，如"C:\\", "D:\\"

        [DllImport("USB2185.DLL")]
        public static extern Boolean USB2185_FILE_Release(					    // 释放文件句柄
                                                                                       IntPtr hFile);  // 磁盘名称，如"C:\\", "D:\\"

        // ################################ 辅助函数 ################################
        [DllImport("USB2185.DLL")]
        public static extern Boolean USB2185_AUX_kbhit();  // 探测用户是否有键盘动作(在Console应用程序中有效)

        [DllImport("USB2185.DLL")]
        public static extern SByte USB2185_AUX_getch();  // 等待并获取用户键盘键值(在Console应用程序中有效)

        [DllImport("USB2185.DLL")]
        public static extern Boolean USB2185_AUX_DelayTime(UInt32 nMicrosecond);  // 微秒延时函数

        [DllImport("USB2185.DLL")]
        public static extern double USB2185_USB2185_AUX_GetCPUTime(Int32 nUnitType, ref Int32 pRetUnitType, ref SByte strUnitType);  // 获取CPU运行时间(1GHz的CPU，则可计584年，2GHz的CPU，则可计292年)

          }
}
