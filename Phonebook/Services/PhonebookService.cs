using LearningProject.Models;
using Phonebook.Repositories;
using System.Collections.Generic;

namespace Phonebook.Services
{
    public class PhonebookService
    {
        private readonly UserRepository _userRepository;

        public PhonebookService()
        {
            _userRepository = new UserRepository();
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public void AddUser(User user)
        {
            _userRepository.AddUser(user);
        }

        public void DeleteUser(int rowIndex)
        {
            _userRepository.DeleteUser(rowIndex);
        }

        public bool IsEmailTaken(string email)
        {
            var users = _userRepository.GetAllUsers();
            return users.Exists(user => user.Email.ToLower() == email.ToLower());
        }

        public bool DoesFileExist()
        {
            return _userRepository.DoesFileExist();
        }

        public void CreateFile()
        {
            _userRepository.CreateFile();
        }
    }
}
