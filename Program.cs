using System.Text;

enum Location
{
    Entrance,
    Lobby,
    Office,
    Kitchen
}

enum Item
{
    Key
}

class Program
{
    static bool shouldQuit = false;
    static Location currentLocation = Location.Entrance;

    // var key är (-1 = inventory)
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
                Console.WriteLine("Please type a command.\n");
                continue;
            }

            if (input == "quit" || input == "exit" || input == "q")
            {
                Console.WriteLine("Goodbye!");
                shouldQuit = true;
            }
            else if (input == "help")
            {
                ShowHelp();
            }
            else if (input == "look")
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
                Console.WriteLine("I don't understand that.\n");
            }
        }
    }

    static void ShowLocation()
    {
        if (currentLocation == Location.Entrance)
        {
            Console.WriteLine("You are outside a building. You can go north.");
        }
        else if (currentLocation == Location.Lobby)
        {
            Console.WriteLine("You are in the lobby. You can go south, east, west.");
        }
        else if (currentLocation == Location.Office)
        {
            Console.WriteLine("You are in a small office. There is a desk here.");

            if (keyLocation == (int)Location.Office)
            {
                Console.WriteLine("You see a key on the desk.");
            }

            Console.WriteLine("You can go west.");
        }
        else if (currentLocation == Location.Kitchen)
        {
            Console.WriteLine("You are in the kitchen. It smells like food.");
            Console.WriteLine("You can go east.");
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
        else if (currentLocation == Location.Lobby && dir == "east")
        {
            currentLocation = Location.Office;
        }
        else if (currentLocation == Location.Lobby && dir == "west")
        {
            currentLocation = Location.Kitchen;
        }
        else if (currentLocation == Location.Office && dir == "west")
        {
            currentLocation = Location.Lobby;
        }
        else if (currentLocation == Location.Kitchen && dir == "east")
        {
            currentLocation = Location.Lobby;
        }
        else
        {
            Console.WriteLine("You can't go that way.\n");
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
                Console.WriteLine("You already have the key.\n");
            }
            else if (keyLocation != (int)currentLocation)
            {
                Console.WriteLine("There is no key here.\n");
            }
            else
            {
                keyLocation = -1;
                Console.WriteLine("You pick up the key.\n");
            }
        }
    }

    static void DropItem(string input)
    {
        if (input.Contains("key"))
        {
            if (keyLocation != -1)
            {
                Console.WriteLine("You don't have the key.\n");
            }
            else
            {
                keyLocation = (int)currentLocation;
                Console.WriteLine("You drop the key.\n");
            }
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

    static void ShowTitle()
    {
        Console.WriteLine("TEXT ADVENTURE\nPress any key...");
        Console.ReadKey(true);
        Console.Clear();
    }

    static void ShowIntro()
    {
        Console.WriteLine("You arrive at a strange place.");
        Console.WriteLine("Commands: look, move, take, drop, inventory, quit\n");
    }

    static void ShowHelp()
    {
        Console.WriteLine("Commands:");
        Console.WriteLine("look, north, south, east, west");
        Console.WriteLine("take [item], drop [item]");
        Console.WriteLine("inventory, quit\n");
    }

    static string ReadCommand()
    {
        StringBuilder text = new StringBuilder();

        while (true)
        {
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
                return "quit";

            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                return text.ToString();
            }

            if (key.Key == ConsoleKey.Backspace && text.Length > 0)
            {
                text.Length--;
                Console.Write("\b \b");
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