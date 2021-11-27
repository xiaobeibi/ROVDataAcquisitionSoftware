using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 炫酷紫界面_一_.Model
{
    public class SerialPortNumberModel : ObservableObject
    {
        public SerialPortNumberModel(string key, string text)
        {
            SerialPortNumberKey = key;
            SerialPortNumberText = text;
        }

        private string serialPortNumberKey;
        /// <summary>
        /// Key值
        /// </summary>
        public string SerialPortNumberKey
        {
            get { return serialPortNumberKey; }
            set { serialPortNumberKey = value; RaisePropertyChanged(() => SerialPortNumberKey); }
        }

        private string serialPortNumberText;
        /// <summary>
        /// Text值
        /// </summary>
        public string SerialPortNumberText
        {
            get { return serialPortNumberText; }
            set { serialPortNumberText = value; RaisePropertyChanged(() => SerialPortNumberText); }
        }
    }
}
