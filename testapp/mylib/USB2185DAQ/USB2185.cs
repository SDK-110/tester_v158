using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Sys
{
    public partial class USB2185
    {
        // ########################## 设备功能指标 ########################
        public const Int32 USB2185_AI_MAX_CHANNELS = 32;        // 本卡最多支持16路模拟量单端输入通道
        public const Int32 USB2185_AO_MAX_CHANNELS = 4;        // 本卡最多支持2个模拟量输出通道
        public const Int32 USB2185_DIO_MAX_PORTS = 2;               // 本卡最多支持2个数字量端口
        public const Int32 USB2185_DIO_PORT0_MAX_LINES = 8;   // 数字量端口0支持8条线
        public const Int32 USB2185_DIO_PORT1_MAX_LINES = 8;   // 数字量端口1支持8条线

        // ############# AI工作参数结构体USB2185_AI_PARAM描述 #############
        // AI通道参数结构体
        public struct USB2185_AI_CH_PARAM
        {
            public UInt32 nChannel;            // 通道号[0, 15]，分别表示物理通道号AI0－AI15
            public UInt32 nSampleRange;		// 采样范围(Sample Range)档位选择
            public UInt32 nRefGround;        // 接地参考方式

            public UInt32 nReserved0;        // 保留(未用)
            public UInt32 nReserved1;        // 保留(未用)
            public UInt32 nReserved2;        // 保留(未用)
        }
        // AI硬件通道参数USB2185_AI_CH_PARAM中的nSampleGain模拟量增益放大倍数所使用的选项
        public const Int32 USB2185_AI_SAMPRANGE_N10_P10V = 0; // 1倍增益
        public const Int32 USB2185_AI_SAMPRANGE_N5_P5V = 1; // 2倍增益
        public const Int32 USB2185_AI_SAMPRANGE_N2_P2V = 2; // 4倍增益
        public const Int32 USB2185_AI_SAMPRANGE_N1_P1V = 3; // 8倍增益
     
        // AI硬件通道参数结构体USB2185_AI_CH_PARAM中的nRefGround参数所使用的地参考选项
        public const Int32 USB2185_AI_REFGND_RSE = 0;      // 接地参考单端(Referenced Single Endpoint)
        public const Int32 USB2185_AI_REFGND_NRSE = 1;    // 非参考单端(Non Referenced Single Endpoint)
        public const Int32 USB2185_AI_REFGND_DIFF = 2;      // 差分(Differential)

        // 跟整个AI有关的硬件工作参数(简称AI工作参数)
        public struct USB2185_AI_PARAM
        {
            // 通道参数
            public UInt32 nSampChanCount;                          // 采样通道总数[1, 16](此参数在单点采样有效，未此注明的参数则在单点采样模式中无效)
            public USB2185_AI_CH_PARAM CHParam0;   // 通道参数配置(此参数在单点采样有效，未此注明的参数则在单点采样模式中无效)
            public USB2185_AI_CH_PARAM CHParam1;
            public USB2185_AI_CH_PARAM CHParam2;
            public USB2185_AI_CH_PARAM CHParam3;
            public USB2185_AI_CH_PARAM CHParam4;
            public USB2185_AI_CH_PARAM CHParam5;
            public USB2185_AI_CH_PARAM CHParam6;
            public USB2185_AI_CH_PARAM CHParam7;
            public USB2185_AI_CH_PARAM CHParam8;
            public USB2185_AI_CH_PARAM CHParam9;
            public USB2185_AI_CH_PARAM CHParam10;
            public USB2185_AI_CH_PARAM CHParam11;
            public USB2185_AI_CH_PARAM CHParam12;
            public USB2185_AI_CH_PARAM CHParam13;
            public USB2185_AI_CH_PARAM CHParam14;
            public USB2185_AI_CH_PARAM CHParam15;
			 public USB2185_AI_CH_PARAM CHParam16;   // 通道参数配置(此参数在单点采样有效，未此注明的参数则在单点采样模式中无效)
            public USB2185_AI_CH_PARAM CHParam17;
            public USB2185_AI_CH_PARAM CHParam18;
            public USB2185_AI_CH_PARAM CHParam19;
            public USB2185_AI_CH_PARAM CHParam20;
            public USB2185_AI_CH_PARAM CHParam21;
            public USB2185_AI_CH_PARAM CHParam22;
            public USB2185_AI_CH_PARAM CHParam23;
            public USB2185_AI_CH_PARAM CHParam24;
            public USB2185_AI_CH_PARAM CHParam25;
            public USB2185_AI_CH_PARAM CHParam26;
            public USB2185_AI_CH_PARAM CHParam27;
            public USB2185_AI_CH_PARAM CHParam28;
            public USB2185_AI_CH_PARAM CHParam29;
            public USB2185_AI_CH_PARAM CHParam30;
            public USB2185_AI_CH_PARAM CHParam31;
            public UInt32 nSampleSignal;                                 // 采样信号(Sample Signal), 详见下面常量定义
            public UInt32 nReserved0;                                     //  保留字段(暂未定义) 
            public UInt32 nReserved1;                                     //  保留字段(暂未定义) 
           
            // 时钟参数
            public UInt32 nSampleMode;                                  // 采样模式, 0=单点采样(按需)， 1、硬件定时单点(暂不支持)，2=有限点采样， 3=连续采样
            public UInt32 nSampsPerChan;                               // 每个通道采样点数(也是每通道待读取点数),取值范围为单点采样模式时应等于1，有限点或连续采样时为[2, 1024*1024]
            public Double fSampleRate;                                    // 采样速率(Sample Rate), 单位为sps，取值范围为[1sps, 100000sps],它是每个采样通道的采样速率, 它与nChannelCount的乘积不能大于100000sps
            public UInt32 nClockSource;                                   // 时钟源选择, =0:内时钟OSCCLK; =1:外时钟CLKIN,由CN2上的Port1.DIO2(CLKIN)复用输入
            public UInt32 bClockOutput;                                   // 采样时钟输出允许, =0:表示禁止; =1:表示允许,由CN2上的Port1.DIO3(CLKOUT)复用输出
            public UInt32 nReserved2;                                      // 保留字段(暂未定义)
            public UInt32 nReserved3;                                     // 保留字段(暂未定义)


            // 开始触发参数
            public UInt32 bDTriggerEn;                                     // 数字触发允许(Digital Trigger Enable), =FALSE:表示禁止; =TRUE:表示允许,触发信号由CN2上的Port1.DIO1(DTR)复用输入，因此初始化时会将DIO1的方向强制置为输入
            public UInt32 nDTriggerDir;                                    // 数字触发方向(Digital Trigger Direction)
            public UInt32 bATriggerEn;                                     // 模拟量触发允许(Analog Trigger Enable), =TRUE:表示允许, =FALSE:表示禁止
            public UInt32 nATriggerDir;                                    // 模拟触发方向(Analog Trigger Direction)
            public UInt32 nATrigChannel;                                  // 触发通道(Analog Trigger Channel)
            public float fTriggerLevel;                                       // 触发电平(Trigger Level)
            public UInt32 nTriggerSens;                                     // 触发灵敏度(Trigger Sensitive for Digital and Analog trigger),单位：微秒(uS)，取值范围[0, 1638]
            public UInt32 nDelaySamps;                                    // 触发延迟点数, 单位：采样点，取值范围32位有效[0, 4294967295]，0:Post Trigger, >0:Delay Trigger
            public UInt32 nReserved4;                                       // 保留字段(暂未定义)
            public UInt32 nReserved5;                                       // 保留字段(暂未定义)

            // 其他方面
            public UInt32 nReserved6;                                       // 保留字段(暂未定义)
            public UInt32 nReserved7;                                       // 保留字段(暂未定义)
            public UInt32 nReserved8;                                       // 保留字段(暂未定义)
            public UInt32 nReserved9;                                       // 保留字段(暂未定义)
        }

        // AI硬件参数结构体USB2185_AI_PARAM中的nSampleSignal采样信号所使用的选项
        public const Int32 USB2185_AI_SAMPSIGNAL_AI = 0;                  // AI通道输入信号
        public const Int32 USB2185_AI_SAMPSIGNAL_0V = 1;                  // 0V(AGND)
        public const Int32 USB2185_AI_SAMPSIGNAL_4D096V = 2;          // 2.5V
        public const Int32 USB2185_AI_SAMPSIGNAL_N4D096V = 3;       // -2.5V
        public const Int32 USB2185_AI_SAMPSIGNAL_AO0 = 4;               // AO0
        public const Int32 USB2185_AI_SAMPSIGNAL_NAO0 = 5;            // -AO0
        public const Int32 USB2185_AI_SAMPSIGNAL_AO1 = 6;               // AO1
        public const Int32 USB2185_AI_SAMPSIGNAL_NAO1 = 7;            // -AO1
		public const Int32 USB2185_AI_SAMPSIGNAL_AO2 = 8;              // AO2
		public const Int32 USB2185_AI_SAMPSIGNAL_NAO2 = 9;                // -AO2
		public const Int32 USB2185_AI_SAMPSIGNAL_AO3 = 10 ;            // AO3
        public const Int32 USB2185_AI_SAMPSIGNAL_NAO3 = 11;             // -AO3

        // AI硬件参数结构体USB2185_AI_PARAM中的nSampleMode采样模式所使用的选项
        public const Int32 USB2185_AI_SAMPMODE_ONE_DEMAND = 0;  // 软件按需单点采样
        public const Int32 USB2185_AI_SAMPMODE_FINITE = 2;      // 有限点采样
        public const Int32 USB2185_AI_SAMPMODE_CONTINUOUS = 3;  // 连续采样

        // AI硬件参数结构体USB2185_AI_PARAM中的nClockSource时钟源所使用的选项
        public const Int32 USB2185_AI_CLKSRC_LOCAL = 0;     // 本地时钟(通常为本地晶振时钟OSCCLK),也叫内部时钟
        public const Int32 USB2185_AI_CLKSRC_CLKIN = 1;      // 外部时钟(由连接器CN2上的DIO2/CLKIN复用输入)

        // AI硬件参数结构体USB2185_AI_PARAM中的nDTriggerDir/nATriggerDir触发方向所使用的选项
        public const Int32 USB2185_AI_TRIGDIR_FALLING = 0;    // 下降沿/低电平
        public const Int32 USB2185_AI_TRIGDIR_RISING = 1;        // 上升沿/高电平
        public const Int32 USB2185_AI_TRIGDIR_CHANGE = 2;     // 变化(即上下边沿/高低电平均有效)

        // #################### AI工作状态结构体USB2185_AI_STATUS描述 #####################
        public struct USB2185_AI_STATUS
        {
            public UInt32 bTaskDone;				// 采集任务完成标志, =TRUE:表示采集任务完成, =FALSE:表示采集任务未完成，正在进行中
            public UInt32 bTriggered;				// 触发标志, =TRUE:表示已被触发, =FALSE:表示未被触发(即正等待触发)

            public UInt32 nTaskState;				// 采集任务状态, =1:正常, 其它值表示有异常情况
            public UInt32 nAvailSampsPerChan;		// 每通道有效点数，只有它大于当前指定读数长度时才能调用AI_ReadAnalog()立即读取指定长度的采样数据
            public UInt32 nMaxAvailSampsPerChan;	// 自AI_StartTask()后每通道出现过的最大有效点数，状态值范围[0, nBufSampsPerChan],它是为监测采集软件性能而提供，如果此值越趋近于1，则表示意味着性能越高，越不易出现溢出丢点的可能
            public UInt32 nBufSampsPerChan;		// 每通道缓冲区大小(采样点数)。
            public long nSampsPerChanAcquired;	// 每通道已采样点数(自开始采集任务(AI_StartTask())之后所采样的点数)，这个只是给用户的统计数据

            public UInt32 nHardOverflowCnt;		// 硬件溢出计数(在不溢出情况下恒等于0)
            public UInt32 nSoftOverflowCnt;		// 软件溢出计数(在不溢出情况下恒等于0)
            public UInt32 nInitTaskCnt;			// 初始化采集任务的次数(即调用AI_InitTask()的次数)
            public UInt32 nReleaseTaskCnt;		// 释放采集任务的次数(即调用AI_ReleaseTask()的次数)
            public UInt32 nStartTaskCnt;			// 开始采集任务的次数(即调用AI_StartTask()的次数)
            public UInt32 nStopTaskCnt;			// 停止采集任务的次数(即调用AI_StopTask()的次数)
            public UInt32 nTransRate;				// 传输速率, 即每秒传输点数(sps)，作为USB及应用软件传输性能的监测信息

            public UInt32 nReserved0;                                      // 保留字段(暂未定义)
            public UInt32 nReserved1;                                      // 保留字段(暂未定义)
            public UInt32 nReserved2;                                      // 保留字段(暂未定义)
            public UInt32 nReserved3;                                      // 保留字段(暂未定义)
            public UInt32 nReserved4;                                      // 保留字段(暂未定义)
        }

        // #################### AI主要信息结构体(USB2185_AI_MAIN_INFO) #######################
        public struct USB2185_AI_MAIN_INFO
        {
            public UInt32 nChannelCount;		// AI通道数量
            public UInt32 nSampRangeCount;	// AI采样范围挡位数量
            public UInt32 nSampGainCount;		// AI采样增益挡位数量
            public UInt32 nCouplingCount;		// AI耦合挡位数量
            public UInt32 nImpedanceCount;	// AI阻抗的挡位数量
            public UInt32 nDepthOfMemory;		// AI板载存储器深度(点数)
            public UInt32 nSampResolution;	// AI采样分辨率(如=8表示8Bit; =12表示12Bit; =14表示14Bit; =16表示16Bit)
            public UInt32 nSampCodeCount;		// AI采样编码数量(如256, 4096, 16384, 65536)
            public UInt32 nTrigLvlResolution;	// 触发电平分辨率(如=8表示8Bit; =12表示12Bit; =16表示16Bit)
            public UInt32 nTrigLvlCodeCount;	// 触发电平编码数量(如256, 4096)

            public UInt32 nReserved0;                          // 保留字段(暂未定义)
            public UInt32 nReserved1;                          // 保留字段(暂未定义)
            public UInt32 nReserved2;                          // 保留字段(暂未定义)
            public UInt32 nReserved3;                          // 保留字段(暂未定义)
        }

        // #################### AI采样范围信息结构体(USB2185_AI_VOLT_RANGE_INFO) #######################
        public struct USB2185_AI_VOLT_RANGE_INFO
        {
            public UInt32 nSampleRange;                 // 当前采样范围挡位号
            public UInt32 nReserved0;                      // 保留字段(暂未定义)
            public double fMaxVolt;                          // 采样范围的最大电压值,单位:伏(V)
            public double fMinVolt;                           // 采样范围的最小电压值,单位:伏(V)
            public double fAmplitude;                       // 采样范围幅度,单位:伏(V)
            public double fHalfOfAmp;                    // 采样范围幅度的二分之一,单位:伏(V)
            public double fCodeWidth;                     // 编码宽度,单位:伏(V), 即每个单位码值所分配的电压值
            public double fOffsetVolt;                      // 偏移电压,单位:伏(V),一般用于零偏校准
            public double fOffsetCode;                    // 偏移码值,一般用于零偏校准,它代表的电压值等价于fOffsetVolt
            public SByte strDesc0;                            // 采样范围字符描述,如"±10V", "0-10V"等
            public SByte strDesc1;
            public SByte strDesc2;
            public SByte strDesc3;
            public SByte strDesc4;
            public SByte strDesc5;
            public SByte strDesc6;
            public SByte strDesc7;
            public SByte strDesc8;
            public SByte strDesc9;
            public SByte strDesc10;
            public SByte strDesc11;
            public SByte strDesc12;
            public SByte strDesc13;
            public SByte strDesc14;
            public SByte strDesc15;

            public UInt32 nPolarity;                       // 采样范围的极性(0=双极性BiPolar, 1=单极性UniPolar)
            public UInt32 nCodeCount;                  // 原码数量
            public Int32 nMaxCode;                       // 原码最大值
            public Int32 nMinCode;                        // 原码最小值

            public UInt32 nReserved1;                   // 保留字段(暂未定义)
            public UInt32 nReserved2;                   // 保留字段(暂未定义)
            public UInt32 nReserved3;                   // 保留字段(暂未定义)
            public UInt32 nReserved4;                   // 保留字段(暂未定义)
        }

        // 结构体AI_VOLT_RANGE_INFO的数据成员Polarity所用的采样范围极性选项
        public const Int32 USB2185_AI_POLAR_BIPOLAR = 0;        // 双极性
        public const Int32 USB2185_AI_POLAR_UNIPOLAR = 1;     // 单极性

        // #################### AI速率信息结构体(USB2185_AI_SAMP_RATE_INFO) #######################
        public struct USB2185_AI_SAMP_RATE_INFO
        {
            public double fMaxRate;             // 单通道最大采样速率(sps)，多通道时各通道平分最大采样率
            public double fMinRate;              // 单通道最小采样速率(sps)，多通道时各通道平分最小采样率
            public double fTimerBase;           // 时钟基准（即板载晶振频率）单位：Hz
            public UInt32 nDivideMode;        // 分频模式，0=整数分频(INTDIV), 1=DDS分频(DDSDIV)
            public UInt32 nRateType;            // 速率类型,指fMaxRate和fMinRate的类型, =0:表示为所有采样通道的总速率, =1:表示为每个采样通道的速率

            public UInt32 nReserved0;           // 保留字段(暂未定义)
            public UInt32 nReserved1;           // 保留字段(暂未定义)
        }

        // ##################### AO工作参数结构体USB2185_AO_PARAM描述 ###################
        public struct USB2185_AO_CH_PARAM
        {
            public UInt32 bChannelEn;        // 通道采样允许
            public UInt32 nSampleRange;    // 采样范围，具体定义请参考下面常量定义
            public UInt32 nReserved0;         // 保留(未用)
            public UInt32 nReserved1;         // 保留(未用)
            public UInt32 nReserved2;         // 保留(未用)
            public UInt32 nReserved3;         // 保留(未用)
        }

        // AO硬件参数结构体USB2185_AO_CH_PARAM中的nSampleRange参数所使用的模拟量输入采样范围挡位选项
        public const Int32 USB2185_AO_SAMPRANGE_N10_P10V = 0;  // ±10V

        // 跟整个AO有关的硬件工作参数(简称AO工作参数)
        public struct USB2185_AO_PARAM
        {
            public USB2185_AO_CH_PARAM CHParam0;   // 通道参数（通道使能，采样范围）
            public USB2185_AO_CH_PARAM CHParam1;
			public USB2185_AO_CH_PARAM CHParam2;
			public USB2185_AO_CH_PARAM CHParam3;
            // 时钟参数
            public UInt32 nSampleMode;		// 采样模式, 0=软件定时单点采样(按需)， 1、硬件定时单点采样(暂不支持)，2=有限点采样， 3=连续采样
            public UInt32 nSampsPerChan;		// 每个通道生成点数(也是每通道待写入点数),取值范围为单点采样模式时应等于1，有限点或连续采样时为[2, 1024*1024]
            public Double fSampleRate;		// 采样速率(Sample Rate), 单位为sps，取值范围为[1sps, 100000sps],它是每个采样通道的采样速率
            public UInt32 nClockSource;		// 时钟源选择, =0:内时钟OSCCLK; =1:外时钟CLKIN,由CN2上的Port1.DIO2(CLKIN)复用输入
            public UInt32 bClockOutput;		// 采样时钟输出允许, =0:表示禁止; =1:表示允许,由CN2上的Port1.DIO3(CLKOUT)复用输出
            public UInt32 bRegenModeEn;		// 波形重生成模式允许(在连续采样模式中有效), =TRUE:只是对开始生成任前写入任务中的波形段数据进行循环重复生成，=FALSE:禁止重生成模式，在开始生成任务后，还要不断的往任务中写入新的波形数据
            public UInt32 nReserved0;			// 保留字段(暂未定义)

            // 开始触发参数
            public UInt32 bDTriggerEn;		// 数字触发DTR允许(Digital Trigger Enable), =FALSE:表示禁止; =TRUE:表示允许,触发信号由CN2上的Port1.DIO1(DTR)复用输入，因此初始化时会将DIO1的方向强制置为输入
            public UInt32 nDTriggerDir;	    // 数字触发方向(Digital Trigger Direction)
            public UInt32 nTriggerSens;		// 触发灵敏度(Trigger Sensitive for Digital trigger),单位：微秒(uS)，取值范围[0, 1638]
            public UInt32 nDelaySamps;		// 触发延迟点数, 单位：采样点，取值范围32位有效[0, 4294967295]，0:Post Trigger, >0:Delay Trigger
            public UInt32 nReserved1;			// 保留字段(暂未定义)
            public UInt32 nReserved2;			// 保留字段(暂未定义)

            // 其他参数
            public UInt32 nReserved3;              // 保留字段(暂未定义)
            public UInt32 nReserved4;              // 保留字段(暂未定义)
            public UInt32 nReserved5;              // 保留字段(暂未定义)
            public UInt32 nReserved6;              // 保留字段(暂未定义)
        }

        // AO硬件参数结构体USB2185_AO_PARAM中的nSampleMode采样模式所使用的选项
        public const Int32 USB2185_AO_SAMPMODE_ONE_DEMAND = 0;      // 单点采样(按需)
        public const Int32 USB2185_AO_SAMPMODE_ONE_HWTIMED = 1;     // 单点采样(硬件定时,Hardware Timed, 本设备暂时不支持)
        public const Int32 USB2185_AO_SAMPMODE_FINITE = 2;          // 有限点采样
        public const Int32 USB2185_AO_SAMPMODE_CONTINUOUS = 3;      // 连续采样

        // AO硬件参数结构体USB2185_AO_PARAM中的nClockSource时钟源所使用的选项
        public const Int32 USB2185_AO_CLKSRC_LOCAL = 0;     // 本地时钟(通常为本地晶振时钟OSCCLK),也叫内部时钟
        public const Int32 USB2185_AO_CLKSRC_CLKIN = 1;      // 外部时钟(由连接器CN2上的DIO2/CLKIN复用输入)

        // AO硬件参数结构体USB2185_AO_PARAM中的nDTriggerDir触发方向所使用的选项
        public const Int32 USB2185_AO_TRIGDIR_FALLING = 0;    // 下降沿/低电平
        public const Int32 USB2185_AO_TRIGDIR_RISING = 1;       // 上升沿/高电平
        public const Int32 USB2185_AO_TRIGDIR_CHANGE = 2;      // 变化(即上下边沿/高低电平均有效)

        // #################### AO工作状态结构体USB2185_AO_STATUS描述 #####################
        public struct USB2185_AO_STATUS
        {
            public UInt32 bTaskDone;                        // 生成任务完成标志, =TRUE:表示生成任务完成, =FALSE:表示生成任务未完成，正在进行中
            public UInt32 bTriggered;                         // 触发标志, =TRUE:表示已被触发, =FALSE:表示未被触发(即正等待触发)

            public UInt32 nTaskState;                         // 生成任务状态, =1:正常, 其它值表示有异常情况
            public UInt32 nAvailSampsPerChan;         // 可写点数，最好等它大于参数nWriteSampsPerChan时才能调用AO_WriteAnalog()写入采样数据
            public UInt32 nMaxAvailSampsPerChan;  // 自AO_StartTask()后出现过的最大可写点数，状态值范围[0, nBufSampsPerChan],它是为监测采集软件性能而提供，如果此值越趋近于1，则表示意味着性能越高，越不易出现溢出丢点的可能
            public UInt32 nBufSampsPerChan;           // 每通道缓冲区大小(采样点数)
            public UInt64 nSampsPerChanAcquired;   // 每通道已采样点数(自开始生成任务(AO_StartTask())之后所采样的点数)，这个只是给用户的统计数据

            public UInt32 nHardUnderflowCnt;           // 硬件缓冲下溢次数(在不下溢情况下恒等于0)
            public UInt32 nSoftUnderflowCnt;             // 软件缓冲下溢次数(在不下溢情况下恒等于0)
            public UInt32 nInitTaskCnt;                       // 初始化生成任务的次数(即调用AO_InitTask()的次数)
            public UInt32 nReleaseTaskCnt;                // 释放生成任务的次数(即调用AO_ReleaseTask()的次数)
            public UInt32 nStartTaskCnt;                     // 开始生成任务的次数(即调用AO_StartTask()的次数)
            public UInt32 nStopTaskCnt;                      // 停止生成任务的次数(即调用AO_StopTask()的次数)
            public UInt32 nTransRate;                          // 传输速率, 即每秒传输点数(sps)，作为USB及应用软件传输性能的监测信息

            public UInt32 nReserved0;                         // 保留字段(暂未定义)
            public UInt32 nReserved1;                         // 保留字段(暂未定义)
            public UInt32 nReserved2;                         // 保留字段(暂未定义)
            public UInt32 nReserved3;                         // 保留字段(暂未定义)
            public UInt32 nReserved4;                         // 保留字段(暂未定义)
        }

        // #################### AO主要信息结构体(USB2185_AO_MAIN_INFO) #######################
        public struct USB2185_AO_MAIN_INFO
        {
            public UInt32 nChannelCount;             // AO通道数量
            public UInt32 nSampRangeCount;       // AO采样范围挡位数量
            public UInt32 nSampGainCount;          // AO增益挡位数量
            public UInt32 nCouplingCount;            // AO耦合挡位数量
            public UInt32 nImpedanceCount;         // AO阻抗的挡位数量
            public UInt32 nDepthOfMemory;        // AO板载存储器深度(点数)
            public UInt32 nSampResolution;          // AO采样分辨率(如=8表示8Bit; =12表示12Bit; =14表示14Bit; =16表示16Bit)
            public UInt32 nSampCodeCount;         // AO采样编码数量(如256, 4096, 16384, 65536)
            public UInt32 nTrigLvlResolution;        // 触发电平分辨率(如=8表示8Bit; =12表示12Bit; =16表示16Bit)
            public UInt32 nTrigLvlCodeCount;       // 触发电平编码数量(如256, 4096)

            public UInt32 nReserved0;                  // 保留字段(暂未定义)
            public UInt32 nReserved1;                  // 保留字段(暂未定义)
            public UInt32 nReserved2;                  // 保留字段(暂未定义)
            public UInt32 nReserved3;                  // 保留字段(暂未定义)
        }

        // #################### AO采样范围信息结构体(USB2185_AO_VOLT_RANGE_INFO) #######################
        public struct USB2185_AO_VOLT_RANGE_INFO
        {
            public UInt32 nSampleRange;          // 当前采样范围挡位号
            public UInt32 nReserved0;               // 保留字段(暂未定义)
            public double fMaxVolt;                   // 采样范围的最大电压值,单位:伏(V)
            public double fMinVolt;                    // 采样范围的最小电压值,单位:伏(V)
            public double fAmplitude;                // 采样范围幅度,单位:伏(V)
            public double fHalfOfAmp;             // 采样范围幅度的二分之一,单位:伏(V)
            public double fCodeWidth;               // 编码宽度,单位:伏(V), 即每个单位码值所分配的电压值
            public double fOffsetVolt;                // 偏移电压,单位:伏(V),一般用于零偏校准
            public double fOffsetCode;              // 偏移码值,一般用于零偏校准,它代表的电压值等价于fOffsetVolt
            public SByte strDesc0;                      // 采样范围字符描述,如"±10V", "0-10V"等
            public SByte strDesc1;
            public SByte strDesc2;
            public SByte strDesc3;
            public SByte strDesc4;
            public SByte strDesc5;
            public SByte strDesc6;
            public SByte strDesc7;
            public SByte strDesc8;
            public SByte strDesc9;
            public SByte strDesc10;
            public SByte strDesc11;
            public SByte strDesc12;
            public SByte strDesc13;
            public SByte strDesc14;
            public SByte strDesc15;

            public UInt32 nPolarity;                   // 采样范围的极性(0=双极性BiPolar, 1=单极性UniPolar)
            public UInt32 nCodeCount;              // 原码数量
            public Int32 nMaxCode;                   // 原码最大值
            public Int32 nMinCode;                   // 原码最小值

            public UInt32 nReserved1;                         // 保留字段(暂未定义)
            public UInt32 nReserved2;                         // 保留字段(暂未定义)
            public UInt32 nReserved3;                         // 保留字段(暂未定义)
            public UInt32 nReserved4;                         // 保留字段(暂未定义)
        }

        // 结构体AO_VOLT_RANGE_INFO的数据成员Polarity所用的采样范围极性选项
        public const Int32 USB2185_AO_POLAR_BIPOLAR = 0;        // 双极性
        public const Int32 USB2185_AO_POLAR_UNIPOLAR = 1;     // 单极性

        // #################### AO速率信息结构体(USB2185_AO_SAMP_RATE_INFO) #######################
        public struct USB2185_AO_SAMP_RATE_INFO
        {
            public double fMaxRate;             // 单通道最大采样速率(sps)，多通道时各通道平分最大采样率
            public double fMinRate;              // 单通道最小采样速率(sps)，多通道时各通道平分最小采样率
            public double fTimerBase;           // 时钟基准（即板载晶振频率）单位：Hz
            public UInt32 nDivideMode;        // 分频模式，0=整数分频(INTDIV), 1=DDS分频(DDSDIV)

            public UInt32 nRateType;            // 速率类型,指fMaxRate和fMinRate的类型, =0:表示为所有采样通道的总速率, =1:表示为每个采样通道的速率
            public UInt32 nReserved1;           // 保留字段(暂未定义)
            public UInt32 nReserved2;           // 保留字段(暂未定义)
        }

        // ###################### CTR工作参数结构体(USB2185_CTR_PARAM) #######################
        public struct USB2185_CTR_PARAM
        {
            public UInt32 nPulseDir;		// 脉冲方向(0=下降沿,1=上升沿, 2=双边沿),脉冲源由Port1.DIO0(CTRSRC)复用输入
            public UInt32 bInitReset;		// 是否在初始化时复位计数器至0，=TRUE:初始化时清零计数器, =FALSE:初始化是不清
            public UInt32 bFullReset;		// 是否在溢出时自动复位计数器至0，=TRUE:溢出时自动复位至0, =FALSE:在溢出时锁住当前计数不变

            public UInt32 nReserved0;		// 保留字段(暂未定义)
            public UInt32 nReserved1;		// 保留字段(暂未定义)
            public UInt32 nReserved2;		// 保留字段(暂未定义)
        }

        // CTR硬件参数结构体USB2185_CTR_PARAM中的nPulseDir脉冲方向所使用的选项
        public const Int32 USB2185_CTR_PULSEDIR_FALLING = 0;    // 下边沿
        public const Int32 USB2185_CTR_PULSEDIR_RISING = 1;       // 上边沿
        public const Int32 USB2185_CTR_PULSEDIR_CHANGE = 2;    // 变化(上下边沿均有效)

        // #################### DIO主要信息结构体(USB2185_DIO_PARAM) #######################
        public struct USB2185_DIO_PARAM
        {
            public Byte bOutputEn0;       // 输出方向允许(Output Enable),bOutputEn[n]=0:输出禁止(即为输入)，=1:输出允许(即输入无效)。(默认方向为输入)
            public Byte bOutputEn1;       // 但与其复用的CTRSRC,DTR,CLKIN,CLKOUT有效时，则与其相应的DIO方向控制自动失效
            public Byte bOutputEn2;
            public Byte bOutputEn3;
            public Byte bOutputEn4;
            public Byte bOutputEn5;
            public Byte bOutputEn6;
            public Byte bOutputEn7;

            public UInt32 nReserved0;		// 保留字段(暂未定义)
            public UInt32 nReserved1;		// 保留字段(暂未定义)
            public UInt32 nReserved2;		// 保留字段(暂未定义)
        }

        // ################################ 函数错误信息 ################################
        public const UInt32 ERROR_NO_AVAILABLE_SAMPS = (0xE0000000 + 1); // 无有效点数
        public const UInt32 ERROR_TASK_FAIL = (0xE0000000 + 2); // 采样任务失败
        public const UInt32 ERROR_RATE_OVER_RANGE = (0xE0000000 + 3); // 采样速率超限

        // ################################ 设备驱动接口申明 ################################

        // ################################ DEV设备对象管理函数 ################################
        [DllImport("USB2185.DLL")]
        public static extern IntPtr USB2185_DEV_Create(					// 创建设备对象句柄(hDevice), 成功返回实际句柄,失败则返回INVALID_HANDLE_VALUE(-1),可调用GetLastError()分析错误原因
                                                                                    UInt32 nDeviceIdx,			// 设备序号(逻辑序号或物理序号, 具体使用哪种序号由参数bUsePhysIdx决定)
                                                                                    Boolean bUsePhysIdx);		// 是否使用物理序号, =TRUE:使用物理序号, =FALSE:使用逻辑序号	

        [DllImport("USB2185.DLL")]
        public static extern UInt32 USB2185_DEV_GetCount();				// 取得该设备总台数,成功返回值>0, 失败返回0(可调用GetLastError()分析错误原因)


        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_DEV_GetCurrentIdx(				// 获得指定设备中的逻辑序号和物理序号
                                                                                            IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                                            ref UInt32 pLgcIdx,			// 返回的逻辑序号
                                                                                            ref UInt32 pPhysIdx);			// 返回的物理序号

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_DEV_GetSpeed(					// 读取设备连接的USB端口速度, 
                                                                                    IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                                    ref UInt32 pSpeed);			// USB接口速度，=1:USB1.0, =2:USB2.0, =3:USB3.0

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_DEV_Release(IntPtr hDevice);		// 释放设备对象(关键函数)

        // ################################ AI模拟量输入实现函数 ################################
        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AI_InitTask(				// 初始化采集任务(Initialize Task)
                                                                            IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                            ref USB2185_AI_PARAM pAIParam,			// AI工作参数, 它仅在此函数中决定硬件初始状态和各工作模式,可以事先由AI_VerifyParam()进行参数校验
                                                                            IntPtr pSampEvent);			// 返回采样事件对象句柄,当设备中出现可读数据段时会触发此事件，参数=NULL,表示不需要此事件句柄

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AI_StartTask(IntPtr hDevice);		// 开始采集任务

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AI_SendSoftTrig(IntPtr hDevice);		// 发送软件触发事件(Send Software Trigger),软件触发也叫强制触发

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AI_GetStatus(				// 取得AI各种状态
                                                                            IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                            ref USB2185_AI_STATUS pAIStatus);

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AI_WaitUntilTaskDone(				// 采集任务结束前等待,返回TRUE表示采集任务结束
                                                                                              IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                                              double fTimeout);        // 用于等待的时间，单位：秒(S)

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AI_ReadAnalog(				// 采集任务结束前等待,返回TRUE表示采集任务结束
                                                                                     IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                                     double[] fAnlgArray,  // 模拟数据数组(电压数组),用于返回采样的电压数据，取值区间由各通道采样时的采样范围决定(单位:V)
                                                                                     UInt32 nReadSampsPerChan,  // 每通道请求读取的点数(单位：点)
                                                                                      ref UInt32 pSampsPerChanRead,  // 返回每通道实际读取的点数(单位：点), =NULL,表示无须返回
                                                                                      ref UInt32 pAvailSampsPerChan,  // 任务中还存在的可读点数, =NULL,表示无须返回
                                                                                      double fTimeout);        // 超时时间，单位：秒(S)

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AI_ReadBinary(				// 采集任务结束前等待,返回TRUE表示采集任务结束
                                                                                     IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                                     Int16[] nBinArray,  // 二进制数据数组（原码数组）,用于返回采样的原码数据，取值区间为[-32768, 32767]
                                                                                     UInt32 nReadSampsPerChan,  // 每通道请求读取的点数(单位：点)
                                                                                      ref UInt32 pSampsPerChanRead,  // 返回每通道实际读取的点数(单位：点), =NULL,表示无须返回
                                                                                      ref UInt32 pAvailSampsPerChan,  // 任务中还存在的可读点数, =NULL,表示无须返回
                                                                                      double fTimeout);        // 超时时间，单位：秒(S)

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AI_StopTask(IntPtr hDevice);		// 停止AI采集任务

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AI_ReleaseTask(IntPtr hDevice);		// 释放采集任务

        // ========================= AI辅助操作函数 =========================
        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AI_GetMainInfo(				// 获得AI主要信息
                                                                            IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                            ref USB2185_AI_MAIN_INFO pMainInfo); // 获得AI主要信息

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AI_GetVoltRangeInfo(				// 采集任务结束前等待,返回TRUE表示采集任务结束
                                                                                     IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                                     UInt32 nChannel,  //AI物理通道号[0, 31]
                                                                                      UInt32 nSampleRange,  // 采样范围选择[0, 3]
                                                                                      ref USB2185_AI_VOLT_RANGE_INFO pRangeInfo);       // 采样范围信息

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AI_GetRateInfo(				// 获得AI主要信息
                                                                            IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                            ref USB2185_AI_SAMP_RATE_INFO pRateInfo); // 采样速率信息

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AI_ScaleBinToVolt(				// 根据指定采样范围将采样原码数据量化成电压数据,返回实际量化的点数
                                                                                     ref USB2185_AI_VOLT_RANGE_INFO pRangeInfo,			// 当前转换数据需要的采样范围信息
                                                                                     IntPtr pGainInfo,  // 当前转换数据需要的采样增益信息(若于=NULL，表示不使用增益)
                                                                                     double[] fVoltArray,  // 电压数据数组,用于返回量化后的电压数据,单位V
                                                                                     ref Int16  nBinArray,  // 二进制原码数组,用于传入量化前的原码数据，取值区间为[-32768, 32767], (单指某个通道的连续数据)
                                                                                      UInt32 nScaleSamps,  // 请求量化的采样点数
                                                                                      ref UInt32 pSampsScaled);        // 返回实际量化的采样点数, =NULL,表示无须返回

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AI_ScaleVoltToBin(				// 根据指定采样范围将电压数据量化成二进制原码数据,返回实际量化的点数
                                                                                     ref USB2185_AI_VOLT_RANGE_INFO pRangeInfo,			// 当前转换数据需要的采样范围信息
                                                                                     IntPtr pGainInfo,  // 当前转换数据需要的采样增益信息(若于=NULL，表示不使用增益)
                                                                                     Int16[] nBinArray,  // 二进制原码数组,用于传入量化前的原码数据，取值区间为[-32768, 32767], (单指某个通道的连续数据)
                                                                                      double[] fVoltArray,  // 电压数据数组,用于返回量化后的电压数据,单位V
                                                                                      UInt32 nScaleSamps,  // 请求量化的采样点数
                                                                                      ref UInt32 pSampsScaled);        // 返回实际量化的采样点数, =NULL,表示无须返回

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AI_VerifyParam(				// 校验AI工作参数(Verify Parameter),建议在初始化AI参数前调用此函数校验各参数合法性
                                                                            IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                            ref USB2185_AI_PARAM pAIParam); // 待校验的AI工作参数


        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AI_LoadParam(				// 从USB2185.ini中加载AI参数
                                                                            IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                            ref USB2185_AI_PARAM pAIParam); // 待校验的AI工作参数

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AI_SaveParam(				// 保存AI参数至USB2185.ini
                                                                            IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                            ref USB2185_AI_PARAM pAIParam);

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AI_ResetParam(				// 保存AI参数至USB2185.ini将当前AI参数复位至出厂值
                                                                            IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                            ref USB2185_AI_PARAM pAIParam);

        // ################################ AO模拟量输出实现函数 ################################
        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AO_InitTask(				// 初始化生成任务(Initialize Task)
                                                                            IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                            ref USB2185_AO_PARAM pAOParam,   // AO工作参数, 它仅在此函数中决定硬件初始状态和各工作模式,可以事先由AO_VerifyParam()进行参数校验
                                                                            IntPtr pSampEvent);  // 返回采样事件对象句柄,当设备中出现可读数据段时会触发此事件，参数=NULL,表示不需要此事件句柄

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AO_StartTask(				// 开始生成任务
                                                                            IntPtr hDevice);

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AO_SendSoftTrig(				// 发送软件触发事件(Send Software Trigger),软件触发也叫强制触发
                                                                                       IntPtr hDevice);

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AO_GetStatus(				// 取得AO各种状态
                                                                                    IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                                    ref USB2185_AO_STATUS pAOStatus);

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AO_WaitUntilTaskDone(				// 生成任务结束前等待,返回TRUE表示生成任务结束
                                                                                                IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                                                double fTimeout);       // 超时时间，单位：秒(S)

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AO_WriteAnalog(				//  向生成任务中写入AO输出的模拟量电压数据(Write analog data to the task)
                                                                                     IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                                     double[] fAnlgArray,  // AO0、AO1电压混合缓冲区,存放AO的模拟(电压)数据，取值区间由各通道采样时的采样范围决定(单位:V)
                                                                                     UInt32 nWriteSampsPerChan,  // 每通道请求写入的点数(单位：点)
                                                                                     ref UInt32 pSampsPerChanWritten,  //  返回每通道实际写入的点数(单位：点), =NULL,表示无须返回
                                                                                     ref UInt32 pAvailSampsPerChan,  // 返回当前可写入的采样点数, =NULL,表示无须返回
                                                                                     double fTimeout);        // 超时时间，单位：秒(S)

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AO_WriteBinary(				// 向生成任务中写入AO输出的二进制原码数据(Write binary data to the task)
                                                                                     IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                                     Int16[] nBinArray,  // AO0、AO1原码混合缓冲区,用于返回采样的原码数据，取值区间为[-32768, 32767]
                                                                                     UInt32 nWriteSampsPerChan,  // 每通道请求写入的点数(单位：点)
                                                                                     ref UInt32 pSampsPerChanWritten,  //  返回每通道实际写入的点数(单位：点), =NULL,表示无须返回
                                                                                     ref UInt32 pAvailSampsPerChan,  // 返回当前可写入的采样点数, =NULL,表示无须返回
                                                                                     double fTimeout);        // 超时时间，单位：秒(S)

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AO_ReadbackAnalog(				//  回读所有AO通道的当前生成的模拟量数据(电压数据序列)(Read back analog data from the task)
                                                                                                IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                                                double[] fAnlgArray);       // fAnlgArray[0]=AO0电压数据,fAnlgArray[1]=AO1电压数据, 取值区间由相应通道的采样范围决定

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AO_ReadbackBinary(				//  回读所有AO通道的当前生成的模拟量数据(二进制原码数据序列)(Read back binary data from the task)
                                                                                             IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                                             Int16[] nBinArray);       // nBinArray[0]=AO0原码数据,nBinArray[1]=AO1原码数据, 取值区间[-32768, 32767]

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AO_StopTask(				// 停止生成任务
                                                                                   IntPtr hDevice);

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AO_ReleaseTask(				// 释放生成任务
                                                                                        IntPtr hDevice);

        // ========================= AO辅助操作函数 =========================
        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AO_GetMainInfo(				// 获得AO的主要信息
                                                                                        IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                                        ref USB2185_AO_MAIN_INFO pMainInfo);       // 获得AO主要信息

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AO_GetVoltRangeInfo(				// 获得采样范围的各种信息(幅度、分辨率、极性)
                                                                                                IntPtr hDevice,                   // 设备对象句柄,它由DEV_Create()函数创建
                                                                                                UInt32 nChannel,                // AO物理通道号
                                                                                                UInt32 nSampleRange,        // 采样范围选择[0, 0]
                                                                                                ref USB2185_AO_VOLT_RANGE_INFO pRangeInfo);       //  采样范围信息

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AO_GetRateInfo(				// 获得采样速率信息
                                                                                        IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                                        ref USB2185_AO_SAMP_RATE_INFO pRateInfo);       // 采样速率信息

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AO_ScaleBinToVolt(				// 根据指定采样范围将采样原码数据量化成电压数据,返回实际量化的点数
                                                                                            ref USB2185_AO_VOLT_RANGE_INFO pRangeInfo,                   // 当前转换数据需要的采样范围信息
                                                                                            double[] fVoltArray,                // 电压缓冲区,用于返回量化后的电压数据,单位V
                                                                                            Int16[] nBinArray,        // 二进制原码缓冲区,用于传入量化前的原码数据，取值区间为[-32768, 32767], (单指某个通道的连续数据)
                                                                                            UInt32 nScaleSamps,    // 请求量化的采样点数
                                                                                            ref UInt32 pSampsScaled);       //  返回实际量化的采样点数

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AO_ScaleVoltToBin(				// 根据指定采样范围将采样原码数据量化成电压数据,返回实际量化的点数
                                                                                            ref USB2185_AO_VOLT_RANGE_INFO pRangeInfo,                   // 当前转换数据需要的采样范围信息
                                                                                            Int16[] nBinArray,                // 二进制原码缓冲区,用于传入量化前的原码数据，取值区间为[-32768, 32767], (单指某个通道的连续数据)
                                                                                            double[] fVoltArray,        // 电压缓冲区,用于返回量化后的电压数据,单位V
                                                                                            UInt32 nScaleSamps,    // 请求量化的采样点数
                                                                                            ref UInt32 pSampsScaled);       //  返回实际量化的采样点数

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AO_VerifyParam(				// 校验AO工作参数(Verify Parameter),建议在初始化生成任务前调用此函数校验各参数合法性
                                                                                        IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                                        ref USB2185_AO_PARAM pAOParam);       // 待校验的AO工作参数

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AO_LoadParam(				// 从USB2185.ini中加载AO参数
                                                                                        IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                                        ref USB2185_AO_PARAM pAOParam);

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AO_SaveParam(				// 保存AO参数至USB2185.ini
                                                                                    IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                                    ref USB2185_AO_PARAM pAOParam);

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_AO_ResetParam(				// 将当前AO参数复位至出厂值
                                                                                    IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                                    ref USB2185_AO_PARAM pAOParam);

        // ############################ CTR计数器函数 ############################
        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_CTR_InitTask(				// 初始采集任务
                                                                                    IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                                    UInt32 nChannel,      // 通道号(本设备仅有1个通道,恒等于0)
                                                                                    ref USB2185_CTR_PARAM pCTRParam);  // 工作参数

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_CTR_StartTask(				// 开始采集任务
                                                                                    IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                                    UInt32 nChannel);  // 通道号(本设备仅有1个通道,恒等于0)

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_CTR_ReadCounter(				// 读取计数器的当前计数值
                                                                                        IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                                        UInt32 nChannel,      // 通道号(本设备仅有1个通道,恒等于0)
                                                                                        ref UInt32 pCountVal);  // 计数值

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_CTR_StopTask(				// 停止(或暂停)采集任务
                                                                                    IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                                    UInt32 nChannel);  // 通道号(本设备仅有1个通道,恒等于0)

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_CTR_ReleaseTask(				// 释放采集任务
                                                                                        IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                                        UInt32 nChannel);  // 通道号(本设备仅有1个通道,恒等于0)

        // ############################ DI、DO数字量输入输出实现函数 ############################
        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_DIO_InitTask(				// 初始化DIO任务
                                                                                IntPtr hDevice,			// 设备对象句柄,它由DEV_Create()函数创建
                                                                                UInt32 nPort,            // 端口号, 取值范围为[0, 1]
                                                                                ref USB2185_DIO_PARAM pDIOParam);  // 工作参数

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_DIO_GetParam(                               // 获取DIO硬件参数
                                                                                    IntPtr hDevice,          // 设备对象句柄,它由DEV_Create()函数创建
                                                                                    UInt32 nPort,            // 端口号, 取值范围为[0, 1]
                                                                                    ref USB2185_DIO_PARAM pDIOParam);  // 工作参数

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_DIO_ReadPort(                               // 读数字量端口值(Read Digital Port Value)
                                                                                    IntPtr hDevice,          // 设备对象句柄,它由DEV_Create()函数创建
                                                                                    UInt32 nPort,            // 端口号, 取值范围为[0, 1]
                                                                                    ref UInt32 pPortData);  // 返回的端口数据, 有效位Bit[7:0]

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_DIO_WritePort(                                // 写数字量端口值(Write Digital Port Value)
                                                                                    IntPtr hDevice,         // 设备对象句柄,它由DEV_Create()函数创建
                                                                                    UInt32 nPort,            // 端口号, 取值范围为[0, 1]
                                                                                    UInt32 nPortData);   // 端口数据, 有效位Bit[7:0]

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_DIO_ReadLines(                                         // 读数字量端口值(Read Digital Port Value)
                                                                                    IntPtr hDevice,                    // 设备对象句柄,它由DEV_Create()函数创建
                                                                                    UInt32 nPort,                       // 端口号, 取值范围为[0, 1]
                                                                                    UInt32[] bLineDataArray);   // 线数据缓冲区, 同时返回端口中各线的状态值bLineDataArray[n]=0:表示关(或低)状态, =1表示开(或高)状态

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_DIO_WriteLines(                                         // 写数字量端口值(Write Digital Port Value)
                                                                                    IntPtr hDevice,                    // 设备对象句柄,它由DEV_Create()函数创建
                                                                                    UInt32 nPort,                       // 端口号, 取值范围为[0, 1]
                                                                                    UInt32[] bLineDataArray);   // 线数据缓冲区, 端口中各线的状态值bLineDataArray[n]=0:表示关(或低)状态, =1表示开(或高)状态

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_DIO_ReadLine(                                     // 读线值
                                                                                    IntPtr hDevice,              // 设备对象句柄,它由DEV_Create()函数创建
                                                                                    UInt32 nPort,                 // 端口号, 取值范围为[0, 1]
                                                                                    UInt32 nLine,                 // 线号,取值范围Port0时[0, 7]， Port1时[0, 3]
                                                                                    ref UInt32 pLineData);   // 线数据, 取值0或1 

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_DIO_WriteLine(                                     // 写线值
                                                                                    IntPtr hDevice,              // 设备对象句柄,它由DEV_Create()函数创建
                                                                                    UInt32 nPort,                 // 端口号, 取值范围为[0, 1]
                                                                                    UInt32 nLine,                 // 线号,取值范围Port0时[0, 7]， Port1时[0, 3]
                                                                                    UInt32 bLineData);        // 线值,取值0或1

        [DllImport("USB2185.DLL")]
        public static extern bool USB2185_DIO_ReleaseTask(                           // 释放DIO任务
                                                                                        IntPtr hDevice,     // 设备对象句柄,它由DEV_Create()函数创建
                                                                                        UInt32 nPort);      // 端口号, 取值范围为[0, 1]
    }
}
