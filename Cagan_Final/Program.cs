using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

public class Game
{
    private bool ClueFound = false;
    private bool Room1Unlocked = false;
    private bool Room2Unlocked = false;
    private Enemy enemy;
    private List<string> inventory = new List<string>();
    private string currentRoom = "Lobby";
    private int playerHealth = 100;
    private DateTime startTime;
    private TimeSpan countdownDuration = TimeSpan.FromMinutes(10); 
    private string saveFileName = "game_save.txt";

    public Game()
    {
        InitializeGame();
        enemy = new Enemy("Enemy", 50, 25, "Room 2 Key");
    }

    private void InitializeGame()
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Clear();
        ShowInitialAsciiArt(); 
        Console.WriteLine("Welcome to the Bomb Escape Game, Mr. Onur.");
        Console.WriteLine("Please press a button to start the game - ATTENTION! -When the button is pressed and the game starts playing, a bomb countdown begins!!!--");
        Console.ReadKey();
        startTime = DateTime.Now;
        Console.Clear();
        Console.WriteLine("Good luck racing against time ;D");
        AnimateTitle();
        ShowAsciiArt("main");
        EnterRoom(currentRoom);
    }

    private void ShowInitialAsciiArt()
    {
        Console.WriteLine(@"
         . . .                         
          \|/                          
        `--+--'                        
          /|\                          
         ' | '                         
           |                           
           |                           
       ,--'#`--.                       
       |#######|                       
    _.-'#######`-._                    
 ,-'###############`-.                 
,'#####################`,               
/#########################\              
|###########################|             
|#############################|            
|#############################|            
|#############################|            
|#############################|            
 |###########################|             
  \#########################/              
   `.#####################,'               
     `._###############_,'                 
        `--..#####..--'");
    }

    private void AnimateTitle()
    {
        string title = "Riddle Room";
        for (int i = 0; i < title.Length; i++)
        {
            Console.Write(title[i]);
            Thread.Sleep(100);
        }
        Console.WriteLine();
    }


    private void ShowAsciiArt(string room)
    {
        switch (room)
        {
            case "main":
                Console.WriteLine(@"                              Y\     /Y
                              | \ _ / |
        _____                 | =(_)= |
    ,-~""     ""~-.           ,-~\/^ ^\/~-.
  ,^ ___     ___ ^.       ,^ ___     ___ ^.
 / .^   ^. .^   ^. \     / .^   ^. .^   ^. \
Y  l    O! l    O!  Y   Y  lo    ! lo    !  Y
l_ `.___.' `.___.' _[   l_ `.___.' `.___.' _[
l^~""-------------""~^I   l^~""-------------""~^I
!\,               ,/!   !                   !
 \ ~-.,_______,.-~ /     \                 /
  ^.             .^       ^.             .^    -Row
    ""-.._____.,-""           ""-.._____.,-""

               ->Mr&MrsPacman<-");
                break;
            case "room1":
            case "room2":
                Console.WriteLine(@"             ___  
         . -     - .
       /   o   o   \
      |   o   o   o   |
      |  o   o   o   o  |
       \   o   o   /
         ' -_____-'   ");
                break;
            case "enemy":
                Console.WriteLine(@"           /\_[]_/\
              |] _||_ [|
       ___     \/ || \/
      /___\       ||
     (|0 0|)      ||
   __/{\U/}\_ ___/vvv
  / \  {~}   / _|_P|
  | /\  ~   /_/   []
  |_| (____)        
  \_]/______\        -edias-
     _\_||_/_           
snd (_,_||_,_)");
                break;
            case "npc":
                Console.WriteLine(@" --  :( -- :)  -- ");
                break;
        }
    }

    private void ChangeConsoleColorForRoom(string roomName)
    {
        switch (roomName)
        {
            case "Lobby":
                Console.BackgroundColor = ConsoleColor.Black;
                break;
            case "Room 1":
            case "Room 2":
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                break;
            case "NPC Room":
                Console.BackgroundColor = ConsoleColor.DarkRed;
                break;
            case "Item Search Room":
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                break;
            default:
                Console.BackgroundColor = ConsoleColor.Black;
                break;
        }
        Console.Clear();
    }

    public void Start()
    {
        bool gameRunning = true;
        while (gameRunning)
        {
            DisplayInventory();
            Console.WriteLine($"Current location: {currentRoom}");
            Console.WriteLine($"\nTime remaining: {countdownDuration - (DateTime.Now - startTime)}");
            Console.WriteLine("\nChoose an action:");
            Console.WriteLine("WASD to move, F to interact, Q to quit, R to fight the enemy, G to leave the room, E to save and exit");

            var key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.W:
                    AttemptMove("Room 1");
                    break;
                case ConsoleKey.A:
                    AttemptMove("NPC Room");
                    break;
                case ConsoleKey.S:
                    AttemptMove("Room 2");
                    break;
                case ConsoleKey.D:
                    AttemptMove("Item Search Room");
                    break;
                case ConsoleKey.F:
                    Interact();
                    break;
                case ConsoleKey.Q:
                    gameRunning = false;
                    Console.WriteLine("Thank you for playing!");
                    break;
                case ConsoleKey.R:
                    StartFight();
                    break;
                case ConsoleKey.G:
                    LeaveRoom();
                    break;
                case ConsoleKey.E:
                    SaveGame();
                    gameRunning = false;
                    Console.WriteLine("Game saved. Thank you for playing!");
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            if (DateTime.Now - startTime > countdownDuration)
            {
                Console.Clear();
                Console.WriteLine("THE BOMB EXPLODED BOOOOM!!*****!! !");
                gameRunning = false;
            }
        }
    }

    private void AttemptMove(string destination)
    {
        if (currentRoom != "Lobby" && destination != "Lobby")
        {
            Console.WriteLine("You need to leave this room first.");
            return;
        }

        EnterRoom(destination);
    }

    private void EnterRoom(string roomName)
    {
        ChangeConsoleColorForRoom(roomName);
        Console.WriteLine($"You approach {roomName}. Do you want to enter? (Y/N)");
        var input = Console.ReadKey();
        if (input.Key == ConsoleKey.Y)
        {
            currentRoom = roomName;
            Console.Clear();
            AnimateTransition(roomName);
            ShowAsciiArt(roomName.Equals("NPC Room") ? "npc" : roomName.ToLower());
            if (roomName == "Room 1" && !Room1Unlocked)
            {
                Console.WriteLine("Room 1 is locked. Find the Room 1 Key to enter.");
                currentRoom = "Lobby";
            }
            else if (roomName == "Room 2" && !Room2Unlocked)
            {
                Console.WriteLine("Room 2 is locked. Defeat the enemy to get the Room 2 Key.");
                currentRoom = "Lobby";
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Going back to the lobby...");
        }
    }

    private void AnimateTransition(string roomName)
    {
        Console.WriteLine($"Entering {currentRoom}...");
        Console.Write("Loading");
        for (int i = 0; i < 3; i++)
        {
            Thread.Sleep(500);
            Console.Write(".");
        }
        Console.WriteLine();
    }

    private void LeaveRoom()
    {
        currentRoom = "Lobby";
        Console.Clear();
        Console.WriteLine("Leaving the room...");
    }

    private void Interact()
    {
        switch (currentRoom)
        {
            case "Room 1":
                LookAround();
                break;
            case "Room 2":
                LookAround2();
                break;
            case "NPC Room":
                TalktoNPC();
                break;
            case "Item Search Room":
                SearchForItems();
                break;
            default:
                Console.WriteLine("There is nothing to interact with here.");
                break;
        }
    }

    private void LookAround()
    {
        Console.WriteLine("You look around the room...");
        Console.WriteLine("There are....");
        Console.WriteLine("1- a bed");
        Console.WriteLine("2- a desk");
        Console.WriteLine("3- a batman costume");
        Console.WriteLine("4- a broken sink");
        var item = Console.ReadLine();
        switch (item)
        {
            case "3":
                ClueFound = true;
                Console.WriteLine("Congratulations, you found the clue! Now, take another look at the riddle.");
                ClueFoundChangeBackground();
                break;
            default:
                Console.WriteLine("There is nothing of interest here.");
                break;
        }
    }

    private void LookAround2()
    {
        Console.WriteLine("You look around the second room...");
        Console.WriteLine("There are....");
        Console.WriteLine("1- a painting");
        Console.WriteLine("2- a chair");
        Console.WriteLine("3- a bookshelf");
        Console.WriteLine("4- a window");
        Console.ReadLine();
    }

    private void TalktoNPC()
    {
        Console.WriteLine("It flies but it is not a bird?");
        Console.WriteLine("1-Chicken");
        Console.WriteLine("2-Donkey");
        Console.WriteLine("3-Eagle");
        Console.WriteLine("4-Dinosaur");
        if (ClueFound)
        {
            Console.WriteLine("5-Bat***");
        }

        int attempt = 2;
        while (attempt > 0)
        {
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                    Console.WriteLine("Wrong!");
                    attempt--;
                    break;
                case "5":
                    if (ClueFound)
                    {
                        Console.WriteLine("Correct! You Win :)");
                        return;
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            if (attempt == 0)
            {
                Console.WriteLine("You have been killed by the NPC!!!!!!");
                return;
            }
        }
    }

    private void StartFight()
    {
        if (enemy == null || !enemy.IsAlive())
        {
            enemy = new Enemy("Enemy", 50, 25, "Room 2 Key");
            Console.WriteLine($"A wild {enemy.Name} appears!");
            ShowAsciiArt("enemy");
        }
        else
        {
            Console.WriteLine($"You start fighting the {enemy.Name}!");
            ShowAsciiArt("enemy");
        }

        Combat();
    }

    private void Combat()
    {
        while (playerHealth > 0 && enemy.Health > 0)
        {
            Console.WriteLine($"Your health: {playerHealth}, Enemy's health: {enemy.Health}");
            Console.WriteLine("Choose an action: \n1. Attack");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine($"You attack the {enemy.Name}!");
                enemy.Health -= 25; 

                if (enemy.Health <= 0)
                {
                    Console.WriteLine($"You defeated the {enemy.Name}!");
                    if (!Room2Unlocked)
                    {
                        Room2Unlocked = true;
                        inventory.Add(enemy.DropItem);
                        Console.WriteLine("You have obtained the Room 2 Key!");
                    }
                    enemy = null;
                    return;
                }
                else
                {
                    playerHealth -= enemy.AttackPower; 
                    Console.WriteLine($"The {enemy.Name} attacks you!");
                }
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }

            if (playerHealth <= 0)
            {
                Console.WriteLine("You have been defeated!");
                return;
            }
        }
    }

    private void SearchForItems()
    {
        Console.WriteLine("You found some items!");
        Console.WriteLine("1- A sword");
        Console.WriteLine("2- A shield");
        Console.WriteLine("3- A potion");
        Console.WriteLine("4- Room 1 key");
        Console.WriteLine("5- A map");
        Console.WriteLine("Choose an item to pick up:");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                inventory.Add("Sword");
                break;
            case "2":
                inventory.Add("Shield");
                break;
            case "3":
                inventory.Add("Potion");
                break;
            case "4":
                inventory.Add("Room 1 Key");
                Room1Unlocked = true;
                break;
            case "5":
                inventory.Add("Map");
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    private void DisplayInventory()
    {
        Console.WriteLine("Your Inventory:");
        foreach (var item in inventory)
        {
            Console.WriteLine($"- {item}");
        }
    }

    private void ClueFoundChangeBackground()
    {
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.Clear();
        InitializeGame();
    }

    private void SaveGame()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(saveFileName))
            {
                writer.WriteLine(ClueFound);
                writer.WriteLine(Room1Unlocked);
                writer.WriteLine(Room2Unlocked);
                writer.WriteLine(playerHealth);
                writer.WriteLine(currentRoom);

                writer.WriteLine(inventory.Count);
                foreach (var item in inventory)
                {
                    writer.WriteLine(item);
                }

                if (enemy != null)
                {
                    writer.WriteLine(enemy.Name);
                    writer.WriteLine(enemy.Health);
                    writer.WriteLine(enemy.AttackPower);
                    writer.WriteLine(enemy.DropItem);
                }
                else
                {
                    writer.WriteLine("null");
                }

                Console.WriteLine("Game saved successfully!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error while saving the game: " + ex.Message);
        }
    }

    private void LoadGame()
    {
        if (File.Exists(saveFileName))
        {
            try
            {
                using (StreamReader reader = new StreamReader(saveFileName))
                {
                    ClueFound = bool.Parse(reader.ReadLine());
                    Room1Unlocked = bool.Parse(reader.ReadLine());
                    Room2Unlocked = bool.Parse(reader.ReadLine());
                    playerHealth = int.Parse(reader.ReadLine());
                    currentRoom = reader.ReadLine();

                    int inventoryCount = int.Parse(reader.ReadLine());
                    inventory.Clear();
                    for (int i = 0; i < inventoryCount; i++)
                    {
                        inventory.Add(reader.ReadLine());
                    }

                    string enemyName = reader.ReadLine();
                    if (enemyName != "null")
                    {
                        int enemyHealth = int.Parse(reader.ReadLine());
                        int enemyAttackPower = int.Parse(reader.ReadLine());
                        string enemyDropItem = reader.ReadLine();
                        enemy = new Enemy(enemyName, enemyHealth, enemyAttackPower, enemyDropItem);
                    }
                    else
                    {
                        enemy = null;
                    }

                    Console.WriteLine("Game loaded successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while loading the game: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("No saved game found.");
        }
    }

    public static void Main(string[] args)
    {
        Game game = new Game();
        game.Start();
    }
}

public class Enemy
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int AttackPower { get; set; }
    public string DropItem { get; set; }

    public Enemy(string name, int health, int attackPower, string dropItem)
    {
        Name = name;
        Health = health;
        AttackPower = attackPower;
        DropItem = dropItem;
    }

    public bool IsAlive()
    {
        return Health > 0;
    }
}

