using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Grid
{
    public class GridReqData
    {
        public Dictionary<string, string> Sort { get; set; }
        public string Filter { get; set; }
        public int PageNumber { get; set; }
        public int RecordPerPage { get; set; }
        public string Mode { get; set; }
        public string FixClause { get; set; }
        public string Columns { get; set; }
        public string ExportColumns { get; set; }
    }
}