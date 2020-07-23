using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace FileSystemVisitor.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string StartFolderPath { get; set; }
        public List<string> Logs { get; set; }
        public List<string> Nodes { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Logs = new List<string>();
            Nodes = new List<string>();
        }

        private void ChooseFolderButton_Click(object sender, RoutedEventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    StartFolderPath = folderBrowserDialog.SelectedPath;
                    this.SelectedFolderPathTextBox.Text = StartFolderPath;
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SystemVisitor visitor;

            ClearData();
            visitor = InitializeVisitor();
            RegisterEventHandlers(visitor);
            var nodes = visitor.StartSearch(StartFolderPath);

            Nodes.AddRange(nodes.Select(n => n.Path).ToList());

            ResultsListBox.ItemsSource = Nodes;
            LogsListBox.ItemsSource = Logs;
        }

        private void ClearData()
        {
            Logs.Clear();
            Nodes.Clear();
            ResultsListBox.ItemsSource = null;
            LogsListBox.ItemsSource = null;
        }

        private void AddLog(string log)
        {
            Logs.Add(log);
        }

        private bool FilterFileNode(Node node, string pattern)
        {
            return node.Path.Contains(pattern);
        }

        private void RegisterEventHandlers(SystemVisitor visitor)
        {
            visitor.NotifySearchStart += AddLog;
            visitor.NotifySearchFinish += AddLog;
            visitor.NotifyFileFound += AddLog;
            visitor.NotifyFolderFound += AddLog;
            visitor.NotifyFileFiltered += AddLog;
            visitor.NotifyFolderFiltered += AddLog;
        }

        private SystemVisitor InitializeVisitor()
        {
            var visitor = string.IsNullOrEmpty(PatternTextBox.Text) ?
                new SystemVisitor(this.StopFilterCheckBox.IsChecked.Value) : 
                new SystemVisitor(FilterFileNode, PatternTextBox.Text, this.StopFilterCheckBox.IsChecked.Value);

            return visitor;
        }
    }
}
