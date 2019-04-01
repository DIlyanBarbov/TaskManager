using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TaskManagerBarbov
{
    class UserRepository : BaseRepository<UserEntity>
    {
        public UserRepository(string path) : base(path)
        {
        }

        public override void ReadFromFile(StreamReader streamReader, UserEntity userEntity)
        {
            userEntity.Username = streamReader.ReadLine();
            userEntity.Password = streamReader.ReadLine();
        }
        public override void WriteToFile(StreamWriter streamWriter, UserEntity userEntity)
        {
            streamWriter.WriteLine(userEntity.Username);
            streamWriter.WriteLine(userEntity.Password);
        }


        public UserEntity GetByUsernameAndPassword(string username, string password)
        {

            UserEntity user = GetAll().Where(u => u.Username == username && u.Password == password).FirstOrDefault();
            return user;
        }
        

    }
}
