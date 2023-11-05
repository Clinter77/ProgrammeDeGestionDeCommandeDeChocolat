using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Models;
// using System.Text.Json;
// using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace ServicesLogs
{
    public class ClassNLogJournalisation
    {
        // public static Logger logger = LogManager.GetCurrentClassLogger();
        FileStream aFile;
        
        // sur mon poste chez-moi
        static string filePath = @"F:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat\ServicesLogs\logsFile.txt"; // path to file
        static string filePathAdminsComptes = @"F:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat\ServicesLogs\comptesAdmins.json";
        static string filePathUsersComptes = @"F:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat\ServicesLogs\comptesUsers.json";

        // sur le PC Mewo
        // static string filePath = @"C:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat\ServicesLogs\logsFile.txt"; // path to file
        // static string filePathAdminsComptes = @"C:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat\ServicesLogs\comptesAdmins.json";
        // static string filePathUsersComptes = @"C:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat\ServicesLogs\comptesUsers.json";

        /// <summary>
        /// Méthode pour journaliser les différentes étapes dans dans le fichier logsFile.txt
        /// </summary>
        /// <param name="arrayOfEtape" type="array of string"></param>
        /// <returns></returns>
        public static void LogToJournalFile(string arrayOfEtape)
        {
            /* 
            logger.Trace("Sample trace message");
            logger.Debug("Sample debug message");
            logger.Info("Sample informational message");
            logger.Warn("Sample warning message");
            logger.Error("Sample error message");
            logger.Fatal("Sample fatal error message");
             */
            using (FileStream aFile = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(aFile))
            {
                sw.WriteLine();
                sw.WriteLine("******************************************************************************************************************");
                sw.WriteLine(arrayOfEtape + " - " + DateTime.Now);
                sw.WriteLine("******************************************************************************************************************");
                sw.WriteLine();
            }
        }

        /// <summary>
        /// Méthode pour journaliser l'ajout des différents articles dans logsFile.txt
        /// </summary>
        /// <param name="tableaudArticles" type="list of Articles"></param>
        /// <returns></returns>
        public static void LogArticlesContentsToJournalFile(List<Articles> tableaudArticles)
        {
            using (FileStream aFile = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(aFile))
            {
                foreach (var article in tableaudArticles)
                {
                    sw.WriteLine($"ajout de {article.Quantite} unités pour l'article {article.Reference}, au prix unitaire de {article.Prix}");
                }
                sw.WriteLine();
                sw.WriteLine("******************************************************************************************************************");
            }
        }

        /// <summary>
        /// Méthode pour journaliser l'ajout d'Administrateur(s) dans logsFile.txt
        /// </summary>
        /// <param name="tableaudAdministrateurs" type="list of Administrateurs"></param>
        /// <returns>void - rien</returns>
        // avant :
        /* 
        public static void LogAdminstrateursToJournalFile(List<Administrateurs> tableaudAdministrateurs)
        {
            using (FileStream aFile = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(aFile))
            {
                foreach (var administrateur in tableaudAdministrateurs)
                {
                    sw.WriteLine($"ajout de l'administrateur ayant pour login {administrateur.Login} et pour password {administrateur.Password}");
                }
                sw.WriteLine("******************************************************************************************************************");
            }
        }
        */

        /// <summary>
        /// Méthode pour journaliser l'ajout d'Administrateur(s) dans logsFile.txt
        /// </summary>
        /// <param name="administrateur" type="Administrateurs"></param>
        /// <returns>void - rien</returns>
        public static void LogAdminstrateursToJournalFile(Administrateurs administrateur)
        {
            using (FileStream aFile = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(aFile))
            {
                sw.WriteLine($"ajout de l'administrateur ayant pour login {administrateur.Login} et pour password {administrateur.Password}");
                sw.WriteLine("******************************************************************************************************************");
            }
        }

        /// <summary>
        /// Méthode pour journaliser l'ajout d'Utilisateur(s) dans logsFile.txt
        /// </summary>
        /// <param name="user" type="Users"></param>
        /// <returns>void - rien</returns>
        public static void LogUtilisateursToJournalFile(Users user)
        {
            using (FileStream aFile = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(aFile))
            {
                sw.WriteLine($"ajout de l'utilisateur ayant pour nom {user.Nom} prénom {user.Prenom} adresse {user.Adresse} et numéro de téléphone {user.Telephone}");
                sw.WriteLine("******************************************************************************************************************");
            }
        }

        /// <summary>
        /// Méthode pour journaliser l'ajout d'Administrateur(s) dans comptesAdmins.json
        /// </summary>
        /// <param name="arrayOfAdmins" type="list of Administrateurs"></param>
        /// <returns></returns>
        public static void LogAdminstrateursToComptesAdminFile(List<Administrateurs> arrayOfAdmins)
        {
            try
            {
                string jsonArrayOfAdmins = arrayOfAdmins.ToString();
                JsonSerializer serializer = new JsonSerializer();
                using (StreamWriter sw = new StreamWriter(filePathAdminsComptes))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, arrayOfAdmins);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("e\t" + e.Message);
            }
        }

        /// <summary>
        /// Méthode pour journaliser l'ajout d'Utilisateur(s) dans comptesUsers.json
        /// </summary>
        /// <param name="user" type="Users"></param>
        /// <returns></returns>
        public static void LogUsersToComptesUserFile(Users user)
        {
            try
            {
                string json = user.ToString();
                JsonSerializer serializer = new JsonSerializer();
                using (StreamWriter sw = new StreamWriter(filePathUsersComptes))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, user);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("e\t" + e.Message);
            }
        }
    }
}
