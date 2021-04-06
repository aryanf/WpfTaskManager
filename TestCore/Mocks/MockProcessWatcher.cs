using Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace TestCore.Mocks
{
    public class MockProcessWatcher : IProcessWatcher
    {

        public event MyEnvetHandler ProcessTriggered;

        public MockProcessWatcher()
        {

        }

        public void StartWatch()
        {

        }

        public void ProcessCreated(int processId, string processName)
        {
            MyEventArgs myEventArgs = new MyEventArgs()
            {
                ProcessId = processId,
                ProcessName = processName,
                Action = ProcessAction.Creation
            };
            ProcessTriggered?.Invoke(this, myEventArgs);
        }

        public void ProcessDeleted(int processId, string processName)
        {
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
