using System;
using System.Windows.Forms;
using System.Drawing;

namespace son_atik_takip
{
    partial class DashboardForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel chartContainer;
        private ComboBox comboBoxChartType;
        private Label labelChartType;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Text = "Dashboard";
            this.ClientSize = new Size(800, 600);

            chartContainer = new Panel();
            chartContainer.Dock = DockStyle.Fill;

            comboBoxChartType = new ComboBox();
            comboBoxChartType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxChartType.Items.AddRange(new object[] { "Column", "Line", "Pie", "Bar" });
            comboBoxChartType.SelectedIndex = 0;
            comboBoxChartType.Dock = DockStyle.Top;
            comboBoxChartType.SelectedIndexChanged += new EventHandler(comboBoxChartType_SelectedIndexChanged);

            labelChartType = new Label();
            labelChartType.Text = "Chart Type:";
            labelChartType.Dock = DockStyle.Top;
            labelChartType.Height = 25;

            this.Controls.Add(chartContainer);
            this.Controls.Add(comboBoxChartType);
            this.Controls.Add(labelChartType);
        }
    }
}
