using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using Common;
using Interfaces;

namespace CoreComponents
{
    public class MyProcessManager : IProcessManager
    {
        private readonly List<IProcessItem> processList;
        private readonly ISimpleLogger simpleLoger;
        private readonly IProcessWatcher processWatcher;
        private readonly IProcessAdapter processAdapter;


        private void OnListChanged()
        {
            ListChanged?.Invoke(processList, new EventArgs());
        }

        public event EventHandler ListChanged;

        public MyProcessManager(ISimpleLogger simpleLogger, IProcessWatcher processWatcher, IProcessAdapter processAdapter)
        {
            this.simpleLoger = simpleLogger;
            this.processWatcher = processWatcher;
            this.processAdapter = processAdapter;
            processList = new List<IProcessItem>();
        }

        /// <summary>
        /// Load all existing processes to processList
        /// </summary>
        public void Load()
        {
            try
            {
                IProcessItem[] myProcs = processAdapter.GetProcesses();
                foreach (IProcessItem process in myProcs)
                {
                    processList.Add(new ProcessItem() { ProcessId = process.ProcessId, ProcessName = process.ProcessName });
                }
                OnListChanged();
            }
            catch (Exception ex)
            {
                // Log exception for developer
                simpleLoger.Log(ex.Message);
                throw;
            }
        }

        public void StartWatch()
        {
            processWatcher.ProcessTriggered += HandleEventArrived;
            processWatcher.StartWatch();
        }

        private void HandleEventArrived(object sender, MyEventArgs e)
        {
            int processId = e.ProcessId;
            string processName = e.ProcessName;

            if (e.Action == ProcessAction.Creation)
            {
                AddProcessToList(processId, processName);
                OnListChanged();
            }
            if (e.Action == ProcessAction.Deletion)
            {
                DeleteProcessFromList(processId);
                OnListChanged();
            }
        }


        /// <summary>
        /// Return list of running processes
        /// </summary>
        /// <returns>ObservableCollection<IProcessItem></returns>
        public List<IProcessItem> GetProcesses()
        {
            return processList;
        }

        /// <summary>
        /// Terminate processes by processIds
        /// </summary>
        /// <param name="processIds"></param>
        public void TerminateProcesses(int[] processIds)
        {
            try
            {
                foreach (int processId in processIds)
                {

                    processAdapter.KillProcessById(processId);
                }
            }
            catch (Exception ex)
            {
                // Log exception for developer
                simpleLoger.Log(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Run a new process
        /// </summary>
        /// <param name="processName"></param>
        public void RunProcess(string processName)
        {
            try
            {
                processAdapter.Start(processName);
            }
            catch (Exception ex)
            {
                // Log exception for developer
                simpleLoger.Log(ex.Message);
                throw;
            }
        }

        public void DeleteProcessFromList(int processId)
        {
            IProcessItem processItem = processList.FirstOrDefault(t => t.ProcessId == processId);
            if (processItem != null)
            {
                processList.Remove(processList.First(t => t.ProcessId == processId));
            }
            else
            {
                simpleLoger.Log($"{processId} is not a valid process Id.");
            }
        }

        public void AddProcessToList(int processId, string processName)
        {
            processList.Add(new ProcessItem() { ProcessId = processId, ProcessName = processName });
        }
    }
}
