/*namespace lab3Client
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
            comboBoxSearch = new ComboBox();
            listBoxSearch = new ListBox();
            labelIP = new Label();
            textBoxIPAddress = new TextBox();
            buttonTurnOffServer = new Button();
            buttonConnect = new Button();
            buttonDisconnect = new Button();
            buttonExit = new Button();
            buttonSendToServer = new Button();
            buttonSendToClient = new Button();
            label1 = new Label();
            listBoxClientSide = new ListBox();
            SuspendLayout();
            // 
            // comboBoxSearch
            // 
            comboBoxSearch.FormattingEnabled = true;
            comboBoxSearch.Location = new Point(14, 14);
            comboBoxSearch.Margin = new Padding(4, 3, 4, 3);
            comboBoxSearch.Name = "comboBoxSearch";
            comboBoxSearch.Size = new Size(325, 23);
            comboBoxSearch.TabIndex = 0;
            comboBoxSearch.SelectedIndexChanged += comboBoxSearch_TextChanged;
            // 
            // listBoxSearch
            // 
            listBoxSearch.FormattingEnabled = true;
            listBoxSearch.ItemHeight = 15;
            listBoxSearch.Location = new Point(15, 45);
            listBoxSearch.Margin = new Padding(4, 3, 4, 3);
            listBoxSearch.Name = "listBoxSearch";
            listBoxSearch.Size = new Size(324, 349);
            listBoxSearch.TabIndex = 1;
            listBoxSearch.DoubleClick += listBoxSearch_DoubleClick;
            // 
            // labelIP
            // 
            labelIP.AutoSize = true;
            labelIP.Location = new Point(15, 406);
            labelIP.Margin = new Padding(4, 0, 4, 0);
            labelIP.Name = "labelIP";
            labelIP.Size = new Size(53, 15);
            labelIP.TabIndex = 2;
            labelIP.Text = "IP-адрес";
            // 
            // textBoxIPAddress
            // 
            textBoxIPAddress.Location = new Point(80, 403);
            textBoxIPAddress.Margin = new Padding(4, 3, 4, 3);
            textBoxIPAddress.Name = "textBoxIPAddress";
            textBoxIPAddress.Size = new Size(117, 23);
            textBoxIPAddress.TabIndex = 3;
            // 
            // buttonTurnOffServer
            // 
            buttonTurnOffServer.Location = new Point(205, 399);
            buttonTurnOffServer.Margin = new Padding(4, 3, 4, 3);
            buttonTurnOffServer.Name = "buttonTurnOffServer";
            buttonTurnOffServer.Size = new Size(134, 31);
            buttonTurnOffServer.TabIndex = 4;
            buttonTurnOffServer.Text = "Сервер отключить";
            buttonTurnOffServer.UseVisualStyleBackColor = true;
            // 
            // buttonConnect
            // 
            buttonConnect.Location = new Point(15, 437);
            buttonConnect.Margin = new Padding(4, 3, 4, 3);
            buttonConnect.Name = "buttonConnect";
            buttonConnect.Size = new Size(100, 31);
            buttonConnect.TabIndex = 5;
            buttonConnect.Text = "Соединиться";
            buttonConnect.UseVisualStyleBackColor = true;
            buttonConnect.Click += buttonConnect_Click;
            // 
            // buttonDisconnect
            // 
            buttonDisconnect.Location = new Point(132, 437);
            buttonDisconnect.Margin = new Padding(4, 3, 4, 3);
            buttonDisconnect.Name = "buttonDisconnect";
            buttonDisconnect.Size = new Size(100, 31);
            buttonDisconnect.TabIndex = 6;
            buttonDisconnect.Text = "Отключиться";
            buttonDisconnect.UseVisualStyleBackColor = true;
            buttonDisconnect.Click += buttonDisconnect_Click;
            // 
            // buttonExit
            // 
            buttonExit.Location = new Point(252, 437);
            buttonExit.Margin = new Padding(4, 3, 4, 3);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(88, 31);
            buttonExit.TabIndex = 7;
            buttonExit.Text = "Выход";
            buttonExit.UseVisualStyleBackColor = true;
            buttonExit.Click += buttonExit_Click;
            // 
            // buttonSendToServer
            // 
            buttonSendToServer.Location = new Point(190, 475);
            buttonSendToServer.Margin = new Padding(4, 3, 4, 3);
            buttonSendToServer.Name = "buttonSendToServer";
            buttonSendToServer.Size = new Size(149, 31);
            buttonSendToServer.TabIndex = 8;
            buttonSendToServer.Text = "Передать серверу";
            buttonSendToServer.UseVisualStyleBackColor = true;
            // 
            // buttonSendToClient
            // 
            buttonSendToClient.Location = new Point(14, 475);
            buttonSendToClient.Margin = new Padding(4, 3, 4, 3);
            buttonSendToClient.Name = "buttonSendToClient";
            buttonSendToClient.Size = new Size(149, 31);
            buttonSendToClient.TabIndex = 9;
            buttonSendToClient.Text = "Передать клиенту";
            buttonSendToClient.UseVisualStyleBackColor = true;
            buttonSendToClient.Click += buttonSendToServer_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(359, 17);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(118, 15);
            label1.TabIndex = 10;
            label1.Text = "Клиентская сторона";
            // 
            // listBoxClientSide
            // 
            listBoxClientSide.FormattingEnabled = true;
            listBoxClientSide.ItemHeight = 15;
            listBoxClientSide.Location = new Point(363, 45);
            listBoxClientSide.Margin = new Padding(4, 3, 4, 3);
            listBoxClientSide.Name = "listBoxClientSide";
            listBoxClientSide.Size = new Size(540, 454);
            listBoxClientSide.TabIndex = 11;
            // 
            // Translator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FloralWhite;
            ClientSize = new Size(916, 512);
            Controls.Add(listBoxClientSide);
            Controls.Add(label1);
            Controls.Add(buttonSendToClient);
            Controls.Add(buttonSendToServer);
            Controls.Add(buttonExit);
            Controls.Add(buttonDisconnect);
            Controls.Add(buttonConnect);
            Controls.Add(buttonTurnOffServer);
            Controls.Add(textBoxIPAddress);
            Controls.Add(labelIP);
            Controls.Add(listBoxSearch);
            Controls.Add(comboBoxSearch);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 3, 4, 3);
            Name = "Translator";
            Text = "Программа для обмена данными между компьютерами";
            Load += Translator_Load;
            ResumeLayout(false);
            PerformLayout();
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
    }
}*/