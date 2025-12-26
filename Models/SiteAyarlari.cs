using System.ComponentModel.DataAnnotations;

namespace BulutPastanesi.Models
{
    public class SiteAyarlari
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Firma Adı")]
        public string? FirmaAdi { get; set; }

        [Display(Name = "Telefon (WhatsApp)")]
        public string? Telefon { get; set; }

        [Display(Name = "E-Posta")]
        public string? Email { get; set; }

        [Display(Name = "Adres")]
        public string? Adres { get; set; }

        [Display(Name = "Hakkımızda Yazısı")]
        public string? Hakkimizda { get; set; }
    }
}