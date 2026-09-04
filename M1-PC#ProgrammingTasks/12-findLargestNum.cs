Console.Write("How many numbers will you enter? ");
int size = Convert.ToInt32(Console.ReadLine());

int[] numbers = new int[size];

for (int i = 0; i < size; i++)
{
    Console.Write($"Enter number {i + 1}: ");
    numbers[i] = Convert.ToInt32(Console.ReadLine());
}

int largest = numbers.Max();

Console.WriteLine($"Largest number = {largest}");