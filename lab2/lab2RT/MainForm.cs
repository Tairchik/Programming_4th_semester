using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace lab2RT
{
    public partial class MainForm : Form
    {
        private lab2RT.Controller.MainController _controller;

        public MainForm(lab2RT.Controller.MainController controller)
        {
            InitializeComponent();
            _controller = controller;
            _controller.AddMenuItems(menuStrip);
            _controller.MenuItemClick += MenuItemClick;
        }

        private void MenuItemClick(object sender, EventArgs e)
        {
            MessageBox.Show(sender.ToString(), "Обработчик меню");
        }
    }
}
