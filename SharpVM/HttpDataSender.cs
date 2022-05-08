using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SharpVM
{
    public class HttpDataSender
    {
        public void DataSend_action(IPAddress _clientIp, int TaskId, string Uid, DSA_VMInfo info)
        {
            try
            {
                var searchData = Server.clients[_clientIp].task[TaskId].action.ContainsKey(Uid);
                if (searchData)
                {
                    //Update
                    if (info.Action != null)
                        Server.clients[_clientIp].task[TaskId].action[Uid].vMInfo.Action = info.Action;

                    if (info.VmName != null)
                        Server.clients[_clientIp].task[TaskId].action[Uid].vMInfo.VmName = info.VmName;

                    if (info.Step1 != null)
                        Server.clients[_clientIp].task[TaskId].action[Uid].vMInfo.Step1 = info.Step1;

                    if (info.Step2 != null)
                        Server.clients[_clientIp].task[TaskId].action[Uid].vMInfo.Step2 = info.Step2;

                    if (info.Status != null)
                        Server.clients[_clientIp].task[TaskId].action[Uid].vMInfo.Status = info.Status;

                    if (info.VmConfig != null)
                        Server.clients[_clientIp].task[TaskId].action[Uid].vMInfo.VmConfig = info.VmConfig;

                    if (info.VmRuntime != null)
                        Server.clients[_clientIp].task[TaskId].action[Uid].vMInfo.VmRuntime = info.VmRuntime;

                    if (info.IsEnd ? true : false)
                        Server.clients[_clientIp].task[TaskId].action[Uid].vMInfo.IsEnd = info.IsEnd;
                }
                else
                {
                    //Create
                    var dataAction = new DS_Action
                    {
                        vMInfo = new DSA_VMInfo()
                        {
                            Action = info.Action,
                            VmName = info.VmName,
                            Step1 = "Talep Alındı",
                            Status = "Wait",
                            IsEnd = false
                        }
                    };

                    Server.clients[_clientIp].task[TaskId].action.Add(Uid, dataAction);
                }

                var Senddata = new DS_Action { RetId = Uid, vMInfo = Server.clients[_clientIp].task[TaskId].action[Uid].vMInfo };
                var json = JsonConvert.SerializeObject(Senddata);
                var Postjsondata = new StringContent(json, Encoding.UTF8, "application/json");

                //Send Post Data
                var client = new HttpClient { BaseAddress = new Uri($"{Server.clients[_clientIp].task[TaskId].retHttp}") };
                client.PostAsync("/data.asp", Postjsondata).Wait();
            }
            catch (Exception ex)
            {
                Form1.instance.LogsWrite("ERROR", "DataSend_action -> " + ex.Message.ToString());
            }
        }

        public void DataSend_cloneVM(IPAddress _clientIp, int TaskId, string Uid, DSClone_VMInfo info)
        {
            try
            {
                var searchData = Server.clients[_clientIp].task[TaskId].cloneTool.ContainsKey(Uid);
                if (searchData)
                {
                    //Update
                    if (info.Action != null)
                        Server.clients[_clientIp].task[TaskId].cloneTool[Uid].vMInfo.Action = info.Action;

                    if (info.VmName != null)
                        Server.clients[_clientIp].task[TaskId].cloneTool[Uid].vMInfo.VmName = info.VmName;

                    if (info.Step1 != null)
                        Server.clients[_clientIp].task[TaskId].cloneTool[Uid].vMInfo.Step1 = info.Step1;//Var ise Önceki Sunucu siliniyor.

                    if (info.Step2 != null)
                        Server.clients[_clientIp].task[TaskId].cloneTool[Uid].vMInfo.Step2 = info.Step2;//Clon Alındı.

                    if (info.Step3 != null)
                        Server.clients[_clientIp].task[TaskId].cloneTool[Uid].vMInfo.Step3 = info.Step3;//Sunucu Ozellikleri Ayarlandı.

                    if (info.Step4 != null)
                        Server.clients[_clientIp].task[TaskId].cloneTool[Uid].vMInfo.Step4 = info.Step4;//Network Ayarları yapıldı.

                    if (info.Step5 != null)
                        Server.clients[_clientIp].task[TaskId].cloneTool[Uid].vMInfo.Step5 = info.Step5;

                    if (info.ErrMsg != null)
                        Server.clients[_clientIp].task[TaskId].cloneTool[Uid].vMInfo.ErrMsg = info.ErrMsg;

                    if (info.Status != null)
                        Server.clients[_clientIp].task[TaskId].cloneTool[Uid].vMInfo.Status = info.Status;

                    if (info.IsEnd ? true : false)
                        Server.clients[_clientIp].task[TaskId].cloneTool[Uid].vMInfo.IsEnd = info.IsEnd;
                }
                else
                {
                    //Create
                    var dataCloneTool = new DS_CloneTool
                    {
                        vMInfo = new DSClone_VMInfo()
                        {
                            Action = info.Action,
                            VmName = info.VmName,
                            Step1 = "Talep Alındı",
                            Status = "Wait",
                            IsEnd = false
                        }
                    };
                    Server.clients[_clientIp].task[TaskId].cloneTool.Add(Uid, dataCloneTool);
                }

                var Senddata = new DS_CloneTool { RetId = Uid, vMInfo = Server.clients[_clientIp].task[TaskId].cloneTool[Uid].vMInfo };
                var json = JsonConvert.SerializeObject(Senddata);
                var Postjsondata = new StringContent(json, Encoding.UTF8, "application/json");

                //Send Post Data
                var client = new HttpClient { BaseAddress = new Uri($"{Server.clients[_clientIp].task[TaskId].retHttp}") };
                client.PostAsync("/recvdata.php", Postjsondata).Wait();
            }
            catch (Exception ex)
            {
                Form1.instance.LogsWrite("ERROR", "DataSend_cloneVM -> " + ex.Message.ToString());
            }
        }
    }
}