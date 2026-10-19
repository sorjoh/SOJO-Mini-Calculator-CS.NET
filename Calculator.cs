
namespace SOJO_Mini_Calculator
{
    /// <summary>
    /// **********************************************************************************************************************************************
    /// 
    /// Calculator Class
    /// 
    /// **********************************************************************************************************************************************
    /// </summary>

    public class Calculator
   {
        /// <summary>
        /// ========= OPERATORS =========
        /// </summary>
        public enum Operator
        {
            PLUS,
            MINUS,
            MULTIPLY,
            DIVIDE,
            NONE
        }

        /// <summary>
        /// ========= GET OPERATOR SIGN =========        
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        public static string GetOperationSign(Operator op)
        {
            return op switch {
                Operator.PLUS => "+",
                Operator.MINUS => "-",
                Operator.MULTIPLY => "×",
                Operator.DIVIDE => "÷",
                Operator.NONE => "",
                _ => ""
            };
        }

        /// <summary>
        /// ========= GET OPERATOR =========
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Operator GetOperation(string key)
        {
            return key switch {
                "+" => Operator.PLUS,
                "−" or "-" => Operator.MINUS,
                "×" or "*" or "x" or "X" => Operator.MULTIPLY,
                "÷" or "/" => Operator.DIVIDE,
                _ => Operator.NONE
            };
        }

        /// <summary>
        /// ========= CALCULATE RESULT =========
        /// </summary>
        /// <param name="firstValue"></param>
        /// <param name="secondValue"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public static double Calculate(double firstValue, double secondValue, Operator operation)
        {
            return operation switch {
                Operator.PLUS => firstValue + secondValue,
                Operator.MINUS => firstValue - secondValue,
                Operator.MULTIPLY => firstValue * secondValue,
                Operator.DIVIDE => firstValue / secondValue,
                _ => firstValue
            };            
        }
    }
}