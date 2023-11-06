using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace ServicesFichiersInteractions
{
    public class FileReader
    {
        public static bool isAuthenticationSucced = false;
        FileStream aFile;

        // sur mon poste chez-moi
        // static string filePathArticles = @"F:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat\Articles.json"; // path to file
        // static string filePathAdmins = @"F:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat\ComptesAdmins.json";

        // sur le PC Mewo
        static string filePathArticles = @"C:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat\Articles.json";
        static string filePathAdmins = @"C:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat\ComptesAdmins.json";

        /// <summary>
        /// Lecture du (ou des) compte(s) Administrateurs en BDD (fichier JSON)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // avant : public static void ReadTableaudArticles(List<Articles> arrayOfArticles)
        public static void ReadTableaudArticles(List<Articles> arrayOfArticles)
        {
            try
            {
                string jsonArrayOfArticles = arrayOfArticles.ToString();
                JsonSerializer serializer = new JsonSerializer();
                using (StreamWriter sw = new StreamWriter(filePathArticles))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, arrayOfArticles);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("e\t" + e.Message);
            }
        }

        // avant : public static List<Articles> LoadArticlesFromJson(string filePathArticles)
        public static List<Articles> LoadArticlesFromJson()
        {
            List<Articles> articles = new List<Articles>();

            // Vérifier si le fichier existe
            if (File.Exists(filePathArticles))
            {
                string json = File.ReadAllText(filePathArticles);

                // Vérifier si le fichier n'est pas vide
                if (!string.IsNullOrWhiteSpace(json))
                {
                    articles = JsonConvert.DeserializeObject<List<Articles>>(json);
                }
            }
            return articles;
        }

        public static List<Administrateurs> LoadAdminsFromJson()
        {
            Console.WriteLine("méthode LoadAdminsFromJson() de FileReader");
            List<Administrateurs> admins = new List<Administrateurs>();

            if (File.Exists(filePathAdmins))
            {
                Console.WriteLine("fichier connu");
                Console.Read();
                string json = File.ReadAllText(filePathAdmins);
                return JsonConvert.DeserializeObject<List<Administrateurs>>(json);
            }
            else
            {
                Console.WriteLine($"Le fichier {filePathAdmins} n'existe pas. Retourne une liste vide.");
                return new List<Administrateurs>();
            }
        }


        public static bool AuthenticateAdmin(List<Administrateurs> admins, string login, string password)
        {
            Console.WriteLine("méthode AuthenticateAdmin()");
            Console.Read();
            // Je vérifie si les informations d'authentification correspondent à un administrateur présent dans la liste
            return admins.Exists(a => a.Login == login && a.Password == password);
        }

        public static bool GetIsAuthenticationSucced()
        {
            return isAuthenticationSucced;
        }

        public static bool checkConnexionAdmin(List<Administrateurs> adminstrateurs, string loginConnexion, string passwordConnexion)
        {
            foreach (Administrateurs admin in adminstrateurs)
            {
                if (loginConnexion == admin.Login)
                {
                    Console.WriteLine("Le login renseigné est correct :)");
                    Console.Read();
                    if (passwordConnexion == admin.Password)
                    {
                        Console.WriteLine("Le password renseigné est correct :)");
                        Console.Read();
                        isAuthenticationSucced = true;
                    }
                    else
                    {
                        Console.WriteLine("le password renseigné ne correspond pas :(");
                        Console.Read();
                    }
                }
                else
                {
                    Console.WriteLine("le login renseigné est inconnu :(");
                    Console.Read();
                }
            }
            return isAuthenticationSucced;
        }
    }
}

