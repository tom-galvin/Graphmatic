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
        public static Settings Settings
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

        public static void LoadSettings()
        {
            try
            {
                Settings = LoadSettings(SettingsFilePath);
                SettingsError = false;
            }
            catch (IOException ex)
            {
                MessageBox.Show("There was a problem accessing the settings file. " +
                    "Settings changed will not be kept.\r\n" +
                    ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Settings = new Settings();
                SettingsError = true;
            }
        }

        public static Settings LoadSettings(string path)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(DataDirectoryPath);
            }

            if (!File.Exists(SettingsFilePath))
            {
                return new Settings();
            }
            else
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    Debug.WriteLine(String.Format("Loading settings from {0}.", path));
                    return new Settings(XDocument.Load(reader).Root);
                }
            }
        }

        public static void SaveSettings(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                Debug.WriteLine(String.Format("Saving settings to {0}.", path));
                XDocument settingsDocument = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                    Settings.ToXml());
                settingsDocument.Save(writer);
            }
        }

        public static void SaveSettings()
        {
            try
            {
                SaveSettings(SettingsFilePath);
            }
            catch (IOException ex)
            {
                MessageBox.Show("There was a problem saving the settings file.\n" +
                    ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
