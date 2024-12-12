using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Windows.Forms;

namespace EmojiExtractorTool
{
    internal class Program
    {
        static string emojiID = "";
        static string outputFormat = "";
        static string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "Extracted_Emojis");

        [STAThread]
        static async Task Main(string[] args)
        {
            Console.Title = "Emoji Extractor Tool";

            SelectLanguage();
            Intro();
            ChooseOutputFormat();
            ChooseOutputPath();
            await DownloadEmoji();
        }

        static void SelectLanguage()
        {
            char input;

            do
            {
                Console.Clear();
                Console.WriteLine("Select your language:");
                Console.WriteLine("[1] English");
                Console.WriteLine("[2] Español");
                Console.WriteLine("[3] Português");
                Console.WriteLine("[4] Türkçe");
                Console.WriteLine();
                Console.Write("> ");

                input = Console.ReadKey().KeyChar;
            }
            while (input != '1' && input != '2' && input != '3' && input != '4');

            switch (input)
            {
                case '1':
                    LanguageManager.Init("English");
                    break;
            }
        }

        static void Intro()
        {
            Console.Clear();
            Console.WriteLine(LanguageManager.GetTranslation("translators"));
            Console.WriteLine();
            Console.WriteLine(LanguageManager.GetTranslation("welcome"));
            Console.WriteLine(LanguageManager.GetTranslation("function"));
            Console.WriteLine(LanguageManager.GetTranslation("howToUse"));
            Console.WriteLine(LanguageManager.GetTranslation("firstInstruction"));
            Console.WriteLine();
            Console.Write("> ");

            emojiID = Console.ReadLine();
        }

        static void ChooseOutputFormat()
        {
            char input;

            do
            {
                Console.Clear();
                Console.WriteLine(LanguageManager.GetTranslation("chooseOutputFormat"));
                Console.WriteLine("[1] PNG");
                Console.WriteLine("[2] JPG");
                Console.WriteLine("[3] GIF");
                Console.WriteLine("[4] WEBP");

                input = Console.ReadKey().KeyChar;
            }
            while (input != '1' && input != '2' && input != '3' && input != '4');

            switch (input)
            {
                case '1':
                    outputFormat = "png";
                    break;

                case '2':
                    outputFormat = "jpg";
                    break;

                case '3':
                    outputFormat = "gif";
                    break;

                case '4':
                    outputFormat = "webp";
                    break;
            }
        }

        static void ChooseOutputPath()
        {
            char input;

            do
            {
                Console.Clear();
                Console.WriteLine(LanguageManager.GetTranslation("askForOutputPath"));
                Console.WriteLine(LanguageManager.GetTranslation("outputPathOption1"));
                Console.WriteLine(LanguageManager.GetTranslation("outputPathOption2"));
                Console.WriteLine();
                Console.Write("> ");

                input = Console.ReadKey().KeyChar;
            }
            while (input != '1' && input != '2');

            if (input == '2')
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.Write(LanguageManager.GetTranslation("insertCustomPath"));

                outputPath = Console.ReadLine();
            }
        }

        static async Task DownloadEmoji()
        {
            Console.Clear();
            Console.WriteLine($"{LanguageManager.GetTranslation("choosenFormat")} {outputFormat.ToUpper()}");
            Console.WriteLine();

            using (var client = new WebClient())
            {
                string outputFilePath = Path.Combine(outputPath, $"Extracted_{emojiID}.{outputFormat}");
                string url = $"https://cdn.discordapp.com/emojis/{emojiID}.{outputFormat}";
                Console.WriteLine(url);

                if (!Directory.Exists(outputPath)) { Directory.CreateDirectory(outputPath); }

                client.DownloadFileAsync(new Uri(url), outputFilePath);

                Console.WriteLine(LanguageManager.GetTranslation("final"));
                Console.ReadKey();
            }
        }
    }
}
