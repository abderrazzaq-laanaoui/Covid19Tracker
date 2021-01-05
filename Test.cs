// File:    Test.cs
// Author:  ABDERRAZZAQ LAANOUI
// Created: 31 December 2020 04:14:37
// Purpose: Definition of Class Test

using System;

namespace Covid19Track
{
    public class Test
    {
        public DateTime date { get; }
        public bool resultat { get; }
        public string laboratoire { get; }

        public Test(String labo, bool resultat)
        {
            laboratoire = labo;
            this.resultat = resultat;
            this.date = DateTime.Now;

        }
        public Test(String labo, bool resultat, DateTime date)
        {
            laboratoire = labo;
            this.resultat = resultat;
            this.date = date;

        }

        internal static void AddTest(Citoyen citoyen, string laboratoire, bool resultat)
        {
            citoyen.Tests.Add(new Test(laboratoire, resultat));
            TestDAO.Create(citoyen.CIN, laboratoire, resultat, DateTime.Now);


        }
    }

}