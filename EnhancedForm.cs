using System;
using System.Data;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using son_atik_takip.Services;
using Microsoft.Extensions.Logging;
using System.IO;

namespace son_atik_takip
{
    public partial class EnhancedForm : MaterialForm
    {
        private readonly IDatabaseService _dbService;
        private readonly INotificationService _notificationService;
        private readonly ILogger<EnhancedForm> _logger;
        private DataTable _stokTable;
        private System.Windows.Forms.Timer _stockCheckTimer; // Tam olarak System.Windows.Forms.Timer kullanılıyor.
        private bool isDarkTheme = false;
        private MaterialSkinManager materialSkinManager;

        public EnhancedForm(IDatabaseService dbService, INotificationService notificationService, ILogger<EnhancedForm> logger)
        {
            InitializeComponent();
            _dbService = dbService;
            _notificationService = notificationService;
            _logger = logger;

            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            LoadStokData();
            SetupTimer();
        }

        private void LoadStokData()
        {
            try
            {
                _stokTable = _dbService.GetAllStokData();
                dgvStok.DataSource = _stokTable;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Stok verileri yüklenirken hata oluştu.");
                MessageBox.Show("Stok verileri yüklenirken hata: " + ex.Message);
            }
        }

        private void SetupTimer()
        {
            _stockCheckTimer = new System.Windows.Forms.Timer();
            _stockCheckTimer.Interval = 60000; // Her 1 dakikada bir kontrol
            _stockCheckTimer.Tick += StockCheckTimer_Tick;
            _stockCheckTimer.Start();
        }

        private void StockCheckTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                decimal totalStock = 0;
                foreach (DataRow row in _stokTable.Rows)
                {
                    totalStock += row.Field<decimal>("Miktar");
                }
                // Stok 100'ün altına düşerse uyarı gönder
                if (totalStock < 100)
                {
                    _notificationService.SendStockAlert("Otomatik Stok Uyarısı", $"Stok miktarı kritik seviyenin altına düştü. Toplam: {totalStock}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Otomatik stok kontrolü sırasında hata oluştu.");
            }
        }

        private void btnToggleTheme_Click(object sender, EventArgs e)
        {
            isDarkTheme = !isDarkTheme;
            materialSkinManager.Theme = isDarkTheme ? MaterialSkinManager.Themes.DARK : MaterialSkinManager.Themes.LIGHT;
        }

        private void btnGenerateQRCode_Click(object sender, EventArgs e)
        {
            if (dgvStok.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvStok.CurrentRow.Cells["Id"].Value);
                string urun = dgvStok.CurrentRow.Cells["Urun"].Value.ToString();
                decimal miktar = Convert.ToDecimal(dgvStok.CurrentRow.Cells["Miktar"].Value);
                decimal fiyat = Convert.ToDecimal(dgvStok.CurrentRow.Cells["BirimFiyat"].Value);
                string qrData = $"ID: {id}\nÜrün: {urun}\nMiktar: {miktar}\nBirim Fiyat: {fiyat}";
                // QRCodeForm dosyalarının projede olduğundan emin olun!
                QRCodeForm qrForm = new QRCodeForm(qrData);
                qrForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Lütfen QR kodunu görmek için bir kayıt seçin.");
            }
        }

        private void btnBackupDB_Click(object sender, EventArgs e)
        {
            try
            {
                string sourcePath = "stok.db";
                string backupDir = @"C:\Stok1\Backup";
                if (!Directory.Exists(backupDir))
                    Directory.CreateDirectory(backupDir);
                string backupFile = Path.Combine(backupDir, $"stok_backup_{DateTime.Now:yyyyMMddHHmmss}.db");
                File.Copy(sourcePath, backupFile, true);
                MessageBox.Show("Veritabanı yedeği alındı:\n" + backupFile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Veritabanı yedeği alınırken hata oluştu.");
                MessageBox.Show("Veritabanı yedeği alınırken hata: " + ex.Message);
            }
        }
    }
}
