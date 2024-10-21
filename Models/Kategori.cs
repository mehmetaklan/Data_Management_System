using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HACKATHON.Models
{
    public class Kategori
    {
        
            public int KategoriId { get; set; }  // Birincil anahtar
            public string KategoriAdi { get; set; }  // Kategori adı
            public string Aciklama { get; set; }  // Kategori açıklaması

            // Bir kategori birden fazla grupa sahip olabilir (1-N)
            public ICollection<Grup> Gruplar { get; set; }

    }
}
