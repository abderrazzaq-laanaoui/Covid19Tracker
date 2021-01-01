// File:    Laboratoire.cs
// Author:  ABDERRAZZAQ LAANOUI
// Created: 03 December 2020 02:34:31
// Purpose: Definition of Class Laboratoire

using System;

namespace Covid19Track
{
    public class Laboratoire : Etablissement
    {
        public override string ToString()
        {
            throw new NotImplementedException();
        }

        protected override void EnvoyerDonnees(Citoyen citoyen, bool resultat)
        {
            throw new NotImplementedException();
        }


    }
}