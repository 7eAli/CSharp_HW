using Microsoft.VisualBasic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;
using System.Text;

namespace _7
{
    
    class Program
    {
        static void Main(string[] args)
        {
            TestClass testClass = new TestClass(1, "asd", 0.5m, [ 'A', 'B', 'C' ]);
            
            Console.WriteLine(testClass.ToString());
            string s = TypeToString(testClass);

            Console.WriteLine(s);

            var obj = StringToObject(s);

            
        }

        static string TypeToString(object obj)
        {
            Type t = obj.GetType();
            StringBuilder result = new StringBuilder();
            result.Append(t.Name);
            result.Append('|');
            foreach (PropertyInfo property in t.GetProperties()) 
            {                
                if (property.GetValue(obj).GetType().IsArray)
                {
                    result.Append('{');
                    foreach (var el in property.GetValue(obj) as Array)
                    {
                        result.Append(el);
                        result.Append(',');
                    }
                    result.Remove(result.Length - 1, 1);
                    result.Append("}");
                    result.Append(';');
                }
                else
                {
                    result.Append(property.GetValue(obj));
                    result.Append(';');
                }
            }
            result.Append('|');
            foreach (PropertyInfo property in t.GetProperties())
            {
                result.Append(property.GetValue(obj).GetType());
                result.Append('=');
                result.Append(property.Name);
                result.Append(';');
            }
            result.Remove(result.Length - 1, 1);
            return result.ToString();
        }

        static object StringToObject(string s)
        {
            string[] splitedInfo = s.Split('|');            
            foreach(string str in splitedInfo)
            {
                Console.WriteLine(str);
            }
            Assembly assembly = typeof(Assembly).Assembly;                      
            object? result = assembly.CreateInstance(splitedInfo[0]);

            string[] values = splitedInfo[1].Split(";");
            string[] typeInfo = splitedInfo[2].Split(';');
            
            for (int i = 0; i < values.Length - 1; i++)
            {
                Console.WriteLine(values[i]);                
                string[] nameAndType = typeInfo[i].Split('=');
                Console.WriteLine($"{nameAndType[0]} {nameAndType[1]}");
                Type propertyType = Type.GetType(nameAndType[0]);
                string propertyName = nameAndType[1];
                foreach (PropertyInfo property in result.GetType().GetProperties())
                {
                    if (property.Name == propertyName)
                    {
                        property.SetValue(result, Convert.ChangeType(values[i], propertyType));
                    }
                }
            }
                       
            return result;
        }
    }
}
