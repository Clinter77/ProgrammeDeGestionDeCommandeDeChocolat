using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Users
    {
        // string nom, string prenom, string adresse, string telephone
        public void Test()
        {
            Console.WriteLine("J'entre dans la classe Users de Models !");
        }

        public string Nom
        {
            get; set;
        }
        public string Prenom
        {
            get; set;
        }
        public string Adresse
        {
            get; set;
        }
        public string Telephone
        {
            get; set;
        }

        public Users() { }
        public Users(string n, string p, string a, string t)
        {
            Nom = n;
            Prenom = p;
            Adresse = a;
            Telephone = t;
        }
    }
}

