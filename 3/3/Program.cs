namespace _3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,,] labirynth = new int[5, 5, 5];
            FillArray(labirynth);
            ShowArray(labirynth);
            Console.WriteLine(Find(labirynth, 2, 2, 2)); 
            ShowArray(labirynth);

        }

        static int Find(int[,,] array, int x, int y, int z)
        {
            if (!IsEmpty(array, x, y, z))
                return 0;
            array[x, y, z] = 2;
            int count = 0;

            if (x == 0 && y == 0 && z == 0 ||
                x == 0 && y == 0 && z == array.GetLength(2) - 1 ||
                x == 0 && y == array.GetLength(1) - 1 && z == 0 ||
                x == 0 && y == array.GetLength(1) - 1 && z == array.GetLength(2) - 1 ||
                x == array.GetLength(0) - 1 && y == 0 && z == 0 ||
                x == array.GetLength(0) - 1 && y == 0 && z == array.GetLength(2) ||
                x == array.GetLength(0) - 1 && y == array.GetLength(1) - 1 && z == 0 ||
                x == array.GetLength(0) - 1 && y == array.GetLength(1) - 1 && z == array.GetLength(2) - 1)
            {
                count += 3;
            }
            else if (x == 0 && y == 0 && z >= 0 ||
                     x == 0 && y == array.GetLength(1) - 1 && z >= 0 ||
                     x == array.GetLength(0) - 1 && y == 0 && z >= 0 ||
                     x == array.GetLength(0) -1 && y == array.GetLength(1) - 1 && z >= 0 ||
                     
                     x == 0 && y >= 0 && z == 0 ||
                     x == 0 && y >= 0 && z == array.GetLength(2) ||
                     x == array.GetLength(0) - 1 && y >= 0 && z == 0 ||
                     x == array.GetLength(0) - 1 && y >= 0 && z == array.GetLength(2) - 1 ||
                     
                     x >= 0 && y == 0 && z == 0 ||
                     x >= 0 && y == 0 && z == array.GetLength(2) - 1 ||
                     x >= 0 && y == array.GetLength(1) - 1 && z == 0 ||
                     x >= 0 && y == array.GetLength(1) - 1 && z == array.GetLength(2) - 1)
            {
                count += 2;
            }
            else if (x == 0 || y == 0 || z == 0 || x == array.GetLength(0) - 1 || y == array.GetLength(1) - 1 || z == array.GetLength(2) - 1)
            {
                count++;
            }


            count += Find(array, x, y + 1, z);
            count += Find(array, x, y - 1, z);
            count += Find(array, x, y, z + 1);
            count += Find(array, x, y, z - 1);
            count += Find(array, x + 1, y, z);
            count += Find(array, x - 1, y, z);
            count += Find(array, x, y, z + 1);
            count += Find(array, x, y, z - 1);
            count += Find(array, x + 1, y, z);
            count += Find(array, x - 1, y, z);
            count += Find(array, x, y + 1, z);
            count += Find(array, x, y - 1, z);

            return count;
        }

        static bool IsEmpty(int[,,] array, int x, int y, int z)
        {
            if (x < 0 || x >= array.GetLength(0))
                return false;
            if (y < 0 || y >= array.GetLength(1))
                return false;
            if (z < 0 || z >= array.GetLength(2))
                return false;
            return array[x, y, z] == 0;
        }

        static void FillArray(int[,,] array)
        {
            var rd = new Random();
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    for (int k = 0; k < array.GetLength(2); k++)
                        array[i, j, k] = rd.Next(0, 2);                        
        }

        static void ShowArray(int[,,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    for (int k = 0; k < array.GetLength(2); k++)
                    {
                        Console.Write($"{array[i, j, k]} ");
                    }
                    Console.Write(" ");
                }
                Console.WriteLine();
            }                
        }
    }
}
