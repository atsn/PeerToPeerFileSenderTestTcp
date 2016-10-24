using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PeerToPeerFileSenderTestTcp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ConsoleKeyInfo pressedkey = Console.ReadKey();
                string testtesttest = "messege";
                Console.WriteLine("press 1 to request file");
                if (pressedkey.Key == ConsoleKey.NumPad1 || pressedkey.Key == ConsoleKey.D1)
                {
                    TcpClient client = new TcpClient("192.168.0.18", 6789);
                    
                    var clientStream = client.GetStream();

                    ConnectionHandler handler = new ConnectionHandler(clientStream);
                    Thread Sendthread = new Thread(handler.Sendmessege);
                    Sendthread.Start();
                }



                TcpListener listener = new TcpListener(IPAddress.Any, 6789);
                listener.Start();

                var serverstream = listener.AcceptTcpClient();

                ConnectionHandler handler2 = new ConnectionHandler(serverstream.GetStream(), serverstream);
                Thread Reshivethreadserver = new Thread(handler2.Reshive);
                Thread Sendthreadserver = new Thread(handler2.Sendmessege);
                Reshivethreadserver.Start();
                Sendthreadserver.Start();



            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
        }



    }
}
