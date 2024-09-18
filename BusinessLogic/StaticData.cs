

using BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.Model.StaticModel;
using static BusinessLogic.Model.StaticModel.SettingsModel;
namespace BusinessLogic
{
    public class StaticData
    {

        public StaticModel.MainParamatersForm_Parameters MainParamatersForm_ParametersFn()
        {
            StaticModel.MainParamatersForm_Parameters _Parameters = new StaticModel.MainParamatersForm_Parameters();
            _Parameters.Volt = 0;
            _Parameters.Current = 0;
            _Parameters.Power = 0;
            _Parameters.Ah = 0;
            _Parameters.Temp = 0;
            return _Parameters;
        }

        public DataTable DataGridView()
        {

            List<string> list = new List<string>()
            {
                "Serial","Voltage (V)","Current (Amps)","Power (kW)","SOC","Total Remaining Capacity(Ah)","Temperature (C)","Cell-1 (V)",
                "Cell-2 (V)","Cell-3 (V)","Cell-4 (V)","Cell-5 (V)","Cell-6 (V)","Cell-7 (V)","Cell-8 (V)","Cell-9 (V)","Cell-10 (V)",
                "Cell-11 (V)","Cell-12 (V)","Cell-13 (V)","Cell-14 (V)","Cell-15 (V)","FAULT-1", "FAULT-2", "FAULT-3", "FAULT-4","Max Cell Volt",
                "Min Cell Volt","Max Cell Diff.","Charge/DischargeMOS"
            };

            DataTable dt = new Utility().ListToDataTable(list);
            return dt;
        }
        public async Task<DataTable> DataGridViewAsync()
        {
            // Simulate async data retrieval
            List<string> list = new List<string>()
            {
                "Serial", "Voltage (V)", "Current (Amps)", "Power (kW)", "SOC", "Total Remaining Capacity(Ah)", "Temperature (C)", "Cell-1 (V)",
                "Cell-2 (V)", "Cell-3 (V)", "Cell-4 (V)", "Cell-5 (V)", "Cell-6 (V)", "Cell-7 (V)", "Cell-8 (V)", "Cell-9 (V)", "Cell-10 (V)",
                "Cell-11 (V)", "Cell-12 (V)", "Cell-13 (V)", "Cell-14 (V)", "Cell-15 (V)", "FAULT-1", "FAULT-2", "FAULT-3", "FAULT-4", "Max Cell Volt",
                "Min Cell Volt", "Max Cell Diff.", "Charge/DischargeMOS"
            };

            return await Task.Run(() => new Utility().ListToDataTable(list));
        }

        public DataTable DataGridViewAlarm()
        {

            List<AlarmMdl> alarms = new List<AlarmMdl>();
            DataTable dt = new Utility().ListToDataTable(alarms);
            return dt;
        }



        #region StaticData_SettingsData

        #endregion
        public SettingsRequest SettingsData()
        {
            List<StaticModel.SettingsModel.ComboBoxModel> Comboitems1 = new List<StaticModel.SettingsModel.ComboBoxModel>()
            {
                new StaticModel.SettingsModel.ComboBoxModel("Please Select", "0"),
                new StaticModel.SettingsModel.ComboBoxModel("Online Monitoring", "1"),
                new StaticModel.SettingsModel.ComboBoxModel("Offline Monitoring", "2")
            };

            SettingsRequest settingsModel = new SettingsRequest()
            {
                tbCurrent = 0,
                tbLowCellVolt = 0,
                tbHighCellVolt = 0,
                tbHighCurrCharge = 0,
                tbHighCurrDischarge = 0,
                tbHighTempCharge = 0,
                tbHighTempDischarge = 0,
                tbHighSumVolt = 0,
                tbLowSumVolt = 0,
                tbSocHighAlarm = 0,
                tbSocLowAlarm = 0,
                tbSetChargeEnergy = 0,
                tbSetDishargeEnergy = 0,
                tbSleepTimeout = 0,
                tbSoc = 0,
                tbUtbOffset = 0,
                tbSerial = 0,
                tbHighDischargeTemp = 0,
                tbLowDischargeTemp = 0,
                tbCellRatedVoltage = 0,
                tbMaxVoltageDifference = 0,
                tbBatteryCapacity = 0,
                tbSleepTime = 0,

                datePickerMS = DateTime.Now,
                cbMode1 = Comboitems1,
                //cbMode = items,
            };
            return settingsModel;
        }


        public DashboardModel.Parameters DashboardData()
        {
            DashboardModel.Parameters parameters = new DashboardModel.Parameters()
            {
                labelChargeEnergy = 0,
                labelDiscEnergy = 0,
                labelPowerSystem = 0,
                labelRemaningEnergy = 0,
                labelRemaningTime = 0,
                labelStateOfCharge = 0,
                labelSystemTemp = 0,
                labelTermilanVolt = 0,
                labelTerminalCurrent = 0,
            };

            return parameters;
        }


    }
}
