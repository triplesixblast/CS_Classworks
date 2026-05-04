using Lab9.Purple;
namespace Lab10.White;

public class WhiteTxtFileManager : IWhiteSerializer
{
    public string Name {get; private set;}
    public string Extension {get; private set;}
    public WhiteTxtFileManager (string name, string smth = "txt")
    {
        Name = name;
        Extension = smth;
    }
    public void Serialize(Purple obj)
    {
        var folder = Directory.GetCurrentDirectory();
        folder = Directory.GetParent(folder).Parent.Parent.FullName;
        folder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        var filePath = Path.Combine(folder, Name);
        filePath += "." + Extension;
        if (!File.Exists(filePath)) 
        {
            File.Create(filePath).Close();
        }
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        File.WriteAllText(filePath, obj.Input);
        File.AppendAllText(filePath, obj.Input);

        Dictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("Type", obj.GetType().Name);
        dict.Add("Input", obj.Input);
        var d  = dict.ToArray();
        string[] lines = new string[dict.Count];
        for (int i=0; i<lines.Length; i++)
        {
            lines[i] = d[i].Key + " : " +d[i].Value;
        }
        File.WriteAllLines(filePath, lines);
        var str = File.ReadAllLines(filePath);
        lines = File.ReadAllLines(filePath);

        var pair = lines[0].Split(":", 2, StringSplitOptions.RemoveEmptyEntries);
        var input = lines[1].Split(":", 2, StringSplitOptions.RemoveEmptyEntries);

        Lab9.Purple.Purple desObj;
        if (pair[0] == "Type")
        {
            switch (pair[1])
            {
                case "Task1" : desObj = new Lab9.Purple.Task1("lalala"); break;
                case "Task2" : desObj = new Lab9.Purple.Task1("lalala"); break;
                case "Task3" : break;
                
            }
        }
    }
}

