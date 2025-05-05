using ScottPlot.WinForms;

namespace lab3_3Client
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
            chart = new FormsPlot();
            lblIP = new Label();
            tBoxIPAddress = new TextBox();
            btn_disconnect = new Button();
            ResetGraphs = new Button();
            btnConnect = new Button();
            SuspendLayout();
            // 
            // chart
            // 
            chart.DisplayScale = 1F;
            chart.Location = new Point(12, 12);
            chart.Name = "chart";
            chart.Size = new Size(905, 427);
            chart.TabIndex = 0;
            // 
            // lblIP
            // 
            lblIP.AutoSize = true;
            lblIP.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblIP.Location = new Point(198, 468);
            lblIP.Name = "lblIP";
            lblIP.Size = new Size(60, 17);
            lblIP.TabIndex = 10;
            lblIP.Text = "IP адрес:";
            // 
            // tBoxIPAddress
            // 
            tBoxIPAddress.Font = new Font("Segoe UI", 10F);
            tBoxIPAddress.Location = new Point(258, 465);
            tBoxIPAddress.Name = "tBoxIPAddress";
            tBoxIPAddress.Size = new Size(155, 25);
            tBoxIPAddress.TabIndex = 9;
            tBoxIPAddress.Text = "127.0.0.1";
            // 
            // btn_disconnect
            // 
            btn_disconnect.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btn_disconnect.Location = new Point(49, 509);
            btn_disconnect.Name = "btn_disconnect";
            btn_disconnect.Size = new Size(143, 29);
            btn_disconnect.TabIndex = 8;
            btn_disconnect.Text = "Отключиться";
            btn_disconnect.UseVisualStyleBackColor = true;
            btn_disconnect.Click += btn_disconnect_Click;
            // 
            // ResetGraphs
            // 
            ResetGraphs.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ResetGraphs.Location = new Point(258, 507);
            ResetGraphs.Name = "ResetGraphs";
            ResetGraphs.Size = new Size(145, 31);
            ResetGraphs.TabIndex = 6;
            ResetGraphs.Text = "Сбросить график";
            ResetGraphs.UseVisualStyleBackColor = true;
            ResetGraphs.Click += ResetGraphs_Click;
            // 
            // btnConnect
            // 
            btnConnect.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnConnect.Location = new Point(47, 461);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(145, 31);
            btnConnect.TabIndex = 7;
            btnConnect.Text = "Подключиться";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // Graph
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(929, 576);
            Controls.Add(lblIP);
            Controls.Add(tBoxIPAddress);
            Controls.Add(btn_disconnect);
            Controls.Add(ResetGraphs);
            Controls.Add(btnConnect);
            Controls.Add(chart);
            Name = "Graph";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot chart;
        private Label lblIP;
        private TextBox tBoxIPAddress;
        private Button btn_disconnect;
        private Button ResetGraphs;
        private Button btnConnect;
    }
}
