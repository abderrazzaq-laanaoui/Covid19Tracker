// File:    MinistereDeLaSante.cs
// Author:  ABDERRAZZAQ LAANOUI
// Created: 03 December 2020 00:43:24
// Purpose: Definition of Class MinistereDeLaSante

using System;

namespace Covid19Track { 
public static class MinistereDeLaSante
{
   public static void ChangerEtatCitoyen(Citoyen citoyen, Etats etat )
   {
            citoyen.Etat = etat;
   }

}}