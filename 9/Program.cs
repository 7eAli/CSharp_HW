using System.Text;
using System.Text.Json;
using System.Text.Unicode;

namespace _9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var sr = new StreamReader(@"C:\Users\Catian\Desktop\Обучение - gamedev\3. C# углубленно\2. Разработка приложения\ДЗ\9\test.json"))
            {
                using (var jsonDoc = JsonDocument.Parse(sr.ReadToEnd()))
                {
                    XmlFromJson(jsonDoc);   
                }
            }            
        }        

        

        static string ParseJsonFields(JsonProperty property, int depth)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string space = new string(' ', depth);
            
            if (property.Value.ValueKind == JsonValueKind.Object)
            {
                stringBuilder.AppendLine($"{space}<{property.Name}>");
                foreach (var item in property.Value.EnumerateObject())
                {
                    stringBuilder.AppendLine(ParseJsonFields(item, depth + 1));
                }
                stringBuilder.AppendLine($"{space}</{property.Name}>");
            }
            else if (property.Value.ValueKind == JsonValueKind.Array)
            {
                foreach (var el in property.Value.EnumerateArray())
                {
                    if (el.ValueKind == JsonValueKind.Object)
                    {
                        stringBuilder.AppendLine($"{space}<{property.Name}>");
                        foreach (var subel in el.EnumerateObject())
                        {
                            stringBuilder.AppendLine(ParseJsonFields(subel, depth + 1));
                        }
                        stringBuilder.AppendLine($"{space}</{property.Name}>");
                    } 
                    else if (el.ValueKind == JsonValueKind.Array)
                    {
                        stringBuilder.AppendLine($"{space}<{property.Name}>");
                        for (int i = 0; i < el.GetArrayLength(); i++)
                        {
                            stringBuilder.AppendLine($"{space} <{i}>{el[i]}</{i}>");
                        }
                        stringBuilder.AppendLine($"{space}<{property.Name}>");
                    }
                    else
                        stringBuilder.AppendLine($"{space}<{property.Name}>{el}</{property.Name}>");
                }
            } 
            else
            {
                stringBuilder.AppendLine($"{space}<{property.Name}>{property.Value}</{property.Name}>");
            }
            
            return stringBuilder.ToString().TrimEnd();
        }

        static void XmlFromJson(JsonDocument jsonDocument)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            stringBuilder.AppendLine("<root>");
            foreach (var item in jsonDocument.RootElement.EnumerateObject())
                stringBuilder.AppendLine(ParseJsonFields(item, 1));
            stringBuilder.AppendLine("</root>");
            string xmlstr = stringBuilder.ToString().TrimEnd();
            File.WriteAllText("test.xml", xmlstr);
        }
    }
}
