using Phonebook.Controllers;
using System;

namespace MainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine(
                    "+-------------------+\n" +
                    "| Main Menu         |\n" +
                    "| 1: Phonebook      |\n" +
                    "| 2: Another App    |\n" +
                    "| 3: Exit           |\n" +
                    "+-------------------+"
                );

                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        RunPhonebook();
                        break;
                    case "2":
                        Console.WriteLine("Another App Here");
                        // Future expansion for another app
                        break;
                    case "3":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

        static void RunPhonebook()
        {
            var phonebookController = new PhonebookController();
            bool phonebookRunning = true;

            while (phonebookRunning)
            {
                phonebookController.DisplayMenu();
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        phonebookController.ReadAllUsers();
                        break;
                    case "2":
                        phonebookController.CreateUser();
                        break;
                    case "3":
                        phonebookController.SearchUser();
                        break;
                    case "4":
                        phonebookController.UpdateUser();
                        break;
                    case "5":
                        phonebookController.DeleteUser();
                        break;
                    case "6":
                        phonebookRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
    }
}
