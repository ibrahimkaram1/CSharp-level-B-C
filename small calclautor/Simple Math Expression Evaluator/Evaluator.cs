using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Math_Expression_Evaluator
{
    public class Evaluator
    {

        public static double Evaluate(MathExpression expr)
        {
            double left = GetValue(expr.LeftSideOperand);
            double right = GetValue(expr.RightSideOperand);

            switch (expr.Operation)
            {
                case MathOperations.Addition: return left + right;
                case MathOperations.Subtraction: return left - right;
                case MathOperations.Multiplication: return left * right;
                case MathOperations.Division:
                    if (right == 0) throw new DivideByZeroException("Division by zero.");
                    return left / right;
                case MathOperations.Modulus: return left % right;
                case MathOperations.Exponentiation: return Math.Pow(left, right);
                case MathOperations.Sine: return Math.Sin(ToRadians(right));
                case MathOperations.Cosine: return Math.Cos(ToRadians(right));
                case MathOperations.Tangent: return Math.Tan(ToRadians(right));
                case MathOperations.SquareRoot: return Math.Sqrt(right);
                case MathOperations.None: return left;
                default: throw new InvalidOperationException("Invalid operation.");
            }
        }

        private static double GetValue(object operand)
        {
            if (operand == null) return 0;
            if (operand is double d) return d;
            if (operand is MathExpression e) return Evaluate(e);
            throw new InvalidOperationException("Unknown operand type.");
        }

        private static double ToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }

}
