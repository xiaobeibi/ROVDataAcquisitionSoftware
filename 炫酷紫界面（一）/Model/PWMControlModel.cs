using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 炫酷紫界面_一_.Model
{
    public class PWMControlModel : ObservableObject
    {
        public PWMControlModel()
        {
            PWM1 = "1500";
            PWM2 = "1500";
            PWM3 = "1500";
            PWM4 = "1500";
            PWM5 = "1500";
        }

        private string pWM1;
        public string PWM1
        {
            get { return pWM1; }
            set { pWM1 = value; RaisePropertyChanged(() => PWM1); }
        }
        private string pWM2;
        public string PWM2
        {
            get { return pWM2; }
            set { pWM2 = value; RaisePropertyChanged(() => PWM2); }
        }
        private string pWM3;
        public string PWM3
        {
            get { return pWM3; }
            set { pWM3 = value; RaisePropertyChanged(() => PWM3); }
        }
        private string pWM4;
        public string PWM4
        {
            get { return pWM4; }
            set { pWM4 = value; RaisePropertyChanged(() => PWM4); }
        }
        private string pWM5;
        public string PWM5
        {
            get { return pWM5; }
            set { pWM5 = value; RaisePropertyChanged(() => PWM5); }
        }
    }
}
