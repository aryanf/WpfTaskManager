using CoreComponents;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCore.Mocks
{
    public class MockProcessAdapter : IProcessAdapter
    {
        private List<IProcessItem> processItems;
        private MockProcessWatcher mockProcessWatcher;

        public MockProcessAdapter(MockProcessWatcher mockProcessWatcher)
        {
            processItems = new List<IProcessItem>();
            this.mockProcessWatcher = mockProcessWatcher;
        }

        public IProcessItem[] GetProcesses()
        {
            return processItems.ToArray();
        }

        public void KillProcessById(int processId)
        {
            IProcessItem processItem = processItems.First(p => p.ProcessId == processId);
            string processName = processItem.ProcessName;
            processItems.Remove(processItem);
            mockProcessWatcher.ProcessDeleted(processId, processName);
        }

        public void Start(string processName)
        {
            int processId = new Random().Next(1, 100000);
            IProcessItem processItem = new ProcessItem() { ProcessId = processId, ProcessName = processName };
            processItems.Add(processItem);
            mockProcessWatcher.ProcessCreated(processId, processName);
        }

        public void FeedFakeRunningProcesses(List<IProcessItem> runningProcesses)
        {
            processItems = runningProcesses;
        }
    }
}
