using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunlukRutinVeDersTakipcisi.Models
{
    public enum ZorlukDerecesi
    {
        Kolay,
        Orta,
        Zor
    }
    public class Dersler
    {
        public int DersId { get; set; }
        public string DersAdi { get; set; }
        public int Kredi { get; set; }
        public double HaftalikHedef { get; set; }
        public ZorlukDerecesi Zorluk { get; set; }
        public double OnerilenCalismaSuresiHesapla()
        {

            double onerilensaat = Kredi * (int)Zorluk * 1;
            
            return onerilensaat;
        }
    }
}
