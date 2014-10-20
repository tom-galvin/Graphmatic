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
        public static XDocument Settings
        {
            get;
            set;
        }

        public static string DataDirectoryPath
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Graphmatic");
            }
        }

        public static string SettingsFilePath
        {
            get
            {
                return Path.Combine(DataDirectoryPath, "settings.xml");
            }
        }

        public static string SettingsNodeName
        {
            get
            {
                return "settings";
            }
        }

        public static bool SettingsError
        {
            get;
            set;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Main main = new Main();
            LoadSettings();
            Application.Run(new Main());
        }

        public static XDocument CreateSettings(string nodeName)
        {
            Debug.WriteLine(String.Format("Creating new settings file, Root={0}.", nodeName));
            return new XDocument(new XElement("settings"));
        }

        public static void LoadSettings()
        {
            try
            {
                Settings = LoadSettings(SettingsFilePath, SettingsNodeName);
                SettingsError = false;
            }
            catch (IOException ex)
            {
                MessageBox.Show("There was a problem accessing the settings file. " +
                    "Settings changed will not be kept.\r\n" +
                    ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Settings = CreateSettings(SettingsNodeName);
                SettingsError = true;
            }
        }

        public static XDocument LoadSettings(string path, string nodeName)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(DataDirectoryPath);
            }

            if (!File.Exists(SettingsFilePath))
            {
                return CreateSettings(nodeName);
            }
            else
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    Debug.WriteLine(String.Format("Loading settings from {0}.", path));
                    return XDocument.Load(reader);
                }
            }
        }

        public static void SaveSettings(string path, XDocument document)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                Debug.WriteLine(String.Format("Saving settings to {0}.", path));
                document.Save(writer);
            }
        }

        public static void SaveSettings()
        {
            try
            {
                SaveSettings(SettingsFilePath, Settings);
            }
            catch (IOException ex)
            {
                MessageBox.Show("There was a problem saving the settings file.\n" +
                    ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
