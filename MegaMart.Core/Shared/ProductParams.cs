using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaMart.Core.Shared
{
    public class GetAllProductParams
    {
        public string? Sort { get; set; }
        public int? CategoryId { get; set; }
        public string? Search { get; set; }
        public int MaxPageSize { get; set; } = 5;
        private int _pageSize { get; set; } = 3;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        public int PageNumber { get; set; } = 1;
    }
}
