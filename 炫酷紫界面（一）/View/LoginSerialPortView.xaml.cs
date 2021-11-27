using GalaSoft.MvvmLight.Messaging;
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
using 炫酷紫界面_一_.UserControls;

namespace 炫酷紫界面_一_.View
{
    /// <summary>
    /// LoginSerialPortView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginSerialPortView : Window
    {
        private static LoginSerialPortView SingleSerailPort;
        private static readonly object lockerPort = new object();

        public LoginSerialPortView()
        {
            InitializeComponent();

            //注册当前MVVM消息
            Messenger.Default.Register<string>(this, "SerialPortErrorMessage", SerialPortShowErrorInfo);

            //卸载当前(this)对象注册的所有MVVMLight消息
            this.Unloaded += (sender, e) => Messenger.Default.Unregister(this);
        }

        private void SerialPortShowErrorInfo(string msg)
        {
            BubbleJumpControl bubbleJumpControl = new BubbleJumpControl()
            {
                NotifyMessage = msg
            };
            bubbleJumpControl.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            bubbleJumpControl.Owner = this;
            bubbleJumpControl.Show();
        }

        public static LoginSerialPortView GetInstance()
        {
            if (SingleSerailPort == null)
            {
                lock (lockerPort)
                {
                    SingleSerailPort = new LoginSerialPortView();
                }
            }
            return SingleSerailPort;
        }

        public void LoginSerialPortShow()
        {
            Storyboard storyboard = this.FindResource("OnLoaded") as Storyboard;
            BeginStoryboard beginStoryboard = new BeginStoryboard();
            beginStoryboard.Storyboard = storyboard;
            storyboard.Begin();

            SingleSerailPort.Show();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        private void Storyboard_Completed(object sender, EventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Storyboard storyboard = this.FindResource("OnUnloaded") as Storyboard;
            BeginStoryboard beginStoryboard = new BeginStoryboard();
            beginStoryboard.Storyboard = storyboard;
            storyboard.Begin();
        }


    }
}
