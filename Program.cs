using System.Text;

enum Location
{
    Entrance,
    Lobby,
    Office,
    Kitchen
}

class Program
{
    static bool shouldQuit = false;
    static bool gameWon = false;

    static Location currentLocation = Location.Entrance;

    // -1 means the player has the item
    static int keyLocation = (int)Location.Office;

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        ShowTitle();
        ShowIntro();
        ShowLocation();

        while (!shouldQuit)
        {
            Console.Write("> ");
            string input = ReadCommand().ToLower().Trim();

            if (input == "")
            {
                Console.WriteLine("Please type a command.");
                Console.WriteLine();
                continue;
            }

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
            else if (input == "inventory" || input == "i")
            {
                ShowInventory();
            }
            else if (input.StartsWith("take "))
            {
                TakeItem(input);
            }
            else if (input.StartsWith("drop "))
            {
                DropItem(input);
            }
            else if (input == "use key")
            {
                UseKey();
            }
            else if (input == "north" || input == "n")
            {
                Move("north");
            }
            else if (input == "south" || input == "s")
            {
                Move("south");
            }
            else if (input == "east" || input == "e")
            {
                Move("east");
            }
            else if (input == "west" || input == "w")
            {
                Move("west");
            }
            else
            {
                Console.WriteLine("I don't understand that.");
                Console.WriteLine();
            }

            CheckWin();
        }
    }

    static void ShowLocation()
    {
        if (currentLocation == Location.Entrance)
        {
            Console.WriteLine("You are outside the building.");
            Console.WriteLine("The entrance leads north into the lobby.");
            Console.WriteLine("You can go: north");
        }
        else if (currentLocation == Location.Lobby)
        {
            Console.WriteLine("You are in the lobby.");
            Console.WriteLine("There is an office to the north and a kitchen to the west.");
            Console.WriteLine("The exit is back to the south.");
            Console.WriteLine("You can go: north, south, west");
        }
        else if (currentLocation == Location.Office)
        {
            Console.WriteLine("You are in a small office.");
            Console.WriteLine("There is a desk here.");

            if (keyLocation == (int)Location.Office)
            {
                Console.WriteLine("You see a key on the desk.");
                Console.WriteLine("It looks important. You could try 'take key'.");
            }

            Console.WriteLine("The lobby is back to the south.");
            Console.WriteLine("You can go: south");
        }
        else if (currentLocation == Location.Kitchen)
        {
            Console.WriteLine("You are in the kitchen.");
            Console.WriteLine("It smells like food.");

            if (!gameWon)
            {
                Console.WriteLine("There is a locked box here.");
                Console.WriteLine("Maybe you can open it somehow. If you have the key, try 'use key'.");
            }
            else
            {
                Console.WriteLine("The box is open.");
            }

            Console.WriteLine("The lobby is back to the east.");
            Console.WriteLine("You can go: east");
        }

        Console.WriteLine();
    }

    static void Move(string dir)
    {
        if (currentLocation == Location.Entrance && dir == "north")
        {
            currentLocation = Location.Lobby;
        }
        else if (currentLocation == Location.Lobby && dir == "south")
        {
            currentLocation = Location.Entrance;
        }
        else if (currentLocation == Location.Lobby && dir == "north")
        {
            currentLocation = Location.Office;
        }
        else if (currentLocation == Location.Lobby && dir == "west")
        {
            currentLocation = Location.Kitchen;
        }
        else if (currentLocation == Location.Office && dir == "south")
        {
            currentLocation = Location.Lobby;
        }
        else if (currentLocation == Location.Kitchen && dir == "east")
        {
            currentLocation = Location.Lobby;
        }
        else
        {
            Console.WriteLine("You can't go that way.");
            Console.WriteLine();
            return;
        }

        ShowLocation();
    }

    static void TakeItem(string input)
    {
        if (input.Contains("key"))
        {
            if (keyLocation == -1)
            {
                Console.WriteLine("You already have the key.");
                Console.WriteLine();
            }
            else if (keyLocation != (int)currentLocation)
            {
                Console.WriteLine("There is no key here.");
                Console.WriteLine();
            }
            else
            {
                keyLocation = -1;
                Console.WriteLine("You pick up the key.");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("You can't take that.");
            Console.WriteLine();
        }
    }

    static void DropItem(string input)
    {
        if (input.Contains("key"))
        {
            if (keyLocation != -1)
            {
                Console.WriteLine("You don't have the key.");
                Console.WriteLine();
            }
            else
            {
                keyLocation = (int)currentLocation;
                Console.WriteLine("You drop the key.");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("You can't drop that.");
            Console.WriteLine();
        }
    }

    static void ShowInventory()
    {
        Console.WriteLine("You are carrying:");

        if (keyLocation == -1)
        {
            Console.WriteLine("- key");
        }
        else
        {
            Console.WriteLine("Nothing");
        }

        Console.WriteLine();
    }

    static void UseKey()
    {
        if (keyLocation != -1)
        {
            Console.WriteLine("You don't have the key.");
            Console.WriteLine();
            return;
        }

        if (currentLocation != Location.Kitchen)
        {
            Console.WriteLine("There is nothing here to use the key on.");
            Console.WriteLine();
            return;
        }

        if (gameWon)
        {
            Console.WriteLine("The box is already open.");
            Console.WriteLine();
            return;
        }

        gameWon = true;

        Console.WriteLine("You unlock the box.");
        Console.WriteLine("Inside, you find...");
        Console.WriteLine("a single raw potato.");
        Console.WriteLine("There is also a note that says:");
        Console.WriteLine("\"Congratulations. You have unlocked disappointment.\"");
        Console.WriteLine();
    }

    static void CheckWin()
    {
        if (gameWon)
        {
            Console.WriteLine("You have completed the game.");
            Console.WriteLine("Thanks for playing!");
            Console.WriteLine();
            shouldQuit = true;
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
            Console.WriteLine("TEXT ADVENTURE");
        }

        Console.WriteLine();
        Console.WriteLine("Press any key...");
        Console.ReadKey(true);
        Console.Clear();
    }

    static void ShowIntro()
    {
        Console.WriteLine("You arrive at a strange place.");
        Console.WriteLine("Maybe there is something hidden here.");
        Console.WriteLine("Commands: look, north, south, east, west, take, drop, inventory, use key, quit");
        Console.WriteLine();
    }

    static void ShowHelp()
    {
        Console.WriteLine("Commands:");
        Console.WriteLine("look");
        Console.WriteLine("north, south, east, west");
        Console.WriteLine("take key");
        Console.WriteLine("drop key");
        Console.WriteLine("inventory");
        Console.WriteLine("use key");
        Console.WriteLine("quit");
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
                    Console.Write("\b \b");
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