namespace QuanLyKhoHang
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            button3 = new Button();
            button4 = new Button();
            txtTen = new TextBox();
            panel1 = new Panel();
            dgvSanPham = new DataGridView();
            txtTimKiem = new TextBox();
            panel2 = new Panel();
            btn_logout = new Button();
            cbxlanguage = new ComboBox();
            button2 = new Button();
            txtSoLuong = new TextBox();
            txtGia = new TextBox();
            progressBar1 = new ProgressBar();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSanPham).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.AllowDrop = true;
            resources.ApplyResources(button1, "button1");
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button3
            // 
            resources.ApplyResources(button3, "button3");
            button3.Name = "button3";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            resources.ApplyResources(button4, "button4");
            button4.Name = "button4";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // txtTen
            // 
            resources.ApplyResources(txtTen, "txtTen");
            txtTen.Name = "txtTen";
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.Controls.Add(dgvSanPham);
            panel1.Controls.Add(txtTimKiem);
            panel1.Name = "panel1";
            // 
            // dgvSanPham
            // 
            resources.ApplyResources(dgvSanPham, "dgvSanPham");
            dgvSanPham.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSanPham.Name = "dgvSanPham";
            dgvSanPham.CellClick += dgvSanPham_CellClick;
            dgvSanPham.CellFormatting += dgvSanPham_CellFormatting;
            // 
            // txtTimKiem
            // 
            resources.ApplyResources(txtTimKiem, "txtTimKiem");
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            // 
            // panel2
            // 
            resources.ApplyResources(panel2, "panel2");
            panel2.Controls.Add(btn_logout);
            panel2.Controls.Add(cbxlanguage);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(txtSoLuong);
            panel2.Controls.Add(txtGia);
            panel2.Controls.Add(txtTen);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(button4);
            panel2.Controls.Add(button3);
            panel2.Name = "panel2";
            // 
            // btn_logout
            // 
            resources.ApplyResources(btn_logout, "btn_logout");
            btn_logout.Name = "btn_logout";
            btn_logout.UseVisualStyleBackColor = true;
            btn_logout.Click += btn_logout_Click;
            // 
            // cbxlanguage
            // 
            cbxlanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxlanguage.FormattingEnabled = true;
            cbxlanguage.Items.AddRange(new object[] { resources.GetString("cbxlanguage.Items"), resources.GetString("cbxlanguage.Items1"), resources.GetString("cbxlanguage.Items2") });
            resources.ApplyResources(cbxlanguage, "cbxlanguage");
            cbxlanguage.Name = "cbxlanguage";
            cbxlanguage.SelectedIndexChanged += cbxlanguage_SelectedIndexChanged;
            // 
            // button2
            // 
            resources.ApplyResources(button2, "button2");
            button2.Name = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // txtSoLuong
            // 
            resources.ApplyResources(txtSoLuong, "txtSoLuong");
            txtSoLuong.Name = "txtSoLuong";
            txtSoLuong.KeyPress += txtSoLuong_KeyPress;
            // 
            // txtGia
            // 
            resources.ApplyResources(txtGia, "txtGia");
            txtGia.Name = "txtGia";
            txtGia.KeyPress += txtGia_KeyPress;
            // 
            // progressBar1
            // 
            resources.ApplyResources(progressBar1, "progressBar1");
            progressBar1.Name = "progressBar1";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(progressBar1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "Form1";
            Load += Form1_Load;
            Shown += Form1_Shown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSanPham).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button button1;
        private Button button3;
        private Button button4;
        private TextBox txtTen;
        private Panel panel1;
        private DataGridView dgvSanPham;
        private TextBox txtTimKiem;
        private Panel panel2;
        private TextBox txtSoLuong;
        private TextBox txtGia;
        private Button button2;
        private ProgressBar progressBar1;
        private ComboBox cbxlanguage;
        private Button btn_logout;
    }
}
