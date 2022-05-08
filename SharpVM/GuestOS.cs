using System.Collections.Generic;


public class GuestOS
{
    public static GuestOS instance;
    public Dictionary<string, _GuesOs> _GuestList = new Dictionary<string, _GuesOs>();


    public GuestOS()
    {
        //Centos List
        _GuestList.Add("centos7_64Guest", new _GuesOs { Name = "centos7_64Guest", Value = "CentOS 7 (64-bit)" });
        _GuestList.Add("centos7Guest", new _GuesOs { Name = "centos7Guest", Value = "CentOS 7" });
        //Debian List
        //_GuestList.Add("debian4_64Guest", new _GuesOs { Name = "debian4_64Guest", Value = "Debian GNU/Linux 4 (64 bit)" });
        //_GuestList.Add("debian5_64Guest", new _GuesOs { Name = "debian5_64Guest", Value = "Debian GNU/Linux 5 (64 bit)" });
        //_GuestList.Add("debian5Guest", new _GuesOs { Name = "debian5Guest", Value = "Debian GNU/Linux 5" });
        //_GuestList.Add("debian6_64Guest", new _GuesOs { Name = "debian6_64Guest", Value = "Debian GNU/Linux 6 (64 bit)" });
        //_GuestList.Add("debian7_64Guest", new _GuesOs { Name = "debian7_64Guest", Value = "	Debian GNU/Linux 7 (64 bit)" });
        _GuestList.Add("debian8_64Guest", new _GuesOs { Name = "debian8_64Guest", Value = "Debian GNU/Linux 8 (64 bit)" });
        //_GuestList.Add("debian8Guest", new _GuesOs { Name = "debian8Guest", Value = "Debian GNU/Linux 8" });
        _GuestList.Add("debian9_64Guest", new _GuesOs { Name = "debian9_64Guest", Value = "Debian GNU/Linux 9 (64 bit)" });
        //_GuestList.Add("debian9Guest", new _GuesOs { Name = "debian9Guest", Value = "Debian GNU/Linux 9" });
        _GuestList.Add("debian10Guest", new _GuesOs { Name = "debian10Guest", Value = "Debian GNU/Linux 10" });
        //Windows List
        //_GuestList.Add("windows7_64Guest", new _GuesOs { Name = "windows7_64Guest", Value = "Windows 7 (64 bit)" });
        _GuestList.Add("windows8_64Guest", new _GuesOs { Name = "windows8_64Guest", Value = "Windows 8 (64 bit)" });
        //_GuestList.Add("windows8Guest", new _GuesOs { Name = "windows8Guest", Value = "Windows 8" });
        _GuestList.Add("windows8Server64Guest", new _GuesOs { Name = "windows8Server64Guest", Value = "Windows 8 Server (64 bit)" });
        _GuestList.Add("windows9_64Guest", new _GuesOs { Name = "windows9_64Guest", Value = "Windows 10 (64 bit)" });
        //_GuestList.Add("windows9Guest", new _GuesOs { Name = "windows9Guest", Value = "Windows 10" });
        _GuestList.Add("windows9Server64Guest", new _GuesOs { Name = "windows9Server64Guest", Value = "Windows 10 Server (64 bit)" });

        instance = this;
    }


}

