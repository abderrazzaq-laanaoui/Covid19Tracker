using System;
using Covid19Track;
using static Covid19Track.DataManipulation.DBManager;
using System.Collections.Generic;
using System.Data;

namespace Covid19Track
{
    public static class CitoyenDAO 
    {
        public static Citoyen Create(Citoyen dto)
        {
            try
            {
                Connection.Open();
                Command.CommandText = "INSERT INTO `Citoyen` (`cin`, `prenom`, `nom`, `date`, `region`,`etat`, `doses`)" +
                    $" VALUES ('{dto.CIN.ToUpper()}', '{dto.prenom.ToUpper()}', '{dto.nom.ToUpper()}'," +
                    $" (STR_TO_DATE('{dto.dateDeNaissance.ToShortDateString()}','%d/%m/%Y')), {(int)dto.Region}, '{(int)dto.Etat}', '{dto.DosesInjectee}')";
                Command.Connection = Connection;
                Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
            }
            return dto;

        }
        public static bool Excist(string cin)
        {
            bool res = false;
            try
            {
                cin = cin.Trim().ToUpper();
                if (Cnx.State != System.Data.ConnectionState.Open)
                    Cnx.Open();
                Command.CommandText = "SELECT COUNT(*) FROM `Citoyen` WHERE `cin`='" + cin + "'";
                Command.Connection = Cnx;
                int R = Convert.ToInt32(Command.ExecuteScalar());
                res = R != 0;

                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();

            }
            return res;
        }


        public static Citoyen Find(string cin)
        {
            Citoyen res = null;
            try
            {
                cin = cin.Trim().ToUpper();
                if (Connection.State != System.Data.ConnectionState.Open)
                    Connection.Open();
                Command.CommandText = "SELECT * FROM `Citoyen` WHERE `cin`='" + cin + "'";
                Command.Connection = Connection;
                Result = Command.ExecuteReader();

                if (Result.Read())
                    res = new Citoyen(Result[0].ToString(), Result[1].ToString(), Result[2].ToString(),
                        DateTime.Parse(Result[3].ToString()).ToString("d"), (Regions)int.Parse(Result[4].ToString()),(Etats)int.Parse(Result[5].ToString()));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();

            }
            return res;
        }

        public static List<Citoyen> FindAll()
        {
            List<Citoyen> citoyens = new List<Citoyen>();

            try
            {
                if (Connection.State != System.Data.ConnectionState.Open)
                    Connection.Open();
                Command.CommandText = "SELECT  * FROM `Citoyen`";
                Command.Connection = Connection;
                using (var reader = Command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var row = dt.Rows[i];
                        var c = new Citoyen(row[0].ToString(),
                           row[1].ToString(), row[2].ToString(),
                           DateTime.Parse(row[3].ToString()).ToString("d"),
                           (Regions)int.Parse(row[4].ToString()),
                           (Etats)int.Parse(row[5].ToString()), byte.Parse(row[6].ToString()));
                        citoyens.Add(c);
                    }
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return citoyens;
        }

        public static void Delete(string cin)
        {
            try
            {
                cin = cin.Trim().ToUpper();
                if (Connection.State != System.Data.ConnectionState.Open)
                    Connection.Open();
                Command.CommandText = "DELETE FROM `Citoyen` WHERE `cin`='" + cin + "'";
                Command.Connection = Connection;
                int i = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
            }

        }

        public static Citoyen Update(Citoyen dto)
        {
            try
            {
                if (Connection.State != System.Data.ConnectionState.Open)
                    Connection.Open();
                Command.CommandText = $"UPDATE Citoyen SET prenom = '{dto.prenom.ToUpper()}', nom = '{dto.nom.ToUpper()}'," +
                    $" date = (STR_TO_DATE('{dto.dateDeNaissance.ToShortDateString()}','%d/%m/%Y')), region = {(int) dto.Region}" +
                    $"etat = '{(int) dto.Etat}', doses = '{dto.DosesInjectee}' WHERE `cin` = '{dto.CIN}'; ";
                Command.Connection = Connection;
                Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }
            return dto;
        }
    }
}
