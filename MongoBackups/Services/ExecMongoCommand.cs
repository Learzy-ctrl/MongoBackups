using MongoBackups.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoBackups.Services
{
    public class ExecMongoCommand : IExecMongoCommand
    {
        public bool mongoImport(string _database, string _collection, string _connectionString, string _path)
        {
            string mongoImportCommand = "mongoimport";
            string mongoImportArguments = $"--host {_connectionString} --db {_database} --collection {_collection} --file {_path}";
            return executeCommand(mongoImportCommand, mongoImportArguments);
        }
        public bool mongoExport(string _database, string _collection, string _connectionString, string _path)
        {
            string mongoExportCommand = "mongoexport";
            string mongoExportArguments = $"--host {_connectionString} --db {_database} --collection {_collection} --out {_path}";
            return executeCommand(mongoExportCommand, mongoExportArguments);
        }

        public bool mongoDump(string _database, string _connectionString, string _path)
        {
            string mongoDumpCommand = "mongodump";
            string mongoDumpArguments = $"--host {_connectionString} --db {_database} --out {_path}";
            return executeCommand(mongoDumpCommand, mongoDumpArguments);
        }

        private static bool executeCommand(string command, string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = command;
            startInfo.Arguments = arguments;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            using (Process process = new Process())
            {
                try
                {
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }
    }
}
