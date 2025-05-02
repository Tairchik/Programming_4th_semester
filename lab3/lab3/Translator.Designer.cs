namespace lab3
{
    partial class Translator
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxSearch = new System.Windows.Forms.ComboBox();
            this.listBoxSearch = new System.Windows.Forms.ListBox();
            this.labelIP = new System.Windows.Forms.Label();
            this.textBoxIPAddress = new System.Windows.Forms.TextBox();
            this.buttonTurnOffServer = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonSendToServer = new System.Windows.Forms.Button();
            this.buttonSendToClient = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxClientSide = new System.Windows.Forms.ListBox();
            this.labelServerSide = new System.Windows.Forms.Label();
            this.listBoxServerSide = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // comboBoxSearch
            // 
            this.comboBoxSearch.FormattingEnabled = true;
            this.comboBoxSearch.Location = new System.Drawing.Point(12, 12);
            this.comboBoxSearch.Name = "comboBoxSearch";
            this.comboBoxSearch.Size = new System.Drawing.Size(279, 21);
            this.comboBoxSearch.TabIndex = 0;
            this.comboBoxSearch.SelectedIndexChanged += comboBoxSearch_SelectedIndexChanged;
            this.comboBoxSearch.SelectedIndexChanged += comboBoxSearch_TextChanged;
            // 
            // listBoxSearch
            // 
            this.listBoxSearch.FormattingEnabled = true;
            this.listBoxSearch.Location = new System.Drawing.Point(13, 39);
            this.listBoxSearch.Name = "listBoxSearch";
            this.listBoxSearch.Size = new System.Drawing.Size(278, 303);
            this.listBoxSearch.TabIndex = 1;
            listBoxSearch.DoubleClick += listBoxSearch_DoubleClick;

            // 
            // labelIP
            // 
            this.labelIP.AutoSize = true;
            this.labelIP.Location = new System.Drawing.Point(13, 352);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(50, 13);
            this.labelIP.TabIndex = 2;
            this.labelIP.Text = "IP-адрес";
            // 
            // textBoxIPAddress
            // 
            this.textBoxIPAddress.Location = new System.Drawing.Point(69, 349);
            this.textBoxIPAddress.Name = "textBoxIPAddress";
            this.textBoxIPAddress.Size = new System.Drawing.Size(101, 20);
            this.textBoxIPAddress.TabIndex = 3;
            // 
            // buttonTurnOffServer
            // 
            this.buttonTurnOffServer.Location = new System.Drawing.Point(176, 346);
            this.buttonTurnOffServer.Name = "buttonTurnOffServer";
            this.buttonTurnOffServer.Size = new System.Drawing.Size(115, 27);
            this.buttonTurnOffServer.TabIndex = 4;
            this.buttonTurnOffServer.Text = "Сервер отключить";
            this.buttonTurnOffServer.UseVisualStyleBackColor = true;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(13, 379);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(86, 27);
            this.buttonConnect.TabIndex = 5;
            this.buttonConnect.Text = "Соединиться";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += buttonConnect_Click;
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(113, 379);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(86, 27);
            this.buttonDisconnect.TabIndex = 6;
            this.buttonDisconnect.Text = "Отключиться";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += buttonDisconnect_Click;
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(216, 379);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 27);
            this.buttonExit.TabIndex = 7;
            this.buttonExit.Text = "Выход";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += buttonExit_Click;
            // 
            // buttonSendToServerф
            // 
            this.buttonSendToServer.Location = new System.Drawing.Point(163, 412);
            this.buttonSendToServer.Name = "buttonSendToServer";
            this.buttonSendToServer.Size = new System.Drawing.Size(128, 27);
            this.buttonSendToServer.TabIndex = 8;
            this.buttonSendToServer.Text = "Передать серверу";
            this.buttonSendToServer.UseVisualStyleBackColor = true;
            this.buttonSendToClient.Click += buttonSendToServer_Click;
            // 
            // buttonSendToClient
            // 
            this.buttonSendToClient.Location = new System.Drawing.Point(12, 412);
            this.buttonSendToClient.Name = "buttonSendToClient";
            this.buttonSendToClient.Size = new System.Drawing.Size(128, 27);
            this.buttonSendToClient.TabIndex = 9;
            this.buttonSendToClient.Text = "Передать клиенту";
            this.buttonSendToClient.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(308, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Клиентская сторона";
            // 
            // listBoxClientSide
            //
            this.listBoxClientSide.FormattingEnabled = true;
            this.listBoxClientSide.Location = new System.Drawing.Point(311, 39);
            this.listBoxClientSide.Name = "listBoxClientSide";
            this.listBoxClientSide.Size = new System.Drawing.Size(221, 394);
            this.listBoxClientSide.TabIndex = 11;
            // 
            // labelServerSide
            // 
            this.labelServerSide.AutoSize = true;
            this.labelServerSide.Location = new System.Drawing.Point(543, 15);
            this.labelServerSide.Name = "labelServerSide";
            this.labelServerSide.Size = new System.Drawing.Size(106, 13);
            this.labelServerSide.TabIndex = 12;
            this.labelServerSide.Text = "Серверная сторона";
            // 
            // listBoxServerSide
            // 
            this.listBoxServerSide.FormattingEnabled = true;
            this.listBoxServerSide.Location = new System.Drawing.Point(546, 39);
            this.listBoxServerSide.Name = "listBoxServerSide";
            this.listBoxServerSide.Size = new System.Drawing.Size(221, 394);
            this.listBoxServerSide.TabIndex = 13;
            // 
            // Translator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(785, 444);
            this.Controls.Add(this.listBoxServerSide);
            this.Controls.Add(this.labelServerSide);
            this.Controls.Add(this.listBoxClientSide);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSendToClient);
            this.Controls.Add(this.buttonSendToServer);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.buttonTurnOffServer);
            this.Controls.Add(this.textBoxIPAddress);
            this.Controls.Add(this.labelIP);
            this.Controls.Add(this.listBoxSearch);
            this.Controls.Add(this.comboBoxSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Translator";
            this.Text = "Программа для обмена данными между компьютерами";
            this.Load += Translator_Load;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxSearch;
        private System.Windows.Forms.ListBox listBoxSearch;
        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.TextBox textBoxIPAddress;
        private System.Windows.Forms.Button buttonTurnOffServer;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonSendToServer;
        private System.Windows.Forms.Button buttonSendToClient;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxClientSide;
        private System.Windows.Forms.Label labelServerSide;
        private System.Windows.Forms.ListBox listBoxServerSide;
    }
}