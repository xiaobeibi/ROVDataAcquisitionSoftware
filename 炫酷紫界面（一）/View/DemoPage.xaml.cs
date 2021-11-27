using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// DemoPage.xaml 的交互逻辑
    /// </summary>
    public partial class DemoPage : Page
    {
        public DemoPage()
        {
            ListData = new List<string>();
            InitializeComponent();
            for (int i = 0; i < 20; i++)
            {
                ListData.Add("");
            }
            DataContext = this;
        }
        public List<string> ListData { get; set; }
    }
}
