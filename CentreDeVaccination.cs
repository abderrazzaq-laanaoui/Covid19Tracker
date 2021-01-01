// File:    CentreDeVaccination.cs
// Author:  ABDERRAZZAQ LAANOUI
// Created: 03 December 2020 13:36:59
// Purpose: Definition of Class CentreDeVaccination

using System;

namespace Covid19Track
{
    public class CentreDeVaccination : Etablissement
    {
        public CentreDeVaccination(string reference, string nom)
        {
            this.nom = nom;
            this.reference = reference;
        }
        
        public override string ToString()
        {
            return "Centre de Vaccination " + nom + " est sous le reference: " + reference;
        }

        public void InjecteDose(Citoyen citoyen)
        {
            throw new NotImplementedException();
        }

        protected override void EnvoyerDonnees(Citoyen citoyen, bool vacciner)
        {
            throw new NotImplementedException();
        }
    }
}