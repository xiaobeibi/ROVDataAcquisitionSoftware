using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 炫酷紫界面_一_.Model;

namespace 炫酷紫界面_一_.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel()
        {
            //初始化传感器数据
            SensorData = new JY9DVLModel();

            //注册串口发过来的消息
            Messenger.Default.Register<string>(this, "SerialPortReceiveMessage", SerialPortMessageHome);
        }

        private JY9DVLModel sensorData;
        public JY9DVLModel SensorData
        {
            get { return sensorData; }
            set { sensorData = value; RaisePropertyChanged(() => SensorData); }
        }

        //从串口那接收来的消息
        private void SerialPortMessageHome(string msg)
        {
            string[] CurrentData = msg.Split(',');

            if (CurrentData.Length == 15 && CurrentData[0].Equals(":JY9") && CurrentData[CurrentData.Length - 1].Equals("$$\r"))
            {
                SensorData.AngleX = CurrentData[7];
                SensorData.AngleY = CurrentData[8];
                SensorData.AngleZ = CurrentData[9];
                SensorData.AngleVelX = CurrentData[4];
                SensorData.AngleVelY = CurrentData[5];
                SensorData.AngleVelZ = CurrentData[6];
                SensorData.AngleAccelX = CurrentData[1];
                SensorData.AngleAccelY = CurrentData[2];
                SensorData.AngleAccelZ = CurrentData[3];
            }
            if (CurrentData.Length == 9 && CurrentData[0].Equals(":DVL") && CurrentData[CurrentData.Length - 1].Equals("$$\r"))
            {
                SensorData.ForwardSpeed = CurrentData[2];
                SensorData.LateralSpeed = CurrentData[3];
                SensorData.VerticalSpeed = CurrentData[4];
                SensorData.HeightBottom = CurrentData[6];
            }
        }

    }
}
