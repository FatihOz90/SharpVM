using System.Collections.Generic;

namespace SharpVM
{
    public class Client
    {
        public System.Net.IPAddress clientIp { get; set; }
        public Dictionary<int?, TaskList> task = new Dictionary<int?, TaskList>();
        public Client(int? Id)
        {
            //clientIp = _clientIp;
            task.Add(Id, new TaskList());
        }

        //public void Disconnect()
        //{
        //    esxHost = null;
        //    vmFunc = null;
        //    esxUser = null;
        //    esxPass = null;
        //    esxip = null;
        //}
    }

    public class TaskList
    {
        public EsxHost esxHost;
        public VmFunc vmFunc;
        public HttpDataSender dataSender;
        public string esxUser { get; set; }
        public string esxPass { get; set; }
        public string esxip { get; set; }
        public Dictionary<string, DS_Action> action;
        public Dictionary<string, DS_CloneTool> cloneTool;
        public string retHttp { get; set; }
        public TaskList()
        {
            esxHost = new EsxHost();
            vmFunc = new VmFunc();
            dataSender = new HttpDataSender();
            action = new Dictionary<string, DS_Action>();
            cloneTool = new Dictionary<string, DS_CloneTool>();
        }
    }

    public class DS_Action
    {
        public string RetId { get; set; }
        public DSA_VMInfo vMInfo { get; set; }
    }

    public class DSA_VMInfo
    {
        public string Action { get; set; }
        public string VmName { get; set; }
        public string Step1 { get; set; }
        public string Step2 { get; set; }
        public dynamic VmConfig { get; set; }
        public dynamic VmRuntime { get; set; }
        public string Status { get; set; }
        public string ErrMsg { get; set; }
        public bool IsEnd { get; set; }
    }


    public class DS_CloneTool
    {
        public string RetId { get; set; }
        public DSClone_VMInfo vMInfo { get; set; }
    }

    public class DSClone_VMInfo
    {
        public string Action { get; set; }
        public string VmName { get; set; }
        public string Step1 { get; set; }
        public string Step2 { get; set; }
        public string Step3 { get; set; }
        public string Step4 { get; set; }
        public string Step5 { get; set; }
        public string Step6 { get; set; }
        public string Status { get; set; }
        public string ErrMsg { get; set; }
        public bool IsEnd { get; set; }
    }
}