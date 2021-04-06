
using CoreComponents;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private IProcessManager processManager;
        private IProcessWatcher processWatcher;
        private ISimpleLogger simpleLogger;
        private IProcessAdapter processAdapeter;
        public MainWindow()
        {
            InitializeComponent();

            // Add columns to ListView
            ReshapeListView();

            Task.Run(() => DoWork());
        }

        void DoWork()
        {
            processWatcher = new MyProcessWatcher();
            simpleLogger = new MySimpleLogger();
            processAdapeter = new MyProcessAdapter();
            processManager = new MyProcessManager(simpleLogger, processWatcher, processAdapeter);
            processManager.ListChanged += HandleListChanged;
            processManager.Load();
            processManager.StartWatch();
        }

        private void ReshapeListView()
        {
            var gridView = new GridView();
            processListView.View = gridView;
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "ProcessId",
                DisplayMemberBinding = new Binding("ProcessId"),
                Width = 60
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "ProcessName",
                DisplayMemberBinding = new Binding("ProcessName"),
                Width = 200
            });
        }

        private void TerminateBtn_Click(object sender, RoutedEventArgs e)
        {
            int[] selectedProcessIds = processListView.SelectedItems.Cast<ProcessItem>().Select(p => p.ProcessId).ToArray();
            Task.Run(() => processManager.TerminateProcesses(selectedProcessIds));
        }

        private void RunBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RunProcess(runTextBox.Text);
            }
            catch (Exception ex)
            {
                // show the exception to the user, it can be designed to catch specific exception and show it the user.
                MessageBoxShow(ex.Message);
            }
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                RunProcess(runTextBox.Text);
            }
        }

        private void RunProcess(string processName)
        {
            try
            {
                Task.Run(() => processManager.RunProcess(processName));
            }
            catch (Exception ex)
            {
                // show the exception to the user, it can be designed to catch specific exception and show it the user.
                MessageBoxShow(ex.Message);
            }
        }

        private void HandleListChanged(object sender, EventArgs e)
        {
            // call the ui thread to update ui component
            this.Dispatcher.Invoke(() =>
            {
                processListView.ItemsSource = null;
                processListView.ItemsSource = processManager.GetProcesses();
                processListView.Items.Refresh();
            });
        }

        private void MessageBoxShow(string Message)
        {
            MessageBox.Show(Message);
        }
    }
}
