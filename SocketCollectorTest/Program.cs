using System;

namespace SocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup Data
            SocketClient client = new SocketClient();

            Console.WriteLine("Welcome to the Socket Cient Tester");
            Console.WriteLine();
            while (!client.VerifyIP())
            {
                Console.Write("Enter Socket IP (without port): ");
                client.IP = Console.ReadLine();
            }
            while (client.Port == 0)
            {
                Console.Write("Enter Socket Port: ");
                var portStr = Console.ReadLine();
                int portNum;
                if (int.TryParse(portStr, out portNum))
                    client.Port = portNum;
            }
            while (!client.VerifyFile())
            {
                Console.Write("Enter path to fake data (xml file only): ");
                client.FilePath = Console.ReadLine();
            }
            Console.Write("How long to wait inbetween sends (in seconds)?: ");
            var waitStr = Console.ReadLine();
            int waitNum;
            if (int.TryParse(waitStr, out waitNum))
                client.WaitInbetween = waitNum;
            else
                client.WaitInbetween = 0;
            Console.Write("Ready to start? (Y/N) ");
            var key = Console.ReadLine();
            if (key.StartsWith("y", StringComparison.CurrentCultureIgnoreCase))
            {
                //Start
                Console.WriteLine("Starting Socket Client!  Stop sending data by clicking any key in the console");
                bool canConnect = client.Connect();
                if (canConnect)
                {
                    client.ReadFile();
                    client.SendData();
                    Console.ReadKey();
                    client.StopSendingData();
                }
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            return; //Quit
        }
    }
}