using nphw3client;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Windows.Input;

var ip = IPAddress.Loopback;
var port = 27001;
var client = new TcpClient();
client.Connect(ip, port);

var stream = client.GetStream();
var br = new BinaryReader(stream);
var bw = new BinaryWriter(stream);
Command command = null;
string response = null;
string str = null;

while (true)
{
    Console.WriteLine("Write command or HELP");
    str = Console.ReadLine().ToUpper();
    if (str == "HELP")
    {
        Console.WriteLine();
        Console.WriteLine("Command list:");
        Console.WriteLine($"{Command.Get} - gets list of cars");
        Console.WriteLine($"{Command.Post} <car_mark> <car_model> <year> - creates new car and adds it to list");
        Console.WriteLine($"{Command.Put} <car_mark> <car_model> <year> <new_mark> <new_model> <new_year> - updates already existing car");
        Console.WriteLine($"{Command.Delete} <car_mark> <car_model> <year> - removes car from list");
        Console.WriteLine("HELP - shows this table");
        Console.ReadLine();
        Console.Clear();
    }
    var input = str.Split(' ').ToList();

    switch (input[0])
    {
        case Command.Get:
            {
                command = new Command { Text = input[0] };
                bw.Write(JsonSerializer.Serialize(command));
                response = br.ReadString();
                var carsList = JsonSerializer.Deserialize<string[]>(response).ToList();
                if (carsList.Count > 0)
                {
                    foreach (var car in carsList)
                    {
                        Console.WriteLine($"        {car}");
                    }
                }
                else
                {
                    Console.WriteLine("There is no cars yet");
                }
                break;
            }
        case Command.Post:
            {
                if (input.Count == 4)
                {
                    command = new Command { Text = input[0], Params = input[1..4] };
                    bw.Write(JsonSerializer.Serialize(command));
                    response = br.ReadString();
                    if (response == "success")
                    {
                        Console.WriteLine("Car has been successfully created");
                    }
                    else if (response == "error")
                    {
                        Console.WriteLine("Error! Input had some mistakes. Try again");
                    }
                }
                break;
            }
        case Command.Put:
            {
                if (input.Count == 7)
                {
                    command = new Command { Text = input[0], Params = input[1..7] };
                    bw.Write(JsonSerializer.Serialize(command));
                    response = br.ReadString();
                    if (response == "success")
                    {
                        Console.WriteLine("Car has been successfully updated");
                    }
                    else if (response == "error")
                    {
                        Console.WriteLine("Error! Input had some mistakes. Try again");
                    }
                }
                break;
            }
        case Command.Delete:
            {
                if (input.Count == 4)
                {
                    command = new Command { Text = input[0], Params = input[1..4] };
                    bw.Write(JsonSerializer.Serialize(command));
                    response = br.ReadString();
                    if (response == "success")
                    {
                        Console.WriteLine("Car has been successfully removed");
                    }
                    else if (response == "error")
                    {
                        Console.WriteLine("Error! Input had some mistakes. Try again");
                    }
                }
                break;
            }
    }
    Console.WriteLine("Press any key to continue");
    Console.ReadLine();
    Console.Clear();
}