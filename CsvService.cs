using System;
using System.Data;
using System.IO;
using Microsoft.Extensions.Logging;

namespace son_atik_takip.Services
{
    public class CsvService : ICsvService
    {
        public string FilePath { get; }
        private readonly ILogger<CsvService> _logger;

        public CsvService(string filePath, ILogger<CsvService> logger)
        {
            FilePath = filePath;
            _logger = logger;
        }

        public void ExportCsv(DataTable stokTable)
        {
            _logger.LogInformation("CSV dışa aktarma işlemi başlatılıyor.");
            using (var writer = new StreamWriter(FilePath))
            {
                writer.WriteLine("Urun,Miktar,BirimFiyat,ToplamFiyat,Tedarikci,Plaka,GirisTarihi");
                foreach (DataRow row in stokTable.Rows)
                {
                    string urun = row["Urun"].ToString();
                    string miktar = row["Miktar"].ToString();
                    decimal birimFiyat = Convert.ToDecimal(row["BirimFiyat"]);
                    decimal toplamFiyat = Convert.ToDecimal(row["Miktar"]) * birimFiyat;
                    string tedarikci = row["Tedarikci"].ToString();
                    string plaka = row["Plaka"].ToString();
                    string girisTarihi = Convert.ToDateTime(row["GirisTarihi"]).ToString("yyyy-MM-dd");
                    writer.WriteLine($"{urun},{miktar},{birimFiyat},{toplamFiyat},{tedarikci},{plaka},{girisTarihi}");
                }
            }
            _logger.LogInformation("CSV dışa aktarma tamamlandı.");
        }
    }
}
