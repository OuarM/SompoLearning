Console.Write("Enter a word: ");
string word = Console.ReadLine();

string reversed = new string(word.Reverse().ToArray());

if (word.ToLower() == reversed.ToLower())
{
    Console.WriteLine("Palindrome");
}
else
{
    Console.WriteLine("Not a palindrome");
}
