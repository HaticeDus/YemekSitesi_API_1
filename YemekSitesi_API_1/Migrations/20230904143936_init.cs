using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YemekSitesi_API_1.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adressler",
                columns: table => new
                {
                    adresID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    evAdresi = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    isAdresi = table.Column<string>(type: "nvarchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adressler", x => x.adresID);
                });

            migrationBuilder.CreateTable(
                name: "Kategoriler",
                columns: table => new
                {
                    kategoriID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    kategoriTuru = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    kategoriImg = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriler", x => x.kategoriID);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    menuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    menuAdi = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    menuImg = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.menuID);
                });

            migrationBuilder.CreateTable(
                name: "Kullanıcılar",
                columns: table => new
                {
                    userID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ad = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    soyad = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(8)", nullable: false),
                    tel = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    isactive = table.Column<bool>(type: "bit", nullable: false),
                    adresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanıcılar", x => x.userID);
                    table.ForeignKey(
                        name: "FK_Kullanıcılar_Adressler_adresId",
                        column: x => x.adresId,
                        principalTable: "Adressler",
                        principalColumn: "adresID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restaurantlar",
                columns: table => new
                {
                    restaurantID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    restaurantAd = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    restaurantAdres = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    restaurantTel = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    restaurantImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kategoriler_1kategoriID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurantlar", x => x.restaurantID);
                    table.ForeignKey(
                        name: "FK_Restaurantlar_Kategoriler_Kategoriler_1kategoriID",
                        column: x => x.Kategoriler_1kategoriID,
                        principalTable: "Kategoriler",
                        principalColumn: "kategoriID");
                });

            migrationBuilder.CreateTable(
                name: "Urunler",
                columns: table => new
                {
                    urunID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    kategoriID = table.Column<int>(type: "int", nullable: false),
                    urunAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    urunFiyat = table.Column<int>(type: "int", nullable: false),
                    urunImg = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urunler", x => x.urunID);
                    table.ForeignKey(
                        name: "FK_Urunler_Kategoriler_kategoriID",
                        column: x => x.kategoriID,
                        principalTable: "Kategoriler",
                        principalColumn: "kategoriID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restaurant_Menu",
                columns: table => new
                {
                    restMenuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    restaurantID = table.Column<int>(type: "int", nullable: false),
                    menuID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurant_Menu", x => x.restMenuID);
                    table.ForeignKey(
                        name: "FK_Restaurant_Menu_Menu_menuID",
                        column: x => x.menuID,
                        principalTable: "Menu",
                        principalColumn: "menuID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Restaurant_Menu_Restaurantlar_restaurantID",
                        column: x => x.restaurantID,
                        principalTable: "Restaurantlar",
                        principalColumn: "restaurantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Menuler",
                columns: table => new
                {
                    menulerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    menuID = table.Column<int>(type: "int", nullable: false),
                    urunID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menuler", x => x.menulerID);
                    table.ForeignKey(
                        name: "FK_Menuler_Menu_menuID",
                        column: x => x.menuID,
                        principalTable: "Menu",
                        principalColumn: "menuID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Menuler_Urunler_urunID",
                        column: x => x.urunID,
                        principalTable: "Urunler",
                        principalColumn: "urunID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Restaurant_Urun",
                columns: table => new
                {
                    restUrunID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    restaurantID = table.Column<int>(type: "int", nullable: false),
                    urunID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurant_Urun", x => x.restUrunID);
                    table.ForeignKey(
                        name: "FK_Restaurant_Urun_Restaurantlar_restaurantID",
                        column: x => x.restaurantID,
                        principalTable: "Restaurantlar",
                        principalColumn: "restaurantID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Restaurant_Urun_Urunler_urunID",
                        column: x => x.urunID,
                        principalTable: "Urunler",
                        principalColumn: "urunID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Siparisler",
                columns: table => new
                {
                    siparisID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: false),
                    urunID = table.Column<int>(type: "int", nullable: false),
                    menulerID = table.Column<int>(type: "int", nullable: false),
                    restaurantID = table.Column<int>(type: "int", nullable: false),
                    siparisTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    siparisAdet = table.Column<int>(type: "int", nullable: false),
                    siparisTutar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siparisler", x => x.siparisID);
                    table.ForeignKey(
                        name: "FK_Siparisler_Kullanıcılar_userID",
                        column: x => x.userID,
                        principalTable: "Kullanıcılar",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Siparisler_Menuler_menulerID",
                        column: x => x.menulerID,
                        principalTable: "Menuler",
                        principalColumn: "menulerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Siparisler_Restaurantlar_restaurantID",
                        column: x => x.restaurantID,
                        principalTable: "Restaurantlar",
                        principalColumn: "restaurantID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Siparisler_Urunler_urunID",
                        column: x => x.urunID,
                        principalTable: "Urunler",
                        principalColumn: "urunID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Adressler",
                columns: new[] { "adresID", "evAdresi", "isAdresi" },
                values: new object[,]
                {
                    { 100, "No: 12, Cumhuriyet Caddesi, İstiklal Mahallesi, İstanbul, 34000", " No: 10, İş Merkezi Caddesi, Merkez Mahallesi, Ankara, 06500" },
                    { 101, "No: 5, Atatürk Bulvarı, Kızılay Mahallesi, Ankara, 06000", "No: 15, İş Parkı Sokak, Finans Mahallesi, İstanbul, 34000" },
                    { 102, "No: 21/A, Mimar Sinan Sokak, Konak Mahallesi, İzmir, 35000", "No: 7, Plaza Yolu, Ticaret Mahallesi, İzmir, 35000" },
                    { 103, "No: 8, İstiklal Mahallesi, Osmangazi Sokak, Bursa, 16000", "No: 25/A, İşhanı Caddesi, Sanayi Mahallesi, Bursa, 16000" },
                    { 104, "No: 3, Fatih Sultan Mehmet Caddesi, Lara Mahallesi, Antalya, 07000", "No: 4, Ofis Sokak, Rezidans Mahallesi, Antalya, 07000" },
                    { 105, "No: 7, Mevlana  Caddesi, Emirler Mahallesi, Eskişehir, 03000", "No: 4, Paşa Sokak, Duran Mahallesi, Ankara, 01000" }
                });

            migrationBuilder.InsertData(
                table: "Kategoriler",
                columns: new[] { "kategoriID", "kategoriImg", "kategoriTuru" },
                values: new object[,]
                {
                    { 500, "https://cdn.pixabay.com/photo/2021/05/07/08/46/ground-beef-6235546_1280.jpg", "Pide/Lahmacun" },
                    { 501, "https://cdn.pixabay.com/photo/2022/07/21/14/13/baklava-7336361_1280.jpg", "Pasta/Tatlı" },
                    { 502, "https://cdn.pixabay.com/photo/2015/05/25/16/14/burger-783551_1280.jpg", "Mangal / Barbekü" },
                    { 503, "https://cdn.pixabay.com/photo/2014/11/05/15/57/salmon-518032_1280.jpg", "Balık/Deniz Ürünleri" },
                    { 504, "https://cdn.pixabay.com/photo/2017/07/19/15/17/bulgur-2519399_1280.jpg", "Türk Mutfağı/Sıcak Yemek" },
                    { 505, "https://cdn.pixabay.com/photo/2021/01/01/15/31/sushi-balls-5878892_1280.jpg", "Asya Mutfağı" },
                    { 506, "https://cdn.pixabay.com/photo/2021/03/24/08/49/durum-6119590_1280.jpg", "Kebap/Döner/Dürüm" },
                    { 507, "https://cdn.pixabay.com/photo/2017/10/18/16/23/bread-2864665_1280.jpg", "Fırın/Börek" },
                    { 508, "https://cdn.pixabay.com/photo/2017/05/11/19/49/iced-coffee-2305203_1280.jpg", "İçecekler" },
                    { 509, "https://cdn.pixabay.com/photo/2018/07/14/21/30/club-sandwich-3538455_1280.jpg", "Tost/bistro/Sandviç" },
                    { 510, "https://cdn.pixabay.com/photo/2018/10/01/18/20/raw-meat-3716962_1280.jpg", "Yan Lezzetler/Çiğköfte" },
                    { 511, "https://cdn.pixabay.com/photo/2016/03/05/19/02/hamburger-1238246_1280.jpg", "Hamburger" },
                    { 512, "https://cdn.pixabay.com/photo/2017/12/10/14/47/pizza-3010062_1280.jpg", "Pizza/Makarna" },
                    { 513, "https://cdn.pixabay.com/photo/2022/05/17/04/57/broasted-chicken-7201660_1280.jpg", "Tavuk" },
                    { 514, "https://cdn.pixabay.com/photo/2017/06/17/16/20/waffles-2412628_1280.jpg", "Waffle/Dondurma" }
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "menuID", "menuAdi", "menuImg" },
                values: new object[,]
                {
                    { 700, "Kebap Ekonomik Menü_1", "https://cdn.getiryemek.com/restaurants/1619394979049_1125x522.jpeg" },
                    { 701, "Mix_1 Tatlı Menüsü", "https://cdn.pixabay.com/photo/2020/03/11/20/10/dessert-4923228_640.jpg" },
                    { 702, "Deniz_1", "https://cdn.pixabay.com/photo/2023/05/17/15/58/shrimp-8000536_640.jpg" },
                    { 703, "Mangal Karışık Menü_1", "https://cdn.pixabay.com/photo/2017/02/25/15/23/barbecue-2098020_1280.jpg" },
                    { 704, "Özel Pide Menüsü_1", "https://cdn.pixabay.com/photo/2018/04/21/21/50/food-3339651_640.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Restaurantlar",
                columns: new[] { "restaurantID", "Kategoriler_1kategoriID", "restaurantAd", "restaurantAdres", "restaurantImg", "restaurantTel" },
                values: new object[,]
                {
                    { 400, null, "Lezzet Durağı", "İstiklal Caddesi No: 123, İstanbul", "https://vegfusionblog.files.wordpress.com/2023/05/soya-etli-sote.png", "5551234567" },
                    { 401, null, "Tatlının Şahı", "Merkez Mahallesi, Atatürk Bulvarı No: 45, Ankara", "https://cdn.pixabay.com/photo/2016/08/12/16/01/cake-1589012_1280.jpg", "5559876543" },
                    { 402, null, "Deniz Köşesi", "Sahil Yolu Sokak No: 67, İzmir", "https://cdn.getiryemek.com/restaurants/1601032811170_1125x522.jpeg", "5552345678" },
                    { 403, null, "Mangal Keyfi", "Göztepe Mahallesi, İstiklal Caddesi No: 89, Bursa", "https://media.istockphoto.com/id/953028822/tr/foto%C4%9Fraf/izgara-sosis-ve-a%C3%A7%C4%B1k-bir-%C4%B1zgara-tabakta-sebze.webp?b=1&s=612x612&w=0&k=20&c=YgQexq06_E4AqasLUJNjRmrvB8tcnzso0_DKrSLDigY=", "5558765432" },
                    { 404, null, "Pidecim", "Meşrutiyet Mahallesi, Cumhuriyet Sokak No: 21, Antalya", "https://cdn.pixabay.com/photo/2018/04/21/21/50/food-3339651_640.jpg", "5553456789" },
                    { 405, null, "Ayten usta", "Yeşilova, 4016. Cad. No:10 BJ, 06796 Etimesgut/Ankara", "https://www.isvecgundemi.com/images/upload/61565937_2387274074628029_7825222120283045888_o_3.jpg", "5365434936" },
                    { 406, null, "Domino's Pizza", "1464. Cad. 1472 Sok. No:1/10 Elvankent, Ankara", "https://cdn.getiryemek.com/restaurants/1617109990692_1125x522.jpeg", "2126359874" },
                    { 407, null, "Papa John's Pizza", "1904 Cadde Tekgül Sitesi No :16, Ankara", "https://yiyegeze.com/wp-content/uploads/2020/01/images-3-2.jpg", "2224569872" },
                    { 408, null, "Little Caesars Pizza", "Ahmet Taner Kışlalı Mah. Park Cad. 2866 Sok. No:38/5 Çayyolu, Ankara", "https://www.littlecaesars.com.tr/CMSFiles/Product/LittleImage/tek-pizzalar.jpg", "2145664412" },
                    { 409, null, "Pizza Hut", "Yıldızevler Mah. 4. Cad. No:43/A Yıldız, Ankara", "https://media-cdn.tripadvisor.com/media/photo-s/20/9f/25/59/great-food.jpg", "5463217896" },
                    { 410, null, "Sbarro Pizza", "ERYAMAN MAH. DUMLUPINAR 30 AĞUSTOS CAD. NO:2-A, X Ankara", "https://images.deliveryhero.io/image/fd-tr/LH/qql7-hero.jpg?width=1600", "2125641236" },
                    { 411, null, "Pizza Lovers", "BALGAT MAH. ZİYABEY CAD. NO: 59 E ÇANKAYA/ ANKARA,", "https://images.deliveryhero.io/image/fd-tr/LH/k5fn-hero.jpg?width=1000", "5369636514" },
                    { 412, null, "Tadım Pizza", "Dikmen Cad. No: 440/B Dikmen, Ankara", "https://images.deliveryhero.io/image/fd-tr/LH/ghuo-hero.jpg?width=1000", "5417896352" },
                    { 413, null, "Pizza Bulls", "Turgut Özal Mah. 2154 Cad. No:11/B Blok 4 Yenimahalle – ANKARA", "https://images.deliveryhero.io/image/fd-tr/LH/jxms-hero.jpg?width=1000", "5423217744" },
                    { 414, null, "Başak Kır Pidesi", "Kentkoop Mah. Batıkent/Yenimahalle, ANKARA", "https://cdn.getiryemek.com/restaurants/1686741420103_1125x522.jpeg", "2551478963" }
                });

            migrationBuilder.InsertData(
                table: "Kullanıcılar",
                columns: new[] { "userID", "ad", "adresId", "email", "isactive", "password", "role", "soyad", "tel" },
                values: new object[,]
                {
                    { 200, "Ahmet", 100, "ahmet.yilmaz @example.com", true, "Sifre12!", "person", "Yılmaz", "5551234567" },
                    { 201, "Ayşe", 101, "ayse.kaya @example.com", true, "Gec3Tr!k", "person", "Kaya", "5559876543" },
                    { 202, "Mehmet", 102, "mehmet.demir @example.com", true, "P@ssw0rd", "person", "Demir", "5552345678" },
                    { 203, "Elif", 103, "elif.ozturk @example.com", true, "9b1!O9by", "person", "Öztürk", "5558765432" },
                    { 204, "Can", 104, "can.aksoy @example.com", true, "$tr0ngP@", "person", "Aksoy", "5553456789" },
                    { 205, "Hatice", 105, "admin01@example.com", true, "Admin*01", "admin", "Düş", "5553456789" }
                });

            migrationBuilder.InsertData(
                table: "Restaurant_Menu",
                columns: new[] { "restMenuID", "menuID", "restaurantID" },
                values: new object[,]
                {
                    { 600, 704, 404 },
                    { 601, 700, 400 },
                    { 602, 703, 403 },
                    { 603, 701, 401 },
                    { 604, 702, 402 }
                });

            migrationBuilder.InsertData(
                table: "Urunler",
                columns: new[] { "urunID", "kategoriID", "urunAdi", "urunFiyat", "urunImg" },
                values: new object[,]
                {
                    { 900, 506, "Adana Kebap", 100, "https://cdn.getiryemek.com/products/1625243947169_500x375.jpeg" },
                    { 901, 510, "Patates Kızartması", 26, "https://cdn.getiryemek.com/products/1679877053375_500x375.jpeg" },
                    { 902, 510, "Cacık", 25, "" },
                    { 903, 508, "Ayran", 10, "https://cdn.getiryemek.com/products/1686202547513_500x375.jpeg" },
                    { 904, 510, "Semizotu Salatası", 25, "" },
                    { 905, 501, "Baklava", 15, "https://cdn.getiryemek.com/restaurants/1648114161397_1125x522.jpeg" },
                    { 906, 501, "Künefe(2 kişilik)", 230, "https://cdn.getiryemek.com/restaurants/1643656790557_1125x522.jpeg" },
                    { 907, 501, "Fıstıklı Kadayıf(1kg)", 240, "" },
                    { 908, 508, "Salep", 16, "" },
                    { 909, 503, "Levrek Izgara", 45, "" },
                    { 910, 503, "Midye Tava", 32, "" },
                    { 911, 503, "Deniz Mahsulleri Salatası", 39, "" },
                    { 912, 508, "Limonata", 14, "https://cdn.getiryemek.com/products/1634137936931_500x375.jpeg" },
                    { 913, 508, "Kola", 25, "" },
                    { 914, 506, "Urfa Kebap", 40, "https://cdn.getiryemek.com/products/1625244160417_500x375.jpeg" },
                    { 915, 513, "Tavuk Şiş", 35, "" },
                    { 916, 510, "Pirinç Pilavı", 14, "https://media.istockphoto.com/id/1361129080/tr/foto%C4%9Fraf/traditional-delicious-turkish-food-turkish-style-rice-pilaf.webp?b=1&s=612x612&w=0&k=20&c=3ZDmbKiRRMxZnIR3JFT8oX1wAAhhyS4WsIICDESW6bY=" },
                    { 917, 508, "Gazoz", 8, "" },
                    { 918, 510, "Ezme", 12, "" },
                    { 919, 500, "Kaşarlı Pide", 28, "https://cdn.getiryemek.com/products/1644567963410_500x375.jpeg" },
                    { 920, 500, "Sucuklu Pide", 30, "https://cdn.getiryemek.com/products/1634741377114_500x375.jpeg" },
                    { 921, 500, "Lahmacun", 22, "https://cdn.getiryemek.com/products/1674032276377_500x375.jpeg" },
                    { 922, 510, "Acılı Ezme", 12, "" },
                    { 923, 503, "Kalamar", 55, "https://cdn.pixabay.com/photo/2016/03/28/10/01/food-1285314_640.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Menuler",
                columns: new[] { "menulerID", "menuID", "urunID" },
                values: new object[,]
                {
                    { 800, 700, 900 },
                    { 801, 700, 903 },
                    { 802, 700, 904 },
                    { 803, 701, 901 },
                    { 804, 701, 906 },
                    { 805, 701, 908 },
                    { 806, 702, 909 },
                    { 807, 702, 911 },
                    { 808, 702, 912 },
                    { 809, 703, 914 },
                    { 810, 703, 916 },
                    { 811, 703, 905 },
                    { 812, 704, 903 },
                    { 813, 704, 919 },
                    { 814, 704, 917 },
                    { 815, 704, 918 }
                });

            migrationBuilder.InsertData(
                table: "Restaurant_Urun",
                columns: new[] { "restUrunID", "restaurantID", "urunID" },
                values: new object[,]
                {
                    { 1000, 404, 903 },
                    { 1001, 400, 900 },
                    { 1002, 400, 903 },
                    { 1003, 400, 904 },
                    { 1004, 403, 914 },
                    { 1005, 403, 916 },
                    { 1006, 403, 905 },
                    { 1007, 401, 901 },
                    { 1008, 401, 906 },
                    { 1009, 401, 908 },
                    { 1010, 402, 909 },
                    { 1011, 402, 911 },
                    { 1012, 402, 912 },
                    { 1013, 404, 917 },
                    { 1014, 404, 918 },
                    { 1015, 404, 919 }
                });

            migrationBuilder.InsertData(
                table: "Siparisler",
                columns: new[] { "siparisID", "menulerID", "restaurantID", "siparisAdet", "siparisTarih", "siparisTutar", "urunID", "userID" },
                values: new object[,]
                {
                    { 300, 800, 400, 2, new DateTime(2023, 6, 30, 10, 30, 0, 0, DateTimeKind.Unspecified), 111, 900, 204 },
                    { 301, 815, 401, 3, new DateTime(2022, 12, 15, 16, 45, 0, 0, DateTimeKind.Unspecified), 178, 917, 202 },
                    { 302, 807, 402, 1, new DateTime(2024, 3, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), 200, 911, 200 },
                    { 303, 801, 403, 5, new DateTime(2023, 9, 22, 14, 20, 0, 0, DateTimeKind.Unspecified), 632, 903, 203 },
                    { 304, 812, 404, 2, new DateTime(2025, 7, 10, 21, 15, 0, 0, DateTimeKind.Unspecified), 99, 903, 201 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kullanıcılar_adresId",
                table: "Kullanıcılar",
                column: "adresId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menuler_menuID",
                table: "Menuler",
                column: "menuID");

            migrationBuilder.CreateIndex(
                name: "IX_Menuler_urunID",
                table: "Menuler",
                column: "urunID");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_Menu_menuID",
                table: "Restaurant_Menu",
                column: "menuID");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_Menu_restaurantID",
                table: "Restaurant_Menu",
                column: "restaurantID");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_Urun_restaurantID",
                table: "Restaurant_Urun",
                column: "restaurantID");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_Urun_urunID",
                table: "Restaurant_Urun",
                column: "urunID");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurantlar_Kategoriler_1kategoriID",
                table: "Restaurantlar",
                column: "Kategoriler_1kategoriID");

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_menulerID",
                table: "Siparisler",
                column: "menulerID");

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_restaurantID",
                table: "Siparisler",
                column: "restaurantID");

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_urunID",
                table: "Siparisler",
                column: "urunID");

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_userID",
                table: "Siparisler",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_kategoriID",
                table: "Urunler",
                column: "kategoriID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Restaurant_Menu");

            migrationBuilder.DropTable(
                name: "Restaurant_Urun");

            migrationBuilder.DropTable(
                name: "Siparisler");

            migrationBuilder.DropTable(
                name: "Kullanıcılar");

            migrationBuilder.DropTable(
                name: "Menuler");

            migrationBuilder.DropTable(
                name: "Restaurantlar");

            migrationBuilder.DropTable(
                name: "Adressler");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Urunler");

            migrationBuilder.DropTable(
                name: "Kategoriler");
        }
    }
}
