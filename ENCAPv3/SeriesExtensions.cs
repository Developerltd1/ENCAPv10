using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMView
{
    public static class SeriesExtensions
    {
        public static void TrimToLast(this ChartValues<double> values, int maxRecords)
        {
            while (values.Count > maxRecords)
            {
                values.RemoveAt(0);
            }
        }
    }

}
