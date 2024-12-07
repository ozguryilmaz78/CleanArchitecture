using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities.State
{
    public class StateFilter
    {
        public string Field { get; set; } // Field to filter by
        public string Value { get; set; } // Value to filter with
        public string Operator { get; set; } // "eq", "neq", "contains", etc.
        public string Type { get; set; } // Fully qualified type name (e.g., "System.Boolean", "System.String")
    }
}
