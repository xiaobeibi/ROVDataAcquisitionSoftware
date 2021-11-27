using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 炫酷紫界面_一_.Model
{
    public class SerialPortDataBitModel : ObservableObject
    {
        public SerialPortDataBitModel(string key, string text)
        {
            SerialPortDataBitKey = key;
            SerialPortDataBitText = text;
        }

        private string serialPortDataBitKey;
        /// <summary>
        /// Key值
        /// </summary>
        public string SerialPortDataBitKey
        {
            get { return serialPortDataBitKey; }
            set { serialPortDataBitKey = value; RaisePropertyChanged(() => SerialPortDataBitKey); }
        }

        private string serialPortDataBitText;
        /// <summary>
        /// Text值
        /// </summary>
        public string SerialPortDataBitText
        {
            get { return serialPortDataBitText; }
            set { serialPortDataBitText = value; RaisePropertyChanged(() => SerialPortDataBitText); }
        }
    }
}
