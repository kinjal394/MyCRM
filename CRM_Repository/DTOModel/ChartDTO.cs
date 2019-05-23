using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Data
{
    public class ChartData
    {
        public List<string> categories { get; set; }
        public List<ChartList> chartDataList { get; set; }
    }
    public class ChartList
    {
        public List<decimal> data { get; set; }
        public string name { get; set; }
    }
}
