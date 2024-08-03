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
    public partial class Options : Form
    {
        private readonly IExecMongoCommand exec;
        private readonly IMongoServices services;
        private readonly string connectionString;
        public Options(IExecMongoCommand _mongoCommand, IMongoServices _services, string _connectionString)
        {
            InitializeComponent();
            exec = _mongoCommand;
            services = _services;
            connectionString = _connectionString;
        }

        private async void Options_Load_1(object sender, EventArgs e)
        {
            lblServerName.Text = connectionString;
            dataGridView1.RowTemplate.Height = 50;
            await dataTableRefresh();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            pbLoading.Visible = true;
            if (e.ColumnIndex == dataGridView1.Columns["Import"].Index && e.RowIndex >= 0)
            {
                string database = dataGridView1.Rows[e.RowIndex].Cells["Database"].Value.ToString();
                string collection = dataGridView1.Rows[e.RowIndex].Cells["Collection"].Value.ToString();
                var path = openFile();
                var response = exec.mongoImport(database, collection, connectionString, path);
                if (response)
                {
                    MessageBox.Show("Se ha importado correctamente");
                }
                else
                {
                    MessageBox.Show("ha ocurrido un error");
                }
            }
            if (e.ColumnIndex == dataGridView1.Columns["Export"].Index && e.RowIndex >= 0)
            {
                string database = dataGridView1.Rows[e.RowIndex].Cells["Database"].Value.ToString();
                string collection = dataGridView1.Rows[e.RowIndex].Cells["Collection"].Value.ToString();
                var path = $"\\\\BackupServer\\Backup\\backupManual\\{collection}Export.json";
                var response = exec.mongoExport(database, collection, connectionString, path);
                if (response)
                {
                    MessageBox.Show("Se ha exportado correctamente");
                }
                else
                {
                    MessageBox.Show("ha ocurrido un error");
                }
            }
            if (e.ColumnIndex == dataGridView1.Columns["Backup"].Index && e.RowIndex >= 0)
            {
                string database = dataGridView1.Rows[e.RowIndex].Cells["Database"].Value.ToString();
                var path = "\\\\BackupServer\\Backup\\backupManual";
                var response = exec.mongoDump(database, connectionString, path);
                if (response)
                {
                    MessageBox.Show("Se ha respaldado correctamente");
                }
                else
                {
                    MessageBox.Show("ha ocurrido un error");
                }
            }
            pbLoading.Visible = false;
        }

        private void Options_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //private string saveFile()
        //{

        //    SaveFileDialog saveFileDialog1 = new SaveFileDialog();

        //    saveFileDialog1.Filter = "Archivos JSON (*.json)|*.json|Archivos CSV (*.csv)|*.csv";
        //    saveFileDialog1.FilterIndex = 1;
        //    saveFileDialog1.RestoreDirectory = true;
        //    string rutaArchivo = "";


        //    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        //    {
        //        rutaArchivo = saveFileDialog1.FileName;
        //    }
        //    return rutaArchivo;
        //}

        private string openFile()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Archivos JSON (*.json)|*.json|Archivos CSV (*.csv)|*.csv";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            string rutaArchivo = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo = openFileDialog1.FileName;
            }
            return rutaArchivo;
        }

        private async Task dataTableRefresh()
        {
            pbLoading.Visible = true;
            var list = await services.toListCollectionMongoDB(connectionString);

            dataGridView1.Rows.Clear();

            foreach (var item in list)
            {
                dataGridView1.Rows.Add(item.DatabaseName, item.Name, item.Documents, "Importar", "Exportar", "Backup");
            }
            pbLoading.Visible = false;
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await dataTableRefresh();
        }
    }
}
