using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _5
{
    class CalculatorArgs : EventArgs
    {
        public string answer = "0";
    }
    class Calculator
    {
        public event EventHandler<CalculatorArgs> Result;
        public string result { get; private set; } = "0";

        Stack<string> results = new Stack<string>();

        public static string operator +(Calculator rawNum1, string rawNum2)
        {
            double result = 0;
            if (double.TryParse(rawNum1.result, out double num1_1))
            {
                if (double.TryParse(rawNum2, out double num2_1))
                {
                    result = num1_1 + num2_1;
                }
                else if (int.TryParse(rawNum2, out int num2_2))
                {
                    result = num1_1 + num2_2;
                }
            }
            else if (int.TryParse(rawNum1.result, out int num1_2))
            {
                if (double.TryParse(rawNum2, out double num2_1))
                {
                    result = num1_2 + num2_1;
                }
                else if (int.TryParse(rawNum2, out int num2_2))
                {
                    result = num1_2 + num2_2;
                }
            }
            return result.ToString();
        }

        public static string operator -(Calculator rawNum1, string rawNum2)
        {
            double result = 0;
            if (double.TryParse(rawNum1.result, out double num1_1))
            { 
                if (double.TryParse(rawNum2, out double num2_1))
                {
                    result = num1_1 - num2_1;
                }
                else if (int.TryParse(rawNum2, out int num2_2))
                {
                    result = num1_1 - num2_2;
                }
            }
            else if (int.TryParse(rawNum1.result, out int num1_2))
            {
                if (double.TryParse(rawNum2, out double num2_1))
                {
                    result = num1_2 - num2_1;
                }
                else if (int.TryParse(rawNum2, out int num2_2))
                {
                    result = num1_2 - num2_2;
                }
            }
            return result.ToString();
        }

        public static string operator *(Calculator rawNum1, string rawNum2)
        {
            double result = 0;
            if (double.TryParse(rawNum1.result, out double num1_1))
            {
                if (double.TryParse(rawNum2, out double num2_1))
                {
                    result = num1_1 * num2_1;
                }
                else if (int.TryParse(rawNum2, out int num2_2))
                {
                    result = num1_1 * num2_2;
                }
            }
            else if (int.TryParse(rawNum1.result, out int num1_2))
            {
                if (double.TryParse(rawNum2, out double num2_1))
                {
                    result = num1_2 * num2_1;
                }
                else if (int.TryParse(rawNum2, out int num2_2))
                {
                    result = num1_2 * num2_2;
                }
            }
            return result.ToString();
        }

        public static string operator /(Calculator rawNum1, string rawNum2)
        {
            double result = 0;
            if (double.TryParse(rawNum1.result, out double num1_1))
            {
                if (double.TryParse(rawNum2, out double num2_1) && num2_1 != 0)
                {
                    result = num1_1 / num2_1;
                }
                else if (int.TryParse(rawNum2, out int num2_2) && num2_2 != 0)
                {
                    result = num1_1 / num2_2;
                }
            }
            else if (int.TryParse(rawNum1.result, out int num1_2))
            {
                if (double.TryParse(rawNum2, out double num2_1) && num2_1 != 0)
                {
                    result = num1_2 / num2_1;
                }
                else if (int.TryParse(rawNum2, out int num2_2) && num2_2 != 0)
                {
                    result = num1_2 / num2_2;
                }
            }
            return result.ToString();
        }

        private void Calculation()
        {
            Result.Invoke(this, new CalculatorArgs { answer = result });
        }

        public void Add(string value)
        {
            results.Push(result);
            result = this + value;
            Calculation();
        }

        public void Sub(string value)
        {
            results.Push(result);
            result = this - value;
            Calculation();
        }
        public void Mult(string value)
        {
            results.Push(result);
            result = this * value;
            Calculation();
        }
        public void Div(string value)
        {
            results.Push(result);
            result = this / value;
            Calculation();
        }

        public void Cancel()
        {
            if (results.Count > 0)
            {
                result = results.Pop();
                Calculation();
            }
        }
    }
}
