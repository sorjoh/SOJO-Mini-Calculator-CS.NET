using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using static SOJO_Mini_Calculator.Calculator;

namespace SOJO_Mini_Calculator
{
    // **********************************************************************************************************************************************
    // 
    // SOJO Mini Calculator
    // 
    // inspired from https://ecdn.teacherspayteachers.com/thumbitem/Calculator-Functions-Anchor-Chart-11020447-1707050000/original-11020447-1.jpg
    // 
    // **********************************************************************************************************************************************

    // ========= MAIN FORM =========
    public partial class Main
    {
        // Use debugging if needed
        private const bool _DEBUG = true;

        // Use rounded controls or not
        private const bool ROUNDED_BUTTON = true;
        private const bool ROUNDED_DISPLAY = true;

        // Background and text colors
        private const int CALCULATOR_BACKGROUND_COLOR = unchecked((int)0xFF585A67);

        private const int DISPLAY_BACKGROUND_COLOR = unchecked((int)0xFFC1D797);
        private const int DISPLAY_TEXT_COLOR = unchecked((int)0xFFFDFFFE);

        private const int NUMBER_BACKGROUND_COLOR = unchecked((int)0xFF3A3D46);
        private const int NUMBER_TEXT_COLOR = unchecked((int)0xFFFDFFFE);

        private const int FUNCTION_BACKGROUND_COLOR = unchecked((int)0xFFBABEC1);
        private const int FUNCTION_TEXT_COLOR = unchecked((int)0xFFFDFFFE);

        // Max length of the display numbers
        private const int MAX_OUTPUT_LENGTH = 13;

        // Variable for Calculator methods and variables
        Calculator calc;

        // Get decimal separator
        private static readonly string DECIMAL_SEPARATOR = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        /// <summary>
        /// 
        /// </summary>
        public Main()
        {
            InitializeComponent();
        }

        // ========= MAIN FORM - Control Added =========
        private void Main_ControlAdded(object sender, ControlEventArgs e)
        {
            // Center application on the current screen
            StartPosition = FormStartPosition.CenterScreen;
        }


        // ========= MAIN FORM - Load =========
        private void Main_Load(object sender, EventArgs e)
        {
            ActiveControl = null;

            // Change period symbol for current language
            Dot.Text = DECIMAL_SEPARATOR;

            // Change background color for the main form
            BackColor = Color.FromArgb(CALCULATOR_BACKGROUND_COLOR);
            DisplayTable.BackColor = Color.FromArgb(CALCULATOR_BACKGROUND_COLOR);

            // Change color and make the display rounded if configured
            Display.BackColor = Color.FromArgb(DISPLAY_BACKGROUND_COLOR);
            Display.ForeColor = Color.FromArgb(DISPLAY_TEXT_COLOR);

            if (ROUNDED_DISPLAY)
                RoundedDisplay(Display);

            // Change color and make all controls rounded if configured
            foreach (Control cntrl in Controls)
            {
                if (cntrl is Button button) {
                    if (cntrl.Tag.ToString().ToLower() == "number") {
                        cntrl.BackColor = Color.FromArgb(NUMBER_BACKGROUND_COLOR);
                        cntrl.ForeColor = Color.FromArgb(NUMBER_TEXT_COLOR);
                    } else if (cntrl.Tag.ToString().ToLower() == "function") {
                        cntrl.BackColor = Color.FromArgb(FUNCTION_BACKGROUND_COLOR);
                        cntrl.ForeColor = Color.FromArgb(FUNCTION_TEXT_COLOR);
                    }

                    if (ROUNDED_BUTTON)
                        RoundedButton((Button)button);
                }                
            }

            // Initialize Calculator
            calc = new Calculator();

            // Set the operator sign for operator buttons
            Plus.Text = Conversions.ToString(calc.GetOperationSign(Calculator.Operator.PLUS));
            Minus.Text = Conversions.ToString(calc.GetOperationSign(Calculator.Operator.MINUS));
            Multiply.Text = Conversions.ToString(calc.GetOperationSign(Calculator.Operator.MULTIPLY));
            Divide.Text = Conversions.ToString(calc.GetOperationSign(Calculator.Operator.DIVIDE));

            calc.ResetValues();
            
            DisplayOutput();
        }


        // ========= NUMBER INPUT =========
        private void NumberInput_Click(object sender, EventArgs e)
        {
            // Get the current selected button
            Button btn = (Button)sender;

            if (_DEBUG)                
                Debug.Print("Number(before) : button=" + btn.Text + ", inputValue=" + calc.inputValue + ", outputValue=" + calc.outputValue + ", Display=" + Display.Text + ", currentOperation=" + calc.GetOperationSign(calc.currentOperator) + ", hasResult=" + calc.hasResult + ", hasOperator=" + calc.hasOperator);

            // Get the number
            string inputKey = btn.Text;

            if (calc.hasResult) { // Calculation has result
                // Reset all values
                calc.ResetValues();
                // Add the first number to inputValue
                calc.inputValue = inputKey;
                // Replace outputValue with the newly and initialized inputValue
                calc.outputValue = calc.inputValue; 
            } else if (calc.hasOperator) { // Calculation has operator
                // Get first and second value from the output
                string[] splitOutputValue = calc.outputValue.Split(Conversions.ToChar(calc.GetOperationSign(calc.currentOperator)));

                // If inputValue is zero then replace inputValue with the number, otherwise add the new number
                calc.inputValue = calc.inputValue == "0" ? inputKey : calc.inputValue + inputKey;

                // Set current result initially as the first split value from the outputValue
                string currentResultValue = splitOutputValue[0];

                // Add minus sign in the beginning if current result value is empty and operator is minus 
                if (calc.currentOperator == Calculator.Operator.MINUS & string.IsNullOrEmpty(currentResultValue)) {                    
                    currentResultValue = calc.GetOperationSign(Calculator.Operator.MINUS) + splitOutputValue[1];
                }

                // Replace output value with the temporary current result value together with current operator sign and input value
                calc.outputValue = currentResultValue + calc.GetOperationSign(calc.currentOperator) + calc.inputValue;
            } else {
                // Otherwise add the number to input value
                calc.inputValue += inputKey;
                // Replace output value with the new input value
                calc.outputValue = calc.inputValue;
            }

            // Equal button has not been pressed
            calc.isEqual = false;

            // Display the output
            DisplayOutput(calc.outputValue);

            if (_DEBUG)        
                Debug.Print("Number(after) : button=" + inputKey + ", inputValue=" + calc.inputValue + ", outputValue=" + calc.outputValue + ", Display=" + Display.Text + ", currentOperation=" + calc.GetOperationSign(calc.currentOperator) + ", hasResult=" + calc.hasResult + ", hasOperator=" + calc.hasOperator);
        }


        // ========= DECIMAL INPUT =========
        private void DecimalInput_Click(object sender, EventArgs e)
        {
            if (_DEBUG)        
                Debug.Print("Decimal(before) : inputValue=" + calc.inputValue + ", outputValue=" + calc.outputValue + ", Display=" + Display.Text + ", currentOperation=" + calc.GetOperationSign(calc.currentOperator) + ", hasResult=" + calc.hasResult + ", hasOperator=" + calc.hasOperator);

            if (calc.hasResult) { // Calculation has result
                // Reset all values
                calc.ResetValues();
            } else { // Calculation has no result
                // Get first and second value from the output
                string[] splitOutputValue = calc.outputValue.Split(Conversions.ToChar(calc.GetOperationSign(calc.currentOperator)));

                if (calc.hasOperator) { // Calculation has operator
                    // Set output value to the first split output value plus the current operator sign
                    calc.outputValue = splitOutputValue[0] + calc.GetOperationSign(calc.currentOperator);
                    // Set input value to the second split output value
                    calc.inputValue = splitOutputValue[1];
                } else { // Otherwise if calculation has no operator
                    // Set output value to a empty string
                    calc.outputValue = "";

                    // If equal has been pressed before this button then replace inputValue with the a empty string, otherwise add the first split value                                        
                    calc.inputValue = calc.isEqual ? "" : splitOutputValue[0];
                }

                // Check if input value already contains a decimal otherwise add the decimal separator
                if (!calc.inputValue.Contains(DECIMAL_SEPARATOR)) {
                    calc.inputValue += DECIMAL_SEPARATOR;
                }
            }

            // Add a zero before the decimal separator if the input value begins with a decimal
            if (calc.inputValue.StartsWith(DECIMAL_SEPARATOR))
                calc.inputValue = "0" + calc.inputValue;

            // Add the decimal and the result of input value to the output value
            calc.outputValue += calc.inputValue;

            // Equal button has not been pressed
            calc.isEqual = false;


            // Display the output
            DisplayOutput(calc.outputValue);

            if (_DEBUG)   
                Debug.Print("Decimal(after) : inputValue=" + calc.inputValue + ", outputValue=" + calc.outputValue + ", Display=" + Display.Text + ", currentOperation=" + calc.GetOperationSign(calc.currentOperator) + ", hasResult=" + calc.hasResult + ", hasOperator=" + calc.hasOperator);
        }


        // ========= OPERATOR INPUT =========
        private void OperatorInput_Click(object sender, EventArgs e)
        {
            // Get the current selected button
            Button btn = (Button)sender;

            if (_DEBUG)        
                Debug.Print("Operator(before) : button=" + btn.Text + ", inputValue=" + calc.inputValue + ", outputValue=" + calc.outputValue + ", Display=" + Display.Text + ", currentOperation=" + calc.GetOperationSign(calc.currentOperator) + ", hasResult=" + calc.hasResult + ", hasOperator=" + calc.hasOperator);

            var previousOperator = calc.currentOperator;

            if (calc.hasOperator) {
                string[] splitOutputValue = calc.outputValue.Split(Conversions.ToChar(calc.GetOperationSign(previousOperator)));

                calc.ResetValues();

                calc.outputValue = splitOutputValue[0];
                calc.inputValue = splitOutputValue[1];

                if (previousOperator == Calculator.Operator.MINUS & string.IsNullOrEmpty(calc.outputValue)) {
                    calc.outputValue = calc.GetOperationSign(Calculator.Operator.MINUS) + splitOutputValue[1];
                    calc.inputValue = splitOutputValue[2];
                }

                double.TryParse(calc.outputValue, out calc.firstValue);
                double.TryParse(calc.inputValue, out calc.secondValue);               

                // Fix for example 12 * %
                if (calc.secondValue != 0.0)
                    calc.resultValue = calc.Calculate(calc.firstValue, calc.secondValue, previousOperator);
                else
                    calc.resultValue = calc.firstValue;

                calc.currentOperator = calc.GetOperation(btn.Text);

                calc.inputValue = "";
                calc.outputValue = calc.resultValue + calc.GetOperationSign(calc.currentOperator);
            } else if (string.IsNullOrEmpty(calc.outputValue)) {
                calc.ResetValues();

                calc.currentOperator = calc.GetOperation(btn.Text);

                calc.inputValue = "0";
                calc.outputValue = calc.inputValue + calc.GetOperationSign(calc.currentOperator);
            } else {
                calc.currentOperator = calc.GetOperation(btn.Text);

                calc.inputValue = "";
                calc.outputValue = calc.outputValue + calc.GetOperationSign(calc.currentOperator);
            }

            calc.hasOperator = true;

            calc.isEqual = false; // Fix for example 2.3 + 5 = .

            DisplayOutput(calc.outputValue);

            if (_DEBUG)
                Debug.Print("Operator(after) : button=" + btn.Text + ", resultValue=" + calc.resultValue + ", firstValue=" + calc.firstValue + ", secondValue=" + calc.secondValue + ", inputValue=" + calc.inputValue + ", outputValue=" + calc.outputValue + ", Display=" + Display.Text + ", currentOperation=" + calc.GetOperationSign(calc.currentOperator) + ", hasResult=" + calc.hasResult + ", hasOperator=" + calc.hasOperator);
        }


        // ========= EQUAL BUTTON =========
        private void Equal_Click(object sender, EventArgs e)
        {
            if (_DEBUG)
                Debug.Print("Equal(before) : inputValue=" + calc.inputValue + ", outputValue=" + calc.outputValue + ", Display=" + Display.Text + ", currentOperation=" + calc.GetOperationSign(calc.currentOperator) + ", hasResult=" + calc.hasResult);

            string[] splitOutputValue = calc.outputValue.Split(Conversions.ToChar(calc.GetOperationSign(calc.currentOperator)));

            calc.outputValue = splitOutputValue[0];

            try {
                if (calc.currentOperator == Calculator.Operator.MINUS & string.IsNullOrEmpty(calc.outputValue)) {
                    calc.outputValue = calc.GetOperationSign(Calculator.Operator.MINUS) + splitOutputValue[1];
                    calc.inputValue = splitOutputValue[2];
                } else {
                    calc.inputValue = splitOutputValue[1];
                }
            } catch (Exception) {
                String tmpOutputValue;

                // Fix for example 22 =
                if (!calc.isEqual)
                    tmpOutputValue = calc.outputValue;
                else
                    tmpOutputValue = "";

                calc.ResetValues();

                calc.outputValue = tmpOutputValue;
            }

            double.TryParse(calc.outputValue, out calc.firstValue);
            double.TryParse(calc.inputValue, out calc.secondValue);

            if (calc.hasOperator)
                calc.resultValue = calc.Calculate(calc.firstValue, calc.secondValue, calc.currentOperator);
            else
                double.TryParse(calc.outputValue, out calc.resultValue);

            calc.inputValue = "";
            calc.outputValue = calc.resultValue.ToString();

            calc.currentOperator = Calculator.Operator.NONE;
            calc.hasOperator = false;
            calc.hasResult = false;

            calc.isEqual = true; // Fix for example 2.3 + 5 = .

            DisplayOutput(calc.outputValue);

            if (_DEBUG)
                Debug.Print("Equal(before) : inputValue=" + calc.inputValue + ", outputValue=" + calc.outputValue + ", Display=" + Display.Text + ", currentOperation=" + calc.GetOperationSign(calc.currentOperator) + ", hasResult=" + calc.hasResult);
        }


        // ========= KEYBOARD INPUT =========
        private void Main_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;

            var btn = new Button();

            // ===== DIGITS & DECIMAL =====
            if (char.IsDigit(key)) {
                btn.Text = Conversions.ToString(key);
                NumberInput_Click(btn, EventArgs.Empty);

                e.Handled = true;
                return;
            } else if (Conversions.ToString(key) == DECIMAL_SEPARATOR) {
                btn.Text = DECIMAL_SEPARATOR;

                DecimalInput_Click(btn, EventArgs.Empty);

                e.Handled = true;
                return;
            }

            // ===== OPERATORS =====
            switch (key) {
                case '+': case '-':  case '*': case '/': {
                    btn.Text = key switch {
                        '+' => calc.GetOperationSign(Calculator.Operator.PLUS),
                        '-' => calc.GetOperationSign(Calculator.Operator.MINUS),
                        '*' => calc.GetOperationSign(Calculator.Operator.MULTIPLY),
                        '/' => calc.GetOperationSign(Calculator.Operator.DIVIDE),
                        _ => btn.Text = ""
                    };

                    OperatorInput_Click(btn, EventArgs.Empty);
                    e.Handled = true;
                    return;
                }
            }

            // ===== ENTER (=) =====
            if (key == '\r' || key == '=') {
                Equal_Click(Equal, EventArgs.Empty);
                e.Handled = true;
                return;
            }

            // ===== BACKSPACE =====
            if (key == '\b')  {
                Delete();
                e.Handled = true;
                return;
            }
        }


        // Handles special keys (like numpad multiply/divide)
        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) {
                case Keys.Multiply: {
                        OperatorInput_Click(new Button() { Text = calc.GetOperationSign(Calculator.Operator.MULTIPLY) }, null);
                        e.Handled = true;
                        break;
                }

                case Keys.Divide: {
                        OperatorInput_Click(new Button() { Text = calc.GetOperationSign(Calculator.Operator.DIVIDE) }, null);
                        e.Handled = true;
                        break;
                }

                case Keys.Add: {
                        OperatorInput_Click(new Button() { Text = calc.GetOperationSign(Calculator.Operator.PLUS) }, null);
                        e.Handled = true;
                        break;
                }

                case Keys.Subtract: {
                        OperatorInput_Click(new Button() { Text = calc.GetOperationSign(Calculator.Operator.MINUS) }, null);
                        e.Handled = true;
                        break;
                }

                case Keys.Delete: {
                        Delete();
                        e.Handled = true;
                        break;
                }
            }
        }


        // ========= Delete (by keyboard) =========
        private void Delete()
        {
            if (_DEBUG)
                Debug.Print("Delete(before) : inputValue=" + calc.inputValue + ", outputValue=" + calc.outputValue + ", Display=" + Display.Text + ", currentOperation=" + calc.GetOperationSign(calc.currentOperator) + ", hasResult=" + calc.hasResult);

            if (calc.hasResult & calc.hasOperator == false) {
                calc.ResetValues();
                DisplayOutput("0");
                return;
            }

            // Delete number from the end if no calculation has been made
            try {
                calc.outputValue = calc.outputValue.Substring(0, calc.outputValue.Length - 1).Trim();

                string[] splitValues = calc.outputValue.Split(Conversions.ToChar(calc.GetOperationSign(calc.currentOperator)));

                if (splitValues.Length > 1) {
                    calc.inputValue = splitValues[1];
                } else {
                    calc.inputValue = calc.outputValue;

                    calc.hasResult = false;
                    calc.hasOperator = false;
                    calc.currentOperator = Calculator.Operator.NONE;
                }
            }
            catch (Exception e) {
                Debug.Print(e.ToString());
            }

            // If the value is empty replaces it with a zero
            if (string.IsNullOrEmpty(calc.outputValue.Trim())) {
                calc.ResetValues();
                calc.outputValue = "0";
            }

            if (_DEBUG)
                Debug.Print("Delete(after) : inputValue=" + calc.inputValue + ", outputValue=" + calc.outputValue + ", Display=" + Display.Text + ", currentOperation=" + calc.GetOperationSign(calc.currentOperator) + ", hasResult=" + calc.hasResult);

            calc.isEqual = false; // Fix for example 2.3 + 5 = .

            DisplayOutput(calc.outputValue);
        }

         
        // ========= Display Output =========
        private void DisplayOutput(string output = "0")
        {
            Display.Text = output;

            Display.Select();
        }

        // ========= RoundedButton =========
        private void RoundedButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;

            var Raduis = new System.Drawing.Drawing2D.GraphicsPath();

            Raduis.StartFigure();
            Raduis.AddArc(new Rectangle(0, 0, 20, 20), 180f, 90f);
            Raduis.AddLine(10, 0, btn.Width - 20, 0);
            Raduis.AddArc(new Rectangle(btn.Width - 20, 0, 20, 20), -90, 90f);
            Raduis.AddLine(btn.Width, 20, btn.Width, btn.Height - 10);
            Raduis.AddArc(new Rectangle(btn.Width - 25, btn.Height - 25, 25, 25), 0f, 90f);
            Raduis.AddLine(btn.Width - 10, btn.Width, 20, btn.Height);
            Raduis.AddArc(new Rectangle(0, btn.Height - 20, 20, 20), 90f, 90f);
            Raduis.CloseFigure();
            btn.Region = new Region(Raduis);
        }

        // ========= RoundedDisplay =========
        private void RoundedDisplay(Label label)
        {
            label.FlatStyle = FlatStyle.Flat;

            var Raduis = new System.Drawing.Drawing2D.GraphicsPath();

            Raduis.StartFigure();
            Raduis.AddArc(new Rectangle(0, 0, 20, 20), 180f, 90f);
            Raduis.AddLine(10, 0, label.Width - 20, 0);
            Raduis.AddArc(new Rectangle(label.Width - 20, 0, 20, 20), -90, 90f);
            Raduis.AddLine(label.Width, 20, label.Width, label.Height - 10);
            Raduis.AddArc(new Rectangle(label.Width - 25, label.Height - 25, 25, 25), 0f, 90f);
            Raduis.AddLine(label.Width - 10, label.Width, 20, label.Height);
            Raduis.AddArc(new Rectangle(0, label.Height - 20, 20, 20), 90f, 90f);
            Raduis.CloseFigure();
            label.Region = new Region(Raduis);
        }

    }
}