Console.Write("Enter text: ");
string text = Console.ReadLine();

string reversed = new string(text.Reverse().ToArray());

Console.WriteLine($"Reversed: {reversed}");