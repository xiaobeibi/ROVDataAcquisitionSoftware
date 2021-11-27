using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AUV上位机2._0
{
    public partial class AUV : Form
    {
        #region 全局变量
        private string[] FunctionCode;  //功能码
        GPS gps = null;     //GPS
        JY9 jy9 = null;     //姿态传感器
        PID pid = null;     //PID参数
        DVL dvl = null;     //DVL参数
        Propeller[] propeller = new Propeller[5];   //电机
        public double depth;    //深度
        public bool leak;   //漏水检测
        #endregion

        public AUV()
        {
            InitializeComponent();
            VariatesInit();
            Control.CheckForIllegalCrossThreadCalls = false;

        }
        /// <summary>
        /// 变量初始化
        /// </summary>
        private void VariatesInit()
        {
            FunctionCode = new string[]
            {
                "GPS",   //GPS
                "JY9",  //姿态传感器
                "ALT",  //高度 
                "VPM",  //速度  
                "DEP",  //深度    
                "PID",  //PID参数设定
                "LEK",  //漏水传感器                  
            };
            gps = new GPS();
            jy9 = new JY9();
            pid = new PID();
            dvl = new DVL();

            for (int i = 0; i < propeller.Length; i++)
            {
                propeller[i] = new Propeller();
            }
        }

        private void AUV_Load(object sender, EventArgs e)
        {
            string[] portList = SerialPort.GetPortNames();
            if (portList.Length > 0)
            {
                serialBox.Text = portList[0];
                for(int i = 0; i < portList.Length; i++)
                {
                    serialBox.Items.Add(portList[i]);
                }
            }
            //初始化
            baudBox.Text = "9600";
            parityBox.Text = "无";
            stopBox.Text = "1";
            dateBox.Text = "8";
            //绑定事件
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //string str = null;
            this.Invoke((EventHandler)(delegate
            {
                string str = serialPort1.ReadLine();
                recvBox.AppendText(str + "\r\n");
                GetData(str);
            }));
            //GetData(str);
        }

        private void GetData(string str)
        {
            string[] data = str.Split(',');
            for (int i = 0; i < data.Length; i++)
            {
                //GPS
                if (data.Length == 5 && data[0].Equals(":GPS"))
                {
                    gps.longitude = Double.Parse(data[1]);
                    gps.latitude = Double.Parse(data[2]);
                }
                if (data.Length == 15 && data[0].Equals(":JY9") && data[14].Equals("$$\r"))
                {
                    jy9.x_acceleration = Double.Parse(data[1]);
                    jy9.y_acceleration = Double.Parse(data[2]);
                    jy9.z_acceleration = Double.Parse(data[3]);

                    jy9.x_angle_speed = Double.Parse(data[4]);
                    jy9.y_angle_speed = Double.Parse(data[5]);
                    jy9.z_angle_speed = Double.Parse(data[6]);

                    jy9.x_angle = Double.Parse(data[7]);
                    jy9.y_angle = Double.Parse(data[8]);
                    jy9.z_angle = Double.Parse(data[9]);

                    //DisplayData();
                    //Console.WriteLine(data[i]);
                }
                if (data.Length == 10 && data[0].Equals(":DVL"))
                {
                    dvl.Vx = float.Parse(data[2]);
                    dvl.Vy = float.Parse(data[3]);
                    dvl.Vz = float.Parse(data[4]);
                    dvl.high = float.Parse(data[6]) * 100;
                    dvl.n_or_y = Convert.ToChar(data[7]);
                }
                //深度传感器
                if (data.Length == 3 && data[0].Equals(":DEP"))
                {
                    depth = Double.Parse(data[1]);
                }
                //漏水传感器
                if (data.Length == 3 && data[0].Equals(":LEK"))
                {
                    leak = (Convert.ToInt16(data[1]) == 1 ? true : false);
                }
                //速度
                if (data.Length == 12 && data[0].Equals(":VPM"))
                {
                    // 主推
                    propeller[0].speed = Convert.ToInt16(data[1]);
                    propeller[0].direction = (Convert.ToInt16(data[2]) == 1 ? true : false);
                    //侧推1
                    propeller[1].speed = Convert.ToInt16(data[3]);
                    propeller[1].direction = (Convert.ToInt16(data[4]) == 1 ? true : false);
                    //侧推2
                    propeller[2].speed = Convert.ToInt16(data[5]);
                    propeller[2].direction = (Convert.ToInt16(data[6]) == 1 ? true : false);
                    //垂推1
                    propeller[3].speed = Convert.ToInt16(data[7]);
                    propeller[3].direction = (Convert.ToInt16(data[8]) == 1 ? true : false);
                    //垂推2
                    propeller[4].speed = Convert.ToInt16(data[9]);
                    propeller[4].direction = (Convert.ToInt16(data[10]) == 1 ? true : false);
                }
                //PID设置
                if (data.Length == 8 && data[0].Equals(":PID"))
                {
                    pid.Kp = Convert.ToInt16(data[1]);
                    pid.Ki = Convert.ToInt16(data[2]);
                    pid.Kd = Convert.ToInt16(data[3]);
                }
            }
            DisplayData();
        }

        private void DisplayData()
        {
            //姿态
            x_angle.Text = Convert.ToString(jy9.x_angle);
            y_angle.Text = Convert.ToString(jy9.y_angle);
            z_angle.Text = Convert.ToString(jy9.z_angle);

            x_angle_spd.Text = Convert.ToString(jy9.x_angle_speed);
            y_angle_spd.Text = Convert.ToString(jy9.y_angle_speed);
            z_angle_spd.Text = Convert.ToString(jy9.z_angle_speed);

            x_acce.Text = Convert.ToString(jy9.x_acceleration);
            y_acce.Text = Convert.ToString(jy9.y_acceleration);
            z_acce.Text = Convert.ToString(jy9.z_acceleration);
            //DVL
            Vx_textBox.Text = Convert.ToString(dvl.Vx);
            Vy_textBox.Text = Convert.ToString(dvl.Vy);
            Vz_textBox.Text = Convert.ToString(dvl.Vz);
            High_textBox.Text = Convert.ToString(dvl.high);
            //GPS
            LongitextBox.Text = Convert.ToString(gps.longitude);
            LatitextBox.Text = Convert.ToString(gps.latitude);
            //深度
            Depth_textBox.Text = Convert.ToString(depth);
            //PWM
            Pwm1_textBox.Text = Convert.ToString(propeller[0].speed);
            Pwm2_textBox.Text = Convert.ToString(propeller[1].speed);
            Pwm3_textBox.Text = Convert.ToString(propeller[2].speed);
            Pwm4_textBox.Text = Convert.ToString(propeller[3].speed);
            Pwm5_textBox.Text = Convert.ToString(propeller[4].speed);
        }
            /// <summary>
            /// lrc校验码
            /// </summary>
            private char[] LrcCheckCode(string raw_s)
        {
            char[] lrc_s = new char[] { '0', '0', '0' };
            char[] hex_s = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            int check = 0;
            for (int i = 0; i < raw_s.Length; i++)
            {
                check += raw_s[i];
            }

            check = 256 - check % 256;
            lrc_s[0] = hex_s[check / 16];
            lrc_s[1] = hex_s[check % 16];
            lrc_s[2] = '\0';
            return lrc_s;
        }

        /// <summary>
        /// 通过串口发送
        /// </summary>
        /// <param name="str"></param>
        private void SendDate(string str)
        {
            if (serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.Write(str);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        //发送校验后的msg
        private void SendMsgByLrc(string msg)
        {
            char[] lrc = LrcCheckCode(msg);
            StringBuilder sb = new StringBuilder();
            foreach (char c in lrc)
            {
                sb.Append(c);
            }
            string s = sb.ToString();
            msg = msg + s + "\r\n";
            SendData(msg);
        }

        //串口发送
        private void SendData(string msg)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.WriteLine(msg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void openSerial_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {

                serialPort1.PortName = serialBox.Text;
                serialPort1.BaudRate = Convert.ToInt32(baudBox.Text,10);

                float f = Convert.ToSingle(stopBox.Text.Trim());
                if (f == 0)//设置停止位
                    serialPort1.StopBits = StopBits.None;
                else if (f == 1.5)
                    serialPort1.StopBits = StopBits.OnePointFive;
                else if (f == 1)
                    serialPort1.StopBits = StopBits.One;
                else if (f == 2)
                    serialPort1.StopBits = StopBits.Two;
                else
                    serialPort1.StopBits = StopBits.One;
                //设置数据位
                serialPort1.DataBits = Convert.ToInt32(dateBox.Text.Trim());
                //设置奇偶校验位
                string s = parityBox.Text.Trim();
                if (s.CompareTo("无") == 0)
                    serialPort1.Parity = Parity.None;
                else if (s.CompareTo("奇校验") == 0)
                    serialPort1.Parity = Parity.Odd;
                else if (s.CompareTo("偶校验") == 0)
                    serialPort1.Parity = Parity.Even;
                else
                    serialPort1.Parity = Parity.None;
            
               serialPort1.Open();
                serialPort1.DataReceived += serialPort1_DataReceived;
                 try
                 {  
                    openSerial.Text = "关闭串口";
                    serialBox.Enabled = false;
                    baudBox.Enabled = false;
                    parityBox.Enabled = false;
                    stopBox.Enabled = false;
                    dateBox.Enabled = false;
                }
                catch
                {
                    MessageBox.Show("串口打开失败！", "错误");
                }

            }

             else if (serialPort1.IsOpen)   //如果串口是打开的则将其关闭
             {
     
                try
                {                
                    serialPort1.DataReceived -= serialPort1_DataReceived;
                    serialPort1.Close();
                    openSerial.Text = "打开串口";
                    MessageBox.Show("串口已关闭");
                }
                catch
                {
                    MessageBox.Show("串口关闭失败！", "错误");
                }              
                serialBox.Enabled = true;
                baudBox.Enabled = true;
                parityBox.Enabled = true;
                stopBox.Enabled = true;
                dateBox.Enabled = true;
            }
        }

        private void Forword_Btn_Click(object sender, EventArgs e)
        {
            string msg = ":FWD,f3\r\n";
            SendData(msg);
            tishi.Text = "前进";
        }

        private void Back_Btn_Click(object sender, EventArgs e)
        {
            string msg = ":BCK,04\r\n";
            SendData(msg);
            tishi.Text = "后退";
        }

        private void Right_Btn_Click(object sender, EventArgs e)
        {
            string msg = ":RIGHT,56\r\n";
            SendData(msg);
            tishi.Text = "右转";
        }

        private void Left_Btn_Click(object sender, EventArgs e)
        {
            string msg = ":LEFT,a9\r\n";
            SendData(msg);
            tishi.Text = "左转";
        }

        private void Up_Btn_Click(object sender, EventArgs e)
        {
            string msg = ":UP,2f\r\n";
            SendData(msg);
            tishi.Text = "上浮";
        }

        private void Down_Btn_Click(object sender, EventArgs e)
        {
            string msg = ":DWN,eb\r\n";
            SendData(msg);
            tishi.Text = "下潜";
        }

        private void Stop_Btn_Click(object sender, EventArgs e)
        {
            string msg = ":STP,dd\r\n";
            SendData(msg);
            tishi.Text = "停止";
        }

        private void LMOVE_Btn_Click(object sender, EventArgs e)
        {
            string msg = ":LMOVE,51\r\n";
            SendData(msg);
            tishi.Text = "左移";
        }

        private void RMOVE_Btn_Click(object sender, EventArgs e)
        {
            string msg = ":RMOVE,4b\r\n";
            SendData(msg);
            tishi.Text = "右移";
        }

        private void Speed_trackBar_ValueChanged(object sender, EventArgs e)
        {
            Speed_label.Text = Convert.ToString(Speed_trackBar.Value);
        }

        private void SetSpd_Btn_Click(object sender, EventArgs e)
        {
            string msg = null; 
            if (Speed_trackBar.Value == 0)
            {
                msg = ":SPD,0,57\r\n";
            }
            else if(Speed_trackBar.Value == 1)
            {
                 msg = ":SPD,1,56\r\n";
            }
            else if (Speed_trackBar.Value == 2)
            {
                msg = ":SPD,2,55\r\n";
            }
            else if (Speed_trackBar.Value == 3)
            {
                msg = ":SPD,3,54\r\n";
            }
            else if (Speed_trackBar.Value == 4)
            {
                msg = ":SPD,4,53\r\n";
            }
            else if (Speed_trackBar.Value == 5)
            {
                msg = ":SPD,5,52\r\n";
            }
            SendData(msg);
        }

        private void ScanUart_Btn_Click(object sender, EventArgs e)
        {
            serialBox.Text = "";
            SearchAndAddSerialToComboBox(serialPort1, serialBox);
        }
        private void SearchAndAddSerialToComboBox(SerialPort MyPort, ComboBox MyBox)
        {
            string Buffer;                                              //缓存
            MyBox.Items.Clear();                                        //清空ComboBox内容
                                                                        //int count = 0;
            for (int i = 1; i < 20; i++)                                //循环
            {
                try                                                     //核心原理是依靠try和catch完成遍历
                {
                    Buffer = "COM" + i.ToString();
                    MyPort.PortName = Buffer;
                    MyPort.Open();                                      //如果失败，后面的代码不会执行
                                                                        // MyString[count] = Buffer;
                    MyBox.Items.Add(Buffer);                            //打开成功，添加至下俩列表
                    MyPort.Close();                                     //关闭
                                                                        //count++;
                }
                catch
                {

                }
            }
        }

        private void SetPID_Btn_Click(object sender, EventArgs e)
        {
            string msg = null;
            //msg = ":KP," + Angle_KP_Box.Text + ",";
            //SendMsgByLrc(msg);

            //msg = ":KI," + Angle_KI_Box + ",";
            //SendMsgByLrc(msg);

            //msg = ":KD," + Angle_KD_Box + ",";
            //SendMsgByLrc(msg);

            msg = ":PID," + Angle_KP_Box.Text + "," + Angle_KI_Box.Text + "," + Angle_KD_Box.Text + "," 
                          + Speed_KP_Box.Text + "," + Speed_KI_Box.Text + "," + Speed_KD_Box.Text + "," 
                          + Depth_KP_Box.Text + "," + Depth_KI_Box.Text + "," + Depth_KD_Box.Text + ",";
            SendMsgByLrc(msg);
        }

        private void SetHeading_Btn_Click(object sender, EventArgs e)
        {
            string msg = null;
            //msg = ":SETHead," + SetHeading_textBox.Text + ",";
            msg = ":PSID," + SetHeading_textBox.Text + ",";
            SendMsgByLrc(msg);
            moshi.Text = "定航";
        }

        private void SetDepth_Btn_Click(object sender, EventArgs e)
        {
            string msg = null;
            //msg = ":SETDepth," + SetDepth_textBox.Text + ",";
            msg = ":PSIA," + SetDepth_textBox.Text + ",";
            SendMsgByLrc(msg);
            moshi.Text = SetHeading_textBox.Text;
        }

        private void Hover_Btn_Click(object sender, EventArgs e)
        {
            string msg= ":AUTO," + SetHeading_textBox.Text +","+ SetDepth_textBox .Text+ ",";
            SendMsgByLrc(msg);
            moshi.Text = "悬停";
        }

        private void Clear_PID_Btn_Click(object sender, EventArgs e)
        {
            Angle_KP_Box.Clear();
            Angle_KI_Box.Clear();
            Angle_KD_Box.Clear();

            Speed_KP_Box.Clear();
            Speed_KI_Box.Clear();
            Speed_KD_Box.Clear();
        }
    }
}
