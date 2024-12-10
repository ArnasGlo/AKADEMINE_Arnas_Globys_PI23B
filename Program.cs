using System;
using System.Windows.Forms;
namespace AKADEMINE_Arnas_Globys_PI23B

{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DBdetails dbDetails = new DBdetails("localhost", "akademineis", "root", "");
            dbDetails.TestConnection();
            Application.Run(new login());
           
        }
    }
}