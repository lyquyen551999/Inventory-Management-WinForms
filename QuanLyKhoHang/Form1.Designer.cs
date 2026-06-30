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
            button1 = new Button();
            button3 = new Button();
            button4 = new Button();
            txtTen = new TextBox();
            panel1 = new Panel();
            dgvSanPham = new DataGridView();
            txtTimKiem = new TextBox();
            panel2 = new Panel();
            button2 = new Button();
            txtSoLuong = new TextBox();
            txtGia = new TextBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSanPham).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.AllowDrop = true;
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button1.Location = new Point(15, 9);
            button1.Name = "button1";
            button1.Size = new Size(117, 35);
            button1.TabIndex = 2;
            button1.Text = "Add";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button3.Location = new Point(290, 9);
            button3.Name = "button3";
            button3.Size = new Size(117, 35);
            button3.TabIndex = 4;
            button3.Text = "Delete";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button4.Location = new Point(428, 9);
            button4.Name = "button4";
            button4.Size = new Size(117, 35);
            button4.TabIndex = 5;
            button4.Text = "Refresh";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // txtTen
            // 
            txtTen.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtTen.Location = new Point(15, 64);
            txtTen.Name = "txtTen";
            txtTen.PlaceholderText = "Product Name";
            txtTen.Size = new Size(170, 23);
            txtTen.TabIndex = 6;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(dgvSanPham);
            panel1.Controls.Add(txtTimKiem);
            panel1.Location = new Point(12, 8);
            panel1.Name = "panel1";
            panel1.Size = new Size(776, 230);
            panel1.TabIndex = 11;
            // 
            // dgvSanPham
            // 
            dgvSanPham.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvSanPham.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSanPham.Location = new Point(3, 89);
            dgvSanPham.Name = "dgvSanPham";
            dgvSanPham.Size = new Size(770, 138);
            dgvSanPham.TabIndex = 12;
            dgvSanPham.CellContentClick += dgvSanPham_CellContentClick;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtTimKiem.Location = new Point(586, 60);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.PlaceholderText = "Search";
            txtTimKiem.Size = new Size(187, 23);
            txtTimKiem.TabIndex = 11;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.Controls.Add(button2);
            panel2.Controls.Add(txtSoLuong);
            panel2.Controls.Add(txtGia);
            panel2.Controls.Add(txtTen);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(button4);
            panel2.Controls.Add(button3);
            panel2.Location = new Point(15, 256);
            panel2.Name = "panel2";
            panel2.Size = new Size(770, 182);
            panel2.TabIndex = 12;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button2.Location = new Point(155, 9);
            button2.Name = "button2";
            button2.Size = new Size(117, 35);
            button2.TabIndex = 12;
            button2.Text = "Update";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 
            // txtSoLuong
            // 
            txtSoLuong.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtSoLuong.Location = new Point(15, 105);
            txtSoLuong.Name = "txtSoLuong";
            txtSoLuong.PlaceholderText = "Quantity";
            txtSoLuong.Size = new Size(170, 23);
            txtSoLuong.TabIndex = 11;
            // 
            // txtGia
            // 
            txtGia.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtGia.Location = new Point(15, 147);
            txtGia.Name = "txtGia";
            txtGia.PlaceholderText = "Price";
            txtGia.Size = new Size(170, 23);
            txtGia.TabIndex = 10;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Product Management";
            Load += Form1_Load;
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
    }
}
