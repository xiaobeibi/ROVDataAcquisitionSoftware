using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace 炫酷紫界面_一_.Toy
{
    //性能要求不高，那就不用DrawingVisual或者DX了
    //目前这个写法嘞性能就很烂了哈哈哈；只为效果而已
    public class WaveCanvas : Canvas
    {
        public override void EndInit()
        {
            base.EndInit();

            LinearGradientBrush lgb = new LinearGradientBrush();
            GradientStop gs1 = new GradientStop(Color.FromRgb(0xD5, 0x05, 0x92), 1);
            GradientStop gs2 = new GradientStop(Color.FromRgb(0xFD, 0x14, 0x16), 0);
            lgb.GradientStops.Add(gs1);
            lgb.GradientStops.Add(gs2);
            lgb.Freeze();

            double leftStep = this.Width / Totals;

            for (int i = 0; i < Totals; i++)
            {
                Rectangle rect = new Rectangle()
                {
                    RadiusX = Size / 2,
                    RadiusY = Size / 2,
                    Width = Size,
                    Height = Size,
                    Fill = lgb
                };
                Canvas.SetLeft(rect, leftStep * i);
                Canvas.SetBottom(rect, 0);
                this.Children.Add(rect);
            }
        }

        public int Totals { get; set; } = 50;
        public double Size { get; set; } = 5;

        public Dictionary<int, double> IndexedValue
        {
            get { return (Dictionary<int, double>)GetValue(IndexedValueProperty); }
            set { SetValue(IndexedValueProperty, value); }
        }
        // Using a DependencyProperty as the backing store for YValues.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IndexedValueProperty =
            DependencyProperty.Register("IndexedValue", typeof(Dictionary<int, double>), typeof(WaveCanvas), new PropertyMetadata(null, OnYValuesChanged));

        private static void OnYValuesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var canvas = d as WaveCanvas;
            if (canvas != null) canvas.UpdateWave(e.NewValue as Dictionary<int, double>);
        }

        void UpdateWave(Dictionary<int, double> yVals)
        {
            foreach (var item in yVals)
            {
                if (item.Key < Totals)
                {
                    Rectangle rect = this.Children[item.Key] as Rectangle;
                    double oldHeighht = rect.Height;
                    double newHeight = (item.Value + 1) * Size;


                    Storyboard keyFrameboard = new Storyboard();

                    DoubleAnimationUsingKeyFrames dakeyframe = new DoubleAnimationUsingKeyFrames();
                    dakeyframe.BeginTime = new TimeSpan(0, 0, 0);
                    Storyboard.SetTarget(dakeyframe, rect);
                    Storyboard.SetTargetProperty(dakeyframe, new PropertyPath("(FrameworkElement.Height)"));

                    EasingDoubleKeyFrame edKeyFrame1 = new EasingDoubleKeyFrame();
                    edKeyFrame1.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1));
                    edKeyFrame1.Value = newHeight;
                    dakeyframe.KeyFrames.Add(edKeyFrame1);

                    EasingDoubleKeyFrame edKeyFrame2 = new EasingDoubleKeyFrame();
                    edKeyFrame2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1));
                    edKeyFrame2.Value = Size;
                    dakeyframe.KeyFrames.Add(edKeyFrame2);

                    keyFrameboard.Children.Add(dakeyframe);
                    keyFrameboard.Begin();


                    //Duration duration = new Duration(TimeSpan.FromMilliseconds(Math.Abs(newHeight - oldHeighht)));
                    //DoubleAnimation doubleAnimation = new DoubleAnimation(oldHeighht, newHeight, duration);
                    //rect.BeginAnimation(Rectangle.HeightProperty, doubleAnimation);
                }
            }
        }
    }
}
