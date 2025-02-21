using System;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using son_atik_takip.Services;
using son_atik_takip.Data;
using Microsoft.EntityFrameworkCore;
using MaterialSkin;
using MaterialSkin.Controls;

namespace son_atik_takip
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // appsettings.json dosyas�ndan konfig�rasyonu olu�turuyoruz.
            var configuration = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

            

            // DI konteynerimizi yap�land�r�yoruz.
            var services = new ServiceCollection();
            ConfigureServices(services, configuration);

            // 1. DE����KL�K: using blo�unu a��k�a belirt
            using (var serviceProvider = services.BuildServiceProvider())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // 2. DE����KL�K: T�m kullan�mlar� using blo�u i�inde yap
                var loginForm = serviceProvider.GetRequiredService<LoginForm>(); // Art�k 25. sat�rda hata YOK
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    var mainForm = serviceProvider.GetRequiredService<Form1>();

                    // MaterialSkin ayarlar�
                    var materialSkinManager = MaterialSkinManager.Instance;
                    materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT; // Varsay�lan tema
                    materialSkinManager.ColorScheme = new ColorScheme(
                        Primary.Blue800,
                        Primary.Blue900,
                        Primary.Blue500,
                        Accent.LightBlue200,
                        TextShade.WHITE
                    );

                    Application.Run(mainForm);
                }
            } // using blo�u burada sonlan�yor
        }
       

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Loglama ayarlar�
            services.AddLogging(configure =>
            {
                configure.AddConsole();
                configure.SetMinimumLevel(LogLevel.Information);
            });

            // Konfig�rasyonu DI konteynerine ekliyoruz.
            services.AddSingleton(configuration);

            // EF Core ile SQLite veritaban� i�in DbContext kayd�
            services.AddDbContext<StokDbContext>(options =>
    options.UseSqlite($"Data Source={configuration["DbFilePath"]}"));

            // IDatabaseService i�in DI kayd�
            services.AddScoped<IDatabaseService, DatabaseService>();

            // ExcelService i�in DI kayd� (string ba��ml�l��� configuration �zerinden sa�lan�yor)
            services.AddSingleton<IExcelService>(provider =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                var logger = provider.GetRequiredService<ILogger<ExcelService>>();
                string filePath = config["StokDosyaYolu"] ?? "stok.xlsx";
                return new ExcelService(filePath, logger);
            });

            // CsvService i�in DI kayd� (string ba��ml�l��� configuration �zerinden sa�lan�yor)
            services.AddSingleton<ICsvService>(provider =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                var logger = provider.GetRequiredService<ILogger<CsvService>>();
                return new CsvService(config["CsvExportPath"] ?? "stok.csv", logger);
            });

            // Di�er servislerin kayd�
            services.AddSingleton<IPdfService, PdfService>();
            services.AddSingleton<INotificationService, NotificationService>();
            services.AddSingleton<IUserService, UserService>();


            // Formlar�n kayd�
            services.AddTransient<LoginForm>();
            services.AddTransient<Form1>();
            services.AddTransient<Form1>();
            services.AddTransient<DashboardForm>(provider =>
            {
                var excelService = provider.GetRequiredService<IExcelService>();
                var logger = provider.GetRequiredService<ILogger<DashboardForm>>();
                return new DashboardForm(excelService.LoadStokData(), logger);
            });
        }
    }
}
