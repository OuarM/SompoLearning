Console.Write("Enter vowell for us to count: ");
string text = Console.ReadLine().ToLower();

int count = 0;

foreach (char c in text)
{
    if ("aeiou".Contains(c))
    {
        count++;
    }
}

Console.WriteLine($"Number of vowels: {count}");