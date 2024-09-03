using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class AlarmMdl
    {
        public int ID { get; set; }
        public string Alarm { get; set; }
        public string OccurrenceTime { get; set; }

        public AlarmMdl(int alarmID, string alarmValue, string occurenceTime)
        {
            ID = alarmID;
            Alarm = alarmValue;
            OccurrenceTime = occurenceTime;
        }
    }
}
