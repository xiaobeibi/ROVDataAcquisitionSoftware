using LiveCharts;
using LiveCharts.Configurations;
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
using 炫酷紫界面_一_.Model;

namespace 炫酷紫界面_一_.UserControls
{
    /// <summary>
    /// ASpeedChartControl.xaml 的交互逻辑
    /// </summary>
    public partial class ASpeedChartControl : UserControl
    {
        public ASpeedChartControl()
        {
            InitializeComponent();

            // 设置图表的XY和数值对应
            CartesianMapper<MeasureModel> mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.Index)
                .Y(model => model.Value);

            Charting.For<MeasureModel>(mapper);

            AngleSpeedXSeriesValues = new ChartValues<MeasureModel>();
            AngleSpeedYSeriesValues = new ChartValues<MeasureModel>();
            AngleSpeedZSeriesValues = new ChartValues<MeasureModel>();
            DataContext = this;
        }

        public ChartValues<MeasureModel> AngleSpeedXSeriesValues { get; set; }
        public ChartValues<MeasureModel> AngleSpeedYSeriesValues { get; set; }
        public ChartValues<MeasureModel> AngleSpeedZSeriesValues { get; set; }

        public double AngleSpeedXValue { get; set; }
        public double AngleSpeedYValue { get; set; }
        public double AngleSpeedZValue { get; set; }

        private int _index;
        // 当数值被更改时，触发更新
        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                Read();
            }
        }

        // 更新图表
        private void Read()
        {
            AngleSpeedXSeriesValues.Add(new MeasureModel
            {
                Index = this.Index,
                Value = this.AngleSpeedXValue
            });
            AngleSpeedYSeriesValues.Add(new MeasureModel
            {
                Index = this.Index,
                Value = this.AngleSpeedYValue
            });
            AngleSpeedZSeriesValues.Add(new MeasureModel
            {
                Index = this.Index,
                Value = this.AngleSpeedZValue
            });
            // 限定图表最多只有十五个元素
            if (AngleSpeedXSeriesValues.Count > 15)
            {
                AngleSpeedXSeriesValues.RemoveAt(0);
                AngleSpeedYSeriesValues.RemoveAt(0);
                AngleSpeedZSeriesValues.RemoveAt(0);
            }
        }
    }
}
