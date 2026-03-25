using System.Text;

enum Location
{
    Entrance,
    Lobby
}

class Program
{
    static bool shouldQuit = false;
    static Location currentLocation = Location.Entrance;

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        ShowTitle();
        ShowIntro();
        ShowLocation();

        while (!shouldQuit)
        {
            Console.Write("> ");
            string input = ReadCommand();

            if (input == "")
            {
                Console.WriteLine("Please type a command.");
                Console.WriteLine();
                continue;
            }

            input = input.ToLower().Trim();

            if (input == "quit" || input == "exit" || input == "end" || input == "q")
            {
                Console.WriteLine("Goodbye!");
                shouldQuit = true;
            }
            else if (input == "help")
            {
                ShowHelp();
            }
            else if (input == "look" || input == "l")
            {
                ShowLocation();
            }
            else if (input == "north" || input == "n")
            {
                Move("north");
            }
            else if (input == "south" || input == "s")
            {
                Move("south");
            }
            else
            {
                Console.WriteLine("I don't understand that command.");
                Console.WriteLine("Try: look, north, south, help, quit");
                Console.WriteLine();
            }
        }
    }

    static void ShowTitle()
    {
        if (File.Exists("Title.txt"))
        {
            Console.WriteLine(File.ReadAllText("Title.txt"));
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

    static void ShowIntro()
    {
        Console.WriteLine("You arrive at the entrance of a strange and quiet place.");
        Console.WriteLine("Tonight, your goal is simple: explore and understand your surroundings.");
        Console.WriteLine();
        Console.WriteLine("Commands: look, north, south, help, quit");
        Console.WriteLine();
    }

    static void ShowLocation()
    {
        if (currentLocation == Location.Entrance)
        {
            Console.WriteLine("You are standing outside a quiet building.");
            Console.WriteLine("A doorway leads north into the lobby.");
            Console.WriteLine("You can go: north");
        }
        else if (currentLocation == Location.Lobby)
        {
            Console.WriteLine("You are inside the lobby.");
            Console.WriteLine("It is still and quiet here.");
            Console.WriteLine("The exit is back to the south.");
            Console.WriteLine("You can go: south");
        }

        Console.WriteLine();
    }

    static void Move(string direction)
    {
        if (currentLocation == Location.Entrance)
        {
            if (direction == "north")
            {
                currentLocation = Location.Lobby;
                ShowLocation();
            }
            else
            {
                Console.WriteLine("You can't go that way.");
                Console.WriteLine();
            }
        }
        else if (currentLocation == Location.Lobby)
        {
            if (direction == "south")
            {
                currentLocation = Location.Entrance;
                ShowLocation();
            }
            else
            {
                Console.WriteLine("You can't go that way.");
                Console.WriteLine();
            }
        }
    }

    static void ShowHelp()
    {
        Console.WriteLine("Available commands:");
        Console.WriteLine("  look  - look around");
        Console.WriteLine("  north - move north");
        Console.WriteLine("  south - move south");
        Console.WriteLine("  help  - show commands");
        Console.WriteLine("  quit  - exit the game");
        Console.WriteLine();
    }

    static string ReadCommand()
    {
        StringBuilder text = new StringBuilder();

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine();
                return "quit";
            }

            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                return text.ToString();
            }

            if (key.Key == ConsoleKey.Backspace)
            {
                if (text.Length > 0)
                {
                    text.Length--;

                    if (Console.CursorLeft > 0)
                    {
                        Console.Write("\b \b");
                    }
                }

                continue;
            }

            if (!char.IsControl(key.KeyChar))
            {
                text.Append(key.KeyChar);
                Console.Write(key.KeyChar);
            }
        }
    }
}