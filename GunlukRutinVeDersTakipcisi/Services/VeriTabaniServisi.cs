using GunlukRutinVeDersTakipcisi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
namespace GunlukRutinVeDersTakipcisi.Services
{
    public class VeriTabaniServisi : IVeritabaniServisi
    {
        public List<Dersler> DersleriYukle()
        {
            if (!File.Exists("veri.json"))
            {
                return new List<Dersler>();
            }

            string jsonMetin = File.ReadAllText("veri.json");
            VeriPaketi paket = JsonSerializer.Deserialize<VeriPaketi>(jsonMetin);

            return paket?.dersler ?? new List<Dersler>();
        }

        public List<DersCalismaKaydi> KayitlariYukle()
        {
            if (!File.Exists("veri.json"))
            {
                return new List<DersCalismaKaydi>();

            }
            string jsonMetin = File.ReadAllText("veri.json");
            VeriPaketi paket = JsonSerializer.Deserialize<VeriPaketi>(jsonMetin);
            return paket?.kayitlar ?? new List<DersCalismaKaydi>();
        }

        public List<GorevRutin> RutinleriYukle()
        {
            if (!File.Exists("veri.json"))
            {
              return new List<GorevRutin>();
            }
            string jsonMetin = File.ReadAllText("veri.json");
            VeriPaketi paket = JsonSerializer.Deserialize<VeriPaketi>(jsonMetin);
            return paket?.rutinler ?? new List<GorevRutin>();
        }

        public void VerileriKaydet(List<Dersler> dersler, List<GorevRutin> rutinler, List<DersCalismaKaydi> kayitlar)
        {
            VeriPaketi paket = new VeriPaketi();
            paket.dersler = dersler;
            paket.rutinler = rutinler;
            paket.kayitlar = kayitlar;
            string jsonMetin = JsonSerializer.Serialize(paket);
            File.WriteAllText("veri.json",jsonMetin);
        }


        

    }
}
