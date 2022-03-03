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
    }
}