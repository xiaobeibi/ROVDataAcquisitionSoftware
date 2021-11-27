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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace 炫酷紫界面_一_.UserControls
{
    /// <summary>
    /// BubbleJumpControl.xaml 的交互逻辑
    /// </summary>
    public partial class BubbleJumpControl : Window
    {
        public BubbleJumpControl()
        {
            InitializeComponent();
        }

        public string NotifyMessage { get; set; }

        async private void Storyboard_Completed(object sender, EventArgs e)
        {
            await Task.Delay(2000);
            BeginStoryboard(FindResource("CloseStoryboard") as Storyboard);
        }

        private void Storyboard_Completed_1(object sender, EventArgs e)
        {
            Close();
        }

        private void ThisWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Text = NotifyMessage;
        }
    }
}
