using System;
using System.Windows.Forms;
using son_atik_takip.Services;
using MaterialSkin.Controls;

namespace son_atik_takip
{
    public partial class LoginForm : MaterialForm
    {
        private readonly IUserService _userService;
        public LoginForm(IUserService userService)
        {
            InitializeComponent();
            _userService = userService;
            // Şifre alanı değiştiğinde şifre gücünü güncelle
            this.txtPassword.TextChanged += txtPassword_TextChanged;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (_userService.ValidateUser(txtUsername.Text.Trim(), txtPassword.Text))
            {
                // "Beni Hatırla" seçeneğini de burada işleyebilirsiniz
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Geçersiz kullanıcı adı veya şifre.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            string password = txtPassword.Text;
            lblPasswordStrength.Text = "Şifre Gücü: " + GetPasswordStrength(password);
        }

        private string GetPasswordStrength(string password)
        {
            if (string.IsNullOrEmpty(password))
                return "Boş";
            int score = 0;
            if (password.Length >= 8)
                score++;
            if (System.Text.RegularExpressions.Regex.IsMatch(password, @"[a-z]"))
                score++;
            if (System.Text.RegularExpressions.Regex.IsMatch(password, @"[A-Z]"))
                score++;
            if (System.Text.RegularExpressions.Regex.IsMatch(password, @"[0-9]"))
                score++;
            // Aşağıdaki desen içinde çift tırnak karakterini (") iki adet yazarak düzeltme yapıyoruz.
            if (System.Text.RegularExpressions.Regex.IsMatch(password, @"[!@#$%^&*(),.?""\:\{\}\|<>]"))
                score++;
            switch (score)
            {
                case 5: return "Çok Güçlü";
                case 4: return "Güçlü";
                case 3: return "Orta";
                case 2: return "Zayıf";
                default: return "Çok Zayıf";
            }
        }
    }
}
