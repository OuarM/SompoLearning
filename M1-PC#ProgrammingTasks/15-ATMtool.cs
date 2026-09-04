double balance = 1000;

Console.WriteLine($"Current Balance: £{balance}");

Console.Write("Enter withdrawal amount: ");
double withdrawal = Convert.ToDouble(Console.ReadLine());

if (withdrawal <= balance)
{
    balance -= withdrawal;
    Console.WriteLine($"New Balance: £{balance}");
}
else
{
    Console.WriteLine("Insufficient funds.");
}