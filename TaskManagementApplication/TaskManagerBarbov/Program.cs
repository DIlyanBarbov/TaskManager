using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace TaskManagerBarbov
{
    class Program
    {
        static void Main(string[] args)
        {
            
            LoginView loginView = new LoginView();
            {
                loginView.Show();
                if (AuthenticationService.LoggedUser == null)
                    return;
            }
            TaskView eventView = new TaskView();

            {
                eventView.Show();

            }
            Console.WriteLine("Bye");
        }
    }
}
