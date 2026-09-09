using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunlukRutinVeDersTakipcisi.Managers
{
    public class SonucIstatistikleri
    {
        private readonly TakipKontrolYoneticisi _takipYoneticisi;
        public SonucIstatistikleri(TakipKontrolYoneticisi takipYoneticisi)
        {

            _takipYoneticisi = takipYoneticisi ?? throw new ArgumentNullException(nameof(takipYoneticisi));
        }
        public double toplamCalismaSuresi()
        {
           return _takipYoneticisi.KayitlarListesi.Sum(k => k.CalisilanSure);
        }
        public double dersCalismaSuresi(int DersId)
        {
            return _takipYoneticisi.KayitlarListesi.Where(k => k.DersId == DersId).Sum(k => k.CalisilanSure);

        }
        public double rutinTamamlamaYuzdesi()
        {
            double toplamRutinSayisi = _takipYoneticisi.RutinlerListesi.Count;
            if (toplamRutinSayisi == 0)
            {
                return 0;
            }
            double tamamlananSayisi = _takipYoneticisi.RutinlerListesi.Count(r => r.Tamamlandimi == true);
            return (tamamlananSayisi / toplamRutinSayisi) * 100;
        }
        public int KalanRutinSayisi()
        {
            return _takipYoneticisi.RutinlerListesi.Count(r => r.Tamamlandimi == false);
        }
        public double BugunkuCalismaSuresi()
        {
            return _takipYoneticisi.KayitlarListesi
                .Where(k => k.Tarih.Date == DateTime.Today)
                .Sum(k => k.CalisilanSure);
        }
        public double GunlukOrtalamaSure()
        {
            if (_takipYoneticisi.KayitlarListesi.Count == 0)
                return 0;

            double toplamSure = _takipYoneticisi.KayitlarListesi.Sum(k => k.CalisilanSure);

            int farkliGunSayisi = _takipYoneticisi.KayitlarListesi
                .Select(k => k.Tarih.Date)
                .Distinct()
                .Count();

            if (farkliGunSayisi == 0)
                return 0;

            return toplamSure / farkliGunSayisi;
        }
        public int EnCokCalisilanDersId()
        {
            if (_takipYoneticisi.KayitlarListesi.Count == 0)
                return 0; 

            return _takipYoneticisi.KayitlarListesi
                .GroupBy(k => k.DersId).OrderByDescending(g => g.Sum(k => k.CalisilanSure)).First().Key; 
        }
    }
}
