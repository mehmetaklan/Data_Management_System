using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Collections.Generic;
using System.ComponentModel;

namespace HACKATHON.Models
{
    public class DegerTuru
    {
        public int DegerTuruId { get; set; }  // Birincil anahtar
        public string DegerTuruKodu { get; set; }  // Değer türü kodu
        public string DegerTuruAdi { get; set; }  // Değer türü adı
        public string Birim { get; set; }  // Birim (Örneğin: Adet, TL, Litre)
        public int VeriPeriyodu { get; set; }  // Veri periyodu (Örneğin: 3 Aylık, 6 Aylık)

        // Bir değer türü bir gruba ait olabilir (N-1)
        public int GrupId { get; set; }  // Yabancı anahtar (Grup)
        public Grup Grup { get; set; }  // Navigasyon özelliği

        // Bir değer türü birden fazla veri girişine sahip olabilir (1-N)

        
        public bool Status { get; set; }
        public ICollection<VeriGirisi> VeriGirisleri { get; set; }

        // Bir değer türü birden fazla sorumlu atamasına sahip olabilir (1-N)
        public ICollection<SorumluAtama> SorumluAtamalari { get; set; }

    }
}
