using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

using System.Security.Cryptography;

using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using System.Diagnostics;
using System.Management;
using System.ComponentModel;
using System.Linq;

namespace common
{
    public class SettingParas
    {
        public static int parascount = 7;
        public static string GetSettingPath(string filename)
        {
            try
            {
                string path = O.GetSysPath();
                //string path = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\signmgr\\";
                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }

                string fullpath = path + filename;
                if (!System.IO.File.Exists(fullpath))
                {
                    //ftri.Description = "ZPAQHelper.zpaq";
                    //ftri.ExePath = "\"" + args[0].TrimEnd('\\') + "\\" + "ZPAQHelper.exe\"";
                    //ftri.ExtendName = ".zpaq";
                    //ftri.IcoPath = args[0] + "file.ICO,0";
                    //System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    Stream stream = new FileStream(fullpath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                    StreamWriter sw = new StreamWriter(stream);
                    sw.WriteLine("DESCRIPTION=");//
                    sw.WriteLine("EXEPATH=");//
                    sw.WriteLine("EXTENDNAME=");//
                    sw.WriteLine("ICONPATH=");//
                    sw.WriteLine("THREAD=4");//
                    sw.WriteLine("METHOD=5");// 
                    sw.WriteLine("CPULEVEL=Below");
                    sw.WriteLine("SFX=0");
                    sw.Close();
                    stream.Close();
                    stream.Dispose();
                    //FileStream fs = File.Create(path + filename);
                    //fs.Close();                    
                }
                else
                {

                }
                return fullpath;
            }
            catch
            {
                return "";
            }
        }

        //public static Dictionary<string, string> paras = GetSettings();

        public static bool SetSettings(string filename, Dictionary<string, string> paras)
        {
            //List<object> obj = GetSettings(filename);
            try
            {
                string path = GetSettingPath(filename);
                System.IO.File.WriteAllText(path, string.Empty);
                //System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Write, FileShare.Read))
                {
                    stream.Position = 0;
                    StreamWriter sw = new StreamWriter(stream);
                    foreach (string k in paras.Keys)
                    {
                        sw.WriteLine(k + "=" + paras[k]);
                    }
                    sw.Close();
                    stream.Close();
                    stream.Dispose();
                }
            }
            catch (Exception ex)
            {
                O.WriteLog("Fail to save settings：" + ex.ToString());
                return false;
            }
            return true;
        }

        public static bool SetSettings(Dictionary<string, string> paras)
        {
            return SetSettings("settings.ini", paras);
        }

        public static Dictionary<string, string> GetSettings()
        {
            return GetSettings("settings.ini");
        }

        public static Dictionary<string, string> GetSettings(string filename)
        {
            string path = GetSettingPath(filename);
            //object[] obj = new object[count];
            //System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            bool updated = false;
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

            StreamReader sr = new StreamReader(stream);
            Dictionary<string, string> p = new Dictionary<string, string>();
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                int pos = line.IndexOf('=');
                if (pos > 0)
                {
                    string key = line.Substring(0, pos).Trim();
                    string value = "";
                    if (pos + 1 < line.Length)
                    {
                        value = line.Substring(pos + 1).Trim();
                    }
                    p.Add(key, value);
                }
            }
            sr.Close();
            stream.Close();
            //sw.WriteLine("DESCRIPTION=ZPAQHelper.zpaq");//
            //sw.WriteLine("EXEPATH=" + "\"" + ZPAQHelper.MainForm.GetSysPath() + "ZPAQHelper.exe\"");//
            //sw.WriteLine("EXTENDNAME=.zpaq");//
            //sw.WriteLine("ICONPATH=" + ZPAQHelper.MainForm.GetSysPath() + "file.ICO,0");//
            //sw.WriteLine("THREAD=4");//
            //sw.WriteLine("METHOD=5");//

            //-----------------TYPEINFO-----------------
            if (!p.ContainsKey("DESCRIPTION"))
            {
                p.Add("DESCRIPTION", "ZPAQHelper.zpaq");
                updated = true;
            }

            if (!p.ContainsKey("EXEPATH"))
            {
                p.Add("EXEPATH", "\"" + O.GetSysPath() + "ZPAQHelper.exe\"");
                updated = true;
            }
            else 
            {
                //bool ok = true;
                //string s = p["EXEPATH"];
                //if(s.Length < 1)
                //    ok = false;
                //else
                //{
                //    s = s.Substring(1).TrimEnd('\"');
                //    if (!System.IO.File.Exists(s))
                //        ok = false;
                //}

                //if (!ok)
                //{
                //    p["EXEPATH"] = "\"" + O.GetSysPath() + "ZPAQHelper.exe\"";
                //    updated = true;
                //}
            }

            if (!p.ContainsKey("EXTENDNAME"))
            {
                p.Add("EXTENDNAME", ".zpaq");
                updated = true;
            }
            else if (p["EXTENDNAME"] != ".zpaq")
            {
                p["EXTENDNAME"] = ".zpaq";
                updated = true;
            }

            if (!p.ContainsKey("ICONPATH"))
            {
                p.Add("ICONPATH", O.GetSysPath() + "file.ICO,0");
                updated = true;
            }
            else 
            {
                //bool ok = true;
                //string s = p["ICONPATH"];
                //if (s.Length < 3)
                //    ok = false;
                //else
                //{
                //    if (!System.IO.File.Exists(s))
                //    {
                //        s = s.Substring(0, s.Length - 2);
                //        if (!System.IO.File.Exists(s))
                //            ok = false;
                //    }
                //}
                //if (!ok)
                //{
                //    p["ICONPATH"] = O.GetSysPath() + "file.ICO,0";
                //    updated = true;
                //}
            }

            //-----------------compress settings-----------------
            if (!p.ContainsKey("THREAD"))
            {
                p.Add("THREAD", "4");
                updated = true;
            }
            else if ( O.Convert2int(  p["THREAD"] )<=0 )
            {
                p["THREAD"] = "4";
                updated = true;
            }

            if (!p.ContainsKey("METHOD"))
            {
                p.Add("METHOD", "5"); updated = true;
            }
            else if (O.Convert2int(p["METHOD"]) >5 || O.Convert2int(p["METHOD"]) <= 0)
            {
                p["METHOD"] = "5"; updated = true;
            }

            if (!p.ContainsKey("CPULEVEL"))
            {
                p.Add("CPULEVEL", "Below"); updated = true;
            }
            else 
            {
                string l = p["CPULEVEL"].ToLower();
                if (l == "above" || l == "normal" || l == "below" || l == "low")
                {

                }
                else
                {
                    p["CPULEVEL"] = "Below";
                    updated = true;
                }
            }

            if (!p.ContainsKey("SFX"))
            {
                p.Add("SFX", "0"); updated = true;
            }
            else if (O.Convert2int(p["SFX"]) > 1 || O.Convert2int(p["SFX"]) < 0)
            {
                p["SFX"] = "0"; updated = true;
            }

            if (updated)
            {
                if (!SetSettings(p))
                {
                    O.WriteLog("can't save the settings.");
                }
            }
            return p;
        }
    }

    public class O
    {
        private static Random random = new Random((int)(DateTime.Now.Ticks & 0xffffffffL) | (int)(DateTime.Now.Ticks >> 32));

        //public static string uploadimagepath = @"../upload/image/";
        //public static string uploadfilepath = @"../upload/file/";

        public O()
        {
        }

        public static int GetRandom()
        {
            return random.Next();
        }

        public static int GetRandom(int min, int max)
        {
            return random.Next(min, max);
        }

        public static bool Convert2double(object obj, ref double value)
        {
            if(obj==null || Convert.IsDBNull(obj))
            {
                value = -1;                
                return false;
            }
            try
            {
                value = Convert.ToDouble(obj);
                return true;
            }
            catch (Exception ex)
            {
                value = -1;
                O.WriteLog("转换成浮点数失败："+ex.ToString()+" OBJ:"+ obj);
                return false;
            }
        }

        public static bool Convert2bool(object obj)
        {
            if (obj == null)
                return false;

            try
            {
                return Convert.ToBoolean(obj);
            }
            catch (Exception ex)
            {
                O.WriteLog(ex.ToString() + " OBJ:" + obj);
                return false;
            }
        }
                
        public static DateTime Convert2date(object obj, ref bool error)
        {
            try
            {
                error = false;
                return Convert.ToDateTime(obj);
            }
            catch (Exception ex)
            {
                error = true;
                O.WriteLog(O.GetString(obj) + "日期转换错误：" + ex.ToString());                
            }
            return DateTime.Now;
        }

        public static bool isDate(object obj)
        {
            try
            {
                DateTime d = Convert.ToDateTime(obj);
                return true;
            }
            catch (Exception ex)
            {
                O.WriteLog(ex.ToString() + " OBJ:" + obj);
            }
            return false;
        }
                
       
        public static bool IsGuidByParse(string strSrc)
        {
            Guid g = Guid.Empty;
            return Guid.TryParse(strSrc, out g);
        }

        public static string GetString(object d)
        {
            try
            {
                if (!Convert.IsDBNull(d) && d != null)
                    return d.ToString();
            }
            catch 
            {
                return "";
            }
            return "";
        }
        
        public static int Convert2int(object obj)
        {
            if (obj == null || Convert.IsDBNull(obj))
                return -1;
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return -1;
            }
        }
        
        public static void WriteLog(string content)
        {
            WriteLog("", content);
        }

        public static void WriteLog(string filename, string content)
        {
            try
            {
                string folder = O.GetSysPath() + "syslogs\\";
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                string path = folder + DateTime.Now.ToString("yyyyMMdd") + ".log";
                if (filename != "")
                    path = folder + filename;


                if (!File.Exists(path))
                {
                    FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
                    fs.Close();
                }
                StreamWriter sw = new StreamWriter(path, true);
                sw.WriteLine(DateTime.Now.ToString() + " " + content);
                sw.WriteLine("");
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("创建日志文件失败："+ex.ToString());
                //File.Delete(path);
            }
        }

        public static void WriteTemp(string content)
        {
            WriteTemp("", content);           
        }

        public static void WriteTemp(string filename, string content)
        {
            string path = O.GetSysPath() + "signmgr.tmp";
            if (filename != "")
                path = O.GetSysPath() + filename;

            try
            {
                if (!File.Exists(path))
                {
                    FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
                    fs.Close();
                }
                StreamWriter sw = new StreamWriter(path, true);
                sw.WriteLine(content);
                //sw.WriteLine("");
                sw.Close();
            }
            catch (Exception ex)
            {
                File.Delete(path);
                O.WriteLog("将 [" + content + "] 写入" + filename + "文件失败：" + ex.ToString());
            }
        }

        public static void DelTempFiles()
        {
            DelTempFiles("signmgr.tmp");
        }

        public static void DelTempFiles(string filename)
        {
            string path = O.GetSysPath() + filename;
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        try
                        {
                            File.Delete(line);
                        }
                        catch
                        { }
                    }
                    sr.Close();
                }    
                File.Delete(path);
            }
            catch
            { }
            
        }
              

        /// <summary>
        /// 获取当前时间组成的字符串，用作生成不会重复的文件名
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            string strRet = string.Empty;
            System.DateTime dtNow = DateTime.Now;
            strRet += dtNow.Year.ToString() +
                        dtNow.Month.ToString("00") +
                        dtNow.Day.ToString("00") +
                        dtNow.Hour.ToString("00") +
                        dtNow.Minute.ToString("00") +
                        dtNow.Second.ToString("00") +
                        System.DateTime.Now.Millisecond.ToString("000");
            return strRet;

        }
        
        public static long GetHardDiskSpace(string AppPath)
        {
            try
            {
                string volume = AppPath.Substring(0, AppPath.IndexOf(':'));
                //long freespace = GetHardDiskSpace(volume);

                long totalSize = 0;
                string str_HardDiskName = volume + ":\\";
                System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
                foreach (System.IO.DriveInfo drive in drives)
                {
                    if (drive.Name == str_HardDiskName)
                    {
                        totalSize = drive.TotalFreeSpace / (1024);
                        break;
                    }
                }
                return totalSize;//size in K
            }
            catch (Exception ex)
            {
                O.WriteLog("获取" + AppPath + "盘剩余空间错误：" + ex.ToString());
            }
            return -1;
        }
        
                   

        private const string KEY_64 = ")4dL#5ps";//注意了，是8个字符，64位    
        private const string IV_64 = "6|9*F3wz";//注意了，是8个字符，64位
        public static string Encode(string data)
        {
            try
            {
                byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
                byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

                int i = cryptoProvider.KeySize;

                MemoryStream ms = new MemoryStream();

                CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

                StreamWriter sw = new StreamWriter(cst);

                sw.Write(data);

                sw.Flush();

                cst.FlushFinalBlock();

                sw.Flush();

                return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
            }
            catch(Exception ex)
            {
                O.WriteLog("加密"+data+"失败："+ex.ToString());
                return "";
            }
        }
        public static string Decode(string data)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);

            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

            byte[] byEnc;

            try
            {
                byEnc = Convert.FromBase64String(data);
            }
            catch
            {
                return null;
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

            MemoryStream ms = new MemoryStream(byEnc);

            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);

            StreamReader sr = new StreamReader(cst);

            return sr.ReadToEnd();
        }

        private static string aeskey = @"kis25,^#FK9)2356vmk z/., `23cvmp";
        public static string Encrypt(string encryptStr, string key)
        {
            if (encryptStr == null)
                return "";
            if (encryptStr == "")
                return "";
            try
            {
                byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(encryptStr);
                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = rDel.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch 
            {
                return "";
            }
        }

        public static string Encrypt(string encryptStr)
        {
            return Encrypt(encryptStr, aeskey);
        }

        public static string Decrypt(string decryptStr, string key)
        {
            if (decryptStr == null)
                return "";
            if (decryptStr == "")
                return "";
            try
            {
                byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
                byte[] toEncryptArray = Convert.FromBase64String(decryptStr);
                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch 
            {
                return "";
            }
        }

        public static string Decrypt(string decryptStr)
        {
            return Decrypt(decryptStr, aeskey);
        }

        public static bool IsFileInUse(string fileName)
        {
            bool inUse = true;

            FileStream fs = null;
            try
            {

                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);

                inUse = false;
            }
            catch
            {
            }
            finally
            {
                if (fs != null)

                    fs.Close();
            }
            return inUse;//true表示正在使用,false没有使用  
        }

        
        
        public static string GetSysPath()
        {
            return System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        }

        /// <summary>
        /// 判断程序是否是以管理员身份运行。
        /// </summary>
        public static bool IsRunAsAdmin()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(id);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }        
    }

    public static class ProcessCommandline
    {
        /// <param name="process">一个正在运行的进程。</param>
        /// <returns>表示应用程序运行命令行参数的字符串。</returns>
        public static string GetCommandLineArgs(Process process)
        {
            try
            {

                using (var searcher = new ManagementObjectSearcher(
                    "SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))
                using (var objects = searcher.Get())
                {
                    var @object = objects.Cast<ManagementBaseObject>().SingleOrDefault();
                    return @object?["CommandLine"]?.ToString() ?? "";
                }

            }
            catch (Win32Exception ex) when ((uint)ex.ErrorCode == 0x80004005)
            {
                // 没有对该进程的安全访问权限。
                return string.Empty;
            }
            catch (InvalidOperationException)
            {
                // 进程已退出。
                return string.Empty;
            }
        }
    }
}
