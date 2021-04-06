using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCore.Mocks
{
    class MockProcessManager : IProcessManager
    {
        public event EventHandler ListChanged;

        public void AddProcessToList(int processId, string processName)
        {
            throw new NotImplementedException();
        }

        public void DeleteProcessFromList(int processId)
        {
            throw new NotImplementedException();
        }

        public List<IProcessItem> GetProcesses()
        {
            throw new NotImplementedException();
        }

        public void Load()
        {
            throw new NotImplementedException();
        }

        public void RunProcess(string processName)
        {
            throw new NotImplementedException();
        }

        public void StartWatch()
        {
            throw new NotImplementedException();
        }

        public void TerminateProcesses(int[] processIds)
        {
            throw new NotImplementedException();
        }
    }
}
