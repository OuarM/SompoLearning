Console.Write("How many numbers would you like to enter? ");
int size = Convert.ToInt32(Console.ReadLine());

int[] numbers = new int[size];

for (int i = 0; i < size; i++)
{
    Console.Write($"Enter number {i + 1}: ");
    numbers[i] = Convert.ToInt32(Console.ReadLine());
}

double average = numbers.Average();

Console.WriteLine($"Average = {average}");
