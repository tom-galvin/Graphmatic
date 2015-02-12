using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Graphmatic
{
    public static class Program
    {
        public static Random Random = new Random();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if(!Debugger.IsAttached)
                Application.ThreadException += Application_ThreadException;

            SerializationExtensionMethods.RegisterGraphmaticObjects();

            Main main = new Main();
            if (args.Length >= 1)
                main.OpenDocument(args[0]);

            Application.Run(new Main());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            new ErrorReportingDialog(e.Exception).ShowDialog();
        }
    }
}
