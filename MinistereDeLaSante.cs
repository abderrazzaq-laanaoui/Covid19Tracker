// File:    MinistereDeLaSante.cs
// Author:  ABDERRAZZAQ LAANOUI
// Created: 03 December 2020 00:43:24
// Purpose: Definition of Class MinistereDeLaSante


using System.Linq;
using System;
using System.Collections.Generic;

namespace Covid19Track
{
    public static class MinistereDeLaSante
    {
        public static void ChangerEtatCitoyen(Citoyen citoyen, Etats etat)
        {
            citoyen.Etat = etat;
            Record.AddRecord(citoyen, DateTime.Now);

            if (etat == Etats.Infecte)
            {
                checkContact(citoyen);
            }

        }

        private static void checkContact(Citoyen citoyen)
        {
            // Quand un citoyen devient infecté tout les aurtes citoyens saint ou d'etat inconnu qu'il a contacter durant la derniere semaine devient Soupçonné.
            foreach (Rencontre rencontre in citoyen.Rencontres.Where(r => r.date >= DateTime.Now.AddDays(-7)))                                                 
            {
                var cit = Citoyen.citoyens.Find(c => c.CIN.Equals(rencontre.citoyen));
                if(cit.Etat == Etats.Inconnu || cit.Etat == Etats.Saint)
                    cit.Etat = Etats.Soupconne;
            }
        }
    }
}