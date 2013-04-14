using System;
using System.IO;
using System.Windows.Forms;

namespace GaiaPulse
{
    public static class Global
    {
        public static Form BaseForm;
        
        public static String AppDir { get; private set; }

        public static String FullPath { get; private set; }

        [STAThread]
        private static void Main()
        {
            AppDir = Path.GetDirectoryName(FullPath = Application.ExecutablePath);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            BaseForm = new EntryForm();
            Application.Run(BaseForm);
        }
    }
}