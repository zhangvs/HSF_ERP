using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HZSoft.Util
{
    /// <summary>
    /// 日志
    /// </summary>
    public class LogHelper
    {
        const string path = "/log/";
        public static void AddLog(string msg)
        {
            StackTrace trace = new StackTrace();
            StackFrame frame = trace.GetFrame(1);//1代表上级，2代表上上级，以此类推
            MethodBase method = frame.GetMethod();
            String className = method.ReflectedType.Name;

            msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-" + className + "-" + method.Name +"-"+ msg;
            string fullPath = HttpContext.Current.Server.MapPath(path);

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            string filename = DateTime.Now.ToString("yyyyMMdd") + ".txt";

            using (StreamWriter sw = new StreamWriter(fullPath + filename, true))
            {
                sw.WriteLine(msg);
            }
        }
    }
}
