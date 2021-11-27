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
    /// DVLChartControl.xaml 的交互逻辑
    /// </summary>
    public partial class DVLChartControl : UserControl
    {
        public DVLChartControl()
        {
            InitializeComponent();

            // 设置图表的XY和数值对应
            CartesianMapper<MeasureModel> mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.Index)
                .Y(model => model.Value);

            Charting.For<MeasureModel>(mapper);

            DVLForwardSeriesValues = new ChartValues<MeasureModel>();
            DVlLateralSeriesValues = new ChartValues<MeasureModel>();
            DVLVerticalSeriesValues = new ChartValues<MeasureModel>();
            DataContext = this;
        }

        public ChartValues<MeasureModel> DVLForwardSeriesValues { get; set; }
        public ChartValues<MeasureModel> DVlLateralSeriesValues { get; set; }
        public ChartValues<MeasureModel> DVLVerticalSeriesValues { get; set; }

        public double DVLForwardValue { get; set; }
        public double DVlLateralValue { get; set; }
        public double DVLVerticalValue { get; set; }

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
            DVLForwardSeriesValues.Add(new MeasureModel
            {
                Index = this.Index,
                Value = this.DVLForwardValue
            });
            DVlLateralSeriesValues.Add(new MeasureModel
            {
                Index = this.Index,
                Value = this.DVlLateralValue
            });
            DVLVerticalSeriesValues.Add(new MeasureModel
            {
                Index = this.Index,
                Value = this.DVLVerticalValue
            });
            // 限定图表最多只有十五个元素
            if (DVLForwardSeriesValues.Count > 15)
            {
                DVLForwardSeriesValues.RemoveAt(0);
                DVlLateralSeriesValues.RemoveAt(0);
                DVLVerticalSeriesValues.RemoveAt(0);
            }
        }
    }
}
