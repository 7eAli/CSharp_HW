namespace _5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            calculator.Result += Calculator_Result;

            
            bool flag = true;
            Actions_List();

            while (flag)
            {
                Console.WriteLine("Введите число: ");
                string num = Console.ReadLine()!;
                Console.WriteLine("Введите действие: ");
                flag = Action(Console.ReadLine()!, num, calculator);                
            }
        }
        
        static void Actions_List()
        {
            Console.WriteLine("Действия: ");
            Console.WriteLine("'+' - сложение");
            Console.WriteLine("'-' - вычитание");
            Console.WriteLine("'*' - умножение");
            Console.WriteLine("'/' - деление");
            Console.WriteLine("'b' - возврат предыдущего значения");
            Console.WriteLine("'q' - выход");
        }

        static bool Action(string action, string num, Calculator calculator)
        {
            switch (action)
            {
                case "+":
                    calculator.Add(num);
                    break;
                case "-":
                    calculator.Sub(num);
                    break;
                case "*":
                    calculator.Mult(num);
                    break;
                case "/":
                    calculator.Div(num);
                    break;
                case "b":
                    calculator.Cancel();
                    break;
                case "q":
                    return false;                    
            }
            return true;
        }

        private static void Calculator_Result(object? sender, CalculatorArgs e)
        {
            Console.WriteLine(e.answer);
        }
    }
}
