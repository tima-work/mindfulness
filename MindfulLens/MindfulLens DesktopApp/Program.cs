using MindfulLens_DesktopApp;
using MindfulLens_DesktopApp.Start_forms;

namespace MindfulLens_DesktopApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new AdminForm());
            //Application.Run(new VisitorForm());
            Application.Run(new StartForm());
        }
    }
}