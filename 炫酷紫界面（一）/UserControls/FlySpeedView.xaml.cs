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

namespace 炫酷紫界面_一_.UserControls
{
    /// <summary>
    /// FlySpeedView.xaml 的交互逻辑
    /// </summary>
    public partial class FlySpeedView : UserControl
    {
        public FlySpeedView()
        {
            InitializeComponent();
        }

        #region 依赖属性
        public static DependencyProperty HundredProperty;
        public static DependencyProperty TenProperty;
        public static DependencyProperty OneProperty;
        public static DependencyProperty OneTapeTranfYProperty;
        public static DependencyProperty TwoProperty;
        public static DependencyProperty TwoTapeTranfYProperty;
        public static DependencyProperty ThreeProperty;
        public static DependencyProperty ThreeTapeTranfYProperty;
        public static DependencyProperty ReadoutProperty;
        #endregion

        #region 属性封装器
        public string Hundred
        {
            get { return (string)GetValue(HundredProperty); }
            set { SetValue(HundredProperty, value); }
        }
        public string Ten
        {
            get { return (string)GetValue(TenProperty); }
            set { SetValue(TenProperty, value); }
        }
        public string One
        {
            get { return (string)GetValue(OneProperty); }
            set { SetValue(OneProperty, value); }
        }
        public string OneTapeTranfY
        {
            get { return (string)GetValue(OneTapeTranfYProperty); }
            set { SetValue(OneTapeTranfYProperty, value); }
        }
        public string Two
        {
            get { return (string)GetValue(TwoProperty); }
            set { SetValue(TwoProperty, value); }
        }
        public string TwoTapeTranfY
        {
            get { return (string)GetValue(TwoTapeTranfYProperty); }
            set { SetValue(TwoTapeTranfYProperty, value); }
        }
        public string Three
        {
            get { return (string)GetValue(ThreeProperty); }
            set { SetValue(ThreeProperty, value); }
        }
        public string ThreeTapeTranfY
        {
            get { return (string)GetValue(ThreeTapeTranfYProperty); }
            set { SetValue(ThreeTapeTranfYProperty, value); }
        }
        public double Readout
        {
            get { return (double)GetValue(ReadoutProperty); }
            set { SetValue(ReadoutProperty, value); }
        }
        #endregion

        static FlySpeedView()
        {
            HundredProperty = DependencyProperty.Register(
                "Hundred", typeof(string), typeof(FlySpeedView),
                new FrameworkPropertyMetadata("0"));

            TenProperty = DependencyProperty.Register(
                "Ten", typeof(string), typeof(FlySpeedView),
                new FrameworkPropertyMetadata("2"));

            OneProperty = DependencyProperty.Register(
                "One", typeof(string), typeof(FlySpeedView),
                new FrameworkPropertyMetadata("0"));

            OneTapeTranfYProperty = DependencyProperty.Register(
                "OneTapeTranfY", typeof(string), typeof(FlySpeedView),
                new FrameworkPropertyMetadata("0"));

            TwoProperty = DependencyProperty.Register(
                "Two", typeof(string), typeof(FlySpeedView),
                new FrameworkPropertyMetadata("0"));

            TwoTapeTranfYProperty = DependencyProperty.Register(
                "TwoTapeTranfY", typeof(string), typeof(FlySpeedView),
                new FrameworkPropertyMetadata("0"));

            ThreeProperty = DependencyProperty.Register(
                "Three", typeof(string), typeof(FlySpeedView),
                new FrameworkPropertyMetadata("0"));

            ThreeTapeTranfYProperty = DependencyProperty.Register(
                "ThreeTapeTranfY", typeof(string), typeof(FlySpeedView),
                new FrameworkPropertyMetadata("0"));

            ReadoutProperty = DependencyProperty.Register(
                "Readout", typeof(double), typeof(FlySpeedView),
                new FrameworkPropertyMetadata(66.666, new PropertyChangedCallback(OnReadoutChanged)));
        }

        private static void OnReadoutChanged(DependencyObject send, DependencyPropertyChangedEventArgs e)
        {
            double input = double.Parse(e.NewValue.ToString());
            string readout = input.ToString("00.000");

            FlySpeedView flySpeedView = (FlySpeedView)send;
            flySpeedView.Hundred = input > 100 ? "-" : input > 10 ? readout[0].ToString() : "0";
            flySpeedView.Ten = input > 100 ? "-" : input > 0 ? readout[1].ToString() : "0";
            flySpeedView.One = readout[3].ToString();
            flySpeedView.OneTapeTranfY = (-(8 - double.Parse(flySpeedView.One)) * 48).ToString();
            flySpeedView.Two = readout[4].ToString();
            flySpeedView.TwoTapeTranfY = (-(8 - double.Parse(flySpeedView.Two)) * 48).ToString();
            flySpeedView.Three = readout[5].ToString();
            flySpeedView.ThreeTapeTranfY = (-(8 - double.Parse(flySpeedView.Three)) * 48).ToString();

        }


    }
}
