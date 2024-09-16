using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class ChartModel
    {
        public DateTime? TimeStamp { get; set; }
        public double? Voltage { get; set; }
        public double? Currents { get; set; }
        public double? Power { get; set; }
        public double? SOC { get; set; }
        public double? Temp { get; set; }

    }

    public class AlarmModel
    {
        public DateTime? TimeStamp { get; set; }
        public int? BatteryId { get; set; }
        public string Alarm { get; set; }
        public DateTime? OccurrenceTime { get; set; }

    }
}
