using GunlukRutinVeDersTakipcisi.Managers;
using GunlukRutinVeDersTakipcisi.Models;
using GunlukRutinVeDersTakipcisi.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GunlukRutinVeDersTakipcisi.Models.VeriPaketi;

namespace GunlukRutinVeDersTakipcisi
{
    
    public partial class Form1 : Form
    {
        // Servislerimiz
        private readonly VeriTabaniServisi _vtServisi;
        private readonly TelegramServisi _telegramServisi;
        private readonly TakipKontrolYoneticisi _yonetici;

        // Kronometre Değişkenleri
        private int _gecenSaniye = 0;
       private bool _kronometreCalisiyor = false;

      
        public Form1()
        {
            InitializeComponent();
            // Servisleri başlatıyoruz
            _vtServisi = new VeriTabaniServisi();
            _telegramServisi = new TelegramServisi("8896499916:AAFlAuM8W0xUIaaob2mWoxGM3KCN5z3mUok",7060561143);
            _yonetici = new TakipKontrolYoneticisi(_vtServisi);
            tmrKronometre.Tick += tmrKronometre_Tick;

            ListeleriYenile();
        }
        // Form yüklendiğinde çalışacak metod
        private async void Form1_Load(object sender, EventArgs e)
        {
            ListeleriYenile();
        }

        // Tabloları veritabanından çekip güncelleyen yardımcı metod
        private void ListeleriYenile()
        {
           // Dersler Listesini Yükle
            dataGridView1.Rows.Clear();
            var dersler = _vtServisi.DersleriYukle();
            foreach (var ders in dersler)
            {
                dataGridView1.Rows.Add(ders.DersAdi, $"{ders.OnerilenCalismaSuresiHesapla()} saat");
            }
           
            // Rutinler Listesini Yükle
            dataGridView2.Rows.Clear();
            var rutinler = _vtServisi.RutinleriYukle();
            foreach (var rutin in rutinler)
            {
                string durumStr = rutin.Tamamlandimi ? "Tamamlandı" : "Bekliyor";
                dataGridView2.Rows.Add(rutin.Baslik, durumStr);
            }
       
            cmbCalisilanDers.DataSource = null;
            cmbCalisilanDers.DataSource = dersler;
            cmbCalisilanDers.DisplayMember = "DersAdi"; // Ekranda dersin ismi görünecek

        }

        private void button1_Click(object sender, EventArgs e)
        {


            string dersAdi = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(dersAdi))
            {
                MessageBox.Show("Lütfen bir ders adı girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ZorlukDerecesi secilenZorluk = ZorlukDerecesi.Orta;
            if (cmbZorluk.SelectedItem != null)
            {
                Enum.TryParse(cmbZorluk.SelectedItem.ToString(), out secilenZorluk);
            }

            var dersler = _vtServisi.DersleriYukle();
            var rutinler = _vtServisi.RutinleriYukle();
            var kayitlar = _vtServisi.KayitlariYukle();


            // MÜKERRER KAYIT KONTROLÜ
            // Aynı isimde ders var mı bakıyoruz
            bool dersVarMi = dersler.Any(d => d.DersAdi.Equals(dersAdi, StringComparison.OrdinalIgnoreCase));

            if (dersVarMi)
            {
                MessageBox.Show($"'{dersAdi}' adında bir ders zaten mevcut!", "Mükerrer Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            int yeniId = dersler.Count > 0 ? dersler.Max(d => d.DersId) + 1 : 1;

            var yeniDers = new Dersler
            {
                DersId = yeniId,
                DersAdi = dersAdi,
                Kredi=(int)numKredi.Value,
                Zorluk=secilenZorluk,
            };
            yeniDers.OnerilenCalismaSuresiHesapla();

            // 2. Yeni ders nesnesi oluşturup listeye ekliyoruz
            dersler.Add(yeniDers);

             // 3. Güncel listeyi tekrar JSON'a kaydediyoruz
             _vtServisi.VerileriKaydet(dersler, rutinler, kayitlar);

             textBox1.Clear();
             ListeleriYenile(); // Tabloyu ekranda yeniliyoruz*/
            MessageBox.Show($"{yeniDers.DersAdi} başarıyla eklendi!\nHaftalık Önerilen Çalışma Süresi: {yeniDers.OnerilenCalismaSuresiHesapla()} saat.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string seciliDersAdi = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                var dersler = _vtServisi.DersleriYukle();
                var rutinler = _vtServisi.RutinleriYukle();
                var kayitlar = _vtServisi.KayitlariYukle();

                // Seçilen dersi listeden kaldırıyoruz
                dersler.RemoveAll(d => d.DersAdi == seciliDersAdi);

                _vtServisi.VerileriKaydet(dersler, rutinler, kayitlar);
                ListeleriYenile();
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz dersi tablodan seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string rutinAdi = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(rutinAdi))
            {
                MessageBox.Show("Lütfen bir rutin adı girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dersler = _vtServisi.DersleriYukle();
            var rutinler = _vtServisi.RutinleriYukle();
            var kayitlar = _vtServisi.KayitlariYukle();

            // MÜKERRER KAYIT KONTROLÜ
            bool rutinVarMi = rutinler.Any(r => r.Baslik.Equals(rutinAdi, StringComparison.OrdinalIgnoreCase));

            if (rutinVarMi)
            {
                MessageBox.Show($"'{rutinAdi}' adında bir rutin zaten eklenmiş!", "Mükerrer Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            rutinler.Add(new GorevRutin { Baslik = rutinAdi, Tamamlandimi = false });

            _vtServisi.VerileriKaydet(dersler, rutinler, kayitlar);

            textBox2.Clear();
            ListeleriYenile();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                string seciliRutinBaslik = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();

                var dersler = _vtServisi.DersleriYukle();
                var rutinler = _vtServisi.RutinleriYukle();
                var kayitlar = _vtServisi.KayitlariYukle();

                rutinler.RemoveAll(r => r.Baslik == seciliRutinBaslik);

                _vtServisi.VerileriKaydet(dersler, rutinler, kayitlar);
                ListeleriYenile();
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz rutini tablodan seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBaslat_Click(object sender, EventArgs e)
        {
             if (!_kronometreCalisiyor)
             {
                 _kronometreCalisiyor = true;
                 tmrKronometre.Interval = 1000;
                 tmrKronometre.Start();
             }
            
        }

        private void btnDurdur_Click(object sender, EventArgs e)
        {
            if (_kronometreCalisiyor)
            {
                // 1. Ders seçilmiş mi kontrol et
                if (cmbCalisilanDers.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen önce çalıştığınız dersi seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _kronometreCalisiyor = false;
                tmrKronometre.Stop();

                // 2. Seçilen dersi al ve çalışma kaydını oluştur
                var secilenDers = (Dersler)cmbCalisilanDers.SelectedItem;
                int calisilanDakika = _gecenSaniye / 60;

                var yeniKayit = new DersCalismaKaydi
                {
                    DersId= secilenDers.DersId,
                    CalisilanSure = _gecenSaniye,
                    Tarih = DateTime.Now
                };

                // 3. Verileri çek ve yeni kaydı JSON'a işle
                var dersler = _vtServisi.DersleriYukle();
                var rutinler = _vtServisi.RutinleriYukle();
                var kayitlar = _vtServisi.KayitlariYukle();

                kayitlar.Add(yeniKayit);
                _vtServisi.VerileriKaydet(dersler, rutinler, kayitlar);

                MessageBox.Show($"Çalışma başarıyla kaydedildi!\n\nDers: {secilenDers.DersAdi}\nSüre: {calisilanDakika} dk ({_gecenSaniye} sn)", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 4. Sayaç ve ekranı sıfırla
                _gecenSaniye = 0;
                lblSure.Text = "00:00:00";
                ListeleriYenile();
            }
            
        }
        private void tmrKronometre_Tick(object sender, EventArgs e)
        {
             _gecenSaniye++;
             TimeSpan t = TimeSpan.FromSeconds(_gecenSaniye);
             lblSure.Text = t.ToString(@"hh\:mm\:ss");
        }
        private void lblSure_Click(object sender, EventArgs e)
        {

        }
        private async void btnTelegramRapor_Click(object sender, EventArgs e)
        {
            try
            {
                var dersler = _vtServisi.DersleriYukle();
                var rutinler = _vtServisi.RutinleriYukle();
                var kayitlar = _vtServisi.KayitlariYukle();

                // Bugüne ait çalışmaları filtreliyoruz
                var bugun = DateTime.Today;
                var bugunkuKayitlar = kayitlar.Where(k => k.Tarih.Date == bugun).ToList();

                // Toplam çalışılan süreyi hesaplıyoruz
                double toplamSaniye = bugunkuKayitlar.Sum(k => k.CalisilanSure);
                double toplamDakika = toplamSaniye / 60;

                // Mesaj başlığı ve genel durum
                string mesaj = $"📊 *Günlük Çalışma & Rutin Raporu*\n" +
                               $"📅 *Tarih:* {DateTime.Now:dd.MM.yyyy}\n\n" +
                               $"⏱ *Bugünkü Toplam Çalışma:* {toplamDakika} dakika\n\n";

                // Ders bazlı detay özetini hazırlıyoruz
                mesaj += "📚 *Ders Bazlı Çalışmalar:* \n";
                if (bugunkuKayitlar.Count > 0)
                {
                    var dersGruplari = bugunkuKayitlar.GroupBy(k => k.DersId);
                    foreach (var grup in dersGruplari)
                    {
                        var dersObj = dersler.FirstOrDefault(d => d.DersId == grup.Key);
                        string dersAdi = dersObj != null ? dersObj.DersAdi : "Genel Çalışma";

                        // O derse ait tutulan saniyeleri toplayıp dakikaya çeviriyoruz
                        double dersSaniye = grup.Sum(k => k.CalisilanSure);
                        int dersDakika = (int)Math.Round(dersSaniye / 60.0);

                        mesaj += $"• *{dersAdi}:* {dersDakika} dk\n";
                    }
                }
                else
                {
                    mesaj += "_Bugün henüz kronometre kaydı alınmadı._\n";
                }

                // Rutinler özetini hazırlıyoruz
                mesaj += "\n✅ *Günlük Rutin Durumları:* \n";
                if (rutinler.Count > 0)
                {
                    foreach (var rutin in rutinler)
                    {
                        string simge = rutin.Tamamlandimi ? "✅" : "⏳";
                        mesaj += $"{simge} {rutin.Baslik}\n";
                    }
                }
                else
                {
                    mesaj += "_Eklenmiş rutin bulunmuyor._\n";
                }

                mesaj += "\n_Gelişim pes etmeyenlerle gelir! İyi çalışmalar!_ 🚀";

                // Telegram servisi ile mesajı gönderiyoruz
                await _telegramServisi.MesajGonderAsync(mesaj);

                MessageBox.Show("Detaylı günlük rapor Telegram hesabınıza gönderildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Mesaj gönderilirken bir hata oluştu:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Tıklanan satır geçerli bir satır mı kontrol et (başlık satırına tıklandıysa işlem yapma)
            if (e.RowIndex >= 0)
            {
                var rutinler = _vtServisi.RutinleriYukle();

                // Çift tıklanan satırdaki rutini buluyoruz
                if (e.RowIndex < rutinler.Count)
                {
                    var secilenRutin = rutinler[e.RowIndex];

                    // Durumu tersine çeviriyoruz (Tamamlandıysa Bekliyor yap, Bekliyorsa Tamamlandı yap)
                    secilenRutin.Tamamlandimi = !secilenRutin.Tamamlandimi;

                    // Verileri güncelleyip JSON'a tekrar kaydediyoruz
                    var dersler = _vtServisi.DersleriYukle();
                    var kayitlar = _vtServisi.KayitlariYukle();
                    _vtServisi.VerileriKaydet(dersler, rutinler, kayitlar);

                    // Tabloyu ekranda yeniliyoruz
                    ListeleriYenile();

                    string yeniDurum = secilenRutin.Tamamlandimi ? "Tamamlandı ✅" : "Bekliyor ⏳";
                    MessageBox.Show($"'{secilenRutin.Baslik}' rutini durumu güncellendi: {yeniDurum}", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }





        }
    }
}
