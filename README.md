# 🎯 Günlük Rutin ve Ders Takipçisi (Daily Routine & Study Tracker)

Bu proje; kişisel çalışma disiplinini artırmak, ders çalışma sürelerini takip etmek ve günlük rutinleri yönetmek amacıyla geliştirilmiş **C# Windows Forms** tabanlı bir masaüstü uygulamasıdır. Katmanlı mimari yapısı ve **Telegram Bot API** entegrasyonu sayesinde kullanıcıya anlık bildirim ve raporlama imkanı sunar.

---

## 🛠️ Teknolojiler ve Mimari

* **Dil & Çerçeve:** C# | .NET Framework / .NET Desktop
* **Arayüz (UI):** Windows Forms
* **Veri Depolama:** Lightweight JSON (`System.Text.Json`)
* **Dış Servis Entegrasyonu:** Telegram Bot API (`Telegram.Bot` / Custom Async HTTP Client)
* **Sorgulama Mantığı:** LINQ (Language Integrated Query)

### 🏗️ Proje Mimarisi (Layered Architecture)
Proje, kodun okunabilirliğini ve sürdürülebilirliğini artırmak adına katmanlı yapıda geliştirilmiştir:
* **UI Katmanı (`Form1.cs`):** Kullanıcı etkileşimlerini ve arayüz bileşenlerini yönetir.
* **Business & Service Katmanı (`VeriTabaniServisi`, `TelegramServisi`):** İş mantığını, veri işleme süreçlerini ve dış API iletişimlerini yürütür.
* **Manager Katmanı (`TakipKontrolYoneticisi`, `SonucIstatistikleri`):** Verilerin LINQ ile sorgulanması, filtrelenmesi ve istatistiksel analizlerinden sorumludur.
* **Model Katmanı (`Models`):** Uygulama içinde kullanılan veri yapılarını kapsar.

---

## ✨ Öne Çıkan Özellikler

* **Ders & Süre Takibi:** Seçilen dersler için canlı kronometre tabanlı çalışma süresi kaydı.
* **Rutin ve Görev Yönetimi:** Tamamlanan veya bekleyen günlük alışkanlıkların takibi.
* **Mükerrer Kayıt Engeli:** Data validation mantığı sayesinde aynı isimde ders veya rutinin tekrar eklenmesini önleme (`LINQ Any()`, `OrdinalIgnoreCase`).
* **Otomatik Telegram Raporlama:** Günlük çalışma performansını ve rutin özetlerini tek tıkla Telegram sohbetine bildirim olarak gönderme.
* **Geleceğe Hazır İstatistik Servisi:** Gelişmiş LINQ metotlarıyla ortalama süre, toplam çalışma ve kalan görev analiz altyapısı.

---

## 🚀 Kurulum ve Çalıştırma
 1. Projeyi bilgisayarınıza klonlayın:
   ```bash
   git clone [https://github.com/mervetimurcuoglu38/GunlukRutinVeDersTakipcisi.git](https://github.com/mervetimurcuoglu38/GunlukRutinVeDersTakipcisi.git)
 2.  Visual Studio ile .slnx / .sln dosyasını açın.

 3.TelegramServisi.cs dosyasındaki Telegram Bot Token alanına kendi Bot Token bilgilerinizi girin.

 4.Projeyi derleyin ve çalıştırın (F5).
   
   
1. Projeyi bilgisayarınıza klonlayın:
   ```bash
   git clone [https://github.com/mervetimurcuoglu38/GunlukRutinVeDersTakipcisi.git](https://github.com/mervetimurcuoglu38/GunlukRutinVeDersTakipcisi.git)
