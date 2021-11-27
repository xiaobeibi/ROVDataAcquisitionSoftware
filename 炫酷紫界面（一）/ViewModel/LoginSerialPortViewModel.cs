using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 炫酷紫界面_一_.Model;

namespace 炫酷紫界面_一_.ViewModel
{
    public class LoginSerialPortViewModel : ViewModelBase
    {
        //新建串口对象
        public SerialPort serialPort = new SerialPort();

        public LoginSerialPortViewModel()
        {
            InitSerialPort();

            //串口传递过来要发送的消息
            Messenger.Default.Register<string>(this, "SerialPortSendMessage", SerialPortMessageSending);

            ConnectButtonText = "登录";
            ImageText = "#FF520029";
        }
        /// <summary>
        /// 串口发送别的ViewModel发过来的数据
        /// </summary>
        /// <param name="msg"></param>
        private void SerialPortMessageSending(string msg)
        {
            if (serialPort.IsOpen)
            {
                try
                {
                    //向串口发送字符串
                    serialPort.WriteLine(msg);
                    //向串口发送整数字节（需重写优化）
                }
                catch (Exception)
                {

                }
            }
        }

        /// <summary>
        /// 初始化设置串口
        /// </summary>
        public void InitSerialPort()
        {
            //获取当前计算机的串行端口名的数组。
            string[] ports = SerialPort.GetPortNames();
            //串口号初始化
            SerialPortNumberCombboxList = new List<SerialPortNumberModel>();
            try
            {
                for (int index = 0; index < ports.Length; index++)
                {
                    SerialPortNumberCombboxList.Add(new SerialPortNumberModel("" + index, ports[index]));
                }

                SerialPortNumberCombboxItem = SerialPortNumberCombboxList[0];
            }
            catch
            {
                SerialPortNumberCombboxItem = new SerialPortNumberModel("", "");
            }

            //串口波特率初始化
            SerialPortBaudRateCombboxList = new List<SerialPortBaudRateModel>
            {
                new SerialPortBaudRateModel("0", "600"),
                new SerialPortBaudRateModel("1", "1200"),
                new SerialPortBaudRateModel("2", "2400"),
                new SerialPortBaudRateModel("3", "4800"),
                new SerialPortBaudRateModel("4", "9600"),
                new SerialPortBaudRateModel("5", "14400"),
                new SerialPortBaudRateModel("6", "19200"),
                new SerialPortBaudRateModel("7", "38400"),
                new SerialPortBaudRateModel("8", "43000"),
                new SerialPortBaudRateModel("9", "57600"),
                new SerialPortBaudRateModel("10", "76800"),
                new SerialPortBaudRateModel("11", "115200"),
                new SerialPortBaudRateModel("12", "128000"),
                new SerialPortBaudRateModel("13", "230400"),
                new SerialPortBaudRateModel("14", "256000"),
                new SerialPortBaudRateModel("15", "460800")
            };
            SerialPortBaudRateCombboxItem = SerialPortBaudRateCombboxList[11];

            //串口停止位初始化
            SerialPortStopBitCombboxList = new List<SerialPortStopBitModel>
            {
                new SerialPortStopBitModel("0", "None"),
                new SerialPortStopBitModel("1", "One"),
                new SerialPortStopBitModel("2", "Two"),
                new SerialPortStopBitModel("3", "OnePointFive")
            };
            SerialPortStopBitCombboxItem = SerialPortStopBitCombboxList[1];

            //串口数据位初始化
            SerialPortDataBitCombboxList = new List<SerialPortDataBitModel>
            {
                new SerialPortDataBitModel("0", "8"),
                new SerialPortDataBitModel("1", "7"),
                new SerialPortDataBitModel("2", "6"),
                new SerialPortDataBitModel("3", "5")
            };
            SerialPortDataBitCombboxItem = SerialPortDataBitCombboxList[0];

            //串口校验位初始化
            SerialPortCheckBitCombboxList = new List<SerialPortCheckBitModel>
            {
                new SerialPortCheckBitModel("0", "None"),
                new SerialPortCheckBitModel("1", "Odd"),
                new SerialPortCheckBitModel("2", "Even")
            };
            SerialPortCheckBitCombboxItem = SerialPortCheckBitCombboxList[0];
        }

        /// <summary>
        /// 串口号赋值
        /// </summary>
        public void ConnectSerialPort()
        {
            try
            {
                serialPort.PortName = SerialPortNumberCombboxItem.SerialPortNumberText;//串口号
                serialPort.BaudRate = Convert.ToInt32(SerialPortBaudRateCombboxItem.SerialPortBaudRateText);//波特率
                serialPort.DataBits = Convert.ToInt32(SerialPortDataBitCombboxItem.SerialPortDataBitText);//数据位
                //停止位
                if (SerialPortStopBitCombboxItem.SerialPortStopBitText.Equals("None"))
                {
                    serialPort.StopBits = StopBits.None;
                }
                else if (SerialPortStopBitCombboxItem.SerialPortStopBitText.Equals("Two"))
                {
                    serialPort.StopBits = StopBits.Two;
                }
                else if (SerialPortStopBitCombboxItem.SerialPortStopBitText.Equals("OnePointFive"))
                {
                    serialPort.StopBits = StopBits.OnePointFive;
                }
                else
                {
                    serialPort.StopBits = StopBits.One;
                }
                //校验位
                if (SerialPortCheckBitCombboxItem.SerialPortCheckBitText.Equals("Odd"))
                {
                    serialPort.Parity = Parity.Odd;
                }
                else if (SerialPortCheckBitCombboxItem.SerialPortCheckBitText.Equals("Even"))
                {
                    serialPort.Parity = Parity.Even;
                }
                else
                {
                    serialPort.Parity = Parity.None;
                }
            }
            catch
            {
                //关闭串口时回抛异常
            }
        }

        /// <summary>
        /// 串口号选中信息
        /// </summary>
        private SerialPortNumberModel serialPortNumberCombboxItem;
        public SerialPortNumberModel SerialPortNumberCombboxItem
        {
            get { return serialPortNumberCombboxItem; }
            set { serialPortNumberCombboxItem = value; RaisePropertyChanged(() => SerialPortNumberCombboxItem); }
        }
        /// <summary>
        /// 串口号下拉框列表
        /// </summary>
        private List<SerialPortNumberModel> serialPortNumberCombboxList;
        public List<SerialPortNumberModel> SerialPortNumberCombboxList
        {
            get { return serialPortNumberCombboxList; }
            set { serialPortNumberCombboxList = value; RaisePropertyChanged(() => SerialPortNumberCombboxList); }
        }

        /// <summary>
        /// 串口波特率选中信息
        /// </summary>
        private SerialPortBaudRateModel serialPortBaudRateCombboxItem;
        public SerialPortBaudRateModel SerialPortBaudRateCombboxItem
        {
            get { return serialPortBaudRateCombboxItem; }
            set { serialPortBaudRateCombboxItem = value; RaisePropertyChanged(() => SerialPortBaudRateCombboxItem); }
        }
        /// <summary>
        /// 串口波特率下拉框列表
        /// </summary>
        private List<SerialPortBaudRateModel> serialPortBaudRateCombboxList;
        public List<SerialPortBaudRateModel> SerialPortBaudRateCombboxList
        {
            get { return serialPortBaudRateCombboxList; }
            set { serialPortBaudRateCombboxList = value; RaisePropertyChanged(() => SerialPortBaudRateCombboxList); }
        }

        /// <summary>
        /// 串口停止位选中信息
        /// </summary>
        private SerialPortStopBitModel serialPortStopBitCombboxItem;
        public SerialPortStopBitModel SerialPortStopBitCombboxItem
        {
            get { return serialPortStopBitCombboxItem; }
            set { serialPortStopBitCombboxItem = value; RaisePropertyChanged(() => SerialPortStopBitCombboxItem); }
        }
        /// <summary>
        /// 串口停止位下拉框列表
        /// </summary>
        private List<SerialPortStopBitModel> serialPortStopBitCombboxList;
        public List<SerialPortStopBitModel> SerialPortStopBitCombboxList
        {
            get { return serialPortStopBitCombboxList; }
            set { serialPortStopBitCombboxList = value; RaisePropertyChanged(() => SerialPortStopBitCombboxList); }
        }

        /// <summary>
        /// 串口数据位选中信息
        /// </summary>
        private SerialPortDataBitModel serialPortDataBitCombboxItem;
        public SerialPortDataBitModel SerialPortDataBitCombboxItem
        {
            get { return serialPortDataBitCombboxItem; }
            set { serialPortDataBitCombboxItem = value; RaisePropertyChanged(() => SerialPortDataBitCombboxItem); }
        }
        /// <summary>
        /// 串口数据位下拉框列表
        /// </summary>
        private List<SerialPortDataBitModel> serialPortDataBitCombboxList;
        public List<SerialPortDataBitModel> SerialPortDataBitCombboxList
        {
            get { return serialPortDataBitCombboxList; }
            set { serialPortDataBitCombboxList = value; RaisePropertyChanged(() => SerialPortDataBitCombboxList); }
        }

        /// <summary>
        /// 串口校验位选中信息
        /// </summary>
        private SerialPortCheckBitModel serialPortCheckBitCombboxItem;
        public SerialPortCheckBitModel SerialPortCheckBitCombboxItem
        {
            get { return serialPortCheckBitCombboxItem; }
            set { serialPortCheckBitCombboxItem = value; RaisePropertyChanged(() => SerialPortCheckBitCombboxItem); }
        }
        /// <summary>
        /// 串口校验位下拉框列表
        /// </summary>
        private List<SerialPortCheckBitModel> serialPortCheckBitCombboxList;
        public List<SerialPortCheckBitModel> SerialPortCheckBitCombboxList
        {
            get { return serialPortCheckBitCombboxList; }
            set { serialPortCheckBitCombboxList = value; RaisePropertyChanged(() => SerialPortCheckBitCombboxList); }
        }

        private string imageText;
        public string ImageText
        {
            get { return imageText; }
            set { imageText = value; RaisePropertyChanged(() => ImageText); }
        }

        private string connectButtonText;
        public string ConnectButtonText
        {
            get { return connectButtonText; }
            set { connectButtonText = value; RaisePropertyChanged(() => ConnectButtonText); }
        }

        //连接命令
        private RelayCommand connectCmd;
        public RelayCommand ConnectCmd
        {
            get
            {
                if (connectCmd == null)
                    return new RelayCommand(() => SerialPortSwitch(), CanSwitch);
                return connectCmd;
            }
            set { connectCmd = value; }
        }
        private void SerialPortSwitch()
        {
            ConnectSerialPort();
            if (ConnectButtonText.Equals("登录"))
            {
                try
                {
                    serialPort.Open();
                    serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);//添加数据接收事件
                    ConnectButtonText = "断开";
                    ImageText = "#FF2BAB36";
                }
                catch (Exception)
                {
                    Messenger.Default.Send<string>("串口占用或失效", "SerialPortErrorMessage"); //注意：token参数一致
                }
            }
            else
            {
                try
                {
                    serialPort.DataReceived -= DataReceivedHandler;
                    serialPort.Close();
                    ConnectButtonText = "登录";
                    ImageText = "#FF520029";
                }
                catch (Exception)
                {

                }
            }

        }
        private bool CanSwitch()
        {
            return !string.IsNullOrEmpty(SerialPortNumberCombboxItem.SerialPortNumberText);

        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    int len = serialPort.BytesToRead;
                    if (len == 0)
                    {
                        return;
                    }

                    //char[] buffer = new char[len];
                    //this.serialPort.Read(buffer, 0, len);
                    string receiveData = serialPort.ReadLine();

                    Messenger.Default.Send<string>(receiveData, "SerialPortReceiveMessage"); //注意：token参数一致
                }
            }
            catch (Exception)
            {

            }
        }



    }
}
