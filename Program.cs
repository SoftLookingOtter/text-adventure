using System.Text;
using System.Text.RegularExpressions;

class Program
{
    const ConsoleColor TitleColor = ConsoleColor.White;
    const ConsoleColor NarrativeColor = ConsoleColor.Gray;
    const ConsoleColor PromptColor = ConsoleColor.White;
    const ConsoleColor ReplyColor = ConsoleColor.DarkGray;
    const int PrintPauseMilliseconds = 20;

    static bool ShouldQuit = false;

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.CursorVisible = true;

        DisplayTitle();
        DisplayIntro();

        while (!ShouldQuit)
        {
            HandlePlayerAction();
        }

        Console.ResetColor();
    }

    static void DisplayTitle()
    {
        Console.ForegroundColor = TitleColor;

        if (File.Exists("Title.txt"))
        {
            string title = File.ReadAllText("Title.txt");
            Console.WriteLine(title);
        }
        else
        {
            Console.WriteLine("========================");
            Console.WriteLine("     TEXT ADVENTURE");
            Console.WriteLine("========================");
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to start...");
        Console.ReadKey(true);
        Console.Clear();
    }

    static void DisplayIntro()
    {
        Console.ForegroundColor = NarrativeColor;

        Print("You arrive at the entrance of a strange and quiet place.");
        Print("Tonight, your goal is simple: explore and understand your surroundings.");
        Print();
        Print("Available commands: look, north, south, help, quit");
        Print("Press Esc at any time to quit.");
        Print();
    }

    static void HandlePlayerAction()
    {
        Console.ForegroundColor = NarrativeColor;
        Print("What do you want to do?");
        Print("Commands: look, north, south, help, quit");
        Print("Press Esc to exit.");

        Console.ForegroundColor = PromptColor;
        Console.Write("> ");

        string? command = ReadCommand();

        if (command == null)
        {
            Reply("Goodbye!");
            ShouldQuit = true;
            Print();
            return;
        }

        command = command.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(command))
        {
            Reply("Please enter a command.");
            Print();
            return;
        }

        string[] words = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string verb = words[0];

        switch (verb)
        {
            case "look":
            case "l":
                Reply("You take a careful look around.");
                break;

            case "north":
            case "n":
                Reply("You try going north.");
                break;

            case "south":
            case "s":
                Reply("You try going south.");
                break;

            case "help":
                ShowHelp();
                break;

            case "quit":
            case "exit":
            case "end":
            case "q":
                Reply("Goodbye!");
                ShouldQuit = true;
                break;

            default:
                Reply("I do not understand that command.");
                Reply("Type one of the listed commands or press Esc to quit.");
                break;
        }

        Print();
    }

    static string? ReadCommand()
    {
        StringBuilder builder = new StringBuilder();

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                Console.WriteLine();
                return null;
            }

            if (keyInfo.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                return builder.ToString();
            }

            if (keyInfo.Key == ConsoleKey.Backspace)
            {
                if (builder.Length > 0)
                {
                    builder.Length--;

                    if (Console.CursorLeft > 2)
                    {
                        Console.Write("\b \b");
                    }
                }

                continue;
            }

            char character = keyInfo.KeyChar;

            if (!char.IsControl(character))
            {
                builder.Append(character);
                Console.Write(character);
            }
        }
    }

    static void ShowHelp()
    {
        Reply("Available commands:");
        Reply("  look (l)   - look around");
        Reply("  north (n)  - try going north");
        Reply("  south (s)  - try going south");
        Reply("  help       - show this list");
        Reply("  quit (q)      - exit the game");
        Reply("  esc        - exit the game immediately");
    }

    static void Print()
    {
        Console.WriteLine();
    }

    static void Print(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine();
            return;
        }

        int maximumLineLength = Math.Max(20, Console.WindowWidth - 1);

        MatchCollection lineMatches = Regex.Matches(
            text,
            @"(.{1," + maximumLineLength + @"})(?:\s|$)"
        );

        foreach (Match match in lineMatches)
        {
            string line = match.Groups[0].Value.TrimEnd();

            if (!string.IsNullOrWhiteSpace(line))
            {
                Console.ForegroundColor = NarrativeColor;
                Console.WriteLine(line);
                Thread.Sleep(PrintPauseMilliseconds);
            }
        }
    }

    static void Reply(string text)
    {
        Console.ForegroundColor = ReplyColor;
        Print(text);
        Console.ForegroundColor = NarrativeColor;
    }
}