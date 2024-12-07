using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities.State
{
    public class State
    {
        public int PageNumber { get; set; } // Sayfa numarası
        public int PageSize { get; set; } // Sayfa başına kayıt sayısı
        public int Skip { get; set; } // Atlanacak kayıt sayısı
        public StateSort Sort { get; set; } // Sıralama bilgileri
        public List<StateFilter> Filter { get; set; } // Filtre bilgileri
    }
}
