using System;
using VMware.Vim;


namespace SharpVM
{
    public class EsxHost
    {
        private string _ipAddress { get; set; }
        public bool userIsConnected { get; set; } = false;
        public bool userIsLoggedIn { get; set; } = false;
        public VimClient vimClient { get; }

        public EsxHost()
        {
            vimClient = new VimClientImpl
            {
                IgnoreServerCertificateErrors = true
            };
        }

        public bool ConnectToESXClient()
        {
            try
            {
                if (_ipAddress == null) return userIsConnected;

                if (!userIsConnected)
                {
                    vimClient.Connect("https://" + _ipAddress + "/sdk");
                    userIsConnected = true;
                    Form1.instance.LogsWrite("INFO", "Connection to: \'" + _ipAddress + "\' has been established.");
                }
                else
                {
                    Form1.instance.LogsWrite("INFO", "Connection to: \'" + _ipAddress + "\' has already been established.");
                }
            }
            catch (Exception e)
            {
                switch (e.GetType().ToString())
                {
                    //wrong IP Address
                    case "VMware.Vim.VimEndpointNotFoundException":
                        Form1.instance.LogsWrite("ERROR", "IP Address: \'" + _ipAddress + "\' is not an ESX host or is powered off!");
                        userIsConnected = false;
                        userIsLoggedIn = false;
                        break;
                    //wrong user name/password
                    case "VMware.Vim.VimException":
                        Form1.instance.LogsWrite("ERROR", $"{_ipAddress} VMware.Vim.VimException EXCEPTION CAUGHT!");
                        userIsLoggedIn = false;
                        break;
                    default:
                        Form1.instance.LogsWrite("ERROR", $"{_ipAddress} SPECIFIC EXCEPTION NOT CAUGHT!");
                        break;
                }

            }
            return userIsConnected;
        }

        public bool LoginToESXClient(string ipAddress, string esxUsername, string esxPassword)
        {
            try
            {
                vimClient.Connect($"https://" + ipAddress + "/sdk");
                userIsConnected = true;
                vimClient.Login(esxUsername, esxPassword);
                userIsLoggedIn = true;
                _ipAddress = ipAddress;
                Form1.instance.LogsWrite("INFO", $"User: {esxUsername} on: {ipAddress} isValid: True");
            }
            catch (Exception e)
            {
                switch (e.GetType().ToString())
                {
                    //wrong IP Address
                    case "VMware.Vim.VimEndpointNotFoundException":
                        Form1.instance.LogsWrite("ERROR", $"{ipAddress} -> is not an ESX host or is powered off!");
                        Form1.instance.LogsWrite("ERROR", $"{ipAddress} ->  Error: " + e.Message);
                        userIsConnected = false;
                        userIsLoggedIn = false;
                        break;
                    //wrong user name/password
                    case "VMware.Vim.VimException":
                        Form1.instance.LogsWrite("ERROR", $"{ipAddress} -> VMware.Vim.VimException EXCEPTION CAUGHT!");
                        Form1.instance.LogsWrite("ERROR", $"{ipAddress} ->  Error: " + e.Message);
                        userIsLoggedIn = false;
                        break;
                    default:
                        Form1.instance.LogsWrite("ERROR", $"{ipAddress} -> SPECIFIC EXCEPTION NOT CAUGHT!");
                        Form1.instance.LogsWrite("ERROR", $"{ipAddress} ->  Error: " + e.Message);
                        break;
                }
            }
            return userIsLoggedIn;
        }
    }
}
