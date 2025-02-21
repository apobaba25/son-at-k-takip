using System;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using son_atik_takip.Data;
using son_atik_takip.Models;

namespace son_atik_takip.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly StokDbContext _context;
        private readonly ILogger<DatabaseService> _logger;

        public DatabaseService(StokDbContext context, ILogger<DatabaseService> logger)
        {
            _context = context;
            _logger = logger;
            _context.Database.EnsureCreated();
        }

        public DataTable GetAllStokData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("GirisTarihi", typeof(DateTime));
            dt.Columns.Add("Miktar", typeof(decimal));
            dt.Columns.Add("BirimFiyat", typeof(decimal));
            dt.Columns.Add("Urun", typeof(string));
            dt.Columns.Add("Tedarikci", typeof(string));
            dt.Columns.Add("Plaka", typeof(string));
            dt.Columns.Add("Exported", typeof(bool));
            dt.Columns.Add("ToplamFiyat", typeof(decimal), "Miktar * BirimFiyat");

            var stokList = _context.Stoklar.AsNoTracking().ToList();
            foreach (var stok in stokList)
            {
                var row = dt.NewRow();
                row["Id"] = stok.Id;
                row["GirisTarihi"] = stok.GirisTarihi;
                row["Miktar"] = stok.Miktar;
                row["BirimFiyat"] = stok.BirimFiyat;
                row["Urun"] = stok.Urun;
                row["Tedarikci"] = stok.Tedarikci;
                row["Plaka"] = stok.Plaka;
                row["Exported"] = false;
                dt.Rows.Add(row);
            }
            return dt;
        }

        public long InsertStok(dynamic stok)
        {
            try
            {
                var newStok = new Stok
                {
                    GirisTarihi = stok.GirisTarihi,
                    Miktar = stok.Miktar,
                    BirimFiyat = stok.BirimFiyat,
                    Urun = stok.Urun,
                    Tedarikci = stok.Tedarikci,
                    Plaka = stok.Plaka
                };
                _context.Stoklar.Add(newStok);
                _context.SaveChanges();
                return newStok.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Stok ekleme sırasında hata oluştu.");
                return -1;
            }
        }

        public void UpdateStok(int id, dynamic stok)
        {
            try
            {
                var existingStok = _context.Stoklar.Find(id);
                if (existingStok != null)
                {
                    existingStok.GirisTarihi = stok.GirisTarihi;
                    existingStok.Miktar = stok.Miktar;
                    existingStok.BirimFiyat = stok.BirimFiyat;
                    existingStok.Urun = stok.Urun;
                    existingStok.Tedarikci = stok.Tedarikci;
                    existingStok.Plaka = stok.Plaka;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Stok güncelleme sırasında hata oluştu.");
            }
        }

        public void DeleteStok(int id)
        {
            try
            {
                var stok = _context.Stoklar.Find(id);
                if (stok != null)
                {
                    _context.Stoklar.Remove(stok);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Stok silme sırasında hata oluştu.");
            }
        }
    }
}
