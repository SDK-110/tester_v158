using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ivi.Visa;

namespace testapp.mylib
{
   
 public   class Rigol_shiboqi
    {
        List<int> shangshengyan_index = new List<int>();
        List<int> xiajianyan_index = new List<int>();
        //  USBPortOperator  rigol_dev = null;
        LANPortOperator rigol_dev = null;
        public Rigol_shiboqi(string devname)
        {
            // rigol_dev = PortUltility.usbport_op(devname);
            rigol_dev = PortUltility.lanport_op(devname);
           rigol_dev.Timeout = 4000;
        }

        //public USBPortOperator rigol_dev_get {

        //    get {

        //        return rigol_dev;
        //    }
       // }


        public LANPortOperator rigol_dev_get
        {

            get
            {

                return rigol_dev;
            }
        }

        public static   string[] get_devs() {



            return PortUltility.FindAddresses();
        }

        ~Rigol_shiboqi() {


            if (rigol_dev != null) {

                try
                {
                    rigol_dev.Close();
                }
                catch { }
            }


        }

        internal void test()
        {
            //  mylib.utility_func.callbackdebuginfo(get_cursor_AX_value());
            // mylib.utility_func.callbackdebuginfo(get_measeure_item("VAVG","CHAN1"));
            //   set_wave_form_parm_and_read();
            // set_wave_form_parm_and_read();
            // get_timebase_point();
            set_ini();
            set_trigger_chan_level_coupling(trigger_level: 1.3, sweep_type: "single");
            System.Threading.Thread.Sleep(3000);
            set_wave_form_parm_and_read();
            // get_trigger_postion();



        }

        public void set_ini() {

            //set_chan_display(1, "on");
            //set_chan_display(2, "on");
            //set_timebase_scale(0.0002);
            //set_chan_probe(2, 10);
            //set_chan_scale(2, 2);
            //set_chan_offset(2, 2);

            string setting_parameter_str = "CHAN1:PROB 10;CHAN2:PROB 10;CHAN3:PROB 10;CHAN4:PROB 10"; //探针倍率

            setting_parameter_str = setting_parameter_str + ";" + ":MEAS:ADIS 0";
            setting_parameter_str = setting_parameter_str + ";" + "CHAN1:DISP 1;CHAN2:DISP 1;CHAN3:DISP 1;CHAN4:DISP 1"; //打开所有通过难道

            setting_parameter_str = setting_parameter_str + ";" + "CHAN1:COUP AC;CHAN2:COUP AC;CHAN3:COUP AC;CHAN4:COUP AC"; //设置所有通道耦合方式为AC

            setting_parameter_str = setting_parameter_str + ";" + "CHAN1:SCAL 1;CHAN2:SCAL 1;CHAN3:SCAL 1;CHAN4:SCAL 1"; //设置垂直比例
            setting_parameter_str = setting_parameter_str + ";" + "CHAN1:UNIT VOLT;CHAN2:UNIT VOLT;CHAN3:UNIT VOLT;CHAN4:UNIT VOLT"; //设置探头单位
            setting_parameter_str = setting_parameter_str + ";" + "CHAN1:BWL 20M;CHAN2:BWL 20M;CHAN3:BWL 20M;CHAN4:BWL 20M"; //带宽设置
            setting_parameter_str = setting_parameter_str + ";" + "CHAN1:OFFSET 0;CHAN2:OFFSET 0.5;CHAN3:OFFSET 1;CHAN4:OFFSET -0.5";

            setting_parameter_str = setting_parameter_str + ";" + ":TRIG:EDG:LEV 1";


          foreach ( var st in setting_parameter_str.Split(';'))
                {


                    rigol_dev_get.WriteLine(st);
                    System.Threading.Thread.Sleep(5);
                }


           



            }
            public int triger_porsition() {

            try {
                string setter = $":trigger:position?";
                rigol_dev.WriteLine(setter);
               return int.Parse(rigol_dev.ReadLine());

              }
            catch {

                return -1;
            }

        }



        public void set_wave_form_parm(string channel="1",string mode="NORMAL",string format="byte") {


            string setter = $":waveform:source channel{channel}";
            rigol_dev.WriteLine(setter);
            setter = $":waveform:mode {mode}";
            rigol_dev.WriteLine(setter);
            setter = $":waveform:format {format}";
            rigol_dev.WriteLine(setter);

            setter = $":timebase:offset 0";
            rigol_dev.WriteLine(setter);


        }

        public double[] set_wave_form_parm_and_read(string channel = "1", string mode = "NORMAL", string format = "ascii")
        {
           
            string setter = $":waveform:YORigin?";
            rigol_dev.WriteLine(setter);
            string p1 = rigol_dev.ReadLine();
           double _y_orgin_value = double.Parse(p1);
            
            setter = $":WAVeform:YREFerence?";
            rigol_dev.WriteLine(setter);
            string str_y_ref = rigol_dev.ReadLine();
            double _y_ref_value = double.Parse(str_y_ref);
            setter = $":WAVeform:YINCrement?";
            rigol_dev.WriteLine(setter);
            string pp = rigol_dev.ReadLine();
            double _y_inc_value = double.Parse(pp);

            List<double> data_trf_after = new List<double>();
           
            set_wave_form_parm("1", "NORMAL", "byte");
            setter = $":waveform:data?";
            rigol_dev.WriteLine(setter);
            var mp = rigol_dev.Read(false);
            shangshengyan_index.Clear(); ;
            xiajianyan_index.Clear(); ;

            var indx = get_timebase_point();
            double time_duan = indx.Item2 / 100;


            if (mp.Length < 5) return new double[] { };
            for (int i = 0; i < mp.Length; i++) {

                data_trf_after.Add((mp[i] - _y_orgin_value - _y_ref_value) * _y_inc_value);

      
            }


            for (int i = 0; i < mp.Length-1;) {

                bool sliding_window_short = false;
                if (Math.Abs((mp[i + 1] - mp[i])) >20){ 
                        int max = 0, min = mp[i], position1=0, position2=0;
                    int neibu_loop = 5;
                    if (mp.Length / 5 > 0) { neibu_loop = 5; }
                    else { neibu_loop = mp.Length % 5; }
                        for (int i2 = 0; i2 < neibu_loop; i2++) {

                            if (max < mp[i + i2]) { max = mp[i + i2]; position1 = i; }
                            if (min > mp[i + i2]) { min = mp[i + i2]; position2 = i; }

 
                        }

                        if ((max - min) > 30) {

                            if ((position1 - position2) > 0) { shangshengyan_index.Add(position1); }
                            else
                            {
                                xiajianyan_index.Add(position2);
                            }

                        }
                    sliding_window_short = true;
                }

                if (sliding_window_short == false) { i += 1; }
                else {

                    i += 5;
                }

            }


            if (shangshengyan_index.Count >=2) { 
            double one_shangsheng = time_duan * (shangshengyan_index[1]- shangshengyan_index[0]);

            }

            return data_trf_after.ToArray();
            
        }


        public (double,double) get_timebase_point() {

           string    setter = $":timebase:offset?";
            rigol_dev.WriteLine(setter);

            string rsu = rigol_dev.ReadLine();

            double time_offset = double.Parse(rsu);

            setter = $":timebase:scale?";
            rigol_dev.WriteLine(setter);

            string rsu2 = rigol_dev.ReadLine();

            double time_scale = double.Parse(rsu2);

            return (time_offset, time_scale);
        }

        public void getlist_dalte(double [] array) {

            double temp = 0;
            List<int> positive = new List<int>();
            List<int> negative = new List<int>();
            double[] diff = new double[array.Length - 1];
            for (int i = 0; i < diff.Length; i++)
            {
               temp = diff[i] = array[i + 1] - array[i];
                
             
            }
      
         


        }

        /// <summary>
        /// {SINusoid|SQUare|RAMP|PULSe|NOISe|DC|EXTernal||SINC|EXPRise|EXPFall|ECG|GAUSs|LORentz|HAVersine
        /// 1,2 source
        /// </summary>
        /// <param name="source"></param>

        public void set_sign_source(string source,string type) {

            string setter = $":source{source}:function:shape {type}";

            rigol_dev.WriteLine(setter);



        }

        /// <summary>
        /// CHANnel1|CHANnel2|CHANnel3|CHANnel4|OFF}
        /// </summary>
        /// <param name="chan"></param>


        public void set_measure_counter_source(string chan) {





            string setter = $":measure:counter:source {chan}";

            rigol_dev.WriteLine(setter);

          


        }


        /// <summary>
        /// 打开或关闭全部测量，或查询当前全部测量状态
        /// </summary>
        /// <param name="on_off"></param>
        public void adisplay_measure(string on_off) {



            string setter = $":measure:adisplay {on_off}";

            rigol_dev.WriteLine(setter);



        }

        /// <summary>
        ///         {VMAX|VMIN|VPP|VTOP|VBASe|VAMP|VAVG|VRMS|
       /// OVERshoot|PREShoot|MARea|MPARea|PERiod|
      ////FREQuency|RTIMe|FTIMe|PWIDth|NWIDth|PDUTy|
        //  NDUTy|RDELay|FDELay|RPHase|FPHase|TVMAX|
      // TVMIN|PSLEWrate|NSLEWrate|VUPper|VMID|VLOWer|
      //VARIance|PVRMS|PPULses|NPULses|PEDGes|NEDGes
    /// 
    /// </summary>

    public void set_measeure_statistic_item(string item, string source) {



            string setter = $":measure:statistic:item {item},{source}";

            rigol_dev.WriteLine(setter);


        }



        public void set_measeure_item(string item, string source)
        {



            string setter = $":measure:item {item},{source}";

            rigol_dev.WriteLine(setter);


        }

        public string get_measeure_item(string item, string source)
        {



            string setter = $":measure:item? {item},{source}";

            rigol_dev.WriteLine(setter);

            return rigol_dev.ReadLine();

        }

        /// <summary>
        /// 设置全部功能信号源
        /// </summary>
        /// <param name="source"></param>
        public void set_measure_amsource(string source) {



            string setter = $":measure:amsource {source}";

            rigol_dev.WriteLine(setter);


        }



        /// <summary>
        /// {ITEM1|ITEM2|ITEM3|ITEM4|ITEM5|ALL}
        /// </summary>

        public void clear_measure_recover() {



            string setter = $":measure:recover";

            rigol_dev.WriteLine(setter);



        }


        /// <summary>
        /// 获得频率值
        /// </summary>
        /// <returns></returns>

        public string set_measure_counter_value()
        {





            string setter = $":measure:counter:value?";

            rigol_dev.WriteLine(setter);

            return rigol_dev.ReadLine();



        }
        public string set_gen_par(string setter, bool if_to_read) {

            rigol_dev.WriteLine(setter);
            if (if_to_read) {

              return  rigol_dev.ReadLine();

            }
            return "null";
        }

        public void set_veasure_clear(string item="all") {


            string setter = $":measure:clear {item}";

            rigol_dev.WriteLine(setter);

        }

        /// <summary>
        /// 设置触发电平
        /// </summary>
        /// <param name="slope_type"></param>

        public void set_trigger_level(double level)
        {


            string setter = $":trigger:edge:level {level}";

            rigol_dev.WriteLine(setter);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="trigger_level"></param>
        /// <param name="coupling_type"></param>
        /// <param name="slope_type">Positive|NEGATIVE|RFALI</param>
        public void set_trigger_chan_level_coupling(string channel = "1",double trigger_level=0,string coupling_type = "DC", string slope_type="POSITIVE" , string sweep_type = "norma") {
            set_run(true);
            set_trigger_source($"channel{channel}");
            set_trigger_level(trigger_level);
            set_trigger_coupling(coupling_type);
            set_trigger_slope(slope_type);
            set_trigger_type(sweep_type);
        }

        /// <summary>
        /// CHANnel1|CHANnel2|CHANnel3|CHANnel4
        /// </summary>
        /// <param name="chan"></param>
        public void set_measure_source(string chan) {


            string setter = $":measure:source {chan}";

            rigol_dev.WriteLine(setter);

        }

        /// <summary>
        /// {AC|DC|LFReject|HFReject}
        /// </summary>
        /// <param name="mode"></param>

        public void set_trigger_coupling(string coup_mode)
        {


            string setter = $":trigger:coupling {coup_mode}";

            rigol_dev.WriteLine(setter);
        }

        public string get_trigger_status()
        {


            string setter = $":trigger:status?";

            rigol_dev.WriteLine(setter);
            return rigol_dev.ReadLine();
        }



        /// <summary>
        /// 设置上升沿或者下降沿    {POSitive|NEGative|RFALl}
        /// </summary>
        /// <param name="trig_source"></param>
        public void set_trigger_slope(string slope_type)
        {


            string setter = $":trigger:edge:slope {slope_type}";

            rigol_dev.WriteLine(setter);
        }

        /// <summary>
        /// {D0|D1|D2|D3|D4|D5|D6|D7|D8|D9|D10|D11|D12|D13|D14|D15|CHANnel1|CHANnel2|CHANnel3|CHANnel4|AC
        /// 
        /// 
        /// </summary>
        /// <param name="trig_source"></param>

        public void set_trigger_source(string trig_source)
        {


            string setter = $":trigger:edge:source {trig_source}";

            rigol_dev.WriteLine(setter);
        }


        /// <summary>
        /// 查询触发内存位置点
        /// </summary>
        /// <returns></returns>

        public string get_trigger_postion()
        {


            string setter = $":trigger:position?";

            rigol_dev.WriteLine(setter);

            string ru = rigol_dev.ReadLine();
            return ru;
                
        }



        /// <summary>
        /// 噪声抑制
        /// </summary>
        /// <param name="on_off"></param>
        public void set_trigger_nreject(string on_off) {


            string setter = $":trigger:nreject {on_off}";

            rigol_dev.WriteLine(setter);


        }
        /// <summary>
        /// {AUTO|NORMal|SINGle}
        /// </summary>
        /// <param name="trig_type"></param>

        public void set_trigger_type(string trig_type)
        {


            string setter = $":trigger:sweep {trig_type}";

            rigol_dev.WriteLine(setter);
        }

        /// <summary>
        /// 
        /// {EDGE|PULSe|RUNT|WIND|NEDG|SLOPe|VIDeo|PATTern| DELay|TIMeout|DURation|SHOLd|RS232|IIC|SPI

        /// 
        /// </summary>


        public void set_trigger_mode(string mode)
        {


            string setter = $":trigger:mode {mode}";

            rigol_dev.WriteLine(setter);
        }


        public void set_timebase_scale(double time)
        {


            string setter = $":timebase:scale {time}";

            rigol_dev.WriteLine(setter);

        }



        public void set_chan_display(int chan, string on_off)
        {


            string setter = $":CHANnel{chan}:display {on_off}";  //AC/DC/GND

            rigol_dev.WriteLine(setter);

        }




        /// <summary>
        /// 耦合方式
        /// </summary>
        /// <param name="chan"></param>
        /// <param name="type"></param>
        public void set_chan_coupling_type(int chan, string type)
        {


            string setter = $":CHANnel{chan}:units {type}";  //AC/DC/GND

            rigol_dev.WriteLine(setter);

        }

        public void set_run(bool is_run) {

            if (is_run)
            {
                rigol_dev.WriteLine(":clear");
                rigol_dev.WriteLine(":RUN");
            }
            else {
                rigol_dev.WriteLine(":STOP");

            }
            
        }

        public void single_trigger() {

            rigol_dev.WriteLine(":single");

        }

        public void set_chan_offset(int chan, float offset) {


            string setter = $":CHANnel{chan}:offset {offset}";

            rigol_dev.WriteLine(setter);
        }

        public void set_chan_range(int chan, float range) {


            string setter = $":CHANnel{chan}:range {range}";

            rigol_dev.WriteLine(setter);

        }

        public void set_chan_scale(int chan, float scale)
        {


            string setter = $":CHANnel{chan}:scale {scale}";

            rigol_dev.WriteLine(setter);

        }

        public void set_chan_probe(int chan, float probe_rate)
        {


            string setter = $":CHANnel{chan}:probe {probe_rate}";

            rigol_dev.WriteLine(setter);

        }
        /// <summary>
        /// 1.voltage 2.watt 3.ampere 4.unknown
        /// </summary>
        /// <param name="chan"></param>
        /// <param name="probe_rate"></param>
        public void set_chan_units(int chan, string unit_type)
        {


            string setter = $":CHANnel{chan}:units {unit_type}";

            rigol_dev.WriteLine(setter);

        }
        /// <summary>
        /// off/manual/track/auto/xy
        /// </summary>
        /// <param name="mode"></param>
        public void set_cursor(string mode) {


            string setter = $":cursor:mode {mode}";

            rigol_dev.WriteLine(setter);

        }

        public void set_manual_cursor_type( int chan) {


            string setter = $":cursor:source {chan}";

            rigol_dev.WriteLine(setter);
            setter = $":cursor:manual S";            //秒 S/HZ/DEGRee/PERCent

            rigol_dev.WriteLine(setter);
            setter = $":cursor:manual  source";  //PERCent/SOURce
 
            rigol_dev.WriteLine(setter);



        }


        public void set_cursor_AX_value(int value)
        {


            string setter = $":cursor:manual:ax {value}";

            rigol_dev.WriteLine(setter);




        }

        public string get_cursor_AX_value()
        {


            string setter = $":cursor:manual:axvalue?";

            rigol_dev.WriteLine(setter);


            return rigol_dev.ReadLine();

        }

       

    }
}
