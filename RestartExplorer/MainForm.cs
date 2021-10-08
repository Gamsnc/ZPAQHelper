using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using common;

namespace RestartExplorer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            Process[] processes = Process.GetProcessesByName("explorer");
            foreach (Process instance in processes)
            {
                string commandline = ProcessCommandline.GetCommandLineArgs(instance).ToLower().Trim();
                //"C:\WINDOWS\explorer.exe"           
                string[] possiblename = new string[] { "\"f:\\windows\\explorer.exe\"", "f:\\windows\\explorer.exe", "\"e:\\windows\\explorer.exe\"", "e:\\windows\\explorer.exe", "\"d:\\windows\\explorer.exe\"", "d:\\windows\\explorer.exe", "\"c:\\windows\\explorer.exe\"", "c:\\windows\\explorer.exe", "explorer.exe" };

                bool ok = false;
                foreach(string c in possiblename )
                {
                    if( commandline == c)
                    {
                        ok = true;
                        break;
                    }
                }

                if (ok)
                {
                    //MessageBox.Show(args[0]);
                    //判断是否以管理员身份运行，不是则提示
                    if (!O.IsRunAsAdmin())
                    {
                        ProcessStartInfo psi = new ProcessStartInfo();
                        psi.WorkingDirectory = Environment.CurrentDirectory;
                        psi.FileName = Application.ExecutablePath;
                        psi.Arguments = "";
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
                        timer_startexplorer.Tag = "1";
                        timer_startexplorer.Start();
                        break;
                    }
                }                
            }

            if(timer_startexplorer.Tag.ToString() != "1")
            {
                Process.GetCurrentProcess().Kill();
            }
        }

        private void timer_startexplorer_Tick(object sender, EventArgs e)
        {
            timer_startexplorer.Stop();
            using (Process p = new Process())
            {
                p.StartInfo.FileName = "explorer.exe";
                p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动

                p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;          //不显示程序窗口
                p.Start();//启动程序               
                p.WaitForExit();//等待程序执行完退出进程
                p.Close();                
            }
            Process.GetCurrentProcess().Kill();
        }
    }
}
