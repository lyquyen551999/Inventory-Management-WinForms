namespace QuanLyKhoHang
{
    partial class RegisterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterForm));
            panel1 = new Panel();
            btnRegister = new Button();
            lblEmail = new Label();
            txtRegEmail = new TextBox();
            lblPass = new Label();
            txtRegPass = new TextBox();
            lblUser = new Label();
            txtRegUser = new TextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnRegister);
            panel1.Controls.Add(lblEmail);
            panel1.Controls.Add(txtRegEmail);
            panel1.Controls.Add(lblPass);
            panel1.Controls.Add(txtRegPass);
            panel1.Controls.Add(lblUser);
            panel1.Controls.Add(txtRegUser);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // btnRegister
            // 
            resources.ApplyResources(btnRegister, "btnRegister");
            btnRegister.Name = "btnRegister";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // lblEmail
            // 
            resources.ApplyResources(lblEmail, "lblEmail");
            lblEmail.Name = "lblEmail";
            // 
            // txtRegEmail
            // 
            resources.ApplyResources(txtRegEmail, "txtRegEmail");
            txtRegEmail.Name = "txtRegEmail";
            // 
            // lblPass
            // 
            resources.ApplyResources(lblPass, "lblPass");
            lblPass.Name = "lblPass";
            // 
            // txtRegPass
            // 
            resources.ApplyResources(txtRegPass, "txtRegPass");
            txtRegPass.Name = "txtRegPass";
            // 
            // lblUser
            // 
            resources.ApplyResources(lblUser, "lblUser");
            lblUser.Name = "lblUser";
            // 
            // txtRegUser
            // 
            resources.ApplyResources(txtRegUser, "txtRegUser");
            txtRegUser.Name = "txtRegUser";
            // 
            // RegisterForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "RegisterForm";
            Load += RegisterForm_Load;
            Shown += RegisterForm_Shown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label lblUser;
        private TextBox txtRegUser;
        private Button btnRegister;
        private Label lblEmail;
        private TextBox txtRegEmail;
        private Label lblPass;
        private TextBox txtRegPass;
    }
}