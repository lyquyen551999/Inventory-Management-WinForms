using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
    }
}
