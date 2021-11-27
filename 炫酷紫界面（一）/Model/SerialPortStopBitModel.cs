using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 炫酷紫界面_一_.Model
{
    public class SerialPortStopBitModel : ObservableObject
    {
        public SerialPortStopBitModel(string key, string text)
        {
            SerialPortStopBitKey = key;
            SerialPortStopBitText = text;
        }

        private string serialPortStopBitKey;
        /// <summary>
        /// Key值
        /// </summary>
        public string SerialPortStopBitKey
        {
            get { return serialPortStopBitKey; }
            set { serialPortStopBitKey = value; RaisePropertyChanged(() => SerialPortStopBitKey); }
        }

        private string serialPortStopBitText;
        /// <summary>
        /// Text值
        /// </summary>
        public string SerialPortStopBitText
        {
            get { return serialPortStopBitText; }
            set { serialPortStopBitText = value; RaisePropertyChanged(() => SerialPortStopBitText); }
        }
    }
}
