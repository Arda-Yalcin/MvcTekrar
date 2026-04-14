using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTekrar.Models
{
    public class Ogrenci
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public int No { get; set; }

        //İlişki
        public int BolumId {get; set;}
        public Bolum? Bolum {get;set;}
    }
}