namespace _4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             Дан массив и число. Найдите три числа в массиве сумма которых равна искомому числу.
            */
            int[] nums = new int[10];
            int targetNum = new Random().Next(20);
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = new Random().Next(0, 16);
            }

            Console.WriteLine(targetNum);
            Console.Write("[ ");
            foreach (int i in nums)
            {
                Console.Write($"{i}, ");
            }
            Console.Write("]");
            Console.WriteLine();

            Console.WriteLine();

            HashSet<int> ints = new HashSet<int>();

            for (int i = 0; i < nums.Length - 1; i++)
            {
                ints.Add(nums[i]);
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (ints.Contains(targetNum - nums[i] - nums[j]) &&
                                        nums[i] != targetNum - nums[j] - nums[i] &&
                                        nums[j] != targetNum - nums[j] - nums[i])
                    {
                        Console.WriteLine($"{nums[i]}  {nums[j]}  {targetNum - nums[j] - nums[i]}");
                    }
                }
            }

            Console.WriteLine();
        }
    }
}

