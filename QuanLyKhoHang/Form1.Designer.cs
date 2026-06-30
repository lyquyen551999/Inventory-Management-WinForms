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
            dgvSanPham = new DataGridView();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            txtTen = new TextBox();
            txtSoLuong = new TextBox();
            txtGia = new TextBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            txtTimKiem = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvSanPham).BeginInit();
            SuspendLayout();
            // 
            // dgvSanPham
            // 
            dgvSanPham.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSanPham.Location = new Point(12, 87);
            dgvSanPham.Name = "dgvSanPham";
            dgvSanPham.Size = new Size(776, 150);
            dgvSanPham.TabIndex = 0;
            dgvSanPham.CellClick += dgvSanPham_CellClick;
            // 
            // button1
            // 
            button1.Location = new Point(12, 256);
            button1.Name = "button1";
            button1.Size = new Size(117, 35);
            button1.TabIndex = 2;
            button1.Text = "Add";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(179, 256);
            button2.Name = "button2";
            button2.Size = new Size(117, 35);
            button2.TabIndex = 3;
            button2.Text = "Update";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(352, 256);
            button3.Name = "button3";
            button3.Size = new Size(117, 35);
            button3.TabIndex = 4;
            button3.Text = "Delete";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(534, 256);
            button4.Name = "button4";
            button4.Size = new Size(117, 35);
            button4.TabIndex = 5;
            button4.Text = "Refresh";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // txtTen
            // 
            txtTen.Location = new Point(12, 314);
            txtTen.Name = "txtTen";
            txtTen.PlaceholderText = "Product Name";
            txtTen.Size = new Size(170, 23);
            txtTen.TabIndex = 6;
            // 
            // txtSoLuong
            // 
            txtSoLuong.Location = new Point(12, 362);
            txtSoLuong.Name = "txtSoLuong";
            txtSoLuong.PlaceholderText = "Quantity";
            txtSoLuong.Size = new Size(170, 23);
            txtSoLuong.TabIndex = 7;
            txtSoLuong.KeyPress += txtSoLuong_KeyPress;
            // 
            // txtGia
            // 
            txtGia.Location = new Point(12, 415);
            txtGia.Name = "txtGia";
            txtGia.PlaceholderText = "Price";
            txtGia.Size = new Size(170, 23);
            txtGia.TabIndex = 9;
            txtGia.KeyPress += txtGia_KeyPress;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(601, 49);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.PlaceholderText = "Search";
            txtTimKiem.Size = new Size(187, 23);
            txtTimKiem.TabIndex = 10;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtTimKiem);
            Controls.Add(txtGia);
            Controls.Add(txtSoLuong);
            Controls.Add(txtTen);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(dgvSanPham);
            Name = "Form1";
            Text = "Product Management";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvSanPham).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvSanPham;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private TextBox txtTen;
        private TextBox txtSoLuong;
        private TextBox txtGia;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private TextBox txtTimKiem;
    }
}
