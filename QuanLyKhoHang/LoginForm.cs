using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.ComponentModel;

namespace QuanLyKhoHang
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.AcceptButton = btn_login;
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string id = txtb_id.Text;
            string pass = txtb_password.Text;

            // Kiểm tra tài khoản cứng (Hardcoded)
            if (id == "quyen123" && pass == "quyen123")
            {
                // Trả về tín hiệu "Thành công" cho hệ thống biết
                this.DialogResult = DialogResult.OK;
                // Đóng form đăng nhập lại
                this.Close();
            }
            else
            {
                // Thông báo lỗi nếu sai
                MessageBox.Show("ID hoặc Password không đúng. Vui lòng thử lại!", "Lỗi Đăng Nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtb_password.Clear(); // Xóa pass nhập sai
                txtb_password.Focus(); // Đưa trỏ chuột về lại ô pass
            }
        }

        private void LoginForm_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = null;
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
        }
    }
}
