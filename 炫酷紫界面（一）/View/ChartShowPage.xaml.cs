using GalaSoft.MvvmLight.Messaging;
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
using 炫酷紫界面_一_.Model;
using 炫酷紫界面_一_.ViewModel;

namespace 炫酷紫界面_一_.View
{
    /// <summary>
    /// ChartShowPage.xaml 的交互逻辑
    /// </summary>
    public partial class ChartShowPage : Page
    {
        public ChartShowPage()
        {
            InitializeComponent();

            //消息标志token：ViewAlert，用于标识只阅读某个或者某些Sender发送的消息，并执行相应的处理，所以Sender那边的token要保持一致
            //执行方法Action：ShowReceiveInfo，用来执行接收到消息后的后续工作，注意这边是支持泛型能力的，所以传递参数很方便。
            Messenger.Default.Register<string>(this, "TransferMessage", ShowReceiveInfo);
            this.DataContext = new TransferViewModel();
            //卸载当前(this)对象注册的所有MVVMLight消息
            this.Unloaded += (sender, e) => Messenger.Default.Unregister(this);
        }

        private JY9DVLModel SensorData;
        private string DepthGauge;

        private void ShowReceiveInfo(string msg)
        {
            string[] CurrentData = msg.Split(',');

            if (CurrentData.Length == 15 && CurrentData[0].Equals(":JY9") && CurrentData[CurrentData.Length - 1].Equals("$$\r"))
            {
                SensorData.AngleX = CurrentData[7];
                SensorData.AngleY = CurrentData[8];
                SensorData.AngleZ = CurrentData[9];
                SensorData.AngleVelX = CurrentData[4];
                SensorData.AngleVelY = CurrentData[5];
                SensorData.AngleVelZ = CurrentData[6];
                SensorData.AngleAccelX = CurrentData[1];
                SensorData.AngleAccelY = CurrentData[2];
                SensorData.AngleAccelZ = CurrentData[3];
            }
            if (CurrentData.Length == 9 && CurrentData[0].Equals(":DVL") && CurrentData[CurrentData.Length - 1].Equals("$$\r"))
            {
                SensorData.ForwardSpeed = CurrentData[2];
                SensorData.LateralSpeed = CurrentData[3];
                SensorData.VerticalSpeed = CurrentData[4];
                SensorData.HeightBottom = CurrentData[6];
            }
            if (CurrentData.Length == 3 && CurrentData[0].Equals(":DEP"))
            {
                DepthGauge = CurrentData[1];
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //初始化传感器数据
            SensorData = new JY9DVLModel();
            //深度计初始化
            DepthGauge = "0";


            Task.Factory.StartNew(RecordData);
        }

        private int index = 0;
        private void RecordData()
        {
            // 持续生成随机数，模拟数据处理过程
            while (true)
            {
                Thread.Sleep(1000);

                // 更新图表数据
                angleChart.Index = index;
                angleChart.AngleXValue = double.Parse(SensorData.AngleX);
                angleChart.AngleYValue = double.Parse(SensorData.AngleY);
                angleChart.AngleZValue = double.Parse(SensorData.AngleZ);

                aSpeedChart.Index = index;
                aSpeedChart.AngleSpeedXValue = double.Parse(SensorData.AngleVelX);
                aSpeedChart.AngleSpeedYValue = double.Parse(SensorData.AngleVelY);
                aSpeedChart.AngleSpeedZValue = double.Parse(SensorData.AngleVelZ);

                accelChart.Index = index;
                accelChart.AngleAccelXValue = double.Parse(SensorData.AngleAccelX);
                accelChart.AngleAccelYValue = double.Parse(SensorData.AngleAccelY);
                accelChart.AngleAccelZValue = double.Parse(SensorData.AngleAccelZ);

                dvlChart.Index = index;
                dvlChart.DVLForwardValue = double.Parse(SensorData.ForwardSpeed);
                dvlChart.DVlLateralValue = double.Parse(SensorData.LateralSpeed);
                dvlChart.DVLVerticalValue = double.Parse(SensorData.VerticalSpeed);

                dvlHeightChart.Index = index;
                dvlHeightChart.DVlHeigthValue = double.Parse(SensorData.HeightBottom);
                dvlHeightChart.DepthGaugeValue = double.Parse(DepthGauge) / 100;

                index++;
            }
        }
    }
}
