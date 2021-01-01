// File:    CentreDeVaccination.cs
// Author:  ABDERRAZZAQ LAANOUI
// Created: 03 December 2020 13:36:59
// Purpose: Definition of Class CentreDeVaccination

using System;

namespace Covid19Track
{
    public class CentreDeVaccination : Etablissement
    {
        public override string ToString()
        {
            throw new NotImplementedException();
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