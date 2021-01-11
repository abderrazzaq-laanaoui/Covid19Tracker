using System;
using System.Collections.Generic;
using System.Data;
using static Covid19Track.DataManipulation.DBManager;

namespace Covid19Track.DataManipulation
{
    class RencontreDAO
    {
        public static void Create(string c1, string c2, DateTime date)
        {
            try
            {
                Connection.Open();
                Command.CommandText = "INSERT INTO `Rencontre` (`citoyen1`, `citoyen2`, `date`)" +
                    $" VALUES ('{c1.ToUpper()}', '{c2.ToUpper()}'," +
                    $"( STR_TO_DATE('{date.ToShortDateString()}','%d/%m/%Y')) )";
                Command.Connection = Connection;
                Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }

        }

        public static void FindAll(Citoyen citoyen)
        {
            citoyen.Rencontres = new List<Rencontre>();
            try
            {

                string[] source = new string[] { "citoyen1", "citoyen2" };
                for (int i = 0; i < source.Length; i++)
                {
                    Command.CommandText = $"SELECT  `{source[i]}`, `date` FROM `Rencontre` WHERE `{source[(i + 1) % 2]}` = '{citoyen.CIN}';";
                    if (Connection.State != ConnectionState.Open)
                        Connection.Open();
                    Command.Connection = Connection;
                    using (var reader = Command.ExecuteReader())
                    {
                        var dt = new DataTable();
                        dt.Load(reader);
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            var row = dt.Rows[j];
                            citoyen.Rencontres.Add(new Rencontre(row[0].ToString(),
                                DateTime.Parse(row[1].ToString())));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
            }
        }

    }
}
