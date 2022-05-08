using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharpVM
{
    public class HttpService
    {
        public static HttpService instance;
        public static HttpListener listener;
        public static string HostName = "";
        public static int Port = 0;
        public static string url = "";
        public static int pageViews = 0;
        public static int requestCount = 0;
        bool runServer = true;
        public Task _task;
        public static string pageData = "<!DOCTYPE>" +
                                        "<html>" +
                                        "  <head>" + "    <title>HttpService</title>" + "  </head>" +
                                        "  <body>" +
                                        "    <p>VMWARE API SERVICE</p>" +
                                        "  </body>" +
                                        "</html>";


        public HttpService()
        {
            if (string.IsNullOrEmpty(Form1.instance.tbHostName.Text))
            {
                Form1.instance.tbHostName.Text = "127.0.0.1";
            }
            if (string.IsNullOrEmpty(Form1.instance.tbPort.Text))
            {
                Form1.instance.tbPort.Text = "8080";
            }

            instance = this;
        }

        public void HttpServiceStart()
        {
            HostName = Form1.instance.tbHostName.Text;
            Port = Convert.ToInt32(Form1.instance.tbPort.Text);
            url = "http://" + HostName + ":" + Port + "/";

            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            Form1.instance.LogsWrite("INFO", "Dinleme baglantisi aktif adres : " + url);

            // Istekleri Al
            Thread ThRunlistenTask = new Thread(new ThreadStart(RunlistenTask))
            { IsBackground = true };
            ThRunlistenTask.Start();
        }

        private protected void RunlistenTask()
        {
            Task.Run(async () => await IncomingConnections()).ConfigureAwait(true).GetAwaiter().GetResult();
            // Close the listener
            listener.Close();
        }

        public async Task IncomingConnections()
        {
            // kapatma istegi gelene kadar donmeye devam et
            while (runServer)
            {
                HttpListenerContext context = await listener.GetContextAsync().ConfigureAwait(true);
                _task = Task.Factory.StartNew(() => ProcessRequest(context));
            }
        }

        private void ProcessRequest(HttpListenerContext context)
        {
            HttpListenerRequest req = context.Request;//POST GET
            HttpListenerResponse resp = context.Response;
            IPAddress clientIp = req.RemoteEndPoint.Address;

            //Yeni Muster Bilgisini Kaydet
            if (!Server.clients.ContainsKey(clientIp))
                Server.clients.Add(clientIp, new Client(_task.Id));

            //Yeni görev bilgilerini kaydet..
            if (!Server.clients[clientIp].task.ContainsKey(_task.Id))
                Server.clients[clientIp].task.Add(_task.Id, new TaskList());

            if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/shutdown"))
            {
                Form1.instance.LogsWrite("INFO", "Kapatma istegi geldi.");
                runServer = false;
            }
            else if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/clonevm"))//Sunucu oluştur.
            {
                Form1.instance.LogsWrite("INFO", "CloneVM -> Başlangıç hazırlıkları yapılıyor.");

                //Default Param
                string _esxIp = req.QueryString.Get("esxIp");
                string _esxUsername = req.QueryString.Get("esxUsername");
                string _esxPassword = req.QueryString.Get("esxPassword");

                if (EsxiCheck(clientIp, _esxIp, _esxUsername, _esxPassword))
                {
                    //Clon Dir
                    string _datastoreName = req.QueryString.Get("datastorename");
                    //Clone Info
                    string _templateName = req.QueryString.Get("templatename");
                    string _templateId = req.QueryString.Get("templateid");
                    string _clonName = req.QueryString.Get("newname");
                    //Machine Config
                    string _defaultusername = req.QueryString.Get("defaultusername");
                    string _defaultpassword = req.QueryString.Get("defaultpassword");
                    string _newpassword = req.QueryString.Get("newpassword");
                    string _Ram = req.QueryString.Get("ram");
                    string _Cpu = req.QueryString.Get("cpu");
                    string _CpuPerSok = req.QueryString.Get("cpuper");
                    string _DiskCap = req.QueryString.Get("disk");
                    //Network
                    string _address = req.QueryString.Get("address");
                    string _gateway = req.QueryString.Get("gateway");
                    string _netmask = req.QueryString.Get("netmask");
                    //Return Info
                    string _RetHttp = req.QueryString.Get("rethttp");
                    string _RedId = req.QueryString.Get("retId");

                    var _dataArray = new string[]
                    {
                        _datastoreName, //0
                        _templateName, //1
                        _templateId, //2
                        _clonName, //3
                        _newpassword, //4
                        _Ram, //5
                        _Cpu, //6
                        _CpuPerSok,//7 
                        _DiskCap, //8
                        
                        _address,//9
                        _gateway,//10
                        _netmask,//11
                        _defaultusername,//12
                        _defaultpassword,//13
                        _RetHttp,//14
                        _RedId//15
                    };

                    foreach (var item in _dataArray)
                    {
                        if (!ParametreCheck(item, clientIp))
                        {
                            HttpServiceClose(resp, clientIp);
                            return;
                        }
                    }

                    //Gonderilecek Dataları oluşturmaya başla.
                    Server.clients[clientIp].task[_task.Id].retHttp = _RetHttp;
                    Server.clients[clientIp].task[_task.Id].dataSender.DataSend_cloneVM(clientIp, _task.Id, _RedId, new DSClone_VMInfo() { Action = "clonevm", VmName = _clonName, Step1 = "Başlangıç Hazırlıkları yapılıyor." });


                    Server.clients[clientIp].task[_task.Id].vmFunc.CloneTools(clientIp, _dataArray);
                }
                else
                {
                    Form1.instance.LogsWrite("ERROR", $"Cvm -> {clientIp} ESXI Bağlantısı yapılamadı.");
                }
            }
            else if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/action"))//start/stop/shutdown/restart/suspend işlemleri
            {
                //Default Param
                string _esxIp = req.QueryString.Get("esxIp");
                string _esxUsername = req.QueryString.Get("esxUsername");
                string _esxPassword = req.QueryString.Get("esxPassword");

                if (EsxiCheck(clientIp, _esxIp, _esxUsername, _esxPassword))
                {
                    string _action = req.QueryString.Get("action");
                    string _vmname = req.QueryString.Get("vmname");
                    //Return Info
                    string _RetHttp = req.QueryString.Get("rethttp");
                    string _RedId = req.QueryString.Get("retId");

                    ParametreCheck(_action, clientIp);
                    ParametreCheck(_vmname, clientIp);
                    ParametreCheck(_vmname, clientIp);

                    ParametreCheck(_RetHttp, clientIp);
                    ParametreCheck(_RedId, clientIp);

                    //Gonderilecek Dataları oluşturmaya başla.
                    Server.clients[clientIp].task[_task.Id].retHttp = _RetHttp;
                    Server.clients[clientIp].task[_task.Id].dataSender.DataSend_action(clientIp, _task.Id, _RedId, new DSA_VMInfo() { Action = _action, IsEnd = false, VmName = _vmname });

                    switch (_action)
                    {
                        case "start":
                            {
                                Server.clients[clientIp].task[_task.Id].vmFunc.PowerOnVM_Task(clientIp, _vmname, _RedId).Wait();
                                break;
                            }
                        case "reset":
                            {
                                Server.clients[clientIp].task[_task.Id].vmFunc.ResetVM_Task(clientIp, _vmname, _RedId).Wait();
                                break;
                            }
                        case "stop":
                            {
                                Server.clients[clientIp].task[_task.Id].vmFunc.PowerOffVM_Task(clientIp, _vmname, _RedId).Wait();
                                break;
                            }
                        case "suspend":
                            {
                                Server.clients[clientIp].task[_task.Id].vmFunc.SuspendVM_Task(clientIp, _vmname, _RedId).Wait();
                                break;
                            }
                        case "remove":
                            {
                                Server.clients[clientIp].task[_task.Id].vmFunc.RemoveVM_Task(clientIp, _vmname, _RedId).Wait();
                                break;
                            }
                        case "consoleid":
                            {
                                Server.clients[clientIp].task[_task.Id].vmFunc.Ticket(clientIp, _vmname, _RedId).Wait();
                                break;
                            }
                        case "vminfo":
                            {
                                Server.clients[clientIp].task[_task.Id].vmFunc.VmInfo(_RedId, clientIp, _vmname).Wait();
                                break;
                            }
                        default:
                            {
                                Form1.instance.LogsWrite("ERROR", $"{clientIp} Action Geçersiz.");
                                break;
                            }
                    }
                }
                else
                {
                    Form1.instance.LogsWrite("ERROR", $"VpsAction -> {clientIp} ESXI Bağlantısı yapılamadı.");
                }
            }
            else
            {
                Form1.instance.LogsWrite("ERROR", "Hatalı istek geldi.");
            }

            HttpServiceClose(resp, clientIp);
        }

        private void HttpServiceClose(HttpListenerResponse resp, IPAddress _clientIp)
        {
            string disableSubmit = !runServer ? "disabled" : "";
            byte[] data = Encoding.UTF8.GetBytes(String.Format(pageData, pageViews, disableSubmit));
            resp.ContentType = "text/html";
            resp.ContentEncoding = Encoding.UTF8;
            resp.ContentLength64 = data.LongLength;
            resp.OutputStream.WriteAsync(data, 0, data.Length).Wait();
            resp.Close();

            //Server.clients[_clientIp].task.Remove(_task.Id);
        }

        private bool EsxiCheck(IPAddress _clientIp, string esxIp, string esxUsername, string esxPassword)
        {
            int? Id = Task.CurrentId;

            ParametreCheck(esxIp, _clientIp);
            ParametreCheck(esxUsername, _clientIp);
            ParametreCheck(esxPassword, _clientIp);

            //Server Info
            Server.clients[_clientIp].task[Id].esxip = esxIp;
            Server.clients[_clientIp].task[Id].esxUser = esxUsername;
            Server.clients[_clientIp].task[Id].esxPass = esxPassword;

            if (!Server.clients[_clientIp].task[Id].esxHost.ConnectToESXClient())
            {
                if (Server.clients[_clientIp].task[Id].esxHost.LoginToESXClient(esxIp, esxUsername, esxPassword))
                    return true;
                else
                    return false;
            }
            else { return true; }
        }

        private bool ParametreCheck(string param, IPAddress _clientIp)
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                Form1.instance.LogsWrite("ERROR", $"CloneVM -> Gelen Parametre Hatası.(IsNullOrWhiteSpace)");
                return false;
            }
            else if (string.IsNullOrEmpty(param))
            {
                Form1.instance.LogsWrite("ERROR", $"CloneVM -> Gelen Parametre Hatası. (IsNullOrEmpty)");
                return false;
            }
            else return true;
        }
    }
}