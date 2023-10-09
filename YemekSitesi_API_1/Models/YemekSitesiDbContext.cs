using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;


namespace YemekSitesi_API_1.Models
{
    public class YemekSitesiDbContext : DbContext
    {

        public YemekSitesiDbContext(DbContextOptions<YemekSitesiDbContext> options) : base(options)
        {

        }

        // DbSet ve diğer özellikler...

        public DbSet<Adres_1> Adressler { get; set; } //buradaki isimler databasedeki isimleri
        public DbSet<Kategoriler_1> Kategoriler { get; set; }
        public DbSet<Kullanicilar_1> Kullanıcılar { get; set; }
        public DbSet<Menu_1> Menu { get; set; }
        public DbSet<Menuler_1> Menuler { get; set; }
        public DbSet<Restaurant_1> Restaurantlar { get; set; }
        public DbSet<Restaurant_Menu_1> Restaurant_Menu { get; set; }
        public DbSet<Restaurant_Urun_1> Restaurant_Urun { get; set; }
        public DbSet<Siparisler_1> Siparisler { get; set; }
        public DbSet<Urunler_1> Urunler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //PK TANIMLAMALARI

            modelBuilder.Entity<Adres_1>()
             .HasKey(a => a.adresID); // Adres_1 sınıfındaki adresID alanını PK (Primary Key) olarak belirtiyoruz

            modelBuilder.Entity<Kategoriler_1>()
                .HasKey(kt => kt.kategoriID);

            modelBuilder.Entity<Kullanicilar_1>()
                .HasKey(k => k.userID);

            modelBuilder.Entity<Menu_1>()
                .HasKey(m => m.menuID);

            modelBuilder.Entity<Menuler_1>()
               .HasKey(m => m.menulerID);

            modelBuilder.Entity<Restaurant_1>()
                .HasKey(r => r.restaurantID);

            modelBuilder.Entity<Restaurant_Menu_1>()
               .HasKey(rm => rm.restMenuID);

            modelBuilder.Entity<Restaurant_Urun_1>()
              .HasKey(ru => ru.restUrunID);

            modelBuilder.Entity<Siparisler_1>()
                .HasKey(s => s.siparisID);

            modelBuilder.Entity<Urunler_1>()
                .HasKey(u => u.urunID);




            // Yapılandırma ve ilişkilerin tanımlanması...


            // 1-1 İLİŞKİLER



            //ADRES-KULLANICILAR
            modelBuilder.Entity<Kullanicilar_1>()
                .HasOne(k => k.npAdres_1)
                .WithOne(a => a.npKullanicilar_1)
                .HasForeignKey<Kullanicilar_1>(k => k.adresId); // Kullanicilar_1 sınıfındaki adresId alanını FK (Foreign Key) olarak belirtiyoruz



            // 1-M İLİŞKİLER

            //KULLANICILAR-SİPARİSLER 


            modelBuilder.Entity<Kullanicilar_1>()
            .HasMany(k => k.npSiparisler_1)   // k Kullanicilar_1 classını temsil etmektedir
            .WithOne(s => s.npKullanicilar_1) // s burada Siparisler_1 temsil etmektedir
            .HasForeignKey(s => s.userID); // Siparisler_1 tablosunda UserID nin FK old bildirdik       


            //SİPARİSLER-RESTAURANT

            modelBuilder.Entity<Restaurant_1>()
            .HasMany(r => r.npSiparisler_1) // r Restaurant_1 classını temsil etmektedir
            .WithOne(s => s.npRestaurant_1) // s burada Siparisler_1 temsil etmektedir
            .HasForeignKey(s => s.restaurantID) //  Siparisler_1 tablosunda restaurantID nin FK old bildirdik
            .OnDelete(DeleteBehavior.Restrict);


            //URUNLER-KATEGORİ //02-08-2023

            modelBuilder.Entity<Kategoriler_1>()
                .HasMany(k => k.npUrunler_1)// k Kategoriler_1 classını temsil etmektedir
                .WithOne(u => u.npKategoriler_1) // u Urunler_1 classını temsil etmektedir
                .HasForeignKey(u => u.kategoriID);// Urunler_1 classındaki kategoriID nin FK old bildirdik



            //URUNLER-SİPARİSLER  //YENİ

            modelBuilder.Entity<Siparisler_1>()
                .HasOne(s => s.npUrunler_1) //s Siparislar_1 classını temsil etmekte
                .WithMany(u => u.npsiparisler_1) //u Urunler_1 classını temsil etmekte
                .HasForeignKey(s => s.urunID) // Siparislar_1 classındaki urunID nin FK old bildirdik //Yeni
                .OnDelete(DeleteBehavior.Restrict);


            //MENULER - SİPARİSLER //YENİ
            modelBuilder.Entity<Siparisler_1>()
                .HasOne(s => s.npMenuler_1) //s siparişler classının temsil ediyor // Siparisler_1 sınıfı için npMenuler_1 özelliğini tekil bir ilişki olarak tanımlar
                .WithMany(m => m.npsiparisler_1) //m Menuler_1 classını temsil ediyor // Menuler_1 sınıfı ile çoğa çok ilişki kurar ve npsiparisler_1 özelliğini kullanır
                .HasForeignKey(s => s.menulerID) //  Siparisler_1 classındaki menulerID nin FK od bildirdim // menulerID alanını yabancı anahtar olarak kullanır
                .OnDelete(DeleteBehavior.Restrict);// Silme işlemi yapıldığında DeleteBehavior.Restrict ile silmeyi kısıtlar
                                                   //.OnDelete(DeleteBehavior.Cascade); //otomatik silmesini istersek





            // M-M İLİŞKİLER

            //RESTAURANT-> RESTAURANT_MENU <-MENU

            // Restaurant_Menu_1 ile Menu_1 arasında (1-M)
            modelBuilder.Entity<Restaurant_Menu_1>()
                .HasOne(rm => rm.npMenu_1)
                .WithMany(m => m.npRestaurant_Menu_1) //m Menu_1 classını referans alır
                .HasForeignKey(rm => rm.menuID); // Menu_1 tablosu ile ilişki belirtildi

            // Restaurant_Menu_1 ile Restaurant_1 arasında (1-M)
            modelBuilder.Entity<Restaurant_Menu_1>()
                .HasOne(rm => rm.npRestaurant_1)
                .WithMany(r => r.npRestaurant_Menu_1) // r  Restaurant_Menu_1 ile Menu_1 arasında (1-M) classını referans alır
                .HasForeignKey(rm => rm.restaurantID);// Restaurant_1 tablosu ile ilişki belirtildi


            //RESTAURANT-> RESTAURANT_URUN <-URUN  // 03/08/2023

            // Restaurant_Urun_1 ile Urunler_1 arasında (1-M)
            modelBuilder.Entity<Restaurant_Urun_1>()
                .HasOne(ru => ru.npUrunler_1)
                .WithMany(u => u.npRestaurant_Urun_1) //u Urunler_1 classını referans alır
                .HasForeignKey(ru => ru.urunID); // Urunler_1 tablosu ile ilişki belirtildi

            // Restaurant_Urun_1 ile Restaurant_1 arasında (1-M)
            modelBuilder.Entity<Restaurant_Urun_1>()
                .HasOne(ru => ru.npRestaurant_1)
                .WithMany(r => r.npRestaurant_Urun_1) // r  Restaurant_Urun_1 ile Restaurant_1 arasında (1-M) classını referans alır
                .HasForeignKey(ru => ru.restaurantID);// Restaurant_1 tablosu ile ilişki belirtildi


            //MENU-> MENULER <- URUNLER 

            modelBuilder.Entity<Menuler_1>()
                .HasOne(mler => mler.npMenu_1) //mler Menuler_1 classını referans alır
                .WithMany(m => m.npMenuler_1) //m Menu_1 classını referans alır 
                .HasForeignKey(mler => mler.menuID) // Menuler_1 ve Menu_1  tablosu ile ilişki belirtildi (FK)
                 .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Menuler_1>()
                .HasOne(mler => mler.npUrunler_1)//mler Menuler_1 classını referans alır
                .WithMany(u => u.npMenuler_1) //u Urunler_1 classını referans alır 
                .HasForeignKey(mler => mler.urunID) // Menuler_1 ve Urunler_1 tablosu ile ilişki belirtildi (FK)
                 .OnDelete(DeleteBehavior.Restrict);



            //SEED DATA EKLEME


            modelBuilder.Entity<Adres_1>() //+
               .HasData(

                     new Adres_1 { adresID = 100, evAdresi = "No: 12, Cumhuriyet Caddesi, İstiklal Mahallesi, İstanbul, 34000", isAdresi = " No: 10, İş Merkezi Caddesi, Merkez Mahallesi, Ankara, 06500" },
                     new Adres_1 { adresID = 101, evAdresi = "No: 5, Atatürk Bulvarı, Kızılay Mahallesi, Ankara, 06000", isAdresi = "No: 15, İş Parkı Sokak, Finans Mahallesi, İstanbul, 34000" },
                     new Adres_1 { adresID = 102, evAdresi = "No: 21/A, Mimar Sinan Sokak, Konak Mahallesi, İzmir, 35000", isAdresi = "No: 7, Plaza Yolu, Ticaret Mahallesi, İzmir, 35000" },
                     new Adres_1 { adresID = 103, evAdresi = "No: 8, İstiklal Mahallesi, Osmangazi Sokak, Bursa, 16000", isAdresi = "No: 25/A, İşhanı Caddesi, Sanayi Mahallesi, Bursa, 16000" },
                     new Adres_1 { adresID = 104, evAdresi = "No: 3, Fatih Sultan Mehmet Caddesi, Lara Mahallesi, Antalya, 07000", isAdresi = "No: 4, Ofis Sokak, Rezidans Mahallesi, Antalya, 07000" },
                     new Adres_1 { adresID = 105, evAdresi = "No: 7, Mevlana  Caddesi, Emirler Mahallesi, Eskişehir, 03000", isAdresi = "No: 4, Paşa Sokak, Duran Mahallesi, Ankara, 01000" }

               );

            modelBuilder.Entity<Kullanicilar_1>() //+
                .HasData(
                      new Kullanicilar_1 { userID = 200, ad = "Ahmet", soyad = "Yılmaz", password = "Sifre12!", email = "ahmet.yilmaz @example.com", tel = "5551234567", role = "person", isactive = true, adresId = 100 },
                      new Kullanicilar_1 { userID = 201, ad = "Ayşe", soyad = "Kaya", password = "Gec3Tr!k", email = "ayse.kaya @example.com", tel = "5559876543", role = "person", isactive = true, adresId = 101 },
                      new Kullanicilar_1 { userID = 202, ad = "Mehmet", soyad = "Demir", password = "P@ssw0rd", email = "mehmet.demir @example.com", tel = "5552345678", role = "person", isactive = true, adresId = 102 },
                      new Kullanicilar_1 { userID = 203, ad = "Elif", soyad = "Öztürk", password = "9b1!O9by", email = "elif.ozturk @example.com", tel = "5558765432", role = "person", isactive = true, adresId = 103 },
                      new Kullanicilar_1 { userID = 204, ad = "Can", soyad = "Aksoy", password = "$tr0ngP@", email = "can.aksoy @example.com", tel = "5553456789", role = "person", isactive = true, adresId = 104 },
                      new Kullanicilar_1 { userID = 205, ad = "Hatice", soyad = "Düş", password = "Admin*01", email = "admin01@example.com", tel = "5553456789", role = "admin", isactive = true, adresId = 105 }
               );



            modelBuilder.Entity<Restaurant_1>()//+
                .HasData(
                  new Restaurant_1 { restaurantID = 400, restaurantAd = "Lezzet Durağı", restaurantAdres = "İstiklal Caddesi No: 123, İstanbul", restaurantTel = "5551234567", restaurantImg = "https://vegfusionblog.files.wordpress.com/2023/05/soya-etli-sote.png" },
                  new Restaurant_1 { restaurantID = 401, restaurantAd = "Tatlının Şahı", restaurantAdres = "Merkez Mahallesi, Atatürk Bulvarı No: 45, Ankara", restaurantTel = "5559876543", restaurantImg = "https://cdn.pixabay.com/photo/2016/08/12/16/01/cake-1589012_1280.jpg" },
                  new Restaurant_1 { restaurantID = 402, restaurantAd = "Deniz Köşesi", restaurantAdres = "Sahil Yolu Sokak No: 67, İzmir", restaurantTel = "5552345678", restaurantImg = "https://cdn.getiryemek.com/restaurants/1601032811170_1125x522.jpeg" },
                  new Restaurant_1 { restaurantID = 403, restaurantAd = "Mangal Keyfi", restaurantAdres = "Göztepe Mahallesi, İstiklal Caddesi No: 89, Bursa", restaurantTel = "5558765432", restaurantImg = "https://media.istockphoto.com/id/953028822/tr/foto%C4%9Fraf/izgara-sosis-ve-a%C3%A7%C4%B1k-bir-%C4%B1zgara-tabakta-sebze.webp?b=1&s=612x612&w=0&k=20&c=YgQexq06_E4AqasLUJNjRmrvB8tcnzso0_DKrSLDigY=" },
                  new Restaurant_1 { restaurantID = 404, restaurantAd = "Pidecim", restaurantAdres = "Meşrutiyet Mahallesi, Cumhuriyet Sokak No: 21, Antalya", restaurantTel = "5553456789", restaurantImg = "https://cdn.pixabay.com/photo/2018/04/21/21/50/food-3339651_640.jpg" },
                  new Restaurant_1 { restaurantID = 405, restaurantAd = "Ayten usta", restaurantAdres = "Yeşilova, 4016. Cad. No:10 BJ, 06796 Etimesgut/Ankara", restaurantTel = "5365434936", restaurantImg = "https://www.isvecgundemi.com/images/upload/61565937_2387274074628029_7825222120283045888_o_3.jpg" },
                  new Restaurant_1 { restaurantID = 406, restaurantAd = "Domino's Pizza", restaurantAdres = "1464. Cad. 1472 Sok. No:1/10 Elvankent, Ankara", restaurantTel = "2126359874", restaurantImg = "https://cdn.getiryemek.com/restaurants/1617109990692_1125x522.jpeg" },
                  new Restaurant_1 { restaurantID = 407, restaurantAd = "Papa John's Pizza", restaurantAdres = "1904 Cadde Tekgül Sitesi No :16, Ankara", restaurantTel = "2224569872", restaurantImg = "https://yiyegeze.com/wp-content/uploads/2020/01/images-3-2.jpg" },
                  new Restaurant_1 { restaurantID = 408, restaurantAd = "Little Caesars Pizza", restaurantAdres = "Ahmet Taner Kışlalı Mah. Park Cad. 2866 Sok. No:38/5 Çayyolu, Ankara", restaurantTel = "2145664412", restaurantImg = "https://www.littlecaesars.com.tr/CMSFiles/Product/LittleImage/tek-pizzalar.jpg" },
                  new Restaurant_1 { restaurantID = 409, restaurantAd = "Pizza Hut", restaurantAdres = "Yıldızevler Mah. 4. Cad. No:43/A Yıldız, Ankara", restaurantTel = "5463217896", restaurantImg = "https://media-cdn.tripadvisor.com/media/photo-s/20/9f/25/59/great-food.jpg" },
                  new Restaurant_1 { restaurantID = 410, restaurantAd = "Sbarro Pizza", restaurantAdres = "ERYAMAN MAH. DUMLUPINAR 30 AĞUSTOS CAD. NO:2-A, X Ankara", restaurantTel = "2125641236", restaurantImg = "https://images.deliveryhero.io/image/fd-tr/LH/qql7-hero.jpg?width=1600" },
                  new Restaurant_1 { restaurantID = 411, restaurantAd = "Pizza Lovers", restaurantAdres = "BALGAT MAH. ZİYABEY CAD. NO: 59 E ÇANKAYA/ ANKARA,", restaurantTel = "5369636514", restaurantImg = "https://images.deliveryhero.io/image/fd-tr/LH/k5fn-hero.jpg?width=1000" },
                  new Restaurant_1 { restaurantID = 412, restaurantAd = "Tadım Pizza", restaurantAdres = "Dikmen Cad. No: 440/B Dikmen, Ankara", restaurantTel = "5417896352", restaurantImg = "https://images.deliveryhero.io/image/fd-tr/LH/ghuo-hero.jpg?width=1000" },
                  new Restaurant_1 { restaurantID = 413, restaurantAd = "Pizza Bulls", restaurantAdres = "Turgut Özal Mah. 2154 Cad. No:11/B Blok 4 Yenimahalle – ANKARA", restaurantTel = "5423217744", restaurantImg = "https://images.deliveryhero.io/image/fd-tr/LH/jxms-hero.jpg?width=1000" },
                  new Restaurant_1 { restaurantID = 414, restaurantAd = "Başak Kır Pidesi", restaurantAdres = "Kentkoop Mah. Batıkent/Yenimahalle, ANKARA", restaurantTel = "2551478963", restaurantImg = "https://cdn.getiryemek.com/restaurants/1686741420103_1125x522.jpeg" }

                );

            modelBuilder.Entity<Kategoriler_1>()//+
                .HasData(
                   new Kategoriler_1 { kategoriID = 500, kategoriTuru = "Pide/Lahmacun", kategoriImg = "https://cdn.pixabay.com/photo/2021/05/07/08/46/ground-beef-6235546_1280.jpg" },
                   new Kategoriler_1 { kategoriID = 501, kategoriTuru = "Pasta/Tatlı", kategoriImg = "https://cdn.pixabay.com/photo/2022/07/21/14/13/baklava-7336361_1280.jpg" },
                   new Kategoriler_1 { kategoriID = 502, kategoriTuru = "Mangal / Barbekü", kategoriImg = "https://cdn.pixabay.com/photo/2015/05/25/16/14/burger-783551_1280.jpg" },
                   new Kategoriler_1 { kategoriID = 503, kategoriTuru = "Balık/Deniz Ürünleri", kategoriImg = "https://cdn.pixabay.com/photo/2014/11/05/15/57/salmon-518032_1280.jpg" },
                   new Kategoriler_1 { kategoriID = 504, kategoriTuru = "Türk Mutfağı/Sıcak Yemek", kategoriImg = "https://cdn.pixabay.com/photo/2017/07/19/15/17/bulgur-2519399_1280.jpg" },
                   new Kategoriler_1 { kategoriID = 505, kategoriTuru = "Asya Mutfağı", kategoriImg = "https://cdn.pixabay.com/photo/2021/01/01/15/31/sushi-balls-5878892_1280.jpg" },
                   new Kategoriler_1 { kategoriID = 506, kategoriTuru = "Kebap/Döner/Dürüm", kategoriImg = "https://cdn.pixabay.com/photo/2021/03/24/08/49/durum-6119590_1280.jpg" },
                   new Kategoriler_1 { kategoriID = 507, kategoriTuru = "Fırın/Börek", kategoriImg = "https://cdn.pixabay.com/photo/2017/10/18/16/23/bread-2864665_1280.jpg" },
                   new Kategoriler_1 { kategoriID = 508, kategoriTuru = "İçecekler", kategoriImg = "https://cdn.pixabay.com/photo/2017/05/11/19/49/iced-coffee-2305203_1280.jpg" },
                   new Kategoriler_1 { kategoriID = 509, kategoriTuru = "Tost/bistro/Sandviç", kategoriImg = "https://cdn.pixabay.com/photo/2018/07/14/21/30/club-sandwich-3538455_1280.jpg" },
                   new Kategoriler_1 { kategoriID = 510, kategoriTuru = "Yan Lezzetler/Çiğköfte", kategoriImg = "https://cdn.pixabay.com/photo/2018/10/01/18/20/raw-meat-3716962_1280.jpg" },
                   new Kategoriler_1 { kategoriID = 511, kategoriTuru = "Hamburger", kategoriImg = "https://cdn.pixabay.com/photo/2016/03/05/19/02/hamburger-1238246_1280.jpg" },
                   new Kategoriler_1 { kategoriID = 512, kategoriTuru = "Pizza/Makarna", kategoriImg = "https://cdn.pixabay.com/photo/2017/12/10/14/47/pizza-3010062_1280.jpg" },
                   new Kategoriler_1 { kategoriID = 513, kategoriTuru = "Tavuk", kategoriImg = "https://cdn.pixabay.com/photo/2022/05/17/04/57/broasted-chicken-7201660_1280.jpg" },
                   new Kategoriler_1 { kategoriID = 514, kategoriTuru = "Waffle/Dondurma", kategoriImg = "https://cdn.pixabay.com/photo/2017/06/17/16/20/waffles-2412628_1280.jpg" }
                );


            modelBuilder.Entity<Menu_1>() //+
               .HasData(
                 new Menu_1 { menuID = 700, menuAdi = "Kebap Ekonomik Menü_1", menuImg = "https://cdn.getiryemek.com/restaurants/1619394979049_1125x522.jpeg" },
                 new Menu_1 { menuID = 701, menuAdi = "Mix_1 Tatlı Menüsü", menuImg = "https://cdn.pixabay.com/photo/2020/03/11/20/10/dessert-4923228_640.jpg" },
                 new Menu_1 { menuID = 702, menuAdi = "Deniz_1", menuImg = "https://cdn.pixabay.com/photo/2023/05/17/15/58/shrimp-8000536_640.jpg" },
                 new Menu_1 { menuID = 703, menuAdi = "Mangal Karışık Menü_1", menuImg = "https://cdn.pixabay.com/photo/2017/02/25/15/23/barbecue-2098020_1280.jpg" },
                 new Menu_1 { menuID = 704, menuAdi = "Özel Pide Menüsü_1", menuImg = "https://cdn.pixabay.com/photo/2018/04/21/21/50/food-3339651_640.jpg" }

               );


            modelBuilder.Entity<Restaurant_Menu_1>() //MENU-RESTAURANT +
                .HasData(
                  new Restaurant_Menu_1 { restMenuID = 600, menuID = 704, restaurantID = 404 },
                  new Restaurant_Menu_1 { restMenuID = 601, menuID = 700, restaurantID = 400 },
                  new Restaurant_Menu_1 { restMenuID = 602, menuID = 703, restaurantID = 403 },
                  new Restaurant_Menu_1 { restMenuID = 603, menuID = 701, restaurantID = 401 },
                  new Restaurant_Menu_1 { restMenuID = 604, menuID = 702, restaurantID = 402 }
                );


            modelBuilder.Entity<Restaurant_Urun_1>() //MENU-RESTAURANT +
                .HasData(
                  new Restaurant_Urun_1 { restUrunID = 1000, restaurantID = 404, urunID = 903 },
                  new Restaurant_Urun_1 { restUrunID = 1001, restaurantID = 400, urunID = 900 },
                  new Restaurant_Urun_1 { restUrunID = 1002, restaurantID = 400, urunID = 903 },
                  new Restaurant_Urun_1 { restUrunID = 1003, restaurantID = 400, urunID = 904 },
                  new Restaurant_Urun_1 { restUrunID = 1004, restaurantID = 403, urunID = 914 },
                  new Restaurant_Urun_1 { restUrunID = 1005, restaurantID = 403, urunID = 916 },
                  new Restaurant_Urun_1 { restUrunID = 1006, restaurantID = 403, urunID = 905 },
                  new Restaurant_Urun_1 { restUrunID = 1007, restaurantID = 401, urunID = 901 },
                  new Restaurant_Urun_1 { restUrunID = 1008, restaurantID = 401, urunID = 906 },
                  new Restaurant_Urun_1 { restUrunID = 1009, restaurantID = 401, urunID = 908 },
                  new Restaurant_Urun_1 { restUrunID = 1010, restaurantID = 402, urunID = 909 },
                  new Restaurant_Urun_1 { restUrunID = 1011, restaurantID = 402, urunID = 911 },
                  new Restaurant_Urun_1 { restUrunID = 1012, restaurantID = 402, urunID = 912 },
                  new Restaurant_Urun_1 { restUrunID = 1013, restaurantID = 404, urunID = 917 },
                  new Restaurant_Urun_1 { restUrunID = 1014, restaurantID = 404, urunID = 918 },
                  new Restaurant_Urun_1 { restUrunID = 1015, restaurantID = 404, urunID = 919 }
                );


            modelBuilder.Entity<Menuler_1>() //MENU-URUNLER +
                .HasData(
                   new Menuler_1 { menulerID = 800, menuID = 700, urunID = 900 },
                   new Menuler_1 { menulerID = 801, menuID = 700, urunID = 903 },
                   new Menuler_1 { menulerID = 802, menuID = 700, urunID = 904 },
                   new Menuler_1 { menulerID = 803, menuID = 701, urunID = 901 },
                   new Menuler_1 { menulerID = 804, menuID = 701, urunID = 906 },
                   new Menuler_1 { menulerID = 805, menuID = 701, urunID = 908 },
                   new Menuler_1 { menulerID = 806, menuID = 702, urunID = 909 },
                   new Menuler_1 { menulerID = 807, menuID = 702, urunID = 911 },
                   new Menuler_1 { menulerID = 808, menuID = 702, urunID = 912 },
                   new Menuler_1 { menulerID = 809, menuID = 703, urunID = 914 },
                   new Menuler_1 { menulerID = 810, menuID = 703, urunID = 916 },
                   new Menuler_1 { menulerID = 811, menuID = 703, urunID = 905 },
                   new Menuler_1 { menulerID = 812, menuID = 704, urunID = 903 },
                   new Menuler_1 { menulerID = 813, menuID = 704, urunID = 919 },
                   new Menuler_1 { menulerID = 814, menuID = 704, urunID = 917 },
                   new Menuler_1 { menulerID = 815, menuID = 704, urunID = 918 }
                   );


            modelBuilder.Entity<Siparisler_1>()//+
               .HasData(
                   new Siparisler_1 { siparisID = 300, userID = 204, urunID = 900, menulerID = 800, restaurantID = 400, siparisTarih = new DateTime(2023, 06, 30, 10, 30, 0), siparisAdet = 2, siparisTutar = 111 },
                   new Siparisler_1 { siparisID = 301, userID = 202, urunID = 917, menulerID = 815, restaurantID = 401, siparisTarih = new DateTime(2022, 12, 15, 16, 45, 0), siparisAdet = 3, siparisTutar = 178 },
                   new Siparisler_1 { siparisID = 302, userID = 200, urunID = 911, menulerID = 807, restaurantID = 402, siparisTarih = new DateTime(2024, 03, 01, 08, 00, 0), siparisAdet = 1, siparisTutar = 200 },
                   new Siparisler_1 { siparisID = 303, userID = 203, urunID = 903, menulerID = 801, restaurantID = 403, siparisTarih = new DateTime(2023, 09, 22, 14, 20, 0), siparisAdet = 5, siparisTutar = 632 },
                   new Siparisler_1 { siparisID = 304, userID = 201, urunID = 903, menulerID = 812, restaurantID = 404, siparisTarih = new DateTime(2025, 07, 10, 21, 15, 0), siparisAdet = 2, siparisTutar = 99 }
               );


            modelBuilder.Entity<Urunler_1>() //+
                .HasData(
                  new Urunler_1 { urunID = 900, urunAdi = "Adana Kebap", urunFiyat = 100, kategoriID = 506, urunImg = "https://cdn.getiryemek.com/products/1625243947169_500x375.jpeg" }, //811
                  new Urunler_1 { urunID = 901, urunAdi = "Patates Kızartması", urunFiyat = 26, kategoriID = 510, urunImg = "https://cdn.getiryemek.com/products/1679877053375_500x375.jpeg" }, //812
                  new Urunler_1 { urunID = 902, urunAdi = "Cacık", urunFiyat = 25, kategoriID = 510, urunImg = "" }, //813
                  new Urunler_1 { urunID = 903, urunAdi = "Ayran", urunFiyat = 10, kategoriID = 508, urunImg = "https://cdn.getiryemek.com/products/1686202547513_500x375.jpeg" }, //814
                  new Urunler_1 { urunID = 904, urunAdi = "Semizotu Salatası", urunFiyat = 25, kategoriID = 510, urunImg = "" }, //815
                  new Urunler_1 { urunID = 905, urunAdi = "Baklava", urunFiyat = 15, kategoriID = 501, urunImg = "https://cdn.getiryemek.com/restaurants/1648114161397_1125x522.jpeg" }, //816
                  new Urunler_1 { urunID = 906, urunAdi = "Künefe(2 kişilik)", urunFiyat = 230, kategoriID = 501, urunImg = "https://cdn.getiryemek.com/restaurants/1643656790557_1125x522.jpeg" }, //817
                  new Urunler_1 { urunID = 907, urunAdi = "Fıstıklı Kadayıf(1kg)", urunFiyat = 240, kategoriID = 501, urunImg = "" }, //818
                  new Urunler_1 { urunID = 908, urunAdi = "Salep", urunFiyat = 16, kategoriID = 508, urunImg = "" }, //819
                  new Urunler_1 { urunID = 909, urunAdi = "Levrek Izgara", urunFiyat = 45, kategoriID = 503, urunImg = "" }, //820
                  new Urunler_1 { urunID = 910, urunAdi = "Midye Tava", urunFiyat = 32, kategoriID = 503, urunImg = "" }, //821
                  new Urunler_1 { urunID = 911, urunAdi = "Deniz Mahsulleri Salatası", urunFiyat = 39, kategoriID = 503, urunImg = "" }, //822
                  new Urunler_1 { urunID = 912, urunAdi = "Limonata", urunFiyat = 14, kategoriID = 508, urunImg = "https://cdn.getiryemek.com/products/1634137936931_500x375.jpeg" }, //823
                  new Urunler_1 { urunID = 913, urunAdi = "Kola", urunFiyat = 25, kategoriID = 508, urunImg = "" }, //824
                  new Urunler_1 { urunID = 914, urunAdi = "Urfa Kebap", urunFiyat = 40, kategoriID = 506, urunImg = "https://cdn.getiryemek.com/products/1625244160417_500x375.jpeg" }, //825
                  new Urunler_1 { urunID = 915, urunAdi = "Tavuk Şiş", urunFiyat = 35, kategoriID = 513, urunImg = "" }, //826
                  new Urunler_1 { urunID = 916, urunAdi = "Pirinç Pilavı", urunFiyat = 14, kategoriID = 510, urunImg = "https://media.istockphoto.com/id/1361129080/tr/foto%C4%9Fraf/traditional-delicious-turkish-food-turkish-style-rice-pilaf.webp?b=1&s=612x612&w=0&k=20&c=3ZDmbKiRRMxZnIR3JFT8oX1wAAhhyS4WsIICDESW6bY=" }, //827
                  new Urunler_1 { urunID = 917, urunAdi = "Gazoz", urunFiyat = 8, kategoriID = 508, urunImg = "" }, //828
                  new Urunler_1 { urunID = 918, urunAdi = "Ezme", urunFiyat = 12, kategoriID = 510, urunImg = "" }, //829
                  new Urunler_1 { urunID = 919, urunAdi = "Kaşarlı Pide", urunFiyat = 28, kategoriID = 500, urunImg = "https://cdn.getiryemek.com/products/1644567963410_500x375.jpeg" }, //830
                  new Urunler_1 { urunID = 920, urunAdi = "Sucuklu Pide", urunFiyat = 30, kategoriID = 500, urunImg = "https://cdn.getiryemek.com/products/1634741377114_500x375.jpeg" }, //831
                  new Urunler_1 { urunID = 921, urunAdi = "Lahmacun", urunFiyat = 22, kategoriID = 500, urunImg = "https://cdn.getiryemek.com/products/1674032276377_500x375.jpeg" }, //832
                  new Urunler_1 { urunID = 922, urunAdi = "Acılı Ezme", urunFiyat = 12, kategoriID = 510, urunImg = "" }, //833
                  new Urunler_1 { urunID = 923, urunAdi = "Kalamar", urunFiyat = 55, kategoriID = 503, urunImg = "https://cdn.pixabay.com/photo/2016/03/28/10/01/food-1285314_640.jpg" } //834,
                 );
        }


    }
}
