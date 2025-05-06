namespace lab3_4Client
{
    partial class Pult
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
            buttonDisconnect = new Button();
            buttonConnect = new Button();
            textBoxIPAddress = new TextBox();
            labelIP = new Label();
            SuspendLayout();
            // 
            // labelIP
            // 
            labelIP.AutoSize = true;
            labelIP.Location = new Point(13, 328);
            labelIP.Margin = new Padding(4, 0, 4, 0);
            labelIP.Name = "labelIP";
            labelIP.Size = new Size(53, 15);
            labelIP.TabIndex = 7;
            labelIP.Text = "IP-адрес";
            // 
            // textBoxIPAddress
            // 
            textBoxIPAddress.Location = new Point(78, 325);
            textBoxIPAddress.Margin = new Padding(4, 3, 4, 3);
            textBoxIPAddress.Name = "textBoxIPAddress";
            textBoxIPAddress.Size = new Size(117, 23);
            textBoxIPAddress.TabIndex = 8;
            textBoxIPAddress.Text = "127.0.0.1";
            // 
            // buttonConnect
            // 
            buttonConnect.Location = new Point(13, 370);
            buttonConnect.Margin = new Padding(4, 3, 4, 3);
            buttonConnect.Name = "buttonConnect";
            buttonConnect.Size = new Size(100, 31);
            buttonConnect.TabIndex = 9;
            buttonConnect.Text = "Соединиться";
            buttonConnect.UseVisualStyleBackColor = true;
            buttonConnect.Click += buttonConnect_Click;
            // 
            // buttonDisconnect
            // 
            buttonDisconnect.Location = new Point(131, 370);
            buttonDisconnect.Margin = new Padding(4, 3, 4, 3);
            buttonDisconnect.Name = "buttonDisconnect";
            buttonDisconnect.Size = new Size(100, 31);
            buttonDisconnect.TabIndex = 10;
            buttonDisconnect.Text = "Отключиться";
            buttonDisconnect.UseVisualStyleBackColor = true;
            buttonDisconnect.Click += buttonDisconnect_Click;
            // 
            // Pult
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonDisconnect);
            Controls.Add(buttonConnect);
            Controls.Add(textBoxIPAddress);
            Controls.Add(labelIP);
            Name = "Pult";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonDisconnect;
        private Button buttonConnect;
        private TextBox textBoxIPAddress;
        private Label labelIP;
    }
}
