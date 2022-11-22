using System;
using ConsolePhonebook.Repository;
using ConsolePhonebook.Entity;
using ConsolePhonebook.Service;

namespace ConsolePhonebook.View
{
    public class LoginView
    {
        public void Show()
        {
            while (true)
            {
                Console.Clear();

                Console.Write("Username: ");
                string username = Console.ReadLine();

                Console.Write("Password: ");
                string password = Console.ReadLine();

                AuthenticationService.AuthenticateUser(username, password);

                if (AuthenticationService.LoggedUser != null)
                {
                    Console.WriteLine("Welcome " + AuthenticationService.LoggedUser.Username);
                    Console.ReadKey(true);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid username or password");
                    Console.ReadKey(true);
                }
            }
        }
    }
}
