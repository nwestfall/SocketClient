using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;

namespace SocketClient
{
    public class SocketClient
    {
        public string IP { get; set; }
        public int Port { get; set; }
        public string FilePath { get; set; }
        public int WaitInbetween { get; set; }
        private string FileData;
        private IPEndPoint EndPoint;
        private IPAddress Address;
        private Socket Socket;
        private CancellationTokenSource CT;

        public SocketClient() { }

        public bool VerifyIP()
        {
            return IPAddress.TryParse(IP, out Address);
        }

        public bool VerifyFile()
        {
            if (string.IsNullOrEmpty(FilePath))
                return false;
            return File.Exists(this.FilePath);
        }

        public void ReadFile()
        {
            if(VerifyFile())
            {
                FileData = File.ReadAllText(FilePath);
            }
        }

        public bool Connect()
        {
            if (VerifyIP())
            {
                try
                {
                    EndPoint = new IPEndPoint(Address, Port);

                    Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    Socket.Connect(EndPoint);
                    Console.WriteLine("Connected to Socket Collector");
                    return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error connecting to Socket: " + ex.StackTrace);
                }
            }
            return false;
        }

        public async void SendData()
        {
            CT = new CancellationTokenSource();
            CT.Token.Register(() =>
            {
                Socket.Shutdown(SocketShutdown.Both);
            });
            await Task.Run(() =>
            {
                byte[] msg = Encoding.ASCII.GetBytes(FileData);
                while (!CT.IsCancellationRequested)
                {
                    try
                    {
                        byte[] bytes = new byte[2048];
                        Console.WriteLine("Sending data to collector...");
                        int bytesSent = Socket.Send(msg);
                        Console.WriteLine(string.Format("Sent {0} bytes to collector", bytesSent));
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Something went wrong while sending data: " + ex.StackTrace);
                    }
                    Thread.Sleep(WaitInbetween * 1000);
                }
            }, CT.Token);
        }

        public void StopSendingData()
        {
            CT.Cancel();
        }
    }
}
