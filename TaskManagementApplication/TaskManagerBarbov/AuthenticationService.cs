using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerBarbov
{
    static class AuthenticationService
    {
        public static UserEntity LoggedUser { get; private set; }
        public static void Authenticate(string username,string password)
        {
            UserRepository userRepo = new UserRepository("UserRepository.txt");
            LoggedUser = userRepo.GetByUsernameAndPassword(username, password);
            
        }
    }
}
