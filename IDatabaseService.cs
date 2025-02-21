using System.Data;

namespace son_atik_takip.Services
{
    public interface IDatabaseService
    {
        DataTable GetAllStokData();
        long InsertStok(dynamic stok);
        void UpdateStok(int id, dynamic stok);
        void DeleteStok(int id);
    }
}
