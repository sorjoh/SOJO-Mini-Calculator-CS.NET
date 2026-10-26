
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
        // Status indicator if equal button or another calculation has presented a total result
        public bool hasResult;

        // Variables holding current operations
        public Calculator.Operator currentOperator = Calculator.Operator.NONE;
        public bool hasOperator;

        // Variables holding the result and input values
        public double resultValue;
        public double firstValue;
        public double secondValue;

        // Variables holding the input and output text
        public string inputValue;
        public string outputValue;

        // Variable for checking if equal has been pressed or not
        public bool isEqual;

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
        public string GetOperationSign(Operator op)
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
        public Operator GetOperation(string key)
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
        public double Calculate(double firstValue, double secondValue, Operator operation)
        {
            return operation switch {
                Operator.PLUS => firstValue + secondValue,
                Operator.MINUS => firstValue - secondValue,
                Operator.MULTIPLY => firstValue * secondValue,
                Operator.DIVIDE => firstValue / secondValue,
                _ => firstValue
            };            
        }

        // ========= Reset Values =========
        public void ResetValues()
        {
            // Status indicator if equal button or another calculation has presented a total result
            hasResult = false;

            // Variables holding current operations
            currentOperator = Calculator.Operator.NONE;
            hasOperator = false;

            // Variables holding the result and input values
            resultValue = 0.0d;
            firstValue = 0.0d;
            secondValue = 0.0d;

            // Variables holding the input and output text
            inputValue = "";
            outputValue = "";

            // Variable for checking if equal has been pressed or not
            isEqual = false; 
        }
    }
}