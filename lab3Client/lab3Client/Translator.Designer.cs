namespace lab3Client
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
            buttonConnect = new Button();
            buttonDisconnect = new Button();
            buttonExit = new Button();
            label1 = new Label();
            fileContent = new RichTextBox();
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
            listBoxSearch.DoubleClick += SendToServer_DoubleClick;
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
            textBoxIPAddress.Text = "127.0.0.1";
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
            // fileContent
            // 
            fileContent.Location = new Point(359, 45);
            fileContent.Name = "fileContent";
            fileContent.Size = new Size(545, 398);
            fileContent.TabIndex = 11;
            fileContent.Text = "";
            // 
            // Translator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FloralWhite;
            ClientSize = new Size(916, 480);
            Controls.Add(fileContent);
            Controls.Add(label1);
            Controls.Add(buttonExit);
            Controls.Add(buttonDisconnect);
            Controls.Add(buttonConnect);
            Controls.Add(textBoxIPAddress);
            Controls.Add(labelIP);
            Controls.Add(listBoxSearch);
            Controls.Add(comboBoxSearch);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 3, 4, 3);
            Name = "Translator";
            Text = "Программа для обмена данными между компьютерами";
            this.FormClosing += menuExit_Click;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox comboBoxSearch;
        private ListBox listBoxSearch;
        private Label labelIP;
        private TextBox textBoxIPAddress;
        private Button buttonConnect;
        private Button buttonDisconnect;
        private Button buttonExit;
        private Label label1;
        private RichTextBox fileContent;
    }
}