using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace testapp.mylib
{
    /// <summary>
    /// 一对串口回环测试结果
    /// </summary>
    public class PairResult
    {
        public string PortA;    // "COM3"
        public string PortB;    // "COM5"
        public bool AB_Pass;    // A→B 方向通过
        public bool BA_Pass;    // B→A 方向通过
        public string Detail;   // 详细信息

        public bool IsPass { get { return AB_Pass && BA_Pass; } }

        public override string ToString()
        {
            return string.Format("{0}<->{1}: AB={2} BA={3} ({4})",
                PortA, PortB, AB_Pass ? "OK" : "FAIL", BA_Pass ? "OK" : "FAIL", Detail);
        }
    }

    /// <summary>
    /// 对一组串口两两进行双向回环测试。
    /// 测试流程: A 发送 → B 接收验证, B 发送 → A 接收验证。
    /// </summary>
    public class SerialLoopbackTester
    {
        /// <summary>
        /// 测试一对串口的双向回环
        /// </summary>
        /// <param name="portA">端口A名称, 如 "COM3"</param>
        /// <param name="portB">端口B名称, 如 "COM5"</param>
        /// <param name="baud">波特率, 默认 9600</param>
        /// <param name="timeoutMs">单方向读取超时毫秒, 默认 1000</param>
        /// <param name="testData">测试字符串, 默认 "HERO_LOOPBACK"</param>
        /// 注意: 打开串口后有 300ms 稳定延时; 单方向失败自动重试最多 3 次。
        /// 原因: 刚打开立即发送会丢失开头几个字符 (FTDI 驱动未就绪 / RS485 方向切换)。
        public static PairResult TestPair(string portA, string portB, int baud, int timeoutMs, string testData)
        {
            var result = new PairResult();
            result.PortA = portA;
            result.PortB = portB;
            result.AB_Pass = false;
            result.BA_Pass = false;
            result.Detail = "";

            if (string.IsNullOrEmpty(testData))
                testData = "HERO_LOOPBACK";

            SerialPort spA = null;
            SerialPort spB = null;

            try
            {
                spA = new SerialPort(portA, baud, Parity.None, 8, StopBits.One);
                spA.ReadTimeout = timeoutMs;
                spA.WriteTimeout = 2000;
                spA.Encoding = Encoding.UTF8;

                spB = new SerialPort(portB, baud, Parity.None, 8, StopBits.One);
                spB.ReadTimeout = timeoutMs;
                spB.WriteTimeout = 2000;
                spB.Encoding = Encoding.UTF8;

                spA.Open();
                spB.Open();

                // 清空缓冲
                spA.DiscardInBuffer();
                spB.DiscardInBuffer();

                // 刚打开串口立即发送会丢失开头几个字符 (FTDI 驱动未就绪 / RS485 方向切换),
                // 先给一段稳定时间, 再开始收发
                Thread.Sleep(300);

                // ── 方向1: A→B (失败自动重试, 最多3次) ──
                string dataAB = testData + "_A";
                string recvAB = "";
                for (int attempt = 1; attempt <= 3 && !result.AB_Pass; attempt++)
                {
                    if (attempt > 1) Thread.Sleep(100);
                    try { spA.DiscardInBuffer(); spB.DiscardInBuffer(); } catch { }
                    Thread.Sleep(50);   // 清缓冲与发送之间留间隔, 避免清缓冲与到达字节竞争丢数据
                    spA.WriteLine(dataAB);

                    recvAB = "";
                    var swAB = System.Diagnostics.Stopwatch.StartNew();
                    while (swAB.ElapsedMilliseconds < timeoutMs)
                    {
                        if (spB.BytesToRead > 0)
                        {
                            recvAB += spB.ReadExisting();   // 必须累加: FTDI 分块上交数据, 覆盖赋值会丢前半段
                            if (recvAB.Contains(dataAB))
                                break;
                        }
                        Thread.Sleep(10);
                    }
                    if (recvAB.Contains(dataAB))
                        result.AB_Pass = true;
                }
                if (!result.AB_Pass)
                    result.Detail += "A->B FAIL recv=" + recvAB.Replace("\r", "").Replace("\n", "") + "; ";

                Thread.Sleep(50);

                // ── 方向2: B→A (失败自动重试, 最多3次) ──
                string dataBA = testData + "_B";
                string recvBA = "";
                for (int attempt = 1; attempt <= 3 && !result.BA_Pass; attempt++)
                {
                    if (attempt > 1) Thread.Sleep(100);
                    try { spA.DiscardInBuffer(); spB.DiscardInBuffer(); } catch { }
                    Thread.Sleep(50);   // 清缓冲与发送之间留间隔, 避免清缓冲与到达字节竞争丢数据
                    spB.WriteLine(dataBA);

                    recvBA = "";
                    var swBA = System.Diagnostics.Stopwatch.StartNew();
                    while (swBA.ElapsedMilliseconds < timeoutMs)
                    {
                        if (spA.BytesToRead > 0)
                        {
                            recvBA += spA.ReadExisting();   // 必须累加: FTDI 分块上交数据, 覆盖赋值会丢前半段
                            if (recvBA.Contains(dataBA))
                                break;
                        }
                        Thread.Sleep(10);
                    }
                    if (recvBA.Contains(dataBA))
                        result.BA_Pass = true;
                }
                if (!result.BA_Pass)
                    result.Detail += "B->A FAIL recv=" + recvBA.Replace("\r", "").Replace("\n", "") + "; ";

                if (result.IsPass)
                    result.Detail = "both directions OK";
            }
            catch (Exception ex)
            {
                result.Detail += "exception: " + ex.Message;
            }
            finally
            {
                try { if (spA != null && spA.IsOpen) spA.Close(); } catch { }
                try { if (spB != null && spB.IsOpen) spB.Close(); } catch { }
            }

            return result;
        }

        /// <summary>
        /// 对所有端口做 C(N,2) 两两组合测试
        /// </summary>
        /// <param name="ports">端口名称列表</param>
        /// <param name="baud">波特率</param>
        /// <param name="timeoutMs">单方向超时毫秒</param>
        /// <param name="testData">测试字符串</param>
        public static List<PairResult> TestAllPairs(List<string> ports, int baud, int timeoutMs, string testData)
        {
            var results = new List<PairResult>();

            for (int i = 0; i < ports.Count; i++)
            {
                for (int j = i + 1; j < ports.Count; j++)
                {
                    var r = TestPair(ports[i], ports[j], baud, timeoutMs, testData);
                    results.Add(r);
                    utility_func.callbackdebuginfo("[SerialLoopback] " + r.ToString());
                }
            }

            return results;
        }
    }
}
