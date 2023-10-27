using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Challange02600234
{
    internal class Program
    {
        
        enum MenuOptions
        {
            AddTask=1,
            EditTask,
            SearchTask,
            Exit
        }
        public class Task
        {
            public string TaskName { get; set; }
            public bool CompletionStatus { get; set; }
            public DateTime DueDate { get; set; }
        }

            public List<Task> toDoList = new List<Task>();

            public bool AddTask(Task task)
            {
                toDoList.Add(task);
                return true;
            }

            public bool EditTask(int index, Task newTask)
            {
                if (index >= 0 && index < toDoList.Count)
                {
                    toDoList[index] = newTask;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public bool SearchTask(string taskName)
            {
                int index = toDoList.FindIndex(task => task.TaskName == taskName);
                if (index != -1)
                {
                    Console.WriteLine("Task found at index: " + index);
                    return true;
                }
                else
                {
                    Console.WriteLine("Task not found.");
                    return false;
                }
            }
        public void DisplayTasks()
        {
            if (toDoList.Count > 0)
            {
                Console.WriteLine("To-do List:");
                for (int i = 0; i < toDoList.Count; i++)
                {
                    Console.WriteLine("- " + toDoList[i].TaskName + " (Due: " + toDoList[i].DueDate.ToShortDateString() + ")");
                }
            }
            else
            {
                Console.WriteLine("You currently have no tasks in your To-do list.");
            }
        }
        public void SaveTasks()
        {
            if (toDoList.Count > 0)
            {
                string jsonString = JsonSerializer.Serialize(toDoList);
                File.WriteAllText("tasks.json", jsonString);
            }
        }

        public void LoadTasks()
        {
            if (File.Exists("tasks.json"))
            {
                string jsonString = File.ReadAllText("tasks.json");
                toDoList = JsonSerializer.Deserialize<List<Task>>(jsonString);
            }
        }


        static void Main(string[] args)
        {
            bool choose = true;
            Program toDoList = new Program();
            while (choose)
            {
                Console.WriteLine("1. Add task");
                Console.WriteLine("2. Edit task");
                Console.WriteLine("3. Search task");
                Console.WriteLine("4. Exit");

                string userChoice = Console.ReadLine();
                MenuOptions option = (MenuOptions)Enum.Parse(typeof(MenuOptions), userChoice);

                switch (option)
                {
                    case MenuOptions.AddTask:
                        Console.WriteLine("Enter task name:");
                        string taskName = Console.ReadLine();
                        Console.WriteLine("Enter due date (yyyy-mm-dd):");
                        DateTime dueDate = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Is the task completed? (y/n)");
                        bool isCompleted = Boolean.Parse(Console.ReadLine());
                        bool addResult = toDoList.AddTask(new Task { TaskName = taskName, CompletionStatus = isCompleted, DueDate = dueDate });
                        if (addResult)
                        {
                            Console.WriteLine("Task added successfully.");
                            toDoList.SaveTasks();
                        }
                        else
                        {
                            Console.WriteLine("Failed to add task.");
                        }
                        break;
                    case MenuOptions.EditTask:
                        Console.WriteLine("Enter the index of the task to edit:");
                        int editIndex = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Enter new task name:");
                        string newTaskName = Console.ReadLine();
                        Console.WriteLine("Enter new due date (yyyy-mm-dd):");
                        DateTime newDueDate = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Is the task completed? (y/n)");
                        bool newIsCompleted = Boolean.Parse(Console.ReadLine());
                        bool editResult = toDoList.EditTask(editIndex, new Task { TaskName = newTaskName, CompletionStatus = newIsCompleted, DueDate = newDueDate });
                        if (editResult)
                        {
                            Console.WriteLine("Task edited successfully.");
                            toDoList.SaveTasks();
                        }
                        else
                        {
                            Console.WriteLine("Failed to edit task.");
                        }
                        break;
                    case MenuOptions.SearchTask:
                        Console.WriteLine("Enter task name to search:");
            string searchTaskName = Console.ReadLine();
            bool searchResult = toDoList.SearchTask(searchTaskName);
            if (searchResult)
            {
                Console.WriteLine("Task found.");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }
            break;
                    case MenuOptions.Exit:
                        
                        toDoList.SaveTasks();
                        choose = false;
                        return;
                }
            }

        }
    }
}
