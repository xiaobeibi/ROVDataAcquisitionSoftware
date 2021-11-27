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
    /// DVLHeightChartControl.xaml 的交互逻辑
    /// </summary>
    public partial class DVLHeightChartControl : UserControl
    {
        public DVLHeightChartControl()
        {
            InitializeComponent();

            // 设置图表的XY和数值对应
            CartesianMapper<MeasureModel> mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.Index)
                .Y(model => model.Value);

            Charting.For<MeasureModel>(mapper);

            DVlHeigthSeriesValues = new ChartValues<MeasureModel>();
            DepthGaugeSeriesValues = new ChartValues<MeasureModel>();
            DataContext = this;
        }

        public ChartValues<MeasureModel> DVlHeigthSeriesValues { get; set; }
        public ChartValues<MeasureModel> DepthGaugeSeriesValues { get; set; }

        public double DVlHeigthValue { get; set; }
        public double DepthGaugeValue { get; set; }

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
            DVlHeigthSeriesValues.Add(new MeasureModel
            {
                Index = this.Index,
                Value = this.DVlHeigthValue
            });
            DepthGaugeSeriesValues.Add(new MeasureModel
            {
                Index = this.Index,
                Value = this.DepthGaugeValue
            });
            // 限定图表最多只有十五个元素
            if (DVlHeigthSeriesValues.Count > 15)
            {
                DVlHeigthSeriesValues.RemoveAt(0);
                DepthGaugeSeriesValues.RemoveAt(0);
            }
        }
    }
}
