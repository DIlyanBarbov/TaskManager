using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace TaskManagerBarbov
{
    class TaskView
    {
        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("[C]reate:");
                Console.WriteLine("[G]et all:");
                Console.WriteLine("[V]iew task:");
                Console.WriteLine("[E]dit:");
                Console.WriteLine("[D]elete:");
                Console.WriteLine("E[x]it:");
                Console.Write("Press a key:");
                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "C":
                        {
                            Create();
                            break;
                        }
                    case "G":
                        {
                            GetAll();
                            break;
                        }
                    case "V":
                        {
                            ViewTask();
                            break;
                        }
                    case "E":
                        {
                            Edit();
                            break;
                        }
                    case "D":
                        {
                            Delete();
                            break;
                        }
                    case "X":
                        {
                            return;
                        }
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("Please select a valid option");
                            Console.ReadKey(true);
                            break;
                        }
                }
            }
        }
        private void Create()
        {
            Console.Clear();
            TaskRepository taskRepository = new TaskRepository("TaskDataBase.txt");
            TaskEntity taskEntity = new TaskEntity();
            Console.WriteLine("Press [ENTER] to start a new task:");
            Console.WriteLine("OR Press X to return: ");
            string choice = Console.ReadLine();
            bool result = Int32.TryParse(choice, out int id);
            if (choice.ToUpper() == "X")
            {
                return;
            }
            Console.Write("Enter a new name: ");
            taskEntity.Name = Console.ReadLine();
            Console.Write("Enter a new description: ");
            taskEntity.Description = Console.ReadLine();
            while (true)
            {

                Console.Write("Enter a new start time:");
                string startTimeEntered = Console.ReadLine();
                DateTime startTime;
                result = DateTime.TryParse(startTimeEntered, out startTime);

                if (DateTime.TryParse(startTimeEntered, out startTime))
                {
                    String.Format(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern, startTime);
                    taskEntity.StartTime = startTime;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid"); // <-- Control flow goes here
                }

                Console.WriteLine("Enter a valid start time!");

            }
            while (true)
            {
                Console.Write("Enter a new end time:");
                string endTimeEntered = Console.ReadLine();
                DateTime endTime;
                result = DateTime.TryParse(endTimeEntered, out endTime);

                if (DateTime.TryParse(endTimeEntered, out endTime))
                {
                    String.Format(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern, endTime);
                    taskEntity.EndTime = endTime;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid"); // <-- Control flow goes here
                }

                Console.WriteLine("Enter a valid end time!");

            }
            taskRepository.Save(taskEntity);
            Console.WriteLine("Task saved!");
            Console.WriteLine("Press enter to return:");
            Console.ReadKey(true);
        }


        private void GetAll()
        {
            Console.Clear();
            TaskRepository taskRepository = new TaskRepository("TaskDataBase.txt");
            Console.WriteLine("All tasks...");
            List<TaskEntity> taskList = taskRepository.GetAll();
            foreach (TaskEntity taskEntity in taskList)
            {
                Console.WriteLine("Task ID: " + taskEntity.Id);
                Console.WriteLine("Task Name: " + taskEntity.Name);
                Console.WriteLine("Task Description: " + taskEntity.Description);
                Console.WriteLine("Task Start Time: " + taskEntity.StartTime);
                Console.WriteLine("Task End Time: " + taskEntity.EndTime);
            }
            Console.WriteLine("Press enter to return:");
            Console.ReadKey(true);

        }
        private void Edit()
        {
            Console.Clear();
            TaskRepository taskRepository = new TaskRepository("TaskDataBase.txt");
            Console.Write("Which task do you want to edit: " + "[Enter ID]");
            Console.WriteLine("OR Press X to return: ");
            string choice = Console.ReadLine();
            bool result = Int32.TryParse(choice, out int id);
            if (choice.ToUpper() == "X")
            {
                return;
            }
            TaskEntity taskEntity = taskRepository.GetByID(id);
            if (taskEntity == null)
            {
                Console.WriteLine("Task not found!");
                Console.ReadKey(true);
                return;
            }

            taskEntity.Id = id;
            Console.Write("Enter a new name: ");
            taskEntity.Name = Console.ReadLine();
            Console.Write("Enter a new description:");
            taskEntity.Description = Console.ReadLine();

            while (true)
            {

                Console.Write("Enter a new start time:");
                string startTimeEntered = Console.ReadLine();
                DateTime startTime;
                result = DateTime.TryParse(startTimeEntered, out startTime);

                if (DateTime.TryParse(startTimeEntered, out startTime))
                {
                    String.Format(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern, startTime);
                    taskEntity.StartTime = startTime;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid");
                }

                Console.WriteLine("Enter a valid start time!");
            }
            while (true)
            {
                Console.Write("Enter a new end time:");
                string endTimeEntered = Console.ReadLine();
                DateTime endTime;
                result = DateTime.TryParse(endTimeEntered, out endTime);

                if (DateTime.TryParse(endTimeEntered, out endTime))
                {
                    String.Format(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern, endTime);
                    taskEntity.EndTime = endTime;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid");
                }

                Console.WriteLine("Enter a valid end time!");
            }
            taskRepository.Edit(taskEntity);
            Console.WriteLine("Task Edited!");
            Console.WriteLine("Press enter to return:");
            Console.ReadKey(true);

        }
        private void Delete()
        {
            Console.Clear();
            TaskRepository taskRepository = new TaskRepository("TaskDataBase.txt");
            Console.WriteLine("Which task do you want to delete:" + "[Enter ID]");
            Console.WriteLine("OR Press X to return:");
            string choice = Console.ReadLine();
            bool result = Int32.TryParse(choice, out int id);
            if (choice.ToUpper() == "X")
            {
                return;
            }
            TaskEntity taskEntity = taskRepository.GetByID(id);
            if (taskEntity == null)
            {
                Console.WriteLine("Task not found!");
                Console.ReadKey(true);
                return;
            }
            taskRepository.Delete(id);
            Console.WriteLine("Task deleted!");
            Console.WriteLine("Press enter to return:");
            Console.ReadKey(true);

        }
        private void ViewTask()
        {
            Console.Clear();
            TaskRepository taskRepository = new TaskRepository("TaskDataBase.txt");
            Console.Write("Which ask do you want to view: " + "[Enter ID]");
            Console.WriteLine("Or Press X to return:");
            string choice = Console.ReadLine();
            bool result = Int32.TryParse(choice, out int id);
            if (choice.ToUpper() == "X")
            {
                return;
            }
            TaskEntity taskEntity = taskRepository.GetByID(id);
            if (taskEntity == null)
            {
                Console.WriteLine("Task not found!");
                Console.ReadKey(true);
                return;
            }
            Console.WriteLine("Task ID: " + taskEntity.Id);
            Console.WriteLine("Task Name: " + taskEntity.Name);
            Console.WriteLine("Task Description: " + taskEntity.Description);
            Console.WriteLine("Task Start Time: " + taskEntity.StartTime);
            Console.WriteLine("Task End Time: " + taskEntity.EndTime);
            Console.WriteLine("Press enter to return:");
            Console.ReadKey(true);



        }
    }
}
