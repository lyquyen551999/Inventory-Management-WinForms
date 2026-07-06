using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhoHang
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // 1. KIỂM TRA DỮ LIỆU TRỐNG (Thêm đoạn này vào đầu hàm)
            if (string.IsNullOrWhiteSpace(txtRegUser.Text) ||
                string.IsNullOrWhiteSpace(txtRegPass.Text) ||
                string.IsNullOrWhiteSpace(txtRegEmail.Text))
            {
                string msgEmpty = LoginForm.SavedLanguage == "vi-VN" ? "Vui lòng nhập đầy đủ thông tin đăng ký!" :
                                  (LoginForm.SavedLanguage == "zh-Hant" ? "請輸入完整的註冊資訊！" : "Please enter full registration information!");
                string title = LoginForm.SavedLanguage == "vi-VN" ? "Thông báo" :
                               (LoginForm.SavedLanguage == "zh-Hant" ? "提示" : "Notification");

                MessageBox.Show(msgEmpty, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng lại, không chạy code SQL phía dưới
            }
            try
            {
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lyquy\Source\Repos\Inventory-Management-WinForms\QuanLyKhoHang\sales.mdf;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString)) // Hãy copy connectionString từ Form1 sang đây
                {
                    conn.Open();
                    // Kiểm tra trùng lặp
                    string checkSql = "SELECT COUNT(*) FROM users WHERE RTRIM(user_id) = @user";
                    SqlCommand checkCmd = new SqlCommand(checkSql, conn);
                    checkCmd.Parameters.AddWithValue("@user", txtRegUser.Text.Trim());

                    if (Convert.ToInt32(checkCmd.ExecuteScalar()) > 0)
                    {
                        string msg = LoginForm.SavedLanguage == "vi-VN" ? "Tài khoản đã tồn tại!" :
                                     (LoginForm.SavedLanguage == "zh-Hant" ? "帳號已存在！" : "Account exists!");
                        MessageBox.Show(msg); return;
                    }

                    // Thêm mới
                    string insSql = "INSERT INTO users (customer_id, customer_name, user_id, passwords, customer_email) VALUES (@cid, @cname, @uid, @pwd, @mail)";
                    SqlCommand insCmd = new SqlCommand(insSql, conn);
                    insCmd.Parameters.AddWithValue("@cid", "CUST_" + new Random().Next(100, 999)); // ID giả định
                    insCmd.Parameters.AddWithValue("@cname", "New User");
                    insCmd.Parameters.AddWithValue("@uid", txtRegUser.Text.Trim());
                    insCmd.Parameters.AddWithValue("@pwd", txtRegPass.Text.Trim());
                    insCmd.Parameters.AddWithValue("@mail",txtRegEmail.Text.Trim());
                    insCmd.ExecuteNonQuery();

                    string success = LoginForm.SavedLanguage == "vi-VN" ? "Đăng ký thành công!" :
                                     (LoginForm.SavedLanguage == "zh-Hant" ? "註冊成功！" : "Registered successfully!");
                    MessageBox.Show(success);
                    this.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        // 1. Hàm áp dụng ngôn ngữ cho Form
        private void ChangeLanguage(string cultureName)
        {
            CultureInfo culture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            // Đổi 'RegisterForm' thành tên class hiện tại của bạn
            ComponentResourceManager resources = new ComponentResourceManager(typeof(RegisterForm));

            // Áp dụng chữ mới cho tiêu đề Form
            resources.ApplyResources(this, "$this");

            // Quét và áp dụng chữ mới cho các control bên trong
            ApplyResourcesToControls(this.Controls, resources);
        }

        // 2. Hàm đệ quy quét control
        private void ApplyResourcesToControls(Control.ControlCollection controls, ComponentResourceManager resources)
        {
            foreach (Control control in controls)
            {
                resources.ApplyResources(control, control.Name);
                if (control.Controls.Count > 0)
                {
                    ApplyResourcesToControls(control.Controls, resources);
                }
            }
        }
        // 1. Hàm tự động tính toán khoảng cách
        private void AdjustSpacing()
        {
            int gap = 10; // Khoảng cách giữa Label và TextBox

            // 1. Tính toán khoảng cách để đẩy TextBox thẳng hàng
            int userLeft = lblUser.Right + gap;
            int passLeft = lblPass.Right + gap;
            int emailLeft = lblEmail.Right + gap;

            int maxLeft = Math.Max(userLeft, Math.Max(passLeft, emailLeft));

            txtRegUser.Left = maxLeft;
            txtRegPass.Left = maxLeft;
            txtRegEmail.Left = maxLeft;

            // 2. Tự động điều chỉnh độ rộng của cái hộp Panel cho vừa khít với TextBox
            // (Lấy lề trái của TextBox + chiều rộng TextBox + 20 pixel dư ra cho đẹp)
            panel1.Width = txtRegUser.Left + txtRegUser.Width + 20;

            // 3. Căn giữa nút Đăng ký ở BÊN TRONG cái Panel
            btnRegister.Left = (panel1.Width - btnRegister.Width) / 2;

            // 4. BƯỚC CHỐT: Căn giữa toàn bộ cái Panel ở BÊN TRONG màn hình Form
            panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
            panel1.Top = (this.ClientSize.Height - panel1.Height) / 2;
        }
        // 3. Sự kiện Form_Load (Chạy ngay khi Form vừa mở lên)
        private void RegisterForm_Load(object sender, EventArgs e)
        {
            // Đọc ngôn ngữ đang được chọn bên LoginForm và áp dụng ngay lập tức
            ChangeLanguage(LoginForm.SavedLanguage);
        }
        private void RegisterForm_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            AdjustSpacing();
        }
    }
}
