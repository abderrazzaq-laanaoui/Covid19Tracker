// File:    Citoyen.cs
// Author:  ABDERRAZZAQ LAANOUI
// Created: 03 December 2020 02:34:33
// Purpose: Definition of Class Record


using System;
using System.Collections.Generic;

namespace Covid19Track
{
    public class Rencontre
    {

        public Citoyen citoyen { get; }
        public DateTime date { get; }

        private Rencontre( Citoyen citoyen)
        {
            this.citoyen = citoyen;
            date = DateTime.Now;
        }
        private Rencontre(Citoyen citoyen, DateTime date)
        {
            this.citoyen = citoyen;
            this.date = date;
        }
        
        public static void AddRencontre(Citoyen c1, Citoyen c2)
        {
            c1.Rencontres.Add(new Rencontre(c2));
            c2.Rencontres.Add(new Rencontre(c1));
        }
        public static void AddRencontre(Citoyen c1, Citoyen c2, DateTime date)
        {
            c1.Rencontres.Add(new Rencontre(c2, date));
            c2.Rencontres.Add(new Rencontre(c1, date));
        }
    }
}
