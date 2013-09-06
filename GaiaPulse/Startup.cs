using System;
using System.IO;
using System.Windows.Forms;
using GaiaPulse.AnimationManager;

namespace GaiaPulse
{
    /// <summary>
    /// Used to start the application.
    /// </summary>
    ///
    public static class Startup
    {
        /// <summary>
        /// Gets the application's directory.
        /// </summary>
        /// <value>
        /// The application's directory.
        /// </value>
        public static String AppDir { get; private set; }

        /// <summary>
        /// Gets the full path of the application, including filename and type.
        /// </summary>
        /// <value>
        /// The full path of the application.
        /// </value>
        public static String FullPath { get; private set; }

        /// <summary>
        /// Starts the program.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            AppDir = Path.GetDirectoryName(FullPath = Application.ExecutablePath);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var editor = new AnimationEditor();
            Application.Run(editor);
        }
    }
}