// File:    Laboratoire.cs
// Author:  ABDERRAZZAQ LAANOUI
// Created: 03 December 2020 02:34:31
// Purpose: Definition of Class Laboratoire

using System;

namespace Covid19Track
{
    public class Laboratoire : Etablissement
    {
        public Laboratoire(string reference, string nom)
        {
            this.nom = nom;
            this.reference = reference;
        }
        
        public override string ToString()
        {
            return "Laboratoire " + nom + " est sous le reference: " + reference;
        }

        public bool TestPCR(Citoyen citoyen)
        {
            Random random = new Random();
            var tmp = random.NextDouble();
            //Taux de positivité : 10%
            bool resultat = tmp >= 0.9;
            EnvoyerDonnees(citoyen, resultat);
            //Sauvgarder le test
            citoyen.Tests.Add(new Test(this.reference, resultat));
            return resultat;
        }

        protected override void EnvoyerDonnees(Citoyen citoyen, bool resultat)
        {
            if (resultat)
            {
                MinistereDeLaSante.ChangerEtatCitoyen(citoyen, Etats.Infecte);
            }
            else
            {
                MinistereDeLaSante.ChangerEtatCitoyen(citoyen, Etats.Saint);
            }

        }
    }
}