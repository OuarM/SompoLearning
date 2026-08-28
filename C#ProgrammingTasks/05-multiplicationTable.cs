Console.Write("Enter a number and I will display its multiplication table: ");
int num = Convert.ToInt32(Console.ReadLine());

for (int i = 1; i <= 10; i++)
{
 
Console.WriteLine($"{num} x {i} = {num} * {i}");
}