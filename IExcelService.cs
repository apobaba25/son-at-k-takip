using System;
using System.Collections.Generic;
using System.Data;

namespace son_atik_takip.Services
{
    public interface IExcelService
    {
        string FilePath { get; }
        DataTable LoadStokData();
        void UpdateExcelReport(DataTable stokTable);
        List<string> GetSupplierList();
        DataTable FilterByDate(string filePath, DateTime baslangic, DateTime bitis);
        string GenerateDateReport(DataTable filteredTable, DateTime baslangic, DateTime bitis);
        string GenerateSupplierReport(string supplier, string sourceFilePath);
    }
}
