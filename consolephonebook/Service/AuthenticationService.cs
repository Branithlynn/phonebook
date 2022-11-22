using ConsolePhonebook.Entity;
using ConsolePhonebook.Repository;
using System;

namespace ConsolePhonebook.Service
{
    public static class AuthenticationService
    {
        public static User LoggedUser { get; private set; }

        public static void AuthenticateUser(string username, string password)
        {
            UsersRepository userRepo = new UsersRepository("users.txt");
            AuthenticationService.LoggedUser = userRepo.GetByUsernameAndPassword(username, password);
        }
    }
}
