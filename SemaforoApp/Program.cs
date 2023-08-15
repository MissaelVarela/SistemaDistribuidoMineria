using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SemaforoApp.SemaforoClient;

namespace SemaforoApp
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {

            SemaforoApp semaforoApp1 = new SemaforoApp(3, 0)
            {
                Id = "3",
                IntersectionId = "4",
                Clave = "12345"
            };
            SemaforoApp semaforoApp2 = new SemaforoApp(3, 1)
            {
                Id = "4",
                IntersectionId = "4",
                Clave = "12345"
            };
            SemaforoApp semaforoApp3 = new SemaforoApp(3, 2)
            {
                Id = "6",
                IntersectionId = "4",
                Clave = "12345"
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new SemaforoDisplay(semaforoApp1));

            SemaforoDisplay s1 =new SemaforoDisplay(semaforoApp1);
            SemaforoDisplay s2 = new SemaforoDisplay(semaforoApp2);
            SemaforoDisplay s3 = new SemaforoDisplay(semaforoApp3);

            SemaforoDisplay[] forms = new SemaforoDisplay[] { s1, s2, s3};
            Application.Run(new Controlador(forms));
        }
    }
}
