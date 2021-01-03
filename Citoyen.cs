// File:    Citoyen.cs
// Author:  ABDERRAZZAQ LAANOUI
// Created: 03 December 2020 00:42:21
// Purpose: Definition of Class Citoyen

using System;
using System.Timers;
using System.Linq;
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
        public string nom;
        public String prenom;
        public DateTime dateDeNaissance;
        public Etats Etat { get; set; }
        public byte DosesInjectee { get; set; }
        // -------------------------------------------- //
        public List<Test> Tests;
        private List<Record> Records;
        private List<Isolation> Isolations;
        public List<Rencontre> Rencontres { get; set; }

        //ctor
        public Citoyen(string cin, string nom, string prenom, string dateDeNaissance)
        {
            this.CIN = cin;
            this.nom = nom;
            this.prenom = prenom;
            this.dateDeNaissance = DateTime.ParseExact(dateDeNaissance, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            this.Etat = Etats.Inconnu;
            DosesInjectee = 0;

            Tests = new List<Test>();
            Records = new List<Record>();
            Isolations = new List<Isolation>();
            Rencontres = new List<Rencontre>();
        }

        public Citoyen(string cin, string nom, string prenom, string dateDeNaissance, Etats etat)
        {
            this.CIN = cin;
            this.nom = nom;
            this.prenom = prenom;
            this.dateDeNaissance = DateTime.ParseExact(dateDeNaissance, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            this.Etat = etat;

            Tests = new List<Test>();
            Records = new List<Record>();
            Isolations = new List<Isolation>();
            Rencontres = new List<Rencontre>();
        }

        // Return l'etat d'un citoyen sous forme d'une chaine des caracteres
        public string ConsultationEtat()
        {
            switch (Etat)
            {
                case Etats.Inconnu:
                    return "Citoyen " + prenom + " " + nom + " avec CIN: " + CIN + " est d'etat Inconnu";
                case Etats.Saint:
                    return "Citoyen " + prenom + " " + nom + " avec CIN: " + CIN + "  est Saint";
                case Etats.Soupconne:
                    return "Citoyen " + prenom + " " + nom + " avec CIN: " + CIN + " est Soupçonné d'etre infercté";
                case Etats.Infecte:
                    return "Citoyen " + prenom + " " + nom + " avec CIN: " + CIN + " est Infecté";
                case Etats.Vaccine:
                    return "Citoyen " + prenom + " " + nom + " avec CIN: " + CIN + " est vacciné";
                case Etats.Decede:
                    return "Citoyen " + prenom + " " + nom + " avec CIN: " + CIN + " est Décédé";
                default:
                    return null;
            }
        }

        // Les operation a faire si un citoyen a été isolé
        public void SeConfiner()
        {
            if(Etat == Etats.Soupconne)
            {
                const double interval10j = 10 * 24 *  60 * 60 * 1000; // milliseconds => 10j

                Timer checkForTime = new Timer(interval10j);
                checkForTime.Elapsed += new ElapsedEventHandler(EndConfinement);
                checkForTime.Enabled = true;
            }
        }

        private void EndConfinement(object sender, ElapsedEventArgs e)
        {
            MinistereDeLaSante.ChangerEtatCitoyen((Citoyen)sender, Etats.Saint);
            this.Isolations.Add(new Isolation(e.SignalTime,DateTime.Now));
        }

        //les operations a effectuer si un citoyen infecté rencotre un autre citoyen
        //Enregestrer dans un DB les rencontres d'une semaines
        public void Contacter(Citoyen citoyen)
        {
            Rencontre.AddRencontre(this, citoyen);
        }

        public Etats GetEtat(DateTime date)
        {
            return Records.Last(r => r.date.Equals(date)).etat;
        }
        //return a peer key, value of 
        public Dictionary<DateTime,Etats> GetEtats(DateTime dateDebut, DateTime dateFin)
        {
             return Records.Where(r => r.date >= dateDebut && r.date <= dateFin).ToDictionary(r => r.date , r => r.etat);
        }
    }
}