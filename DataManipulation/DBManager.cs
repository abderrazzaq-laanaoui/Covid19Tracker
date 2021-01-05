using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Covid19Track.DataManipulation
{
    public static class DBManager
    {
        private static readonly string cnxStr;
        public static MySqlConnection Connection { get; set; }
        public static MySqlConnection Cnx { get; set; }
        public static MySqlCommand Command { get; set; }
        public static MySqlDataAdapter Adapter { get; set; }
        public static MySqlDataReader Result { get; set; }
        public static DataTable dataTable;

        static DBManager()
        {
            try
            {
                cnxStr = "Server=95.217.200.204;Database=lmastran_Covid19Tracker;Uid=lmastran_Covid19Admin;Pwd=projet@2020;";
                Connection = new MySqlConnection(cnxStr);
                Cnx = new MySqlConnection(cnxStr);
                Command = new MySqlCommand();
                Adapter = new MySqlDataAdapter(Command);
                Command.Connection = Connection;
                Adapter.SelectCommand = Command;
                dataTable = new DataTable();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

    }
}
