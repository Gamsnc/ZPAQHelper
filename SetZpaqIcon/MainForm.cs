using Microsoft.Win32;
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

namespace SetZpaqIcon
{
    public partial class MainForm : Form
    {
        public MainForm(string[] args)
        {
            InitializeComponent();
            //args = new string[] { "RESTORE" };
            if (args.Length > 0)
            {
                try
                {
                    Dictionary<string, string> paras = SettingParas.GetSettings();
                    FileTypeRegInfo ftri = new FileTypeRegInfo();
                    if (args[0] == "ADD")
                    {
                        ftri.Description = paras["DESCRIPTION"];
                        ftri.ExePath = paras["EXEPATH"];
                        ftri.ExtendName = ".zpaq";
                        ftri.IcoPath = paras["ICONPATH"];
                        DeleteFileTypeRegInfo(ftri);

                        ftri.Description = "ZPAQHelper.zpaq";
                        ftri.ExePath = "\"" + O.GetSysPath() + "ZPAQHelper.exe\"";
                        ftri.ExtendName = ".zpaq";
                        ftri.IcoPath = O.GetSysPath() + "file.ICO,0";
                        RegisterFileType(ftri);
                    }
                    else if (args[0] == "RESTORE")
                    {

                        ftri.Description = "ZPAQHelper.zpaq";
                        ftri.ExePath = "\"" + O.GetSysPath() + "ZPAQHelper.exe\"";
                        ftri.ExtendName = ".zpaq";
                        ftri.IcoPath = O.GetSysPath() + "file.ICO,0";
                        DeleteFileTypeRegInfo(ftri);

                        ftri.Description = paras["DESCRIPTION"];
                        ftri.ExePath = paras["EXEPATH"];
                        ftri.ExtendName = ".zpaq";
                        ftri.IcoPath = paras["ICONPATH"];
                        RegisterFileType(ftri);
                    }
                    else if (args[0] == "DELETE")
                    {

                        ftri.Description = "ZPAQHelper.zpaq";
                        ftri.ExePath = "\"" + O.GetSysPath() + "ZPAQHelper.exe\"";
                        ftri.ExtendName = ".zpaq";
                        ftri.IcoPath = O.GetSysPath() + "file.ICO,0";
                        DeleteFileTypeRegInfo(ftri);
                    }
                }
                catch (Exception ex)
                {
                    O.WriteLog("Association failed:" + ex.ToString());
                }
            }

            Process.GetCurrentProcess().Kill();
        }
                
        /// <summary>
        /// RegisterFileType 使文件类型与对应的图标及应用程序关联起来。
        /// </summary>        
        public void RegisterFileType(FileTypeRegInfo regInfo)
        {
            RegistryKey relationKey = Registry.ClassesRoot.OpenSubKey(regInfo.ExtendName, true);
            if (relationKey == null )
            {
                relationKey = Registry.ClassesRoot.CreateSubKey(regInfo.ExtendName);                
            }
            relationKey.SetValue("", regInfo.Description);
            relationKey.Close();

            //=====================================================================================================
            relationKey = Registry.ClassesRoot.OpenSubKey(regInfo.Description, true);
            if (relationKey == null)
            {
                relationKey = Registry.ClassesRoot.CreateSubKey(regInfo.Description);
            }
            RegistryKey iconKey = relationKey.OpenSubKey("DefaultIcon", true);
            if (iconKey == null)
            {
                iconKey = relationKey.CreateSubKey("DefaultIcon");
            }
            iconKey.SetValue("", regInfo.IcoPath);
            iconKey.Close();

            RegistryKey commandKey = relationKey.OpenSubKey(@"shell\open\command", true);
            if (commandKey == null)
            {
                commandKey = relationKey.CreateSubKey(@"shell\open\command");
            }
            commandKey.SetValue("", regInfo.ExePath + " \"%1\"");
            commandKey.Close();

            relationKey.Close();

            //=====================================================================================================
            relationKey = Registry.LocalMachine.OpenSubKey(@"Software\Classes\" + regInfo.Description, true);
            if (relationKey == null)
            {
                relationKey = Registry.LocalMachine.CreateSubKey(@"Software\Classes\" + regInfo.Description);
            }
                        
            iconKey = relationKey.OpenSubKey("DefaultIcon", true);
            if(iconKey == null)
            {
                iconKey = relationKey.CreateSubKey("DefaultIcon");
            }                       
            iconKey.SetValue("", regInfo.IcoPath);
            iconKey.Close();

            commandKey = relationKey.OpenSubKey(@"shell\open\command", true);
            if (commandKey == null)
            {
                commandKey = relationKey.CreateSubKey(@"shell\open\command");
            }                        
            commandKey.SetValue("", regInfo.ExePath + " \"%1\"");
            commandKey.Close();

            relationKey.Close();
        }

        /// <summary>
        /// DeleteFileTypeRegInfo 删除指定文件类型关联信息
        /// </summary>    
        public bool DeleteFileTypeRegInfo(FileTypeRegInfo regInfo)
        {
            try
            {
                Registry.ClassesRoot.DeleteSubKey(regInfo.ExtendName);
            }
            catch(Exception ex)
            {
                O.WriteLog("Association failed:" + ex.ToString());
            }

            try
            {
                Registry.ClassesRoot.DeleteSubKeyTree(regInfo.Description);
            }
            catch (Exception ex)
            {
                O.WriteLog("Association failed:" + ex.ToString());
            }

            try
            {
                Registry.LocalMachine.DeleteSubKeyTree(@"Software\Classes\" + regInfo.Description);
            }
            catch (Exception ex)
            {
                O.WriteLog("Association failed:" + ex.ToString());
            }           
            
            return true;
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
        ///// FileTypeRegister 用于注册自定义的文件类型。
        ///// zhuweisky 2005.08.31
        ///// </summary>
        //public class FileTypeRegister
        //{

        //    #region RegisterFileType
        //    /// <summary>
        //    /// RegisterFileType 使文件类型与对应的图标及应用程序关联起来。
        //    /// </summary>        
        //    public static void RegisterFileType(FileTypeRegInfo regInfo)
        //    {
        //        if (FileTypeRegistered(regInfo.ExtendName))
        //        {
        //            return;
        //        }                

        //        RegistryKey relationKey = Registry.ClassesRoot.CreateSubKey(regInfo.ExtendName);
        //        relationKey.SetValue("", regInfo.Description);
        //        relationKey.Close();
                
        //        relationKey = Registry.LocalMachine.CreateSubKey(@"Software\Classes\" + regInfo.Description);

        //        RegistryKey iconKey = relationKey.CreateSubKey("DefaultIcon");
        //        iconKey.SetValue("", regInfo.IcoPath);
        //        iconKey.Close();

        //        RegistryKey commandKey = relationKey.CreateSubKey(@"shell\open\command");                
        //        commandKey.SetValue("", regInfo.ExePath + " \"%1\"");
        //        commandKey.Close();
                
        //        relationKey.Close();
        //    }

        //    /// <summary>
        //    /// GetFileTypeRegInfo 得到指定文件类型关联信息
        //    /// </summary>        
        //    public static FileTypeRegInfo GetFileTypeRegInfo(string extendName)
        //    {
        //        if (!FileTypeRegistered(extendName))
        //        {
        //            return null;
        //        }

        //        FileTypeRegInfo regInfo = new FileTypeRegInfo(extendName);

        //        RegistryKey relationKey = Registry.ClassesRoot.OpenSubKey(extendName);

        //        regInfo.Description = relationKey.GetValue("").ToString();

        //        relationKey = Registry.LocalMachine.OpenSubKey(@"Software\Classes\"+ regInfo.Description);// relationKey.OpenSubKey("DefaultIcon");
        //        RegistryKey iconKey = relationKey.OpenSubKey("DefaultIcon");
        //        regInfo.IcoPath = iconKey.GetValue("").ToString();
                               
        //        RegistryKey commandKey = relationKey.OpenSubKey(@"shell\open\command");
        //        regInfo.ExePath = commandKey.GetValue("").ToString().TrimEnd( new char[] { '\"', '%', '1', '\"' }).Trim();
                
        //        return regInfo;
        //    }

        //    /// <summary>
        //    /// UpdateFileTypeRegInfo 更新指定文件类型关联信息
        //    /// </summary>    
        //    public static bool UpdateFileTypeRegInfo(FileTypeRegInfo regInfo)
        //    {
        //        if (!FileTypeRegistered(regInfo.ExtendName))
        //        {
        //            return false;
        //        }

        //        RegistryKey relationKey = Registry.ClassesRoot.OpenSubKey(regInfo.ExtendName, true);
        //        relationKey.SetValue("", regInfo.Description);
        //        relationKey.Close();

        //        relationKey = Registry.LocalMachine.OpenSubKey(@"Software\Classes\" + regInfo.Description);

        //        RegistryKey iconKey = relationKey.OpenSubKey("DefaultIcon", true);
        //        iconKey.SetValue("", regInfo.IcoPath);
        //        iconKey.Close();

        //        RegistryKey commandKey = relationKey.OpenSubKey(@"shell\open\command", true);
        //        commandKey.SetValue("", regInfo.ExePath + " \"%1\"");
        //        commandKey.Close();

        //        relationKey.Close();
        //        //string extendName = regInfo.ExtendName;
        //        //string relationName = extendName.Substring(1, extendName.Length - 1).ToUpper() + "_FileType";
        //        //RegistryKey relationKey = Registry.ClassesRoot.OpenSubKey(relationName, true);
        //        //relationKey.SetValue("", regInfo.Description);

        //        //RegistryKey iconKey = relationKey.OpenSubKey("DefaultIcon", true);
        //        //iconKey.SetValue("", regInfo.IcoPath);

        //        //RegistryKey shellKey = relationKey.OpenSubKey("Shell");
        //        //RegistryKey openKey = shellKey.OpenSubKey("Open");
        //        //RegistryKey commandKey = openKey.OpenSubKey("Command", true);
        //        //commandKey.SetValue("", regInfo.ExePath + " %1");

        //        //relationKey.Close();

        //        return true;
        //    }
            
        //    /// <summary>
        //    /// UpdateFileTypeRegInfo 更新指定文件类型关联信息
        //    /// </summary>    
        //    public static bool DeleteFileTypeRegInfo(FileTypeRegInfo regInfo)
        //    {
        //        if (!FileTypeRegistered(regInfo.ExtendName))
        //        {
        //            return false;
        //        }

        //         Registry.ClassesRoot.DeleteSubKey(regInfo.ExtendName);
                

        //        Registry.LocalMachine.DeleteSubKey(@"Software\Classes\" + regInfo.Description);

                
        //        return true;
        //    }

        //    /// <summary>
        //    /// FileTypeRegistered 指定文件类型是否已经注册
        //    /// </summary>        
        //    public static bool FileTypeRegistered(string extendName)
        //    {
        //        RegistryKey softwareKey = Registry.ClassesRoot.OpenSubKey(extendName);
        //        if (softwareKey != null)
        //        {
        //            return true;
        //        }

        //        return false;
        //    }
        //    #endregion
        //}
    }
}
