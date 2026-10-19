
namespace SOJO_Mini_Calculator
{
    public class Calculator
    {
        // ========= OPERATORS =========
        public enum Operators
        {
            PLUS,
            MINUS,
            MULTIPLY,
            DIVIDE,

            NONE
        }


        // ========= GET OPERATOR SIGN =========
        public static object getOperationSign(Operators op)
        {
            string sign = "";

            switch (op)
            {
                case Operators.PLUS:
                    {
                        sign = "+";
                        break;
                    }
                case Operators.MINUS:
                    {
                        sign = "-";
                        break;
                    }
                case Operators.MULTIPLY:
                    {
                        sign = "×";
                        break;
                    }
                case Operators.DIVIDE:
                    {
                        sign = "÷";
                        break;
                    }
            }

            return sign;
        }


        // ========= GET OPERATOR =========
        public static object getOperation(string key)
        {
            Operators operation;

            switch (key ?? "")
            {
                case "+":
                    {
                        operation = Operators.PLUS;
                        break;
                    }
                case "−":
                case "-":
                    {
                        operation = Operators.MINUS;
                        break;
                    }
                case "×":
                case "*":
                case "x":
                case "X":
                    {
                        operation = Operators.MULTIPLY;
                        break;
                    }
                case "÷":
                case "/":
                    {
                        operation = Operators.DIVIDE;
                        break;
                    }

                default:
                    {
                        operation = Operators.NONE;
                        break;
                    }
            }

            return operation;
        }


        // ========= CALCULATE RESULT =========
        public static double Calculate(double firstValue, double secondValue, Operators operation)
        {
            double result = 0d;

            switch (operation)
            {
                case Operators.PLUS:
                    {
                        result = firstValue + secondValue;
                        break;
                    }
                case Operators.MINUS:
                    {
                        result = firstValue - secondValue;
                        break;
                    }
                case Operators.MULTIPLY:
                    {
                        result = firstValue * secondValue;
                        break;
                    }
                case Operators.DIVIDE:
                    {
                        result = firstValue / secondValue;
                        break;
                    }

                default:
                    {
                        result = firstValue;
                        break;
                    }
            }

            return result;
        }

    }
}