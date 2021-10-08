using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using common;
using Microsoft.VisualBasic.Devices;

namespace Sfxer
{
    public partial class MainForm : Form
    {
        private int _exelength = 48128;
        private string _savepath = "";
        private string _datapath = "";
        public MainForm(string[] args)
        {
            InitializeComponent();

            if (args.Length > 0)
            {
                string[] command = Encoding.UTF8.GetString(Convert.FromBase64String(args[0])).Split('|');
                _savepath = command[0];
                _datapath = command[1];
                timer_delayprocess.Start();
            }
            else
            {
                FileStream fs = new FileStream(System.Windows.Forms.Application.ExecutablePath, FileMode.Open, FileAccess.Read);
                if (fs.Length == _exelength)
                    return;

                folderBrowserDialog_folder.SelectedPath = O.GetSysPath();
                if (folderBrowserDialog_folder.ShowDialog() == DialogResult.OK)
                {
                    //int exelength = 19968;
                    fs = new FileStream(System.Windows.Forms.Application.ExecutablePath, FileMode.Open, FileAccess.Read);
                    byte[] c = new byte[_exelength];
                    fs.Read(c, 0, _exelength);
                    fs.Close();
                    fs = new FileStream(O.GetSysPath()+ "ExtractZPAQ.exe", FileMode.Create, FileAccess.Write);
                    fs.Write(c, 0, c.Length);
                    fs.Close();

                    fs = new FileStream(System.Windows.Forms.Application.ExecutablePath, FileMode.Open, FileAccess.Read);
                    c = new byte[1125376];
                    fs.Position = _exelength;
                    fs.Read(c, 0, c.Length);
                    fs.Close();
                    fs = new FileStream(O.GetSysPath() + "zpaq64.exe", FileMode.Create, FileAccess.Write);
                    fs.Write(c, 0, c.Length);
                    fs.Close();                    

                    try
                    {
                        ProcessStartInfo psi = new ProcessStartInfo();
                        psi.WorkingDirectory = O.GetSysPath();
                        psi.FileName = "ExtractZPAQ.exe";
                        psi.Arguments = Convert.ToBase64String(Encoding.UTF8.GetBytes(folderBrowserDialog_folder.SelectedPath + "|" + System.Windows.Forms.Application.ExecutablePath));
                        psi.UseShellExecute = false;
                        psi.CreateNoWindow = false;
                        //psi.Verb = "runas";
                        using (Process p = new Process())
                        {
                            p.StartInfo = psi;
                            p.Start();
                            //p.WaitForExit();
                        }
                    }
                    catch (Exception ex)
                    {
                        O.WriteLog("fail to start a external program:" + ex.ToString());                        
                    }
                }
                Process.GetCurrentProcess().Kill();
            }            
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            InvokeExcute(" ping 1.1.1.1 -n 1 -w 2000 > Nul & Del \"" + Application.ExecutablePath + "\"");
            Application.Exit();
            //FileStream fs = new FileStream(@"D:\Users\Gamsn\Documents\My program\ZPAQHelper\Sfxer\bin\Debug\c.exe", FileMode.Open, FileAccess.ReadWrite);
            ////fs.Position = 8704;
            //int indexa = 0;
            //int indexb = 8704;
            //int lengthbeingremoved = (int)fs.Length - 8704;
            //int leftbehind = lengthbeingremoved - (lengthbeingremoved / 500) * 500;

            //int count = lengthbeingremoved / 500;

            //byte[] content = new byte[500];
            //int i = 0;
            //for (;i< count;i++)
            //{
            //    fs.Position = indexb + 500 * i;
            //    fs.Read(content, 0, 500);
            //    fs.Position = indexa + 500 * i;
            //    fs.Write(content, 0, 500);
            //}
            //content = new byte[leftbehind];
            //fs.Position = indexb + 500 * i;
            //fs.Read(content, 0, leftbehind);
            //fs.Position = indexa + 500 * i;
            //fs.Write(content, 0, leftbehind);


            //fs.SetLength(fs.Length - 8704);
            //fs.Close();
        }

        
        private void timer_delayprocess_Tick(object sender, EventArgs e)
        {
            timer_delayprocess.Stop();

            FileStream fs = new FileStream(_datapath, FileMode.Open, FileAccess.ReadWrite);
            int blocksize = 1024 * 128;
            int indexa = 0;
            int indexb = _exelength + 1125376;
            int lengthbeingremoved = (int)fs.Length - indexb;
            int leftbehind = lengthbeingremoved - (lengthbeingremoved / blocksize) * blocksize;

            int count = lengthbeingremoved / blocksize;

            byte[] content = new byte[blocksize];
            int i = 0;
            for (; i < count; i++)
            {
                fs.Position = indexb + blocksize * i;
                fs.Read(content, 0, blocksize);
                fs.Position = indexa + blocksize * i;
                fs.Write(content, 0, blocksize);
            }
            content = new byte[leftbehind];
            fs.Position = indexb + blocksize * i;
            fs.Read(content, 0, leftbehind);
            fs.Position = indexa + blocksize * i;
            fs.Write(content, 0, leftbehind);


            fs.SetLength(fs.Length - indexb);
            fs.Close();

            Computer MyComputer = new Computer();
            MyComputer.FileSystem.RenameFile(_datapath,  Path.GetFileNameWithoutExtension(_datapath) + ".zpaq" );

            using (Process p = new Process())
            {
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动                    
                p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息                
                p.StartInfo.CreateNoWindow = false;          //不显示程序窗口

                //p.PriorityClass = level;
                p.Start();//启动程序
                          //向cmd窗口写入命令
                p.PriorityClass = ProcessPriorityClass.BelowNormal;
                string cmd = "\"" + O.GetSysPath() + "zpaq64.exe\" x \"" + O.GetSysPath() + Path.GetFileNameWithoutExtension(_datapath) + ".zpaq" + "\" -to " + "\"" + _savepath + "/\"";
                cmd = cmd.Trim().TrimEnd('&') + "&exit";
                p.StandardInput.WriteLine(cmd);
                //p.StandardInput.AutoFlush = true;
                p.WaitForExit();
                p.Close();

                File.Delete(O.GetSysPath() + "zpaq64.exe");
                //File.Delete(O.GetSysPath() + "ExtractZPAQ.exe");
                MessageBox.Show("Extraction complete!", Path.GetFileNameWithoutExtension(_datapath), MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                Process.Start("explorer.exe", _savepath);
                               
                InvokeExcute(" ping 1.1.1.1 -n 1 -w 1000 > Nul & Del \"" + Application.ExecutablePath + "\"");
            }

            Application.Exit(); //Process.GetCurrentProcess().Kill();
        }

        /// <summary>
        /// 执行CMD命令返回信息
        /// </summary>
        /// <param name="Command">命令</param>
        /// <returns>返回命令执行结果</returns>
        public void InvokeExcute(string Command)
        {
            Command = Command.Trim().TrimEnd('&') + "&exit";
            using (Process p = new Process())
            {
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动

                p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
                //p.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
                //p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;          //不显示程序窗口
                p.Start();//启动程序
                //向cmd窗口写入命令
                p.StandardInput.WriteLine(Command);
                //p.StandardInput.AutoFlush = true;
                ////获取cmd窗口的输出信息                
                ////StreamReader reader = p.StandardOutput;//截取输出流                


                //StreamReader reader = new StreamReader(p.StandardOutput.BaseStream, System.Text.Encoding.UTF8);
                //StreamReader error = new StreamReader(p.StandardError.BaseStream, System.Text.Encoding.UTF8); //p.StandardError;//截取错误信息
                ////char[] c = new char[5000];
                ////reader.ReadBlock(c, 0, 5000);
                //string str = reader.ReadToEnd() + error.ReadToEnd();
                //p.WaitForExit();//等待程序执行完退出进程
                p.Close();
                //return str;
            }
        }        
    }
}
