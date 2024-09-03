namespace MorseCodeTranslator.Controller
{
    public class MorseCodeTranslatorController
    {
        Dictionary<string, string> dictionary;
        public MorseCodeTranslatorController()
        {
            dictionary = new Dictionary<string, string>()
            {
                {"a", ".-"},
                {"b", "-..."},
                {"c", "-.-."},
                {"d", "-.."},
                {"e", "."},
                {"f", "..-."},
                {"g", "--."},
                {"h", "...."},
                {"i", ".."},
                {"j", ".---"},
                {"k", "-.-"},
                {"l", ".-.."},
                {"m", "--"},
                {"n", "-."},
                {"o", "---"},
                {"p", ".--."},
                {"q", "--.-"},
                {"r", ".-."},
                {"s", "..."},
                {"t", "-"},
                {"u", "..-"},
                {"v", "...-"},
                {"w", ".--"},
                {"x", "-..-"},
                {"y", "-.--"},
                {"z", "--.."},

                {"0", "-----"},
                {"1", ".----"},
                {"2", "..---"},
                {"3", "...--"},
                {"4", "....-"},
                {"5", "....."},
                {"6", "-...."},
                {"7", "--..."},
                {"8", "---.."},
                {"9", "----."},

                {".", ".-.-.-"},
                {",", "--..--"},
                {"?", "..--.."},
                {"\'", ".----."},
                {"!", "-.-.--"},
                {"/", "-..-."},
                {"(", "-.--."},
                {")", "-.--.-"},
                {"&", ".-..."},
                {":", "---..."},
                {";", "-.-.-."},
                {"=", "-...-"},
                {"+", ".-.-."},
                {"-", "-....-"},
                {"_", "..--.-"},
                {"\"", ".-..-."},
                {"$", "...-..-"},
                {"@", ".--.-."}
            };
        }
        public void translate(String input)
        {
            input = input.ToLower();
            string output = "";

            if (input.Contains(".") && input.Contains("-"))
            {
                dictionary = dictionary.ToDictionary(x => x.Value, x => x.Key);
                string[] words = input.Split("   ");
                foreach (string word in words)
                {
                    string[] letters = word.Split(" ");
                    foreach (string letter in letters)
                    {
                        try
                        {
                            output += dictionary[letter];
                        }
                        catch (KeyNotFoundException)
                        {
                            output += "";
                        }

                    }
                    output += " ";
                }
            }
            else
            {
                foreach (char c in input)
                {
                    if (c == ' ')
                    {
                        output += "   ";
                    }
                    else
                    {
                        try
                        {

                            output += dictionary[Convert.ToString(c)] + " ";
                        }
                        catch (KeyNotFoundException)
                        {
                            output += "";
                        }
                    }
                }
            }
            Console.WriteLine(output);
        }
    }
}
