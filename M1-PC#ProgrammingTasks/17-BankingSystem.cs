double balance = 0;

Console.Write("Enter starting balance: £");
balance = Convert.ToDouble(Console.ReadLine());

while (true)
{
    Console.WriteLine("\n1. Deposit");
    Console.WriteLine("2. Withdraw");
    Console.WriteLine("3. Check Balance");
    Console.WriteLine("4. Exit");

    Console.Write("Choose an option: ");
    int option = Convert.ToInt32(Console.ReadLine());

    switch (option)
    {
        case 1:
            Console.Write("Deposit amount: £");
            balance += Convert.ToDouble(Console.ReadLine());
            break;

        case 2:
            Console.Write("Withdraw amount: £");
            double withdrawal = Convert.ToDouble(Console.ReadLine());

            if (withdrawal <= balance)
            {
                balance -= withdrawal;
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
            }
            break;

        case 3:
            Console.WriteLine($"Balance: £{balance}");
            break;

        case 4:
            return;

        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}