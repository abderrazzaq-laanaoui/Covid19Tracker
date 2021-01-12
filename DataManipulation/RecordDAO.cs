// File:    Test.cs
// Author:  ABDERRAZZAQ LAANOUI
// Created: 31 December 2020 04:14:37
// Purpose: Definition of Class Test

using System;
using System.Collections.Generic;
using System.Data;
using static Covid19Track.DataManipulation.DBManager;


namespace Covid19Track
{
    public class RecordDAO
    {
        public static void Create(string CIN, Etats etat, DateTime date)
        {
            try
            {
                Connection.Open();
                Command.CommandText = "INSERT INTO `Record` (`citoyen`, `etat`, `date`)" +
                    $" VALUES ('{CIN.ToUpper()}', '{(int)etat}', " +
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
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
            }
        }

        public static void FindAll(Citoyen citoyen)
        {
            citoyen.Records = new List<Record>();
            try
            {
                Command.CommandText = $"SELECT  `date`, `etat` FROM `Record` WHERE `citoyen` = '{citoyen.CIN}';";
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
                        citoyen.Records.Add(new Record(DateTime.Parse(row[0].ToString()), (Etats)int.Parse(row[1].ToString())));
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

        public static List<Record> SelectAll()
        {
            var list = new List<Record>();
            try
            {
                Command.CommandText = $"SELECT  `date`, `etat` FROM `Record` order by date;";
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
                        list.Add(new Record(DateTime.Parse(row[0].ToString()), (Etats)int.Parse(row[1].ToString())));
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
            return list;
        }
    }
}
