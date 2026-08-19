using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Festool.Sdk.ApplicationLayer.FmpProtobuf;
using Festool.Sdk.ApplicationLayer.FmpProtobuf.Scheme;
using Festool.Sdk.LinkLayer;
using Festool.Sdk.PhysicalLayer.Rs232;
using Festool.Sdk.LinkLayer.Fmp;
using System.IO;
using variable_space;
using OfficeOpenXml;
using testapp.glob_set;

namespace testapp
{
    delegate void setfestforserial(string  serialnumber);
    class FestoolCom
    {
        festool_mmu_project_var mmu_var = null;
        SemaphoreSlim semaphore = new SemaphoreSlim(0);
        public volatile int syncint = 0;
        public setfestforserial serialnumberset;
        private  Mutex mutex= new Mutex();
        public uint sn_by_scaner;
        uint EOL_ADDR = 3;
        uint MAIN_CONTROLLER_ADDR = 4;
        // const uint PDAT_UNLOCK_KEY = 4176656669;
        const uint DRV_PDAT_UNLOCK_KEY = 2473798234;
        
        // const uint TESTMODE_UNLOCK_KEY = 3025402110;
        const uint TESTMODE_UNLOCK_KEY = 1278976938;
        public SerialPort _transport = null;
        FmpSupervisor _fmpSupervisor = null;
        public FmpProtobuf _fmpProtobuf = null;
        string port = "";
        int boudrate = 0;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="port"></param>
        /// <param name="boudrate"></param>
        public FestoolCom(string port, int boudrate)
        {

            if (glob_ini_instance.getInstance().getSetupIniData["setport"]["Festool_contol_add"] != null)
            {

                MAIN_CONTROLLER_ADDR = 2;

            }
            else {

                MAIN_CONTROLLER_ADDR = 4;

            }

            Task.Factory.StartNew(() =>
            {
                if (!File.Exists("./mmu_production_info.xml"))
                {

                    mmu_var = new festool_mmu_project_var();
                    mmu_var.ManufactureNumber = "SEA\0";
                    mmu_var.partNumber = "1087160";
                    mmu_var.electronicsVision = "0";
                    mmu_var.barePcbPartNumber = "10817661";
                    mmu_var.barePcbVersion = "5";
                    mmu_var.schematicPartNumbe = "10948452";
                    mmu_var.schematicPcbVersion = "4";                 
                    mmu_var.assembledPcbPartNumber = "10896215";
                    mmu_var.assembledPcbVersion = "0";
                    
                    testapp.useful.XmlHelper.SerializeToXml<festool_mmu_project_var>(mmu_var, "./mmu_production_info.xml");
                }
                else
                {
                    try
                    {
                        mylib.utility_func.callbackdebuginfo("loading production info ");
                        if (glob_ini_instance.getInstance().getSetupIniData["setport"]["Festool_contol_add"] != null)
                        {

                            mmu_var = testapp.useful.XmlHelper.DeserializeFromXml<festool_mmu_project_var>("./mmu_hmi_production_info.xml");

                        }
                        else
                        {

                            mmu_var = testapp.useful.XmlHelper.DeserializeFromXml<festool_mmu_project_var>("./mmu_production_info.xml");

                        }
                       
                        mylib.utility_func.callbackdebuginfo("production info loaded ");
                    }
                    catch (Exception e)
                    {
                        mylib.utility_func.callbackdebuginfo("festool production info load error \n" + e.ToString());


                    }


                }

            });


            this.port = port;
            this.boudrate = boudrate;


            _transport = new SerialPort(this.port);
            _transport.Open();
            _fmpSupervisor = new FmpSupervisor(_transport/*, new Log4NetLogger(typeof(FmpSupervisor))*/)
            {
                V2OriginAddress = EOL_ADDR,               // FMP V2: Address of the sending device (C# application)
                V2TargetAddress = MAIN_CONTROLLER_ADDR    // FMP V2: Address of the target device (Main device controller (MC1) = 4)
            };
            _fmpProtobuf = new FmpProtobuf(_fmpSupervisor/*, new Log4NetLogger(typeof(FmpProtobuf))*/)
            {
                //UseFmpV1 = false,                               // Determines the FMP Version to be used (V1 or V2).
                AutoRespondDeviceOnline = true,                   // If set to true, the FmpProtobuf instance will confirm a DeviceOnline message if V2OriginAddress matches.
                AutoRespondNonMatchingTargetAddress = true,       // If set to true, the instance FmpProtobuf automatically respond to certain messages (if configured), even if the destination address does not match the V2OriginAddress.
                                                                  //LogDefaultPbValues = false,                     // Configures, if default Protobuf values should be logged
                ResponseTimeout = 150                    // Alter the default response timeout

            };

            _fmpProtobuf.ProtobufReceived += FmpProtobuf_ProtobufReceived;

            // Eventhandler, triggered if a FmpProtobuf message has been sent (by the C# application)
            _fmpProtobuf.ProtobufSent += FmpProtobuf_ProtobufSent;

           // input_testmode();
        }

        private void FmpProtobuf_ProtobufSent(object sender, ProtobufEventArgs e)
        {

            switch (e.EventType) {
                case ProtobufEventArgs.Type.V2_REQUEST: {

                        //   syncint = 0;
                        break;
                    }

                case ProtobufEventArgs.Type.V2_RESPONSE_EVENT_DEVICE_ONLINE: {

                        syncint = 1;

                        Task.Factory.StartNew(() => {


                            try
                            {

                                System.Threading.Thread.Sleep(10);
                                // if (!semaphore.WaitAsync(-1).Result) { }
                                if ((_fmpProtobuf.V2.SetDeviceModeAsync(DeviceMode.Types.Mode.Test, TESTMODE_UNLOCK_KEY, timeout: 1000)).Result.ReturnCode == ReturnCode.NoError)
                                {

                                    syncint = 0;

                                }
                                else
                                {


                                    syncint = 1;
                                }

                            }
                            catch
                            {

                                syncint = -1;

                            }
                            finally { 
                            
                            
                            }



                        });

                        //if ((_fmpProtobuf.SetDeviceModeAsync(DeviceMode.Types.Mode.Test, TESTMODE_UNLOCK_KEY)).Result.ReturnCode == ReturnCode.NoError)
                        //{

                        //    syncint = 0;
                        //}
                        //else {


                        //    syncint = 1;
                        //}

                        //System.Threading.Thread.Sleep(20);

                        break;
                    }

            }

        }

        private void FmpProtobuf_ProtobufReceived(object sender, ProtobufEventArgs e)
        {


          

            switch (e.EventType)
            {
                case ProtobufEventArgs.Type.V2_RESPONSE:
                    {

                       
                        break;
                    }

                case ProtobufEventArgs.Type.V2_RESPONSE_EVENT_DEVICE_ONLINE:
                    {

                        //   syncint = 1;
                        break;
                    }
                case ProtobufEventArgs.Type.V2_EVENT:
                    {



                        if (false) { 
                     
                            Task.Factory.StartNew(() => {

                                if (mutex.WaitOne(0)) // 尝试获取互斥锁
                                {
                                    try
                                    {


                                        if ((_fmpProtobuf.V2.SetDeviceModeAsync(DeviceMode.Types.Mode.Test, TESTMODE_UNLOCK_KEY, timeout: 1000)).Result.ReturnCode == ReturnCode.NoError)
                                        {
                                            mylib.utility_func.callbackdebuginfo("mmu_unit into test mode;");


                                        }
                                        else
                                        {

                                            mylib.utility_func.callbackdebuginfo("mmu_unit not into test mode");

                                        }

                                    }
                                    catch
                                    {

                                        mylib.utility_func.callbackdebuginfo("mmu_unit test mode error");

                                    }
                                    finally
                                    {

                                        mutex.ReleaseMutex();

                                    }
                                }
                                


                            });

                        }

                    }

                        break;
                case ProtobufEventArgs.Type.V2_REQUEST: {
                       
                        

                    }
                    break;

                default:
                    {



                        



                    }
                    break;

            }
        }

        public int input_testmode() {


            int status = 0;
           var rp =  Task.Factory.StartNew(() => {

                if (mutex.WaitOne(0)) // 尝试获取互斥锁
                {
                    try
                    {


                        if ((_fmpProtobuf.V2.SetDeviceModeAsync(DeviceMode.Types.Mode.Test, TESTMODE_UNLOCK_KEY, timeout: 1000)).Result.ReturnCode == ReturnCode.NoError)
                        {
                            mylib.utility_func.callbackdebuginfo("mmu_unit into test mode;");
                            status = 1;

                        }
                        else
                        {

                            mylib.utility_func.callbackdebuginfo("mmu_unit not into test mode");
                            status = 0;
                        }

                    }
                    catch
                    {

                        mylib.utility_func.callbackdebuginfo("mmu_unit test mode error");

                    }
                    finally
                    {

                        mutex.ReleaseMutex();

                    }
                }



            });


            rp.Wait();

            return status;


        }
        /// <summary>
        /// used setp1 step10
        /// </summary>
        /// <returns></returns>
        public async Task<int> SetDeviceIntoTestMode()
        {
            await Task.CompletedTask;





            if (syncint == 0)
            {
                syncint = 1; return 1;
            }
            else
            {
                syncint = 1; return -1;
            }


            ////for (int i = 0; i < 3; i++)
            ////{
            //    set_comm_init();
            //    //  System.Threading.Thread.Sleep(100);
            //    wait_delay();
            //    try
            //    {
            //        if ((await _fmpProtobuf.SetDeviceModeAsync(DeviceMode.Types.Mode.Test, TESTMODE_UNLOCK_KEY)).ReturnCode == ReturnCode.NoError) { 

            //            return 1;
            //         //   System.Threading.Thread.Sleep(1000);

            //        }

            //    }
            //    catch
            //    {

            //        return -1;
            //    }



            ////}


            //return 0;

        }


        public async Task<(int, Object)> hmi_read_encoder_count()
        {


            try
            {
                // Read production data from the device
                System.Threading.Thread.Sleep(50);


                mylib.utility_func.callbackdebuginfo($"Read encoder count" + "\n");

                var retval = await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.EncoderCount, timeout: 2000);
                if (retval.ReturnCode != ReturnCode.NoError) { mylib.utility_func.callbackdebuginfo($"retval.ReturnCode{retval.ReturnCode}  val:" + retval.Payload.IntValue.Value); return (-3, null); }
                var val = retval.Payload.IntValue.Value;
                mylib.utility_func.callbackdebuginfo($"retval  val:" + retval.Payload.IntValue.Value);
                return (1, val);
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo(ex.ToString());
                return (-1, null);
            }

            mylib.utility_func.callbackdebuginfo("out try error!!");
            return (-2, null);

        }

        public async Task<(int, Object)> hmi_led_test(int led_num=0,int led_status=1)
        {


            try
            {
                // Read production data from the device
                System.Threading.Thread.Sleep(50);


                mylib.utility_func.callbackdebuginfo($"Read encoder count" + "\n");

                var retval = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.SetLedOn, boolParam: new BoolValue() { Value = led_status==1?true:false }, intParam: new Int32Value() { Value = led_num });
                if (retval.ReturnCode != ReturnCode.NoError) { mylib.utility_func.callbackdebuginfo($"retval.ReturnCode{retval.ReturnCode}"); return (-3, null); }
                
                mylib.utility_func.callbackdebuginfo($"retval  val:" + "noerror");
                return (1, 1);
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo(ex.ToString());
                return (-1, null);
            }

            mylib.utility_func.callbackdebuginfo("out try error!!");
            return (-2, null);

        }






        public async Task<(int, Object)> read_trace_dataAsync() {


            try
            {
                UInt32 fst_PartNumber, fst_Version, fst_BarePcbPartNumber, fst_BarePcbVersion,
                    fst_AssembledPcbPartNumber, fst_AssembledPcbVersion, fst_SchematicPartNumber,
                    fst_SchematicPcbVersion, fst_ManufacturerNumber;
                ulong fst_SerialNumber;
                string mfn = "";
                UInt32 fw_ver=0;
                // Read production data from the device
                System.Threading.Thread.Sleep(50);
                FmpProtobuf.Response<TraceData> readTraceDataResponse = await _fmpProtobuf.V2.ReadTraceDataAsync();
                if (readTraceDataResponse.ReturnCode != ReturnCode.NoError) { return (-3, null); }
                TraceData traceData = readTraceDataResponse.Payload;
               

               //TraceData traceData = new TraceData();   

                mylib.utility_func.callbackdebuginfo($"write_traceData:" + "\n"
                + $"PartNumber:{mmu_var.partNumber}" + "\n"
                + $"Version:{mmu_var.electronicsVision}" + "\n"
                + $"BarePcbPartNumber:{mmu_var.barePcbPartNumber}" + "\n"
                + $"BarePcbVersion:{mmu_var.barePcbVersion}" + "\n"
                + $"AssembledPcbPartNumber:{mmu_var.assembledPcbPartNumber}" + "\n"
                + $"AssembledPcbVersion:{mmu_var.assembledPcbVersion}" + "\n"
                + $"SerialNumber:{sn_by_scaner}" + "\n"
                + $"ManufacturerNumber:{mmu_var.ManufactureNumber}" + "\n");
                
                if (!BitConverter.IsLittleEndian)
                {
                    traceData.Subproducts[0].PartNumber = uint.Parse(mmu_var.partNumber);
                    traceData.Subproducts[0].Version = BitConverter.ToUInt32(BitConverter.GetBytes(uint.Parse(mmu_var.electronicsVision)).Reverse().ToArray(), 0);
                    traceData.Subproducts[0].BarePcbPartNumber = uint.Parse(mmu_var.barePcbPartNumber);
                    traceData.Subproducts[0].BarePcbVersion = BitConverter.ToUInt32(BitConverter.GetBytes(uint.Parse(mmu_var.barePcbVersion)).Reverse().ToArray(),0);
                    traceData.Subproducts[0].AssembledPcbPartNumber = uint.Parse(mmu_var.assembledPcbPartNumber);
                    traceData.Subproducts[0].AssembledPcbVersion = BitConverter.ToUInt32(BitConverter.GetBytes(uint.Parse(mmu_var.assembledPcbVersion)).Reverse().ToArray(),0);
                    traceData.Subproducts[0].SchematicPartNumber = uint.Parse(mmu_var.schematicPartNumbe);
                    traceData.Subproducts[0].SchematicPcbVersion = BitConverter.ToUInt32(BitConverter.GetBytes(uint.Parse(mmu_var.schematicPcbVersion)).Reverse().ToArray(), 0);
                    traceData.Subproducts[0].SerialNumber = BitConverter.ToUInt32(BitConverter.GetBytes(sn_by_scaner).Reverse().ToArray(),0);
                    traceData.Subproducts[0].ManufacturerNumber = BitConverter.ToUInt32(System.Text.ASCIIEncoding.ASCII.GetBytes((mmu_var.ManufactureNumber + "\0").ToCharArray()), 0);
                    traceData.Subproducts[0].ManufacturingDate = ConvertDateTimeInt(DateTime.Now);
                    traceData.Subproducts[0].AssembledPcbManufacturingDate = ConvertDateTimeInt(DateTime.Now);



                }
                else {


                    /*原始状态*/ //  traceData.Subproducts[0].PartNumber = BitConverter.ToUInt32(BitConverter.GetBytes(uint.Parse(mmu_var.partNumber)).Reverse().ToArray(), 0);
                    /*修改*/traceData.Subproducts[0].PartNumber = uint.Parse(mmu_var.partNumber);
                    traceData.Subproducts[0].Version = uint.Parse(mmu_var.electronicsVision);
                    /*原始状态*/ //traceData.Subproducts[0].BarePcbPartNumber = BitConverter.ToUInt32(BitConverter.GetBytes(uint.Parse(mmu_var.barePcbPartNumber)).Reverse().ToArray(), 0);
                    /*修改*/traceData.Subproducts[0].BarePcbPartNumber = uint.Parse(mmu_var.barePcbPartNumber);
                    traceData.Subproducts[0].BarePcbVersion = uint.Parse(mmu_var.barePcbVersion);
                    /*原始状态*/ //traceData.Subproducts[0].AssembledPcbPartNumber = BitConverter.ToUInt32(BitConverter.GetBytes(uint.Parse(mmu_var.assembledPcbPartNumber)).Reverse().ToArray(), 0);
                    /*修改*/traceData.Subproducts[0].AssembledPcbPartNumber = uint.Parse(mmu_var.assembledPcbPartNumber);
                    traceData.Subproducts[0].AssembledPcbVersion = uint.Parse(mmu_var.assembledPcbVersion);
                    /*原始状态*///traceData.Subproducts[0].SchematicPartNumber = BitConverter.ToUInt32(BitConverter.GetBytes(uint.Parse(mmu_var.schematicPartNumbe)).Reverse().ToArray(), 0);
                    /*修改*/traceData.Subproducts[0].SchematicPartNumber = uint.Parse(mmu_var.schematicPartNumbe);
                    traceData.Subproducts[0].SchematicPcbVersion = uint.Parse(mmu_var.schematicPcbVersion);
                    traceData.Subproducts[0].SerialNumber = sn_by_scaner;
                    /*原始状态*/ // traceData.Subproducts[0].ManufacturerNumber = BitConverter.ToUInt32(System.Text.ASCIIEncoding.ASCII.GetBytes((mmu_var.ManufactureNumber+"\0").ToCharArray().Reverse().ToArray()), 0); ;
                    /*修改*/traceData.Subproducts[0].ManufacturerNumber = BitConverter.ToUInt32(System.Text.ASCIIEncoding.ASCII.GetBytes((mmu_var.ManufactureNumber + "\0").ToCharArray()), 0);
                    /*原始状态*/ //   traceData.Subproducts[0].ManufacturingDate = BitConverter.ToUInt32(BitConverter.GetBytes(ConvertDateTimeInt(DateTime.Now)).Reverse().ToArray(),0);
                    /*原始状态*/ //  traceData.Subproducts[0].AssembledPcbManufacturingDate = BitConverter.ToUInt32(BitConverter.GetBytes(ConvertDateTimeInt(DateTime.Now)).Reverse().ToArray(), 0);
                    /*修改*/traceData.Subproducts[0].ManufacturingDate = ConvertDateTimeInt(DateTime.Now);
                    /*修改*/traceData.Subproducts[0].AssembledPcbManufacturingDate = ConvertDateTimeInt(DateTime.Now);

                 

                    if (glob_ini_instance.getInstance().getSetupIniData["setport"]["Festool_contol_add"] != null)
                    {


                        traceData.Subproducts[0].Type = TraceData.Types.SubproductData.Types.ProductType.ElectronicsExtBle;

                    }




                }


                // ToDo: Alter read trace data
                // Send a set device mode request to get production data write access.
                System.Threading.Thread.Sleep(10);

         

                    if ((await _fmpProtobuf.V2.SetDeviceModeAsync(DeviceMode.Types.Mode.Pdat, DRV_PDAT_UNLOCK_KEY)).ReturnCode != ReturnCode.NoError)
                    {
                        mylib.utility_func.callbackdebuginfo($"SetDeviceModeAsync{(await _fmpProtobuf.V2.SetDeviceModeAsync(DeviceMode.Types.Mode.Pdat, DRV_PDAT_UNLOCK_KEY)).ReturnCode}");
                        return (-2, null); ;
                    }

              
                System.Threading.Thread.Sleep(10);
                // Write a TraceData object to the device.
                if ((await _fmpProtobuf.V2.WriteTraceDataAsync(traceData)).ReturnCode != ReturnCode.NoError) {
                    mylib.utility_func.callbackdebuginfo($"WriteTraceDataAsync(traceData).ReturnCode{(await _fmpProtobuf.V2.WriteTraceDataAsync(traceData)).ReturnCode}");
                    return (-1, null); }


               if (glob_ini_instance.getInstance().getSetupIniData["setport"]["Festool_contol_add"] != null)
                {

                    System.Threading.Thread.Sleep(1500);

                }
                else
                {

                    System.Threading.Thread.Sleep(20);

                }
               
                // Read production data from the device
                FmpProtobuf.Response<TraceData> readdate = await _fmpProtobuf.V2.ReadTraceDataAsync();
                if (readdate.ReturnCode != ReturnCode.NoError) { mylib.utility_func.callbackdebuginfo($"readdate.ReturnCode{readdate.ReturnCode}"); return (-6, null); }
                TraceData traceData_read = readdate.Payload;
                string read_str = "";

              
                if (!BitConverter.IsLittleEndian)
                {


                    fst_PartNumber = traceData.Subproducts[0].PartNumber;
                    fst_Version = BitConverter.ToUInt32(BitConverter.GetBytes(traceData.Subproducts[0].Version).Reverse().ToArray(), 0);
                    fst_BarePcbPartNumber = traceData.Subproducts[0].BarePcbPartNumber;
                    fst_BarePcbVersion = BitConverter.ToUInt32(BitConverter.GetBytes(traceData.Subproducts[0].BarePcbVersion).Reverse().ToArray(), 0);
                    fst_AssembledPcbPartNumber = traceData.Subproducts[0].AssembledPcbPartNumber;
                    fst_AssembledPcbVersion = BitConverter.ToUInt32(BitConverter.GetBytes(traceData.Subproducts[0].AssembledPcbVersion).Reverse().ToArray(), 0);
                    fst_SchematicPartNumber = traceData.Subproducts[0].SchematicPartNumber;
                    fst_SchematicPcbVersion = BitConverter.ToUInt32(BitConverter.GetBytes(traceData.Subproducts[0].SchematicPcbVersion).Reverse().ToArray(), 0);
                    fst_SerialNumber = BitConverter.ToUInt32(BitConverter.GetBytes(traceData.Subproducts[0].SerialNumber).ToArray(), 0);
                    fst_ManufacturerNumber = traceData.Subproducts[0].ManufacturerNumber;
                    
                    mfn = System.Text.ASCIIEncoding.ASCII.GetString(BitConverter.GetBytes(fst_AssembledPcbPartNumber));
               read_str = $"read_traceData:" + "\n"
                + $"PartNumber:{fst_PartNumber}" + "\n"
                + $"Version:{fst_Version}" + "\n"
                + $"BarePcbPartNumber:{fst_BarePcbPartNumber}" + "\n"
                + $"BarePcbVersion:{fst_BarePcbVersion}" + "\n"
                + $"AssembledPcbPartNumber:{fst_AssembledPcbPartNumber}" + "\n"
                + $"AssembledPcbVersion:{fst_AssembledPcbVersion}" + "\n"
                 + $"schematicPcbPartNumber:{fst_SchematicPartNumber}" + "\n"
                + $"schematicPcbVersion:{fst_SchematicPcbVersion}" + "\n"
                + $"SerialNumber:{fst_SerialNumber}" + "\n"
                + $"ManufacturerNumber:{mfn}" ;
                }
                else {





                    /*原始状态*/ //fst_PartNumber = BitConverter.ToUInt32(BitConverter.GetBytes(traceData.Subproducts[0].PartNumber).Reverse().ToArray(), 0);
                   /*修改*/ fst_PartNumber = traceData_read.Subproducts[0].PartNumber;
                    fst_Version = traceData_read.Subproducts[0].Version;
                    /*原始状态*/ // fst_BarePcbPartNumber = BitConverter.ToUInt32(BitConverter.GetBytes(traceData.Subproducts[0].BarePcbPartNumber).Reverse().ToArray(), 0);
                    /*修改*/fst_BarePcbPartNumber = traceData_read.Subproducts[0].BarePcbPartNumber;
                    fst_BarePcbVersion = traceData_read.Subproducts[0].BarePcbVersion;
                    /*原始状态*/ //fst_AssembledPcbPartNumber = BitConverter.ToUInt32(BitConverter.GetBytes(traceData.Subproducts[0].AssembledPcbPartNumber).Reverse().ToArray(), 0);
                    /*修改*/fst_AssembledPcbPartNumber = traceData_read.Subproducts[0].AssembledPcbPartNumber;
                    fst_AssembledPcbVersion = traceData_read.Subproducts[0].AssembledPcbVersion;
                    /*原始状态*/ //fst_SchematicPartNumber = BitConverter.ToUInt32(BitConverter.GetBytes(traceData.Subproducts[0].SchematicPartNumber).Reverse().ToArray(), 0);
                    /*修改*/fst_SchematicPartNumber = traceData_read.Subproducts[0].SchematicPartNumber;
                    fst_SchematicPcbVersion = traceData_read.Subproducts[0].SchematicPcbVersion;
                    fst_SerialNumber = traceData_read.Subproducts[0].SerialNumber;
                    /*原始状态*/ //fst_ManufacturerNumber = BitConverter.ToUInt32(BitConverter.GetBytes(traceData.Subproducts[0].ManufacturerNumber).Reverse().ToArray(), 0);
                    /*修改*/fst_ManufacturerNumber = traceData_read.Subproducts[0].ManufacturerNumber;
                    mfn = System.Text.ASCIIEncoding.ASCII.GetString(BitConverter.GetBytes(fst_ManufacturerNumber)).Substring(0,3);
                    fw_ver = traceData_read.Subproducts[0].SwConfigNumber;
                    read_str = $"read_traceData:" + "["
                  + $"PartNumber:{fst_PartNumber}" + "&"
                  + $"Version:{fst_Version}" + "&"
                  + $"BarePcbPartNumber:{fst_BarePcbPartNumber}" + "&"
                  + $"BarePcbVersion:{fst_BarePcbVersion}" + "&"
                  + $"AssembledPcbPartNumber:{fst_AssembledPcbPartNumber}" + "&"
                  + $"AssembledPcbVersion:{fst_AssembledPcbVersion}" + "&"
                  + $"schematicPcbPartNumber:{fst_SchematicPartNumber}" + "&"
                  + $"schematicPcbVersion:{fst_SchematicPcbVersion}" + "&"
                  + $"SerialNumber:{fst_SerialNumber}" + "&"
                  + $"ManufacturerNumber:{mfn}" + "]"
                  +$"fw_ver:{fw_ver}";
                }



                mylib.utility_func.callbackdebuginfo(read_str);

         
                if (fst_PartNumber == uint.Parse(mmu_var.partNumber) &&
                 fst_Version == uint.Parse(mmu_var.electronicsVision) &&
                 fst_BarePcbPartNumber == uint.Parse(mmu_var.barePcbPartNumber) &&
                 fst_BarePcbVersion == uint.Parse(mmu_var.barePcbVersion) &&
                 fst_AssembledPcbPartNumber == uint.Parse(mmu_var.assembledPcbPartNumber) &&
                 fst_AssembledPcbVersion == uint.Parse(mmu_var.assembledPcbVersion) &&
                 fst_SchematicPartNumber == uint.Parse(mmu_var.schematicPartNumbe) &&
                 fst_SchematicPcbVersion == uint.Parse(mmu_var.schematicPcbVersion) &&
                 fst_SerialNumber == sn_by_scaner &&
                  mfn == mmu_var.ManufactureNumber
                 )
                {

                    return (1, read_str);
                }
                else {


                    mylib.utility_func.callbackdebuginfo($"read_traceData:" + "不匹配");

                    return (-7, null); ;

                }



            }
            catch(Exception ex)
            {

                mylib.utility_func.callbackdebuginfo(ex.ToString());
                return (-4, null);
            }
            mylib.utility_func.callbackdebuginfo("out try error!!!");
            return (-8, null);
            // ToDo: Re-Read trace data from the device and verify the result
        }  
        public async Task<(int, Object)> itme25_check_vdc_link_value()
        {


            try
            {
                // Read production data from the device
                System.Threading.Thread.Sleep(50);


            mylib.utility_func.callbackdebuginfo($"check_vdc_link_value" + "\n");

                var retval = await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.DcLinkVoltageMv,timeout:2000);
                if (retval.ReturnCode != ReturnCode.NoError) { mylib.utility_func.callbackdebuginfo($"retval.ReturnCode{retval.ReturnCode}"); return (-3, null); }
                var val = retval.Payload.IntValue.Value;
                
            return (1,val);
          }
            catch(Exception ex)
            {

                mylib.utility_func.callbackdebuginfo(ex.ToString());
                return (-1, null);
            }

            mylib.utility_func.callbackdebuginfo("out try error!!");
            return (-2, null);
           
        }
        public async Task<(int, Object)> check_15v_sense_value()
        {


            try
            {
                // Read production data from the device
                System.Threading.Thread.Sleep(50);


                mylib.utility_func.callbackdebuginfo($"check_15v_sense_value" + "\n");
             
                var retval = await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.SupplyVoltage15VMv, timeout: 2000);
                if (retval.ReturnCode != ReturnCode.NoError) { mylib.utility_func.callbackdebuginfo($"retval.ReturnCode{retval.ReturnCode}"); return (-3, null); }
                var val = retval.Payload.IntValue.Value;

                return (1, val);
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo(ex.ToString());
                return (-1, null);
            }

            mylib.utility_func.callbackdebuginfo("out try error!!");
            return (-2, null);

        }
        public async Task<(int, Object)> check_trigger_state()
        {


            try
            {
                // Read production data from the device
                System.Threading.Thread.Sleep(50);


                mylib.utility_func.callbackdebuginfo($"item31_check_trigger_state" + "\n");

                var retval = await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.TriggerSwitchOn, timeout: 2000);
                if (retval.ReturnCode != ReturnCode.NoError) { mylib.utility_func.callbackdebuginfo($"retval.ReturnCode{retval.ReturnCode}"); return (-3, null); }
                var val = retval.Payload.BoolValue.Value;

                return (1, val);
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo(ex.ToString());
                return (-1, null);
            }

            mylib.utility_func.callbackdebuginfo("out try error!!");
            return (-2, null);

        }
        public async Task<(int, Object)> check_Sync_50hz_value()
        {


            try
            {
                // Read production data from the device
                System.Threading.Thread.Sleep(50);


                mylib.utility_func.callbackdebuginfo($"check_Sync_50hz_value" + "\n");

                var retval = await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.GridFrequencyHz, timeout: 2000);
                if (retval.ReturnCode != ReturnCode.NoError) { mylib.utility_func.callbackdebuginfo($"retval.ReturnCode{retval.ReturnCode}"); return (-3, null); }
                var val = retval.Payload.IntValue.Value;

                return (1, val);
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo(ex.ToString());
                return (-1, null);
            }

            mylib.utility_func.callbackdebuginfo("out try error!!");
            return (-2, null);

        }
        public async Task<(int, Object)> check_temp_IGBT_value()
        {


            try
            {
                // Read production data from the device
                System.Threading.Thread.Sleep(50);


                mylib.utility_func.callbackdebuginfo($"check_temp_IGBT_value" + "\n");

                var retval = await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.ElectronicTemp100Mc, timeout: 2000);
                if (retval.ReturnCode != ReturnCode.NoError) { mylib.utility_func.callbackdebuginfo($"retval.ReturnCode{retval.ReturnCode}"); return (-3, null); }
                var val = retval.Payload.IntValue.Value;

                return (1, val);
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo(ex.ToString());
                return (-1, null);
            }

            mylib.utility_func.callbackdebuginfo("out try error!!");
            return (-2, null);

        }
        public async Task<(int, Object)> check_key_offset_phonse_value(string phase)
        {


            try
            {
                // Read production data from the device
                System.Threading.Thread.Sleep(50);


                mylib.utility_func.callbackdebuginfo($"check_key_offset_phonse_value" + "\n");

                FmpProtobuf.Response<TmValue> retval = null;
                if(phase.ToUpper()=="U") retval = await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.OffsetPhaseUMv, timeout: 2000);
                if (phase.ToUpper() == "V") retval = await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.OffsetPhaseVMv, timeout: 2000);
                if (phase.ToUpper() == "W") retval = await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.OffsetPhaseWMv, timeout: 2000);
                if (retval.ReturnCode != ReturnCode.NoError) { mylib.utility_func.callbackdebuginfo($"retval.ReturnCode{retval.ReturnCode}"); return (-3, null); }
                var val = retval.Payload.IntValue.Value;

                return (1, val);
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo(ex.ToString());
                return (-1, null);
            }

            mylib.utility_func.callbackdebuginfo("out try error!!");
            return (-2, null);

        }
        public async Task<(int, Object)> check_reset_state_low_current_limit(bool status=false)
        {


            try
            {
                // Read production data from the device
                System.Threading.Thread.Sleep(50);


                mylib.utility_func.callbackdebuginfo($"check_reset_state_low_current_limit" + "\n");

                FmpProtobuf.Response<TmCommand> retval = await _fmpProtobuf.V2.SetTmCommandAsync( TmCommand.Types.Key.ReduceOvercurrentLimitOn,boolParam:new BoolValue() { Value= status }, timeout: 2000);
             
                if (retval.ReturnCode != ReturnCode.NoError) { mylib.utility_func.callbackdebuginfo($"retval.ReturnCode{retval.ReturnCode}"); return (-3, null); }

                return (1, null);
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo(ex.ToString());
                return (-1, null);
            }

            mylib.utility_func.callbackdebuginfo("out try error!!");
            return (-2, null);

        }
        public async Task<(int, Object)> set_Phase_test_ON(string UVW_STATUS)
        {


            try
            {
                // Read production data from the device
                System.Threading.Thread.Sleep(50);


                mylib.utility_func.callbackdebuginfo($"set_Phase_test_ON" + "\n");

                FmpProtobuf.Response<TmCommand> retval=null;
                if (UVW_STATUS.ToUpper().IndexOf("U-V") >= 0) {
                    mylib.utility_func.callbackdebuginfo($"set_Phase_test: U-V" + "\n");
                    retval = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.SetPwmPhaseUVlow, timeout: 2000);
                }

                if (UVW_STATUS.ToUpper().IndexOf("V-U") >= 0)
                {
                    mylib.utility_func.callbackdebuginfo($"set_Phase_test: V-U" + "\n");
                    retval = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.SetPwmPhaseVUlow, timeout: 2000);
                }

                if (UVW_STATUS.ToUpper().IndexOf("V-W") >= 0)
                {
                    mylib.utility_func.callbackdebuginfo($"set_Phase_test: V-W" + "\n");
                    retval = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.SetPwmPhaseVWlow, timeout: 2000);
                }

                if (UVW_STATUS.ToUpper().IndexOf("W-V") >= 0)
                {
                    mylib.utility_func.callbackdebuginfo($"set_Phase_test: W-V" + "\n");
                    retval = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.SetPwmPhaseWVlow, timeout: 2000);
                }

                if (retval.ReturnCode != ReturnCode.NoError) { mylib.utility_func.callbackdebuginfo($"retval.ReturnCode{retval.ReturnCode}"); return (-3, null); }

                return (1, null);
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo(ex.ToString());
                return (-1, null);
            }

            mylib.utility_func.callbackdebuginfo("out try error!!");
            return (-2, null);

        }
        public async Task<(int, Object)> check_phonse_current_value(string phase)
        {


            try
            {
                // Read production data from the device
                System.Threading.Thread.Sleep(50);


                mylib.utility_func.callbackdebuginfo($"check_key_offset_phonse_value" + "\n");

                FmpProtobuf.Response<TmValue> retval = null;
                if (phase.ToUpper() == "U") retval = await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.CurrentPhaseUMa, timeout: 2000);
                if (phase.ToUpper() == "V") retval = await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.CurrentPhaseVMa, timeout: 2000);
                if (phase.ToUpper() == "W") retval = await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.CurrentPhaseWMa, timeout: 2000);
                if (retval.ReturnCode != ReturnCode.NoError) { mylib.utility_func.callbackdebuginfo($"retval.ReturnCode{retval.ReturnCode}"); return (-3, null); }
                var val = retval.Payload.IntValue.Value;

                return (1, val);
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo(ex.ToString());
                return (-1, null);
            }

            mylib.utility_func.callbackdebuginfo("out try error!!");
            return (-2, null);

        }
        public async Task<(int, object)> check_current_fault()
        {


            try
            {
                // Read production data from the device
                System.Threading.Thread.Sleep(50);


                mylib.utility_func.callbackdebuginfo($"check_current_faulte" + "\n");

                FmpProtobuf.Response<TmValue> retval =  await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.PowerstageFault, timeout: 2000);
                if (retval.ReturnCode != ReturnCode.NoError) { mylib.utility_func.callbackdebuginfo($"retval.ReturnCode{retval.ReturnCode}"); return (-3, null); }
               
                return (1, retval.Payload.BoolValue.Value);
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo(ex.ToString());
                return (-1, null);
            }

            mylib.utility_func.callbackdebuginfo("out try error!!");
            return (-2, null);

        }
        public async Task<(int, object)> Reset_eeprom()
        {


            try
            {
                // Read production data from the device
                System.Threading.Thread.Sleep(50);


                mylib.utility_func.callbackdebuginfo($"Reset_eeprom" + "\n");

                FmpProtobuf.Response<TmCommand> retval =
                await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.ResetEeprom, timeout: 2000);




                if (retval.ReturnCode != ReturnCode.NoError) { mylib.utility_func.callbackdebuginfo($"retval.ReturnCode{retval.ReturnCode}"); return (-3, null); }

                return (1, null);
               
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo(ex.ToString());
                return (-1, null);
            }

            mylib.utility_func.callbackdebuginfo("out try error!!");
            return (-2, null);

        }
        public async Task<(int, object)> config_device()
        {
            await Task.CompletedTask;


            try
            {
                // Read production data from the device
                System.Threading.Thread.Sleep(50);


                _fmpSupervisor.V2OriginAddress = 1;
                DeviceConfig config = new DeviceConfig();
                config.OutputRpm = new UInt32ConfigValue();
                config.OutputRpm.Value = 10000;

                var retval = _fmpProtobuf.V2.SetDeviceConfigAsync(config).Result;
                if (retval.ReturnCode != ReturnCode.NoError)
                {

                    mylib.utility_func.callbackdebuginfo($"retval.ReturnCode{retval.ReturnCode}"); return (-3, null);
                }
                _fmpSupervisor.V2OriginAddress = 3;

                return (1, null);

            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo(ex.ToString());
                return (-1, null);
            }

            mylib.utility_func.callbackdebuginfo("out try error!!");
            return (-2, null);

        }

        public async Task<(int, object)> read_config()
        {
            await Task.CompletedTask;


            try
            {
                // Read production data from the device
                System.Threading.Thread.Sleep(50);



                var retval = _fmpProtobuf.V2.ReadDeviceConfigAsync().Result;
                if (retval.ReturnCode != ReturnCode.NoError)
                {
                   

                    mylib.utility_func.callbackdebuginfo($"retval.ReturnCode{retval.ReturnCode}"); return (-3, null);
                }
              

                return (1, retval.Payload.OutputRpm.Value);

            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo(ex.ToString());
                return (-1, null);
            }

            mylib.utility_func.callbackdebuginfo("out try error!!");
            return (-2, null);

        }
        public async Task<(int, object)> set_flash_state()
        {


            try
            {
                // Read production data from the device
                System.Threading.Thread.Sleep(50);


                mylib.utility_func.callbackdebuginfo($"check_current_faulte" + "\n");

                FmpProtobuf.Response<TmValue> retval = await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.TestExternalFlash, timeout: 2000);
                if (retval.ReturnCode != ReturnCode.NoError) { mylib.utility_func.callbackdebuginfo($"retval.ReturnCode{retval.ReturnCode}"); return (-3, null); }

                return (1, retval.Payload.BoolValue.Value);
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo(ex.ToString());
                return (-1, null);
            }

            mylib.utility_func.callbackdebuginfo("out try error!!");
            return (-2, null);

        }
        public async Task<(int, Object)> set_Phase_test_OFF()
        {


            try
            {
                // Read production data from the device
                System.Threading.Thread.Sleep(50);


                mylib.utility_func.callbackdebuginfo($"set_Phase_test_OFF_U_V_W" + "\n");

                FmpProtobuf.Response<TmCommand> retval = 
                    retval = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.SetPwmUVWOff, timeout: 2000);
              

        

                if (retval.ReturnCode != ReturnCode.NoError) { mylib.utility_func.callbackdebuginfo($"retval.ReturnCode{retval.ReturnCode}"); return (-3, null); }

                return (1, null);
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo(ex.ToString());
                return (-1, null);
            }

            mylib.utility_func.callbackdebuginfo("out try error!!");
            return (-2, null);

        }
        public async Task<(int, Object)> CHECK_LED_STATUS(bool onoff)
        {


            try
            {
                // Read production data from the device
                System.Threading.Thread.Sleep(50);


                mylib.utility_func.callbackdebuginfo($"CHECK_LED_STATUS:{onoff}" + "\n");

                FmpProtobuf.Response<TmCommand> retval;
                    retval = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.SetLightOn, boolParam: new BoolValue() { Value = onoff } ,timeout: 2000);

                if (retval.ReturnCode != ReturnCode.NoError) { mylib.utility_func.callbackdebuginfo($"retval.ReturnCode{retval.ReturnCode}"); return (-3, null); }

                return (1, null);
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo(ex.ToString());
                return (-1, null);
            }

            mylib.utility_func.callbackdebuginfo("out try error!!");
            return (-2, null);

        }
    
        public async Task<int> readFirmwareVersion()
        {

            for (int i = 0; i < 3; i++)
            {
                try
                {

                    FmpProtobuf.Response<FirmwareVersion> fwver = await _fmpProtobuf.V2.ReadFirmwareVersionAsync();

                    if (fwver.ReturnCode != ReturnCode.NoError) continue;

                    return (int)fwver.Payload.SoftdeviceVersion;

                }
                catch
                {



                }



            }

            return -1;

        }




        /// <summary>
        ///  step19 KEY_TRIGGER_SWITCH_ON
        ///  setp20 step21 step22 step23 step24
        /// </summary>
        /// <param name="checkname"></param>
        /// <returns></returns>
        public async Task<int> readProve_digital_inputs(string checkname)
        {
            
            for (int i = 0; i < 3; i++)
            {
                System.Threading.Thread.Sleep(100);
                try
                {

                    int vl;
                    FmpProtobuf.Response<TmValue> rs = null;
                    switch (checkname) {

                        case "KEY_TRIGGER_SWITCH_ON": /*KEY_TRIGGER_SWITCH_ON*/

                            rs = (await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.TriggerSwitchOn));

                            if (rs.ReturnCode == ReturnCode.NoError)
                            {
                                vl=  rs.Payload.BoolValue.Value?2:1;

                            }
                            else {

                                vl = -3;    
                            }
                           
                            break;
                        case "KEY_POWERSTAGE_FAULT":
                            rs = (await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.PowerstageFault));
                            if (rs.ReturnCode == ReturnCode.NoError)
                            {
                                vl = rs.Payload.BoolValue.Value ? 2 : 1;

                            }
                            else
                            {

                                vl = -3;
                            }
                            break;
                        case "KEY_DIRECTION_SWITCH_RIGHT":
                            rs = (await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.DirectionSwitchRight));
                            if (rs.ReturnCode == ReturnCode.NoError)
                            {
                                vl = rs.Payload.BoolValue.Value ? 2 : 1;

                            }
                            else
                            {

                                vl = -3;
                            }
                            break;
                        case "KEY_HALL_SENSOR_1":
                            rs = (await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.HallSensor1));
                            if (rs.ReturnCode == ReturnCode.NoError)
                            {
                                vl = rs.Payload.BoolValue.Value ? 2 : 1;

                            }
                            else
                            {

                                vl = -3;
                            }
                            break;
                        case "KEY_HALL_SENSOR_2":
                            rs = (await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.HallSensor2));
                            if (rs.ReturnCode == ReturnCode.NoError)
                            {
                                vl = rs.Payload.BoolValue.Value ? 2 : 1;

                            }
                            else
                            {

                                vl = -3;
                            }
                            break;
                        case "KEY_HALL_SENSOR_3":
                            rs = (await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.HallSensor3));
                            if (rs.ReturnCode == ReturnCode.NoError)
                            {
                                vl = rs.Payload.BoolValue.Value ? 2 : 1;

                            }
                            else
                            {

                                vl = -3;
                            }
                            break;
                        default:
                            throw new Exception();
                            // goto jump;
                            // vl = true;
                            break;


                    }
                    if (vl < 0) continue;
                    return vl;

                }
                catch
                {

                    return -2;
                }



            }


            return -1;

        }

      
        /// <summary>
        /// step12  step25 step26 step28 step29 step27_
        /// </summary>
        /// <param name="checkname"></param>
        /// <returns></returns>
        public async Task<int> readtmvalue_analog(string checkname)
        {

            for (int i = 0; i < 3; i++)
            {
                System.Threading.Thread.Sleep(100);
                try
                {
                    int p = 0;
                    FmpProtobuf.Response<TmValue> rs = null;
                    switch (checkname)
                    {
                        case "KEY_BATTERY_PACK_VOLTAGE_MV":

                            rs = (await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.BatteryPackVoltageMv));
                            if (rs.ReturnCode == ReturnCode.NoError)
                            {
                                p = rs.Payload.IntValue.Value;
                            }
                            else {
                                p = -3;
                            }      

                            break;
                        case "KEY_MOTOR_HALL_TEMP_100MC":  //KEY_MOTOR_HALL_TEMP_100MC


                            rs = (await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.MotorHallTemp100Mc));
                            if (rs.ReturnCode == ReturnCode.NoError)
                            {
                                p = rs.Payload.IntValue.Value;
                            }
                            else
                            {
                                p = -3;
                            }

                            break;
                        case "KEY_POWERSTAGE_TEMP_100MC"://KEY_MOTOR_HALL_TEMP_100MC STEP12
                            rs = (await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.PowerstageTemp100Mc));
                            if (rs.ReturnCode == ReturnCode.NoError)
                            {
                                
                                p = rs.Payload.IntValue.Value;
                            }
                            else
                            {
                                p = -3;
                            }


                            break;
                        case "KEY_TRIGGER_SWITCH_MV":

                            rs = (await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.TriggerSwitchMv));
                            if (rs.ReturnCode == ReturnCode.NoError)
                            {
                                p = rs.Payload.IntValue.Value;
                            }
                            else
                            {
                                p = -3;
                            }

                            break;
                        case "TORQUE_POTENTIOMETER_MV":

                            rs = (await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.TorquePotentiometerMv));

                            if (rs.ReturnCode == ReturnCode.NoError)
                            {
                                p = rs.Payload.IntValue.Value;
                            }
                            else
                            {
                                p = -3;
                            }

                            break;
                        default:

                            break;
                    }


                    if (p > 0) { return p; }

                }
                catch
                {

                    return -2;
                }



            }


            return -1;

        }


        /// <summary>
        /// step13 step14 step15 step16 step17 step18
        /// </summary>
        /// <param name="UVW"></param>
        /// <param name="amp"></param>
        /// <returns></returns>
        public async Task<int[]> calibrate_phase_UVW(string UVW, int amp)
        {
           // System.Windows.Forms.MessageBox.Show("0fffffffffffffffffffffffffff");
            for (int i = 0; i < 3; i++)
            {
               
                try
                {
                    BoolValue bv = new BoolValue() { Value = true };
                    Int32Value iv = new Int32Value() { Value = amp };
                    FmpProtobuf.Response<TmCommand> setTmCommandResponse = null;
                    switch (UVW) {
                        case "KEY_CALIBRATE_PHASE_U_MA":
                            {
                                setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.CalibratePhaseUMa, bv, iv,timeout:2000);

                                break;
                            }
                        case "KEY_CALIBRATE_PHASE_V_MA":
                            {
                                setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.CalibratePhaseVMa, bv, iv, timeout: 2000); break;

                            }
                        case "KEY_CALIBRATE_PHASE_W_MA":
                            {
                                setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.CalibratePhaseWMa, bv, iv, timeout: 2000); break;

                            }
                        case "CALIBRATE_TRIGGER_SWITCH":
                            {
                                setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.CalibrateTriggerSwitch, bv, timeout: 2000); break;

                            }

                        default:
                            {
                                setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.CalibratePhaseWMa, bv, iv, timeout: 2000); break;

                            }


                    }
                 //   System.Windows.Forms.MessageBox.Show("1fffffffffffffffffffffffffff" + setTmCommandResponse.ReturnCode.ToString() + ":" + setTmCommandResponse.Payload.IntParam.Value);
                    if (setTmCommandResponse.ReturnCode == ReturnCode.NoError) return new int[] { 1, setTmCommandResponse.Payload.IntParam.Value, (setTmCommandResponse.Payload.BoolParam.Value == true) ? 1 : 0 };


                }
                catch (Exception e)
                {
                   // System.Windows.Forms.MessageBox.Show("2fffffffffffffffffffffffffff " + e.ToString());
                    return new int[] { -1, -1, -1 };
                }



            }
         //   System.Windows.Forms.MessageBox.Show("3fffffffffffffffffffffffffff");

            return new int[] { -1, -1, -1 };

        }


        public async Task<int> LED_SET(string led, bool onoff)
        {

            for (int i = 0; i < 3; i++)
            {
                System.Threading.Thread.Sleep(100);
                try
                {








                    FmpProtobuf.Response<TmCommand> setTmCommandResponse=null;


                    

                    switch (led)
                    {
                        case "LED1ON":
                            { setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.SetLed1On, new BoolValue() { Value = true });  break; }
                        case "LED1OFF":
                            { setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.SetLed1On, new BoolValue() { Value = false }); break; }
                        case "LED2ON":
                            { setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.SetLed2On, new BoolValue() { Value = true }); break; }
                        case "LED2OFF":
                            { setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.SetLed2On, new BoolValue() { Value = false }); break; }
                        case "LED3ON":
                            { setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.SetLed3On, new BoolValue() { Value = true }); ; break; }
                        case "LED3OFF":
                            { setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.SetLed3On, new BoolValue() { Value = false }); break; }
                        case "LED4ON":
                            { setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.SetLed4On, new BoolValue() { Value = true });  break; }
                        case "LED4OFF":
                            { setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.SetLed4On, new BoolValue() { Value = false }); break; }

                        default:
                            //{ setTmCommandResponse = await _fmpProtobuf.LedSetAsync(new LedSet() { Led4 = false }); break; }
                            break;
                    }







                    //FmpProtobuf.Response<Empty> setTmCommandResponse = null;
                    //switch (led)
                    //{
                    //    case "LED1ON":
                    //        { setTmCommandResponse = await _fmpProtobuf.LedSetAsync(new LedSet() { Led1 = true }); break; }
                    //    case "LED1OFF":
                    //        { setTmCommandResponse = await _fmpProtobuf.LedSetAsync(new LedSet() { Led1 = false }); break; }
                    //    case "LED2ON":
                    //        { setTmCommandResponse = await _fmpProtobuf.LedSetAsync(new LedSet() { Led2 = true }); break; }
                    //    case "LED2OFF":
                    //        { setTmCommandResponse = await _fmpProtobuf.LedSetAsync(new LedSet() { Led2 = false }); break; }
                    //    case "LED3ON":
                    //        { setTmCommandResponse = await _fmpProtobuf.LedSetAsync(new LedSet() { Led3 = true }); break; }
                    //    case "LED3OFF":
                    //        { setTmCommandResponse = await _fmpProtobuf.LedSetAsync(new LedSet() { Led3 = false }); break; }
                    //    case "LED4ON":
                    //        {
                    //            setTmCommandResponse = await _fmpProtobuf.LedSetAsync(new LedSet() { Led4 = true }); break;
                    //        }
                    //    case "LED4OFF":
                    //        { setTmCommandResponse = await _fmpProtobuf.LedSetAsync(new LedSet() { Led4 = false }); break; }

                    //    default:
                    //        //{ setTmCommandResponse = await _fmpProtobuf.LedSetAsync(new LedSet() { Led4 = false }); break; }
                    //        break;
                    //}
                    if (setTmCommandResponse.ReturnCode == ReturnCode.NoError) return 1;


                }
                catch
                {

                    return -1;
                }



            }


            return -2;

        }



        /// <summary>
        /// step30 step32 step31 step27+
        /// </summary>
        /// <param name="testname"></param>
        /// <param name="bf"></param>
        /// <returns></returns>
        public async Task<int> calibrate_SWITCH_AND_offset_UVW(string testname, bool bf = false)
        {

            for (int i = 0; i < 3; i++)
            {
                System.Threading.Thread.Sleep(100);
                try
                {
                    BoolValue bv = new BoolValue() { Value = bf };
                    // Int32Value iv = new Int32Value() { Value = amp };
                    FmpProtobuf.Response<TmCommand> setTmCommandResponse = null;
                    switch (testname)
                    {

                        case "KEY_CALIBRATE_TRIGGER_SWITCH":
                            {

                                setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.CalibrateTriggerSwitch, bv,timeout:2000); break;

                            }
                        case "KEY_CALIBRATE_OFFSET_PHASE_U_MV":
                            {

                                setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.CalibrateOffsetPhaseUMv, timeout: 2000); break;

                            }
                        case "KEY_CALIBRATE_OFFSET_PHASE_V_MV":
                            {

                                setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.CalibrateOffsetPhaseVMv, timeout: 2000); break;

                            }
                        case "KEY_CALIBRATE_OFFSET_PHASE_W_MV":
                            {

                                setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.CalibrateOffsetPhaseWMv, timeout: 2000); break;

                            }


                        default:
                            {
                                setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.CalibrateOffsetPhaseWMv, timeout: 2000); break;

                            }

                    }
                    if (setTmCommandResponse.ReturnCode == ReturnCode.NoError) return 1;


                }
                catch
                {

                    return -1;
                }



            }


            return -2;

        }

        /// <summary>
        /// step33 step34 step36 step38
        /// </summary>
        /// <param name="testname"></param>
        /// <returns></returns>
        public async Task<int> check_moseft_and_current_analog(string testname)
        {

            for (int i = 0; i < 3; i++)
            {
               // System.Threading.Thread.Sleep(100);
                try
                {
                    int p = 0;
                    FmpProtobuf.Response<TmValue> rs = null;
                    switch (testname)
                    {
                        case "KEY_CURRENT_PHASE_V_MA":

                            rs = (await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.CurrentPhaseVMa,timeout:2000));

                            if (rs.ReturnCode == ReturnCode.NoError)
                            {
                                p = rs.Payload.IntValue.Value;
                            }
                            else {

                                p = -3;
                            }
                           
                            break;
                        case "KEY_CURRENT_PHASE_W_MA":

                            rs = (await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.CurrentPhaseWMa, timeout: 2000));
                            if (rs.ReturnCode == ReturnCode.NoError)
                            {
                                p = rs.Payload.IntValue.Value;
                            }
                            else
                            {

                                p = -3;
                            }
                            break;
                        case "KEY_CURRENT_PHASE_U_MA":
                            rs = (await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.CurrentPhaseUMa, timeout: 2000));
                            if (rs.ReturnCode == ReturnCode.NoError)
                            {
                                p = rs.Payload.IntValue.Value;
                            }
                            else
                            {

                                p = -3;
                            }
                            break;
                        case "KEY_TORQUE_POTENTIOMETER_MV":
                            rs = (await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.TorquePotentiometerMv,timeout: 2000));
                            if (rs.ReturnCode == ReturnCode.NoError)
                            {
                                p = rs.Payload.IntValue.Value;
                            }
                            else
                            {

                                p = -3;
                            }

                            break;
                        case "KEY_CURRENT_PHASE_MA":
                           rs  = (await _fmpProtobuf.V2.ReadTmValueAsync(TmValue.Types.Key.CurrentPhaseMa, timeout: 2000));
                            if (rs.ReturnCode == ReturnCode.NoError)
                            {
                                p = rs.Payload.IntValue.Value;
                            }
                            else
                            {

                                p = -3;
                            }
                            break;
                        default:
                            break;
                    }

                    if (p >= 0) { return p; }
                }
                catch
                {

                    return -2;
                }



            }


            return -1;

        }
        /// <summary>
        /// step34 step35 step36 step37
        /// </summary>
        /// <param name="testname"></param>
        /// <returns></returns>
        public async Task<int> set_moseft_and_current_analog(string testname)
        {

            for (int i = 0; i < 3; i++)
            {
               // System.Threading.Thread.Sleep(100);
                try
                {
                    //  BoolValue bv = new BoolValue() { Value = bf };
                    // Int32Value iv = new Int32Value() { Value = amp };
                    FmpProtobuf.Response<TmCommand> setTmCommandResponse = null;
                    switch (testname)
                    {

                        case "CALIBRATE_TRIGGER_SWITCH":
                            {

                                setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.CalibrateTriggerSwitch, timeout: 2000); break;

                            }
                        case "FSET_PHASE_U_MV":
                            {

                                setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.CalibrateOffsetPhaseUMv, timeout: 2000); break;

                            }
                        case "FSET_PHASE_V_MV":
                            {

                                setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.CalibrateOffsetPhaseVMv, timeout: 2000); break;

                            }
                        case "FSET_PHASE_W_MV":
                            {

                                setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.CalibrateOffsetPhaseWMv, timeout: 2000); break;

                            }
                        case "KEY_SET_PWM_PHASE_U_VLOW":
                            {
                                setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.SetPwmPhaseUVlow, new BoolValue() { Value = true }, new Int32Value() { Value = 75 },timeout:2000);
                                
                                
                                break;
                            }
                        case "KEY_SET_PWM_U_V_W_OFF":
                            {
                                setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.SetPwmUVWOff,timeout:2000); break;
                            }
                        case "KEY_SET_PWM_PHASE_V_WLOW":
                            {
                                setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.SetPwmPhaseVWlow, new BoolValue() { Value = true }, new Int32Value() { Value = 75 }, timeout: 2000); break;
                            }
                        case "KEY_SET_PWM_PHASE_W_ULOW":
                            {
                                setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.SetPwmPhaseWUlow, new BoolValue() { Value = true }, new Int32Value() { Value = 75 }, timeout: 2000); break;
                            }

                        default:
                            {
                                setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.CalibrateOffsetPhaseWMv, timeout: 2000); break;

                            }

                    }
                    if (setTmCommandResponse.ReturnCode == ReturnCode.NoError) return 1;


                }
                catch
                {

                    return -1;
                }



            }


            return -2;

        }









        /// <summary>
        /// used step2 step3
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        public async Task<int> WritePdat(ulong serial11 = 0)
        {


            try
            {
                System.Threading.Thread.Sleep(100);
                // Read production data from the device

               

                FmpProtobuf.Response<TraceData> readTraceDataResponse = await _fmpProtobuf.V2.ReadTraceDataAsync(timeout:1000);
               

                if (readTraceDataResponse.ReturnCode != ReturnCode.NoError) {
                    return -3;
                }
               

                TraceData traceData = readTraceDataResponse.Payload;

                //   DateTime dt = LongDateTimeToDateTime((long)traceData.Subproducts[0].SerialNumber);
                // if (traceData.Subproducts[0].SerialNumber != null && dt.Year >= 2021 && dt.Year <= 2025)
                //   if (traceData.Subproducts[0].SerialNumber != null && traceData.Subproducts[0].SerialNumber>20210000000000)
                //  {

                //   serialnumberset(traceData.Subproducts[0].SerialNumber + "");

                //  serialnumberset(dt.ToString("yyyy-MM-dd_hh-mm-ss"));

                //  }
                //  else
                //  {
                /*
                if (!File.Exists("festoolSerial.txt"))
                {
                    File.WriteAllText("festoolSerial.txt", "" + 0);
                }

                string serialnumber = File.ReadAllLines("festoolSerial.txt")[0];

                */
                   ulong sn = ConvertDateTimeInt(DateTime.Now);
                //   serialnumberset(DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss"));

                //  ulong sn = serial11;

                    //  File.WriteAllText("festoolSerial.txt", "" + sn);
                traceData.Subproducts[0].SerialNumber = sn;
                System.Threading.Thread.Sleep(50);
                // ToDo: Alter read trace data
                // Send a set device mode request to get production data write access.
                if ((await _fmpProtobuf.V2.SetDeviceModeAsync(DeviceMode.Types.Mode.Pdat, DRV_PDAT_UNLOCK_KEY)).ReturnCode != ReturnCode.NoError) { return -2; }

                // Write a TraceData object to the device.
                System.Threading.Thread.Sleep(50);
                FmpProtobuf.Response<Empty> writeTraceDataResponse = (await _fmpProtobuf.V2.WriteTraceDataAsync(traceData,timeout:1000));
                //   if ((await _fmpProtobuf.WriteTraceDataAsync(traceData)).ReturnCode != ReturnCode.NoError) {
                if (writeTraceDataResponse.ReturnCode != ReturnCode.NoError) { 
                    return -1; 
                }





          //      }
            }
            catch
            {


                return -4;
            }

            return 1;
            // ToDo: Re-Read trace data from the device and verify the result
        }

        public async Task<int> WritePdat_frondend(uint manufacturerNumber, uint Verison, uint partnumber)
        {


            try
            {
                // Read production data from the device
                System.Threading.Thread.Sleep(50);
                FmpProtobuf.Response<TraceData> readTraceDataResponse = await _fmpProtobuf.V2.ReadTraceDataAsync();
                if (readTraceDataResponse.ReturnCode != ReturnCode.NoError) { return -3; }
                TraceData traceData = readTraceDataResponse.Payload;
                //traceData.Subproducts[0].SerialNumber = manufacturerNumber;
                traceData.Subproducts[0].ManufacturerNumber = manufacturerNumber;
                traceData.Subproducts[0].Version = Verison;
                traceData.Subproducts[0].ManufacturingDate = ConvertDateTimeInt(DateTime.Now);
                traceData.Subproducts[0].PartNumber = partnumber;
                // ToDo: Alter read trace data
                // Send a set device mode request to get production data write access.
                System.Threading.Thread.Sleep(50);
                if ((await _fmpProtobuf.V2.SetDeviceModeAsync(DeviceMode.Types.Mode.Pdat, DRV_PDAT_UNLOCK_KEY)).ReturnCode != ReturnCode.NoError) { return -2; }

                System.Threading.Thread.Sleep(50);
                // Write a TraceData object to the device.
                if ((await _fmpProtobuf.V2.WriteTraceDataAsync(traceData)).ReturnCode != ReturnCode.NoError) { return -1; }
            }
            catch
            {


                return -4;
            }
            return 1;
            // ToDo: Re-Read trace data from the device and verify the result
        }



        public async Task<int> WritePdat_frondend(string manufacturerNumber, string Verison, uint partnumber)
        {


            try
            {
                // Read production data from the device
                System.Threading.Thread.Sleep(50);
                FmpProtobuf.Response<TraceData> readTraceDataResponse = await _fmpProtobuf.V2.ReadTraceDataAsync(timeout:1000);
                if (readTraceDataResponse.ReturnCode != ReturnCode.NoError) {
                    return -3; 
                }

                TraceData traceData = readTraceDataResponse.Payload;
                //traceData.Subproducts[0].SerialNumber = manufacturerNumber;
                traceData.Subproducts[0].ManufacturerNumber = str_to_manufactureNumber_4b(manufacturerNumber);
                traceData.Subproducts[0].Version = (uint)(Verison[0] - 'A' + 1);
                traceData.Subproducts[0].ManufacturingDate = ConvertDateTimeInt(DateTime.Now);
                traceData.Subproducts[0].PartNumber = partnumber;
                // ToDo: Alter read trace data
                // Send a set device mode request to get production data write access.
                System.Threading.Thread.Sleep(50);
                if ((await _fmpProtobuf.V2.SetDeviceModeAsync(DeviceMode.Types.Mode.Pdat, DRV_PDAT_UNLOCK_KEY, timeout: 3000)).ReturnCode != ReturnCode.NoError)
                {
                    return -2;
                }


                System.Threading.Thread.Sleep(50);
                // Write a TraceData object to the device.
                if ((await _fmpProtobuf.V2.WriteTraceDataAsync(traceData, timeout: 3000)).ReturnCode != ReturnCode.NoError)
                {
                    return -1;
                }
            }
            catch
            {


                return -4;
            }
            return 1;
            // ToDo: Re-Read trace data from the device and verify the result
        }


        public async Task<ulong[]> readPdat_forend(/*uint manufacturerNumber, uint Verison, uint partnumber*/)
        {


            try
            {
                System.Threading.Thread.Sleep(50);
                // Read production data from the device
                FmpProtobuf.Response<TraceData> readTraceDataResponse = await _fmpProtobuf.V2.ReadTraceDataAsync();
                if (readTraceDataResponse.ReturnCode != ReturnCode.NoError) { return new ulong[] { 0, 0, 0, 0 }; }
                TraceData traceData = readTraceDataResponse.Payload;

                //traceData.Subproducts[0].SerialNumber = manufacturerNumber;
                //traceData.Subproducts[0].ManufacturerNumber = manufacturerNumber;
                //traceData.Subproducts[0].Version = Verison;
                //traceData.Subproducts[0].ManufacturingDate = ConvertDateTimeInt(DateTime.Now);
                //traceData.Subproducts[0].PartNumber = partnumber;
                return new ulong[] { traceData.Subproducts[0].ManufacturerNumber,
                                   traceData.Subproducts[0].Version,
                                   traceData.Subproducts[0].ManufacturingDate,
                                   traceData.Subproducts[0].PartNumber

                                  };
            }
            catch
            {


                return new ulong[] { 0, 0, 0, 0 };
            }
            return new ulong[] { 0, 0, 0, 0 };
            // ToDo: Re-Read trace data from the device and verify the result
        
        
        }











        public async Task< List<object>> ReadPdatMode()
        {


            try
            {
               for(int ci = 0; ci < 3; ci++) { 
                // Read production data from the device
                FmpProtobuf.Response<TraceData> readTraceDataResponse = await _fmpProtobuf.V2.ReadTraceDataAsync();
                if (readTraceDataResponse.ReturnCode != ReturnCode.NoError) { return new List<object> { 0, new DateTime(1970, 1, 1) }; }
                TraceData traceData = readTraceDataResponse.Payload;
                //  return  new List<object> { 1, LongDateTimeToDateTime((long)traceData.Subproducts[0].SerialNumber) } ;
                if((ulong)traceData.Subproducts[0].SerialNumber==0)continue;
                // if (!File.Exists("festoolSerial.txt"))
                //  {
                File.WriteAllText("festoolSerial.txt", "" + (ulong)traceData.Subproducts[0].SerialNumber);
                    // }
                    return new List<object> { 1, LongDateTimeToDateTime((long)traceData.Subproducts[0].SerialNumber) };

                }

               


           }
           catch
           {


               // return new List<object> { 0, DateTime.Now };
               return new List<object> { 0, new DateTime(1970, 1, 1) };
           }
           //  return new List<object> { 0, DateTime.Now };
            return new List<object> { 0, new DateTime(1970, 1, 1) };

           // ToDo: Re-Read trace data from the device and verify the result
       }

       /// <summary>
       /// step6   KEY_SHUT_DOWN_ELECTRONIC
       /// </summary>
       /// <returns></returns>
       public async Task<int>set_shut_down_electronic()
       {


           try
           {
                for (int ci = 0; ci < 1; ci++)
                {
                    BoolValue bl = new BoolValue() { Value = true };
                    FmpProtobuf.Response<TmCommand> setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.ShutDownElectronic, bl);
                    if (setTmCommandResponse.ReturnCode == ReturnCode.NoError /*&& setTmCommandResponse.Payload.BoolParam.Value == true*/) 
                    {
                        return 1;
                    }
                    else continue;
                }
           }
           catch
           {


               return 0;
           }
           return 0;

       }
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
       public async Task<int> set_analog_27_28_30_31_32_34()
       {


           try
           {
               BoolValue bl = new BoolValue() { Value = true };
               FmpProtobuf.Response<TmCommand> setTmCommandResponse = await _fmpProtobuf.V2.SetTmCommandAsync(TmCommand.Types.Key.ShutDownElectronic, bl);
               if (setTmCommandResponse.ReturnCode == ReturnCode.NoError && setTmCommandResponse.Payload.BoolParam.Value == true) return 1;


           }
           catch
           {


               return 0;
           }
           return 0;

       }



       public  uint ConvertDateTimeInt(System.DateTime time)
       {
           System.DateTime startTime = TimeZoneInfo.ConvertTimeFromUtc(new System.DateTime(1970, 1, 1, 8, 00, 00, System.DateTimeKind.Utc), TimeZoneInfo.Local);
           return (uint)(time - startTime).TotalSeconds;
       }

       public  DateTime LongDateTimeToDateTime(long longDateTime)
       {



           DateTime start = new DateTime(1970, 1, 1, 8, 0, 0, DateTimeKind.Utc);
           return start.AddSeconds(longDateTime).ToLocalTime();


       }



       public uint str_to_manufactureNumber_4b(string mfn) {
           string m = mfn;
        return   BitConverter.ToUInt32(new byte[] { (byte)m[0], (byte)m[1], (byte)m[2], (byte)(byte)m[3] }, 0); ;
       }
       /// <summary>
       /// 用于将内存的uint 4字节 byte 转成字符串
       /// </summary>
       /// <param name="b"></param>
       /// <returns></returns>
       public string uint_to_str_formanufacturenumber(uint b) {


           byte[] v = BitConverter.GetBytes(b);

           //	Console.WriteLine($"{v[3]:c}{v[2]:c}{v[1]:c}{v[0]:c}");
          return $"{(char)v[0]}{(char)v[1]}{(char)v[2]}{(char)v[3]}";
       }



       ~FestoolCom() {


            if (_fmpProtobuf != null) _fmpProtobuf.Dispose();
            if (_fmpSupervisor != null) _fmpSupervisor.Dispose();
            if(_transport != null && _transport.IsOpen)_transport.Close();
       

        }


        public  string DecimalTo26Base(int decimalNumber)
        {
            string result = string.Empty;

            while (decimalNumber > 0)
            {
                int remainder = (decimalNumber - 1) % 26;
                result = (char)(remainder + 'A') + result;
                decimalNumber = (decimalNumber - 1) / 26;
            }

            return result;
        }

    }
}
