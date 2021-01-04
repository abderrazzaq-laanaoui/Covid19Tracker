// File:    Etablissement.cs
// Author:  ABDERRAZZAQ LAANOUI
// Created: 03 December 2020 14:01:14
// Purpose: Definition of Class Etablissement

namespace Covid19Track
{
    public abstract class Etablissement
    {
        public string nom { get; set; }
        
        public string reference { get; set; }

        protected abstract void EnvoyerDonnees(Citoyen citoyen, bool resultat);
        
        public override abstract string ToString();
    }
}