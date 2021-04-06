using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreComponents
{
    public class MyProcessAdapter : IProcessAdapter
    {
        public MyProcessAdapter()
        {

        }

        public void Start(string processName)
        {
            System.Diagnostics.Process.Start(processName);
        }

        public void KillProcessById(int processId)
        {

            System.Diagnostics.Process process = System.Diagnostics.Process.GetProcessById(processId);
            process.Kill();
        }

        public IProcessItem[] GetProcesses()
        {
            return System.Diagnostics.Process.GetProcesses().OrderBy(p => p.ProcessName).Select(p => new ProcessItem() { ProcessId = p.Id, ProcessName = p.ProcessName }).ToArray();
        }
    }
}
