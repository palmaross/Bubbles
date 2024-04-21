using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using PRAManager;
using Mindjet.MindManager.Interop;

namespace Bubbles
{
    internal class OmniTools
    {
        public static void RunTool(string tool)
        {
            switch (tool)
            {
                case "OT_clock":
                    Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = "/C explorer.exe shell:Appsfolder\\Microsoft.WindowsAlarms_8wekyb3d8bbwe!App";
                    process.StartInfo = startInfo;
                    process.Start();
                    break;
                case "OT_saveall":
                    foreach (Document doc in MMUtils.MindManager.AllDocuments)
                        doc.Save();
                    break;
                case "OT_notepad":
                    
                    break;
            }
        }
    }
}
