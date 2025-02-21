using System;
using System.ComponentModel.DataAnnotations;

namespace son_atik_takip.Models
{
    public class Stok
    {
        [Key]
        public int Id { get; set; }
        public DateTime GirisTarihi { get; set; }
        public decimal Miktar { get; set; }
        public decimal BirimFiyat { get; set; }
        public string Urun { get; set; }
        public string Tedarikci { get; set; }
        public string Plaka { get; set; }
    }
}
