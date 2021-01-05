// File:    Citoyen.cs
// Author:  ABDERRAZZAQ LAANOUI
// Created: 03 December 2020 02:34:33
// Purpose: Definition of Class Record


using Covid19Track.DataManipulation;
using System;
using System.Collections.Generic;

namespace Covid19Track
{
    public class Rencontre
    {

        public string citoyen { get; }
        public DateTime date { get; }

        private Rencontre( string citoyen)
        {
            this.citoyen = citoyen;
            date = DateTime.Now;
        }
        public Rencontre(string citoyen, DateTime date)
        {
            this.citoyen = citoyen;
            this.date = date;
        }
        
        public static void AddRencontre(Citoyen c1, Citoyen c2)
        {
            c1.Rencontres.Add(new Rencontre(c2.CIN));
            c2.Rencontres.Add(new Rencontre(c1.CIN));

            //Enregestrer dans un DB les rencontres d'une semaines
            RencontreDAO.Create(c1.CIN, c2.CIN, DateTime.Now);
            
        }
        public static void AddRencontre(Citoyen c1, Citoyen c2, DateTime date)
        {
            c1.Rencontres.Add(new Rencontre(c2.CIN, date));
            c2.Rencontres.Add(new Rencontre(c1.CIN, date));

            //Enregestrer dans un DB les rencontres d'une semaines
            RencontreDAO.Create(c1.CIN, c2.CIN, date);

        }
    }
}
