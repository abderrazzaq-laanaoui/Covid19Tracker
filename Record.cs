// File:    Record.cs
// Author:  ABDERRAZZAQ LAANOUI
// Created: 03 December 2020 02:34:33
// Purpose: Definition of Class Record

using System;

namespace Covid19Track
{
    public class Record
    {
        public DateTime date { get; }

        public Etats etat { get; }

        public Record(DateTime date, Etats etat)
        {
            this.date = date;
            this.etat = etat;
        }

        internal static void AddRecord(Citoyen citoyen, DateTime date)
        {
            citoyen.Records.Add(new Record(date, citoyen.Etat));
            RecordDAO.Create(citoyen.CIN,citoyen.Etat,date);


        }

    }
}
