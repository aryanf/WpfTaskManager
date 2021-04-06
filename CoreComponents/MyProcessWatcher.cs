using Common;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace CoreComponents
{
    public class MyProcessWatcher : IProcessWatcher
    {
        private readonly ManagementEventWatcher creationWatcher;
        private readonly ManagementEventWatcher deletionWatcher;

        public event MyEnvetHandler ProcessTriggered;

        public MyProcessWatcher()
        {
            WqlEventQuery creationQuery = new WqlEventQuery("__InstanceCreationEvent", new TimeSpan(0, 0, 1), "TargetInstance isa \"Win32_Process\"");
            creationWatcher = new ManagementEventWatcher(creationQuery);
            creationWatcher.EventArrived += new EventArrivedEventHandler(OnProcessCreated);

            WqlEventQuery deletionQuery = new WqlEventQuery("__InstanceDeletionEvent", new TimeSpan(0, 0, 1), "TargetInstance isa \"Win32_Process\"");
            deletionWatcher = new ManagementEventWatcher(deletionQuery);
            deletionWatcher.EventArrived += new EventArrivedEventHandler(OnProcessDeleted);
        }

        public void StartWatch()
        {
            creationWatcher.Start();
            deletionWatcher.Start();
        }

        private void OnProcessCreated(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject targetInstance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
            int processId = int.Parse((targetInstance)["ProcessID"].ToString());
            string processName = (targetInstance)["Name"].ToString();
            MyEventArgs myEventArgs = new MyEventArgs()
            {
                ProcessId = processId,
                ProcessName = processName,
                Action = ProcessAction.Creation
            };
            ProcessTriggered?.Invoke(this, myEventArgs);
        }

        private void OnProcessDeleted(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject targetInstance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
            int processId = int.Parse((targetInstance)["ProcessID"].ToString());
            string processName = (targetInstance)["Name"].ToString();
            MyEventArgs myEventArgs = new MyEventArgs()
            {
                ProcessId = processId,
                ProcessName = processName,
                Action = ProcessAction.Deletion
            };
            ProcessTriggered?.Invoke(this, myEventArgs);
        }
    }
}
