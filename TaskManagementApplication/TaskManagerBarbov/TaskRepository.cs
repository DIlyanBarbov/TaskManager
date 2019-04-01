using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TaskManagerBarbov
{
    public class TaskRepository : BaseRepository<TaskEntity> 
    {
        public TaskRepository(string path) : base(path)
        {
        }

        public override void ReadFromFile(StreamReader streamReader,TaskEntity task)
        {
            
            task.Name = (streamReader.ReadLine());
            task.Description = (streamReader.ReadLine());
            task.StartTime = Convert.ToDateTime(streamReader.ReadLine());
            task.EndTime = Convert.ToDateTime(streamReader.ReadLine());
        }
        public override void WriteToFile(StreamWriter streamWriter,TaskEntity task)
        {
            streamWriter.WriteLine(task.Name);
            streamWriter.WriteLine(task.Description);
            streamWriter.WriteLine(task.StartTime);
            streamWriter.WriteLine(task.EndTime);
        }
    }
}
