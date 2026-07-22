using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using  System.Runtime.InteropServices;
namespace test_temp
{
    public partial class Form1 : Form
    {


    
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct VCI_BOARD_INFO
        {

            /// USHORT->unsigned short
            public ushort hw_Version;

            /// USHORT->unsigned short
            public ushort fw_Version;

            /// USHORT->unsigned short
            public ushort dr_Version;

            /// USHORT->unsigned short
            public ushort in_Version;

            /// USHORT->unsigned short
            public ushort irq_Num;

            /// BYTE->unsigned char
            public byte can_Num;

            /// CHAR[20]
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string str_Serial_Num;

            /// CHAR[40]
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string str_hw_Type;

            /// USHORT[4]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U2)]
            public ushort[] Reserved;
        }
     
        [DllImportAttribute("ControlCAN.dll", EntryPoint = "VCI_ReadBoardInfo", CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_ReadBoardInfo(uint DevType, uint DevIndex, ref VCI_BOARD_INFO pInfo);

        //字符串数组传入传出参数
        [DllImportAttribute("MFCtest.dll", EntryPoint = "ShowDlg",CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
       
        public static extern int ShowDlg([In, Out]string [] p ,int z);

        //字符串传入参数
        [DllImport("MFCtest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Str_Output([MarshalAs(UnmanagedType.LPWStr)]string pStr);

        //传出字符串参数
        [DllImport("MFCtest.dll", EntryPoint = "Str_Change", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        // public static extern int Str_Change([MarshalAs(UnmanagedType.LPWStr)]StringBuilder pStr, int len);
        public static extern int Str_Change([MarshalAs(UnmanagedType.LPWStr)]StringBuilder  pStr, int len);

        //返回值是字符串  string strIntPtr = Marshal.PtrToStringUni(strPtr);
        [DllImport("MFCtest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Str_Return();


        //EXTERN_C TESTCPPDLL_API void __stdcall AddInt(int* i);

        ////传入一个整型数组的指针以及数组长度，遍历每一个元素并且输出
        //EXTERN_C TESTCPPDLL_API void __stdcall AddIntArray(int* firstElement, int arraylength);

        ////在C++中生成一个整型数组，并且数组指针返回给C#
        //EXTERN_C TESTCPPDLL_API int* __stdcall GetArrayFromCPP();

        ////TestCPPDLL.cpp中，代码如下所示：

        //TESTCPPDLL_API void __stdcall AddInt(int* i)
        //{
        //    (*i)++;
        //}

        //TESTCPPDLL_API void __stdcall AddIntArray(int* firstElement, int arrayLength)
        //{
        //    int* currentPointer = firstElement;
        //    for (int i = 0; i < arrayLength; i++)
        //    {
        //        cout << *currentPointer;
        //        currentPointer++;
        //    }
        //    cout << endl;
        //}

        //int* arrPtr;
        //TESTCPPDLL_API int* __stdcall GetArrayFromCPP()
        //{
        //    arrPtr = new int[10];

        //    for (int i = 0; i < 10; i++)
        //    {
        //        arrPtr[i] = i;
        //    }
        //    return arrPtr;
        //}

        [DllImport(@"D:\work\Interop\DLLDir\TestCPPDLL.dll", EntryPoint = "AddInt")]
        extern static void AddInt(ref int i);

        [DllImport(@"D:\work\Interop\DLLDir\TestCPPDLL.dll", EntryPoint = "AddIntArray")]
        extern static void AddIntArray(ref int firstElement, int arraylength);

        [DllImport(@"D:\work\Interop\DLLDir\TestCPPDLL.dll", EntryPoint = "GetArrayFromCPP")]
        extern static IntPtr GetArrayFromCPP();


        //        typedef struct _DEMOSTRUCT
        //        {
        //            int a;
        //            short b;
        //            float c;
        //            double d;
        //        }
        //        DEMOSTRUCT, *pDEMOSTRUCT;

        //EXTERN_C TESTCPPDLL_API void __stdcall Func(pDEMOSTRUCT p_demoStruct);


        //        TESTCPPDLL_API void __stdcall Func(pDEMOSTRUCT p_demoStruct)
        //        {
        //            printf("got struct in cpp, int = %d, short = %d,   float = $f, double = %f \n",
        //                p_demoStruct->a,
        //                p_demoStruct->b,
        //                p_demoStruct->c,
        //                p_demoStruct->d
        //            );

        //            p_demoStruct->a += 10;
        //        }

        private struct ManagedDemoStruct
        {
            public int a;
            public short b;
            public float c;
            public double d;
        }
        [DllImport("TestCPPDLL.dll", EntryPoint = "Func")]
        private extern static void myFunc(ref ManagedDemoStruct argStruct); //使用指针传递参数要声明ref

        //调用过程如下所示：

        //ManagedDemoStruct demoStruct = new ManagedDemoStruct();
        //demoStruct.a = 10;
        //    demoStruct.b = 20;
        //    demoStruct.c = 3.5f;
        //    demoStruct.d = 6.8f;
        //    myFunc(ref demoStruct);

        //struct CXTest
        //{
        //    LPBYTE pData;     // 一个指向byte数组的指针
        //    int nLen;         // 数组的长度
        //}
        //BOOL WINAPI XFunction(const CXTest &inData_, CXTest &outData_);

        struct CXTest
        {
            public IntPtr pData;
            public int nLen;
        };
        static extern bool XFunction( [In] ref CXTest inData_, ref CXTest outData_);


        //调用过程如下所示：

        //设数组长度为nDataLen
        //CXTest stIn = new CXTest(), stOut = new CXTest();
        //byte[] pIn = new byte[nDataLen];
        // 为数组赋值
        //        stIn.pData = Marshal.AllocHGlobal(nDataLen);
        //Marshal.Copy(pIn, 0, stIn.pData, nDataLen);
        //stIn.nLen = nDataLen;
        //stOut.pData = Marshal.AllocHGlobal(nDataLen);
        //stOut.nLen = nDataLen;
        //XFunction(ref stIn, ref stOut);
        //        byte[] pOut = new byte[nDataLen];
        //        Marshal.Copy(stOut.pData, pOut, 0, nDataLen);
        //// ....
        //Marshal.FreeHGlobal(stIn.pData);
        //Marshal.FreeHGlobal(stOut.pData);



        //struct CXTest
        //{
        //    WCHAR wzName[64];
        //    int nLen;
        //    byte byData[100];
        //};
        //bool SetTest(const CXTest &stTest_);

        [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Unicode)]
        class CXTestw
        {
            public void Init()
            {
                strName = "";
                nLen = 0;
                byData = new byte[100];
            }
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string strName;
            public int nLen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
            public byte[] byData;
        }
        [DllImport("tt",CallingConvention= CallingConvention.Cdecl,CharSet = CharSet.Ansi)]
        static extern bool SetTest(CXTest stTest_);


        //使用IntPtr接收时，需要手动释放
        [DllImport("MFCtest.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "Str_ParameterOut")]
        public static extern void Str_ParameterOuttPtr(ref IntPtr ppStr);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct testStru1
        {
            public int iVal;
            public sbyte cVal;
            public long llVal;
        };
        //参数是普通结构体，
        [DllImport("MFCtest.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void Struct_Change(ref testStru1 pStru);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct testStru5
        {
            public int iVal;
        };

        //返回值是结构体  testStru5 stru5 = (testStru5)(Marshal.PtrToStructure(struIntPtr, typeof(testStru5)));
        [DllImport("MFCtest.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr Struct_Return();
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct testStru6
        {
            public int iVal;
        };
        //传入的结构体数组 
        [DllImport("MFCtest.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void Struct_StruArr([In, Out]testStru6[] pStru, int len);
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct testStru8
        {
            public int m;
        };

        //传入的是指针结构体
        //Struct_ParameterOut(ref outPtr);
        //testStru8 stru8 = (testStru8)(Marshal.PtrToStructure(outPtr, typeof(testStru8)));
        //Marshal.FreeCoTaskMem(outPtr);
        [DllImport("MFCtest.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void Struct_ParameterOut(ref IntPtr ppStru);

        //结构体内是数组，数组长度固定
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct testStru3
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
            public int[] iValArrp;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string szChArr;
        };
        [DllImport("MFCtest.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void Struct_ChangeArr(ref testStru3 pStru);
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct testStru7Pre
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public int[] m;
        };
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct testStru7
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public testStru7Pre[] m;
        };
        [DllImport("MFCtest.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void Struct_Change2DArr(ref testStru7 pStru);
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct testStru9
        {
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pWChArr;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pChArr;
            [MarshalAs(UnmanagedType.U1)]
            public bool IsCbool;
            [MarshalAs(UnmanagedType.Bool)]
            public bool IsBOOL;
        };
        [DllImport("MFCtest.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void Struct_ChangePtr(ref testStru9 pStru);

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
        public struct _testStru2
        {
            public int iVal;
            public sbyte cVal;
            public long llVal;
        };
        [DllImport("MFCtest.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void Struct_PackN(ref _testStru2 pStru);
        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
        public struct testStru4
        {
            [FieldOffset(0)]
            int iValLower;
            [FieldOffset(4)]
            int iValUpper;
            [FieldOffset(0)]
            long llLocation;
        };
        [DllImport("MFCtest.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void Struct_Union(ref testStru4 pStru);

        [DllImport("MFCtest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern void  ShowDlg_QQ([MarshalAs(UnmanagedType.U4)]ref int a);

        //回掉函数调用
        public delegate void CSCallback(int tick);
        [DllImport("MFCtest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void SetCallback(CSCallback a);

        public enum BeepType
        {
            SimpleBeep = -1,
            IconAsterisk = 0x00000040,
            IconExclamation = 0x00000030,
            IconHand = 0x00000010,
            IconQuestion = 0x00000020,
            Ok = 0x00000000,
        }
        [DllImport("user32.dll")]
        public static extern bool MessageBeep(BeepType beepType);
       public  struct CXTest1 {
            public IntPtr pData;
            public int nLen;

        }

        [DllImport("MFCtest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern bool XFunction([In] ref  CXTest1 inData_, ref CXTest1 outData_);

        [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Unicode)]
        public class CXTest2
        {

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string strName;
            public int nLen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
            public byte[] byData;

            public void Init()
            {
                strName = "";
                nLen = 0;
                byData = new byte[100];
            }
        };
        [DllImport("MFCtest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern  bool SetTest2(CXTest2 stTest_);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]//可以指定编码类型   
        public struct UIM_BOOK_STRUCT
        {
            public int UimIndex;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
            public string szName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
            public string szPhone;
        };

        [DllImport("MFCtest.dll", EntryPoint = "ReadUimAllBook")]
        public static extern int ReadUimAllBook([Out] UIM_BOOK_STRUCT[] lpUimBookItem, int nMaxArraySize);
       


        VCI_BOARD_INFO a;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            Str_Output("fffffffffffff");


            return;



              UIM_BOOK_STRUCT[] p = new UIM_BOOK_STRUCT[20];
            int ret = ReadUimAllBook(p, p.Length);

            int nDataLen = 100;
            CXTest stIn = new CXTest(), stOut = new CXTest();
            byte[] pIn = new byte[nDataLen];
            // 为数组赋值
            stIn.pData = Marshal.AllocHGlobal(nDataLen);
            Marshal.Copy(pIn, 0, stIn.pData, nDataLen);
            stIn.nLen = nDataLen;
            stOut.pData = Marshal.AllocHGlobal(nDataLen);
            stOut.nLen = nDataLen;
            XFunction(ref stIn, ref stOut);
            byte[] pOut = new byte[nDataLen];
            Marshal.Copy(stOut.pData, pOut, 0, nDataLen);
            // ....
            Marshal.FreeHGlobal(stIn.pData);
            Marshal.FreeHGlobal(stOut.pData);






            MessageBeep(BeepType.Ok);


          return;









            SetCallback((o) =>
            {
                MessageBox.Show("Test" + o);

            });




        
            string[] strArr = new string[4] {new string('\0', 10),
                                 new string('\0', 10),
                                 new string('\0', 10),
                                 new string('\0', 10) };
            ShowDlg(strArr, 4);
           
            string str = "hjkl;";
            Str_Output(str);
        //    StringBuilder a = new StringBuilder() ;
         //   Str_Change(a, 5);

           IntPtr strPtr = Str_Return();
          string strIntPtr = Marshal.PtrToStringUni(strPtr);

            //手动释放
            IntPtr strOutIntPtr = IntPtr.Zero;
            Str_ParameterOuttPtr(ref strOutIntPtr);
            string strOut2 = Marshal.PtrToStringUni(strOutIntPtr);
            Marshal.FreeCoTaskMem(strOutIntPtr);


           testStru1 stru1 = new testStru1();
           Struct_Change(ref stru1);
            //返回结构指针
            IntPtr struIntPtr = Struct_Return();
            testStru5 stru5 = (testStru5)(Marshal.PtrToStructure(struIntPtr, typeof(testStru5)));

            testStru6[] stru6 = new testStru6[5];
            Struct_StruArr(stru6, 5);

            IntPtr outPtr = IntPtr.Zero;
            Struct_ParameterOut(ref outPtr);
            testStru8 stru8 = (testStru8)(Marshal.PtrToStructure(outPtr, typeof(testStru8)));
            Marshal.FreeCoTaskMem(outPtr);

            testStru3 stru3 = new testStru3();
            Struct_ChangeArr(ref stru3);

            testStru7 stru7 = new testStru7();
            Struct_Change2DArr(ref stru7);

            testStru9 stru9 = new testStru9();
            Struct_ChangePtr(ref stru9);

            _testStru2 stru2 = new _testStru2();
            Struct_PackN(ref stru2);



        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
