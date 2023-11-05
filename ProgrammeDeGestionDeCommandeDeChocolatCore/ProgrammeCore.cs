﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using ServicesFichiersInteractions;
using ServicesLogs;

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
                    break;
                case 2:
                    Console.WriteLine($"En ayant choisi la valeur {inputAccountTypeInt}, cela signifie que vous souhaitez vous connecter en tant qu'Administrateur dans cette application.");
                    Console.Read();
                    createAdminAccount();
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
                        if (passwordChar == c){
                            isPasswordValid = true;
                        }
                    }
                }
                // Console.WriteLine("isPasswordValid "+ isPasswordValid);
                Console.Read();
                if (isPasswordValid==true)
                {
                    Console.WriteLine("Votre mot de passe \' "+password+" \' est valide");
                } else
                {
                    Console.WriteLine("Votre mot de passe \' " + password + "\' n'est pas valide");
                }
            }
        }

        // -------------------------------

        public static void createUserAccount()
        {
            List<Users> tableaudUsers = new List<Users>();

            Console.Write("Entrez votre nom : ");
            string nom = Console.ReadLine();

            Console.Write("Entrez votre prénom : ");
            string prenom = Console.ReadLine();

            Console.Write("Entrez votre adresse : ");
            string adresse = Console.ReadLine();

            Console.Write("Entrez votre numéro de téléphone : ");
            string telephone = Console.ReadLine();

            Console.Read();

            checkValidateInfos(nom, prenom, adresse, telephone);

            Users user = new Users(nom, prenom, adresse, telephone);
            tableaudUsers.Add(user);

            // Console.WriteLine("Voici votre login de connexion : " + login);
            // Console.WriteLine("Appuyez sur une touche pour fermer la console.");
            // Console.ReadKey();

            
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

        public static void connexion()
        {
            // Charger les informations depuis le fichier JSON
            List<Administrateurs> admins = FileReader.LoadAdminsFromJson();

            // admins.ForEach(a => Console.WriteLine("Login "+a.Login+" Password " + a.Password)); // admins est connu, et juste
            // Console.Read();

            Console.WriteLine("Entrez votre login : ");
            string login = Console.ReadLine();
            Console.Read();

            Console.WriteLine("Entrez votre mot de passe : ");
            string password = Console.ReadLine();
            Console.Read();

            // Vérifier les informations d'authentification
            bool isAuthenticated = FileReader.AuthenticateAdmin(admins, login, password);
            Console.WriteLine("isAuthenticated : "+ isAuthenticated);
            Console.Read();

            // Afficher le résultat de l'authentification
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
        }
    }
}
