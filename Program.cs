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
            // appsettings.json dosyasýndan konfigürasyonu oluþturuyoruz.
            var configuration = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

            

            // DI konteynerimizi yapýlandýrýyoruz.
            var services = new ServiceCollection();
            ConfigureServices(services, configuration);

            // 1. DEÐÝÞÝKLÝK: using bloðunu açýkça belirt
            using (var serviceProvider = services.BuildServiceProvider())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // 2. DEÐÝÞÝKLÝK: Tüm kullanýmlarý using bloðu içinde yap
                var loginForm = serviceProvider.GetRequiredService<LoginForm>(); // Artýk 25. satýrda hata YOK
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    var mainForm = serviceProvider.GetRequiredService<Form1>();

                    // MaterialSkin ayarlarý
                    var materialSkinManager = MaterialSkinManager.Instance;
                    materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT; // Varsayýlan tema
                    materialSkinManager.ColorScheme = new ColorScheme(
                        Primary.Blue800,
                        Primary.Blue900,
                        Primary.Blue500,
                        Accent.LightBlue200,
                        TextShade.WHITE
                    );

                    Application.Run(mainForm);
                }
            } // using bloðu burada sonlanýyor
        }
       

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Loglama ayarlarý
            services.AddLogging(configure =>
            {
                configure.AddConsole();
                configure.SetMinimumLevel(LogLevel.Information);
            });

            // Konfigürasyonu DI konteynerine ekliyoruz.
            services.AddSingleton(configuration);

            // EF Core ile SQLite veritabaný için DbContext kaydý
            services.AddDbContext<StokDbContext>(options =>
    options.UseSqlite($"Data Source={configuration["DbFilePath"]}"));

            // IDatabaseService için DI kaydý
            services.AddScoped<IDatabaseService, DatabaseService>();

            // ExcelService için DI kaydý (string baðýmlýlýðý configuration üzerinden saðlanýyor)
            services.AddSingleton<IExcelService>(provider =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                var logger = provider.GetRequiredService<ILogger<ExcelService>>();
                string filePath = config["StokDosyaYolu"] ?? "stok.xlsx";
                return new ExcelService(filePath, logger);
            });

            // CsvService için DI kaydý (string baðýmlýlýðý configuration üzerinden saðlanýyor)
            services.AddSingleton<ICsvService>(provider =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                var logger = provider.GetRequiredService<ILogger<CsvService>>();
                return new CsvService(config["CsvExportPath"] ?? "stok.csv", logger);
            });

            // Diðer servislerin kaydý
            services.AddSingleton<IPdfService, PdfService>();
            services.AddSingleton<INotificationService, NotificationService>();
            services.AddSingleton<IUserService, UserService>();


            // Formlarýn kaydý
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
