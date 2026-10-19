using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

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

        // Status indicator if equal button or another calculation has presented a total result
        private bool hasResult;

        // Variables holding current and previous operations
        private Calculator.Operator currentOperator = Calculator.Operator.NONE;

        private bool hasOperator;

        // Variables holding the result and input values
        private double resultValue;
        private double firstValue;
        private double secondValue;

        // Variables holding the input and output text
        private string inputValue;
        private string outputValue;

        // Get decimal seperator
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

            // Set the operator sign for operator buttons
            Plus.Text = Conversions.ToString(Calculator.GetOperationSign(Calculator.Operator.PLUS));
            Minus.Text = Conversions.ToString(Calculator.GetOperationSign(Calculator.Operator.MINUS));
            Multiply.Text = Conversions.ToString(Calculator.GetOperationSign(Calculator.Operator.MULTIPLY));
            Divide.Text = Conversions.ToString(Calculator.GetOperationSign(Calculator.Operator.DIVIDE));

            ResetValues();
            
            DisplayOutput();
        }


        // ========= NUMBER INPUT =========
        private void NumberInput_Click(object sender, EventArgs e)
        {
            // Get the current selected button
            Button btn = (Button)sender;

            if (_DEBUG)
                Debug.Print(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Number(before) : button=" + btn.Text + ", inputValue=" + inputValue + ", outputValue=" + outputValue + ", Display=" + Display.Text + ", currentOperation=", Calculator.GetOperationSign(currentOperator)), ", hasResult="), hasResult), ", hasOperator="), hasOperator)));

            string inputKey = btn.Text;

            if (hasResult) {
                ResetValues();
                inputValue = inputKey;
                outputValue = inputValue;
            } else if (hasOperator) {
                string[] splittedOutputValue = outputValue.Split(Conversions.ToChar(Calculator.GetOperationSign(currentOperator)));
                inputValue += inputKey;

                string currentResultValue = splittedOutputValue[0];

                if (currentOperator == Calculator.Operator.MINUS & string.IsNullOrEmpty(currentResultValue)) {
                    currentResultValue = Conversions.ToString(Operators.ConcatenateObject(Calculator.GetOperationSign(Calculator.Operator.MINUS), splittedOutputValue[1]));
                }

                outputValue = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(currentResultValue, Calculator.GetOperationSign(currentOperator)), inputValue));
            } else {
                inputValue += inputKey;
                outputValue = inputValue;
            }

            DisplayOutput(outputValue);

            if (_DEBUG)
                Debug.Print(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Number(after) : button=" + btn.Text + ", inputValue=" + inputValue + ", outputValue=" + outputValue + ", Display=" + Display.Text + ", currentOperation=", Calculator.GetOperationSign(currentOperator)), ", hasResult="), hasResult), ", hasOperator="), hasOperator)));
        }


        // ========= DECIMAL INPUT =========
        private void DecimalInput_Click(object sender, EventArgs e)
        {
            if (_DEBUG)
                Debug.Print(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Decimal(before) : inputValue=" + inputValue + ", outputValue=" + outputValue + ", Display=" + Display.Text + ", currentOperation=", Calculator.GetOperationSign(currentOperator)), ", hasResult="), hasResult), ", hasOperator="), hasOperator)));

            if (hasResult) {
                ResetValues();
            } else {
                string[] splittedOutputValue = outputValue.Split(Conversions.ToChar(Calculator.GetOperationSign(currentOperator)));

                if (hasOperator) {
                    outputValue = Conversions.ToString(Operators.ConcatenateObject(splittedOutputValue[0], Calculator.GetOperationSign(currentOperator)));
                    inputValue = splittedOutputValue[1];
                } else {
                    outputValue = "";
                    inputValue = splittedOutputValue[0];
                }

                if (!inputValue.Contains(DECIMAL_SEPARATOR)) {
                    inputValue += DECIMAL_SEPARATOR;
                }
            }

            if (inputValue.StartsWith(DECIMAL_SEPARATOR))
                inputValue = "0" + inputValue;

            outputValue += inputValue;
                        
            DisplayOutput(outputValue);

            if (_DEBUG)
                Debug.Print(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Decimal(after) : inputValue=" + inputValue + ", outputValue=" + outputValue + ", Display=" + Display.Text + ", currentOperation=", Calculator.GetOperationSign(currentOperator)), ", hasResult="), hasResult), ", hasOperator="), hasOperator)));
        }


        // ========= OPERATOR INPUT =========
        private void OperatorInput_Click(object sender, EventArgs e)
        {
            // Get the current selected button
            Button btn = (Button)sender;

            if (_DEBUG)
                Debug.Print(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Operator(before) : button=" + btn.Text + ", inputValue=" + inputValue + ", outputValue=" + outputValue + ", Display=" + Display.Text + ", currentOperation=", Calculator.GetOperationSign(currentOperator)), ", hasResult="), hasResult), ", hasOperator="), hasOperator)));

            var previousOperator = currentOperator;

            if (hasOperator) {
                string[] splittedOutputValue = outputValue.Split(Conversions.ToChar(Calculator.GetOperationSign(previousOperator)));

                ResetValues();

                outputValue = splittedOutputValue[0];
                inputValue = splittedOutputValue[1];

                if (previousOperator == Calculator.Operator.MINUS & string.IsNullOrEmpty(outputValue)) {
                    outputValue = Conversions.ToString(Operators.ConcatenateObject(Calculator.GetOperationSign(Calculator.Operator.MINUS), splittedOutputValue[1]));
                    inputValue = splittedOutputValue[2];
                }

                double.TryParse(outputValue, out firstValue);
                double.TryParse(inputValue, out secondValue);

                resultValue = Calculator.Calculate(firstValue, secondValue, previousOperator);

                currentOperator = (Calculator.Operator)Conversions.ToInteger(Calculator.GetOperation(btn.Text));

                inputValue = "";
                outputValue = Conversions.ToString(Operators.ConcatenateObject(resultValue, Calculator.GetOperationSign(currentOperator)));
            } else if (string.IsNullOrEmpty(outputValue)) {
                ResetValues();

                currentOperator = (Calculator.Operator)Conversions.ToInteger(Calculator.GetOperation(btn.Text));

                inputValue = "0";
                outputValue = Conversions.ToString(Operators.ConcatenateObject(inputValue, Calculator.GetOperationSign(currentOperator)));
            } else {
                currentOperator = (Calculator.Operator)Conversions.ToInteger(Calculator.GetOperation(btn.Text));

                inputValue = "";
                outputValue = Conversions.ToString(outputValue + Calculator.GetOperationSign(currentOperator));
            }

            hasOperator = true;
                        
            DisplayOutput(outputValue);

            if (_DEBUG)
                Debug.Print(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Operator(after) : button=" + btn.Text + ", resultValue=" + resultValue + ", firstValue=" + firstValue + ", secondValue=" + secondValue + ", inputValue=" + inputValue + ", outputValue=" + outputValue + ", Display=" + Display.Text + ", currentOperation=", Calculator.GetOperationSign(currentOperator)), ", hasResult="), hasResult), ", hasOperator="), hasOperator)));
        }


        // ========= EQUAL BUTTON =========
        private void Equal_Click(object sender, EventArgs e)
        {
            if (_DEBUG)
                Debug.Print(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Equal(before) : inputValue=" + inputValue + ", outputValue=" + outputValue + ", Display=" + Display.Text + ", currentOperation=", Calculator.GetOperationSign(currentOperator)), ", hasResult="), hasResult)));

            string[] splittedOutputValue = outputValue.Split(Conversions.ToChar(Calculator.GetOperationSign(currentOperator)));

            outputValue = splittedOutputValue[0];

            try {
                if (currentOperator == Calculator.Operator.MINUS & string.IsNullOrEmpty(outputValue)) {
                    outputValue = Conversions.ToString(Operators.ConcatenateObject(Calculator.GetOperationSign(Calculator.Operator.MINUS), splittedOutputValue[1]));
                    inputValue = splittedOutputValue[2];
                } else {
                    inputValue = splittedOutputValue[1];
                }
            } catch (Exception) {
                ResetValues();
            }

            double.TryParse(outputValue, out firstValue);
            double.TryParse(inputValue, out secondValue);

            resultValue = Calculator.Calculate(firstValue, secondValue, currentOperator);

            inputValue = "";
            outputValue = resultValue.ToString();

            currentOperator = Calculator.Operator.NONE;
            hasOperator = false;
            hasResult = false;
                        
            DisplayOutput(outputValue);

            if (_DEBUG)
                Debug.Print(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Equal(after) : inputValue=" + inputValue + ", outputValue=" + outputValue + ", Display=" + Display.Text + ", currentOperation=", Calculator.GetOperationSign(currentOperator)), ", hasResult="), hasResult)));
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
                        '+' => Conversions.ToString(Calculator.GetOperationSign(Calculator.Operator.PLUS)),
                        '-' => Conversions.ToString(Calculator.GetOperationSign(Calculator.Operator.MINUS)),
                        '*' => Conversions.ToString(Calculator.GetOperationSign(Calculator.Operator.MULTIPLY)),
                        '/' => Conversions.ToString(Calculator.GetOperationSign(Calculator.Operator.DIVIDE)),
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
                        OperatorInput_Click(new Button() { Text = Conversions.ToString(Calculator.GetOperationSign(Calculator.Operator.MULTIPLY)) }, null);
                        e.Handled = true;
                        break;
                }

                case Keys.Divide: {
                        OperatorInput_Click(new Button() { Text = Conversions.ToString(Calculator.GetOperationSign(Calculator.Operator.DIVIDE)) }, null);
                        e.Handled = true;
                        break;
                }

                case Keys.Add: {
                        OperatorInput_Click(new Button() { Text = Conversions.ToString(Calculator.GetOperationSign(Calculator.Operator.PLUS)) }, null);
                        e.Handled = true;
                        break;
                }

                case Keys.Subtract: {
                        OperatorInput_Click(new Button() { Text = Conversions.ToString(Calculator.GetOperationSign(Calculator.Operator.MINUS)) }, null);
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
                Debug.Print(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Delete(before) : inputValue=" + inputValue + ", outputValue=" + outputValue + ", Display=" + Display.Text + ", currentOperation=", Calculator.GetOperationSign(currentOperator)), ", hasResult="), hasResult)));

            if (hasResult & hasOperator == false) {
                ResetValues();
                DisplayOutput("0");
                return;
            }

            // Delete number from the end if no calculation has been made
            try {
                outputValue = outputValue.Substring(0, outputValue.Length - 1).Trim();

                string[] splittedValues = outputValue.Split(Conversions.ToChar(Calculator.GetOperationSign(currentOperator)));

                if (splittedValues.Length > 1) {
                    inputValue = splittedValues[1];
                } else {
                    inputValue = outputValue;

                    hasResult = false;
                    hasOperator = false;
                    currentOperator = Calculator.Operator.NONE;
                }
            }
            catch (Exception e) {
                Debug.Print(e.ToString());
            }

            // If the value is empty replaces it with a zero
            if (string.IsNullOrEmpty(outputValue.Trim())) {
                ResetValues();
                outputValue = "0";
            }

            if (_DEBUG)
                Debug.Print(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Delete(after) : inputValue=" + inputValue + ", outputValue=" + outputValue + ", Display=" + Display.Text + ", currentOperation=", Calculator.GetOperationSign(currentOperator)), ", hasResult="), hasResult)));
                        
            DisplayOutput(outputValue);
        }


        // ========= Reset Values =========
        private void ResetValues()
        {
            hasResult = false;

            currentOperator = Calculator.Operator.NONE;

            hasOperator = false;

            resultValue = 0.0d;
            firstValue = 0.0d;
            secondValue = 0.0d;

            inputValue = "";
            outputValue = "";
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