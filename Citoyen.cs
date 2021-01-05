// File:    Citoyen.cs
// Author:  ABDERRAZZAQ LAANOUI
// Created: 03 December 2020 00:42:21
// Purpose: Definition of Class Citoyen

using Covid19Track.DataManipulation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Timers;

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
        public static List<Citoyen> citoyens = new List<Citoyen>();
        /* --------- Attributes -------------- */
        public string CIN { get; }
        public string nom;
        public String prenom;
        public DateTime dateDeNaissance;
        private Etats etat;
        private byte dosesInjectee;

        /* --------- GETTERS/SETTERS -------------- */
        public Etats Etat
        {
            get => etat;
            set
            {
                etat = value;
                CitoyenDAO.Update(this);

            }
        }
        public byte DosesInjectee
        {
            get => dosesInjectee;
            set
            {
                dosesInjectee = value;
                CitoyenDAO.Update(this);
            }
        }

        /* --------- Journal De Citoyen  -------------- */
        public List<Test> Tests { get; set; }
        public List<Record> Records { get; set; }
        public List<Isolation> Isolations { get; set; }
        public List<Rencontre> Rencontres { get; set; }


        /* ---------  Constructor -------------- */
        public Citoyen(string cin, string nom, string prenom, string dateDeNaissance)
        {
            this.CIN = cin.ToUpper();
            this.nom = nom.ToUpper();
            this.prenom = prenom.ToUpper();
            this.dateDeNaissance = DateTime.ParseExact(dateDeNaissance, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            this.etat = Etats.Inconnu;
            dosesInjectee = 0;

            Isolations = new List<Isolation>();
            RencontreDAO.FindAll(this);
            RecordDAO.FindAll(this);
            TestDAO.FindAll(this);
            //Ajouter a la liste:
            citoyens.Add(this);

            if (!CitoyenDAO.Excist(cin))
            {
                CitoyenDAO.Create(this);
            }

        }
        public Citoyen(string cin, string nom, string prenom, string dateDeNaissance, Etats etat)
        {
            this.CIN = cin.ToUpper();
            this.nom = nom.ToUpper();
            this.prenom = prenom.ToUpper();
            this.dateDeNaissance = DateTime.ParseExact(dateDeNaissance, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            this.etat = etat;
            dosesInjectee = 0;
            Isolations = new List<Isolation>();
            RencontreDAO.FindAll(this);
            RecordDAO.FindAll(this);
            TestDAO.FindAll(this);

            //Ajouter a la liste:
            citoyens.Add(this);

            if (!CitoyenDAO.Excist(cin))
            {
                CitoyenDAO.Create(this);
            }
        }
        public Citoyen(string cin, string nom, string prenom, string dateDeNaissance, Etats etat, byte doses)
        {
            this.CIN = cin.ToUpper();
            this.nom = nom.ToUpper();
            this.prenom = prenom.ToUpper();
            this.dateDeNaissance = DateTime.ParseExact(dateDeNaissance, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            this.etat = etat;
            dosesInjectee = doses;


            Isolations = new List<Isolation>();
            RencontreDAO.FindAll(this);
            RecordDAO.FindAll(this);
            TestDAO.FindAll(this);
            //Ajouter a la liste:
            citoyens.Add(this);

            if (!CitoyenDAO.Excist(cin))
            {
                CitoyenDAO.Create(this);
            }

        }

        /* ---------  Methodes -------------- */
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
            if (Etat == Etats.Soupconne)
            {
                const double interval10j = 10 * 24 * 60 * 60 * 1000; // milliseconds => 10j

                Timer checkForTime = new Timer(interval10j);
                checkForTime.Elapsed += new ElapsedEventHandler(EndConfinement);
                checkForTime.Enabled = true;
            }
        }

        private void EndConfinement(object sender, ElapsedEventArgs e)
        {
            MinistereDeLaSante.ChangerEtatCitoyen((Citoyen)sender, Etats.Saint);
            Isolation.AddConfinement(this, e.SignalTime, DateTime.Now);
        }

        //les operations a effectuer si un citoyen infecté rencontre un autre citoyen
        public void Contacter(Citoyen citoyen)
        {
            Rencontre.AddRencontre(this, citoyen);
        }

        public List<Etats> GetEtat(DateTime date)
        {
            return Records.Where(r => r.date.Equals(date)).Select(r => r.etat).ToList();
        }

        //return a peer key, value =>  date, etat
        public Dictionary<DateTime, Etats> GetEtats(DateTime dateDebut, DateTime dateFin)
        {
            return Records.Where(r => r.date >= dateDebut && r.date <= dateFin).ToDictionary(r => r.date, r => r.etat);
        }

        public List<Rencontre> GetRencontres(DateTime date)
        {
            return Rencontres.Where(r => r.date.Equals(date)).ToList();
        }

        public List<Rencontre> GetRencontres(DateTime dateDebut, DateTime dateFin)
        {
            return Rencontres.Where(r => r.date >= dateDebut && r.date <= dateFin).ToList();
        }
    }
}