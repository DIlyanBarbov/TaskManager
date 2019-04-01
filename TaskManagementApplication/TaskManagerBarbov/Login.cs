using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TaskManagerBarbov
{
    class UserRepository
    {
        public readonly string path;
        internal UserRepository(string path)
        {
            this.path = path;
        }

        public User GetByUsernameAndPassword(string username, string password)
        {

            User user = GetAll().Where(u => u.Username == username && u.Password == password).FirstOrDefault();
            return user;
        }
        public List<User> GetAll()
        {
            List<User> userlist = new List<User>();
            FileStream userfile = new FileStream(this.path, FileMode.OpenOrCreate);
            StreamReader userreader = new StreamReader(userfile);
            try
            {
                while (!userreader.EndOfStream)
                {
                    User user = new User();

                    user.Username = userreader.ReadLine();
                    user.Password = userreader.ReadLine();

                    userlist.Add(user);
                }
            }
            finally
            {
                userreader.Close();
                userfile.Close();
            }
            return userlist;
        }
        public void CheckLogin(User user)
        {
            FileStream userfile = new FileStream(this.path, FileMode.OpenOrCreate);
            StreamReader userreader = new StreamReader(userfile);


            while (!userreader.EndOfStream)
            {

                user.Username = userreader.ReadLine();
                user.Password = userreader.ReadLine();

            }
            userreader.Close();

            FileStream usersave = new FileStream(this.path, FileMode.Append);
            StreamWriter userwriter = new StreamWriter(usersave);
            if (user.Username == null || user.Password == null)
            {
                userwriter.WriteLine(user.Username);
                userwriter.WriteLine(user.Password);
            }

            userfile.Close();
            userwriter.Close();

        }
    }
}
