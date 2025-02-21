using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Extensions.Logging;
using MaterialSkin.Controls;
using MaterialSkin;

namespace son_atik_takip
{
    public partial class DashboardForm : MaterialForm
    {
        private readonly DataTable stokTable;
        private readonly ILogger _logger;

        public DashboardForm(DataTable stokTable, ILogger logger)
        {
            InitializeComponent();
            this.stokTable = stokTable;
            this._logger = logger;
            LoadDashboard();
        }

        private void LoadDashboard()
        {
            try
            {
                SeriesChartType chartType = GetSelectedChartType();

                chartContainer.Controls.Clear();

                Chart chart = new Chart { Dock = DockStyle.Fill };
                chart.ChartAreas.Add(new ChartArea("MainArea"));

                var chartData = stokTable.AsEnumerable()
                    .GroupBy(r => r.Field<string>("Urun"))
                    .Select(g => new { Urun = g.Key, Toplam = g.Sum(r => r.Field<decimal>("Miktar")) })
                    .ToList();

                var series = new Series("Stok")
                {
                    ChartType = chartType
                };

                foreach (var item in chartData)
                {
                    series.Points.AddXY(item.Urun, item.Toplam);
                }
                chart.Series.Add(series);
                chartContainer.Controls.Add(chart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Dashboard yüklenirken hata oluştu.");
                MessageBox.Show("Dashboard yüklenirken hata: " + ex.Message);
            }
        }

        // DashboardForm.cs'de chart stil ayarları
        private void ApplyChartTheme(Chart chart)
        {
            chart.BackColor = MaterialSkinManager.Instance.BackgroundColor;
            chart.ChartAreas[0].BackColor = MaterialSkinManager.Instance.BackgroundColor;
            chart.ChartAreas[0].AxisX.LabelStyle.ForeColor = MaterialSkinManager.Instance.TextHighEmphasisColor;
            chart.ChartAreas[0].AxisY.LabelStyle.ForeColor = MaterialSkinManager.Instance.TextHighEmphasisColor;

            foreach (var series in chart.Series)
            {
                series.Color = MaterialSkinManager.Instance.ColorScheme.AccentColor;
                series.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            }
        }

        private SeriesChartType GetSelectedChartType()
        {
            if (comboBoxChartType.SelectedItem != null)
            {
                string selected = comboBoxChartType.SelectedItem.ToString();
                return selected switch
                {
                    "Column" => SeriesChartType.Column,
                    "Line" => SeriesChartType.Line,
                    "Pie" => SeriesChartType.Pie,
                    "Bar" => SeriesChartType.Bar,
                    _ => SeriesChartType.Column,
                };
            }
            return SeriesChartType.Column;
        }

        private void comboBoxChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDashboard();
        }
    }
}
