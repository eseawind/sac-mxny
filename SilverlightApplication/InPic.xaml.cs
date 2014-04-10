using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls.Charting;

namespace SilverlightApplication
{
    public partial class InPic : UserControl
    {
        public InPic()
        {
            InitializeComponent();
            setChart("电量");
            ConfigureChart();
        }

        private void ConfigureChart()
        {
            LineSeriesDefinition lineSeries = new LineSeriesDefinition();
            lineSeries.ShowItemLabels = false;
            lineSeries.ShowPointMarks = false;

            radChart.DefaultView.ChartArea.NoDataString = "正在载入图形数据，请等待...";


            radChart.DefaultView.ChartArea.Padding = new Thickness(5, 10, 20, 5);

            radChart.DefaultView.ChartArea.LabelFormatBehavior = LabelFormatBehavior.None;
            radChart.SamplingSettings.SamplingThreshold = 30000;  //此属性用于控制动画的时间
            radChart.DefaultView.ChartArea.EnableAnimations = true; //此属性控制动画效果
            radChart.DefaultView.ChartArea.EnableTransitionAnimations = true;

            radChart.DefaultView.ChartLegend.Visibility = Visibility.Visible;

        }

        private void setChart(string title)
        {
            radChart.DefaultView.ChartTitle.Content = title;
            this.radChart.DefaultSeriesDefinition.LegendDisplayMode = LegendDisplayMode.None;
            radChart.DefaultView.ChartArea.AxisX.MajorGridLinesVisibility = Visibility.Visible;
            radChart.DefaultView.ChartArea.AxisY.StripLinesVisibility = Visibility.Collapsed;
            radChart.DefaultView.ChartArea.AxisY.MajorGridLinesVisibility = Visibility.Visible;
            radChart.DefaultView.ChartArea.AxisY.MinorGridLinesVisibility = Visibility.Collapsed;
            this.radChart.DefaultView.ChartArea.AxisY.AxisStyles.GridLineStyle = this.Resources["GridLineStyle"] as Style;
            this.radChart.DefaultView.ChartArea.AxisX.AxisStyles.GridLineStyle = this.Resources["GridLineStyle"] as Style;

            radChart.DefaultView.ChartTitle.HorizontalAlignment = HorizontalAlignment.Center;

            radChart.DefaultView.ChartArea.AxisX.AutoRange = true;
            radChart.DefaultView.ChartArea.AxisX.StripLinesVisibility = Visibility.Visible;
            radChart.DefaultView.ChartArea.AxisY.StripLinesVisibility = Visibility.Visible;
            radChart.DefaultView.ChartTitle.HorizontalAlignment = HorizontalAlignment.Center;
            Data();
        }

        private void Data()
        {
            DataSeries series = new DataSeries();
            series.Definition = new Pie3DSeriesDefinition();// DoughnutSeriesDefinition();// Doughnut3DSeriesDefinition();// PieSeriesDefinition();// Pie3DSeriesDefinition();
            radChart.Width = 600;
            radChart.Height = 300;

            for (int i = 1; i < 4; i++)
            {
                series.Add(new DataPoint(i, i * i));
            }
            radChart.DefaultView.ChartArea.DataSeries.Add(series);
        }
    }
}
