using LearningProject.Models;
using System.Collections.Generic;
using System.IO;

namespace Phonebook.Repositories
{
    public class UserRepository
    {
        private const string FilePath = "./users.csv";

        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            if (File.Exists(FilePath))
            {
                foreach (var line in File.ReadAllLines(FilePath))
                {
                    var parts = line.Split(';');
                    users.Add(new User { Name = parts[0], Email = parts[1], Password = parts[2] });
                }
            }
            return users;
        }

        public void AddUser(User user)
        {
            File.AppendAllLines(FilePath, new[] { $"{user.Name};{user.Email};{user.Password}" });
        }

        public void DeleteUser(int rowIndex)
        {
            var lines = new List<string>(File.ReadAllLines(FilePath));
            if (rowIndex < lines.Count)
            {
                lines.RemoveAt(rowIndex);
                File.WriteAllLines(FilePath, lines);
                Console.WriteLine("User deleted successfully!");
            }
            else
            {
                Console.WriteLine("Invalid row index");
            }
        }

        public bool DoesFileExist()
        {
            return File.Exists(FilePath);
        }

        public void CreateFile()
        {
            File.Create(FilePath).Dispose();
        }
    }
}
