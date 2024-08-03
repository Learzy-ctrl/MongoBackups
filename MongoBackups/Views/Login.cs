using MongoBackups.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MongoBackups.Views
{
    public partial class Login : Form
    {
        private readonly IMongoServices services;
        private readonly IExecMongoCommand command;
        public Login(IMongoServices _services, IExecMongoCommand _execMongo)
        {
            InitializeComponent();
            services = _services;
            command = _execMongo;
        }

        private async void btnConect_Click_1(object sender, EventArgs e)
        {
            pbLoading.Visible = true;
            var response = await services.Verifyconnection(txtConnectionString.Text);
            if (response)
            {
                var options = new Options(command, services, txtConnectionString.Text);
                options.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Error en la conexion");
            }
            pbLoading.Visible = false;
        }
    }
}
