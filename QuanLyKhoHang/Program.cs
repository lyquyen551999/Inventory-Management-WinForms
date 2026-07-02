namespace QuanLyKhoHang
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // 1. Khởi tạo màn hình đăng nhập
            LoginForm loginForm = new LoginForm();

            // 2. Mở nó lên dưới dạng hộp thoại (ShowDialog) và chờ người dùng thao tác
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // 3. Nếu LoginForm trả về kết quả OK (đăng nhập đúng), thì mới chạy Form1
                Application.Run(new Form1());
            }
            else
            {
                // 4. Nếu người dùng nhấn dấu X tắt form đăng nhập, thoát toàn bộ ứng dụng
                Application.Exit();
            }
        }
    }
}