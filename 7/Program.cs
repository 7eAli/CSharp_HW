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
            TestClass testClass = new TestClass(1, "asd", 0.5m, ['A', 'B', 'C']);


            string s = TypeToString(testClass);


            var obj = StringToObject(s);
            Console.WriteLine(obj.ToString());
        }

        public static Type GetType(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type != null) return type;
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                Console.WriteLine(a.GetType("Program"));
                type = a.GetType(typeName);
                if (type != null)
                    return type;
            }
            return null;
        }
        static string TypeToString(object obj)
        {
            Type t = obj.GetType();
            StringBuilder result = new StringBuilder();
            result.Append(t.FullName);
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
                result.Append(property.GetValue(obj).GetType().FullName);
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

            Type type = Type.GetType(splitedInfo[0]);
            object? result = Activator.CreateInstance(type);

            string[] values = splitedInfo[1].Split(";");
            string[] typeInfo = splitedInfo[2].Split(';');

            for (int i = 0; i < values.Length - 1; i++)
            {
                string[] nameAndType = typeInfo[i].Split('=');
                Type propertyType = Type.GetType(nameAndType[0]);
                string propertyName = nameAndType[1];
                foreach (PropertyInfo property in result.GetType().GetProperties())
                {
                    if (property.Name == propertyName)
                    {
                        if (property.PropertyType.IsArray)
                        {
                            values[i] = values[i].Substring(1, values[i].Length - 2);
                            string[] arrayValues = values[i].Split(',');
                            Array array = Array.CreateInstance(propertyType.GetElementType(), arrayValues.Length);
                            for (int j = 0; j < arrayValues.Length; j++)
                            {
                                array.SetValue(Convert.ChangeType(arrayValues[j], array.GetType().GetElementType()), j);
                                Console.WriteLine(array.GetValue(j));
                            }
                            property.SetValue(result, Convert.ChangeType(array, propertyType));
                        }
                        else
                            property.SetValue(result, Convert.ChangeType(values[i], propertyType));
                    }
                }
            }

            return result;
        }
    }
}
