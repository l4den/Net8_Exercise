using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class QueryObject
    {
        public string? SortBy { get; set; } = null;
        public bool IsDescending {get; set;} = false;
        public int PageNumber {get; set;} = 1;
        public int PageSize {get; set;} = 10;
        public DateTime StartDate {get; set;} = DateTime.Now;
        public DateTime EndDate {get; set;} = DateTime.Now;
        public int excludeId {get; set;} = 0;
        public string? SearchWord {get; set;} = null;
    }
}