using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GaiaPulse
{
    public static class Global
    {
        public static Form BaseForm;
        public static Form CurrentForm;
        public static String AppDir { get; private set; }
        public static String FullPath { get; private set; }

        [STAThread]
        static void Main()
        {
            AppDir = Path.GetDirectoryName(FullPath = Application.ExecutablePath);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            BaseForm = new EntryForm();
            Application.Run(BaseForm);
            
        }
    }
}
