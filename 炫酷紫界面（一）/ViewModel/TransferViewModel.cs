using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 炫酷紫界面_一_.ViewModel
{
    public class TransferViewModel : ViewModelBase
    {
        public TransferViewModel()
        {
            //注册串口发过来的消息
            Messenger.Default.Register<string>(this, "SerialPortReceiveMessage", SerialPortMessageTransfer);
        }

        //从串口那接收来的消息
        private void SerialPortMessageTransfer(string msg)
        {
            Messenger.Default.Send<string>(msg, "TransferMessage"); //注意：token参数一致
        }
    }
}
