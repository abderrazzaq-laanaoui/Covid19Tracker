using System;
using System.Collections.Generic;

namespace Covid19Track
{
    public class Rencontre
    {
        public Citoyen c1 { get; }
        public Citoyen c2 { get; }
        public DateTime date { get; }

        public Rencontre(Citoyen citoyen1, Citoyen citoyen2)
        {
            c1 = citoyen1;
            c2 = citoyen2;
            date = DateTime.Now;

            citoyen2.Rencontres.Add(this);
        }

    }
}
