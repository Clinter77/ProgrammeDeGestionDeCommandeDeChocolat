using Models;
using ServicesFichiersInteractions;
using ServicesLogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace ProgrammeDeGestionDeCommandeDeChocolatCore
{
    public class ProgrammeCore : Exception
    {
        // static bool errorCase = false;
        static string login = "";
        static string password = "";
        static bool isPasswordValid = false;
        static char[] arrayOfSpecialsChars = { '&', '#', '{', '[', '(', '|', '-', '_', ')', ']', '}', '$', '*', '%', '<', '>', ',', ';', ':', '!', '?', '.', '@' };
        static string nom = "";
        static string prenom = "";
        static string adresse = "";
        static string telephone = "";
        static string choix = "";
        static bool isAuthenticationSucced = false;
        static string[] currentUser = new string[2];
        // Obtention des informations par rapport à la date actuelle de la commande, et toutes ses informations (dont les heures et minutes)
        static DateTime now = DateTime.Now;
        static int inputUserArticle;

        // sur mon poste chez-moi
        static string filePathArticles = @"F:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat\Articles.json";
        // sur mon poste chez-moi
        // static string filePathArticles = @"C:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat\Articles.json";

        public static void initialiserLesArticles()
        {
            List<Articles> tableaudArticles = new List<Articles>();
            tableaudArticles.Add(new Articles("Ferrero rocher - boîte de 16 chocolats", 2.83F, 50));
            tableaudArticles.Add(new Articles("Jeff De Bruges - boîte ronde de 26 chocolats assortis", 39.35F, 50));
            tableaudArticles.Add(new Articles("Lindt - Connaisseurs - boîte de 217 grammes d'assortiment de chocolats", 8.09F, 50));
            tableaudArticles.Add(new Articles("Lindt - Lindor - boîte de 200 grammes d'assortiment de chocolats", 3.91F, 50));
            tableaudArticles.Add(new Articles("Kinder Bueno - boîte de 10 unités", 6.59F, 50));
            foreach (var article in tableaudArticles)
            {
                Console.WriteLine($"ajout de {article.Quantite} unités pour l'article {article.Reference}, au prix unitaire de {article.Prix}");
            }
            Console.Read();
            Console.Read();
            FileWriter.CreateTableaudArticles(tableaudArticles);
            ServicesLogs.ClassNLogJournalisation.LogArticlesContentsToJournalFile(tableaudArticles);
            // Console.Clear();
        }

        /* public static void ajouterAdministrateur0()
        {
            List<Administrateurs> tableaudAdministrateurs = new List<Administrateurs>();
            tableaudAdministrateurs.Add(new Administrateurs("Christophe", "Christophe57@CsharpExam"));
            foreach (var administrateur in tableaudAdministrateurs)
            {
                Console.WriteLine($"ajout de l'administrateur ayant pour login {administrateur.Login} et pour password {administrateur.Password}");
            }
            ServicesLogs.ClassNLogJournalisation.LogAdminstrateursToJournalFile(tableaudAdministrateurs);
            ServicesLogs.ClassNLogJournalisation.LogAdminstrateursToComptesAdminFile(tableaudAdministrateurs);
        } */

        // demander à l'User d'abord de bien vouloir créer son compte
        public static void askAccountType()
        {
            Console.Clear();
            // Console.Read();
            Console.WriteLine("1: Utilisateur");
            Console.WriteLine("2: Administrateur");
            Console.WriteLine("Quel compte souhaitez vous créer ? Quel est votre profil ?");
            // int inputChoiceProfil = Convert.ToInt32(Console.ReadLine());
            var inputAccountType = Console.ReadLine(); // ReadLine returns string type
            Console.Read();
            checkTypeOfVariable(inputAccountType);
            // Console.WriteLine(inputAccountType+" "+inputAccountType.GetTypeCode());
            // Console.Read();

            /* List<Administrateurs> tableaudAdministrateurs = new List<Administrateurs>();
            tableaudAdministrateurs.Add(new Administrateurs("Christophe", "Christophe57@CsharpExam"));
            foreach (var administrateur in tableaudAdministrateurs)
            {
                Console.WriteLine($"ajout de l'administrateur ayant pour login {administrateur.Login} et pour password {administrateur.Password}");
            }
            ServicesLogs.ClassNLogJournalisation.LogAdminstrateursToJournalFile(tableaudAdministrateurs);
            ServicesLogs.ClassNLogJournalisation.LogAdminstrateursToComptesAdminFile(tableaudAdministrateurs); */
        }

        // Fonction pour vérifier la validité de la variable entrée par l'User
        public static void checkTypeOfVariable<T>(T inputAccountType)
        {
            // Console.WriteLine("Type : {0}, Valeur : {1}", typeof(T), inputAccountType); // 0 : le type et 1 sa valeur
            // Console.WriteLine(inputAccountType + " " + inputAccountType.GetType());
            // Console.Read();

            try
            {
                int inputAccountTypeInt = Convert.ToInt32(inputAccountType);
                // Console.WriteLine("inputAccountType " + inputAccountType.GetType());
                // Console.WriteLine("inputAccountTypeInt" + inputAccountTypeInt.GetType());
                // Console.Read();
                // Console.WriteLine("méthode checkTypeOfVariable()");
                // Console.WriteLine("voici votre choix : " + inputChoiceAccountType + " " + inputChoiceAccountType.GetType());
                Console.WriteLine("voici votre choix : " + inputAccountTypeInt);
                Console.Read();
                // Console.Clear();
                checkChoiceAccount(inputAccountTypeInt);
            }
            catch (Exception e)
            {
                // Console.WriteLine(inputAccountType + " " + inputAccountType.GetType());
                Console.WriteLine(inputAccountType.GetType() + " " + e.Message);
                Console.Read();
                Console.WriteLine("Le profil choisi ne fait pas partie des choix disponibles, entrez un choix correct, 1 ou 2, selon le profil de compte");
                Console.WriteLine("Quel est votre choix de profil de compte ?");
                Console.Read();
                // Console.Clear();
                askAccountType();
                // askAccountType(); malheureusement ne sors plus de la boucle une fois entré dans le catch, même avec des choix corrects dans askAccountType()
                /* Console.WriteLine($"{e.Message}");
                Console.Read(); */
            }
        }

        public static void checkChoiceAccount(int inputAccountTypeInt)
        {
            // Console.WriteLine("méthode checkChoiceAccount()");
            // Console.Read();
            if ((inputAccountTypeInt >= 3) || (inputAccountTypeInt < 0))
            {
                Console.WriteLine($"Le choix {inputAccountTypeInt} n'est pas un choix correct. Les seuls choix possibles sont 1 ou 2.");
                Console.Read();
                askAccountType();
            }
            switch (inputAccountTypeInt)
            {
                case 1:
                    Console.WriteLine($"En ayant choisi la valeur {inputAccountTypeInt}, cela signifie que vous souhaitez vous connecter en tant qu'Utilisateur dans cette application.");
                    Console.Read();
                    createUserAccount();
                    connexionUser();
                    break;
                case 2:
                    Console.WriteLine($"En ayant choisi la valeur {inputAccountTypeInt}, cela signifie que vous souhaitez vous connecter en tant qu'Administrateur dans cette application.");
                    Console.Read();
                    createAdminAccount();
                    connexionAdmin();
                    break;
                default:
                    Console.WriteLine("Valeur inconnue au bataillon !");
                    Console.Read();
                    askAccountType();
                    break;
            }
        }

        

        public static void createAdminAccount()
        {
            login = "";
            password = "";
            List<Administrateurs> tableaudAdministrateurs = new List<Administrateurs>();
            // Console.WriteLine("Quel identifiant de connexion (login) voulez-vous ? - L'identifiant doit comprendre au moins trois caractères et ne pas dépasser 15 caractères");
            // Console.Read();
            // j'assigne la variable de l'User à ma variable login - ReadLine retunrs string type

            // Console.WriteLine("Quel identifiant de connexion (login) voulez-vous ?\nL'identifiant doit comprendre au moins trois caractères et ne pas dépasser 15 caractères");
            Console.WriteLine("Quel identifiant de connexion (login) voulez-vous ? \nL'identifiant doit comprendre au moins trois caractères et ne pas dépasser 15 caractères");
            Console.Read();
            login = Console.ReadLine();

            // Console.WriteLine("Voici votre login de connexion : " + login);
            // Console.WriteLine("Appuyez sur une touche pour fermer la console.");
            // Console.ReadKey();

            checkValidateLogin(login);

            Console.WriteLine("Entrez le mot de passe de connexion (password) que vous souhaitez ?\nVotre mot de passe doit contenir au moins 6 caractères alphanumériques et au moins un caractère spécial parmis les suivants : &#{[(|-_)]}$*%<>;,:!?.@ ( par exemple : AZERT1%)");
            Console.Read();
            password = Console.ReadLine();

            checkValidatePassword(password, arrayOfSpecialsChars);

            if (isPasswordValid == true)
            {
                // tableaudAdministrateurs.Add(new Administrateurs(login, password));
                Administrateurs administrateur = new Administrateurs(login, password);
                tableaudAdministrateurs.Add(administrateur);
                Console.WriteLine($"ajout de l'administrateur ayant pour login {login} et pour password {password}");
                ServicesLogs.ClassNLogJournalisation.LogAdminstrateursToJournalFile(administrateur);
                // avant - ServicesLogs.ClassNLogJournalisation.LogAdminstrateursToComptesAdminFile(tableaudAdministrateurs);
                FileWriter.CreateAdmin(login, password);
            }

            /* tableaudAdministrateurs.Add(new Administrateurs("Christophe", "Christophe57@CsharpExam"));
            foreach (var administrateur in tableaudAdministrateurs)
            {
                Console.WriteLine($"ajout de l'administrateur ayant pour login {administrateur.Login} et pour password {administrateur.Password}");
                ServicesLogs.ClassNLogJournalisation.LogAdminstrateursToJournalFile(tableaudAdministrateurs);
                ServicesLogs.ClassNLogJournalisation.LogAdminstrateursToComptesAdminFile(tableaudAdministrateurs);
            } */
        }

        public static void checkValidateLogin(string lgn)
        {
            if ((lgn.Length <= 2) || (lgn.Length > 15))
            {
                Console.WriteLine("Le login (identifiant) renseigné ne respecte pas les consignes ! au moins trois caractères et ne pas dépasser les 15 caractères");
                Console.Read();
                // createAdminAccount();
            }
            else Console.WriteLine($"Voici votre identifiant de connexion (login) : {lgn}");
            Console.Read();
        }

        public static void checkValidatePassword(string pswrd, char[] arrayOfSpecialsChars)
        {
            // throw new NotImplementedException();
            // Console.WriteLine("méthode checkValidatePassword()");

            /* Console.WriteLine("Caractères spéciaux dans le tableau :");
            foreach (char caractere in arrayOfSpecialsChars)
            {
                Console.WriteLine(caractere);
            } */
            // La longueur est juste
            // Console.WriteLine($"pswrd : {pswrd}");
            // Console.WriteLine(pswrd.Length);
            Console.WriteLine($"votre mot de passe : {password}");
            Console.Read();
            while (password.Length < 6)
            {
                Console.WriteLine("Votre mot de passe n'est pas valide, il ne respecte pas les consignes");
                Console.WriteLine("Entrez le mot de passe de connexion (password) que vous souhaitez ?\nVotre mot de passe doit contenir au moins 6 caractères alphanumériques et au moins un caractère spécial parmis les suivants : &#{[(|-_)]}$*%<>;,:!?.@ ( par exemple : AZERT1%)");
                Console.Read();
                password = Console.ReadLine();
            }
            if (password.Length >= 6)
            {
                foreach (char passwordChar in password)
                {
                    // isPasswordValid
                    foreach (char c in arrayOfSpecialsChars)
                    {
                        if (passwordChar == c)
                        {
                            isPasswordValid = true;
                        }
                    }
                }
                // Console.WriteLine("isPasswordValid "+ isPasswordValid);
                Console.Read();
                if (isPasswordValid == true)
                {
                    Console.WriteLine("Votre mot de passe \' " + password + " \' est valide");
                }
                else
                {
                    Console.WriteLine("Votre mot de passe \' " + password + "\' n'est pas valide");
                }
            }
        }

        // -------------------------------

        public static void createUserAccount()
        {
            List<Users> tableaudUsers = new List<Users>();

            Console.Write("Entrez votre nom :\n");
            string nom = Console.ReadLine();
            Console.Read();

            Console.Write("Entrez votre prénom :\n");
            string prenom = Console.ReadLine();
            Console.Read();

            Console.Write("Entrez votre adresse :\n");
            string adresse = Console.ReadLine();
            Console.Read();

            Console.Write("Entrez votre numéro de téléphone :\n");
            string telephone = Console.ReadLine();
            Console.Read();


            Console.WriteLine($"Voici les information renseignées : ");
            Console.WriteLine($"Nom : {nom}");
            Console.WriteLine($"Prénom : {prenom}");
            Console.WriteLine($"Adresse : {adresse}");
            Console.WriteLine($"Numéro de téléphone : {telephone}");
            Console.Read();

            checkValidateInfos(nom, prenom, adresse, telephone);

            Users user = new Users(nom, prenom, adresse, telephone);
            tableaudUsers.Add(user);

            // je stocke son nom et son prénom dans un array car j'en aurais besoin un peu plus tard ...
            // le nom de l'User actuel dans la première cellule du tableau, et son prénom dans la suivante
            currentUser[0] = nom;
            currentUser[1] = prenom;

            // Console.WriteLine("Voici votre login de connexion : " + login);
            // Console.WriteLine("Appuyez sur une touche pour fermer la console.");
            // Console.ReadKey();

        }

        public static void connexionUser()
        {
            // avant - string userResponse = askUserIfHeWantsToCommand(); le Framework refuse de prendre en considération la variable
            Console.WriteLine("Voulez-vous passer commande ? (O: Oui / N: Non) : ");
            string userResponse = Console.ReadLine();
            userResponse = Console.ReadLine();
            Console.Read();
            Console.Read();
            Console.WriteLine($"Voici votre choix {userResponse}");
            Console.Read();
            if ( (userResponse.Equals("O", StringComparison.OrdinalIgnoreCase)) || (userResponse=="O") || (userResponse=="o") )
            {
                functionUserCommand();
            }
        }
        public static void functionUserCommand()
        {
            Console.WriteLine("Voici les articles actuellement disponibles ");
            Console.Read();

            string cheminAcces = @"F:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat";

            // Création d'une nouvelle instance du processus CMD (Command Prompt)
            Process cmdProcess = new Process();

            // Configuration de ses propriétés
            cmdProcess.StartInfo.FileName = "cmd.exe";
            cmdProcess.StartInfo.WorkingDirectory = cheminAcces;

            // J'indique que je souhaite rediriger la sortie standard
            cmdProcess.StartInfo.RedirectStandardInput = true;
            cmdProcess.StartInfo.RedirectStandardOutput = true;
            cmdProcess.StartInfo.UseShellExecute = false;
            cmdProcess.StartInfo.CreateNoWindow = true;

            // Démarrage du processus
            cmdProcess.Start();

            // Obtention du flux d'entrée standard du processus
            StreamWriter sw = cmdProcess.StandardInput;

            // Exécution de la commande CMD
            sw.WriteLine("Notepad Articles.json");

            // Je ferme le flux d'entrée standard et j'attend que le processus se termine
            sw.Close();
            cmdProcess.WaitForExit();

            // Je ferme le processus
            cmdProcess.Close();

            List<Articles> articles = FileReader.LoadArticlesFromJson();
            

            Console.WriteLine("Saisissez F pour mettre Fin à la commande en cours ou bien poursuivez votre commande en sélectionnant les articles et leurs quantités au fur à mesure");
            Console.WriteLine("Voici les articles actuellement disponibles");
            Console.Read();
            foreach (Articles article in articles)
            {
                Console.WriteLine("Référence : "+article.Reference+"\n\tPrix unitaire : "+article.Prix+ "\n\tQuantité actuellemet disponile en stock " + article.Quantite);
            }
            Console.Read();
            commandArticles();
            Console.ReadKey();
        }

        public static void commandArticles()
        {
            // je charge les articles depuis le fichier JSON
            List<Articles> articles = FileReader.LoadArticlesFromJson();
            int choixRef = 1;
            Console.WriteLine("Voici les choix disponibles pour passer commande");
            foreach (Articles article in articles)
            {
                Console.WriteLine("Saisissez " + choixRef + " pour commander l'article : " + article.Reference);
                choixRef++;
            }

            string inputUserStr = "";
            
            float prixTotalCommande = 0F;
            List<ArticlesAchetes> listeArticlesAchetes = new List<ArticlesAchetes>();
            List<Articles> currentCommandList = new List<Articles>();
            
            while ( (inputUserStr != "F") || (inputUserStr != "f") )
            {
                Console.WriteLine("Quel est votre choix d'article ?");
                inputUserArticle = Convert.ToInt32(Console.ReadLine());
                Console.Read();
                if ((inputUserArticle >= 0) && (inputUserArticle < articles.Count-1))
                {
                    Console.WriteLine("Combien en voulez-vous ? ");
                    int quantiteArticleCommande = Convert.ToInt32(Console.ReadLine());
                    if (quantiteArticleCommande <= articles[inputUserArticle].Quantite)
                    {
                        ArticlesAchetes articlesAchete = new ArticlesAchetes(articles[inputUserArticle].Id, quantiteArticleCommande);
                        // ajout à la liste de sa commande
                        listeArticlesAchetes.Add(articlesAchete);
                        currentCommandList.Add(new Articles(articles[inputUserArticle].Reference, articles[inputUserArticle].Prix, quantiteArticleCommande));

                        // Utilisation du Setter pour mettre à jour la quantité
                        articles[inputUserArticle].Quantite -= quantiteArticleCommande;

                        // Enregistrement des modifications dans le fichier JSON
                        FileWriter.SaveArticlesToJson(articles);

                        // mettre à jour le prix de sa commande en cours
                        prixTotalCommande += articles[inputUserArticle].Prix * quantiteArticleCommande;
                    }
                }
                else
                {
                    Console.WriteLine("Choix d'article inconnu");
                    Console.Read();
                }
            }

            if ((inputUserStr == "F") || (inputUserStr == "f")) 
            {
                Console.WriteLine("Saisissez P si vous souhaitez connaître le prix de votre commande en cours\n ou V pour Visualiser votre commande");
                inputUserStr = Console.ReadLine();
                Console.Read();
                if ((inputUserStr == "P") || (inputUserStr == "p"))
                {
                    // l'User veut voir le prix de sa commande en cours
                    Console.WriteLine("Prix de votre commande actuellement : "+ prixTotalCommande);
                    Console.Read();
                }
                if ((inputUserStr == "V") || (inputUserStr == "v"))
                {
                    // l'User veut visualiser sa commande (généré dans un dossier à son nom et son nom de fichier doit avoir le format suivant "Nom-Prenom-Jour-Mois-Annee-Heure-Minute.txt"
                    foreach (ArticlesAchetes articleAchete in listeArticlesAchetes)
                    {
                        Console.WriteLine(articleAchete);
                        // 10/10/2023 Ajout d'un kinder 100g à 10h23 par Toto l'asticot
                        ClassNLogJournalisation.LogArticleCommandToJournalFile(now.Day + "/" + now.Month + "/" + now.Year + " Ajout de "+ currentCommandList[inputUserArticle].Quantite + " pour l'article " + currentCommandList[inputUserArticle].Reference + " à " + now.Hour+"h"+now.Minute + " par " + currentUser[0] + " " + currentUser[1]);
                    }
                    Console.Read();

                    string cheminEnregistrement = @"F:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat";

                    // Création d'une nouvelle instance du processus CMD (Command Prompt)
                    Process cmdProcess = new Process();

                    // Configuration de ses propriétés
                    cmdProcess.StartInfo.FileName = "cmd.exe";
                    cmdProcess.StartInfo.WorkingDirectory = cheminEnregistrement;

                    // J'indique que je souhaite rediriger la sortie standard
                    cmdProcess.StartInfo.RedirectStandardInput = true;
                    cmdProcess.StartInfo.RedirectStandardOutput = true;
                    cmdProcess.StartInfo.UseShellExecute = false;
                    cmdProcess.StartInfo.CreateNoWindow = true;

                    // Démarrage du processus
                    cmdProcess.Start();

                    // Obtention du flux d'entrée standard du processus
                    StreamWriter sw = cmdProcess.StandardInput;

                    // Exécution des commande CMD
                    sw.WriteLine("mkdir "+currentUser[0]+"_"+currentUser[1]);

                    // Je ferme le flux d'entrée standard et j'attend que le processus se termine
                    sw.Close();
                    cmdProcess.WaitForExit();

                    // Je ferme le processus
                    cmdProcess.Close();

                    

                    /* Console.WriteLine($"Date : {now.Day}/{now.Month}/{now.Year}");
                    Console.WriteLine($"Heure : {now.Hour}:{now.Minute}"); */

                    // currentUser[0] contient son nom et currentUser[1] son prénom et ensuite les infos en rapport avec la date, l'heure et les minutes
                    cheminEnregistrement += currentUser[0] + "-" + currentUser[1] + "-" + now.Day + "-" + now.Month + "-" +now.Year + "-" + now.Hour + "-" + now.Minute + ".txt";

                    // Enregistrement de sa commande dans le fichier texte - dans son répertoire
                    FileWriter.logCommand(currentCommandList, prixTotalCommande, cheminEnregistrement);

                }
                else 
                {
                    Console.WriteLine("choix inconnu !");
                }
            }

        }

        public static string askUserIfHeWantsToCommand()
        {
            Console.Write("Voulez-vous passer commande ? (O: Oui / N: Non) : ");
            string userResponse = Console.ReadLine();
            Console.Read();
            Console.WriteLine("Voici votre choix " + userResponse);
            Console.Read();
            return choix;
        }

        

        public static void checkValidateInfos(string n, string p, string a, string t)
        {
            if ((n == "") || (p == "") || (a == "") || (t == ""))
            {
                Console.WriteLine("Vous n'avez pas renseignées toutes les informations requises : ");
                Console.WriteLine($"nom {n}, prénom {p}, adresse {a}, et téléphone {t}");
                Console.Read();
                createUserAccount();
            }
            else
            {
                // case if ((n == "") && (p == "") && (a == "") && (t == ""))
                Console.WriteLine("Voici les informations renseignées : ");
                Console.WriteLine($"nom {n}, prénom {p}, adresse {a}, et téléphone {t}");
                Console.Read();

                // instanciation d'un nouvel utilisateur
                Users user = new Users(n, p, a, t);

                // journalisation dans logsFile.txt
                ServicesLogs.ClassNLogJournalisation.LogUtilisateursToJournalFile(user);

                // journalisation dans comptesUsers.json
                ServicesLogs.ClassNLogJournalisation.LogUsersToComptesUserFile(user);

                // journalisation dans comptesUsers.json
                Console.WriteLine($"étape d'ajout de l'utilisateur dans le fichier Json");
                Console.Read();
                FileWriter.CreateUser(n, p, a, t);
            }
        }

        public static void connexionAdmin()
        {
            // Charger les informations depuis le fichier JSON
            List<Administrateurs> adminstrateurs = FileReader.LoadAdminsFromJson();

            // admins.ForEach(a => Console.WriteLine("Login "+a.Login+" Password " + a.Password)); // admins est connu, et juste
            // Console.Read();



            Console.WriteLine("Entrez votre login : ");
            string loginConnexion = Console.ReadLine();
            Console.Read();
            Console.Read();

            Console.WriteLine("Entrez votre mot de passe : ");
            string passwordConnexion = Console.ReadLine();
            Console.Read();
            Console.Read();

            // bool isAuthenticationSucced = false;

            /* foreach (Administrateurs admin in adminstrateurs)
            {
                Console.WriteLine(admin.Login + " - " + admin.Password);
            }
            Console.Read(); */

            FileReader.checkConnexionAdmin(adminstrateurs, loginConnexion, passwordConnexion);
            FileReader.checkConnexionAdmin(adminstrateurs, loginConnexion, passwordConnexion);
            Console.Read();

            
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
                    }
                }
                else
                {
                    Console.WriteLine("le login renseigné est inconnu :(");
                }
            }
            

            if (isAuthenticationSucced)
            {
                Console.WriteLine("Félicitations, vous êtes parvenu à vous connecter en tant qu'Administrateur");
            }
            

            // Vérifier les informations d'authentification - appel de la méthode AuthenticateAdmin() impossible
            // AuthenticateAdmin(adminstrateurs, loginConnexion, passwordConnexion);
            // bool isAuthenticated = FileReader.AuthenticateAdmin(admins, loginConnexion, passwordConnexion); // il ne veut pas appeler la méthode depuis FileReader
            // bool isAuthenticated = AuthenticateAdmin(adminstrateurs, loginConnexion, passwordConnexion);
            // Console.WriteLine("isAuthenticated : "+ isAuthenticated);
            // Console.Read();

            // Afficher le résultat de l'authentification
            /*
            if (isAuthenticated)
            {
                Console.WriteLine("Authentification réussie !");
                Console.Read();
            }
            else
            {
                Console.WriteLine("Échec de l'authentification. Vérifiez vos informations.");
                Console.Read();
            }
            */
        }

        public static bool AuthenticateAdmin(List<Administrateurs> admins, string login, string password)
        {
            Console.WriteLine("méthode AuthenticateAdmin()");
            Console.Read();
            // Je vérifie si les informations d'authentification correspondent à un administrateur présent dans la liste
            return admins.Exists(a => a.Login == login && a.Password == password);
        }
    }
}
