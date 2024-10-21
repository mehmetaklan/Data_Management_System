using System.Collections.Generic;

namespace HACKATHON.Models
{
    public class Grup
    {
        public int GrupId { get; set; }  // Birincil anahtar
        public string GrupAdi { get; set; }  // Grup adı
        public string Aciklama { get; set; }  // Grup açıklaması

        // Bir grup bir kategoriye ait olabilir (N-1)
        public int KategoriId { get; set; }  // Yabancı anahtar (Kategori)
        public Kategori Kategori { get; set; }
        public ICollection<DegerTuru> DegerTurleri { get; set; }
    }
}
