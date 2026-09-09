using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunlukRutinVeDersTakipcisi.Models
{
    public class DersCalismaKaydi
    {
        public int Id { get; set; }
        public int DersId { get; set; }
        public DateTime Tarih { get; set; }
        public double CalisilanSure { get; set; }
    }
}
