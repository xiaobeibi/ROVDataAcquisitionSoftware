using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using 炫酷紫界面_一_.Model;
using HandyControl.Controls;

namespace 炫酷紫界面_一_.ViewModel
{
    public class PIDViewModel : ViewModelBase
    {
        public PIDViewModel()
        {
            //PID接收文本初始化
            PidText = "角度:12-12-12 速度:12-5-6 深度:4-5-7";
            //航行模式文本初始化
            NavigationText = "悬停";
            //定航值初始化
            FixedVoyageValue = "0";
            //定深值初始化
            FixedDepthValue = "0";
            //PID数据初始化
            PidData = new PIDControlModel();
            //执行初始化数据调用一次
            SavePIDOnceBegin();

            //注册串口发过来的消息
            Messenger.Default.Register<string>(this, "SerialPortReceiveMessage", SerialPortMessagePID);
        }

        //从串口那接收来的消息
        private void SerialPortMessagePID(string msg)
        {
            string[] CurrentData = msg.Split(',');

            if (CurrentData.Length == 11 && CurrentData[0].Equals(":PID"))
            {
                string newPIDData = "角度:" + CurrentData[1] + "-" + CurrentData[2] + "-" + CurrentData[3] +
                    " 速度:" + CurrentData[4] + "-" + CurrentData[5] + "-" + CurrentData[6] +
                    " 深度:" + CurrentData[7] + "-" + CurrentData[8] + "-" + CurrentData[9];

                if (!PidText.Equals(newPIDData))
                {
                    PidText = newPIDData;
                }
            }
        }

        //初始化PID数据
        private void SavePIDOnceBegin()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("xml.xml");
                XmlNodeList nodeList = xmlDoc.SelectSingleNode("PID").ChildNodes;
                PidData.AngleKp = ((XmlElement)nodeList[0]).InnerText;
                PidData.AngleKi = ((XmlElement)nodeList[1]).InnerText;
                PidData.AngleKd = ((XmlElement)nodeList[2]).InnerText;
                PidData.SpeedKp = ((XmlElement)nodeList[3]).InnerText;
                PidData.SpeedKi = ((XmlElement)nodeList[4]).InnerText;
                PidData.SpeedKd = ((XmlElement)nodeList[5]).InnerText;
                PidData.DepthKp = ((XmlElement)nodeList[6]).InnerText;
                PidData.DepthKi = ((XmlElement)nodeList[7]).InnerText;
                PidData.DepthKd = ((XmlElement)nodeList[8]).InnerText;
            }
            catch (Exception)
            {
                Growl.Warning("读取失败，请检查文件是否存在！！！");
            }
        }

        //PID文本
        private string pidText;
        public string PidText
        {
            get { return pidText; }
            set { pidText = value; RaisePropertyChanged(() => PidText); }
        }

        //定航值
        private string fixedVoyageValue;
        public string FixedVoyageValue
        {
            get { return fixedVoyageValue; }
            set { fixedVoyageValue = value; RaisePropertyChanged(() => FixedVoyageValue); }
        }

        //定深值
        private string fixedDepthValue;
        public string FixedDepthValue
        {
            get { return fixedDepthValue; }
            set { fixedDepthValue = value; RaisePropertyChanged(() => FixedDepthValue); }
        }

        //航行模式文本
        private string navigationText;
        public string NavigationText
        {
            get { return navigationText; }
            set { navigationText = value; RaisePropertyChanged(() => NavigationText); }
        }

        //PID控制文本类
        private PIDControlModel pidData;
        public PIDControlModel PidData
        {
            get { return pidData; }
            set { pidData = value; RaisePropertyChanged(() => PidData); }
        }

        //按钮区

        //定航模式发送
        private RelayCommand fixedVoyageButton;
        public RelayCommand FixedVoyageButton
        {
            get
            {
                if (fixedVoyageButton == null) return new RelayCommand(() => FixedVoyageClicking());
                return fixedVoyageButton;
            }
            set { fixedVoyageButton = value; }
        }
        private void FixedVoyageClicking()
        {
            string msg = ":PSID," + (int)double.Parse(FixedVoyageValue) + ",";
            SendMsgByLrc(msg);
            NavigationText = "定航";
        }

        //悬停模式发送
        private RelayCommand hoverButton;
        public RelayCommand HoverButton
        {
            get
            {
                if (hoverButton == null) return new RelayCommand(() => HoverButtonClicking());
                return hoverButton;
            }
            set { hoverButton = value; }
        }
        private void HoverButtonClicking()
        {
            string msg = ":AUTO," + (int)double.Parse(FixedVoyageValue) + "," + Math.Round(double.Parse(FixedDepthValue), 2) + ",";
            SendMsgByLrc(msg);
            NavigationText = "悬停";
        }

        //定深模式发送
        private RelayCommand fixedDepthButton;
        public RelayCommand FixedDepthButton
        {
            get
            {
                if (fixedDepthButton == null) return new RelayCommand(() => FixedDepthClicking());
                return fixedDepthButton;
            }
            set { fixedDepthButton = value; }
        }
        private void FixedDepthClicking()
        {
            string msg = ":PSIA," + Math.Round(double.Parse(FixedDepthValue), 2) + ",";
            SendMsgByLrc(msg);
            NavigationText = "定深";
        }

        //设置PID发送
        private RelayCommand setupPID;
        public RelayCommand SetupPID
        {
            get
            {
                if (setupPID == null) return new RelayCommand(() => SetupPIDClicking(), CanSwitch);
                return setupPID;
            }
            set { setupPID = value; }
        }
        private void SetupPIDClicking()
        {
            string msg = ":PID," + PidData.AngleKp + "," + PidData.AngleKi + "," + PidData.AngleKd + ","
                          + PidData.SpeedKp + "," + PidData.SpeedKi + "," + PidData.SpeedKd + ","
                          + PidData.DepthKp + "," + PidData.DepthKi + "," + PidData.DepthKd + ",";
            SendMsgByLrc(msg);

            Growl.Success("发送成功！！！");
        }
        private bool CanSwitch()
        {
            if (string.IsNullOrEmpty(PidData.AngleKp) || string.IsNullOrEmpty(PidData.AngleKi) 
                || string.IsNullOrEmpty(PidData.AngleKd) || string.IsNullOrEmpty(PidData.SpeedKp) 
                || string.IsNullOrEmpty(PidData.SpeedKi) || string.IsNullOrEmpty(PidData.SpeedKd) 
                || string.IsNullOrEmpty(PidData.DepthKp) || string.IsNullOrEmpty(PidData.DepthKi) 
                || string.IsNullOrEmpty(PidData.DepthKd))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //保存PID到本地XML文件
        private RelayCommand savePID;
        public RelayCommand SavePID
        {
            get
            {
                if (savePID == null) return new RelayCommand(() => SavePIDClicking());
                return savePID;
            }
            set { savePID = value; }
        }
        private void SavePIDClicking()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("xml.xml");
                XmlNodeList nodeList = xmlDoc.SelectSingleNode("PID").ChildNodes;
                ((XmlElement)nodeList[0]).InnerText = PidData.AngleKp;
                ((XmlElement)nodeList[1]).InnerText = PidData.AngleKi;
                ((XmlElement)nodeList[2]).InnerText = PidData.AngleKd;
                ((XmlElement)nodeList[3]).InnerText = PidData.SpeedKp;
                ((XmlElement)nodeList[4]).InnerText = PidData.SpeedKi;
                ((XmlElement)nodeList[5]).InnerText = PidData.SpeedKd;
                ((XmlElement)nodeList[6]).InnerText = PidData.DepthKp;
                ((XmlElement)nodeList[7]).InnerText = PidData.DepthKi;
                ((XmlElement)nodeList[8]).InnerText = PidData.DepthKd;
                //保存。
                xmlDoc.Save("xml.xml");

                Growl.Success("保存成功！！！");
            }
            catch (Exception)
            {
                Growl.Warning("保存失败，请检查文件是否存在！！！");
            }
        }

        //发送数据到串口
        private void SendData(string msg)
        {
            Messenger.Default.Send<string>(msg, "SerialPortSendMessage"); //注意参数一致
        }


        #region 冗余代码

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

        #endregion

    }
}
