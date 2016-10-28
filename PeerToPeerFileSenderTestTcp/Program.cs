using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PeerToPeerFileSenderTestTcp.Powernap;
using ContentFile = SoapService.ContentFile;

namespace PeerToPeerFileSenderTestTcp
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Powernap.PowerNapServiceClient powernap = new PowerNapServiceClient();

                //powernap.Add("Anders.txt", "192.168.3.102", 6789);
                //powernap.Add("Jane.txt", "192.168.3.102", 6789);
                //powernap.Add("Jasper.txt", "192.168.3.102", 6789);
                //powernap.Add("Thomas.txt", "192.168.3.102", 6789);
                

              
                Console.WriteLine("press 1 to request file");
                ConsoleKeyInfo pressedkey = Console.ReadKey(true);
                string testtesttest = "messege";
                if (pressedkey.Key == ConsoleKey.NumPad1 || pressedkey.Key == ConsoleKey.D1)
                {
                    Console.WriteLine("Type In Filename");

                    string filename = Console.ReadLine();
                    if (powernap.RequestFile(filename).FileName == null)
                    {
                        Console.WriteLine("file not file");
                    }
                    else
                    {
                        TcpClient client = new TcpClient(powernap.RequestFile(filename).IpAddress, powernap.RequestFile(filename).Port);

                        var clientStream = client.GetStream();

                        ConnectionHandler handler = new ConnectionHandler(clientStream, powernap.RequestFile(filename).FileName);
                        Thread Sendthread = new Thread(handler.Sendmessege);
                        Sendthread.Start();
                    }

                }

                TcpListener listener = new TcpListener(IPAddress.Any, 6789);
                listener.Start();
                while (true)
                {
                    var serverstream = listener.AcceptTcpClient();
                    Console.WriteLine("client forbundet");
                    ConnectionHandler handler2 = new ConnectionHandler(serverstream.GetStream(), serverstream);
                    Thread Reshivethreadserver = new Thread(handler2.Reshive);
                    //     Thread Sendthreadserver = new Thread(handler2.Sendmessege);
                    Reshivethreadserver.Start();
                    //    Sendthreadserver.Start();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
