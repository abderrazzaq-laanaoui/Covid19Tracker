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
    public enum Regions
    {
        Tangier_Tetouan_AlHociema,
        Oriental,
        Fez_Meknes,
        Rabat_Sale_Kenitra,
        BeniMellal_Khenifra,
        CasaBlanca_Settat,
        Marrakesh_Safi,
        Draa_Tafilalt,
        Sous_Massa,
        Guelmim_OuedNoun,
        Laayoune_SakiaElHamra,
        Dakhla_OuedEddahab
    }

    public class Citoyen
    {
        //attributes
        public string CIN { get; }
        public string nom;
        public String prenom;
        public DateTime dateDeNaissance;
        public Regions Region { get; }
        public Etats Etat { get; set; }
        public byte DosesInjectee { get; set; }
        // -------------------------------------------- //
        public List<Test> Tests;
        public List<Record> Records { get; set; }
        private List<Isolation> Isolations;
        public List<Rencontre> Rencontres { get; set; }

        //ctor
        public Citoyen(string cin, string nom, string prenom, string dateDeNaissance, Regions region)
        {
            this.CIN = cin;
            this.nom = nom;
            this.prenom = prenom;
            this.dateDeNaissance = DateTime.ParseExact(dateDeNaissance, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            this.Etat = Etats.Inconnu;
            DosesInjectee = 0;
            Region = region;

            Tests = new List<Test>();
            Records = new List<Record>();
            Isolations = new List<Isolation>();
            Rencontres = new List<Rencontre>();
        }

        public Citoyen(string cin, string nom, string prenom, string dateDeNaissance, Regions region, Etats etat)
        {
            this.CIN = cin;
            this.nom = nom;
            this.prenom = prenom;
            this.dateDeNaissance = DateTime.ParseExact(dateDeNaissance, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            this.Etat = etat;
            Region = region;


            Tests = new List<Test>();
            Records = new List<Record>();
            Isolations = new List<Isolation>();
            Rencontres = new List<Rencontre>();
        }
        public Citoyen(string cin, string nom, string prenom, string dateDeNaissance, Etats etat, byte doses, Regions region)
        {
            this.CIN = cin.ToUpper();
            this.nom = nom.ToUpper();
            this.prenom = prenom.ToUpper();
            this.dateDeNaissance = DateTime.ParseExact(dateDeNaissance, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            this.Etat = etat;
            DosesInjectee = doses;
            Region = region;


            Isolations = new List<Isolation>();
            Rencontres = new List<Rencontre>();
            Tests = new List<Test>();
            Records = new List<Record>();


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
        //return a peer key, value of etats in dates
        public Dictionary<DateTime,Etats> GetEtats(DateTime dateDebut, DateTime dateFin)
        {
             return Records.Where(r => r.date >= dateDebut && r.date <= dateFin).ToDictionary(r => r.date , r => r.etat);
        }

        //Rencontrer les rencontrer d'un date specific 
        public List<Rencontre> GetRencontres(DateTime date)
        {
            return Rencontres.Where(r => r.date.Equals(date)).ToList();
        }

        //Rencontrer les rencoontrer dans un interval 
        public List<Rencontre> GetRencontres(DateTime dateDebut, DateTime dateFin)
        {
            return Rencontres.Where(r => r.date >= dateDebut && r.date <= dateFin).ToList();
        }
    }
}