namespace QuanLyKhoHang
{
    partial class ForgotPasswordForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ForgotPasswordForm));
            panel1 = new Panel();
            btnRecover = new Button();
            lblForgotEmail = new Label();
            txtForgotEmail = new TextBox();
            lblForgotUser = new Label();
            txtForgotUser = new TextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnRecover);
            panel1.Controls.Add(lblForgotEmail);
            panel1.Controls.Add(txtForgotEmail);
            panel1.Controls.Add(lblForgotUser);
            panel1.Controls.Add(txtForgotUser);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // btnRecover
            // 
            resources.ApplyResources(btnRecover, "btnRecover");
            btnRecover.Name = "btnRecover";
            btnRecover.UseVisualStyleBackColor = true;
            btnRecover.Click += btnRecover_Click;
            // 
            // lblForgotEmail
            // 
            resources.ApplyResources(lblForgotEmail, "lblForgotEmail");
            lblForgotEmail.Name = "lblForgotEmail";
            // 
            // txtForgotEmail
            // 
            resources.ApplyResources(txtForgotEmail, "txtForgotEmail");
            txtForgotEmail.Name = "txtForgotEmail";
            // 
            // lblForgotUser
            // 
            resources.ApplyResources(lblForgotUser, "lblForgotUser");
            lblForgotUser.Name = "lblForgotUser";
            // 
            // txtForgotUser
            // 
            resources.ApplyResources(txtForgotUser, "txtForgotUser");
            txtForgotUser.Name = "txtForgotUser";
            // 
            // ForgotPasswordForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "ForgotPasswordForm";
            Load += ForgotPasswordForm_Load;
            Shown += ForgotPasswordForm_Shown;
            Resize += ForgotPasswordForm_Resize;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label lblForgotEmail;
        private TextBox txtForgotEmail;
        private Label lblForgotUser;
        private TextBox txtForgotUser;
        private Button btnRecover;
    }
}