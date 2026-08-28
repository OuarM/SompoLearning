List<string> tasks = new List<string>();

Console.Write("How many tasks would you like to add? ");
int count = Convert.ToInt32(Console.ReadLine());

for (int i = 0; i < count; i++)
{
    Console.Write($"Task {i + 1}: ");
    tasks.Add(Console.ReadLine());
}

Console.WriteLine("\nYour Tasks:");

foreach (string task in tasks)
{
    Console.WriteLine($"- {task}");
}