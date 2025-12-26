using System.ComponentModel.DataAnnotations;

namespace BulutPastanesi.Models
{
    public class Kategori
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Kategori Adı")]
        [Required(ErrorMessage = "Lütfen kategori adını giriniz.")]
        public string Ad { get; set; } = string.Empty; // Varsayılan boş değer

        // Listeyi başlattık ki null hatası vermesin
        public virtual ICollection<Urun> Urunler { get; set; } = new List<Urun>();
    }
}