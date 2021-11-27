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
    /// AccelChartControl.xaml 的交互逻辑
    /// </summary>
    public partial class AccelChartControl : UserControl
    {
        public AccelChartControl()
        {
            InitializeComponent();

            // 设置图表的XY和数值对应
            CartesianMapper<MeasureModel> mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.Index)
                .Y(model => model.Value);

            Charting.For<MeasureModel>(mapper);

            AngleAccelXSeriesValues = new ChartValues<MeasureModel>();
            AngleAccelYSeriesValues = new ChartValues<MeasureModel>();
            AngleAccelZSeriesValues = new ChartValues<MeasureModel>();
            DataContext = this;
        }

        public ChartValues<MeasureModel> AngleAccelXSeriesValues { get; set; }
        public ChartValues<MeasureModel> AngleAccelYSeriesValues { get; set; }
        public ChartValues<MeasureModel> AngleAccelZSeriesValues { get; set; }

        public double AngleAccelXValue { get; set; }
        public double AngleAccelYValue { get; set; }
        public double AngleAccelZValue { get; set; }

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
            AngleAccelXSeriesValues.Add(new MeasureModel
            {
                Index = this.Index,
                Value = this.AngleAccelXValue
            });
            AngleAccelYSeriesValues.Add(new MeasureModel
            {
                Index = this.Index,
                Value = this.AngleAccelYValue
            });
            AngleAccelZSeriesValues.Add(new MeasureModel
            {
                Index = this.Index,
                Value = this.AngleAccelZValue
            });
            // 限定图表最多只有十五个元素
            if (AngleAccelXSeriesValues.Count > 15)
            {
                AngleAccelXSeriesValues.RemoveAt(0);
                AngleAccelYSeriesValues.RemoveAt(0);
                AngleAccelZSeriesValues.RemoveAt(0);
            }
        }
    }
}
