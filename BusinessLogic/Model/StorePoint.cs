using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class StorePoint
    {
        public string Parameter { get; set; }
        public DateTime? TimeStamp { get; set; }
        public double? Battery1 { get; set; }
        public double? Battery2 { get; set; }
        public double? Battery3 { get; set; }
        public double? Battery4 { get; set; }
        public double? Battery5 { get; set; }
        public int? SlaveId1 { get; set; }
        public int? SlaveId2 { get; set; }
        public int? SlaveId3 { get; set; }
        public int? SlaveId4 { get; set; }
        public int? SlaveId5 { get; set; }
    }
}
