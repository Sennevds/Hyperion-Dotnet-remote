using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Newtonsoft.Json.Linq;
using Renci.SshNet;

namespace Hyperion_remote_dotnet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Login_OnClick(object sender, RoutedEventArgs e)
        {
            SshClient ssh = new SshClient(IP.Text, Convert.ToInt32(Port.Text), Username.Text, Password.Text);
            ssh.Connect();
            var command = ssh.RunCommand("hyperion-remote -l");
            var result = command.Result;
            result = result.Substring(0, result.Length - 1);
            ssh.Disconnect();
            //JObject resJObject = JObject.Parse(result);
            //string value = (string) resJObject["effect"];
            string[] test = result.Split(Environment.NewLine.ToCharArray());
            foreach (string s in test)
            {
                string temp = s.Trim();
                if (temp.StartsWith("\"name\""))
                {
                    string temp2 = temp.Remove(0, 9);
                    string temp3 = temp2.Split(new char[] { '\"', '\"' })[1];
                    output.Items.Add(temp3 + "\n");
                }
            }
        }

        private void Output_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(output.SelectedItem.ToString());
        }
    }
}
