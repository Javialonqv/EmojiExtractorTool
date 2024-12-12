using System.Reflection;
using System.Text;

public static class LanguageManager
{
    static string language = "";
    static Dictionary<string, string> translations = new();

    public static void Init(string language)
    {
        switch (language)
        {
            case "English":
                LoadTranslationsJSON("en-us.json");
                break;
        }

        Console.Title = GetTranslation("title");
    }

    static void LoadTranslationsJSON(string fileName)
    {
        string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Languages", fileName);
        translations = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(jsonFilePath));
    }

    public static string GetTranslation(string key)
    {
        if (translations.ContainsKey(key))
        {
            return translations[key];
        }
        else
        {
            return "";
        }
    }
}