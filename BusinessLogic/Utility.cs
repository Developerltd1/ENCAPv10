using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static BusinessLogic.Model.StaticModel.DashboardModel;

namespace BusinessLogic
{
    public class Utility
    {



        public DataTable ReplaceSpecialCharsWithNull(DataTable dataTable)
        {
            Regex specialCharPattern = new Regex(@"^[,\/\-_%\s]+$");

            // Loop through all rows in the DataTable
            foreach (DataRow row in dataTable.Rows)
            {
                // Loop through each column in the row
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    var cellValue = row[i]?.ToString();

                    // Check if the cell value contains only special characters
                    if (specialCharPattern.IsMatch(cellValue))
                    {
                        // Assign NULL if the cell contains only special characters
                        row[i] = DBNull.Value;
                    }
                }
            }

            return dataTable; // Return the updated DataTable
        }


        public DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }



        public DataTable ListToDataTable(List<string> list)
        {
            DataTable dt = new DataTable();
            List<GridViewDbModel> _list = new List<GridViewDbModel>();
            GridViewDbModel model = new GridViewDbModel();
            List<object> llist  = null;
            // Add columns to DataTable
            dt.Columns.Add("Parameter", typeof(string));
            dt.Columns.Add("Battery1", typeof(string));
            dt.Columns.Add("Battery2", typeof(string));
            dt.Columns.Add("Battery3", typeof(string));
            dt.Columns.Add("Battery4", typeof(string));
            dt.Columns.Add("Battery5", typeof(string));
            dt.Columns.Add("Battery6", typeof(double));
            dt.Columns.Add("Battery7", typeof(double));
            dt.Columns.Add("Battery8", typeof(double));
            dt.Columns.Add("Battery9", typeof(double));
            dt.Columns.Add("Battery10", typeof(double));
            dt.Columns.Add("Battery11", typeof(double));
            dt.Columns.Add("Battery12", typeof(double));
            dt.Columns.Add("Battery13", typeof(double));
            dt.Columns.Add("Battery14", typeof(double));
            dt.Columns.Add("Battery15", typeof(double));
            dt.Columns.Add("Battery16", typeof(double));
            dt.Columns.Add("Battery17", typeof(double));
            dt.Columns.Add("Battery18", typeof(double));
            dt.Columns.Add("Battery19", typeof(double));
            dt.Columns.Add("Battery20", typeof(double));
            dt.Columns.Add("SlaveId1", typeof(int));
            dt.Columns.Add("SlaveId2", typeof(int));
            dt.Columns.Add("SlaveId3", typeof(int));
            dt.Columns.Add("SlaveId4", typeof(int));
            dt.Columns.Add("SlaveId5", typeof(int));
            dt.Columns.Add("SlaveId6", typeof(int));
            dt.Columns.Add("SlaveId7", typeof(int));
            dt.Columns.Add("SlaveId8", typeof(int));
            dt.Columns.Add("SlaveId9", typeof(int));
            dt.Columns.Add("SlaveId10", typeof(int));
            dt.Columns.Add("SlaveId11", typeof(int));
            dt.Columns.Add("SlaveId12", typeof(int));
            dt.Columns.Add("SlaveId13", typeof(int));
            dt.Columns.Add("SlaveId14", typeof(int));
            dt.Columns.Add("SlaveId15", typeof(int));
            dt.Columns.Add("SlaveId16", typeof(int));
            dt.Columns.Add("SlaveId17", typeof(int));
            dt.Columns.Add("SlaveId18", typeof(int));
            dt.Columns.Add("SlaveId19", typeof(int));
            dt.Columns.Add("SlaveId20", typeof(int));






            // Add rows from the list
            foreach (string item in list)
            {
                DataRow row = dt.NewRow();
                row["Parameter"] = item;
                // Other columns are left blank for each row
                dt.Rows.Add(item);
                model.Parameter = item;
                _list.Add(model);
            }
           
            return dt;
        }














        #region Extra

        public async Task<List<ChartValues<double>>> LoadModbusDataTestingByAqib(Int32 batteryIndex, int functionCode, int registerNumber, int data)
        {
            string serialNumberString = "-";
            try
            {
                lst = new List<double>();
                dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Logger.Info("MainParamaterForm/LoadModbusData| batteryIndex: " + batteryIndex.ToString() + " SERIAL_READ: " + functionCode.ToString() + " registerNumber: " + registerNumber + " data: " + data.ToString());

                modbusTotalReg = modbusRegCount + modbusStartReg;
                Logger.Info("MainParamaterForm/LoadModbusData| modbusTotalReg: " + modbusTotalReg.ToString());

                index = 0;
                searchValue = 0;
                searchRange = 0;
                string hexString;
                z = 0;
                {
                    {
                        numSlaveID.Text = batteryIndex.ToString();
                        slaveID = batteryIndex;
                        StaticModelValues.slaveId1 = Convert.ToInt32(numSlaveID.Text);
                        Logger.Info("MainParamaterForm/LoadModbusData| slaveId1: " + StaticModelValues.slaveId1.ToString());
                        try
                        {
                            //InitializeModbusClient();
                            UpdateStatusConnection("Connected");
                            try
                            {
                                Logger.Info("MainParamaterForm/LoadModbusData| functionCode: " + functionCode);
                                switch (functionCode)
                                {//StaticComment
                                 //case 1:
                                 //boolsRegister = modbusClient.ReadCoils(modbusStartReg, modbusRegCount);// Read Coils (0x01)
                                 // Logger.Info("MainParamaterForm/LoadModbusData| boolsRegister: " + boolsRegister.ToString());
                                 // break;
                                 //case 2:
                                 // boolsRegister = modbusClient.ReadDiscreteInputs(modbusStartReg, modbusRegCount);// Read Discrete Inputs (0x02)
                                 //  break;
                                    case 3:
                                        //registers = modbusClient.ReadHoldingRegisters(modbusStartReg, modbusRegCount);    //uncomment
                                        //int[] serialNumberRaw = modbusClient.ReadHoldingRegisters(registerNumber, data);  //uncomment
                                        //registers = arr;
                                        //int[] serialNumberRaw = registers;



                                        //hexString = string.Join("", Array.ConvertAll(serialNumberRaw, val => val.ToString("X")));   //uncomment
                                        //Logger.Info("MainParamaterForm/LoadModbusData| hexString: " + hexString.ToString());

                                        //serialNumberString = ConvertHexStringToAscii(hexString);    //uncomment
                                        await Task.Delay(1000); // Remove this and replace with actual async call

                                        Logger.Info("MainParamaterForm/LoadModbusData| serialNumberString: " + serialNumberString.ToString());
                                        #region StaticDataByAQIB
                                        rnd = new Random();
                                        voltage = rnd.Next(0, 1000) * 0.1;
                                        current = (rnd.Next(0, 1000) - 30000) * 0.1;
                                        power = (rnd.Next(0, 1000));
                                        soc = (rnd.Next(0, 1000)) * 0.1;
                                        ah = (rnd.Next(0, 1000)) * 0.1;
                                        temp = (rnd.Next(0, 1000)) - 40;
                                        cell1 = (rnd.Next(0, 1000)) * 0.001;
                                        cell2 = (rnd.Next(0, 1000)) * 0.001;
                                        cell3 = (rnd.Next(0, 1000)) * 0.001;
                                        cell4 = (rnd.Next(0, 1000)) * 0.001;
                                        cell5 = (rnd.Next(0, 1000)) * 0.001;
                                        cell6 = (rnd.Next(0, 1000)) * 0.001;
                                        cell7 = (rnd.Next(0, 1000)) * 0.001;
                                        cell8 = (rnd.Next(0, 1000)) * 0.001;
                                        cell9 = (rnd.Next(0, 1000)) * 0.001;
                                        cell10 = (rnd.Next(0, 1000)) * 0.001;
                                        cell11 = (rnd.Next(0, 1000)) * 0.001;
                                        cell12 = (rnd.Next(0, 1000)) * 0.001;
                                        cell13 = (rnd.Next(0, 1000)) * 0.001;
                                        cell14 = (rnd.Next(0, 1000)) * 0.001;
                                        cell15 = (rnd.Next(0, 1000)) * 0.001;

                                        fault1 = (rnd.Next(1, 1000)) * 0.009;
                                        fault2 = (rnd.Next(2, 1000)) * 0.009;
                                        fault3 = (rnd.Next(3, 1000)) * 0.009;
                                        fault4 = (rnd.Next(4, 1000)) * 0.009;
                                        #endregion

                                        #region DynamicData

                                        maxCell = FindMax(cell1, cell2, cell3, cell4, cell5, cell6, cell7, cell8, cell9, cell10, cell11, cell12, cell13, cell14, cell15);
                                        minCell = FindMin(cell1, cell2, cell3, cell4, cell5, cell6, cell7, cell8, cell9, cell10, cell11, cell12, cell13, cell14, cell15);
                                        diffCell = Math.Abs(maxCell - minCell);
                                        #endregion

                                        #region CMOS & DMOS reading
                                        string mos = (12) + "/" + (20);

                                        #endregion

                                        #region dynamicToGrid
                                        UpdateDataGridView(z++, batteryIndex, serialNumberString);
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(voltage, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(current, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(power, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(soc, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(ah, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(temp, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell1, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell2, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell3, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell4, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell5, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell6, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell7, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell8, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell9, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell10, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell11, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell12, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell13, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell14, 2));
                                        UpdateDataGridView(z++, batteryIndex, Math.Round(cell15, 2));
                                        UpdateDataGridView(z++, batteryIndex, fault1);
                                        UpdateDataGridView(z++, batteryIndex, fault2);
                                        UpdateDataGridView(z++, batteryIndex, fault3);
                                        UpdateDataGridView(z++, batteryIndex, fault4);
                                        UpdateDataGridView(z++, batteryIndex, mos);

                                        var w = dgvCellLevel.Rows[23].Cells[batteryIndex].Value;
                                        int c = Convert.ToInt32(dgvCellLevel.Rows[23].Cells[batteryIndex].Value);//int.Parse(w.ToString());
                                        #endregion

                                        #region FAULTS / ALARMS
                                        z = 0;//fault 2
                                        #endregion

                                        staticDt = MainParamatersFormPartial.ConvertDataGridViewToDataTable(dgvCellLevel);
                                        Logger.Info("MainParamaterForm/LoadModbusData| staticDt " + staticDt.ToString());
                                        fnpublic();
                                        break;
                                    //case 4:
                                    //registers = modbusClient.ReadInputRegisters(modbusStartReg, modbusRegCount);// Read Input Registers (0x04)
                                    //Logger.Info("MainParamaterForm/LoadModbusData| registers " + registers.ToString());
                                    //break;
                                    //case 6:
                                    //modbusClient.WriteSingleRegister(registerNumber, data);
                                    //Logger.Info("MainParamaterForm/LoadModbusData| registerNumber " + registerNumber.ToString() + " data: " + data.ToString());
                                    //Logger.Info("MainParamaterForm/LoadModbusData| registers " + modbusClient.ToString());
                                    //break;
                                    //case 0x50:
                                    //modbusClient.Disconnect();
                                    //Logger.Info("MainParamaterForm/LoadModbusData| modbusClient.Disconnect() " + modbusClient.ToString());
                                    //
                                    //InitializeSerialPort();
                                    //Logger.Info("MainParamaterForm/LoadModbusData| InitializeSerialPort()");
                                    //string FixedFrameID = "A5401908";
                                    //Logger.Info("MainParamaterForm/LoadModbusData|  FixedFrameID");
                                    //short value4 = (short)registerNumber;
                                    //Logger.Info("MainParamaterForm/LoadModbusData|  value4:" + value4.ToString());
                                    //short value3 = (short)registerNumber;
                                    //Logger.Info("MainParamaterForm/LoadModbusData|  value3:" + value3.ToString());
                                    //short value2 = (short)data;
                                    //Logger.Info("MainParamaterForm/LoadModbusData|  value2:" + value2.ToString());
                                    //short value1 = (short)data;
                                    //Logger.Info("MainParamaterForm/LoadModbusData|  value2:" + value1.ToString());
                                    //byte[] canData = new byte[8];
                                    //Array.Copy(SwapBytes(BitConverter.GetBytes(value1)), 0, canData, 0, 2);
                                    //Array.Copy(SwapBytes(BitConverter.GetBytes(value2)), 0, canData, 2, 2);
                                    //Array.Copy(SwapBytes(BitConverter.GetBytes(value3)), 0, canData, 4, 2);
                                    //Array.Copy(SwapBytes(BitConverter.GetBytes(value4)), 0, canData, 6, 2);
                                    //Logger.Info("MainParamaterForm/LoadModbusData| Array.Copy");
                                    //// Create the CAN packet
                                    //byte[] canPacket = CreateCANPacket(FixedFrameID, canData);
                                    //
                                    //// Display the resulting CAN packet
                                    ////canPkt.Text = "Generated CAN Packet: " + BitConverter.ToString(canPacket).Replace("-", "");
                                    //SendCANPacket(canPacket);
                                    //string tmpp = BitConverter.ToString(canPacket).Replace("-", string.Empty);
                                    //Logger.Info("MainParamaterForm/LoadModbusData|  canPacket:" + tmpp);
                                    //case 98://load setting page data points
                                    //    registers = modbusClient.ReadHoldingRegisters(registerNumber, data);
                                    //    Logger.Info("MainParamaterForm/LoadModbusData|  registers:" + registers.ToString());
                                    //    StaticModelValues.register_Settings = registers;
                                    //    break;
                                    //case 99://load serial number
                                    //    registers = modbusClient.ReadHoldingRegisters(registerNumber, data);
                                    //    Logger.Info("MainParamaterForm/LoadModbusData|  registers:" + registers.ToString());
                                    //    StaticModelValues.register_Settings = registers;
                                    //    hexString = string.Join("", Array.ConvertAll(registers, val => val.ToString("X")));
                                    //    serialNumberString = ConvertHexStringToAscii(hexString);
                                    //    Logger.Info("MainParamaterForm/LoadModbusData|  serialNumberString:" + serialNumberString.ToString());
                                    //    break;
                                    //case 100://values writing function from settings page
                                    //    modbusClient.WriteSingleRegister(registerNumber, data);
                                    //    Logger.Info("MainParamaterForm/LoadModbusData|  modbusClient:" + modbusClient.ToString());
                                    //    registers = modbusClient.ReadHoldingRegisters(registerNumber, 1);// Read Holding Registers (0x03)
                                    //    Logger.Info("MainParamaterForm/LoadModbusData|  modbusClient:" + registers.ToString());
                                    //    StaticModelValues.tbLowCellVolt = registers[0];
                                    //    Logger.Info("MainParamaterForm/LoadModbusData|  registers[0]:" + registers[0].ToString());
                                    //    break;
                                    default:
                                        UpdateInfoMessages("Function code not handled.", Color.Red, Color.White);
                                        break;

                                }

                                z = 0;
                                infoMessages.Text = ("Reading Successful");

                                avgcell = ((cell1 + cell2 + cell3 + cell4 + cell5 + cell6 + cell7 + cell8 + cell9 + cell10 + cell11 + cell12 + cell13 + cell14) / 14);
                                labelVolt.Text = voltage.ToString("0.0");
                                labelCurrent.Text = current.ToString("0.0");
                                labelPower.Text = power.ToString("0.0");
                                labelTemp.Text = temp.ToString("0.0");
                                labelSoc.Text = soc.ToString("0.0");
                            }
                            catch (Exception ex)
                            {
                                Logger.Error("MainParamaterForm/LoadModbusData|  Exception:" + ex.Message.ToString());
                                int[] tmp = { 0, 0 };
                                UpdateInfoMessages($"Error reading Modbus data:: {ex.Message}", Color.Red, Color.White);
                                StaticModelValues.register_Settings = tmp;
                                for (z = 0; z <= 18; z++)
                                    dgvCellLevel.Rows[z].Cells[batteryIndex].Value = "-";
                                labelVolt.Text = "-";
                                labelCurrent.Text = "-";
                                labelPower.Text = "-";
                                labelTemp.Text = "-";
                                labelSoc.Text = "-";
                            }
                            finally   //uncomment
                            {
                                //if (modbusClient.Connected)
                                //{
                                //    modbusClient.Disconnect(); // Disconnect from Modbus server
                                //    statusConnection.Text = "Disconnected";
                                //}
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Error("MainParamaterForm/LoadModbusData|  Outer Exception:" + ex.Message.ToString());
                            UpdateStatusConnection("Error: " + ex.Message);
                            UpdateInfoMessages($"MainParamaterForm/LoadModbusData|  Outer Exception:: {ex.Message}", Color.Red, Color.White);
                        }
                        readReady = false;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error("MainParamaterForm/LoadModbusData|  Main Exception:" + ex.Message.ToString());
                UpdateStatusConnection("Error: " + ex.Message);
                UpdateInfoMessages($"Error: {ex.Message}", Color.Red, Color.White);
            }

            return allLists;
        }

        #endregion
    }
}
