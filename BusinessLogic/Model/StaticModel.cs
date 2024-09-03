using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class StaticModel
    {
        
            
        public class MainParamatersForm_Chart
        {
            public int Year { get; set; }
            public int Month { get; set; }
            public int Value { get; set; }
        }
        public class MainParamatersForm_Parameters
        {
            public double Volt { get; set; }
            public double Current { get; set; }
            public double Power { get; set; }
            public double Ah { get; set; }
            public double Temp { get; set; }
        }

        public class SettingsModel
        {
            public class ComboBoxModel
            {
                public string DisplayText { get; set; }
                public string Value { get; set; }

                public ComboBoxModel(string displayText, string value)
                {
                    DisplayText = displayText;
                    Value = value;
                }

                public override string ToString()
                {
                    return DisplayText;
                }
            }

            public class SettingsRequest
            {
                public SettingsRequest()
                {
                    cbMode1 = new List<ComboBoxModel>();
                }
                public List<ComboBoxModel> cbMode1 { get; set; }
                public Dictionary<int, string> cbMode { get; set; }
                public DateTime datePickerMS { get; set; }
                public double tbSetDishargeEnergy { get; set; }
                public double tbSetChargeEnergy { get; set; }
                public double tbHighCurrDischarge { get; set; }
                public double tbHighCurrCharge { get; set; }
                public double tbSocLowAlarm { get; set; }
                public double tbSocHighAlarm { get; set; }
                public double tbLowSumVolt { get; set; }
                public double tbHighSumVolt { get; set; }
                public double tbHighTempDischarge { get; set; }
                public double tbHighTempCharge { get; set; }
                public double tbCurrent { get; set; }
                public double tbSoc { get; set; }
                public double tbSleepTimeout { get; set; }
                public double tbUtbOffset { get; set; }
                public double tbLowCellVolt { get; set; }
                public double tbHighCellVolt { get; set; }
                public double tbSerial { get; set; }
                public double tbHighDischargeTemp { get; set; }

                public double tbLowDischargeTemp { get; set; }
                public double  tbCellRatedVoltage { get; set; }
                public double  tbMaxVoltageDifference { get; set; }
                public double  tbBatteryCapacity { get; set; }
                public double tbSleepTime { get; set; }
            }
            //public class SettingsResponse
            //{

            //}
        }

        public class DashboardModel
        {
            public class Parameters
            {
                public double labelDiscEnergy { get; set; }
                public double labelChargeEnergy { get; set; }
                public double labelTerminalCurrent { get; set; }
                public double labelTermilanVolt { get; set; }
                public double labelSystemTemp { get; set; }
                public double labelStateOfCharge { get; set; }
                public double labelRemaningTime { get; set; }
                public double labelPowerSystem { get; set; }
                public double labelRemaningEnergy { get; set; }
            }

            public class GridViewDbModel
            {
                public string Parameter { get; set; }
                public string Battery1 { get; set; }
                public string Battery2 { get; set; }
                public string Battery3 { get; set; }
                public string Battery4 { get; set; }
                public string Battery5 { get; set; }
                //public dynamic Parameter1 { get; set; }
            }
            }
    }
}