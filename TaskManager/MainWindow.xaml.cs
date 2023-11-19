using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace TaskManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
 
    public partial class MainWindow : Window
    {
        private Process Create_process()
        {
            Process process = new Process();
            process.StartInfo.FileName = Process_box.Text;
            process.Start();
            Status_text.Foreground = Brushes.Green;
            Status_text.Text = $"{process.ProcessName} - start launched";
         //   Status_text.Foreground = Brushes.Black;
            return process;
        }
        private void Process_view()
        { 
            var allProcesses = Process.GetProcesses();
        }
        private void Delete_process ()
        {
            try
            {
               

                foreach (var process in Process.GetProcessesByName(Process_box.Text))
                {
                    process.Kill();
                }

               
                Status_text.Text = $"{Process_box.Text} - delete process"; 
            }
            catch (Exception ex)
            {
                Status_text.Foreground = Brushes.Red;
                Status_text.Text = ex.Message;
              //  Status_text.Foreground = Brushes.Black;
            }

        }
        private Process Edit_priority(Process process)
        {
            Status_text.Text = $"{process.ProcessName} - priority = {process.PriorityClass}";
            process.PriorityClass = ProcessPriorityClass.Normal;
       
            return process;
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Task_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem Task_list = ((sender as ListBox).SelectedItem as ListBoxItem);
        }

     
        private void LoadProcessInfo()
        {
            processInfoListBox.Items.Clear();
            Process[] processes = Process.GetProcesses().Take(10).ToArray();
            processInfoListBox.Items.Add($"Processe's: {processes.Length}");
            processInfoListBox.Items.Add("pid - name - start time - life time");
            foreach (Process process in processes)
            {
                string pid, name, startTime, lifeTime;
                pid = process.Id.ToString();
                name = process.ProcessName;
                try
                {
                    pid = process.Id.ToString();
                }
                catch
                {
                    pid = "unavailable";
                }
                try
                {
                    name = process.ProcessName;
                }
                catch
                {
                    name = "unavailable";
                }
                try
                {
                    startTime = process.StartTime.ToString();
                }
                catch
                {
                    startTime = "unavailable";
                }
                try
                {
                    lifeTime = (DateTime.Now - process.StartTime).TotalSeconds.ToString();
                }
                catch
                {
                    lifeTime = "unavailable";
                }
                processInfoListBox.Items.Add($"{pid} - {name} - {startTime} - {lifeTime}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {   LoadProcessInfo();

                Status_text.Foreground = Brushes.Green;
                Status_text.Text = "Load process, your Windows";
              //  Status_text.Foreground = Brushes.Black;

            }
            catch (Exception ex) 
            {

                Status_text.Foreground = Brushes.Red;
                Status_text.Text = ex.Message;
              //  Status_text.Foreground = Brushes.Black;
            }
           
        }

     

        private void New_process(object sender, RoutedEventArgs e)
        {
            Create_process();
        }

        private void Button_kill_process(object sender, RoutedEventArgs e)
        {
            Delete_process();
        }

        private void Process_box_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
