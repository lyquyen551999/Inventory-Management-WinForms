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
            txtb_id = new TextBox();
            txtb_password = new TextBox();
            btn_login = new Button();
            cbxlanguage = new ComboBox();
            SuspendLayout();
            // 
            // txtb_id
            // 
            resources.ApplyResources(txtb_id, "txtb_id");
            txtb_id.Name = "txtb_id";
            // 
            // txtb_password
            // 
            resources.ApplyResources(txtb_password, "txtb_password");
            txtb_password.Name = "txtb_password";
            txtb_password.UseSystemPasswordChar = true;
            // 
            // btn_login
            // 
            resources.ApplyResources(btn_login, "btn_login");
            btn_login.Name = "btn_login";
            btn_login.UseVisualStyleBackColor = true;
            btn_login.Click += btn_login_Click;
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
            // LoginForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(cbxlanguage);
            Controls.Add(btn_login);
            Controls.Add(txtb_password);
            Controls.Add(txtb_id);
            Name = "LoginForm";
            Load += LoginForm_Load;
            Shown += LoginForm_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtb_id;
        private TextBox txtb_password;
        private Button btn_login;
        private ComboBox cbxlanguage;
    }
}