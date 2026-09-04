using System;
using System.Collections.Generic;

class Employee
{
    public string Name { get; set; }
    public decimal Salary { get; set; }
}

class Program
{
    static void Main()
    {
        List<Employee> employees = new List<Employee>();

        Console.Write("How many employees? ");
        int count = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < count; i++)
        {
            Employee employee = new Employee();

            Console.Write("Employee Name: ");
            employee.Name = Console.ReadLine();

            Console.Write("Salary: ");
            employee.Salary = Convert.ToDecimal(Console.ReadLine());

            employees.Add(employee);
        }

        Console.WriteLine("\nEmployee List:");

        foreach (Employee employee in employees)
        {
            Console.WriteLine($"{employee.Name} - £{employee.Salary}");
        }
    }
}