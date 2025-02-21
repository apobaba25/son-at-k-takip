using System;
using System.Drawing;
using System.Windows.Forms;

namespace son_atik_takip
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblPasswordStrength; // Şifre gücü göstergesi
        private CheckBox chkRememberMe;     // "Beni Hatırla" seçeneği

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            lblUsername = new Label();
            lblPassword = new Label();
            lblPasswordStrength = new Label();
            chkRememberMe = new CheckBox();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.White;
            txtUsername.Location = new Point(120, 30);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(200, 23);
            txtUsername.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(120, 70);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(200, 23);
            txtPassword.TabIndex = 1;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(120, 180);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(75, 30);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Giriş";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // lblUsername
            // 
            lblUsername.BackColor = Color.DarkBlue;
            lblUsername.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblUsername.ForeColor = SystemColors.ControlLightLight;
            lblUsername.Location = new Point(14, 30);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(100, 23);
            lblUsername.TabIndex = 3;
            lblUsername.Text = "Kullanıcı Adı:";
            // 
            // lblPassword
            // 
            lblPassword.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblPassword.Location = new Point(24, 70);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(100, 23);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "Şifre:";
            // 
            // lblPasswordStrength
            // 
            lblPasswordStrength.Location = new Point(120, 100);
            lblPasswordStrength.Name = "lblPasswordStrength";
            lblPasswordStrength.Size = new Size(200, 20);
            lblPasswordStrength.TabIndex = 5;
            lblPasswordStrength.Text = "Şifre Gücü: ";
            // 
            // chkRememberMe
            // 
            chkRememberMe.Location = new Point(120, 130);
            chkRememberMe.Name = "chkRememberMe";
            chkRememberMe.Size = new Size(200, 20);
            chkRememberMe.TabIndex = 6;
            chkRememberMe.Text = "Beni Hatırla";
            chkRememberMe.UseVisualStyleBackColor = true;
            // 
            // LoginForm
            // 
            ClientSize = new Size(350, 230);
            Controls.Add(txtUsername);
            Controls.Add(txtPassword);
            Controls.Add(btnLogin);
            Controls.Add(lblUsername);
            Controls.Add(lblPassword);
            Controls.Add(lblPasswordStrength);
            Controls.Add(chkRememberMe);
            Name = "LoginForm";
            Text = "Giriş";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
