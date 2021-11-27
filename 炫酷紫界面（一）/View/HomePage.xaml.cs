using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 炫酷紫界面_一_.View
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        public bool isTiming = true;

        ~HomePage()
        {
            isTiming = false;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                while (isTiming)
                {
                    Thread.Sleep(1000);
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        var time = DateTime.Now;
                        HourText.Text = time.Hour.ToString();
                        MinText.Text = time.Minute.ToString();
                        Second.Angle = (time.Second + 1) / 60.0 * 360.0;
                    }));
                }
            });
        }
    }
}
