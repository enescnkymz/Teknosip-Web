# 🚀 Teknosip - DPÜ Teknopark & Öğrenci Kariyer Platformu

> **🌐 Canlı Yayın (Live Demo):** [Teknosip'i Ziyaret Etmek İçin Tıklayın](http://seninsiteninadresi.monsterweb.com.tr)

Teknosip, Dumlupınar Üniversitesi (DPÜ) öğrencileri ile Teknopark bünyesindeki teknoloji firmalarını, kurumları ve akademisyenleri tek bir ekosistemde buluşturan kapsamlı bir kariyer ve proje yönetim platformudur.

## 🎯 Projenin Amacı
Üniversite öğrencilerinin staj, iş ve proje bulma süreçlerini kolaylaştırmak; firmaların ise yetenekli gençlere doğrudan ulaşmasını sağlamak. Sistem, roller bazlı (Admin, Şirket, Öğrenci, Akademisyen, Kurum) özel paneller barındıran tam donanımlı bir ağ modelidir.

---

## ✨ Öne Çıkan Özellikler

* **Rol Bazlı Gelişmiş Kimlik Yönetimi :** Şirketler, Öğrenciler, Akademisyenler, Kurumlar ve Admin olmak üzere 5 farklı kullanıcı tipi.
  * Admin onayından geçmeyen şirket veya kurumlar sisteme dahil olamaz.
* **İlan ve Başvuru Modülü:**  Şirketler İş, Staj ve Proje kategorilerinde ilan açabilir.
  * Öğrenciler kendi yetenek ve CV'leriyle bu ilanlara başvurabilir.
  * Şirketler gelen başvuruları inceleyip onay/red durumlarını yönetebilir.
* **Mesajlaşma ve Bildirim Sistemi:** Ekosistemdeki paydaşların birbirleriyle gerçek zamanlı veya asenkron iletişim kurabilmesi için entegre mesajlaşma altyapısı.
* **Kapsamlı Profil Yönetimi:** Her kullanıcının kendi vitrinini düzenleyebildiği dinamik arayüz.

---

## 🛠️ Mimari ve Teknolojiler 

Bu proje, sektör standartlarındaki **Onion Architecture** ve **Clean Architecture** prensiplerine sadık kalınarak geliştirilmiştir. 

İş mantığı ve veri erişim süreçleri, performansı maksimize etmek için **CQRS** deseniyle ayrıştırılmıştır:

* **Backend:** C# / ASP.NET Core 8 MVC
* **Mimari Desenler:** Onion Architecture, CQRS , Repository Pattern
* **Mediator Uygulaması:** MediatR kütüphanesi
* **Veritabanı:** MS SQL Server
* **ORM (Command):** Entity Framework Core 
* **Mikro ORM (Query):** Dapper 
* **Kimlik Doğrulama:** ASP.NET Core Identity
* **Frontend :** HTML5, CSS3, Bootstrap 5, AJAX, JavaScript
* **Temalar:** 
  * *Public :* Arsha 
  * *Admin / Paneller:* Sneat 

---

## 📂 Katmanlı Mimari Yapısı

Proje bağımlılıkların içten dışa doğru ilerlediği modüler bir yapıya sahiptir:
1. **Core / Domain:** Entities ve Enum'lar.
2. **Application:** CQRS Handler'ları , DTO'lar ve Interfaceler .
3. **Infrastructure / Persistence:** Entity Framework DBContext, EF Repository'leri ve Dapper Query Repository .
4. **WebUI:** ASP.NET Core MVC Controller ve View yapıları.

---

## 📸 Ekran Görüntüleri

> **Not:** Sistem arayüzünden birkaç örnek:

<img width="1899" height="955" alt="Ekran görüntüsü 2026-06-03 113849" src="https://github.com/user-attachments/assets/1a5a8d8e-4b6d-4165-a379-e119f2653c32" />

 *Arayüz ve genel vitrin .*

---

<img width="1683" height="603" alt="Ekran görüntüsü 2026-06-03 114226" src="https://github.com/user-attachments/assets/28601d7d-c487-4668-9320-cc73ac56581a" />

*Şirketlerin ilanlarını ve başvurularını yönettiği yönetim paneli .*

---

<img width="1901" height="957" alt="Ekran görüntüsü 2026-06-03 113909" src="https://github.com/user-attachments/assets/b93eadd4-ef8e-441c-adb2-cdd50e0b6038" />

*Öğrencilerin proje ve iş ilanlarına başvurduğu dinamik sistem.*

---

<img width="2217" height="698" alt="Ekran görüntüsü 2026-06-02 200644" src="https://github.com/user-attachments/assets/046ec28b-c121-42a9-9cb1-c8990f79dc21" />

*Adminin yeni kayıtları onayladığı ekran.*

---
