namespace son_atik_takip
{
    public class StokBilgisi
    {
        public string UrunTipi { get; set; }
        public decimal ToplamMiktar { get; set; }

        public override string ToString() => $"{UrunTipi}: {ToplamMiktar:N2} Kg";
    }
}
