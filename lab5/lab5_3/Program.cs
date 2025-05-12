using lab5_3;

MicroController microController = new MicroController();
microController.SetTemperatureInFahrenheit(451);
microController.SetPressureInPascals(100392);
IProcessController processController = new MicrocontrollerAdapter(microController);
Console.WriteLine(processController);