using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 炫酷紫界面_一_.ViewModel
{
    public class DebugPageViewModel : ViewModelBase
    {
        public DebugPageViewModel()
        {
            //注册串口发过来的消息
            Messenger.Default.Register<string>(this, "SerialPortReceiveMessage", SerialPortMessageReceiving);

            SerialPortReceiveBit = "0";
            ReceiveCount = 0;
            SendCount = 0;
            SerialPortSendBit = "0";
            SerialPortButtonText = "开启接收";
        }

        //从串口那接收来的消息
        private void SerialPortMessageReceiving(string msg)
        {
            if (SerialPortButtonText.Equals("关闭接收"))
            {
                //时间戳
                DateTime SerialPortnow = DateTime.Now;
                string SerialPortTimeDateString = string.Format("{0}:{1}:{2}-->",
                    SerialPortnow.Hour.ToString("00"),
                    SerialPortnow.Minute.ToString("00"),
                    SerialPortnow.Second.ToString("00"));

                //前端界面显示
                ReceiveCount += msg.Length;
                SerialPortReceiveBit = ReceiveCount.ToString();
                SerialPortReceiveTextBlock += SerialPortTimeDateString + msg + "\n";
            }

        }

        //串口接收数据显示
        private string serialPortReceiveTextBlock;
        public string SerialPortReceiveTextBlock
        {
            get { return serialPortReceiveTextBlock; }
            set { serialPortReceiveTextBlock = value; RaisePropertyChanged(() => SerialPortReceiveTextBlock); }
        }
        //串口发送数据显示
        private string serialPortSendTextBox;
        public string SerialPortSendTextBox
        {
            get { return serialPortSendTextBox; }
            set { serialPortSendTextBox = value; RaisePropertyChanged(() => SerialPortSendTextBox); }
        }
        //接收字符数
        public int ReceiveCount;
        private string serialPortReceiveBit;
        public string SerialPortReceiveBit
        {
            get { return serialPortReceiveBit; }
            set { serialPortReceiveBit = value; RaisePropertyChanged(() => SerialPortReceiveBit); }
        }
        //发送字符数
        public int SendCount;
        private string serialPortSendBit;
        public string SerialPortSendBit
        {
            get { return serialPortSendBit; }
            set { serialPortSendBit = value; RaisePropertyChanged(() => SerialPortSendBit); }
        }
        //开启关闭接收功能按钮文本
        private string serialPortButtonText;
        public string SerialPortButtonText
        {
            get { return serialPortButtonText; }
            set { serialPortButtonText = value; RaisePropertyChanged(() => SerialPortButtonText); }
        }

        //按钮区
        //串口接收关闭开启按钮
        private RelayCommand serialPortReceiveOpenButton;
        public RelayCommand SerialPortReceiveOpenButton
        {
            get
            {
                if (serialPortReceiveOpenButton == null) return new RelayCommand(() => SerialPortOpenClicking());
                return serialPortReceiveOpenButton;
            }
            set { serialPortReceiveOpenButton = value; }
        }
        private void SerialPortOpenClicking()
        {
            if (SerialPortButtonText.Equals("开启接收"))
            {
                SerialPortButtonText = "关闭接收";
            }
            else
            {
                SerialPortButtonText = "开启接收";
            }
        }

        //串口清空接收按钮
        private RelayCommand serialPortEmptyReceiveButton;
        public RelayCommand SerialPortEmptyReceiveButton
        {
            get
            {
                if (serialPortEmptyReceiveButton == null) return new RelayCommand(() => SerialPortEmptyReceiveClicking());
                return serialPortEmptyReceiveButton;
            }
            set { serialPortEmptyReceiveButton = value; }
        }
        private void SerialPortEmptyReceiveClicking()
        {
            SerialPortReceiveTextBlock = "";
        }

        //串口清空发送按钮
        private RelayCommand serialPortEmptySendButton;
        public RelayCommand SerialPortEmptySendButton
        {
            get
            {
                if (serialPortEmptySendButton == null) return new RelayCommand(() => SerialPortEmptySendClicking());
                return serialPortEmptySendButton;
            }
            set { serialPortEmptySendButton = value; }
        }
        private void SerialPortEmptySendClicking()
        {
            SerialPortSendTextBox = "";
        }

        //串口清空计数按钮
        private RelayCommand serialPortEmptyCountButton;
        public RelayCommand SerialPortEmptyCountButton
        {
            get
            {
                if (serialPortEmptyCountButton == null) return new RelayCommand(() => SerialPortEmptyCountClicking());
                return serialPortEmptyCountButton;
            }
            set { serialPortEmptyCountButton = value; }
        }
        private void SerialPortEmptyCountClicking()
        {
            SerialPortReceiveBit = "0";
            ReceiveCount = 0;
            SerialPortSendBit = "0";
            SendCount = 0;
        }

        //串口发送按钮
        private RelayCommand serialPortSendButton;
        public RelayCommand SerialPortSendButton
        {
            get
            {
                if (serialPortSendButton == null) return new RelayCommand(() => SerialPortSendClicking(), CanSend);
                return serialPortSendButton;
            }
            set { serialPortSendButton = value; }
        }
        private void SerialPortSendClicking()
        {
            Messenger.Default.Send<string>(SerialPortSendTextBox, "SerialPortSendMessage"); //注意：token参数一致
            SendCount += SerialPortSendTextBox.Length;
            SerialPortSendBit = SendCount.ToString();
        }
        private bool CanSend()
        {
            return !string.IsNullOrEmpty(SerialPortSendTextBox);
        }

    }
}
