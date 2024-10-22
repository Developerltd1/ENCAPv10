using EasyModbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMView
{
    public class ModbusClientWithHandler
    {
        private ModbusClient modbusClient;

        public ModbusClientWithHandler(ModbusClient modbusClient)
        {
            this.modbusClient = modbusClient;
        }

        // Define the delegate and event for when data is received
        public delegate void DataReceivedHandler(object sender, int[] data);
        public event DataReceivedHandler DataReceived;

        // Method to read registers asynchronously
        public async Task ReadRegistersAsync(int startReg, int regCount)
        {
            try
            {
                // Read the registers asynchronously
                var registers = await Task.Run(() => modbusClient.ReadHoldingRegisters(startReg, regCount));

                // Raise the DataReceived event
                DataReceived?.Invoke(this, registers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading registers: {ex.Message}");
            }
        }
    }



}
