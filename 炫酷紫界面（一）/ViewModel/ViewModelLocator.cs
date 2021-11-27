using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace 炫酷紫界面_一_.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<LoginSerialPortViewModel>();

            SimpleIoc.Default.Register<DebugPageViewModel>();

            SimpleIoc.Default.Register<HomeViewModel>();

            SimpleIoc.Default.Register<PWMViewModel>();

            SimpleIoc.Default.Register<PIDViewModel>();

            SimpleIoc.Default.Register<SaveExcelViewModel>();

            SimpleIoc.Default.Register<TransferViewModel>();
        }

        public LoginSerialPortViewModel LoginSerialPortViewModel => ServiceLocator.Current.GetInstance<LoginSerialPortViewModel>();

        public DebugPageViewModel DebugPageViewModel => ServiceLocator.Current.GetInstance<DebugPageViewModel>();

        public HomeViewModel HomeViewModel => ServiceLocator.Current.GetInstance<HomeViewModel>();

        public PWMViewModel PWMViewModel => ServiceLocator.Current.GetInstance<PWMViewModel>();

        public PIDViewModel PIDViewModel => ServiceLocator.Current.GetInstance<PIDViewModel>();

        public SaveExcelViewModel SaveExcelViewModel => ServiceLocator.Current.GetInstance<SaveExcelViewModel>();

        public TransferViewModel TransferViewModel => ServiceLocator.Current.GetInstance<TransferViewModel>();


        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
