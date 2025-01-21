using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanielAmosApp.Utills.Common
{

    /// <summary>
    /// The class responsible for Log operation.
    /// </summary>
    
    public class LoggerModel
    {
        public void CreateLogInfo(string section, string desc)
        {
            Log.Information($"{section} - {desc}");
        }

        public void CreateErrorInfo(string section, string desc)
        {
            Log.Error($"{section} - {desc}");
        }
    }
}
