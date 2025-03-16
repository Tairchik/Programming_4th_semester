using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_MethodsOfProgram
{
    internal class TestClass
    {
        public TestClass() 
        {
            Start();  
        }

        private void Start()
        {
            PrintInfo();

            while (true) 
            {
                Console.Write("VM>");
                string command = Console.ReadLine();
                string nameCommand = command.Split(' ')[0];
                if (nameCommand.ToLower() == "create")
                {

                }
                else if (nameCommand.ToLower() == "input")
                {

                }
                else if (nameCommand.ToLower() == "print")
                {

                }
                else if (nameCommand.ToLower() == "exit")
                {

                }
                else if (nameCommand.ToLower() == "help")
                {
                    PrintInfo();
                }
                else
                {
                    Console.WriteLine("Некорректный ввод команды. Введите еще раз.");
                    Console.WriteLine("Help - вывести список команд.");
                }
            }
        }

        private void PrintInfo()
        {
            Console.WriteLine("Список команд:");
            Console.WriteLine("Create имя файла(int | char(длина строки) | varchar(максимальная длина строки)) – создает все необходимые структуры в памяти и файлы на диске.");
            Console.WriteLine("Input (индекс, значение) – записывает значение в элемент массива с номером индекс. Строковое значение обрамляется кавычками");
            Console.WriteLine("Print (индекс) – выводит на экран значение элемента массива с номером индекс.");
            Console.WriteLine("Exit – закрывает все файлы и завершает программу. Файлы виртуального массива при завершении не уничтожаются. Они уничтожаются вручную после просмотра дампа файлов при защите.");
            Console.WriteLine("Help - вывести список команд.");
        }

        private void Create(string command)
        {

        }
        private void Input(string command)
        {

        }
        private void Print(string command)
        {

        }
        private void Exit(string command)
        {

        }

    }
}
