using ClosedXML.Excel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms; // Đảm bảo có thư viện này cho Form
using System;
using System.Threading.Tasks;

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

        private void UpdateStatistics()
        {
            int totalProducts = 0;
            int totalQuantity = 0;
            double totalValue = 0;

            // Quét qua tất cả các dòng đang hiển thị trên bảng
            foreach (DataGridViewRow row in dgvSanPham.Rows)
            {
                // Bỏ qua dòng trống ở cuối bảng (nếu có)
                if (!row.IsNewRow && row.Cells[2].Value != null && row.Cells[3].Value != null)
                {
                    int qty = Convert.ToInt32(row.Cells[2].Value);
                    double price = Convert.ToDouble(row.Cells[3].Value);

                    totalProducts++;
                    totalQuantity += qty;
                    totalValue += (qty * price);
                }
            }

            // Hiển thị lên giao diện với định dạng số phân cách hàng ngàn (N0)
            lblTongMatHang.Text = totalProducts.ToString("N0");
            lblTongSoLuong.Text = totalQuantity.ToString("N0");

            // Xử lý hiển thị ký hiệu tiền tệ linh hoạt theo ngôn ngữ người dùng đang chọn
            if (LoginForm.SavedLanguage == "vi-VN")
            {
                lblTongGiaTri.Text = totalValue.ToString("N0") + " VNĐ";
            }
            else if (LoginForm.SavedLanguage == "zh-Hant")
            {
                lblTongGiaTri.Text = "NT$ " + totalValue.ToString("N0");
            }
            else
            {
                lblTongGiaTri.Text = "$ " + totalValue.ToString("N2");
            }
        }
        // Hàm hỗ trợ hiện thông báo lỗi bằng 3 ngôn ngữ
        private void ShowError(Exception ex)
        {
            string errorMsg = LoginForm.SavedLanguage == "vi-VN" ? "Lỗi hệ thống: " :
                              (LoginForm.SavedLanguage == "zh-Hant" ? "系統錯誤: " : "System Error: ");

            string errorTitle = LoginForm.SavedLanguage == "vi-VN" ? "Hệ thống báo lỗi" :
                                (LoginForm.SavedLanguage == "zh-Hant" ? "系統提示" : "Error");

            MessageBox.Show(errorMsg + ex.Message, errorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        //Viết hàm lấy dữ liệu từ Database đổ vào DataGridView
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

                    UpdateStatistics();
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
            this.ActiveControl = null;
            // Kiểm tra đảm bảo ComboBox đã có danh sách lựa chọn để tránh lỗi văng app
            if (cbxlanguage.Items.Count > 0)
            {
                // Đọc biến SavedLanguage từ LoginForm truyền sang
                string langToLoad = LoginForm.SavedLanguage;

                if (langToLoad == "vi-VN")
                {
                    cbxlanguage.SelectedIndex = 1; // Vị trí của "Tiếng Việt"
                }
                else if (langToLoad == "zh-Hant")
                {
                    cbxlanguage.SelectedIndex = 2; // Vị trí của "中文" 
                }
                else
                {
                    cbxlanguage.SelectedIndex = 0; // Vị trí của "English"
                }
            }
            LoadData();
        }
        // Hàm dùng chung nhận vào 2 tham số: Nút đang được bấm và Hành động cần làm
        private async Task ExecuteWithLoadingAsync(Button btn, Action dbAction)
        {
            try
            {
                // 1. Trạng thái bắt đầu
                progressBar1.Visible = true;
                progressBar1.Value = 0;
                if (btn != null) btn.Enabled = false;

                // 2. Chạy hiệu ứng thanh loading giả lập
                for (int i = 0; i <= 100; i += 10)
                {
                    progressBar1.Value = i;
                    await Task.Delay(30); // Tốc độ chạy (30ms)
                }

                // 3. Thực thi hành động thực tế được truyền vào (Load, Thêm, Sửa...)
                dbAction.Invoke();
            }
            catch (Exception ex)
            {
                // Gọi hàm báo lỗi đa ngôn ngữ mà chúng ta đã làm ở bước trước
                ShowError(ex);
            }
            finally
            {
                // 4. Trạng thái kết thúc (Luôn luôn chạy dù có lỗi hay không)
                progressBar1.Visible = false;
                progressBar1.Value = 0;
                if (btn != null) btn.Enabled = true;
            }
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra thông tin trống (Đa ngôn ngữ)
            if (string.IsNullOrWhiteSpace(txtTen.Text) || string.IsNullOrWhiteSpace(txtSoLuong.Text) || string.IsNullOrWhiteSpace(txtGia.Text))
            {
                string msgEmpty = LoginForm.SavedLanguage == "vi-VN" ? "Vui lòng nhập đầy đủ thông tin sản phẩm!" :
                                  (LoginForm.SavedLanguage == "zh-Hant" ? "請輸入完整的產品資訊！" : "Please enter full product information!");
                string titleMsg = LoginForm.SavedLanguage == "vi-VN" ? "Thông báo" :
                                  (LoginForm.SavedLanguage == "zh-Hant" ? "提示" : "Notification");

                MessageBox.Show(msgEmpty, titleMsg, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            await ExecuteWithLoadingAsync(sender as Button, () =>
            {
                try
                {
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
                    // 4. Thông báo thành công (Đa ngôn ngữ)
                    string msgSuccess = LoginForm.SavedLanguage == "vi-VN" ? "Đã thêm sản phẩm thành công!" :
                                        (LoginForm.SavedLanguage == "zh-Hant" ? "新增產品成功！" : "Product added successfully!");
                    string titleSuccess = LoginForm.SavedLanguage == "vi-VN" ? "Thông báo" :
                                        (LoginForm.SavedLanguage == "zh-Hant" ? "提示" : "Notification");

                    MessageBox.Show(msgSuccess, titleSuccess, MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                    // Gọi hàm báo lỗi đa ngôn ngữ mà chúng ta đã làm ở bước trước
                    ShowError(ex);
                }
            });

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra ID (Đa ngôn ngữ)
            if (selectedId == 0)
            {
                string msgSelect = LoginForm.SavedLanguage == "vi-VN" ? "Vui lòng chọn một sản phẩm trong bảng để sửa!" :
                                   (LoginForm.SavedLanguage == "zh-Hant" || LoginForm.SavedLanguage == "zh-Hant-TW" ? "請在表格中選擇要修改的產品！" : "Please select a product from the table to edit!");
                string titleSelect = LoginForm.SavedLanguage == "vi-VN" ? "Thông báo" : (LoginForm.SavedLanguage == "zh-Hant" || LoginForm.SavedLanguage == "zh-Hant-TW" ? "提示" : "Notification");

                MessageBox.Show(msgSelect, titleSelect, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            await ExecuteWithLoadingAsync(sender as Button, () =>
            {
                try
                {
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
                    // Thông báo cập nhật thành công (Đa ngôn ngữ)
                    string msgSuccess = LoginForm.SavedLanguage == "vi-VN" ? "Cập nhật thông tin thành công!" :
                                       (LoginForm.SavedLanguage == "zh-Hant" || LoginForm.SavedLanguage == "zh-Hant-TW" ? "更新資訊成功！" : "Information updated successfully!");
                    string titleSuccess = LoginForm.SavedLanguage == "vi-VN" ? "Thông báo" :
                                         (LoginForm.SavedLanguage == "zh-Hant" || LoginForm.SavedLanguage == "zh-Hant-TW" ? "提示" : "Notification");

                    MessageBox.Show(msgSuccess, titleSuccess, MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                    // Gọi hàm báo lỗi đa ngôn ngữ mà chúng ta đã làm ở bước trước
                    ShowError(ex);
                }
            });

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            // 1. Khai báo các biến chứa nội dung thông báo và tiêu đề
            string canhBaoChuaChon = "";
            string tieuDeCanhBao = "";
            string xacNhanXoa = "";
            string tieuDeXacNhan = "";
            string xoaThanhCong = "";
            string tieuDeThongBao = "";

            // 2. Gán nội dung chữ theo ngôn ngữ hệ thống đang lưu
            if (LoginForm.SavedLanguage == "vi-VN")
            {
                canhBaoChuaChon = "Vui lòng chọn một sản phẩm trong bảng để xóa!";
                tieuDeCanhBao = "Thông báo";
                xacNhanXoa = "Bạn có chắc chắn muốn xóa sản phẩm này không?";
                tieuDeXacNhan = "Xác nhận xóa";
                xoaThanhCong = "Đã xóa sản phẩm khỏi kho!";
                tieuDeThongBao = "Thông báo";
            }
            else if (LoginForm.SavedLanguage == "zh-Hant" || LoginForm.SavedLanguage == "zh-Hant-TW")
            {
                canhBaoChuaChon = "請在表格中選擇要刪除的產品！";
                tieuDeCanhBao = "提示";
                xacNhanXoa = "您確定要刪除此產品嗎？";
                tieuDeXacNhan = "確認刪除";
                xoaThanhCong = "產品 quấn 已從倉庫中刪除！";
                tieuDeThongBao = "提示";
            }
            else // Mặc định là English
            {
                canhBaoChuaChon = "Please select a product from the table to delete!";
                tieuDeCanhBao = "Warning";
                xacNhanXoa = "Are you sure you want to delete this product?";
                tieuDeXacNhan = "Confirm Delete";
                xoaThanhCong = "Product deleted successfully from inventory!";
                tieuDeThongBao = "Notification";
            }

            // 3. Kiểm tra điều kiện đầu vào
            if (selectedId == 0)
            {
                MessageBox.Show(canhBaoChuaChon, tieuDeCanhBao, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 4. Hiện hộp thoại xác nhận trước khi xóa
            DialogResult dialogResult = MessageBox.Show(xacNhanXoa, tieuDeXacNhan, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                await ExecuteWithLoadingAsync(sender as Button, () =>
                {
                    try
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
                    // Nếu có lỗi xảy ra, nhảy ngay vào đây để bắt lấy
                    catch (Exception ex)
                    {
                        // Gọi hàm báo lỗi đa ngôn ngữ mà chúng ta đã làm ở bước trước
                        ShowError(ex);
                    }
                });
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await ExecuteWithLoadingAsync(sender as Button, () =>
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
                    // Gọi hàm báo lỗi đa ngôn ngữ mà chúng ta đã làm ở bước trước
                    ShowError(ex);
                }
            });
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // 1. Lấy nội dung tìm kiếm
                string inputText = txtTimKiem.Text.Trim();

                // Nếu người dùng xóa hết chữ trong ô tìm kiếm -> Tải lại toàn bộ dữ liệu ban đầu
                if (string.IsNullOrEmpty(inputText))
                {
                    LoadData();
                    return;
                }

                // 2. Tách chuỗi dựa vào dấu phẩy (,), tự động bỏ qua các khoảng trắng thừa
                string[] keywords = inputText.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                // 3. Khởi tạo câu lệnh SQL gốc (Mẹo: 1=1 luôn đúng, dùng để dễ dàng cộng dồn chữ AND ở dưới)
                string query = "SELECT * FROM [Table] WHERE 1=1 ";

                // 4. Vòng lặp xây dựng câu lệnh SQL động cho từng từ khóa
                for (int i = 0; i < keywords.Length; i++)
                {
                    // Yêu cầu: 1 từ khóa có thể nằm ở Tên, HOẶC Số lượng, HOẶC Giá.
                    // Dùng CAST(...) để ép kiểu cột Số (int, float) sang Chữ (NVARCHAR) để xài được lệnh LIKE
                    query += $" AND (Product_Name LIKE @kw{i} OR CAST(Quantity AS NVARCHAR(50)) LIKE @kw{i} OR CAST(Price AS NVARCHAR(50)) LIKE @kw{i})";
                }

                // 5. Kết nối DB và nhồi dữ liệu thực tế vào các tham số @kw
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        for (int i = 0; i < keywords.Length; i++)
                        {
                            // Thêm dấu % vào 2 đầu để tìm kiếm chứa đựng (Contains)
                            string kw = "%" + keywords[i].Trim() + "%";
                            adapter.SelectCommand.Parameters.AddWithValue($"@kw{i}", kw);
                        }

                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Đổ danh sách kết quả lên bảng
                        dgvSanPham.DataSource = dt;

                        UpdateStatistics();
                    }
                }
            }
            catch (Exception ex)
            {
                // Gọi hàm báo lỗi đa ngôn ngữ mà chúng ta đã làm ở bước trước
                ShowError(ex);
            }
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }
        private void ChangeLanguage(string cultureName)
        {
            // Cập nhật lại biến SavedLanguage để các hộp thoại MessageBox nghe theo ngôn ngữ mới
            LoginForm.SavedLanguage = cultureName;

            // Ghi đè ngôn ngữ mới nhất vào file cấu hình mỗi khi người dùng đổi lựa chọn
            File.WriteAllText("lang.txt", cultureName);

            // Đặt văn hóa (Culture) hiện tại của luồng chạy thành ngôn ngữ mới
            CultureInfo culture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            // Gọi công cụ quản lý Resource
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));

            // Áp dụng chữ mới cho bản thân cái Form (như tiêu đề Form)
            resources.ApplyResources(this, "$this");

            // Quét và áp dụng chữ mới cho tất cả các control (nút bấm, nhãn dán, v.v.)
            ApplyResourcesToControls(this.Controls, resources);
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
            UpdateStatistics();
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            // 1. Khai báo 2 biến để chứa nội dung thông báo
            string thongBao = "";
            string tieuDe = "";

            // 2. Kiểm tra xem hệ thống đang chạy ngôn ngữ nào để gắn chữ tương ứng
            if (LoginForm.SavedLanguage == "vi-VN")
            {
                thongBao = "Bạn có chắc chắn muốn đăng xuất khỏi hệ thống?";
                tieuDe = "Xác nhận đăng xuất";
            }
            else if (LoginForm.SavedLanguage == "zh-Hant" || LoginForm.SavedLanguage == "zh-Hant-TW")
            {
                thongBao = "您確定要登出系統嗎？";
                tieuDe = "確認登出";
            }
            else
            {
                thongBao = "Are you sure you want to log out of the system?";
                tieuDe = "Confirm Logout";
            }

            // 3. Hiện hộp thoại xác nhận bằng chính ngôn ngữ đã được lọc ở trên
            DialogResult result = MessageBox.Show(thongBao, tieuDe, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // 4. Xử lý khi người dùng chọn Yes (Đồng ý)
            if (result == DialogResult.Yes)
            {
                // Khởi động lại toàn bộ ứng dụng, dọn sạch bộ nhớ
                Application.Restart();
                Environment.Exit(0);
            }
        }

        private void dgvSanPham_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 1. Xác định đúng cột Số lượng để xử lý (Giả sử cột Quantity của bạn nằm ở vị trí index = 2)
            // Bạn có thể kiểm tra bằng tên cột: dgvSanPham.Columns[e.ColumnIndex].Name == "Quantity"
            if (e.ColumnIndex == 2)
            {
                // Kiểm tra đảm bảo ô đó có dữ liệu thực tế
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    // Ép kiểu giá trị trong ô sang số nguyên để so sánh
                    int quantity = Convert.ToInt32(e.Value);

                    // THẾ GIỚI THỰC: Không dùng màu đỏ tươi chói mắt, hãy dùng màu dịu chuyên nghiệp (Soft Colors)

                    // Trường hợp 1: Nguy cấp - Tồn kho từ 5 trở xuống (Báo động đỏ)
                    if (quantity <= 5)
                    {
                        e.CellStyle.BackColor = Color.MistyRose;     // Màu nền hồng nhạt
                        e.CellStyle.ForeColor = Color.Red;           // Màu chữ đỏ
                        e.CellStyle.Font = new Font(dgvSanPham.Font, FontStyle.Bold); // In đậm con số
                    }
                    // Trường hợp 2: Cần chú ý - Tồn kho từ 6 đến 15 (Cảnh báo vàng)
                    else if (quantity <= 15)
                    {
                        e.CellStyle.BackColor = Color.LightYellow;    // Màu nền vàng nhạt
                        e.CellStyle.ForeColor = Color.DarkGoldenrod; // Màu chữ vàng đậm/nâu
                    }
                }
            }
        }

        private async void btn_excel_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem bảng có dữ liệu không, nếu trống thì thông báo theo đúng ngôn ngữ
            if (dgvSanPham.Rows.Count == 0)
            {
                string msgTrong = LoginForm.SavedLanguage == "vi-VN" ? "Không có dữ liệu để xuất!" :
                                  (LoginForm.SavedLanguage == "zh-Hant" || LoginForm.SavedLanguage == "zh-Hant-TW" ? "沒有資料可匯出！" : "No data to export!");
                string titleTrong = LoginForm.SavedLanguage == "vi-VN" ? "Thông báo" :
                                    (LoginForm.SavedLanguage == "zh-Hant" || LoginForm.SavedLanguage == "zh-Hant-TW" ? "提示" : "Warning");

                MessageBox.Show(msgTrong, titleTrong, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Cấu hình hộp thoại lưu file (SaveFileDialog)
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Workbook|*.xlsx";
            sfd.FileName = "BaoCaoKhoHang.xlsx"; // Tên file mặc định khi lưu
            sfd.Title = LoginForm.SavedLanguage == "vi-VN" ? "Lưu báo cáo kho hàng" :
                        (LoginForm.SavedLanguage == "zh-Hant" || LoginForm.SavedLanguage == "zh-Hant-TW" ? "儲存庫存報告" : "Save Inventory Report");

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                await ExecuteWithLoadingAsync(sender as Button, () =>
                {
                    try
                    {
                        // Tạo một file Excel mới tinh trong bộ nhớ
                        using (XLWorkbook workbook = new XLWorkbook())
                        {
                            // Tạo một trang tính (Sheet) tên là "KhoHang"
                            var worksheet = workbook.Worksheets.Add("KhoHang");

                            // ĐƠN VỊ DOANH NGHIỆP: Quét và xuất tiêu đề cột theo đúng ngôn ngữ hiển thị trên UI
                            for (int i = 0; i < dgvSanPham.Columns.Count; i++)
                            {
                                worksheet.Cell(1, i + 1).Value = dgvSanPham.Columns[i].HeaderText;

                                // Trang trí dòng tiêu đề cho đẹp và chuyên nghiệp
                                worksheet.Cell(1, i + 1).Style.Font.Bold = true; // In đậm
                                worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightGray; // Nền xám nhạt
                                worksheet.Cell(1, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; // Căn giữa
                            }

                            // 3. Quét từng dòng, từng ô trên bảng để đổ dữ liệu vào Excel
                            for (int r = 0; r < dgvSanPham.Rows.Count; r++)
                            {
                                for (int c = 0; c < dgvSanPham.Columns.Count; c++)
                                {
                                    var cellValue = dgvSanPham.Rows[r].Cells[c].Value;

                                    // Đổ giá trị vào ô tương ứng (Dòng dữ liệu Excel bắt đầu từ dòng số 2)
                                    worksheet.Cell(r + 2, c + 1).Value = cellValue != null ? cellValue.ToString() : "";
                                }
                            }

                            // Lệnh tinh tế: Tự động co giãn độ rộng của các cột trong Excel cho vừa khít với chữ
                            worksheet.Columns().AdjustToContents();


                            // 4. Tiến hành lưu file thực tế xuống ổ đĩa
                            workbook.SaveAs(sfd.FileName);


                        }

                        // 5. Hiện thông báo thành công đa ngôn ngữ
                        string successMsg = LoginForm.SavedLanguage == "vi-VN" ? "Xuất file Excel thành công!" :
                                           (LoginForm.SavedLanguage == "zh-Hant" || LoginForm.SavedLanguage == "zh-Hant-TW" ? "匯出 Excel 成功！" : "Exported to Excel successfully!");
                        string successTitle = LoginForm.SavedLanguage == "vi-VN" ? "Hoàn tất" :
                                             (LoginForm.SavedLanguage == "zh-Hant" || LoginForm.SavedLanguage == "zh-Hant-TW" ? "完成" : "Success");

                        MessageBox.Show(successMsg, successTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        // Gọi hàm báo lỗi đa ngôn ngữ mà chúng ta đã làm ở bước trước
                        ShowError(ex);
                    }
                });
            }
        }
    }
}
