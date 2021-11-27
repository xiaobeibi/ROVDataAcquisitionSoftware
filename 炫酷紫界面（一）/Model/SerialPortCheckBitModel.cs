using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 炫酷紫界面_一_.Model
{
    public class SerialPortCheckBitModel : ObservableObject
    {
        public SerialPortCheckBitModel(string key, string text)
        {
            SerialPortCheckBitKey = key;
            SerialPortCheckBitText = text;
        }

        private string serialPortCheckBitKey;
        /// <summary>
        /// Key值
        /// </summary>
        public string SerialPortCheckBitKey
        {
            get { return serialPortCheckBitKey; }
            set { serialPortCheckBitKey = value; RaisePropertyChanged(() => SerialPortCheckBitKey); }
        }

        private string serialPortCheckBitText;
        /// <summary>
        /// Text值
        /// </summary>
        public string SerialPortCheckBitText
        {
            get { return serialPortCheckBitText; }
            set { serialPortCheckBitText = value; RaisePropertyChanged(() => SerialPortCheckBitText); }
        }
    }
}
