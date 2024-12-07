using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities.State
{
    public class StateSort
    {
        public string Dir { get; set; } // "asc" or "desc"
        public string Field { get; set; } // Field to sort by
    }
}
