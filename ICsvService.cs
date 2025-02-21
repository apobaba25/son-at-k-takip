using System.Data;

namespace son_atik_takip.Services
{
    public interface ICsvService
    {
        string FilePath { get; }
        void ExportCsv(DataTable stokTable);
    }
}
