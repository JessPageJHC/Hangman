using System.IO;
using System.Text;
using static System.Console;

class Program
{
    class Character
    {
        public char Value { get; set; }
        public bool Hidden { get; set; }

        public Character(char value)
        {
            Value = Char.ToUpper(value);

            if (Char.IsLetter(value))
            {
                Hidden = true;
            }
            else
            {
                Hidden = false;
            }
        }
    }

    class Puzzle
    {
        public List<Character> Word = new List<Character>();

        public Puzzle(string word)
        {
            Word = new List<Character>();

            foreach (char c in word)
            {
                Word.Add(new Character(c));
            }
        }

        public void Show(char c)
        {
            foreach (Character character in Word)
            {
                if (character.Value == Char.ToUpper(c))
                {
                    character.Hidden = false;
                }
            }
        }

        public void ShowAll()
        {
            foreach (Character character in Word)
            {
                character.Hidden = false;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (Character character in Word)
            {
                if (character.Hidden)
                {
                    sb.Append('_');
                }
                else
                {
                    sb.Append(character.Value);
                }
            }

            return sb.ToString();
        }
    }

    static void Main()
    {
        // Initialize
        Clear();
        ForegroundColor = ConsoleColor.White;
        Random random = new Random();

        // Get files from "categories" folder
        DirectoryInfo directory = new DirectoryInfo(Directory.GetCurrentDirectory() + @"\categories");
        FileInfo[] files = directory.GetFiles();

        // Print out list of categories
        WriteLine("Choose a category:");
        foreach(FileInfo i in files)
        {           
            WriteLine("  {0}", Path.GetFileNameWithoutExtension(i.Name));
        }
        WriteLine("Press ENTER to select.");

        // Allow user to select a category
        int selection = 0;
        bool selected = false;
        while (!selected)
        {
            SetCursorPosition(0, selection + 1);
            switch (ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow:
                    if (selection > 0) { selection--; }
                    break;
                case ConsoleKey.DownArrow:
                    if (selection < files.Length - 1) { selection++; }
                    break;
                case ConsoleKey.Enter:
                    selected = true;
                    break;
            }
        }

        string category = Directory.GetCurrentDirectory() + @"\categories\" + files[selection].Name;
        List<string> words = File.ReadAllLines(category).ToList();

        Puzzle puzzle = new Puzzle(words[random.Next(words.Count)]);

        Clear();
        puzzle.ShowAll();
        WriteLine(puzzle.ToString());
    }
}