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
    public static class Program
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
        /// Gets the global texture path.
        /// </summary>
        /// <value>
        /// The global texture path.
        /// </value>
        public static String TexturePath { get; private set; }

        /// <summary>
        /// Starts the program.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            FullPath = Application.ExecutablePath;
            AppDir = Path.GetDirectoryName(FullPath) + "\\";
            TexturePath = AppDir + "Textures\\";

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var editor = new AnimationEditor();
            Application.Run(editor);
        }
    }
}