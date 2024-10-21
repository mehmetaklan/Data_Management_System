using System;
using System.ComponentModel;

namespace HACKATHON.Models
{
    public class VeriGirisi
    {

        public int VeriGirisiId { get; set; }  // Birincil anahtar
        public string Yil { get; set; }  // Veri yılı
        public string Donem { get; set; }  // Dönem (Örneğin: Ocak-Mart)
        public float Deger { get; set; }  // Değer

        // Bir veri girişi bir değer türüne ait olabilir (N-1)
        public int DegerTuruId { get; set; }  // Yabancı anahtar (Değer Türü)
        public DegerTuru DegerTuru { get; set; }  // Navigasyon özelliği

        [DefaultValue(true)]
        
        public bool Status { get; set; }
        
    }
}
