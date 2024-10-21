namespace HACKATHON.Models
{
    public class SorumluAtama
    {
        public int SorumluAtamaId { get; set; }  // Birincil anahtar

        // Bir sorumlu ataması bir sorumluya ait olabilir (N-1)
        public int SorumluId { get; set; }  // Yabancı anahtar (Sorumlu)
        public Sorumlu Sorumlu { get; set; }  // Navigasyon özelliği

        // Bir sorumlu ataması bir değer türüne ait olabilir (N-1)
        public int DegerTuruId { get; set; }  // Yabancı anahtar (Değer Türü)
        public DegerTuru DegerTuru { get; set; }  // Navigasyon özelliği
    }
}
