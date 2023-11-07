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
        // static int inputUserArticle;
        static int inputUserArticleInt;
        static int inputUserInt = -1;
        static string inputUser = "";
        // static object inputUser = new object();
        static string quantiteArticlesCommandesParUser = "";
        static int quantiteArticlesCommandesParUserInt = 0;
        public static float prixTotalCommande = 0F;


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
            // création de l'Administrateur de l'application
            createAdminAccount();
            Console.Clear();
            // Console.Read();
            Console.WriteLine("1: Utilisateur ");
            Console.WriteLine("2: Administrateur ");
            Console.WriteLine("Quel est votre profil ? ");
            // int inputChoiceProfil = Convert.ToInt32(Console.ReadLine());
            var inputAccountType = Console.ReadLine(); // ReadLine returns string type
            Console.WriteLine("inputAccountType "+ inputAccountType);
           
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
        public static void checkTypeOfVariable(string inputAccountType)
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
                // Console.Clear();
                checkChoiceAccount(inputAccountTypeInt);
            }
            catch (Exception e)
            {
                // Console.WriteLine(inputAccountType + " " + inputAccountType.GetType());
                Console.WriteLine(inputAccountType.GetType() + " " + e.Message);
                /* Console.WriteLine("Le profil choisi ne fait pas partie des choix disponibles, entrez un choix correct, 1 ou 2, selon le profil de compte");
                Console.WriteLine("Quel est votre choix de profil de compte ?");
                Console.Read(); */
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
            if ((inputAccountTypeInt < 0) || (inputAccountTypeInt >= 3))
            {
                Console.WriteLine($"Le choix {inputAccountTypeInt} n'est pas un choix correct. Les seuls choix possibles sont 1 ou 2.");
               
                askAccountType();
            }
            switch (inputAccountTypeInt)
            {
                case 1:
                    Console.WriteLine($"En ayant choisi la valeur {inputAccountTypeInt}, cela signifie que vous souhaitez vous connecter en tant qu'Utilisateur dans cette application.");
                  
                    createUserAccount();
                    connexionUser();
                    break;
                case 2:
                    Console.WriteLine($"En ayant choisi la valeur {inputAccountTypeInt}, cela signifie que vous souhaitez vous connecter en tant qu'Administrateur dans cette application.");
                    connexionAdmin();
                    break;
                default:
                    Console.WriteLine("Valeur inconnue au bataillon !");
                    askAccountType();
                    break;
            }
        }

        

        public static void createAdminAccount()
        {
            List<Administrateurs> tableaudAdministrateurs = new List<Administrateurs>();
            // Console.WriteLine("Quel identifiant de connexion (login) voulez-vous ? - L'identifiant doit comprendre au moins trois caractères et ne pas dépasser 15 caractères");
            // Console.Read();
            // j'assigne la variable de l'User à ma variable login - ReadLine retunrs string type

            // Console.WriteLine("Quel identifiant de connexion (login) voulez-vous ?\nL'identifiant doit comprendre au moins trois caractères et ne pas dépasser 15 caractères");

            /* Console.WriteLine("L'identifiant doit comprendre au moins trois caractères et ne pas dépasser 15 caractères");
            Console.WriteLine("Saisissez vos informations ");
            Console.WriteLine("Quel identifiant de connexion (login) voulez-vous ? ");
           
            login = Console.ReadLine(); */

            // Console.WriteLine("Voici votre login de connexion : " + login);
            // Console.WriteLine("Appuyez sur une touche pour fermer la console.");
            // Console.ReadKey();
            Administrateurs admin = new Administrateurs("ChristopheAdmin","ChristophePassword@123");
            tableaudAdministrateurs.Add(admin);
            // Console.WriteLine(admin.Login+" "+admin.Password);
            checkValidateLogin(admin.Login);

            // Console.WriteLine("Votre mot de passe doit contenir au moins 6 caractères alphanumériques et au moins un caractère spécial parmis les suivants : &#{[(|-_)]}$*%<>;,:!?.@ ( par exemple : AZERT1%)");
            // Console.WriteLine("Entrez le mot de passe de connexion (password) que vous souhaitez ? ");
            /// password = Console.ReadLine();

            checkValidatePassword(admin.Password, arrayOfSpecialsChars);

            // tableaudAdministrateurs.Add(new Administrateurs(login, password));
            Console.WriteLine($"ajout de l'administrateur en BDD - dans le fichier JSON");
            // Console.WriteLine($"ajout de l'administrateur ayant pour login {login} et pour password {password}");
            ServicesLogs.ClassNLogJournalisation.LogAdminstrateursToJournalFile(admin);
            // avant - ServicesLogs.ClassNLogJournalisation.LogAdminstrateursToComptesAdminFile(tableaudAdministrateurs);
            FileWriter.CreateAdmin(tableaudAdministrateurs);


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
                createAdminAccount();
            }
            else Console.WriteLine($"Voici votre identifiant de connexion (login) : {lgn}");
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
            // Console.WriteLine($"votre mot de passe : {password}");

            while (pswrd.Length < 6)
            {
                Console.WriteLine("Votre mot de passe n'est pas valide, il ne respecte pas les consignes ");
                Console.WriteLine("Celui-ci doit contenir au moins 6 caractères alphanumériques et au moins un caractère spécial parmis les suivants : &#{[(|-_)]}$*%<>;,:!?.@ ( par exemple : AZERT1% ) ");
            }
            if (password.Length >= 6)
            {
                foreach (char passwordChar in password)
                {
                    foreach (char c in arrayOfSpecialsChars)
                    {
                        if (passwordChar == c)
                        {
                            isPasswordValid = true;
                        }
                    }
                }
                // Console.WriteLine("isPasswordValid "+ isPasswordValid);
              
                
                if (isPasswordValid == true)
                {
                    Console.WriteLine("Le mot de passe renseigné par l'Administrateur est valide");
                    // Console.WriteLine("Votre mot de passe \' " + password + " \' est valide");
                }
                else
                {
                    Console.WriteLine("Le mot de passe renseigné par l'Administrateur n'est pas valide");
                    // Console.WriteLine("Votre mot de passe \' " + password + "\' n'est pas valide");
                }
            }
        }

        // -------------------------------

        public static void createUserAccount()
        {

            Console.WriteLine("Saisissez vos informations ");

            Console.Write("Entrez votre nom : ");
            string nom = Console.ReadLine();


            Console.Write("Entrez votre prénom : ");
            string prenom = Console.ReadLine();


            Console.Write("Entrez votre adresse : ");
            string adresse = Console.ReadLine();


            Console.Write("Entrez votre numéro de téléphone : ");
            string telephone = Console.ReadLine();

            // Affichage des informations saisies
            Console.WriteLine($"Nom : {nom}");
            Console.WriteLine($"Prénom : {prenom}");
            Console.WriteLine($"Adresse : {adresse}");
            Console.WriteLine($"Numéro de téléphone : {telephone}");

            Console.Read();

            checkValidateInfos(nom, prenom, adresse, telephone);

            Users user = new Users(nom, prenom, adresse, telephone);
            List<Users> tableaudUsers = new List<Users>();
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
            Console.WriteLine();

            // chez-moi
            // string cheminAcces = @"F:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat";

            // poste MEWO
            string cheminAcces = @"C:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat";

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
            foreach (Articles article in articles)
            {
                Console.WriteLine("Référence : " + article.Reference + "\n\tPrix unitaire : " + article.Prix + "\n\tQuantité actuellemet disponile en stock " + article.Quantite);
            }
            Console.WriteLine();
            commandArticles();
            // Console.ReadKey();
        }


        

        public static void commandArticles()
        {
            // je charge les articles depuis le fichier JSON
            List<Articles> articles = FileReader.LoadArticlesFromJson();
            int choixRef = 0;
            Console.WriteLine("Voici les choix disponibles pour passer commande");
            foreach (Articles article in articles)
            {
                Console.WriteLine("Saisissez " + choixRef + " pour commander l'article : " + article.Reference);
                choixRef++;
            }
            Console.WriteLine();
            

            // keyTouch la touche saisie par l'User (qui peut aussi bien être sous la forme d'un int (de 0 à 9 par exemple)
            // ou aussi bien une touche comme F, P, ou V (pour mettre Fin à la commande, P Print pour l'afficher ou V pour Visualiser celle-ci)
            object keyTouch = new object();

            List<ArticlesAchetes> listeArticlesAchetes = new List<ArticlesAchetes>();
            List<Articles> currentCommandList = new List<Articles>();

            Console.WriteLine("Quel est votre choix d'article ? ");
            Console.WriteLine("Saisissez la valeur correspondante à l'article ");
            inputUser = Console.ReadLine();
            Console.Read();

            while ( (inputUser != "F") || (inputUser != "f") )
            {
                try
                {
                    inputUserInt = Convert.ToInt32(inputUser);
                    // Console.Read();
                    Console.WriteLine(inputUserInt + " de type " + inputUserInt.GetType());
                    Console.WriteLine("inputUser est de type int");
                    // donc l'User veut faire son choix d'articles, il souhaite passer commande
                    // checker inputUser pour que la valeur corresponde à un choix possible et ensuite créer la commande 
                    doCommand(articles, listeArticlesAchetes, currentCommandList, inputUser, inputUserInt);
                }
                catch (FormatException)
                {
                    Console.WriteLine(inputUser + " de type " + inputUser.GetType());
                    if (typeof(string) == inputUser.GetType())
                    {
                        Console.WriteLine("inputUser est de type string");
                        if ((inputUser.ToString() == "F") || (inputUser.ToString() == "f"))
                        {
                            Console.WriteLine("En saisissant la lettre F, vous mettez Fin à la commande en cours");
                        }
                        if ((inputUser.ToString() == "P") || (inputUser.ToString() == "p"))
                        {
                            Console.WriteLine("En saisissant la lettre P, vous souhaitez connaître le Prix de la commande en cours");
                            showCommandPrice(prixTotalCommande);
                        }
                        if ((inputUser.ToString() == "V") || (inputUser.ToString() == "v"))
                        {
                            Console.WriteLine("En saisissant la lettre V, vous souhaitez Visualiser la commande en cours");
                            showCommand(listeArticlesAchetes, currentCommandList, inputUser);
                        }
                    }
                    else Console.WriteLine("type inconnu");
                }
            }

            
            




            /* 
            try
            {
                Console.WriteLine();
                // Conversion de la chaîne en un entier en utilisant Convert.ToInt32
                inputUserInt = Convert.ToInt32(inputUser);

                // Affichage du résultat
                Console.WriteLine($"Vous avez saisi : {inputUserInt} qui est de type {inputUserInt.GetType()}");
            }
            catch (FormatException)
            {
                if ((inputUserInt.ToString() == "F") || (inputUserInt.ToString() == "f"))
                {
                    Console.WriteLine("En saisissant la lettre F, vous mettez Fin à la commande en cours");
                }
                if ((inputUserInt.ToString() == "P") || (inputUserInt.ToString() == "p"))
                {
                    showCommandPrice();
                    Console.WriteLine("En saisissant la lettre P, vous souhaitez connaître le Prix de la commande en cours");
                }
                if ((inputUserInt.ToString() == "V") || (inputUserInt.ToString() == "v"))
                {
                    Console.WriteLine("En saisissant la lettre V, vous souhaitez Visualiser la commande en cours");
                }
                else Console.WriteLine("Erreur de format. Assurez-vous d'entrer un nombre entier valide.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Le nombre saisi est trop grand ou trop petit pour être représenté en tant qu'entier.");
            }
            */


            /* 
            while ((inputUser.ToString() != "F") || (inputUser.ToString() != "f"))
            {
                // Console.WriteLine("Quel est votre choix d'article ?");

                // inputUser = Console.ReadLine();
                // Console.Read();
                // checkKeyTouch(inputUser);

                // le type d'inputUser
                // Console.WriteLine(inputUser.GetType());
                // Console.Read();

                /* 
                string[] numbersArray = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                foreach (string n in numbersArray)
                {
                    if (inputUser == n)
                    {
                        // conversion de l'inputUser en int pour pouvoir être utilisé sous cette forme pour la suite ...
                        inputUserInt = Convert.ToInt32(inputUser);
                    }
                }
                */
            /*
                // je m'assure que inputUserInt est bien de type int
                if (typeof(int) == inputUserInt.GetType())
                {
                    // Console.WriteLine(articles.Count); // 5 c'est bon

                    if ((inputUserInt >= 0) && (inputUserInt < articles.Count))
                    {
                        Console.WriteLine($"Vous souhaitez commander l'article : {articles[inputUserInt].Reference}");
                        Console.Read();
                        Console.WriteLine("Combien en voulez-vous ? ");
                        quantiteArticlesCommandesParUser = Console.ReadLine();
                        quantiteArticlesCommandesParUserInt = Convert.ToInt32(quantiteArticlesCommandesParUser);
                        if (quantiteArticlesCommandesParUserInt <= articles[inputUserInt].Quantite)
                        {
                            ArticlesAchetes articlesAchete = new ArticlesAchetes(articles[inputUserInt].Id, quantiteArticlesCommandesParUserInt);
                            // ajout à la liste de sa commande
                            listeArticlesAchetes.Add(articlesAchete);
                            currentCommandList.Add(new Articles(articles[inputUserInt].Reference, articles[inputUserInt].Prix, quantiteArticlesCommandesParUserInt));

                            // Utilisation du Setter pour mettre à jour la quantité
                            articles[inputUserInt].Quantite -= quantiteArticlesCommandesParUserInt;

                            // Enregistrement des modifications dans le fichier JSON
                            FileWriter.SaveArticlesToJson(articles);

                            // mettre à jour le prix de sa commande en cours
                            prixTotalCommande += articles[inputUserInt].Prix * quantiteArticlesCommandesParUserInt;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Choix d'article inconnu");
                        Console.Read();
                    }
                }
            } */

        }

        public static void doCommand(List<Articles> articles, List<ArticlesAchetes> listeArticlesAchetes, List<Articles> currentCommandList, string inputUser, int inputUserInt)
        {
            // Console.WriteLine(articles.Count); // 5 c'est bon

            if ((inputUserInt >= 0) && (inputUserInt < articles.Count))
            {

                Console.Read();
                Console.WriteLine($"Vous souhaitez commander l'article : {articles[inputUserInt].Reference}");
                Console.WriteLine("Combien en voulez-vous ? ");
                string quantiteArticlesCommandesParUser = Console.ReadLine();
                Console.Read();
                Console.WriteLine("quantiteArticlesCommandesParUser : "+ quantiteArticlesCommandesParUser + " et de type " + quantiteArticlesCommandesParUser.GetType());
                
                quantiteArticlesCommandesParUserInt = Convert.ToInt32(quantiteArticlesCommandesParUser);
                Console.WriteLine("quantiteArticlesCommandesParUserInt : " + quantiteArticlesCommandesParUserInt + " et de type " + quantiteArticlesCommandesParUserInt.GetType());
                if (quantiteArticlesCommandesParUserInt <= articles[inputUserInt].Quantite)
                {
                    ArticlesAchetes articlesAchete = new ArticlesAchetes(articles[inputUserInt].Id, quantiteArticlesCommandesParUserInt);
                    // ajout à la liste de sa commande
                    listeArticlesAchetes.Add(articlesAchete);
                    currentCommandList.Add(new Articles(articles[inputUserInt].Reference, articles[inputUserInt].Prix, quantiteArticlesCommandesParUserInt));

                    // Utilisation du Setter pour mettre à jour la quantité
                    articles[inputUserInt].Quantite -= quantiteArticlesCommandesParUserInt;

                    // Enregistrement des modifications dans le fichier JSON
                    FileWriter.SaveArticlesToJson(articles);

                    // mettre à jour le prix de sa commande en cours
                    prixTotalCommande += articles[inputUserInt].Prix * quantiteArticlesCommandesParUserInt;
                }
            }
            else
            {
                Console.WriteLine("Choix d'article inconnu");
            }
        }

        public static void showCommand(List<ArticlesAchetes> listeArticlesAchetes, List<Articles> currentCommandList, string inputUser)
        {
            Console.WriteLine("En saisissant la lettre V, vous souhaitez Visualiser la commande en cours");
            // l'User veut visualiser sa commande (généré dans un dossier à son nom et son nom de fichier doit avoir le format suivant "Nom-Prenom-Jour-Mois-Annee-Heure-Minute.txt"
            foreach (ArticlesAchetes articleAchete in listeArticlesAchetes)
            {
                Console.WriteLine(articleAchete);
                // exemple à suivre : 10/10/2023 Ajout d'un kinder 100g à 10h23 par Toto l'asticot
                ClassNLogJournalisation.LogArticleCommandToJournalFile(now.Day + "/" + now.Month + "/" + now.Year + " Ajout de " + currentCommandList[inputUserInt].Quantite + " pour l'article " + currentCommandList[inputUserInt].Reference + " à " + now.Hour + "h" + now.Minute + " par " + currentUser[0] + " " + currentUser[1]);
            }

            // chez-moi
            string cheminEnregistrement = @"F:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat";

            // poste MEWO
            // string cheminEnregistrement = @"C:\Users\Christophe.DESKTOP-EMFR2GT\source\repos\ProgrammeDeGestionDeCommandeDeChocolat";

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
            sw.WriteLine("mkdir " + currentUser[0] + "_" + currentUser[1]);

            // Je ferme le flux d'entrée standard et j'attend que le processus se termine
            sw.Close();
            cmdProcess.WaitForExit();

            // Je ferme le processus
            cmdProcess.Close();



            /* Console.WriteLine($"Date : {now.Day}/{now.Month}/{now.Year}");
            Console.WriteLine($"Heure : {now.Hour}:{now.Minute}"); */

            // currentUser[0] contient son nom et currentUser[1] son prénom et ensuite les infos en rapport avec la date, l'heure et les minutes
            cheminEnregistrement += currentUser[0] + "-" + currentUser[1] + "-" + now.Day + "-" + now.Month + "-" + now.Year + "-" + now.Hour + "-" + now.Minute + ".txt";

            // Enregistrement de sa commande dans le fichier texte - dans son répertoire
            FileWriter.logCommand(currentCommandList, prixTotalCommande, cheminEnregistrement);

        }



        public static void showCommandPrice(float prixTotalCommande)
        {
            // l'User veut voir le prix de sa commande en cours
            Console.WriteLine("Prix de votre commande actuellement : " + prixTotalCommande);
        }

        public static void checkKeyTouch(object keyTouch)
        {
            // Lire la touche saisie par l'utilisateur
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            // Afficher des informations sur la touche saisie
            Console.WriteLine("\nTouche saisie : " + keyInfo.Key);
            Console.WriteLine("Modificateurs : " + keyInfo.Modifiers);
            Console.WriteLine("Caractère : " + keyInfo.KeyChar);
        }

        public static string askUserIfHeWantsToCommand()
        {
            Console.Write("Voulez-vous passer commande ? (O: Oui / N: Non) : ");
            string userResponse = Console.ReadLine();
            Console.Read();
            Console.WriteLine("Voici votre choix " + userResponse);
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
            List<Administrateurs> administrateurs = FileReader.LoadAdminsFromJson();

            // admins.ForEach(a => Console.WriteLine("Login "+a.Login+" Password " + a.Password)); // admins est connu, et juste
            // Console.Read();
            foreach (Administrateurs admin in administrateurs)
            {
                Console.WriteLine(admin.Login + " " + admin.Password);
            }
            Console.Read();

            Console.WriteLine("Entrez votre login : ");
            string loginConnexion = Console.ReadLine();

            Console.WriteLine("Entrez votre mot de passe : ");
            string passwordConnexion = Console.ReadLine();
            Console.Read();

            // bool isAuthenticationSucced = false;
            /* foreach (Administrateurs admin in adminstrateurs)
            {
                Console.WriteLine(admin.Login + " - " + admin.Password);
            }
            Console.Read(); */
            // FileReader.checkConnexionAdmin(adminstrateurs, loginConnexion, passwordConnexion);


            Console.WriteLine("Vérifications des identifiants de connexion");
            foreach (Administrateurs admin in administrateurs)
            {
                if (loginConnexion == admin.Login)
                {
                    if (passwordConnexion == admin.Password)
                    {
                        Console.WriteLine("Les login et password renseignés sont correct :)");
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
                Console.Read();
            }
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
