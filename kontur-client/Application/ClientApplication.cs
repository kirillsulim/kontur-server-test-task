using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


using kontur_server_core.Protocol;namespace kontur_client.Application
{
    public class ClientApplication : IClientApplication
    {
        private IPAddress ip;
        private int port;
        private TextReader cin;
        private TextWriter cout;
        private IProtocolReader reader;

        public ClientApplication(IPAddress address, int port, TextReader cin, TextWriter cout, IProtocolReader reader)
        {
            this.ip = address;
            this.port = port;
            this.cin = cin;
            this.cout = cout;
            this.reader = reader;
        }

        public void Run()
        {
            IPEndPoint point = new IPEndPoint(ip, port);

            using (TcpClient client = new TcpClient())
            {
                try
                {
                    client.Connect(point);

                    while (true)
                    {
                        cout.WriteLine(
    @"Enter command: 
    get <prefix> - to get words
    exit to exit app");

                        string command = cin.ReadLine();

                        Stream stream = client.GetStream();

                        reader.WriteString(stream, command);

                        if (command == "exit")
                            break;

                        var response = reader.ReadStringArray(stream);

                        cout.WriteLine("Server response:");
                        foreach (var w in response)
                        {
                            cout.WriteLine(w);
                        }
                    }
                }
                catch (Exception e)
                {
                    cout.WriteLine("Error! " + e.Message);
                }
            }            
        }
    }
}
