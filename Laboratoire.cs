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

        protected override void EnvoyerDonnees(Citoyen citoyen, bool resultat)
        {
            throw new NotImplementedException();
        }

    }
}