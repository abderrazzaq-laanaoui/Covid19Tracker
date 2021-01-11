// File:    CentreDeVaccination.cs
// Author:  ABDERRAZZAQ LAANOUI
// Created: 03 December 2020 13:36:59
// Purpose: Definition of Class CentreDeVaccination

using System.Collections.Generic;

namespace Covid19Track
{
    // date de vaccination est calculable (DURÉE DE PROTECTION ) => Last record vaccinee
    public class CentreDeVaccination : Etablissement
    {
        public static List<CentreDeVaccination> centres { get;  set; }

        public string nom
        {
            get => _nom;
            set
            {
                _nom = value;
                EtablisementDAO.Update(this, 2);
            }
        }
        public string reference
        {
            get => _reference;
        }
        public CentreDeVaccination(string reference, string nom)
        {
            this._nom = nom;
            this._reference = reference;

            if (!EtablisementDAO.Excist(reference, 2))
            {
                EtablisementDAO.Create(this, 2);
            }
        }

        public override string ToString()
        {
            return "Centre de Vaccination " + nom + " est sous le reference: " + reference;
        }

        public void InjecteDose(Citoyen citoyen)
        {
            //si le citoyen a injecté 2 doses de vaccine il devient vaccinée
            bool res = (++citoyen.DosesInjectee >= 2);
            EnvoyerDonnees(citoyen, res);
        }

        protected override void EnvoyerDonnees(Citoyen citoyen, bool vacciner)
        {
            if (vacciner)
            {
                MinistereDeLaSante.ChangerEtatCitoyen(citoyen, Etats.Vaccine);
            }
        }
    }
}