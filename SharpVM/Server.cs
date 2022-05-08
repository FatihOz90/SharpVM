using System.Collections.Generic;

namespace SharpVM
{
    public class Server
    {
        public static Dictionary<System.Net.IPAddress, Client> clients;
        public Server()
        {
            clients = new Dictionary<System.Net.IPAddress, Client>();
        }
    }
}
