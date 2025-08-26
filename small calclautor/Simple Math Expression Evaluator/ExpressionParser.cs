using Simple_Math_Expression_Evaluator;
using System;
using System.Linq.Expressions;

internal class ExpressionParser
{
    private const string Operators = "+*/%^";
    private static MathExpression ParseAddSubtract(string expr, ref int index)
    {
        var left = ParseMultiplyDivide(expr, ref index);

        while (index < expr.Length)
        {
            char op = expr[index];
            if (op == '+' || op == '-')
            {
                index++;
                var right = ParseMultiplyDivide(expr, ref index);
                left = new MathExpression
                {
                    LeftSideOperand = left,
                    RightSideOperand = right,
                    Operation = (op == '+') ? MathOperations.Addition : MathOperations.Subtraction
                };
            }
            else break;
        }

        return left;
    }

    private static MathExpression ParseMultiplyDivide(string expr, ref int index)
    {
        var left = ParseFactor(expr, ref index);

        while (index < expr.Length)
        {
            char op = expr[index];
            if (op == '*' || op == '/')
            {
                index++;
                var right = ParseFactor(expr, ref index);
                left = new MathExpression
                {
                    LeftSideOperand = left,
                    RightSideOperand = right,
                    Operation = (op == '*') ? MathOperations.Multiplication : MathOperations.Division
                };
            }
            else break;
        }

        return left;
    }


    private static MathExpression ParseFactor(string expr, ref int index)
    {
        SkipSpaces(expr, ref index);

        if (index < expr.Length && (expr[index] == '+' || expr[index] == '-'))
        {
            char sign = expr[index++];
            var factor = ParseFactor(expr, ref index);
            if (sign == '-')
            {
                return new MathExpression
                {
                    LeftSideOperand = 0.0,
                    RightSideOperand = factor,
                    Operation = MathOperations.Subtraction
                };
            }
            return factor; 
        }
        if (expr.Substring(index).StartsWith("pi", StringComparison.OrdinalIgnoreCase))
        {
            index += 2;
            return new MathExpression { LeftSideOperand = Math.PI };
        }

        if (index < expr.Length && char.IsLetter(expr[index]))
        {
            string func = "";
            while (index < expr.Length && char.IsLetter(expr[index]))
            {
                func += expr[index++];
            }

            var operation = ParseMathOperation(func);

            SkipSpaces(expr, ref index);

            MathExpression arg;
            if (index < expr.Length && expr[index] == '(')
            {
                index++; 
                arg = ParseAddSubtract(expr, ref index);
                if (index >= expr.Length || expr[index] != ')')
                    throw new Exception("Missing closing parenthesis after function argument");
                index++; 
            }
            else
            {
                arg = ParseFactor(expr, ref index);
            }

            return new MathExpression
            {
                Operation = operation,
                RightSideOperand = arg
            };
        }

        if (index < expr.Length && expr[index] == '(')
        {
            index++; 
            var inner = ParseAddSubtract(expr, ref index);
            if (index >= expr.Length || expr[index] != ')')
                throw new Exception("Missing closing parenthesis");
            index++; 
            return inner;
        }
       

        string number = "";
       
        while (index < expr.Length && (char.IsDigit(expr[index]) || expr[index] == '.'))
        {
            number += expr[index++];
        }

        if (number != "")
        {
            return new MathExpression { LeftSideOperand = double.Parse(number) };
        }

        throw new Exception($"Unexpected char {expr[index]}");
    }

    private static void SkipSpaces(string expr, ref int index)
    {
        while (index < expr.Length && char.IsWhiteSpace(expr[index]))
            index++;
    }
    public static MathExpression ParseExpression(string expression)
    {
        int index = 0;
        return ParseAddSubtract(expression, ref index);
    }
    public static MathOperations ParseMathOperation(string token)
    {
        switch (token.ToLower())
        {
            case "+": return MathOperations.Addition;
            case "-": return MathOperations.Subtraction;
            case "*": return MathOperations.Multiplication;
            case "/": return MathOperations.Division;
            case "%":
            case "mod": return MathOperations.Modulus;
            case "^":
            case "pow": return MathOperations.Exponentiation;
            case "sin": return MathOperations.Sine;
            case "cos": return MathOperations.Cosine;
            case "tan": return MathOperations.Tangent;
            case "sqrt": return MathOperations.SquareRoot;   
            default: return MathOperations.None;
        }
    }

    internal static MathExpression ParseExpression1(string input)
    {
        int index = 0;
        return ParseAddSubtract(input, ref index);
    }

}
