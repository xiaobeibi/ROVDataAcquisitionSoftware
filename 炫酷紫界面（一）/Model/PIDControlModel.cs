using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 炫酷紫界面_一_.Model
{
    public class PIDControlModel : ObservableObject
    {
        public PIDControlModel()
        {
            AngleKp = "";
            AngleKi = "";
            AngleKd = "";
            SpeedKp = "";
            SpeedKi = "";
            SpeedKd = "";
            DepthKp = "";
            DepthKi = "";
            DepthKd = "";
        }

        private string angleKp;
        public string AngleKp
        {
            get { return angleKp; }
            set { angleKp = value; RaisePropertyChanged(() => AngleKp); }
        }
        private string angleKi;
        public string AngleKi
        {
            get { return angleKi; }
            set { angleKi = value; RaisePropertyChanged(() => AngleKi); }
        }
        private string angleKd;
        public string AngleKd
        {
            get { return angleKd; }
            set { angleKd = value; RaisePropertyChanged(() => AngleKd); }
        }
        private string speedKp;
        public string SpeedKp
        {
            get { return speedKp; }
            set { speedKp = value; RaisePropertyChanged(() => SpeedKp); }
        }
        private string speedKi;
        public string SpeedKi
        {
            get { return speedKi; }
            set { speedKi = value; RaisePropertyChanged(() => SpeedKi); }
        }
        private string speedKd;
        public string SpeedKd
        {
            get { return speedKd; }
            set { speedKd = value; RaisePropertyChanged(() => SpeedKd); }
        }
        private string depthKp;
        public string DepthKp
        {
            get { return depthKp; }
            set { depthKp = value; RaisePropertyChanged(() => DepthKp); }
        }
        private string depthKi;
        public string DepthKi
        {
            get { return depthKi; }
            set { depthKi = value; RaisePropertyChanged(() => DepthKi); }
        }
        private string depthKd;
        public string DepthKd
        {
            get { return depthKd; }
            set { depthKd = value; RaisePropertyChanged(() => DepthKd); }
        }
    }
}
