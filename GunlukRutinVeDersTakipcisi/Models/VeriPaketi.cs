using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunlukRutinVeDersTakipcisi.Models
{
    public class VeriPaketi
    {
        public List<Dersler> dersler { get; set; }=new List<Dersler>();
        public List<GorevRutin> rutinler { get; set; } = new List<GorevRutin>();
        public List<DersCalismaKaydi> kayitlar { get; set; } = new List<DersCalismaKaydi>();

        
        
    }
    
}
