namespace son_atik_takip
{
    partial class EnhancedForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvStok;
        private MaterialSkin.Controls.MaterialButton btnToggleTheme;
        private MaterialSkin.Controls.MaterialButton btnGenerateQRCode;
        private MaterialSkin.Controls.MaterialButton btnBackupDB;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvStok = new System.Windows.Forms.DataGridView();
            this.btnToggleTheme = new MaterialSkin.Controls.MaterialButton();
            this.btnGenerateQRCode = new MaterialSkin.Controls.MaterialButton();
            this.btnBackupDB = new MaterialSkin.Controls.MaterialButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStok)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvStok
            // 
            this.dgvStok.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStok.Location = new System.Drawing.Point(12, 80);
            this.dgvStok.Name = "dgvStok";
            this.dgvStok.Size = new System.Drawing.Size(760, 350);
            this.dgvStok.TabIndex = 0;
            // 
            // btnToggleTheme
            // 
            this.btnToggleTheme.AutoSize = false;
            this.btnToggleTheme.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnToggleTheme.Depth = 0;
            this.btnToggleTheme.DrawShadows = true;
            this.btnToggleTheme.HighEmphasis = true;
            this.btnToggleTheme.Icon = null;
            this.btnToggleTheme.Location = new System.Drawing.Point(12, 450);
            this.btnToggleTheme.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnToggleTheme.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnToggleTheme.Name = "btnToggleTheme";
            this.btnToggleTheme.Size = new System.Drawing.Size(150, 36);
            this.btnToggleTheme.TabIndex = 1;
            this.btnToggleTheme.Text = "Toggle Theme";
            this.btnToggleTheme.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnToggleTheme.UseAccentColor = false;
            this.btnToggleTheme.UseVisualStyleBackColor = true;
            this.btnToggleTheme.Click += new System.EventHandler(this.btnToggleTheme_Click);
            // 
            // btnGenerateQRCode
            // 
            this.btnGenerateQRCode.AutoSize = false;
            this.btnGenerateQRCode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnGenerateQRCode.Depth = 0;
            this.btnGenerateQRCode.DrawShadows = true;
            this.btnGenerateQRCode.HighEmphasis = true;
            this.btnGenerateQRCode.Icon = null;
            this.btnGenerateQRCode.Location = new System.Drawing.Point(180, 450);
            this.btnGenerateQRCode.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnGenerateQRCode.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnGenerateQRCode.Name = "btnGenerateQRCode";
            this.btnGenerateQRCode.Size = new System.Drawing.Size(180, 36);
            this.btnGenerateQRCode.TabIndex = 2;
            this.btnGenerateQRCode.Text = "Generate QR Code";
            this.btnGenerateQRCode.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnGenerateQRCode.UseAccentColor = false;
            this.btnGenerateQRCode.UseVisualStyleBackColor = true;
            this.btnGenerateQRCode.Click += new System.EventHandler(this.btnGenerateQRCode_Click);
            // 
            // btnBackupDB
            // 
            this.btnBackupDB.AutoSize = false;
            this.btnBackupDB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBackupDB.Depth = 0;
            this.btnBackupDB.DrawShadows = true;
            this.btnBackupDB.HighEmphasis = true;
            this.btnBackupDB.Icon = null;
            this.btnBackupDB.Location = new System.Drawing.Point(380, 450);
            this.btnBackupDB.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnBackupDB.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnBackupDB.Name = "btnBackupDB";
            this.btnBackupDB.Size = new System.Drawing.Size(150, 36);
            this.btnBackupDB.TabIndex = 3;
            this.btnBackupDB.Text = "Backup DB";
            this.btnBackupDB.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnBackupDB.UseAccentColor = false;
            this.btnBackupDB.UseVisualStyleBackColor = true;
            this.btnBackupDB.Click += new System.EventHandler(this.btnBackupDB_Click);
            // 
            // EnhancedForm
            // 
            this.ClientSize = new System.Drawing.Size(784, 511);
            this.Controls.Add(this.btnBackupDB);
            this.Controls.Add(this.btnGenerateQRCode);
            this.Controls.Add(this.btnToggleTheme);
            this.Controls.Add(this.dgvStok);
            this.Name = "EnhancedForm";
            this.Text = "Enhanced Stok Takip";
            ((System.ComponentModel.ISupportInitialize)(this.dgvStok)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
