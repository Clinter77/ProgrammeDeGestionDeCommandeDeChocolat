using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace ServicesFichiersInteractions
{
    public class FileWriter
    {

        // sur mon poste chez-moi
        static string filePathArticles = @"F:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat\Articles.json"; // path to file
        static string filePathAdmins = @"F:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat\ComptesAdmins.json";
        static string filePathUsers = @"F:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat\ComptesUsers.json";

        // sur le PC Mewo
        // static string filePathArticles = @"C:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat\Articles.json"; // path to file
        // static string filePathAdmins = @"C:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat\ComptesAdmins.json";
        // static string filePathUsers = @"C:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat\ComptesUsers.json";


        /// <summary>
        /// Initialisation des articles en BDD (fichier JSON)
        /// </summary>
        /// <param name="arrayOfArticles"></param>
        /// <returns>void</returns>
        public static void CreateTableaudArticles(List<Articles> arrayOfArticles)
        {
            try
            {
                /* avant - 
                string jsonArrayOfArticles = arrayOfArticles.ToString();
                JsonSerializer serializer = new JsonSerializer();
                using (StreamWriter sw = new StreamWriter(filePathArticles))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, arrayOfArticles);
                }
                */
                string json = JsonConvert.SerializeObject(arrayOfArticles, Formatting.Indented);
                File.WriteAllText(filePathArticles, json);
                Console.WriteLine($"Les articles ont bien été sauvegardés-ajoutés dans le fichier {filePathArticles}.");
            }
            catch (Exception e)
            {
                Console.WriteLine("e\t" + e.Message);
            }
            
        }

        /// <summary>
        /// Ajout d'un aministrateur en BDD (fichier JSON)
        /// </summary>
        /// <param name="login, password"></param>
        /// <returns>void</returns>
        public static void CreateAdmin(string login, string password)
        {
            List<Administrateurs> admins = new List<Administrateurs>();

            if (LoadAdminsFromJson(filePathAdmins) != null)
            {
                // Charger les administrateurs existants depuis le fichier JSON
                admins = LoadAdminsFromJson(filePathAdmins);
            }
            
            Administrateurs administrateur = new Administrateurs(login, password);

            // Ajouter le nouvel administrateur à la liste
            admins.Add(administrateur);

        
            // Sauvegarder la liste mise à jour dans le fichier JSON
            SaveAdminsToJson(admins, filePathAdmins);
        
            if (admins.Count()>0)
            {
                // Afficher les administrateurs mis à jour
                /* Console.WriteLine("administrateurs mis à jour :");
                foreach (Administrateurs admin in admins)
                {
                    Console.WriteLine($"Id: {admin.Id}, Login: {admin.Login}, Password: {admin.Password}");
                } */
                // Afficher l'administrateur ajouté
                Console.WriteLine("l'administrateur ajouté :");
                Console.WriteLine($"Id: {administrateur.Id}, Login: {administrateur.Login}, Password: {administrateur.Password}");
            }
        }

        /// <summary>
        /// Ajout d'un utilisateur en BDD (fichier JSON)
        /// </summary>
        /// <param name="nom, prenom, adresse, telephone"></param>
        /// <returns>void</returns>
        public static void CreateUser(string nom, string prenom, string adresse, string telephone)
        {
            List<Users> users = new List<Users>();

            if (LoadUsersFromJson(filePathUsers) != null)
            {
                // Charger les utilisateurs existants depuis le fichier JSON
                users = LoadUsersFromJson(filePathUsers);
            }

            Users user = new Users(nom, prenom, adresse, telephone);

            // Ajouter le nouvel utilisateur à la liste
            users.Add(user);


            // Sauvegarder la liste mise à jour dans le fichier JSON
            SaveUsersToJson(users, filePathUsers);

            if (users.Count() > 0)
            {
                // Afficher les utilisateurs mis à jour
                /* Console.WriteLine("utilisateurs mis à jour :");
                foreach (Utilisateurs user in users)
                {
                    Console.WriteLine($"Nom: {user.Nom}, Prénom: {user.Prenom}, Adresse: {user.Adresse}, Téléphone: {user.Telephone}");
                } */
                // Afficher l'utilisateur ajouté
                Console.WriteLine("l'utilisateur ajouté :");
                Console.WriteLine($"Nom: {user.Nom}, Prénom: {user.Prenom}, Adresse: {user.Adresse}, Téléphone: {user.Telephone}");
                Console.Read();
            }
        }

        public static List<Administrateurs> LoadAdminsFromJson(string filePathAdmins)
        {
            Console.WriteLine("méthode LoadAdminsFromJson()");
            Console.Read();

            List<Administrateurs> admins = new List<Administrateurs>();

            // Vérifier si le fichier existe
            if (File.Exists(filePathAdmins))
            {
                string json = File.ReadAllText(filePathAdmins);

                // Vérifier si le fichier n'est pas vide
                if (!string.IsNullOrWhiteSpace(json))
                {
                    admins = JsonConvert.DeserializeObject<List<Administrateurs>>(json);
                }
            }
            return admins;
        }

        public static void SaveAdminsToJson(List<Administrateurs> admins, string filePathAdmins)
        {
            string json = JsonConvert.SerializeObject(admins, Formatting.Indented);
            File.WriteAllText(filePathAdmins, json);
            Console.WriteLine($"L'administrateur a bien été sauvegardé-ajouté dans le fichier {filePathAdmins}.");
            Console.Read();
        }

        public static List<Users> LoadUsersFromJson(string filePathUsers)
        {
            List<Users> users = new List<Users>();

            // Vérifier si le fichier existe
            if (File.Exists(filePathUsers))
            {
                string json = File.ReadAllText(filePathUsers);

                // Vérifier si le fichier n'est pas vide
                if (!string.IsNullOrWhiteSpace(json))
                {
                    users = JsonConvert.DeserializeObject<List<Users>>(json);
                }
            }
            return users;
        }

        public static void SaveUsersToJson(List<Users> users, string filePathUsers)
        {
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(filePathUsers, json);
            Console.WriteLine($"L'utilisateur a bien été sauvegardé-ajouté dans le fichier {filePathUsers}.");
            Console.Read();
        }

        public static List<Articles> LoadArticlesFromJson(string filePathArticles)
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

    }
}
