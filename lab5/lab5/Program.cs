using lab5;

Console.Write("Введите время в формате HH:MM в 24-часовом формате: ");
string digitalTime = Console.ReadLine();
try
{
    DigitalClock digitalClock = new DigitalClock(digitalTime);
    IAnalogClock analogClock = new DigitalToAnalogAdapter(digitalClock);
    Console.WriteLine("Время в градусах: " + analogClock);
}
catch(ArgumentException ex)
{
    Console.WriteLine("Введено неправильное время");
}