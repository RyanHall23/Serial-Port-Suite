using System;
using System.Windows.Forms;

namespace SerialSuite
{
    static class SerialSuite
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindowForm());   //main menu form
        }
    }
}
