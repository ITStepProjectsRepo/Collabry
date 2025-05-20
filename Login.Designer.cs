namespace Collabry
{
    partial class Login
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
            this.logPan = new System.Windows.Forms.Panel();
            this.regPan = new System.Windows.Forms.Panel();
            this.loginBtn = new System.Windows.Forms.Button();
            this.signinBtn = new System.Windows.Forms.Button();
            this.SignLogin = new System.Windows.Forms.TextBox();
            this.SignPass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.PassTxtBox = new System.Windows.Forms.TextBox();
            this.LoginTxtBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.NameTxtBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.TagTxtBox = new System.Windows.Forms.TextBox();
            this.politicsChkBox = new System.Windows.Forms.CheckBox();
            this.RegSignIn = new System.Windows.Forms.Button();
            this.logPan.SuspendLayout();
            this.regPan.SuspendLayout();
            this.SuspendLayout();
            // 
            // logPan
            // 
            this.logPan.BackColor = System.Drawing.SystemColors.ControlLight;
            this.logPan.Controls.Add(this.label3);
            this.logPan.Controls.Add(this.label2);
            this.logPan.Controls.Add(this.label1);
            this.logPan.Controls.Add(this.SignPass);
            this.logPan.Controls.Add(this.SignLogin);
            this.logPan.Controls.Add(this.signinBtn);
            this.logPan.Controls.Add(this.loginBtn);
            this.logPan.Location = new System.Drawing.Point(12, 12);
            this.logPan.Name = "logPan";
            this.logPan.Size = new System.Drawing.Size(383, 426);
            this.logPan.TabIndex = 0;
            // 
            // regPan
            // 
            this.regPan.BackColor = System.Drawing.Color.LightGray;
            this.regPan.Controls.Add(this.RegSignIn);
            this.regPan.Controls.Add(this.politicsChkBox);
            this.regPan.Controls.Add(this.label9);
            this.regPan.Controls.Add(this.TagTxtBox);
            this.regPan.Controls.Add(this.label8);
            this.regPan.Controls.Add(this.NameTxtBox);
            this.regPan.Controls.Add(this.label7);
            this.regPan.Controls.Add(this.label6);
            this.regPan.Controls.Add(this.label4);
            this.regPan.Controls.Add(this.label5);
            this.regPan.Controls.Add(this.PassTxtBox);
            this.regPan.Controls.Add(this.LoginTxtBox);
            this.regPan.Location = new System.Drawing.Point(401, 12);
            this.regPan.Name = "regPan";
            this.regPan.Size = new System.Drawing.Size(383, 426);
            this.regPan.TabIndex = 1;
            this.regPan.Visible = false;
            // 
            // loginBtn
            // 
            this.loginBtn.Location = new System.Drawing.Point(44, 242);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(75, 23);
            this.loginBtn.TabIndex = 0;
            this.loginBtn.Text = "Log In";
            this.loginBtn.UseVisualStyleBackColor = true;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // signinBtn
            // 
            this.signinBtn.Location = new System.Drawing.Point(187, 242);
            this.signinBtn.Name = "signinBtn";
            this.signinBtn.Size = new System.Drawing.Size(75, 23);
            this.signinBtn.TabIndex = 1;
            this.signinBtn.Text = "Sign In";
            this.signinBtn.UseVisualStyleBackColor = true;
            this.signinBtn.Click += new System.EventHandler(this.signinBtn_Click);
            // 
            // SignLogin
            // 
            this.SignLogin.Location = new System.Drawing.Point(124, 138);
            this.SignLogin.Multiline = true;
            this.SignLogin.Name = "SignLogin";
            this.SignLogin.Size = new System.Drawing.Size(138, 24);
            this.SignLogin.TabIndex = 2;
            // 
            // SignPass
            // 
            this.SignPass.Location = new System.Drawing.Point(124, 172);
            this.SignPass.Multiline = true;
            this.SignPass.Name = "SignPass";
            this.SignPass.PasswordChar = '*';
            this.SignPass.Size = new System.Drawing.Size(138, 24);
            this.SignPass.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cooper Black", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(80, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Collabry (test)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(40, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Login";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(40, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(69, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label5.Location = new System.Drawing.Point(69, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Login";
            // 
            // PassTxtBox
            // 
            this.PassTxtBox.Location = new System.Drawing.Point(160, 162);
            this.PassTxtBox.Multiline = true;
            this.PassTxtBox.Name = "PassTxtBox";
            this.PassTxtBox.PasswordChar = '*';
            this.PassTxtBox.Size = new System.Drawing.Size(138, 24);
            this.PassTxtBox.TabIndex = 8;
            // 
            // LoginTxtBox
            // 
            this.LoginTxtBox.Location = new System.Drawing.Point(160, 128);
            this.LoginTxtBox.Multiline = true;
            this.LoginTxtBox.Name = "LoginTxtBox";
            this.LoginTxtBox.Size = new System.Drawing.Size(138, 24);
            this.LoginTxtBox.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Cooper Black", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(110, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(167, 24);
            this.label6.TabIndex = 11;
            this.label6.Text = "Collabry (test)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Cooper Black", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(151, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 24);
            this.label7.TabIndex = 12;
            this.label7.Text = "Sign In";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(69, 226);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 20);
            this.label8.TabIndex = 14;
            this.label8.Text = "Tag";
            // 
            // NameTxtBox
            // 
            this.NameTxtBox.Location = new System.Drawing.Point(160, 192);
            this.NameTxtBox.Multiline = true;
            this.NameTxtBox.Name = "NameTxtBox";
            this.NameTxtBox.Size = new System.Drawing.Size(138, 24);
            this.NameTxtBox.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(69, 196);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 20);
            this.label9.TabIndex = 16;
            this.label9.Text = "UserName";
            // 
            // TagTxtBox
            // 
            this.TagTxtBox.Location = new System.Drawing.Point(160, 222);
            this.TagTxtBox.Multiline = true;
            this.TagTxtBox.Name = "TagTxtBox";
            this.TagTxtBox.Size = new System.Drawing.Size(138, 24);
            this.TagTxtBox.TabIndex = 15;
            // 
            // politicsChkBox
            // 
            this.politicsChkBox.AutoSize = true;
            this.politicsChkBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.politicsChkBox.Location = new System.Drawing.Point(54, 336);
            this.politicsChkBox.Name = "politicsChkBox";
            this.politicsChkBox.Size = new System.Drawing.Size(300, 17);
            this.politicsChkBox.TabIndex = 25;
            this.politicsChkBox.Text = "By checking, I agree to Collabry Politics of work in Internet";
            this.politicsChkBox.UseVisualStyleBackColor = true;
            this.politicsChkBox.CheckedChanged += new System.EventHandler(this.politicsChkBox_CheckedChanged);
            // 
            // RegSignIn
            // 
            this.RegSignIn.Enabled = false;
            this.RegSignIn.Location = new System.Drawing.Point(134, 375);
            this.RegSignIn.Name = "RegSignIn";
            this.RegSignIn.Size = new System.Drawing.Size(116, 23);
            this.RegSignIn.TabIndex = 26;
            this.RegSignIn.Text = "Sign In and Begin";
            this.RegSignIn.UseVisualStyleBackColor = true;
            this.RegSignIn.Click += new System.EventHandler(this.RegSignIn_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 449);
            this.Controls.Add(this.regPan);
            this.Controls.Add(this.logPan);
            this.MinimumSize = new System.Drawing.Size(420, 488);
            this.Name = "Login";
            this.Text = "Login";
            this.logPan.ResumeLayout(false);
            this.logPan.PerformLayout();
            this.regPan.ResumeLayout(false);
            this.regPan.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel logPan;
        private System.Windows.Forms.Panel regPan;
        private System.Windows.Forms.TextBox SignPass;
        private System.Windows.Forms.TextBox SignLogin;
        private System.Windows.Forms.Button signinBtn;
        private System.Windows.Forms.Button loginBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox PassTxtBox;
        private System.Windows.Forms.TextBox LoginTxtBox;
        private System.Windows.Forms.Button RegSignIn;
        private System.Windows.Forms.CheckBox politicsChkBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox TagTxtBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox NameTxtBox;
    }
}