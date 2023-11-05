using ServicesListes;
using Models;
using ProgrammeDeGestionDeCommandeDeChocolatCore;
using System;
using ServicesLogs;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices;
using ServicesFichiersInteractions;
using System.Collections.Generic;

namespace ProgrammeDeGestionDeCommandeDeChocolat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* 
            ServicesListes.Class1 objectServicesListes = new ServicesListes.Class1();
            Models.Class1 objectModels = new Models.Class1();
            objectServicesListes.Test();
            objectModels.Test();

            ServicesFichiersInteractions.Class1 objectServicesFichiersInteractions = new ServicesFichiersInteractions.Class1();
            objectServicesFichiersInteractions.Test();

            ProgrammeDeGestionDeCommandeDeChocolatCore.Class1 objectProgrammeDeGestionDeCommandeDeChocolatCore = new ProgrammeDeGestionDeCommandeDeChocolatCore.Class1();
            objectProgrammeDeGestionDeCommandeDeChocolatCore.Test();
             */

            // sur mon poste chez-moi
            string filePathArticles = @"F:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat\Articles.json"; // path to file
            // sur le PC Mewo
            // static string filePathArticles = @"C:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat\Articles.json"; // path to file

            string[] arrayOfEtape = new string[] {
                "Etape de démarrage de l'application",
                "Etape d'initialisation des articles en base de données (ajout des articles) dans le fichier",
                "Etape de choix de profil de compte",
                "Etape d'ajout de compte Administrateur",
                "Etape d'ajout de compte Utilisateur",
                "Etape de connexion",
                "Etape de fin de connexion d'Administrateur",
                "Etape d'ajout de compte Acheteur",
                "Etape de connexion d'Acheteur",
                "Etape de fin de connexion d'Acheteur",
                "Etape d'ajout d'article(s) à la commande en cours (articles achetés)",
                "Etape de modification de la commande en cours (articles achetés)",
                "Etape de visualisation de la commande en cours (articles achetés)",
                "Etape de fermeture de la visualisation de la commande en cours (articles achetés)"
            };

            Console.WriteLine(arrayOfEtape[0] + " - " + DateTime.Now);
            Console.WriteLine(arrayOfEtape[1] + " - " + DateTime.Now);
            Console.Read();

            ServicesLogs.ClassNLogJournalisation.LogToJournalFile(arrayOfEtape[0]);
            ServicesLogs.ClassNLogJournalisation.LogToJournalFile(arrayOfEtape[1]);
            ProgrammeDeGestionDeCommandeDeChocolatCore.ProgrammeCore.initialiserLesArticles();

            ServicesFichiersInteractions.FileReader.LoadArticlesFromJson(filePathArticles);
            List<Articles> articles = new List<Articles>();
            foreach (Articles article in articles)
            {
                Console.WriteLine($"Id: {article.Id}, Référence: {article.Reference}, Prix: {article.Prix}, Quantité: {article.Quantite}");
            }
            Console.Read();

            Console.WriteLine(arrayOfEtape[2] + " - " + DateTime.Now);
            Console.Read();
            ServicesLogs.ClassNLogJournalisation.LogToJournalFile(arrayOfEtape[2]);
            ProgrammeDeGestionDeCommandeDeChocolatCore.ProgrammeCore.askAccountType();
            Console.Read();

            Console.WriteLine(arrayOfEtape[5] + " - " + DateTime.Now);
            Console.Read();
            ServicesLogs.ClassNLogJournalisation.LogToJournalFile(arrayOfEtape[5]);
            ProgrammeDeGestionDeCommandeDeChocolatCore.ProgrammeCore.connexion();
            Console.Read();



        }
    }
}
