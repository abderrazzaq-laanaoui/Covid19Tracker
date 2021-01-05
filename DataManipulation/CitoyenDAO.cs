using System;
using Covid19Track;
using static Covid19Track.DataManipulation.DBManager;
using System.Collections.Generic;


namespace Covid19Track
{
    public static class CitoyenDAO 
    {
        public static Citoyen Create(Citoyen dto)
        {
            try
            {
                Connection.Open();
                Command.CommandText = "INSERT INTO `Citoyen` (`cin`, `nom`, `prenom`, `dateNaissance`, `etat`, `doseInjectee`)" +
                    $" VALUES ('{dto.CIN.ToUpper()}', '{dto.nom.ToUpper()}', '{dto.prenom.ToUpper()}'," +
                    $" '{dto.dateDeNaissance.ToString("d")}', '{(int)dto.Etat}', '{dto.DosesInjectee}')";
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

        public static Citoyen Find(string cin)
        {
            Citoyen res = null;
            try
            {
                cin = cin.Trim().ToUpper();
                Connection.Open();
                Command.CommandText = "SELECT * FROM `Citoyen` WHERE `cin`='" + cin + "'";
                Command.Connection = Connection;
                Result = Command.ExecuteReader();

                if (Result.Read())
                    res = new Citoyen(Result[0].ToString(), Result[1].ToString(), Result[2].ToString(),
                        DateTime.Parse(Result[3].ToString()).ToString("d"), (Etats)Int64.Parse(Result[4].ToString()));
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
                Connection.Open();
                Command.CommandText = "SELECT * FROM `Citoyen`";
                Command.Connection = Connection;
                Result = Command.ExecuteReader();
                while (Result.Read())
                {
                    var c = new Citoyen(Result[0].ToString(), Result[1].ToString(), Result[2].ToString(),
                        DateTime.Parse(Result[3].ToString()).ToString("d"), (Etats)Int64.Parse(Result[4].ToString()));  

                    c.DosesInjectee = byte.Parse(Result[5].ToString());
                    citoyens.Add(c);
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
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            }

        }

        public static Citoyen Update(Citoyen dto)
        {
            try
            {
                Connection.Open();
                Command.CommandText = $"UPDATE Citoyen SET prenom = '{dto.prenom.ToUpper()}', nom = '{dto.nom.ToUpper()}'," +
                    $" date = '{dto.dateDeNaissance.ToString("d")}', etat = '{(int) dto.Etat}', doses = '{dto.DosesInjectee}' WHERE `cin` = '{dto.CIN}'; ";
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
