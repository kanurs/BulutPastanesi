using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulutPastanesi.Models
{
    public class Urun
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Ürün Adı")]
        [Required]
        public string Ad { get; set; } = string.Empty;

        [Display(Name = "Açıklama")]
        public string? Aciklama { get; set; } // Boş olabilir

        [Display(Name = "Fiyat")]
        public decimal Fiyat { get; set; }

        [Display(Name = "Resim URL")]
        public string? ResimUrl { get; set; } // Boş olabilir

        [Display(Name = "Anasayfada Göster")]
        public bool VitrinDurumu { get; set; }

        [Display(Name = "Kategori")]
        public int KategoriId { get; set; }

        [ForeignKey("KategoriId")]
        public virtual Kategori? Kategori { get; set; } // Kategori verisi henüz yüklenmemiş olabilir
    }
}