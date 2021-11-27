using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using 炫酷紫界面_一_.Model;

namespace 炫酷紫界面_一_.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            MenuList = new ObservableCollection<MenuInfoModel>()
            {
                new MenuInfoModel(){
                 Title="基础显示", 
                 MenuInfoBtnList=new ObservableCollection<MenuInfoBtnModel>(){ 
                 new MenuInfoBtnModel(){ Ico="\xf2bc", Name="数据首页", ErrCount=0, PageUri="View/HomePage.xaml"},
                 new MenuInfoBtnModel(){ Ico="\xf188", Name="调试界面", ErrCount=0, PageUri="View/DebugPage.xaml"},
                 }
                },

                new MenuInfoModel(){
                 Title="功能控制",
                 MenuInfoBtnList=new ObservableCollection<MenuInfoBtnModel>(){
                 new MenuInfoBtnModel(){ Ico="\xf085", Name="控制界面", ErrCount=1, PageUri="View/ControlPage.xaml"},
                 new MenuInfoBtnModel(){ Ico="\xf20b", Name="PID模式控制", ErrCount=2, PageUri="View/PIDControlPage.xaml"},
                 new MenuInfoBtnModel(){ Ico="\xf0cb", Name="数据表展示", ErrCount=3, PageUri="View/DataShowPage.xaml"},
                 new MenuInfoBtnModel(){ Ico="\xf21e", Name="实时曲线图", ErrCount=4, PageUri="View/ChartShowPage.xaml"},
                 new MenuInfoBtnModel(){ Ico="\xf124", Name="实时定位图", ErrCount=5, PageUri="View/RealTimeMapPage.xaml"},
                 new MenuInfoBtnModel(){ Ico="\xf152", Name="仿真展示", ErrCount=6, PageUri="View/RotationPage.xaml"},
                 }
                }
            };
            MenuInfoBtnModel = MenuList[0].MenuInfoBtnList[0];
        }

        public RelayCommand<MenuInfoBtnModel> ItmeBtnCommand => new RelayCommand<MenuInfoBtnModel>((menuInfoBtnModel) => {
            MenuInfoBtnModel = menuInfoBtnModel;
            menuInfoBtnModel.ErrCount = 0;
        });

        public RelayCommand<Frame> NavigatedCommand => new RelayCommand<Frame>((frame) => {
            frame.NavigationService.RemoveBackEntry();
        });

        public RelayCommand<Window> MinWindowCommand => new RelayCommand<Window>((window) => {
            window.WindowState = WindowState.Minimized;
        });

        public RelayCommand CloseWindowCommand => new RelayCommand(() => {
            Application.Current.Shutdown();
        });

        public RelayCommand<Window> ChangeWindowCommand => new RelayCommand<Window>((window) => {
            if (window.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Normal;
            }
            else {
                window.WindowState = WindowState.Maximized;
            }
        });

        public ObservableCollection<MenuInfoModel> MenuList { get; set; }
        private MenuInfoBtnModel _MenuInfoBtnModel;

        public MenuInfoBtnModel MenuInfoBtnModel
        {
            get { return _MenuInfoBtnModel; }
            set { _MenuInfoBtnModel = value; RaisePropertyChanged(); }
        }
    }
}