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
    /// AngleChartControl.xaml 的交互逻辑
    /// </summary>
    public partial class AngleChartControl : UserControl
    {
        public AngleChartControl()
        {
            InitializeComponent();

            // 设置图表的XY和数值对应
            CartesianMapper<MeasureModel> mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.Index)
                .Y(model => model.Value);

            Charting.For<MeasureModel>(mapper);

            AngleXSeriesValues = new ChartValues<MeasureModel>();
            AngleYSeriesValues = new ChartValues<MeasureModel>();
            AngleZSeriesValues = new ChartValues<MeasureModel>();
            DataContext = this;
        }

        public ChartValues<MeasureModel> AngleXSeriesValues { get; set; }
        public ChartValues<MeasureModel> AngleYSeriesValues { get; set; }
        public ChartValues<MeasureModel> AngleZSeriesValues { get; set; }

        public double AngleXValue { get; set; }
        public double AngleYValue { get; set; }
        public double AngleZValue { get; set; }

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
            AngleXSeriesValues.Add(new MeasureModel
            {
                Index = this.Index,
                Value = this.AngleXValue
            });
            AngleYSeriesValues.Add(new MeasureModel
            {
                Index = this.Index,
                Value = this.AngleYValue
            });
            AngleZSeriesValues.Add(new MeasureModel
            {
                Index = this.Index,
                Value = this.AngleZValue
            });
            // 限定图表最多只有十五个元素
            if (AngleXSeriesValues.Count > 15)
            {
                AngleXSeriesValues.RemoveAt(0);
                AngleYSeriesValues.RemoveAt(0);
                AngleZSeriesValues.RemoveAt(0);
            }
        }
    }
}
