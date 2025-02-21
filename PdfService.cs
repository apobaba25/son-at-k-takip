using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Extensions.Logging;
using son_atik_takip.Models;

namespace son_atik_takip.Services
{
    public class PdfService : IPdfService
    {
        private readonly ILogger<PdfService> _logger;
        public PdfService(ILogger<PdfService> logger)
        {
            _logger = logger;
        }

        public void GeneratePdfReport(DataTable stokTable, string filePath)
        {
            _logger.LogInformation("PDF raporu oluşturuluyor (DataTable).");
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                Document doc = new Document(PageSize.A4.Rotate());
                PdfWriter.GetInstance(doc, fs);
                doc.Open();

                Paragraph header = new Paragraph("STOK RAPORU",
                    FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLUE))
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 10f
                };
                doc.Add(header);

                PdfPTable table = new PdfPTable(7) { WidthPercentage = 100 };
                string[] headers = { "GirisTarihi", "Miktar", "BirimFiyat", "ToplamFiyat", "Urun", "Tedarikci", "Plaka" };
                foreach (var h in headers)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(h))
                    {
                        BackgroundColor = new BaseColor(240, 240, 240),
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    table.AddCell(cell);
                }
                foreach (DataRow row in stokTable.Rows)
                {
                    DateTime girisTarihi = Convert.ToDateTime(row["GirisTarihi"]);
                    decimal miktar = Convert.ToDecimal(row["Miktar"]);
                    decimal fiyat = Convert.ToDecimal(row["BirimFiyat"]);
                    decimal toplam = miktar * fiyat;
                    table.AddCell(girisTarihi.ToString("dd.MM.yyyy HH:mm"));
                    table.AddCell(miktar.ToString("N2"));
                    table.AddCell(fiyat.ToString("C2", CultureInfo.GetCultureInfo("tr-TR")));
                    table.AddCell(toplam.ToString("C2", CultureInfo.GetCultureInfo("tr-TR")));
                    table.AddCell(row["Urun"].ToString());
                    table.AddCell(row["Tedarikci"].ToString());
                    table.AddCell(row["Plaka"].ToString());
                }
                doc.Add(table);
                doc.Close();
            }
            _logger.LogInformation("PDF raporu oluşturuldu (DataTable).");
        }

        public void GenerateStockPdf(List<StokRaporModel> stokRaporu, string filePath)
        {
            _logger.LogInformation("Stok PDF raporu oluşturuluyor.");
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                Document doc = new Document(PageSize.A4);
                PdfWriter.GetInstance(doc, fs);
                doc.Open();

                Paragraph header = new Paragraph("STOK ÖZET RAPORU",
                    FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLUE))
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 15f
                };
                doc.Add(header);

                PdfPTable table = new PdfPTable(3) { WidthPercentage = 100 };
                table.SetWidths(new float[] { 4, 2, 2 });
                AddTableHeader(table, "Ürün Adı");
                AddTableHeader(table, "Toplam Miktar (Kg)");
                AddTableHeader(table, "Ortalama Fiyat (₺)");

                foreach (var item in stokRaporu)
                {
                    table.AddCell(item.Urun);
                    table.AddCell(item.ToplamMiktar.ToString("N2"));
                    table.AddCell(item.OrtalamaFiyat.ToString("C2", new CultureInfo("tr-TR")));
                }
                doc.Add(table);
                doc.Close();
            }
            _logger.LogInformation("Stok PDF raporu oluşturuldu.");
        }

        private void AddTableHeader(PdfPTable table, string headerText)
        {
            PdfPCell cell = new PdfPCell(new Phrase(headerText))
            {
                BackgroundColor = new BaseColor(230, 230, 230),
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            table.AddCell(cell);
        }
    }
}
