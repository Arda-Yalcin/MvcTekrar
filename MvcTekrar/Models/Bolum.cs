using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTekrar.Models
{
    public class Bolum
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public List<Ogrenci>? Ogrenciler {get; set;}

    }
}