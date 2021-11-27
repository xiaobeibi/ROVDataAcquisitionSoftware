using Microsoft.Expression.Shapes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 炫酷紫界面_一_.UserControls
{
    /// <summary>
    /// TimeCompass.xaml 的交互逻辑
    /// </summary>
    public partial class TimeCompass : UserControl
    {
        //可以使用另一实现方法，继承canvas等控件
        //要计算绘制的坐标，以及可能重写Measure、Arrange、Render等
        //可以仿造系统自带的TickBar来实现
        //此处只是图方便使用PathListBox来快速实现布局（Tick等的绘制）
        public TimeCompass()
        {
            InitializeComponent();
            this.Loaded += DirectionCompass_Loaded;
            this.SizeChanged += DirectionCompass_SizeChanged;
        }

        private void DirectionCompass_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double length = Math.Min(this.ActualWidth, this.ActualHeight);
            RootHost.Width = RootHost.Height = length;
        }

        private void DirectionCompass_Loaded(object sender, RoutedEventArgs e)
        {
            SetTicks();
        }

        void SetTicks()
        {
            List<object> longs = new List<object>();
            List<object> shorts = new List<object>();
            List<object> numbers = new List<object>();

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    shorts.Add(new object());
                }
                longs.Add(new object());
                numbers.Add(i);
            }

            LongTicks = longs;
            ShortTicks = shorts;
            Numbers = numbers;
        }


        public List<object> LongTicks
        {
            get { return (List<object>)GetValue(LongTicksProperty); }
            set { SetValue(LongTicksProperty, value); }
        }
        // Using a DependencyProperty as the backing store for AngleTicks.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LongTicksProperty =
            DependencyProperty.Register("LongTicks", typeof(List<object>), typeof(TimeCompass), new PropertyMetadata(null));



        public List<object> ShortTicks
        {
            get { return (List<object>)GetValue(ShortTicksProperty); }
            set { SetValue(ShortTicksProperty, value); }
        }
        // Using a DependencyProperty as the backing store for ShortTicks.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShortTicksProperty =
            DependencyProperty.Register("ShortTicks", typeof(List<object>), typeof(TimeCompass), new PropertyMetadata(null));



        public List<object> Numbers
        {
            get { return (List<object>)GetValue(NumbersProperty); }
            set { SetValue(NumbersProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Numbers.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumbersProperty =
            DependencyProperty.Register("Numbers", typeof(List<object>), typeof(TimeCompass), new PropertyMetadata(null));


        //可改为Time.Second来计算Angle
        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Angle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(TimeCompass), new PropertyMetadata(0.0, OnAngleChanged));

        private static void OnAngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TimeCompass).UpdateAngle((double)e.OldValue, (double)e.NewValue);
        }


        void UpdateAngle(double OldAngle, double NewAngle)
        {
            var delta = Math.Abs(NewAngle - OldAngle);

            //Duration duration = new Duration(TimeSpan.FromMilliseconds(delta * 1000 / 6));
            Duration duration = new Duration(TimeSpan.FromMilliseconds(1000));
            DoubleAnimation doubleAnimation = new DoubleAnimation(OldAngle, NewAngle, duration);
            TimeArc.BeginAnimation(Arc.EndAngleProperty, doubleAnimation);
        }

    }
}
