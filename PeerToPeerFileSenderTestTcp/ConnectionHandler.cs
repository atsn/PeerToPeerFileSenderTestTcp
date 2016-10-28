using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PeerToPeerFileSenderTestTcp
{
    class ConnectionHandler
    {
        private NetworkStream clientStream;
        public string messege { get; set; } = "kør";
        private TcpClient Client;

        public ConnectionHandler(NetworkStream ClientStream)
        {
            clientStream = ClientStream;
        }
        public ConnectionHandler(NetworkStream ClientStream, TcpClient client)
        {
            clientStream = ClientStream;
            Client = client;
        }



        public void Reshive()
        {
            while (!messege.ToLower().Equals("stop"))
            {
                try
                {
                    StreamReader clientStreamReader = new StreamReader(clientStream);

                    messege = clientStreamReader.ReadLine();

                    if (messege.ToLower().StartsWith("request"))
                    {
                        string[] messeges = messege.Split();
                        if (messeges[2] != null)
                        {
                            Sendmessege("Wrong Request Format");
                        }
                        foreach (var s in messeges)
                        {
                            if (!s.Contains("request"))
                            {
                                Sendfile(s.Trim());
                            }
                        }
                    }
                    Console.WriteLine(clientStreamReader.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }

            }



        }

        public void Sendmessege()
        {
            while (!messege.ToLower().Equals("stop"))
            {
                {
                    try
                    {
                        StreamWriter clientStreamWriter = new StreamWriter(clientStream);
                        clientStreamWriter.AutoFlush = true;

                        messege = Console.ReadLine();

                        if (messege.ToLower().StartsWith("request") && Client == null);
                        {
                            string[] messeges = messege.Split();
                            if (messeges[2] != null)
                            {
                                Sendmessege("Wrong Request Format");
                            }
                            clientStreamWriter.WriteLine(messege);
                            reshivefile();
                        }
                        clientStreamWriter.WriteLine(messege);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);

                    }
                }

            }


        }

        public void Sendmessege(string Messege)
        {

            {
                try
                {
                    StreamWriter clientStreamWriter = new StreamWriter(clientStream);
                    clientStreamWriter.AutoFlush = true;

                    clientStreamWriter.WriteLine(Messege);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }
            }




        }

        public void Sendfile(string filename)
        {


            try
            {
                FileStream file =
                    new FileStream(
                        @"C:\Users\Ander\Documents\MEGA\Visual Studio Apps\Små Projector\PeerToPeerFileSenderTestTcp\Files\" +
                        filename, FileMode.Open);

                StreamWriter clientStreamWriter = new StreamWriter(clientStream);
                clientStreamWriter.AutoFlush = true;

                messege = Console.ReadLine();
                clientStreamWriter.Write(file);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Sendmessege(e.Message);

            }
        }

        public void reshivefile()
        {
            try
            {
                StreamReader clientStreamReader = new StreamReader(clientStream);

                messege = clientStreamReader.ReadToEnd();
                byte[] bytes = new byte[messege.Length];

                FileStream file =
                    new FileStream(
                        @"C:\Users\Ander\Documents\MEGA\Visual Studio Apps\Små Projector\PeerToPeerFileSenderTestTcp\Files\",
                        FileMode.Create);

                file.Write(bytes, 0, messege.Length);

                Console.WriteLine("file reshived");

            }

            catch (Exception e)
            {
                Console.WriteLine(e + e.Message);
            }

        }
    }
}
