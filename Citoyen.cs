// File:    Citoyen.cs
// Author:  ABDERRAZZAQ LAANOUI
// Created: 03 December 2020 00:42:21
// Purpose: Definition of Class Citoyen

using System;
using System.Collections.Generic;
using System.Globalization;

namespace Covid19Track
{

    public enum Etats
    {
        Inconnu = 0,
        Saint,
        Soupconne,
        Infecte,
        Vaccine,
        Decede
    }

    public class Citoyen
    {
        //attributes
        public string CIN { get; }
        private string nom;
        private String prenom;
        private DateTime dateDeNaissance;
        public Etats Etat { get; set; }
        public byte DosesInjectee { get; set; }
        //-----//
        private List<Test> Tests;
        private List<Record> Records;

        //ctor
        public Citoyen(string cin, string nom, string prenom, string dateDeNaissance)
        {
            this.CIN = cin;
            this.nom = nom;
            this.prenom = prenom;
            this.dateDeNaissance = DateTime.ParseExact(dateDeNaissance, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            this.Etat = Etats.Inconnu;
            DosesInjectee = 0;
        }

        public Citoyen(string cin, string nom, string prenom, string dateDeNaissance, Etats etat)
        {
            this.CIN = cin;
            this.nom = nom;
            this.prenom = prenom;
            this.dateDeNaissance = DateTime.ParseExact(dateDeNaissance, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            this.Etat = etat;
        }

        //return l'etat d'un citoyen sous forme d'une chaine des caracteres
        public string ConsultationEtat()
        {
            switch (Etat)
            {
                case Etats.Inconnu:
                    return "Citoyen " + prenom + " " + nom + " avec CIN: " + CIN + " est d'etat Inconnu";
                case Etats.Saint:
                    return "Citoyen " + prenom + " " + nom + " avec CIN: " + CIN + "  est Saint";
                case Etats.Soupconne:
                    return "Citoyen " + prenom + " " + nom + " avec CIN: " + CIN + " est Soup�onn� d'etre inferct�";
                case Etats.Infecte:
                    return "Citoyen " + prenom + " " + nom + " avec CIN: " + CIN + " est Infect�";
                case Etats.Vaccine:
                    return "Citoyen " + prenom + " " + nom + " avec CIN: " + CIN + " est vaccin�";
                case Etats.Decede:
                    return "Citoyen " + prenom + " " + nom + " avec CIN: " + CIN + " est D�c�d�";
                default:
                    return null;
            }
        }

        //les operations a effectuer si un citoyen infect� rencotre un autre citoyen
        public void SeConfiner()
        {
            throw new NotImplementedException();

        }


        //les operations a effectuer si un citoyen infect� rencotre un autre citoyen
        public void Rencontrer(Citoyen citoyen)
        {
            throw new NotImplementedException();
        }
    }
}