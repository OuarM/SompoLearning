List<string> books = new List<string>();

Console.Write("How many books would you like to add? ");
int count = Convert.ToInt32(Console.ReadLine());

for (int i = 0; i < count; i++)
{
    Console.Write("Book title: ");
    books.Add(Console.ReadLine());
}

Console.WriteLine("\nLibrary Inventory:");

foreach (string book in books)
{
    Console.WriteLine(book);
}