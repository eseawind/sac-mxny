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

namespace SilverlightApplication
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();


        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {


        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {

            WcfService.UnitClient unitClient = new WcfService.UnitClient();
            unitClient.ValCompleted += new EventHandler<WcfService.ValCompletedEventArgs>(clientUnit_ValCompleted);
            unitClient.ValAsync("#1");


        }

        void clientUnit_ValCompleted(object sender, WcfService.ValCompletedEventArgs e)
        {
            IList<double> list = new List<double>();
            list = e.Result;
            UnitObj uo = new UnitObj();
            uo.Power = list[0];
            uo.Flow = list[1];
            uo.Pressure = list[2];
            uo.Temperature = list[3];
            uo.ReheatTemperature = list[4];
            uo.Vacuum = list[5];
            uo.Efficiency = list[6];
            uo.Heatconsumption = list[7];
            uo.Coalconsumption = list[8];

            IList<UnitObj> lt = new List<UnitObj>();
            lt.Add(uo);
            dgData.ItemsSource = lt;
        }
    }

    public class UnitObj
    {
        double power;
        public double Power { get { return power; } set { power = value; } }
        double flow;
        public double Flow { get { return flow; } set { flow = value; } }
        double pressure;
        public double Pressure { get { return pressure; } set { pressure = value; } }
        double temperature;
        public double Temperature { get { return temperature; } set { temperature = value; } }
        double reheatTemperature;
        public double ReheatTemperature { get { return reheatTemperature; } set { reheatTemperature = value; } }
        double vacuum;
        public double Vacuum { get { return vacuum; } set { vacuum = value; } }
        double efficiency;
        public double Efficiency { get { return efficiency; } set { efficiency = value; } }
        double heatconsumption;
        public double Heatconsumption { get { return heatconsumption; } set { heatconsumption = value; } }
        double coalconsumption;
        public double Coalconsumption { get { return coalconsumption; } set { coalconsumption = value; } }
    }
}
