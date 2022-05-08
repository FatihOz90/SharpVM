using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using VMware.Vim;

namespace SharpVM
{
    public class VmFunc
    {
        public async System.Threading.Tasks.Task PowerOnVM_Task(System.Net.IPAddress clientIp, string vm, string retId = null)
        {
            int? Id = System.Threading.Tasks.Task.CurrentId;

            var SelectMachine = VmEntity.GetVm(Server.clients[clientIp].task[Id].esxHost.vimClient, vm);
            if (SelectMachine == null)
            {
                Form1.instance.LogsWrite("ERROR", $"{clientIp} -> Machine Not Found. IN:{vm}");

                if (retId != null)
                    Server.clients[clientIp].task[Id].dataSender.DataSend_action(clientIp, (int)Id, retId, new DSA_VMInfo() { Action = "start", IsEnd = true, VmName = vm, Step2 = "Machine Not Found.", Status = "Failed" });
            }
            else
            {
                var _PowerOnVM_Task = SelectMachine.PowerOnVM_Task(null);
                Server.clients[clientIp].task[Id].esxHost.vimClient.WaitForTask(_PowerOnVM_Task);
            }

            if (retId != null)
                Server.clients[clientIp].task[Id].dataSender.DataSend_action(clientIp, (int)Id, retId, new DSA_VMInfo() { Action = "start", IsEnd = true, VmName = vm, Step2 = "Başlatıldı.", Status = "Success" });

            await System.Threading.Tasks.Task.CompletedTask;
        }

        public async System.Threading.Tasks.Task PowerOffVM_Task(System.Net.IPAddress clientIp, string vm, string retId = null)
        {
            int? Id = System.Threading.Tasks.Task.CurrentId;

            var SelectMachine = VmEntity.GetVm(Server.clients[clientIp].task[Id].esxHost.vimClient, vm);
            if (SelectMachine == null)
            {
                Form1.instance.LogsWrite("ERROR", $"{clientIp} -> Machine Not Found. IN:{vm}");
                Server.clients[clientIp].task[Id].dataSender.DataSend_action(clientIp, (int)Id, retId, new DSA_VMInfo() { Action = "stop", IsEnd = true, VmName = vm, Step2 = "Machine Not Found.", Status = "Failed" });
            }
            else
            {
                var _PowerOffVM_Task = SelectMachine.PowerOffVM_Task();
                if (retId != null)
                    Server.clients[clientIp].task[Id].esxHost.vimClient.WaitForTask(_PowerOffVM_Task);
            }

            if (retId != null)
                Server.clients[clientIp].task[Id].dataSender.DataSend_action(clientIp, (int)Id, retId, new DSA_VMInfo() { Action = "stop", IsEnd = true, VmName = vm, Step2 = "Resetlendi.", Status = "Success" });

            await System.Threading.Tasks.Task.CompletedTask;
        }

        public async System.Threading.Tasks.Task SuspendVM_Task(System.Net.IPAddress clientIp, string vm, string retId = null)
        {
            int? Id = System.Threading.Tasks.Task.CurrentId;

            var SelectMachine = VmEntity.GetVm(Server.clients[clientIp].task[Id].esxHost.vimClient, vm);
            if (SelectMachine == null)
            {
                Form1.instance.LogsWrite("ERROR", $"{clientIp} -> Machine Not Found. IN:{vm}");
                //vms.Clear();

                if (retId != null)
                    Server.clients[clientIp].task[Id].dataSender.DataSend_action(clientIp, (int)Id, retId, new DSA_VMInfo() { Action = "suspend", IsEnd = true, VmName = vm, Step2 = "Machine Not Found.", Status = "Failed" });
            }
            else
            {
                var _SuspendVM_Task = SelectMachine.SuspendVM_Task();
                Server.clients[clientIp].task[Id].esxHost.vimClient.WaitForTask(_SuspendVM_Task);
            }

            //vms.Clear();

            if (retId != null)
                Server.clients[clientIp].task[Id].dataSender.DataSend_action(clientIp, (int)Id, retId, new DSA_VMInfo() { Action = "suspend", IsEnd = true, VmName = vm, Step2 = "suspend edildi.", Status = "Success" });

            await System.Threading.Tasks.Task.CompletedTask;
        }

        public async System.Threading.Tasks.Task ResetVM_Task(System.Net.IPAddress clientIp, string vm, string retId = null)
        {
            int? Id = System.Threading.Tasks.Task.CurrentId;

            var SelectMachine = VmEntity.GetVm(Server.clients[clientIp].task[Id].esxHost.vimClient, vm);
            if (SelectMachine == null)
            {
                Form1.instance.LogsWrite("ERROR", $"{clientIp} -> Machine Not Found. IN:{vm}");

                //vms.Clear();

                if (retId != null)
                    Server.clients[clientIp].task[Id].dataSender.DataSend_action(clientIp, (int)Id, retId, new DSA_VMInfo() { Action = "reset", IsEnd = true, VmName = vm, Step2 = "Machine Not Found.", Status = "Failed" });


            }
            else
            {
                var _ResetVM_Task = SelectMachine.ResetVM_Task();
                Server.clients[clientIp].task[Id].esxHost.vimClient.WaitForTask(_ResetVM_Task);
            }

            //vms.Clear();

            if (retId != null)
                Server.clients[clientIp].task[Id].dataSender.DataSend_action(clientIp, (int)Id, retId, new DSA_VMInfo() { Action = "reset", IsEnd = true, VmName = vm, Step2 = "resetlendi.", Status = "Success" });

            await System.Threading.Tasks.Task.CompletedTask;
        }

        public async System.Threading.Tasks.Task RemoveVM_Task(System.Net.IPAddress clientIp, string vm, string retId = null)
        {
            int? Id = System.Threading.Tasks.Task.CurrentId;

            var SelectMachine = VmEntity.GetVm(Server.clients[clientIp].task[Id].esxHost.vimClient, vm);
            if (SelectMachine == null)
            {
                Form1.instance.LogsWrite("ERROR", $"{clientIp} -> Machine Not Found. IN:{vm}");
                if (retId != null)
                    Server.clients[clientIp].task[Id].dataSender.DataSend_action(clientIp, (int)Id, retId, new DSA_VMInfo() { Action = "remove", IsEnd = true, VmName = vm, Step2 = "Machine Not Found.", Status = "Failed" });

            }
            else
            {
                var _Destroy_Task = SelectMachine.Destroy_Task();
                Server.clients[clientIp].task[Id].esxHost.vimClient.WaitForTask(_Destroy_Task);
            }

            if (retId != null)
                Server.clients[clientIp].task[Id].dataSender.DataSend_action(clientIp, (int)Id, retId, new DSA_VMInfo() { Action = "remove", IsEnd = true, VmName = vm, Step2 = "Mevcut Makine Silindi.", Status = "Success" });

            await System.Threading.Tasks.Task.CompletedTask;
        }

        public async System.Threading.Tasks.Task Ticket(System.Net.IPAddress clientIp, string vm, string retId = null)
        {
            int? Id = System.Threading.Tasks.Task.CurrentId;
            VirtualMachineMksTicket _ticket = null;

            var SelectMachine = VmEntity.GetVm(Server.clients[clientIp].task[Id].esxHost.vimClient, vm);
            if (SelectMachine == null)
            {
                Form1.instance.LogsWrite("ERROR", $"{clientIp} -> Machine Not Found. IN:{vm}");

                if (retId != null)
                    Server.clients[clientIp].task[Id].dataSender.DataSend_action(clientIp, (int)Id, retId, new DSA_VMInfo() { Action = "ticket", IsEnd = true, VmName = vm, Step2 = "Machine Not Found.", Status = "Failed" });
            }
            else
            {
                _ticket = SelectMachine.AcquireMksTicket();
            }

            if (retId != null)
                Server.clients[clientIp].task[Id].dataSender.DataSend_action(clientIp, (int)Id, retId, new DSA_VMInfo() { Action = "ticket", IsEnd = true, VmName = vm, Step2 = "Consol Başlatıldı.", Status = "Success" });

            await System.Threading.Tasks.Task.CompletedTask;
        }

        public async System.Threading.Tasks.Task VmInfo(string retId, System.Net.IPAddress clientIp, string vm)
        {
            int? Id = System.Threading.Tasks.Task.CurrentId;

            var SelectMachine = VmEntity.GetVm(Server.clients[clientIp].task[Id].esxHost.vimClient, vm);
            if (SelectMachine == null)
            {
                Form1.instance.LogsWrite("ERROR", $"{clientIp} -> Machine Not Found. IN:{vm}");
                if (retId != null)
                    Server.clients[clientIp].task[Id].dataSender.DataSend_action(clientIp, (int)Id, retId, new DSA_VMInfo() { Action = "VmInfo", IsEnd = true, VmName = vm, Step2 = "Machine Not Found.", Status = "Failed" });

                await System.Threading.Tasks.Task.CompletedTask;
            }

            if (retId != null)
                Server.clients[clientIp].task[Id].dataSender.DataSend_action(clientIp, (int)Id, retId, new DSA_VMInfo() { Action = "VmInfo", IsEnd = true, VmName = vm, Step2 = "Bilgi Alındı.", VmConfig = SelectMachine.Config, VmRuntime = SelectMachine.Runtime, Status = "Success" });
        }

        public void CloneTools(System.Net.IPAddress clientIp, string[] data)
        {
            int? Id = System.Threading.Tasks.Task.CurrentId;

            ////Eger Mevcutta bir makine var ise sil.
            if (VmEntity.GetVm(Server.clients[clientIp].task[Id].esxHost.vimClient, data[3]) != null)
            {
                Server.clients[clientIp].task[Id].vmFunc.PowerOffVM_Task(clientIp, data[3], null).Wait();
                Server.clients[clientIp].task[Id].vmFunc.RemoveVM_Task(clientIp, data[3], null).Wait();
            }

            //Send Data
            Server.clients[clientIp].task[Id].dataSender.DataSend_cloneVM(clientIp, (int)Id, data[15], new DSClone_VMInfo() { IsEnd = false, Step2 = "Var ise Önceki Sunucu siliniyor." });

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = @".\\win32\ovftool.exe",
                Arguments = $" --acceptAllEulas --noSSLVerify -ds=datastore1 -n={data[3]} --diskMode=thin vi://{Server.clients[clientIp].task[Id].esxUser}:{Server.clients[clientIp].task[Id].esxPass}@{Server.clients[clientIp].task[Id].esxip}/{data[1]} vi://{Server.clients[clientIp].task[Id].esxUser}:{Server.clients[clientIp].task[Id].esxPass}@{Server.clients[clientIp].task[Id].esxip}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };
            var proc = Process.Start(psi);
            proc.WaitForExit();
            var tex = proc.StandardOutput.ReadToEnd();
            proc.Close();
            //var tex = "successfully";
            if (tex.ToString().Contains("successfully"))
            {
                //Send Data
                Server.clients[clientIp].task[Id].dataSender.DataSend_cloneVM(clientIp, (int)Id, data[15], new DSClone_VMInfo() { IsEnd = false, Step3 = "Clon Alındı." });

                VirtualMachine GetCreateServer = VmEntity.GetVm(Server.clients[clientIp].task[Id].esxHost.vimClient, data[3]);
                var _datastore = VmEntity.GetDatastore(Server.clients[clientIp].task[Id].esxHost.vimClient, data[0]);
                VirtualDisk virtualDisk = (VirtualDisk)GetCreateServer.Config.Hardware.Device.FirstOrDefault(x => x.DeviceInfo.Label.Contains("Hard disk"));

                var Config = new VirtualMachineConfigSpec
                {
                    MemoryMB = Convert.ToInt32(data[5]),
                    Name = data[3],
                    NumCPUs = Convert.ToInt32(data[6]),
                    NumCoresPerSocket = Convert.ToInt32(data[7])
                };

                var ReconfigTask = GetCreateServer.ReconfigVM_Task(Config);
                Server.clients[clientIp].task[Id].esxHost.vimClient.WaitForTask(ReconfigTask);

                //HardDisk Or Other Drivers
                List<VirtualDeviceConfigSpec> deviceConfigSpecList = new List<VirtualDeviceConfigSpec>();
                VirtualDisk disk = new VirtualDisk();
                disk = virtualDisk;
                disk.CapacityInBytes = (long?)(Convert.ToDouble(data[8]) * (1024.0d * 1024.0d));
                disk.CapacityInKB = (long)(Convert.ToDouble(data[8]) * (1024.0d));
                VirtualDeviceConfigSpec diskSpec = new VirtualDeviceConfigSpec
                {
                    Operation = VirtualDeviceConfigSpecOperation.edit,
                    Device = disk,
                };
                deviceConfigSpecList.Add(diskSpec);

                var ReconfigVM_Task = GetCreateServer.ReconfigVM_Task(new VirtualMachineConfigSpec()
                {
                    DeviceChange = deviceConfigSpecList.ToArray()
                });
                Server.clients[clientIp].task[Id].esxHost.vimClient.WaitForTask(ReconfigVM_Task);
                //Send Data
                Server.clients[clientIp].task[Id].dataSender.DataSend_cloneVM(clientIp, (int)Id, data[15], new DSClone_VMInfo() { IsEnd = false, Step4 = "Sunucu Ozellikleri Ayarlandı." });


                //Sunucuyu Başlat.
                if (GetCreateServer.Guest.GuestState != "running")
                    Server.clients[clientIp].task[Id].vmFunc.PowerOnVM_Task(clientIp, GetCreateServer.Name, null).Wait();
                //Send Data
                Server.clients[clientIp].task[Id].dataSender.DataSend_cloneVM(clientIp, (int)Id, data[15], new DSClone_VMInfo() { IsEnd = false, Step5 = "Sunucu Başlatıldı." });


                switch (data[2]/*_templateId*/)
                {
                    case "windows":
                        {
                            Server.clients[clientIp].task[Id].vmFunc.WindowsLogin_Update(clientIp, GetCreateServer, data);
                            //Send Data
                            Server.clients[clientIp].task[Id].dataSender.DataSend_cloneVM(clientIp, (int)Id, data[15], new DSClone_VMInfo() { IsEnd = true, Step6 = "Network Ayarları Yapıldı.", Status = "Success" });
                            break;
                        }
                    case "centos7_64Guest":
                    case "centos8_64Guest":
                        {
                            Server.clients[clientIp].task[Id].vmFunc.centos7_8_64Login_Update(clientIp, GetCreateServer, data);
                            //Send Data
                            Server.clients[clientIp].task[Id].dataSender.DataSend_cloneVM(clientIp, (int)Id, data[15], new DSClone_VMInfo() { IsEnd = true, Step6 = "Network Ayarları Yapıldı.", Status = "Success" });
                            break;
                        }
                    case "ubuntu16_64Guest":
                        {
                            Server.clients[clientIp].task[Id].vmFunc.ubuntu16_64Login_Update(clientIp, GetCreateServer, data);
                            //Send Data
                            Server.clients[clientIp].task[Id].dataSender.DataSend_cloneVM(clientIp, (int)Id, data[15], new DSClone_VMInfo() { IsEnd = true, Step6 = "Network Ayarları Yapıldı.", Status = "Success" });
                            break;
                        }
                    case "ubuntu18_64Guest":
                    case "ubuntu20_64Guest":
                        {
                            Server.clients[clientIp].task[Id].vmFunc.ubuntu18_20_64Login_Update(clientIp, GetCreateServer, data);
                            //Send Data
                            Server.clients[clientIp].task[Id].dataSender.DataSend_cloneVM(clientIp, (int)Id, data[15], new DSClone_VMInfo() { IsEnd = true, Step6 = "Network Ayarları Yapıldı.", Status = "Success" });
                            break;
                        }
                    case "debian8_64Guest":
                    case "debian9_64Guest":
                    case "debian10_64Guest":
                        {
                            Server.clients[clientIp].task[Id].vmFunc.debian_8_9_10_64Login_Update(clientIp, GetCreateServer, data);
                            Server.clients[clientIp].task[Id].dataSender.DataSend_cloneVM(clientIp, (int)Id, data[15], new DSClone_VMInfo() { IsEnd = true, Step6 = "Network Ayarları Yapıldı.", Status = "Success" });
                            break;
                        }
                    default:
                        {
                            //Send Data
                            Server.clients[clientIp].task[Id].dataSender.DataSend_cloneVM(clientIp, (int)Id, data[15], new DSClone_VMInfo() { IsEnd = true, ErrMsg = "Geçerli bir işletim sistemi gelmedi. (TemplateID)", Status = "Failed" });
                            Form1.instance.LogsWrite("ERROR", $"Cvm -> {clientIp} İşletim Sistemi Bulunamadı. -> {data[2]/*_templateId*/}");
                        }
                        break;
                }
            }
        }

        public void WindowsLogin_Update(System.Net.IPAddress clientIp, VirtualMachine vm, string[] data)
        {
            string ethname = "Ethernet2";
            string ethernet_ip = $"'netsh interface ipv4 set address name={ethname} static {data[9]} {data[11]} {data[10]}'";
            string DNS_ip = $"'netsh interface ipv4 set dns name={ethname} static 8.8.8.8'";
            string powershellPath = "C:\\Windows\\system32\\WindowsPowerShell\\v1.0\\powershell.exe";
            string ChangePassword = $"net user administrator  {data[4]}";

            //Windows IP değistir
            RunProgramInGuest(clientIp, vm, powershellPath, ethernet_ip, data).ConfigureAwait(false).GetAwaiter().GetResult();
            RunProgramInGuest(clientIp, vm, powershellPath, DNS_ip, data).ConfigureAwait(false).GetAwaiter().GetResult();
            RunProgramInGuest(clientIp, vm, powershellPath, ChangePassword, data).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public void centos7_8_64Login_Update(System.Net.IPAddress clientIp, VirtualMachine vm, string[] data)
        {
            var config = File.ReadAllText("..\\..\\bashscript\\centos7_8_64.sh");

            //IP_ADDRESS
            config = config.Replace("IP_ADDRESS", data[9]);
            //IP_GATEWAY
            config = config.Replace("IP_GATEWAY", data[10]);
            //IP_NETMASK
            config = config.Replace("IP_NETMASK", data[11]);


            var permcentos764 = "chmod 777 config.sh";
            var startbashscript = "bash config.sh";
            //var changePassword = $"echo -e ${data[4]}'\n'${data[4]} | passwd root";
            var changePassword = $"echo \"{data[12]}:{data[4]}\" | chpasswd";
            //centos764 IP&Dns değistir
            RunProgramInGuest(clientIp, vm, @"/bin/echo", config, data).ConfigureAwait(false).GetAwaiter().GetResult();
            RunProgramInGuest(clientIp, vm, @"/bin/bash", permcentos764, data).ConfigureAwait(false).GetAwaiter().GetResult();
            RunProgramInGuest(clientIp, vm, @"/bin/bash", startbashscript, data).ConfigureAwait(false).GetAwaiter().GetResult();
            ////centos764 Sifre degistir.
            RunProgramInGuest(clientIp, vm, @"/bin/bash", changePassword, data).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public void ubuntu16_64Login_Update(System.Net.IPAddress clientIp, VirtualMachine vm, string[] data)
        {
            var config = File.ReadAllText("..\\..\\bashscript\\ubuntu16.sh");
            //IP_ADDRESS
            config = config.Replace("IP_ADDRESS", data[9]);
            //IP_GATEWAY
            config = config.Replace("IP_GATEWAY", data[10]);
            //IP_NETMASK
            config = config.Replace("IP_NETMASK", data[11]);

            var permcentos764 = "chmod 777 config.sh";
            var startbashscript = "bash config.sh";
            var changePassword = $"echo -e ${data[4]}'\n'${data[4]} | passwd root";

            //IP&Dns değistir
            RunProgramInGuest(clientIp, vm, @"/bin/echo", config, data).ConfigureAwait(false).GetAwaiter().GetResult();
            RunProgramInGuest(clientIp, vm, @"/bin/bash", permcentos764, data).ConfigureAwait(false).GetAwaiter().GetResult();
            RunProgramInGuest(clientIp, vm, @"/bin/bash", startbashscript, data).ConfigureAwait(false).GetAwaiter().GetResult();
            //Sifre degistir.
            RunProgramInGuest(clientIp, vm, @"/bin/bash", changePassword, data).ConfigureAwait(false).GetAwaiter().GetResult();

        }

        public void ubuntu18_20_64Login_Update(System.Net.IPAddress clientIp, VirtualMachine vm, string[] data)
        {
            var config = File.ReadAllText("..\\..\\bashscript\\ubuntu18_20.sh");

            //IP_ADDRESS
            config = config.Replace("IP_ADDRESS", data[9]);
            //IP_GATEWAY
            config = config.Replace("IP_GATEWAY", data[10]);
            //IP_NETMASK
            config = config.Replace("IP_NETMASK", data[11]);


            var permcentos764 = "chmod 777 config.sh";
            var startbashscript = "bash config.sh";
            var changePassword = $"echo \"{data[12]}:{data[4]}\" | chpasswd";
            //IP&Dns değistir
            RunProgramInGuest(clientIp, vm, @"/bin/echo", config, data).ConfigureAwait(false).GetAwaiter().GetResult();
            RunProgramInGuest(clientIp, vm, @"/bin/bash", permcentos764, data).ConfigureAwait(false).GetAwaiter().GetResult();
            RunProgramInGuest(clientIp, vm, @"/bin/bash", startbashscript, data).ConfigureAwait(false).GetAwaiter().GetResult();
            //Sifre degistir.
            RunProgramInGuest(clientIp, vm, @"/bin/bash", changePassword, data).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public void debian_8_9_10_64Login_Update(System.Net.IPAddress clientIp, VirtualMachine vm, string[] data)
        {
            var config = File.ReadAllText("..\\..\\bashscript\\debian_8_9_10.sh");

            //IP_ADDRESS
            config = config.Replace("IP_ADDRESS", data[9]);
            //IP_GATEWAY
            config = config.Replace("IP_GATEWAY", data[10]);
            //IP_NETMASK
            config = config.Replace("IP_NETMASK", data[11]);

            var permcentos764 = "chmod 777 config.sh";
            var startbashscript = "bash config.sh";
            //var changePassword = $"echo -e ${data[4]}'\n'${data[4]} | passwd root";
            var changePassword = $"echo \"{data[12]}:{data[4]}\" | chpasswd";
            //Change Ip&Dns
            RunProgramInGuest(clientIp, vm, @"/bin/echo", config, data).ConfigureAwait(false).GetAwaiter().GetResult();
            RunProgramInGuest(clientIp, vm, @"/bin/bash", permcentos764, data).ConfigureAwait(false).GetAwaiter().GetResult();
            RunProgramInGuest(clientIp, vm, @"/bin/bash", startbashscript, data).ConfigureAwait(false).GetAwaiter().GetResult();
            //Change Password
            RunProgramInGuest(clientIp, vm, @"/bin/bash", changePassword, data).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        //data[12] => password
        private async System.Threading.Tasks.Task RunProgramInGuest(System.Net.IPAddress clientIp, VirtualMachine GetVMx, string shellPath, string arguments, string[] data)
        {
            GuestOperationsManager _guestOperationsManager;
            NamePasswordAuthentication authentication;
            _guestOperationsManager = new GuestOperationsManager(GetVMx.Client, GetVMx.Client.ServiceContent.GuestOperationsManager);
            _guestOperationsManager.UpdateViewData();
            int? Id = System.Threading.Tasks.Task.CurrentId;

            GuestAuthManager auth = new GuestAuthManager(GetVMx.Client, _guestOperationsManager.AuthManager);
            try
            {
                authentication = new NamePasswordAuthentication
                {
                    Username = data[12],
                    Password = data[13],
                    InteractiveSession = false
                };

                int retCount = 0;
                while (true)
                {
                    //VirtualMachineToolsStatus.toolsOk
                    if (GetVMx.Guest.ToolsStatus.GetValueOrDefault() == VirtualMachineToolsStatus.toolsNotInstalled || GetVMx.Guest.ToolsStatus.GetValueOrDefault() == VirtualMachineToolsStatus.toolsNotRunning)
                    {
                        Thread.Sleep(1000);
                        GetVMx.UpdateViewData();
                        retCount++;

                        if (retCount == 15)
                        {
                            Server.clients[clientIp].task[Id].dataSender.DataSend_cloneVM(clientIp, (int)Id, data[15], new DSClone_VMInfo() { IsEnd = true, ErrMsg = "VmwareTools Çalışmıyor.", Status = "Failed" });
                            return;
                        }
                    }
                    else { break; }
                }

                auth.ValidateCredentialsInGuest(GetVMx.MoRef, authentication);

                GuestProgramSpec args = new GuestProgramSpec();

                switch (data[2]/*_templateId*/)
                {
                    case "windows":
                        {
                            args.WorkingDirectory = "c:\\";
                            args.ProgramPath = shellPath;
                            args.Arguments = $"{arguments} | cmd";
                            break;
                        }
                    case "centos7_64Guest":
                    case "centos8_64Guest":
                        {
                            args.ProgramPath = shellPath;
                            StringBuilder openPortScript = new StringBuilder(arguments);

                            var env = new string[] { "PATH=/usr/local/sbin:/usr/local/bin:/sbin:/bin:/usr/sbin:/usr/bin:/root/bin", "SHELL=/bin/bash" };
                            string[] newstring = new string[env.Length];
                            args.EnvVariables = env.ToArray();
                            args.WorkingDirectory = "/root";

                            if (shellPath == "/bin/echo")
                                args.Arguments = $"\"{arguments}\"> config.sh";
                            else
                                args.Arguments = "-c \"" + openPortScript.ToString() + "\"";

                            break;
                        }
                    case "ubuntu16_64Guest":
                    case "ubuntu18_64Guest":
                    case "ubuntu20_64Guest":
                        {
                            args.ProgramPath = shellPath;
                            StringBuilder openPortScript = new StringBuilder(arguments);

                            var env = new string[] { "PATH=/usr/local/sbin:/usr/local/bin:/sbin:/bin:/usr/sbin:/usr/bin:/root/bin", "SHELL=/bin/bash" };
                            string[] newstring = new string[env.Length];
                            args.EnvVariables = env.ToArray();
                            args.WorkingDirectory = "/root";

                            if (shellPath == "/bin/echo")
                                args.Arguments = $"\"{arguments}\"> config.sh";
                            else
                                args.Arguments = "-c \"" + openPortScript.ToString() + "\"";

                            break;
                        }
                    case "debian8_64Guest":
                    case "debian9_64Guest":
                    case "debian10_64Guest":
                        {
                            args.ProgramPath = shellPath;
                            StringBuilder openPortScript = new StringBuilder(arguments);

                            var env = new string[] { "PATH=/usr/local/sbin:/usr/local/bin:/sbin:/bin:/usr/sbin:/usr/bin:/root/bin", "SHELL=/bin/bash" };
                            string[] newstring = new string[env.Length];
                            args.EnvVariables = env.ToArray();
                            args.WorkingDirectory = "/root";

                            if (shellPath == "/bin/echo")
                                args.Arguments = $"\"{arguments}\"> config.sh";
                            else
                                args.Arguments = "-c \"" + openPortScript.ToString() + "\"";

                            break;
                        }
                    default:
                        break;
                }

                GuestProcessManager GPM = new GuestProcessManager(GetVMx.Client, ((GuestOperationsManager)GetVMx.Client.GetView(GetVMx.Client.ServiceContent.GuestOperationsManager, null)).ProcessManager);
                GPM.StartProgramInGuest(GetVMx.MoRef, authentication, args);
            }
            catch (Exception ex)
            {
                Form1.instance.LogsWrite("ERROR", "RunProgram-> " + ex.Message);
            }

            await System.Threading.Tasks.Task.CompletedTask;
        }
    }
}