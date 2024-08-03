using MongoBackups.Infrastructure;
using MongoBackups.Services;
using MongoBackups.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MongoBackups
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IExecMongoCommand IEMC = new ExecMongoCommand();
            IMongoServices services = new MongoServices();
            Application.Run(new Login(services, IEMC));
        }
    }
}
