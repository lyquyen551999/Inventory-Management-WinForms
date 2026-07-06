using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhoHang
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.AcceptButton = btn_login;
            this.Shown += LoginForm_Shown;
        }

        private void AdjustSpacing()
        {
            // 1. Lấy tọa độ điểm giữa của cái Panel
            int centerPanel = panel1.Width / 2;

            // 2. Căn giữa tất cả các thành phần BÊN TRONG Panel
            txtb_id.Left = centerPanel - (txtb_id.Width / 2);
            txtb_password.Left = centerPanel - (txtb_password.Width / 2);
            btn_login.Left = centerPanel - (btn_login.Width / 2);
            lilbl_forgetpasswords.Left = centerPanel - (lilbl_forgetpasswords.Width / 2);
            lilbl_signup.Left = centerPanel - (lilbl_signup.Width / 2);

            // 3. BƯỚC CHỐT: Căn giữa toàn bộ cái Panel ở BÊN TRONG màn hình Form
            panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
            panel1.Top = (this.ClientSize.Height - panel1.Height) / 2;
        }
        private void ShowError(Exception ex)
        {
            // Xác định nội dung lỗi dựa trên ngôn ngữ đang chọn
            string errorMsg = LoginForm.SavedLanguage == "vi-VN" ? "Lỗi hệ thống: " :
                              (LoginForm.SavedLanguage == "zh-Hant" || LoginForm.SavedLanguage == "zh-Hant-TW" ? "系統錯誤: " : "System Error: ");

            // Xác định tiêu đề dựa trên ngôn ngữ đang chọn
            string errorTitle = LoginForm.SavedLanguage == "vi-VN" ? "Hệ thống báo lỗi" :
                                (LoginForm.SavedLanguage == "zh-Hant" || LoginForm.SavedLanguage == "zh-Hant-TW" ? "系統提示" : "Error");

            MessageBox.Show(errorMsg + ex.Message, errorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LoginForm_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            this.PerformLayout();
            AdjustSpacing();
        }
        // Hàm đệ quy để quét sạch mọi control, kể cả những control nằm sâu trong Panel
        private void ApplyResourcesToControls(Control.ControlCollection controls, ComponentResourceManager resources)
        {
            foreach (Control control in controls)
            {
                resources.ApplyResources(control, control.Name);
                // Nếu bên trong control này lại chứa control khác (như Panel chứa Button)
                if (control.Controls.Count > 0)
                {
                    ApplyResourcesToControls(control.Controls, resources);
                }
            }
        }
        // 1. Biến tĩnh để lưu ngôn ngữ và truyền sang Form1
        public static string SavedLanguage = "en";

        // 2. Hàm thực thi việc đổi chữ cho LoginForm
        private void ChangeLanguage(string cultureName)
        {
            SavedLanguage = cultureName;

            // Ghi nhớ lựa chọn vào file để khi Restart không bị mất dữ liệu
            File.WriteAllText("lang.txt", cultureName);

            CultureInfo culture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            ComponentResourceManager resources = new ComponentResourceManager(typeof(LoginForm));
            // Áp dụng ngôn ngữ mới cho LoginForm
            resources.ApplyResources(this, "$this");

            // Quét các control con bên trong
            ApplyResourcesToControls(this.Controls, resources);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // 1. Tự động kiểm tra và đọc lại ngôn ngữ đã lưu từ file (nếu có)
            if (File.Exists("lang.txt"))
            {
                SavedLanguage = File.ReadAllText("lang.txt").Trim();
            }

            // 2. Chọn đúng ngôn ngữ trên ComboBox thay vì gán cứng Index = 0 như trước
            if (cbxlanguage.Items.Count > 0)
            {
                if (SavedLanguage == "vi-VN")
                {
                    cbxlanguage.SelectedItem = "Tiếng Việt";
                }
                else if (SavedLanguage == "zh-Hant")
                {
                    cbxlanguage.SelectedItem = "中文";
                }
                else
                {
                    cbxlanguage.SelectedItem = "English";
                }
            }
        }

        private void cbxlanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLang = cbxlanguage.SelectedItem.ToString().Trim();

            if (selectedLang == "Tiếng Việt")
            {
                ChangeLanguage("vi-VN");
            }
            else if (selectedLang == "中文")
            {
                ChangeLanguage("zh-Hant");
            }
            else
            {
                // Mặc định là English (hoặc ngôn ngữ gốc của hệ điều hành)
                ChangeLanguage("en");
            }
            AdjustSpacing();
        }

        private void btn_login_Click_1(object sender, EventArgs e)
        {
            // 1. Kiểm tra không cho để trống
            if (string.IsNullOrWhiteSpace(txtb_id.Text) || string.IsNullOrWhiteSpace(txtb_password.Text))
            {
                string msgEmpty = SavedLanguage == "vi-VN" ? "Vui lòng nhập tài khoản và mật khẩu!" :
                                  (SavedLanguage == "zh-Hant" ? "請輸入帳號和密碼！" : "Please enter username and password!");
                MessageBox.Show(msgEmpty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kết nối tới DB sales.mdf
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lyquy\Source\Repos\Inventory-Management-WinForms\QuanLyKhoHang\sales.mdf;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // 3. Viết câu lệnh truy vấn tìm user_id và passwords trong bảng users
                    // Dùng hàm RTRIM trong SQL để cắt dấu cách thừa của kiểu nchar(10)
                    string query = "SELECT COUNT(*) FROM users WHERE RTRIM(user_id) = @user AND RTRIM(passwords) = @pass";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Dùng .Trim() ở C# để cắt dấu cách do người dùng lỡ tay gõ vào
                        cmd.Parameters.AddWithValue("@user", txtb_id.Text.Trim());
                        cmd.Parameters.AddWithValue("@pass", txtb_password.Text.Trim());

                        // Lấy kết quả đếm xem có bao nhiêu tài khoản khớp
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count > 0)
                        {
                            // Trả về tín hiệu "Thành công" cho hệ thống biết
                            this.DialogResult = DialogResult.OK;
                            // Đóng form đăng nhập lại
                            this.Close();
                        }
                        else
                        {
                            // Đăng nhập thất bại (Sai user hoặc pass)
                            string msgFail = SavedLanguage == "vi-VN" ? "Tài khoản hoặc mật khẩu không chính xác!" :
                                             (SavedLanguage == "zh-Hant" ? "帳號或密碼不正確！" : "Invalid username or password!");
                            string titleFail = SavedLanguage == "vi-VN" ? "Lỗi đăng nhập" : (SavedLanguage == "zh-Hant" ? "登入失敗" : "Login Failed");

                            MessageBox.Show(msgFail, titleFail, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void lilbl_forgetpasswords_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new ForgotPasswordForm().ShowDialog();
        }

        private void lilbl_signup_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new RegisterForm().ShowDialog();
        }

        private void LoginForm_Resize(object sender, EventArgs e)
        {
            if (panel1 != null)
            {
                AdjustSpacing();
            }
        }
    }
}
