using System;
using System.Drawing;
using System.Windows.Forms;

namespace son_atik_takip
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel mainTableLayoutPanel;
        private ToolStrip toolStrip1;
        private ToolStripButton btnRapor;
        private ToolStripButton btnGrafik;
        private DataGridView dgvStok;
        private TableLayoutPanel bottomTableLayoutPanel;
        private GroupBox gbYeniKayit;
        private DateTimePicker dtpGirisTarihi;
        private Label lblPlaka;
        private TextBox txtPlaka;
        private Label lblTedarikci;
        private TextBox txtTedarikci;
        private Label lblUrun;
        private ComboBox cmbUrun;
        private Label lblMiktar;
        private TextBox txtMiktar;
        private Label lblFiyat;
        private TextBox txtFiyat;
        private Button btnEkle;
        private Button btnGuncelle;
        private Button btnSil;
        private Button btnExcelAktar;
        private GroupBox gbFiltreleme;
        private DateTimePicker dtpBaslangic;
        private DateTimePicker dtpBitis;
        private Button btnTarihFiltrele;
        private ComboBox cmbTedarikciFiltre;
        private Button btnTedarikciFiltre;
        private TextBox txtArama;
        private Label lblToplamStok;
        private TableLayoutPanel flowPanelBottom;
        private Button btnStokRaporu;
        private Button btnTemizle;
        private Button btnOpenEnhanced; // Yeni buton

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            dgvStok = new DataGridView();
            btnOpenEnhanced = new Button();
            mainTableLayoutPanel = new TableLayoutPanel();
            toolStrip1 = new ToolStrip();
            btnRapor = new ToolStripButton();
            btnGrafik = new ToolStripButton();
            bottomTableLayoutPanel = new TableLayoutPanel();
            gbYeniKayit = new GroupBox();
            dtpGirisTarihi = new DateTimePicker();
            lblPlaka = new Label();
            txtPlaka = new TextBox();
            lblTedarikci = new Label();
            txtTedarikci = new TextBox();
            lblUrun = new Label();
            cmbUrun = new ComboBox();
            lblMiktar = new Label();
            txtMiktar = new TextBox();
            lblFiyat = new Label();
            txtFiyat = new TextBox();
            btnEkle = new Button();
            btnGuncelle = new Button();
            btnSil = new Button();
            btnExcelAktar = new Button();
            gbFiltreleme = new GroupBox();
            dtpBaslangic = new DateTimePicker();
            dtpBitis = new DateTimePicker();
            btnTarihFiltrele = new Button();
            cmbTedarikciFiltre = new ComboBox();
            btnTedarikciFiltre = new Button();
            txtArama = new TextBox();
            lblToplamStok = new Label();
            flowPanelBottom = new TableLayoutPanel();
            btnStokRaporu = new Button();
            btnTemizle = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvStok).BeginInit();
            mainTableLayoutPanel.SuspendLayout();
            toolStrip1.SuspendLayout();
            bottomTableLayoutPanel.SuspendLayout();
            gbYeniKayit.SuspendLayout();
            gbFiltreleme.SuspendLayout();
            flowPanelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // dgvStok
            // 
            dgvStok.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStok.Location = new Point(3, 74);
            dgvStok.Name = "dgvStok";
            dgvStok.Size = new Size(760, 350);
            dgvStok.TabIndex = 0;
            // 
            // btnOpenEnhanced
            // 
            btnOpenEnhanced.Location = new Point(3, 28);
            btnOpenEnhanced.Name = "btnOpenEnhanced";
            btnOpenEnhanced.Size = new Size(150, 40);
            btnOpenEnhanced.TabIndex = 1;
            btnOpenEnhanced.Text = "Gelişmiş Formu Aç";
            btnOpenEnhanced.UseVisualStyleBackColor = true;
            btnOpenEnhanced.Click += btnOpenEnhanced_Click;
            // 
            // mainTableLayoutPanel
            // 
            mainTableLayoutPanel.ColumnCount = 1;
            mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainTableLayoutPanel.Controls.Add(toolStrip1, 0, 0);
            mainTableLayoutPanel.Controls.Add(btnOpenEnhanced, 0, 1);
            mainTableLayoutPanel.Controls.Add(dgvStok, 0, 2);
            mainTableLayoutPanel.Controls.Add(bottomTableLayoutPanel, 0, 3);
            mainTableLayoutPanel.Dock = DockStyle.Fill;
            mainTableLayoutPanel.Location = new Point(3, 64);
            mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            mainTableLayoutPanel.RowCount = 4;
            mainTableLayoutPanel.RowStyles.Add(new RowStyle());
            mainTableLayoutPanel.RowStyles.Add(new RowStyle());
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            mainTableLayoutPanel.Size = new Size(1105, 887);
            mainTableLayoutPanel.TabIndex = 0;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnRapor, btnGrafik });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1105, 25);
            toolStrip1.TabIndex = 0;
            // 
            // btnRapor
            // 
            btnRapor.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnRapor.Name = "btnRapor";
            btnRapor.Size = new Size(42, 22);
            btnRapor.Text = "Rapor";
            btnRapor.Click += btnRapor_Click;
            // 
            // btnGrafik
            // 
            btnGrafik.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnGrafik.Name = "btnGrafik";
            btnGrafik.Size = new Size(42, 22);
            btnGrafik.Text = "Grafik";
            btnGrafik.Click += btnGrafik_Click;
            // 
            // bottomTableLayoutPanel
            // 
            bottomTableLayoutPanel.ColumnCount = 2;
            bottomTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            bottomTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            bottomTableLayoutPanel.Controls.Add(gbYeniKayit, 0, 0);
            bottomTableLayoutPanel.Controls.Add(gbFiltreleme, 1, 0);
            bottomTableLayoutPanel.Controls.Add(flowPanelBottom, 0, 1);
            bottomTableLayoutPanel.Dock = DockStyle.Fill;
            bottomTableLayoutPanel.Location = new Point(3, 563);
            bottomTableLayoutPanel.Name = "bottomTableLayoutPanel";
            bottomTableLayoutPanel.RowCount = 2;
            bottomTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            bottomTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            bottomTableLayoutPanel.Size = new Size(1099, 321);
            bottomTableLayoutPanel.TabIndex = 2;
            // 
            // gbYeniKayit
            // 
            gbYeniKayit.Controls.Add(dtpGirisTarihi);
            gbYeniKayit.Controls.Add(lblPlaka);
            gbYeniKayit.Controls.Add(txtPlaka);
            gbYeniKayit.Controls.Add(lblTedarikci);
            gbYeniKayit.Controls.Add(txtTedarikci);
            gbYeniKayit.Controls.Add(lblUrun);
            gbYeniKayit.Controls.Add(cmbUrun);
            gbYeniKayit.Controls.Add(lblMiktar);
            gbYeniKayit.Controls.Add(txtMiktar);
            gbYeniKayit.Controls.Add(lblFiyat);
            gbYeniKayit.Controls.Add(txtFiyat);
            gbYeniKayit.Controls.Add(btnEkle);
            gbYeniKayit.Controls.Add(btnGuncelle);
            gbYeniKayit.Controls.Add(btnSil);
            gbYeniKayit.Controls.Add(btnExcelAktar);
            gbYeniKayit.Dock = DockStyle.Fill;
            gbYeniKayit.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 162);
            gbYeniKayit.Location = new Point(3, 3);
            gbYeniKayit.Name = "gbYeniKayit";
            gbYeniKayit.Size = new Size(543, 250);
            gbYeniKayit.TabIndex = 0;
            gbYeniKayit.TabStop = false;
            gbYeniKayit.Text = "Yeni Kayıt";
            // 
            // dtpGirisTarihi
            // 
            dtpGirisTarihi.Location = new Point(15, 25);
            dtpGirisTarihi.Name = "dtpGirisTarihi";
            dtpGirisTarihi.Size = new Size(230, 23);
            dtpGirisTarihi.TabIndex = 0;
            // 
            // lblPlaka
            // 
            lblPlaka.AutoSize = true;
            lblPlaka.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblPlaka.Location = new Point(15, 60);
            lblPlaka.Name = "lblPlaka";
            lblPlaka.Size = new Size(50, 20);
            lblPlaka.TabIndex = 1;
            lblPlaka.Text = "Plaka:";
            // 
            // txtPlaka
            // 
            txtPlaka.Location = new Point(130, 57);
            txtPlaka.Name = "txtPlaka";
            txtPlaka.Size = new Size(180, 23);
            txtPlaka.TabIndex = 2;
            // 
            // lblTedarikci
            // 
            lblTedarikci.AutoSize = true;
            lblTedarikci.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblTedarikci.Location = new Point(15, 90);
            lblTedarikci.Name = "lblTedarikci";
            lblTedarikci.Size = new Size(75, 20);
            lblTedarikci.TabIndex = 3;
            lblTedarikci.Text = "Tedarikçi:";
            // 
            // txtTedarikci
            // 
            txtTedarikci.Location = new Point(130, 87);
            txtTedarikci.Name = "txtTedarikci";
            txtTedarikci.Size = new Size(180, 23);
            txtTedarikci.TabIndex = 4;
            // 
            // lblUrun
            // 
            lblUrun.AutoSize = true;
            lblUrun.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblUrun.Location = new Point(15, 120);
            lblUrun.Name = "lblUrun";
            lblUrun.Size = new Size(48, 20);
            lblUrun.TabIndex = 5;
            lblUrun.Text = "Ürün:";
            // 
            // cmbUrun
            // 
            cmbUrun.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUrun.Items.AddRange(new object[] { "Karışık", "Kağıt", "Plastik", "Metal", "Cam" });
            cmbUrun.Location = new Point(130, 117);
            cmbUrun.Name = "cmbUrun";
            cmbUrun.Size = new Size(180, 23);
            cmbUrun.TabIndex = 6;
            // 
            // lblMiktar
            // 
            lblMiktar.AutoSize = true;
            lblMiktar.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblMiktar.Location = new Point(15, 150);
            lblMiktar.Name = "lblMiktar";
            lblMiktar.Size = new Size(60, 20);
            lblMiktar.TabIndex = 7;
            lblMiktar.Text = "Miktar:";
            // 
            // txtMiktar
            // 
            txtMiktar.Location = new Point(130, 147);
            txtMiktar.Name = "txtMiktar";
            txtMiktar.Size = new Size(180, 23);
            txtMiktar.TabIndex = 8;
            // 
            // lblFiyat
            // 
            lblFiyat.AutoSize = true;
            lblFiyat.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblFiyat.Location = new Point(15, 180);
            lblFiyat.Name = "lblFiyat";
            lblFiyat.Size = new Size(89, 20);
            lblFiyat.TabIndex = 9;
            lblFiyat.Text = "Birim Fiyat:";
            // 
            // txtFiyat
            // 
            txtFiyat.Location = new Point(130, 177);
            txtFiyat.Name = "txtFiyat";
            txtFiyat.Size = new Size(180, 23);
            txtFiyat.TabIndex = 10;
            // 
            // btnEkle
            // 
            btnEkle.Location = new Point(12, 210);
            btnEkle.Name = "btnEkle";
            btnEkle.Size = new Size(80, 35);
            btnEkle.TabIndex = 11;
            btnEkle.Text = "Ekle";
            btnEkle.Click += btnEkle_Click;
            // 
            // btnGuncelle
            // 
            btnGuncelle.Location = new Point(102, 210);
            btnGuncelle.Name = "btnGuncelle";
            btnGuncelle.Size = new Size(80, 35);
            btnGuncelle.TabIndex = 12;
            btnGuncelle.Text = "Güncelle";
            btnGuncelle.Click += btnGuncelle_Click;
            // 
            // btnSil
            // 
            btnSil.Location = new Point(187, 210);
            btnSil.Name = "btnSil";
            btnSil.Size = new Size(80, 35);
            btnSil.TabIndex = 13;
            btnSil.Text = "Sil";
            btnSil.Click += btnSil_Click;
            // 
            // btnExcelAktar
            // 
            btnExcelAktar.Location = new Point(272, 210);
            btnExcelAktar.Name = "btnExcelAktar";
            btnExcelAktar.Size = new Size(110, 35);
            btnExcelAktar.TabIndex = 14;
            btnExcelAktar.Text = "Excele Aktar";
            btnExcelAktar.Click += btnExcelAktar_Click;
            // 
            // gbFiltreleme
            // 
            gbFiltreleme.Controls.Add(dtpBaslangic);
            gbFiltreleme.Controls.Add(dtpBitis);
            gbFiltreleme.Controls.Add(btnTarihFiltrele);
            gbFiltreleme.Controls.Add(cmbTedarikciFiltre);
            gbFiltreleme.Controls.Add(btnTedarikciFiltre);
            gbFiltreleme.Controls.Add(txtArama);
            gbFiltreleme.Controls.Add(lblToplamStok);
            gbFiltreleme.Dock = DockStyle.Fill;
            gbFiltreleme.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 162);
            gbFiltreleme.Location = new Point(552, 3);
            gbFiltreleme.Name = "gbFiltreleme";
            gbFiltreleme.Size = new Size(544, 250);
            gbFiltreleme.TabIndex = 1;
            gbFiltreleme.TabStop = false;
            gbFiltreleme.Text = "Filtreleme";
            // 
            // dtpBaslangic
            // 
            dtpBaslangic.Location = new Point(15, 30);
            dtpBaslangic.Name = "dtpBaslangic";
            dtpBaslangic.Size = new Size(160, 23);
            dtpBaslangic.TabIndex = 0;
            // 
            // dtpBitis
            // 
            dtpBitis.Location = new Point(15, 60);
            dtpBitis.Name = "dtpBitis";
            dtpBitis.Size = new Size(160, 23);
            dtpBitis.TabIndex = 1;
            // 
            // btnTarihFiltrele
            // 
            btnTarihFiltrele.Location = new Point(15, 90);
            btnTarihFiltrele.Name = "btnTarihFiltrele";
            btnTarihFiltrele.Size = new Size(160, 35);
            btnTarihFiltrele.TabIndex = 2;
            btnTarihFiltrele.Text = "Tarih Filtrele";
            btnTarihFiltrele.Click += btnTarihFiltrele_Click;
            // 
            // cmbTedarikciFiltre
            // 
            cmbTedarikciFiltre.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTedarikciFiltre.Location = new Point(15, 130);
            cmbTedarikciFiltre.Name = "cmbTedarikciFiltre";
            cmbTedarikciFiltre.Size = new Size(340, 23);
            cmbTedarikciFiltre.TabIndex = 3;
            // 
            // btnTedarikciFiltre
            // 
            btnTedarikciFiltre.Location = new Point(15, 160);
            btnTedarikciFiltre.Name = "btnTedarikciFiltre";
            btnTedarikciFiltre.Size = new Size(160, 35);
            btnTedarikciFiltre.TabIndex = 4;
            btnTedarikciFiltre.Text = "Tedarikçi Raporu";
            btnTedarikciFiltre.Click += btnTedarikciRaporu_Click;
            // 
            // txtArama
            // 
            txtArama.Location = new Point(15, 200);
            txtArama.Name = "txtArama";
            txtArama.PlaceholderText = "Ürün ara...";
            txtArama.Size = new Size(340, 23);
            txtArama.TabIndex = 5;
            txtArama.TextChanged += TxtArama_TextChanged;
            // 
            // lblToplamStok
            // 
            lblToplamStok.AutoSize = true;
            lblToplamStok.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 162);
            lblToplamStok.Location = new Point(15, 230);
            lblToplamStok.Name = "lblToplamStok";
            lblToplamStok.Size = new Size(153, 21);
            lblToplamStok.TabIndex = 6;
            lblToplamStok.Text = "Toplam Stok: 0 Kg";
            // 
            // flowPanelBottom
            // 
            flowPanelBottom.ColumnCount = 2;
            flowPanelBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            flowPanelBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            flowPanelBottom.Controls.Add(btnStokRaporu, 1, 0);
            flowPanelBottom.Controls.Add(btnTemizle, 0, 0);
            flowPanelBottom.Dock = DockStyle.Fill;
            flowPanelBottom.Location = new Point(3, 259);
            flowPanelBottom.Name = "flowPanelBottom";
            flowPanelBottom.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            flowPanelBottom.Size = new Size(543, 59);
            flowPanelBottom.TabIndex = 2;
            // 
            // btnStokRaporu
            // 
            btnStokRaporu.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 162);
            btnStokRaporu.Location = new Point(274, 3);
            btnStokRaporu.Name = "btnStokRaporu";
            btnStokRaporu.Size = new Size(150, 35);
            btnStokRaporu.TabIndex = 0;
            btnStokRaporu.Text = "Stok Raporu Göster";
            btnStokRaporu.Click += btnStokRaporu_Click;
            // 
            // btnTemizle
            // 
            btnTemizle.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 162);
            btnTemizle.Location = new Point(3, 3);
            btnTemizle.Name = "btnTemizle";
            btnTemizle.Size = new Size(120, 35);
            btnTemizle.TabIndex = 1;
            btnTemizle.Text = "Formu Temizle";
            btnTemizle.Click += BtnTemizle_Click;
            // 
            // Form1
            // 
            ClientSize = new Size(1111, 954);
            Controls.Add(mainTableLayoutPanel);
            Name = "Form1";
            Text = "Stok Takip";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvStok).EndInit();
            mainTableLayoutPanel.ResumeLayout(false);
            mainTableLayoutPanel.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            bottomTableLayoutPanel.ResumeLayout(false);
            gbYeniKayit.ResumeLayout(false);
            gbYeniKayit.PerformLayout();
            gbFiltreleme.ResumeLayout(false);
            gbFiltreleme.PerformLayout();
            flowPanelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }
        #endregion
    }
}
