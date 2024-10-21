using System.Collections.Generic;

namespace HACKATHON.Models
{
    public class Sorumlu
    {
        public int SorumluId { get; set; }  // Birincil anahtar
        public string AdSoyad { get; set; }  // Sorumlu adı
        public string Email { get; set; }  // Sorumlu email adresi 
        public string Parola { get; set; } //Sorumlu şifre

        // Bir sorumlu birden fazla atamaya sahip olabilir (1-N)
        public ICollection<SorumluAtama> SorumluAtamalari { get; set; }
    }
}
