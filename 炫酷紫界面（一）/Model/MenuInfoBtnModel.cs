using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 炫酷紫界面_一_.Model
{
    public class MenuInfoBtnModel: ObservableObject
    {
        private string _Ico;

        public string Ico
        {
            get { return _Ico; }
            set { _Ico = value; RaisePropertyChanged(); }
        }
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; RaisePropertyChanged(); }
        }
        private int _ErrCount;

        public int ErrCount
        {
            get { return _ErrCount; }
            set { _ErrCount = value; RaisePropertyChanged(); }
        }
        private string _PageUri;

        public string PageUri
        {
            get { return _PageUri; }
            set { _PageUri = value; RaisePropertyChanged(); }
        }
    }
}
