using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Math_Expression_Evaluator
{
    public class MathExpression
    {
        public object LeftSideOperand { get; set; }
        public object RightSideOperand { get; set; }
        public MathOperations Operation { get; set; } = MathOperations.None;
    }
}
