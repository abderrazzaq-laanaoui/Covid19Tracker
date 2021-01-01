// File:    Citoyen.cs
// Author:  ABDERRAZZAQ LAANOUI
// Created: 03 December 2020 00:42:21
// Purpose: Definition of Class Citoyen

using System;
using System.Collections.Generic;

namespace Covid19Track
{

    public enum Etats
    {
        Inconnu = 0,
        Saint,
        Soupconne,
        Infecte,
        Vaccine,
        Deces
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


        //return l'etat d'un citoyen sous forme d'une chaine des caracteres
        public int ConsultationEtat()
        {
            throw new NotImplementedException();
        }

        //les operations a effectuer si un citoyen infecté rencotre un autre citoyen
        public void SeConfiner()
        {
            throw new NotImplementedException();

        }


        //les operations a effectuer si un citoyen infecté rencotre un autre citoyen
        public void Rencontrer(Citoyen citoyen)
        {
            throw new NotImplementedException();
        }
    }
}