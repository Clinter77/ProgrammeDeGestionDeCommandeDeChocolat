﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Articles
    {
        public void Test()
        {
            Console.WriteLine("J'entre dans la classe Articles de Models !");
        }

        public Guid Id { get; set; }
        public string Reference { get; set; }
        public float Prix { get; set; }
        public int Quantite { get; set; }

        public Articles(string Ref, float Px, int Qte)
        {
            Id = Guid.NewGuid();
            Reference = Ref;
            Prix = Px;
            Quantite = Qte;
        }
    }
}
