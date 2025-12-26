using Microsoft.AspNetCore.Mvc;

namespace BulutPastanesi.Controllers
{
    public class AccountController : Controller
    {
        // 1. Giriş Sayfasını Göster
        public IActionResult Login()
        {
            return View();
        }

        // 2. Giriş Yap Butonuna Basılınca Çalışan Kısım
        [HttpPost]
        public IActionResult Login(string k, string s)
        {
            // Şifre Kontrolü (Basitlik için sabit yazdık)
            if (k == "admin" && s == "1234")
            {
                // Doğruysa oturumu başlat
                HttpContext.Session.SetString("Yonetici", "GirisYapti");

                // Ürünler paneline yönlendir
                return RedirectToAction("Index", "Urunler");
            }

            // Yanlışsa hata mesajı ver
            ViewBag.Hata = "Kullanıcı adı veya şifre hatalı!";
            return View();
        }

        // 3. Çıkış Yapma İşlemi
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Oturumu sil
            return RedirectToAction("Index", "Home"); // Anasayfaya gönder
        }
    }
}