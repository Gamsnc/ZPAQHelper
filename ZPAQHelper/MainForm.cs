using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;
using System.IO;
using IWshRuntimeLibrary;
using Microsoft.Win32;
using System.Management;
using System.Security.Principal;
using common;

namespace ZPAQHelper
{
    public partial class MainForm : Form
    {
        public class ShortcutCreator
        {
            public static string GetFileLocation(string linklocation)
            {
                WshShell shell = new WshShell();
                IWshShortcut sct = (IWshShortcut)shell.CreateShortcut(linklocation);
                return sct.TargetPath;
            }

            public static bool FileExist(string linklocation)
            {
                return System.IO.File.Exists(GetFileLocation(linklocation));
            }

            //需要引入IWshRuntimeLibrary，搜索Windows Script Host Object Model

            /// <summary>
            /// 创建快捷方式
            /// </summary>
            /// <param name="directory">快捷方式所处的文件夹</param>
            /// <param name="shortcutName">快捷方式名称</param>
            /// <param name="targetPath">目标路径</param>
            /// <param name="description">描述</param>
            /// <param name="iconLocation">图标路径，格式为"可执行文件或DLL路径, 图标编号"，
            /// 例如System.Environment.SystemDirectory + "\\" + "shell32.dll, 165"</param>
            /// <remarks></remarks>
            public static void CreateShortcut(string directory, string shortcutName, string targetPath,
                string description = null, string iconLocation = null)
            {
                if (!System.IO.Directory.Exists(directory))
                {
                    System.IO.Directory.CreateDirectory(directory);
                }

                string shortcutPath = Path.Combine(directory, string.Format("{0}.lnk", shortcutName));
                IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutPath);//创建快捷方式对象
                shortcut.TargetPath = targetPath;//指定目标路径
                shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);//设置起始位置
                shortcut.WindowStyle = 1;//设置运行方式，默认为常规窗口
                shortcut.Description = description;//设置备注
                shortcut.IconLocation = string.IsNullOrWhiteSpace(iconLocation) ? targetPath : iconLocation;//设置图标路径
                shortcut.Save();//保存快捷方式
            }

            /// <summary>
            /// 创建桌面快捷方式
            /// </summary>
            /// <param name="shortcutName">快捷方式名称</param>
            /// <param name="targetPath">目标路径</param>
            /// <param name="description">描述</param>
            /// <param name="iconLocation">图标路径，格式为"可执行文件或DLL路径, 图标编号"</param>
            /// <remarks></remarks>
            public static void CreateShortcutOnDesktop(string shortcutName, string targetPath,
                string description = null, string iconLocation = null)
            {
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);//获取桌面文件夹路径
                CreateShortcut(desktop, shortcutName, targetPath, description, iconLocation);
            }
        }


        public class CmdUtils
        {     
            public string result = "";
            public string commandline = "";
            public string shell = "";
            public void sendCmd(MainForm parent, ProcessPriorityClass level)
            {
                commandline = shell;
                shell = shell.Trim().TrimEnd('&') + "&exit";
                using (Process p = new Process())
                {                   
                    p.StartInfo.FileName = "cmd.exe";
                    p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动                    
                    p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
                    p.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
                    p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
                    p.StartInfo.CreateNoWindow = true;          //不显示程序窗口
                    
                    //p.PriorityClass = level;
                    p.Start();//启动程序
                              //向cmd窗口写入命令
                    p.PriorityClass = level;// ProcessPriorityClass.BelowNormal;
                    p.StandardInput.WriteLine(shell);
                    p.StandardInput.AutoFlush = true;
                    //获取cmd窗口的输出信息
                    StreamReader reader = new StreamReader(p.StandardOutput.BaseStream, System.Text.Encoding.UTF8); //StreamReader reader = p.StandardOutput;//截取输出流
                    StreamReader error = new StreamReader(p.StandardError.BaseStream, System.Text.Encoding.UTF8); //p.StandardError;//截取错误信息

                    reader.ReadLine();
                    reader.ReadLine();
                    reader.ReadLine();
                    reader.ReadLine();
                    updateLog(parent, shell);

                    //char[] r2 = new char[5000];
                    //char[] r = new char[1];
                    //int count = 0;
                    //while( reader.Read(r, count, 1) > 0)
                    //{
                    //    r2[count] = r[0];
                    //    count++;
                    //}
                    shell = "";
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        //byte[] d = Encoding.Default.GetBytes(line);
                        //string newline = Encoding.UTF8.GetString(d);
                        updateLog(parent, line);

                        //if(shell == "exit")
                        //{
                        //    updateLog(parent, "Cancelled by User.");
                        //    break;
                        //}
                    }

                    //if (shell != "exit")
                    //{
                    result = line = error.ReadToEnd();
                    if (line != null)
                        updateLog(parent, line);

                    p.WaitForExit();//等待程序执行完退出进程
                    //}
                    p.Close();
                }
            }


            private delegate void UpdateLog();
            private void updateLog(MainForm cmd, string log)
            {
                try
                {
                    UpdateLog set = delegate ()
                    {
                        cmd.textBox_cmdinfo.AppendText(Environment.NewLine.ToString() + log);
                    };
                    cmd.Invoke(set);
                }
                catch(Exception ex)
                {
                    O.WriteLog("updatelog:" + ex.ToString());
                }
            }
        }

        private ThreadPriority GetThreadLevel()
        {
            if (aboveNormalToolStripMenuItem.Checked)
                return ThreadPriority.AboveNormal;
            if (belowNormalToolStripMenuItem.Checked)
                return ThreadPriority.BelowNormal;
            if (lowToolStripMenuItem.Checked)
                return ThreadPriority.Lowest;

            return ThreadPriority.Normal;
        }

        private ProcessPriorityClass GetProcessLevel()
        {
            if (aboveNormalToolStripMenuItem.Checked)
                return ProcessPriorityClass.AboveNormal;
            if (belowNormalToolStripMenuItem.Checked)
                return ProcessPriorityClass.BelowNormal;
            if (lowToolStripMenuItem.Checked)
                return ProcessPriorityClass.Idle;

            return ProcessPriorityClass.Normal;
        }

        public String _isRun = "start";
        CmdUtils _cmd = new CmdUtils();

        public int _bestlevel = 0;
        public int _bestthread = 0;
        //private bool _compressmode = true;//compress mode
        public bool _isfromsendto = false;
        public MainForm(string[] args)
        {
            InitializeComponent();

            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.SendTo) + "\\ZPAQHelper.lnk";
            if (!System.IO.File.Exists(path) || !ShortcutCreator.FileExist(path))
            {
                System.IO.File.Delete(path);
                ShortcutCreator.CreateShortcut(System.Environment.GetFolderPath(Environment.SpecialFolder.SendTo),
                                               "ZPAQHelper",
                                               System.Windows.Forms.Application.ExecutablePath);
            }

            listView_files.Columns.Add("name");
            listView_files.Columns[0].Width = 170;
            listView_files.Columns.Add("type");
            listView_files.Columns[1].Width = 40;
            listView_files.Columns.Add("size");
            listView_files.Columns[2].Width = 80;
            listView_files.Columns.Add("modified");
            listView_files.Columns[3].Width = 80;

            //int firstfileindex = 0;
            if (args.Length > 0)
            {
                listView_files.Items.Clear();
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].Length > 0)
                    {
                        string lp = GetLongPathNameUnicode(args[i]);
                        if (lp != "")
                        {
                            AddFilesToList(lp);
                        }
                    }
                }
                _isfromsendto = listView_files.Items.Count > 0;
            }
        }

        private void UpdateListInfo()
        {
            bool compressmode = true;
            if (listView_files.Items.Count == 1)
            {
                if (Path.GetExtension(listView_files.Items[0].Tag.ToString()).ToLower() == ".zpaq")
                {
                    compressmode = false;//extract mode
                }
            }

            UpdateUI(compressmode);

            if (compressmode)
            {


                if (listView_files.Items.Count > 0)
                {
                    textBox_archivefolder.Text = Path.GetDirectoryName(listView_files.Items[0].Tag.ToString()) + "\\";
                    textBox_archivename.Text = Path.GetFileName(listView_files.Items[0].Tag.ToString());
                }
            }
            else
            {
                InitZpaqListView();
            }
        }

        private void ResetSettings()
        {
            comboBox_threads.Items.Clear();
            for (int i = 1; i <= Environment.ProcessorCount; i++)
            {
                comboBox_threads.Items.Add(i);
            }
            comboBox_threads.SelectedIndex = _bestthread = Environment.ProcessorCount / 2 - 1;
            label_optimum_thread.Text = "Recommend:" + comboBox_threads.SelectedItem.ToString();


            ComputerInfo ci = new ComputerInfo();
            double availableMemory = (double)ci.AvailablePhysicalMemory / 1024 / 1024 / 1024;

            if (availableMemory <= 1)
            {
                comboBox_level.SelectedIndex = _bestlevel = 0;
            }

            if (1 < availableMemory && availableMemory <= 2.5)
            {
                comboBox_level.SelectedIndex = _bestlevel = 1;
            }

            if (2.5 < availableMemory && availableMemory <= 4)
            {
                comboBox_level.SelectedIndex = _bestlevel = 2;
            }

            if (4 < availableMemory && availableMemory <= 6)
            {
                comboBox_level.SelectedIndex = _bestlevel = 3;
            }

            if (6 < availableMemory)
            {
                comboBox_level.SelectedIndex = _bestlevel = 4;
            }
            label_optimum_level.Text = "Recommend:" + comboBox_level.SelectedItem.ToString();
        }

        private void AddFilesToList(string lp)
        {
            string filename = Path.GetFileName(lp);
            FileInfo fi = new FileInfo(lp);
            if (Directory.Exists(lp))
            {
                ListViewItem lvi = listView_files.Items.Add(new ListViewItem(new string[] { filename, "folder", "", fi.LastWriteTime.ToString("MM-dd-yyyy") }));
                lvi.Tag = lp;
            }
            else
            {
                ListViewItem lvi = listView_files.Items.Add(new ListViewItem(new string[] { filename, "file", GetFileSize(fi.Length), fi.LastWriteTime.ToString("MM-dd-yyyy") }));
                lvi.Tag = lp;
            }
        }

        //自动调整ListView的列宽的方法
        private void AutoResizeColumnWidth(ListView lv)
        {
            int count = lv.Columns.Count;
            int MaxWidth = 0;
            Graphics graphics = lv.CreateGraphics();
            int width;
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            for (int i = 0; i < count; i++)
            {
                string str = lv.Columns[i].Text;
                MaxWidth = lv.Columns[i].Width;

                foreach (ListViewItem item in lv.Items)
                {
                    str = item.SubItems[i].Text;
                    width = (int)graphics.MeasureString(str, lv.Font).Width;
                    if (width > MaxWidth)
                    {
                        MaxWidth = width;
                    }
                }
                if (MaxWidth <= 150)
                {
                    lv.Columns[i].Width = MaxWidth;
                }
                else
                {
                    lv.Columns[i].Width = 150;
                }
            }
        }


        [DllImport("kernel32.dll ", CharSet = CharSet.Unicode)]
        static extern uint GetLongPathName(string shortname, StringBuilder longnamebuff, uint buffersize);

        private string GetLongPathNameUnicode(string shortpath)
        {//[DllImport("kernel32.dll ", CharSet = CharSet.Unicode)]
            //MessageBox.Show(shortpath);
            uint bufferSize = 4 * 1024;
            StringBuilder longNameBuffer = new StringBuilder(Convert.ToInt32(bufferSize));
            GetLongPathName(shortpath, longNameBuffer, bufferSize);
            //const string longPathSpecifier = @"\\?";
            string path = longNameBuffer.ToString();
            //string longPath = (path.StartsWith(longPathSpecifier) ? path : longPathSpecifier  + path);

            return path;
        }

        private void init()
        {
            _cmd.sendCmd(this, GetProcessLevel());
        }

        private Thread _cmdthread;
        private void button_ok_Click(object sender, EventArgs e)
        {
            if (_cmdthread == null ? false : _cmdthread.IsAlive)
            {
                MessageBox.Show("Current thread is busy, cancel it or wait until it's finished.");
                return;
            }

            if (groupBox_compress.Visible)
            {
                if (listView_files.Items.Count <= 0)
                {
                    MessageBox.Show("Add files or folders to the list first.");
                    return;
                }

                string filepath = textBox_archivefolder.Text.TrimEnd('\\') + "\\" + textBox_archivename.Text + ".zpaq";// = Path.GetDirectoryName(listView_files.Items[0].Tag.ToString()) + "\\" + Path.GetFileNameWithoutExtension(listView_files.Items[0].Tag.ToString()) + ".zpaq";
                groupBox_compress.Tag = filepath;
                string files = "";
                foreach (ListViewItem lvi in listView_files.Items)
                {
                    files += " \"" + lvi.Tag + "\"";
                }
                _cmd.shell = "\"" + O.GetSysPath() + "zpaq64.exe\" a \"" + filepath + "\" " + files + " -m" + (comboBox_level.SelectedIndex + 1) + " -t" + comboBox_threads.Text;
                _cmd.result = "";
                _cmdthread = new Thread(new ThreadStart(init));
                _cmdthread.Priority = GetThreadLevel();
                _cmdthread.Start();
                timer_singlethreadcheck.Start();
            }
            else
            {
                if (listView_zpaq.SelectedItems.Count <= 0)
                {
                    if (listView_zpaq.Items.Count == 1)
                    {
                        _cmd.shell = "\"" + O.GetSysPath() + "zpaq64.exe\" x \"" + listView_zpaq.Items[0].Tag + "\" -to " + "\"" + textBox_destinationpath.Text.TrimEnd('\\') + "\\" + listView_zpaq.Items[0].Text + "/\"";
                    }
                    else
                    {
                        MessageBox.Show("select a zpaq file from the zpaq list first.");
                        return;
                    }
                }
                else
                {
                    _cmd.shell = "\"" + O.GetSysPath() + "zpaq64.exe\" x \"" + listView_zpaq.SelectedItems[0].Tag + "\" -to " + "\"" + textBox_destinationpath.Text.TrimEnd('\\') + "\\" + listView_zpaq.Items[0].Text + "/\"";
                }
                _cmd.result = "";
                _cmdthread = new Thread(new ThreadStart(init));
                _cmdthread.Priority = GetThreadLevel();
                _cmdthread.Start();
                timer_singlethreadcheck.Start();
            }
            //InvokeExcute(_cmd.shell);
            //textBox_cmdinfo.AppendText( InvokeExcute(_cmd.shell));


            //string Command = _cmd.shell;
            //Command = Command.Trim().TrimEnd('&') + "&exit";
            //using (Process p = new Process())
            //{
            //    p.StartInfo.FileName = "cmd.exe";
            //    p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动

            //    p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
            //    p.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
            //    p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
            //    p.StartInfo.CreateNoWindow = true;          //不显示程序窗口
            //    p.Start();//启动程序
            //    //向cmd窗口写入命令
            //    p.StandardInput.WriteLine(Command);
            //    p.StandardInput.AutoFlush = true;
            //    //获取cmd窗口的输出信息
            //    StreamReader reader = p.StandardOutput;//截取输出流
            //    StreamReader error = p.StandardError;//截取错误信息
            //    string str = reader.ReadToEnd() + error.ReadToEnd();
            //    p.WaitForExit();//等待程序执行完退出进程
            //    p.Close();                
            //}
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dictionary<string, string> paras = SettingParas.GetSettings();
            paras["SFX"] = checkBox_exe.Checked ? "1" : "0";
            SettingParas.SetSettings(paras);

            if (_cmdthread == null ? false : _cmdthread.IsAlive)
            {
                if (MessageBox.Show("Current zpaq thread is still alive, kill it anyway?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    KillCurrentThread();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private bool CheckSettings()
        {
            return comboBox_level.SelectedIndex == _bestlevel && comboBox_threads.SelectedIndex == _bestthread;
        }

        private string GetFileSize(long size)
        {
            double s = (double)size;
            if (size / 1024 == 0)
            {
                return size.ToString() + "B";
            }

            if (size / 1024 / 1024 == 0)
            {
                s = s / 1024;
                return Math.Round(s, 3).ToString() + "KB";
            }

            if (size / 1024 / 1024 / 1024 == 0)
            {
                s = s / 1024 / 1024;
                return Math.Round(s, 3).ToString() + "MB";
            }

            if (size / 1024 / 1024 / 1024 / 1024 == 0)
            {
                s = s / 1024 / 1024 / 1024;
                return Math.Round(s, 3).ToString() + "GB";
            }


            s = s / 1024 / 1024 / 1024 / 1024;
            return Math.Round(s, 3).ToString() + "TB";
        }

        private void toolStripButton_add_Click(object sender, EventArgs e)
        {
            if (_cmdthread == null ? false : _cmdthread.IsAlive)
            {
                MessageBox.Show("Current thread is busy, cancel it or wait until it's finished.");
                return;
            }

            if (openFileDialog_files.ShowDialog() == DialogResult.OK)
            {
                foreach (string lp in openFileDialog_files.FileNames)
                {
                    AddFilesToList(lp);
                }
                UpdateListInfo();
            }
        }

        private void toolStripButton_addfolder_Click(object sender, EventArgs e)
        {
            if (_cmdthread == null ? false : _cmdthread.IsAlive)
            {
                MessageBox.Show("Current thread is busy, cancel it or wait until it's finished.");
                return;
            }

            folderBrowserDialog_folder.ShowNewFolderButton = false;
            if (folderBrowserDialog_folder.ShowDialog() == DialogResult.OK)
            {
                AddFilesToList(folderBrowserDialog_folder.SelectedPath);
                UpdateListInfo();
            }
        }


        /// <summary>
        /// 执行CMD命令返回信息
        /// </summary>
        /// <param name="Command">命令</param>
        /// <returns>返回命令执行结果</returns>
        public string InvokeExcute(string Command)
        {
            Command = Command.Trim().TrimEnd('&') + "&exit";
            using (Process p = new Process())
            {
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动

                p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;          //不显示程序窗口
                p.Start();//启动程序
                //向cmd窗口写入命令
                p.StandardInput.WriteLine(Command);
                p.StandardInput.AutoFlush = true;
                //获取cmd窗口的输出信息                
                //StreamReader reader = p.StandardOutput;//截取输出流                


                StreamReader reader = new StreamReader(p.StandardOutput.BaseStream, System.Text.Encoding.UTF8);
                StreamReader error = new StreamReader(p.StandardError.BaseStream, System.Text.Encoding.UTF8); //p.StandardError;//截取错误信息
                //char[] c = new char[5000];
                //reader.ReadBlock(c, 0, 5000);
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                string str = reader.ReadToEnd() + error.ReadToEnd();
                p.WaitForExit();//等待程序执行完退出进程
                p.Close();
                return str;
            }
        }

        ///// <summary>
        ///// 执行CMD命令返回信息
        ///// </summary>
        ///// <param name="Command">命令</param>
        ///// <returns>返回命令执行结果</returns>
        //public List<string> GetZpaqFilesList(string Command)
        //{
        //    List<string> lines = new List<string>();
        //    Command = Command.Trim().TrimEnd('&') + "&exit";
        //    using (Process p = new Process())
        //    {
        //        p.StartInfo.FileName = "cmd.exe";
        //        p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动

        //        p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
        //        p.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
        //        p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
        //        p.StartInfo.CreateNoWindow = true;          //不显示程序窗口
        //        p.Start();//启动程序
        //        //向cmd窗口写入命令
        //        p.StandardInput.WriteLine(Command);
        //        p.StandardInput.AutoFlush = true;

        //        //获取cmd窗口的输出信息
        //        StreamReader reader = p.StandardOutput;//截取输出流
        //        StreamReader error = p.StandardError;//截取错误信息
        //        string line;
        //        while ((line = reader.ReadLine()) != null)
        //        {
        //            byte[] d = Encoding.Default.GetBytes(line);
        //            lines.Add( Encoding.UTF8.GetString(d));

        //        }
        //        lines.Add(error.ReadToEnd());

        //        p.WaitForExit();//等待程序执行完退出进程
        //        p.Close();
        //        return lines;
        //    }
        //    return null;
        //}

        private void button_browse_Click(object sender, EventArgs e)
        {
            if (saveFileDialog_zpaq.ShowDialog() == DialogResult.OK)
            {
                textBox_archivefolder.Text = Path.GetDirectoryName(saveFileDialog_zpaq.FileName) + "\\";
                textBox_archivename.Text = Path.GetFileNameWithoutExtension(saveFileDialog_zpaq.FileName);
            }
        }

        private void button_autoset_Click(object sender, EventArgs e)
        {
            ResetSettings();
        }


        private void InitZpaqListView()
        {
            List<string> zpaqfiles = new List<string>();
            foreach (ListViewItem lvi in listView_files.Items)
            {
                string s = lvi.Tag.ToString();
                if (Path.GetExtension(s).ToLower() == ".zpaq")
                {
                    zpaqfiles.Add(s);
                }
            }
            FileInfoList fileList = new FileInfoList(zpaqfiles);


            listView_zpaq.Items.Clear();
            listView_zpaq.BeginUpdate();
            foreach (FileInfoWithIcon file in fileList.list)
            {
                ListViewItem item = new ListViewItem();
                item.Text = Path.GetFileNameWithoutExtension(file.fileInfo.FullName);
                item.ImageIndex = file.iconIndex;
                item.SubItems.Add(file.fileInfo.LastWriteTime.ToString());
                item.SubItems.Add(file.fileInfo.Extension.Replace(".", ""));
                item.SubItems.Add(string.Format(("{0:N0}"), file.fileInfo.Length));
                item.Tag = file.fileInfo.FullName;
                listView_zpaq.Items.Add(item);
            }
            listView_zpaq.LargeImageList = fileList.imageListLargeIcon;
            listView_zpaq.SmallImageList = fileList.imageListSmallIcon;
            listView_zpaq.Show();
            listView_zpaq.EndUpdate();

            if (listView_zpaq.Items.Count == 1)
            {
                string folder = listView_zpaq.Items[0].Tag.ToString();
                ListZPAQFile(folder);
            }
        }


        class FileInfoList
        {
            public List<FileInfoWithIcon> list;
            public ImageList imageListLargeIcon;
            public ImageList imageListSmallIcon;


            /// <summary>
            /// 根据文件路径获取生成文件信息，并提取文件的图标
            /// </summary>
            /// <param name="filespath"></param>
            public FileInfoList(List<string> filespath)
            {
                list = new List<FileInfoWithIcon>();
                imageListLargeIcon = new ImageList();
                imageListLargeIcon.ImageSize = new Size(32, 32);
                imageListSmallIcon = new ImageList();
                imageListSmallIcon.ImageSize = new Size(16, 16);
                foreach (string path in filespath)
                {
                    FileInfoWithIcon file = new FileInfoWithIcon(path);
                    imageListLargeIcon.Images.Add(file.largeIcon);
                    imageListSmallIcon.Images.Add(file.smallIcon);
                    file.iconIndex = imageListLargeIcon.Images.Count - 1;
                    list.Add(file);
                }
            }


        }
        class FileInfoWithIcon
        {
            public FileInfo fileInfo;
            public Icon largeIcon;
            public Icon smallIcon;
            public int iconIndex;
            public FileInfoWithIcon(string path)
            {
                fileInfo = new FileInfo(path);
                largeIcon = GetSystemIcon.GetIconByFileName(path, true);
                if (largeIcon == null)
                    largeIcon = GetSystemIcon.GetIconByFileType(Path.GetExtension(path), true);


                smallIcon = GetSystemIcon.GetIconByFileName(path, false);
                if (smallIcon == null)
                    smallIcon = GetSystemIcon.GetIconByFileType(Path.GetExtension(path), false);
            }
        }

        public static class GetSystemIcon
        {
            /// <summary>
            /// 依据文件名读取图标，若指定文件不存在，则返回空值。  
            /// </summary>
            /// <param name="fileName">文件路径</param>
            /// <param name="isLarge">是否返回大图标</param>
            /// <returns></returns>
            public static Icon GetIconByFileName(string fileName, bool isLarge = true)
            {
                int[] phiconLarge = new int[1];
                int[] phiconSmall = new int[1];
                //文件名 图标索引 
                Win32.ExtractIconEx(fileName, 0, phiconLarge, phiconSmall, 1);
                IntPtr IconHnd = new IntPtr(isLarge ? phiconLarge[0] : phiconSmall[0]);

                if (IconHnd.ToString() == "0")
                    return null;
                return Icon.FromHandle(IconHnd);
            }


            /// <summary>  
            /// 根据文件扩展名（如:.*），返回与之关联的图标。
            /// 若不以"."开头则返回文件夹的图标。  
            /// </summary>  
            /// <param name="fileType">文件扩展名</param>  
            /// <param name="isLarge">是否返回大图标</param>  
            /// <returns></returns>  
            public static Icon GetIconByFileType(string fileType, bool isLarge)
            {
                if (fileType == null || fileType.Equals(string.Empty)) return null;


                RegistryKey regVersion = null;
                string regFileType = null;
                string regIconString = null;
                string systemDirectory = Environment.SystemDirectory + "\\";


                if (fileType[0] == '.')
                {
                    //读系统注册表中文件类型信息  
                    regVersion = Registry.ClassesRoot.OpenSubKey(fileType, false);
                    if (regVersion != null)
                    {
                        regFileType = regVersion.GetValue("") as string;
                        regVersion.Close();
                        regVersion = Registry.ClassesRoot.OpenSubKey(regFileType + @"\DefaultIcon", false);
                        if (regVersion != null)
                        {
                            regIconString = regVersion.GetValue("") as string;
                            regVersion.Close();
                        }
                    }
                    if (regIconString == null)
                    {
                        //没有读取到文件类型注册信息，指定为未知文件类型的图标  
                        regIconString = systemDirectory + "shell32.dll,0";
                    }
                }
                else
                {
                    //直接指定为文件夹图标  
                    regIconString = systemDirectory + "shell32.dll,3";
                }
                string[] fileIcon = regIconString.Split(new char[] { ',' });
                if (fileIcon.Length != 2)
                {
                    //系统注册表中注册的标图不能直接提取，则返回可执行文件的通用图标  
                    fileIcon = new string[] { systemDirectory + "shell32.dll", "2" };
                }
                Icon resultIcon = null;
                try
                {
                    //调用API方法读取图标  
                    int[] phiconLarge = new int[1];
                    int[] phiconSmall = new int[1];
                    uint count = Win32.ExtractIconEx(fileIcon[0], Int32.Parse(fileIcon[1]), phiconLarge, phiconSmall, 1);
                    IntPtr IconHnd = new IntPtr(isLarge ? phiconLarge[0] : phiconSmall[0]);
                    resultIcon = Icon.FromHandle(IconHnd);
                }
                catch { }
                return resultIcon;
            }
        }

        /// <summary>  
        /// 定义调用的API方法  
        /// </summary>  
        class Win32
        {
            [DllImport("shell32.dll")]
            public static extern uint ExtractIconEx(string lpszFile, int nIconIndex, int[] phiconLarge, int[] phiconSmall, uint nIcons);
        }

        private void listView_zpaq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_zpaq.SelectedItems.Count > 0)
            {
                string folder = listView_zpaq.SelectedItems[0].Tag.ToString();

                ListZPAQFile(folder);
            }
        }

        private void ListZPAQFile(string folder)
        {
            textBox_destinationpath.Text = Path.GetDirectoryName(folder).TrimEnd('\\') + "\\" + Path.GetFileNameWithoutExtension(folder);

            //string cmd = "\"" + GetSysPath() + "zpaq64.exe\" l \"" + folder + "\" ";
            //cmd = InvokeExcute(cmd);
            //byte[] d = Encoding.Default.GetBytes(cmd);
            //textBox_cmdinfo.Text = Encoding.UTF8.GetString(d);
            _cmd.result = "";
            _cmd.shell = "\"" + O.GetSysPath() + "zpaq64.exe\" l \"" + folder + "\" ";

            textBox_cmdinfo.AppendText(InvokeExcute(_cmd.shell));
            //new Thread(new ThreadStart(init)).Start();            
        }

        private void button_browsefolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog_folder.ShowNewFolderButton = true;
            if (folderBrowserDialog_folder.ShowDialog() == DialogResult.OK)
            {
                textBox_destinationpath.Text = folderBrowserDialog_folder.SelectedPath;
            }
        }

        private void toolStripButton_forceextract_Click(object sender, EventArgs e)
        {
            if (_cmdthread == null ? false : _cmdthread.IsAlive)
            {
                MessageBox.Show("Current thread is busy, cancel it or wait until it's finished.");
                return;
            }

            if (groupBox_extract.Visible)
            {
                ResetSettings();
                if (listView_files.Items.Count > 0)
                {
                    textBox_archivefolder.Text = Path.GetDirectoryName(listView_files.Items[0].Tag.ToString()) + "\\";
                    textBox_archivename.Text = Path.GetFileName(listView_files.Items[0].Tag.ToString());
                }
            }
            else
            {
                InitZpaqListView();
            }
            UpdateUI(!groupBox_compress.Visible);
        }

        private void UpdateUI(bool compressmode)
        {
            groupBox_compress.Visible = compressmode;
            button_extractall.Visible = groupBox_extract.Visible = !groupBox_compress.Visible;
            button_ok.Text = compressmode ? "Go" : "Extract";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //FileStream fs = new FileStream(@"D:\test.txt", FileMode.Open, FileAccess.ReadWrite);
            //fs.SetLength(12);
            //byte[] c = new byte[6];
            //byte[] newbyte = { 40, 41, 42, 43, 44, 45, 46 };
            //fs.Read(c, 0, 6);
            //fs.Position = 6;
            //fs.Write(c, 0, 6);
            //fs.Position = 0;
            //fs.Write(newbyte, 0, 6);
            //fs.Close();


            ResetSettings();
            UpdateListInfo();

            bool associated = false;

            FileTypeRegInfo ftri = GetFileTypeRegInfo();
            if (ftri != null)
            {
                if (ftri.Description == "ZPAQHelper.zpaq")
                    associated = true;
            }

            if (associated)
            {
                toolStripButton_fileassociate.Image = Image.FromFile(O.GetSysPath() + "connect.png");
                toolStripButton_fileassociate.Tag = 1;
            }
            else
            {
                toolStripButton_fileassociate.Image = Image.FromFile(O.GetSysPath() + "disconnect.png");
                toolStripButton_fileassociate.Tag = 0;
            }

            aboveNormalToolStripMenuItem.Checked = normalToolStripMenuItem.Checked = belowNormalToolStripMenuItem.Checked = lowToolStripMenuItem.Checked = false;
            Dictionary<string, string> paras = SettingParas.GetSettings();
            string l = paras["CPULEVEL"].ToLower();
            if(l == "above")
            {
                aboveNormalToolStripMenuItem.Checked = true;
            }
            if (l == "normal")
            {
                normalToolStripMenuItem.Checked = true;
            }
            if (l == "below")
            {
                belowNormalToolStripMenuItem.Checked = true;
            }
            if (l == "low")
            {
                lowToolStripMenuItem.Checked = true;
            }

            checkBox_exe.Checked = paras["SFX"] == "1";
        }

        //private int _cancelcount = 0;
        private void toolStripButton_cancel_Click(object sender, EventArgs e)
        {
            //if (_cancelcount < 1 )
            //{                
            //    _cmd.shell = "exit";
            //}
            //else if(_cancelcount ==1)
            //{
            //    _cmdthread.Abort();
            //}
            //else
            //{
            if (_cmdthread == null ? false : _cmdthread.IsAlive)
            {
                if (MessageBox.Show("Force to cancel?", "ZPAQHelper", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    if (KillCurrentThread())
                        textBox_cmdinfo.AppendText(Environment.NewLine.ToString() + "Cancelled by User.");
                }
            }
            else
            {
                textBox_cmdinfo.AppendText("Current thread is idel" + Environment.NewLine.ToString());
            }
            //}
            //_cancelcount++;
        }

        private bool KillCurrentThread()
        {
            Process[] processes = Process.GetProcessesByName("zpaq64");
            foreach (Process instance in processes)
            {
                string commandline = ProcessCommandline.GetCommandLineArgs(instance).Replace("\"" + O.GetSysPath() + "zpaq64.exe\"", "").Trim();
                if (commandline.IndexOf(_cmd.commandline.Replace("\"" + O.GetSysPath() + "zpaq64.exe\"", "").Trim()) >= 0)
                {
                    //判断是否以管理员身份运行，不是则提示
                    if (!O.IsRunAsAdmin())
                    {
                        //ProcessStartInfo psi = new ProcessStartInfo();
                        //psi.WorkingDirectory = O.GetSysPath();
                        //psi.FileName = "ZPAQTerminator.exe";
                        //psi.Arguments = Convert.ToBase64String(Encoding.UTF8.GetBytes(_cmd.commandline));
                        //psi.UseShellExecute = true;
                        //psi.Verb = "runas";
                        //Process p = new Process();
                        //p.StartInfo = psi;
                        //p.Start();
                        //p.WaitForExit();
                        return StartExternalProgram("ZPAQTerminator.exe", Convert.ToBase64String(Encoding.UTF8.GetBytes(_cmd.commandline)));
                        break;
                    }
                    else
                    {
                        instance.Kill();
                        break;
                    }
                }
            }
            return true;
        }



        public extracttask _et;
        private void button_extractall_Click(object sender, EventArgs e)
        {
            if (_cmdthread == null ? false : _cmdthread.IsAlive)
            {
                MessageBox.Show("Current thread is busy, cancel it or wait until it's finished.");
                return;
            }

            _et = new extracttask(listView_zpaq);

            string path = _et.Next();
            if (path != "")
            {
                _cmd.result = "";
                _cmd.shell = "\"" + O.GetSysPath() + "zpaq64.exe\" x \"" + path + "\" -to " + "\"" + textBox_destinationpath.Text.TrimEnd('\\') + "\\" + Path.GetFileNameWithoutExtension(path) + "/\"";
                _cmdthread = new Thread(new ThreadStart(init));
                _cmdthread.Priority = GetThreadLevel();
                _cmdthread.Start();
            }

            timer_checkthread.Start();
        }

        private void toolStripButton_clearinfo_Click(object sender, EventArgs e)
        {
            textBox_cmdinfo.Text = "";
        }

        private void timer_checkthread_Tick(object sender, EventArgs e)
        {
            if (_cmdthread == null ? false : _cmdthread.IsAlive)
            {

            }
            else
            {
                string path = _et.Next();
                if (path != "")
                {
                    _cmd.result = "";
                    _cmd.shell = "\"" + O.GetSysPath() + "zpaq64.exe\" x \"" + path + "\" -to " + "\"" + textBox_destinationpath.Text + "\"";
                    _cmdthread = new Thread(new ThreadStart(init));
                    _cmdthread.Priority = GetThreadLevel();
                    _cmdthread.Start();
                }
                else
                {
                    timer_checkthread.Stop();
                    timer_singlethreadcheck.Start();
                }
            }
        }

        private bool StartExternalProgram(string name, string para)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.WorkingDirectory = O.GetSysPath();
                psi.FileName = name;
                psi.Arguments = para;
                psi.UseShellExecute = true;
                psi.Verb = "runas";
                using (Process p = new Process())
                {
                    p.StartInfo = psi;
                    p.Start();
                    p.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                O.WriteLog("fail to start a external program:" + name + "|" + para + "|" + ex.ToString());
                return false;
            }
            return true;
        }

        private void toolStripButton_fileassociate_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> paras = SettingParas.GetSettings();

            if ((int)toolStripButton_fileassociate.Tag == 1)
            {//disconnect
                if (paras["DESCRIPTION"] != "ZPAQHelper.zpaq")
                {//restore former values
                    //ProcessStartInfo psi = new ProcessStartInfo();
                    //psi.WorkingDirectory = O.GetSysPath();
                    //psi.FileName = "SetZpaqIcon.exe";
                    //psi.Arguments = "RESTORE";
                    //psi.UseShellExecute = true;
                    //psi.Verb = "runas";
                    //Process p = new Process();
                    //p.StartInfo = psi;
                    //p.Start();
                    //p.WaitForExit();
                    if (StartExternalProgram("SetZpaqIcon.exe", "RESTORE"))
                    {
                        toolStripButton_fileassociate.Image = Image.FromFile(O.GetSysPath() + "disconnect.png");
                        toolStripButton_fileassociate.Tag = 0;
                    }
                }
                else
                {//delete keys from register table
                    //ProcessStartInfo psi = new ProcessStartInfo();
                    //psi.WorkingDirectory = O.GetSysPath();
                    //psi.FileName = "SetZpaqIcon.exe";
                    //psi.Arguments = "DELETE";
                    //psi.UseShellExecute = true;
                    //psi.Verb = "runas";
                    //Process p = new Process();
                    //p.StartInfo = psi;
                    //p.Start();
                    //p.WaitForExit();
                    if (StartExternalProgram("SetZpaqIcon.exe", "DELETE"))
                    {
                        toolStripButton_fileassociate.Image = Image.FromFile(O.GetSysPath() + "disconnect.png");
                        toolStripButton_fileassociate.Tag = 0;
                    }
                }
            }
            else
            {//connect
                FileTypeRegInfo ftri = GetFileTypeRegInfo();
                if (ftri != null)
                {
                    if (ftri.Description != "ZPAQHelper.zpaq")
                    {
                        //sw.WriteLine("DESCRIPTION=ZPAQHelper.zpaq");//
                        //sw.WriteLine("EXEPATH=" + "\"" + O.GetSysPath() + "ZPAQHelper.exe\"");//
                        //sw.WriteLine("EXTENDNAME=.zpaq");//
                        //sw.WriteLine("ICONPATH=" + O.GetSysPath() + "file.ICO,0");//
                        //sw.WriteLine("THREAD=4");//
                        //sw.WriteLine("METHOD=5");//                   

                        paras["DESCRIPTION"] = ftri.Description;
                        paras["EXEPATH"] = ftri.ExePath;
                        paras["EXTENDNAME"] = ".zpaq"; //ftri.ExtendName
                        paras["ICONPATH"] = ftri.IcoPath;
                        SettingParas.SetSettings(paras);

                        if (MessageBox.Show("zpaq is associated with another program, still continue?", "ZPAQHelper", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            return;
                        }
                    }
                }
                //ProcessStartInfo psi = new ProcessStartInfo();
                //psi.WorkingDirectory = O.GetSysPath();
                //psi.FileName = "SetZpaqIcon.exe";
                //psi.Arguments = "ADD";
                //psi.UseShellExecute = true;
                //psi.Verb = "runas";
                //Process p = new Process();
                //p.StartInfo = psi;
                //p.Start();
                //p.WaitForExit();
                if (StartExternalProgram("SetZpaqIcon.exe", "ADD"))
                {
                    toolStripButton_fileassociate.Image = Image.FromFile(O.GetSysPath() + "connect.png");
                    toolStripButton_fileassociate.Tag = 1;
                }
            }
        }

        public class FileTypeRegInfo
        {
            /// <summary>
            /// 目标类型文件的扩展名
            /// </summary>
            public string ExtendName; //".xcf"

            /// <summary>
            /// 目标文件类型说明
            /// </summary>
            public string Description; //"XCodeFactory项目文件"

            /// <summary>
            /// 目标类型文件关联的图标
            /// </summary>
            public string IcoPath;

            /// <summary>
            /// 打开目标类型文件的应用程序
            /// </summary>
            public string ExePath;

            public FileTypeRegInfo()
            {
            }

            public FileTypeRegInfo(string extendName)
            {
                this.ExtendName = extendName;
            }
        }

        ///// <summary>
        ///// FileTypeRegistered 指定文件类型是否已经注册
        ///// </summary>        
        //public bool FileTypeRegistered()
        //{
        //    RegistryKey softwareKey = Registry.ClassesRoot.OpenSubKey(".zpaq");
        //    if (softwareKey != null)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        /// <summary>
        /// GetFileTypeRegInfo 得到指定文件类型关联信息
        /// </summary>        
        public FileTypeRegInfo GetFileTypeRegInfo()
        {
            if (Registry.ClassesRoot.OpenSubKey(".zpaq") == null)
            {
                return null;
            }

            FileTypeRegInfo regInfo = new FileTypeRegInfo(".zpaq");

            RegistryKey relationKey = Registry.ClassesRoot.OpenSubKey(".zpaq");
            if (relationKey != null)
                regInfo.Description = relationKey.GetValue("").ToString();
            else
                return null;

            relationKey = Registry.LocalMachine.OpenSubKey(@"Software\Classes\" + regInfo.Description);// relationKey.OpenSubKey("DefaultIcon");
            if (relationKey != null)
            {
                RegistryKey iconKey = relationKey.OpenSubKey("DefaultIcon");
                if (iconKey != null)
                    regInfo.IcoPath = iconKey.GetValue("").ToString();

                RegistryKey commandKey = relationKey.OpenSubKey(@"shell\open\command");
                if (commandKey != null)
                    regInfo.ExePath = commandKey.GetValue("").ToString().TrimEnd(new char[] { '\"', '%', '1', '\"' }).Trim();
            }
            else
                return null;

            return regInfo;
        }

        private void timer_singlethreadcheck_Tick(object sender, EventArgs e)
        {
            if (_cmdthread == null ? false : _cmdthread.IsAlive)
            {

            }
            else
            {
                timer_singlethreadcheck.Stop();
                if (_cmd.result.IndexOf("OK") >= 0)
                {
                    if(groupBox_compress.Visible && checkBox_exe.Checked)
                    {
                        string sfx = O.GetSysPath() + "Sfxer.exe";
                        string zpaq = O.GetSysPath() + "zpaq64.exe";
                        string data = groupBox_compress.Tag.ToString();
                        int sfxlength;
                        FileStream fs = new FileStream(sfx, FileMode.Open, FileAccess.Read);
                        sfxlength = (int)fs.Length;
                        byte[] sfxdata = new byte[sfxlength];
                        fs.Read(sfxdata, 0, sfxlength);
                        fs.Close();

                        int zpaqlength;
                        fs = new FileStream(zpaq, FileMode.Open, FileAccess.Read);
                        zpaqlength = (int)fs.Length;
                        byte[] zpaqdata = new byte[zpaqlength];
                        fs.Read(zpaqdata, 0, zpaqlength);
                        fs.Close();

                        fs = new FileStream(data, FileMode.Open, FileAccess.ReadWrite);
                        int addtionlength = sfxlength + zpaqlength;
                        int formerlength = (int)fs.Length;
                        int blocksize = 1024 * 128;
                        int leftbehind = formerlength - (formerlength / blocksize) * blocksize;
                        int count = formerlength / blocksize;
                        int newlength = formerlength + addtionlength;
                        fs.SetLength(newlength);

                        byte[] content = new byte[blocksize];
                        int i = 0;
                        for (; i < count; i++)
                        {
                            fs.Position = formerlength - blocksize*(i+1);
                            fs.Read(content, 0, blocksize);
                            fs.Position = newlength - blocksize * (i+1);
                            fs.Write(content, 0, blocksize);
                        }
                        content = new byte[leftbehind];
                        fs.Position = 0;
                        fs.Read(content, 0, leftbehind);
                        fs.Position = addtionlength;
                        fs.Write(content, 0, leftbehind);

                        fs.Position = 0;
                        fs.Write(sfxdata, 0, sfxlength);
                        fs.Write(zpaqdata, 0, zpaqlength);
                        
                        fs.Close();

                        Computer MyComputer = new Computer();
                        MyComputer.FileSystem.RenameFile(data, Path.GetFileNameWithoutExtension(data) + ".exe");









                        //string sfx =  O.GetSysPath() + "Sfxer.exe";
                        //string zpaq = O.GetSysPath() + "zpaq64.exe";
                        //string plus = "+";
                        //string newpath = Path.GetDirectoryName(groupBox_compress.Tag.ToString()).TrimEnd('\\') + "\\" + Path.GetFileNameWithoutExtension(groupBox_compress.Tag.ToString()) + ".exe";
                        //string cmd = "copy /b \"" + sfx + "\"" + plus + "\"" + zpaq + "\"" + plus + "\"" + groupBox_compress.Tag + "\" \"" + newpath + "\"";
                        //cmd = cmd.Trim().TrimEnd('&') + "&exit";
                        //using (Process p = new Process())
                        //{
                        //    p.StartInfo.FileName = "cmd.exe";
                        //    p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动                    
                        //    p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息                
                        //    p.StartInfo.CreateNoWindow = true;          //不显示程序窗口

                        //    //p.PriorityClass = level;
                        //    p.Start();//启动程序
                        //              //向cmd窗口写入命令
                        //    p.PriorityClass = ProcessPriorityClass.BelowNormal;
                        //    p.StandardInput.WriteLine(cmd);
                        //    //p.StandardInput.AutoFlush = true;
                        //    p.WaitForExit();
                        //    p.Close();
                        //    System.IO.File.Delete(groupBox_compress.Tag.ToString());
                        //}                                            
                    }
                    MessageBox.Show("Completed!");                    
                }
                else
                {
                    MessageBox.Show("Failed!");
                }
                if (_isfromsendto)
                {
                    Process.GetCurrentProcess().Kill();
                }
            }
        }

        private void toolStripButton_restartexplorer_Click(object sender, EventArgs e)
        {
            //ProcessStartInfo psi = new ProcessStartInfo();
            //psi.WorkingDirectory = O.GetSysPath();
            //psi.FileName = "RestartExplorer.exe";
            ////psi.Arguments = Convert.ToBase64String(Encoding.UTF8.GetBytes(_cmd.commandline));
            //psi.UseShellExecute = true;
            //psi.Verb = "runas";
            //Process p = new Process();
            //p.StartInfo = psi;
            //p.Start();
            //p.WaitForExit();
            StartExternalProgram("RestartExplorer.exe", "");
        }

        private void aboveNormalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboveNormalToolStripMenuItem.Checked = normalToolStripMenuItem.Checked = belowNormalToolStripMenuItem.Checked = lowToolStripMenuItem.Checked = false;
            aboveNormalToolStripMenuItem.Checked = true;
            Dictionary<string, string> paras = SettingParas.GetSettings();
            paras["CPULEVEL"] = "Above";
            SettingParas.SetSettings(paras);
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboveNormalToolStripMenuItem.Checked = normalToolStripMenuItem.Checked = belowNormalToolStripMenuItem.Checked = lowToolStripMenuItem.Checked = false;
            normalToolStripMenuItem.Checked = true;
            Dictionary<string, string> paras = SettingParas.GetSettings();
            paras["CPULEVEL"] = "Normal";
            SettingParas.SetSettings(paras);
        }

        private void belowNormalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboveNormalToolStripMenuItem.Checked = normalToolStripMenuItem.Checked = belowNormalToolStripMenuItem.Checked = lowToolStripMenuItem.Checked = false;
            belowNormalToolStripMenuItem.Checked = true;
            Dictionary<string, string> paras = SettingParas.GetSettings();
            paras["CPULEVEL"] = "Below";
            SettingParas.SetSettings(paras);
        }

        private void lowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboveNormalToolStripMenuItem.Checked = normalToolStripMenuItem.Checked = belowNormalToolStripMenuItem.Checked = lowToolStripMenuItem.Checked = false;
            lowToolStripMenuItem.Checked = true;
            Dictionary<string, string> paras = SettingParas.GetSettings();
            paras["CPULEVEL"] = "Low";
            SettingParas.SetSettings(paras);
        }
    }

    public class extracttask
    {
        private List<string> _path = new List<string>();
        private int _index = 0;
        public extracttask( ListView lv)
        {
            foreach(ListViewItem lvi in lv.Items)
            {
                _path.Add(lvi.Tag.ToString());
            }
        }

        public string Next()
        {
            if (_index < _path.Count)
                return _path[_index++];
            else
                return "";
        }
    }    
}
