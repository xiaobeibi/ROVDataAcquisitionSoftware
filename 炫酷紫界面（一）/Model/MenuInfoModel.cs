using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 炫酷紫界面_一_.Model
{
    public class MenuInfoModel: ObservableObject
    {
        private string _Title;

        public string Title
        {
            get { return _Title; }
            set { _Title = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<MenuInfoBtnModel> _MenuInfoBtnList;

        public ObservableCollection<MenuInfoBtnModel> MenuInfoBtnList
        {
            get { return _MenuInfoBtnList; }
            set { _MenuInfoBtnList = value; RaisePropertyChanged(); }
        }

    }
}
