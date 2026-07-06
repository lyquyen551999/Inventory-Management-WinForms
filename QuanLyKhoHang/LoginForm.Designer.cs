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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            cbxlanguage = new ComboBox();
            panel1 = new Panel();
            lilbl_signup = new LinkLabel();
            lilbl_forgetpasswords = new LinkLabel();
            btn_login = new Button();
            txtb_password = new TextBox();
            txtb_id = new TextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // cbxlanguage
            // 
            resources.ApplyResources(cbxlanguage, "cbxlanguage");
            cbxlanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxlanguage.FormattingEnabled = true;
            cbxlanguage.Items.AddRange(new object[] { resources.GetString("cbxlanguage.Items"), resources.GetString("cbxlanguage.Items1"), resources.GetString("cbxlanguage.Items2") });
            cbxlanguage.Name = "cbxlanguage";
            cbxlanguage.SelectedIndexChanged += cbxlanguage_SelectedIndexChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(lilbl_signup);
            panel1.Controls.Add(lilbl_forgetpasswords);
            panel1.Controls.Add(btn_login);
            panel1.Controls.Add(txtb_password);
            panel1.Controls.Add(txtb_id);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // lilbl_signup
            // 
            resources.ApplyResources(lilbl_signup, "lilbl_signup");
            lilbl_signup.Name = "lilbl_signup";
            lilbl_signup.TabStop = true;
            lilbl_signup.LinkClicked += lilbl_signup_LinkClicked_1;
            // 
            // lilbl_forgetpasswords
            // 
            resources.ApplyResources(lilbl_forgetpasswords, "lilbl_forgetpasswords");
            lilbl_forgetpasswords.Name = "lilbl_forgetpasswords";
            lilbl_forgetpasswords.TabStop = true;
            lilbl_forgetpasswords.LinkClicked += lilbl_forgetpasswords_LinkClicked_1;
            // 
            // btn_login
            // 
            resources.ApplyResources(btn_login, "btn_login");
            btn_login.Name = "btn_login";
            btn_login.UseVisualStyleBackColor = true;
            btn_login.Click += btn_login_Click_1;
            // 
            // txtb_password
            // 
            resources.ApplyResources(txtb_password, "txtb_password");
            txtb_password.Name = "txtb_password";
            txtb_password.UseSystemPasswordChar = true;
            // 
            // txtb_id
            // 
            resources.ApplyResources(txtb_id, "txtb_id");
            txtb_id.Name = "txtb_id";
            // 
            // LoginForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Controls.Add(cbxlanguage);
            Name = "LoginForm";
            Load += LoginForm_Load;
            Shown += LoginForm_Shown;
            Resize += LoginForm_Resize;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private ComboBox cbxlanguage;
        private Panel panel1;
        private LinkLabel lilbl_signup;
        private LinkLabel lilbl_forgetpasswords;
        private Button btn_login;
        private TextBox txtb_password;
        private TextBox txtb_id;
    }
}