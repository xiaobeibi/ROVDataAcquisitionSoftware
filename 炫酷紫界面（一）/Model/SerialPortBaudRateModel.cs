using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 炫酷紫界面_一_.Model
{
    public class SerialPortBaudRateModel : ObservableObject
    {
        public SerialPortBaudRateModel(string key, string text)
        {
            SerialPortBaudRateKey = key;
            SerialPortBaudRateText = text;
        }

        private string serialPortBaudRateKey;
        /// <summary>
        /// Key值
        /// </summary>
        public string SerialPortBaudRateKey
        {
            get { return serialPortBaudRateKey; }
            set { serialPortBaudRateKey = value; RaisePropertyChanged(() => SerialPortBaudRateKey); }
        }

        private string serialPortBaudRateText;
        /// <summary>
        /// Text值
        /// </summary>
        public string SerialPortBaudRateText
        {
            get { return serialPortBaudRateText; }
            set { serialPortBaudRateText = value; RaisePropertyChanged(() => SerialPortBaudRateText); }
        }
    }
}
