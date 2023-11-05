using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Administrateurs
    {
        public void Test()
        {
            Console.WriteLine("J'entre dans la classe Administrateurs de Models !");
        }

        public Guid Id
        {
            get; set;
        }
        public string Login
        {
            get; set;
        }
        public string Password
        {
            get; set;
        }

        public Administrateurs() { }
        public Administrateurs(string Lgn, string Pswd)
        {
            Id = Guid.NewGuid();
            Login = Lgn;
            Password = Pswd;
        }
        
        public Administrateurs(Guid Identifiant, string Lgn, string Pswd)
        {
            Id = Identifiant;
            Login = Lgn;
            Password = Pswd;
        }
        

    }
}
