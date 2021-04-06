using System;
using CoreComponents;
using TestCore.Mocks;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace TestCore
{
    [TestClass]
    public class TestMyProcessManager
    {
        private MyProcessManager processManager;
        private MockProcessAdapter processAdapter;
        private MockProcessWatcher processWatcher;
        private MockSimpleLogger simpleLogger;

        [TestInitialize]
        public void TestInitialize()
        {
            processWatcher = new MockProcessWatcher();
            processAdapter = new MockProcessAdapter(processWatcher);
            simpleLogger = new MockSimpleLogger();
        }

        [TestMethod]
        public void TestRunProcess()
        {
            // Assign
            List<IProcessItem> fakeRunningProcesses = new List<IProcessItem>() {
                new ProcessItem(){ ProcessId = 13, ProcessName = "FakeProcess13"},
                new ProcessItem(){ ProcessId = 113, ProcessName = "FakeProcess113"},
                new ProcessItem(){ ProcessId = 1113, ProcessName = "FakeProcess1113"},
                new ProcessItem(){ ProcessId = 11113, ProcessName = "FakeProcess11113"},
            };
            processAdapter.FeedFakeRunningProcesses(fakeRunningProcesses);
            processManager = new MyProcessManager(simpleLogger, processWatcher, processAdapter);
            processManager.Load();
            processManager.StartWatch();

            // Run
            string processName = "newTask.exe";
            processManager.RunProcess(processName);

            // Assert
            Assert.AreEqual(5, processManager.GetProcesses().Count, "Unexpected number of processes after running a process.");
            Assert.IsTrue(processManager.GetProcesses().Any(p => p.ProcessName == processName), "Process is not added to the list after running a process.");
        }

        [TestMethod]
        public void TestTerminateProcess()
        {
            // Assign
            List<IProcessItem> fakeRunningProcesses = new List<IProcessItem>() {
                new ProcessItem(){ ProcessId = 13, ProcessName = "FakeProcess13"},
                new ProcessItem(){ ProcessId = 113, ProcessName = "FakeProcess113"},
                new ProcessItem(){ ProcessId = 1113, ProcessName = "FakeProcess1113"},
                new ProcessItem(){ ProcessId = 11113, ProcessName = "FakeProcess11113"},
            };
            processAdapter.FeedFakeRunningProcesses(fakeRunningProcesses);
            processManager = new MyProcessManager(simpleLogger, processWatcher, processAdapter);
            processManager.Load();
            processManager.StartWatch();

            // Run
            processManager.TerminateProcesses(new int[] { 113, 11113 });

            // Assert
            Assert.AreEqual(2, processManager.GetProcesses().Count, "Unexpected number of processes after running a process.");
            Assert.IsFalse(processManager.GetProcesses().Any(p => p.ProcessId == 113), "Process 113 still running after terminating.");
            Assert.IsFalse(processManager.GetProcesses().Any(p => p.ProcessId == 11113), "Process 11113 still running after terminating.");
        }

        [TestCleanup]
        public void TestCleanup()
        {

        }
    }
}
