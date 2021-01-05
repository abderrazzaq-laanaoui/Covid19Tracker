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
    class TestDAO
    {
        public static void Create(string CIN, string laboratoire, bool resultat, DateTime date)
        {
            int res = resultat ? 1 : 0;
            try
            {
                Connection.Open();
                Command.CommandText = "INSERT INTO `Test` (`citoyen`, `labo`, `date`, `resultat`)" +
                    $" VALUES ('{CIN.ToUpper()}', '{laboratoire.ToUpper()}', " +
                    $"( STR_TO_DATE('{date.ToShortDateString()}','%d/%m/%Y')), '{ res}' )";
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
        }

        public static void FindAll(Citoyen citoyen)
        {
            citoyen.Tests = new List<Test>();
            try
            {
                Command.CommandText = $"SELECT  `labo`, `date`, `resultat` FROM `Test` WHERE `citoyen` = '{citoyen.CIN}';";
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
                        bool resulat = row[2].ToString().Equals("1");
                        citoyen.Tests.Add(new Test(row[0].ToString(),resulat,DateTime.Parse(row[1].ToString())));
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