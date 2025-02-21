using System.Collections.Generic;
using System.Data;
using son_atik_takip.Models;

namespace son_atik_takip.Services
{
    public interface IPdfService
    {
        void GeneratePdfReport(DataTable stokTable, string filePath);
        void GenerateStockPdf(List<StokRaporModel> stokRaporu, string filePath);
    }
}
