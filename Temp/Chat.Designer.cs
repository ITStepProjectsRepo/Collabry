namespace Collabry
{
    partial class Chat
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
            this.MsgListBox = new System.Windows.Forms.ListBox();
            this.InputTxtBox = new System.Windows.Forms.TextBox();
            this.SendBtn = new System.Windows.Forms.Button();
            this.OtherUserName = new System.Windows.Forms.Label();
            this.CallBtn = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageContactsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChatPanel = new System.Windows.Forms.Panel();
            this.ContactsListBox = new System.Windows.Forms.ListBox();
            this.SettingPanel = new System.Windows.Forms.Panel();
            this.MngCntPanel = new System.Windows.Forms.Panel();
            this.SaveData = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.NameTxtBox = new System.Windows.Forms.TextBox();
            this.LoginTxtBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PassTxtBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TagTxtBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.AboutTxtBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsLstBox = new System.Windows.Forms.ListBox();
            this.InviteTxtBox = new System.Windows.Forms.TextBox();
            this.AccInvBtn = new System.Windows.Forms.Button();
            this.DelUBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.MuteUBtn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.UNLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.UTLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.ChatPanel.SuspendLayout();
            this.SettingPanel.SuspendLayout();
            this.MngCntPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MsgListBox
            // 
            this.MsgListBox.FormattingEnabled = true;
            this.MsgListBox.Location = new System.Drawing.Point(220, 42);
            this.MsgListBox.Name = "MsgListBox";
            this.MsgListBox.Size = new System.Drawing.Size(401, 329);
            this.MsgListBox.TabIndex = 0;
            // 
            // InputTxtBox
            // 
            this.InputTxtBox.Location = new System.Drawing.Point(220, 377);
            this.InputTxtBox.Multiline = true;
            this.InputTxtBox.Name = "InputTxtBox";
            this.InputTxtBox.Size = new System.Drawing.Size(341, 32);
            this.InputTxtBox.TabIndex = 2;
            // 
            // SendBtn
            // 
            this.SendBtn.Location = new System.Drawing.Point(570, 377);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(51, 32);
            this.SendBtn.TabIndex = 3;
            this.SendBtn.Text = "Send";
            this.SendBtn.UseVisualStyleBackColor = true;
            this.SendBtn.Click += new System.EventHandler(this.SendBtn_Click);
            // 
            // OtherUserName
            // 
            this.OtherUserName.AutoSize = true;
            this.OtherUserName.Location = new System.Drawing.Point(223, 19);
            this.OtherUserName.Name = "OtherUserName";
            this.OtherUserName.Size = new System.Drawing.Size(35, 13);
            this.OtherUserName.TabIndex = 4;
            this.OtherUserName.Text = "label1";
            // 
            // CallBtn
            // 
            this.CallBtn.Location = new System.Drawing.Point(579, 0);
            this.CallBtn.Name = "CallBtn";
            this.CallBtn.Size = new System.Drawing.Size(42, 32);
            this.CallBtn.TabIndex = 6;
            this.CallBtn.Text = "Call";
            this.CallBtn.UseVisualStyleBackColor = true;
            this.CallBtn.Click += new System.EventHandler(this.CallBtn_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programToolStripMenuItem,
            this.otherToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(634, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // programToolStripMenuItem
            // 
            this.programToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chatToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.manageContactsToolStripMenuItem});
            this.programToolStripMenuItem.Name = "programToolStripMenuItem";
            this.programToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.programToolStripMenuItem.Text = "Program";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // manageContactsToolStripMenuItem
            // 
            this.manageContactsToolStripMenuItem.Name = "manageContactsToolStripMenuItem";
            this.manageContactsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.manageContactsToolStripMenuItem.Text = "Manage Contacts";
            this.manageContactsToolStripMenuItem.Click += new System.EventHandler(this.manageContactsToolStripMenuItem_Click);
            // 
            // otherToolStripMenuItem
            // 
            this.otherToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.otherToolStripMenuItem.Name = "otherToolStripMenuItem";
            this.otherToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.otherToolStripMenuItem.Text = "Other";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // ChatPanel
            // 
            this.ChatPanel.Controls.Add(this.ContactsListBox);
            this.ChatPanel.Controls.Add(this.SendBtn);
            this.ChatPanel.Controls.Add(this.CallBtn);
            this.ChatPanel.Controls.Add(this.OtherUserName);
            this.ChatPanel.Controls.Add(this.MsgListBox);
            this.ChatPanel.Controls.Add(this.InputTxtBox);
            this.ChatPanel.Location = new System.Drawing.Point(0, 27);
            this.ChatPanel.Name = "ChatPanel";
            this.ChatPanel.Size = new System.Drawing.Size(633, 424);
            this.ChatPanel.TabIndex = 8;
            // 
            // ContactsListBox
            // 
            this.ContactsListBox.FormattingEnabled = true;
            this.ContactsListBox.Location = new System.Drawing.Point(6, 4);
            this.ContactsListBox.Name = "ContactsListBox";
            this.ContactsListBox.Size = new System.Drawing.Size(208, 407);
            this.ContactsListBox.TabIndex = 7;
            this.ContactsListBox.SelectedIndexChanged += new System.EventHandler(this.ContactsListBox_SelectedIndexChanged);
            // 
            // SettingPanel
            // 
            this.SettingPanel.Controls.Add(this.AboutTxtBox);
            this.SettingPanel.Controls.Add(this.label5);
            this.SettingPanel.Controls.Add(this.TagTxtBox);
            this.SettingPanel.Controls.Add(this.label4);
            this.SettingPanel.Controls.Add(this.PassTxtBox);
            this.SettingPanel.Controls.Add(this.label3);
            this.SettingPanel.Controls.Add(this.LoginTxtBox);
            this.SettingPanel.Controls.Add(this.label2);
            this.SettingPanel.Controls.Add(this.NameTxtBox);
            this.SettingPanel.Controls.Add(this.label1);
            this.SettingPanel.Controls.Add(this.SaveData);
            this.SettingPanel.Location = new System.Drawing.Point(639, 27);
            this.SettingPanel.Name = "SettingPanel";
            this.SettingPanel.Size = new System.Drawing.Size(633, 424);
            this.SettingPanel.TabIndex = 9;
            // 
            // MngCntPanel
            // 
            this.MngCntPanel.Controls.Add(this.UTLabel);
            this.MngCntPanel.Controls.Add(this.label9);
            this.MngCntPanel.Controls.Add(this.UNLabel);
            this.MngCntPanel.Controls.Add(this.label7);
            this.MngCntPanel.Controls.Add(this.MuteUBtn);
            this.MngCntPanel.Controls.Add(this.label6);
            this.MngCntPanel.Controls.Add(this.DelUBtn);
            this.MngCntPanel.Controls.Add(this.AccInvBtn);
            this.MngCntPanel.Controls.Add(this.InviteTxtBox);
            this.MngCntPanel.Controls.Add(this.SettingsLstBox);
            this.MngCntPanel.Location = new System.Drawing.Point(639, 457);
            this.MngCntPanel.Name = "MngCntPanel";
            this.MngCntPanel.Size = new System.Drawing.Size(633, 424);
            this.MngCntPanel.TabIndex = 10;
            // 
            // SaveData
            // 
            this.SaveData.Location = new System.Drawing.Point(544, 388);
            this.SaveData.Name = "SaveData";
            this.SaveData.Size = new System.Drawing.Size(75, 23);
            this.SaveData.TabIndex = 0;
            this.SaveData.Text = "Save";
            this.SaveData.UseVisualStyleBackColor = true;
            this.SaveData.Click += new System.EventHandler(this.SaveData_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(121, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "UserName";
            // 
            // NameTxtBox
            // 
            this.NameTxtBox.Location = new System.Drawing.Point(231, 67);
            this.NameTxtBox.Name = "NameTxtBox";
            this.NameTxtBox.Size = new System.Drawing.Size(196, 20);
            this.NameTxtBox.TabIndex = 2;
            // 
            // LoginTxtBox
            // 
            this.LoginTxtBox.Enabled = false;
            this.LoginTxtBox.Location = new System.Drawing.Point(231, 148);
            this.LoginTxtBox.Name = "LoginTxtBox";
            this.LoginTxtBox.Size = new System.Drawing.Size(196, 20);
            this.LoginTxtBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(121, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Email";
            // 
            // PassTxtBox
            // 
            this.PassTxtBox.Location = new System.Drawing.Point(231, 189);
            this.PassTxtBox.Name = "PassTxtBox";
            this.PassTxtBox.Size = new System.Drawing.Size(196, 20);
            this.PassTxtBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(121, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password";
            // 
            // TagTxtBox
            // 
            this.TagTxtBox.Location = new System.Drawing.Point(231, 106);
            this.TagTxtBox.Name = "TagTxtBox";
            this.TagTxtBox.Size = new System.Drawing.Size(196, 20);
            this.TagTxtBox.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(121, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "UserTag";
            // 
            // AboutTxtBox
            // 
            this.AboutTxtBox.Location = new System.Drawing.Point(231, 231);
            this.AboutTxtBox.Multiline = true;
            this.AboutTxtBox.Name = "AboutTxtBox";
            this.AboutTxtBox.Size = new System.Drawing.Size(196, 102);
            this.AboutTxtBox.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(121, 234);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "About Me";
            // 
            // chatToolStripMenuItem
            // 
            this.chatToolStripMenuItem.Name = "chatToolStripMenuItem";
            this.chatToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.chatToolStripMenuItem.Text = "Chat";
            this.chatToolStripMenuItem.Click += new System.EventHandler(this.chatToolStripMenuItem_Click);
            // 
            // SettingsLstBox
            // 
            this.SettingsLstBox.FormattingEnabled = true;
            this.SettingsLstBox.Location = new System.Drawing.Point(15, 12);
            this.SettingsLstBox.Name = "SettingsLstBox";
            this.SettingsLstBox.Size = new System.Drawing.Size(208, 407);
            this.SettingsLstBox.TabIndex = 8;
            this.SettingsLstBox.SelectedIndexChanged += new System.EventHandler(this.SettingsLstBox_SelectedIndexChanged);
            // 
            // InviteTxtBox
            // 
            this.InviteTxtBox.Location = new System.Drawing.Point(349, 50);
            this.InviteTxtBox.Multiline = true;
            this.InviteTxtBox.Name = "InviteTxtBox";
            this.InviteTxtBox.Size = new System.Drawing.Size(270, 24);
            this.InviteTxtBox.TabIndex = 9;
            // 
            // AccInvBtn
            // 
            this.AccInvBtn.Location = new System.Drawing.Point(544, 94);
            this.AccInvBtn.Name = "AccInvBtn";
            this.AccInvBtn.Size = new System.Drawing.Size(75, 23);
            this.AccInvBtn.TabIndex = 10;
            this.AccInvBtn.Text = "Accept";
            this.AccInvBtn.UseVisualStyleBackColor = true;
            this.AccInvBtn.Click += new System.EventHandler(this.AccInvBtn_Click);
            // 
            // DelUBtn
            // 
            this.DelUBtn.Location = new System.Drawing.Point(243, 301);
            this.DelUBtn.Name = "DelUBtn";
            this.DelUBtn.Size = new System.Drawing.Size(75, 23);
            this.DelUBtn.TabIndex = 11;
            this.DelUBtn.Text = "Delete";
            this.DelUBtn.UseVisualStyleBackColor = true;
            this.DelUBtn.Click += new System.EventHandler(this.DelUBtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(240, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Invite Link";
            // 
            // MuteUBtn
            // 
            this.MuteUBtn.Location = new System.Drawing.Point(349, 301);
            this.MuteUBtn.Name = "MuteUBtn";
            this.MuteUBtn.Size = new System.Drawing.Size(75, 23);
            this.MuteUBtn.TabIndex = 13;
            this.MuteUBtn.Text = "Mute";
            this.MuteUBtn.UseVisualStyleBackColor = true;
            this.MuteUBtn.Click += new System.EventHandler(this.MuteUBtn_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(240, 155);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "UserName";
            // 
            // UNLabel
            // 
            this.UNLabel.AutoSize = true;
            this.UNLabel.Location = new System.Drawing.Point(346, 155);
            this.UNLabel.Name = "UNLabel";
            this.UNLabel.Size = new System.Drawing.Size(35, 13);
            this.UNLabel.TabIndex = 15;
            this.UNLabel.Text = "label8";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(240, 196);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "UserTag";
            // 
            // UTLabel
            // 
            this.UTLabel.AutoSize = true;
            this.UTLabel.Location = new System.Drawing.Point(346, 196);
            this.UTLabel.Name = "UTLabel";
            this.UTLabel.Size = new System.Drawing.Size(41, 13);
            this.UTLabel.TabIndex = 17;
            this.UTLabel.Text = "label10";
            // 
            // Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 451);
            this.Controls.Add(this.MngCntPanel);
            this.Controls.Add(this.SettingPanel);
            this.Controls.Add(this.ChatPanel);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(649, 489);
            this.Name = "Chat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chat";
            this.TopMost = true;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ChatPanel.ResumeLayout(false);
            this.ChatPanel.PerformLayout();
            this.SettingPanel.ResumeLayout(false);
            this.SettingPanel.PerformLayout();
            this.MngCntPanel.ResumeLayout(false);
            this.MngCntPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox MsgListBox;
        private System.Windows.Forms.TextBox InputTxtBox;
        private System.Windows.Forms.Button SendBtn;
        private System.Windows.Forms.Label OtherUserName;
        private System.Windows.Forms.Button CallBtn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageContactsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Panel ChatPanel;
        private System.Windows.Forms.ListBox ContactsListBox;
        private System.Windows.Forms.Panel SettingPanel;
        private System.Windows.Forms.Panel MngCntPanel;
        private System.Windows.Forms.TextBox PassTxtBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox LoginTxtBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox NameTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SaveData;
        private System.Windows.Forms.TextBox TagTxtBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox AboutTxtBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem chatToolStripMenuItem;
        private System.Windows.Forms.ListBox SettingsLstBox;
        private System.Windows.Forms.Button DelUBtn;
        private System.Windows.Forms.Button AccInvBtn;
        private System.Windows.Forms.TextBox InviteTxtBox;
        private System.Windows.Forms.Label UTLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label UNLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button MuteUBtn;
        private System.Windows.Forms.Label label6;
    }
}