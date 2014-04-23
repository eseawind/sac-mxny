using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SAC.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IUnit”。
    [ServiceContract]
    public interface IUnit
    {
        [OperationContract]
        double Power(string key);

        [OperationContract]
        double HisPower(string key);

        [OperationContract]
        double PowerMax(string key);

        [OperationContract]
        double PowerMin(string key);

        [OperationContract]
        double PowerAvg(string key);

        [OperationContract]
        double Flow(string key);

        [OperationContract]
        double Pressure(string key);

        [OperationContract]
        double Temperature(string key);

        [OperationContract]
        double ReheatTemperature(string key);

        [OperationContract]
        double Vacuum(string key);

        [OperationContract]
        double Efficiency(string key);

        [OperationContract]
        double Heatconsumption(string key);

        [OperationContract]
        double Coalconsumption(string key);

        [OperationContract]
        double[] Val(string key);

        [OperationContract]
        double GetDL(string key, string st, string et);

        [OperationContract]
        double GetWind(string key);
    }
}
