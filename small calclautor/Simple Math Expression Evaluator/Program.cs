using System;
using static ExpressionParser;

namespace Simple_Math_Expression_Evaluator
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t\t=====================================================");
            Console.WriteLine("\t\t\t\t======= Welcome to the Simple Math Expression =======");
            Console.WriteLine("\t\t\t\t=====================================================");
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Supported operations: +, -, *, /, %, ^ , sin , cos , tan,sqrt,pi");
            Console.ResetColor();

            while (true)
            {
                Console.Write("Enter expression : ");
                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                    break;

                try
                {
                    var parsed = ExpressionParser.ParseExpression1(input);

                    Console.WriteLine($"Parsed Expression: {parsed.Operation}");

                    double result = Evaluator.Evaluate(parsed);
                    Console.WriteLine($"Result = {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
