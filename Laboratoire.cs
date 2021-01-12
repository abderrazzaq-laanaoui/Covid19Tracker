// File:    Laboratoire.cs
// Author:  ABDERRAZZAQ LAANOUI
// Created: 03 December 2020 02:34:31
// Purpose: Definition of Class Laboratoire

using System;
using System.Collections.Generic;

namespace Covid19Track
{
    public class Laboratoire : Etablissement
    {
        public static List<Laboratoire> laboratoires { get; set; }

        public string nom
        {
            get => _nom;
            set
            {
                _nom = value;
                EtablisementDAO.Update(this, 1);
            }
        } 
        public string reference
        {
            get => _reference;
        }

        public Laboratoire(string reference, string nom)
        {
            this._nom = nom;
            this._reference = reference;

            if (!EtablisementDAO.Excist(reference,1))
            {
                EtablisementDAO.Create(this,1);
            }
        }

        public override string ToString()
        {
            return "Laboratoire " + nom + " est sous le reference: " + reference;
        }

        public bool TestPCR(Citoyen citoyen)
        {
            Random random = new Random();
            var tmp = random.NextDouble();
            bool resultat;
            if (citoyen.Etat == Etats.Vaccine)
                //Probalité de positivité : 0%
                resultat = tmp >= 0;
            if (citoyen.Etat == Etats.Saint)
                //Probalité de positivité : 10%
                resultat = tmp >= 0.9;
            if (citoyen.Etat == Etats.Soupconne)
                //Probalité de positivité : 30%
                resultat = tmp >= 0.7;
            if (citoyen.Etat == Etats.Infecte)
                //Probalité de positivité : 70%
                resultat = tmp >= 0.3;
            else
                //Probalité de positivité : 50%
                resultat = tmp >= 0.5;

            EnvoyerDonnees(citoyen, resultat);
            //Sauvgarder le test
            Test.AddTest(citoyen,this.reference,resultat);
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
                if (citoyen.Etat != Etats.Vaccine) 
                    MinistereDeLaSante.ChangerEtatCitoyen(citoyen, Etats.Saint);


            }

        }
    }
}