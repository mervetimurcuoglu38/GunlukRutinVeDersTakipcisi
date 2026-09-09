using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunlukRutinVeDersTakipcisi.Models
{
    public class GorevRutin
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public bool Tamamlandimi { get; set; }
        public bool BildirimGonderildimi { get; set; }
        public TimeSpan PlanlananSaat { get; set; }
        public DateTime Tarih { get; set; } 
        public bool DurumGuncelleme()
        {
            Tamamlandimi =!Tamamlandimi;
            return Tamamlandimi; 
        }

    }
}
