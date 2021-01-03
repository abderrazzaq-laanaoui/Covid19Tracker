// File:    Test.cs
// Author:  ABDERRAZZAQ LAANOUI
// Created: 31 December 2020 04:14:37
// Purpose: Definition of Class Test

using System;

namespace Covid19Track
{
    public class Test
    {
        private DateTime date;
        private bool resultat;
        private string laboratoire;

        public Test(String labo, bool resultat)
        {
            laboratoire = labo;
            this.resultat = resultat;
            this.date = DateTime.Now;
        }
    }

}