using System;
using System.Collections.Generic;
using System.Data;
using static Covid19Track.DataManipulation.DBManager;

namespace Covid19Track
{
    public static class EtablisementDAO
    {
        public static Etablissement Create(Etablissement dto, int type)
        {

            try
            {
                if (type == 1)
                {
                    Command.CommandText = "INSERT INTO `Etablisement` (`reference`, `nom`, `type`)" +
                                        $" VALUES ('{((Laboratoire)dto).reference.ToUpper()}', '{((Laboratoire)dto).nom.ToUpper()}', '1')";
                }
                else
                {
                    Command.CommandText = "INSERT INTO `Etablisement` (`reference`, `nom`, `type`)" +
                    $" VALUES ('{((CentreDeVaccination)dto).reference.ToUpper()}', '{((CentreDeVaccination)dto).nom.ToUpper()}', '2')";
                }
                if (Connection.State != System.Data.ConnectionState.Open)
                    Connection.Open();
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
        public static bool Excist(string reference, int type)
        {
            bool res = false;
            try
            {
                reference = reference.Trim().ToUpper();
                if (Cnx.State != System.Data.ConnectionState.Open)
                    Cnx.Open();
                Command.CommandText = "SELECT COUNT(*) FROM `Etablisement` WHERE `type`='" + type + "' and `reference` = '" + reference + "'";
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


        public static Etablissement Find(string reference, int type)
        {
            Etablissement res = null;
            try
            {
                reference = reference.Trim().ToUpper();
                if (Connection.State != System.Data.ConnectionState.Open)
                    Connection.Open();
                Command.CommandText = "SELECT *  FROM `Etablisement` WHERE `type`='" + type + "' and `reference` = '" + reference + "';";
                Command.Connection = Connection;
                Result = Command.ExecuteReader();

                if (Result.Read())
                    if (type == 1)
                    {
                        res = new Laboratoire(Result[0].ToString(), Result[1].ToString());
                    }
                    else
                    {
                        res = new CentreDeVaccination(Result[0].ToString(), Result[1].ToString());

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
            return res;
        }

        public static List<Etablissement> FindAll(int type)
        {
            List<Etablissement> etablissements = new List<Etablissement>();

            try
            {
                if (Connection.State != System.Data.ConnectionState.Open)
                    Connection.Open();
                Command.CommandText = "SELECT * FROM `Etablisement` WHERE `type` = '" + type + "'";
                Command.Connection = Connection;
                using (var reader = Command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var row = dt.Rows[i];
                        Etablissement e;
                        if (type == 1)
                        {
                            e = new Laboratoire(row[0].ToString(), row[1].ToString());
                        }
                        else
                        {
                            e = new CentreDeVaccination(row[0].ToString(), row[1].ToString());

                        }

                        etablissements.Add(e);
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
            return etablissements;
        }

        public static void Delete(string reference, int type)
        {
            try
            {
                reference = reference.Trim().ToUpper();
                if (Connection.State != System.Data.ConnectionState.Open)
                    Connection.Open();
                Command.CommandText = "DELETE FROM `Etablisement` WHERE `reference`='" + reference + "' and `type`='" + type + "';";
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

        public static Etablissement Update(Etablissement dto, int type)
        {
            try
            {

                if (type == 1)
                {
                    Command.CommandText = $"UPDATE `Etablisement` SET  nom = '{((Laboratoire)dto).nom.ToUpper()}'" +
                                        $" WHERE `reference` = '{((Laboratoire)dto).reference}' and `type` = '1';";
                }
                else
                {
                    Command.CommandText = $"UPDATE `Etablisement` SET  nom = '{((CentreDeVaccination)dto).nom.ToUpper()}'" +
                                       $" WHERE `reference` = '{((CentreDeVaccination)dto).reference}' and `type` = '2';";
                }
                if (Connection.State != System.Data.ConnectionState.Open)
                    Connection.Open();
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
