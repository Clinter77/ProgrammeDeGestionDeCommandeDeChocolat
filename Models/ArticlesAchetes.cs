using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ArticlesAchetes
    {
        public void Test()
        {
            Console.WriteLine("J'entre dans la classe ArticlesAchetes de Models !");
        }

        public Guid IdAcheteur { get; set; }
        public Guid IdChocolat { get; set; }
        public int Quantite { get; set; }
        public DateTime DateAchat { get; set; }

        public ArticlesAchetes(Guid BuyerId, Guid ChocolateId, int Qte, DateTime DateAchat)
        {
            IdAcheteur = BuyerId;
            IdChocolat = ChocolateId;
            Quantite = Qte;
            DateAchat = DateTime.Now;
        }
    }
}

