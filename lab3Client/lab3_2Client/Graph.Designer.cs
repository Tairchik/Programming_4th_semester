using ScottPlot.WinForms;

namespace lab3_2Client
{
    partial class Graph
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
            Temperature = new FormsPlot();
            btnConnect = new Button();
            btn_disconnect = new Button();
            Pressure = new FormsPlot();
            tBoxIPAddress = new TextBox();
            lblIP = new Label();
            ResetGraphs = new Button();
            SuspendLayout();
            // 
            // Temperature
            // 
            Temperature.DisplayScale = 1F;
            Temperature.Location = new Point(12, 12);
            Temperature.Name = "Temperature";
            Temperature.Size = new Size(434, 427);
            Temperature.TabIndex = 0;
            // 
            // btnConnect
            // 
            btnConnect.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnConnect.Location = new Point(32, 458);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(145, 31);
            btnConnect.TabIndex = 1;
            btnConnect.Text = "Подключиться";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btn_disconnect
            // 
            btn_disconnect.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btn_disconnect.Location = new Point(34, 506);
            btn_disconnect.Name = "btn_disconnect";
            btn_disconnect.Size = new Size(143, 29);
            btn_disconnect.TabIndex = 2;
            btn_disconnect.Text = "Отключиться";
            btn_disconnect.UseVisualStyleBackColor = true;
            btn_disconnect.Click += btn_disconnect_Click;
            // 
            // Pressure
            // 
            Pressure.DisplayScale = 1F;
            Pressure.Location = new Point(519, 12);
            Pressure.Name = "Pressure";
            Pressure.Size = new Size(434, 427);
            Pressure.TabIndex = 3;
            // 
            // tBoxIPAddress
            // 
            tBoxIPAddress.Font = new Font("Segoe UI", 10F);
            tBoxIPAddress.Location = new Point(243, 462);
            tBoxIPAddress.Name = "tBoxIPAddress";
            tBoxIPAddress.Size = new Size(155, 25);
            tBoxIPAddress.TabIndex = 4;
            tBoxIPAddress.Text = "127.0.0.1";
            // 
            // lblIP
            // 
            lblIP.AutoSize = true;
            lblIP.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblIP.Location = new Point(183, 465);
            lblIP.Name = "lblIP";
            lblIP.Size = new Size(60, 17);
            lblIP.TabIndex = 5;
            lblIP.Text = "IP адрес:";
            // 
            // ResetGraphs
            // 
            ResetGraphs.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ResetGraphs.Location = new Point(243, 504);
            ResetGraphs.Name = "ResetGraphs";
            ResetGraphs.Size = new Size(145, 31);
            ResetGraphs.TabIndex = 1;
            ResetGraphs.Text = "Сбросить графики";
            ResetGraphs.UseVisualStyleBackColor = true;
            ResetGraphs.Click += ResetGraphs_Click;
            // 
            // Graph
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(976, 572);
            Controls.Add(lblIP);
            Controls.Add(tBoxIPAddress);
            Controls.Add(Pressure);
            Controls.Add(btn_disconnect);
            Controls.Add(ResetGraphs);
            Controls.Add(btnConnect);
            Controls.Add(Temperature);
            Name = "Graph";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot Temperature;
        private Button btnConnect;
        private Button btn_disconnect;
        private FormsPlot Pressure;
        private TextBox tBoxIPAddress;
        private Label lblIP;
        private Button ResetGraphs;
    }
}
