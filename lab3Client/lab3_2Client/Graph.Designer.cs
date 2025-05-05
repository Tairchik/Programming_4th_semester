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
            btnConnect.Location = new Point(12, 489);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(145, 71);
            btnConnect.TabIndex = 1;
            btnConnect.Text = "Подключиться";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btn_disconnect
            // 
            btn_disconnect.Location = new Point(178, 489);
            btn_disconnect.Name = "btn_disconnect";
            btn_disconnect.Size = new Size(143, 71);
            btn_disconnect.TabIndex = 2;
            btn_disconnect.Text = "Отключиться";
            btn_disconnect.UseVisualStyleBackColor = true;
            btn_disconnect.Click += btn_disconnect_Click;
            // 
            // Pressure
            // 
            Pressure.DisplayScale = 1F;
            Pressure.Location = new Point(536, 12);
            Pressure.Name = "Pressure";
            Pressure.Size = new Size(434, 427);
            Pressure.TabIndex = 3;
            // 
            // Graph
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1079, 572);
            Controls.Add(Pressure);
            Controls.Add(btn_disconnect);
            Controls.Add(btnConnect);
            Controls.Add(Temperature);
            Name = "Graph";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot Temperature;
        private Button btnConnect;
        private Button btn_disconnect;
        private ScottPlot.WinForms.FormsPlot Pressure;
    }
}
