using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GunlukRutinVeDersTakipcisi.Models;

namespace GunlukRutinVeDersTakipcisi.Services
{
    public interface IVeritabaniServisi
    {
        void VerileriKaydet(List<Dersler> dersler, List<GorevRutin> rutinler, List<DersCalismaKaydi> kayitlar);
        List<Dersler> DersleriYukle();
        List<GorevRutin> RutinleriYukle();
        List<DersCalismaKaydi> KayitlariYukle();


        
    }
}
