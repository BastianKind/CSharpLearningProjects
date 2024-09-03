using LearningProject.Models;
using Phonebook.Services;
using System;

namespace Phonebook.Controllers
{
    public class PhonebookController
    {
        private readonly PhonebookService _phonebookService;

        public PhonebookController()
        {
            _phonebookService = new PhonebookService();
        }

        public void DisplayMenu()
        {
            Console.WriteLine(
                "+-----------------------+\n" +
                "| Phonebook Menu        |\n" +
                "| 1: Read all Users     |\n" +
                "| 2: Create a User      |\n" +
                "| 3: Search a User      |\n" +
                "| 4: Update a User      |\n" +
                "| 5: Delete a User      |\n" +
                "| 6: Back to Main Menu  |\n" +
                "+-----------------------+"
            );
        }

        public void ReadAllUsers()
        {
            var users = _phonebookService.GetAllUsers();
            Console.WriteLine("0: Name \t| E-Mail \t| Password");
            for (int i = 0; i < users.Count; i++)
            {
                var user = users[i];
                var maskedPassword = new string('*', user.Password.Length);
                Console.WriteLine($"{i + 1}: {user.Name} | {user.Email} | {maskedPassword}");
            }
        }

        public void CreateUser()
        {
            var user = new User();

            Console.WriteLine("Enter Name");
            user.Name = Console.ReadLine();
            Console.WriteLine("Enter Email");
            user.Email = Console.ReadLine();
            if (!IsEmailValid(user.Email))
            {
                Console.WriteLine("Invalid Email");
                return;
            }
            if (_phonebookService.IsEmailTaken(user.Email))
            {
                Console.WriteLine($"The Email {user.Email} is already taken");
                return;
            }
            Console.WriteLine("Enter Password");
            user.Password = Console.ReadLine();

            Console.WriteLine("Is this information correct?");
            Console.WriteLine($"Name: {user.Name}");
            Console.WriteLine($"Email: {user.Email}");
            Console.WriteLine($"Password: {user.Password}");
            Console.WriteLine("1: Yes");
            Console.WriteLine("2: No");
            var input = Console.ReadLine();
            if (input == "1")
            {
                if (!_phonebookService.DoesFileExist())
                {
                    _phonebookService.CreateFile();
                    Console.WriteLine("File created successfully");
                }
                _phonebookService.AddUser(user);
                Console.WriteLine("User created successfully");
            }
        }

        public void DeleteUser()
        {
            int rowIndex = GetUserRow();
            _phonebookService.DeleteUser(rowIndex);
            Console.WriteLine("User deleted successfully");
        }

        public void SearchUser()
        {
            Console.WriteLine("Searching a user");
            Console.WriteLine("Enter Email");
            string email = Console.ReadLine();
            if (IsEmailValid(email))
            {
                var users = _phonebookService.GetAllUsers();
                for (int i = 0; i < users.Count; i++)
                {
                    if (users[i].Email.ToLower().Contains(email.ToLower()))
                    {
                        var user = users[i];
                        var maskedPassword = new string('*', user.Password.Length);
                        Console.WriteLine($"{i + 1}: {user.Name} | {user.Email} | {maskedPassword}");
                        return;
                    }
                    if (i == users.Count - 1)
                    {
                        Console.WriteLine("E-Mail not found");
                    }
                }
            }
            else
            {
                Console.WriteLine("Input not Valid");
            }
        }

        public void UpdateUser()
        {
            Console.WriteLine("Updating a user");
            int row = GetUserRow();
            try
            {
                var users = _phonebookService.GetAllUsers();
                if (row > users.Count)
                {
                    Console.WriteLine("Row does not exist");
                    return;
                }
                var user = users[row];
                Console.WriteLine($"{row + 1}: {user.Name} | {user.Email} | {new string('*', user.Password.Length)}");

                Console.WriteLine("What do you want to change?");
                Console.WriteLine(
                        "+-------------+\n" +
                        "| 1: Name     |\n" +
                        "| 2: Email    |\n" +
                        "| 3: Password |\n" +
                        "+-------------+"
                    );
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.WriteLine("Enter Name");
                        user.Name = Console.ReadLine();
                        break;
                    case "2":
                        Console.WriteLine("Enter Email");
                        user.Email = Console.ReadLine();
                        if (!IsEmailValid(user.Email))
                        {
                            Console.WriteLine("Invalid Email");
                            return;
                        }
                        if (_phonebookService.IsEmailTaken(user.Email))
                        {
                            Console.WriteLine($"The Email {user.Email} is already taken");
                            return;
                        }
                        break;
                    case "3":
                        Console.WriteLine("Enter Password");
                        user.Password = Console.ReadLine();
                        if (!IsPasswordValid(user.Password))
                        {
                            Console.WriteLine("Invalid Password");
                            return;
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
                users[row] = user;
                File.WriteAllLines("./users.csv", users.ConvertAll(user => $"{user.Name};{user.Email};{user.Password}"));
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine(e.Message);
            }
        }

        public bool IsEmailValid(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }

        public bool IsPasswordValid(string password)
        {
            return password.Length >= 6;
        }

        public int GetUserRow()
        {
            int row;
            while (true)
            {
                Console.WriteLine("Enter Row number of User");
                if (int.TryParse(Console.ReadLine(), out row))
                {
                    return row - 1;
                }
                Console.WriteLine("Invalid Row Number");
            }
        }
    }
}
