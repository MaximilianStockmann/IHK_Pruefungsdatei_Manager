using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IHK_Pruefungsdatei_Manager
{
    static class Program
    {
        private static int SCREEN_WIDTH = Screen.PrimaryScreen.Bounds.Width;
        private static int SCREEN_HEIGHT = Screen.PrimaryScreen.Bounds.Height;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DefaultManager defaultManager = new DefaultManager();

            //defaultManager.ShowDialog();

            Application.Run(defaultManager);

        }

        private static void Init()
        {

        }
    }
}
