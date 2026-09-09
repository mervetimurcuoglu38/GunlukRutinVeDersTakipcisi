using GunlukRutinVeDersTakipcisi.Models;
using GunlukRutinVeDersTakipcisi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GunlukRutinVeDersTakipcisi.Managers
{
    public class TakipKontrolYoneticisi
    {
        private readonly IVeritabaniServisi _veriTabaniServisi;

        public List<Dersler> DerslerListesi { get; private set; }
        public List<GorevRutin> RutinlerListesi { get; private set; }
        public List<DersCalismaKaydi> KayitlarListesi { get; private set; }

        public TakipKontrolYoneticisi(IVeritabaniServisi veriTabaniServisi)
        {
            _veriTabaniServisi = veriTabaniServisi;

            DerslerListesi = _veriTabaniServisi.DersleriYukle();
            RutinlerListesi = _veriTabaniServisi.RutinleriYukle();
            KayitlarListesi = _veriTabaniServisi.KayitlariYukle();
            
        }
        public void DersEkle(Dersler yeniDers)
        {
            DerslerListesi.Add(yeniDers);
            VerileriKaydet();
            
        }
        public void VerileriKaydet()
        {
            _veriTabaniServisi.VerileriKaydet(DerslerListesi,RutinlerListesi,KayitlarListesi);
        }

        public void RutinEkle(GorevRutin yeniRutin)
        {
            RutinlerListesi.Add(yeniRutin);
            VerileriKaydet();
        }
        public void KayitEkle(DersCalismaKaydi yeniKayit)
        {
            KayitlarListesi.Add(yeniKayit);
            VerileriKaydet();
        }
        public void RutinDurumGuncelle(GorevRutin rutinler, bool yeniDurum)
        {
            var hedefRutin = RutinlerListesi.FirstOrDefault(r => r.Id == rutinler.Id);
            if (hedefRutin != null)
            {
                hedefRutin.Tamamlandimi = yeniDurum;
                VerileriKaydet();
            }
        }
        public void DersSil(Dersler ders)
        {
            DerslerListesi.Remove(ders);
            VerileriKaydet();
        }
        public void RutinSil(GorevRutin rutin)
        {
            RutinlerListesi.Remove(rutin);
            VerileriKaydet();
        }
        public void KayitSil(DersCalismaKaydi kayit)
        {
            KayitlarListesi.Remove(kayit);
            VerileriKaydet();
        }
    }
}
