namespace son_atik_takip.Services
{
    public interface INotificationService
    {
        void SendStockAlert(string subject, string message);
    }
}
