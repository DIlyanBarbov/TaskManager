using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TaskManagerBarbov
{
    class LoginView
    {
        public void Show()
        {
            int count = 0;
            do
            {
                Console.WriteLine("Username is admin/ Password is password");
                UserRepository userRepo = new UserRepository("UserRepository.txt");
                Console.Write("Enter username: ");
                string username = Console.ReadLine();
                Console.Write("Enter password: ");
                string password = Console.ReadLine();
                AuthenticationService.Authenticate(username, password);
                if (AuthenticationService.LoggedUser == null)
                {
                    Console.Clear();
                    Console.WriteLine("Invalid username or password..");
                    count++;
                }
                if (count >= 3)
                {
                    Console.WriteLine("Too many invalid logins..");
                    Console.ReadKey(true);
                    return;
                }
            }
            while (AuthenticationService.LoggedUser == null && count < 3);

        }
    }
}
