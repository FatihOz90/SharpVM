using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using VMware.Vim;


public class VmEntity
{
    public static List<T> GetEntities<T>(VimClient vimClient, ManagedObjectReference beginEntity, NameValueCollection filter, string[] properties)
    {
        List<T> things = new List<T>();
        List<EntityViewBase> vBase = vimClient.FindEntityViews(typeof(T), beginEntity, filter, properties);

        foreach (EntityViewBase eBase in vBase)
        {
            T thing = (T)(object)eBase;
            things.Add(thing);
        }
        return things;
    }

    public static T GetEntity<T>(VimClient vimClient, ManagedObjectReference beginEntity, NameValueCollection filter, string[] properties)
    {
        EntityViewBase vBase = vimClient.FindEntityView(typeof(T), beginEntity, filter, properties);
        T thing = (T)(object)vBase;
        return thing;
    }

    public static T GetObject<T>(VimClient vimClient, ManagedObjectReference moRef, string[] properties)
    {
        ViewBase vBase = vimClient.GetView(moRef, properties);
        T thisObject = (T)(object)vBase;
        return thisObject;
    }

    public static HostSystem GetHost(VimClient vimClient)
    {
        return GetEntity<HostSystem>(vimClient, null, null, null);
    }

    public static VirtualMachineConfigOption virtualMachineConfigOption(VimClient vimClient, ManagedObjectReference beginEntity, string filter)
    {
        NameValueCollection Filter = new NameValueCollection();

        if (filter != null)
            Filter.Add("name", filter);
        else
            Filter = null;

        return GetEntity<VirtualMachineConfigOption>(vimClient, beginEntity, Filter, null);
    }

    public static string GetGuestFamily(VimClient vimClient, string MoRefString)
    {
        ManagedObjectReference SourceVmMoRef = new ManagedObjectReference(MoRefString);
        VirtualMachine SourceVm = GetObject<VirtualMachine>(vimClient, SourceVmMoRef, null);
        return SourceVm.Guest.GuestFamily;
    }

    public static List<VirtualMachine> GetVms(VimClient vimClient)
    {
        List<VirtualMachine> lstVirtualMachines = GetEntities<VirtualMachine>(vimClient, null, null, null);
        var VirtualMachines = new List<VirtualMachine>();
        foreach (VirtualMachine itmVm in lstVirtualMachines)
            VirtualMachines.Add(itmVm);

        return VirtualMachines;
    }

    public static VirtualMachine GetVm(VimClient vimClient, string VmName)
    {

        var vms = VmEntity.GetVms(vimClient);
        VirtualMachine SelectMachine = null;

        for (int i = 0; i < vms.Count; i++)
            if (vms[i].Name == VmName) SelectMachine = vms[i];

        vms.Clear();

        return SelectMachine;
        //NameValueCollection Filter = new NameValueCollection();
        //if (VmName != null)
        //{
        //    Filter.Add("name", VmName);
        //}
        //else
        //{
        //    Filter = null;
        //}
        //ManagedObjectReference beginEntity = null;
        //if (MoRefString != null)
        //{
        //    beginEntity = new ManagedObjectReference(MoRefString);
        //}
        //VirtualMachine itmVirtualMachine = GetEntity<VirtualMachine>(vimClient, beginEntity, Filter, null);
        //
        //return itmVirtualMachine;
    }

    public static List<_Object> GetClusters(VimClient vimClient, string Value)
    {
        //Config.GuestFullName
        NameValueCollection Filter = new NameValueCollection
        {
            { "name", Value }
        };
        List<ClusterComputeResource> lstClusters = GetEntities<ClusterComputeResource>(vimClient, null, Filter, null);
        lstClusters = lstClusters.OrderBy(thisCluster => thisCluster.Name).ToList();
        List<_Object> Clusters = new List<_Object>();
        foreach (ClusterComputeResource itmClster in lstClusters)
        {
            Clusters.Add(new _Object { Name = itmClster.Name, Value = itmClster.MoRef.ToString() });
        }
        return Clusters;
    }

    public static _Object GetVObject(VimClient vimClient, string MoRefString, Type type)
    {
        ManagedObjectReference MoRef = new ManagedObjectReference(MoRefString);
        _Object sObject = new _Object();

        switch (type)
        {
            case Type.ClusterComputeResource:
                ClusterComputeResource itmCluster = GetObject<ClusterComputeResource>(vimClient, MoRef, null);
                sObject.Name = itmCluster.Name;
                sObject.Value = itmCluster.Value.ToString();
                break;
            case Type.VirtualMachine:
                VirtualMachine itmVirtualMachine = GetObject<VirtualMachine>(vimClient, MoRef, null);
                sObject.Name = itmVirtualMachine.Name;
                sObject.Value = itmVirtualMachine.MoRef.ToString();
                break;
            case Type.Datastore:
                Datastore itmDatastore = GetObject<Datastore>(vimClient, MoRef, null);
                sObject.Name = itmDatastore.Name;
                sObject.Value = itmDatastore.MoRef.ToString();
                break;
            case Type.PortGroup:
                DistributedVirtualPortgroup itmDistributedVirtualPortGroup = GetObject<DistributedVirtualPortgroup>(vimClient, MoRef, null);
                sObject.Name = itmDistributedVirtualPortGroup.Name;
                sObject.Value = itmDistributedVirtualPortGroup.MoRef.ToString();
                break;
        }
        return sObject;
    }

    public static Hashtable GetOsCustomization(VimClient vimClient)
    {
        CustomizationSpecManager specManager = GetObject<CustomizationSpecManager>(vimClient, vimClient.ServiceContent.CustomizationSpecManager, null);
        Hashtable Customizations = new Hashtable();
        foreach (CustomizationSpecInfo specInfo in specManager.Info)
        {
            Customizations.Add(specInfo.Name, specInfo.Name + "." + specInfo.Type);
        }
        return Customizations;
    }

    public static List<_Object> GetDatastores(VimClient vimClient)
    {

        var SelectedDatastore = GetEntity<HostSystem>(vimClient, null, null, null);

        List<Datastore> lstDatastores = new List<Datastore>();
        foreach (ManagedObjectReference dsMoRef in SelectedDatastore.Datastore)
        {
            ViewBase dsView = vimClient.GetView(dsMoRef, null);
            Datastore clusterDS = (Datastore)dsView;
            lstDatastores.Add(clusterDS);
        }

        List<_Object> Datastores = new List<_Object>();
        lstDatastores = lstDatastores.OrderByDescending(thisStore => thisStore.Info.FreeSpace).ToList();
        foreach (Datastore itmDatastore in lstDatastores)
        {
            Datastores.Add(new _Object { Name = itmDatastore.Name, Value = itmDatastore.MoRef.ToString() });
        }
        return Datastores;
    }

    public static Datastore GetDatastore(VimClient vimClient, string datastore)
    {

        NameValueCollection Filter = new NameValueCollection();
        if (datastore != null)
        {
            Filter.Add("name", datastore);
        }
        else
        {
            Filter = null;
        }

        var SelectedDatastore = GetEntity<Datastore>(vimClient, null, Filter, null);

        return SelectedDatastore;
    }

    public static Hashtable GetDatacenters(VimClient vimClient)
    {
        NameValueCollection Filter = new NameValueCollection();
        HostSystem SelectedCluster = GetEntity<HostSystem>(vimClient, null, null, null);
        Filter.Add("hostfolder", SelectedCluster.Parent.Value);
        Datacenter itmDatacenter = GetEntity<Datacenter>(vimClient, null, null, null);
        Hashtable DC = new Hashtable
        {
            { itmDatacenter.Name, itmDatacenter.MoRef }
        };
        return DC;
    }

    public static Datacenter GetDatacenter(VimClient vimClient)
    {
        NameValueCollection Filter = new NameValueCollection();
        HostSystem SelectedCluster = GetEntity<HostSystem>(vimClient, null, null, null);
        Filter.Add("hostfolder", SelectedCluster.Parent.Value);
        Datacenter itmDatacenter = GetEntity<Datacenter>(vimClient, null, null, null);
        return itmDatacenter;
    }

    public static Hashtable GetPortGroups(VimClient vimClient, string MoRefString, string Value)
    {
        NameValueCollection Filter = new NameValueCollection();
        ManagedObjectReference beginEntity = null;
        if (MoRefString != null)
        {
            beginEntity = new ManagedObjectReference(MoRefString);
        }
        ClusterComputeResource SelectedCluster = GetObject<ClusterComputeResource>(vimClient, beginEntity, null);
        Filter.Add("hostfolder", SelectedCluster.Parent.Value);
        Datacenter itmDatacenter = GetEntity<Datacenter>(vimClient, null, Filter, null);
        Filter.Remove("hostfolder");
        if (Value != null)
            Filter.Add("name", Value);
        else
            Filter = null;

        List<DistributedVirtualPortgroup> lstPortGroups = GetEntities<DistributedVirtualPortgroup>(vimClient, itmDatacenter.MoRef, Filter, null);
        Hashtable PortGroup = new Hashtable();
        foreach (DistributedVirtualPortgroup itmPortGroup in lstPortGroups)
        {
            PortGroup.Add(itmPortGroup.Name, itmPortGroup.MoRef);
        }
        return PortGroup;
    }

    public static Hashtable GetResourcePools(VimClient vimClient)
    {
        NameValueCollection Filter = new NameValueCollection();
        var SelectedResourcePool = GetEntity<HostSystem>(vimClient, null, null, null);

        Filter.Add("parent", SelectedResourcePool.Parent.Value);
        List<ResourcePool> lstResourcePools = GetEntities<ResourcePool>(vimClient, SelectedResourcePool.Parent, Filter, null);
        Hashtable ResourcePools = new Hashtable();
        foreach (ResourcePool itmResourcePool in lstResourcePools)
        {
            ResourcePools.Add(itmResourcePool.Name, itmResourcePool.MoRef.ToString());
        }
        return ResourcePools;
    }

    public static ResourcePool GetResourcePool(VimClient vimClient)
    {
        NameValueCollection Filter = new NameValueCollection();
        var SelectedResourcePool = GetEntity<HostSystem>(vimClient, null, null, null);
        Filter.Add("parent", SelectedResourcePool.Parent.Value);
        ResourcePool lstResourcePools = GetEntity<ResourcePool>(vimClient, SelectedResourcePool.Parent, Filter, null);
        return lstResourcePools;
    }

    public static string getVolumeName(string volName)
    {
        string volumeName;
        if (volName != null && volName.Length > 0)
            volumeName = "[" + volName + "]";
        else
            volumeName = "[Local]";

        return volumeName;
    }
}
