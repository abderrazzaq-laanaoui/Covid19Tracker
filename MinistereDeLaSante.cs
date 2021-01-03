// File:    MinistereDeLaSante.cs
// Author:  ABDERRAZZAQ LAANOUI
// Created: 03 December 2020 00:43:24
// Purpose: Definition of Class MinistereDeLaSante


using System.Linq;
using System;

namespace Covid19Track
{
    public static class MinistereDeLaSante
    {
        public static void ChangerEtatCitoyen(Citoyen citoyen, Etats etat)
        {
            citoyen.Etat = etat;
            Record record = new Record(citoyen, DateTime.Now, etat);

            if (etat == Etats.Infecte)
            {
                checkContact(citoyen);
            }
        }

        private static void checkContact(Citoyen citoyen)
        {
            // quand un citoyen devient infecté tout les aurtes citoyen saint ou d'etat inconnu qu'il a contacter durant la derniere semaine devient Soupçonné

            foreach (Rencontre rencontre in citoyen.Rencontres.Where(r => r.date >= DateTime.Now.AddDays(-7)))
            {
                if(rencontre.citoyen.Etat == Etats.Inconnu || rencontre.citoyen.Etat == Etats.Saint)
                {
                    rencontre.citoyen.Etat = Etats.Soupconne;
                }

            }
        }

    }
}