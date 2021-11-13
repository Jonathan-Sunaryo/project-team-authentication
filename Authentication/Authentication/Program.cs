using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Authentication
{
    class Program
    {
        static bool mainMenu = true;
        static bool userFound = false;
        public static List<User> ListUser = new List<User>();
        static void Main(string[] args)
        {
            while (mainMenu == true)
            {
                Console.WriteLine("==BASIC AUTHENTICATION==");
                Console.WriteLine("1. Create User");
                Console.WriteLine("2. Show User");
                Console.WriteLine("3. Search");
                Console.WriteLine("4. Login");
                Console.WriteLine("5. Edit User");
                Console.WriteLine("6. Delete User");
                Console.WriteLine("7. Exit");
                Console.Write("Input :");

                string swc;
                swc = Console.ReadLine();
                switch (swc)
                {
                    case "1":
                        CreateUser();
                        break;
                    case "2":
                        ShowUser();
                        break;
                    case "3":
                        SearchUser();
                        break;
                    case "4":
                        LoginUser();
                        break;
                    case "5":
                        EditUser();
                        break;
                    case "6":
                        DeleteUser();
                        break;
                    case "7":
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.Write("ERROR : Input Not Valid");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }

            }

            bool PasswordValidation(string password)
            {
                Console.Clear();
                var hasNumber = new Regex(@"[0-9]+");
                var hasUpperChar = new Regex(@"[A-Z]+");
                var hasMinimum8Chars = new Regex(@".{8,}");

                var isValidated = hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password);
               
                if (password == "")
                {
                    Console.WriteLine();
                    Console.WriteLine("Input Not Valid");
                    return false;
                }
                else if (!isValidated)
                {
                    Console.WriteLine("Password harus mengandung angka, huruf besar,dan minimal 8 karakter");
                    return false;
                }
                else
                {
                    return true;
                }
            }

            bool NameValidation(string userName)
            {
                Console.Clear();
               
                var hasMinimum2Chars = new Regex(@".{2,}");

                var isValidated = hasMinimum2Chars.IsMatch(userName);

                if (userName == "")
                {
                    Console.WriteLine();
                    Console.WriteLine("Input Not Valid");
                    return false;
                }
                else if (!isValidated)
                {
                    Console.WriteLine("Nama minimal 2 karakter");
                    return false;
                }
                else
                {
                    return true;
                }
            }

            string UserValidation(string userName, int RNG)
            {
                for (int i = 0; i < ListUser.Count; i++)
                {
                    
                    if (userName == ListUser[i].userName)
                    {
                       
                        userName = userName + RNG.ToString();
                    }
                }
                return userName;
            }



            void CreateUser()
            {
                bool cekVal = false;
                string firstName,lastName,password;
                Console.Clear();
                Console.WriteLine("==CREATE USER==");
                
                do
                {
                    Console.Write("Firstname: ");
                    firstName = Console.ReadLine();
                    cekVal = NameValidation(firstName);
                } while (cekVal == false);
                cekVal = false;

               

                do
                {
                    Console.Write("Lastname: ");
                    lastName = Console.ReadLine();
                    cekVal = NameValidation(lastName);
                } while (cekVal == false);
                cekVal = false;

                do
                {
                    Console.Write("Password: ");
                     password = Console.ReadLine();
                    cekVal = PasswordValidation(password);
                } while (cekVal == false);
                cekVal = false;

                string userName = firstName.Substring(0, 2) + lastName.Substring(0, 2);

                Random rnd = new Random();
                int RNG = rnd.Next(00,99);
                bool sameUser=false;
                do
                {
                    userName = UserValidation(userName, RNG);
                    for (int i = 0; i < ListUser.Count; i++)
                    {
                        if (userName == ListUser[i].userName)
                        {
                            sameUser = true;
                        }
                    }
                }
                while ( sameUser == true);

                string hashPassword = BCrypt.Net.BCrypt.HashPassword(password);
                User myUser = new User(firstName, lastName, hashPassword, userName);

                ListUser.Add(myUser);
                Console.Write("User Created successfully");
                Console.ReadKey();
                Console.Clear();
            }

            void ShowUser()
            {
                Console.Clear();
                Console.WriteLine("==SHOW USER==");
                for (int i = 0; i < ListUser.Count; i++)
                {
                    Console.WriteLine("===========================================");
                    Console.WriteLine("NAME :" + ListUser[i].firstName +" "+ ListUser[i].lastName);
                    Console.WriteLine("USERNAME :" + ListUser[i].userName);
                    Console.WriteLine("PASSWORD :" + ListUser[i].password);
                    Console.WriteLine("===========================================");
                    Console.WriteLine("");
                }
                Console.ReadKey();
                Console.Clear();
            }

            void SearchUser()
            {
                userFound = false;
                Console.Clear();
                Console.WriteLine("==SEARCH USER==");
                Console.Write("Username: ");
                string username = Console.ReadLine();

                for (int i = 0; i < ListUser.Count; i++)
                {
                    if (username == ListUser[i].userName)
                    {
                        userFound = true;
                        Console.WriteLine("===========================================");
                        Console.WriteLine("NAME :" + ListUser[i].firstName + ListUser[i].lastName);
                        Console.WriteLine("USERNAME :" + ListUser[i].userName);
                        Console.WriteLine("PASSWORD :" + ListUser[i].password);
                        Console.WriteLine("===========================================");
                        Console.WriteLine("");
                    }
                   
                }

                UserNotFound();

                Console.ReadKey();
                Console.Clear();
                }

            void LoginUser ()
            {
                userFound = false;
                Console.Clear();
                Console.WriteLine("==LOGIN USER==");
                Console.Write("Username: ");
                string username = Console.ReadLine();
               
                
                for (int i = 0; i < ListUser.Count; i++)
                {
                    if (username == ListUser[i].userName)
                    {
                        userFound = true;
                        Console.Write("Password: ");
                        string password = Console.ReadLine();

                        if (BCrypt.Net.BCrypt.Verify(password,ListUser[i].password))
                        {
                            Console.WriteLine("Successfully login " + ListUser[i].userName);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Wrong Password");
                            break;
                        }
                    }
                }

                UserNotFound();

                Console.ReadKey();
                Console.Clear();
            }

            void EditUser()
            {
                userFound = false;
                Console.Clear();
                Console.WriteLine("==EDIT USER==");
                Console.Write("Username: ");
                string username = Console.ReadLine();

                for (int i = 0; i < ListUser.Count; i++)
                {
                    if (username == ListUser[i].userName)
                    {
                        userFound = true;
                        Console.WriteLine("===========================================");
                        Console.WriteLine("EDITING USER :" + ListUser[i].userName);
                        Console.WriteLine("Input menu number for editing");
                        Console.WriteLine("1.FULL NAME");
                        Console.WriteLine("2.USERNAME");
                        Console.WriteLine("3.PASSWORD");
                        Console.WriteLine("4.EXIT");
                        Console.WriteLine("===========================================");
                        Console.WriteLine("");
                        int swc = Convert.ToInt32(Console.ReadLine());
                        switch (swc)
                        {
                            case 1:
                                Console.WriteLine("CURRENT NAME: " + ListUser[i].firstName + " " + ListUser[i].lastName);
                                Console.Write("EDIT FIRSTNAME: ");
                                ListUser[i].firstName = Console.ReadLine();
                                Console.Write("EDIT LASTNAME: ");
                                ListUser[i].lastName = Console.ReadLine();
                                Console.WriteLine("NAME: " + ListUser[i].firstName + " " + ListUser[i].lastName + " EDITED SUCCESSULLY");
                                break;
                            case 2:
                                Console.WriteLine("CURRENT USERNAME: " + ListUser[i].userName);
                                Console.Write("EDIT USERNAME: ");
                                ListUser[i].userName = Console.ReadLine();
                                Console.WriteLine("USERNAME: " + ListUser[i].userName + " EDITED SUCCESSULLY");
                                break;
                            case 3:
                                Console.WriteLine("CURRENT PASSWORD: " + ListUser[i].password);
                                Console.Write("EDIT PASSWORD: ");
                                ListUser[i].password = Console.ReadLine();
                                Console.WriteLine("PASSWORD: " + ListUser[i].password + " EDITED SUCCESSULLY");
                                break;
                            case 4:


                                break;
                            default:
                                Console.WriteLine("WRONG INPUT");
                                break;
                        }
                    }
                }

                UserNotFound();
                Console.ReadKey();
                Console.Clear();
            }

            void DeleteUser()
            {
                userFound = false;
                Console.Clear();
                Console.WriteLine("==SEARCH USER==");
                Console.Write("Username: ");
                string username = Console.ReadLine();

                for (int i = 0; i < ListUser.Count; i++)
                {
                    if (username == ListUser[i].userName)
                    {
                        userFound = true;
                        Console.WriteLine("===========================================");
                        Console.WriteLine("DELETING USER :" + ListUser[i].userName);
                        Console.WriteLine("===========================================");
                        Console.WriteLine("");
                        ListUser.RemoveAt(i);
                    }

                }

                UserNotFound();

                Console.ReadKey();
                Console.Clear();
            }

            void UserNotFound ()
            {
                if (userFound == false)
                {
                    Console.WriteLine("Username not found");
                }
            }

        }
    }
}
