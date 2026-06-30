using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms; // Đảm bảo có thư viện này cho Form

namespace QuanLyKhoHang
{
    public partial class Form1 : Form
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Users\lyquy\source\repos\QuanLyKhoHang\QuanLyKhoHang\KhoHangDB.mdf;Integrated Security=True";
        int selectedId = 0; // Biến này để nhớ xem bạn đang thao tác trên sản phẩm nào
        SqlConnection conn;
        public Form1()
        {
            InitializeComponent();
        }
        // 2. Viết hàm lấy dữ liệu từ Database đổ vào DataGridView
        private void LoadData()
        {
            try
            {
                using (conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Đổi "Table" thành tên bảng thực tế bạn đã tạo (ví dụ: SanPham)
                    string query = "SELECT * FROM [Table]";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // dgvSanPham là tên của DataGridView bạn đã kéo vào Form
                    dgvSanPham.DataSource = dt;
                }
            }
            // Nếu có lỗi xảy ra, nhảy ngay vào đây để bắt lấy
            catch (Exception ex)
            {
                // Hiện thông báo lỗi lịch sự thay vì làm văng app
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Hệ thống báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // 3. Gọi hàm LoadData khi Form vừa mở lên
        private void Form1_Load(object sender, EventArgs e)
        {
            dgvSanPham.AllowUserToAddRows = false;
            LoadData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Xóa trắng các ô nhập liệu
                txtTen.Clear();
                txtSoLuong.Clear();
                txtGia.Clear();

                // 2. Xóa trắng ô tìm kiếm (nếu bạn có đặt tên ô tìm kiếm là txtTimKiem)
                txtTimKiem.Clear();

                // 3. Đưa biến lưu ID về 0 (trạng thái chưa chọn gì cả)
                selectedId = 0;

                // 4. Tải lại toàn bộ dữ liệu nguyên bản lên bảng
                LoadData();

                // 5. Đưa con trỏ chuột nhấp nháy về lại ô Tên sản phẩm để sẵn sàng gõ
                txtTen.Focus();
            }
            // Nếu có lỗi xảy ra, nhảy ngay vào đây để bắt lấy
            catch (Exception ex)
            {
                // Hiện thông báo lỗi lịch sự thay vì làm văng app
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Hệ thống báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedId == 0)
                {
                    MessageBox.Show("Vui lòng chọn một sản phẩm trong bảng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Hiện hộp thoại xác nhận trước khi xóa
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    string query = "DELETE FROM [Table] WHERE Id = @Id";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", selectedId);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Đã xóa sản phẩm khỏi kho!", "Thông báo");
                    LoadData();

                    selectedId = 0;
                    txtTen.Clear();
                    txtSoLuong.Clear();
                    txtGia.Clear();
                }
            }
            // Nếu có lỗi xảy ra, nhảy ngay vào đây để bắt lấy
            catch (Exception ex)
            {
                // Hiện thông báo lỗi lịch sự thay vì làm văng app
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Hệ thống báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Cú pháp LIKE trong SQL dùng để tìm kiếm chuỗi ký tự gần đúng
                string query = "SELECT * FROM [Table] WHERE Product_Name LIKE @Keyword";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Dùng SqlDataAdapter để lấy dữ liệu thay vì SqlCommand + ExecuteNonQuery
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        // Kẹp từ khóa vào giữa 2 dấu % để DB hiểu là tìm tên sản phẩm CÓ CHỨA từ khóa này
                        adapter.SelectCommand.Parameters.AddWithValue("@Keyword", "%" + txtTimKiem.Text + "%");

                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Đổ lại danh sách kết quả tìm được lên bảng
                        dgvSanPham.DataSource = dt;
                    }
                }
            }
            // Nếu có lỗi xảy ra, nhảy ngay vào đây để bắt lấy
            catch (Exception ex)
            {
                // Hiện thông báo lỗi lịch sự thay vì làm văng app
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Hệ thống báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra: Nếu ký tự vừa gõ KHÔNG PHẢI là số (IsDigit) 
            // và KHÔNG PHẢI là phím điều khiển như Backspace (IsControl)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Từ chối nhận ký tự này (chặn lại ngay trên bàn phím)
            }
        }

        private void txtGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Giá tiền thì cho phép nhập thêm 1 dấu chấm (.) để biểu diễn số thập phân
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Kiểm tra: Nếu đã có dấu chấm rồi thì không cho gõ thêm dấu chấm thứ 2
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Kiểm tra xem người dùng có click vào dòng dữ liệu hợp lệ không (tránh click vào thanh tiêu đề cột)
                if (e.RowIndex >= 0)
                {
                    // Lấy dòng đang được chọn
                    DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];

                    // Gán dữ liệu từ các cột tương ứng lên TextBox
                    // Cột 0 là Id, Cột 1 là Product_Name, Cột 2 là Quantity, Cột 3 là Price
                    selectedId = Convert.ToInt32(row.Cells[0].Value);
                    txtTen.Text = row.Cells[1].Value.ToString();
                    txtSoLuong.Text = row.Cells[2].Value.ToString();
                    txtGia.Text = row.Cells[3].Value.ToString();
                }
            }
            // Nếu có lỗi xảy ra, nhảy ngay vào đây để bắt lấy
            catch (Exception ex)
            {
                // Hiện thông báo lỗi lịch sự thay vì làm văng app
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Hệ thống báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra xem người dùng đã nhập đủ thông tin chưa
                if (string.IsNullOrWhiteSpace(txtTen.Text) || string.IsNullOrWhiteSpace(txtSoLuong.Text) || string.IsNullOrWhiteSpace(txtGia.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Dừng lại, không chạy code bên dưới nữa
                }
                // 2.Viết câu lệnh SQL INSERT
                // Dùng @Name, @Quantity, @Price (gọi là Parameter) để chống lỗi cú pháp và bảo mật (SQL Injection)
                string query = "INSERT INTO [Table] (Product_Name, Quantity, Price) VALUES (@Name, @Quantity, @Price)";
                // 3. Kết nối và thực thi
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Ép kiểu dữ liệu từ chữ (Text) sang số (int/float) để khớp với Database
                        cmd.Parameters.AddWithValue("@Name", txtTen.Text);
                        cmd.Parameters.AddWithValue("@Quantity", int.Parse(txtSoLuong.Text));
                        cmd.Parameters.AddWithValue("@Price", float.Parse(txtGia.Text));

                        conn.Open();
                        cmd.ExecuteNonQuery(); // Lệnh này dùng để chạy các câu query Thêm/Sửa/Xóa
                    }
                }
                // 4. Thông báo thành công và dọn dẹp
                MessageBox.Show("Đã thêm sản phẩm thành công!", "Thông báo");

                // Tải lại lưới dữ liệu để thấy sản phẩm mới vừa thêm
                LoadData();

                // Xóa trắng các ô TextBox để sẵn sàng nhập món mới
                txtTen.Clear();
                txtSoLuong.Clear();
                txtGia.Clear();
                // Nếu có lỗi xảy ra, nhảy ngay vào đây để bắt lấy
            }
            catch (Exception ex)
            {
                // Hiện thông báo lỗi lịch sự thay vì làm văng app
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Hệ thống báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Ràng buộc kiểm tra xem người dùng đã chọn sản phẩm để sửa chưa
                if (selectedId == 0)
                {
                    MessageBox.Show("Vui lòng chọn một sản phẩm trong bảng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Lệnh cập nhật dựa trên Id đã chọn
                string query = "UPDATE [Table] SET Product_Name = @Name, Quantity = @Quantity, Price = @Price WHERE Id = @Id";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", txtTen.Text);
                        cmd.Parameters.AddWithValue("@Quantity", int.Parse(txtSoLuong.Text));
                        cmd.Parameters.AddWithValue("@Price", float.Parse(txtGia.Text));
                        cmd.Parameters.AddWithValue("@Id", selectedId);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo");
                LoadData(); // Tải lại bảng để thấy thay đổi

                // Reset mọi thứ sau khi sửa xong
                selectedId = 0;
                txtTen.Clear();
                txtSoLuong.Clear();
                txtGia.Clear();
            }
            // Nếu có lỗi xảy ra, nhảy ngay vào đây để bắt lấy
            catch (Exception ex)
            {
                // Hiện thông báo lỗi lịch sự thay vì làm văng app
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Hệ thống báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}