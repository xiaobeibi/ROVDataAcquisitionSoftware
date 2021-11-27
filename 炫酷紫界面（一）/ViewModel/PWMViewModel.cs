using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 炫酷紫界面_一_.Model;

namespace 炫酷紫界面_一_.ViewModel
{
    public class PWMViewModel : ViewModelBase
    {
        public PWMViewModel()
        {
            //PWM初始化
            PWMData = new PWMControlModel();

            //按钮文本初始化
            ButtonText = "停止";

            //深度计初始化
            DepthGauge = "0";

            //速度初始化
            SpeedOne = "1500";
            SpeedTwo = "0";

            //注册串口发过来的消息
            Messenger.Default.Register<string>(this, "SerialPortReceiveMessage", SerialPortMessagePWM);
        }

        //从串口那接收来的消息
        private void SerialPortMessagePWM(string msg)
        {
            string[] CurrentData = msg.Split(',');

            if (CurrentData.Length == 3 && CurrentData[0].Equals(":DEP"))
            {
                DepthGauge = CurrentData[1];
            }

            if (CurrentData.Length == 12 && CurrentData[0].Equals(":VPM"))
            {
                PWMData.PWM1 = CurrentData[1];
                PWMData.PWM2 = CurrentData[3];
                PWMData.PWM3 = CurrentData[5];
                PWMData.PWM4 = CurrentData[7];
                PWMData.PWM5 = CurrentData[9];
            }
        }

        //PWM数据
        private PWMControlModel pWMData;
        public PWMControlModel PWMData
        {
            get { return pWMData; }
            set { pWMData = value; RaisePropertyChanged(() => PWMData); }
        }

        //深度计
        private string depthGauge;
        public string DepthGauge
        {
            get { return depthGauge; }
            set { depthGauge = value; RaisePropertyChanged(() => DepthGauge); }
        }

        //按钮文本
        private string buttonText;
        public string ButtonText
        {
            get { return buttonText; }
            set { buttonText = value; RaisePropertyChanged(() => ButtonText); }
        }

        //速度1
        private string speedOne;
        public string SpeedOne
        {
            get { return speedOne; }
            set { speedOne = value; RaisePropertyChanged(() => SpeedOne); }
        }

        //速度2
        private string speedTwo;
        public string SpeedTwo
        {
            get { return speedTwo; }
            set { speedTwo = value; RaisePropertyChanged(() => SpeedTwo); }
        }

        #region 按钮区

        //运动停止发送
        private RelayCommand motionStop;
        public RelayCommand MotionStop
        {
            get
            {
                if (motionStop == null) return new RelayCommand(() => MotionStopClicking());
                return motionStop;
            }
            set { motionStop = value; }
        }
        private void MotionStopClicking()
        {
            string msg = ":STP,dd\r\n";
            SendData(msg);
            ButtonText = "停止";
        }

        //运动速度1发送
        private RelayCommand motionSpeed1;
        public RelayCommand MotionSpeed1
        {
            get
            {
                if (motionSpeed1 == null) return new RelayCommand(() => MotionSpeed1Clicking());
                return motionSpeed1;
            }
            set { motionSpeed1 = value; }
        }
        private void MotionSpeed1Clicking()
        {
            string msg = ":SPD1," + SpeedOne.Remove(4) + "\r\n";
            SendData(msg);
        }

        //运动速度2发送
        private RelayCommand motionSpeed2;
        public RelayCommand MotionSpeed2
        {
            get
            {
                if (motionSpeed2 == null) return new RelayCommand(() => MotionSpeed2Clicking());
                return motionSpeed2;
            }
            set { motionSpeed2 = value; }
        }
        private void MotionSpeed2Clicking()
        {
            string msg = ":SPD," + SpeedTwo + "\r\n";
            SendData(msg);
        }

        //运动上浮发送
        private RelayCommand motionFloatUp;
        public RelayCommand MotionFloatUp
        {
            get
            {
                if (motionFloatUp == null) return new RelayCommand(() => MotionFloatUpClicking());
                return motionFloatUp;
            }
            set { motionFloatUp = value; }
        }
        private void MotionFloatUpClicking()
        {
            string msg = ":UP,2f\r\n";
            SendData(msg);
            ButtonText = "上浮";
        }

        //运动左移发送
        private RelayCommand motionShiftLeft;
        public RelayCommand MotionShiftLeft
        {
            get
            {
                if (motionShiftLeft == null) return new RelayCommand(() => MotionShiftLeftClicking());
                return motionShiftLeft;
            }
            set { motionShiftLeft = value; }
        }
        private void MotionShiftLeftClicking()
        {
            string msg = ":LMOVE,51\r\n";
            SendData(msg);
            ButtonText = "左移";
        }

        //运动右移发送
        private RelayCommand motionShiftRight;
        public RelayCommand MotionShiftRight
        {
            get
            {
                if (motionShiftRight == null) return new RelayCommand(() => MotionShiftRightClicking());
                return motionShiftRight;
            }
            set { motionShiftRight = value; }
        }
        private void MotionShiftRightClicking()
        {
            string msg = ":RMOVE,4b\r\n";
            SendData(msg);
            ButtonText = "右移";
        }

        //运动下潜发送
        private RelayCommand motionDive;
        public RelayCommand MotionDive
        {
            get
            {
                if (motionDive == null) return new RelayCommand(() => MotionDiveClicking());
                return motionDive;
            }
            set { motionDive = value; }
        }
        private void MotionDiveClicking()
        {
            string msg = ":DWN,eb\r\n";
            SendData(msg);
            ButtonText = "下潜";
        }

        //运动前进发送
        private RelayCommand motionForward;
        public RelayCommand MotionForward
        {
            get
            {
                if (motionForward == null) return new RelayCommand(() => MotionForwardClicking());
                return motionForward;
            }
            set { motionForward = value; }
        }
        private void MotionForwardClicking()
        {
            string msg = ":FWD,f3\r\n";
            SendData(msg);
            ButtonText = "前进";
        }

        //运动左旋发送
        private RelayCommand motionSinistral;
        public RelayCommand MotionSinistral
        {
            get
            {
                if (motionSinistral == null) return new RelayCommand(() => MotionSinistralClicking());
                return motionSinistral;
            }
            set { motionSinistral = value; }
        }
        private void MotionSinistralClicking()
        {
            string msg = ":LEFT,a9\r\n";
            SendData(msg);
            ButtonText = "左旋";
        }

        //运动右旋发送
        private RelayCommand motionDextral;
        public RelayCommand MotionDextral
        {
            get
            {
                if (motionDextral == null) return new RelayCommand(() => MotionDextralClicking());
                return motionDextral;
            }
            set { motionDextral = value; }
        }
        private void MotionDextralClicking()
        {
            string msg = ":RIGHT,56\r\n";
            SendData(msg);
            ButtonText = "右旋";
        }

        //运动后退发送
        private RelayCommand motionBackOff;
        public RelayCommand MotionBackOff
        {
            get
            {
                if (motionBackOff == null) return new RelayCommand(() => MotionBackOffClicking());
                return motionBackOff;
            }
            set { motionBackOff = value; }
        }
        private void MotionBackOffClicking()
        {
            string msg = ":BCK,04\r\n";
            SendData(msg);
            ButtonText = "后退";
        }

        #endregion

        //发送数据到串口
        private void SendData(string msg)
        {
            Messenger.Default.Send<string>(msg, "SerialPortSendMessage"); //注意参数一致
        }


    }
}
