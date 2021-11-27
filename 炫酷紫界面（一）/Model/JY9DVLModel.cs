using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 炫酷紫界面_一_.Model
{
    public class JY9DVLModel : ObservableObject
    {
        public JY9DVLModel()
        {
            AngleX = "0";
            AngleY = "0";
            AngleZ = "0";
            AngleVelX = "0.000";
            AngleVelY = "0.000";
            AngleVelZ = "0.000";
            AngleAccelX = "0.000";
            AngleAccelY = "0.000";
            AngleAccelZ = "0.000";
            ForwardSpeed = "0.000";
            LateralSpeed = "0.000";
            VerticalSpeed = "0.000";
            HeightBottom = "0.00";
        }

        //角度
        private string angleX;
        public string AngleX
        {
            get { return angleX; }
            set { angleX = value; RaisePropertyChanged(() => AngleX); }
        }

        private string angleY;
        public string AngleY
        {
            get { return angleY; }
            set { angleY = value; RaisePropertyChanged(() => AngleY); }
        }

        private string angleZ;
        public string AngleZ
        {
            get { return angleZ; }
            set { angleZ = value; RaisePropertyChanged(() => AngleZ); }
        }

        //角速度
        private string angleVelX;
        public string AngleVelX
        {
            get { return angleVelX; }
            set { angleVelX = value; RaisePropertyChanged(() => AngleVelX); }
        }

        private string angleVelY;
        public string AngleVelY
        {
            get { return angleVelY; }
            set { angleVelY = value; RaisePropertyChanged(() => AngleVelY); }
        }

        private string angleVelZ;
        public string AngleVelZ
        {
            get { return angleVelZ; }
            set { angleVelZ = value; RaisePropertyChanged(() => AngleVelZ); }
        }

        //角加速度
        private string angleAccelX;
        public string AngleAccelX
        {
            get { return angleAccelX; }
            set { angleAccelX = value; RaisePropertyChanged(() => AngleAccelX); }
        }

        private string angleAccelY;
        public string AngleAccelY
        {
            get { return angleAccelY; }
            set { angleAccelY = value; RaisePropertyChanged(() => AngleAccelY); }
        }

        private string angleAccelZ;
        public string AngleAccelZ
        {
            get { return angleAccelZ; }
            set { angleAccelZ = value; RaisePropertyChanged(() => AngleAccelZ); }
        }

        //DVL数据
        private string forwardSpeed;
        public string ForwardSpeed
        {
            get { return forwardSpeed; }
            set { forwardSpeed = value; RaisePropertyChanged(() => ForwardSpeed); }
        }

        private string lateralSpeed;
        public string LateralSpeed
        {
            get { return lateralSpeed; }
            set { lateralSpeed = value; RaisePropertyChanged(() => LateralSpeed); }
        }

        private string verticalSpeed;
        public string VerticalSpeed
        {
            get { return verticalSpeed; }
            set { verticalSpeed = value; RaisePropertyChanged(() => VerticalSpeed); }
        }

        private string heightBottom;
        public string HeightBottom
        {
            get { return heightBottom; }
            set { heightBottom = value; RaisePropertyChanged(() => HeightBottom); }
        }
    }
}
