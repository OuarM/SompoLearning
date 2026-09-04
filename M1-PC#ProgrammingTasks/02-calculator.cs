Console.Write("Would you like to add, subtract or multiply?");
string op = Convert.ToString(Console.ReadLine());
if (op == "add")
{
Console.Write("Enter first number: ");
double num1 = Convert.ToDouble(Console.ReadLine());
Console.Write("Enter second number: ");
double num2 = Convert.ToDouble(Console.ReadLine());
Console.WriteLine($"Sum = {num1 + num2}");
return;
}
else if (op == "subtract")
{
Console.Write("Enter first number: ");
double num1 = Convert.ToDouble(Console.ReadLine());
Console.Write("Enter second number: ");
double num2 = Convert.ToDouble(Console.ReadLine());
Console.WriteLine($"Sum = {num1 - num2}");
return;    
}
else if (op == "multiply")
{
Console.Write("Enter first number: ");
double num1 = Convert.ToDouble(Console.ReadLine());
Console.Write("Enter second number: ");
double num2 = Convert.ToDouble(Console.ReadLine());
Console.WriteLine($"Sum = {num1 * num2}");   
}
else {Console.Write("Sorry you have provided an incorrect input "); }