Random random = new Random();
int secretNumber = random.Next(1, 101);

Console.WriteLine("Guess a number between 1 and 100");

while (true)
{
    int guess = Convert.ToInt32(Console.ReadLine());

    if (guess < secretNumber)
    {
        Console.WriteLine("Too low!");
    }
    else if (guess > secretNumber)
    {
        Console.WriteLine("Too high!");
    }
    else
    {
        Console.WriteLine("Correct!");
        break;
    }
}