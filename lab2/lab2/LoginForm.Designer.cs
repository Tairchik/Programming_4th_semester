namespace lab2
{
    partial class LoginForm
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
            bannerPanel = new Panel();
            keyIcon = new PictureBox();
            label1 = new Label();
            systemNameLabel = new Label();
            versionLabel = new Label();
            usernameLabel = new Label();
            usernameTextBox = new TextBox();
            passwordLabel = new Label();
            passwordTextBox = new TextBox();
            loginButton = new Button();
            cancelButton = new Button();
            statusStrip = new StatusStrip();
            languageLabel = new ToolStripStatusLabel();
            capsLockLabel = new ToolStripStatusLabel();
            bannerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)keyIcon).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // bannerPanel
            // 
            bannerPanel.BackColor = SystemColors.GradientActiveCaption;
            bannerPanel.Controls.Add(keyIcon);
            bannerPanel.Controls.Add(label1);
            bannerPanel.Controls.Add(systemNameLabel);
            bannerPanel.Controls.Add(versionLabel);
            bannerPanel.Dock = DockStyle.Top;
            bannerPanel.Location = new Point(0, 0);
            bannerPanel.Margin = new Padding(4, 3, 4, 3);
            bannerPanel.Name = "bannerPanel";
            bannerPanel.Size = new Size(436, 115);
            bannerPanel.TabIndex = 0;
            // 
            // keyIcon
            // 
            keyIcon.BackColor = Color.Transparent;
            keyIcon.Image = Image.FromFile("..\\..\\..\\keys.png");
            keyIcon.Location = new Point(7, 7);
            keyIcon.Margin = new Padding(4, 3, 4, 3);
            keyIcon.Name = "keyIcon";
            keyIcon.Size = new Size(70, 67);
            keyIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            keyIcon.TabIndex = 0;
            keyIcon.TabStop = false;
            // 
            // label1
            // 
            label1.BackColor = Color.White;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(4, 75);
            label1.Name = "label1";
            label1.Size = new Size(428, 21);
            label1.TabIndex = 3;
            label1.Text = "Введите имя пользователя или пароль";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // systemNameLabel
            // 
            systemNameLabel.BackColor = Color.LemonChiffon;
            systemNameLabel.Font = new Font("Arial", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            systemNameLabel.Location = new Point(4, 9);
            systemNameLabel.Margin = new Padding(4, 0, 4, 0);
            systemNameLabel.Name = "systemNameLabel";
            systemNameLabel.Size = new Size(420, 32);
            systemNameLabel.TabIndex = 1;
            systemNameLabel.Text = "АИС Отдел кадров";
            systemNameLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // versionLabel
            // 
            versionLabel.BackColor = Color.FromArgb(255, 215, 2);
            versionLabel.Font = new Font("Arial", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            versionLabel.Location = new Point(4, 43);
            versionLabel.Margin = new Padding(4, 0, 4, 0);
            versionLabel.Name = "versionLabel";
            versionLabel.Size = new Size(420, 29);
            versionLabel.TabIndex = 2;
            versionLabel.Text = "Версия 1.0.0.0";
            versionLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // usernameLabel
            // 
            usernameLabel.AutoSize = true;
            usernameLabel.Font = new Font("Arial", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            usernameLabel.Location = new Point(4, 118);
            usernameLabel.Margin = new Padding(4, 0, 4, 0);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(117, 16);
            usernameLabel.TabIndex = 4;
            usernameLabel.Text = "Имя пользователя";
            // 
            // usernameTextBox
            // 
            usernameTextBox.Location = new Point(154, 116);
            usernameTextBox.Margin = new Padding(4, 3, 4, 3);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(268, 23);
            usernameTextBox.TabIndex = 5;
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Font = new Font("Arial", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            passwordLabel.Location = new Point(7, 158);
            passwordLabel.Margin = new Padding(4, 0, 4, 0);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(51, 16);
            passwordLabel.TabIndex = 6;
            passwordLabel.Text = "Пароль";
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(154, 156);
            passwordTextBox.Margin = new Padding(4, 3, 4, 3);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(268, 23);
            passwordTextBox.TabIndex = 7;
            passwordTextBox.UseSystemPasswordChar = true;
            // 
            // loginButton
            // 
            loginButton.BackColor = SystemColors.ButtonFace;
            loginButton.Location = new Point(22, 189);
            loginButton.Margin = new Padding(4, 3, 4, 3);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(91, 24);
            loginButton.TabIndex = 8;
            loginButton.Text = "Вход";
            loginButton.UseVisualStyleBackColor = false;
            loginButton.Click += LoginButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.BackColor = SystemColors.ButtonFace;
            cancelButton.Location = new Point(322, 189);
            cancelButton.Margin = new Padding(4, 3, 4, 3);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(91, 24);
            cancelButton.TabIndex = 9;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = false;
            cancelButton.Click += CancelButton_Click;
            // 
            // statusStrip
            // 
            statusStrip.BackColor = SystemColors.GradientActiveCaption;
            statusStrip.GripStyle = ToolStripGripStyle.Visible;
            statusStrip.Items.AddRange(new ToolStripItem[] { languageLabel, capsLockLabel });
            statusStrip.Location = new Point(0, 216);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 16, 0);
            statusStrip.RenderMode = ToolStripRenderMode.Professional;
            statusStrip.RightToLeft = RightToLeft.No;
            statusStrip.Size = new Size(436, 22);
            statusStrip.TabIndex = 10;
            // 
            // languageLabel
            // 
            languageLabel.Name = "languageLabel";
            languageLabel.Size = new Size(138, 17);
            languageLabel.Text = "Язык ввода Английский";
            // 
            // capsLockLabel
            // 
            capsLockLabel.AutoSize = false;
            capsLockLabel.ImageAlign = ContentAlignment.MiddleRight;
            capsLockLabel.Name = "capsLockLabel";
            capsLockLabel.Overflow = ToolStripItemOverflow.Always;
            capsLockLabel.Size = new Size(258, 17);
            capsLockLabel.Text = "Клавиша CapsLock нажата";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(436, 238);
            Controls.Add(cancelButton);
            Controls.Add(loginButton);
            Controls.Add(passwordTextBox);
            Controls.Add(passwordLabel);
            Controls.Add(usernameTextBox);
            Controls.Add(usernameLabel);
            Controls.Add(bannerPanel);
            Controls.Add(statusStrip);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Вход";
            Activated += Form_Activated;
            KeyDown += Form_KeyDown;
            KeyUp += Form_KeyUp;
            bannerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)keyIcon).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Panel bannerPanel;
        private System.Windows.Forms.PictureBox keyIcon;
        private System.Windows.Forms.Label systemNameLabel;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel languageLabel;
        private System.Windows.Forms.ToolStripStatusLabel capsLockLabel;
        private Label label1;
    } 
}
#endregion