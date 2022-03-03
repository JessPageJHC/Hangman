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

        public bool Contains(char c)
        {
            bool contains = false;
            foreach (Character character in Word)
            {
                if (character.Value == Char.ToUpper(c))
                {
                    contains = true;
                }
            }

            return contains;
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

        public bool Finished()
        {
            bool finished = true;
            foreach (Character character in Word)
            {
                if (character.Hidden)
                {
                    finished = false;
                }
            }
            
            return finished;
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
        while (true)
        {
            // Initialize
            Clear();
            ForegroundColor = ConsoleColor.White;
            CursorVisible = true;
            Random random = new Random();

            // Get files from "categories" folder
            DirectoryInfo directory = new DirectoryInfo(Directory.GetCurrentDirectory() + @"\categories");
            FileInfo[] files = directory.GetFiles();

            // Print out list of categories
            WriteLine(" Choose a category:");
            foreach(FileInfo i in files)
            {           
                WriteLine("   {0}", Path.GetFileNameWithoutExtension(i.Name));
            }
            WriteLine(" Press ENTER to select.");

            // Allow user to select a category
            int selection = 0;
            bool selected = false;
            while (!selected)
            {
                SetCursorPosition(1, selection + 1);
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

            // Create a new puzzle with a random word from the selected category
            string category = Directory.GetCurrentDirectory() + @"\categories\" + files[selection].Name;
            List<string> words = File.ReadAllLines(category).ToList();
            string word = words[random.Next(words.Count)];
            Puzzle puzzle = new Puzzle(word);

            CursorVisible = false;
            bool win = true;
            int mistakes = 0;
            List<char> incorrectLetters = new List<char>();
            while (true)
            {
                Clear();
                switch (mistakes)
                {
                    case 1:
                        WriteLine(" \n\n\n\n\n\n\n     _____");
                        break;
                    case 2:
                        WriteLine(" \n      |\n      |\n      |\n      |\n      |\n      |\n     _|___");
                        break;
                    case 3:
                        WriteLine("       _______\n      |/\n      |\n      |\n      |\n      |\n      |\n     _|___");
                        break;
                    case 4:
                        WriteLine("       _______\n      |/      |\n      |      (_)\n      |\n      |\n      |\n      |\n     _|___");
                        break;
                    case 5:
                        WriteLine("       _______\n      |/      |\n      |      (_)\n      |       |\n      |       |\n      |\n      |\n     _|___");
                        break;
                    case 6:
                        WriteLine("       _______\n      |/      |\n      |      (_)\n      |      \\|/\n      |       |\n      |\n      |\n     _|___");
                        break;
                    case 7:
                        WriteLine("       _______\n      |/      |\n      |      (_)\n      |      \\|/\n      |       |\n      |      / \\\n      |\n     _|___");
                        break;
                    default:
                        WriteLine(" \n\n\n\n\n\n\n");
                        break;
                }
                WriteLine("\n " + puzzle.ToString() + "\n");
                ForegroundColor = ConsoleColor.Red;
                foreach (char c in incorrectLetters)
                {
                    Write(" " + Char.ToUpper(c));
                }
                ForegroundColor = ConsoleColor.White;
                
                char attempt = ' ';
                switch (ReadKey(true).Key)
                {
                    case ConsoleKey.A: { attempt = 'a'; break; }
                    case ConsoleKey.B: { attempt = 'b'; break; }
                    case ConsoleKey.C: { attempt = 'c'; break; }
                    case ConsoleKey.D: { attempt = 'd'; break; }
                    case ConsoleKey.E: { attempt = 'e'; break; }
                    case ConsoleKey.F: { attempt = 'f'; break; }
                    case ConsoleKey.G: { attempt = 'g'; break; }
                    case ConsoleKey.H: { attempt = 'h'; break; }
                    case ConsoleKey.I: { attempt = 'i'; break; }
                    case ConsoleKey.J: { attempt = 'j'; break; }
                    case ConsoleKey.K: { attempt = 'k'; break; }
                    case ConsoleKey.L: { attempt = 'l'; break; }
                    case ConsoleKey.M: { attempt = 'm'; break; }
                    case ConsoleKey.N: { attempt = 'n'; break; }
                    case ConsoleKey.O: { attempt = 'o'; break; }
                    case ConsoleKey.P: { attempt = 'p'; break; }
                    case ConsoleKey.Q: { attempt = 'q'; break; }
                    case ConsoleKey.R: { attempt = 'r'; break; }
                    case ConsoleKey.S: { attempt = 's'; break; }
                    case ConsoleKey.T: { attempt = 't'; break; }
                    case ConsoleKey.U: { attempt = 'u'; break; }
                    case ConsoleKey.V: { attempt = 'v'; break; }
                    case ConsoleKey.W: { attempt = 'w'; break; }
                    case ConsoleKey.X: { attempt = 'x'; break; }
                    case ConsoleKey.Y: { attempt = 'y'; break; }
                    case ConsoleKey.Z: { attempt = 'z'; break; }
                }

                if (puzzle.Contains(attempt))
                {
                    puzzle.Show(attempt);
                }
                else if (!puzzle.Contains(attempt) && !incorrectLetters.Contains(attempt))
                {
                    incorrectLetters.Add(attempt);
                    mistakes++;
                }

                if (puzzle.Finished())
                {
                    win = true;
                    break;
                }
                else if (mistakes > 7)
                {
                    win = false;
                    break;
                }
            }

            // Ending screen
            if (win)
            {
                Clear();
                ForegroundColor = ConsoleColor.Green;
                WriteLine(" == WELL DONE! ==");
                WriteLine("\n You got the word:");
                WriteLine("  - {0} -", word);
                if (mistakes == 0)
                {
                    WriteLine(" with no mistakes!");
                }
                else
                {
                    WriteLine(" with {0} mistakes!", mistakes);
                }

                ForegroundColor = ConsoleColor.White;
                WriteLine("\n Play again? (y/n)");
            }
            else
            {
                Clear();
                ForegroundColor = ConsoleColor.Red;
                WriteLine(" == BAD LUCK! ==");
                WriteLine("\n The word was:");
                WriteLine("  - {0} -", word);

                ForegroundColor = ConsoleColor.White;
                WriteLine("\n Try again? (y/n)");
            }  

            CursorVisible = true;
            Write(" >> ");
            if (ReadLine() != "y")
            {
                break;
            }
        }

        Clear();
        WriteLine("Thanks for playing!");
    }
}