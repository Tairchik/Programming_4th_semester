namespace lab3_4Client
{
    public partial class Pult : Form
    {
        private PultController controller = new();
        private List<Button> buttons = new List<Button>();
        public Pult()
        {
            InitializeComponent();

            controller.Errors += ShowError;
            controller.DataUpdated += UpdateButtons;
        }

        private void ShowError(string message)
        {
            MessageBox.Show($"Error: {message}");
        }

        private void UpdateButtons(List<Color> buttonsStates)
        {
            for (int i = 0; i < buttonsStates.Count; i++)
            {
                buttons[i].BackColor = buttonsStates[i];
            }
        }

        private void CreateButtons(int numberOfUnits)
        {
            int buttonSize = 100; // Размер кнопки
            int padding = 10;     // Отступ между кнопками
            int buttonsPerRow = 10; // Количество кнопок в строке

            // Вычисление количества строк
            int rows = (int)Math.Ceiling((double)numberOfUnits / buttonsPerRow);

            // Высота формы
            int formHeight = padding + (buttonSize + padding) * rows + 200;

            // Установка размера формы
            this.ClientSize = new System.Drawing.Size(1100, formHeight); // Ширина фиксированная, высота динамическая

            for (int i = 0; i < numberOfUnits; i++)
            {
                Button button = new Button
                {
                    Text = $"Установка {i + 1}",
                    Width = buttonSize,
                    Height = buttonSize,
                    Location = new System.Drawing.Point(
                        padding + (buttonSize + padding) * (i % buttonsPerRow), // X
                        padding + (buttonSize + padding) * (i / buttonsPerRow)  // Y
                    ),
                    BackColor = System.Drawing.Color.Green
                };
                buttons.Add(button);
                this.Controls.Add(button);
            }
        }


        private void buttonConnect_Click(object sender, EventArgs e)
        {
            CreateButtons(controller.ConnectToServer(textBoxIPAddress.Text));
            controller.StartGetData();
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            controller.Disconnect();
        }
    }
}
