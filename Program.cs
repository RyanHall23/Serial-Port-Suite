using System;
using System.Windows.Forms;

namespace SerialSuite
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());   //main menu form
            Application.Run(new Form2());   //options form
        }
    }
}
