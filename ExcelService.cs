using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using Microsoft.Extensions.Logging;

namespace son_atik_takip.Services
{
    public class ExcelService : IExcelService
    {
        public string FilePath { get; }
        private readonly ILogger<ExcelService> _logger;

        public ExcelService(string filePath, ILogger<ExcelService> logger)
        {
            FilePath = filePath;
            _logger = logger;
        }

        public DataTable LoadStokData()
        {
            _logger.LogInformation("Stok verileri okunuyor.");
            DataTable dt = new DataTable();

            if (!File.Exists(FilePath))
            {
                _logger.LogError("Excel dosyası bulunamadı: {FilePath}", FilePath);
                throw new FileNotFoundException("Excel dosyası bulunamadı.", FilePath);
            }

            try
            {
                using (var workbook = new XLWorkbook(FilePath))
                {
                    var worksheet = workbook.Worksheet("Stok")
                        ?? throw new Exception("'Stok' sayfası bulunamadı.");

                    var firstRow = worksheet.FirstRowUsed();
                    if (firstRow == null || !firstRow.CellsUsed().Any())
                        throw new Exception("'Stok' sayfası başlık içermiyor.");

                    foreach (var cell in firstRow.Cells())
                    {
                        dt.Columns.Add(cell.GetString());
                    }

                    foreach (var row in worksheet.RowsUsed().Skip(1))
                    {
                        var dataRow = dt.NewRow();
                        int colIndex = 0;
                        foreach (var cell in row.Cells(1, dt.Columns.Count))
                        {
                            dataRow[colIndex++] = cell.Value;
                        }
                        dt.Rows.Add(dataRow);
                    }
                }
                _logger.LogInformation("Stok verileri başarıyla okundu.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excel veri okuma hatası");
                dt = new DataTable();
            }

            return dt;
        }

        public void UpdateExcelReport(DataTable stokTable)
        {
            _logger.LogInformation("Excel raporu güncelleniyor.");
            using (var workbook = File.Exists(FilePath) ? new XLWorkbook(FilePath) : new XLWorkbook())
            {
                var stokSheet = workbook.Worksheets.FirstOrDefault(ws => ws.Name == "Stok")
                                ?? workbook.Worksheets.Add("Stok");
                if (!stokSheet.Rows().Any())
                {
                    stokSheet.Cell("A1").Value = "GirisTarihi";
                    stokSheet.Cell("B1").Value = "Miktar";
                    stokSheet.Cell("C1").Value = "BirimFiyat";
                    stokSheet.Cell("D1").Value = "Urun";
                    stokSheet.Cell("E1").Value = "Tedarikci";
                    stokSheet.Cell("F1").Value = "Plaka";
                    stokSheet.Cell("G1").Value = "ToplamFiyat";
                }
                int lastRow = stokSheet.LastRowUsed()?.RowNumber() ?? 1;
                var newRows = stokTable.AsEnumerable().Where(r => !(r.Field<bool?>("Exported") ?? true)).ToList();

                foreach (var row in newRows)
                {
                    lastRow++;
                    stokSheet.Cell(lastRow, 1).Value = row.Field<DateTime>("GirisTarihi");
                    stokSheet.Cell(lastRow, 2).Value = row.Field<decimal>("Miktar");
                    stokSheet.Cell(lastRow, 3).Value = row.Field<decimal>("BirimFiyat");
                    stokSheet.Cell(lastRow, 4).Value = row.Field<string>("Urun");
                    stokSheet.Cell(lastRow, 5).Value = row.Field<string>("Tedarikci");
                    stokSheet.Cell(lastRow, 6).Value = row.Field<string>("Plaka");
                    decimal miktar = row.Field<decimal>("Miktar");
                    decimal fiyat = row.Field<decimal>("BirimFiyat");
                    stokSheet.Cell(lastRow, 7).Value = miktar * fiyat;
                    row["Exported"] = true;
                }

                var headerRow = stokSheet.Row(1);
                var lastColumn = headerRow.LastCellUsed().Address.ColumnNumber;
                var dataRange = stokSheet.Range(1, 1, lastRow, lastColumn);
                if (!dataRange.IsEmpty())
                {
                    var table = dataRange.CreateTable("StokTable");
                    table.Theme = XLTableTheme.TableStyleMedium9;
                }

                // Raporun kaydedileceği klasör C:\Stok1 olarak ayarlanıyor
                string targetDir = @"C:\Stok1";
                if (!Directory.Exists(targetDir))
                {
                    Directory.CreateDirectory(targetDir);
                }
                string targetPath = Path.Combine(targetDir, Path.GetFileName(FilePath));
                workbook.SaveAs(targetPath);
            }
            _logger.LogInformation("Excel raporu güncellendi.");
        }

        public List<string> GetSupplierList()
        {
            var suppliers = new List<string>();
            if (File.Exists(FilePath))
            {
                using (var workbook = new XLWorkbook(FilePath))
                {
                    var sheet = workbook.Worksheet("Tedarikci Listesi");
                    if (sheet != null)
                    {
                        int lastRow = sheet.LastRowUsed()?.RowNumber() ?? 1;
                        for (int i = 2; i <= lastRow; i++)
                        {
                            string supp = sheet.Cell(i, 1).GetValue<string>().Trim();
                            if (!string.IsNullOrEmpty(supp) && !suppliers.Contains(supp))
                                suppliers.Add(supp);
                        }
                    }
                }
            }
            return suppliers;
        }

        public DataTable FilterByDate(string filePath, DateTime baslangic, DateTime bitis)
        {
            DataTable filteredTable = new DataTable();
            filteredTable.Columns.Add("Urun", typeof(string));
            filteredTable.Columns.Add("Miktar", typeof(decimal));
            filteredTable.Columns.Add("BirimFiyat", typeof(decimal));
            filteredTable.Columns.Add("ToplamFiyat", typeof(decimal));
            filteredTable.Columns.Add("Tedarikci", typeof(string));
            filteredTable.Columns.Add("GirisTarihi", typeof(DateTime));
            filteredTable.Columns.Add("Plaka", typeof(string));

            using (var workbook = new XLWorkbook(filePath))
            {
                var sheet = workbook.Worksheet("Stok") ?? throw new Exception("'Stok' sayfası bulunamadı.");
                int rowNumber = 2;
                while (!sheet.Cell(rowNumber, 1).IsEmpty())
                {
                    DateTime girisTarihi = sheet.Cell(rowNumber, 1).GetDateTime();
                    if (girisTarihi >= baslangic && girisTarihi <= bitis)
                    {
                        decimal miktar = sheet.Cell(rowNumber, 2).GetValue<decimal>();
                        decimal fiyat = sheet.Cell(rowNumber, 3).GetValue<decimal>();
                        string urun = sheet.Cell(rowNumber, 4).GetString();
                        string tedarikci = sheet.Cell(rowNumber, 5).GetString();
                        string plaka = sheet.Cell(rowNumber, 6).GetString();
                        decimal toplamFiyat = miktar * fiyat;
                        filteredTable.Rows.Add(urun, miktar, fiyat, toplamFiyat, tedarikci, girisTarihi, plaka);
                    }
                    rowNumber++;
                }
            }
            return filteredTable;
        }

        public string GenerateDateReport(DataTable filteredTable, DateTime baslangic, DateTime bitis)
        {
            // Çıktıların kaydedileceği klasör C:\Stok1 olarak belirleniyor
            string targetDir = @"C:\Stok1";
            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }
            string reportName = $"Tarih_Raporu_{baslangic:yyyyMMdd}_{bitis:yyyyMMdd}_{DateTime.Now:HHmmss}.xlsx";
            string reportPath = Path.Combine(targetDir, reportName);
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Tarih Raporu");
                var table = worksheet.Cell("A1").InsertTable(filteredTable, "DateReport", true);
                table.Theme = XLTableTheme.TableStyleMedium9;
                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(reportPath);
            }
            return reportPath;
        }

        public string GenerateSupplierReport(string supplier, string sourceFilePath)
        {
            // Çıktıların kaydedileceği klasör C:\Stok1 olarak belirleniyor
            string targetDir = @"C:\Stok1";
            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }
            string reportName = $"Tedarikci_Raporu_{supplier}_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
            string reportPath = Path.Combine(targetDir, reportName);
            using (var sourceWorkbook = new XLWorkbook(sourceFilePath))
            using (var reportWorkbook = new XLWorkbook())
            {
                var sourceWorksheet = sourceWorkbook.Worksheet("Stok")
                    ?? throw new Exception("'Stok' sayfası bulunamadı.");
                var reportWorksheet = reportWorkbook.Worksheets.Add("Rapor");

                sourceWorksheet.FirstRow().CopyTo(reportWorksheet.FirstRow());
                int rowCounter = 2;
                var supplierRows = sourceWorksheet.RowsUsed().Skip(1)
                    .Where(row => string.Equals(row.Cell(5).GetString().Trim(), supplier, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (!supplierRows.Any())
                    throw new Exception($"'{supplier}' isimli tedarikçi bulunamadı.");

                foreach (var row in supplierRows)
                {
                    row.CopyTo(reportWorksheet.Row(rowCounter));
                    rowCounter++;
                }
                var lastColumn = reportWorksheet.FirstRow().LastCellUsed().Address.ColumnNumber;
                var range = reportWorksheet.Range(1, 1, rowCounter - 1, lastColumn);
                var table = range.CreateTable("SupplierReport");
                table.Theme = XLTableTheme.TableStyleMedium9;

                reportWorksheet.Columns().AdjustToContents();
                reportWorkbook.SaveAs(reportPath);
            }
            return reportPath;
        }
    }
}
