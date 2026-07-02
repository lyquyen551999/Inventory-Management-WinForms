namespace QuanLyKhoHang
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtb_id = new TextBox();
            txtb_password = new TextBox();
            btn_login = new Button();
            SuspendLayout();
            // 
            // txtb_id
            // 
            txtb_id.Location = new Point(282, 217);
            txtb_id.Name = "txtb_id";
            txtb_id.PlaceholderText = "User ID";
            txtb_id.Size = new Size(235, 23);
            txtb_id.TabIndex = 0;
            // 
            // txtb_password
            // 
            txtb_password.Location = new Point(282, 256);
            txtb_password.Name = "txtb_password";
            txtb_password.PlaceholderText = "Passwords";
            txtb_password.Size = new Size(235, 23);
            txtb_password.TabIndex = 1;
            txtb_password.UseSystemPasswordChar = true;
            // 
            // btn_login
            // 
            btn_login.Location = new Point(360, 296);
            btn_login.Name = "btn_login";
            btn_login.Size = new Size(75, 23);
            btn_login.TabIndex = 2;
            btn_login.Text = "Login";
            btn_login.UseVisualStyleBackColor = true;
            btn_login.Click += btn_login_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btn_login);
            Controls.Add(txtb_password);
            Controls.Add(txtb_id);
            Name = "LoginForm";
            Text = "Login Window";
            Shown += LoginForm_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtb_id;
        private TextBox txtb_password;
        private Button btn_login;
    }
}