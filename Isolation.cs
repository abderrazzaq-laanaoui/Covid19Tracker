// File:    Confinement.cs
// Author:  ABDERRAZZAQ LAANOUI
// Created: 31 December 2020 03:53:52
// Purpose: Definition of Class Confinement

using System;

namespace Covid19Track
{
    public class Isolation
    {
        private DateTime dateDebut;
        private DateTime dateFin;

        public Isolation(DateTime dateDebut, DateTime dateFin)
        {
            this.dateDebut = dateDebut;
            this.dateFin = dateFin;
        }
        public static void AddConfinement(Citoyen citoyen, DateTime DateDebut, DateTime DateFin)
        {
            citoyen.Isolations.Add(new Isolation(DateDebut, DateFin));
            IsolationDAO.Create(citoyen.CIN, DateDebut, DateFin);

        }
    }
}