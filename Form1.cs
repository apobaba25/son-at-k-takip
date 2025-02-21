using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using son_atik_takip.Models;
using Microsoft.Extensions.Logging;
using son_atik_takip.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Console;
using Microsoft.EntityFrameworkCore;

namespace son_atik_takip
{
    public partial class Form1 : MaterialForm
    {
        private DataTable stokTable;
        private BindingSource stokBindingSource;
        private readonly IDatabaseService dbService;
        private readonly IExcelService excelService;
        private readonly IPdfService pdfService;
        private readonly ICsvService csvService;
        private readonly INotificationService notificationService;
        private readonly ILogger<Form1> logger;
        private readonly ILoggerFactory loggerFactory;  // Yeni: ILoggerFactory alanı

        public Form1(
            IDatabaseService dbService,
            IExcelService excelService,
            IPdfService pdfService,
            ICsvService csvService,
            INotificationService notificationService,
            ILogger<Form1> logger,
            ILoggerFactory loggerFactory)   // Yeni parametre
        {
            InitializeComponent();
            this.dbService = dbService;
            this.excelService = excelService;
            this.pdfService = pdfService;
            this.csvService = csvService;
            this.notificationService = notificationService;
            this.logger = logger;
            this.loggerFactory = loggerFactory;   // Yeni: atama yapılıyor

            InitializeStokTable();
            stokBindingSource = new BindingSource { DataSource = stokTable };
            dgvStok.DataSource = stokBindingSource;
            dgvStok.DataError += dgvStok_DataError;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            logger.LogInformation("Form1 yüklendi.");
            try
            {
                DataTable dt = await Task.Run(() => dbService.GetAllStokData());
                foreach (DataRow row in dt.Rows)
                {
                    stokTable.ImportRow(row);
                }
                logger.LogInformation("Veritabanı verileri başarıyla yüklendi.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Veritabanı verileri yüklenirken hata oluştu.");
                MessageBox.Show("Veritabanı verileri yüklenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            stokBindingSource.ResetBindings(false);
            UpdateSupplierFilter();
            UpdateTotalStock();
        }

        private void InitializeStokTable()
        {
            stokTable = new DataTable();
            stokTable.Columns.Add("Id", typeof(int));
            stokTable.Columns.Add("GirisTarihi", typeof(DateTime));
            stokTable.Columns.Add("Miktar", typeof(decimal));
            stokTable.Columns.Add("BirimFiyat", typeof(decimal));
            stokTable.Columns.Add("Urun", typeof(string));
            stokTable.Columns.Add("Tedarikci", typeof(string));
            stokTable.Columns.Add("Plaka", typeof(string));
            stokTable.Columns.Add("Exported", typeof(bool));
            DataColumn toplamFiyat = new DataColumn("ToplamFiyat", typeof(decimal))
            {
                Expression = "Miktar * BirimFiyat"
            };
            stokTable.Columns.Add(toplamFiyat);
        }

        private void UpdateSupplierFilter()
        {
            var supplierList = stokTable.AsEnumerable()
                .Select(r => r.Field<string>("Tedarikci"))
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Distinct()
                .OrderBy(s => s)
                .ToList();
            supplierList.Insert(0, "Tümü");
            cmbTedarikciFiltre.DataSource = supplierList;
            cmbTedarikciFiltre.SelectedIndex = 0;
        }

        private void UpdateTotalStock()
        {
            decimal total = stokTable.AsEnumerable().Sum(row => row.Field<decimal>("Miktar"));
            lblToplamStok.Text = $"Toplam Stok: {total:N0} Kg";
            if (total < 100)
            {
                notificationService.SendStockAlert("Kritik Stok Uyarısı", "Stok miktarı kritik seviyenin altına düştü.");
            }
        }

        private bool FormDogrula()
        {
            if (string.IsNullOrWhiteSpace(cmbUrun.Text) ||
                string.IsNullOrWhiteSpace(txtMiktar.Text) ||
                string.IsNullOrWhiteSpace(txtFiyat.Text) ||
                string.IsNullOrWhiteSpace(txtTedarikci.Text) ||
                string.IsNullOrWhiteSpace(txtPlaka.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private async void btnEkle_Click(object sender, EventArgs e)
        {
            if (!FormDogrula())
                return;
            if (!decimal.TryParse(txtMiktar.Text, out decimal miktar) || miktar <= 0)
            {
                MessageBox.Show("Miktar alanına sadece pozitif sayısal değer girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMiktar.Focus();
                return;
            }
            if (!decimal.TryParse(txtFiyat.Text, out decimal birimFiyat) || birimFiyat < 0)
            {
                MessageBox.Show("Geçersiz fiyat formatı! Örnek: 19.99", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFiyat.Focus();
                return;
            }
            try
            {
                var newRecord = new
                {
                    GirisTarihi = dtpGirisTarihi.Value,
                    Miktar = miktar,
                    BirimFiyat = birimFiyat,
                    Urun = cmbUrun.Text,
                    Tedarikci = txtTedarikci.Text.Trim(),
                    Plaka = txtPlaka.Text.Trim()
                };
                long newId = await Task.Run(() => dbService.InsertStok(newRecord));
                var newRow = stokTable.NewRow();
                newRow["Id"] = newId;
                newRow["GirisTarihi"] = newRecord.GirisTarihi;
                newRow["Miktar"] = newRecord.Miktar;
                newRow["BirimFiyat"] = newRecord.BirimFiyat;
                newRow["Urun"] = newRecord.Urun;
                newRow["Tedarikci"] = newRecord.Tedarikci;
                newRow["Plaka"] = newRecord.Plaka;
                newRow["Exported"] = false;
                stokTable.Rows.Add(newRow);
                stokBindingSource.ResetBindings(false);
                UpdateTotalStock();
                cmbUrun.SelectedIndex = -1;
                txtMiktar.Clear();
                txtFiyat.Clear();
                txtTedarikci.Clear();
                txtPlaka.Clear();
                logger.LogInformation("Yeni kayıt eklendi.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Kayıt eklenirken hata oluştu.");
                MessageBox.Show("Veri eklenirken hata: " + ex.Message, "Veri Ekleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (dgvStok.CurrentRow == null || dgvStok.CurrentRow.Index < 0)
            {
                MessageBox.Show("Lütfen güncellenecek bir satır seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int selectedIndex = dgvStok.CurrentRow.Index;
                DataRow row = stokTable.Rows[selectedIndex];
                if (row["Id"] == DBNull.Value)
                {
                    MessageBox.Show("Bu kayıt henüz veritabanına eklenmemiş.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int id = Convert.ToInt32(row["Id"]);
                if (!decimal.TryParse(txtMiktar.Text, out decimal yeniMiktar))
                {
                    MessageBox.Show("Geçersiz miktar değeri.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!decimal.TryParse(txtFiyat.Text, out decimal yeniBirimFiyat))
                {
                    MessageBox.Show("Geçersiz fiyat değeri.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var updatedRecord = new
                {
                    GirisTarihi = dtpGirisTarihi.Value,
                    Miktar = yeniMiktar,
                    BirimFiyat = yeniBirimFiyat,
                    Urun = cmbUrun.Text,
                    Tedarikci = txtTedarikci.Text.Trim(),
                    Plaka = txtPlaka.Text.Trim()
                };
                await Task.Run(() => dbService.UpdateStok(id, updatedRecord));
                row["GirisTarihi"] = updatedRecord.GirisTarihi;
                row["Miktar"] = updatedRecord.Miktar;
                row["BirimFiyat"] = updatedRecord.BirimFiyat;
                row["Urun"] = updatedRecord.Urun;
                row["Tedarikci"] = updatedRecord.Tedarikci;
                row["Plaka"] = updatedRecord.Plaka;
                stokBindingSource.ResetBindings(false);
                logger.LogInformation("Kayıt güncellendi.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Güncelleme hatası");
                MessageBox.Show("Güncelleme başarısız: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSil_Click(object sender, EventArgs e)
        {
            if (dgvStok.CurrentRow == null)
                return;
            try
            {
                int selectedIndex = dgvStok.CurrentRow.Index;
                DataRow row = stokTable.Rows[selectedIndex];
                if (row["Id"] == DBNull.Value)
                {
                    MessageBox.Show("Bu kayıt henüz veritabanına eklenmemiş.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int id = Convert.ToInt32(row["Id"]);
                await Task.Run(() => dbService.DeleteStok(id));
                stokTable.Rows.RemoveAt(selectedIndex);
                stokBindingSource.ResetBindings(false);
                UpdateTotalStock();
                logger.LogInformation("Kayıt silindi.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Silme hatası");
                MessageBox.Show("Silme işlemi başarısız: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcelAktar_Click(object sender, EventArgs e)
        {
            try
            {
                excelService.UpdateExcelReport(stokTable);
                MessageBox.Show("Excel dosyası güncellendi:\n" + excelService.FilePath, "Excel Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                foreach (DataRow row in stokTable.Rows)
                {
                    row["Exported"] = true;
                }
                UpdateSupplierFilter();
                logger.LogInformation("Excel güncelleme işlemi başarıyla tamamlandı.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Excel güncelleme sırasında hata oluştu.");
                MessageBox.Show("Excel güncelleme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTedarikciRaporu_Click(object sender, EventArgs e)
        {
            string selectedSupplier = cmbTedarikciFiltre.SelectedItem?.ToString().Trim();
            if (string.IsNullOrEmpty(selectedSupplier) || selectedSupplier == "Tümü")
            {
                MessageBox.Show("Lütfen belirli bir tedarikçi seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string reportPath = excelService.GenerateSupplierReport(selectedSupplier, excelService.FilePath);
                MessageBox.Show($"Rapor başarıyla oluşturuldu:\n{reportPath}", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                logger.LogInformation("Tedarikçi raporu oluşturuldu.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Tedarikçi raporu oluşturulurken hata oluştu.");
                MessageBox.Show("Tedarikçi raporu oluşturulurken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTarihFiltrele_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tarih filtreleme özelliği veritabanı üzerinden eklenebilir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCSVExport_Click(object sender, EventArgs e)
        {
            try
            {
                csvService.ExportCsv(stokTable);
                MessageBox.Show("CSV dosyası başarıyla oluşturuldu:\n" + csvService.FilePath, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                logger.LogInformation("CSV aktarımı tamamlandı.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "CSV aktarımı sırasında hata oluştu.");
                MessageBox.Show("CSV aktarım hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRapor_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "PDF Dosyaları|*.pdf";
                    if (sfd.ShowDialog() == DialogResult.OK)
                        pdfService.GeneratePdfReport(stokTable, sfd.FileName);
                }
                logger.LogInformation("PDF raporu oluşturuldu.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "PDF raporu oluşturulurken hata oluştu.");
                MessageBox.Show("PDF rapor oluşturma hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGrafik_Click(object sender, EventArgs e)
        {
            try
            {
                DashboardForm dashboard = new DashboardForm(stokTable, logger);
                dashboard.Show();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Grafik oluşturma hatası");
                MessageBox.Show("Grafik oluşturulamadı: " + ex.Message);
            }
        }

        private void btnStokRaporu_Click(object sender, EventArgs e)
        {
            try
            {
                var stokRaporu = stokTable.AsEnumerable()
                    .GroupBy(row => row.Field<string>("Urun"))
                    .Select(g => new StokRaporModel
                    {
                        Urun = g.Key,
                        ToplamMiktar = g.Sum(r => r.Field<decimal>("Miktar")),
                        OrtalamaFiyat = g.Average(r => r.Field<decimal>("BirimFiyat"))
                    })
                    .ToList();

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "PDF Dosyaları|*.pdf";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        pdfService.GenerateStockPdf(stokRaporu, sfd.FileName);
                        MessageBox.Show("PDF raporu oluşturuldu: " + sfd.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Stok raporu oluşturulamadı");
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            cmbUrun.SelectedIndex = -1;
            txtMiktar.Clear();
            txtFiyat.Clear();
            txtTedarikci.Clear();
            txtPlaka.Clear();
        }

        private void dgvStok_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void TxtArama_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtArama.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                stokBindingSource.Filter = "";
            }
            else
            {
                try
                {
                    var filteredRows = stokTable.AsEnumerable()
                        .Where(row => row.Field<string>("Urun").IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0);
                    if (filteredRows.Any())
                    {
                        stokBindingSource.DataSource = filteredRows.CopyToDataTable();
                    }
                    else
                    {
                        stokBindingSource.DataSource = stokTable.Clone();
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Arama sırasında hata oluştu");
                    MessageBox.Show("Arama yapılamadı: Geçersiz filtre!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            stokBindingSource.ResetBindings(false);
            UpdateTotalStock();
        }

        // Yeni eklenen "Gelişmiş Formu Aç" butonunun tıklama olayı:
        private void btnOpenEnhanced_Click(object sender, EventArgs e)
        {
            // ILoggerFactory kullanarak EnhancedForm için tipik logger oluşturuyoruz.
            ILogger<EnhancedForm> enhancedLogger = loggerFactory.CreateLogger<EnhancedForm>();
            EnhancedForm enhancedForm = new EnhancedForm(dbService, notificationService, enhancedLogger);
            enhancedForm.ShowDialog();
        }
    }
}
