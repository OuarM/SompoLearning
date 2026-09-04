using System;
using System.Collections.Generic;
using System.Linq;

public interface ITaskManager
{
    void AddEmployee();
    void ViewEmployees();
    void AssignTask();
    void ViewTasks();
    void CompleteTask();
}

public class WorkTask
{
    public int TaskId { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<WorkTask> Tasks { get; set; } = new List<WorkTask>();
}

public class TaskManager : ITaskManager
{
    private List<Employee> employees = new List<Employee>();

    public void AddEmployee()
    {
        Console.Write("Employee ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        if (employees.Any(e => e.Id == id))
        {
            Console.WriteLine("Employee ID already exists.");
            return;
        }

        Console.Write("Employee Name: ");
        string name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Name cannot be empty.");
            return;
        }

        employees.Add(new Employee
        {
            Id = id,
            Name = name
        });

        Console.WriteLine("Employee added successfully.");
    }

    public void ViewEmployees()
    {
        if (employees.Count == 0)
        {
            Console.WriteLine("No employees found.");
            return;
        }

        foreach (Employee employee in employees)
        {
            Console.WriteLine($"ID: {employee.Id} | Name: {employee.Name}");
        }
    }

    public void AssignTask()
    {
        try
        {
            Console.Write("Employee ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Employee employee =
                employees.FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                Console.WriteLine("Employee not found.");
                return;
            }

            Console.Write("Task ID: ");
            int taskId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Task Description: ");
            string description = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Description cannot be empty.");
                return;
            }

            employee.Tasks.Add(new WorkTask
            {
                TaskId = taskId,
                Description = description,
                IsCompleted = false
            });

            Console.WriteLine("Task assigned.");
        }
        catch
        {
            Console.WriteLine("Invalid input.");
        }
    }

    public void ViewTasks()
    {
        Console.Write("Employee ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Employee employee =
            employees.FirstOrDefault(e => e.Id == id);

        if (employee == null)
        {
            Console.WriteLine("Employee not found.");
            return;
        }

        if (employee.Tasks.Count == 0)
        {
            Console.WriteLine("No tasks assigned.");
            return;
        }

        foreach (WorkTask task in employee.Tasks)
        {
            Console.WriteLine(
                $"Task ID: {task.TaskId} | {task.Description} | Completed: {task.IsCompleted}");
        }
    }

    public void CompleteTask()
    {
        Console.Write("Employee ID: ");

        if (!int.TryParse(Console.ReadLine(), out int employeeId))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Employee employee =
            employees.FirstOrDefault(e => e.Id == employeeId);

        if (employee == null)
        {
            Console.WriteLine("Employee not found.");
            return;
        }

        Console.Write("Task ID: ");

        if (!int.TryParse(Console.ReadLine(), out int taskId))
        {
            Console.WriteLine("Invalid task ID.");
            return;
        }

        WorkTask task =
            employee.Tasks.FirstOrDefault(t => t.TaskId == taskId);

        if (task == null)
        {
            Console.WriteLine("Task not found.");
            return;
        }

        task.IsCompleted = true;

        Console.WriteLine("Task marked as completed.");
    }
}

public class Program
{
    public static void Main()
    {
        TaskManager manager = new TaskManager();

        while (true)
        {
            Console.WriteLine("\n=== Employee Task Management System ===");
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. View Employees");
            Console.WriteLine("3. Assign Task");
            Console.WriteLine("4. View Employee Tasks");
            Console.WriteLine("5. Complete Task");
            Console.WriteLine("6. Exit");

            Console.Write("\nChoose an option: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Please enter a valid number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    manager.AddEmployee();
                    break;

                case 2:
                    manager.ViewEmployees();
                    break;

                case 3:
                    manager.AssignTask();
                    break;

                case 4:
                    manager.ViewTasks();
                    break;

                case 5:
                    manager.CompleteTask();
                    break;

                case 6:
                    Console.WriteLine("Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}