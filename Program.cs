using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS;

namespace ZonView017
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            ESRI.ArcGIS.RuntimeManager.Bind(ProductCode.ArcReader);

            if (!RuntimeManager.Bind(ProductCode.ArcReader))
            {
                MessageBox.Show(
                    "O ArcReader 10 não fou instalado. Instale antes de executar o ViZon.");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmPrincipal());

        }
    }
}