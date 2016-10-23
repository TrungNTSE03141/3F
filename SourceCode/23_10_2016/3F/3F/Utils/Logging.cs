using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace _3F.Utils
{
    public class Logging
    {
        public static void Log(string e)
        {
            File.AppendAllText(Utils.DIR_FILE_LOG, e);
        }
    }
}