using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IProcessManager
    {
        event EventHandler ListChanged;
        void Load();
        List<IProcessItem> GetProcesses();
        void TerminateProcesses(int[] processIds);
        void RunProcess(string processName);
        void DeleteProcessFromList(int processId);
        void AddProcessToList(int processId, string processName);
        void StartWatch();
    }
}
