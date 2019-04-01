using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TaskManagerBarbov
{
    public abstract class BaseRepository<T> where T : BaseEntity,new()
    {
        public abstract void ReadFromFile(StreamReader streamReader, T item);
        public abstract void WriteToFile(StreamWriter streamWriter, T item);
       
        private readonly string path;
        public BaseRepository(string path)
        {
            this.path = path;
        }
        public void Save(T item)
        {
            int id = 0;

            FileStream fileStream = new FileStream(this.path, FileMode.OpenOrCreate);
            StreamReader streamReader = new StreamReader(fileStream);
            while (!streamReader.EndOfStream)
            {
                T readItem = new T();
                readItem.Id = Convert.ToInt32(streamReader.ReadLine());
                ReadFromFile(streamReader, readItem);
                id = readItem.Id + 1;
            }
            streamReader.Close();
            fileStream.Close();
            item.Id = id;
            FileStream newFileStream = new FileStream(this.path, FileMode.Append);
            StreamWriter newStreamWriter = new StreamWriter(newFileStream);
            {
                newStreamWriter.WriteLine(item.Id);
                WriteToFile(newStreamWriter, item);
            }

            newStreamWriter.Close();
            newFileStream.Close();

            
        }
        public List<T> GetAll()
        {
            List<T> itemList = new List<T>();
            FileStream fileStream = new FileStream(this.path, FileMode.OpenOrCreate);
            StreamReader streamReader = new StreamReader(fileStream);
            try
            {
                while (!streamReader.EndOfStream)
                {
                    T readItem = new T();
                    readItem.Id = Convert.ToInt32(streamReader.ReadLine());
                    ReadFromFile(streamReader, readItem);
                    itemList.Add(readItem);
                }
            }
            finally
            {
                streamReader.Close();
                fileStream.Close();
            }
            return itemList;
        }
        public void Delete(int id)
        {
            string temp = this.path + "temp";
            FileStream newFileStream = new FileStream(temp, FileMode.OpenOrCreate);
            StreamWriter streamWriter = new StreamWriter(newFileStream);

            FileStream fileStream = new FileStream(this.path, FileMode.OpenOrCreate);
            StreamReader streamReader = new StreamReader(fileStream);
            try
            {
                while (!streamReader.EndOfStream)
                {
                    T readItem = new T();
                    readItem.Id = Convert.ToInt32(streamReader.ReadLine());
                    ReadFromFile(streamReader, readItem);

                    if (readItem.Id != id)
                    {
                        streamWriter.WriteLine(readItem.Id);
                        WriteToFile(streamWriter, readItem);
                    }
                }

            }
            finally
            {
                streamWriter.Close();
                streamReader.Close();
                fileStream.Close();
                newFileStream.Close();
            }
            File.Delete(this.path);
            File.Move(temp, this.path);
        }
        public void Edit(T item)
        {
            string temp = this.path + "temp";
            FileStream fileStream = new FileStream(this.path, FileMode.OpenOrCreate);
            StreamReader streamReader = new StreamReader(fileStream);

            FileStream newFileStream = new FileStream(temp, FileMode.OpenOrCreate);
            StreamWriter streamWriter = new StreamWriter(newFileStream);
            try
            {
                while (!streamReader.EndOfStream)
                {
                    T oldItem = new T();
                    oldItem.Id = Convert.ToInt32(streamReader.ReadLine());
                    ReadFromFile(streamReader, oldItem);
                    if (oldItem.Id != item.Id)
                    {
                        streamWriter.WriteLine(oldItem.Id);
                        WriteToFile(streamWriter, oldItem);
                    }
                    else
                    {
                        streamWriter.WriteLine(item.Id);
                        WriteToFile(streamWriter, item);
                    }
                }
            }
            finally
            {
                streamWriter.Close();
                streamReader.Close();
                newFileStream.Close();
                fileStream.Close();
            }
            File.Delete(this.path);
            File.Move(temp, this.path);
        }
        public T GetByID(int id)
        {
            
            T item = GetAll().Where(lambdaitem => lambdaitem.Id == id).FirstOrDefault();
            return item;
        }
        
    }
}
