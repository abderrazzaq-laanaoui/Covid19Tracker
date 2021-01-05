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
    class IsolationDAO
    {
        public static void Create(string CIN, DateTime DateDebut, DateTime DateFin)
        {
            try
            {
                Connection.Open();
                Command.CommandText = "INSERT INTO `Isolation` (`citoyen`, `dateDebut`, `dateFin`)" +
                                        $" VALUES ('{CIN.ToUpper()}'," +
                                        $"( STR_TO_DATE('{DateDebut.ToShortDateString()}','%d/%m/%Y')),"+
                                        $"( STR_TO_DATE('{DateFin.ToShortDateString()}','%d/%m/%Y')) )";
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
        }

        public static void FindAll(Citoyen citoyen)
        {
            citoyen.Isolations = new List<Isolation>();
            try
            {
                Command.CommandText = $"SELECT  `dateDebut`, `dateFin` FROM `Isolation` WHERE `citoyen` = '{citoyen.CIN}';";
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
                        citoyen.Isolations.Add(new Isolation(DateTime.Parse(row[0].ToString()), DateTime.Parse(row[1].ToString())) );
                    }
                }

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

    }
}