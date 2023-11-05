using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Acheteurs
    {
        public void Test()
        {
            Console.WriteLine("J'entre dans la classe Acheteurs de Models !");
        }

        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public int Telephone { get; set; }

        public Acheteurs(Guid Id, string Name, string Firstname, string Address, int PhoneNumber)
        {
            Id = Guid.NewGuid();
            Nom = Name;
            Prenom = Firstname;
            Adresse = Address;
            Telephone = PhoneNumber;
        }
    }
}

