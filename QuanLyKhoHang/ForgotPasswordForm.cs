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
    public partial class ForgotPasswordForm : Form
    {
        public ForgotPasswordForm()
        {
            InitializeComponent();
            this.Shown += ForgotPasswordForm_Shown;
        }
        private void AdjustSpacing()
        {
            int gap = 10; // Khoảng cách giữa Label và TextBox

            // 1. Tính toán khoảng cách để đẩy TextBox thẳng hàng
            int userLeft = lblForgotUser.Right + gap;
            int emailLeft = lblForgotEmail.Right + gap;

            // Tìm ra khoảng cách xa nhất
            int maxLeft = Math.Max(userLeft, emailLeft);

            txtForgotUser.Left = maxLeft;
            txtForgotEmail.Left = maxLeft;

            // 2. Tự động điều chỉnh độ rộng của cái hộp Panel cho vừa khít với TextBox
            panel1.Width = txtForgotUser.Left + txtForgotUser.Width + 20;

            // 3. Căn giữa nút bấm ở BÊN TRONG Panel (Giả sử tên nút là btnRecover)
            btnRecover.Left = (panel1.Width - btnRecover.Width) / 2;

            // 4. BƯỚC CHỐT: Căn giữa toàn bộ cái Panel ở BÊN TRONG màn hình Form
            panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
            panel1.Top = (this.ClientSize.Height - panel1.Height) / 2;
        }
        private void btnRecover_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lyquy\Source\Repos\Inventory-Management-WinForms\QuanLyKhoHang\sales.mdf;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT passwords FROM users WHERE RTRIM(user_id) = @user AND customer_email = @email";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user", txtForgotUser.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", txtForgotEmail.Text.Trim());

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        string msg = LoginForm.SavedLanguage == "vi-VN" ? "Mật khẩu của bạn là: " :
                                     (LoginForm.SavedLanguage == "zh-Hant" ? "您的密碼是: " : "Your password is: ");
                        MessageBox.Show(msg + result.ToString().Trim());
                    }
                    else
                    {
                        string err = LoginForm.SavedLanguage == "vi-VN" ? "Thông tin không khớp!" :
                                     (LoginForm.SavedLanguage == "zh-Hant" ? "資訊不符！" : "Information mismatch!");
                        MessageBox.Show(err);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        // 1. Hàm áp dụng ngôn ngữ cho Form
        private void ChangeLanguage(string cultureName)
        {
            CultureInfo culture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            // Đổi 'RegisterForm' thành tên class hiện tại của bạn
            ComponentResourceManager resources = new ComponentResourceManager(typeof(ForgotPasswordForm));

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

        private void ForgotPasswordForm_Load(object sender, EventArgs e)
        {
            // Đọc ngôn ngữ đang được chọn bên LoginForm và áp dụng ngay lập tức
            ChangeLanguage(LoginForm.SavedLanguage);
        }

        private void ForgotPasswordForm_Shown(object sender, EventArgs e)
        {
            // Bỏ nhấp nháy chuột mặc định
            this.ActiveControl = null;
            AdjustSpacing();
        }

    }
}
