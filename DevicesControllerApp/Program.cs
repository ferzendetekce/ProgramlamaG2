using DevicesControllerApp.Ana_ekran_Login;
using System;
using System.Windows.Forms;

namespace DevicesControllerApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
           Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Login loginForm = new Login();

          
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                
                Application.Run(new MainForm());
           }

          
            else
            {
                Application.Exit();
            }
        }
    }
}
