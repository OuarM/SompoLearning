double total = 0;

Console.Write("How many expenses? ");
int count = Convert.ToInt32(Console.ReadLine());

for (int i = 0; i < count; i++)
{
    Console.Write($"Expense {i + 1}: £");
    double expense = Convert.ToDouble(Console.ReadLine());

    total += expense;
}

Console.WriteLine($"Total Expenses: £{total}");