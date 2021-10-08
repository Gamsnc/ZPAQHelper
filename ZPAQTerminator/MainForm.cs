using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using common;

namespace ZPAQTerminator
{
    public partial class MainForm : Form
    {
        
        public MainForm(string[] args)
        {
            InitializeComponent();
            //args = new string[] { "a \"C:\\Users\\Gamsn\\Desktop\\CefSharpTest.zpaq.zpaq\"  \"C:\\Users\\Gamsn\\Desktop\\CefSharpTest.zpaq\" \"C:\\Users\\Gamsn\\Desktop\\GeckofxTest.zpaq\" -m5 -t" };
            

            if (args.Length > 0)
            {
                string command = Encoding.UTF8.GetString(Convert.FromBase64String(args[0]));

                //MessageBox.Show(AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
                Process[] processes = Process.GetProcessesByName("zpaq64");
                foreach (Process instance in processes)
                {
                    string commandline = ProcessCommandline.GetCommandLineArgs(instance).Replace("\"" + AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "zpaq64.exe\"", "").Trim();
                    string c = command.Replace("\"" + AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "zpaq64.exe\"", "").Trim();
                    //MessageBox.Show(commandline);
                    //MessageBox.Show(c);
                    if (commandline.IndexOf(c) >= 0)
                    {
                        //MessageBox.Show(args[0]);
                        //判断是否以管理员身份运行，不是则提示
                        if (!O.IsRunAsAdmin())
                        {
                            ProcessStartInfo psi = new ProcessStartInfo();
                            psi.WorkingDirectory = Environment.CurrentDirectory;
                            psi.FileName = Application.ExecutablePath;
                            psi.Arguments = args[0];
                            psi.UseShellExecute = true;
                            psi.Verb = "runas";
                            Process p = new Process();
                            p.StartInfo = psi;
                            p.Start();
                            this.Dispose();
                            Process.GetCurrentProcess().Kill();
                            return;
                        }
                        else
                        {
                            instance.Kill();
                            break;
                        }
                    }
                }
            }
            Process.GetCurrentProcess().Kill();
        }

        
        
    }
}
